using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XModule.Standard;
using XModule.Unit;
using static XModule.Unit.C_LibraryGlobal;

namespace XModule
{
    public class DisplaceSensor
    {

        //ERRMSG
        public delegate void ErrorMsg_EventHandler(string errtitle, string errmsg);
        public event ErrorMsg_EventHandler ErrorMsg_Event = null;
        //ReiveMSG
        public delegate void Recivemsg_EventHandler(string msg);
        public event Recivemsg_EventHandler Recivemsg_Event = null;

        public const char STX = (char)0x4D;
        public const char ETX = (char)0x03;
        public const char CR = (char)0x15;

        public ComPort SPort;
        ComPort_Properties Portprop;

        string strReceiveData = "";
        bool FisConnected = false;
        public string strValue;
        public double Value = 0;

        enum Displace_CMD
        {
            STATUS_CHECK
,
        };

        public DisplaceSensor()
        {
        }
        
        #region Connect
        public bool Init(string strPortName)
        {
            bool result = false;

            //파라미터 로드
            Portprop = new ComPort_Properties();
            //Portprop.strPortName = cDEF.Work.LoadCell.SerialPortName;
            Portprop.strPortName = strPortName;
            Portprop.iBaudRate = 9600;
            Portprop.iDataBits = 8;
            Portprop.iReadBuferSize = 20;
            Portprop.iWriteBuferSize = 1024;
            Portprop.ReciveWaitTime = 10000; //응답 타임 아웃 
            Portprop.LoopWatitTime = 1000;
            Portprop.bsendtypestr = true; //전송 데이터가 문자열 인지, hex 인지
            Portprop.reciveuse = true; //응답 받고 다음 전송 아니면 그냥 전송
            Portprop.iniFilepath = "loop";

            SPort = new ComPort(Portprop);

            //전체 등록
            SPort.AddCommandRegister(Displace_CMD.STATUS_CHECK.ToString(), SendData());
            //Loop등록
            SPort.AddLoopCommandRegister(Displace_CMD.STATUS_CHECK.ToString());

            result = Connected();
            return result;
        }

        public bool IsConnected()
        {
         
            return FisConnected;
        }
        public bool Connected()
        {
            SPort.Recivemsg_Event += Com_Recive_Data;
            SPort.ErrorMsg_Event += Com_ErrMsgShow;
            bool result = SPort.PortOpen();

            return result;
        }
        public bool DeConnected()
        {
            bool result = SPort.PortClose();
            SPort.Recivemsg_Event -= Com_Recive_Data;
            SPort.ErrorMsg_Event -= Com_ErrMsgShow;
            return result;
        }


        //에러 메시지
        public void ErrMsg(string errtitle, string errmsg)
        {
            if (ErrorMsg_Event != null)
                ErrorMsg_Event(errtitle, errmsg);
        }

        //응답 메시지 폼에 표시
        public void ReciveMsg(string recivemsg)
        {
            if (Recivemsg_Event != null)
                Recivemsg_Event(recivemsg);
        }
        #endregion

        #region ReciveData

        private void Com_ErrMsgShow(string errtitle, string errmsg)
        {
            ErrMsg(errtitle, errmsg);
        }

        /// <summary>
        /// Recive Data
        /// </summary>
        /// <param name="msg"></param>
        private void Com_Recive_Data(string msg)
        {

            // stx   //etx 일때만 취급하자
            strReceiveData += msg;
            int stxIndex = strReceiveData.IndexOf("M1");
            int extIndex = strReceiveData.IndexOf("\r");

            string strSendClientPacket = "";
            if (stxIndex >= 0 && extIndex > 0 && extIndex > stxIndex)
            {
                strSendClientPacket = ClientSendPreparseMessage(strReceiveData);
                ParseMessage(strSendClientPacket);
                FisConnected = true;
                strReceiveData = "";// PacketClear(strReceiveData);
                strReceiveData = "";
            }
            else if(extIndex > 0)
            {
                strReceiveData = strReceiveData.Substring(extIndex + 1);
            }
            //strReceiveData = "";



            //if (strReceiveData.IndexOf("M1") >= 0 && strReceiveData.IndexOf("M1") < 30)
            //{
            //    while (strReceiveData.IndexOf(STX, 0) != -1)
            //    {
            //        string strTemp = msg;
            //        string strSendClientPacket = "";
            //        strSendClientPacket = ClientSendPreparseMessage(strTemp);
            //        ParseMessage(strSendClientPacket);
            //        strReceiveData = "";// PacketClear(strReceiveData);
            //        FisConnected = true;
            //    }

            //    //Trace.WriteLine(string.Format("m_strReceiveData : {0}", m_strReceiveData));
            //    strReceiveData = "";
            //}
            //// 로그 기록
            //ReciveMsg(msg);
            //msg = "";
        }

        public void ParseMessage(string strMsg)
        {
            if (strMsg.Length < 8)
                return;

            while(strMsg.IndexOf("\0") > 0)
            {
               strMsg =  strMsg.Replace("\0", "");
            }

            int nExt = 0;
            string[] sArr = strMsg.Split(',');
            if (sArr[1] != null)
            {
                nExt = sArr[1].IndexOf('F');
                if (nExt > 0)
                {
                    Value = 0;
                    strValue = "FFFFFF";
                }
                else
                {
                    Value = Convert.ToDouble(sArr[1]);
                    strValue = Value.ToString();
                }
            }


        }

        public string PacketClear(string strMsg)
        {
            // 2개 이상 연달아 붙어 나올 것을 대비해 맨 앞 하나를 지운다.
            int nNextIndex = 0;
            nNextIndex = strMsg.IndexOf(STX, 1);

            if (nNextIndex == -1)
                strMsg = "";
            else if (nNextIndex > 0)
                strMsg = strMsg.Remove(0, nNextIndex);

            return strMsg;
        }

        public string ClientSendPreparseMessage(string strMsg)
        {
            int nIndexSTX = strMsg.IndexOf("M1");			// STX(시작위치)를 찾는다.
            int nIndexETX = 0;
            if(strMsg.IndexOf("\r") >= 0)
            {
                nIndexETX = strMsg.IndexOf("\r");
            }
            else if(strMsg.IndexOf("FFFF") > 0)
            {
                nIndexETX = strMsg.IndexOf("FFFF");
                nIndexETX += 4;
            }
            string strPacketData = "";
            try
            {
                strPacketData = strMsg.Substring(nIndexSTX, nIndexSTX + nIndexETX);
            }
            catch
            {
                strPacketData = "";
            }

            return strPacketData;
        }

        #endregion

        #region SendData
        private string SendData()
        {
            byte[] sendBuffer = new byte[10];
            sendBuffer[0] = 0x4D;
            sendBuffer[1] = 0x31;
            sendBuffer[2] = 0x0D;
            return Encoding.Default.GetString(sendBuffer);
        }
        
        #endregion

    }
}

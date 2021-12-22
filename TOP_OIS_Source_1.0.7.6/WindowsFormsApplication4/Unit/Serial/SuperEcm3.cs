using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XModule.Standard;
using XModule.Unit;

namespace XModule.Unit
{
    public class clsSuperEcm3:BaseData
    {
        private string section;
        //ERRMSG
        public delegate void ErrorMsg_EventHandler(string errtitle, string errmsg);
        public event ErrorMsg_EventHandler ErrorMsg_Event = null;
        //ReiveMSG
        public delegate void Recivemsg_EventHandler(string msg);
        public event Recivemsg_EventHandler Recivemsg_Event = null;

        public delegate void RecivemsgByte_EventHandler(byte[] msg);
        public event RecivemsgByte_EventHandler RecivemsgByte_Event = null;


        public ComPort SPort;
        C_LibraryGlobal.ComPort_Properties Portprop;

        private Thread thread;
        public int Step = 0;
        private int FCalc = 0;

        string strReceiveData = "";
        bool FisConnected = false;
        public string strValue;

        public EWriteRecive ParamWriteRecive;
        public EWriteRecive ADJWriteRecive;
        public ESystemStatus Status;
        public ECMDMode CMDMode = ECMDMode.ChangeMode;
        public bool CommError;

        private bool Ack = false;
        private bool Reply = false;
        public bool SetValueFinish = false;
        public int SetMode = 0;   // 0: Time   , 1: Manual
        #region PraraReadValue
        public int PressValue = 0;
        public int PressTime = 1;
        public int VacValue = 0;

        private string Name;
        #endregion

        public clsSuperEcm3(string name) : base("SuperEcm")
        {
            if (cDEF.Work.DispSensor.DispenserType != 2)
            {
                thread = new Thread(Execute);
                thread.Start();
                Step = 0;
                Name = name;
                Load();
            }
        }
        public enum EWriteRecive
        {
            Success,
            Fail
        };
        public enum ECMDMode
        {
            ChangeMode,
            SetValue
        };
        public enum ESystemStatus
        {
            None,
            StandBy, //대기
            Operation, //작업
            Fault, //고장
            Nozzle_Calibration, //노즐 Calibration
            Parameter_Setting, //파라미터 설정
            Maintenance_Required, //유지보수 필요
        };
        public void SetValueStart()
        {
            SetValueFinish = false;
            Step = 1;
        }
        private void Execute()
        {
            while(true)
            {
                switch(Step)
                {
                    case 0:
                        // 대기
                        break;

                    case 1:
                        Ack = false;
                        Reply = false;
                        CommError = false;
                        
                        SendReq();
                        FCalc = Environment.TickCount;
                        Step++;
                        break;

                    case 2:
                        if(Environment.TickCount - FCalc < 5000)
                        {
                            if (!Ack)
                                break;
                            Step++;
                        }
                        else
                        {
                            CommError = true;
                            Step = 5;
                        }
                        break;

                    case 3:
                        SendCommand(CMDMode);
                        FCalc = Environment.TickCount;
                        Step++;
                        break;

                    case 4:
                        if (Environment.TickCount - FCalc < 5000)
                        {
                            if (!Reply)
                                break;
                            Step++;
                        }
                        else
                        {
                            CommError = true;
                            Step++;
                        }
                        break;

                    case 5:
                        SetValueFinish = true;
                        SendComplete();
                        Step = 0;
                        break;

                }
                Thread.Sleep(1);
            }
        }
        private void SendCommand(ECMDMode cmdMode)
        {
            switch(cmdMode)
            {
                case ECMDMode.ChangeMode:
                    SendChangeMode(SetMode);
                    break;

                case ECMDMode.SetValue:
                    SendPR(PressValue, PressTime, VacValue);
                    break;

            }
        }
        #region Connect
        public bool Init(string strPortName)
        {
            if (strPortName == "")            
                return false;

            bool result = false;

            //파라미터 로드

            Portprop = new C_LibraryGlobal.ComPort_Properties();
            Portprop.strPortName = strPortName;
            Portprop.iBaudRate = 38400;
            //Portprop.strStopBits = "1";
            //Portprop.strPstrarity = "None";
            Portprop.iDataBits = 8;
            Portprop.iReadBuferSize = 2048;
            Portprop.iWriteBuferSize = 2048;
            Portprop.ReciveWaitTime = 10000; //응답 타임 아웃 
            Portprop.LoopWatitTime = 1000;
            Portprop.bsendtypestr = true; //전송 데이터가 문자열 인지, hex 인지
            Portprop.reciveuse = true; //응답 받고 다음 전송 아니면 그냥 전송
            Portprop.iniFilepath = null;

            //ReciveByte = new byte[Portprop.iReadBuferSize];

            SPort = new ComPort(Portprop);

            result = Connected();
            FisConnected = result;
            return result;
        }

        public bool IsConnected()
        {
          
            return FisConnected;
        }
        public bool Connected()
        {
            SPort.ErrorMsg_Event += Com_ErrMsgShow;
            SPort.RecivemsgByte_Event += Com_Recive_DataByte;
            bool result = SPort.PortOpen();

            return result;
        }
        public bool DeConnected()
        {
            bool result = SPort.PortClose();
            SPort.ErrorMsg_Event -= Com_ErrMsgShow;
            SPort.RecivemsgByte_Event -= Com_Recive_DataByte;
            FisConnected = result;
            return result;
        }


        //에러 메시지
        public void ErrMsg(string errtitle, string errmsg)
        {
            try
            {
                if (ErrorMsg_Event != null)
                    ErrorMsg_Event(errtitle, errmsg);
            }
            catch (Exception)
            {
            }

        }

        //응답 메시지 폼에 표시
        public void ReciveMsg(string recivemsg)
        {
            try
            {
                if (Recivemsg_Event != null)
                    Recivemsg_Event(recivemsg);
            }
            catch (Exception)
            {

            }

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
        private void Com_Recive_DataByte(byte[] msg)
        {
            string strchkHex = string.Empty;
            strReceiveData = string.Empty;
            for (int i = 0; i < msg.Length; i++)
            {
                strReceiveData += msg[i].ToString();
            }
            // 48 : 0     50: 2   65 : A    50 : 2  50 : 2  66: B
            switch(strReceiveData)
            {
                case "6":
                    Ack = true;
                    break;

                case "24850654850683":  //OK
                    Reply = true;
                    break;

                case "24850655050663":  //NG
                    Reply = false;
                    break;




            }
            ReciveMsg(strReceiveData);
            FisConnected = true;
        }
        public static byte CheckSum(byte[] array)
        {
            return array.Aggregate<byte, byte>(0, (current, b) => (byte)((current - b) & 0xff));
        }
        #endregion

        #region SendData
        private void SendReq()
        {
            byte[] senddata = { 0x05 };
            SPort.DataWriteByte(senddata);
        }
        private void SendComplete()
        {
            byte[] senddata = { 0x04 };
            SPort.DataWriteByte(senddata);
        }
        private void SendPR(int P, int T, int V)
        {
            List<byte> senddata = new List<byte>();
            senddata.Clear();
            char stx = (char)0x2;
            char sp = (char)0x20;
            char etx = (char)0x03;

            if (T < 1)
                T = 1;

            string tmpCmd = $"PR{sp}{sp}P{P.ToString("0000")}T{T.ToString("0000")}V-{V.ToString("0000")}";
            byte[] strTmpByte = Encoding.ASCII.GetBytes(tmpCmd);

            string strLen = strTmpByte.Length.ToString("X2");
            string strCmd = $"{strLen}{tmpCmd}";
            byte[] strCmdByte = Encoding.ASCII.GetBytes(strCmd);

            string chkSum = CheckSum(strCmdByte).ToString("X2");

            string strCmdLast = $"{stx}{strLen}{tmpCmd}{chkSum}{etx}";
            byte[] ByteCmdLast = Encoding.ASCII.GetBytes(strCmdLast);
            SPort.DataWriteByte(ByteCmdLast.ToArray());

        }
        private void SendChangeMode(int Mode)
        {
            char stx = (char)0x2;
            char etx = (char)0x03;

            string tmpCmd = $"TM0{Mode.ToString("0")}";
            byte[] strTmpByte = Encoding.ASCII.GetBytes(tmpCmd);
            string strLen = strTmpByte.Length.ToString("X2");
            string strCmd = $"{strLen}{tmpCmd}";
            byte[] strCmdByte = Encoding.ASCII.GetBytes(strCmd);

            string chkSum = CheckSum(strCmdByte).ToString("X2");

            string strCmdLast = $"{stx}{strLen}{tmpCmd}{chkSum}{etx}";
            byte[] ByteCmdLast = Encoding.ASCII.GetBytes(strCmdLast);
            SPort.DataWriteByte(ByteCmdLast.ToArray());
        }
        #endregion

        public override void DataProc(fpIni.IniType type)
        {
            section = Name;

            ini.DataProc(type, section, "PressTime", ref PressTime);
            ini.DataProc(type, section, "PressValue", ref PressValue);
            ini.DataProc(type, section, "VacValue", ref VacValue);
        }
    }
}

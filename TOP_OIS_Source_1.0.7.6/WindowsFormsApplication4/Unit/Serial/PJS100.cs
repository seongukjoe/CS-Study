using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XModule.Standard;
using XModule.Unit;

namespace XModule.Unit
{
    public class PJS100
    {
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
        byte[] ReciveByte;
        int ReciveByteSize = 0;

        string strReceiveData = "";
        bool FisConnected = false;
        public string strValue;

        public EWriteRecive ParamWriteRecive;
        public EWriteRecive ADJWriteRecive;
        public ESystemStatus Status;
        public int SystemErrorCode;
        public int CalibrationValue_Hi;
        public int CalibrationValue_Low;

        #region PraraReadValue
        public double RTValue, HTValue, FTValue, DelayValue;
        public double PCTValue, PluseNumValue, VoltageValue;
        public EWorkMode WorkModeValue = EWorkMode.None;
        #endregion

        public PJS100()
        {

        }
        public enum EWriteRecive
        {
            Success,
            Fail
        };
        public enum EWorkMode
        {
            None,
            Point,
            Line,
            Clear
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

        #region Connect
        public bool Init(string strPortName)
        {


            if (strPortName == "")            
                return false;

            bool result = false;

            //파라미터 로드

            Portprop = new C_LibraryGlobal.ComPort_Properties();
            Portprop.strPortName = strPortName;
            Portprop.iBaudRate = 115200;
            Portprop.iDataBits = 8;
            Portprop.iReadBuferSize = 2048;
            Portprop.iWriteBuferSize = 2048;
            Portprop.ReciveWaitTime = 10000; //응답 타임 아웃 
            Portprop.LoopWatitTime = 1000;
            Portprop.bsendtypestr = true; //전송 데이터가 문자열 인지, hex 인지
            Portprop.reciveuse = true; //응답 받고 다음 전송 아니면 그냥 전송
            Portprop.iniFilepath = null;

            ReciveByte = new byte[Portprop.iReadBuferSize];

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
            SPort.Recivemsg_Event += Com_Recive_Data;
            SPort.ErrorMsg_Event += Com_ErrMsgShow;
            SPort.RecivemsgByte_Event += Com_Recive_DataByte;
            bool result = SPort.PortOpen();

            return result;
        }
        public bool DeConnected()
        {
            bool result = SPort.PortClose();
            SPort.Recivemsg_Event -= Com_Recive_Data;
            SPort.ErrorMsg_Event -= Com_ErrMsgShow;
            SPort.RecivemsgByte_Event -= Com_Recive_DataByte;
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
        private void Com_Recive_Data(string msg)
        {

            //strReceiveData += msg;
            //string ReceiveData = "";
            //if (strReceiveData.IndexOf(ETX, 0) >= 0)
            //{
            //    ReceiveData = strReceiveData;
            //    while (strReceiveData.IndexOf(STX, 0) != -1)
            //    {
            //        string strSendClientPacket = "";
            //        strSendClientPacket = ClientSendPreparseMessage(strReceiveData);
            //        ParseMessage(strSendClientPacket);
            //        strReceiveData = PacketClear(strReceiveData);
            //        FisConnected = true;
            //    }

            //    //Trace.WriteLine(string.Format("m_strReceiveData : {0}", m_strReceiveData));
            //    strReceiveData = "";
            //}
            // 로그 기록
            try
            {
                ReciveMsg(msg);
            }
            catch (Exception ex)
            {

                ReciveMsg(ex.ToString());
            }
            FisConnected = true;
        }



        /// <summary>
        /// Recive Data
        /// </summary>
        /// <param name="msg"></param>
        private void Com_Recive_DataByte(byte[] msg)
        {
            string strchkHex = string.Empty;
            for (int i = 0; i < msg.Length; i++)
            {
                strReceiveData += msg[i].ToString("X2");
            }
            msg.CopyTo(ReciveByte, ReciveByteSize);
            ReciveByteSize += msg.Length;

            for (int i = 0; i < ReciveByteSize - 2; i++)
            {
                strchkHex += ReciveByte[i].ToString("X2");
            }

            if (strReceiveData == CrcMaker(strchkHex))
            {
                strReceiveData = "";
                FisConnected = true;
                string hexString = string.Empty;
                try
                {
                    for (int i = 0; i < ReciveByteSize; i++)
                    {
                        hexString += ReciveByte[i].ToString("X2");
                    }
                    string strHexPos1 = ReciveByte[1].ToString("X2");

                    switch (strHexPos1)
                    {
                        case "10":
                            //파리미터 쓰기 ADJ ON/OFF 구동 명령 응답
                            WriteRecive(ReciveByte[3], hexString);
                            break;
                        case "03":
                            //파라미터 읽기, 시스템 정보,ADJ 정보 읽기
                            ReadeRecive(ReciveByte);
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {

                    ReciveMsg($"\r\n msgLen:{msg.Length} \t {hexString} \r\n{ ex.ToString()}");
                }

                Array.Clear(ReciveByte, 0, ReciveByte.Length);
                ReciveMsg(hexString); //로그기록
                ReciveByteSize = 0;
                strReceiveData = "";
            }
        }

        private void WriteRecive(byte msg, string strHex ) 
        {
            string strHexPos3 = msg.ToString("X2");
            
            switch (strHexPos3)
            {
                case "10":
                    if (strHex == "011000100008C00A")
                    {
                        ReciveMsg("Param 쓰기 성공");
                        ParamWriteRecive = EWriteRecive.Success;
                    }
                    break;
                case "25":
                    if (strHex == "0110002500011002")
                    {
                        ReciveMsg("ADJ 구동 성공");
                        ADJWriteRecive = EWriteRecive.Success;
                    }
                    break;
                default:
                    break;
            }
        }

        StringBuilder strReciveValue = new StringBuilder();
        private void ReadeRecive(byte[] msg)
        {
            string strHexPos3 = msg[3].ToString("X2");
            int HexValue;
            strReciveValue.Clear();
            switch (strHexPos3)
            {
                case "10":
                    HexValue = Convert.ToInt32(msg[5]);
                    RTValue = HexValue * 0.01;
                    strReciveValue.Append($" [RT] Value:{RTValue} Hex:{msg[5].ToString("X2")} = {HexValue}");

                    HexValue = Convert.ToInt32(msg[7]);
                    HTValue = HexValue * 0.01;
                    strReciveValue.Append($" [HT] Value:{HTValue} Hex:{msg[7].ToString("X2")} = {HexValue}");
                    
                    HexValue = Convert.ToInt32(msg[9]);
                    FTValue = HexValue * 0.01;
                    strReciveValue.Append($" [FT] Value:{FTValue} Hex:{msg[9].ToString("X2")} = {HexValue}");

                    HexValue = Convert.ToInt32(msg[11]);
                    DelayValue = HexValue * 0.01;
                    strReciveValue.Append($" [Delay] Value:{DelayValue} Hex:{msg[11].ToString("X2")} = {HexValue}");

                    PCTValue = Convert.ToInt32(msg[13]);
                    strReciveValue.Append($" [PCT] Value:{PCTValue} Hex:{msg[13].ToString("X2")} = {PCTValue}");

                    PluseNumValue = Convert.ToInt32(msg[15]);
                    strReciveValue.Append($" [PluseNum] Value:{PluseNumValue} Hex:{msg[15].ToString("X2")} = {PluseNumValue}");

                    HexValue = Convert.ToInt32(msg[17]);
                    strReciveValue.Append($" [WorkMode] Value:{((EWorkMode)HexValue).ToString()} Hex:{msg[17].ToString("X2")} = {HexValue}");
                    WorkModeValue = (EWorkMode)HexValue;

                    VoltageValue = Convert.ToInt32(msg[19]);
                    strReciveValue.Append($" [Voltage] Value:{VoltageValue} Hex:{msg[19].ToString("X2")} = {VoltageValue}");


                    break;
                case "04":
                    HexValue = Convert.ToInt32(msg[5]);
                    Status = (ESystemStatus)HexValue;
                    strReciveValue.Append($" [System] Value:{((ESystemStatus)HexValue).ToString()} Hex:{msg[5].ToString("X2")} = {HexValue}");

                    //switch (HexValue)
                    //{
                    //    case 1:
                    //        Status = ESystemStatus.StandBy;
                    //        break;
                    //    case 2:
                    //        Status = ESystemStatus.Operation;
                    //        break;
                    //    case 3:
                    //        Status = ESystemStatus.Fault;
                    //        break;
                    //    case 4:
                    //        Status = ESystemStatus.Nozzle_Calibration;
                    //        break;
                    //    case 5:
                    //        Status = ESystemStatus.Parameter_Setting;
                    //        break;
                    //    case 6:
                    //        Status = ESystemStatus.Maintenance_Required;
                    //        break;
                    //    default:
                    //        Status = ESystemStatus.None;
                    //        break;
                    //}
                    SystemErrorCode = Convert.ToInt32(msg[7]);
                    strReciveValue.Append($" [System_Err] Value:{((ESystemStatus)HexValue).ToString()} Hex:{msg[5].ToString("X2")} = {HexValue}");

                    break;
                case "02":
                    CalibrationValue_Hi = Convert.ToInt32(msg[4]);
                    strReciveValue.Append($" [CalibrationValue_Hi] Value:{CalibrationValue_Hi} Hex:{msg[4].ToString("X2")} = {CalibrationValue_Hi}");

                    CalibrationValue_Low = Convert.ToInt32(msg[5]);
                    strReciveValue.Append($" [CalibrationValue_Hi] Value:{CalibrationValue_Low} Hex:{msg[5].ToString("X2")} = {CalibrationValue_Low}");
                    break;
                default:
                    break;
            }

            ReciveMsg(strReciveValue.ToString());
        }
        #endregion

        #region SendData
        private void ReciveDataReset()
        {
            Array.Clear(ReciveByte, 0, ReciveByte.Length);
            ReciveByteSize = 0;
            strReceiveData = "";
        }
        //문자열의 CRC를 생성한다
        /// <summary>
        /// 문자열의 CRC를 생성한다
        /// </summary>
        /// <param name="str">보낼 문자열</param>
        /// <returns>CRC가 포함된 문자열</returns>
        public string CrcMaker(string str)
        {
            if (str.Length % 2 == 1 || str.Length == 0)
            {
                return "StringError";
            }

            UInt32 usCRC = 0xFFFF;
            byte bytTemp;

            try
            {
                for (int i = 0; i < str.Length - 1; i += 2)
                {
                    bytTemp = (byte)Convert.ToUInt32(str.Substring(i, 2), 16);
                    usCRC = Convert.ToUInt32(usCRC ^ bytTemp);

                    for (int j = 1; j < 9; j++)
                    {
                        if ((usCRC & 1) == 1)
                        {
                            usCRC = usCRC >> 1;
                            usCRC = Convert.ToUInt32(usCRC ^ Convert.ToUInt32(0xa001));
                        }
                        else
                        {
                            usCRC = usCRC >> 1;
                        }
                    }
                }

                string tempstr = usCRC.ToString("X4").ToUpper();

                return str + tempstr.Substring(2, 2) + tempstr.Substring(0, 2);
            }
            catch (Exception)
            {
                return "StringError";
            }

        }

        public static byte[] ConvertHexStringToByte(string convertString)
        {
            byte[] convertArr = new byte[convertString.Length / 2];

            for (int i = 0; i < convertArr.Length; i++)
            {
                convertArr[i] = Convert.ToByte(convertString.Substring(i * 2, 2), 16);
            }
            return convertArr;
        }
        public bool Send_ParamWrite(double rt, double ht, double ft, double delay,
                               int pct, int PluseNum, int WorkMode, int Voltage)
        {
            bool result = true;
            int RtValue = (int)(rt * 100);
            int HtValue = (int)(ht * 100);
            int FtValue = (int)(ft * 100);
            int DelayValue = (int)(delay * 100);

            //string strParamData = "011000100008";
            StringBuilder strParamData = new StringBuilder();
            strParamData.Append("011000100008");
            strParamData.Append(RtValue.ToString("X4"));
            strParamData.Append(HtValue.ToString("X4"));
            strParamData.Append(FtValue.ToString("X4"));
            strParamData.Append(DelayValue.ToString("X4"));
            strParamData.Append(pct.ToString("X4"));
            strParamData.Append(PluseNum.ToString("X4"));
            strParamData.Append(WorkMode.ToString("X4"));
            strParamData.Append(Voltage.ToString("X4"));

            string strData = CrcMaker(strParamData.ToString());

            ReciveMsg($"[Send-Send_ParamWrite] {strData}"); //임시 보낸 데이터 기록

            if (strData == "StringError")
                result = false;
            else 
            {
                byte[] SendByteData = ConvertHexStringToByte(strData);
                SPort.DataWriteByte(SendByteData);
                ReciveDataReset();
            }

            return result;
        }
        public void Send_ParamRead()
        {
            if (SPort != null)
            {
                //0x01 0x03 0x00 0x10 0x00 0x08 0x45 0xC9 
                byte[] senddata = { 0x01, 0x03, 0x00, 0x10, 0x00, 0x08, 0x45, 0xC9 };
                SPort.DataWriteByte(senddata);
                ReciveDataReset();
            }
        }

        public void Send_SystemStatusRead()
        {
            //0x01 0x03 0x00 0x00 0x00 0x02 0xC4 0x0B            
            byte[] senddata = { 0x01, 0x03, 0x00, 0x00, 0x00, 0x02, 0xC4, 0x0B };
            SPort.DataWriteByte(senddata);
            ReciveDataReset();
        }
        
        //노즐 Calibration 
        public bool Send_ADJOnOff(bool flag)
        {
            bool result=true;
            //0x01 0x10 0x00 0x25 0x00 0x01 0x00 0xFF/0x00 CRC CRC 

            StringBuilder strSendData = new StringBuilder();

            strSendData.Append("01100025000100");
            if (flag)
                strSendData.Append("FF");
            else
                strSendData.Append("00");

            string strData = CrcMaker(strSendData.ToString());

            ReciveMsg($"[Send-Send_ADJOnOff] {strData}"); //임시 보낸 데이터 기록

            if (strData == "StringError")
                result = false;
            else
            {
                byte[] SendByteData = ConvertHexStringToByte(strData);
                SPort.DataWriteByte(SendByteData);
                ReciveDataReset();
            }

            return result;
        }
        public void Send_ADJRead()
        {
            //0x01 0x03 0x00 0x24 0x00 0x01 0xC4 0x01 
            byte[] senddata = { 0x01, 0x03, 0x00 ,0x24, 0x00, 0x01 ,0xC4, 0x01 };
            SPort.DataWriteByte(senddata);
            ReciveDataReset();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XModule.Standard;
using static XModule.Unit.C_LibraryGlobal;

namespace XModule.Unit
{
    public class CNetLjX8000
    {
      
        //ERRMSG
        public delegate void ErrorMsg_EventHandler(string errtitle, string errmsg);
        public event ErrorMsg_EventHandler ErrorMsg_Event = null;
        //ReiveMSG
        public delegate void Recivemsg_EventHandler(string msg);
        public event Recivemsg_EventHandler Recivemsg_Event = null;

        Socket_Properties prop = new Socket_Properties();

        public ClientSocket LjxSocket;

        public bool FaceAngleStatusRun { get; set; }
        public bool SendFlag { get; set; }

        public CmmStatus ReciveStatus;
        public double AngleValue { get; set; }

        public double[] Jig_PeakZ_Value = new double[5];

        public double[] Side_PeakZ_Value = new double[20];

        private string FType;


        public CNetLjX8000()
        {
            
        }
        ~CNetLjX8000()
        {

        }
           

        #region SocketConnect
        public bool Init(string hostName, int hostPort, string type)
        {
            if (hostName == "")
                return false;

            FType = type;
            prop = new Socket_Properties();
            prop.hostName = hostName;
            prop.hostPort = hostPort;
            prop.eEncoding = Encoding.ASCII;
            prop.bsendtypestr = true; //전송 데이터가 문자열 인지, hex 인지
            LjxSocket = new ClientSocket(prop);
            Connected();


            return true;
        }

        public bool Connected()
        {
            bool result = LjxSocket.ConnectToServer();
            if (result)
            {
                LjxSocket.RecivemsgByte_Event += Client_Recive_Data;
                LjxSocket.ErrorMsg_Event += Client_ErrMsgShow;
            }
            return result;
        }

        public bool DeConnected()
        {
            bool result = true;
            if (LjxSocket != null)
            {
                if (LjxSocket.IsConnected)
                {
                    result = LjxSocket.CloseToServer();
                    LjxSocket.RecivemsgByte_Event -= Client_Recive_Data;
                    LjxSocket.ErrorMsg_Event -= Client_ErrMsgShow;
                }
            }

            return result;
        }
        public bool IsConnected()
        {
            bool result = false;
            if (LjxSocket != null)
             result = LjxSocket.IsConnected;

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

        private void Client_ErrMsgShow(string errtitle, string errmsg)
        {
            ErrMsg(errtitle, errmsg);
        }

        private void Client_Recive_Data(byte[] msg)
        {
            try
            {
                string str = prop.eEncoding.GetString(msg);
                // ReciveMsg(str);

                string[] strTemp = str.Split('\r');
                if (strTemp.Length > 1)
                {
                    SendFlag = false;

                    string[] StrCmdSplit = strTemp[0].Split(',');
                    switch (StrCmdSplit[0])
                    {
                        case "RM":                            
                            ReciveStatus = CmmStatus.Ok;
                            if (StrCmdSplit[1] == "0")
                                FaceAngleStatusRun = false;
                            else
                                FaceAngleStatusRun = true;
                            break;
                        case "T1":
                            ReciveStatus = CmmStatus.Ok;

                            //결과값 처리
                            break;
                        case "RS":
                            break;
                        case "ER":
                            //리셋 처리
                            Send_Reset();
                            break;
                        case "VA":
                            if (StrCmdSplit.Length > 1)
                            {
                                AngleValue = Convert.ToDouble(StrCmdSplit[1]);
                                Side_PeakZ_Value = new double[20];
                                if (FType == "SideAngle")
                                {
                                    for (int i = 0; i < StrCmdSplit.Length - 2; i++)
                                    {
                                        if (i > 20)
                                            break;

                                        double.TryParse(StrCmdSplit[2 + i], out Side_PeakZ_Value[i]);
                                    }



                                }
                                else if (FType == "JigAngle")
                                {
                                    double dmin = 9999;
                                    double dmax = 0;
                                    for (int i = 0; i < StrCmdSplit.Length - 2; i++)
                                    {
                                        if (i > 3)
                                            break;

                                        double.TryParse(StrCmdSplit[2 + i], out Jig_PeakZ_Value[i]);

                                        if (dmin > Jig_PeakZ_Value[i])
                                            dmin = Jig_PeakZ_Value[i];

                                        if (dmax < Jig_PeakZ_Value[i])
                                            dmax = Jig_PeakZ_Value[i];
                                    }

                                    Jig_PeakZ_Value[4] = dmax - dmin;
                                }


                                ReciveStatus = CmmStatus.Ok;
                            }
                            else
                                ReciveStatus = CmmStatus.Ng;

                            break;
                        default:
                            // AngleValue = Convert.ToDouble(StrCmdSplit[0]);
                            // ReciveStatus = CmmStatus.Ok;
                            break;
                    }
                }
                //cDEF.TaskLogAppend(TaskLog.Keyence, str.ToString(), true); //ksyoon, 테스트용, 210319
            }
            catch(Exception ex)
            {
                cDEF.TaskLogAppend(TaskLog.Keyence, $"exception:{ex}", true);
            }
        }
       
        #endregion

        #region SendData

        
        private void ReciveDataReset()
        {
            AngleValue = 0;
            Array.Clear(Jig_PeakZ_Value, 0, Jig_PeakZ_Value.Length);
            Array.Clear(Side_PeakZ_Value, 0, Side_PeakZ_Value.Length);
            

            //ReciveByteSize = 0;
            //strReceiveData = "";
        }
        #region 체크섬
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
        #endregion 체크섬


        /// <summary>
        /// 트리거 발행
        /// </summary>
        public void Send_Trigger()
        {
            ReciveDataReset();
            LjxSocket.SendMessageStr("T1\r");
            ReciveStatus = CmmStatus.NoResponse;
        }
        /// <summary>
        /// 운전모드 변경
        /// </summary>
        public void Send_Run()
        {
            LjxSocket.SendMessageStr("R0\r");
            ReciveStatus = CmmStatus.NoResponse;
        }
        /// <summary>
        /// 설정모드 변경
        /// </summary>
        public void Send_Setup()
        {
            LjxSocket.SendMessageStr("S0\r");
            ReciveStatus = CmmStatus.NoResponse;
        }

        /// <summary>
        /// 화상, 각종 버퍼 클리어
        /// 데이터를 저장 하는 파알의 파일명을 신규 작성
        /// 종합 판정 출력 초기화
        /// 이력, 통계 데이터 클리어
        /// 계측 횟수 클리어
        /// OUT DATA0 ~ OUTDATA15 클리어
        /// </summary>
        public void Send_Reset()
        {
            LjxSocket.SendMessageStr("RS\r");
            ReciveStatus = CmmStatus.NoResponse;
        }
        /// <summary>
        /// 현재의 검사 설정을 저장하고 리부팅
        /// </summary>
        public void Send_Rebooting()
        {
            LjxSocket.SendMessageStr("RB\r");
            ReciveStatus = CmmStatus.NoResponse;
        }
        /// <summary>
        /// 현재의 검사 설정, 환경 설정을 저장
        /// </summary>
        public void Send_SetubSave()
        {
            LjxSocket.SendMessageStr("SS\r");
            ReciveStatus = CmmStatus.NoResponse;
        }
        /// <summary>
        /// 운전/설정 모드 읽기
        ///  0: 설정모드 1:운전모드
        /// </summary>
        public void Send_RunStatus()
        {
            LjxSocket.SendMessageStr("RM\r");
            ReciveStatus = CmmStatus.NoResponse;
        }

        /// <summary>
        /// 촬상이 개시될 때까지의 딜리이 시간 설정
        /// 0~999 ms 
        /// </summary>
        public void Send_SetTriggerDelay(int iDelay)
        {
            LjxSocket.SendMessageStr($"CTD,1,{iDelay}\r");
            ReciveStatus = CmmStatus.NoResponse;
        }

        /// <summary>
        /// 계측값 보정의 값을 반환
        /// </summary>
        /// <param name="iTool"> 툴번호 100~199</param>
        /// <param name="iMeasuring">계측 항목 번호 0~31</param>
        public void Send_MeasuringValue(int iTool, int iMeasuring)
        {
            LjxSocket.SendMessageStr($"MCR,{iTool},{iMeasuring}\r");
            ReciveStatus = CmmStatus.NoResponse;
        }

        #endregion
    }
}

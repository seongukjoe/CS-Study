using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XModule.Unit.C_LibraryGlobal;

namespace XModule.Unit
{
    class CRsLjx8000
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
        ComPort_Properties Portprop;
        byte[] ReciveByte;
        int ReciveByteSize = 0;

        string strReceiveData = "";
        bool FisConnected = false;
        public string strValue;

        public EWriteRecive ParamWriteRecive;
       

        public CRsLjx8000()
        {

        }
        public enum EWriteRecive
        {
            Success,
            Fail
        };
      
       

        #region Connect
        public bool Init(string strPortName, int iBaudRate)
        {
            bool result = false;

            //파라미터 로드

            Portprop = new ComPort_Properties();
            Portprop.eEncoding = Encoding.ASCII;
            Portprop.strPortName = strPortName;
            Portprop.iBaudRate = iBaudRate;
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
            try
            {
                ReciveMsg(msg);
            }
            catch (Exception ex)
            {

                ReciveMsg(ex.ToString());
            }

        }



        /// <summary>
        /// Recive Data
        /// </summary>
        /// <param name="msg"></param>
        private void Com_Recive_DataByte(byte[] msg)
        {
            string str = Portprop.eEncoding.GetString(msg);
            ReciveMsg(str);
         
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
       
        /// <summary>
        /// 트리거 발행
        /// </summary>
        public void Send_Trigger()
        {
            SPort.DataWriteStr("T1\r");
            ReciveDataReset();
        }
        /// <summary>
        /// 운전모드 변경
        /// </summary>
        public void Send_Run()
        {
            SPort.DataWriteStr("R0\r");
            ReciveDataReset();
        }
        /// <summary>
        /// 설정모드 변경
        /// </summary>
        public void Send_Setup()
        {
            SPort.DataWriteStr("S0\r");
            ReciveDataReset();
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
            SPort.DataWriteStr("RS\r");
            ReciveDataReset();
        }
        /// <summary>
        /// 현재의 검사 설정을 저장하고 리부팅
        /// </summary>
        public void Send_Rebooting()
        {
            SPort.DataWriteStr("RB\r");
            ReciveDataReset();
        }
        /// <summary>
        /// 현재의 검사 설정, 환경 설정을 저장
        /// </summary>
        public void Send_SetubSave()
        {
            SPort.DataWriteStr("SS\r");
            ReciveDataReset();
        }
        /// <summary>
        /// 운전/설정 모드 읽기
        ///  0: 설정모드 1:운전모드
        /// </summary>
        public void Send_RunStatus()
        {
            SPort.DataWriteStr("RM\r");
            ReciveDataReset();
        }

        /// <summary>
        /// 촬상이 개시될 때까지의 딜리이 시간 설정
        /// 0~999 ms 
        /// </summary>
        public void Send_SetTriggerDelay(int iDelay)
        {
            SPort.DataWriteStr($"CTD,1,{iDelay}\r");
            ReciveDataReset();
        }

        /// <summary>
        /// 계측값 보정의 값을 반환
        /// </summary>
        /// <param name="iTool"> 툴번호 100~199</param>
        /// <param name="iMeasuring">계측 항목 번호 0~31</param>
        public void Send_MeasuringValue(int iTool,int iMeasuring)
        {
            SPort.DataWriteStr($"MCR,{iTool},{iMeasuring}\r");
            ReciveDataReset();
        }

        #endregion
    }
}

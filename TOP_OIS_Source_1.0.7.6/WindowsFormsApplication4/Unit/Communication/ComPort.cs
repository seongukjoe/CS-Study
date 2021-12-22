using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using XModule.Unit.Communication;

namespace XModule.Unit
{
    public class ComPort : C_LibraryGlobal
    {
       ComPort_Properties _PORT_PROP;
       SerialPort sPort;

       public ComPort()
       {
           sPort = new SerialPort();
       }

        public ComPort(ComPort_Properties prop)
        {
            sPort = new SerialPort();
            _PORT_PROP = prop;
        }
        public ComPort_Properties PORT_PROP {
            set{ _PORT_PROP = value ;} 
        }

        private void SetProp()
        {

            sPort.PortName = _PORT_PROP.strPortName;
            sPort.BaudRate = _PORT_PROP.iBaudRate;
            switch (_PORT_PROP.strPstrarity)
            {
                case "NONE":
                    sPort.Parity = Parity.None;
                    break;
                case "Even":
                    sPort.Parity = Parity.Even;
                    break;
                case "Mark":
                    sPort.Parity = Parity.Mark;
                    break;
                case "Odd":
                    sPort.Parity = Parity.Odd;
                    break;
                case "Space":
                    sPort.Parity = Parity.Space;
                    break;
                default:
                    sPort.Parity = Parity.None;
                    break;
            }
            sPort.DataBits = _PORT_PROP.iDataBits;
            switch (_PORT_PROP.strStopBits)
            { 
                case "NONE":
                    sPort.StopBits = StopBits.None;
                    break;
                case "One":
                    sPort.StopBits = StopBits.One;
                    break;
                case "OnePointFive":
                    sPort.StopBits = StopBits.OnePointFive;
                    break;
                case "Two":
                    sPort.StopBits = StopBits.Two;
                    break;
                default:
                    sPort.StopBits = StopBits.One;
                    break;
            }
            if (_PORT_PROP.eEncoding != null)
            {
               sPort.Encoding = _PORT_PROP.eEncoding; 
            }

            //sPort.DtrEnable = _PORT_PROP.bDtrEnable;
            //sPort.RtsEnable = _PORT_PROP.bRtsEnable;
            
            if (_PORT_PROP.iReadBuferSize > 0)
            {
                 sPort.ReadBufferSize = _PORT_PROP.iReadBuferSize;
            }
            if (_PORT_PROP.iWriteBuferSize > 0)
            {
                 sPort.WriteBufferSize = _PORT_PROP.iWriteBuferSize;
            }
     
          
        }

        public bool PortOpen()
        {
          
            SetProp(); // 

            if (!sPort.IsOpen)
            {
                try
                {
                    sPort.Open();

                    Init();

                    //LOOPSend
                    if ( _PORT_PROP.iniFilepath != null)
                    {
                        SetParameter(_PORT_PROP.iniFilepath);
                        ThreadLoopSend = new Thread(new ThreadStart(LoopSend));
                        ThreadLoopSend.Start();

                    }

                    return true;
                }
                catch (Exception ex)
                {
                    ErrMsg("PORT OPEN Faile", ex.Message);
                }
              
            }
            return false;
        
        }

        public bool isConnected()
        {
            if (sPort.IsOpen) return  true;
            else return false;
        }

        private void Init()
        {
            if (sPort.IsOpen)
            {
                sPort.DataReceived += new SerialDataReceivedEventHandler(serialDataRecvHandler);
                sPort.ErrorReceived += new SerialErrorReceivedEventHandler(serialErrorReceived);

                Senddata = true;
            }
        }


        /// <summary>
        /// 종료
        /// </summary>
        /// <returns></returns>
        public bool PortClose()
        {
            bool result = false;

            try
            {
                if (_PORT_PROP.iniFilepath != null)
                {
                    if (ThreadLoopSend.ThreadState == System.Threading.ThreadState.Running)
                    {
                        ThreadLoopSend.Abort();
                        ThreadLoopSend.Join();
                    }

                }
            }
            catch (Exception ex)
            {


            }

            try
            {
                sPort.DataReceived -= new SerialDataReceivedEventHandler(serialDataRecvHandler);
                sPort.ErrorReceived -= new SerialErrorReceivedEventHandler(serialErrorReceived);

                if (sPort.IsOpen)
                {
                    
                    sPort.Close();
                    sPort.Dispose();
                    sPort = null;
              
                }

                if (sPort == null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                // m_ErrMsg = e.Message;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 데이터 받기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void serialDataRecvHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string FReaddata;

            //if (_PORT_PROP.bsendtypestr)
            //{          
            // FReaddata = sPort.ReadExisting();
            //}
            //  else
            //{
            int Freadlen = sPort.BytesToRead;// sPort.ReadBufferSize;
            byte[] Fbytebuf = new byte[Freadlen];

            sPort.Read(Fbytebuf, 0, Freadlen);
           

           // FReaddata = System.Text.Encoding.Default.GetString(Fbytebuf);
            //}

            if (!Senddata)
            {
                if (_PORT_PROP.bsendtypestr)
                    loopCmdDatastr.RemoveAt(0);
                else
                    loopCmdDatabyte.RemoveAt(0);

                Senddata = true;
                RetryCnt = 0;
            }

            ReciveMsgByte(Fbytebuf);
        }


        public override void LoopSend()
        {
            while (sPort.IsOpen)
            {
                if (Senddata)
                {
                    if (loopCmdDatastr.Count == 0)
                    {
                        SetParameterLoop(_PORT_PROP.bsendtypestr, _PORT_PROP.reciveuse);
                    }

                    //명령어 전송
                    if (_PORT_PROP.bsendtypestr)
                    {
                        sPort.Write(loopCmdDatastr[0].Key);
                       
                        //응답 대기 여부
                        if (loopCmdDatastr[0].Value)
                        {
                            Senddata = false;
                        }
                        else
                        {
                            loopCmdDatastr.RemoveAt(0);
                            Senddata = true;
                            RetryCnt = 0;
                        }
                    }
                    else
                    {
                        sPort.Write(loopCmdDatabyte[0].Key, 0, loopCmdDatabyte[0].Key.Length);
                        //응답 대기 여부
                        if (loopCmdDatabyte[0].Value)
                        {
                            Senddata = false;
                        }
                        else
                        {
                            loopCmdDatabyte.RemoveAt(0);
                            Senddata = true;
                            RetryCnt = 0;
                        }
                    }

                   
                    timeStamp.Restart();
                }
                else
                {
                    //타이머 응답 대기 및 Retry
                    if (timeStamp.ElapsedMilliseconds > _PORT_PROP.ReciveWaitTime)
                    {
                        RetryCnt++;

                        if (RetryCnt > 5)
                        {
                            if (_PORT_PROP.bsendtypestr)
                                loopCmdDatastr.RemoveAt(0);
                            else
                                loopCmdDatabyte.RemoveAt(0);

                            RetryCnt = 0;
                        }
                        Senddata = true;
                    }
                }
                Thread.Sleep(_PORT_PROP.LoopWatitTime);
                if (sPort == null)
                    break;
            }

            if (sPort != null)
            {
            if (!sPort.IsOpen)
            {
                if (PortClose())
                    if (!PortOpen())
                    {
                        ErrMsg("PORT OPEN Faile", "PORT OPEN Faile");
                    } 
            }
            }
        }



        public override void LoopDataWriteByte(byte[] senddata, bool reciveuse)
        {
            if (Senddata == false)
            {
                //명령어 전송 응답 대기중 상태
                loopCmdDatabyte.Insert(1, new KeyValuePair<byte[], bool>(senddata, reciveuse));
       
            }
            else
                loopCmdDatabyte.Insert(0, new KeyValuePair<byte[], bool>(senddata, reciveuse));

        }
        public override void LoopDataWriteStr(string senddata, bool reciveuse)
        {

            if (Senddata == false)
            {
                //명령어 전송 응답 대기중 상태
                loopCmdDatastr.Insert(1, new KeyValuePair<string, bool>(senddata, reciveuse));
            }
            else
                loopCmdDatastr.Insert(0, new KeyValuePair<string, bool>(senddata, reciveuse));
        }


        public override void DataWriteByte(byte[] senddata)
        {
            if (sPort.IsOpen)
            { 
                sPort.Write(senddata, 0, senddata.Length);
            }
        }
        public override void DataWriteStr(string senddata)
        {
            if (sPort.IsOpen)
            {
                sPort.Write(senddata);
            }
        }

        private void serialErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            string err = e.EventType.ToString();
            ErrMsg("ReciveDataError", err);
            if (!Senddata)
            {
                RetryCnt++;

                if (RetryCnt > 5)
                {
                    if (_PORT_PROP.bsendtypestr)
                        loopCmdDatastr.RemoveAt(0);
                    else
                        loopCmdDatabyte.RemoveAt(0);

                    RetryCnt = 0;
                }
                Senddata = true;
            }
        }


    }
}

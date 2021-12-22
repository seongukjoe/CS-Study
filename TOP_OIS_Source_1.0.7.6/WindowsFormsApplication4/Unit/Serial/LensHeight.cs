using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Ports;
using SIC_COM;
using XModule.Standard;

namespace XModule.Unit
{
    public class cSerials
    {
        // Lens Height
        public cRs232c LensHeight = null;
        public bool RecvComplete = false;
        public double Value = 0.0;
        public string strValue = string.Empty;
        private Thread thread = null;

        // Bond1
        public cRs232c Bond1 = null;
        public bool RecvComplete_Bond1 = false;
        public double Value_Bond1 = 0.0;
        public string strValue_Bond1 = string.Empty;
        private Thread thread_Bond1 = null;

        // Bond2
        public cRs232c Bond2 = null;
        public bool RecvComplete_Bond2 = false;
        public double Value_Bond2 = 0.0;
        public string strValue_Bond2 = string.Empty;
        private Thread thread_Bond2 = null;

        public cSerials()
        {
            LensHeight = new cRs232c(cPath.FILE_232, "LensHeight");
            LensHeight.Init(true);
            LensHeight.OnDataReceived += LensHeight_OnDataReceived;
            LensHeight.Open();
            thread = new Thread(execute);
            thread.Start();
            if (cDEF.Work.DispSensor.DispenserType == 1)
            {

                Bond1 = new cRs232c(cPath.FILE_232, "Bond1");
                Bond1.Init(true);
                Bond1.OnDataReceived += LensHeight_OnDataReceived_Bond1;
                Bond1.Open();
                thread_Bond1 = new Thread(execute_Bond1);
                thread_Bond1.Start();

                Bond2 = new cRs232c(cPath.FILE_232, "Bond2");
                Bond2.Init(true);
                Bond2.OnDataReceived += LensHeight_OnDataReceived_Bond2;
                Bond2.Open();
                thread_Bond2 = new Thread(execute_Bond2);
                thread_Bond2.Start();
            }
        }

        private void LensHeight_OnDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string rsv = ((SerialPort)(sender)).ReadLine();
                //string parsingStr = rsv.Substring("M0", 5);
                int nExt = -1;
                string[] sArr = rsv.Split(',');
                if (sArr[1] != null)
                {
                    nExt = sArr[0].IndexOf("ER");
                    if (nExt >= 0)
                    {
                        Value = 0;
                        strValue = "ERR";
                    }
                    else
                    {
                        Value = Convert.ToDouble(sArr[1]);
                        strValue = Value.ToString();
                    }
                }
                if (rsv.Length > 5)
                {

                    RecvComplete = true;
                }

                
            }
            catch (Exception ex)
            {
                SIC_COM.cLog.WriteError("[RS232_IO] Input Exception Err - " + ex.Message);
            }
        }

        private void LensHeight_OnDataReceived_Bond1(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string rsv = ((SerialPort)(sender)).ReadLine();
                //string parsingStr = rsv.Substring("M0", 5);
                int nExt = -1;
                string[] sArr = rsv.Split(',');
                if (sArr[1] != null)
                {
                    nExt = sArr[0].IndexOf("ER");
                    if (nExt >= 0)
                    {
                        Value_Bond1 = 0;
                        strValue_Bond1 = "ERR";
                    }
                    else
                    {
                        Value_Bond1 = Convert.ToDouble(sArr[1]) * 1000.0;
                        strValue_Bond1 = Value.ToString();
                    }
                }
                if (rsv.Length > 5)
                {

                    RecvComplete_Bond1 = true;
                }


            }
            catch (Exception ex)
            {
                SIC_COM.cLog.WriteError("[RS232_IO] Bond1 Exception Err - " + ex.Message);
            }
        }
        private void LensHeight_OnDataReceived_Bond2(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string rsv = ((SerialPort)(sender)).ReadLine();
                //string parsingStr = rsv.Substring("M0", 5);
                int nExt = -1;
                string[] sArr = rsv.Split(',');
                if (sArr[1] != null)
                {
                    nExt = sArr[0].IndexOf("ER");
                    if (nExt >= 0)
                    {
                        Value_Bond2 = 0;
                        strValue_Bond2 = "ERR";
                    }
                    else
                    {
                        Value_Bond2 = Convert.ToDouble(sArr[1]) * 1000.0;
                        strValue_Bond2 = Value.ToString();
                    }
                }
                if (rsv.Length > 5)
                {

                    RecvComplete_Bond2 = true;
                }


            }
            catch (Exception ex)
            {
                SIC_COM.cLog.WriteError("[RS232_IO] Bond1 Exception Err - " + ex.Message);
            }
        }
        public void Connect()
        { 
        
        }
        public void SendMeasure()
        {
            RecvComplete = false;
            LensHeight.WriteLine("M0\r\n");
        }
        public void SendMeasure_Bond1()
        {
            RecvComplete_Bond1 = false;
            Bond1.WriteLine("M0\r\n");
        }
        public void SendMeasure_Bond2()
        {
            RecvComplete_Bond2 = false;
            Bond2.WriteLine("M0\r\n");
        }
        private void execute()
        {
            while(true)
            {
                SendMeasure();
                Thread.Sleep(100);
            }
        }

        private void execute_Bond1()
        {
            while (true)
            {
                SendMeasure_Bond1();
                Thread.Sleep(100);
            }
        }

        private void execute_Bond2()
        {
            while (true)
            {
                SendMeasure_Bond2();
                Thread.Sleep(100);
            }
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace XModule.Unit
{
    class CLogBuffer
    {
        public bool IsBuffering = false;
        public ConcurrentQueue<string> strList = new ConcurrentQueue<string>();
    }

    public class CLogWrite
    {

        private CLogBuffer FLogBuffProduct= null;
        private CLogBuffer FLogBuffTactTime = null;

        private bool FIsWriting = false;
        public bool IsWriting { get { return FIsWriting; } }


        private List<string> FStrListW = null;

        private bool bExecute = true;

        protected string FLogDirectory;
       
        public CLogWrite()
        {
            FStrListW = new List<string>();
        
            FLogBuffProduct = new CLogBuffer();
            FLogBuffTactTime = new CLogBuffer();

            Task.Factory.StartNew(OnExecute);
        }
        private void OnExecute()
        {
            // 예외 쿼리문
            string strTrace = "";

            while (bExecute)
            {
                if (!FIsWriting)
                {
                    FIsWriting = true;

                    try
                    {
                        string message;
                        FStrListW.Clear();

                        //FLogBuffProduct
                        while (FLogBuffProduct.strList.TryDequeue(out message)) 
                        {
                            FStrListW.Add(message);          
                        }
                        if (FStrListW.Count > 0)
                        {
                            foreach (string str in FStrListW)
                            {
                                SaveHistory("PD", str); 
                            }

                            FStrListW.Clear();
                        }
                        //FLogBuffTactTime
                        while (FLogBuffTactTime.strList.TryDequeue(out message))
                        {
                            FStrListW.Add(message);
                        }
                        if (FStrListW.Count > 0)
                        {
                            foreach (string str in FStrListW)
                            {
                                SaveHistory("TT", str);
                            }

                            FStrListW.Clear();
                        }

                    }
                    catch (Exception ex)
                    {
                        //strTrace = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} [OnExecute]  Message : {ex.ToString()}";

                        //Trace.WriteLine(strTrace);
                    }

                    FIsWriting = false;
                }
                System.Threading.Thread.Sleep(500);
            }
            bExecute = false;
        }
        private void SaveHistory(string flag, string AText)
        {
            try
            {
                string path = String.Format("{0}{1:D4}\\", Standard.cPath.FILE_MesEquipFile, DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string file;
                if (flag == "PD")
                    file = string.Format("{0}\\PD_{1}.csv", path, DateTime.Now.ToString("yyyyMMddHHmmss"));
                else
                    file = string.Format("{0}\\TT_{1}.csv", path, DateTime.Now.ToString("yyyyMMddHHmmss"));


                using (FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write))
                {
                    byte[] text = System.Text.UnicodeEncoding.Default.GetBytes(AText.ToString());
                    fs.Write(text, 0, text.Length);
                }
            }
            catch (Exception ex)
            {
              //  Trace.WriteLine(ex.ToString());
            }
        }

        public void LogWrite_Product(string sgrmsg)
        {
          
            try
            {
                FLogBuffProduct.IsBuffering = true;
                FLogBuffProduct.strList.Enqueue(sgrmsg);
                FLogBuffProduct.IsBuffering = false;
            }
            catch (Exception ex)
            {
                //string strTrace = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}\t{ex.ToString()}";

                //Trace.WriteLine(strTrace);
            }

        }
        public void LogWrite_TactTime(string sgrmsg)
        {

            try
            {
                FLogBuffTactTime.IsBuffering = true;
                FLogBuffTactTime.strList.Enqueue(sgrmsg);
                FLogBuffTactTime.IsBuffering = false;
            }
            catch (Exception ex)
            {
                //string strTrace = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}\t{ex.ToString()}";

                //Trace.WriteLine(strTrace);
            }

        }

    }
}

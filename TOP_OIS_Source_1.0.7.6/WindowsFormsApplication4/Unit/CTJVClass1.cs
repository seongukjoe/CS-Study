using System;
using TJV_RMDLL;

namespace XModule.Unit
{
    public class TJVClass : RMDLL
    {
        public delegate void ErrorMsgHandler(String Msg);
        public event ErrorMsgHandler ErrorMsg;
        public bool Connect = false;
        
      
        public bool Init(int IP)
        {
            try
            {         
                if (RMDLL.RM_init(IP) == 1)
                {
                    Connect = true;
                    return true;
                }
                else
                {
                    Connect = false;
                    return false;
                }
            }
            catch
            {
                Connect = false;
                return false;
            }           
        }

        public string ErrorMsg_Check()
        {
            try
            {
                if (RMDLL.RM_getErrorMessage() != "")
                    return RMDLL.RM_getErrorMessage();
                else
                    return "Normal Status";
            }
            catch
            {
                return "Abnormal Status";
            }
        }
        public bool DispensorWave(double Inivolt, double[] falltime, double[] openvolt, double[] opentime, double[] risetime, int[] pixelcount, int rowcount)
        {
            try
            {
                if (RMDLL.RM_PDDsetWaveform(Inivolt, falltime, openvolt, opentime, risetime, pixelcount, rowcount) == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }    
        }
        public int PDDStartSpitting(double Hz, int ndrop)
        {
            try
            {
                return RMDLL.RM_PDDstartSpitting(Hz, ndrop);
            }
            catch
            {
                return -1;
            }
        }
        public int PDDSetTriggerMode(int Mode)
        {
            try
            {
                return RMDLL.RM_PDDsetTrigMode(Mode);
            }
            catch
            {
                return -1;
            }
        }
        public int PDDStopSpitting()
        {
            try
            {
                return RMDLL.RM_PDDstopSpitting();
            }
            catch
            {
                return -1;
            }
        }

      
    }
}

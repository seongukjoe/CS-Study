using System;
using TJV_RMDLL;

namespace XModule.Unit
{
    public class TJVClass2 : RMDLL2
    {
        public delegate void ErrorMsgHandler(String Msg);
        public event ErrorMsgHandler ErrorMsg;
        public bool Connect = false;
        
      
        public bool Init(int IP)
        {
            try
            {         
                if (RMDLL2.RM_init(IP) == 1)
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
                if (RMDLL2.RM_getErrorMessage() != "")
                    return RMDLL2.RM_getErrorMessage();
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
                if (RMDLL2.RM_PDDsetWaveform(Inivolt, falltime, openvolt, opentime, risetime, pixelcount, rowcount) == 0)
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

            return RMDLL2.RM_PDDstartSpitting(Hz, ndrop);
        }
        public int PDDSetTriggerMode(int Mode)
        {
            return RMDLL2.RM_PDDsetTrigMode(Mode); 
        }
        public int PDDStopSpitting()
        {
            return RMDLL2.RM_PDDstopSpitting();
        }

        public int SelectDevice(int Device)
        {
            return RMDLL2.RM_selectDevice(Device);
        }

        public int GetDevice()
        {
            int Get = 0;
            Get = RMDLL2.RM_getDevice();
            return Get;

        }




    }
}

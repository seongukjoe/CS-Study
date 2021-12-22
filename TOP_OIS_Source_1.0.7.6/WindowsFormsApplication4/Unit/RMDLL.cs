using System.Runtime.InteropServices;

namespace TJV_RMDLL
{
    public abstract class RMDLL2
    {
        // multi-RM system management : no need to use for single RM system
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_selectDevice(int device);

        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_getDevice();

        // get error message
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern string RM_getErrorMessage();
        
        // For example, IP address = 192.168.0.122, then id = 22 (rotary dip switch)
        // The IP address is only checked at power cycle.
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_init(int id);

        // Encoder interface ------------------------------------------------------------

        // setup
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERsetSource(int sourceX, int sourceY); // 0=float, 2=external, 3=emulation
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERdefineDirection(int xdir, int ydir); // dir = 0, 1 (reverse)

        // encoder position counter
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERresetPosition();
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERgetPositionX(); // 0 <= pos < 2^24
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERgetPositionY(); // 0 <= pos < 2^24

        // printing
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERsetDistance(int value); // 1 <= value < 2097151 ( 1448*1448 )

        // encoder emulation
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERstartEmulation(double kHz, int phasecount, int dirX, int dirY); // 0.003 <= kHz <= 100.0,   phasecount < 2^26, dir = 0, 1 (reverse)
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERisFinishEmulation(); // return 1 when finished
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_ENCODERstopEmulation();

        // PDD interface  -----------------------------------------------------------------------

        // multi-module management : no need to use for single module
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDgetCount();// get the number of module found
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDselectWithIndex(int id);// select active Module 0 <= id < nPDD
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDselectWithModuleID(int id); // select active Module with ModuleID

        // PDD functions

        // initvolt : 30 ~ 120 V, openvolt : 0 ~ initvolt,  fall/rise time : 60 us ~ 4092 us,  opentime : 2 us ~ 4092 us , rowcount : 1 ~ 409
        // RM_PDDsetWaveform sets DCDC enabled.
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDsetWaveform(double initvolt, double[] falltime, double[] openvolt, double[] opentime, double[] risetime, int[] piexlcount, int rowcount);
        // default gain is set at initialization.
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDsetWaveformGain(int initgain, int fallgain, int opengain, int risegain); // 0 <= gain <= 15

        // spitting
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDsetTrigMode(int mode); // 0 = software spitting, 1=io point mode spitting, 2 = io line mode spitting, 3 = distance trigger 
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDstartSpitting(double Hz, int nDrop); // 10 <= Hz < 3000, 0 <= nDrop < 32768
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDisSpittingStopped(); // return 1 when finished
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDstopSpitting();

        // printing
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDwritePattern(byte[] pattern, int count); // pattern[x]=1 active, 0 deactive, 1 <= count < 131,040
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDstartPrinting();
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDstopPrinting();

        // heater
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDsetHeaterGain(int gain); // 0 <= gain <= 7
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern double RM_PDDgetHeaterTemperature(); // Celcius Degree
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDsetHeaterTemperature(double T); // Celcius Degree
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDenableHeater();
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDdisableHeater();

        // PZT actuator feedback info
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDgetHighCount();

        [DllImport("RMDLL2.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDwrite(int address, int data);
        [DllImport("RMDLL2.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDread(int address);


        // debug info
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDgetWaveform(double[] point, int size); // point[] != NULL, 1<= size <= 1000, time scale = 4 us
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern double RM_PDDgetPCBtemp(); // Celcius Degree
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDgetIO(); // bit1 (1) : input0, bit2 (2) : input1, bit3(4) : input2, bit4(8) : input3, bit n = 0 active, 1 deactive
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern double RM_PDDgetCurrentVolt(); // get the output voltage

        // DCDC debug
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDsetVolt(double target); // set DCDC voltage  30 <= target <= 140
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern double RM_PDDgetVolt(); // current DCDC output voltage
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDgetUnderVolt(); // if voltage drop occurred, return 1. That means DCDC failure.
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDclearUnderVolt(); // clear error flag
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDgetReadyHV(); // if DCDC voltage is greater than target - 4 V, return 1
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDenableHighVolt();
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDdisableHighVolt();

        // FRAM : index = 0 ~ 127, 4 byte signed integer or 4 byte float value
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern double RM_PDDfreadFRAM(int index);
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDireadFRAM(int index);
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDfwriteFRAM(int index, double data);
        [DllImport("RMDLL2.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_PDDiwriteFRAM(int index, int data);

        // EBus functions
        [DllImport("RMDLL2.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_EBuswrite(int address, int data);
        [DllImport("RMDLL2.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RM_EBusread(int address);
    }
}

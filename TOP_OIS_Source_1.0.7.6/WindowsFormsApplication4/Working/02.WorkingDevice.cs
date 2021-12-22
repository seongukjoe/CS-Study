using System;
using System.IO;
using XModule.Standard;
using System.Windows.Forms;

namespace XModule.Working
{
    public class WorkingDeviceLaser
    {
        public string Laser_IP;
        public int Laser_SocketPortNumber;

        public WorkingDeviceLaser()
        {
            Clear();
            Open();
        }

        public void Clear()
        {
            Laser_IP = "";
            Laser_SocketPortNumber = 0;
        }

        public bool Open()
        {
            string sPath = Application.StartupPath + "\\Config\\DeviceLaser.Config";
            string Section = string.Empty;

            if (!File.Exists(sPath))
                return false;
            fpIni Files = new fpIni(sPath);
            Section = "DeviceLaser";

            Laser_IP = Files.ReadString(Section, "Laser_IP", Laser_IP);
            Laser_SocketPortNumber = Files.ReadInteger(Section, "Laser_SocketPortNumber", Laser_SocketPortNumber);

            return true;
        }

        public bool Save()
        {
            String Section = "";
            string sPath = Application.StartupPath + "\\Config\\DeviceLaser.Config";
            fpIni Files = new fpIni(sPath);


            Section = "DeviceLaser";

            Files.WriteString(Section, "Laser_IP", Laser_IP);
            Files.WriteInteger(Section, "Laser_SocketPortNumber", Laser_SocketPortNumber);
            

            return true;
        }
    }
 
    public class WorkingDeviceDispSensor
    {
     

        public string Pizco1_Port;
        public int Pizco1_Baudrate;

        public string Pizco2_Port;
        public int Pizco2_Baudrate;

        public int DispenserType;
 

        public WorkingDeviceDispSensor()
        {
            Clear();
            Open();
        }
        public void Clear()
        {
            Pizco1_Port = "COM2";
            Pizco1_Baudrate = 0;
            Pizco2_Port = "";
            Pizco2_Baudrate = 0;
            DispenserType = 0;
        }

        public bool Open()
        {
            string sPath = Application.StartupPath + "\\Config\\DeviceDispSensor.Config";
            string Section = string.Empty;

            if (!File.Exists(sPath))
                return false;
            fpIni Files = new fpIni(sPath);
            Section = "DeviceDispSensor";

            Pizco1_Port = Files.ReadString(Section, "Pizco1_Port", "");
            Pizco1_Baudrate = Files.ReadInteger(Section, "Pizco1_Baudrate", 0);

            Pizco2_Port = Files.ReadString(Section, "Pizco2_Port", "");
            Pizco2_Baudrate = Files.ReadInteger(Section, "Pizco2_Baudrate", 0);

            DispenserType = Files.ReadInteger(Section, "DispenserType", 0);

         
            return true;
        }

        public bool Save()
        {
            String Section = "";
            string sPath = Application.StartupPath + "\\Config\\DeviceDispSensor.Config";
            fpIni Files = new fpIni(sPath);

            Section = "DeviceDispSensor";

            Files.WriteString(Section, "Pizco1_Port", Pizco1_Port);
            Files.WriteInteger(Section, "Pizco1_Baudrate", Pizco1_Baudrate);

            Files.WriteString(Section, "Pizco2_Port", Pizco2_Port);
            Files.WriteInteger(Section, "Pizco2_Baudrate", Pizco2_Baudrate);

          
            return true;
        }
    }

    public class WorkingDeviceLensHeight
    {

        public string ComPort;
        public int Baudrate;

        public WorkingDeviceLensHeight()
        {
            Clear();
            Open();
        }
        public void Clear()
        {
            ComPort = "COM1";
            Baudrate = 9600;
          
        }

        public bool Open()
        {
            string sPath = cPath.FILE_232;// Application.StartupPath + "\\Config\\PC232.DAT";
            string Section = string.Empty;

            if (!File.Exists(sPath))
                return false;
            fpIni Files = new fpIni(sPath);
            Section = "LensHeight";

            ComPort = Files.ReadString(Section, "PortName", ComPort);
            Baudrate = Files.ReadInteger(Section, "BaudRate", Baudrate);

            return true;
        }

        public bool Save()
        {
            String Section = "";
            string sPath = cPath.FILE_232;// Application.StartupPath + "\\Config\\PC232.DAT";
            fpIni Files = new fpIni(sPath);

            Section = "LensHeight";

            Files.WriteString(Section, "PortName", ComPort);
            Files.WriteInteger(Section, "BaudRate", Baudrate);


            return true;
        }
    }
    public class WorkingDeviceVision
    {
        public string LensTop_IP;
        public int LensTop_Port;

        public string LensBottom_IP;
        public int LensBottom_Port;

        public string VcmTop_IP;
        public int VcmTop_Port;

        public string Dipping1_IP;
        public int Dipping1_Port;

        public string Dipping2_IP;
        public int Dipping2_Port;

        public string Inspect_IP;
        public int Inspect_Port;



        public WorkingDeviceVision()
        {
            Clear();
            Open();
        }
        public void Clear()
        {
            LensTop_IP = "";
            LensTop_Port = 0;

            LensBottom_IP = "";
            LensBottom_Port = 0;

            VcmTop_IP = "";
            VcmTop_Port = 0;

            Dipping1_IP = "";
            Dipping1_Port = 0;

            Dipping2_IP = "";
            Dipping2_Port = 0;

            Inspect_IP = "";
            Inspect_Port = 0;
        }

        public bool Open()
        {
            string sPath = cPath.FILE_TCPIP;// Application.StartupPath + "\\Config\\Tcp.dat";
            string Section = string.Empty;

            if (!File.Exists(sPath))
                return false;
            fpIni Files = new fpIni(sPath);


            Section = "CLIENT_LENS_TOP";
            LensTop_IP = Files.ReadString(Section, "Address", LensTop_IP);
            LensTop_Port = Files.ReadInteger(Section, "PortNo", LensTop_Port);

            Section = "CLIENT_LENS_BOTTOM";
            LensBottom_IP = Files.ReadString(Section, "Address", LensBottom_IP);
            LensBottom_Port = Files.ReadInteger(Section, "PortNo", LensBottom_Port);

            Section = "CLIENT_VCM_TOP";
            VcmTop_IP = Files.ReadString(Section, "Address", VcmTop_IP);
            VcmTop_Port = Files.ReadInteger(Section, "PortNo", VcmTop_Port);

            Section = "CLIENT_DIPPING_1";
            Dipping1_IP = Files.ReadString(Section, "Address", Dipping1_IP);
            Dipping1_Port = Files.ReadInteger(Section, "PortNo", Dipping1_Port);

            Section = "CLIENT_DIPPING_2";
            Dipping2_IP = Files.ReadString(Section, "Address", Dipping2_IP);
            Dipping2_Port = Files.ReadInteger(Section, "PortNo", Dipping2_Port);

            Section = "CLIENT_INSPECT";
            Inspect_IP = Files.ReadString(Section, "Address", Inspect_IP);
            Inspect_Port = Files.ReadInteger(Section, "PortNo", Inspect_Port);

            return true;
        }

        public bool Save()
        {
            String Section = "";
            string sPath = cPath.FILE_TCPIP; //Application.StartupPath + "\\Config\\Tcp.dat";
            fpIni Files = new fpIni(sPath);


            Section = "CLIENT_LENS_TOP";
            Files.WriteString(Section, "Address", LensTop_IP);
            Files.WriteInteger(Section, "PortNo", LensTop_Port);

            Section = "CLIENT_LENS_BOTTOM";
            Files.WriteString(Section, "Address", LensBottom_IP);
            Files.WriteInteger(Section, "PortNo", LensBottom_Port);

            Section = "CLIENT_VCM_TOP";
            Files.WriteString(Section, "Address", VcmTop_IP);
            Files.WriteInteger(Section, "PortNo", VcmTop_Port);

            Section = "CLIENT_DIPPING_1";
            Files.WriteString(Section, "Address", Dipping1_IP);
            Files.WriteInteger(Section, "PortNo", Dipping1_Port);

            Section = "CLIENT_DIPPING_2";
            Files.WriteString(Section, "Address", Dipping2_IP);
            Files.WriteInteger(Section, "PortNo", Dipping2_Port);

            Section = "CLIENT_INSPECT";
            Files.WriteString(Section, "Address", Inspect_IP);
            Files.WriteInteger(Section, "PortNo", Inspect_Port);


            return true;
        }


    }


    public class WorkingDeviceMeasuring
    {
        public string FaceAngle_IP;
        public int FaceAngle_Port;

        public WorkingDeviceMeasuring()
        {
            Clear();
            Open();
        }

        public void Clear()
        {
            FaceAngle_IP = "";
            FaceAngle_Port = 0;
        }

        public bool Open()
        {
            string sPath = Application.StartupPath + "\\Config\\DeviceMeasuring.Config";
            string Section = string.Empty;

            if (!File.Exists(sPath))
                return false;
            fpIni Files = new fpIni(sPath);
            Section = "DeviceMeasuring";

            FaceAngle_IP = Files.ReadString(Section, "FaceAngle_IP", FaceAngle_IP);
            FaceAngle_Port = Files.ReadInteger(Section, "FaceAngle_Port", FaceAngle_Port);

            return true;
        }

        public bool Save()
        {
            String Section = "";
            string sPath = Application.StartupPath + "\\Config\\DeviceMeasuring.Config";
            fpIni Files = new fpIni(sPath);


            Section = "DeviceMeasuring";

            Files.WriteString(Section, "FaceAngle_IP", FaceAngle_IP);
            Files.WriteInteger(Section, "FaceAngle_Port", FaceAngle_Port);


            return true;
        }
    }  
}



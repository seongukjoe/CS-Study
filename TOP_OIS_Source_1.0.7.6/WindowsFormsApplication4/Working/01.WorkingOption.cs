using System;
using System.IO;
using XModule.Standard;
using System.Windows.Forms;

namespace XModule.Working
{
    public class WorkingOption
    {
       
        public int RunMode;
        public int Language = 0;

        public int DoorAlarmDispMode = 0; // 0: Disable 1: Output 2: Blink
        public int PickOverrideUse = 0; // 0: Not Use, 1: Use
        public int PlaceOverrideUse = 0; // 0: Not Use, 1: Use

        public string ActuatorPath1 = "";
        public string ActuatorPath2 = "";

        public bool[] IndexSkip = new bool[12];

        public int TorqueLimit = 0;
        public int TorqueLimitPick = 0;
        public int TorqueLimitTheta = 0;

        public int ResetTime1;
        public int ResetTime2;

        public bool LensPickerUpperTDirectionCCW;

        public double ProductDiameter = 0.0;

        public string MESAgentPath = "";
        public int Measure_RetryCount = 1;
        public int Measure_GoodCount = 1;
        public int ActuatingType = 0;
        public WorkingOption()
        {
            Clear();
        }

        public void Clear()
        {
            RunMode = 0;
            ResetTime1 = 0;
            ResetTime2 = 0;
        }

        public bool Open(String FileName)
        {
            string Section = string.Empty;
            string FullPath = cPath.FILE_PROJECT + FileName + "\\RunOption.ini";

            if (!File.Exists(FullPath))
                return false;

            fpIni Files = new fpIni(FullPath);
            Section = "RunOption";


            RunMode = Files.ReadInteger(Section, "RunMode", 0);
            Language = Files.ReadInteger(Section, "Language", 0);
            DoorAlarmDispMode = Files.ReadInteger(Section, "DoorAlarmDispMode", 0);
            PickOverrideUse = Files.ReadInteger(Section, "PickOverrideUse", 0);
            PlaceOverrideUse = Files.ReadInteger(Section, "PlaceOverrideUse", 0);

            ActuatorPath1 = Files.ReadString(Section, "ActuatorPath1", "");
            ActuatorPath2 = Files.ReadString(Section, "ActuatorPath2", "");

            for(int i=0; i<12; i++)
            {
                IndexSkip[i] = Files.ReadBoolean(Section, $"IndexSkip{i}", false);
            }

            TorqueLimit = Files.ReadInteger(Section, "TorqueLimit", 0);
            TorqueLimitPick = Files.ReadInteger(Section, "TorqueLimitPick", 0);
            TorqueLimitTheta = Files.ReadInteger(Section, "TorqueLimitTheta", 0);

            ResetTime1 = Files.ReadInteger(Section, "ResetTime1", 0);
            ResetTime2 = Files.ReadInteger(Section, "ResetTime2", 0);

            LensPickerUpperTDirectionCCW = Files.ReadBoolean(Section, "LensPickerUpperTDirectionCCW", false);

            ProductDiameter = Files.ReadDouble(Section, "ProductDiameter", 10.6);
            MESAgentPath = Files.ReadString(Section, "MESAgentPath", "");

            ActuatingType = Files.ReadInteger(Section, "ActuatingType", 0);
            Measure_RetryCount = Files.ReadInteger(Section, "Measure_RetryCount", 1);
            Measure_GoodCount = Files.ReadInteger(Section, "Measure_GoodCount", 1);
            return true;
        }

        public bool Save(String FileName)
        {
            String Section = "RunOption";
            string FullPath = cPath.FILE_PROJECT + FileName + "\\RunOption.ini";
            fpIni Files = new fpIni(FullPath);

            Section = "RunOption";

            Files.WriteInteger(Section, "RunMode", RunMode);
            Files.WriteInteger(Section, "Language", Language);
            Files.WriteInteger(Section, "DoorAlarmDispMode", DoorAlarmDispMode);
            Files.WriteInteger(Section, "PickOverrideUse", PickOverrideUse);
            Files.WriteInteger(Section, "PlaceOverrideUse", PlaceOverrideUse);

            Files.WriteString(Section, "ActuatorPath1", ActuatorPath1);
            Files.WriteString(Section, "ActuatorPath2", ActuatorPath2);

            for (int i = 0; i < 12; i++)
            {
                Files.WriteBoolean(Section, $"IndexSkip{i}", IndexSkip[i]);
            }

            Files.WriteInteger(Section, "TorqueLimit", TorqueLimit);
            Files.WriteInteger(Section, "TorqueLimitPick", TorqueLimitPick);
            Files.WriteInteger(Section, "TorqueLimitTheta", TorqueLimitTheta);

            Files.WriteInteger(Section, "ResetTime1", ResetTime1);
            Files.WriteInteger(Section, "ResetTime2", ResetTime2);

            Files.WriteBoolean(Section, "LensPickerUpperTDirectionCCW", LensPickerUpperTDirectionCCW);
            Files.WriteDouble(Section, "ProductDiameter", ProductDiameter);

            Files.WriteString(Section, "MESAgentPath", MESAgentPath);
            
            Files.WriteInteger(Section, "Measure_RetryCount", Measure_RetryCount);
            Files.WriteInteger(Section, "Measure_GoodCount", Measure_GoodCount);
            return true;
        }
    }
}



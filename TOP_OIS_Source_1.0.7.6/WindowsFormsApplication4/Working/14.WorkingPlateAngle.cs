using XModule.Standard;

namespace XModule.Working
{
    public class WorkingPlateAngle : BaseData
    {
        private  string section;
        
        // Delay
        public int MovingDelay;

        public int DefaultValue;            // 기준 값
        public double MinLimit;                // 합격 범위 Min
        public double MaxLimit;                // 합격 범위 Max
        public double RowDataMinLimit;
        public double RowDataMaxLimit;
        public double LensBotRowDataMinLimit; //ksyoon, SideAngle
        public double LensBotRowDataMaxLimit; //ksyoon, SideAngle
        public int SideAnglePoint; //ksyoon
        public int SideAngleLensBottomPoint; //ksyoon, SideAngle
        public int SideAngleVCMPoint; //ksyoon, SideAngle
        public bool SideAngleFailCheck; //ksyoon, SideAgnle

        public int MeasureTime;

        public int VacuumDelay;
        public int BlowDelay;

        public int ActuatorTime;

        public double RnRPercent;
        public double RnRShift;

        public WorkingPlateAngle(string name) : base(cDefString.PlateAngle)
        {
            Clear();
            //Load();       Recipe 관리를 위해 생성될때 오픈 안함. 
        }
        public void RecipeOpen(string FileName)
        {
            ini.FileName = FileName;
            Load();
        }
        public void RecipeSave(string FileName)
        {
            ini.FileName = FileName;
            Save();
        }
        public void Clear()
        {
            DefaultValue = 0;
            MinLimit = 0;
            MaxLimit = 0;
            RowDataMaxLimit = 0;
            RowDataMinLimit = 0;
            LensBotRowDataMinLimit = 0; //ksyoon, SideAngle
            LensBotRowDataMaxLimit = 0; //ksyoon, SideAngle
            SideAnglePoint = 0; //ksyoon
            SideAngleLensBottomPoint = 0; //ksyoon, SideAngle
            SideAngleVCMPoint = 0; //ksyoon, SideAngle
            SideAngleFailCheck = false; //ksyoon, SideAngle

            MovingDelay = 0;

            MeasureTime = 0;

            VacuumDelay = 0;
            BlowDelay = 0;
            ActuatorTime = 0;

            RnRPercent = 0.0;
            RnRShift = 0.0;
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.PlateAngle;

            ini.DataProc(type, section, "MovingDelay", ref MovingDelay);

            ini.DataProc(type, section, "DefaultValue", ref DefaultValue);
            ini.DataProc(type, section, "MinLimit", ref MinLimit);                  // 단위는 분
            ini.DataProc(type, section, "MaxLimit", ref MaxLimit);
            ini.DataProc(type, section, "RowDataMinLimit", ref RowDataMinLimit);
            ini.DataProc(type, section, "RowDataMaxLimit", ref RowDataMaxLimit);
            ini.DataProc(type, section, "LensBottomRowDataMinLimit", ref LensBotRowDataMinLimit); //ksyoon, SideAngle
            ini.DataProc(type, section, "LensBottomRowDataMaxLimit", ref LensBotRowDataMaxLimit); //ksyoon, SideAngle
            ini.DataProc(type, section, "SideAnglePointNumber", ref SideAnglePoint); //ksyoon
            ini.DataProc(type, section, "SideAngleLensBottomPointNumber", ref SideAngleLensBottomPoint); //ksyoon, SideAngle
            ini.DataProc(type, section, "SideAngleVCMPointNumber", ref SideAngleVCMPoint); //ksyoon, SideAngle

            ini.DataProc(type, section, "MeasureTime", ref MeasureTime);

            ini.DataProc(type, section, "VacuumDelay", ref VacuumDelay);
            ini.DataProc(type, section, "BlowDelay", ref BlowDelay);
            ini.DataProc(type, section, "ActuatorTime", ref ActuatorTime);

            ini.DataProc(type, section, "RnRPercent", ref RnRPercent);
            ini.DataProc(type, section, "RnRShift", ref RnRShift);
        }
       
        

    }

    public class WorkingTeachingPlateAngle : BaseData
    {
        private string section;
        #region Position
        // Position
        // Plate Angle
        public int ReadyPosition = 0;               // JigPlateAngle Ready Position;
        public int WorkPosition = 0;                // JigPlateAngle Work Position; 

        #endregion
        public WorkingTeachingPlateAngle(string name) : base(cDefString.PlateAngle)
        {
            Clear();
         //   Load(); 
        }

        public void Clear()
        {
            ReadyPosition = 0;
            WorkPosition = 0;
        }
        public void RecipeOpen(string FileName)
        {
            ini.FileName = FileName;
            Load();
        }
        public void RecipeSave(string FileName)
        {
            ini.FileName = FileName;
            Save();
        }
        public override void DataProc(fpIni.IniType type)
        {
            section = cDefString.PlateAngle;
            ini.DataProc(type, section, "ReadyPosition", ref ReadyPosition);
            ini.DataProc(type, section, "WorkPosition", ref WorkPosition);
        }
    }
}


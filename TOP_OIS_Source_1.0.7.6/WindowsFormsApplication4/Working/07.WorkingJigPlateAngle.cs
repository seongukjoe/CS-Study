using XModule.Standard;

namespace XModule.Working
{
    public class WorkingJigPlateAngle : BaseData
    {
        private  string section;

        
        // Delay
        public int MovingDelay;

        public int MeasureTime;

        public WorkingJigPlateAngle(string name) : base(cDefString.JigPlateAngle)
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
            MovingDelay = 0;
            MeasureTime = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.JigPlateAngle;

            ini.DataProc(type, section, "MovingDelay", ref MovingDelay);
            ini.DataProc(type, section, "MeasureTime", ref MeasureTime);
        }
    }
    public class WorkingTeachingJigPlateAngle : BaseData
    {
        private string section;

        #region Position
        // Position
        //  Jig Plate Angle
        public int ReadyPosition = 0;               // JigPlateAngle Ready Position;
        public int WorkPosition = 0;                // JigPlateAngle Work Position; 

        // Delay
        public int MovingDelay;

        public int MeasureTime;
        #endregion

        public WorkingTeachingJigPlateAngle(string name) : base(cDefString.JigPlateAngle)
        {
            Clear();
            //Load();
        }
       
        public void Clear()
        {
            ReadyPosition = 0;
            WorkPosition = 0;

            MovingDelay = 0;
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
            section = cDefString.JigPlateAngle;

            ini.DataProc(type, section, "ReadyPosition", ref ReadyPosition);
            ini.DataProc(type, section, "WorkPosition", ref WorkPosition);

            ini.DataProc(type, section, "MovingDelay", ref MovingDelay);
        }
    }
}


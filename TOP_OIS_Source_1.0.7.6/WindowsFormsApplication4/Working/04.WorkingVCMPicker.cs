using XModule.Standard;

namespace XModule.Working
{
    public class WorkingVCMPicker : BaseData
    {
        private string section;

        // Delay
        public int MovingDelayX;
        public int MovingDelayY;
        public int MovingDelayZ;
        public int MovingDelayT;
        public int StepPlaceSpeed;
        

        public int VCMVacDelay;
        public int VCMBlowDelay;

        

        public WorkingVCMPicker(string name) : base(cDefString.VCMPicker)
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
            MovingDelayX = 0;
            MovingDelayY = 0;
            MovingDelayZ = 0;
            MovingDelayT = 0;
            StepPlaceSpeed = 0;

            VCMVacDelay = 0;
            VCMBlowDelay = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.VCMPicker;

            ini.DataProc(type, section, "MovingDelayX", ref MovingDelayX);
            ini.DataProc(type, section, "MovingDelayY", ref MovingDelayY);
            ini.DataProc(type, section, "MovingDelayZ", ref MovingDelayZ);
            ini.DataProc(type, section, "MovingDelayT", ref MovingDelayT);
            ini.DataProc(type, section, "StepPlaceSpeed", ref StepPlaceSpeed);

            ini.DataProc(type, section, "VCMVacDelay", ref VCMVacDelay);
            ini.DataProc(type, section, "VCMBlowDelay", ref VCMBlowDelay);
        }



    }
    public class WorkingTeachingVCMPicker : BaseData
    {
        private string section;

        #region Position
        // Position
        //  Head
        public int ReadyPositionX = 0;              //Head Ready Position   
        public int ReadyPositionY = 0;              //Head Ready Position   
        public int ReadyPositionZ = 0;              //Head Ready Position   
        public int ReadyPositionT = 0;              //Head Ready Position   

        public int StageFirstPickPositionX;              // Head Pick
        public int StagePickPositionY;              // Head Pick
        public int StagePickPositionZ;              // Head Pick
        public int StagePickPositionT;              // Head Pick

        public int IndexPlacePositionX;              // Head Place
        public int IndexPlacePositionY;              // Head Place
        public int IndexPlacePositionZ;              // Head Place
        public int IndexStepPlaceOffset;
        public int ClampPlacePositionZ;                   // Head Clamp Place
        public int IndexPlacePositionT;              // Head Place

       
        #endregion

        public WorkingTeachingVCMPicker(string name) : base(cDefString.VCMPicker)
        {
            Clear();
            //Load();
        }

        public void Clear()
        {
            ReadyPositionX = 0;
            ReadyPositionY = 0;
            ReadyPositionZ = 0;
            ReadyPositionT = 0;

            StageFirstPickPositionX = 0;
            StagePickPositionY = 0;
            StagePickPositionZ = 0;
            StagePickPositionT = 0;

            IndexPlacePositionX = 0;
            IndexPlacePositionY = 0;
            IndexPlacePositionZ = 0;
            IndexStepPlaceOffset = 0;
            ClampPlacePositionZ = 0;
            IndexPlacePositionT = 0;

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

            section = cDefString.VCMPicker;

            ini.DataProc(type, section, "ReadyPositionX", ref ReadyPositionX);
            ini.DataProc(type, section, "ReadyPositionY", ref ReadyPositionY);
            ini.DataProc(type, section, "ReadyPositionZ", ref ReadyPositionZ);
            ini.DataProc(type, section, "ReadyPositionT", ref ReadyPositionT);

            ini.DataProc(type, section, "StageFirstPickPositionX", ref StageFirstPickPositionX);
            ini.DataProc(type, section, "StagePickPositionY", ref StagePickPositionY);
            ini.DataProc(type, section, "StagePickPositionZ", ref StagePickPositionZ);
            ini.DataProc(type, section, "StagePickPositionT", ref StagePickPositionT);

            ini.DataProc(type, section, "IndexPlacePositionX", ref IndexPlacePositionX);
            ini.DataProc(type, section, "IndexPlacePositionY", ref IndexPlacePositionY);
            ini.DataProc(type, section, "IndexPlacePositionZ", ref IndexPlacePositionZ);
            ini.DataProc(type, section, "IndexStepPlaceOffset", ref IndexStepPlaceOffset);

            ini.DataProc(type, section, "ClampPlacePositionZ", ref ClampPlacePositionZ);
            ini.DataProc(type, section, "IndexPlacePositionT", ref IndexPlacePositionT);



        }
    }
}


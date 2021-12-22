using XModule.Standard;

namespace XModule.Working
{
    public class WorkingUnloadPicker : BaseData
    {
        private  string section;

        #region Position
        
        // Delay
        public int MovingDelayX;
        public int MovingDelayY;
        public int MovingDelayZ;
        public int MovingDelayT;

        public int StepPlaceSpeed;
        public int NgStepPlaceSpeed;

        public int UnloaderVacDelay;
        public int UnloaderBlowDelay;
        
        #endregion

        public WorkingUnloadPicker(string name) : base(cDefString.UnloadPicker)
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
            NgStepPlaceSpeed = 0;

            UnloaderVacDelay = 0;
            UnloaderBlowDelay = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.UnloadPicker;

            ini.DataProc(type, section, "MovingDelayX", ref MovingDelayX);
            ini.DataProc(type, section, "MovingDelayY", ref MovingDelayY);
            ini.DataProc(type, section, "MovingDelayZ", ref MovingDelayZ);
            ini.DataProc(type, section, "MovingDelayT", ref MovingDelayT);

            ini.DataProc(type, section, "StepPlaceSpeed", ref StepPlaceSpeed);
            ini.DataProc(type, section, "NgStepPlaceSpeed", ref NgStepPlaceSpeed);

            ini.DataProc(type, section, "UnloaderVacDelay", ref UnloaderVacDelay);
            ini.DataProc(type, section, "UnloaderBlowDelay", ref UnloaderBlowDelay);
        }
       
        

    }

    public class WorkingTeachingUnloadPicker : BaseData
    {
        private string section;

        #region Position
        // Position
        //  Head
        public int ReadyPositionX = 0;              //Head Ready Position   
        public int ReadyPositionY = 0;              //Head Ready Position   
        public int ReadyPositionZ = 0;              //Head Ready Position   
        public int ReadyPositionT = 0;              //Head Ready Position   

        public int IndexPickPositionX;              // Head Pick
        public int IndexPickPositionY;              // Head Pick
        public int IndexPickPositionZ;              // Head Pick
        public int IndexPickPositionT;              // Head Pick

        public int StageFirstPlacePositionX;             // Head Place
        public int StagePlacePositionY;             // Head Place
        public int StagePlacePositionZ;             // Head Place
        public int StageStepPlaceOffset;
        public int StagePlacePositionT;             // Head Place

        public int NGTrayPositionX;                 // Head NG Tray Place (NG Tray StartPosition)
        public int NGTrayPositionY;                 // Head NG Tray Place (NG Tray StartPosition)
        public int NGTrayPositionZ;                 // Head NG Tray Place
        public int NGTrayStepPlaceOffset;
        public int NGTrayPositionT;

        public int AvoidPositionX;
        public int AvoidPositionY;
        #endregion

        public WorkingTeachingUnloadPicker(string name) : base(cDefString.UnloadPicker)
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

            IndexPickPositionX = 0;
            IndexPickPositionY = 0;
            IndexPickPositionZ = 0;
            IndexPickPositionT = 0;

            StageFirstPlacePositionX = 0;
            StagePlacePositionY = 0;
            StagePlacePositionZ = 0;
            StageStepPlaceOffset = 0;
            StagePlacePositionT = 0;

            NGTrayPositionX = 0;
            NGTrayPositionY = 0;
            NGTrayPositionZ = 0;
            NGTrayStepPlaceOffset = 0;
            NGTrayPositionT = 0;

            AvoidPositionX = 0;
            AvoidPositionY = 0;
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
            section = cDefString.UnloadPicker;

            ini.DataProc(type, section, "ReadyPositionX", ref ReadyPositionX);
            ini.DataProc(type, section, "ReadyPositionY", ref ReadyPositionY);
            ini.DataProc(type, section, "ReadyPositionZ", ref ReadyPositionZ);
            ini.DataProc(type, section, "ReadyPositionT", ref ReadyPositionT);

            ini.DataProc(type, section, "IndexPickPositionX", ref IndexPickPositionX);
            ini.DataProc(type, section, "IndexPickPositionY", ref IndexPickPositionY);
            ini.DataProc(type, section, "IndexPickPositionZ", ref IndexPickPositionZ);
            ini.DataProc(type, section, "IndexPickPositionT", ref IndexPickPositionT);

            ini.DataProc(type, section, "StageFirstPlacePositionX", ref StageFirstPlacePositionX);
            ini.DataProc(type, section, "StagePlacePositionY", ref StagePlacePositionY);
            ini.DataProc(type, section, "StagePlacePositionZ", ref StagePlacePositionZ);
            ini.DataProc(type, section, "StageStepPlaceOffset", ref StageStepPlaceOffset);
            ini.DataProc(type, section, "StagePlacePositionT", ref StagePlacePositionT);

            ini.DataProc(type, section, "NGTrayPositionX", ref NGTrayPositionX);
            ini.DataProc(type, section, "NGTrayPositionY", ref NGTrayPositionY);
            ini.DataProc(type, section, "NGTrayPositionZ", ref NGTrayPositionZ);
            ini.DataProc(type, section, "NGTrayStepPlaceOffset", ref NGTrayStepPlaceOffset);
            ini.DataProc(type, section, "NGTrayPositionT", ref NGTrayPositionT);

            ini.DataProc(type, section, "AvoidPositionX", ref AvoidPositionX);
            ini.DataProc(type, section, "AvoidPositionX", ref AvoidPositionY);
        }
    }
}


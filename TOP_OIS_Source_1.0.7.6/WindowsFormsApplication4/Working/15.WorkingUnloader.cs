using XModule.Standard;

namespace XModule.Working
{
    public class WorkingUnloader : BaseData
    {
        private  string section;

        #region Magazine
        // 기능 Pitch
        public int SlotCount = 5;                   // Magazine Slot Count;
        public int SlotPitchZ = 0;                  // Magazine Slot Pitch   
       
        #endregion

        #region Tray
        public int TrayCountX = 0;                  // Tray Count;
        public int TrayCountY = 0;                  // Tray Count;
        public int TrayPitchX = 0;                  // Tray Pitch;
        public int TrayPitchY = 0;                  // Tray Pitch;

        public int NG_TrayCountX = 0;               // NG Tray Count;
        public int NG_TrayCountY = 0;
        public int NG_TrayPitchX = 0;               // NG Tray Pitch;
        public int NG_TrayPitchY = 0;
        #endregion

        // Delay
        public int MovingDelayZ;
        public int MovingDelayY;
        public int MovingDelayX;
        

        public WorkingUnloader(string name) : base(cDefString.Unloader)
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
            SlotCount = 0;
            SlotPitchZ = 0;
            

            TrayCountX = 0;
            TrayCountY = 0;
            TrayPitchX = 0;
            TrayPitchY = 0;

            NG_TrayCountX = 0;
            NG_TrayCountX = 0;
            NG_TrayPitchY = 0;
            NG_TrayPitchY = 0;

            MovingDelayZ = 0;
            MovingDelayY = 0;
            MovingDelayX = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.Unloader;

            ini.DataProc(type, section, "SlotCount", ref SlotCount);
            ini.DataProc(type, section, "SlotPitchZ", ref SlotPitchZ);
            

            ini.DataProc(type, section, "TrayCountX", ref TrayCountX);
            ini.DataProc(type, section, "TrayCountY", ref TrayCountY);
            ini.DataProc(type, section, "TrayPitchX", ref TrayPitchX);
            ini.DataProc(type, section, "TrayPitchY", ref TrayPitchY);

            ini.DataProc(type, section, "NG_TrayCountX", ref NG_TrayCountX);
            ini.DataProc(type, section, "NG_TrayCountY", ref NG_TrayCountY);
            ini.DataProc(type, section, "NG_TrayPitchX", ref NG_TrayPitchX);
            ini.DataProc(type, section, "NG_TrayPitchY", ref NG_TrayPitchY);

            ini.DataProc(type, section, "MovingDelayZ", ref MovingDelayZ);
            ini.DataProc(type, section, "MovingDelayY", ref MovingDelayY);
            ini.DataProc(type, section, "MovingDelayX", ref MovingDelayX);
        }
       
    }

    public class WorkingTeachingUnloader : BaseData
    {
        private string section;

        #region Position
        // Position
        //  Magazine
        public int ReadyPositionZ = 0;              //Magazine Ready Position   Slot 0번 위치.
        public int InOutOffsetZ = 0;                // 투입. 배출 시 Offset Z
        //  transfer
        public int ReadyPositionX = 0;              // Transfer Ready Position;
        public int StagePositionX = 0;              // Transfer Stage Position; 
        public int MagazinePositionX = 0;           // Transfer MagazinePosition;

        // Stage
        public int ReadyPositionY = 0;              // Stage Ready Position;
        public int EjectPositionY = 0;              // Stage Eject Position;
        public int StageFirstPlacePositionY;
        public int NGTrayFirstPlacePositionY;
        public int WorkPositionY = 0;               // Stage Work Position;
        public int StageMagazineChangePositionY = 0;
        #endregion

        public WorkingTeachingUnloader(string name) : base(cDefString.Unloader)
        {
            Clear();
            //Load();
        }

        public void Clear()
        {
            ReadyPositionZ = 0;
            InOutOffsetZ = 0;

            ReadyPositionX = 0;
            StagePositionX = 0;
            MagazinePositionX = 0;

            ReadyPositionY = 0;
            EjectPositionY = 0;
            StageFirstPlacePositionY = 0;
            NGTrayFirstPlacePositionY = 0;
            WorkPositionY = 0;
            StageMagazineChangePositionY = 0;

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
            section = cDefString.Unloader;

            ini.DataProc(type, section, "ReadyPositionZ", ref ReadyPositionZ);
            ini.DataProc(type, section, "InOutOffsetZ", ref InOutOffsetZ);

            ini.DataProc(type, section, "ReadyPositionX", ref ReadyPositionX);
            ini.DataProc(type, section, "StagePositionX", ref StagePositionX);
            ini.DataProc(type, section, "MagazinePositionX", ref MagazinePositionX);

            ini.DataProc(type, section, "ReadyPositionY", ref ReadyPositionY);
            ini.DataProc(type, section, "EjectPositionY", ref EjectPositionY);
            ini.DataProc(type, section, "StageFirstPlacePositionY", ref StageFirstPlacePositionY);
            ini.DataProc(type, section, "NGTrayFirstPlacePositionY", ref NGTrayFirstPlacePositionY);
            ini.DataProc(type, section, "WorkPositionY", ref WorkPositionY);
            ini.DataProc(type, section, "StageMagazineChangePositionY", ref StageMagazineChangePositionY);
        }



    }
}


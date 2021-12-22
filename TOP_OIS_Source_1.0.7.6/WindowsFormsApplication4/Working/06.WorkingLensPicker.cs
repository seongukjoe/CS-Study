  using XModule.Standard;

namespace XModule.Working
{
    public class WorkingLensPicker : BaseData
    {
        private  string section;

        
        // Delay
        public int MovingDelayX;
        public int MovingDelayY;
        public int MovingDelayZ;
        public int MovingDelayT;
        public int StepPlaceSpeed;
        public int BottomCamStepSpeed;

        public int LensVacDelay;
        public int LensBlowDelay;

        public int LensUpperGrabDelay;
        public int LensUnderGrabDelay;

        public double SecondaryCorrLimit;
        

        public WorkingLensPicker(string name) : base(cDefString.LensPicker)
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
            BottomCamStepSpeed = 0;

            LensVacDelay = 0;
            LensBlowDelay = 0;

            LensUpperGrabDelay = 0;
            LensUnderGrabDelay = 0;

            SecondaryCorrLimit = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {
            section = cDefString.LensPicker;

            ini.DataProc(type, section, "MovingDelayX", ref MovingDelayX);
            ini.DataProc(type, section, "MovingDelayY", ref MovingDelayY);
            ini.DataProc(type, section, "MovingDelayZ", ref MovingDelayZ);
            ini.DataProc(type, section, "MovingDelayT", ref MovingDelayT);
            ini.DataProc(type, section, "StepPlaceSpeed", ref StepPlaceSpeed);
            ini.DataProc(type, section, "BottomCamStepSpeed", ref BottomCamStepSpeed);

            ini.DataProc(type, section, "LensVacDelay", ref LensVacDelay);
            ini.DataProc(type, section, "LensBlowDelay", ref LensBlowDelay);

            ini.DataProc(type, section, "LensUpperGrabDelay", ref LensUpperGrabDelay);
            ini.DataProc(type, section, "LensUnderGrabDelay", ref LensUnderGrabDelay);

            ini.DataProc(type, section, "SecondaryCorrLimit", ref SecondaryCorrLimit);

        }
       
        

    }

    public class WorkingTeachingLensPicker : BaseData
    {
        private string section;

        #region Position
        // Position
        //  Head
        public int ReadyPositionX = 0;              //Head Ready Position   
        public int ReadyPositionY = 0;              //Head Ready Position   
        public int ReadyPositionZ = 0;              //Head Ready Position   
        public int ReadyPositionT = 0;              //Head Ready Position   

        public int StagePickPositionX;              // Head Pick
        public int StageFirstPickPositionY;              // Head Pick
        public int StagePickPositionZ;              // Head Pick
        public int StageStepPickOffset;
        public int StagePickPositionT;              // Head Pick

        public int IndexPlacePositionX;              // Head Place
        public int IndexPlacePositionY;              // Head Place
        public int IndexPlacePositionZ;              // Head Place
        public int IndexStepPlaceOffset;
        public int IndexPlacePositionT;              // Head Place

        public int BottomCamPositionX;               // Cam Pos X
        public int BottomCamPositionY;               // Cam Pos Y
        public int BottomCamPositionZ;               // Cam Pos Z
        public int BottomCamStepOffset;
        public int BottomCamPositionT;               // Cam Pos T
        public int LensOffsetT;

        // Offset
        public int CameraDistanceOffsetX;            // Camera - Picker Distance
        public int CameraDistanceOffsetY;

        public int LockingUp;
        public int LockingPositionT;

        //public int PlaceUserOffsetX;
        //public int PlaceUserOffsetY;

        //public int[] PlaceUserOffsetX;
        //public int[] PlaceUserOffsetY;

        #endregion

        public WorkingTeachingLensPicker(string name) : base(cDefString.LensPicker)
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

            StagePickPositionX = 0;
            StageFirstPickPositionY = 0;
            StagePickPositionZ = 0;
            StageStepPickOffset = 0;
            StagePickPositionT = 0;

            IndexPlacePositionX = 0;
            IndexPlacePositionY = 0;
            IndexPlacePositionZ = 0;
            IndexStepPlaceOffset = 0;
            IndexPlacePositionT = 0;

            BottomCamPositionX = 0;
            BottomCamPositionY = 0;
            BottomCamPositionZ = 0;
            BottomCamStepOffset = 0;
            BottomCamPositionT = 0;
            LensOffsetT = 0;

            CameraDistanceOffsetX = 0;
            CameraDistanceOffsetY = 0;

            LockingUp = 0;
            LockingPositionT = 0;

            //PlaceUserOffsetX = 0;
            //PlaceUserOffsetY = 0;

            //PlaceUserOffsetX = new int[12];
            //PlaceUserOffsetY = new int[12];
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

            section = cDefString.LensPicker;

            ini.DataProc(type, section, "ReadyPositionX", ref ReadyPositionX);
            ini.DataProc(type, section, "ReadyPositionY", ref ReadyPositionY);
            ini.DataProc(type, section, "ReadyPositionZ", ref ReadyPositionZ);
            ini.DataProc(type, section, "ReadyPositionT", ref ReadyPositionT);

            ini.DataProc(type, section, "StagePickPositionX", ref StagePickPositionX);
            ini.DataProc(type, section, "StageFirstPickPositionY", ref StageFirstPickPositionY);
            ini.DataProc(type, section, "StagePickPositionZ", ref StagePickPositionZ);
            ini.DataProc(type, section, "StageStepPickOffset", ref StageStepPickOffset);
            ini.DataProc(type, section, "StagePickPositionT", ref StagePickPositionT);

            ini.DataProc(type, section, "IndexPlacePositionX", ref IndexPlacePositionX);
            ini.DataProc(type, section, "IndexPlacePositionY", ref IndexPlacePositionY);
            ini.DataProc(type, section, "IndexPlacePositionZ", ref IndexPlacePositionZ);
            ini.DataProc(type, section, "IndexStepPlaceOffset", ref IndexStepPlaceOffset);
            ini.DataProc(type, section, "IndexPlacePositionT", ref IndexPlacePositionT);

            ini.DataProc(type, section, "BottomCamPositionX", ref BottomCamPositionX);
            ini.DataProc(type, section, "BottomCamPositionY", ref BottomCamPositionY);
            ini.DataProc(type, section, "BottomCamPositionZ", ref BottomCamPositionZ);
            ini.DataProc(type, section, "BottomCamStepOffset", ref BottomCamStepOffset);
            ini.DataProc(type, section, "BottomCamPositionT", ref BottomCamPositionT);
            ini.DataProc(type, section, "LensOffsetT", ref LensOffsetT);

            ini.DataProc(type, section, "CameraDistanceOffsetX", ref CameraDistanceOffsetX);
            ini.DataProc(type, section, "CameraDistanceOffsetY", ref CameraDistanceOffsetY);

            ini.DataProc(type, section, "LockingUp", ref LockingUp);
            ini.DataProc(type, section, "LockingPositionT", ref LockingPositionT);

            //ini.DataProc(type, section, "PlaceUserOffsetX", ref PlaceUserOffsetX);
            //ini.DataProc(type, section, "PlaceUserOffsetY", ref PlaceUserOffsetY);

            //for (int i = 0; i < 12; i++)
            //{
            //    ini.DataProc(type, section, $"PlaceUserOffsetX_{i + 1}", ref PlaceUserOffsetX[i]);
            //    ini.DataProc(type, section, $"PlaceUserOffsetY_{i + 1}", ref PlaceUserOffsetY[i]);
            //}

        }



    }
}


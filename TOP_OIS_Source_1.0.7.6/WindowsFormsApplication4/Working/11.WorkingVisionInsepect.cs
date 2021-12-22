using XModule.Standard;

namespace XModule.Working
{
    public class WorkingVisionInspect : BaseData
    {
        private string section;

        #region Position
        // Position
        public int VCMVisionMovingDelay;

        public int VCMVisionGrabDelay;

        public int VisionInspectGrabDelay;

        
        #endregion

        public WorkingVisionInspect(string name) : base(cDefString.VisionInspect)
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
            VCMVisionMovingDelay = 0;

            VCMVisionGrabDelay = 0;

        
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.VisionInspect;

            ini.DataProc(type, section, "VCMVisionMovingDelay", ref VCMVisionMovingDelay);

            ini.DataProc(type, section, "VCMVisionGrabDelay", ref VCMVisionGrabDelay);

            ini.DataProc(type, section, "VisionInspectGrabDelay", ref VisionInspectGrabDelay);
            
        }


    }
    public class WorkingTeachingVisionInspect : BaseData
    {
        private string section;

        #region Position
        // Position
        public int VCMVisionReadyPosition;
        public int VCMVisionWorkPosition;


        public int VCMVisionMovingDelay;
        #endregion

        public WorkingTeachingVisionInspect(string name) : base(cDefString.VisionInspect)
        {
            Clear();
            //Load();
        }

        public void Clear()
        {
            VCMVisionReadyPosition = 0;
            VCMVisionWorkPosition = 0;
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

            section = cDefString.VisionInspect;

            ini.DataProc(type, section, "VCMVisionReadyPosition", ref VCMVisionReadyPosition);
            ini.DataProc(type, section, "VCMVisionWorkPosition", ref VCMVisionWorkPosition);
        }

    }
}

using XModule.Standard;

namespace XModule.Working
{
    public class WorkingIndex : BaseData
    {
        private  string section;

        public int StepPitch;            // 1 Step 이동 거리

        public int MovingDelay;           // 동작 후 지연

        public WorkingIndex(string name) : base(cDefString.Index)
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
            StepPitch = 0;
            MovingDelay = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.Index;

            ini.DataProc(type, section, "StepPitch", ref StepPitch);
            ini.DataProc(type, section, "MovingDelay", ref MovingDelay);
        }
       
        

    }
}


using XModule.Standard;

namespace XModule.Working
{
    public class WorkingCuring2 : BaseData
    {
        private  string section;

        public int CuringTime;           // 경화 시간
        public int ActuatorTime;

        public WorkingCuring2(string name) : base(cDefString.Curing2)
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
            CuringTime = 0;
            ActuatorTime = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.Curing2;

            ini.DataProc(type, section, "CuringTime", ref CuringTime);
            ini.DataProc(type, section, "ActuatorTime", ref ActuatorTime);
        }
       
        

    }
}


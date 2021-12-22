using XModule.Standard;

namespace XModule.Working
{
    public class WorkingCleanJig : BaseData
    {
        private  string section;

        public int CleanTime;           // 클린타임

        public WorkingCleanJig(string name) : base(cDefString.CleanJig)
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
            CleanTime = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.CleanJig;

            ini.DataProc(type, section, "CleanTime", ref CleanTime);
        }
       
        

    }
}


using XModule.Standard;

namespace XModule.Working
{
    public class WorkingLensHeight : BaseData
    {
        private  string section;

        //public int DefaultHeight;           // 기준 높이
        public int MinOver;                 // Lens 유무 Min
        public int MaxOver;                 // Lens 유무 Max
        //public int MinLimit;                // Lens 합격 범위 Min
        //public int MaxLimit;                // Lens 합격 범위 Max


        public double [] DefaultHeight;           // 기준 높이

        public int MeasureTime;

        public WorkingLensHeight(string name) : base(cDefString.LensHeight)
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
            //DefaultHeight = 0;
            MinOver = 0;
            MaxOver = 0;
            //MinLimit = 0;
            //MaxLimit = 0;

            DefaultHeight = new double[12];
        }

        public override void DataProc(fpIni.IniType type)
        {

            section = cDefString.LensHeight;

            //ini.DataProc(type, section, "DefaultHeight", ref DefaultHeight);
            ini.DataProc(type, section, "MinOver", ref MinOver);
            ini.DataProc(type, section, "MaxOver", ref MaxOver);
            //ini.DataProc(type, section, "MinLimit", ref MinLimit);
            //ini.DataProc(type, section, "MaxLimit", ref MaxLimit);

            for (int i = 0; i < DefaultHeight.Length; i++)
            {
                ini.DataProc(type, section, $"DefaultHeight_{i}", ref DefaultHeight[i]);
            }
            ini.DataProc(type, section, "MeasureTime", ref MeasureTime);
        }
       
        

    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;


namespace XModule
{
    public class LanguageUnit
    {
        public string Indexer;
        public string Language;
        //public string Korean;
        //public string English;
        //public string Chiness;
        //public string Vietnamese;

        public LanguageUnit()
        {
            Indexer = string.Empty;
            Language = string.Empty;
            //Korean = string.Empty;
            //English = string.Empty;
            //Chiness = string.Empty;
            //Vietnamese = string.Empty;
        }
    }
    public class Language : fpIni
    {
        public eLanguage type;
        private List<LanguageUnit> lstLang;

        public Language()
        {
            lstLang = new List<LanguageUnit>();
            Load();
        }
        
        public void Load()
        {
            lstLang.Clear();

            //string sPath = Application.StartupPath + $"\\Config\\Lang\\Lang.Dat";
            string sPath;
            if (cDEF.Work.Option.Language == (int)eLanguage.KOREA)
                sPath = Application.StartupPath + $"\\Config\\Lang\\KOREA.Dat";
            else if (cDEF.Work.Option.Language == (int)eLanguage.ENGLISH)
                sPath = Application.StartupPath + $"\\Config\\Lang\\ENGLISH.Dat";
            else if (cDEF.Work.Option.Language == (int)eLanguage.VIETNAM)
                sPath = Application.StartupPath + $"\\Config\\Lang\\VIETNAM.Dat";
            else
                sPath = Application.StartupPath + $"\\Config\\Lang\\CHINA.Dat";


            FileStream File = new FileStream(sPath, FileMode.OpenOrCreate, FileAccess.Read);

            if (!System.IO.File.Exists(sPath))
            {
                return;
            }
            
            StreamReader st = new StreamReader(File, Encoding.Default, true);

            st.BaseStream.Seek(0, SeekOrigin.Begin);
            while (st.Peek() > -1)
            {
                String FTemp = st.ReadLine();
                string[] sArr = FTemp.Split(',');


                LanguageUnit lu = new LanguageUnit();

                lu.Indexer = sArr[0];
                lu.Language = sArr[1];

                //lu.Korean = sArr[0];
                //lu.English = sArr[1];
                //lu.Chiness = sArr[2];
                //lu.Vietnamese = sArr[3];

                lstLang.Add(lu);

            }

            st.Close();
            File.Close();
            return ;
        }

    
        public string Trans(string msg, bool spaceToNewLine = false)
        {
            if (cDEF.Work.Option.Language == (int)eLanguage.KOREA)
                type = eLanguage.KOREA;
            else if (cDEF.Work.Option.Language == (int)eLanguage.ENGLISH)
                type = eLanguage.ENGLISH;
            else if (cDEF.Work.Option.Language == (int)eLanguage.VIETNAM)
                type = eLanguage.VIETNAM;
            else
                type = eLanguage.CHINA;

            foreach (LanguageUnit lu in lstLang)
            {
                
                switch (type)
                {
                    case eLanguage.KOREA:
                        if (lu.Indexer == msg)
                        {
                            return lu.Language;
                        }
                        break;
                    case eLanguage.ENGLISH:
                        if (lu.Indexer == msg)
                        {
                            return lu.Language;
                        }
                        break;
                    case eLanguage.CHINA:
                        if (lu.Indexer == msg)
                        {
                            return lu.Language;
                        }
                        break;
                    case eLanguage.VIETNAM:
                        if (lu.Indexer == msg)
                        {
                            return lu.Language;
                        }
                        break;
                }
                
            }
            return "Not Trans";
        }



    }
}

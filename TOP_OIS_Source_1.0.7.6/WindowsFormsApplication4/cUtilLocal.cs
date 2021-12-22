using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace XModule.Standard
{
    public static class cUtilLocal
    {


        #region "INI파일 읽기/쓰기"
        public static int RDInt(string FileName, string Section, string Title, int Value)
        {
            return cAPI.brDll.RDInt(FileName, Section, Title, Value);
        }
        public static void WRInt(string FileName, string Section, string Title, int Value)
        {
            cAPI.brDll.WRInt(FileName, Section, Title, Value);
        }

        public static double RDDouble(string FileName, string Section, string Title, double Value)
        {
            return cAPI.brDll.RDDouble(FileName, Section, Title, Value);
        }
        public static void WRDouble(string FileName, string Section, string Title, double Value)
        {
            cAPI.brDll.WRDouble(FileName, Section, Title, Value);
        }

        public static string RDStr(string FileName, string Section, string Title, string Value)
        {
            return cAPI.brDll.RDStr(FileName, Section, Title, Value);
        }
        public static void WRStr(string FileName, string Section, string Title, string Value)
        {
            cAPI.brDll.WRStr(FileName, Section, Title, Value);
        }

        public static bool RDBool(string FileName, string Section, string Title, int Value)
        {
            int iValue = cAPI.brDll.RDInt(FileName, Section, Title, Value);
            if (iValue == 1) return true;
            return false;
        }
        public static void WRBool(string FileName, string Section, string Title, bool Value)
        {
            int iValue = Value == true ? 1 : 0;
            cAPI.brDll.WRInt(FileName, Section, Title, iValue);
        }

        #endregion "INI파일 읽기/쓰기"

        public static void WRFile(string fn, string s, bool appeded)
        {
            int fm = (int)FileMode.Create;
            if (appeded) fm = (int)FileMode.Append;

            FileStream fs = new FileStream(fn, (FileMode)fm);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.Write(s);
            sw.Flush();
            sw.Close();
        }
        /*
        #region 임시 ini

        private string _Filename;

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(    // ini Read 함수
                    string section,
                    string key,
                    string def,
                    StringBuilder retVal,
                    int size,
                    string filePath);
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(  // ini Write 함수
                    string section,
                    string key,
                    string val,
                    string filePath);

        public cUtilLocal(string fname)
        {
            _Filename = fname;
        }
        public void WriteString(string section, string key, string value)
        {
            WritePrivateProfileString (section, key, value, _Filename);
        }
        public String ReadString(string section, string key, string defvalue)
        {
            StringBuilder temp = new StringBuilder(4096);
            int i = GetPrivateProfileString (section, key, defvalue, temp, 4096, _Filename);
            return temp.ToString();
        }
        public void WriteInteger(string section, string key, int value)
        {
            WritePrivateProfileString(section, key, value.ToString(), _Filename);
        }
        public int ReadInteger(string section, string key, int defvalue)
        {
            StringBuilder temp = new StringBuilder(4096);
            int i = GetPrivateProfileString (section, key, defvalue.ToString (), temp, 4096, _Filename);
            try{
              return int.Parse(temp.ToString());
            }
            catch{
              return defvalue;
            }
        }
        public void WriteBoolean(string section, string key, bool Value)
        {
            WritePrivateProfileString (section, key, Value.ToString (), _Filename);
        }
        public Boolean ReadBoolean(string section, string key, bool defvalue)
        {
            StringBuilder temp = new StringBuilder(4096);
            int i = GetPrivateProfileString (section, key, defvalue.ToString (), temp, 4096, _Filename);
            try{
                return bool.Parse(temp.ToString());
            }
            catch 
            {
                return defvalue;
            }
        }
        public void WriteDouble(string section, string key, double Value)
        {
            WritePrivateProfileString (section, key, Value.ToString (), _Filename);
        }
        public double ReadDouble(string section, string key, double defvalue)
        {
            StringBuilder temp = new StringBuilder(4096);
            int i = GetPrivateProfileString (section, key, defvalue.ToString (), temp, 4096, _Filename);
            try
            {
                return double.Parse(temp.ToString());
            }
            catch
            {
                return defvalue;
            }
        }
        public string FileName
        {
            get { return _Filename; }
            set { _Filename = value; }
        }
        #endregion 임시 ini
        */


        /*

        #region "1초마다 저장데이터 READ/WRITE"

        #endregion "1초마다 저장테이터 READ/WRITE"

        #region "로그인 패스워드  READ/WRITE"
        public static bool RDLoginPassword(){
            int iIdx = 0;
            StreamReader sr = new StreamReader(cPath.pathPassword, Encoding.UTF8);
            try{
                while (sr.Peek() != -1){
                    string sIn = sr.ReadLine();
                    string[] sTemp = sIn.Split(':');
                    switch (iIdx){
                        case 0:
                            cDEF.stPassWord.OP = sTemp[1];
                            break;
                        case 1:
                            cDEF.stPassWord.ENG = sTemp[1];
                            break;
                        case 2:
                            cDEF.stPassWord.SUP = sTemp[1];
                            break;
                        case 3:
                            cDEF.stPassWord.SOFT = sTemp[1];
                            break;
                    }
                    iIdx++;
                }
                return true;
            }
            catch (Exception ex){
                MessageBox.Show("LOGIN PASSWORD READ FAIL = " + ex.ToString());
                //cLog.SaveLogException("cUtilLocal->RDLoginPassword", ex);
                return false;
            }
            
        }
        public static bool WRLoginPassword(){
            StreamWriter sw = new StreamWriter(cPath.pathPassword);
            try{
                sw.WriteLine("OP:" + "");
                sw.WriteLine("ENG:" + cDEF.stPassWord.ENG.Trim());
                sw.WriteLine("SUP:" + cDEF.stPassWord.SUP.Trim());
                sw.WriteLine("SOFT:" + cDEF.stPassWord.SOFT.Trim());
                sw.Close();
                return true;
            }
            catch (Exception ex){
                MessageBox.Show("LOGIN PASSWORD WRITE FAIL = " + ex.ToString());
                cLog.SaveLogException("cUtilLocal->WRLoginPassword", ex);
                sw.Close();
                return false;
            }
        }
        #endregion "로그인 패스위드 READ/WRITE"

        #region "타위램프 상태 READ/WRITE"
        public static bool RDTowerLamp(){
            int iIdx = 0;
            if (!File.Exists(cPath.pathTOWERLAMP)){
                //cUtil.PrintMsg("타워램프 파일을 찾을 수 없습니다." + etc.CrLf + "File not found.", false, false, false);
                MessageBox.Show("NOT FIND TOWER LAMP DATA!");
                return false;
            }
            StreamReader sw = new StreamReader(cPath.pathTOWERLAMP, Encoding.UTF8);
            try{
                while (sw.Peek() != -1){
                    string input = sw.ReadLine();
                    string[] mTemp = input.Split(':');
                    switch (iIdx){
                        case 0:
                            cDEF.DataTowerStatus[iIdx] = mTemp[1];
                            cDEF.BackupTowerStatus[iIdx] = cDEF.DataTowerStatus[iIdx];
                            break;
                        case 1:
                            cDEF.DataTowerStatus[iIdx] = mTemp[1];
                            cDEF.BackupTowerStatus[iIdx] = cDEF.DataTowerStatus[iIdx];
                            break;
                        case 2:
                            cDEF.DataTowerStatus[iIdx] = mTemp[1];
                            cDEF.BackupTowerStatus[iIdx] = cDEF.DataTowerStatus[iIdx];
                            break;
                        case 3:
                            cDEF.DataTowerStatus[iIdx] = mTemp[1];
                            cDEF.BackupTowerStatus[iIdx] = cDEF.DataTowerStatus[iIdx];
                            break;
                        case 4:
                            cDEF.DataTowerStatus[iIdx] = mTemp[1];
                            cDEF.BackupTowerStatus[iIdx] = cDEF.DataTowerStatus[iIdx];
                            break;
                        case 5:
                            cDEF.DataTowerStatus[iIdx] = mTemp[1];
                            cDEF.BackupTowerStatus[iIdx] = cDEF.DataTowerStatus[iIdx];
                            break;
                        case 6:
                            cDEF.DataTowerStatus[iIdx] = mTemp[1];
                            cDEF.BackupTowerStatus[iIdx] = cDEF.DataTowerStatus[iIdx];
                            break;
                    }
                    iIdx++;
                }
                return true;
            }
            catch (Exception ex){
                MessageBox.Show("TOWER LAMP DATA READ FAIL = " + ex.ToString());
                //cLog.SaveLogException("cUtilLocal->RDTowerLamp" , ex);
                return false;
            }
        }
        public static bool WRTowerLamp(){
            StreamWriter sw = new StreamWriter(cPath.pathTOWERLAMP);
            try{
                sw.WriteLine("RUN:" + cDEF.DataTowerStatus[0]);
                sw.WriteLine("STOP:" + cDEF.DataTowerStatus[1]);
                sw.WriteLine("INITIALIZING:" + cDEF.DataTowerStatus[2]);
                sw.WriteLine("WAIT RUN:" + cDEF.DataTowerStatus[3]);
                sw.WriteLine("ERROR:" + cDEF.DataTowerStatus[4]);
                sw.WriteLine("MANUAL:" + cDEF.DataTowerStatus[5]);
                sw.WriteLine("LOT-END:" + cDEF.DataTowerStatus[6]);
                sw.Close();
                return true;
            }
            catch (Exception ex){
                MessageBox.Show("LOGIN PASSWORD WRITE FAIL = " + ex.ToString());
                cLog.SaveLogException("cUtilLocal->WRLoginPassword", ex);
                sw.Close();
                return false;
            }
        }
        #endregion "타워램프 상태 READ/WRITE"
        
        #region "모터 정보 READ"
        public static bool RDMT(){
            int iIdx = 0;
            try {
                XDocument xdoc = XDocument.Load(cPath.pathMTName);
                var CODE = from r in xdoc.Descendants("CODE")
                           select new{
                               NUM                      = r.Element("NUM").Value,
                               TITLE                    = r.Element("TITLE").Value,
                               PIC                      = r.Element("PIC").Value,
                               SPD                      = r.Element("SPD").Value,
                               JOG_H                    = r.Element("JOG1SPD").Value,
                               JOG_M                    = r.Element("JOG2SPD").Value,
                               JOG_L                    = r.Element("JOG1SPD").Value,
                               ShortLength              = r.Element("SHORTLENGTH").Value,
                               ShortSpeed               = r.Element("SHORTSPEED").Value,
                               BD                       = r.Element("DB").Value,
                           };
                foreach (var r in CODE){
                    iIdx                            = Convert.ToInt16(r.NUM);
                    cMT.Name[iIdx]                  = r.TITLE;
                    cMT.Image[iIdx]                 = Convert.ToInt16(r.PIC);
                    cMT.mSoftData[iIdx].MaxSpd      = Convert.ToDouble(r.SPD);
                    cMT.mSoftData[iIdx].JogHSpd     = Convert.ToDouble(r.JOG_H);
                    cMT.mSoftData[iIdx].JogMSpd     = Convert.ToDouble(r.JOG_M);
                    cMT.mSoftData[iIdx].JogLSpd     = Convert.ToDouble(r.JOG_L);
                    cMT.mSoftData[iIdx].ShortLength = Convert.ToDouble(r.ShortLength);
                    cMT.mSoftData[iIdx].ShortSpd    = Convert.ToDouble(r.ShortSpeed);
                    if (cMT.Name[iIdx].Length > 2)  cDEF.iMaxMT = iIdx;
                    else                            break;                   
                }
                return true;
            }
            catch (System.Exception ex){
                System.Diagnostics.Trace.WriteLine("cUtilLocal -> RDMT READ FAIL - " + ex.ToString());
                return false;
            }
        }

        #endregion "모터 정보 READ/WRITE"

        #region "IO DATA & IO INFO READ/WRITE"
        public static bool RDInput(){
            string mStr     = string.Empty;
            short i         = 0;
            StreamReader sr = new StreamReader(cPath.pathInputName);
            try{
                while (sr.Peek() != -1){
                    if (cDI.name.Length < i) break;
                    mStr = sr.ReadLine();
                    cDI.name[i] = mStr.Trim();
                    i++;
                }
                sr.Close();
                return true;
            }
            catch (System.Exception ex){
                //System.Diagnostics.Trace.WriteLine("ClsUtilLocal[RD_INPUT] -> " + ex.ToString());
                MessageBox.Show("INPUT LAVEL READ FAIL = " + ex.ToString());
                cLog.SaveLogException("cUtilLocal->RDInput", ex);
                sr.Close();
                return false;
            }
        }
        public static bool RDOutput(){
            string mStr = string.Empty;
            short i = 0;
            StreamReader sr = new StreamReader(cPath.pathOutputName);
            try{
                while (sr.Peek() != -1){
                    if (cDO.name.Length < i) break;
                    mStr = sr.ReadLine();
                    cDO.name[i] = mStr.Trim();
                    i++;
                }
                sr.Close();
                return true;
            }
            catch (System.Exception ex){
                //System.Diagnostics.Trace.WriteLine("ClsUtilLocal[RD_OUTPUT] -> " + ex.ToString());
                MessageBox.Show("OUTPUT LAVEL READ FAIL = " + ex.ToString());
                cLog.SaveLogException("cUtilLocal->RDOutput", ex);
                sr.Close();
                return false;
            }
        }

        public static void RDInfo_IO(){
            if (!File.Exists(cPath.pathInfoInput)){
                for (short i = 0; i < cDEF.cntIn; i++){
                    cDI.ChkIn[i].contactB = false;
                    cDI.ChkIn[i].chkEnd = false;
                    cDI.ChkIn[i].isVirtual = false;
                }
            }
            else{
                string[] iArr = File.ReadAllLines(cPath.pathInfoInput);
                for (short i = 0; i < iArr.Length - 1; i++){
                    string[] subArr = iArr[i].Split(',');
                    cDI.ChkIn[i].contactB       = bool.Parse(subArr[0].Trim());
                    cDI.ChkIn[i].chkEnd         = bool.Parse(subArr[1]);
                    cDI.ChkIn[i].isVirtual      = bool.Parse(subArr[2]);
                }
            } //input

            if (!File.Exists(cPath.pathInfoOutput)){
                for (short i = 0; i < cDEF.cntOut; i++){
                    cDO.ChkOut[i].contactB      = false;
                    cDO.ChkOut[i].chkEnd        = false;
                    cDO.ChkOut[i].isVirtual     = false;
                }
            }
            else{
                string[] oArr = File.ReadAllLines(cPath.pathInfoOutput);
                for (short i = 0; i < oArr.Length; i++){
                    string[] subArr = oArr[i].Split(',');
                    cDO.ChkOut[i].contactB      = bool.Parse(subArr[0].Trim());
                    cDO.ChkOut[i].chkEnd        = bool.Parse(subArr[1]);
                    cDO.ChkOut[i].isVirtual     = bool.Parse(subArr[2]);
                }
            } //output
        }
        #endregion "IO DATA & IO INFO READ/WRITE"

        #region "파라메타 READ/WRITE"
        public static void SaveMTTeachingLimit(){
            string fn = cPath.pathMTTeachingLimit;
            string ss = "";

            for (int i = 0; i < cDEF.cntMT; i++){
                for (int j = 0; j < cDEF.cntPOS - 1; j++)
                {
                    ss += cMT.mDataLimit[i, j].posLimitMinus.ToString() + "," + cMT.mDataLimit[i, j].posLimitPlus.ToString() + ",";
                    if (cMT.mDataLimit[i, j].Enable) ss += "1:";
                    else ss += "0:";
                }
                ss += etc.CrLf;
            }
            WRFile(fn, ss, false);
        }
        static public void LoadMTTeachingLimit(){
            string fn = cPath.pathMTTeachingLimit;
            if (!File.Exists(fn)) return;

            string[] ss = File.ReadAllLines(fn);
            for (int i = 0; i < ss.Length; i++){
                string[] sArr = ss[i].Split(':');
                if (sArr.Length < 2) continue; // 데이터 없으면...
                for (int j = 0; j < cDEF.cntPOS - 1; j++){
                    string[] arrPos = sArr[j].Split(',');
                    cMT.mDataLimit[i, j].posLimitMinus = double.Parse(arrPos[0]);
                    cMT.mDataLimit[i, j].posLimitPlus = double.Parse(arrPos[1]);
                    if (arrPos[2] == "1") cMT.mDataLimit[i, j].Enable = true;
                    else cMT.mDataLimit[i, j].Enable = false;
                }
            }
        }

        static public stMVInfo RDTeaching(int m, int p){
            stMVInfo md = cMT.mData[0, 0];
            string fn = string.Empty;

            if (p < 9) fn = cPath.pathMCPara;
            else fn = mS.var[mS.msCurrJobName];

            md.Pos      = cUtilLocal.RDDouble(fn, "MT_" + m.ToString(), "POS_" + p.ToString(), 0.1);
            md.Spd      = cUtilLocal.RDDouble(fn, "MT_" + m.ToString(), "SPD_" + p.ToString(), 10);
            md.Acc      = cUtilLocal.RDDouble(fn, "MT_" + m.ToString(), "ACC_" + p.ToString(), 1000);
            md.Dec      = cUtilLocal.RDDouble(fn, "MT_" + m.ToString(), "DEC_" + p.ToString(), 1000);
            md.MoveTime = (int)cUtilLocal.RDDouble(fn, "MT_" + m.ToString(), "MOVE TIME_" + p.ToString(), 10000);
            md.rMove    = cUtilLocal.RDDouble(fn, "MT_" + m.ToString(), "RELATIVE_" + p.ToString(), 0);
            return md;
        }
        public static void RD_MTData(){
            for (int m = 0; m < cDEF.cntMT; m++){
                for (int p = 0; p < cDEF.cntPOS; p++){
                    cMT.mData[m, p] = RDTeaching(m, p);
                }
            }
        }
        public static void WR_MTData(int m, int p){
            stMVInfo oldmv  = cMT.mOld;
            stMVInfo mv     = cMT.mData[m, p];
            string sSave    = string.Empty;
            string fn = string.Empty;
            if (p < 9) fn = cPath.pathMCPara;
            else fn = mS.var[mS.msCurrJobName];

            cUtilLocal.WRDouble(fn, "MT_" + m.ToString(), "POS_" + p.ToString(), mv.Pos);
            cUtilLocal.WRDouble(fn, "MT_" + m.ToString(), "SPD_" + p.ToString(), mv.Spd);
            cUtilLocal.WRDouble(fn, "MT_" + m.ToString(), "ACC_" + p.ToString(), mv.Acc);
            cUtilLocal.WRDouble(fn, "MT_" + m.ToString(), "DEC_" + p.ToString(), mv.Dec);
            cUtilLocal.WRDouble(fn, "MT_" + m.ToString(), "MOVE TIME_" + p.ToString(), mv.MoveTime);
            cUtilLocal.WRDouble(fn, "MT_" + m.ToString(), "RELATIVE_" + p.ToString(), mv.rMove);

            mB.var[mB.mbTeachChanged] = true;
            if (!mB.var[mB.mbNotTeachSave]){
                if (oldmv.Pos != mv.Pos){
                    sSave = cMT.Name[m] + " , " + cMT.NamePos[m, p] + " Location , " + cUtil.GetStr(oldmv.Pos, "0.000") + " -> " + cUtil.GetStr(mv.Pos, "0.000");
                    cLog.SaveChangeDataEvent(sSave);
                }
                if (oldmv.Spd != mv.Spd){
                    sSave = cMT.Name[m] + " , " + cMT.NamePos[m, p] + " Speed , " + cUtil.GetStr(oldmv.Spd, "0.000") + " -> " + cUtil.GetStr(mv.Spd, "0.000");
                    cLog.SaveChangeDataEvent(sSave);
                }
                if (oldmv.Acc != mv.Acc){
                    sSave = cMT.Name[m] + " , " + cMT.NamePos[m, p] + " Acc , " + cUtil.GetStr(oldmv.Acc, "0.0") + " -> " + cUtil.GetStr(mv.Acc, "0.0");
                    cLog.SaveChangeDataEvent(sSave);
                }
                if (oldmv.Dec != mv.Dec){
                    sSave = cMT.Name[m] + " , " + cMT.NamePos[m, p] + " Dec , " + cUtil.GetStr(oldmv.Dec, "0.0") + " -> " + cUtil.GetStr(mv.Dec, "0.0");
                    cLog.SaveChangeDataEvent(sSave);
                }
                if (oldmv.MoveTime != mv.MoveTime){
                    sSave = cMT.Name[m] + " , " + cMT.NamePos[m, p] + " MoveTime , " + cUtil.GetStr(oldmv.MoveTime, "0.0") + " -> " + cUtil.GetStr(mv.MoveTime, "0.0");
                    cLog.SaveChangeDataEvent(sSave);
                }
            }
        }

        public static void RD_McPara(){
            if (!File.Exists(cPath.pathMCPara)) return;
            for (int i = 0; i < cDEF.cntMCPara; i++){
                cPara.prMachine[i] = cUtilLocal.RDDouble(cPath.pathMCPara, "PARAMETER", "MCPara_" + i.ToString(), 0.0);
            }
        }
        public static bool WR_McPara(int i, double value){
            if (cPara.prMachine[i] != value){
                if (!mB.var[mB.mbNotTeachSave]){
                    cLog.SaveChangeDataEvent("[" + i.ToString() + "]" + cPara.nameMC[i] + " : " + cPara.prMachine[i] + " -> " + value.ToString());
                    cPara.prMachine[i] = value;
                }
                cUtilLocal.WRDouble(cPath.pathMCPara, "PARAMETER", "MCPara_" + i.ToString(), cPara.prMachine[i]);
                return true;
            }
            return false;
        }

        public static void RD_MdlPara(){
            for (int i = 0; i < cDEF.cntMDLPara; i++){
                cPara.prModel[i] = cUtilLocal.RDDouble(mS.var[mS.msCurrJobName], "PARAMETER", "MDLPara_" + i.ToString(), 0.0);
            }
        }
        public static bool WR_MdlPara(int i, double value){
            if (cPara.prModel[i] != value){
                if (!mB.var[mB.mbNotTeachSave]){
                    cLog.SaveChangeDataEvent("[" + i.ToString() + "]" + cPara.nameMDL[i] + " : " + cPara.prModel[i] + " -> " + value.ToString());
                    cPara.prModel[i] = value;
                }
                cUtilLocal.WRDouble(mS.var[mS.msCurrJobName], "PARAMETER", "MDLPara_" + i.ToString(), cPara.prModel[i]);
                return true;
            }
            return false;
        }

        #endregion "파라메타 READ/WRITE"
         
         */

        #region "로그인 패스워드  READ/WRITE"
        public static bool RDLoginPassword()
        {
            int iIdx = 0;
            string a = cPath.FILE_PASSWORD;
            StreamReader sr = new StreamReader(cPath.FILE_PASSWORD, Encoding.UTF8);
            try
            {
                while (sr.Peek() != -1)
                {
                    string sIn = sr.ReadLine();
                    string[] sTemp = sIn.Split(':');
                    switch (iIdx)
                    {
                        case 0:
                            cDEF.stPassWord.OP = sTemp[1];
                            break;
                        case 1:
                            cDEF.stPassWord.ENG = sTemp[1];
                            break;
                        case 2:
                            cDEF.stPassWord.SUP = sTemp[1];
                            break;
                            //case 3:
                            //    cDEF.stPassWord.SUPER = sTemp[1];
                            //    break;
                    }
                    iIdx++;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOGIN PASSWORD READ FAIL = " + ex.ToString());
                return false;
            }

        }
        public static bool WRLoginPassword()
        {
            StreamWriter sw = new StreamWriter(cPath.FILE_PASSWORD);
            try
            {
                sw.WriteLine("OP:" + "");
                sw.WriteLine("ENG:" + cDEF.stPassWord.ENG.Trim());
                sw.WriteLine("SUP:" + cDEF.stPassWord.SUP.Trim());
                //sw.WriteLine("SUPER:" + cDEF.stPassWord.SUPER.Trim());
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOGIN PASSWORD WRITE FAIL = " + ex.ToString());
                sw.Close();
                return false;
            }
        }
        #endregion "로그인 패스위드 READ/WRITE"

        // 이 프로그램이 실행 중이면 true 아니면 false
        static public bool IsApplicationAlreadyRunning()
        {
            bool ch1 = false;
            bool ch2 = false;
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.StartsWith("Lens_Assembly_CH1"))
                    ch1 = true;
                else if (process.ProcessName.StartsWith("Lens_Assembly_CH2"))
                    ch2 = true;
            }
            return ch1 & ch2;
        }

        public class AlarmDefin
        {

            public int Code;
            public string Grade;
            public string Text;
            public int ImageNum;
        }
    }
}

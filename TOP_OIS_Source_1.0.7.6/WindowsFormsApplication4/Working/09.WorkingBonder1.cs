using System.Collections.Generic;
using XModule.Standard;
using XModule.Datas;

namespace XModule.Working
{
    public class WorkingBonder1JetData : BaseData
    {
        private string section;
        public List<JettingData> JetData;   // Point

        private int JetDataCount;
        public WorkingBonder1JetData(string name) : base(cDefString.Bonder1Point)
        {

            JetData = new List<JettingData>();
            Clear();

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
            JetDataCount = 0;
        }

        public void JettingDataInit()
        {
            foreach (JettingData jd in JetData)
            {
                jd.Finish = false;
            }
        }
        public override void DataProc(fpIni.IniType type)
        {
            if (type == fpIni.IniType.Read)
            {
                section = cDefString.Bonder1Point;
                bool enable = false;
                double radius = 0.0;
                double angle = 0.0;
                int delay = 0;
                int zoffset = 0;
                int zup = 0;
                int pluseNum = 0;
                int dptime = 0;

                ini.DataProc(type, section, "JetDataCount", ref JetDataCount);
                JetData.Clear();
                for (int i = 0; i < JetDataCount; i++)
                {
                    ini.DataProc(type, section, $"{i}_Enable", ref enable);
                    ini.DataProc(type, section, $"{i}_Radius", ref radius);
                    ini.DataProc(type, section, $"{i}_Angle", ref angle);
                    ini.DataProc(type, section, $"{i}_Delay", ref delay);
                    ini.DataProc(type, section, $"{i}_ZOffset", ref zoffset);
                    ini.DataProc(type, section, $"{i}_ZUp", ref zup);
                    ini.DataProc(type, section, $"{i}_PluseNum", ref pluseNum);
                    ini.DataProc(type, section, $"{i}_DPTime", ref dptime);

                    JettingData jd = new JettingData();
                    jd.Enable = enable;
                    jd.Radius = radius;
                    jd.Angle = angle;
                    jd.Delay = delay;
                    jd.ZOffset = zoffset;
                    jd.ZUp = zup;
                    jd.PluseNum = pluseNum;
                    jd.DpTime = dptime;

                    JetData.Add(jd);
                }
            }
            else if (type == fpIni.IniType.Write)
            {
                section = cDefString.Bonder1Point;
                int jetcount = JetData.Count;
                ini.DataProc(type, section, "JetDataCount", ref jetcount);
                int index = 0;
                foreach (JettingData jd in JetData)
                {
                    ini.DataProc(type, section, $"{index}_Enable", ref jd.Enable);
                    ini.DataProc(type, section, $"{index}_Radius", ref jd.Radius);
                    ini.DataProc(type, section, $"{index}_Angle", ref jd.Angle);
                    ini.DataProc(type, section, $"{index}_Delay", ref jd.Delay);
                    ini.DataProc(type, section, $"{index}_ZOffset", ref jd.ZOffset);
                    ini.DataProc(type, section, $"{index}_ZUp", ref jd.ZUp);
                    ini.DataProc(type, section, $"{index}_PluseNum", ref jd.PluseNum);
                    ini.DataProc(type, section, $"{index}_DPTime", ref jd.DpTime);

                    index++;
                }
            }

        }
    }

    public class WorkingBonder1JetPTLine : BaseData
    {
        private string section;
        public List<JettingPatternLineData> JetPatternLineData;  // PatternLine

        private int JetPatternLineDataCount;
        public int JetPatternLineCount;
        public WorkingBonder1JetPTLine(string name) : base(cDefString.Bonder1Pattern)
        {
            JetPatternLineData = new List<JettingPatternLineData>();
            Clear();
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
            JetPatternLineDataCount = 0;
            JetPatternLineCount = 0;
        }
        public void JettingPatternInit()
        {
            foreach (JettingPatternLineData jd in JetPatternLineData)
            {
                jd.Finish = false;
            }
        }
        public override void DataProc(fpIni.IniType type)
        {
            if (type == fpIni.IniType.Read)
            {
                //PatternLine
                section = cDefString.Bonder1Pattern;
                int xpos = 0;
                int ypos = 0;
                int zpos = 0;
                int linespeed = 0;
                int zspeed = 0;
                bool shot = false;
                ini.DataProc(type, section, "PL_JetPatternLineDataCount", ref JetPatternLineCount);

                JetPatternLineData.Clear();

                for (int i = 0; i < JetPatternLineCount; i++)
                {
                    JettingPatternLineData jpld = new JettingPatternLineData();
                    jpld.Finish = false;

                    ini.DataProc(type, section, $"PL_{i}_JetPatternLineDataCount", ref JetPatternLineDataCount);
                    for (int j = 0; j < JetPatternLineDataCount; j++)
                    {
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_XPos", ref xpos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_YPos", ref ypos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_ZPos", ref zpos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_LineSpeed", ref linespeed);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_ZSpeed", ref zspeed);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_Shot", ref shot);

                        JettingLineData jld = new JettingLineData();
                        jld.XPos = xpos;
                        jld.YPos = ypos;
                        jld.ZPos = zpos;
                        jld.LineSpeed = linespeed;
                        jld.ZSpeed = zspeed;
                        jld.Shot = shot;

                        jpld.JetLineData.Add(jld);

                    }
                    JetPatternLineData.Add(jpld);
                }
            }
            else if (type == fpIni.IniType.Write)
            {
                //PatternLine
                section = cDefString.Bonder1Pattern;
                JetPatternLineCount = JetPatternLineData.Count;

                ini.DataProc(type, section, "PL_JetPatternLineDataCount", ref JetPatternLineCount);

                for (int i = 0; i < JetPatternLineCount; i++)
                {
                    JetPatternLineDataCount = JetPatternLineData[i].JetLineData.Count;
                    ini.DataProc(type, section, $"PL_{i}_JetPatternLineDataCount", ref JetPatternLineDataCount);
                    int index = 0;
                    foreach (JettingLineData jld in JetPatternLineData[i].JetLineData)
                    {
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_XPos", ref jld.XPos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_YPos", ref jld.YPos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_ZPos", ref jld.ZPos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_LineSpeed", ref jld.LineSpeed);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_ZSpeed", ref jld.ZSpeed);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_Shot", ref jld.Shot);
                        index++;
                    }
                }
            }
        }
    }

    public class WorkingBonder1JetPTArc : BaseData
    {
        private string section;
        public List<JettingPatternArcData> JetPatternArcData;  // PatternLine

        private int JetPatternArcDataCount;
        public int JetPatternArcCount;
        public WorkingBonder1JetPTArc(string name) : base(cDefString.Bonder1Arc)
        {
            JetPatternArcData = new List<JettingPatternArcData>();
            Clear();
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
            JetPatternArcDataCount = 0;
            JetPatternArcCount = 0;
        }
        public void JettingPatternInit()
        {
            foreach (JettingPatternArcData jd in JetPatternArcData)
            {
                jd.Finish = false;
            }
        }
        public override void DataProc(fpIni.IniType type)
        {
            if (type == fpIni.IniType.Read)
            {
                //PatternLine
                section = cDefString.Bonder1Arc;  
                int sxpos = 0;
                int sypos = 0;
                int expos = 0;
                int eypos = 0;
                int radius = 0;
                int zpos = 0;
                int linespeed = 0;
                int zspeed = 0;
                double Angle = 0;
                bool shot = false;
                int jettype = 0;
                ini.DataProc(type, section, "PL_JetPatternARcDataCount", ref JetPatternArcCount);

                JetPatternArcData.Clear();

                for (int i = 0; i < JetPatternArcCount; i++)
                {
                    JettingPatternArcData jpld = new JettingPatternArcData();
                    jpld.Finish = false;

                    ini.DataProc(type, section, $"PL_{i}_JetPatternArcDataCount", ref JetPatternArcDataCount);
                    for (int j = 0; j < JetPatternArcDataCount; j++)
                    {
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_XPos", ref sxpos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_YPos", ref sypos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_EXPos", ref expos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_EYPos", ref eypos);
                        //ini.DataProc(type, section, $"PL_Ptn{i}_{j}_Radius", ref radius);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_Angle", ref Angle);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_ZPos", ref zpos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_LineSpeed", ref linespeed);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_ZSpeed", ref zspeed);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_Shot", ref shot);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{j}_Type", ref jettype);

                        JettingArcData jld = new JettingArcData();
                        jld.XPos = sxpos;
                        jld.YPos = sypos;
                        jld.EXPos = expos;
                        jld.EYPos = eypos;
                        jld.Radius = radius;
                        jld.Angle = Angle;
                        jld.ZPos = zpos;
                        jld.LineSpeed = linespeed;
                        jld.ZSpeed = zspeed;
                        jld.Shot = shot;
                        jld.JetType = (eJetType) jettype;
                        jpld.JetArcData.Add(jld);

                    }
                    JetPatternArcData.Add(jpld);
                }
            }
            else if (type == fpIni.IniType.Write)
            {
                //PatternLine
                section = cDefString.Bonder1Arc;
                JetPatternArcCount = JetPatternArcData.Count;

                ini.DataProc(type, section, "PL_JetPatternARcDataCount", ref JetPatternArcCount);

                for (int i = 0; i < JetPatternArcCount; i++)
                {
                    JetPatternArcDataCount = JetPatternArcData[i].JetArcData.Count;
                    ini.DataProc(type, section, $"PL_{i}_JetPatternArcDataCount", ref JetPatternArcDataCount);
                    int index = 0;
                    foreach (JettingArcData jld in JetPatternArcData[i].JetArcData)
                    {
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_XPos", ref jld.XPos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_YPos", ref jld.YPos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_EXPos", ref jld.EXPos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_EYPos", ref jld.EYPos);
                        //ini.DataProc(type, section, $"PL_Ptn{i}_{index}_Radius", ref jld.Radius);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_Angle", ref jld.Angle);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_ZPos", ref jld.ZPos);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_LineSpeed", ref jld.LineSpeed);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_ZSpeed", ref jld.ZSpeed);
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_Shot", ref jld.Shot);
                        int jet = (int)jld.JetType;
                        ini.DataProc(type, section, $"PL_Ptn{i}_{index}_Type",ref jet);
                        index++;
                    }
                }
            }
        }
    }
    public class WorkingBonder1 : BaseData
    {
        private  string section;

        #region Position
        // Position
        //  Head
        public List<TfpConti> LstConti;         // Jet Pos

        //Count
        public int JettingCount;

        public int MovingDelayX;
        public int MovingDelayY;
        public int MovingDelayZ;

        public int JettingTime;

        public int IdleTime;

        public int Bonder1GrabDelay;
        #endregion

        #region Jettting Set
        public double RiseTime;
        public double HoldTime;
        public double FallTime;
        public double JetDelay;
        public int Pct;
        public int PulseNum;
        public int WorkMode;
        public int Voltage;
        #endregion

        public int GapPosX = 0;
        public int GapPosY = 0;
        public int GapOffsetLimitZ = 0;
        public int GapMeasureDelay = 0;
        public int GapOffsetZ = 0;


        public WorkingBonder1(string name) : base(cDefString.Bonder1)
        {
            LstConti = new List<TfpConti>();
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
            JettingCount = 0;

            MovingDelayX = 0;
            MovingDelayY = 0;
            MovingDelayZ = 0;

            JettingTime = 0;
            IdleTime = 0;

            Bonder1GrabDelay = 0;
        }

        public override void DataProc(fpIni.IniType type)
        {
            section = cDefString.Bonder1;
            ini.DataProc(type, section, "JettingCount", ref JettingCount);
            ini.DataProc(type, section, "JettingTime", ref JettingTime);
            ini.DataProc(type, section, "IdleTime", ref IdleTime);

            ini.DataProc(type, section, "RiseTime", ref RiseTime);
            ini.DataProc(type, section, "HoldTime", ref HoldTime);
            ini.DataProc(type, section, "FallTime", ref FallTime);
            ini.DataProc(type, section, "JetDelay", ref JetDelay);
            ini.DataProc(type, section, "Pct", ref Pct);
            ini.DataProc(type, section, "PulseNum", ref PulseNum);
            ini.DataProc(type, section, "WorkMode", ref WorkMode);
            ini.DataProc(type, section, "Voltage", ref Voltage);

            ini.DataProc(type, section, "MovingDelayX", ref MovingDelayX);
            ini.DataProc(type, section, "MovingDelayY", ref MovingDelayY);
            ini.DataProc(type, section, "MovingDelayZ", ref MovingDelayZ);

            ini.DataProc(type, section, "Bonder1GrabDelay", ref Bonder1GrabDelay);

            ini.DataProc(type, section, "GapPosX", ref GapPosX);
            ini.DataProc(type, section, "GapPosY", ref GapPosY);
            ini.DataProc(type, section, "GapOffsetLimitZ", ref GapOffsetLimitZ);
            ini.DataProc(type, section, "GapMeasureDelay", ref GapMeasureDelay);
            ini.DataProc(type, section, "GapOffsetZ", ref GapOffsetZ);

        }
    }

    public class WorkingTeachingBonder1 : BaseData
    {
        private string section;

        #region Position
        // Position
        //  Head
        public int ReadyPositionX = 0;              //Head Ready Position   
        public int ReadyPositionY = 0;              //Head Ready Position   
        public int ReadyPositionZ = 0;              //Head Ready Position   

        //Cam 기준
        public int CamPositionX;           // Inspection  
        public int CamPositionY;           // Inspection
        public int CamPositionZ;           // Inspection

        // DistanceOffset
        public int CameraDistanceOffsetX;        // 
        public int CameraDistanceOffsetY;        // 4th Dipping

        public int Jetting1OffsetX;           // 1nd Offset
        public int Jetting1OffsetY;           // 1nd Offset
        public int Jetting2OffsetX;           // 2nd Offset
        public int Jetting2OffsetY;           // 2nd Offset
        public int Jetting3OffsetX;           // 3rd Offset
        public int Jetting3OffsetY;           // 3rd Offset
        public int Jetting4OffsetX;           // 4th Offset
        public int Jetting4OffsetY;           // 4th Offset

        public int JettingPositionZ;
        public int ReadyJettingPositionZ;

        public int AvoidPositionX;
        public int AvoidPositionY;

        public int IdlePosX;                   // Idle && Purge
        public int IdlePosY;
        public int IdlePosZ;

        public int SamplePosX;                   // Sample
        public int SamplePosY;
        public int SamplePosZ;
        public int SampleVisionPosZ;
        public int SampleGapPosX;
        public int SampleGapPosY;
        

        // 추후 구현.
        public int TouchPosX;
        public int TouchPosY;
        public int TouchPosZ;
        public int TouchLimitZ;
        public int TouchStep;
        public int TouchOffsetZ;

        public int CleanPositionX = 0;              //Head Clean Position   //Position 생성, Next 클리어 작업
        public int CleanPositionY = 0;              //Head Clean Position   
        public int CleanPositionZ = 0;              //Head Clean Position

        public int TipCleanStartPosX = 0;             
        public int TipCleanStartPosY = 0;             
        public int TipCleanPosZ = 0;

        public int TipCleanCntY = 0;
        public int TipCleanPitchY = 0;


        // AutoCal
        public int AutoCalSpec = 0;
        public int AutoCalCount = 0;

        public int GapDistanceX = 0;
        public int GapDistanceY = 0;

        public int GapTouchX = 0;
        public int GapTouchY = 0;
        public int GapAdjustX = 0;
        public int GapAdjustY = 0;
        public int GapMeasureZ = 0;

        #endregion

        public WorkingTeachingBonder1(string name) : base(cDefString.Bonder1)
        {
            Clear();
            //Load();       
        }

        public void Clear()
        {
            ReadyPositionX = 0;
            ReadyPositionY = 0;
            ReadyPositionZ = 0;

            CamPositionX = 0;
            CamPositionY = 0;
            CamPositionZ = 0;

            CameraDistanceOffsetX = 0;
            CameraDistanceOffsetY = 0;

            Jetting1OffsetX = 0;
            Jetting1OffsetY = 0;
            Jetting2OffsetX = 0;
            Jetting2OffsetY = 0;
            Jetting3OffsetX = 0;
            Jetting3OffsetY = 0;
            Jetting4OffsetX = 0;
            Jetting4OffsetY = 0;

            JettingPositionZ = 0;
         
            AvoidPositionX = 0;
            AvoidPositionY = 0;

            IdlePosX = 0;
            IdlePosY = 0;
            IdlePosZ = 0;

            SamplePosX = 0;
            SamplePosY = 0;
            SamplePosZ = 0;
            SampleVisionPosZ = 0;

            TouchPosX = 0;
            TouchPosY = 0;
            TouchPosZ = 0;
            TouchLimitZ = 0;
            TouchStep = 0;
            TouchOffsetZ = 0;

            CleanPositionX = 0;  //Position 초기화, Next 
            CleanPositionY = 0;
            CleanPositionZ = 0;

            TipCleanStartPosX = 0;
            TipCleanStartPosY = 0;
            TipCleanPosZ = 0;

            TipCleanCntY = 0;
            TipCleanPitchY = 0;

            AutoCalSpec = 0;
            AutoCalCount = 0;
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

            section = cDefString.Bonder1;

            ini.DataProc(type, section, "ReadyPositionX", ref ReadyPositionX);
            ini.DataProc(type, section, "ReadyPositionY", ref ReadyPositionY);
            ini.DataProc(type, section, "ReadyPositionZ", ref ReadyPositionZ);

            ini.DataProc(type, section, "CamPositionX", ref CamPositionX);
            ini.DataProc(type, section, "CamPositionY", ref CamPositionY);
            ini.DataProc(type, section, "CamPositionZ", ref CamPositionZ);

            ini.DataProc(type, section, "CameraDistanceOffsetX", ref CameraDistanceOffsetX);
            ini.DataProc(type, section, "CameraDistanceOffsetY", ref CameraDistanceOffsetY);

            ini.DataProc(type, section, "Jetting1OffsetX", ref Jetting1OffsetX);
            ini.DataProc(type, section, "Jetting1OffsetY", ref Jetting1OffsetY);
            ini.DataProc(type, section, "Jetting2OffsetX", ref Jetting2OffsetX);
            ini.DataProc(type, section, "Jetting2OffsetY", ref Jetting2OffsetY);
            ini.DataProc(type, section, "Jetting3OffsetX", ref Jetting3OffsetX);
            ini.DataProc(type, section, "Jetting3OffsetY", ref Jetting3OffsetY);
            ini.DataProc(type, section, "Jetting4OffsetX", ref Jetting4OffsetX);
            ini.DataProc(type, section, "Jetting4OffsetY", ref Jetting4OffsetY);

            ini.DataProc(type, section, "JettingPositionZ", ref JettingPositionZ);

            ini.DataProc(type, section, "AvoidPositionX", ref AvoidPositionX);
            ini.DataProc(type, section, "AvoidPositionY", ref AvoidPositionY);

            ini.DataProc(type, section, "IdlePosX", ref IdlePosX);
            ini.DataProc(type, section, "IdlePosY", ref IdlePosY);
            ini.DataProc(type, section, "IdlePosZ", ref IdlePosZ);

            ini.DataProc(type, section, "SamplePosX", ref SamplePosX);
            ini.DataProc(type, section, "SamplePosY", ref SamplePosY);
            ini.DataProc(type, section, "SamplePosZ", ref SamplePosZ);
            ini.DataProc(type, section, "SampleVisionPosZ", ref SampleVisionPosZ);
            ini.DataProc(type, section, "SampleGapPosX", ref SampleGapPosX);
            ini.DataProc(type, section, "SampleGapPosY", ref SampleGapPosY);

            ini.DataProc(type, section, "TouchPosX", ref TouchPosX);
            ini.DataProc(type, section, "TouchPosY", ref TouchPosY);
            ini.DataProc(type, section, "TouchPosZ", ref TouchPosZ);
            ini.DataProc(type, section, "TouchLimitZ", ref TouchLimitZ);
            ini.DataProc(type, section, "TouchStep", ref TouchStep);
            ini.DataProc(type, section, "TouchOffsetZ", ref TouchOffsetZ);

            ini.DataProc(type, section, "CleanPositionX", ref CleanPositionX);   //Clean Position, Next grid
            ini.DataProc(type, section, "CleanPositionY", ref CleanPositionY);
            ini.DataProc(type, section, "CleanPositionZ", ref CleanPositionZ);

      
            ini.DataProc(type, section, "TipCleanStartPosX", ref TipCleanStartPosX);
            ini.DataProc(type, section, "TipCleanStartPosY", ref TipCleanStartPosY);
            ini.DataProc(type, section, "TipCleanPosZ", ref TipCleanPosZ);
            ini.DataProc(type, section, "TipCleanCntY", ref TipCleanCntY);
            ini.DataProc(type, section, "TipCleanPitchY", ref TipCleanPitchY);

            ini.DataProc(type, section, "AutoCalSpec", ref AutoCalSpec);
            ini.DataProc(type, section, "AutoCalCount", ref AutoCalCount);


            ini.DataProc(type, section, "GapDistanceX", ref GapDistanceX);
            ini.DataProc(type, section, "GapDistanceY", ref GapDistanceY);
            ini.DataProc(type, section, "GapTouchX", ref GapTouchX);
            ini.DataProc(type, section, "GapTouchY", ref GapTouchY);
            ini.DataProc(type, section, "GapAdjustX", ref GapAdjustX);
            ini.DataProc(type, section, "GapAdjustY", ref GapAdjustY);
            ini.DataProc(type, section, "GapMeasureZ", ref GapMeasureZ);


        }
    }
}


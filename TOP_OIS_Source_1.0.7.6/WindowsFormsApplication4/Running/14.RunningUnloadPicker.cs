using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;
using XModule.Datas;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using XModule.Working;
using XModule.Unit;
using System.Linq;

namespace XModule.Running
{
    #region class Chart Count
    public class clsChartOkCount
    {
        private int[] FChartOkCount;
        fpObject FOwner = null;
        public clsChartOkCount(fpObject Owner)
        {
            FChartOkCount = new int[12];
            this.FOwner = Owner;
        }
        public int this[int Index]
        {
            get { return FChartOkCount[Index]; }
            set
            {
                if (FChartOkCount[Index] != value)
                {
                    FChartOkCount[Index] = value;
                    this.FOwner.Change();
                }
            }
        }
    }
    public class clsChartLensHeightFailCount
    {
        private int[] FChartLensHeightFailCount;
        fpObject FOwner = null;
        public clsChartLensHeightFailCount(fpObject Owner)
        {
            FChartLensHeightFailCount = new int[12];
            this.FOwner = Owner;
        }
        public int this[int Index]
        {
            get { return FChartLensHeightFailCount[Index]; }
            set
            {
                if (FChartLensHeightFailCount[Index] != value)
                {
                    FChartLensHeightFailCount[Index] = value;
                    this.FOwner.Change();
                }
            }
        }
    }
    public class clsChartVisionInspectFailCount
    {
        private int[] FChartVisionInspectFailCount;
        fpObject FOwner = null;
        public clsChartVisionInspectFailCount(fpObject Owner)
        {
            FChartVisionInspectFailCount = new int[12];
            this.FOwner = Owner;
        }
        public int this[int Index]
        {
            get { return FChartVisionInspectFailCount[Index]; }
            set
            {
                if (FChartVisionInspectFailCount[Index] != value)
                {
                    FChartVisionInspectFailCount[Index] = value;
                    this.FOwner.Change();
                }
            }
        }
    }
    public class clsChartAct1FailCount
    {
        private int[] FChartAct1FailCount;
        fpObject FOwner = null;
        public clsChartAct1FailCount(fpObject Owner)
        {
            FChartAct1FailCount = new int[12];
            this.FOwner = Owner;
        }
        public int this[int Index]
        {
            get { return FChartAct1FailCount[Index]; }
            set
            {
                if (FChartAct1FailCount[Index] != value)
                {
                    FChartAct1FailCount[Index] = value;
                    this.FOwner.Change();
                }
            }
        }
    }
    public class clsChartSidAngleFailCount
    {
        private int[] FChartSidAngleFailCount;
        fpObject FOwner = null;
        public clsChartSidAngleFailCount(fpObject Owner)
        {
            FChartSidAngleFailCount = new int[12];
            this.FOwner = Owner;
        }
        public int this[int Index]
        {
            get { return FChartSidAngleFailCount[Index]; }
            set
            {
                if (FChartSidAngleFailCount[Index] != value)
                {
                    FChartSidAngleFailCount[Index] = value;
                    this.FOwner.Change();
                }
            }
        }
    }
    public class clsChartAct2FailCount
    {
        private int[] FChartAct2FailCount;
        fpObject FOwner = null;
        public clsChartAct2FailCount(fpObject Owner)
        {
            FChartAct2FailCount = new int[12];
            this.FOwner = Owner;
        }
        public int this[int Index]
        {
            get { return FChartAct2FailCount[Index]; }
            set
            {
                if (FChartAct2FailCount[Index] != value)
                {
                    FChartAct2FailCount[Index] = value;
                    this.FOwner.Change();
                }
            }
        }
    }
    public class clsChartAct3FailCount
    {
        private int[] FChartAct3FailCount;
        fpObject FOwner = null;
        public clsChartAct3FailCount(fpObject Owner)
        {
            FChartAct3FailCount = new int[12];
            this.FOwner = Owner;
        }
        public int this[int Index]
        {
            get { return FChartAct3FailCount[Index]; }
            set
            {
                if (FChartAct3FailCount[Index] != value)
                {
                    FChartAct3FailCount[Index] = value;
                    this.FOwner.Change();
                }
            }
        }
    }
    public class clsChartSideHeightFailCount //Adding Error
    {
        private int[] FChartSideHeightFailCount;
        fpObject FOwner = null;
        public clsChartSideHeightFailCount(fpObject Owner)
        {
            FChartSideHeightFailCount = new int[12];
            this.FOwner = Owner;
        }
        public int this[int Index]
        {
            get { return FChartSideHeightFailCount[Index]; }
            set
            {
                if (FChartSideHeightFailCount[Index] != value)
                {
                    FChartSideHeightFailCount[Index] = value;
                    this.FOwner.Change();
                }
            }
        }
    }
    #endregion
    public class RunUnloadPickerInformation : fpObject
    {
        
        #region 변수
        private bool FResult = false;

        // Chart Count
        #region Chart Count
        public clsChartOkCount ChartOkCount;
        public clsChartLensHeightFailCount ChartLensHeightFailCount;
        public clsChartVisionInspectFailCount ChartVisionInspectFailCount;
        public clsChartAct1FailCount ChartAct1FailCount;
        public clsChartSidAngleFailCount ChartSidAngleFailCount;
        public clsChartAct2FailCount ChartAct2FailCount;
        public clsChartAct3FailCount ChartAct3FailCount;
        public clsChartSideHeightFailCount ChartSideHeightFailCount; //Adding Error

        #endregion

        // Daily Count
        private int FDailyOKCount;
        private int FDailyNGCount;
        #endregion

        private Tray_Data FUnloader_tray;
        private Tray_Data FNG_tray;
        //private TrayStatus FHeadStatus;
        public Lens_Data HeadLensData;

        public List<double> LensHeightData;
        public List<double> SideAngleData;

        public bool HeadOverLoad = false;
        #region Property
        public bool Result
        {
            get { return FResult; }
            set
            {
                if (FResult != value)
                {
                    FResult = value;
                    Change();
                }
            }
        }
        
        public int ChartTotalCount
        {
            get
            {
                int totalCnt = 0;
                for(int i = 0; i < 12; i++)
                {
                    totalCnt += ChartOkCount[i] + ChartLensHeightFailCount[i] + ChartVisionInspectFailCount[i] + ChartAct1FailCount[i] + ChartSidAngleFailCount[i] + ChartAct2FailCount[i] + ChartAct3FailCount[i] + ChartSideHeightFailCount[i]; //Adding Error
                }
                return totalCnt;
            }
        }

        public int DailyOKCount
        {
            get { return FDailyOKCount; }
            set
            {
                if (FDailyOKCount != value)
                {
                    FDailyOKCount = value;
                    Change();
                }
            }
        }
        public int DailyNGCount
        {
            get { return FDailyNGCount; }
            set
            {
                if (FDailyNGCount != value)
                {
                    FDailyNGCount = value;
                    Change();
                }
            }
        }
        public int DailyTotalCount
        {
            get { return FDailyOKCount + FDailyNGCount; }
        }
        public Tray_Data Unloader_Tray
        {
            get { return FUnloader_tray; }
        }
        public Tray_Data NG_tray
        {
            get { return FNG_tray; }
        }
        #endregion

        public RunUnloadPickerInformation() : base()
        {
            FUnloader_tray = new Tray_Data();
            FNG_tray = new Tray_Data();
            HeadLensData = new Lens_Data();

            ChartOkCount = new clsChartOkCount(this);
            ChartLensHeightFailCount = new clsChartLensHeightFailCount(this);
            ChartVisionInspectFailCount = new clsChartVisionInspectFailCount(this);
            ChartAct1FailCount = new clsChartAct1FailCount(this);
            ChartSidAngleFailCount = new clsChartSidAngleFailCount(this);
            ChartAct2FailCount = new clsChartAct2FailCount(this);
            ChartAct3FailCount = new clsChartAct3FailCount(this);
            ChartSideHeightFailCount = new clsChartSideHeightFailCount(this); //Adding Error

            LensHeightData = new List<double>();
            SideAngleData = new List<double>();

            Clear(true);
            RecoveryOpen();
            SideAngleCountOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunUnloadPicker.dat";
        string Recovery_SideAngleCountPath = Application.StartupPath + "\\Recovery\\SideAngleCount.dat";
        public bool RecoveryOpen()
        {
            Lock();
            Clear(true);

            FileStream File = new FileStream(Recovery_Path, FileMode.OpenOrCreate, FileAccess.Read);

            if (!System.IO.File.Exists(Recovery_Path))
            {
                Unlock();
                return false;
            }

            StreamReader st = new StreamReader(File, Encoding.Default, true);

            st.BaseStream.Seek(0, SeekOrigin.Begin);
            while (st.Peek() > -1)
            {
                String FTemp = st.ReadLine();

                string[] sArr = FTemp.Split(',');
                int index = 0;
                string strIndex = string.Empty;
                if(sArr[0].Contains("ChartOkCount"))
                {
                    strIndex = sArr[0].Replace("ChartOkCount", "");
                    index = Convert.ToInt32(strIndex);
                    ChartOkCount[index] = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("ChartLensHeightFailCount"))
                {
                    strIndex = sArr[0].Replace("ChartLensHeightFailCount", "");
                    index = Convert.ToInt32(strIndex);
                    ChartLensHeightFailCount[index] = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("ChartVisionInspectFailCount"))
                {
                    strIndex = sArr[0].Replace("ChartVisionInspectFailCount", "");
                    index = Convert.ToInt32(strIndex);
                    ChartVisionInspectFailCount[index] = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("ChartAct1FailCount"))
                {
                    strIndex = sArr[0].Replace("ChartAct1FailCount", "");
                    index = Convert.ToInt32(strIndex);
                    ChartAct1FailCount[index] = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("ChartSidAngleFailCount"))
                {
                    strIndex = sArr[0].Replace("ChartSidAngleFailCount", "");
                    index = Convert.ToInt32(strIndex);
                    ChartSidAngleFailCount[index] = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("ChartAct2FailCount"))
                {
                    strIndex = sArr[0].Replace("ChartAct2FailCount", "");
                    index = Convert.ToInt32(strIndex);
                    ChartAct2FailCount[index] = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("ChartAct3FailCount"))
                {
                    strIndex = sArr[0].Replace("ChartAct3FailCount", "");
                    index = Convert.ToInt32(strIndex);
                    ChartAct3FailCount[index] = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("ChartSideHeightFailCount")) //Adding Error
                {
                    strIndex = sArr[0].Replace("ChartSideHeightFailCount", "");
                    index = Convert.ToInt32(strIndex);
                    ChartSideHeightFailCount[index] = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("FDailyOKCount"))
                {
                    FDailyOKCount = Convert.ToInt32(sArr[1]);
                }
                else if (sArr[0].Contains("FDailyNGCount"))
                {
                    FDailyNGCount = Convert.ToInt32(sArr[1]);
                }
            }

            st.Close();
            File.Close();

            Unlock();
            return true;
        }

        protected bool RecoverySave()
        {
            FileStream File;
            StreamWriter FileWrite;

            File = new FileStream(Recovery_Path, FileMode.Create);
            FileWrite = new StreamWriter(File);

            for(int i = 0; i < 12; i++)
            {
                FileWrite.WriteLine($"ChartOkCount{i},{ChartOkCount[i]}");
                FileWrite.WriteLine($"ChartLensHeightFailCount{i},{ChartLensHeightFailCount[i]}");
                FileWrite.WriteLine($"ChartVisionInspectFailCount{i},{ChartVisionInspectFailCount[i]}");
                FileWrite.WriteLine($"ChartAct1FailCount{i},{ChartAct1FailCount[i]}");
                FileWrite.WriteLine($"ChartSidAngleFailCount{i},{ChartSidAngleFailCount[i]}");
                FileWrite.WriteLine($"ChartAct2FailCount{i},{ChartAct2FailCount[i]}");
                FileWrite.WriteLine($"ChartAct3FailCount{i},{ChartAct3FailCount[i]}");
                FileWrite.WriteLine($"ChartSideHeightFailCount{i},{ChartSideHeightFailCount[i]}"); //Adding Error
            }

            FileWrite.WriteLine($"FDailyOKCount,{FDailyOKCount}");
            FileWrite.WriteLine($"FDailyNGCount,{FDailyNGCount}");

            FileWrite.Close();
            File.Close();

            return true;
        }
        protected override void WndProc(ref Message wMessage)
        {
            switch (wMessage.Msg)
            {
                case (int)TMessages.FM_RECOVERY_SAVE:
                    wMessage.Result = (IntPtr)1;
                    RecoverySave();
                    break;
                default:
                    base.WndProc(ref wMessage);
                    break;
            }
        }
        public override void Change()
        {
            if (!Convert.ToBoolean(LockCount))
            {
                if (!Convert.ToBoolean(LockCount)) //Recovery Save
                    XModuleMain.PostMessage((int)Handle, (int)TMessages.FM_RECOVERY_SAVE, 0, 0);
            }
        }

        public void Clear(bool Ignore = false)
        {
            Lock();
            Unlock(Ignore);
        }
        public void ChartDataClear()
        {
            for (int i = 0; i < 12; i++)
            {
                ChartOkCount[i] = 0;
                ChartLensHeightFailCount[i] = 0;
                ChartVisionInspectFailCount[i] = 0;
                ChartAct1FailCount[i] = 0;
                ChartSidAngleFailCount[i] = 0;
                ChartAct2FailCount[i] = 0;
                ChartAct3FailCount[i] = 0;
                ChartSideHeightFailCount[i] = 0; //Adding Error
            }
            SideAngleData.Clear();
            SideAngleCountSave();
        }
        public void DailyDataClear(string TimeMode)
        {
            int ok = 0;
            int LensFail = 0;
            int VisionFail = 0;
            int Act1Fail = 0;
            int SideFail = 0;
            int Act2Fail = 0;
            int Act3Fail = 0;
            int SideHeightFail = 0; //Adding Error
            for (int i = 0; i < 12; i++)
            {
                ok += ChartOkCount[i];
                LensFail += ChartLensHeightFailCount[i];
                VisionFail += ChartVisionInspectFailCount[i];
                Act1Fail += ChartAct1FailCount[i];
                SideFail += ChartSidAngleFailCount[i];
                Act2Fail += ChartAct2FailCount[i];
                Act3Fail += ChartAct3FailCount[i];
                SideHeightFail += ChartSideHeightFailCount[i]; //Adding Error
            }
            int total = FDailyOKCount + FDailyNGCount;

            double TotalSide = 0.0;
            foreach (double d in SideAngleData)
            {
                TotalSide += d;
            }

            double yield = TotalSide / SideAngleData.Count;

            cDEF.TaskLogAppend(TaskLog.Count, $"\n" +
                $"[{TimeMode}],\n" +
                $"Total,{total},\n" +
                $"OK,{FDailyOKCount},\n" +
                $"NG,{FDailyNGCount},\n" +
                $"Chart OK,{ok}, {((double) ok / (double)total * 100.0).ToString("N2")} %,\n" +
                $"Chart Lens Height Fail, {LensFail}, {((double)LensFail / (double)total * 100.0).ToString("N2")} %,\n" +
                $"Chart Vision Inspect Fail, {VisionFail}, {((double)VisionFail / (double)total * 100.0).ToString("N2")} %,\n" +
                $"Chart Actuating 1 Fail, {Act1Fail}, {((double)Act1Fail / (double)total * 100.0).ToString("N2")} %,\n" +
                $"Chart Side Angle Fail, {SideFail}, {((double)SideFail / (double)total * 100.0).ToString("N2")} %,\n" +
                $"Chart Actuating 2 Fail, {Act2Fail}, {((double)Act2Fail / (double)total * 100.0).ToString("N2")} %,\n" +
                $"Chart Actuating 3 Fail, {Act3Fail}, {((double)Act3Fail / (double)total * 100.0).ToString("N2")} %,\n" +
                $"Chart Side Height Fail, {SideHeightFail}, {((double)SideHeightFail / (double)total * 100.0).ToString("N2")} %,\n" + //Adding Error
                $"Side Angle AVG, {yield.ToString("N3")} \n\n", true);
            FDailyOKCount = 0;
            FDailyNGCount = 0;
            ChartDataClear();
        }
        public void SideAngleCountSave()
        {
            FileStream fs = new FileStream(Recovery_SideAngleCountPath, FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, SideAngleData);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                fs.Close();
            }
        }

        public void SideAngleCountOpen()
        {
            if (!File.Exists(Recovery_SideAngleCountPath))
                return;
            FileStream fs = new FileStream(Recovery_SideAngleCountPath, FileMode.Open);
            try
            {
                
                BinaryFormatter formatter = new BinaryFormatter();
                SideAngleData = (List<double>)formatter.Deserialize(fs);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                fs.Close();
            }
        }
        
    }
    public enum TRunUnloadPickerMode
    {
        Stop,
        Pick,
        Place
    };

    
    public class RunUnloadPicker : TfpRunningModule 
    {
        //Evnet
        public delegate void UnloaderDisplayHandler(int x, int y, LensTrayStatus status);
        public event UnloaderDisplayHandler OnVCM_Display;
        public delegate void NGDisplayHandler(int x, int y, eFailType FailType);
        public event NGDisplayHandler OnNg_Display;

        public delegate void ChartYieldHandler(int Index, eFailType FailType);
        public event ChartYieldHandler OnYieldChart;

        private RunUnloadPickerInformation FInformation;
        private TRunUnloadPickerMode FMode;

        public int FCalc;
        //WorkIndex
        public Lens_Data WorkLens;

        private TimeSpan Part1;
        private TimeSpan Part2;
        private int oldTime;
        private bool Part1Reset = false;
        private bool Part2Reset = false;

        StringBuilder strbPD = new StringBuilder();
        StringBuilder strbTT = new StringBuilder();
        public RunUnloadPicker(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunUnloadPickerInformation();
            WorkLens = new Lens_Data();

            Part1 = new TimeSpan(8, 30, 0);
            Part2 = new TimeSpan(20, 30, 0);
            Part1Reset = false;
            Part2Reset = false;
        }
        

        #region **Property**
        public RunUnloadPickerInformation Information
        {
            get { return FInformation; }
        }

        public TRunUnloadPickerMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        // Motor
        public TfpMotionItem HeadX
        {
            get { return GetMotions(0); }
        }
        public TfpMotionItem HeadY
        {
            get { return GetMotions(1); }
        }
        public TfpMotionItem HeadZ
        {
            get { return GetMotions(2); }
        }
        public TfpMotionItem HeadT
        {
            get { return GetMotions(3); }
        }
        public TfpMotionItem StageY
        {
            get { return GetMotions(4); }
        }
        #endregion //Property//

        private TRunUnloadPickerMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunUnloadPickerMode.Stop;
        }
        private void SetMode(TRunUnloadPickerMode Value)
        {
            if (Value == TRunUnloadPickerMode.Stop)
                DetailMode = TfpRunningModuleMode.fmmStop;
            else
            {
                DetailMode = TfpRunningModuleMode.fmmRun;
                FMode = Value;
            }
        }
        public override string ModeToString()
        {
            switch (Mode)
            {
                case TRunUnloadPickerMode.Stop:
                    return "Stop";
                case TRunUnloadPickerMode.Pick:
                    return "Pick";
                case TRunUnloadPickerMode.Place:
                    return "Place";

                default:
                    return "";
            }
        }
        protected override void ProcReal()
        {
            if(MotionCount > 0 && cDEF.Run.DetailMode == TfpRunningMode.frmStop)
            {
                if (DateTime.Now.TimeOfDay > TimeSpan.Parse(cDEF.Work.Option.ResetTime1.ToString("00:00")) && DateTime.Now.TimeOfDay < TimeSpan.Parse(cDEF.Work.Option.ResetTime2.ToString("00:00")) && !Part1Reset)
                {
                    Information.DailyDataClear("NIGHT");
                    Part1Reset = true;
                }
                else if (DateTime.Now.TimeOfDay > TimeSpan.Parse(cDEF.Work.Option.ResetTime2.ToString("00:00"))  && !Part2Reset)
                {
                    Information.DailyDataClear("DAY");
                    Part2Reset = true;
                }
                else
                {
                    if (oldTime != DateTime.Now.Day)
                    {
                        Part1Reset = false;
                        Part2Reset = false;
                    }
                    oldTime = DateTime.Now.Day;
                }
            }
        }
        protected override bool ProcInitialize()
        {
            switch (Step)
            {
                case 0:
                    HeadZ.Home();
                    Step++;
                    break;
                case 1:
                    if (!HeadZ.HomeEnd)
                        return false;
                    Step++;
                    break;
                case 2:
                    HeadY.Home();
                    Step++;
                    break;
                case 3:
                    if (!HeadY.HomeEnd)
                        return false;
                    if (!cDEF.Run.PlateAngle.Fw_RvCylinder.IsBackward())
                        return false;
                    HeadX.Home();
                    HeadT.Home();
                    Step++;
                    break;
                case 4:
                    if (!HeadX.HomeEnd || !HeadT.HomeEnd)
                        return false;
                    if (!cDEF.Run.Unloader.TransferX.HomeEnd)
                        return false;
                    StageY.Home();
                    Step++;
                    break;
                case 5:
                    if (!StageY.HomeEnd)
                        break;
                    Step++;
                    break;
                case 6:
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Initialize] End", true);
                    return true;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return false;
        }
        protected override bool ProcToRun()
        {
            if (!IsReady())
                return false;

            switch (Step)
            {
                case 0:
                    Move_Head_ReadyPositionZ();
                    Step++;
                    break;

                case 1:
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Step++;
                    break;

                case 2:
                    if(cDEF.Run.Digital.Input[cDI.Stage_VCMUnLoading_NG_Tray_Check_Sensor])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunUnloaderPicker + 203;
                        cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 203, "");
                        Mode = TRunUnloadPickerMode.Stop;
                        return true;
                    }

                    
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.Unload, "[To-Run] Done.", true);
                    return true;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return false;
        }
        protected override bool ProcToStop()
        {
            if (!IsReady())
                return false;
            switch (Step)
            {
                case 0:
                    Move_Head_ReadyPositionZ();
                    Step++;
                    break;
                case 1:
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Unload, "[To-Stop] Done.", true);
                    return true;
                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return false;

        }
        protected override bool ProcToWarning()
        {
            if (!IsReady())
                return false;
            switch (Step)
            {
                case 0:
                    Move_Head_ReadyPositionZ();
                    Step++;
                    break;
                case 1:
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Unload, "[To-Warning] Done.", true);
                    return true;
                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return false;
        }
        protected override void ProcBegin()
        {
            switch (Mode)
            {
                case TRunUnloadPickerMode.Pick:
                    Running_Pick();
                    break;

                case TRunUnloadPickerMode.Place:
                    Running_Place();
                    break;
            }
        }
        
        protected override void ProcMain()
        {
            if (cDEF.Run.Mode == TRunMode.Main_Run)
            {
                // Pick
                if (cDEF.Run.Unloader.Information.ExistStage
                    && cDEF.Run.PlateAngle.Information.LensData.Status == LensTrayStatus.Load
                    && cDEF.Run.PlateAngle.Information.MeasureFinish
                    && Information.HeadLensData.Status == LensTrayStatus.Empty)
                {
                    cDEF.Tact.UnloadPicker.Start();
                    Mode = TRunUnloadPickerMode.Pick;
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Pick Start.", true);
                    return;
                }


                // Place
                if (Information.HeadLensData.Status == LensTrayStatus.Load && !Information.Unloader_Tray.IsFinish())
                {
                    if (Information.Result)
                    {
                        if (GetEmptyTray())
                        {
                            Mode = TRunUnloadPickerMode.Place;
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Place Start.", true);
                            return;
                        }
                       
                    }
                    else
                    {
                        if(GetEmptyNGTray())
                        {
                            Mode = TRunUnloadPickerMode.Place;
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Place Start.", true);
                            return;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloaderPicker + 200;
                            //cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 200, "[UNLOAD PICKER] NG Tray Full");
                            cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 200, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] NG Tray is Full.", true);
                            return;
                        }
                    }
                    
                }

            }

            if (cDEF.Run.Mode == TRunMode.Manual_UnloadPick)
            {
                cDEF.Tact.UnloadPicker.Start();
                Mode = TRunUnloadPickerMode.Pick;
                cDEF.TaskLogAppend(TaskLog.Unload, "[Unloader - Manual] Pick Start.", true);
                return;
            }

            if (cDEF.Run.Mode == TRunMode.Manual_UnloadStagePlace)
            {
                if (Information.Result)
                {
                    if (GetEmptyTray())
                    {
                        cDEF.Tact.UnloadPicker.Start();
                        Mode = TRunUnloadPickerMode.Place;
                        cDEF.TaskLogAppend(TaskLog.Unload, "[Unloader - Manual]  Stage Place Start.", true);


                        return;
                    }

                }
                else
                {
                    if (GetEmptyNGTray())
                    {
                        cDEF.Tact.UnloadPicker.Start();
                        Mode = TRunUnloadPickerMode.Place;
                        cDEF.TaskLogAppend(TaskLog.Unload, "[Unloader - Manual] NG Tray Place Start.", true);

                        return;
                    }
                }
            }

        }
        public bool GetEmptyTray()
        {
            foreach (Lens_Data rd in Information.Unloader_Tray.Items)
            {
                if (rd.Status == LensTrayStatus.Load)
                {
                    WorkLens = rd;
                    return true;
                }
            }
            return false;
        }
        public bool GetEmptyNGTray()
        {
            foreach (Lens_Data rd in Information.NG_tray.Items)
            {
                if (rd.Status == LensTrayStatus.Empty)
                {
                     WorkLens = rd;
                    return true;
                }
            }
            return false;
        }
        #region Running Func
        protected void Running_Pick()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Ready Position Z.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 1:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (!cDEF.Run.PlateAngle.Fw_RvCylinder.IsBackward())
                            break;
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunUnloaderPicker + 201;
                        //cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 201, "[UNLOAD PICKER] Side Angle Measure FW 실린더 Time Out.");
                        cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 201, "");
                        cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Unload Picker Side Angle Measere Fw-Bw Cylinder Time Out.", true);
                        Mode = TRunUnloadPickerMode.Stop;
                        return;
                    }
                    break;

                case 2:
                    Move_Head_IndexPickPositionX();
                    Move_Head_IndexPickPositionY();
                    Move_Head_IndexPickPositionT();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Index Pick Postion X Y T.", true);
                    Step++;
                    break;

                case 3:
                    Move_Head_IndexPickPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Index Pick Position Z.",  true);
                    Step++;
                    break;

                case 4:
                    //I.O 
                    cDEF.Run.Digital.Output[cDO.VCM_Unloading_Vacuum] = true;
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Vacuum On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < cDEF.Work.UnloadPicker.UnloaderVacDelay)
                        break;

                    Step++;
                    break;

                case 6:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 7:
                    if (cDEF.Run.Mode == TRunMode.Manual_UnloadPick)
                    {
                        Move_Head_ReadyPositionX();
                        Move_Head_ReadyPositionY();
                        Step++;
                        break;
                    }
                    //Move_Head_StagePlacePositionT();
                    //cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Ready Position X Y.", true);
                    Step++;
                    break;

                case 8:
                    // Vac Check
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.VCMUnloading_Vacuum_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloaderPicker + 202;
                            //cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 202, "[UNLOAD PICKER] Vacuum Check Fail.");
                            cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 202, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Vacuum Check Fail.", true);
                            Mode = TRunUnloadPickerMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 9:
                    Information.Result = cDEF.Run.PlateAngle.Information.Result;
                    cDEF.Run.PlateAngle.Information.Result = false;

                    if (Information.Result)
                    {
                        //다시 한번 검증 필요....
                        if (cDEF.Work.Project.GlobalOption.UseResultDummyPass)
                        {
                            if (cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.LensHeightFail
                                 || cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.VisionInspectFail
                                 || cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.Actuating1Fail)
                            {
                                Information.Result = false;
                                cDEF.TaskLogAppend(TaskLog.Unload, $"[Unload Picker]Index:{cDEF.Run.PlateAngle.Information.LensData.Index + 1} UseResultDummyPass FailType:{cDEF.Run.PlateAngle.Information.LensData.FailType}.", true);

                            }
                        }
                        else
                        {
                            
                            if (cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.LensHeightFail
                                 || cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.VisionInspectFail
                                 || cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.Actuating1Fail
                                 || cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.Actuating2Fail
                                 || cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.PlateAngleFail
                                 || cDEF.Run.PlateAngle.Information.LensData.FailType == eFailType.SideHeightFail) //Adding Error
                            {
                                Information.Result = false;
                                cDEF.TaskLogAppend(TaskLog.Unload, $"[Unload Picker]Index:{cDEF.Run.PlateAngle.Information.LensData.Index + 1}  NotUseResultDummyPass FailType:{cDEF.Run.PlateAngle.Information.LensData.FailType}.", true);

                            }
                        }
                    }
                    else
                    {
                        cDEF.TaskLogAppend(TaskLog.Unload, $"[Unload Picker]Index:{cDEF.Run.PlateAngle.Information.LensData.Index + 1}  FailType:{cDEF.Run.PlateAngle.Information.LensData.FailType} Judge:{Information.Result}.", true);

                    }

                    Information.HeadLensData.Assign(cDEF.Run.PlateAngle.Information.LensData);
                    cDEF.Run.PlateAngle.Information.LensData.Clear();
                    Step++;
                    break;

                case 10:
                    if (cDEF.Run.Mode == TRunMode.Manual_UnloadPick)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunUnloadPickerMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Unload, $"[Unload Picker] Pick End - Cycle Time [{cDEF.Tact.UnloadPicker.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        protected void Running_Place()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Ready Position Z", true);
                    Step++;
                    break;

                case 1:
                    if (Information.Result)
                    {
                        Move_Head_WorkPositionXY(WorkLens.x, WorkLens.y);
                        cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Stage Work Position XY", true);
                    }
                    else
                    {
                        Move_Head_NGTray_WorkPositionXY(WorkLens.x, WorkLens.y);
                        cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head NG Tray Work Position XY", true);
                    }

                    Move_Head_StagePlacePositionT();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Stage Place Position T", true);
                    Step++;
                    break;

                case 2:
                    if (Information.Result)
                    {
                        if (cDEF.Work.Option.PlaceOverrideUse == 0)
                        {
                            Move_Head_StagePlacePositionZ();
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Stage Place Position Z", true);
                        }
                        else
                        {
                            Move_Head_PlaceOverridePositionZ();
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Override Position Z", true);
                        }

                    }
                    else
                    {
                        if (cDEF.Work.Option.PlaceOverrideUse == 0)
                        {
                            Move_Head_NGTrayPositionZ();
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head NG Tray Position Z", true);
                        }
                        else
                        {
                            Move_Head_NG_PlaceOverridePositionZ();
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head NG Tray Override Position Z", true);
                        }
                    }
                    Step++;
                    break;

                case 3:
                    //I.O 
                    cDEF.Run.Digital.Output[cDO.VCM_Unloading_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Vacuum Off", true);
                    cDEF.Run.Digital.Output[cDO.VCM_Unloading_Blow] = true;
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Blow On", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < cDEF.Work.UnloadPicker.UnloaderBlowDelay)
                        break;
                    Step++;
                    break;

                case 5:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Ready Position Z", true);
                    Step++;
                    break;

                case 6:
                    cDEF.Run.Digital.Output[cDO.VCM_Unloading_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Blow Off", true);

                    Step++;
                    break;

                case 7:
                    Move_Head_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Ready Position X", true);
                    Move_Head_ReadyPositionY();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Ready Position Y", true);
                    Move_Head_IndexPickPositionT();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Head Index Place Position T", true);
                    Step++;
                    break;

                case 8:
                    //low 데이터 출력
                    cDEF.TaskLogAppend(TaskLog.LowData, $"Index : {Information.HeadLensData.Index + 1},{Information.HeadLensData.FailType},{Information.HeadLensData.LensHeightData},{Information.HeadLensData.PlateAngleData}", true);
                    cDEF.Tact.UnloadPicker.GetTact();
                    Information.HeadLensData.TT_UnloadPicker = cDEF.Tact.UnloadPicker.CycleTime;
                    MESDataSend_PD();
                    MESDataSend_TT();
              
                    OnYieldChart(Information.HeadLensData.Index, Information.HeadLensData.FailType);
                    
                    WorkLens.Status = LensTrayStatus.Finish;

                    WorkLens.Index = Information.HeadLensData.Index + 1;
                    WorkLens.FailType = Information.HeadLensData.FailType;
                    WorkLens.LensHeightData = Information.HeadLensData.LensHeightData;
                    WorkLens.PlateAngleData = Information.HeadLensData.PlateAngleData;

                    if (Information.Result)
                        OnVCM_Display(WorkLens.x, WorkLens.y, LensTrayStatus.Finish);
                    else
                        OnNg_Display(WorkLens.x, WorkLens.y, Information.HeadLensData.FailType);
                    Information.HeadLensData.Clear();
              
                    Step++;
                    break;

                case 9:
                    if (!Information.Result)
                    {
                        if (!GetEmptyNGTray())
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloaderPicker + 200;
                            //cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 200, "[UNLOAD PICKER] NG Tray Full");
                            cDEF.Run.LogWarning(cLog.RunUnloaderPicker + 200, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] NG Tray is Full.", true);
                            if (cDEF.Run.Mode == TRunMode.Manual_UnloadStagePlace)
                                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                            Mode = TRunUnloadPickerMode.Stop;
                            cDEF.TaskLogAppend(TaskLog.Unload, $"[Unload Picker] Place End.", true);

                            return;
                        }
                    }
                    Step++;
                    break;
                case 10:
                    if (cDEF.Run.Mode == TRunMode.Manual_UnloadStagePlace)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunUnloadPickerMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Unload, $"[Unload Picker] Place End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        #endregion
        #region Move Command
        //Head X
        public void Move_Head_ReadyPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachUnloadPicker.ReadyPositionX, Sleep);
        }
        public void Move_Head_StageFirstPlacePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachUnloadPicker.StageFirstPlacePositionX, Sleep);
        }
        public void Move_Head_IndexPickPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachUnloadPicker.IndexPickPositionX, Sleep);
        }
        public void Move_Head_NGTrayPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachUnloadPicker.NGTrayPositionX, Sleep);
        }
        //Head Y
        public void Move_Head_ReadyPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachUnloadPicker.ReadyPositionY, Sleep);
        }
        public void Move_Head_StagePlacePositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachUnloadPicker.StagePlacePositionY, Sleep);
        }
        public void Move_Head_IndexPickPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachUnloadPicker.IndexPickPositionY, Sleep);
        }
        public void Move_Head_NGTrayPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachUnloadPicker.NGTrayPositionY, Sleep);
        }
        //Head Z
        public void Move_Head_ReadyPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachUnloadPicker.ReadyPositionZ, Sleep);
        }
        public void Move_Head_StagePlacePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachUnloadPicker.StagePlacePositionZ, Sleep);
        }
        public void Move_Head_IndexPickPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachUnloadPicker.IndexPickPositionZ, Sleep);
        }
        public void Move_Head_NGTrayPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachUnloadPicker.NGTrayPositionZ, Sleep);
        }
        public void Move_Head_PlaceOverridePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayZ;
            if (cDEF.Run.Motion.Simul)
                HeadZ.Absolute(cDEF.Work.TeachUnloadPicker.StagePlacePositionZ, Sleep);
            else
            HeadZ.Absolute(cDEF.Work.TeachUnloadPicker.StagePlacePositionZ, HeadZ.Speed.FRun.FMaximumVelocity, cDEF.Work.TeachUnloadPicker.StagePlacePositionZ - cDEF.Work.TeachUnloadPicker.StageStepPlaceOffset, HeadZ.Speed.FRun.FMaximumVelocity * (double)(cDEF.Work.UnloadPicker.StepPlaceSpeed) / 100.0, Sleep);
        }
        public void Move_Head_NG_PlaceOverridePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayZ;
            if (cDEF.Run.Motion.Simul)
                HeadZ.Absolute(cDEF.Work.TeachUnloadPicker.NGTrayPositionZ, Sleep);
            else
                HeadZ.Absolute(cDEF.Work.TeachUnloadPicker.NGTrayPositionZ, HeadZ.Speed.FRun.FMaximumVelocity, cDEF.Work.TeachUnloadPicker.NGTrayPositionZ - cDEF.Work.TeachUnloadPicker.NGTrayStepPlaceOffset, HeadZ.Speed.FRun.FMaximumVelocity * (double)(cDEF.Work.UnloadPicker.NgStepPlaceSpeed) / 100.0, Sleep);
        }
        //Head T
        public void Move_Head_ReadyPositionT()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayT;
            HeadT.Absolute(cDEF.Work.TeachUnloadPicker.ReadyPositionT, Sleep);
        }
        public void Move_Head_StagePlacePositionT()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayT;
            HeadT.Absolute(cDEF.Work.TeachUnloadPicker.StagePlacePositionT, Sleep);
        }
        public void Move_Head_IndexPickPositionT()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayT;
            HeadT.Absolute(cDEF.Work.TeachUnloadPicker.IndexPickPositionT, Sleep);
        }

        public void Move_Head_NGTrayPositionT()
        {
            int Sleep;
            Sleep = cDEF.Work.UnloadPicker.MovingDelayT;
            HeadT.Absolute(cDEF.Work.TeachUnloadPicker.NGTrayPositionT, Sleep);
        }
        //Head XY
        public void Move_Head_WorkPositionXY(int IndexX , int IndexY)
        {
            int SleepX;
            int SleepY;
            int SleepStageY;
            SleepX = cDEF.Work.UnloadPicker.MovingDelayX;
            SleepY = cDEF.Work.UnloadPicker.MovingDelayY;
            SleepStageY = cDEF.Work.Unloader.MovingDelayY;

            int posX = cDEF.Work.TeachUnloadPicker.StageFirstPlacePositionX;
            posX += cDEF.Work.Unloader.TrayPitchX * IndexX;

            int posY = cDEF.Work.TeachUnloadPicker.StagePlacePositionY;

            int posStageY = cDEF.Work.TeachUnloader.StageFirstPlacePositionY;
            posStageY += cDEF.Work.Unloader.TrayPitchY * IndexY;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
            StageY.Absolute(posStageY, SleepStageY);
        }

        public void Move_Head_AvoidPositionXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.UnloadPicker.MovingDelayX;
            SleepY = cDEF.Work.UnloadPicker.MovingDelayY;

            int posX = cDEF.Work.TeachUnloadPicker.AvoidPositionX;

            int posY = cDEF.Work.TeachUnloadPicker.AvoidPositionY;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
        }

        public void Move_Stage_ReadyPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayX;
            StageY.Absolute(cDEF.Work.TeachUnloader.ReadyPositionY, Sleep);
        }

        public void Move_Head_NGTray_WorkPositionXY(int IndexX, int IndexY)
        {
            int SleepX;
            int SleepY;
            int SleepStageY;

            SleepX = cDEF.Work.UnloadPicker.MovingDelayX;
            SleepY = cDEF.Work.UnloadPicker.MovingDelayY;
            SleepStageY = cDEF.Work.Unloader.MovingDelayY;

            int posX = cDEF.Work.TeachUnloadPicker.NGTrayPositionX;
            posX += cDEF.Work.Unloader.NG_TrayPitchX * IndexX;

            int posY = cDEF.Work.TeachUnloadPicker.StagePlacePositionY;

            int posStageY = cDEF.Work.TeachUnloader.NGTrayFirstPlacePositionY;
            posStageY += cDEF.Work.Unloader.NG_TrayPitchY * IndexY; ;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
            StageY.Absolute(posStageY, SleepStageY);
        }

        //Stage
        public void Move_Stage_EjectPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayX;
            StageY.Absolute(cDEF.Work.TeachUnloader.EjectPositionY, Sleep);
        }
        public void Move_Stage_WorkPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayX;
            StageY.Absolute(cDEF.Work.TeachUnloader.WorkPositionY, Sleep);
        }
        public void Move_Stage_FirstPickPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayY;
            StageY.Absolute(cDEF.Work.TeachUnloader.StageFirstPlacePositionY, Sleep);
        }
        public void Move_Stage_MagazineChangePositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayY;
            StageY.Absolute(cDEF.Work.TeachUnloader.StageMagazineChangePositionY, Sleep);
        }
        #endregion

        #region CheckPosition
        public bool Is_Head_ReadyPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.ReadyPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StageFirstPlacePositionX()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.StageFirstPlacePositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPickPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.IndexPickPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.ReadyPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePlacePositionY()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.StagePlacePositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPickPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.IndexPickPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.ReadyPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePlacePositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.StagePlacePositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPickPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.IndexPickPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionT()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.ReadyPositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePlacePositionT()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.StagePlacePositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPickPositionT()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.IndexPickPositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_NGTrayPositionT()
        {
            if (IsRange((double)cDEF.Work.TeachUnloadPicker.NGTrayPositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        #endregion
        private bool IsRange(double target, double current, double limit = 5)
        {
            if (Math.Abs(target - current) < limit)
                return true;
            else
                return false;
        }
        public bool Is_Stage_ReadyPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachUnloader.ReadyPositionY, StageY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_EjectPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachUnloader.EjectPositionY, StageY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_WorkPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachUnloader.WorkPositionY, StageY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_MagazineChangePositionY()
        {
            if (IsRange((double)cDEF.Work.TeachUnloader.StageMagazineChangePositionY, StageY.ActualPosition))
                return true;
            else
                return false;
        }

        public void ChartDisplayTest(int Index, eFailType failType)
        {
            OnYieldChart(Index, failType);
        }

        private void MESDataSend_PD()
        {

            // if (cDEF.Work.Project.GlobalOption.UseMES)
            {
                strbPD.Clear();

                strbPD.Append("VER,");
                strbPD.Append("VER-001");
                strbPD.Append("\r\n");
                strbPD.Append("EQUIP_ID,");
                strbPD.Append(cDEF.Mes.EQPCode);
                strbPD.Append("\r\n");
                strbPD.Append("DEVICE,");
                strbPD.Append(cDEF.Mes.DeviceCode);
                strbPD.Append("\r\n");
                strbPD.Append("OPERAITON,");
                strbPD.Append(cDEF.Mes.OperationCode);
                strbPD.Append("\r\n");
                strbPD.Append("PROD_TYPE,");
                strbPD.Append(cDEF.Mes.Product_TypeCode);
                strbPD.Append("\r\n");
                strbPD.Append("BARCODE,NA");
                strbPD.Append("\r\n");
                strbPD.Append("EQIP_ZONE,1");
                strbPD.Append("\r\n");
                strbPD.Append("TRANS_TIME,");
                strbPD.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
                strbPD.Append("\r\n");

                strbPD.Append("RESULT,");
                if (Information.Result)
                    strbPD.Append("OK");
                else
                    strbPD.Append("NG");
                strbPD.Append("\r\n");

                strbPD.Append("NG_CODE");
                if (Information.HeadLensData.FailType != eFailType.None)
                {
                    strbPD.Append(",");
                    strbPD.Append(Information.HeadLensData.FailType);
                }
                else
                    strbPD.Append(",NA");
                strbPD.Append("\r\n");

                strbPD.Append("START_TEST_RESULT");
                strbPD.Append("\r\n");

                strbPD.Append("LensHeight");
                if (cDEF.Work.Project.GlobalOption.UseLensHeight)
                {
                    strbPD.Append(",");
                    if (Information.HeadLensData.FailType == eFailType.LensHeightFail)
                    {
                        strbPD.Append(Information.HeadLensData.LensHeightData);
                        strbPD.Append(",,NG");

                    }
                    else
                    {
                        strbPD.Append(Information.HeadLensData.LensHeightData);
                        strbPD.Append(",,OK");

                    }
                }
                else
                {
                    strbPD.Append(",0,,NA");
                }
                strbPD.Append("\r\n");

                strbPD.Append("Vision_Exist");
                if (cDEF.Work.Project.GlobalOption.UseVision)
                {
                    strbPD.Append(",,,");
                    if (Information.HeadLensData.FailType == eFailType.VisionInspectFail)
                    {
                        strbPD.Append("NG");
                    }
                    else
                    {
                        if (Information.HeadLensData.FailType == eFailType.None)
                            strbPD.Append("OK");
                        else
                            strbPD.Append("NA");
                    }
                }
                else
                {
                    strbPD.Append(",,,");
                    strbPD.Append("NA");
                }
                strbPD.Append("\r\n");

                strbPD.Append("Acutator_1");
                strbPD.Append(",,,");
                if (Information.HeadLensData.FailType == eFailType.Actuating1Fail)
                {
                    strbPD.Append("NG");
                }
                else
                {
                    if (Information.HeadLensData.FailType == eFailType.None)
                        strbPD.Append("OK");

                    else
                        strbPD.Append("NA");
                }
                strbPD.Append("\r\n");

                strbPD.Append("Acutator_2");
                strbPD.Append(",,,");
                if (cDEF.Work.Project.GlobalOption.UseCuring2)
                {
                    if (Information.HeadLensData.FailType == eFailType.Actuating2Fail)
                    {
                        strbPD.Append("NG");
                    }
                    else
                    {
                        if (Information.HeadLensData.FailType == eFailType.None)
                            strbPD.Append("OK");

                        else
                            strbPD.Append("NA");
                    }
                }
                else
                {
                    strbPD.Append("NA");
                }
                strbPD.Append("\r\n");

                strbPD.Append("Side_Angle_Value");
                if (cDEF.Work.Project.GlobalOption.UsePlateAngle)
                {
                    strbPD.Append(",,,");
                    if (Information.HeadLensData.FailType == eFailType.PlateAngleFail)
                    {
                        strbPD.Append("NG");
                    }
                    else
                    {
                        if (Information.HeadLensData.FailType == eFailType.None)
                            strbPD.Append("OK");

                        else
                            strbPD.Append("NA");
                    }
                }
                else
                {
                    strbPD.Append(",,,");
                    strbPD.Append("NA");
                }
                strbPD.Append("\r\n");

                strbPD.Append("Side_Angle_Average");
                if (cDEF.Work.Project.GlobalOption.UsePlateAngle)
                {
                    strbPD.Append(",");
                    double average = Information.SideAngleData.Count > 0 ? Information.SideAngleData.Average() : 0.0;
                    strbPD.Append(average);
                    strbPD.Append(",,");
                    if (Information.HeadLensData.FailType == eFailType.PlateAngleFail)
                    {
                        strbPD.Append("NG");
                    }
                    else
                    {
                        if (Information.HeadLensData.FailType == eFailType.None)
                            strbPD.Append("OK");

                        else
                            strbPD.Append("NA");
                    }

                }
                else
                {
                    strbPD.Append(",0,,NA");
                }
                strbPD.Append("\r\n");
                strbPD.Append("END_TEST_RESULT");

                cDEF.MesLog.LogWrite_Product(strbPD.ToString());
            }
        }

        private void MESDataSend_TT()
        {

            // if (cDEF.Work.Project.GlobalOption.UseMES)
            {
                strbTT.Clear();

                strbTT.Append("VER,");
                strbTT.Append("VER-001");
                strbTT.Append("\r\n");
                strbTT.Append("EQUIP_ID,");
                strbTT.Append(cDEF.Mes.EQPCode);
                strbTT.Append("\r\n");
                strbTT.Append("DEVICE,");
                strbTT.Append(cDEF.Mes.DeviceCode);
                strbTT.Append("\r\n");
                strbTT.Append("OPERAITON,");
                strbTT.Append(cDEF.Mes.OperationCode);
                strbTT.Append("\r\n");
                strbTT.Append("PROD_TYPE,");
                strbTT.Append(cDEF.Mes.Product_TypeCode);
                strbTT.Append("\r\n");
                strbTT.Append("BARCODE,NA");
                strbTT.Append("\r\n");
                strbTT.Append("EQIP_ZONE,1");
                strbTT.Append("\r\n");
                strbTT.Append("TRANS_TIME,");
                strbTT.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
                strbTT.Append("\r\n");

                strbTT.Append("RESULT,");
                if (Information.Result)
                    strbTT.Append("OK");
                else
                    strbTT.Append("NG");
                strbTT.Append("\r\n");

                strbTT.Append("START_TEST_RESULT");
                strbTT.Append("\r\n");
                strbTT.Append("Full_Seq_T/T,");
                strbTT.Append(Information.HeadLensData.TT_LensTact.ToString("N3"));
                if (Information.Result)
                    strbTT.Append(",OK");
                else
                    strbTT.Append(",NG");
                strbTT.Append("\r\n");

                strbTT.Append("VCM_Vision_Align_T/T,");
                strbTT.Append(Information.HeadLensData.TT_TopVision.ToString("N3"));
                strbTT.Append(",OK");
                strbTT.Append("\r\n");

                strbTT.Append("UPPER_Vision_Align_T/T,");
                strbTT.Append(Information.HeadLensData.TT_LensPickerPickCam.ToString("N3"));
                if (cDEF.Work.Project.GlobalOption.UseLensHeight)
                {
                    if (Information.HeadLensData.FailType == eFailType.LensHeightFail)
                        strbTT.Append(",NG");
                    else
                        strbTT.Append(",OK");
                }
                else
                    strbTT.Append(",NA");

                strbTT.Append("\r\n");

                strbTT.Append("UNDER_Vision_Align_T/T,");
                strbTT.Append(Information.HeadLensData.TT_LensPickerCam.ToString("N3"));
                if (cDEF.Work.Project.GlobalOption.UseLensHeight)
                {
                    if (Information.HeadLensData.FailType == eFailType.LensHeightFail)
                        strbTT.Append(",NG");
                    else
                        strbTT.Append(",OK");
                }
                else
                    strbTT.Append(",NA");
                strbTT.Append("\r\n");

                strbTT.Append("Lens_Picker_T/T,");
                strbTT.Append(Information.HeadLensData.TT_LensPicker.ToString("N3"));
                if (cDEF.Work.Project.GlobalOption.UseLensHeight)
                {
                    if (Information.HeadLensData.FailType == eFailType.LensHeightFail)
                        strbTT.Append(",NG");
                    else
                        strbTT.Append(",OK");
                }
                else
                    strbTT.Append(",NA");
                strbTT.Append("\r\n");

                strbTT.Append("Bonder1_Vision_Align_T/T,");
                strbTT.Append(Information.HeadLensData.TT_Bonder1Cam.ToString("N3"));
                if (Information.HeadLensData.FailType == eFailType.None)
                    strbTT.Append(",OK");
                else
                    strbTT.Append(",NG");
                strbTT.Append("\r\n");

                strbTT.Append("Bonder1_Dispensing_T/T,");
                strbTT.Append(Information.HeadLensData.TT_Bonder1.ToString("N3"));
                if (Information.HeadLensData.FailType == eFailType.None)
                    strbTT.Append(",OK");
                else
                    strbTT.Append(",NG");
                strbTT.Append("\r\n");

                strbTT.Append("Bonder2_Vision_Align_T/T,");
                strbTT.Append(Information.HeadLensData.TT_Bonder2Cam.ToString("N3"));
                if (Information.HeadLensData.FailType == eFailType.None)
                    strbTT.Append(",OK");
                else
                    strbTT.Append(",NG");
                strbTT.Append("\r\n");

                strbTT.Append("Bonder2_Dispensing_T/T,");
                strbTT.Append(Information.HeadLensData.TT_Bonder2.ToString("N3"));
                if (Information.HeadLensData.FailType == eFailType.None)
                    strbTT.Append(",OK");
                else
                    strbTT.Append(",NG");
                strbTT.Append("\r\n");

                strbTT.Append("EXIST_Vision_Align_T/T,");
                strbTT.Append(Information.HeadLensData.TT_VisionInspect.ToString("N3"));
                if (cDEF.Work.Project.GlobalOption.UseVision)
                {
                    if (Information.HeadLensData.FailType == eFailType.VisionInspectFail)
                        strbTT.Append(",NG");
                    else
                    {
                        if (Information.HeadLensData.FailType == eFailType.None)
                            strbTT.Append(",OK");
                        else
                            strbTT.Append(",NG");
                    }
                }
                else
                    strbTT.Append(",NA");
                strbTT.Append("\r\n");

                strbTT.Append("Curing_1_T/T,");
                strbTT.Append(Information.HeadLensData.TT_Curing1.ToString("N3"));

                if (Information.HeadLensData.FailType == eFailType.Actuating1Fail)
                {
                    strbTT.Append(",NG");
                }
                else
                {
                    if (Information.HeadLensData.FailType == eFailType.None)
                        strbTT.Append(",OK");
                    else
                        strbTT.Append(",NG");
                }

                strbTT.Append("\r\n");

                strbTT.Append("Curing_2_T/T,");
                strbTT.Append(Information.HeadLensData.TT_Curing2.ToString("N3"));

                if (cDEF.Work.Project.GlobalOption.UseCuring2)
                {
                    if (Information.HeadLensData.FailType == eFailType.Actuating2Fail)
                    {
                        strbTT.Append(",NG");
                    }
                    else
                    {
                        if (Information.HeadLensData.FailType == eFailType.None)
                            strbTT.Append(",OK");
                        else
                            strbTT.Append(",NG");
                    }

                }
                else
                    strbTT.Append(",NA");
                strbTT.Append("\r\n");

                strbTT.Append("LensHight_T/T,");
                strbTT.Append(Information.HeadLensData.TT_LensHeight.ToString("N3"));
                if (Information.HeadLensData.FailType == eFailType.LensHeightFail)
                    strbTT.Append(",NG");
                else
                    strbTT.Append(",OK");

                strbTT.Append("\r\n");
                strbTT.Append("Side_Angle_T/T,");
                strbTT.Append(Information.HeadLensData.TT_PlateAngle.ToString("N3"));

                if (cDEF.Work.Project.GlobalOption.UsePlateAngle)
                {
                    strbPD.Append(",");

                    if (Information.HeadLensData.FailType == eFailType.PlateAngleFail)
                    {
                        strbTT.Append(",NG");
                    }
                    else
                    {
                        if (Information.HeadLensData.FailType == eFailType.None)
                            strbTT.Append(",OK");
                        else
                            strbTT.Append(",NG");
                    }

                }
                else
                    strbTT.Append(",NA");

                strbTT.Append("\r\n");

                strbTT.Append("VCM_Loading_T/T,");
                strbTT.Append(Information.HeadLensData.TT_VCMPicker.ToString("N3"));
                strbTT.Append(",OK");
                strbTT.Append("\r\n");

                strbTT.Append("Unloading_T/T,");
                strbTT.Append(Information.HeadLensData.TT_UnloadPicker.ToString("N3"));
                strbTT.Append(",OK");
                strbTT.Append("\r\n");

                strbTT.Append("END_TEST_RESULT");
                strbTT.Append("\r\n");
                cDEF.MesLog.LogWrite_TactTime(strbTT.ToString());
            }
        }

    }
}

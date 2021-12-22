using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;

namespace XModule.Running
{

    public class RunLensHeightInformation : fpObject
    {

        #region 변수
        private double FValue;
        private double FResultHeight;      // 개별 측정된 자재 높이

        //public LensIndexStatus Status;   
        public Index_Data IndexData;
        #endregion

        #region Property
        public double Value
        {
            get { return FValue; }
            set
            {
                if(FValue != value)
                {
                    FValue = value;
                    Change();                  
                }
            }
        }
        public double ResultHeight
        {
            get { return FResultHeight; }
            set
            {
                if (FResultHeight != value)
                {
                    FResultHeight = value;
                    Change();
                }
            }
        }





        #endregion

        public RunLensHeightInformation() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunLensHeight.dat";
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

                switch (sArr[0])
                {
                    //case "FLedCount": FLedCount = Convert.ToInt32(sArr[1]); break;
                    //case "FPcbCount": FPcbCount = Convert.ToInt32(sArr[1]); break;
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


            //FileWrite.WriteLine($"FLedCount,{FLedCount}");
            //FileWrite.WriteLine($"FPcbCount,{FPcbCount}");

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
            IndexData.Clear();
            Unlock(Ignore);
        }
    }
    public enum TRunLensHeightMode
    {
        Stop,
        Measure,                  //  Height Measure
    };

    
    public class RunLensHeight : TfpRunningModule 
    {
        //Evnet
        public delegate void LensHeightChartHandler(int Index, double Value);
        public event LensHeightChartHandler OnChart;

        private RunLensHeightInformation FInformation;
        private TRunLensHeightMode FMode;

        public int FCalc;

        public RunLensHeight(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunLensHeightInformation();
        }
        

        #region **Property**
        public RunLensHeightInformation Information
        {
            get { return FInformation; }
        }

        public TRunLensHeightMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }
        #endregion //Property//

        private TRunLensHeightMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunLensHeightMode.Stop;
        }
        private void SetMode(TRunLensHeightMode Value)
        {
            if (Value == TRunLensHeightMode.Stop)
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
                case TRunLensHeightMode.Stop:
                    return "Stop";
                case TRunLensHeightMode.Measure:
                    return "Measure";

                default:
                    return "";
            }
        }
        protected override void ProcReal()
        {
            
        }
        protected override bool ProcInitialize()
        {
            switch (Step)
            {
                case 0:
                    cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] = false;
                    Step++;
                    break;
                case 1:
                    cDEF.TaskLogAppend(TaskLog.LensHeight, "[Initialize] Lens Height End", true);
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
                    cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] = false;
                    Step++;
                    break;

                case 1:
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.LensHeight, "[To-Run] Done.", true);
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
                    cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] = false;
                    Step++;
                    break;
                case 1:
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.LensHeight, "[To-Stop] Done.", true);
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
                case TRunLensHeightMode.Measure:
                    Running_Measure();
                    break;
                
            }
        }
        
        protected override void ProcMain()
        {
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if(Information.IndexData.Status == eLensIndexStatus.AssembleFinish)
                {
                    cDEF.Tact.LensHeight.Start();
                    Mode = TRunLensHeightMode.Measure;
                    cDEF.TaskLogAppend(TaskLog.LensHeight, $"[Lens Height Measure]Index:{Information.IndexData.Index} Lens Height Measure Start", true);
                    return;
                }
            }

            if(cDEF.Run.Mode == TRunMode.Manual_MeasureHeight)
            {
                cDEF.Tact.LensHeight.Start();
                Mode = TRunLensHeightMode.Measure;
                cDEF.TaskLogAppend(TaskLog.LensHeight, "[Lens Height Measure - Manual] Lens Height Measure Start", true);
                return;
            }
        }
        
        #region Running Func
        protected void Running_Measure()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] = false;
                    cDEF.TaskLogAppend(TaskLog.LensHeight, "[Lens Height Measure]  Lens Height (I/O) Off.", true);
                    Step++;
                    break;

                case 1:
                    if(!cDEF.Work.Project.GlobalOption.UseLensHeight)
                    {
                        cDEF.Tact.LensHeight.GetTact();
                        Information.IndexData.Status = eLensIndexStatus.LensHeightFinish;
                        Mode = TRunLensHeightMode.Stop;
                        return;
                    }
                    if(cDEF.Work.Project.GlobalOption.IndexCheck)
                    {
                        if(!cDEF.Run.Digital.Input[cDI.Lens_Height_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensHeight + 200;
                            //cDEF.Run.LogWarning(cLog.RunLensHeight + 200, "[Lens Height] Product is UnMatching.");
                            cDEF.Run.LogWarning(cLog.RunLensHeight + 200, "");
                            Mode = TRunLensHeightMode.Stop;
                            return;
                        }
                    }
                    cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] = true;
                    cDEF.TaskLogAppend(TaskLog.LensHeight, "[Lens Height Measure]  Lens Height (I/O) On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < cDEF.Work.LensHeight.MeasureTime)
                        break;
                    Information.Value = cDEF.Serials.Value;    // 실측 = mm 단위.
                    Information.IndexData.LensHeightData = Information.Value;
                    Information.ResultHeight = Math.Abs(cDEF.Work.LensHeight.DefaultHeight[Information.IndexData.Index] - Information.Value);  // 측정 된 자재 높이.
                    OnChart(Information.IndexData.Index, Information.Value);
                    cDEF.TaskLogAppend(TaskLog.LensHeight, $"[Lens Height Measure] Lens Height Measure Value : {Information.Value} mm, Product Heigt : {Information.ResultHeight} mm, JudgeValue {(cDEF.Work.Recipe.LensHeight - Information.ResultHeight).ToString("N3")} ", true);
                    cDEF.TaskLogAppend(TaskLog.LensHeightData, $"Index:{Information.IndexData.Index + 1},ResultHeight:{Information.Value},MeasureLensHeigt:{Information.ResultHeight},JudgeValue {(cDEF.Work.Recipe.LensHeight - Information.ResultHeight).ToString("N3")} ", true);
                    Step++;
                    break;

                case 3:
                    Step++;
                    break;

                case 4:
                    if (cDEF.Work.Project.GlobalOption.LensHeightSoftWareJudge)
                    {
                       
                        if (Information.Value * 1000 < cDEF.Work.LensHeight.MinOver || cDEF.Work.LensHeight.MaxOver < Information.Value * 1000)
                        {
                            Information.IndexData.Status = eLensIndexStatus.LensHeightFail;
                            Information.IndexData.FailType = eFailType.LensHeightFail;
                            cDEF.TaskLogAppend(TaskLog.LensHeight, $"[Lens Height Measure]1 Lens Height Measure Fail ", true);
                        }
                        else
                        {
                            //변경 후
                            if (Math.Abs(cDEF.Work.Recipe.LensHeight - Information.ResultHeight) < cDEF.Work.Recipe.LensHeightAllowMin
                                || cDEF.Work.Recipe.LensHeightAllowMax < Math.Abs(cDEF.Work.Recipe.LensHeight - Information.ResultHeight))
                            {
                                Information.IndexData.Status = eLensIndexStatus.LensHeightFail;
                                Information.IndexData.FailType = eFailType.LensHeightFail;
                                cDEF.TaskLogAppend(TaskLog.LensHeight, $"[Lens Height Measure] 2 Lens Height Measure Fail", true);
                            }
                            else
                            {
                                Information.IndexData.Status = eLensIndexStatus.LensHeightFinish;
                                cDEF.TaskLogAppend(TaskLog.LensHeight, $"[Lens Height Measure] Lens Height Measure Finish", true);
                            }
                        }

                    }
                    else
                    {
                        if(cDEF.Run.Digital.Input[cDI.Lens_Height_Unit_Go]) 
                        {
                            if (Math.Abs(cDEF.Work.Recipe.LensHeight - Information.ResultHeight) < cDEF.Work.Recipe.LensHeightAllowMin
                                || cDEF.Work.Recipe.LensHeightAllowMax < Math.Abs(cDEF.Work.Recipe.LensHeight - Information.ResultHeight))
                            {
                                Information.IndexData.Status = eLensIndexStatus.LensHeightFail;
                                Information.IndexData.FailType = eFailType.LensHeightFail;
                            }
                            else
                                Information.IndexData.Status = eLensIndexStatus.LensHeightFinish;
                        }
                        else
                        {
                            Information.IndexData.Status = eLensIndexStatus.LensHeightFail;
                            Information.IndexData.FailType = eFailType.LensHeightFail;
                            cDEF.TaskLogAppend(TaskLog.LensHeight, $"[Lens Height Measure] Lens Height Measure Fail", true);
                            
                        }
                    }
                    Step++;
                    break;

                case 5:
                    cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] = false;
                    cDEF.TaskLogAppend(TaskLog.LensHeight, $"[Lens Height Measure] Lens Height Measure (I/O) Off. {cDEF.Run.Digital.Output[cDO.Lens_Height_Measure]}", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;
                case 6:
                    if (Environment.TickCount - FCalc < cDEF.Work.LensHeight.MeasureTime)
                        break;

                    cDEF.Tact.LensHeight.GetTact();
                    Information.IndexData.TT_LensHeight = cDEF.Tact.LensHeight.CycleTime;
                   Step++;
                    break;
                case 7:
                    if (cDEF.Run.Mode == TRunMode.Manual_MeasureHeight)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunLensHeightMode.Stop;
              
                    cDEF.TaskLogAppend(TaskLog.LensHeight, $"[Lens Height Meausre] Lens Height Measure End - Cycle Time [{cDEF.Tact.LensHeight.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        #endregion
    }
}

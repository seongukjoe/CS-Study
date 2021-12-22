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

    public class RunCuring2Information : fpObject
    {

        #region 변수
        //public LensIndexStatus Status;
        public Index_Data IndexData;
        public bool FinishCuring2 = false;
        #endregion

        #region Property



        #endregion

        public RunCuring2Information() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunCuring2.dat";
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
    public enum TRunCuring2Mode
    {
        Stop,
        Curing,                  
    };

    
    public class RunCuring2 : TfpRunningModule 
    {
        //Evnet
        public delegate void LedStarterHandler(int led);
        public event LedStarterHandler OnLedStarter;

        public delegate void LedFinishHandler(int led);
        public event LedFinishHandler OnLedFinish;

        private RunCuring2Information FInformation;
        private TRunCuring2Mode FMode;

        public int FCalc;
        

        public RunCuring2(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunCuring2Information();
        }
        

        #region **Property**
        public RunCuring2Information Information
        {
            get { return FInformation; }
        }

        public TRunCuring2Mode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        //public TfpCylinderItem Contact
        //{
        //    get { return GetCylinders(0); }
        //}

        public TfpCylinderItem UVDown
        {
            get { return GetCylinders(0); }
        }

        #endregion //Property//

        private TRunCuring2Mode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunCuring2Mode.Stop;
        }
        private void SetMode(TRunCuring2Mode Value)
        {
            if (Value == TRunCuring2Mode.Stop)
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
                case TRunCuring2Mode.Stop:
                    return "Stop";
                case TRunCuring2Mode.Curing:
                    return "Curing";

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
                    UVDown.Backward();
                    Step++;
                    break;
                case 1:
                    Step++;
                    break;
                case 2:
                    Step++;
                    break;
                case 3:
                    Step++;
                    break;
                case 4:
                    Step++;
                    break;
                case 5:
                    Step++;
                    break;
                case 6:
                    cDEF.TaskLogAppend(TaskLog.Cure2, "[Initialize] End", true);
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
                    UVDown.Backward();
                    Step++;
                    break;

                case 1:
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.Cure2, "[To-Run] Done.", true);
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
                    UVDown.Backward();
                    Step++;
                    break;
                case 1:
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Cure2, "[To-Stop] Done.", true);
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
                case TRunCuring2Mode.Curing:
                    Running_Curing();
                    break;
                
            }
        }
        
        protected override void ProcMain()
        {
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                int IndexNum = (cDEF.Run.Index.Information.IndexNum + 4) % 12;

                if (cDEF.Work.Option.IndexSkip[IndexNum])
                    return;

                if (cDEF.Work.Project.GlobalOption.UseCureVisionFail)
                {
                    if (Information.IndexData.Status == eLensIndexStatus.Curing1Finish || 
                        Information.IndexData.Status == eLensIndexStatus.VisionInspectFail ||
                        (Information.IndexData.Status == eLensIndexStatus.Actuating1Fail && !Information.FinishCuring2))
                    {
                        cDEF.Tact.Curing2.Start();
                        Mode = TRunCuring2Mode.Curing;
                        cDEF.TaskLogAppend(TaskLog.Cure2, $"[Curing 2] Index : {Information.IndexData.Index + 1} Curing Start.", true);
                        return;
                    }
                }
                else
                {
                    if (Information.IndexData.Status == eLensIndexStatus.Curing1Finish ||
                        (Information.IndexData.Status == eLensIndexStatus.Actuating1Fail && !Information.FinishCuring2))
                    {
                        cDEF.Tact.Curing2.Start();
                        Mode = TRunCuring2Mode.Curing;
                        cDEF.TaskLogAppend(TaskLog.Cure2, $"[Curing 2] Index : {Information.IndexData.Index + 1} Curing Start.", true);
                        return;
                    }
                }
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Cure2UV)
            {
                cDEF.Tact.Curing2.Start();
                Mode = TRunCuring2Mode.Curing;
                cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] Manual_Cure2UV Start.", true);
                return;
            }
        }
        
        #region Running Func
        protected void Running_Curing()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    UVDown.Backward();
                    cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] Up-Down Cylinder Up.", true);
                    Step++;
                    break;
                case 1:
                    if (!cDEF.Work.Project.GlobalOption.UseCuring2)
                    {
                        cDEF.Tact.Curing2.GetTact();
                        Information.IndexData.Status = eLensIndexStatus.Curing2Finish;

                        Mode = TRunCuring2Mode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] Actuating End.", true);
                        return;
                    }
                    if (cDEF.Work.Project.GlobalOption.IndexCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.Curing_2])
                        {
                            //cDEF.Run.LogWarning(cLog.RunCuring2 + 201, "[CURING 2] Product is UnMatching.");
                            cDEF.Run.SetAlarmID = cLog.RunCuring2 + 201;
                            cDEF.Run.LogWarning(cLog.RunCuring2 + 201, "");
                            cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] Product is Unmatching.", true);
                            Mode = TRunCuring2Mode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;
                case 2:
                    UVDown.Forward();
                    cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] Up-Down Cylinder Dowm.", true);

                    Step++;
                    break;
                case 3:
                    if (cDEF.Run.Digital.Input[cDI.UV_2_Alarm_Monitor] 
                        || !cDEF.Run.Digital.Input[cDI.UV_2_Lamp_Ready_Monitor])
                    {
                        UVDown.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] Up_Down Cylinder Up.", true);


                        //cDEF.Run.LogWarning(cLog.RunCuring2 + 202, "[CURING 2] UV Lamp is not On");
                        cDEF.Run.SetAlarmID = cLog.RunCuring2 + 202;
                        cDEF.Run.LogWarning(cLog.RunCuring2 + 202, "");
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 2] UV Lamp is Not On.", true);
                        Mode = TRunCuring2Mode.Stop;
                        return;
                    }
                    Step++;
                    break;
                case 4:
                    cDEF.Run.Digital.Output[cDO.UV_2_Start] = true;
                    cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] UV 2 Shutter Open.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < cDEF.Work.Curing2.CuringTime)
                        break;
                    cDEF.Run.Digital.Output[cDO.UV_2_Start] = false;
                    cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] UV 2 Shutter Close.", true);
                    Step++;
                    break;

                case 6:
                    UVDown.Backward();
                    cDEF.TaskLogAppend(TaskLog.Cure2, "[Curing 2] Up-Down Cylinder Up.", true);
                    Step++;
                    break;

                case 7:
                    if (Information.IndexData.Status == eLensIndexStatus.Actuating1Fail)
                    {
                        Information.IndexData.Status = eLensIndexStatus.Actuating1Fail;
                        Information.FinishCuring2 = true;
                    }
                    else
                    {
                        if (cDEF.Work.Project.GlobalOption.UseCureVisionFail)
                        {
                            Information.IndexData.Status = eLensIndexStatus.VisionInspectFail;
                        }
                        else
                        {
                            Information.IndexData.Status = eLensIndexStatus.Curing2Finish;
                        }
                    }
                    cDEF.Tact.Curing2.GetTact();
                    Information.IndexData.TT_Curing2 = cDEF.Tact.Curing2.CycleTime;
                    Step++;
                    break;

                case 8:
           
                    if (cDEF.Run.Mode == TRunMode.Manual_Cure2UV)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunCuring2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Cure2, $"[Cure 2] Curing End - [{cDEF.Tact.Curing2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        
        
        #endregion
    }
}

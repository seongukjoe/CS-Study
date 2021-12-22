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

    public class RunVisionInspectInformation : fpObject
    {

        #region 변수
        public bool Result = false;

        //public LensIndexStatus Status;
        public Index_Data IndexData;
          
            
        #endregion

        #region Property
        #endregion

        public RunVisionInspectInformation() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunVisionInspect.dat";
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
    public enum TRunVisionInspectMode
    {
        Stop,
        Measure,                  //  Measure
    };

    
    public class RunVisionInspect : TfpRunningModule 
    {
        //Evnet
        public delegate void LedStarterHandler(int led);
        public event LedStarterHandler OnLedStarter;

        public delegate void LedFinishHandler(int led);
        public event LedFinishHandler OnLedFinish;

        private RunVisionInspectInformation FInformation;
        private TRunVisionInspectMode FMode;

        public int FCalc;

        public RunVisionInspect(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunVisionInspectInformation();
        }
        

        #region **Property**
        public RunVisionInspectInformation Information
        {
            get { return FInformation; }
        }

        public TRunVisionInspectMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        #endregion //Property//

        private TRunVisionInspectMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunVisionInspectMode.Stop;
        }
        private void SetMode(TRunVisionInspectMode Value)
        {
            if (Value == TRunVisionInspectMode.Stop)
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
                case TRunVisionInspectMode.Stop:
                    return "Stop";
                case TRunVisionInspectMode.Measure:
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
                    cDEF.TaskLogAppend(TaskLog.Inspect, "[Initialize] End", true);
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
                    Step++;
                    break;

                case 1:
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.Inspect, "[To-Run] Done.", true);
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
                    Step++;
                    break;
                case 1:
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Inspect, "[To-Stop] Done.", true);
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
                case TRunVisionInspectMode.Measure:
                    Running_Measure();
                    break;
                
            }
        }
        
        protected override void ProcMain()
        {
            if (cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if(Information.IndexData.Status == eLensIndexStatus.Bonder2Finish)
                {
                    cDEF.Tact.VisionInspect.Start();
                    Mode = TRunVisionInspectMode.Measure;
                    cDEF.TaskLogAppend(TaskLog.Inspect, $"[Vision]Index:{Information.IndexData.Index + 1} Inspect Start.", true);
                    return; 
                }
            }

            if(cDEF.Run.Mode == TRunMode.Manual_VisionInspect)
            {
                cDEF.Tact.VisionInspect.Start();
                Mode = TRunVisionInspectMode.Measure;
                cDEF.TaskLogAppend(TaskLog.Inspect, "[Vision - Manual] Inspect Start.", true);
                return;
            }
        }

        #region Running Func
        private int InspRetryCount = 0;
        private int ReadyRetryCount = 0;
        protected void Running_Measure()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if(!cDEF.Work.Project.GlobalOption.UseVision)
                    {
                        cDEF.Tact.VisionInspect.GetTact();
                        Information.IndexData.TT_VisionInspect = cDEF.Tact.VisionInspect.CycleTime;
                        Information.IndexData.Status = eLensIndexStatus.VisionInspectFinish;
                        Mode = TRunVisionInspectMode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Inspect, $"[Vision] UseVision (False) Inspect End - [{cDEF.Tact.VisionInspect.CycleTime.ToString("N3")}].", true);
                        return;
                    }
                    InspRetryCount = 0;
                    ReadyRetryCount = 0;
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 1:
                    if (Environment.TickCount - FCalc < cDEF.Work.VCMVision.VisionInspectGrabDelay)
                        break;
                    Information.Result = false;
                    cDEF.Visions.Sendmsg(eVision.V6_Ready);
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Inspect, "[Vision] Vision V6 Ready Send.", true);
                    Step++;
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV6_Ready.Status != CmmStatus.Ok)
                            break;
                        cDEF.TaskLogAppend(TaskLog.Inspect, "[Vision] Vision V6 Ready OK Recieve.", true);
                        Step++;
                    }
                    else
                    {
                        if (ReadyRetryCount < cDEF.Work.Recipe.VIsionInspectRetryCount)
                        {
                            ReadyRetryCount++;
                            Step = 1;
                            break;
                        }
                        cDEF.Run.SetAlarmID = cLog.RunInspectVision + 200;
                        //cDEF.Run.LogWarning(cLog.RunInspectVision + 200, "[VISION INSPECT] (Measure) V6 Ready Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunInspectVision + 200, "");
                        Mode = TRunVisionInspectMode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Inspect, "[Vision] Vision V6 Ready Recieve Time Out.", true);
                        return;
                    }
                    break;

                case 3:
                    cDEF.Visions.Sendmsg(eVision.V6_Complete);
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Inspect, "[Vision] Vision V6 Complete Send.", true);
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV6_Complete.Status != CmmStatus.Ok)
                            break;
                        Information.Result = cDEF.Visions.ackV6_Complete.exist;
                        cDEF.TaskLogAppend(TaskLog.Inspect, $"[Vision] Vision V6 Complete Recieve Result : [{Information.Result}].", true);
                        Step++;
                    }
                    else
                    {
                        if (InspRetryCount < cDEF.Work.Recipe.VIsionInspectRetryCount)
                        {
                            InspRetryCount++;
                            ReadyRetryCount = 0;
                            Step = 1;
                            break;
                        }
                        cDEF.Run.SetAlarmID = cLog.RunInspectVision + 201;
                        //cDEF.Run.LogWarning(cLog.RunInspectVision + 201, "[VISION INSPECT] (Measure) V6 Complete Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunInspectVision + 201, "");
                        Mode = TRunVisionInspectMode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Inspect, "[Vision] Vision V6 Complete Recieve Time Out.", true);
                        return;
                    }
                    break;

                case 5:
                    cDEF.Tact.VisionInspect.GetTact();
                    Information.IndexData.TT_VisionInspect = cDEF.Tact.VisionInspect.CycleTime;
                    if (Information.Result)
                    {
                        Information.IndexData.Status = eLensIndexStatus.VisionInspectFinish;
                        Information.IndexData.FailType = eFailType.None;
                    }
                    else
                    {
                        Information.IndexData.Status = eLensIndexStatus.VisionInspectFail;
                        Information.IndexData.FailType = eFailType.VisionInspectFail;
                    }
                    cDEF.TaskLogAppend(TaskLog.Inspect, $"[Vision] Status Change  Status : [{Information.IndexData.Status}]", true);
                    Step++;
                    break;

                case 6:
              
                    if (cDEF.Run.Mode == TRunMode.Manual_VisionInspect)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunVisionInspectMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Inspect, $"[Vision] Inspect End  - CycleTime : [{cDEF.Tact.VisionInspect.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        
        
        #endregion
   
    }
}

using System;
using XModule.Standard;
using System.Runtime.InteropServices;
using XModule.Unit;
using System.Linq;

namespace XModule.Running
{
    public enum TRunMode
    {
        Main_Stop,
        Main_Run,

        // VCM Loading
        Manual_VCMLoading,
        Manual_VCMUnloading,
        Manual_VCMPick,
        Manual_VCMPlace,
        Manual_Act3_Actuating,
        Manual_LensLoading,
        Manual_LensUnloading,
        Manual_LensPick,
        Manual_LensPlace,
        Manual_LensBottomCheck,
        Manual_UnloaderLoading,
        Manual_UnloaderUnloading,
        Manual_UnloadPick,
        Manual_UnloadStagePlace,
        Manual_UnloadNGTrayPlace,
        Manual_MeasureJigPlateAngle,
        Manual_MeasureHeight,
        Manual_PlateAnglePickPlace,
        Manual_PlateAngleMeasure,
        Manual_Cure1Actuating,
        Manual_Cure2Actuating,
        Manual_Cure1UV,
        Manual_Cure2UV,
        Manual_Clean,
        Manual_IndexStepMove,
        Manual_Bonder1CheckVision,
        Manual_Bonder1Jetting,
        Manual_Bonder2CheckVision,
        Manual_Bonder2Jetting,
        Manual_VisionInspect,
        Manual_TopVisionCheck,


        Manual_Bonder1_MoveSample,
        Manual_Bonder1_MoveCam,
        Manual_Bonder1_AutoCal,
        Manual_Bonder1_Touch,
        Manual_Bonder2_MoveSample,
        Manual_Bonder2_MoveCam,
        Manual_Bonder2_AutoCal,
        Manual_Bonder2_Touch,

        Manual_Jig_Index_Measure,
        Manual_Cure1ActuatingAnd1UV,  

        Manual_Bonder1_Clean,
        Manual_Bonder2_Clean,
        Manual_Bonder1_TipClean,
        Manual_Bonder2_TipClean,
        Manual_Bonder1_Dummy,
        Manual_Bonder2_Dummy,
        Manual_Bonder1_Ready,
        Manual_Bonder2_Ready,

        Manual_Bonder1_GapMeasure,
        Manual_Bonder2_GapMeasure,
        Manual_Bonder1_GapTouchAdjust,
        Manual_Bonder2_GapTouchAdjust,

    };
   
    public class TRun : fpRunning 
    {
        private RunVCMLoader FRunVCMLoader;
        private RunVCMPicker FRunVCMPicker;
        private RunLensLoader FRunLensLoader;
        private RunLensPicker FRunLensPicker;
        private RunJigPlateAngle FRunJigPlateAngle;
        private RunLensHeight FRunLensHeight;
        private RunBonder1 FRunBonder1;
        private RunBonder2 FRunBonder2;
        private RunVisionInspect FRunVisionInspect;
        private RunCuring1 FRunCuring1;
        private RunCuring2 FRunCuring2;
        private RunPlateAngle FRunPlateAngle;
        private RunUnloader FRunUnloader;
        private RunUnloadPicker FRunUnloadPicker;
        private RunCleanJig FRunCleanJig;
        private RunIndex FRunIndex;
        private RunVCMVision FRunVCMVision;
        private RunActuator3 FRunAct3;
        TRunMode FMode;
        MMTimer mRun;
        public eMESEqpStatus MESEQPStatus;
        public string MesStatusMsg;
        public int SetAlarmID;

        public bool LotEnd = false;
        public bool CheckBeforeInitialize = false;
        public TRun(uint Interval, int MessageCode) : base(Interval, MessageCode)
        {
            //모션축이 있는것 부터 생성 하시오...
            this.OnLogRunAlarm += TRun_OnLogRunAlarm;
            this.Switch.OnSwitchAlarm += Switch_OnSwitchAlarm;
            this.Switch.OnSwitchWarning += Switch_OnSwitchWarning;

            FRunVCMLoader = new RunVCMLoader(this, "RunVCMLoader", 1000);
            FRunVCMLoader.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunVCMLoader);

            FRunVCMPicker = new RunVCMPicker(this, "RunVCMPicker", 1500);
            FRunVCMPicker.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunVCMPicker);

            FRunLensLoader = new RunLensLoader(this, "RunLensLoader", 2000);
            FRunLensLoader.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunLensLoader);

            FRunLensPicker = new RunLensPicker(this, "RunLensPicker", 2500);
            FRunLensPicker.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunLensPicker);

            FRunJigPlateAngle = new RunJigPlateAngle(this, "RunJigPlateAngle", 3000);
            FRunJigPlateAngle.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunJigPlateAngle);

            FRunLensHeight = new RunLensHeight(this, "RunLensHeight", 3500);
            FRunLensHeight.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunLensHeight);

            FRunBonder1 = new RunBonder1(this, "RunBonder1", 4000);
            FRunBonder1.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunBonder1);

            FRunBonder2 = new RunBonder2(this, "RunBonder2", 4500);
            FRunBonder2.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunBonder2);

            FRunVisionInspect = new RunVisionInspect(this, "RunVisionInspect", 5000);
            FRunVisionInspect.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunVisionInspect);

            FRunCuring1 = new RunCuring1(this, "RunCuring1", 5500);
            FRunCuring1.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunCuring1);

            FRunCuring2 = new RunCuring2(this, "RunCuring2", 6000);
            FRunCuring2.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunCuring2);

            FRunPlateAngle = new RunPlateAngle(this, "RunPlateAngle", 6500);
            FRunPlateAngle.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunPlateAngle);

            FRunUnloader = new RunUnloader(this, "RunUnloader", 7000);
            FRunUnloader.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunUnloader);

            FRunUnloadPicker = new RunUnloadPicker(this, "RunUnloadPicker", 7500);
            FRunUnloadPicker.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunUnloadPicker);

            FRunCleanJig = new RunCleanJig(this, "RunCleanJig", 8000);
            FRunCleanJig.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunCleanJig);

            FRunIndex = new RunIndex(this, "RunIndex", 8500);
            FRunIndex.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunIndex);

            FRunVCMVision = new RunVCMVision(this, "RunVCMVision", 8500);
            FRunVCMVision.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunVCMVision);

            FRunAct3 = new RunActuator3(this, "RunActuator3", 9000);
            FRunAct3.OnLogAlarm += OnLogAlarm;
            RunningModule.Add(FRunAct3);

            MESEQPStatus = Unit.eMESEqpStatus.SETUP;
            MesStatusMsg = "START PROGRAM";

            mRun = new MMTimer();
            mRun.Start();
        }

        private void Switch_OnSwitchWarning(object sender, int Code)
        {
            //cDEF.Run.LogWarning(Code, "", true ,0);
            cDEF.Run.LogWarning(Code, "");
            cDEF.Run.SetAlarmID = Code;
            if (cDEF.Run.VCMLoader.Mode == TRunVCMLoaderMode.Loading ||
                cDEF.Run.VCMLoader.Mode == TRunVCMLoaderMode.Unloading)
            {
                if (Code == 907 || Code == 908)
                {
                    cDEF.Run.VCMLoader.TransferX.Stop();
                    cDEF.Run.VCMLoader.Mode = TRunVCMLoaderMode.Stop;
                }
            }

            if (cDEF.Run.LensLoader.Mode == TRunLensLoaderMode.Loading ||
                cDEF.Run.LensLoader.Mode == TRunLensLoaderMode.Unloading)
            {
                if (Code == 909 || Code == 910)
                {
                    cDEF.Run.LensLoader.TransferY.Stop();
                    cDEF.Run.LensLoader.Mode = TRunLensLoaderMode.Stop;
                }
            }

            if (cDEF.Run.Unloader.Mode == TRunUnloaderMode.Loading ||
                cDEF.Run.Unloader.Mode == TRunUnloaderMode.Unloading)
            {
                if (Code == 911 || Code == 912)
                {
                    cDEF.Run.Unloader.TransferX.Stop();
                    cDEF.Run.Unloader.Mode = TRunUnloaderMode.Stop;
                }
            }
            //20.09.16 JY 작업
            if (Code == 913)
            {
                cDEF.Run.VCMPicker.Information.HeadOverLoad = true;
            }
            if (Code == 914)
            {
                cDEF.Run.UnloadPicker.Information.HeadOverLoad = true;
            }
            //cDEF.Run.Bonder1.JettingIO = false;
            //cDEF.Run.Bonder2.JettingIO = false;

        }

        private void Switch_OnSwitchAlarm(object sender, int Code)
        {
            cDEF.Run.SetAlarmID = Code;
            CAXM.AxmContiWriteClear(0);
            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);

            CAXM.AxmContiWriteClear(1);
            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);

            cDEF.Run.Bonder1.JettingIO = false;
            cDEF.Run.Bonder2.JettingIO = false;
            cDEF.Run.LogAlarm(Code, "");
        }

        private void OnLogAlarm(object sender, int Code)
        {
            cDEF.Run.SetAlarmID = Code;
            CAXM.AxmContiWriteClear(0);
            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);

            CAXM.AxmContiWriteClear(1);
            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);

            cDEF.Run.Bonder1.JettingIO = false;
            cDEF.Run.Bonder2.JettingIO = false;
            cDEF.Run.LogAlarm(Code, "");
        }

        private void TRun_OnLogRunAlarm(object sender, int Code)
        {
            cDEF.Run.SetAlarmID = Code;
            CAXM.AxmContiWriteClear(0);
            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);

            CAXM.AxmContiWriteClear(1);
            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);

            cDEF.Run.Bonder1.JettingIO = false;
            cDEF.Run.Bonder2.JettingIO = false;
            cDEF.Run.LogAlarm(Code, "");
        }

        public void RunEnd()
        {
            mRun.Stop();
        }

        #region **Property**
        public RunVCMLoader VCMLoader
        {
            get { return FRunVCMLoader; }
        }
       
        public RunVCMPicker VCMPicker
        {
            get { return FRunVCMPicker; }
        }

        public RunLensLoader LensLoader
        {
            get { return FRunLensLoader; }
        }

        public RunLensPicker LensPicker
        {
            get { return FRunLensPicker; }
        }

        public RunJigPlateAngle JigPlateAngle
        {
            get { return FRunJigPlateAngle; }
        }

        public RunLensHeight LensHeight
        {
            get { return FRunLensHeight; }
        }

        public RunBonder1 Bonder1
        {
            get { return FRunBonder1; }
        }

        public RunBonder2 Bonder2
        {
            get { return FRunBonder2; }
        }

        public RunVisionInspect VisionInspect
        {
            get { return FRunVisionInspect; }
        }

        public RunCuring1 Curing1
        {
            get { return FRunCuring1; }
        }

        public RunCuring2 Curing2
        {
            get { return FRunCuring2; }
        }

        public RunPlateAngle PlateAngle
        {
            get { return FRunPlateAngle; }
        }

        public RunUnloader Unloader
        {
            get { return FRunUnloader; }
        }

        public RunUnloadPicker UnloadPicker
        {
            get { return FRunUnloadPicker; }
        }

        public RunCleanJig CleanJig
        {
            get { return FRunCleanJig; }
        }

        public RunIndex Index
        {
            get { return FRunIndex; }
        }
        public RunVCMVision VCMVision
        {
            get { return FRunVCMVision; }
        }
        public RunActuator3 Act3
        {
            get { return FRunAct3; }
        }

        public TRunMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }
        #endregion **Property**
            

        private TRunMode GetMode()
        {
	        if(EasyMode == TfpRunningEasyMode.femRun)
		        return FMode;
	        else
		        return TRunMode.Main_Stop;
        }
        private void SetMode(TRunMode Value)
        {
            if (Value == TRunMode.Main_Stop)
            {
                EasyMode = TfpRunningEasyMode.femStop;
            }
            else
            {
                EasyMode = TfpRunningEasyMode.femRun;
                FMode = Value;
            }
        }
        
        protected override void ProcRun()
        {
            
        }
        protected override void ProcBegin()
        {
        }
        public override void DetailModeChanged()
        {
	        FMode = TRunMode.Main_Run;
        }
#if !Notebook
        public override void DetailModeChanged(TfpRunningMode oldMode, TfpRunningMode NewMode)
        {
            try
            {

                switch (NewMode)
                {
                    case TfpRunningMode.frmNone:
                        if (cDEF.Work.Project.GlobalOption.UseMES)
                        {
                            if (cDEF.Run.SetAlarmID > -1 && cDEF.Run.MESEQPStatus == eMESEqpStatus.DOWN)
                            {
                                var aaitem = cDEF.AlarmDefineList.Where(se => se.Code == cDEF.Run.SetAlarmID).Select(s => s.Text).ToList();
                                string strMsg = "";
                                if (aaitem.Count >= 1)
                                {
                                    strMsg = aaitem[0].ToString();
                                }
                                cDEF.Mes.Send_AlarmStatus(eAlarmStatus.Clear, cDEF.Run.SetAlarmID, strMsg);

                                cDEF.Run.SetAlarmID = -1;
                            }

                            cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.SETUP;
                            cDEF.Run.MesStatusMsg = "NONE";
                            cDEF.Mes.Send_EquipStatus();
                        }
                        break;
                    case TfpRunningMode.frmToAlarm:

                        break;
                    case TfpRunningMode.frmAlarm:

                        if (cDEF.Work.Project.GlobalOption.UseMES)
                        {
                            cDEF.Run.MESEQPStatus = eMESEqpStatus.DOWN;
                            var aaitem = cDEF.AlarmDefineList.Where(se => se.Code == cDEF.Run.SetAlarmID).Select(s => s.Text).ToList();
                            string strMsg = "";
                            if (aaitem.Count >= 1)
                            {
                                strMsg = aaitem[0].ToString();
                            }

                            cDEF.Run.MesStatusMsg = strMsg;
                            cDEF.Mes.Send_EquipStatus();
                            cDEF.Mes.Send_AlarmStatus(eAlarmStatus.Set, cDEF.Run.SetAlarmID, strMsg);
                        }
                        break;
                    case TfpRunningMode.frmToInitialize:
                        break;
                    case TfpRunningMode.frmInitialize:
                        if (cDEF.Work.Project.GlobalOption.UseMES)
                        {
                            cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.SETUP;
                            cDEF.Run.MesStatusMsg = "INITIALIZE";
                            cDEF.Mes.Send_EquipStatus();
                        }
                        break;
                    case TfpRunningMode.frmToStop:

                        break;
                    case TfpRunningMode.frmStop:
                        if (cDEF.Work.Project.GlobalOption.UseMES)
                        {
                            if (cDEF.Run.SetAlarmID > -1 && cDEF.Run.MESEQPStatus == eMESEqpStatus.DOWN)
                            {
                                var aaitem = cDEF.AlarmDefineList.Where(se => se.Code == cDEF.Run.SetAlarmID).Select(s => s.Text).ToList();
                                string strMsg = "";
                                if (aaitem.Count >= 1)
                                {
                                    strMsg = aaitem[0].ToString();
                                }
                                cDEF.Mes.Send_AlarmStatus(eAlarmStatus.Clear, cDEF.Run.SetAlarmID, strMsg);

                                cDEF.Run.SetAlarmID = -1;

                                cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.IDLE;
                                cDEF.Run.MesStatusMsg = "AUTO";
                                cDEF.Mes.Send_EquipStatus();
                            }
                        }
                        break;
                    case TfpRunningMode.frmToWarning:
                        break;
                    case TfpRunningMode.frmWarning:
                        if (cDEF.Work.Project.GlobalOption.UseMES)
                        {
                            cDEF.Run.MESEQPStatus = eMESEqpStatus.DOWN;
                            var aaitem = cDEF.AlarmDefineList.Where(se => se.Code == cDEF.Run.SetAlarmID).Select(s => s.Text).ToList();
                            string strMsg = "";
                            if (aaitem.Count >= 1)
                            {
                                strMsg = aaitem[0].ToString();
                            }

                            cDEF.Run.MesStatusMsg = strMsg;
                            cDEF.Mes.Send_EquipStatus();
                            cDEF.Mes.Send_AlarmStatus(eAlarmStatus.Set, cDEF.Run.SetAlarmID, strMsg);
                        }
                        break;
                    case TfpRunningMode.frmToRun:
                        break;
                    case TfpRunningMode.frmRun:

                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {


            }
        }
#endif
        public override String ModeToString()
        {
	        switch(FMode)
	        {
		        case TRunMode.Main_Stop:
			        return "STOP";
		        case TRunMode.Main_Run:
			        return "RUN";

                default:
			        return base.ModeToString();
	        }
        }



        public class MMTimer
        {
            private TimerEventHandler mHandler;
            private int mTimerId;

            public delegate void EventHandler();

            //public event EventHandler CallBack;

            private void TimerHandler(int id, int msg, IntPtr user, int dw1, int dw2)
            {
                cDEF.Run.Execute();
            }

            public void Start()
            {
                timeBeginPeriod(1);
                mHandler = TimerHandler;
                mTimerId = timeSetEvent(1, 2, mHandler, IntPtr.Zero, 1);
            }

            public void Stop()
            {
                timeKillEvent(mTimerId);
                timeEndPeriod(1);
                mTimerId = 0;
            }

            [DllImport("winmm.dll")]
            private static extern int timeSetEvent(int delay, int resolution, TimerEventHandler handler, IntPtr user, int eventType);

            [DllImport("winmm.dll")]
            private static extern int timeKillEvent(int id);

            [DllImport("winmm.dll")]
            private static extern int timeBeginPeriod(int msec);

            [DllImport("winmm.dll")]
            private static extern int timeEndPeriod(int msec);

            private delegate void TimerEventHandler(int id, int msg, IntPtr user, int dw1, int dw2);
        }
    }
}

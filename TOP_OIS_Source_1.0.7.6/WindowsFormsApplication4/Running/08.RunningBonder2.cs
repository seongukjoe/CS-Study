using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;
using System.Collections.Generic;

namespace XModule.Running
{

    public class RunBonder2Information : fpObject
    {

        #region 변수
        //public LensIndexStatus Status;
        public Index_Data IndexData;
        private bool FCheckVisionFinish = false;
        private bool FGapMeasureFinish = false;
        private int FJettingCount = 0;

        // Vision Result
        public int VisionResultX = 0;
        public int VisionResultY = 0;

        public bool MoveSample = false;

        private int FCleanJetCount = 0;

        //private int FCleanPitchXCount = 0;
        private int FCleanPitchYCount = 0;
        private double FGapMeasureValue = 0.0;
        #endregion

        #region Property
        public bool CheckVisionFinish
        {
            get { return FCheckVisionFinish; }
            set
            {
                if (FCheckVisionFinish != value)
                {
                    FCheckVisionFinish = value;
                    Change();
                }
            }
        }
        public bool GapMeasureFinish
        {
            get { return FGapMeasureFinish; }
            set
            {
                if (FGapMeasureFinish != value)
                {
                    FGapMeasureFinish = value;
                    Change();
                }
            }
        }

        public int JettingCount
        {
            get { return FJettingCount; }
            set
            {
                if (FJettingCount != value)
                {
                    FJettingCount = value;
                    Change();
                }
            }
        }
        public double GapMeasureValue
        {
            get { return FGapMeasureValue; }
            set
            {
                if (FGapMeasureValue != value)
                {
                    FGapMeasureValue = value;
                    Change();
                }
            }
        }
        public int CleanJetCount
        {
            get { return FCleanJetCount; }
            set { FCleanJetCount = value; }
        }
        //public int CleanPitchXCount
        //{
        //    get { return FCleanPitchXCount; }
        //    set { FCleanPitchXCount = value; }
        //}
        public int CleanPitchYCount
        {
            get { return FCleanPitchYCount; }
            set { FCleanPitchYCount = value; }
        }
        #endregion

        public RunBonder2Information() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunBonder2.dat";
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
                    case "FJettingCount": FJettingCount = Convert.ToInt32(sArr[1]); break;
                    case "FCleanJetCount": FCleanJetCount = Convert.ToInt32(sArr[1]); break;
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


            FileWrite.WriteLine($"FJettingCount,{FJettingCount}");
            FileWrite.WriteLine($"FCleanJetCount,{FCleanJetCount}");

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
    public enum TRunBonder2Mode
    {
        Stop,
        CheckCamera,
        Jetting,
        PtnLineJetting,
        PtnArcJetting,
        Clean,

        MoveSample,
        MoveCam,
        Touch,

        AutoCal,

        TipCleaning,
        DummyDisChage,
        MoveReadyPos,

        GapMeasure,
        GapTouchAdjust,
    };


    public class RunBonder2 : TfpRunningModule
    {
        public delegate void GridDataDisplayHandler();
        public event GridDataDisplayHandler OnGrid_Display;

        private RunBonder2Information FInformation;
        private TRunBonder2Mode FMode;
        private JettingData WorkJetData;
        private JettingPatternLineData WorkJetPtnLineData;
        private JettingPatternArcData WorkJetPtnArcData;

        private bool JetfirstShot = false;
        public int FCalc;

        private bool ContiStart = false;

        public RunBonder2(fpRunning Owner, String Name, int MessageCode)
            : base(Owner, Name, MessageCode)
        {
            FInformation = new RunBonder2Information();
            WorkJetData = new JettingData();
            WorkJetPtnLineData = new JettingPatternLineData();
            WorkJetPtnArcData = new JettingPatternArcData();
        }


        #region **Property**
        public RunBonder2Information Information
        {
            get { return FInformation; }
        }

        public TRunBonder2Mode Mode
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
        public TfpCylinderItem TipClean
        {
            get { return GetCylinders(0); }
        }

        uint FJettingIO = 0;
        public bool JettingIO
        {
            get
            {
                CAXD.AxdoReadOutportBit(6, 1, ref FJettingIO);
                if (FJettingIO == 1)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    CAXD.AxdoWriteOutportBit(6, 1, 1);
                }
                else
                    CAXD.AxdoWriteOutportBit(6, 1, 0);
            }
        }

        public void Proc_JettingIO(bool bvalue)
        {
            if (bvalue)
                CAXD.AxdoWriteOutportBit(6, 1, 1);
            else
                CAXD.AxdoWriteOutportBit(6, 1, 0);
        }
        #endregion //Property//

        private TRunBonder2Mode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunBonder2Mode.Stop;
        }
        private void SetMode(TRunBonder2Mode Value)
        {
            if (Value == TRunBonder2Mode.Stop)
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
                case TRunBonder2Mode.Stop:
                    return "Stop";
                case TRunBonder2Mode.CheckCamera:
                    return "Check Camera";
                case TRunBonder2Mode.Jetting:
                    return "Jetting";

                case TRunBonder2Mode.PtnLineJetting:
                    return "Pattern Line Jetting";
                case TRunBonder2Mode.PtnArcJetting:
                    return "Pattern ARC Jetting";

                case TRunBonder2Mode.Clean:
                    return "Clean";

                case TRunBonder2Mode.MoveSample:
                    return "Move Sample";

                case TRunBonder2Mode.MoveCam:
                    return "Move Cam";

                case TRunBonder2Mode.TipCleaning:
                    return "TipCleaning";

                case TRunBonder2Mode.AutoCal:
                    return "AutoCal";

                case TRunBonder2Mode.GapMeasure:
                    return "Gap Measure";

                case TRunBonder2Mode.GapTouchAdjust:
                    return "Gap Touch Adjust";

                default:
                    return "";
            }
        }
        private int IdleTicCount = 0;
        protected override void ProcReal()
        {
            if(cDEF.Run.DetailMode == TfpRunningMode.frmAlarm)
            {
                if (JettingIO)
                    JettingIO = false;
            }
            if (this.MotionCount < 1)
                return;

            if (!cDEF.Work.Project.GlobalOption.UseBonder2)
                return;

            if ((cDEF.Run.DetailMode == TfpRunningMode.frmStop || cDEF.Run.DetailMode == TfpRunningMode.frmRun) && Mode == TRunBonder2Mode.Stop)
            {
                if (cDEF.Work.Project.GlobalOption.UseIdle2)
                {
                    if (!IsReady())
                        return;
                    if (!Is_Bonder2_IdleCenterX() || !Is_Bonder2_IdleCenterY())
                    {
                        if (Is_Bonder2_ReadyPositionZ())
                        {
                            Move_Bonder2_IdlePositionX();
                            Move_Bonder2_IdlePositionY();
                            return;
                        }
                        else
                        {
                            Move_Bonder2_ReadyPositionZ();
                            return;
                        }

                    }
                    else  // XY 맞으면.
                    {
                        if (Is_Bonder2_IdleCenterZ()) // Z 맞으면.
                        {
                            if (IdleTicCount < cDEF.Work.Bonder2.IdleTime)
                            {
                                if (IdleTicCount > cDEF.Work.Bonder1.JettingTime && JettingIO)           //cDEF.Run.Digital.Output[cDO.Bonder_Dispensor_2_Jetting])
                                    JettingIO = false;
                                    //cDEF.Run.Digital.Output[cDO.Bonder_Dispensor_2_Jetting] = false;
                                IdleTicCount++;
                                return;
                            }
                            else
                            {
                                JettingIO = true;
                                //cDEF.Run.Digital.Output[cDO.Bonder_Dispensor_2_Jetting] = true;
                                IdleTicCount = 0;
                                return;
                            }
                        }
                        else
                        {
                            Move_Bonder2_IdlePositionZ();
                            return;
                        }
                    }

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
                        break;
                    HeadX.Home();
                    Step++;
                    break;
                case 2:
                    if (!HeadX.HomeEnd)
                        break;
                    HeadY.Home();
                    Step++;
                    break;
                case 3:
                    if (!HeadY.HomeEnd)
                        break;
                    Step++;
                    break;
                case 4:
                    TipClean.Backward();
                    Step++;
                    break;
                case 5:
                    Move_Bonder2_ReadyPositionZ();            
                    Step++;
                    break;
                case 6:

                    Move_Bonder2_ReadyPositionX();
                    Step++;
                    break;
                case 7:
                    if (Information.IndexData.Status == eLensIndexStatus.Bonder1Finish
                     && Information.CheckVisionFinish)
                    {
                        Information.CheckVisionFinish = false;
                        JetfirstShot = false;
                        Information.VisionResultX = 0;
                        Information.VisionResultY = 0;
                    }
                    Step++;
                    break;
                case 8:
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Initialize] End", true);
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
                    Move_Bonder2_ReadyPositionZ();
                    JetfirstShot = false;

                    if (cDEF.Work.DispSensor.DispenserType == 2)
                    {
                        if (!cDEF.TJV_2.Connect)
                        {
                            if (!cDEF.TJV_2.Init(cDEF.Work.Recipe.TJV_IP_2))
                            {
                                cDEF.Run.LogWarning(cLog.RunBonder2 + 295, "[BONDER 2] (Check PJ Controller) Comm Check");
                                return false;

                            }
                        }
                    }
                    //20088 jy Idle 상태로 인한 프로그램 수정 (에러메세지 띄우는 방향으로 변경)
                    //if (cDEF.Work.Project.GlobalOption.UseIdle2)
                    //    cDEF.Work.Project.GlobalOption.UseIdle2 = false;
                    Step++;
                    break;

                case 1:
                    if (cDEF.Run.Mode != TRunMode.Manual_Bonder2_MoveCam)
                        Move_Bonder2_ReadyPositionX();

                    if(cDEF.Work.DispSensor.DispenserType == 0)
                        cDEF.Dispenser2.Send_ParamRead();
                    else if(cDEF.Work.DispSensor.DispenserType == 2)
                        cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_2, cDEF.Work.Recipe.falltime_2, cDEF.Work.Recipe.openvolt_2, cDEF.Work.Recipe.opentime_2, cDEF.Work.Recipe.risetime_2, cDEF.Work.Recipe.pixelcount_2, 0);

                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < 500)
                        break;
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                    {
                        PJS100.EWorkMode jetmode = PJS100.EWorkMode.None;
                        if (cDEF.Work.Project.GlobalOption.JettingMode2 == 0)
                        {
                            //포인트
                            jetmode = PJS100.EWorkMode.Point;
                        }
                        else
                        {
                            jetmode = PJS100.EWorkMode.Line;
                        }
#if !Notebook
                        if (jetmode != cDEF.Dispenser2.WorkModeValue)
                        {

                            cDEF.Run.LogWarning(cLog.RunBonder2 + 204, "[BONDER 2] (Check PJ Controller) WorkMode Notmatch");
                            return false;
                        }
#endif
                    }
                    else if(cDEF.Work.DispSensor.DispenserType == 2)
                        cDEF.TJV_2.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_2);

                    if (cDEF.Run.Mode == TRunMode.Main_Run)
                        Information.CleanJetCount = cDEF.Work.Recipe.DummyPeriodCount2;
                    Step++;
                    break;
                case 3:
                    if (cDEF.Work.DispSensor.DispenserType == 2)
                        cDEF.TJV_2.PDDStartSpitting(cDEF.Work.Recipe.Hz_2, cDEF.Work.Recipe.nDrop_2);
                    Step++;
                    break;         
                case 4:
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[To-Run] Done.", true);
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
                    if (!Information.MoveSample)
                    {
                        Move_Bonder2_ReadyPositionZ();
                    }
                    else
                        Information.MoveSample = false;
                    Step++;
                    break;
                case 1:
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[To-Stop] Done.", true);
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
                case TRunBonder2Mode.CheckCamera:
                    Running_CheckCamera();
                    break;

                case TRunBonder2Mode.Jetting:
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                        Running_Jetting();
                    else if (cDEF.Work.DispSensor.DispenserType == 1)
                        Running_Jetting_Air();
                    else if (cDEF.Work.DispSensor.DispenserType == 2)
                        Running_Jetting_TJV();
                    break;

                case TRunBonder2Mode.PtnLineJetting:
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                        Running_PtnLineJetting();
                    else if (cDEF.Work.DispSensor.DispenserType == 1)
                        Running_PtnLineJetting_Air();
                    else if (cDEF.Work.DispSensor.DispenserType == 2)
                        Running_PtnLineJetting_TJV();
                    break;
                case TRunBonder2Mode.PtnArcJetting:
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                        Running_PtnArcJetting();
                    else if (cDEF.Work.DispSensor.DispenserType == 1)
                        Running_PtnArcJetting_Air();
                    else if (cDEF.Work.DispSensor.DispenserType == 2)
                        Running_PtnArcJetting_TJV();
                    break;
                case TRunBonder2Mode.Clean:
                    Running_Cleaning();
                    break;

                case TRunBonder2Mode.MoveSample:
                    Running_Move_Sample();
                    break;

                case TRunBonder2Mode.MoveCam:
                    Running_Move_Cam();
                    break;

                case TRunBonder2Mode.Touch:
                    Running_Touch();
                    break;
                case TRunBonder2Mode.TipCleaning:
                    Running_TipCleaning();
                    break;
                case TRunBonder2Mode.DummyDisChage:
                    Running_Dummy();
                    break;
                case TRunBonder2Mode.MoveReadyPos:
                    Running_ReadyPos();
                    break;

                case TRunBonder2Mode.AutoCal:
                    Running_AutoCal();
                    break;

                case TRunBonder2Mode.GapMeasure:
                    Running_Gap_Measure();
                    break;

                case TRunBonder2Mode.GapTouchAdjust:
                    Running_Gap_TouchAdjust();
                    break;
            }
        }

        protected override void ProcMain()
        {
            if (cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if (cDEF.Work.Project.GlobalOption.UseBonder2)
                {
                    // Jotting Count Over
                    if (Information.JettingCount >= cDEF.Work.Bonder2.JettingCount)
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 206;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 206, "");
                        //cDEF.Run.LogWarning(cLog.RunBonder2 + 206, "Bonder2 Jetting Count Over.");
                        return;
                    }

                    if (cDEF.Work.Project.GlobalOption.UseTipClean2 || cDEF.Work.Project.GlobalOption.UseDummy2)
                    {
                        if (Information.CleanJetCount >= cDEF.Work.Recipe.DummyPeriodCount2)
                        {
                            if (cDEF.Work.TeachBonder1.AvoidPositionX
                                        < cDEF.Run.Bonder1.HeadX.ActualPosition)
                                return;

                            Mode = TRunBonder2Mode.TipCleaning;
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Tip Cleaning Start", true);
                            return;
                        }
                    }
                }

                if (Information.IndexData.Status == eLensIndexStatus.Bonder1Finish
                    && !Information.CheckVisionFinish)
                {

                    cDEF.Tact.Bonder2.Start();
                    cDEF.Tact.Bonder2Cam.Start();
                    Mode = TRunBonder2Mode.CheckCamera;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Check Vision Start", true);
                    return;
                }

                if (cDEF.Work.Project.GlobalOption.UseGap && cDEF.Work.DispSensor.DispenserType != 0)
                {
                    // Gap Measure
                    if (Information.IndexData.Status == eLensIndexStatus.Bonder1Finish
                        && Information.CheckVisionFinish
                        && !Information.GapMeasureFinish)
                    {

                        if (cDEF.Work.TeachBonder1.AvoidPositionX
                                < cDEF.Run.Bonder1.HeadX.ActualPosition)
                            return;

                        cDEF.Tact.Bonder2GapMesure.Start();
                        Mode = TRunBonder2Mode.GapMeasure;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Gap Measure Start", true);
                        return;

                    }
                }
                else
                {
                    Information.GapMeasureFinish = true;
                }

                if (Information.IndexData.Status == eLensIndexStatus.Bonder1Finish
                    && Information.CheckVisionFinish
                    && Information.GapMeasureFinish)
                {
                    if (cDEF.Work.Project.GlobalOption.JettingMode2 == 0)    // Point
                    {
                        if (!IsFinish())
                        {
                            if (cDEF.Work.TeachBonder1.AvoidPositionX 
                                < cDEF.Run.Bonder1.HeadX.ActualPosition)
                                return;
                            
                            Mode = TRunBonder2Mode.Jetting;
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Jetting Start", true);
                            return;
                        }
                        
                    }
                    else if (cDEF.Work.Project.GlobalOption.JettingMode2 == 1)    // Line
                    {

                        if (!IsPatternFinish())
                        {
                            if (cDEF.Work.TeachBonder1.AvoidPositionX
                                < cDEF.Run.Bonder1.HeadX.ActualPosition)
                                return;

                            Mode = TRunBonder2Mode.PtnLineJetting;
                            cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Index:{Information.IndexData.Index + 1} Pattern Line Jetting Start", true);
                            
                            return;
                        }
                        else if (Is_Bonder2_ReadyPositionZ() && !Is_Bonder2_CamCenterX())
                        {
                            Move_Bonder2_CamCenterX();
                        }
                        else
                        {
                            if (!Is_Bonder2_ReadyPositionZ())
                            {
                                Move_Bonder2_ReadyPositionZ();
                                return;
                            }
                            else
                            {
                                cDEF.Tact.Bonder2.GetTact();
                                Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                                cDEF.Work.Bonder2Pattern.JettingPatternInit(); //젯팅 2번 동작 방지
                                Information.JettingCount++;
                                Information.CleanJetCount++;
                                Information.CheckVisionFinish = false;
                                if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                                    cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                                return;
                            }

                        }
                    }
                    else if (cDEF.Work.Project.GlobalOption.JettingMode2 == 2)    // Arc
                    {

                        if (!IsPatternArcFinish())
                        {
                            if (cDEF.Work.TeachBonder1.AvoidPositionX
                                < cDEF.Run.Bonder1.HeadX.ActualPosition)
                                return;

                            Mode = TRunBonder2Mode.PtnArcJetting;
                            cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Index:{Information.IndexData.Index + 1} Pattern ARC Jetting Start", true);

                            return;
                        }
                        else if (Is_Bonder2_ReadyPositionZ() && !Is_Bonder2_CamCenterX())
                        {

                            Move_Bonder2_CamCenterX();
                        }
                        else
                        {
                            if (!Is_Bonder2_ReadyPositionZ())
                            {
                                Move_Bonder2_ReadyPositionZ();
                                return;
                            }
                            else
                            {
                                cDEF.Tact.Bonder2.GetTact();
                                Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                                cDEF.Work.Bonder2ARC.JettingPatternInit(); //젯팅 2번 동작 방지
                                Information.JettingCount++;
                                Information.CleanJetCount++;
                                Information.CheckVisionFinish = false;
                                if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                                    cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                                return;
                            }

                        }
                    }
                }
            }


            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2CheckVision)
            {
                cDEF.Tact.Bonder2.Start();
                Mode = TRunBonder2Mode.CheckCamera;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Check Vision Start.", true);
                return;
            }

            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
            {
                if (cDEF.Work.Project.GlobalOption.JettingMode2 == 0)    // Point
                {
                    if (!IsFinish())
                    {
                        if (cDEF.Work.TeachBonder1.AvoidPositionX
                                < cDEF.Run.Bonder1.HeadX.ActualPosition)
                            return;

                        cDEF.Tact.Bonder2.Start();
                        Mode = TRunBonder2Mode.Jetting;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Jetting Start.", true);
                        return;
                    }
                    else
                    {
                        if (!Is_Bonder2_ReadyPositionZ())
                        {
                            Move_Bonder2_ReadyPositionZ();
                            return;
                        }
                        else if (Is_Bonder2_ReadyPositionZ() && !Is_Bonder2_CamCenterX())
                        {
                            Move_Bonder2_CamCenterX();
                        }
                        else
                        {
                            Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                            Information.CheckVisionFinish = false;
                            cDEF.Work.Bonder2Point.JettingDataInit(); //젯팅 2번 동작 방지
                            Information.JettingCount++;
                            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                            return;
                        }

                    }
                }
                else if (cDEF.Work.Project.GlobalOption.JettingMode2 == 1)    // Line
                {

                    if (!IsPatternFinish())
                    {
                        if (cDEF.Work.TeachBonder1.AvoidPositionX
                                < cDEF.Run.Bonder1.HeadX.ActualPosition)
                            return;

                        Mode = TRunBonder2Mode.PtnLineJetting;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Pattern Line Jetting Start", true);
                        return;
                    }
                    else
                    {
                        if (!Is_Bonder2_ReadyPositionZ() )
                        {
                            Move_Bonder2_ReadyPositionZ();
                          
                            return;
                        }
                        else if (Is_Bonder2_ReadyPositionZ() && !Is_Bonder2_CamCenterX())
                        {
                            Move_Bonder2_CamCenterX();
                        }
                        else
                        {
                            cDEF.Tact.Bonder2.GetTact();
                            Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;

                            cDEF.Work.Bonder2Pattern.JettingPatternInit(); //젯팅 2번 동작 방지
                            Information.JettingCount++;
                            Information.CheckVisionFinish = false;
                            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                            return;
                        }

                    }
                }
                else if (cDEF.Work.Project.GlobalOption.JettingMode2 == 2)    // Line
                {

                    if (!IsPatternArcFinish())
                    {
                        if (cDEF.Work.TeachBonder1.AvoidPositionX
                                < cDEF.Run.Bonder1.HeadX.ActualPosition)
                            return;

                        Mode = TRunBonder2Mode.PtnArcJetting;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Pattern ARC Jetting Start", true);
                        return;
                    }
                    else
                    {
                        if (!Is_Bonder2_ReadyPositionZ())
                        {
                            Move_Bonder2_ReadyPositionZ();

                            return;
                        }
                        else if (Is_Bonder2_ReadyPositionZ() && !Is_Bonder2_CamCenterX())
                        {
                            Move_Bonder2_CamCenterX();
                        }
                        else
                        {
                            cDEF.Tact.Bonder2.GetTact();
                            Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;

                            cDEF.Work.Bonder2ARC.JettingPatternInit(); //젯팅 2번 동작 방지
                            Information.JettingCount++;
                            Information.CheckVisionFinish = false;
                            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                            return;
                        }

                    }
                }
            }

            // Manual Move
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_MoveSample)
            {
                cDEF.Tact.Bonder2.Start();
                Mode = TRunBonder2Mode.MoveSample;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Move Sample Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_MoveCam)
            {
                Mode = TRunBonder2Mode.MoveCam;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Move Camera Start.", true);
                return;
            }
            if(cDEF.Run.Mode == TRunMode.Manual_Bonder2_AutoCal)
            {
                Mode = TRunBonder2Mode.AutoCal;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Auto Calbration Start.", true);
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_Touch)
            {
                Mode = TRunBonder2Mode.Touch;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Touch Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_Clean)
            {
                Mode = TRunBonder2Mode.Clean;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual]  Clean Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_TipClean)
            {
                Mode = TRunBonder2Mode.TipCleaning;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Tip Clean Start.", true);
                return;
            }

            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_Dummy)
            {
                Mode = TRunBonder2Mode.DummyDisChage;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Dummy Discharge Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_Ready)
            {
                Mode = TRunBonder2Mode.MoveReadyPos;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Move Ready Pos Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_GapMeasure)
            {
                Mode = TRunBonder2Mode.GapMeasure;
                cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2 - Manual] Move Gap Measure Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_GapTouchAdjust)
            {
                Mode = TRunBonder2Mode.GapTouchAdjust;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2 - Manual] Move Gap Touch Adjust Start.", true);
                return;
            }
        }
        public bool IsFinish()
        {
            foreach (JettingData jd in cDEF.Work.Bonder2Point.JetData)
            {
                if (jd.Enable)
                {
                    if (jd.Finish == false)
                    {
                        WorkJetData = jd;
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsPatternFinish()
        {
            foreach (JettingPatternLineData jd in cDEF.Work.Bonder2Pattern.JetPatternLineData)
            {

                if (jd.Finish == false)
                {
                    WorkJetPtnLineData = jd;
                    return false;
                }
            }
            return true;
        }

        public bool IsPatternArcFinish()
        {
             foreach (JettingPatternArcData jd in cDEF.Work.Bonder2ARC.JetPatternArcData)
            {

                if (jd.Finish == false)
                {
                    WorkJetPtnArcData = jd;
                    return false;
                }
            }
            return true;
        }

        #region Running Func
        private int InspRetryCount = 0;
        private int ReadyRetryCount = 0;

        protected void Running_CheckCamera()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    InspRetryCount = 0;
                    ReadyRetryCount = 0;
                    Step++;
                    break;

                case 1:
                    if (!cDEF.Work.Project.GlobalOption.UseBonder2)
                    {
                        cDEF.Tact.Bonder2.GetTact();
                        Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                        if (cDEF.Run.Mode == TRunMode.Manual_Bonder2CheckVision)
                            cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                        Mode = TRunBonder2Mode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Check Vision End - [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                        return;
                    }

                    Move_Bonder2_CamCenterX();
                    Move_Bonder2_CamCenterY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                    Step++;
                    break;

                case 2:
                    Move_Bonder2_CamPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 3:
                    Information.VisionResultX = 0;
                    Information.VisionResultY = 0;
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder2.Bonder2GrabDelay)
                        break;
                    if (!cDEF.Work.Project.GlobalOption.VisionCheck)
                    {
                        Step = 7;
                        return;
                    }
                    cDEF.Visions.Sendmsg(eVision.V5_Ready);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Send Vision V4 Ready (Check Vision).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV5_Ready.Status != CmmStatus.Ok)
                            break;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Receive Vision V5 Ready OK (Check Vision).", true);
                        Step++;
                    }
                    else
                    {
                        if (ReadyRetryCount < cDEF.Work.Recipe.Bonder2RetryCount)
                        {
                            ReadyRetryCount++;
                            Step = 2;
                            break;
                        }
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 200;
                        //cDEF.Run.LogWarning(cLog.RunBonder2 + 200, "[Bonder 2] (Check Camera) V5 Ready Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 200, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] (Check Camera) V5 Ready Vision Time Out.", true);
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 5:
                    cDEF.Visions.Sendmsg(eVision.V5_Complete);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Send Vision V5 Complete (Check Vision).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 6:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV5_Complete.Status != CmmStatus.Ok)
                            break;

                        if (cDEF.Visions.ackV5_Complete.exist)
                        {
                            Information.VisionResultX = (int)(cDEF.Visions.ackV5_Complete.x);
                            Information.VisionResultY = (int)(cDEF.Visions.ackV5_Complete.y);
                            cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Receive Vision V5 Complete OK (Check Vision) X : {Information.VisionResultX} Y : {Information.VisionResultY}.", true);
                            Step++;
                        }
                        else
                        {
                            if (InspRetryCount < cDEF.Work.Recipe.Bonder2RetryCount)
                            {
                                InspRetryCount++;
                                ReadyRetryCount = 0;
                                Step = 2;
                                break;
                            }
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 201;
                            //cDEF.Run.LogWarning(cLog.RunBonder2 + 201, "[Bonder 2] (Check Camera) V5 Complete Vision TimeOut");
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 201, "");
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] (Check Camera) V5 Compelte Vision Time Out.", true);
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }
                    }
                    else
                    {
                        if (InspRetryCount < cDEF.Work.Recipe.Bonder2RetryCount)
                        {
                            InspRetryCount++;
                            ReadyRetryCount = 0;
                            Step = 2;
                            break;
                        }
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 201;
                        //cDEF.Run.LogWarning(cLog.RunBonder2 + 201, "[Bonder 2] (Check Camera) V5 Complete Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 201, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] (Check Camera) V5 Compelte Vision Time Out.", true);
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 7:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Bonder 2 Ready Position Z.", true);
                    Step++;
                    break;

                case 8:
                    JetfirstShot = false;
                    Information.CheckVisionFinish = true;
                    Information.GapMeasureFinish = false;
                    cDEF.Work.Bonder2Point.JettingDataInit();

                    cDEF.Tact.Bonder2Cam.GetTact();
                    Information.IndexData.TT_Bonder2Cam = cDEF.Tact.Bonder2Cam.CycleTime;
                    Step++;
                    break;

                case 9:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2CheckVision)
                    {
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                        Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    }
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Check Vision End - Unit Tact Time : [{cDEF.Tact.Bonder2Cam.CycleTime.ToString("N3")}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Jetting()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    Move_Bonder2_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;

                case 2:
                    // Get Working 
                    if (!IsFinish())
                    {
                        Step++;
                    }
                    else
                    {
                        // Finish
                        // Step Jump
                        Step = 13;
                    }
                    break;

                case 3:
                    if (cDEF.Work.Project.GlobalOption.UsePlusenum2)
                    {
                        if (cDEF.Dispenser2.PluseNumValue != WorkJetData.PluseNum)
                        {
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue,
                                cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue,
                                (int)cDEF.Dispenser2.PCTValue, WorkJetData.PluseNum,
                                (int)cDEF.Dispenser2.WorkModeValue, (int)cDEF.Dispenser2.VoltageValue);

                            FCalc = Environment.TickCount;
                        }
                    }
                    Step++;
                    break;

                case 4:
                    if (cDEF.Work.Project.GlobalOption.UsePlusenum2)
                    {
                        if (Environment.TickCount - FCalc < 100)
                            break;

                        // 피에조 변경 데이터 읽기
                        cDEF.Dispenser2.Send_ParamRead();
                        FCalc = Environment.TickCount;
                    }
                    Step++;
                    break;

                case 5:
                    if (cDEF.Work.Project.GlobalOption.UsePlusenum2)
                    {
                        if (Environment.TickCount - FCalc < 1000)
                        {
                            if (cDEF.Dispenser2.PluseNumValue != WorkJetData.PluseNum)
                                break;
                            cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Jetting Pluse Num Value {WorkJetData.PluseNum}", true);
                        }
                        else
                        {
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue,
                                cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue,
                                (int)cDEF.Dispenser2.PCTValue, WorkJetData.PluseNum,
                                (int)cDEF.Dispenser2.WorkModeValue, (int)cDEF.Dispenser2.VoltageValue);

                            cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Send Jetting Pluse Num Value {WorkJetData.PluseNum}", true);
                        }
                    }
                    Step++;
                    break;

                case 6:
                    Move_Bonder2_JetUpPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position Z.", true);
                    Step++;
                    break;

                case 7:
                    Move_Bonder2_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_JetPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position Z.", true);
                    Step++;
                    break;

                case 9:
                    JettingIO = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 10:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder2.JettingTime)
                        break;
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 11:
                    if (Environment.TickCount - FCalc < WorkJetData.Delay)
                        break;

                    // Move_Bonder2_JetUpPositionZ(WorkJetData);
                    //cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jet Up Position Z.", true);
                    Step++;
                    break;

                case 12:
                    WorkJetData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting Radius: {WorkJetData.Angle} Finish:{WorkJetData.Finish}", true);
                    Step = 2;  // 여기서 loop
                    break;


                case 13:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 14:
                    cDEF.Tact.Bonder2.GetTact();

                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2Point.JettingDataInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 15:
                    Move_Bonder2_CamCenterX();
                    Move_Bonder2_CamCenterY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                    Step++;
                    break;

                case 16:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;


                case 17:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_PtnLineJetting()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    CAXM.AxmContiWriteClear(1);
                    Step++;
                    break;

                case 2:
                    if(!IsPatternFinish())
                    {
                        Move_Bonder2_PtnLine_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;

                case 3:
                    Move_Bonder2_PtnLine_FristJetPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head First Jet Position Z.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder2_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = 
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(1);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        //ContiStart = Move_Bonder2_PtnLine_JettingPositionXYZ(Information.VisionResultX, Information.VisionResultY);
                        ContiStart = Move_Bonder2_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnLineData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting  Finish:{WorkJetPtnLineData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder2.GetTact();

                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2Pattern.JettingPatternInit();
                    Step++;
                    break;

                case 12:
                    Move_Bonder2_CamCenterX();
                    Move_Bonder2_CamCenterY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                    Step++;
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        protected void Running_PtnArcJetting()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    CAXM.AxmContiWriteClear(1);
                    Step++;
                    break;

                case 2:
                    if (!IsPatternArcFinish())
                    {
                        Move_Bonder2_PtnArc_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;

                case 3:
                    Move_Bonder2_PtnArc_FristJetPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head First Jet Position Z.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder2_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = 
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(1);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        ContiStart = Move_Bonder2_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnArcData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting  Finish:{WorkJetPtnArcData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder2.GetTact();

                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2ARC.JettingPatternInit();
                    Step++;
                    break;

                case 12:
                    Move_Bonder2_CamCenterX();
                    Move_Bonder2_CamCenterY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                    Step++;
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Jetting_Air()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_2_Alarm1])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 215;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 215, "Air Dispenser Alarm Type 1");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }

                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_2_Alarm2])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 216;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 216, "Air Dispenser Alarm Type 2");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    Move_Bonder2_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;

                case 2:
                    // Get Working 
                    if (!IsFinish())
                    {
                        Step++;
                    }
                    else
                    {
                        // Finish
                        // Step Jump
                        Step = 13;
                    }
                    break;

                case 3:
                    // Air Tiem Set
                    cDEF.DispenserEcm2.PressTime = WorkJetData.DpTime;
                    cDEF.DispenserEcm2.CMDMode = clsSuperEcm3.ECMDMode.SetValue;
                    cDEF.DispenserEcm2.SetValueStart();
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (!cDEF.DispenserEcm2.SetValueFinish)
                            break;
                        if (cDEF.DispenserEcm2.CommError)
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 2 Comm Err");
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 211;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 211, "Dispenser 2 Comm Time Out");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 5:
                    
                    Step++;
                    break;

                case 6:
                    Move_Bonder2_JetUpPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position Z.", true);
                    Step++;
                    break;

                case 7:
                    Move_Bonder2_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_JetPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position Z.", true);
                    Step++;
                    break;

                case 9:
                    JettingIO = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 10:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder2.JettingTime)
                        break;
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 11:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Bonder2ECM_DSO])
                            break;
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 213;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 213, "[Bonder 2] DSO Signal is not Off - Time Out");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < WorkJetData.Delay)
                        break;
                    WorkJetData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting Radius: {WorkJetData.Angle} Finish:{WorkJetData.Finish}", true);
                    Step = 2;  // 여기서 loop
                    break;


                case 13:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 14:
                    cDEF.Tact.Bonder2.GetTact();

                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2Point.JettingDataInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 15:
                    Move_Bonder2_CamCenterX();
                    Move_Bonder2_CamCenterY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                    Step++;
                    break;

                case 16:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;


                case 17:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_PtnLineJetting_Air()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_2_Alarm1])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 215;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 215, "Air Dispenser Alarm Type 1");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }

                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_2_Alarm2])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 216;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 216, "Air Dispenser Alarm Type 2");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    CAXM.AxmContiWriteClear(1);
                    Step++;
                    break;

                case 2:
                    if (!IsPatternFinish())
                    {
                        Move_Bonder2_PtnLine_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;

                case 3:
                    Move_Bonder2_PtnLine_FristJetPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head First Jet Position Z.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder2_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = 
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(1);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        //ContiStart = Move_Bonder2_PtnLine_JettingPositionXYZ(Information.VisionResultX, Information.VisionResultY);
                        ContiStart = Move_Bonder2_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnLineData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting  Finish:{WorkJetPtnLineData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder2.GetTact();

                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2Pattern.JettingPatternInit();
                    Step++;
                    break;

                case 12:
                    Move_Bonder2_CamCenterX();
                    Move_Bonder2_CamCenterY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                    Step++;
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_PtnArcJetting_Air()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_2_Alarm1])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 215;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 215, "Air Dispenser Alarm Type 1");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }

                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_2_Alarm2])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 216;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 216, "Air Dispenser Alarm Type 2");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    CAXM.AxmContiWriteClear(1);
                    Step++;
                    break;

                case 2:
                    if (!IsPatternArcFinish())
                    {
                        Move_Bonder2_PtnArc_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;

                case 3:
                    Move_Bonder2_PtnArc_FristJetPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head First Jet Position Z.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder2_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = 
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(1);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        ContiStart = Move_Bonder2_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnArcData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting  Finish:{WorkJetPtnArcData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder2.GetTact();

                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2ARC.JettingPatternInit();
                    Step++;
                    break;

                case 12:
                    Move_Bonder2_CamCenterX();
                    Move_Bonder2_CamCenterY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                    Step++;
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Jetting_TJV()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    cDEF.TJV_2.PDDStopSpitting();
                    Move_Bonder2_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;


                case 2:
                    // Get Working 
                    if (!IsFinish())
                    {
                        Step++;
                    }
                    else
                    {
                        // Finish
                        // Step Jump
                        Step = 13;
                    }
                    break;

                case 3:
                    // TJV Set
                    if (cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_2, cDEF.Work.Recipe.falltime_2, cDEF.Work.Recipe.openvolt_2, cDEF.Work.Recipe.opentime_2, cDEF.Work.Recipe.risetime_2, cDEF.Work.Recipe.pixelcount_2, 0))
                    {
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_2.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_2.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_2) == 1)
                    {
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_2.PDDStartSpitting(cDEF.Work.Recipe.Hz_2, cDEF.Work.Recipe.nDrop_2) == 1)
                        Step++;
                    else
                    {
                        cDEF.TJV_2.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 6:
                    if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                        cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = true;
                    Move_Bonder2_JetUpPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position Z. ", true);
                    Step++;
                    break;

                case 7:
                    Move_Bonder2_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_JetPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Jetting Position Z.", true);
                    Step++;
                    break;

                case 9:
                    JettingIO = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 10:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder2.JettingTime)
                        break;
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 11:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Bonder2ECM_DSO])
                            break;
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 213;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 213, "[Bonder 2] DSO Signal is not Off - Time Out");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < WorkJetData.Delay)
                        break;
                    WorkJetData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting Radius: {WorkJetData.Angle} Finish:{WorkJetData.Finish}", true);
                    Step = 2;    // 여기서 loop
                    break;


                case 13:
                    if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                        cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = false;
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 14:
                    cDEF.Tact.Bonder2.GetTact();
                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2Point.JettingDataInit(); //젯팅 2번 동작 방지
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 15:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;

                        if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder2_CamCenterX();
                            Move_Bonder2_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 17;
                    }
                    break;

                case 16:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 17:
                    Mode = TRunBonder2Mode.Stop;
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_PtnLineJetting_TJV()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    cDEF.TJV_2.PDDStopSpitting();
                    Move_Bonder2_ReadyPositionZ();
                    if (cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_2, cDEF.Work.Recipe.falltime_2, cDEF.Work.Recipe.openvolt_2, cDEF.Work.Recipe.opentime_2, cDEF.Work.Recipe.risetime_2, cDEF.Work.Recipe.pixelcount_2, 0))
                    {

                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_2.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 1:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_2.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_2) == 1)
                    {
                        CAXM.AxmContiWriteClear(0);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_2.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 2:
                    if (!IsPatternFinish())
                    {
                        Move_Bonder2_PtnLine_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;


                case 3:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_2.PDDStartSpitting(cDEF.Work.Recipe.Hz_2, cDEF.Work.Recipe.nDrop_2) == 1)
                    {
                        if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                            cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = true;
                        Move_Bonder2_PtnLine_FristJetPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head First Jet Position Z.", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_2.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 4:
                    Move_Bonder2_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = GetContiQueue();
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(0);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        //ContiStart = Move_Bonder2_PtnLine_JettingPositionXYZ(Information.VisionResultX, Information.VisionResultY);
                        ContiStart = Move_Bonder2_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnLineData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting  Finish:{WorkJetPtnLineData.Finish}", true);
                    Step = 2;
                    break;

                case 10:

                    if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                        cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = false;
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder2.GetTact();
                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2Pattern.JettingPatternInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;

                        if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder2_CamCenterX();
                            Move_Bonder2_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 14;
                    }
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_PtnArcJetting_TJV()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    cDEF.TJV_2.PDDStopSpitting();
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    if (cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_2, cDEF.Work.Recipe.falltime_2, cDEF.Work.Recipe.openvolt_2, cDEF.Work.Recipe.opentime_2, cDEF.Work.Recipe.risetime_2, cDEF.Work.Recipe.pixelcount_2, 0))
                    {
                        CAXM.AxmContiWriteClear(0);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_2.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 2:
                    if (!IsPatternArcFinish())
                    {
                        if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                            cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = true;
                        Move_Bonder2_PtnArc_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;


                case 3:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_2.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_2) == 1)
                    {
                        FCalc = Environment.TickCount;
                        Move_Bonder2_PtnArc_FristJetPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head First Jet Position Z.", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_2.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_2.PDDStartSpitting(cDEF.Work.Recipe.Hz_2, cDEF.Work.Recipe.nDrop_2) == 1)
                    {
                        Move_Bonder2_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        FCalc = Environment.TickCount;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Start Head Jetting Position X Y Z. ", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_2.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    //ContiStart = false;
                    //ContiStart = GetContiQueue();
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(0);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        ContiStart = Move_Bonder2_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnArcData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2].Jetting  Finish:{WorkJetPtnArcData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                        cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = false;
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder2.GetTact();
                    Information.IndexData.TT_Bonder2 = cDEF.Tact.Bonder2.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder2ARC.JettingPatternInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;

                        if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder2_CamCenterX();
                            Move_Bonder2_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 14;
                    }
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder2Jetting)
                    //{
                    //    Move_Bonder2_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Jetting End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Cleaning()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Step++;
                    break;

                case 1:
                    //Ready 위치 이동
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 2:
                    //Clean XY 위치 이동
                    Move_Bonder2_CleanPositionXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2] Move Head Clean Position X, Y.", true);
                    Step++;
                    break;

                case 3:
                    //Clean Z 위치 이동
                    Move_Bonder2_CleanPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2] Move Head Clean Position Z.", true);
                    Step++;
                    break;

                case 4:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_Clean)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder2 - Clean Tip] End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        protected void Running_TipCleaning()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    //클리닝 스폰지 교체 알람 띄우기
                    if (cDEF.Work.TeachBonder2.TipCleanCntY <= Information.CleanPitchYCount)
                    {
                        Information.CleanPitchYCount = 0;

                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 205;
                        //cDEF.Run.LogWarning(cLog.RunBonder2 + 205, "[Bonder 2] Cleaning Change Please.");
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 205, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Cleaning Change Please", true);
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;



                case 1:
                    if(!cDEF.Work.Project.GlobalOption.UseDummy2)
                    {
                        Step = 6;
                        break;
                    }
                    Step++;
                    break;

                case 2:
                    Move_Bonder2_IdlePositionX();
                    Move_Bonder2_IdlePositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Dummy Shot Position X,Y .", true);
                    TipClean.Backward();
                    Step++;
                    break;

                case 3:
                    Move_Bonder2_IdlePositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Dummy Shot Position Z .", true);
                    Step++;
                    break;

                case 4:
                    JettingIO = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Jetting IO : True.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < cDEF.Work.Recipe.DummyTime2)
                        break;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Jetting IO : False  Jetting Time :{Environment.TickCount - FCalc} ms", true);
                    JettingIO = false;
                    Step++;
                    break;

                case 6:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    if (!cDEF.Work.Project.GlobalOption.UseTipClean2)
                    {
                        Step = 11;
                        break;
                    }
                    Step++;
                    break;

                case 7:
                    Move_Bonder2_TipCleanPositionXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Clean Position X, Y.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder2_TipCleanPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Clean Position Z.", true);
                    Step++;
                    break;

                case 9:
                    TipClean.Forward();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] TipClean Cylinder Forward.", true);
                    Step++;
                    break;

                case 10:
                    Information.CleanPitchYCount++;
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    TipClean.Backward();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] TipClean Cylinder Backward.", true);
                    Step++;
                    break;

                case 11:
                    Move_Bonder2_ReadyPositionX();
                    Step++;
                    break;

                case 12:
                    Information.CleanJetCount = 0;
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_TipClean)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder2 - Clean Tip] End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        protected void Running_Touch()
        {
            if (!IsReady())
                return;

            if (cDEF.Run.DetailMode == TfpRunningMode.frmToStop)
            {
                Mode = TRunBonder2Mode.Stop;
                return;
            }

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    Move_Bonder2_TouchPositionX();
                    Move_Bonder2_TouchPositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Touch Position X Y.", true);
                    Step++;
                    break;

                case 2:
                    Move_Bonder2_TouchPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Touch Position Z.", true);
                    Step++;
                    break;

                case 3:
                    if (HeadZ.ActualPosition < cDEF.Work.TeachBonder2.TouchLimitZ)
                    {
                        Move_Bonder2_RelativeTouchStep();
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Touch Relative Position Z.", true);
                        Step++;
                    }
                    else
                    {
                        Move_Bonder2_ReadyPositionZ();
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 202;
                        //cDEF.Run.LogWarning(cLog.RunBonder2 + 202, "[Bonder 2] TOUCH LIMIT!");
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 202, "");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 4:
                    if (cDEF.Run.Digital.Input[cDI.Bonding_Head_2_Nozzle_Height_Touch_Sensor])
                        Step++;
                    else
                        Step = 3;
                    break;

                case 5:
                    Move_Bonder2_ReadyPositionZ();
                    //MessageBox.Show($"Touch Z : {(HeadZ.ActualPosition - cDEF.Work.TeachBonder2.TouchOffsetZ) / 1000.0}");
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2 - Touch] Touch Z : {(HeadZ.ActualPosition - cDEF.Work.TeachBonder2.TouchOffsetZ) / 1000.0} ", true);
                    cDEF.Work.TeachBonder2.JettingPositionZ = (int)HeadZ.ActualPosition - cDEF.Work.TeachBonder2.TouchOffsetZ;
                    cDEF.Work.TeachBonder2.Save();
                    OnGrid_Display();
                    Step++;
                    break;


                case 6:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_Touch)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder2 - Touch] End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        private int AutoCalCount = 0;
        protected void Running_AutoCal()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    AutoCalCount = 0;
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    // Gap Check
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Move_Bonder2_SampleGapPositionXY();
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Sample Gap Position XY.", true);
                    }
                    Step++;
                    break;

                case 2:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Move_Bonder2_GapMeasure_Z(true);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Sample Gap Position Z.", true);
                    }
                    Step++;
                    break;

                case 3:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Information.GapMeasureValue = cDEF.Serials.Value_Bond2;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Gap Measure : {Information.GapMeasureValue / 1000.0}.", true);
                    }
                    Step++;
                    break;

                case 4:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 5:
                    Move_Bonder2_SamplePositionX();
                    Move_Bonder2_SamplePositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Smaple Position X Y.", true);
                    Step++;
                    break;

                case 6:
                    double dValue = Math.Abs(Information.GapMeasureValue);
                    if (dValue > 5000)
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 217;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 217, "");
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 217, "Gap Measure Limit Over");
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    Move_Bonder2_SamplePositionZ(Information.GapMeasureValue);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Sample Position Z.", true);
                    Step++;
                    break;

                case 7:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.DispenserEcm2.SetMode == 0)
                    {
                        cDEF.DispenserEcm2.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                        cDEF.DispenserEcm2.PressTime = cDEF.Work.Bonder2.JettingTime;
                        cDEF.DispenserEcm2.SetValueStart();
                        FCalc = Environment.TickCount;
                    }
                    Step++;
                    break;

                case 8:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.DispenserEcm2.SetMode == 0)
                    {
                        if (Environment.TickCount - FCalc < 5000)
                        {
                            if (!cDEF.DispenserEcm2.SetValueFinish)
                                break;
                            if (cDEF.DispenserEcm2.CommError)
                            {
                                cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                                cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 2 Comm Err");
                                Mode = TRunBonder2Mode.Stop;
                                return;
                            }
                            Step++;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 211;
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 211, "Dispenser 2 Comm Time Out");
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }
                    }
                    JettingIO = true;
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 9:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder1.JettingTime)
                        break;
                    JettingIO = false;
                    Step++;
                    break;

                case 10:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    Move_Bonder2_CameraRelativeXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Relative X Y", true);
                    Step++;
                    break;

                case 12:
                    Move_Bonder2_CamPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    Step++;
                    break;

                case 13:
                    //Auto Calibration Vision Command 20.09.16 JY작업
                    Information.VisionResultX = 0;
                    Information.VisionResultY = 0;

                    cDEF.Visions.Sendmsg(eVision.V8_Ready);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Send Vision V8 Ready (Auto Calibration).", true);

                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 14:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV8_Ready.Status != CmmStatus.Ok)
                            break;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Receive Vision V8 Ready OK (Auto Calibration).", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 207;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 207, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] (Auto Calibration) V8 Ready Vision Time Out.", true);
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 15:
                    cDEF.Visions.Sendmsg(eVision.V8_Complete);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Send Vision V8 Complete (Auto Calibration).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;
                case 16:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV8_Complete.Status != CmmStatus.Ok)
                            break;

                        if (cDEF.Visions.ackV8_Complete.exist)
                        {
                            Information.VisionResultX = (int)(cDEF.Visions.ackV8_Complete.x);
                            Information.VisionResultY = (int)(cDEF.Visions.ackV8_Complete.y);
                            cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Receive Vision V7 Complete OK (Check Vision) X : {Information.VisionResultX} Y : {Information.VisionResultY}.", true);
                            Step++;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 208;
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 208, "");
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Vision V8 (Auto Calibration) Auto Calibration Fail.", true);
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder2 + 209;
                        cDEF.Run.LogWarning(cLog.RunBonder2 + 209, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] (Auto Calibration) V8 Compelte Vision Time Out.", true);
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    break;

                case 17:
                    if (Math.Abs(Information.VisionResultX) > cDEF.Work.TeachBonder2.AutoCalSpec || Math.Abs(Information.VisionResultY) > cDEF.Work.TeachBonder2.AutoCalSpec)
                    {
                        if (AutoCalCount > cDEF.Work.TeachBonder2.AutoCalCount)
                        {
                            Move_Bonder2_ReadyPositionZ();
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 210;
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 210, "Auto Cal Count Over.");
                            cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] (Auto Calibration) Auto Cal Count Over.", true);
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }

                        HeadX.Relative(Information.VisionResultX, 1000);
                        HeadY.Relative(Information.VisionResultY, 1000);
                        AutoCalCount++;
                        Step = 12;
                        return;
                    }
                    Step++;
                    break;

                case 18:
                    cDEF.Work.TeachBonder2.CameraDistanceOffsetX = (int)cDEF.Run.Bonder2.HeadX.ActualPosition - cDEF.Work.TeachBonder2.SamplePosX;
                    cDEF.Work.TeachBonder2.CameraDistanceOffsetY = (int)cDEF.Run.Bonder2.HeadY.ActualPosition - cDEF.Work.TeachBonder2.SamplePosY;
                    cDEF.Work.TeachBonder2.Save();
                    OnGrid_Display();
                    Step++;
                    break;

                case 19:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_AutoCal)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Auto Calibration End", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        protected void Running_Move_Sample()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    // Gap Check
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Move_Bonder2_SampleGapPositionXY();
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Sample Gap Position XY.", true);
                    }
                    Step++;
                    break;

                case 2:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Move_Bonder2_GapMeasure_Z(true);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Sample Gap Position Z.", true);
                    }
                    Step++;
                    break;

                case 3:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Information.GapMeasureValue = cDEF.Serials.Value_Bond2;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Gap Measure : {Information.GapMeasureValue / 1000.0}.", true);
                    }
                    Step++;
                    break;

                case 4:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 5:
                    Move_Bonder2_SamplePositionX();
                    Move_Bonder2_SamplePositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Smaple Position XY.", true);
                    Step++;
                    break;

                case 6:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        double dValue = Math.Abs(Information.GapMeasureValue);
                        if (dValue > 5000)
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 217;
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 217, "");
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 217, "Gap Measure Limit Over");
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }
                    }
                    else
                    {
                        Information.GapMeasureValue = 0;
                    }

                    Move_Bonder2_SamplePositionZ(Information.GapMeasureValue);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Sample Position Z.", true);
                    Step++;
                    break;

                case 7:
                    Information.MoveSample = true;
                    //cDEF.Tact.Bonder2.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_MoveSample)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Move Sample End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        protected void Running_Move_Cam()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    Move_Bonder2_CameraRelativeXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Relative X Y", true);
                    Step++;
                    break;

                case 2:
                    Move_Bonder2_SampleVisionPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Camera Position Z.", true);
                    Step++;
                    break;

                case 3:
                    Information.MoveSample = true;
                    //cDEF.Tact.Bonder2.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_MoveCam)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Move Cam End - Cycle Time : [{cDEF.Tact.Bonder2.CycleTime.ToString("N3)")}.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        protected void Running_Dummy()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    //Ready 위치 이동
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.Work.Project.GlobalOption.JettingMode2 == 0)
                    {
                        cDEF.DispenserEcm2.SetMode = 1;
                        cDEF.DispenserEcm2.CMDMode = clsSuperEcm3.ECMDMode.ChangeMode;
                        cDEF.DispenserEcm2.SetValueStart();
                        FCalc = Environment.TickCount;
                    }
                    Step++;
                    break;

                case 2:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.Work.Project.GlobalOption.JettingMode2 == 0)
                    {
                        if (Environment.TickCount - FCalc < 5000)
                        {
                            if (!cDEF.DispenserEcm2.SetValueFinish)
                                break;
                            if (cDEF.DispenserEcm2.CommError)
                            {
                                cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                                cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 2 Comm Err");
                                Mode = TRunBonder2Mode.Stop;
                                return;
                            }
                            Step++;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 211;
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 211, "Dispenser 2 Comm Time Out");
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }
                    }
                    break;

                case 3:
                    Move_Bonder2_IdlePositionX();
                    Move_Bonder2_IdlePositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Idle Position XY.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder2_IdlePositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Idle Position Z.", true);
                    Step++;
                    break;

                case 5:
                    JettingIO = true;
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 6:
                    if (Environment.TickCount - FCalc < cDEF.Work.Recipe.DummyTime2)
                        break;

                    JettingIO = false;
                    Step++;
                    break;
                case 7:
                    //Ready 위치 이동
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 8:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.Work.Project.GlobalOption.JettingMode2 == 0)
                    {
                        cDEF.DispenserEcm2.SetMode = 0;
                        cDEF.DispenserEcm2.CMDMode = clsSuperEcm3.ECMDMode.ChangeMode;
                        cDEF.DispenserEcm2.SetValueStart();
                        FCalc = Environment.TickCount;
                    }
                    Step++;
                    break;

                case 9:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.Work.Project.GlobalOption.JettingMode2 == 0)
                    {
                        if (Environment.TickCount - FCalc < 5000)
                        {
                            if (!cDEF.DispenserEcm2.SetValueFinish)
                                break;
                            if (cDEF.DispenserEcm2.CommError)
                            {
                                cDEF.Run.SetAlarmID = cLog.RunBonder2 + 212;
                                cDEF.Run.LogWarning(cLog.RunBonder2 + 212, "Dispenser 2 Comm Err");
                                Mode = TRunBonder2Mode.Stop;
                                return;
                            }
                            Step++;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 211;
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 211, "Dispenser 2 Comm Time Out");
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }
                    }
                    break;

                case 10:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_Dummy)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder2 - Dummy Discharge] End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }


        protected void Running_ReadyPos()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    //Ready 위치 이동
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2] Move Head Ready Position Z.", true);

                    Step++;
                    break;

                case 1:
                    Move_Bonder2_ReadyPositionX();
                    Move_Bonder2_ReadyPositionY();

                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2] Move Head Ready Position X Y.", true);
                    Step++;
                    break;


                case 2:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_Ready)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder2 - Ready Pos] End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Gap_Measure()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if(!cDEF.Work.Project.GlobalOption.UseBonder2)
                    {
                        Step = 6;
                        break;

                    }

                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    // 측정 위치 이동
                    Move_Bonder2_GapMeasureXY(Information.VisionResultX, Information.VisionResultY);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Gap Measure X Y", true);
                    Step++;
                    break;

                case 2:
                    // 측정 Z축 이동
                    Move_Bonder2_GapMeasure_Z(false);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Gap Measure Position Z.", true);
                    Step++;
                    break;


                case 3:
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder2.GapMeasureDelay)
                        break;

                    Information.GapMeasureValue = cDEF.Serials.Value_Bond2 ;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Gap Measure Value : {Information.GapMeasureValue}.", true);
                    Step++;
                    break;

                case 5:
                    // 측정 판단
                    if (Math.Abs(Information.GapMeasureValue) < cDEF.Work.Bonder2.GapOffsetLimitZ)
                    {
                        //OK
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Judge OK", true);
                    }
                    else
                    {
                        // NG
                        Information.IndexData.Status = eLensIndexStatus.LensHeightFail;
                        Information.IndexData.FailType = eFailType.LensHeightFail;
                        Information.GapMeasureValue = 0;
                        cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Judge NG. Gap Value : {Information.GapMeasureValue}, Spec : {cDEF.Work.Bonder2.GapOffsetLimitZ} ", true);
                        Move_Bonder2_ReadyPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                        if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_GapMeasure)
                        {
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 218, "Gap Measure NG");
                            cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                        }
                        Mode = TRunBonder2Mode.Stop;
                        return;
                    }
                    Step++;
                    break;

                case 6:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 7:
                    Information.GapMeasureFinish = true;
                    cDEF.Tact.Bonder2GapMesure.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_GapMeasure)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder2] Gap Measure End - Cycle Time : [{cDEF.Tact.Bonder2GapMesure.CycleTime.ToString("N3)")}.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Gap_TouchAdjust()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    // 측정 위치 이동
                    Move_Bonder2_GapTouchPositionXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Gap Touch XY", true);
                    Step++;
                    break;

                case 2:
                    // 측정 Z축 이동
                    Move_Bonder2_GapMeasure_Z(true);
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Gap Measure Position Z.", true);
                    Step++;
                    break;


                case 3:
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder2.GapMeasureDelay)
                        break;

                    Information.GapMeasureValue = cDEF.Serials.Value_Bond2;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Gap Measure Value : {Information.GapMeasureValue}.", true);
                    Step++;
                    break;

                case 5:
                    // 측정 판단
                    if (Math.Abs(Information.GapMeasureValue) > 5)
                    {
                        if (Math.Abs(Information.GapMeasureValue) > 5000)
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder2 + 217;
                            cDEF.Run.LogWarning(cLog.RunBonder2 + 217, "Gap Measure Limit Over");
                            Mode = TRunBonder2Mode.Stop;
                            return;
                        }
                        //OK
                        HeadZ.Relative(-Information.GapMeasureValue, 100);
                        cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Judge OK", true);
                        Step = 3;
                        break;
                    }
                    else
                    {
                        // NG
                        Step++;
                    }
                    break;

                case 6:
                    cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Head Z Move Complete. Old : {cDEF.Work.TeachBonder2.GapMeasureZ / 1000.0} mm ,Current : {HeadZ.ActualPosition / 1000.0} mm, Gap Value : {cDEF.Serials.Value_Bond2 / 1000.0} ", true);
                    cDEF.Work.TeachBonder2.GapMeasureZ = (int)HeadZ.ActualPosition;
                    cDEF.Work.TeachBonder2.Save();
                    OnGrid_Display();
                    Move_Bonder2_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder 2] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 7:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder2_GapTouchAdjust)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder2Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder2, "[Bonder2] Gap Touch Adjust End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        #endregion
        #region Move Command
        //Ready Position
        public void Move_Bonder2_ReadyPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachBonder2.ReadyPositionX, Sleep);
        }
        public void Move_Bonder2_ReadyPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder2.ReadyPositionY, Sleep);
        }
        public void Move_Bonder2_ReadyPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder2.ReadyPositionZ, Sleep);
        }

        //Dipping CenterXY
        public void Move_Bonder2_CamCenterX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachBonder2.CamPositionX, Sleep);
        }
        public void Move_Bonder2_CamCenterY()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder2.CamPositionY, Sleep);
        }

        //Dipping Z
        public void Move_Bonder2_CamPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder2.CamPositionZ, Sleep);
        }
        public void Move_Bonder2_JettingPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder2.JettingPositionZ, Sleep);
        }
      
        public void Move_Bonder2_JettingPositionXY(int Index, int CamOffsetX = 0, int CamOffsetY = 0)
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int posX = cDEF.Work.TeachBonder2.CamPositionX;
            posX += cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            if (Index == 0)
                posX += cDEF.Work.TeachBonder2.Jetting1OffsetX;
            else if (Index == 1)
                posX += cDEF.Work.TeachBonder2.Jetting2OffsetX;
            else if (Index == 2)
                posX += cDEF.Work.TeachBonder2.Jetting3OffsetX;
            else if (Index == 3)
                posX += cDEF.Work.TeachBonder2.Jetting4OffsetX;
            posX += CamOffsetX;

            int posY = cDEF.Work.TeachBonder2.CamPositionY;
            posY += cDEF.Work.TeachBonder2.CameraDistanceOffsetY;
            if (Index == 0)
                posY += cDEF.Work.TeachBonder2.Jetting1OffsetY;
            else if (Index == 1)
                posY += cDEF.Work.TeachBonder2.Jetting2OffsetY;
            else if (Index == 2)
                posY += cDEF.Work.TeachBonder2.Jetting3OffsetY;
            else if (Index == 3)
                posY += cDEF.Work.TeachBonder2.Jetting4OffsetY;

            posY += CamOffsetY;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
        }


        public void Move_Bonder2_PtnLine_FristJetPositionZ()
        {
            int Sleep;
            int PosZ = cDEF.Work.TeachBonder2.JettingPositionZ;
            JettingLineData jld = WorkJetPtnLineData.JetLineData[0];

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            PosZ += jld.ZPos;
            PosZ += cDEF.Work.Bonder2.GapOffsetZ;
            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;
            HeadZ.Absolute(PosZ, Sleep);
        }
        public void Move_Bonder2_PtnArc_FristJetPositionZ()
        {
            int Sleep;
            int PosZ = cDEF.Work.TeachBonder2.JettingPositionZ;
            JettingArcData jld = WorkJetPtnArcData.JetArcData[0];

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            PosZ += jld.ZPos;
            PosZ += cDEF.Work.Bonder2.GapOffsetZ;
            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;
            HeadZ.Absolute(PosZ, Sleep);
        }

        public void Move_Bonder2_PtnLine_FristJettingPositionXY(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder2.CamPositionX;
            PosY = cDEF.Work.TeachBonder2.CamPositionY;

            PosX -= cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder2.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            JettingLineData jld = WorkJetPtnLineData.JetLineData[0];

            PosX += jld.XPos;
            PosY += jld.YPos;

            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);

        }

        public void Move_Bonder2_PtnArc_FristJettingPositionXY(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder2.CamPositionX;
            PosY = cDEF.Work.TeachBonder2.CamPositionY;

            PosX -= cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder2.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            JettingArcData jld = WorkJetPtnArcData.JetArcData[0];

            PosX += jld.XPos;
            PosY += jld.YPos;

            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);

        }
        public bool Move_Bonder2_PtnLine_JettingPositionXYZ(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder2.CamPositionX;
            PosY = cDEF.Work.TeachBonder2.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder2.JettingPositionZ;

            PosX -= cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder2.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

          
            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 25;
            axis[1] = 26;
            axis[2] = 27;

            List<TfpConti> lstConti = new List<TfpConti>();
            //lst 

            for (int i = 1; i < WorkJetPtnLineData.JetLineData.Count; i++)
            {
                TfpConti conti = new TfpConti();
                // Digital Conti add

                JettingLineData jld = WorkJetPtnLineData.JetLineData[i];

                conti.DI_ModuleType = 1;
                conti.DI_ModuleNo[0] = 5;
                conti.DI_BitOffset[0] = 2;// cDO.Bonder_Dispensor_2_Jetting;

                conti.DI_Mode[0] = 0;  //비율로 할꺼냐 시간으로 할거냐
                conti.Value[0] = 0;

                /*
                conti.DI_Mode[0] = 2;
                if (cDEF.Work.Project.GlobalOption.UseLineIoFull2)
                    conti.Value[0] = 100;
                else
                    conti.Value[0] = 99;
                    */

                if (jld.Shot)
                {
                    conti.DI_BitOutput[0] = 1;
                }
                else
                    conti.DI_BitOutput[0] = 0;

                conti.Mode = TfpContiMode.fcmDigital;
                lstConti.Add(conti);

                // Motion  Conti add
                conti = new TfpConti();
                conti.Pos[0] = PosX + jld.XPos;
                conti.Pos[1] = PosY + jld.YPos;
                conti.Pos[2] = PosZ + jld.ZPos;

                conti.Velocity = jld.LineSpeed;
                conti.Acc = HeadX.Speed.FRun.FAccelerator;
                conti.Dec = HeadX.Speed.FRun.FDecelerator;
                conti.Mode = TfpContiMode.fcmLine;
                lstConti.Add(conti);
            }

            return HeadX.ContiStart(1, axis, lstConti, 3, 1);

        }


        public bool Move_Bonder2_PtnLine_JettingPositionXYZ_DistIO(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder2.CamPositionX;
            PosY = cDEF.Work.TeachBonder2.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder2.JettingPositionZ;
            PosZ += cDEF.Work.Bonder2.GapOffsetZ;

            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;

            PosX -= cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder2.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;


            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 25;
            axis[1] = 26;
            axis[2] = 27;


            int lCoordinate = 1;


            CAXM.AxmContiWriteClear(lCoordinate);
            // 축지정. 반드시 낮은 축부터 지정 되야 함.
            // 8축 이상 구성 되었을때, 같은 축그룹끼리만 된다고 함. *확인 필요
            CAXM.AxmContiSetAxisMap(lCoordinate, 3, axis);
            // 0: abs, 1: rel
            CAXM.AxmContiSetAbsRelMode(lCoordinate, 0);
            CAXM.AxmContiBeginNode(lCoordinate);

            //Motion Move 등록

            bool BeforeDI_Value = !WorkJetPtnLineData.JetLineData[1].Shot;


            double[] Pos = new double[3];
            double Velocity;
            double Acc;
            double Dec;

            int DI_ModuleType = 0;
            int[] DI_BitOffset = new int[2];
            int[] DI_Mode = new int[2];
            int[] DI_ModuleNo = new int[2];
            int[] DI_BitOutput = new int[2];
            double[] Value = new double[2];
            bool BitOutput;

            for (int i = 1; i < WorkJetPtnLineData.JetLineData.Count; i++)
            {
                JettingLineData jld = WorkJetPtnLineData.JetLineData[i];


                BitOutput = jld.Shot;

                //IO 추가
                if (BeforeDI_Value != BitOutput)
                {
                    DI_ModuleType = 1;
                    //DI_ModuleNo[0] = 5;
                    //DI_BitOffset[0] = 2;

                    DI_ModuleNo[0] = 6;
                    DI_BitOffset[0] = 1;
                    DI_Mode[0] = 0;  
                    Value[0] = 0;

                    if (BitOutput)
                    {
                        DI_BitOutput[0] = 1;
                    }
                    else
                        DI_BitOutput[0] = 0;

#if !Notebook
                    CAXM.AxmContiDigitalOutputBit(lCoordinate, 1, DI_ModuleType, DI_ModuleNo, DI_BitOffset, DI_BitOutput, Value, DI_Mode);
#endif

                    BeforeDI_Value = BitOutput;
                }

                //모션 추가
                Pos[0] = PosX + jld.XPos;
                Pos[1] = PosY + jld.YPos;
                Pos[2] = PosZ + jld.ZPos;

                Velocity = jld.LineSpeed;
                Acc = HeadX.Speed.FRun.FAccelerator;
                Dec = HeadX.Speed.FRun.FDecelerator;

                CAXM.AxmLineMove(lCoordinate, Pos, Velocity, Acc, Dec);
            }


            CAXM.AxmContiEndNode(lCoordinate);

            //uint CheckQueue = 0;
            //CAXM.AxmContiReadFree(lCoordinate, ref CheckQueue);
            //if (CheckQueue == 0)
            //    return false;

            //CAXM.AxmContiSetAbsRelMode(lCoordinate, 0);
            //CAXM.AxmContiStart(lCoordinate, 0, 0);

            return true;
        }

        public bool Move_Bonder2_PtnArc_JettingPositionXYZ_DistIO(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder2.CamPositionX;
            PosY = cDEF.Work.TeachBonder2.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder2.JettingPositionZ;
            PosZ += cDEF.Work.Bonder2.GapOffsetZ;

            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;

            PosX -= cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder2.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;


            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 25;
            axis[1] = 26;
            axis[2] = 27;

            int[] axisArc = new int[2];
            axisArc[0] = 25;
            axisArc[1] = 26;

            int lCoordinate = 1;


            CAXM.AxmContiWriteClear(lCoordinate);
            // 축지정. 반드시 낮은 축부터 지정 되야 함.
            // 8축 이상 구성 되었을때, 같은 축그룹끼리만 된다고 함. *확인 필요
            CAXM.AxmContiSetAxisMap(lCoordinate, 3, axis);
            // 0: abs, 1: rel
            CAXM.AxmContiSetAbsRelMode(lCoordinate, 0);
            CAXM.AxmContiBeginNode(lCoordinate);

            //Motion Move 등록

            bool BeforeDI_Value = !WorkJetPtnArcData.JetArcData[1].Shot;


            double[] cPos = new double[2];
            double[] Pos = new double[3];
            double[] PosArc = new double[2];
            double[] endPos = new double[2];
            double Velocity;
            double Acc;
            double Dec;
            double Radius;

            int DI_ModuleType = 0;
            int[] DI_BitOffset = new int[2];
            int[] DI_Mode = new int[2];
            int[] DI_ModuleNo = new int[2];
            int[] DI_BitOutput = new int[2];
            double[] Value = new double[2];
            bool BitOutput;

            int oldEX = WorkJetPtnArcData.JetArcData[0].XPos;
            int oldEY = WorkJetPtnArcData.JetArcData[0].YPos;

            for (int i = 0; i < WorkJetPtnArcData.JetArcData.Count; i++)
            {
                JettingArcData jld = WorkJetPtnArcData.JetArcData[i];
                BitOutput = jld.Shot;

                //IO 추가
                //if (BeforeDI_Value != BitOutput)
                {
                    DI_ModuleType = 1;

                    DI_ModuleNo[0] = 6;
                    DI_BitOffset[0] = 1;

                    DI_Mode[0] = 2; // 아진 확인 - 비율 모드 2 기본 : 0
                    Value[0] = 100; // 100 기본: 0

                    if (BitOutput)
                    {
                        DI_BitOutput[0] = 1;
                    }
                    else
                        DI_BitOutput[0] = 0;

#if !Notebook
                    CAXM.AxmContiDigitalOutputBit(lCoordinate, 1, DI_ModuleType, DI_ModuleNo, DI_BitOffset, DI_BitOutput, Value, DI_Mode);
#endif
                    BeforeDI_Value = BitOutput;
                }


                //모션 추가

                Velocity = jld.LineSpeed;
                Acc = HeadX.Speed.FRun.FAccelerator;
                Dec = HeadX.Speed.FRun.FDecelerator;

                switch (jld.JetType)
                {
                    case eJetType.Line:
                        Pos[0] = PosX + jld.XPos;
                        Pos[1] = PosY + jld.YPos;
                        Pos[2] = PosZ + jld.ZPos;

                        CAXM.AxmLineMove(lCoordinate, Pos, Velocity, Acc, Dec);
                        break;

                    case eJetType.Arc:
                        PosArc[0] = PosX;
                        PosArc[1] = PosY;

                        CAXM.AxmCircleAngleMove(lCoordinate, axisArc, PosArc, jld.Angle, Velocity, Acc, Dec, 0);
                        break;
                }

            }


            CAXM.AxmContiEndNode(lCoordinate);

            //uint CheckQueue = 0;
            //CAXM.AxmContiReadFree(lCoordinate, ref CheckQueue);
            //if (CheckQueue == 0)
            //    return false;

            //CAXM.AxmContiSetAbsRelMode(lCoordinate, 0);
            //CAXM.AxmContiStart(lCoordinate, 0, 0);

            return true;
        }
        public bool GetContiQueue()
        {
            int lCoordinate = 1;
            uint CheckQueue = 0;
            CAXM.AxmContiReadFree(lCoordinate, ref CheckQueue);
            if (CheckQueue == 0)
                return false;
            CAXM.AxmContiSetAbsRelMode(lCoordinate, 0);
            CAXM.AxmContiStart(lCoordinate, 0, 0);
            return true;
        }

        public bool Move_Bonder2_PtnLine_JettingPositionXYZ_ControlIO(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder2.CamPositionX;
            PosY = cDEF.Work.TeachBonder2.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder2.JettingPositionZ;

            PosX -= cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder2.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;


            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 25;
            axis[1] = 26;
            axis[2] = 27;


            int lCoordinate = 1;


            CAXM.AxmContiWriteClear(lCoordinate);
            // 축지정. 반드시 낮은 축부터 지정 되야 함.
            // 8축 이상 구성 되었을때, 같은 축그룹끼리만 된다고 함. *확인 필요
            CAXM.AxmContiSetAxisMap(lCoordinate, 3, axis);
            // 0: abs, 1: rel
            CAXM.AxmContiSetAbsRelMode(lCoordinate, 0);
            CAXM.AxmContiBeginNode(lCoordinate);

            //Motion Move 등록

            bool BeforeDI_Value = !WorkJetPtnLineData.JetLineData[1].Shot;


            double[] Pos = new double[3];
            double Velocity;
            double Acc;
            double Dec;

            int[] DI_BitOffset = new int[2];
            int[] DI_Mode = new int[2];
            int[] DI_ModuleNo = new int[2];
            int[] DI_BitOutput = new int[2];
            double[] Value = new double[2];
      
            for (int i = 0; i < WorkJetPtnLineData.JetLineData.Count; i++)
            {
                JettingLineData jld = WorkJetPtnLineData.JetLineData[i];
                if (jld.Shot)
                {
                    //모션 추가
                    Pos[0] = PosX + jld.XPos;
                    Pos[1] = PosY + jld.YPos;
                    Pos[2] = PosZ + jld.ZPos;

                    Velocity = jld.LineSpeed;
                    Acc = HeadX.Speed.FRun.FAccelerator;
                    Dec = HeadX.Speed.FRun.FDecelerator;

                    CAXM.AxmLineMove(lCoordinate, Pos, Velocity, Acc, Dec);
                }
            }


            CAXM.AxmContiEndNode(lCoordinate);

            uint CheckQueue = 0;
            CAXM.AxmContiReadFree(lCoordinate, ref CheckQueue);
            if (CheckQueue == 0)
                return false;

            CAXM.AxmContiSetAbsRelMode(lCoordinate, 0);
            // Io 신호켜기
            JettingIO = true;
            //cDEF.Run.Digital.Output[cDO.Bonder_Dispensor_2_Jetting] = true;


            CAXM.AxmContiStart(lCoordinate, 0, 0);

            return true;
        }
        public void Move_Bonder2_CameraRelativeXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            HeadX.Relative(cDEF.Work.TeachBonder2.CameraDistanceOffsetX, SleepX);
            HeadY.Relative(cDEF.Work.TeachBonder2.CameraDistanceOffsetY, SleepY);
        }
        public void Move_Bonder2_BonderRelativeXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            HeadX.Relative(-cDEF.Work.TeachBonder2.CameraDistanceOffsetX, SleepX);
            HeadY.Relative(-cDEF.Work.TeachBonder2.CameraDistanceOffsetY, SleepY);
        }

        //Avoid Position
        public void Move_Bonder2_AvoidPositionX()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayX;

            HeadX.Absolute(cDEF.Work.TeachBonder2.AvoidPositionX, Sleep);
        }
        public void Move_Bonder2_AvoidPositionY()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayY;

            HeadY.Absolute(cDEF.Work.TeachBonder2.AvoidPositionY, Sleep);
        }

        public void Move_Bonder2_IdlePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayX;

            HeadX.Absolute(cDEF.Work.TeachBonder2.IdlePosX, Sleep);
        }
        public void Move_Bonder2_IdlePositionY()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder2.IdlePosY, Sleep);
        }
        public void Move_Bonder2_IdlePositionZ()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder2.IdlePosZ, Sleep);
        }
        public void Move_Bonder2_SamplePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayX;

            HeadX.Absolute(cDEF.Work.TeachBonder2.SamplePosX, Sleep);
        }
        public void Move_Bonder2_SamplePositionY()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder2.SamplePosY, Sleep);
        }
        public void Move_Bonder2_SampleGapPositionXY()
        {
            int SleepX;
            int SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;
            PosX = cDEF.Work.TeachBonder2.SampleGapPosX;
            PosY = cDEF.Work.TeachBonder2.SampleGapPosY;

            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);
        }
        public void Move_Bonder2_SamplePositionZ(double GapZ)
        {
            int Sleep;

            int posZ = cDEF.Work.TeachBonder2.SamplePosZ;

            if(cDEF.Work.DispSensor.DispenserType != 0)
            {
//                if (Math.Abs(GapZ) > 5000)
//                    GapZ = 300;

                posZ = cDEF.Work.TeachBonder2.JettingPositionZ;
                posZ -= (int)GapZ;

                posZ -= 500;
            }

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(posZ, Sleep);
        }
        public void Move_Bonder2_SampleVisionPositionZ()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder2.SampleVisionPosZ, Sleep);
        }
        public void Move_Bonder2_TouchPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder2.MovingDelayX;

            HeadX.Absolute(cDEF.Work.TeachBonder2.TouchPosX, Sleep);
        }
        public void Move_Bonder2_TouchPositionY()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder2.TouchPosY, Sleep);
        }
        public void Move_Bonder2_TouchPositionZ()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder2.TouchPosZ, Sleep);
        }
        public void Move_Bonder2_RelativeTouchStep()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Relative(cDEF.Work.TeachBonder2.TouchStep, Sleep);
        }

        //JettingData
        public void Move_Bonder2_JetPositionXY(int OffsetX, int OffsetY, JettingData Data)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder2.CamPositionX;
            PosY = cDEF.Work.TeachBonder2.CamPositionY;

            PosX -= cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder2.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            Point Calc = GetCirclePoint(0, 0, Data.Radius, Data.Angle);

            PosX += Calc.X;
            PosY += Calc.Y;


            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);
        }
        public void Move_Bonder2_JetPositionXY()
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder2.CamPositionX;
            PosY = cDEF.Work.TeachBonder2.CamPositionY;

            PosX -= cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder2.CameraDistanceOffsetY;


            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);
        }
  
        public void Move_Bonder2_JetPositionZ(JettingData Data)
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;

            int PosZ = cDEF.Work.TeachBonder2.JettingPositionZ;

            PosZ += Data.ZOffset;
            PosZ += cDEF.Work.Bonder2.GapOffsetZ;

            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;

            HeadZ.Absolute(PosZ, Sleep);
        }
        public void Move_Bonder2_JetUpPositionZ(JettingData Data)
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;

            int PosZ = cDEF.Work.TeachBonder2.JettingPositionZ;
            PosZ += cDEF.Work.Bonder2.GapOffsetZ;

            if (cDEF.Work.Project.GlobalOption.UseGap)
            {
                PosZ -= (int)Information.GapMeasureValue;
            }

            PosZ -= Data.ZUp;

            HeadZ.Absolute(PosZ, Sleep);
        }

        public void Move_Bonder2_CleanPositionZ() // 함수 생성
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder2.CleanPositionZ, Sleep);
        }

        public void Move_Bonder2_CleanPositionXY() // 함수 생성
        {
            int Sleep1;
            int Sleep2;

            Sleep1 = cDEF.Work.Bonder2.MovingDelayX;
            Sleep2 = cDEF.Work.Bonder2.MovingDelayY;
            HeadX.Absolute(cDEF.Work.TeachBonder2.CleanPositionX, Sleep1);
            HeadY.Absolute(cDEF.Work.TeachBonder2.CleanPositionY, Sleep2);
        }
        public void Move_Bonder2_TipCleanPositionXY() // 함수 생성
        {
            int Sleep1;
            int Sleep2;

            Sleep1 = cDEF.Work.Bonder2.MovingDelayX;
            Sleep2 = cDEF.Work.Bonder2.MovingDelayY;

            int PitchY = cDEF.Work.TeachBonder2.TipCleanPitchY * Information.CleanPitchYCount;

            int posX = cDEF.Work.TeachBonder2.TipCleanStartPosX;
            int posY = cDEF.Work.TeachBonder2.TipCleanStartPosY + PitchY;

            HeadX.Absolute(posX, Sleep1);
            HeadY.Absolute(posY, Sleep2);
        }
        public void Move_Bonder2_TipCleanPositionZ() // 함수 생성
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder2.TipCleanPosZ, Sleep);
        }
        public void Move_Bonder2_GapMeasureXY(int OffsetX, int OffsetY)
        {
            int Sleep1;
            int Sleep2;

            Sleep1 = cDEF.Work.Bonder2.MovingDelayX;
            Sleep2 = cDEF.Work.Bonder2.MovingDelayY;

            int PosX = cDEF.Work.TeachBonder2.CamPositionX;
            int PosY = cDEF.Work.TeachBonder2.CamPositionY;

            PosX -= cDEF.Work.TeachBonder2.GapDistanceX;
            PosY -= cDEF.Work.TeachBonder2.GapDistanceY;

            PosX += cDEF.Work.Bonder2.GapPosX;
            PosY += cDEF.Work.Bonder2.GapPosY;

            PosX += OffsetX;    // Vision Offset
            PosY += OffsetY;

            HeadX.Absolute(PosX, Sleep1);
            HeadY.Absolute(PosY, Sleep2);
        }
        public void Move_Bonder2_GapTouchPositionXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            HeadX.Absolute(cDEF.Work.TeachBonder2.GapTouchX, SleepX);
            HeadY.Absolute(cDEF.Work.TeachBonder2.GapTouchY, SleepY);
        }
        public void Move_Bonder2_GapAdjustPositionXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder2.MovingDelayX;
            SleepY = cDEF.Work.Bonder2.MovingDelayY;

            HeadX.Absolute(cDEF.Work.TeachBonder2.GapAdjustX, SleepX);
            HeadY.Absolute(cDEF.Work.TeachBonder2.GapAdjustY, SleepY);
        }
        public void Move_Bonder2_GapMeasure_Z(bool UI)
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder2.MovingDelayZ;

            int PosZ = cDEF.Work.TeachBonder2.GapMeasureZ;

            if (!UI)
                PosZ += cDEF.Work.Bonder2.GapOffsetZ;

            HeadZ.Absolute(PosZ, Sleep);
        }

#endregion
        public Point GetCirclePoint(int ax, int ay, double radius, double Angle)
        {
            Point target = new Point();
            double radian = Math.PI * Angle / 180.0;
            target.X = ax + (int)((radius * Math.Cos(radian) * 1000.0));
            target.Y = ay + (int)((radius * Math.Sin(radian) * 1000.0));

            return target;
        }


#region CheckPosition
        public bool Is_Bonder2_ReadyPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.ReadyPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_ReadyPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.ReadyPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_ReadyPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.ReadyPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_CamCenterX()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.CamPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_CamCenterY()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.CamPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_CamPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.CamPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_IdleCenterX()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.IdlePosX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_IdleCenterY()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.IdlePosY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_IdleCenterZ()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.IdlePosZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_SampleCenterX()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.SamplePosX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_SampleCenterY()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.SamplePosY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_SampleCenterZ()
        {
            if (IsRange((double)cDEF.Work.TeachBonder2.SamplePosZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_JettingPositionXY(int Index)
        {
            int posX = cDEF.Work.TeachBonder2.CamPositionX;
            posX += cDEF.Work.TeachBonder2.CameraDistanceOffsetX;
            if (Index == 0)
                posX += cDEF.Work.TeachBonder2.Jetting1OffsetX;
            else if (Index == 1)
                posX += cDEF.Work.TeachBonder2.Jetting2OffsetX;
            else if (Index == 2)
                posX += cDEF.Work.TeachBonder2.Jetting3OffsetX;
            else if (Index == 3)
                posX += cDEF.Work.TeachBonder2.Jetting4OffsetX;

            int posY = cDEF.Work.TeachBonder2.CamPositionY;
            posY += cDEF.Work.TeachBonder2.CameraDistanceOffsetY;
            if (Index == 0)
                posY += cDEF.Work.TeachBonder2.Jetting1OffsetY;
            else if (Index == 1)
                posY += cDEF.Work.TeachBonder2.Jetting2OffsetY;
            else if (Index == 2)
                posY += cDEF.Work.TeachBonder2.Jetting3OffsetY;
            else if (Index == 3)
                posY += cDEF.Work.TeachBonder2.Jetting4OffsetY;

            if (IsRange((double)posX, HeadX.ActualPosition) && IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder2_JettingPositionZ()
        {
            int posZ = cDEF.Work.TeachBonder2.JettingPositionZ;

            if (IsRange((double)posZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_AvoidPositionX()
        {
            int posX = cDEF.Work.TeachBonder2.AvoidPositionX;

            if (IsRange((double)posX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_AvoidPositionY()
        {
            int posY = cDEF.Work.TeachBonder2.AvoidPositionY;

            if (IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }


        public bool Is_Bonder2_CleanPositionZ()
        {
            int posZ = cDEF.Work.TeachBonder2.CleanPositionZ;

            if (IsRange((double)posZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder2_CleanPositionX()
        {
            int posX = cDEF.Work.TeachBonder2.CleanPositionX;

            if (IsRange((double)posX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder2_CleanPositionY()
        {
            int posY = cDEF.Work.TeachBonder2.CleanPositionY;

            if (IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_GapTouchXY()
        {
            int posX = cDEF.Work.TeachBonder2.GapTouchX;
            int posY = cDEF.Work.TeachBonder2.GapTouchY;

            if (IsRange((double)posX, HeadX.ActualPosition) && IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder2_GapAdjustXY()
        {
            int posX = cDEF.Work.TeachBonder2.GapAdjustX;
            int posY = cDEF.Work.TeachBonder2.GapAdjustY;

            if (IsRange((double)posX, HeadX.ActualPosition) && IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder2_GapMeasureZ()
        {
            int posZ = cDEF.Work.TeachBonder2.GapMeasureZ;

            if (IsRange((double)posZ, HeadZ.ActualPosition))
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

        
    }
}

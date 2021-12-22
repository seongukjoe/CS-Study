using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;
using System.Windows.Documents;
using System.Windows.Media.Animation;

namespace XModule.Running
{

    public class RunBonder1Information : fpObject
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
                if(FCheckVisionFinish != value)
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
                if(FJettingCount != value)
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

        public RunBonder1Information() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunBonder1.dat";
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
    public enum TRunBonder1Mode
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

    
    public class RunBonder1 : TfpRunningModule 
    {
        public delegate void GridDataDisplayHandler();
        public event GridDataDisplayHandler OnGrid_Display;

        private RunBonder1Information FInformation;
        private TRunBonder1Mode FMode;
        private JettingData WorkJetData;
        private JettingPatternLineData WorkJetPtnLineData;
        private JettingPatternArcData WorkJetPtnArcData;

        public int FCalc;
        private bool ContiStart = false;



        public RunBonder1(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunBonder1Information();
            WorkJetData = new JettingData();
            WorkJetPtnLineData = new JettingPatternLineData();
            WorkJetPtnArcData = new JettingPatternArcData();
        }
        

        #region **Property**
        public RunBonder1Information Information
        {
            get { return FInformation; }
        }

        public TRunBonder1Mode Mode
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
                CAXD.AxdoReadOutportBit(6, 0, ref FJettingIO);
                if (FJettingIO == 1)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    CAXD.AxdoWriteOutportBit(6, 0, 1);
                }
                else
                    CAXD.AxdoWriteOutportBit(6, 0, 0);
            }
        }

    

        public void Proc_JettingIO(bool bvalue)
        {
            if (bvalue)
                CAXD.AxdoWriteOutportBit(6, 0, 1);
            else
                CAXD.AxdoWriteOutportBit(6, 0, 0);
        }
        #endregion //Property//

        private TRunBonder1Mode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunBonder1Mode.Stop;
        }
        private void SetMode(TRunBonder1Mode Value)
        {
            if (Value == TRunBonder1Mode.Stop)
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
                case TRunBonder1Mode.Stop:
                    return "Stop";
                case TRunBonder1Mode.CheckCamera:
                    return "Check Camera";
                case TRunBonder1Mode.Jetting:
                    return "Jetting";

                case TRunBonder1Mode.PtnLineJetting:
                    return "Pattern Line Jetting";

                case TRunBonder1Mode.PtnArcJetting:
                    return "Pattern ARC Jetting";
                case TRunBonder1Mode.Clean:
                    return "Clean";

                case TRunBonder1Mode.MoveSample:
                    return "Move Sample";

                case TRunBonder1Mode.MoveCam:
                    return "Move Cam";

                case TRunBonder1Mode.TipCleaning:
                    return "TipCleaning";

                case TRunBonder1Mode.AutoCal:
                    return "AutoCal";

                case TRunBonder1Mode.GapMeasure:
                    return "Gap Measure";

                case TRunBonder1Mode.GapTouchAdjust:
                    return "Gap Touch Adjust";

                default:
                    return "";
            }
        }
        private int IdleTicCount = 0;
        protected override void ProcReal()
        {
            if (cDEF.Run.DetailMode == TfpRunningMode.frmAlarm)
            {
                if (JettingIO)
                    JettingIO = false;
            }
            if (this.MotionCount < 1)
                return;

            if (!cDEF.Work.Project.GlobalOption.UseBonder1)
                return;

            if ((cDEF.Run.DetailMode == TfpRunningMode.frmStop || cDEF.Run.DetailMode == TfpRunningMode.frmRun) && Mode == TRunBonder1Mode.Stop)
            {
                if (cDEF.Work.Project.GlobalOption.UseIdle1)
                {
                    if (!IsReady())
                        return;
                    if (!Is_Bonder1_IdleCenterX() || !Is_Bonder1_IdleCenterY())
                    {
                        if (Is_Bonder1_ReadyPositionZ())
                        {
                            Move_Bonder1_IdlePositionX();
                            Move_Bonder1_IdlePositionY();
                            return;
                        }
                        else
                        {
                            Move_Bonder1_ReadyPositionZ();
                            return;
                        }

                    }
                    else  // XY 맞으면.
                    {
                        if (Is_Bonder1_IdleCenterZ()) // Z 맞으면.
                        {
                            if (IdleTicCount < cDEF.Work.Bonder1.IdleTime)
                            {
                                if (IdleTicCount > cDEF.Work.Bonder1.JettingTime && JettingIO)//cDEF.Run.Digital.Output[cDO.Bonder_Dispensor_1_Jetting])
                                    JettingIO = false;              //cDEF.Run.Digital.Output[cDO.Bonder_Dispensor_1_Jetting] = false;
                                IdleTicCount++;
                                return;
                            }
                            else
                            {
                                JettingIO = true;
                                //cDEF.Run.Digital.Output[cDO.Bonder_Dispensor_1_Jetting] = true;
                                IdleTicCount = 0;
                                return;
                            }
                        }
                        else
                        {
                            Move_Bonder1_IdlePositionZ();
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
                    Move_Bonder1_ReadyPositionZ();
                    Step++;
                    break;
                case 6:
                    if (Information.IndexData.Status == eLensIndexStatus.LensHeightFinish
                   && Information.CheckVisionFinish)
                    {
                        Information.CheckVisionFinish = false;
                        Information.VisionResultX = 0;
                        Information.VisionResultY = 0;
                    }
                    Step++;
                    break;
                case 7:
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Initialize] End", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    //20088 jy Idle 상태로 인한 프로그램 수정 (에러메세지 띄우는 방향으로 변경)
                    //if (cDEF.Work.Project.GlobalOption.UseIdle1)
                    //    cDEF.Work.Project.GlobalOption.UseIdle1 = false;
                    Step++;
                    break;

                case 1:
                    if(cDEF.Work.DispSensor.DispenserType == 0)
                        cDEF.Dispenser1.Send_ParamRead();
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < 500)
                        break;
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                    {
                        PJS100.EWorkMode jetmode = PJS100.EWorkMode.None;
                        if (cDEF.Work.Project.GlobalOption.JettingMode1 == 0)
                        {
                            //포인트
                            jetmode = PJS100.EWorkMode.Point;
                        }
                        else
                        {
                            jetmode = PJS100.EWorkMode.Line;
                        }
#if !Notebook
                        if (jetmode != cDEF.Dispenser1.WorkModeValue)
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 204;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 204, "[BONDER 1] (Check PJ Controller) WorkMode Notmatch");
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 204, "");
                            return false;
                        }
#endif
                    }
                    Step++;
                    break;

                case 3:
                    if (cDEF.Run.Mode == TRunMode.Main_Run)
                        Information.CleanJetCount = cDEF.Work.Recipe.DummyPeriodCount1;
                    Step++;
                    break;
                case 4:
                    if (cDEF.Work.DispSensor.DispenserType == 2)
                    {
                        if (!cDEF.TJV_1.Connect)
                        {
                            if (!cDEF.TJV_1.Init(cDEF.Work.Recipe.TJV_IP_1))
                            {
                                cDEF.Run.LogWarning(cLog.RunBonder1 + 295, "[BONDER 1] (Check PJ Controller) Comm Check");
                                return false;

                            }
                        }
                    }
                    Step++;
                    break;
                case 5:
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[To-Run] Done.", true);
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
                        Move_Bonder1_ReadyPositionZ();
                    }
                    else
                        Information.MoveSample = false;
                    Step++;
                    break;
                case 1:
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[To-Stop] Done.", true);
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
                case TRunBonder1Mode.CheckCamera:
                    Running_CheckCamera();
                    break;

                case TRunBonder1Mode.Jetting:
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                        Running_Jetting();
                    else if (cDEF.Work.DispSensor.DispenserType == 1)
                        Running_Jetting_Air();
                    else if (cDEF.Work.DispSensor.DispenserType == 2)
                        Running_Jetting_TJV();
                    break;

                case TRunBonder1Mode.PtnLineJetting:
                    if(cDEF.Work.DispSensor.DispenserType == 0)
                        Running_PtnLineJetting();
                    else if (cDEF.Work.DispSensor.DispenserType == 1)
                        Running_PtnLineJetting_Air();
                    else if (cDEF.Work.DispSensor.DispenserType == 2)
                        Running_PtnLineJetting_TJV();
                    break;

                case TRunBonder1Mode.PtnArcJetting:
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                        Running_PtnArcJetting();
                    else if (cDEF.Work.DispSensor.DispenserType == 1)
                        Running_PtnArcJetting_Air();
                    else if (cDEF.Work.DispSensor.DispenserType == 2)
                        Running_PtnArcJetting_TJV();
                    break;

                case TRunBonder1Mode.Clean:
                    Running_Cleaning();
                    break;

                case TRunBonder1Mode.MoveSample:
                    Running_Move_Sample();
                    break;

                case TRunBonder1Mode.MoveCam:
                    Running_Move_Cam();
                    break;

                case TRunBonder1Mode.Touch:
                    Running_Touch();
                    break;

                case TRunBonder1Mode.TipCleaning:
                    Running_TipCleaning();
                    break;

                case TRunBonder1Mode.DummyDisChage:
                    Running_Dummy();
                    break;

                case TRunBonder1Mode.MoveReadyPos:
                    Running_ReadyPos();
                    break;

                case TRunBonder1Mode.AutoCal:
                    Running_AutoCal();
                    break;

                case TRunBonder1Mode.GapMeasure:
                    Running_Gap_Measure();
                    break;

                case TRunBonder1Mode.GapTouchAdjust:
                    Running_Gap_TouchAdjust();
                    break;

            }
        }
        
        protected override void ProcMain()
        {
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if (cDEF.Work.Project.GlobalOption.UseBonder1)
                {
                    // Jotting Count Over
                    if (Information.JettingCount >= cDEF.Work.Bonder1.JettingCount)
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 206;
                        //cDEF.Run.LogWarning(cLog.RunBonder1 + 206, "Bonder1 Jetting Count Over.");
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 206, "");
                        return;
                    }

                    if (cDEF.Work.Project.GlobalOption.UseTipClean1 || cDEF.Work.Project.GlobalOption.UseDummy1)
                    {
                        if (Information.CleanJetCount >= cDEF.Work.Recipe.DummyPeriodCount1)
                        {
                            if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                                return;

                            Mode = TRunBonder1Mode.TipCleaning;
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Tip Cleaning Start", true);
                            return;
                        }
                    }
                }
                if (Information.IndexData.Status == eLensIndexStatus.LensHeightFinish
                    && !Information.CheckVisionFinish)
                {
                    if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                        return;

                    cDEF.Tact.Bonder1.Start();
                    cDEF.Tact.Bonder1Cam.Start();
                    Mode = TRunBonder1Mode.CheckCamera;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Check Vision Start", true);
                    return;
                }

                if (cDEF.Work.Project.GlobalOption.UseGap && cDEF.Work.DispSensor.DispenserType != 0)
                {
                    // Gap Measure
                    if (Information.IndexData.Status == eLensIndexStatus.LensHeightFinish
                        && Information.CheckVisionFinish
                        && !Information.GapMeasureFinish)
                    {
                        //if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                        //    return;

                        cDEF.Tact.Bonder1GapMesure.Start();
                        Mode = TRunBonder1Mode.GapMeasure;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Gap Measure Start", true);
                        return;

                    }
                }
                else
                {
                    Information.GapMeasureFinish = true;
                }

                if (Information.IndexData.Status == eLensIndexStatus.LensHeightFinish
                    && Information.CheckVisionFinish
                    && Information.GapMeasureFinish)
                {


                    if (cDEF.Work.Project.GlobalOption.JettingMode1 == 0)    // Point
                    {
                        if (!IsFinish())
                        {                            
                            Mode = TRunBonder1Mode.Jetting;
                            cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1]Index:{Information.IndexData.Index + 1} Jetting Start", true);
                            cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting Radius: {WorkJetData.Angle}  Finish:{WorkJetData.Finish}", true);
                            return;
                        }
                    }
                    else if (cDEF.Work.Project.GlobalOption.JettingMode1 == 1)    // Line
                    {
                        if (!IsPatternFinish())
                        {
                            Mode = TRunBonder1Mode.PtnLineJetting;
                            cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Index:{Information.IndexData.Index + 1} Pattern Line Jetting Start", true);
                            return;
                        }

                    }
                    else if (cDEF.Work.Project.GlobalOption.JettingMode1 == 2)    // Arc
                    {

                        if (!IsPatternArcFinish())
                        {
                            Mode = TRunBonder1Mode.PtnArcJetting;
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Pattern ARC Jetting Start", true);
                            return;
                        }
                    }
                }
            }


            if(cDEF.Run.Mode == TRunMode.Manual_Bonder1CheckVision)
            {
                cDEF.Tact.Bonder1.Start();
                Mode = TRunBonder1Mode.CheckCamera;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Check Vision Start.", true);
                return;
            }

            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
            {
                if (cDEF.Work.Project.GlobalOption.JettingMode1 == 0)    // Point
                {
                    if (!IsFinish())
                    {
                        Mode = TRunBonder1Mode.Jetting;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Jetting Start.", true);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting Radius: {WorkJetData.Radius}  Finish:{WorkJetData.Finish}", true);
                        return;
                    }
                    else
                    {
                        if (!Is_Bonder1_ReadyPositionZ())
                        {
                            Move_Bonder1_ReadyPositionZ();
                            return;
                        }
                        else
                        {
                           
                            Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                            Information.CheckVisionFinish = false;
                            cDEF.Work.Bonder1Point.JettingDataInit(); //젯팅 2번 동작 방지
                            Information.JettingCount++;
                            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                            return;
                        }

                    }
                }
                else if (cDEF.Work.Project.GlobalOption.JettingMode1 == 1)    // Line
                {

                    if (!IsPatternFinish())
                    {

                        Mode = TRunBonder1Mode.PtnLineJetting;


                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Pattern Line Jetting Start", true);
                        return;
                    }
                    else
                    {
                        if (!Is_Bonder1_ReadyPositionZ())
                        {
                            Move_Bonder1_ReadyPositionZ();
                            return;
                        }
                        else
                        {
                            cDEF.Tact.Bonder1.GetTact();
                            Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                            Information.CheckVisionFinish = false;
                            Information.JettingCount++;
                            cDEF.Work.Bonder1Pattern.JettingPatternInit(); //젯팅 2번 동작 방지

                            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                            return;
                        }

                    }
                }
                else if (cDEF.Work.Project.GlobalOption.JettingMode1 == 2)    // Arc
                {

                    if (!IsPatternArcFinish())
                    {

                        Mode = TRunBonder1Mode.PtnArcJetting;


                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Pattern ARC Jetting Start", true);
                        return;
                    }
                    else
                    {
                        if (!Is_Bonder1_ReadyPositionZ())
                        {
                            Move_Bonder1_ReadyPositionZ();
                            return;
                        }
                        else
                        {
                            cDEF.Tact.Bonder1.GetTact();
                            Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                            Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                            Information.CheckVisionFinish = false;
                            Information.JettingCount++;
                            cDEF.Work.Bonder1ARC.JettingPatternInit(); //젯팅 2번 동작 방지

                            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                            return;
                        }

                    }
                }
            }

            // Manual Move
            if(cDEF.Run.Mode == TRunMode.Manual_Bonder1_MoveSample)
            {
                Mode = TRunBonder1Mode.MoveSample;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Move Sample Start.", true); 
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_MoveCam)
            {
                Mode = TRunBonder1Mode.MoveCam;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Move Camera Start.", true); 
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_AutoCal)
            {
                Mode = TRunBonder1Mode.AutoCal;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Auto Calibration Start.", true);
                return;
            }
            
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_Touch)
            {
                Mode = TRunBonder1Mode.Touch;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Touch Start.", true);
                return;
            }

            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_Clean)
            {
                Mode = TRunBonder1Mode.Clean;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Clean Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_TipClean)
            {
                Mode = TRunBonder1Mode.TipCleaning;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Tip Clean Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_Dummy)
            {
                Mode = TRunBonder1Mode.DummyDisChage;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 2 - Manual] Dummy Discharge Start.", true);
                return;
            }
            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_Ready)
            {
                Mode = TRunBonder1Mode.MoveReadyPos;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Move Ready Pos Start.", true);
                return;
            }

            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_GapMeasure)
            {
                Mode = TRunBonder1Mode.GapMeasure;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Move Gap Measure Start.", true);
                return;
            }

            if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_GapTouchAdjust)
            {
                Mode = TRunBonder1Mode.GapTouchAdjust;
                cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1 - Manual] Move Gap Touch Adjust Start.", true);
                return;
            }
        }
        public bool IsFinish()
        {
            foreach (JettingData jd in cDEF.Work.Bonder1Point.JetData)
            {
                if (jd.Enable)
                {
                    if (jd.Finish == false)
                    {
                        WorkJetData = jd;
                        return false;
                    }
                    //else
                    //    jd.Finish = false;
                }
            }
            return true;
        }

        public bool IsPatternFinish()
        {
            foreach (JettingPatternLineData jd in cDEF.Work.Bonder1Pattern.JetPatternLineData)
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
            foreach (JettingPatternArcData jd in cDEF.Work.Bonder1ARC.JetPatternArcData)
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    InspRetryCount = 0;
                    ReadyRetryCount = 0;
                    Step++;
                    break;

                case 1:
                    if (!cDEF.Work.Project.GlobalOption.UseBonder1)
                    {
                        cDEF.Tact.Bonder1.GetTact();
                        Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;

                        if (cDEF.Run.Mode == TRunMode.Manual_Bonder1CheckVision)
                            cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                        Mode = TRunBonder1Mode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Check Vision End - [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
                        return;
                    }
                    
                    Move_Bonder1_CamCenterX();
                    Move_Bonder1_CamCenterY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                    Step++;
                    break;

                case 2:
                    Move_Bonder1_CamPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 3:
                    Information.VisionResultX = 0;
                    Information.VisionResultY = 0;
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder1.Bonder1GrabDelay)
                        break;
                    if (!cDEF.Work.Project.GlobalOption.VisionCheck)
                    {
                        Step = 7;
                        return;
                    }
                    cDEF.Visions.Sendmsg(eVision.V4_Ready);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Send Vision V4 Ready (Check Vision).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV4_Ready.Status != CmmStatus.Ok)
                            break;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Receive Vision V4 Ready OK (Check Vision).", true);
                        Step++;
                    }
                    else
                    {
                        if (ReadyRetryCount < cDEF.Work.Recipe.Bonder1RetryCount)
                        {
                            ReadyRetryCount++;
                            Step = 2;
                            break;
                        }
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 200;
                        //cDEF.Run.LogWarning(cLog.RunBonder1 + 200, "[BONDER 1] (Check Camera) V4 Ready Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 200, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] (Check Camera) V4 Ready Vision Time Out.", true);
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 5:
                    cDEF.Visions.Sendmsg(eVision.V4_Complete);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Send Vision V4 Complete (Check Vision).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 6:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV4_Complete.Status != CmmStatus.Ok)
                            break;

                        if (cDEF.Visions.ackV4_Complete.exist)
                        {
                            Information.VisionResultX = (int)(cDEF.Visions.ackV4_Complete.x);
                            Information.VisionResultY = (int)(cDEF.Visions.ackV4_Complete.y);
                            cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Receive Vision V4 Complete OK (Check Vision) X : {Information.VisionResultX} Y : {Information.VisionResultY}.", true);
                            Step++;
                        }
                        else
                        {
                            if (InspRetryCount < cDEF.Work.Recipe.Bonder1RetryCount)
                            {
                                InspRetryCount++;
                                ReadyRetryCount = 0;
                                Step = 2;
                                break;
                            }
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 201;
                            //cDEF.Run.LogWarning(cLog.RunBonder1 + 201, "[BONDER 1] (Check Camera) V4 Complete Vision TimeOut");
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 201, "");
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] (Check Camera) V4 Compelte Vision Time Out.", true);
                            Mode = TRunBonder1Mode.Stop;
                            return;
                        }
                    }
                    else
                    {
                        if (InspRetryCount < cDEF.Work.Recipe.Bonder1RetryCount)
                        {
                            InspRetryCount++;
                            ReadyRetryCount = 0;
                            Step = 2;
                            break;
                        }
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 201;
                        //cDEF.Run.LogWarning(cLog.RunBonder1 + 201, "[BONDER 1] (Check Camera) V4 Complete Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 201, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] (Check Camera) V4 Compelte Vision Time Out.", true);
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 7:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Bonder 1 Ready Position Z.", true);
                    Step++;
                    break;

                case 8:
                    Information.CheckVisionFinish = true;
                    Information.GapMeasureFinish = false;
                    cDEF.Work.Bonder1Point.JettingDataInit();
                    cDEF.Work.Bonder1Pattern.JettingPatternInit();
                    cDEF.Tact.Bonder1Cam.GetTact();
                    Information.IndexData.TT_Bonder1Cam = cDEF.Tact.Bonder1Cam.CycleTime;
                    Step++;
                    break;

                case 9:
           
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1CheckVision)
                    {
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                        Information.IndexData.Status = eLensIndexStatus.LensHeightFinish;
                    }
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Check Vision End - Unit Tact Time : [{cDEF.Tact.Bonder1Cam.CycleTime.ToString("N3")}]", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    Move_Bonder1_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position X Y.", true); 
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
                    if (cDEF.Work.Project.GlobalOption.UsePlusenum1)
                    {
                        if (cDEF.Dispenser1.PluseNumValue != WorkJetData.PluseNum)
                        {
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue,
                                cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue,
                                (int)cDEF.Dispenser1.PCTValue, WorkJetData.PluseNum,
                                (int)cDEF.Dispenser1.WorkModeValue, (int)cDEF.Dispenser1.VoltageValue);

                            FCalc = Environment.TickCount;
                        }
                    }
                    Step++;
                    break;

                case 4:
                    if (cDEF.Work.Project.GlobalOption.UsePlusenum1)
                    {
                        if (Environment.TickCount - FCalc < 100)
                            break;

                        // 피에조 변경 데이터 읽기
                        cDEF.Dispenser1.Send_ParamRead();
                        FCalc = Environment.TickCount;
                    }
                    Step++;
                    break;

                case 5:
                    if (cDEF.Work.Project.GlobalOption.UsePlusenum1)
                    {
                        if (Environment.TickCount - FCalc < 1000)
                        {
                            if (cDEF.Dispenser1.PluseNumValue != WorkJetData.PluseNum)
                            {
                                break;
                            }
                            cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Jetting Pluse Num Value {WorkJetData.PluseNum}", true);
                        }
                        else
                        {
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue,
                                cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue,
                                (int)cDEF.Dispenser1.PCTValue, WorkJetData.PluseNum,
                                (int)cDEF.Dispenser1.WorkModeValue, (int)cDEF.Dispenser1.VoltageValue);

                            cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] send Jetting Pluse Num Value {WorkJetData.PluseNum}", true);
                        }
                    }
                    Step++;
                    break;

                case 6:
                    Move_Bonder1_JetUpPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position Z. ", true);
                    Step++;
                    break;

                case 7:
                    Move_Bonder1_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_JetPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position Z.", true);
                    Step++;
                    break;
                
                case 9:
                    JettingIO = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 10:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder1.JettingTime)
                        break;
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 11:
                    if (Environment.TickCount - FCalc < WorkJetData.Delay)
                        break;

                   // Move_Bonder1_JetUpPositionZ(WorkJetData);
                    //cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jet Up Position Z.", true);
                    Step++;
                    break;

                case 12:
                    WorkJetData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting Radius: {WorkJetData.Angle} Finish:{WorkJetData.Finish}", true);
                    Step = 2;    // 여기서 loop
                    break;


                case 13:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 14:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime; 
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1Point.JettingDataInit(); //젯팅 2번 동작 방지
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
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 17;
                    }
                    break;

                case 16:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder1Jetting)
                    //{
                    //    Move_Bonder1_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 17:
                    Mode = TRunBonder1Mode.Stop;
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    CAXM.AxmContiWriteClear(0);
                    Step++;
                    break;

                case 2:
                    if(!IsPatternFinish())
                    {
                        Move_Bonder1_PtnLine_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;


                case 3:
                    Move_Bonder1_PtnLine_FristJetPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head First Jet Position Z.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder1_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = GetContiQueue();
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(0);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        //ContiStart = Move_Bonder1_PtnLine_JettingPositionXYZ(Information.VisionResultX, Information.VisionResultY);
                        ContiStart = Move_Bonder1_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnLineData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting  Finish:{WorkJetPtnLineData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1Pattern.JettingPatternInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;

                        if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 14;
                    }
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder1Jetting)
                    //{
                    //    Move_Bonder1_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    CAXM.AxmContiWriteClear(0);
                    Step++;
                    break;

                case 2:
                    if (!IsPatternArcFinish())
                    {
                        Move_Bonder1_PtnArc_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;


                case 3:
                    Move_Bonder1_PtnArc_FristJetPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head First Jet Position Z.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder1_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = GetContiQueue();
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(0);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        ContiStart = Move_Bonder1_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Re Conti Start OK", true);
                    }
                    Step++;
                    break;
                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnArcData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting  Finish:{WorkJetPtnArcData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1ARC.JettingPatternInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;

                        if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 14;
                    }
                    break;

                case 13:
                    
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    if(cDEF.Run.Digital.Input[cDI.Air_Dispenser_1_Alarm1])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 215;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 215, "Air Dispenser Alarm Type 1");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }

                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_1_Alarm2])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 216;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 216, "Air Dispenser Alarm Type 2");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }

                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    Move_Bonder1_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position X Y.", true);
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
                    cDEF.DispenserEcm1.PressTime = WorkJetData.DpTime;
                    cDEF.DispenserEcm1.CMDMode = clsSuperEcm3.ECMDMode.SetValue;
                    cDEF.DispenserEcm1.SetValueStart();
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if(Environment.TickCount - FCalc < 5000)
                    {
                        if (!cDEF.DispenserEcm1.SetValueFinish)
                            break;
                        if(cDEF.DispenserEcm1.CommError)
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                            Mode = TRunBonder1Mode.Stop;
                            return;
                        }
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 211;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 211, "Dispenser 1 Comm Time Out");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 5:
                    Step++;
                    break;

                case 6:
                    Move_Bonder1_JetUpPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position Z. ", true);
                    Step++;
                    break;

                case 7:
                    Move_Bonder1_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_JetPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position Z.", true);
                    Step++;
                    break;

                case 9:
                    JettingIO = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 10:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder1.JettingTime)
                        break;
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 11:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Bonder1ECM_DSO])
                            break;
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 213;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 213, "[Bonder 1] DSO Signal is not Off - Time Out");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < WorkJetData.Delay)
                        break;
                    WorkJetData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting Radius: {WorkJetData.Angle} Finish:{WorkJetData.Finish}", true);
                    Step = 2;    // 여기서 loop
                    break;


                case 13:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 14:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1Point.JettingDataInit(); //젯팅 2번 동작 방지
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
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 17;
                    }
                    break;

                case 16:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder1Jetting)
                    //{
                    //    Move_Bonder1_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 17:
                    Mode = TRunBonder1Mode.Stop;
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_1_Alarm1])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 215;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 215, "Air Dispenser Alarm Type 1");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }

                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_1_Alarm2])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 216;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 216, "Air Dispenser Alarm Type 2");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    CAXM.AxmContiWriteClear(0);
                    Step++;
                    break;

                case 2:
                    if (!IsPatternFinish())
                    {
                        Move_Bonder1_PtnLine_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;


                case 3:
                    Move_Bonder1_PtnLine_FristJetPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head First Jet Position Z.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder1_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = GetContiQueue();
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(0);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        //ContiStart = Move_Bonder1_PtnLine_JettingPositionXYZ(Information.VisionResultX, Information.VisionResultY);
                        ContiStart = Move_Bonder1_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnLineData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting  Finish:{WorkJetPtnLineData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1Pattern.JettingPatternInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;
                        
                        if(cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 14;
                    }
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder1Jetting)
                    //{
                    //    Move_Bonder1_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_1_Alarm1])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 215;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 215, "Air Dispenser Alarm Type 1");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }

                    if (cDEF.Run.Digital.Input[cDI.Air_Dispenser_1_Alarm2])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 216;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 216, "Air Dispenser Alarm Type 2");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    CAXM.AxmContiWriteClear(0);
                    Step++;
                    break;

                case 2:
                    if (!IsPatternArcFinish())
                    {
                        Move_Bonder1_PtnArc_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
                        Step++;
                    }
                    else
                    {
                        Step = 10;
                    }
                    break;


                case 3:
                    Move_Bonder1_PtnArc_FristJetPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head First Jet Position Z.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder1_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = GetContiQueue();
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(0);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        ContiStart = Move_Bonder1_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnArcData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting  Finish:{WorkJetPtnArcData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1ARC.JettingPatternInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;

                        if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 14;
                    }
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder1Jetting)
                    //{
                    //    Move_Bonder1_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    cDEF.TJV_1.PDDStopSpitting();
                    Move_Bonder1_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position X Y.", true);
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
                    if (cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 0))
                    {
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }                 
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_1.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_1) == 1)
                    {
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_1.PDDStartSpitting(cDEF.Work.Recipe.Hz_1, cDEF.Work.Recipe.nDrop_1) == 1)
                        Step++;
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 6:
                    if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                        cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = true;
                    Move_Bonder1_JetUpPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position Z. ", true);
                    Step++;
                    break;

                case 7:
                    Move_Bonder1_JetPositionXY(Information.VisionResultX, Information.VisionResultY, WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position X Y.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_JetPositionZ(WorkJetData);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Jetting Position Z.", true);
                    Step++;
                    break;

                case 9:
                    JettingIO = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 10:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder1.JettingTime)
                        break;
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 11:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Bonder1ECM_DSO])
                            break;
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 213;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 213, "[Bonder 1] DSO Signal is not Off - Time Out");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < WorkJetData.Delay)
                        break;
                    WorkJetData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting Radius: {WorkJetData.Angle} Finish:{WorkJetData.Finish}", true);
                    Step = 2;    // 여기서 loop
                    break;


                case 13:
                    if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                        cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = false;
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 14:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1Point.JettingDataInit(); //젯팅 2번 동작 방지
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
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 17;
                    }
                    break;

                case 16:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder1Jetting)
                    //{
                    //    Move_Bonder1_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 17:
                    Mode = TRunBonder1Mode.Stop;
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    cDEF.TJV_1.PDDStopSpitting();
                    Move_Bonder1_ReadyPositionZ();
                    if (cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 0))
                    {
                      
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }                
                    break;

                case 1:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_1.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_1) == 1)
                    {
                        CAXM.AxmContiWriteClear(0);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 2:
                    if (!IsPatternFinish())
                    {
                        Move_Bonder1_PtnLine_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
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
                    if (cDEF.TJV_1.PDDStartSpitting(cDEF.Work.Recipe.Hz_1, cDEF.Work.Recipe.nDrop_1) == 1)
                    {
                        if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                            cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = true;
                        Move_Bonder1_PtnLine_FristJetPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head First Jet Position Z.", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 4:
                    Move_Bonder1_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                    //ContiStart = false;
                    //ContiStart = GetContiQueue();
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Start Head Jetting Position X Y Z. ", true);
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 50)
                        break;
                    if (!GetContiQueue())
                    {
                        ContiStart = false;
                        CAXM.AxmContiWriteClear(0);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        //ContiStart = Move_Bonder1_PtnLine_JettingPositionXYZ(Information.VisionResultX, Information.VisionResultY);
                        ContiStart = Move_Bonder1_PtnLine_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnLineData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting  Finish:{WorkJetPtnLineData.Finish}", true);
                    Step = 2;
                    break;

                case 10:

                    if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                        cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = false;
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1Pattern.JettingPatternInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;

                        if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 14;
                    }
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder1Jetting)
                    //{
                    //    Move_Bonder1_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    cDEF.TJV_1.PDDStopSpitting();
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    if (cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 0))
                    {
                        CAXM.AxmContiWriteClear(0);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 2:
                    if (!IsPatternArcFinish())
                    {
                        if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                            cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = true;
                        Move_Bonder1_PtnArc_FristJettingPositionXY(Information.VisionResultX, Information.VisionResultY);
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
                    if (cDEF.TJV_1.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_1) == 1)
                    {
                        FCalc = Environment.TickCount;
                        Move_Bonder1_PtnArc_FristJetPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head First Jet Position Z.", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 100)
                        break;
                    if (cDEF.TJV_1.PDDStartSpitting(cDEF.Work.Recipe.Hz_1, cDEF.Work.Recipe.nDrop_1) == 1)
                    {
                        Move_Bonder1_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        FCalc = Environment.TickCount;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Start Head Jetting Position X Y Z. ", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.TJV_1.Connect = false;
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                        Mode = TRunBonder1Mode.Stop;
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
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Write Clear", true);
                        FCalc = Environment.TickCount;
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Conti Start OK", true);
                    Step++;
                    break;

                case 6:
                    if (!ContiStart)
                    {
                        if (Environment.TickCount - FCalc < 200)
                            break;

                        ContiStart = Move_Bonder1_PtnArc_JettingPositionXYZ_DistIO(Information.VisionResultX, Information.VisionResultY);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. ", true);
                        if (!ContiStart)
                        {
                            //확인 후 알람 띄우기
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move ReStart Head Jetting Position X Y Z. Fail ", true);
                        }
                        else
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Re Conti Start OK", true);
                    }
                    Step++;
                    break;

                case 7:
                    JettingIO = false;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Dispensor Jetting Off.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 9:
                    WorkJetPtnArcData.Finish = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1].Jetting  Finish:{WorkJetPtnArcData.Finish}", true);
                    Step = 2;
                    break;

                case 10:
                    if (!cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                        cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = false;
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    cDEF.Tact.Bonder1.GetTact();
                    Information.IndexData.TT_Bonder1 = cDEF.Tact.Bonder1.CycleTime;
                    Information.IndexData.Status = eLensIndexStatus.Bonder1Finish;
                    Information.CheckVisionFinish = false;
                    Information.JettingCount++;
                    Information.CleanJetCount++;
                    cDEF.Work.Bonder1ARC.JettingPatternInit(); //젯팅 2번 동작 방지
                    Step++;
                    break;

                case 12:
                    if (Environment.TickCount - FCalc < 5000)
                    {
                        if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                            return;

                        if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                        {
                            Move_Bonder1_CamCenterX();
                            Move_Bonder1_CamCenterY();
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera CenterPosition X Y.", true);
                        }

                        Step++;
                    }
                    else
                    {
                        Step = 14;
                    }
                    break;

                case 13:
                    //if (cDEF.Run.Mode != TRunMode.Manual_Bonder1Jetting)
                    //{
                    //    Move_Bonder1_CamPositionZ();
                    //    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    //}
                    Step++;
                    break;

                case 14:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1Jetting)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Jetting End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3")}].", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 2:
                    //Clean XY 위치 이동
                    Move_Bonder1_CleanPositionXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Clean Position X, Y.", true);
                    Step++;
                    break;

                case 3:
                    //Clean Z 위치 이동
                    Move_Bonder1_CleanPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Clean Position Z.", true);
                    Step++;
                    break;

                case 4:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_Clean)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder1 - Clean Tip] End.", true);
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

            if(cDEF.Run.DetailMode == TfpRunningMode.frmToStop)
            {
                Mode = TRunBonder1Mode.Stop;
                return;
            }

            switch (Step)
            {
                case 0:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    Move_Bonder1_TouchPositionX();
                    Move_Bonder1_TouchPositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Touch Position X Y.", true);
                    Step++;
                    break;

                case 2:
                    Move_Bonder1_TouchPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Touch Position Z.", true);
                    Step++;
                    break;

                case 3:
                    if(HeadZ.ActualPosition < cDEF.Work.TeachBonder1.TouchLimitZ)
                    {
                        Move_Bonder1_RelativeTouchStep();
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Touch Relative Position Z.", true);
                        Step++;
                    }
                    else
                    {
                        Move_Bonder1_ReadyPositionZ();
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 202;
                        //cDEF.Run.LogWarning(cLog.RunBonder1 + 202, "[BONDER 1] TOUCH LIMIT!");
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 202, "");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 4:
                    if (cDEF.Run.Digital.Input[cDI.Bonding_Head_1_Nozzle_Height_Touch_Sensor])
                        Step++;
                    else
                        Step = 3;
                    break;

                case 5:
                    Move_Bonder1_ReadyPositionZ();
                    //MessageBox.Show($"Touch Z : {(HeadZ.ActualPosition - cDEF.Work.TeachBonder1.TouchOffsetZ) / 1000.0}");
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1 - Touch] Touch Z : {(HeadZ.ActualPosition - cDEF.Work.TeachBonder1.TouchOffsetZ) / 1000.0} ", true);
                    cDEF.Work.TeachBonder1.JettingPositionZ = (int)HeadZ.ActualPosition - cDEF.Work.TeachBonder1.TouchOffsetZ;
                    cDEF.Work.TeachBonder1.Save();
                    OnGrid_Display();
                    Step++;
                    break;


                case 6:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_Touch)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder1 - Touch] End.", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    // Gap Check
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Move_Bonder1_SampleGapPositionXY();
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Sample Gap Position XY.", true);
                    }
                    Step++;
                    break;

                case 2:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Move_Bonder1_GapMeasure_Z(true);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Sample Gap Position Z.", true);
                    }
                    Step++;
                    break;

                case 3:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Information.GapMeasureValue = cDEF.Serials.Value_Bond1;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Gap Measure : {Information.GapMeasureValue / 1000.0}.", true);
                    }
                    Step++;
                    break;

                case 4:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 5:
                    Move_Bonder1_SamplePositionX();
                    Move_Bonder1_SamplePositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Smaple Position X Y.", true);
                    Step++;
                    break;

                case 6:
                    double dValue = Math.Abs(Information.GapMeasureValue);
                    if (dValue > 5000)
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 217;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 217, "");
                       // cDEF.Run.LogWarning(cLog.RunBonder1 + 217, "Gap Measure Limit Over");
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    Move_Bonder1_SamplePositionZ(Information.GapMeasureValue);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Sample Position Z.", true);
                    Step++;
                    break;

                case 7:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.DispenserEcm1.SetMode == 0)
                    {
                        cDEF.DispenserEcm1.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                        cDEF.DispenserEcm1.PressTime = cDEF.Work.Bonder1.JettingTime;
                        cDEF.DispenserEcm1.SetValueStart();
                        FCalc = Environment.TickCount;
                    }
                    Step++;
                    break;

                case 8:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.DispenserEcm1.SetMode == 0)
                    {
                        if (Environment.TickCount - FCalc < 5000)
                        {
                            if (!cDEF.DispenserEcm1.SetValueFinish)
                                break;
                            if (cDEF.DispenserEcm1.CommError)
                            {
                                cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                                cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                                Mode = TRunBonder1Mode.Stop;
                                return;
                            }
                            Step++;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 211;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 211, "Dispenser 1 Comm Time Out");
                            Mode = TRunBonder1Mode.Stop;
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 11:
                    Move_Bonder1_CameraRelativeXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Relative X Y", true);
                    Step++;
                    break;

                case 12:
                    Move_Bonder1_CamPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    Step++;
                    break;

                case 13:
                    Information.VisionResultX = 0;
                    Information.VisionResultY = 0;
                    cDEF.Visions.Sendmsg(eVision.V7_Ready);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Send Vision V7 Ready (Auto Calibration).", true);

                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 14:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV7_Ready.Status != CmmStatus.Ok)
                            break;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Receive Vision V7 Ready OK (Auto Calibration).", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 207;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 207, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] (Auto Calibration) V7 Ready Vision Time Out.", true);
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 15:
                    cDEF.Visions.Sendmsg(eVision.V7_Complete);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Send Vision V7 Complete (Auto Calibration).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 16:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV7_Complete.Status != CmmStatus.Ok)
                            break;

                        if (cDEF.Visions.ackV7_Complete.exist)
                        {
                            Information.VisionResultX = (int)(cDEF.Visions.ackV7_Complete.x);
                            Information.VisionResultY = (int)(cDEF.Visions.ackV7_Complete.y);
                            cDEF.TaskLogAppend(TaskLog.Bonder2, $"[Bonder 2] Receive Vision V7 Complete OK (Check Vision) X : {Information.VisionResultX} Y : {Information.VisionResultY}.", true);
                            Step++;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 208;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 208, "");
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Vision V7 (Auto Calibration) Auto Calibration Fail.", true);
                            Mode = TRunBonder1Mode.Stop;
                            return;
                        }
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 209;
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 209, "");
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] (Auto Calibration) V7 Compelte Vision Time Out.", true);
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    break;

                case 17:
                    if (Math.Abs(Information.VisionResultX) > cDEF.Work.TeachBonder1.AutoCalSpec || Math.Abs(Information.VisionResultY) > cDEF.Work.TeachBonder1.AutoCalSpec)
                    {
                        if(AutoCalCount > cDEF.Work.TeachBonder1.AutoCalCount)
                        {
                            Move_Bonder1_ReadyPositionZ();
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 210;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 210, "Auto Cal Count Over.");
                            cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] (Auto Calibration) V7 Compelte Vision Time Out.", true);
                            Mode = TRunBonder1Mode.Stop;
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
                    cDEF.Work.TeachBonder1.CameraDistanceOffsetX = (int)cDEF.Run.Bonder1.HeadX.ActualPosition - cDEF.Work.TeachBonder1.SamplePosX;
                    cDEF.Work.TeachBonder1.CameraDistanceOffsetY = (int)cDEF.Run.Bonder1.HeadY.ActualPosition - cDEF.Work.TeachBonder1.SamplePosY;
                    cDEF.Work.TeachBonder1.Save();
                    OnGrid_Display();
                    Step++;
                    break;

                case 19:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_AutoCal)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Auto Calibration End", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:                                                                                                                                                                                                                                                                                                                                                                                                                                
                // Gap Check
                    if(cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Move_Bonder1_SampleGapPositionXY();
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Sample Gap Position XY.", true);
                    }
                    Step++;
                    break;

                case 2:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Move_Bonder1_GapMeasure_Z(true);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Sample Gap Position Z.", true);
                    }
                    Step++;
                    break;

                case 3:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        Information.GapMeasureValue = cDEF.Serials.Value_Bond1;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Gap Measure : {Information.GapMeasureValue / 1000.0}.", true);
                    }
                    Step++;
                    break;

                case 4:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 5:
                    Move_Bonder1_SamplePositionX();
                    Move_Bonder1_SamplePositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Smaple Position XY.", true);
                    Step++;
                    break;

                case 6:
                    if (cDEF.Work.DispSensor.DispenserType != 0)
                    {
                        double dValue = Math.Abs(Information.GapMeasureValue);
                        if (dValue > 5000)
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 217;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 217, "");
                           // cDEF.Run.LogWarning(cLog.RunBonder1 + 217, "Gap Measure Limit Over");
                            Mode = TRunBonder1Mode.Stop;
                            return;
                        }
                    }
                    else
                    {
                        Information.GapMeasureValue = 0;
                    }
                    Move_Bonder1_SamplePositionZ(Information.GapMeasureValue);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Sample Position Z.", true);
                    Step++;
                    break;

                case 7:
                    Information.MoveSample = true;
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_MoveSample)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Move Sample End.", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    Move_Bonder1_CameraRelativeXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Relative X Y", true);
                    Step++;
                    break;

                case 2:
                    Move_Bonder1_SampleVisionPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Camera Position Z.", true);
                    Step++;
                    break;

                case 3:
                    Information.MoveSample = true;
                    //cDEF.Tact.Bonder1.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_MoveCam)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Move Cam End - Cycle Time : [{cDEF.Tact.Bonder1.CycleTime.ToString("N3)")}.", true);
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
                    if (cDEF.Work.TeachBonder1.TipCleanCntY <= Information.CleanPitchYCount)
                    {
                        Information.CleanPitchYCount = 0;

                        cDEF.Run.SetAlarmID = cLog.RunBonder1 + 205;
                        //cDEF.Run.LogWarning(cLog.RunBonder1 + 205, "[Bonder 1] Cleaning Change Please.");
                        cDEF.Run.LogWarning(cLog.RunBonder1 + 205, "");
                        //cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Cleaning Change Please", true);
                        Mode = TRunBonder1Mode.Stop;

                        return;
                    }
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    if (!cDEF.Work.Project.GlobalOption.UseDummy1)
                    {
                        Step = 6;
                        break;
                    }
                    Step++;
                    break;

                case 2:
                    Move_Bonder1_IdlePositionX();
                    Move_Bonder1_IdlePositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Dummy Shot Position X,Y .", true);
                    TipClean.Backward();
                    Step++;
                    break;

                case 3:
                    Move_Bonder1_IdlePositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Dummy Shot Position Z .", true);
                    Step++;
                    break;

                case 4:
                    JettingIO = true;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Jetting IO : True.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < cDEF.Work.Recipe.DummyTime1)
                        break;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Jetting IO : False  Jetting Time :{Environment.TickCount - FCalc} ms", true);
                    JettingIO = false;
                    Step++;
                    break;

                case 6:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    if (!cDEF.Work.Project.GlobalOption.UseTipClean1)
                    {
                        Step = 11;
                        break;
                    }
                    Step++;
                    break;

                case 7:
                    Move_Bonder1_TipCleanPositionXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Clean Position X, Y.", true);
                    Step++;
                    break;

                case 8:
                    Move_Bonder1_TipCleanPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Clean Position Z.", true);
                    Step++;
                    break;

                case 9:
                    TipClean.Forward();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] TipClean Cylinder Forward.", true);
                    Step++;
                    break;

                case 10:
                    Information.CleanPitchYCount++;
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    TipClean.Backward();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] TipClean Cylinder Backward.", true);
                    Step++;
                    break;

                case 11:
                    Information.CleanJetCount = 0;
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_TipClean)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder1 - Clean Tip] End.", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.Work.Project.GlobalOption.JettingMode1 == 0)
                    {
                        cDEF.DispenserEcm1.SetMode = 1;
                        cDEF.DispenserEcm1.CMDMode = clsSuperEcm3.ECMDMode.ChangeMode;
                        cDEF.DispenserEcm1.SetValueStart();
                        FCalc = Environment.TickCount;
                    }
                    Step++;
                    break;

                case 2:
                    if(cDEF.Work.DispSensor.DispenserType != 0 && cDEF.Work.Project.GlobalOption.JettingMode1 == 0)
                    {
                        if(Environment.TickCount - FCalc < 5000)
                        {
                            if (!cDEF.DispenserEcm1.SetValueFinish)
                                break;
                            if (cDEF.DispenserEcm1.CommError)
                            {
                                cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                                cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                                Mode = TRunBonder1Mode.Stop;
                                return;
                            }
                            Step++;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 211;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 211, "Dispenser 1 Comm Time Out");
                            Mode = TRunBonder1Mode.Stop;
                            return;
                        }
                    }
                    break;

                case 3:
                    Move_Bonder1_IdlePositionX();
                    Move_Bonder1_IdlePositionY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Idle Position XY.", true);
                    Step++;
                    break;

                case 4:
                    Move_Bonder1_IdlePositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Idle Position Z.", true);
                    Step++;
                    break;

                case 5:
                    JettingIO = true;
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 6:
                    if (Environment.TickCount - FCalc < cDEF.Work.Recipe.DummyTime1)
                        break;

                    JettingIO = false;
                    Step++;
                    break;

                case 7:
                    //Ready 위치 이동
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 8:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.Work.Project.GlobalOption.JettingMode1 == 0)
                    {
                        cDEF.DispenserEcm1.SetMode = 0;
                        cDEF.DispenserEcm1.CMDMode = clsSuperEcm3.ECMDMode.ChangeMode;
                        cDEF.DispenserEcm1.SetValueStart();
                        FCalc = Environment.TickCount;
                    }

                    Step++;
                    break;

                case 9:
                    if (cDEF.Work.DispSensor.DispenserType != 0 && cDEF.Work.Project.GlobalOption.JettingMode1 == 0)
                    {
                        if (Environment.TickCount - FCalc < 5000)
                        {
                            if (!cDEF.DispenserEcm1.SetValueFinish)
                                break;
                            if (cDEF.DispenserEcm1.CommError)
                            {
                                cDEF.Run.SetAlarmID = cLog.RunBonder1 + 212;
                                cDEF.Run.LogWarning(cLog.RunBonder1 + 212, "Dispenser 1 Comm Err");
                                Mode = TRunBonder1Mode.Stop;
                                return;
                            }
                            Step++;
                        }
                        else
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 211;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 211, "Dispenser 1 Comm Time Out");
                            Mode = TRunBonder1Mode.Stop;
                            return;
                        }
                    }
                    break;

                case 10:                  
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_Dummy)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder1 - Dummy Discharge] End.", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);

                    Step++;
                    break;

                case 1:
                    Move_Bonder1_ReadyPositionX();
                    Move_Bonder1_ReadyPositionY();

                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position X Y.", true);
                    Step++;
                    break;

               
                case 2:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_Ready)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder1 - Ready Pos] End.", true);
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
                    if (!cDEF.Work.Project.GlobalOption.UseBonder1)
                    {
                        Step = 6;
                        break;

                    }
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    // 측정 위치 이동
                    Move_Bonder1_GapMeasureXY(Information.VisionResultX, Information.VisionResultY);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Gap Measure X Y", true);
                    Step++;
                    break;

                case 2:
                    // 측정 Z축 이동
                    Move_Bonder1_GapMeasure_Z(false);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Gap Measure Position Z.", true);
                    Step++;
                    break;


                case 3:
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder1.GapMeasureDelay)
                        break;

                    Information.GapMeasureValue = cDEF.Serials.Value_Bond1;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Gap Measure Value : {Information.GapMeasureValue}.", true);
                    Step++;
                    break;

                case 5:
                    // 측정 판단
                    if(Math.Abs(Information.GapMeasureValue) < cDEF.Work.Bonder1.GapOffsetLimitZ)
                    {
                        //OK
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Judge OK", true);
                    }
                    else
                    {
                        // NG
                        Information.IndexData.Status = eLensIndexStatus.LensHeightFail;
                        Information.IndexData.FailType = eFailType.LensHeightFail;
                        Information.GapMeasureValue = 0;
                        cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Judge NG. Gap Value : {Information.GapMeasureValue}, Spec : {cDEF.Work.Bonder1.GapOffsetLimitZ} " , true);
                        Move_Bonder1_ReadyPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                        if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_GapMeasure)
                        {
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 218, "Gap Measure NG");
                            cDEF.Run.DetailMode = TfpRunningMode.frmToStop;                        
                        }
                        Mode = TRunBonder1Mode.Stop;
                        return;
                    }
                    Step++;
                    break;

                case 6:
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 7:
                    Information.GapMeasureFinish = true;
                    cDEF.Tact.Bonder1GapMesure.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_GapMeasure)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder1] Gap Measure End - Cycle Time : [{cDEF.Tact.Bonder1GapMesure.CycleTime.ToString("N3)")}.", true);
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
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 1:
                    // 측정 위치 이동
                    Move_Bonder1_GapTouchPositionXY();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Gap Touch XY", true);
                    Step++;
                    break;

                case 2:
                    // 측정 Z축 이동
                    Move_Bonder1_GapMeasure_Z(true);
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Gap Measure Position Z.", true);
                    Step++;
                    break;


                case 3:
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < cDEF.Work.Bonder1.GapMeasureDelay)
                        break;

                    Information.GapMeasureValue = cDEF.Serials.Value_Bond1;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Gap Measure Value : {Information.GapMeasureValue}.", true);
                    Step++;
                    break;

                case 5:
                    // 측정 판단
                    if (Math.Abs(Information.GapMeasureValue) > 5)
                    {
                        if(Math.Abs(Information.GapMeasureValue) > 5000 )
                        {
                            cDEF.Run.SetAlarmID = cLog.RunBonder1 + 217;
                            cDEF.Run.LogWarning(cLog.RunBonder1 + 217, "Gap Measure Limit Over");
                            Mode = TRunBonder1Mode.Stop;
                            return;
                        }
                        HeadZ.Relative(-Information.GapMeasureValue, 100);
                        cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Judge OK", true);
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
                    cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1] Head Z Move Complete. Old : {cDEF.Work.TeachBonder1.GapMeasureZ / 1000.0} mm ,Current : {HeadZ.ActualPosition / 1000.0} mm, Gap Value : {cDEF.Serials.Value_Bond1 /1000.0} ", true);
                    cDEF.Work.TeachBonder1.GapMeasureZ = (int)HeadZ.ActualPosition;
                    cDEF.Work.TeachBonder1.Save();
                    OnGrid_Display();
                    Move_Bonder1_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder 1] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 7:
                    if (cDEF.Run.Mode == TRunMode.Manual_Bonder1_GapTouchAdjust)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunBonder1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Bonder1, "[Bonder1] Gap Touch Adjust End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        #endregion
        #region Move Command
        //Ready Position
        public void Move_Bonder1_ReadyPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachBonder1.ReadyPositionX, Sleep);
        }
        public void Move_Bonder1_ReadyPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder1.ReadyPositionY, Sleep);
        }
        public void Move_Bonder1_ReadyPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder1.ReadyPositionZ, Sleep);
        }

        //Dipping CenterXY
        public void Move_Bonder1_CamCenterX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachBonder1.CamPositionX, Sleep);
        }
        public void Move_Bonder1_CamCenterY()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder1.CamPositionY, Sleep);
        }

        //Dipping Z
        public void Move_Bonder1_CamPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder1.CamPositionZ, Sleep);
        }
        public void Move_Bonder1_JettingPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder1.JettingPositionZ, Sleep);
        }

        public void Move_Bonder1_JettingPositionXY(int Index, int CamOffsetX = 0, int CamOffsetY = 0)
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int posX = cDEF.Work.TeachBonder1.CamPositionX;
            posX += cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            if(Index == 0)
                posX += cDEF.Work.TeachBonder1.Jetting1OffsetX;
            else if(Index == 1)
                posX += cDEF.Work.TeachBonder1.Jetting2OffsetX;
            else if (Index == 2)
                posX += cDEF.Work.TeachBonder1.Jetting3OffsetX;
            else if (Index == 3)
                posX += cDEF.Work.TeachBonder1.Jetting4OffsetX;
            posX += CamOffsetX;

            int posY = cDEF.Work.TeachBonder1.CamPositionY;
            posY += cDEF.Work.TeachBonder1.CameraDistanceOffsetY;
            if (Index == 0)
                posY += cDEF.Work.TeachBonder1.Jetting1OffsetY;
            else if (Index == 1)
                posY += cDEF.Work.TeachBonder1.Jetting2OffsetY;
            else if (Index == 2)
                posY += cDEF.Work.TeachBonder1.Jetting3OffsetY;
            else if (Index == 3)
                posY += cDEF.Work.TeachBonder1.Jetting4OffsetY;

            posY += CamOffsetY;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
        }

        public void Move_Bonder1_CameraRelativeXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            HeadX.Relative(cDEF.Work.TeachBonder1.CameraDistanceOffsetX, SleepX);
            HeadY.Relative(cDEF.Work.TeachBonder1.CameraDistanceOffsetY, SleepY);
        }
        public void Move_Bonder1_BonderRelativeXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            HeadX.Relative(-cDEF.Work.TeachBonder1.CameraDistanceOffsetX, SleepX);
            HeadY.Relative(-cDEF.Work.TeachBonder1.CameraDistanceOffsetY, SleepY);
        }

        //Avoid Position
        public void Move_Bonder1_AvoidPositionX()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayX;

            HeadX.Absolute(cDEF.Work.TeachBonder1.AvoidPositionX, Sleep);
        }
        public void Move_Bonder1_AvoidPositionY()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayY;

            HeadY.Absolute(cDEF.Work.TeachBonder1.AvoidPositionY, Sleep);
        }

        public void Move_Bonder1_IdlePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayX;

            HeadX.Absolute(cDEF.Work.TeachBonder1.IdlePosX, Sleep);
        }
        public void Move_Bonder1_IdlePositionY()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder1.IdlePosY, Sleep);
        }
        public void Move_Bonder1_IdlePositionZ()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder1.IdlePosZ, Sleep);
        }
        public void Move_Bonder1_SamplePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayX;

            HeadX.Absolute(cDEF.Work.TeachBonder1.SamplePosX, Sleep);
        }
        public void Move_Bonder1_SamplePositionY()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder1.SamplePosY, Sleep);
        }
        public void Move_Bonder1_SampleGapPositionXY()
        {
            int SleepX;
            int SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;
            PosX = cDEF.Work.TeachBonder1.SampleGapPosX;
            PosY = cDEF.Work.TeachBonder1.SampleGapPosY;

            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);
        }
        public void Move_Bonder1_SamplePositionZ(double GapZ)
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;

            int posZ = cDEF.Work.TeachBonder1.SamplePosZ;

            if(cDEF.Work.DispSensor.DispenserType != 0)
            {
//                if (Math.Abs(GapZ) > 5000)
//                    GapZ = 300;

                posZ = cDEF.Work.TeachBonder1.JettingPositionZ;
                posZ -= (int)GapZ;

                posZ -= 500;
            }

            HeadZ.Absolute(posZ, Sleep);
        }
        public void Move_Bonder1_SampleVisionPositionZ()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder1.SampleVisionPosZ, Sleep);
        }
        public void Move_Bonder1_TouchPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Bonder1.MovingDelayX;

            HeadX.Absolute(cDEF.Work.TeachBonder1.TouchPosX, Sleep);
        }
        public void Move_Bonder1_TouchPositionY()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachBonder1.TouchPosY, Sleep);
        }
        public void Move_Bonder1_TouchPositionZ()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder1.TouchPosZ, Sleep);
        }
        public void Move_Bonder1_RelativeTouchStep()
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Relative(cDEF.Work.TeachBonder1.TouchStep, Sleep);
        }
        public void Move_Bonder1_JetPositionXY()
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            
            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);
        }

        //JettingData Point
        public void Move_Bonder1_JetPositionXY(int OffsetX, int OffsetY, JettingData Data)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            Point Calc = GetCirclePoint(0, 0, Data.Radius, Data.Angle);

            PosX += Calc.X;
            PosY += Calc.Y;


            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);
        }


        public void Move_Bonder1_PtnLine_FristJetPositionZ()
        {
            int Sleep;
            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;
            JettingLineData jld = WorkJetPtnLineData.JetLineData[0];

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            PosZ += jld.ZPos;
            PosZ += cDEF.Work.Bonder1.GapOffsetZ;
            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;
            HeadZ.Absolute(PosZ, Sleep);
        }
        public void Move_Bonder1_PtnArc_FristJetPositionZ()
        {
            int Sleep;
            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;
            JettingArcData jld = WorkJetPtnArcData.JetArcData[0];

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            PosZ += jld.ZPos;
            PosZ += cDEF.Work.Bonder1.GapOffsetZ;
            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;
            HeadZ.Absolute(PosZ, Sleep);
        }

        public void Move_Bonder1_PtnLine_FristJettingPositionXY(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            JettingLineData jld = WorkJetPtnLineData.JetLineData[0];

            PosX += jld.XPos;
            PosY += jld.YPos;

            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);

        }
        public void Move_Bonder1_PtnArc_FristJettingPositionXY(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            JettingArcData jld = WorkJetPtnArcData.JetArcData[0];

            PosX += jld.XPos;
            PosY += jld.YPos;

            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);

        }
        public bool Move_Bonder1_PtnLine_JettingPositionXYZ(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 22;
            axis[1] = 23;
            axis[2] = 24;

            List<TfpConti> lstConti = new List<TfpConti>();
            //lst 
          

            for (int i = 1; i < WorkJetPtnLineData.JetLineData.Count; i++)
            {
                TfpConti conti = new TfpConti();
                // Digital Conti add

                JettingLineData jld = WorkJetPtnLineData.JetLineData[i];

                conti.DI_ModuleType = 1;
                conti.DI_ModuleNo[0] = 5;
                conti.DI_BitOffset[0] = 1;// cDO.Bonder_Dispensor_1_Jetting;

                conti.DI_Mode[0] = 0;  //비율로 할꺼냐 시간으로 할거냐
                conti.Value[0] = 0;


                /*
                conti.DI_Mode[0] = 2;  // 0이고
                 // 타임도 0
                if (cDEF.Work.Project.GlobalOption.UseLineIoFull1)
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

            return HeadX.ContiStart(0, axis, lstConti, 3, 1);

        }

        public bool Move_Bonder1_PtnLine_JettingPositionXYZ_DistIO(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;
            PosZ += cDEF.Work.Bonder1.GapOffsetZ;
            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 22;
            axis[1] = 23;
            axis[2] = 24;

        
            int lCoordinate = 0;


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
                    //DI_BitOffset[0] = 1;// cDO.Bonder_Dispensor_1_Jetting;

                    DI_ModuleNo[0] = 6;
                    DI_BitOffset[0] = 0;// cDO.Bonder_Dispensor_1_Jetting;

                    DI_Mode[0] = 0;
                    Value[0] = 0;

                    if (BitOutput)
                    {
                        DI_BitOutput[0] = 1;
                    }
                    else
                        DI_BitOutput[0] = 0;

#if !Notebook
                    CAXM.AxmContiDigitalOutputBit(lCoordinate, 1, DI_ModuleType, DI_ModuleNo,DI_BitOffset, DI_BitOutput, Value, DI_Mode);
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

        public bool Move_Bonder1_PtnArc_JettingPositionXYZ_DistIO(int OffsetX, int OffsetY) //ARC
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;
            PosZ += cDEF.Work.Bonder1.GapOffsetZ;
            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 22;
            axis[1] = 23;
            axis[2] = 24;

            int[] axisArc = new int[2];
            axisArc[0] = 22;
            axisArc[1] = 23;

            int lCoordinate = 0;

            CAXM.AxmContiWriteClear(lCoordinate);
            // 축지정. 반드시 낮은 축부터 지정 되야 함.
            // 8축 이상 구성 되었을때, 같은 축그룹끼리만 된다고 함. *확인 필요
            CAXM.AxmContiSetAxisMap(lCoordinate,3, axis);
            // 0: abs, 1: rel
            CAXM.AxmContiSetAbsRelMode(lCoordinate,0);
            CAXM.AxmContiBeginNode(lCoordinate);

            //Motion Move 등록

            bool BeforeDI_Value = !WorkJetPtnArcData.JetArcData[1].Shot;

            double[] cPos = new double[2];
            double[] Pos = new double[3];
            double[] PosArc = new double[2];
            double[] endPos = new double[2];
            double Radius;
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

            int oldEX = WorkJetPtnArcData.JetArcData[0].XPos;
            int oldEY = WorkJetPtnArcData.JetArcData[0].YPos;

            for (int i = 0; i < WorkJetPtnArcData.JetArcData.Count; i++)
            {
                JettingArcData jld = WorkJetPtnArcData.JetArcData[i];
                BitOutput = jld.Shot;

                //IO 추가
                {
                    DI_ModuleType = 1;
                   
                    DI_ModuleNo[0] = 6;
                    DI_BitOffset[0] = 0;// cDO.Bonder_Dispensor_1_Jetting;

                    DI_Mode[0] = 2; // 아진 확인 - 비율 모드 2 기본 : 0
                    Value[0] = 100; // 100 기본: 0

                    if (BitOutput)
                    {
                        DI_BitOutput[0] = 1;
                    }
                    else
                        DI_BitOutput[0] = 0;

                    // DI_Mode 0 = 거리몯, 1= 시간모드,  2 = 비율 모드 100 이면 바로 나감
#if !Notebook
                    CAXM.AxmContiDigitalOutputBit(lCoordinate, 1, DI_ModuleType, DI_ModuleNo, DI_BitOffset, DI_BitOutput, Value, DI_Mode);
#endif
                    BeforeDI_Value = BitOutput;
                }


                //모션 추가
                Velocity = jld.LineSpeed;
                Acc = HeadX.Speed.FRun.FAccelerator;
                Dec = HeadX.Speed.FRun.FDecelerator;

                switch(jld.JetType)
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
        public bool Move_Bonder1_PtnArc_JettingPositionXYZ_DistIOaa(int OffsetX, int OffsetY) //ARC
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;
            PosZ += cDEF.Work.Bonder1.GapOffsetZ;
            if (cDEF.Work.Project.GlobalOption.UseGap)
                PosZ -= (int)Information.GapMeasureValue;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 22;
            axis[1] = 23;
            axis[2] = 24;

            //int[] caxis = new int[2];
            //caxis[0] = 22;
            //caxis[1] = 23;
            int lCoordinate = 0;


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
            double[] endPos = new double[2];
            double Radius;
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

            int oldEX = WorkJetPtnArcData.JetArcData[0].XPos;
            int oldEY = WorkJetPtnArcData.JetArcData[0].YPos;

            for (int i = 1; i < WorkJetPtnArcData.JetArcData.Count; i++)
            {
                JettingArcData jld = WorkJetPtnArcData.JetArcData[i];


                BitOutput = jld.Shot;

                //IO 추가
                // if (BeforeDI_Value != BitOutput) 
                {
                    DI_ModuleType = 1;

                    DI_ModuleNo[0] = 6;
                    DI_BitOffset[0] = 0;// cDO.Bonder_Dispensor_1_Jetting;

                    DI_Mode[0] = 2; // 아진 확인 - 비율 모드 2 기본 : 0
                    Value[0] = 100; // 100 기본: 0

                    if (BitOutput)
                    {
                        DI_BitOutput[0] = 1;
                    }
                    else
                        DI_BitOutput[0] = 0;

                    //
                    // DI_Mode 0 = 거리몯, 1= 시간모드,  2 = 비율 모드 100 이면 바로 나감
#if !Notebook
                    CAXM.AxmContiDigitalOutputBit(lCoordinate, 1, DI_ModuleType, DI_ModuleNo, DI_BitOffset, DI_BitOutput, Value, DI_Mode);
#endif
                    BeforeDI_Value = BitOutput;
                }


                //모션 추가

                Velocity = jld.LineSpeed;
                Acc = HeadX.Speed.FRun.FAccelerator;
                Dec = HeadX.Speed.FRun.FDecelerator;

                if (jld.EXPos == 0 && jld.EXPos == 0)
                {
                    //////Start Pos 라인 이동
                    Pos[0] = PosX + jld.XPos;
                    Pos[1] = PosY + jld.YPos;
                    Pos[2] = PosZ + jld.ZPos;

                    oldEX = jld.XPos;
                    oldEY = jld.YPos;

                    CAXM.AxmLineMove(lCoordinate, Pos, Velocity, Acc, Dec);
                }
                else if (jld.XPos == 0 && jld.YPos == 0)
                {
                    endPos[0] = PosX + jld.EXPos;
                    endPos[1] = PosY + jld.EYPos;
                    cPos[0] = PosX;
                    cPos[1] = PosY;

                    oldEX = jld.EXPos;
                    oldEY = jld.EYPos;

                    CAXM.AxmCircleCenterMove(lCoordinate, axis, cPos, endPos, Velocity, Acc, Dec, 0);
                }
                else
                {
                    if (jld.XPos != oldEX || jld.YPos != oldEY)
                    {
                        //////Start Pos 라인 이동
                        Pos[0] = PosX + jld.XPos;
                        Pos[1] = PosY + jld.YPos;
                        Pos[2] = PosZ + jld.ZPos;
                        CAXM.AxmLineMove(lCoordinate, Pos, Velocity, Acc, Dec);
                    }
                    endPos[0] = PosX + jld.EXPos;
                    endPos[1] = PosY + jld.EYPos;
                    cPos[0] = PosX;
                    cPos[1] = PosY;

                    oldEX = jld.EXPos;
                    oldEY = jld.EYPos;

                    CAXM.AxmCircleCenterMove(lCoordinate, axis, cPos, endPos, Velocity, Acc, Dec, 0);

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
            int lCoordinate = 0;
            uint CheckQueue = 0;
            CAXM.AxmContiReadFree(lCoordinate, ref CheckQueue);
            if (CheckQueue == 0)
                return false;
            CAXM.AxmContiSetAbsRelMode(lCoordinate, 0);
            CAXM.AxmContiStart(lCoordinate, 0, 0);
            return true;
        }
        

        public bool Move_Bonder1_PtnLine_JettingPositionXYZ_ControlIO(int OffsetX, int OffsetY)
        {
            int SleepX, SleepY;

            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            int PosX, PosY;

            PosX = cDEF.Work.TeachBonder1.CamPositionX;
            PosY = cDEF.Work.TeachBonder1.CamPositionY;
            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;

            PosX -= cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            PosY -= cDEF.Work.TeachBonder1.CameraDistanceOffsetY;

            PosX += OffsetX;
            PosY += OffsetY;

            //해당 모터 축
            int[] axis = new int[3];
            axis[0] = 22;
            axis[1] = 23;
            axis[2] = 24;


            int lCoordinate = 0;


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

            //IO 신호 켜기
            JettingIO = true;

            CAXM.AxmContiStart(lCoordinate, 0, 0);

            return true;
        }

        public void Move_Bonder1_JetPositionZ(JettingData Data)
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;

            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;

            PosZ += Data.ZOffset;
            PosZ += cDEF.Work.Bonder1.GapOffsetZ;

            if (cDEF.Work.Project.GlobalOption.UseGap)
            {
                PosZ -= (int)Information.GapMeasureValue;
            }

            HeadZ.Absolute(PosZ, Sleep);
            cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1 Z] : Jetting Z-{cDEF.Work.TeachBonder1.JettingPositionZ}, Gap Offset-{cDEF.Work.Bonder1.GapOffsetZ}, Z Up-{Data.ZUp} ", true);

        }
        public void Move_Bonder1_JetUpPositionZ(JettingData Data)
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;

            int PosZ = cDEF.Work.TeachBonder1.JettingPositionZ;
            PosZ += cDEF.Work.Bonder1.GapOffsetZ;
            if (cDEF.Work.Project.GlobalOption.UseGap)
            {
                PosZ -= (int)Information.GapMeasureValue;
            }
            PosZ -= Data.ZUp;

            HeadZ.Absolute(PosZ, Sleep);
            cDEF.TaskLogAppend(TaskLog.Bonder1, $"[Bonder 1 Z] : Jetting Z-{cDEF.Work.TeachBonder1.JettingPositionZ}, Gap Offset-{cDEF.Work.Bonder1.GapOffsetZ}, Measure-{(int)Information.GapMeasureValue} ", true);
        }

        public void Move_Bonder1_CleanPositionZ() // 함수 생성
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder1.CleanPositionZ, Sleep);
        }

        public void Move_Bonder1_CleanPositionXY() // 함수 생성
        {
            int Sleep1;
            int Sleep2;

            Sleep1 = cDEF.Work.Bonder1.MovingDelayX;
            Sleep2 = cDEF.Work.Bonder1.MovingDelayY;
            HeadX.Absolute(cDEF.Work.TeachBonder1.CleanPositionX, Sleep1);
            HeadY.Absolute(cDEF.Work.TeachBonder1.CleanPositionY, Sleep2);
        }

        public void Move_Bonder1_TipCleanPositionXY() // 함수 생성
        {
            int Sleep1;
            int Sleep2;

            Sleep1 = cDEF.Work.Bonder1.MovingDelayX;
            Sleep2 = cDEF.Work.Bonder1.MovingDelayY;

            int PitchY = cDEF.Work.TeachBonder1.TipCleanPitchY * Information.CleanPitchYCount;

            int posX = cDEF.Work.TeachBonder1.TipCleanStartPosX;
            int posY = cDEF.Work.TeachBonder1.TipCleanStartPosY + PitchY;

            HeadX.Absolute(posX, Sleep1);
            HeadY.Absolute(posY, Sleep2);
        }
        public void Move_Bonder1_TipCleanPositionZ() // 함수 생성
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachBonder1.TipCleanPosZ, Sleep);
        }
        public void Move_Bonder1_GapMeasureXY(int OffsetX, int OffsetY)
        {
            int Sleep1;
            int Sleep2;

            Sleep1 = cDEF.Work.Bonder1.MovingDelayX;
            Sleep2 = cDEF.Work.Bonder1.MovingDelayY;

            int PosX = cDEF.Work.TeachBonder1.CamPositionX;
            int PosY = cDEF.Work.TeachBonder1.CamPositionY;

            PosX -= cDEF.Work.TeachBonder1.GapDistanceX;
            PosY -= cDEF.Work.TeachBonder1.GapDistanceY;

            PosX += cDEF.Work.Bonder1.GapPosX;
            PosY += cDEF.Work.Bonder1.GapPosY;

            PosX += OffsetX;    // Vision Offset
            PosY += OffsetY;

            HeadX.Absolute(PosX, Sleep1);
            HeadY.Absolute(PosY, Sleep2);
        }
        public void Move_Bonder1_GapTouchPositionXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            HeadX.Absolute(cDEF.Work.TeachBonder1.GapTouchX, SleepX);
            HeadY.Absolute(cDEF.Work.TeachBonder1.GapTouchY, SleepY);
        }
        public void Move_Bonder1_GapAdjustPositionXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.Bonder1.MovingDelayX;
            SleepY = cDEF.Work.Bonder1.MovingDelayY;

            HeadX.Absolute(cDEF.Work.TeachBonder1.GapAdjustX, SleepX);
            HeadY.Absolute(cDEF.Work.TeachBonder1.GapAdjustY, SleepY);
        }
        public void Move_Bonder1_GapMeasure_Z(bool UI) 
        {
            int Sleep;

            Sleep = cDEF.Work.Bonder1.MovingDelayZ;

            int PosZ = cDEF.Work.TeachBonder1.GapMeasureZ;

            if (!UI)
                PosZ += cDEF.Work.Bonder1.GapOffsetZ;
            
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
        public bool Is_Bonder1_ReadyPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.ReadyPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_ReadyPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.ReadyPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_ReadyPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.ReadyPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_CamCenterX()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.CamPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_CamCenterY()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.CamPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_CamPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.CamPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_IdleCenterX()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.IdlePosX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_IdleCenterY()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.IdlePosY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_IdleCenterZ()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.IdlePosZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_SampleCenterX()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.SamplePosX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_SampleCenterY()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.SamplePosY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_SampleCenterZ()
        {
            if (IsRange((double)cDEF.Work.TeachBonder1.SamplePosZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_JettingPositionXY(int Index )
        {
            int posX = cDEF.Work.TeachBonder1.CamPositionX;
            posX += cDEF.Work.TeachBonder1.CameraDistanceOffsetX;
            if (Index == 0)
                posX += cDEF.Work.TeachBonder1.Jetting1OffsetX;
            else if (Index == 1)
                posX += cDEF.Work.TeachBonder1.Jetting2OffsetX;
            else if (Index == 2)
                posX += cDEF.Work.TeachBonder1.Jetting3OffsetX;
            else if (Index == 3)
                posX += cDEF.Work.TeachBonder1.Jetting4OffsetX;

            int posY = cDEF.Work.TeachBonder1.CamPositionY;
            posY += cDEF.Work.TeachBonder1.CameraDistanceOffsetY;
            if (Index == 0)
                posY += cDEF.Work.TeachBonder1.Jetting1OffsetY;
            else if (Index == 1)
                posY += cDEF.Work.TeachBonder1.Jetting2OffsetY;
            else if (Index == 2)
                posY += cDEF.Work.TeachBonder1.Jetting3OffsetY;
            else if (Index == 3)
                posY += cDEF.Work.TeachBonder1.Jetting4OffsetY;

            if (IsRange((double)posX, HeadX.ActualPosition) && IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder1_JettingPositionZ()
        {
            int posZ = cDEF.Work.TeachBonder1.JettingPositionZ;
            
            if (IsRange((double)posZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_AvoidPositionX()
        {
            int posX = cDEF.Work.TeachBonder1.AvoidPositionX;

            if (IsRange((double)posX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_AvoidPositionY()
        {
            int posY = cDEF.Work.TeachBonder1.AvoidPositionY;

            if (IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder1_CleanPositionZ()   //Move check function
        {
            int posZ = cDEF.Work.TeachBonder1.CleanPositionZ;

            if (IsRange((double)posZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder1_CleanPositionX()
        {
            int posX = cDEF.Work.TeachBonder1.CleanPositionX;

            if (IsRange((double)posX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder1_CleanPositionY()
        {
            int posY = cDEF.Work.TeachBonder1.CleanPositionY;

            if (IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_GapTouchXY()
        {
            int posX = cDEF.Work.TeachBonder1.GapTouchX;
            int posY = cDEF.Work.TeachBonder1.GapTouchY;

            if (IsRange((double)posX, HeadX.ActualPosition) && IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Bonder1_GapAdjustXY()
        {
            int posX = cDEF.Work.TeachBonder1.GapAdjustX;
            int posY = cDEF.Work.TeachBonder1.GapAdjustY;

            if (IsRange((double)posX, HeadX.ActualPosition) && IsRange((double)posY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Bonder1_GapMeasureZ()
        {
            int posZ = cDEF.Work.TeachBonder1.GapMeasureZ;

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

using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;

namespace XModule.Running
{

    public class RunLensPickerInformation : fpObject
    {

        #region 변수
        private bool FBottomCheckFinish = false;
        #endregion

        private Tray_Data FLens_tray;
        private TrayStatus FHeadStatus;

        // Vision Result
        public double VisionResultX;
        public double VisionResultY;
        public double VisionResultT;

        // ToquePlaceResult
        public bool ToquePlaceResult_NG;
        public int TargetTheta;

        #region Property
        public bool BottomCheckFinish
        {
            get { return FBottomCheckFinish; }
            set
            {
                if(FBottomCheckFinish != value)
                {
                    FBottomCheckFinish = value;
                    Change();
                }
            }
        }
        public TrayStatus HeadStatus
        {
            get { return FHeadStatus; }
            set
            {
                if (FHeadStatus != value)
                {
                    FHeadStatus = value;
                    Change();
                }
            }
        }
        public Tray_Data Lens_Tray
        {
            get { return FLens_tray; }
        }

        #endregion

        public RunLensPickerInformation() : base()
        {
            FLens_tray = new Tray_Data();
            FHeadStatus = new TrayStatus();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunLensPicker.dat";
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
            FHeadStatus = TrayStatus.Empty;
            FBottomCheckFinish = false;
            Unlock(Ignore);
        }
    }
    public enum TRunLensPickerMode
    {
        Stop,
        Pick,
        Place,
        BottomCheck,
    };

    
    public class RunLensPicker : TfpRunningModule 
    {
        //Evnet
        public delegate void LensDisplayHandler(int x, int y, LensTrayStatus status);
        public event LensDisplayHandler OnLens_Display;

        private RunLensPickerInformation FInformation;
        private TRunLensPickerMode FMode;

        public int FCalc;
        //WorkIndex
        public Lens_Data WorkLens;

        public RunLensPicker(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunLensPickerInformation();
            WorkLens = new Lens_Data();
        }
        

        #region **Property**
        public RunLensPickerInformation Information
        {
            get { return FInformation; }
        }

        public TRunLensPickerMode Mode
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
        public TfpMotionItem StageX
        {
            get { return GetMotions(4); }
        }
        #endregion //Property//

        private TRunLensPickerMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunLensPickerMode.Stop;
        }
        private void SetMode(TRunLensPickerMode Value)
        {
            if (Value == TRunLensPickerMode.Stop)
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
                case TRunLensPickerMode.Stop:
                    return "Stop";
                case TRunLensPickerMode.Pick:
                    return "Pick";
                case TRunLensPickerMode.Place:
                    return "Place";
                case TRunLensPickerMode.BottomCheck:
                    return "BottomCheck";

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
                    HeadZ.Home();
                    Step++;
                    break;
                case 1:
                    if (!HeadZ.HomeEnd)
                        return false;
                    HeadY.Home();
                    Step++;
                    break;
                case 2:
                    if (!HeadY.HomeEnd)
                        return false;
                    HeadX.Home();
                    HeadT.Home();
                    Step++;
                    break;
                case 3:
                    if (!HeadX.HomeEnd || !HeadT.HomeEnd)
                        return false;
                    if (!cDEF.Run.LensLoader.TransferY.HomeEnd)
                        return false;
                    StageX.Home();
                    Step++;
                    break;
                case 4:
                    if (!StageX.HomeEnd)
                        break;
                    Step++;
                    break;
                case 5:
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Move_Head_ReadyPositionZ();
                    if (cDEF.Work.Project.GlobalOption.VisionCheck)
                    {
                        //cDEF.Visions.Sendmsg(eVision.Recipe);
                        //FCalc = Environment.TickCount;
                        //Step++;
                        Step = 8;
                    }
                    else
                        Step = 8;
                    break;

                case 6:
                    if(Environment.TickCount - FCalc < 20000)
                    {
                        if(cDEF.Visions.ackV1_Recipe.Status == CmmStatus.Ok)
                        {
                            if(cDEF.Visions.ackV1_Recipe.exist)
                                Step++;
                            else
                            {
                                cDEF.Run.LogAlarm(cLog.RunLensPicker + 212, "Vision Recipe Miss Match.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        cDEF.Run.LogAlarm(cLog.RunLensPicker + 213, "Vision Recipe No Response.");
                        return false;
                    }
                    break;

                case 7:
                     Step++;
                    break;

                case 8:
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Initialize] End", true);
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
                    //cDEF.Visions.Sendmsg(eVision.Recipe);
                    Move_Head_ReadyPositionZ();
                    Step++;
                    break;

                case 1:
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Step++;
                    break;

                case 2:
                    if (Information.HeadStatus == TrayStatus.Empty
                        && cDEF.Run.Digital.Input[cDI.LensInsert_Vacuum_Check])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunLensPicker + 211;
                        //cDEF.Run.LogWarning(cLog.RunLensPicker + 211, "[LENS PICKER] Product is UnMatching.");
                        cDEF.Run.LogWarning(cLog.RunLensPicker + 211, "");
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Product is UnMatching.", true);
                        return false;
                    }
                    if (Information.HeadStatus == TrayStatus.Load
                       && !cDEF.Run.Digital.Input[cDI.LensInsert_Vacuum_Check])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunLensPicker + 211;
                        //cDEF.Run.LogWarning(cLog.RunLensPicker + 211, "[LENS PICKER] Product is UnMatching.");
                        cDEF.Run.LogWarning(cLog.RunLensPicker + 211, "");
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Product is UnMatching.", true);
                        return false;
                    }
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.Lens, "[To-Run] Done.", true);
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
                    Move_Head_ReadyPositionZ();  // 이동명령
                    Step++;
                    break;
                case 1:
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Lens, "[To-Stop] Done.", true);
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
                case TRunLensPickerMode.Pick:
                    Running_Pick();
                    break;

                case TRunLensPickerMode.Place:
                    Running_Place();
                    break;

                case TRunLensPickerMode.BottomCheck:
                    Running_BottomCheck();
                    break;

            }
        }
        
        protected override void ProcMain()
        {
            if (cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if (!cDEF.Work.Project.GlobalOption.UseLensPicker)
                {
                    if (cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.Act3Finish)
                    {
                        cDEF.Run.Index.Information.LensData.Status = eLensIndexStatus.AssembleFinish;
                    }
                    return;
                }

                // Pick
                if (cDEF.Run.LensLoader.Information.ExistStage
                    && Information.HeadStatus == TrayStatus.Empty
                    && GetLens())
                {
                    // 마지막
                    if (!cDEF.Run.VCMLoader.IsWorkTray()
                        && !cDEF.Run.VCMLoader.Information.ExistStage
                        && cDEF.Run.VCMPicker.Information.HeadStatus == TrayStatus.Empty
                        && cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.Empty
                        && cDEF.Run.Index.Information.VCMData.Status == eLensIndexStatus.Empty
                        && cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.Empty
                        && cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.Empty)
                        return;

                    if (cDEF.Run.LotEnd
                        && cDEF.Run.VCMPicker.Information.HeadStatus == TrayStatus.Empty
                        && cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.Empty
                        && cDEF.Run.Index.Information.VCMData.Status == eLensIndexStatus.Empty
                        && cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.Empty
                        && cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.Empty)
                        return;

                    cDEF.Tact.LensPicker.Start();
                    cDEF.Tact.LensPickerPick.Start();
                    Mode = TRunLensPickerMode.Pick;
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Pick Start", true);
                    return;
                }

                // BottomCheck
                if(Information.HeadStatus == TrayStatus.Load
                    && !Information.BottomCheckFinish)
                {
                    cDEF.Tact.LensPickerCam.Start();
                    Mode = TRunLensPickerMode.BottomCheck;
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Bottom Check Start", true);
                    return;
                }


                // Place
                if (Information.HeadStatus == TrayStatus.Load
                    && Information.BottomCheckFinish
                    && cDEF.Run.VCMVision.Information.InspectFinish
                    && cDEF.Run.Index.Mode == TRunIndexMode.Stop
                    && cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.Act3Finish)
                {
                    cDEF.Tact.LensPickerPlace.Start();
                    Mode = TRunLensPickerMode.Place;
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Place Start", true);
                    return;
                }

            }

            if (cDEF.Run.Mode == TRunMode.Manual_LensPick)
            {
                if (GetLens())
                {
                    cDEF.Tact.LensPicker.Start();
                    Mode = TRunLensPickerMode.Pick;
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker - Manual] Pick Start.", true);
                    return;
                }
            }

            if (cDEF.Run.Mode == TRunMode.Manual_LensPlace)
            {
                Mode = TRunLensPickerMode.Place;
                cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker - Manual] Place Start.", true);
                return;
            }

            if (cDEF.Run.Mode == TRunMode.Manual_LensBottomCheck)
            {
                Mode = TRunLensPickerMode.BottomCheck;
                cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker - Manual] Bottom Check Start.", true);
                return;
            }
        }
        public bool GetLens()
        {
            foreach (Lens_Data rd in Information.Lens_Tray.Items)
            {
                if (rd.Status == LensTrayStatus.Load)
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
                    cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Vacuum Off", true);
                    cDEF.Run.Digital.Output[cDO.Lens_Insert_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Blow Off", true);
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Move Head Ready Position Z", true);
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] (Before Pick) - Position Z ({HeadZ.ActualPosition / 1000.0}, Torque: {HeadZ.TorqueValue})", true);
                    ReadyRetryCount = 0;
                    InspRetryCount = 0;
                    Step++;
                    break;

                // Vision Check
                case 1:
                    Information.VisionResultX = 0;
                    Information.VisionResultY = 0;
                    Information.VisionResultT = 0;
                    Move_Head_CamPositionXY(WorkLens.x, WorkLens.y);
                    cDEF.Tact.LensPickerPickCam.Start();
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Camera Position Index X : {WorkLens.x}, Index Y : {WorkLens.y}", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < cDEF.Work.LensPicker.LensUpperGrabDelay)
                        break;
                    if (!cDEF.Work.Project.GlobalOption.VisionCheck)
                    {
                        Step = 6;
                        return;
                    }
                    cDEF.Visions.Sendmsg(eVision.V2_Ready);
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Send Vision V2 Ready (Pick).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 3:
                    if(Environment.TickCount - FCalc < 3000)
                    {
                        if (cDEF.Visions.ackV2_Ready.Status != CmmStatus.Ok)
                            break;
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Receive Vision V2 Ready OK (Pick).", true);
                        Step++;
                    }
                    else
                    {
                        if (ReadyRetryCount < cDEF.Work.Recipe.LensUpperRetryCount)
                        {
                            ReadyRetryCount++;
                            Step = 2;
                            break;
                        }
                        cDEF.Run.SetAlarmID = cLog.RunLensPicker + 200;
                        //cDEF.Run.LogWarning(cLog.RunLensPicker + 200, "[LENS PICKER] (Pick) V2 Ready Vision Time Out.");
                        cDEF.Run.LogWarning(cLog.RunLensPicker + 200, "");
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] V2 Ready (Pick) Vision Time Out", true);
                        Mode = TRunLensPickerMode.Stop;
                        return;
                    }
                    break;

                case 4:
                    cDEF.Visions.Sendmsg(eVision.V2_Complete);
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Send Vision V2 Complete (Pick).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV2_Complete.Status != CmmStatus.Ok)
                            break;
                        if(!cDEF.Visions.ackV2_Complete.exist)
                        {
                            if (InspRetryCount < cDEF.Work.Recipe.LensUpperRetryCount)
                            {
                                InspRetryCount++;
                                ReadyRetryCount = 0;
                                Step = 2;
                                break;
                            }
                            // cDEF.Run.LogWarning(cLog.RunLensPicker + 201, "[LENS PICKER] Vision 인식 실패 (자재 없음).");
                            cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Vision Fail.(V2 Pick)", true);
                            //자재 인식을 못 할 경우 자동 으로 다음 자재 이동
                            WorkLens.Status = LensTrayStatus.Finish;
                            OnLens_Display(WorkLens.x, WorkLens.y, LensTrayStatus.Finish);
                            Mode = TRunLensPickerMode.Stop;
                            return;
                        }
                        Information.VisionResultX = (int)(cDEF.Visions.ackV2_Complete.x);
                        Information.VisionResultY = (int)(cDEF.Visions.ackV2_Complete.y);
                        Information.VisionResultT = (int)(cDEF.Visions.ackV2_Complete.t);
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Receive Vision V2 Complete OK (Pick). X : {Information.VisionResultX}, Y : {Information.VisionResultY}, T : {Information.VisionResultT}", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunLensPicker + 202;
                        //cDEF.Run.LogWarning(cLog.RunLensPicker + 202, "[LENS PICKER] (Pick) V2 Complete Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunLensPicker + 202, "");
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] (Pick) V2 Complete Vision Time Out", true);
                        Mode = TRunLensPickerMode.Stop;
                        return;
                    }
                    break;

                case 6:
                    cDEF.Tact.LensPickerPickCam.GetTact();
                    Move_Head_WorkPositionXY(WorkLens.x, WorkLens.y, Information.VisionResultX, Information.VisionResultY);
                    Move_Head_StagePickPositionT(Information.VisionResultT);
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Work Position X : {Information.VisionResultX} Y : {Information.VisionResultY} T : {Information.VisionResultT}", true);
                    Step++;
                    break;

                case 7:
                    if (cDEF.Work.Option.PickOverrideUse == 0)
                    {
                        Move_Head_StagePickPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Stage Pick Position Z", true);
                    }
                    else if (cDEF.Work.Option.PickOverrideUse == 1)
                    {
                        Move_Head_StagePickOverridePositionZ();
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Stage Pick Override Position Z", true);
                    }
                    else if (cDEF.Work.Option.PickOverrideUse == 2)
                    {
                        Move_Head_Stage1stPickPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Stage Pick Step 1 Position Z", true);
                    }
                    Step++;
                    break;

                case 8:
                    if (cDEF.Work.Option.PickOverrideUse == 2)
                    {
                        Move_Head_Stage2ndPickPositionZ();
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Stage Pick Step 2 Position Z", true);
                    }
                    Step++;
                    break;

                case 9:
                    if (cDEF.Work.Recipe.LensPickUpTorqueUse)
                    {
                        if (HeadZ.ActualPosition + cDEF.Work.Recipe.LensInsertTorqueLimit < cDEF.Work.TeachLensPicker.StagePickPositionZ)
                        {
                            cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Torque  Fail - Current Position Z ({HeadZ.ActualPosition / 1000.0}, {HeadZ.TorqueValue})", true);
                            WorkLens.Status = LensTrayStatus.Finish;
                            OnLens_Display(WorkLens.x, WorkLens.y, LensTrayStatus.Finish);
                            Mode = TRunLensPickerMode.Stop;
                            return;
                        }
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] (After Pick) - Position Z : {HeadZ.ActualPosition / 1000.0}, Torque: {HeadZ.TorqueValue}", true);
                    }
                    cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum] = true;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Vacuum On", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 10:
                    if (Environment.TickCount - FCalc < cDEF.Work.LensPicker.LensVacDelay)
                        break;
                    Step++;
                    break;

                case 11:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Ready Position Z", true);
                    Step++;
                    break;

                case 12:
                    // Vac Check
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.LensInsert_Vacuum_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensPicker + 203;
                            //cDEF.Run.LogWarning(cLog.RunLensPicker + 203, "[LENS PICKER] Vacuum Check Fail.");
                            cDEF.Run.LogWarning(cLog.RunLensPicker + 203, "");
                            cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Vacuum Check Fail", true);
                            Mode = TRunLensPickerMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 13:
                    Information.HeadStatus = TrayStatus.Load;
                    WorkLens.Status = LensTrayStatus.Finish;
                    Information.BottomCheckFinish = false;
                    OnLens_Display(WorkLens.x, WorkLens.y, LensTrayStatus.Finish);
                    Step++;
                    break;

                case 14:
                    cDEF.Tact.LensPickerPick.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_LensPick)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunLensPickerMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Pick End - Unit Tact Time [{cDEF.Tact.LensPickerPick.CycleTime.ToString("N3")}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Place()
        {
            if(Mode == TRunLensPickerMode.Place && (Step == 6 || Step == 8 || Step == 9)
                && Environment.TickCount - FCalc > 3000)
            {
                HeadZ.Stop();
                HeadT.Stop();
            }
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    Information.ToquePlaceResult_NG = false;
                    if(cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if(!cDEF.Run.Digital.Input[cDI.LensInsert_Vacuum_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensPicker + 204;
                            //cDEF.Run.LogWarning(cLog.RunLensPicker + 204, "[LENS PICKER] Place 중 자재 lost");
                            cDEF.Run.LogWarning(cLog.RunLensPicker + 204, "");
                            cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Lens Lost", true);
                            Mode = TRunLensPickerMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 1:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Ready Position Z", true);
                    
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (!cDEF.Run.VCMVision.Is_VCMVision_ReadyPosition())
                            break;
                        
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunLensPicker + 205;
                        //cDEF.Run.LogWarning(cLog.RunLensPicker + 205, "[LENS PICKER] VCM Vision Ready Position Move Time out");
                        cDEF.Run.LogWarning(cLog.RunLensPicker + 205, "");
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] VCM Vision Ready Position Move Time Out",true);
                        Mode = TRunLensPickerMode.Stop;
                        return;
                    }
                    break;

                case 3:
                    Move_Head_IndexPlacePositionXY(Information.VisionResultX, Information.VisionResultY,
                        cDEF.Run.VCMVision.Information.VisionResultX, cDEF.Run.VCMVision.Information.VisionResultY);
                    if(!cDEF.Work.Project.GlobalOption.UseSecondaryCorrection)
                        Move_Head_IndexPlacePositionT(Information.VisionResultT);
                    else
                        HeadT.Relative((int)(cDEF.Work.TeachLensPicker.LensOffsetT), 100);
                    //cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Index Place Position T", true);
                    Step++;
                    break;

                case 4:
                    if (cDEF.Work.Option.PlaceOverrideUse == 0)
                    {
                        Move_Head_IndexPlacePositionZ();
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Index Place Position Z", true);
                    }
                    else if(cDEF.Work.Option.PlaceOverrideUse == 1)
                    {
                        Move_Head_IndexPlaceOverridePositionZ();
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Index Place Override Position Z", true);
                    }
                    else if (cDEF.Work.Option.PlaceOverrideUse == 2)
                    {
                        Move_Head_Index1stPlacePositionZ();
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Index Place Step 1 Position Z", true);
                    }

                    Step++;
                    break;

                case 5:
                    if (cDEF.Work.Option.PlaceOverrideUse == 2)
                    {
                        Move_Head_Index2ndPlacePositionZ();
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Index Place Step 2 Position Z", true);
                    }
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 6:
                    // 토크 fail 검사
                    if (cDEF.Work.Recipe.LensInsertTorqueUse)
                    {
                        if(HeadZ.ActualPosition + cDEF.Work.Recipe.LensInsertTorqueLimit < cDEF.Work.TeachLensPicker.IndexPlacePositionZ )
                        {
                            cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] (Torque Z Fail), Current Head Z : {HeadZ.ActualPosition / 1000} mm, Index: {((cDEF.Run.Index.Information.IndexNum + 10) % 12)}, Torque: {HeadZ.TorqueValue} ", true);
                            Information.ToquePlaceResult_NG = true;
                            Step = 10;
                            break;
                        }
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker]  (Torque Z Pass), Current Head Z : {HeadZ.ActualPosition / 1000} mm, Index: {((cDEF.Run.Index.Information.IndexNum + 10) % 12)}, Torque: {HeadZ.TorqueValue} ", true);
                    }
                    Step++;
                    break;

                case 7:
                    if(!cDEF.Work.Project.GlobalOption.UseLockType)
                    {
                        Step = 10;
                        break;
                    }
                    Move_Head_Relative_LockingUpZ();
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Locking Up.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 8:
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Locking Up Complete. Current Head Z : {HeadZ.ActualPosition / 1000} mm", true);
                    Information.TargetTheta = (int)HeadT.ActualPosition + cDEF.Work.TeachLensPicker.LockingPositionT;
                    Move_Relative_LockingPositionT();
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Locking T. Current Head T : {HeadT.ActualPosition / 100} '", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 9:
                    if (cDEF.Work.Recipe.LensPickUpThetaTorqueUse)
                    {
                        if (HeadT.ActualPosition + cDEF.Work.Recipe.LensInsertTorqueLimitTheta < Information.TargetTheta)
                        {
                            cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] (Torque T Fail), Current Head T : {HeadT.ActualPosition / 100} mm, Index: {((cDEF.Run.Index.Information.IndexNum + 10) % 12)}, Torque : {HeadT.TorqueValue}", true);
                            Information.ToquePlaceResult_NG = true;
                        }
                        else
                        {
                            cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] (Torque T Pass), Current Head T : {HeadT.ActualPosition / 100} mm, Index: {((cDEF.Run.Index.Information.IndexNum + 10) % 12)}, Torque : {HeadT.TorqueValue}", true);
                        }
                    }
                    Step++;
                    break;

                case 10:
                    cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Vacuum Off.", true);
                    cDEF.Run.Digital.Output[cDO.Lens_Insert_Blow] = true;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Blow On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 11:
                    if (Environment.TickCount - FCalc < cDEF.Work.LensPicker.LensBlowDelay)
                        break;
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 12:
                    cDEF.Run.Digital.Output[cDO.Lens_Insert_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Blow Off.", true);
                    Information.HeadStatus = TrayStatus.Empty;
                    if((cDEF.Work.Recipe.LensInsertTorqueUse || cDEF.Work.Recipe.LensPickUpThetaTorqueUse) && Information.ToquePlaceResult_NG)
                    {
                        if (cDEF.Work.Project.GlobalOption.VisionCheck)
                        {
                            cDEF.Run.Index.Information.LensData.Status = eLensIndexStatus.LensHeightFail;
                            cDEF.Run.Index.Information.LensData.FailType = eFailType.LensHeightFail;
                        }
                       // Move_Head_IndexPlacePositionXY(Information.VisionResultX, Information.VisionResultY,
                       //cDEF.Run.VCMVision.Information.VisionResultX, cDEF.Run.VCMVision.Information.VisionResultY);

                        int indxNum = (cDEF.Run.Index.Information.IndexNum + 9) % 12;
                        cDEF.TaskLogAppend(TaskLog.TempTorqueFaileData, $"Index:{indxNum} X:{HeadX.ActualPosition} Y:{HeadY.ActualPosition}  BtmX:{Information.VisionResultX} BtmY:{Information.VisionResultY}  VcmX:{ cDEF.Run.VCMVision.Information.VisionResultX} VcmY:{cDEF.Run.VCMVision.Information.VisionResultY} ", true);

                    }
                    else
                        cDEF.Run.Index.Information.LensData.Status = eLensIndexStatus.AssembleFinish;
                    cDEF.Tact.LensPicker.GetTact();
                    cDEF.Tact.LensPickerPlace.GetTact();

                    cDEF.Run.Index.Information.LensData.TT_TopVision = cDEF.Tact.TopVision.CycleTime;
                    cDEF.Run.Index.Information.LensData.TT_LensPicker = cDEF.Tact.LensPicker.CycleTime;
                    cDEF.Run.Index.Information.LensData.TT_LensPickerPlace = cDEF.Tact.LensPickerPlace.CycleTime;
                    cDEF.Run.Index.Information.LensData.TT_LensPickerCam = cDEF.Tact.LensPickerCam.CycleTime;
                    cDEF.Run.Index.Information.LensData.TT_LensPickerPickCam = cDEF.Tact.LensPickerPickCam.CycleTime;

                  Step++;
                    break;

                case 13:
                    cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum] = true; //201125, 추가
                    Move_Head_CamPositionXYWithOutStage( WorkLens.y);
                    //Move_Head_ReadyPositionX();
                    //Move_Head_ReadyPositionY();
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Move Head Next Position X Y.", true);
                    Step++;
                    break;

                case 14:
                    cDEF.Run.VCMVision.Mode = TRunVCMVisionMode.Ready;
                    Step++;
                    break;

                case 15:
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if (cDEF.Run.Digital.Input[cDI.LensInsert_Vacuum_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensPicker + 206;
                            //cDEF.Run.LogWarning(cLog.RunLensPicker + 206, "[LENS PICKER] Place 후 자재 확인.");
                            cDEF.Run.LogWarning(cLog.RunLensPicker + 206, "");
                            cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] After Place Detected Lens.", true);
                            Mode = TRunLensPickerMode.Stop;
                            return;
                        }
                    }
                    cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum] = false; //201125, 추가
                    Step++;
                    break;

                case 16:
                    if (cDEF.Run.Mode == TRunMode.Manual_LensPlace)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunLensPickerMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Place End - Unit Tact TIme [{cDEF.Tact.LensPickerPlace.CycleTime.ToString("N3")}]", true);
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Pick & Place End - CycleTime [{cDEF.Tact.LensPicker.CycleTime.ToString("N3")}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        private  int InspRetryCount = 0;
        private int ReadyRetryCount = 0;
        private bool SecondCorrected = false;
        protected void Running_BottomCheck()
        {
            if (!IsReady())
                return;
            int NextX;

            switch (Step)
            {
                case 0:
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.LensInsert_Vacuum_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensPicker + 207;
                            //cDEF.Run.LogWarning(cLog.RunLensPicker + 207, "[LENS PICKER] Bottom Check 중 자재 lost");
                            cDEF.Run.LogWarning(cLog.RunLensPicker + 207, "");
                            cDEF.TaskLogAppend(TaskLog.Lens,"[LENS PICKER] Lens Lost", true);
                            Mode = TRunLensPickerMode.Stop;
                            return;
                        }
                    }
                    Information.VisionResultX = 0;
                    Information.VisionResultY = 0;
                    Information.VisionResultT = 0;
                    InspRetryCount = 0;
                    ReadyRetryCount = 0;
                    SecondCorrected = false;
                    if (cDEF.Work.Project.GlobalOption.UseSecondaryCorrection)
                        cDEF.Visions.BottomIndex = 1;
                    else
                        cDEF.Visions.BottomIndex = 2;
                    Step++;
                    break;

                case 1:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[LENS PICKER] Move Head Ready Position Z.", true);
                    Step++;
                    break;

                case 2:
                    Move_Head_BottomCamPositionX();
                    Move_Head_BottomCamPositionY();
                    Move_Head_BottomCamPositionT();

                    if (!Information.Lens_Tray.IsFinish())
                    {
                        if (WorkLens.x + 1 < cDEF.Work.LensLoader.TrayCountX)
                        {
                            NextX = WorkLens.x + 1;
                            Move_Stage_CamPositionX(NextX);
                        }
                        else
                        {
                            NextX = 0;
                            Move_Stage_CamPositionX(NextX);
                        }
                    }
                    
                    cDEF.TaskLogAppend(TaskLog.Lens, "[LENS PICKER] Move Head Bottom Cmera Position.", true);
                    cDEF.TaskLogAppend(TaskLog.Lens, "[LENS PICKER] Move Head Bottom Camera Position X Y", true);
                    Step++;
                    break;

                case 3:
                    Move_Head_BottomCamPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[LENS PICKER] Move Head Bottom Camera Position Z", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < cDEF.Work.LensPicker.LensUnderGrabDelay)
                        break;
                    if(!cDEF.Work.Project.GlobalOption.VisionCheck)
                    {
                        Step = 8;
                        return;
                    }

                    cDEF.Visions.Sendmsg(eVision.V3_Ready);
                    cDEF.TaskLogAppend(TaskLog.Lens, "[LENS PICKER] Send Vision V3 Ready (Bottom Check) ", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 2000)
                    {
                        if (cDEF.Visions.ackV3_Ready.Status != CmmStatus.Ok)
                            break;
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Receive Vision V3 Ready OK (Bottom Check)", true);
                        Step++;
                    }
                    else
                    {
                        if (ReadyRetryCount < cDEF.Work.Recipe.LensUnderRetryCount)
                        {
                            ReadyRetryCount++;
                            Step = 3;
                            break;
                        }

                        cDEF.Run.SetAlarmID = cLog.RunLensPicker + 208;
                        //cDEF.Run.LogWarning(cLog.RunLensPicker + 208, "[LENS PICKER] (Bottom Check) V3 Ready Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunLensPicker + 208, "");
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] (Bottom Check) V3 Ready Vision Time Out.", true);
                        Mode = TRunLensPickerMode.Stop;
                        return;
                    }
                    break;

                case 6:
                    cDEF.Visions.Sendmsg(eVision.V3_Complete);
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] Send Vision V3 Complete (Bottom Check).", true);
                    FCalc = Environment.TickCount;  
                    Step++;
                    break;

                case 7:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV3_Complete.Status != CmmStatus.Ok)
                            break;

                        if(!cDEF.Visions.ackV3_Complete.exist)
                        {
                            if(InspRetryCount < cDEF.Work.Recipe.LensUnderRetryCount)
                            {
                                InspRetryCount++;
                                ReadyRetryCount = 0;
                               Step = 3;
                                break;
                            }

                            cDEF.Run.SetAlarmID = cLog.RunLensPicker + 210;
                            //cDEF.Run.LogWarning(cLog.RunLensPicker + 210, "[LENS PICKER] (Bottom Check) V3 Complete Vision NG");
                            cDEF.Run.LogWarning(cLog.RunLensPicker + 210, "");
                            cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] (Bottom Check) V3 Complete Vision NG", true);
                            Mode = TRunLensPickerMode.Stop;
                            return;
                        }

                        Information.VisionResultX = (cDEF.Visions.ackV3_Complete.y);
                        Information.VisionResultY = (cDEF.Visions.ackV3_Complete.x);
                        Information.VisionResultT = (cDEF.Visions.ackV3_Complete.t);
                        Information.VisionResultT = Math.Round(Information.VisionResultT, 3);
                        if(cDEF.Work.Project.GlobalOption.UseSecondaryCorrection && !SecondCorrected)
                        {
                            if(Math.Abs(Information.VisionResultT) < cDEF.Work.LensPicker.SecondaryCorrLimit)
                            {
                                if (cDEF.Work.Option.LensPickerUpperTDirectionCCW)
                                    HeadT.Relative((int)(Information.VisionResultT * 100), 100);
                                else
                                    HeadT.Relative((int)(Information.VisionResultT * -100), 100);

                                cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Secondary Correction. Move T, Vision T {Information.VisionResultT}", true);
                                Information.VisionResultX = 0;
                                Information.VisionResultY = 0;
                                Information.VisionResultT = 0;
                                SecondCorrected = true;
                                cDEF.Visions.BottomIndex = 2;
                                Step = 4;
                                break;
                            }
                        }
                        cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] Receive Vision V3 Complete Ok X : {Information.VisionResultX} , Y : {Information.VisionResultY} , T : {Information.VisionResultT}", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunLensPicker + 209;
                        //cDEF.Run.LogWarning(cLog.RunLensPicker + 209, "[LENS PICKER] (Bottom Check) V3 Complete Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunLensPicker + 209, "");
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Picker] (Bottom Check) V3 Complete Vision Time Out", true);
                        Mode = TRunLensPickerMode.Stop;
                        return;
                    }
                    break;

                case 8:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Len Picker] Move Head Ready Position Z", true);
                    Information.BottomCheckFinish = true;
                    Step++;
                    break;

                case 9:
                    cDEF.Tact.LensPickerCam.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_LensBottomCheck)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunLensPickerMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Bottom Check] End.- Unit Tact Time [{cDEF.Tact.LensPickerCam.CycleTime}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        public double getAngle(int ax, int ay, int bx, int by)
        {
            double dy = by - ay;
            double dx = bx - ax;
            double angle = Math.Atan(dy / dx) * (180.0 / Math.PI);

            if (dx < 0.0)
            {
                angle += 180.0;
            }
            else
            {
                //if (dy < 0.0) angle += 360.0;
            }

            return angle;
        }
        public double GetDistance(int ax, int ay, int bx, int by)
        {

            double dy = by - ay;
            double dx = bx - ax;

            return Math.Sqrt(dx * dx + dy * dy);

        }
        public Point GetCirclePoint(int ax, int ay, double radius, double Angle)
        {
            Point target = new Point();
            double radian = Math.PI * Angle / 180.0;
            target.X = ax + (int)((radius * Math.Cos(radian)));
            target.Y = ay + (int)((radius * Math.Sin(radian)));

            return target;
        }
        public Point CalcAngle(int BottomOffsetX, int BottomOffsetY, int AngleX, int AngleY)
        {
            double abAngle = 0;

            //  1. 해당 위치의 각도를 구한다.
            abAngle = getAngle(BottomOffsetX, BottomOffsetY, AngleX, AngleY);
            //// 2. 60도 튼다.
            //abAngle += 60 - AngleX / 600;
            abAngle -= 60;

            double radius = 0;
            // 3. 이상적인 상태에서 해당 위치 와의 거리를 구한다.
            radius = GetDistance(BottomOffsetX, BottomOffsetY, AngleX, AngleY);

            //  4. leftBottom 를 기준으로 2 의 거리와 4 의 각도를 갖는 위치를 계산한다.
            Point destPoint = GetCirclePoint(0, 0, radius, abAngle);

            return destPoint;

        }

        #endregion
        #region Move Command
        //Head X
        public void Move_Head_ReadyPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachLensPicker.ReadyPositionX, Sleep);
        }
        public void Move_Head_StagePickPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachLensPicker.StagePickPositionX, Sleep);
        }
        public void Move_Head_IndexPlacePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachLensPicker.IndexPlacePositionX, Sleep);
        }
        public void Move_Head_BottomCamPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachLensPicker.BottomCamPositionX, Sleep);
        }
        //Head Y
        public void Move_Head_ReadyPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachLensPicker.ReadyPositionY, Sleep);
        }
        public void Move_Head_StageFirstPickPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachLensPicker.StageFirstPickPositionY, Sleep);
        }
        public void Move_Head_IndexPlacePositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachLensPicker.IndexPlacePositionY, Sleep);
        }
        public void Move_Head_IndexPlacePositionXY(double BottomOffsetX, double BottomOffsetY, int VCMOffsetX, int VCMOffsetY)
        {
            int SleepX, SleepY;
            SleepX = cDEF.Work.LensPicker.MovingDelayX;
            SleepY = cDEF.Work.LensPicker.MovingDelayY;

            int PosX, PosY;
            PosX = cDEF.Work.TeachLensPicker.IndexPlacePositionX;
            PosY = cDEF.Work.TeachLensPicker.IndexPlacePositionY;

            if (Math.Abs(BottomOffsetX) > 2000)
            {
                BottomOffsetX = 0;
            }
            if (Math.Abs(BottomOffsetY) > 2000)
            {
                BottomOffsetY = 0;
            }

            // Bottom Offset
            PosX += (int)BottomOffsetX;
            PosY += (int)BottomOffsetY;

            // VCM Offset
            int OffsetX, OffsetY;
            OffsetX = VCMOffsetX;
            OffsetY = VCMOffsetY;

            if (OffsetX != 0 || OffsetY != 0)
            {
                Point VCMResult = CalcAngle(0, 0, OffsetX, OffsetY);

                if (Math.Abs(VCMResult.X) > 2000)
                {
                    VCMResult.X = 0;
                }
                if (Math.Abs(VCMResult.Y) > 2000)
                {
                    VCMResult.Y = 0;
                }

                PosX += VCMResult.X; 
                PosY -= VCMResult.Y;

            }

            //20200604 ssj 인덱스별 유저 옵셋 값 적용
            int indxNum = (cDEF.Run.Index.Information.IndexNum + 9) % 12;

            PosX += cDEF.Work.Recipe.IndexOffsetX;
            PosY += cDEF.Work.Recipe.IndexOffsetY;
            //PosX += cDEF.Work.TeachLensPicker.PlaceUserOffsetX[indxNum];
            //PosY += cDEF.Work.TeachLensPicker.PlaceUserOffsetY[indxNum];

            //
            //PosX += cDEF.Work.TeachLensPicker.PlaceUserOffsetX;
            //PosY += cDEF.Work.TeachLensPicker.PlaceUserOffsetY;
            //

            cDEF.TaskLogAppend(TaskLog.TempPlaceData, $"Index:{indxNum} orgX:{cDEF.Work.TeachLensPicker.IndexPlacePositionX} orgY:{cDEF.Work.TeachLensPicker.IndexPlacePositionY} X:{PosX} Y:{PosY}  BtmX:{BottomOffsetX} BtmY:{BottomOffsetY}  VcmX:{VCMOffsetX} VcmY:{VCMOffsetY} ", true);

            HeadX.Absolute(PosX, SleepX);
            HeadY.Absolute(PosY, SleepY);
        }
        public void Move_Head_BottomCamPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachLensPicker.BottomCamPositionY, Sleep);
        }
        //Head Z
        public void Move_Head_ReadyPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;

            HeadZ.SetTorquePara(2000, 1000, false);

            HeadZ.Absolute(cDEF.Work.TeachLensPicker.ReadyPositionZ, Sleep);
        }
        public void Move_Head_StagePickPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            if (cDEF.Work.Recipe.LensPickUpTorqueUse)
            {
                HeadZ.SetTorquePara(cDEF.Work.Option.TorqueLimitPick, 5000, true);
            }
            HeadZ.Absolute(cDEF.Work.TeachLensPicker.StagePickPositionZ, Sleep);
        }
        public void Move_Head_Stage1stPickPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachLensPicker.StagePickPositionZ - cDEF.Work.TeachLensPicker.StageStepPickOffset, Sleep);
        }
        public void Move_Head_Stage2ndPickPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            if (cDEF.Work.Recipe.LensInsertTorqueUse)
            {
                HeadZ.SetTorquePara(cDEF.Work.Option.TorqueLimitPick, 5000, true);
            }
            HeadZ.Absolute(cDEF.Work.TeachLensPicker.StagePickPositionZ, HeadZ.Speed.FRun.FMaximumVelocity * (double)(cDEF.Work.LensPicker.StepPlaceSpeed) / 100.0, Sleep);
        }
        public void Move_Head_StagePickOverridePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            if (cDEF.Work.Recipe.LensInsertTorqueUse)
            {
                HeadZ.SetTorquePara(cDEF.Work.Option.TorqueLimitPick, 5000, true);
            }

            if (cDEF.Run.Motion.Simul)
            {
                HeadZ.Absolute(cDEF.Work.TeachLensPicker.StagePickPositionZ, HeadZ.Speed.FRun.FMaximumVelocity * (double)(cDEF.Work.LensPicker.StepPlaceSpeed) / 100.0, Sleep);

            }
            else
                HeadZ.Absolute(cDEF.Work.TeachLensPicker.StagePickPositionZ, HeadZ.Speed.FRun.FMaximumVelocity, cDEF.Work.TeachLensPicker.StagePickPositionZ - cDEF.Work.TeachLensPicker.StageStepPickOffset, HeadZ.Speed.FRun.FMaximumVelocity * (double)(cDEF.Work.LensPicker.StepPlaceSpeed) / 100.0, Sleep);


        }
        public void Move_Head_IndexPlacePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            if (cDEF.Work.Recipe.LensInsertTorqueUse)
            {
                HeadZ.SetTorquePara(cDEF.Work.Option.TorqueLimit, 5000, true);
            }
            HeadZ.Absolute(cDEF.Work.TeachLensPicker.IndexPlacePositionZ, Sleep);
        }
        public void Move_Head_Index1stPlacePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachLensPicker.IndexPlacePositionZ - cDEF.Work.TeachLensPicker.IndexStepPlaceOffset, Sleep);
        }
        public void Move_Head_Index2ndPlacePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            if (cDEF.Work.Recipe.LensInsertTorqueUse)
            {
                HeadZ.SetTorquePara(cDEF.Work.Option.TorqueLimit, 5000, true);
            }
            HeadZ.Absolute(cDEF.Work.TeachLensPicker.IndexPlacePositionZ, HeadZ.Speed.FRun.FMaximumVelocity * (double)(cDEF.Work.LensPicker.StepPlaceSpeed) / 100.0 ,Sleep);
        }
        public void Move_Head_IndexPlaceOverridePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            if (cDEF.Work.Recipe.LensInsertTorqueUse)
            {
                HeadZ.SetTorquePara(cDEF.Work.Option.TorqueLimit, 5000, true);
            }

            if (cDEF.Run.Motion.Simul)
                HeadZ.Absolute(cDEF.Work.TeachLensPicker.IndexPlacePositionZ, HeadZ.Speed.FRun.FMaximumVelocity * (double)(cDEF.Work.LensPicker.StepPlaceSpeed) / 100.0, Sleep);
            else
            HeadZ.Absolute (cDEF.Work.TeachLensPicker.IndexPlacePositionZ, HeadZ.Speed.FRun.FMaximumVelocity, cDEF.Work.TeachLensPicker.IndexPlacePositionZ - cDEF.Work.TeachLensPicker.IndexStepPlaceOffset, HeadZ.Speed.FRun.FMaximumVelocity * (double)(cDEF.Work.LensPicker.StepPlaceSpeed) / 100.0 , Sleep);


        }
        public void Move_Head_BottomCamPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachLensPicker.BottomCamPositionZ, Sleep);
        }
        public void Move_Head_Relative_LockingUpZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayZ;
            HeadZ.Relative(cDEF.Work.TeachLensPicker.LockingUp, Sleep);
        }
        //Head T
        public void Move_Head_ReadyPositionT()
        {
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayT;
            HeadT.SetTorquePara(2000, 1000, false);
            HeadT.Absolute(cDEF.Work.TeachLensPicker.ReadyPositionT, Sleep);
        }
        public void Move_Head_StagePickPositionT(double OffsetT)
        {
            int Sleep;
            int Pos;
            HeadT.TorqueUse = false;
            Sleep = cDEF.Work.LensPicker.MovingDelayT;
            if(cDEF.Work.Option.LensPickerUpperTDirectionCCW)
                Pos = cDEF.Work.TeachLensPicker.StagePickPositionT - (int)(OffsetT * 100);
            else
                Pos = cDEF.Work.TeachLensPicker.StagePickPositionT + (int)(OffsetT * 100);
            HeadT.Absolute(Pos, Sleep);
        }
        public void Move_Head_IndexPlacePositionT(double OffsetT)
        {
            int Sleep;
            int Pos;
            // 오 보정 방지코드
            if(Math.Abs(OffsetT) > cDEF.Work.LensPicker.SecondaryCorrLimit)
            {
                OffsetT = 0;
            }
            HeadT.TorqueUse = false;
            Sleep = cDEF.Work.LensPicker.MovingDelayT;
            if (cDEF.Work.Option.LensPickerUpperTDirectionCCW)
                Pos = cDEF.Work.TeachLensPicker.IndexPlacePositionT + (int)(OffsetT * 100);
            else
                Pos = cDEF.Work.TeachLensPicker.IndexPlacePositionT - (int)(OffsetT * 100);
            HeadT.Absolute(Pos, Sleep);
            cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Picker] PLACE T [{Pos}]", true);
        }
        public void Move_Head_BottomCamPositionT()
        {
            int Sleep;
            HeadT.TorqueUse = false;
            Sleep = cDEF.Work.LensPicker.MovingDelayT;
            HeadT.Absolute(cDEF.Work.TeachLensPicker.BottomCamPositionT, Sleep);
        }
        public void Move_Relative_LockingPositionT()
        {
            int Pos;
            int Sleep;
            Sleep = cDEF.Work.LensPicker.MovingDelayT;

            if (cDEF.Work.Recipe.LensPickUpThetaTorqueUse)
            {
                HeadT.SetTorquePara(cDEF.Work.Option.TorqueLimitTheta, 5000, true);
            }

            if (cDEF.Work.Option.LensPickerUpperTDirectionCCW)
                Pos = cDEF.Work.TeachLensPicker.LockingPositionT * 1;
            else
                Pos = cDEF.Work.TeachLensPicker.LockingPositionT * -1;

            HeadT.Relative(Pos, HeadT.Speed.FRun.FMaximumVelocity / 2 , Sleep);
        }
        //Head XY
        public void Move_Head_CamPositionXY(int IndexX, int IndexY)
        {
            int SleepX;
            int SleepY;
            int SleepStageX;
            SleepX = cDEF.Work.LensPicker.MovingDelayX;
            SleepY = cDEF.Work.LensPicker.MovingDelayY;
            SleepStageX = cDEF.Work.LensLoader.MovingDelayX;

            int posX = cDEF.Work.TeachLensPicker.StagePickPositionX;

            int posStageX = cDEF.Work.TeachLensLoader.StageFirstPickPositionX;
            posStageX += cDEF.Work.LensLoader.TrayPitchX * IndexX;

            int posY = cDEF.Work.TeachLensPicker.StageFirstPickPositionY;
            posY += cDEF.Work.LensLoader.TrayPitchY * IndexY;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
            StageX.Absolute(posStageX, SleepStageX);
        }
        public void Move_Stage_CamPositionX(int IndexX)
        {
            int SleepStageX;
            
            SleepStageX = cDEF.Work.LensLoader.MovingDelayX;

            int posStageX = cDEF.Work.TeachLensLoader.StageFirstPickPositionX;
            posStageX += cDEF.Work.LensLoader.TrayPitchX * IndexX;

            StageX.Absolute(posStageX, SleepStageX);
        }
        public void Move_Head_CamPositionXYWithOutStage( int IndexY)
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.LensPicker.MovingDelayX;
            SleepY = cDEF.Work.LensPicker.MovingDelayY;

            int posX = cDEF.Work.TeachLensPicker.StagePickPositionX;
            int posY = cDEF.Work.TeachLensPicker.StageFirstPickPositionY;
            posY += cDEF.Work.LensLoader.TrayPitchY * IndexY;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
        }
        public void Move_Head_WorkPositionXY(int IndexX, int IndexY, double OffsetX, double OffsetY)
        {
            int SleepX;
            int SleepY;
            int SleepStageX;

            SleepX = cDEF.Work.LensPicker.MovingDelayX;
            SleepY = cDEF.Work.LensPicker.MovingDelayY;
            SleepStageX = cDEF.Work.LensLoader.MovingDelayX;

            int posX = cDEF.Work.TeachLensPicker.StagePickPositionX;
            posX += cDEF.Work.TeachLensPicker.CameraDistanceOffsetX;
            posX -= (int)OffsetX;

            //int posStageX = cDEF.Work.TeachLensLoader.StageFirstPickPositionX;
            //posStageX += cDEF.Work.LensLoader.TrayPitchX * IndexX;
            //posStageX += OffsetX;

            int posY = cDEF.Work.TeachLensPicker.StageFirstPickPositionY;
            posY += cDEF.Work.LensLoader.TrayPitchY * IndexY;
            posY += cDEF.Work.TeachLensPicker.CameraDistanceOffsetY;
            posY += (int)OffsetY;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
            //StageX.Absolute(posStageX, SleepStageX);
        }
        //Stage
        public void Move_Stage_ReadyPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayX;
            StageX.Absolute(cDEF.Work.TeachLensLoader.ReadyPositionX, Sleep);
        }
        public void Move_Stage_EjectPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayX;
            StageX.Absolute(cDEF.Work.TeachLensLoader.EjectPositionX, Sleep);
        }
        public void Move_Stage_WorkPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayX;
            StageX.Absolute(cDEF.Work.TeachLensLoader.WorkPositionX, Sleep);
        }
        public void Move_Stage_FirstPickPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayX;
            StageX.Absolute(cDEF.Work.TeachLensLoader.StageFirstPickPositionX, Sleep);
        }

        public void Move_Stage_MagazineChangePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayX;
            StageX.Absolute(cDEF.Work.TeachLensLoader.StageMagazineChangePositionX, Sleep);
        }
        public void Move_Head_CameraRelativeXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.LensPicker.MovingDelayX;
            SleepY = cDEF.Work.LensPicker.MovingDelayY;

            HeadX.Relative(-cDEF.Work.TeachLensPicker.CameraDistanceOffsetX, SleepX);
            HeadY.Relative(-cDEF.Work.TeachLensPicker.CameraDistanceOffsetY, SleepY);
        }
        public void Move_Head_PickerRelativeXY()
        {
            int SleepX;
            int SleepY;
            SleepX = cDEF.Work.LensPicker.MovingDelayX;
            SleepY = cDEF.Work.LensPicker.MovingDelayY;

            HeadX.Relative(cDEF.Work.TeachLensPicker.CameraDistanceOffsetX, SleepX);
            HeadY.Relative(cDEF.Work.TeachLensPicker.CameraDistanceOffsetY, SleepY);
        }
        #endregion

        #region CheckPosition
        public bool Is_Head_ReadyPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.ReadyPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePickPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.StagePickPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPlacePositionX()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.IndexPlacePositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_BottomCamPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.BottomCamPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.ReadyPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StageFirstPickPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.StageFirstPickPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPlacePositionY()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.IndexPlacePositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_BottomCamPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.BottomCamPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.ReadyPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePickPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.StagePickPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPlacePositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.IndexPlacePositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_BottomCamPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.BottomCamPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionT()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.ReadyPositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePickPositionT()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.StagePickPositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPlacePositionT()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.IndexPlacePositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_BottomCamPositionT()
        {
            if (IsRange((double)cDEF.Work.TeachLensPicker.BottomCamPositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_ReadyPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachLensLoader.ReadyPositionX, StageX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_EjectPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachLensLoader.EjectPositionX, StageX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_MagazineChangePositionX()
        {
            if (IsRange((double)cDEF.Work.TeachLensLoader.StageMagazineChangePositionX, StageX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_WorkPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachLensLoader.WorkPositionX, StageX.ActualPosition))
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

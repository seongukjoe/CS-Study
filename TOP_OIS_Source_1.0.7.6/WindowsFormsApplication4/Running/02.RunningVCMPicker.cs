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

    public class RunVCMPickerInformation : fpObject
    {

        #region 변수

        //Count
        #endregion

        private Tray_Data FVCM_tray;
        private TrayStatus FHeadStatus;
        public bool HeadOverLoad = false;

        #region Property
        public TrayStatus HeadStatus
        {
            get { return FHeadStatus; }
            set
            {
                if(FHeadStatus != value)
                {
                    FHeadStatus = value;
                    Change();
                }
            }
        }
        public Tray_Data VCM_Tray
        {
            get { return FVCM_tray; }
        }
        #endregion

        public RunVCMPickerInformation() : base()
        {
            FVCM_tray = new Tray_Data();
            FHeadStatus = new TrayStatus();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunVCMPicker.dat";
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
            Unlock(Ignore);
        }
    }
    public enum TRunVCMPickerMode
    {
        Stop,
        Pick,
        Place
    };

    
    public class RunVCMPicker : TfpRunningModule 
    {
        //Evnet
        
        public delegate void VCMDisplayHandler(int x, int y, LensTrayStatus status);
        public event VCMDisplayHandler OnVCM_Display;

        private RunVCMPickerInformation FInformation;
        private TRunVCMPickerMode FMode;

        

        public int FCalc;
        //WorkIndex
        public Lens_Data WorkLens;

        public RunVCMPicker(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunVCMPickerInformation();
            WorkLens = new Lens_Data();
        }
        

        #region **Property**
        public RunVCMPickerInformation Information
        {
            get { return FInformation; }
        }

        public TRunVCMPickerMode Mode
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
        public TfpCylinderItem VCMClamp
        {
            get { return GetCylinders(0); }
        }
        #endregion //Property//

        private TRunVCMPickerMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunVCMPickerMode.Stop;
        }
        private void SetMode(TRunVCMPickerMode Value)
        {
            if (Value == TRunVCMPickerMode.Stop)
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
                case TRunVCMPickerMode.Stop:
                    return "Stop";
                case TRunVCMPickerMode.Pick:
                    return "Pick";
                case TRunVCMPickerMode.Place:
                    return "Place";

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
                    VCMClamp.Backward();
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
                    if (!HeadX.HomeEnd  || !HeadT.HomeEnd)
                        return false;
                    if(!cDEF.Run.VCMLoader.TransferX.HomeEnd)
                        return false;
                    StageY.Home();
                    Step++;
                    break;
                case 4:
                    if (!StageY.HomeEnd)
                        break;
                    
                    Step++;
                    break;
                case 5:
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Step++;
                    break;
                case 6:
                    cDEF.TaskLogAppend(TaskLog.VCM, "[Initialize] End", true);
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
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Move_Head_ReadyPositionZ();
                    Step++;
                    break;

                case 1:
                    VCMClamp.Backward();
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.VCM, "[To-Run] Done.", true);
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
                    VCMClamp.Backward();
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.VCM, "[To-Stop] Done.", true);
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
                case TRunVCMPickerMode.Pick:
                    Running_Pick();
                    break;

                case TRunVCMPickerMode.Place:
                    Running_Place();
                    break;
            }
        }
        
        protected override void ProcMain()
        {
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                // Pick
                if(cDEF.Run.VCMLoader.Information.ExistStage
                    && Information.HeadStatus == TrayStatus.Empty
                    && GetVCM())
                {
                    if (cDEF.Run.LotEnd)
                        return;
                    cDEF.Tact.VCMPicker.Start();
                    Mode = TRunVCMPickerMode.Pick;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM PICKER] Pick Start.", true);
                    return;
                }


                // Place
                if(Information.HeadStatus == TrayStatus.Load
                    && cDEF.Run.Index.Mode == TRunIndexMode.Stop
                    && cDEF.Run.Index.Information.VCMData.Status == eLensIndexStatus.Empty)
                {
                    if (cDEF.Work.Option.IndexSkip[cDEF.Run.Index.Information.IndexNum])
                        return;
                    Mode = TRunVCMPickerMode.Place;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM PICKER] Place Start.", true);
                    return;
                }

            }
            if (cDEF.Run.Mode == TRunMode.Manual_VCMPick)
            {
                if (GetVCM())
                {
                    cDEF.Tact.VCMPicker.Start();
                    Mode = TRunVCMPickerMode.Pick;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker - Manual] Pick Start", true);
                    return;
                }
            }

            if (cDEF.Run.Mode == TRunMode.Manual_VCMPlace)
            {
                Mode = TRunVCMPickerMode.Place;
                cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker - Manual] Place Start", true);
                return;
            }
        }

        public bool GetVCM()
        {
            foreach(Lens_Data rd in Information.VCM_Tray.Items)
            {
                if(rd.Status == LensTrayStatus.Load)
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
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Ready Position Z", true);
                    Step++;
                    break;

                case 1:
                    Move_Head_WorkingPositionXY(WorkLens.x, WorkLens.y);
                    cDEF.TaskLogAppend(TaskLog.VCM, $"[VCM Picker] Move Head Work Position Index X : {WorkLens.x}, Index Y : {WorkLens.y}", true);
                    Move_Head_StagePickPositionT();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Stage Pick Position T", true);
                    Step++;
                    break;

                case 2:
                    Move_Head_StagePickPositionZ();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Stage Pick Position Z", true);
                    Step++;
                    break;

                case 3:
                    //I.O 
                    cDEF.Run.Digital.Output[cDO.VCM_Loading_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Blow Off", true);
                    cDEF.Run.Digital.Output[cDO.VCM_Loading_Vacuum] = true;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Vacuum On", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < cDEF.Work.VCMPicker.VCMVacDelay)
                        break;
                    Step++;
                    break;

                case 5:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Ready Position Z", true);
                    Step++;
                    break;

                case 6:
                    // Vac Check
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.VCMLoading_Vacuum_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMPicker + 200;
                            cDEF.Run.LogWarning(cLog.RunVCMPicker + 200, "");
                            //cDEF.Run.LogWarning(cLog.RunVCMPicker + 200, "[VCM PICKER] Vacuum Check Fail.");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Vacuum Check Fail.", true);
                            Mode = TRunVCMPickerMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 7:
                    Information.HeadStatus = TrayStatus.Load;
                    WorkLens.Status = LensTrayStatus.Finish;
                    OnVCM_Display(WorkLens.x, WorkLens.y, LensTrayStatus.Finish);
                    Step++;
                    break;

                case 8:
                    //cDEF.Tact.VCMPicker.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_VCMPick)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunVCMPickerMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.VCM, $"[VCM Pick] End - Cycly Time [{cDEF.Tact.VCMPicker.CycleTime.ToString("N3")}]", true);
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
                    VCMClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] VCM Clamp Unclamp", true);
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Ready Position Z", true);
                    Step++;
                    break;

                case 1:
                    Move_Head_IndexPlacePositionX();
                    Move_Head_IndexPlacePositionY();
                    Move_Head_IndexPlacePositionT();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Index Place Position X Y T", true);
                    Step++;
                    break;

                case 2:
                    Move_Head_ClampPlacePositionZ();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Clamp Place Position Z", true);
                    Step++;
                    break;

                case 3:
                    VCMClamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] VCM Clamp Clamp", true);
                    Step++;
                    break;

                case 4:
                    VCMClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] VCM Clamp Unclamp", true);
                    Step++;
                    break;

                case 5 :
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.VCMLoading_Vacuum_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMPicker + 202;
                            //cDEF.Run.LogWarning(cLog.RunVCMPicker + 202, "[VCM PICKER] Vacuum Check Fail.");
                            cDEF.Run.LogWarning(cLog.RunVCMPicker + 202, "");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Vacuum Check Fail", true);
                            Mode = TRunVCMPickerMode.Stop;
                            return;
                        }
                    }
                        Step++;
                    break;
                case 6:
                    Move_Head_IndexPlacePositionZ();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Index Place Position Z", true);
                    Step++;
                    break;

                case 7:
                    cDEF.Run.Digital.Output[cDO.VCM_Loading_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Vacuum Off", true);
                    cDEF.Run.Digital.Output[cDO.VCM_Loading_Blow] = true;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Blow On", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 8:
                    if (Environment.TickCount - FCalc < cDEF.Work.VCMPicker.VCMBlowDelay)
                        break;
                    Information.HeadStatus = TrayStatus.Empty;
                    Step++;
                    break;

                case 9:
                    Move_Head_ReadyPositionZ();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Ready Position Z", true);
                    Step++;
                    break;

                case 10:
                    if (cDEF.Work.Project.GlobalOption.IndexCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.VCM_Loading])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMPicker + 201;
                            //cDEF.Run.LogWarning(cLog.RunVCMPicker + 201, "[VCM PICKER] Index Check Fail.");
                            cDEF.Run.LogWarning(cLog.RunVCMPicker + 201, "");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Index Check Fail", true);
                            Mode = TRunVCMPickerMode.Stop;
                            return;
                        }
                    }
                    cDEF.Run.Digital.Output[cDO.VCM_Loading_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Blow Off", true); 
                    Step++;
                    break;

                case 11:
                    Information.HeadStatus = TrayStatus.Empty;
                    cDEF.Run.Index.Information.VCMData.Status = eLensIndexStatus.VCMLoaded;
                    cDEF.Run.Index.Information.VCMData.Index = cDEF.Run.Index.Information.IndexNum;
                    cDEF.Run.Index.Information.VCMData.x = WorkLens.x;
                    cDEF.Run.Index.Information.VCMData.y = WorkLens.y;
                    cDEF.Tact.VCMPicker.GetTact();
                    cDEF.Run.Index.Information.VCMData.TT_VCMPicker = cDEF.Tact.VCMPicker.CycleTime;
                    Step++;
                    break;

                case 12:
                    Move_Head_ReadyPositionX();
                    Move_Head_ReadyPositionY();
                    Move_Head_StagePickPositionT();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Picker] Move Head Ready Position XY", true);
                    Step++;
                    break;

                case 13:
                    if (cDEF.Run.Mode == TRunMode.Manual_VCMPlace)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunVCMPickerMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.VCM, $"[VCM Place] End - Cycle Time [{cDEF.Tact.VCMPicker.CycleTime.ToString("N3")}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        

        #endregion
        #region Move Command
        public void Move_Head_ReadyPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachVCMPicker.ReadyPositionX, Sleep);
        }
        public void Move_Head_StagePickPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachVCMPicker.StageFirstPickPositionX, Sleep);
        }
        public void Move_Head_IndexPlacePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayX;
            HeadX.Absolute(cDEF.Work.TeachVCMPicker.IndexPlacePositionX, Sleep);
        }
        // Head Y
        public void Move_Head_ReadyPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachVCMPicker.ReadyPositionY, Sleep);
        }
        public void Move_Head_StagePickPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachVCMPicker.StagePickPositionY, Sleep);
        }
        public void Move_Head_IndexPlacePositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayY;
            HeadY.Absolute(cDEF.Work.TeachVCMPicker.IndexPlacePositionY, Sleep);
        }
        // HeadXY
        public void Move_Head_WorkingPositionXY(int IndexX, int IndexY)
        {
            int SleepX;
            int SleepY;
            int SleepStageY;
            SleepX = cDEF.Work.VCMPicker.MovingDelayX;
            SleepY = cDEF.Work.VCMPicker.MovingDelayY;
            SleepStageY = cDEF.Work.VCMLoader.MovingDelayY;

            int posX = cDEF.Work.TeachVCMPicker.StageFirstPickPositionX;
            posX += cDEF.Work.VCMLoader.TrayPitchX * IndexX;

            int posY = cDEF.Work.TeachVCMPicker.StagePickPositionY;

            int posStageY = cDEF.Work.TeachVCMLoader.StageFirstPickPositionY;
            posStageY += cDEF.Work.VCMLoader.TrayPitchY * IndexY;

            HeadX.Absolute(posX, SleepX);
            HeadY.Absolute(posY, SleepY);
            StageY.Absolute(posStageY, SleepStageY);
        }
        // Head Z
        public void Move_Head_ReadyPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachVCMPicker.ReadyPositionZ, Sleep);
        }
        public void Move_Head_StagePickPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachVCMPicker.StagePickPositionZ, Sleep);
        }
        public void Move_Head_IndexPlacePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachVCMPicker.IndexPlacePositionZ, Sleep);
        }
        public void Move_Head_ClampPlacePositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayZ;
            HeadZ.Absolute(cDEF.Work.TeachVCMPicker.ClampPlacePositionZ, Sleep);
        }
        // Head T
        public void Move_Head_ReadyPositionT()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayT;
            HeadT.Absolute(cDEF.Work.TeachVCMPicker.ReadyPositionT, Sleep);
        }
        public void Move_Head_StagePickPositionT()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayT;
            HeadT.Absolute(cDEF.Work.TeachVCMPicker.StagePickPositionT, Sleep);
        }
        public void Move_Head_IndexPlacePositionT()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMPicker.MovingDelayT;
            HeadT.Absolute(cDEF.Work.TeachVCMPicker.IndexPlacePositionT, Sleep);
        }

        // Stage
        public void Move_Stage_ReadyPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayY;
            StageY.Absolute(cDEF.Work.TeachVCMLoader.ReadyPositionY, Sleep);
        }
        public void Move_Stage_EjectPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayY;
            StageY.Absolute(cDEF.Work.TeachVCMLoader.EjectPositionY, Sleep);
        }
        public void Move_Stage_FirstPickPosition()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayY;
            StageY.Absolute(cDEF.Work.TeachVCMLoader.StageFirstPickPositionY, Sleep);
        }
        public void Move_Stage_MagazineChagnePosition()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayY;
            StageY.Absolute(cDEF.Work.TeachVCMLoader.StageMagazineChangePositionY, Sleep);
        }
        //public void Move_Stage_WorkPositionY()
        //{
        //    int Sleep;
        //    Sleep = cDEF.Work.VCMLoader.MovingDelayY;
        //    StageY.Absolute(cDEF.Work.TeachVCMLoader.WorkPositionY, Sleep);
        //}
        #endregion

        #region CheckPosition
        public bool Is_Head_ReadyPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.ReadyPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StageFirstPickPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.StageFirstPickPositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPlacePositionX()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.IndexPlacePositionX, HeadX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.ReadyPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePickPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.StagePickPositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPlacePositionY()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.IndexPlacePositionY, HeadY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.ReadyPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePickPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.StagePickPositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPlacePositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.IndexPlacePositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ClampPlacePositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.ClampPlacePositionZ, HeadZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_ReadyPositionT()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.ReadyPositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_StagePickPositionT()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.StagePickPositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Head_IndexPlacePositionT()
        {
            if (IsRange((double)cDEF.Work.TeachVCMPicker.IndexPlacePositionT, HeadT.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_ReadyPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachVCMLoader.ReadyPositionY, StageY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_EjectPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachVCMLoader.EjectPositionY, StageY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Stage_FirstPickPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachVCMLoader.StageFirstPickPositionY, StageY.ActualPosition))
                return true;
            else
                return false;
        }

        public bool Is_Stage_MagazineChangePositionY()
        {
            if (IsRange((double)cDEF.Work.TeachVCMLoader.StageMagazineChangePositionY, StageY.ActualPosition))
                return true;
            else
                return false;
        }
        //public bool Is_Stage_WorkPositionY()
        //{
        //    if (IsRange((double)cDEF.Work.TeachVCMLoader.WorkPositionY, StageY.ActualPosition))
        //        return true;
        //    else
        //        return false;
        //}
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

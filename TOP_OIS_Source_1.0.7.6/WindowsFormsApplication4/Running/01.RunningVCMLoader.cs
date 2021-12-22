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

    public class RunVCMLoaderInformation : fpObject
    {

        #region 변수
        private bool FExistStage = false;

        private int FWorkTray = 0;
        private Magazine_Data FVCM_Magazine;

        #endregion

        #region Property
        public bool ExistStage
        {
            get { return FExistStage; }
            set
            {
                if(FExistStage != value)
                {
                    FExistStage = value;
                    Change();
                }
            }
        }
        public int WorkTray
        {
            get { return FWorkTray; }
            set
            {
                if(FWorkTray != value)
                {
                    FWorkTray = value;
                    Change();
                }
            }
        }

        public Magazine_Data VCM_Magazine
        {
            get { return FVCM_Magazine; }
        }

    
      


        #endregion

        public RunVCMLoaderInformation() : base()
        {
            FVCM_Magazine = new Magazine_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunVCMLoader.dat";
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
                    //case "FExistStage": FExistStage = Convert.ToBoolean(sArr[1]); break;
                    //case "FWorkTray": FWorkTray = Convert.ToInt32(sArr[1]); break;
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


            FileWrite.WriteLine($"FExistStage,{FExistStage}");
            FileWrite.WriteLine($"FWorkTray,{FWorkTray}");

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
    }
    public enum TRunVCMLoaderMode
    {
        Stop,
        Loading,                // Magazine -> Stage
        Unloading               // Stage -> Magazine

    };

    
    public class RunVCMLoader : TfpRunningModule 
    {
        //Evnet
        public delegate void VCMDisplayInitHandler();
        public event VCMDisplayInitHandler OnVCM_DisplayInit;

        private RunVCMLoaderInformation FInformation;
        private TRunVCMLoaderMode FMode;

        public int FCalc;

        public RunVCMLoader(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunVCMLoaderInformation();
        }
        

        #region **Property**
        public RunVCMLoaderInformation Information
        {
            get { return FInformation; }
        }

        public TRunVCMLoaderMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        // Motor
        public TfpMotionItem MagazineZ
        {
            get { return GetMotions(0); }
        }
        public TfpMotionItem TransferX
        {
            get { return GetMotions(1); }
        }
        
        public TfpCylinderItem TransferClamp
        {
            get { return GetCylinders(0); }
        }
        public TfpCylinderItem StageClamp
        {
            get { return GetCylinders(1); }
        }

        #endregion //Property//

        private TRunVCMLoaderMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunVCMLoaderMode.Stop;
        }
        private void SetMode(TRunVCMLoaderMode Value)
        {
            if (Value == TRunVCMLoaderMode.Stop)
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
                case TRunVCMLoaderMode.Stop:
                    return "Stop";
                case TRunVCMLoaderMode.Loading:
                    return "Loading";
                case TRunVCMLoaderMode.Unloading:
                    return "Unloading";

                default:
                    return "";
            }
        }
        protected override void ProcReal()
        {
            // 상시 진입 구간
            //if (cDEF.Work.Project.GlobalOption.UseMES)
            //{
            //    if (cDEF.Run.MESEQPStatus == eMESEqpStatus.IDLE &&
            //        (cDEF.Run.VCMLoader.GetEmptyTray() ||
            //        cDEF.Run.LensLoader.GetEmptyTray() ||
            //          cDEF.Run.Unloader.GetEmptyTray()))
            //    {
            //        cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.RUN;
            //        cDEF.Run.MesStatusMsg = "VCM TRAY LOADING";
            //        cDEF.Mes.Send_EquipStatus();
            //    }
            //}
        }
        protected override bool ProcInitialize()
        {
            switch (Step)
            {
                case 0:
                    StageClamp.Backward();
                    TransferClamp.Backward();
                    Step++;
                    break;

                case 1:
                    TransferX.Home();
                    Step++;
                    break;

                case 2:
                    if (!TransferX.HomeEnd)
                        break;
                    MagazineZ.Home();
                    
                    Step++;
                    break;

                case 3:
                    if (!MagazineZ.HomeEnd)
                        break;
                    
                    Step++;
                    break;

                case 4:
                    if (Information.ExistStage)
                        StageClamp.Forward();
                    Step++;
                    break;
                case 5:
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
                    Move_Transfer_ReadyPositionX();
                    Step++;
                    break;

                case 1:
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
                    Move_Transfer_ReadyPositionX();
                    Step++;
                    break;
                case 1:
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
                case TRunVCMLoaderMode.Loading:
                    Running_Loading();
                    break;

                case TRunVCMLoaderMode.Unloading:
                    Running_Unloading();
                    break;

            }
        }
        
        protected override void ProcMain()
        {
            
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if (Information.ExistStage)
                {
                    if (cDEF.Run.VCMPicker.Information.VCM_Tray.IsFinish())
                    {
                        if (GetUsingTray())
                        {
                            //cDEF.Tact.VCMLoader.Start();
                            Mode = TRunVCMLoaderMode.Unloading;
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Unloading Start", true);
                            return;
                        }
                        else
                        { 
                            if (GetEmptyTray())
                            {
                                //cDEF.Tact.VCMLoader.Start();
                                Mode = TRunVCMLoaderMode.Unloading;
                                cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Unloading Start", true);
                                return;
                            }
                            else
                            {
                                cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 201;
                                cDEF.Run.LogWarning(cLog.RunVCMLoader + 201, "[VCM LOADER] 투입할 Tray 공간이 없습니다.");
                                cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] No Slot in Magazine for Unloading", true);
                                return;
                            }
                        }
                    }
                }

                if (!Information.ExistStage)
                {
                    if (GetWorkTray())
                    {
                        cDEF.Tact.VCMLoader.Start();
                        Mode = TRunVCMLoaderMode.Loading;       // Loading
                        cDEF.TaskLogAppend(TaskLog.VCM,"[VCM Loader] Loading Start", true);
                        return;
                    }
                    else
                    {
                        if (!cDEF.Run.Index.Product_On_Index() && cDEF.Run.PlateAngle.Information.LensData.Status == LensTrayStatus.Empty && cDEF.Run.UnloadPicker.Information.HeadLensData.Status == LensTrayStatus.Empty)
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 200;
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 200, "");
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 200, "[VCM LOADER] 투입할 Tray가 없습니다.");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] No Tray in Magazine for Loading", true);
                            return;
                        }
                    }
                }


                
            }

            if(cDEF.Run.Mode == TRunMode.Manual_VCMLoading)
            {
                if (GetWorkTray())
                {
                    cDEF.Tact.VCMLoader.Start();
                    Mode = TRunVCMLoaderMode.Loading;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader - Manual] Loading Start", true);
                    return;
                }
            }

            if (cDEF.Run.Mode == TRunMode.Manual_VCMUnloading)
            {
                if (GetUsingTray())
                {
                    cDEF.Tact.VCMLoader.Start();
                    Mode = TRunVCMLoaderMode.Unloading;
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader - Manual] Unloading Start", true);
                    return;
                }
                else
                {
                    if (GetEmptyTray())
                    {
                        cDEF.Tact.VCMLoader.Start();
                        Mode = TRunVCMLoaderMode.Unloading;
                        cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader - Manual] Unloading Start", true);
                        return;
                    }
                }
            }




        }
        public bool IsWorkTray()
        {
            foreach (Tray_Data td in Information.VCM_Magazine.Items)
            {
                if (td.Status == TrayStatus.Load)
                {
                    return true;
                }
            }
            return false;
        }
        public bool GetWorkTray()
        {
            if (cDEF.Work.Recipe.LoaderMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse( Information.VCM_Magazine.Items))
                {
                    if (td.Status == TrayStatus.Load)
                    {
                        Information.WorkTray = td.Slot;
                        return true;
                    }
                }
            }
            else
            {
                foreach (Tray_Data td in Information.VCM_Magazine.Items)
                {
                    if (td.Status == TrayStatus.Load)
                    {
                        Information.WorkTray = td.Slot;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool GetEmptyTray()
        {
            if (cDEF.Work.Recipe.LoaderMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse(Information.VCM_Magazine.Items))
                {
                    if (td.Status == TrayStatus.Empty)
                    {
                        Information.WorkTray = td.Slot;
                        return true;
                    }

                }
            }
            else
            {
                foreach (Tray_Data td in Information.VCM_Magazine.Items)
                {
                    if (td.Status == TrayStatus.Empty)
                    {
                        Information.WorkTray = td.Slot;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool GetUsingTray()
        {
            if (cDEF.Work.Recipe.LoaderMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse(Information.VCM_Magazine.Items))
                {
                    if (td.Status == TrayStatus.Using)
                    {
                        Information.WorkTray = td.Slot;
                        return true;
                    }

                }
            }
            else
            {
                foreach (Tray_Data td in Information.VCM_Magazine.Items)
                {
                    if (td.Status == TrayStatus.Using)
                    {
                        Information.WorkTray = td.Slot;
                        return true;
                    }
                }
            }
            return false;
        }

        #region Running Func
        protected void Running_Loading()
        {
            if (cDEF.Run.Digital.Input[cDI.Transfer_VCMLoading_Overload_1] || cDEF.Run.Digital.Input[cDI.Transfer_VCMLoading_Overload_2])
                TransferX.Stop();
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.VCMLoading_Magazine_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 202;
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 202, "[VCM LOADER] Magazine이 없습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 202, "");
                            Mode = TRunVCMLoaderMode.Stop;
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] No Magazine. Check Magazine", true);
                            return;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_End_Sensor] || cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_ConTact_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 203;
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 203, "[VCM LOADER] Stage 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 203, "");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Tray Detected on Stage. Check Stage", true);
                            Mode = TRunVCMLoaderMode.Stop;
                            return;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Transfer_Loading_Tray_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 204;
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 204, "[VCM LOADER] Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 204, "");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Tray Detected on Transfer. Check Transfer.", true);
                            Mode = TRunVCMLoaderMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 1:
                    Move_Transfer_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Move Ready Position", true);
                    Step++;
                    break;

                case 2:
                    Move_Magazine_WorkingPositionZ(Information.WorkTray);
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Magazine Move Working Position", true);
                    Step++;
                    break;

                case 3:
                    if (cDEF.Run.VCMPicker.Mode != TRunVCMPickerMode.Stop)
                        break;
                    cDEF.Run.VCMPicker.Move_Stage_EjectPositionY();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Stage Move EjectPosition.", true);
                    Step++;
                    break;

                case 4:
                    if (!cDEF.Run.VCMPicker.IsReady())
                        break;
                    StageClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Stage Clamp Unclamp.", true);
                    TransferClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Clamp Unclamp", true);
                    Step++;
                    break;

                case 5:
                    Move_Transfer_MagazinePositionX();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Move Magazine Position.", true);
                    Step++;
                    break;

                case 6:
                    TransferClamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Clamp Clamp", true);
                    Step++;
                    break;

                case 7:
                    Move_Transfer_StagePositionX();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Move Stage Position", true);
                    Step++;
                    break;

                case 8:
                    TransferClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Clamp Unclamp", true);
                    Step++;
                    break;

                case 9:
                    Move_Transfer_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Move Ready Position", true);
                    Step++;
                    break;

                case 10:
                    StageClamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Stage Clamp Clamp", true);
                    Step++;
                    break;

                case 11:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 205;
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 205, "");
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 205, "[VCM LOADER] 센서 감지가 안됨. 자재 확인 바람.");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Not Detected Tray End Sensor.", true);
                            Mode = TRunVCMLoaderMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 12:
                    Information.ExistStage = true;
                    Information.VCM_Magazine.Items[Information.WorkTray].Status = TrayStatus.Using;
                    cDEF.Run.VCMPicker.Information.VCM_Tray.Load();
                    OnVCM_DisplayInit();
                    Step++;
                    break;

                case 13:
                    cDEF.Tact.VCMLoader.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_VCMLoading)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunVCMLoaderMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.VCM, $"[VCM Loader] Loading End - CycleTime : [{cDEF.Tact.VCMLoader.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Unloading()
        {
            if (cDEF.Run.Digital.Input[cDI.Transfer_VCMLoading_Overload_1] || cDEF.Run.Digital.Input[cDI.Transfer_VCMLoading_Overload_2])
                TransferX.Stop();

            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.VCMLoading_Magazine_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 202;
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 202, "[VCM LOADER] Magazine이 없습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 202, "");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] No Magazine. Check Magazine", true);

                            Mode = TRunVCMLoaderMode.Stop;
                            return;
                        }
                        if (!cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 206;
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 206, "[VCM LOADER]Stage 위에 자재가 감지 되지 않았습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 206, "");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Tray Detected on Stage. Check Stage", true);
                            Mode = TRunVCMLoaderMode.Stop;
                            return;
                        }
                        //if (cDEF.Run.Digital.Input[cDI.Transfer_Loading_Tray_Check])
                        //{
                        //    cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 204;
                        //    cDEF.Run.LogWarning(cLog.RunVCMLoader + 204, "");
                        //    //cDEF.Run.LogWarning(cLog.RunVCMLoader + 204, "[VCM LOADER] Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                        //    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Tray Detected on Transfer. Check Transfer.", true);
                        //    Mode = TRunVCMLoaderMode.Stop;
                        //    return;
                        //}
                    }
                    Step++;
                    break;

                case 1:
                    Move_Transfer_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Move Ready Position", true);
                    Step++;
                    break;

                case 2:
                    Move_Magazine_WorkingPositionZ(Information.WorkTray);
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Magazine Move Work Position", true);
                    Step++;
                    break;

                case 3:
                    if (cDEF.Run.VCMPicker.Mode != TRunVCMPickerMode.Stop)
                        break;
                    cDEF.Run.VCMPicker.Move_Stage_EjectPositionY();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Stage Move Eject Position.", true);
                    TransferClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Clamp Unclamp.", true);
                    Step++;
                    break;

                case 4:
                    if (!cDEF.Run.VCMPicker.IsReady())
                        break;
                    Move_Transfer_StagePositionX();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Move Stage Position", true);
                    Step++;
                    break;

                case 5:
                    StageClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Stage Clamp Unclamp.", true);
                    Step++;
                    break;

                case 6:
                    Move_Transfer_MagazinePositionX();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Move Magazine Position.", true);
                    Step++;
                    break;

                case 7:
                    Move_Transfer_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Transfer Move Ready Position.", true);
                    Step++;
                    break;

                case 8:
                    Step++;
                    break;

                case 9:
                    Step++;
                    break;

                case 10:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 203;
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 203, "[VCM LOADER] Stage 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 203, "");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Tray Detected on Stage. Check Stage.", true);
                            Mode = TRunVCMLoaderMode.Stop;
                            return;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Transfer_Loading_Tray_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunVCMLoader + 204;
                            //cDEF.Run.LogWarning(cLog.RunVCMLoader + 204, "[VCM LOADER] Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunVCMLoader + 204, "");
                            cDEF.TaskLogAppend(TaskLog.VCM, "[VCM Loader] Tray Detected On Transfer. Check Transfer.",true);
                            Mode = TRunVCMLoaderMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 11:
                    Information.VCM_Magazine.Items[Information.WorkTray].Status = TrayStatus.Finish;
                    Information.ExistStage = false;
                    cDEF.TaskLogAppend(TaskLog.VCM, $"[VCM Loader] Unload Change Status [{Information.WorkTray}].", true);
                    Step++;
                    break;

                case 12:
                    //cDEF.Tact.VCMLoader.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_VCMUnloading)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunVCMLoaderMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.VCM, $"[VCM Unloading] End. - Cycle Time[{cDEF.Tact.VCMLoader.CycleTime.ToString("N3")}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }


        #endregion
        #region Move Command
        public void Move_Magazine_ReadyPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayZ;
            MagazineZ.Absolute(cDEF.Work.TeachVCMLoader.ReadyPositionZ, Sleep);
        }
        public void Move_Magazine_WorkingPositionZ(int Slot)
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayZ;

            int pos = 0;
            pos = cDEF.Work.TeachVCMLoader.ReadyPositionZ;
            pos += Slot * cDEF.Work.VCMLoader.SlotPitchZ;

            MagazineZ.Absolute(pos, Sleep);
        }
        public void Move_Magazine_Relative_UpZ()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayZ;

            int pos = 0;
            if (cDEF.Work.TeachVCMLoader.InOutOffsetZ < cDEF.Work.VCMLoader.SlotPitchZ / 2)
                pos = cDEF.Work.TeachVCMLoader.InOutOffsetZ;

            MagazineZ.Relative(pos, Sleep);
        }
        // Transfer
        public void Move_Transfer_ReadyPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayX;
            TransferX.Absolute(cDEF.Work.TeachVCMLoader.ReadyPositionX, Sleep);
        }
        public void Move_Transfer_StagePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayX;
            TransferX.Absolute(cDEF.Work.TeachVCMLoader.StagePositionX, Sleep);
        }
        public void Move_Transfer_MagazinePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMLoader.MovingDelayX;
            TransferX.Absolute(cDEF.Work.TeachVCMLoader.MagazinePositionX, Sleep);
        }
        
        #endregion

        #region CheckPosition
        public bool Is_Magazine_ReadyPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachVCMLoader.ReadyPositionZ, MagazineZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Magazine_WorkingPositionZ(int slot)
        {
            int pos = cDEF.Work.TeachVCMLoader.ReadyPositionZ;
            pos += slot * cDEF.Work.VCMLoader.SlotPitchZ;
            if (IsRange((double)pos, MagazineZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_ReadyPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachVCMLoader.ReadyPositionX, TransferX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_StagePositionX()
        {
            if (IsRange((double)cDEF.Work.TeachVCMLoader.StagePositionX, TransferX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_MagazinePositionX()
        {
            if (IsRange((double)cDEF.Work.TeachVCMLoader.MagazinePositionX, TransferX.ActualPosition))
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

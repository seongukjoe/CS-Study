using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;
using System.Linq;

namespace XModule.Running
{

    public class RunLensLoaderInformation : fpObject
    {

        #region 변수
        private bool FExistStage = false;

        private int FWorkTray = 0;

        private Magazine_Data FLens_Magazine;
        #endregion

        #region Property
        public bool ExistStage
        {
            get { return FExistStage; }
            set
            {
                if (FExistStage != value)
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
                if (FWorkTray != value)
                {
                    FWorkTray = value;
                    Change();
                }
            }
        }
        public Magazine_Data Lens_Magazine
        {
            get { return FLens_Magazine; }
        }
        


        #endregion

        public RunLensLoaderInformation() : base()
        {
            FLens_Magazine = new Magazine_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunLensLoader.dat";
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
    public enum TRunLensLoaderMode
    {
        Stop,
        Loading,                // Magazine -> Stage
        Unloading               // Stage -> Magazine
    };

    
    public class RunLensLoader : TfpRunningModule 
    {
        //Evnet
        public delegate void LensDisplayInitHandler();
        public event LensDisplayInitHandler OnLens_DisplayInit;

        private RunLensLoaderInformation FInformation;
        private TRunLensLoaderMode FMode;

        public int FCalc;

        public RunLensLoader(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunLensLoaderInformation();
        }
        

        #region **Property**
        public RunLensLoaderInformation Information
        {
            get { return FInformation; }
        }

        public TRunLensLoaderMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        // Motor
        public TfpMotionItem MagazineZ
        {
            get { return GetMotions(0); }
        }
        public TfpMotionItem TransferY
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

        private TRunLensLoaderMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunLensLoaderMode.Stop;
        }
        private void SetMode(TRunLensLoaderMode Value)
        {
            if (Value == TRunLensLoaderMode.Stop)
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
                case TRunLensLoaderMode.Stop:
                    return "Stop";

                case TRunLensLoaderMode.Loading:
                    return "Loading";

                case TRunLensLoaderMode.Unloading:
                    return "Unloading";

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
                    StageClamp.Backward();
                    TransferClamp.Backward();
                    Step++;
                    break;

                case 1:
                    TransferY.Home();
                    Step++;
                    break;

                case 2:
                    if (!TransferY.HomeEnd)
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
                    Move_Transfer_ReadyPositionY();
                    Step++;
                    break;

                case 1:
                    Step++;
                    break;

                case 2:
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
                    Move_Transfer_ReadyPositionY();
                    Step++;
                    break;
                case 1:
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
                case TRunLensLoaderMode.Loading:
                    Running_Loading();
                    break;

                case TRunLensLoaderMode.Unloading:
                    Running_Unloading();
                    break;
            }
        }
        
        protected override void ProcMain()
        {
            if (cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if (Information.ExistStage)  //&& Tray 자재 Pick Up 끝
                {
                    if (cDEF.Run.LensPicker.Information.Lens_Tray.IsFinish())
                    {
                        if (GetUsingTray())
                        {
                            Mode = TRunLensLoaderMode.Unloading;
                            return;
                        }
                        else
                        {
                            if (GetEmptyTray())
                            {
                                Mode = TRunLensLoaderMode.Unloading;
                                return;
                            }
                            else
                            {
                                cDEF.Run.SetAlarmID = cLog.RunLensLoader + 201;
                                cDEF.Run.LogWarning(cLog.RunLensLoader + 201, "[LENS LOADER] 투입할 Tray 공간이 없습니다.");
                                cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] No Slot In Magazine for Unloading", true);
                                return;
                            }
                        }
                    }
                }

                if (!Information.ExistStage)
                {
                    if (GetWorkTray())
                    {
                        cDEF.Tact.LensLoader.Start();
                        Mode = TRunLensLoaderMode.Loading;       // Loading
                        return;
                    }
                    else
                    {
                        if (cDEF.Run.LotEnd)
                            return;
                        cDEF.Run.SetAlarmID = cLog.RunLensLoader + 200;
                        //cDEF.Run.LogWarning(cLog.RunLensLoader + 200, "[LENS LOADER] 투입할 Tray가 없습니다.");
                        cDEF.Run.LogWarning(cLog.RunLensLoader + 200, "");
                        cDEF.TaskLogAppend(TaskLog.Lens,"[Lens Loader] No Tray in Magazine For Loading", true);
                        return;
                    }
                }


                
            }

            if (cDEF.Run.Mode == TRunMode.Manual_LensLoading)
            {
                if (GetWorkTray())
                {
                    cDEF.Tact.LensLoader.Start();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader - Manual] Loading Start", true);
                    Mode = TRunLensLoaderMode.Loading;
                    return;
                }
            }

            if (cDEF.Run.Mode == TRunMode.Manual_LensUnloading)
            {
                if (GetUsingTray())
                {
                    Mode = TRunLensLoaderMode.Unloading;
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader - Manual] UnLoading Start", true);

                    return;
                }
                else
                {
                    if (GetEmptyTray())
                    {
                        Mode = TRunLensLoaderMode.Unloading;
                        cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader - Manual] UnLoading Start", true);

                        return;
                    }
                }
            }
        }
        private bool GetWorkTray()
        {
            if (cDEF.Work.Recipe.LensMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse(Information.Lens_Magazine.Items))
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
                foreach (Tray_Data td in Information.Lens_Magazine.Items)
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
            if (cDEF.Work.Recipe.LensMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse(Information.Lens_Magazine.Items))
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
                foreach (Tray_Data td in Information.Lens_Magazine.Items)
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
        private bool GetUsingTray()
        {
            if (cDEF.Work.Recipe.LensMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse(Information.Lens_Magazine.Items))
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
                foreach (Tray_Data td in Information.Lens_Magazine.Items)
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
            if (cDEF.Run.Digital.Input[cDI.Transfer_LensLoading_Overload_1] || cDEF.Run.Digital.Input[cDI.Transfer_LensLoading_Overload_2])
                TransferY.Stop();

            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.LENSLoading_Magazine_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensLoader + 202;
                            //cDEF.Run.LogWarning(cLog.RunLensLoader + 202, "[LENS LOADER] Magazine이 없습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunLensLoader + 202, "");
                            cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] No Magazine. Check Magazine",true);
                            Mode = TRunLensLoaderMode.Stop;
                            return;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Stage_LensLoading_Tray_End_Sensor] || cDEF.Run.Digital.Input[cDI.Stage_LensLoading_Tray_Contact_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensLoader + 203;
                            //cDEF.Run.LogWarning(cLog.RunLensLoader + 203, "[LENS LOADER] Stage 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunLensLoader + 203, "");
                            cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Tray Detected On Stage. Check Stage", true);
                            Mode = TRunLensLoaderMode.Stop;
                            return;
                        }
                        //if (cDEF.Run.Digital.Input[cDI.Transfer_Lens_Tray_Check])
                        //{
                        //    cDEF.Run.SetAlarmID = cLog.RunLensLoader + 204;
                        //    //cDEF.Run.LogWarning(cLog.RunLensLoader + 204, "[LENS LOADER] Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                        //    cDEF.Run.LogWarning(cLog.RunLensLoader + 204, "");
                        //    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Tray Detected On Transfer. Check Transfer", true);
                        //    Mode = TRunLensLoaderMode.Stop;
                        //    return;
                        //}
                      
                    }
                    Step++;
                    break;

                case 1:
                    Move_Transfer_ReadyPositionY();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Move Transfer Ready Position", true);
                    Step++;
                    break;

                case 2:
                    Move_Magazine_WorkingPositionZ(Information.WorkTray);
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Move Magazine Working Position Z", true);
                    Step++;
                    break;

                case 3:
                    if (cDEF.Run.LensPicker.Mode != TRunLensPickerMode.Stop)
                        break;
                    cDEF.Run.LensPicker.Move_Stage_EjectPositionX();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Move Stage Eject Position X", true);
                    Step++;
                    break;

                case 4:
                    if (!cDEF.Run.LensPicker.IsReady())
                        break;
                    StageClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Stage Clamp UnClamp", true);
                    TransferClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Transfer Clamp Unclamp", true); 
                    Step++;
                    break;

                case 5:
                    Move_Transfer_MagazinePositionY();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Move Transfer Magazine Position Y", true);
                    Step++;
                    break;

                case 6:
                    TransferClamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Transfer Clamp Clamp", true);
                    Step++;
                    break;

                case 7:
                    Move_Transfer_StagePositionY();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Move Transfer Stage Position Y", true);
                    Step++;
                    break;

                case 8:
                    TransferClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Transfer Clamp Unclamp", true);
                    Step++;
                    break;

                case 9:
                    Move_Transfer_ReadyPositionY();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Move Transfer Ready Position", true);
                    Step++;
                    break;

                case 10:
                    StageClamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Stage Clamp Clamp", true);
                    Step++;
                    break;

                case 11:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.Stage_LensLoading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensLoader + 205;
                            //cDEF.Run.LogWarning(cLog.RunLensLoader + 205, "[LENS LOADER] 센서 감지가 안됨. 자재 확인 바람.");
                            cDEF.Run.LogWarning(cLog.RunLensLoader + 205, "");
                            cDEF.TaskLogAppend(TaskLog.Lens, "[Lens Loader] Not Detected Tray End Sensor.", true);
                            Mode = TRunLensLoaderMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 12:
                    Information.ExistStage = true;
                    Information.Lens_Magazine.Items[Information.WorkTray].Status = TrayStatus.Using;
                    cDEF.Run.LensPicker.Information.Lens_Tray.Load();
                    OnLens_DisplayInit();
                    Step++;
                    break;

                case 13:
                    cDEF.Tact.LensLoader.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_LensLoading)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunLensLoaderMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Loading] End - Cycle Time [{cDEF.Tact.LensLoader.CycleTime.ToString("N3")}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Unloading()
        {
            if (cDEF.Run.Digital.Input[cDI.Transfer_LensLoading_Overload_1] || cDEF.Run.Digital.Input[cDI.Transfer_LensLoading_Overload_2])
                TransferY.Stop();

            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.LENSLoading_Magazine_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensLoader + 202;
                            //cDEF.Run.LogWarning(cLog.RunLensLoader + 202, "[LENS LOADER] Magazine이 없습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunLensLoader + 202, "");
                            Mode = TRunLensLoaderMode.Stop;
                            return;
                        }
                        if (!cDEF.Run.Digital.Input[cDI.Stage_LensLoading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunLensLoader + 206;
                            //cDEF.Run.LogWarning(cLog.RunLensLoader + 206, "[LENS LOADER] Stage 위에 자재가 감지 되지 않았습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunLensLoader + 206, "");
                            Mode = TRunLensLoaderMode.Stop;
                            return;
                        }
                        //if (cDEF.Run.Digital.Input[cDI.Transfer_Lens_Tray_Check])
                        //{
                        //    cDEF.Run.SetAlarmID = cLog.RunLensLoader + 207;
                        //    //cDEF.Run.LogWarning(cLog.RunLensLoader + 207, "[LENS LOADER] Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                        //    cDEF.Run.LogWarning(cLog.RunLensLoader + 207, "");
                        //    Mode = TRunLensLoaderMode.Stop;
                        //    return;
                        //}
                        //if (cDEF.Run.LotEnd && cDEF.Run.LensPicker.Information.HeadStatus != TrayStatus.Empty)
                        //{
                        //    cDEF.Run.LogWarning(cLog.RunLensLoader + 208, "[LENS LOADER] Lens Picker Head에 자재 정보가 있습니다. 확인 바랍니다.");
                        //    Mode = TRunLensLoaderMode.Stop;
                        //    return;
                        //}
                    }
                    Step++;
                    break;

                case 1:
                    Move_Transfer_ReadyPositionY();
                    Step++;
                    break;

                case 2:
                    Move_Magazine_WorkingPositionZ(Information.WorkTray);
                    Step++;
                    break;

                case 3:
                    if (cDEF.Run.LensPicker.Mode != TRunLensPickerMode.Stop)
                        break;
                    cDEF.Run.LensPicker.Move_Stage_EjectPositionX();
                    TransferClamp.Backward();
                    Step++;
                    break;

                case 4:
                    if (!cDEF.Run.LensPicker.IsReady())
                        break;
                    Move_Transfer_StagePositionY();
                    Step++;
                    break;

                case 5:
                    StageClamp.Backward();
                    Step++;
                    break;

                case 6:
                    Move_Transfer_MagazinePositionY();
                    Step++;
                    break;

                case 7:
                    Move_Transfer_ReadyPositionY();
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
                        if (cDEF.Run.Digital.Input[cDI.Stage_LensLoading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = 1000 + 4;
                            //cDEF.Run.LogWarning(1000 + 4, "Stage 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(1000 + 4, "");
                            Mode = TRunLensLoaderMode.Stop;
                            return;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Transfer_Lens_Tray_Check])
                        {
                            cDEF.Run.SetAlarmID = 1000 + 5;
                            //cDEF.Run.LogWarning(1000 + 5, "Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(1000 + 5, "");
                            Mode = TRunLensLoaderMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 11:
                    Information.Lens_Magazine.Items[Information.WorkTray].Status = TrayStatus.Finish;
                    Information.ExistStage = false;
                    Step++;
                    break;

                case 12:
                    if (cDEF.Run.Mode == TRunMode.Manual_LensUnloading)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunLensLoaderMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[Lens Unloading] End - Cycle Time [{cDEF.Tact.LensLoader.CycleTime.ToString("N3")}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }


        #endregion
        #region Move Command
        //Magazine
        public void Move_Magazine_ReadyPositionZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayZ;
            MagazineZ.Absolute(cDEF.Work.TeachLensLoader.ReadyPositionZ, Sleep);
        }
        public void Move_Magazine_WorkingPositionZ(int Slot)
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayZ;

            int pos = 0;
            pos = cDEF.Work.TeachLensLoader.ReadyPositionZ;
            pos += Slot * cDEF.Work.LensLoader.SlotPitchZ;

            MagazineZ.Absolute(pos, Sleep);
        }
        public void Move_Magazine_Relative_UpZ()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayZ;

            int pos = 0;
            if (cDEF.Work.TeachLensLoader.InOutOffsetZ < cDEF.Work.LensLoader.SlotPitchZ / 2)
                pos = cDEF.Work.TeachLensLoader.InOutOffsetZ;
            MagazineZ.Relative(pos, Sleep);
        }
        //Transfer
        public void Move_Transfer_ReadyPositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayY;
            TransferY.Absolute(cDEF.Work.TeachLensLoader.ReadyPositionY, Sleep);
        }
        public void Move_Transfer_StagePositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayY;
            TransferY.Absolute(cDEF.Work.TeachLensLoader.StagePositionY, Sleep);
        }
        public void Move_Transfer_MagazinePositionY()
        {
            int Sleep;
            Sleep = cDEF.Work.LensLoader.MovingDelayY;
            TransferY.Absolute(cDEF.Work.TeachLensLoader.MagazinePositionY, Sleep);
        }
        
        #endregion

    #region CheckPosition
        public bool Is_Magazine_ReadyPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachLensLoader.ReadyPositionZ, MagazineZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Magazine_WorkingPositionZ(int Slot)
        {
            int pos = cDEF.Work.TeachLensLoader.ReadyPositionZ;
            pos += Slot * cDEF.Work.LensLoader.SlotPitchZ;
            if (IsRange((double)pos, MagazineZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_ReadyPositionY()
        {
            if (IsRange((double)cDEF.Work.TeachLensLoader.ReadyPositionY, TransferY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_StagePositionY()
        {
            if (IsRange((double)cDEF.Work.TeachLensLoader.StagePositionY, TransferY.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_MagazinePositionY()
        {
            if (IsRange((double)cDEF.Work.TeachLensLoader.MagazinePositionY, TransferY.ActualPosition))
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

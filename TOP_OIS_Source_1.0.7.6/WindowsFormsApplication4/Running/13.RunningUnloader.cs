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

    public class RunUnloaderInformation : fpObject
    {

        #region 변수
        private bool FExistStage = false;

        private int FWorkTray = 0;

        private Magazine_Data FUnloader_Magazine;
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
        public Magazine_Data Unloader_Magazine
        {
            get { return FUnloader_Magazine; }
        }
        


        #endregion

        public RunUnloaderInformation() : base()
        {
            FUnloader_Magazine = new Magazine_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunUnloader.dat";
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
            
            Unlock(Ignore);
        }
    }
    public enum TRunUnloaderMode
    {
        Stop,
        Loading,                // Magazine -> Stage
        Unloading               // Stage -> Magazine
    };

    
    public class RunUnloader : TfpRunningModule 
    {
        //Evnet
        public delegate void UnloadDisplayInitHandler();
        public event UnloadDisplayInitHandler OnUnload_DisplayInit;
        public delegate void NgDisplayInitHandler();
        public event NgDisplayInitHandler OnNg_DisplayInit;

        private RunUnloaderInformation FInformation;
        private TRunUnloaderMode FMode;

        public int FCalc;

        public RunUnloader(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunUnloaderInformation();
        }
        

        #region **Property**
        public RunUnloaderInformation Information
        {
            get { return FInformation; }
        }

        public TRunUnloaderMode Mode
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

        private TRunUnloaderMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunUnloaderMode.Stop;
        }
        private void SetMode(TRunUnloaderMode Value)
        {
            if (Value == TRunUnloaderMode.Stop)
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
                case TRunUnloaderMode.Stop:
                    return "Stop";
                case TRunUnloaderMode.Loading:
                    return "Loading";
                case TRunUnloaderMode.Unloading:
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
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Initialize] End", true);
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
                    cDEF.TaskLogAppend(TaskLog.Unload, "[To-Run] Done.", true);
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
                    cDEF.TaskLogAppend(TaskLog.Unload, "[To-Stop] Done.", true);
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
                case TRunUnloaderMode.Loading:
                    Running_Loading();
                    break;

                case TRunUnloaderMode.Unloading:
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
                    if (cDEF.Run.UnloadPicker.Information.Unloader_Tray.IsFinish())
                    {
                        if (GetUsingTray())
                        {
                            Mode = TRunUnloaderMode.Unloading;
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Unloading Start.", true);
                            return;
                        }
                        else
                        {
                            if (GetEmptyTray())
                            {
                                Mode = TRunUnloaderMode.Unloading;
                                cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Unloading Start.", true);
                                return;
                            }
                            else
                            {
                                cDEF.Run.SetAlarmID = cLog.RunUnloader + 200;
                                //cDEF.Run.LogWarning(cLog.RunUnloader + 200, "[Unloader] 투입할 Tray 공간이 없습니다.");
                                cDEF.Run.LogWarning(cLog.RunUnloader + 200, "");
                                cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] No Space for Tray to Unload.", true);
                                return;
                            }
                        }
                    }
                }

                if (!Information.ExistStage)
                {
                    if (GetWorkTray())
                    {
                        cDEF.Tact.Unloader.Start();
                        Mode = TRunUnloaderMode.Loading;       // Loading
                        cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Loading Start.", true);
                        return;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunUnloader + 200;
                        //cDEF.Run.LogWarning(cLog.RunUnloader + 200, "[Unloader] 투입할 Tray 공간이 없습니다.");
                        cDEF.Run.LogWarning(cLog.RunUnloader + 200, "");
                        cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] No Tray to Load.", true);
                        return;
                    }
                }


                
            }

            if (cDEF.Run.Mode == TRunMode.Manual_UnloaderLoading)
            {
                if (GetWorkTray())
                {
                    Mode = TRunUnloaderMode.Loading;
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unloader - Manual] Loading Start.", true);
                    return;
                }
            }

            if (cDEF.Run.Mode == TRunMode.Manual_UnloaderUnloading)
            {
                if (GetUsingTray())
                {
                    Mode = TRunUnloaderMode.Unloading;
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unloader - Manual] Unloading Start.", true);
                    return;
                }
                else
                {
                    if (GetEmptyTray())
                    {
                        Mode = TRunUnloaderMode.Unloading;
                        cDEF.TaskLogAppend(TaskLog.Unload, "[Unloader - Manual] Unloading Start.", true);
                        return;
                    }
                }
            }
        }
        private bool GetWorkTray()
        {
            if (cDEF.Work.Recipe.UnLoaderMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse( Information.Unloader_Magazine.Items))
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
                foreach (Tray_Data td in Information.Unloader_Magazine.Items)
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
            if (cDEF.Work.Recipe.UnLoaderMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse(Information.Unloader_Magazine.Items))
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
                foreach (Tray_Data td in Information.Unloader_Magazine.Items)
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
            if (cDEF.Work.Recipe.UnLoaderMagazineDirection)
            {
                foreach (Tray_Data td in System.Linq.Enumerable.Reverse(Information.Unloader_Magazine.Items))
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
                foreach (Tray_Data td in Information.Unloader_Magazine.Items)
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
            if (cDEF.Run.Digital.Input[cDI.Transfer_VCMUnloading_Overload_1] || cDEF.Run.Digital.Input[cDI.Transfer_VCMUnloading_Overload_2])
                TransferX.Stop();

            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.VCMUnloading_Magazine_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloader + 201;
                            //cDEF.Run.LogWarning(cLog.RunUnloader + 201, "[Unloader] Magazine이 없습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunUnloader + 201, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] No Magazine. Check Magazine.", true);
                            Mode = TRunUnloaderMode.Stop;
                            return;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Stage_Unloading_Tray_End_Sensor] || cDEF.Run.Digital.Input[cDI.Stage_Unloading_Tray_Contact_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloader + 202;
                            //cDEF.Run.LogWarning(cLog.RunUnloader + 202, "[Unloader] Stage 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunUnloader + 202, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Tray Detected On Stage. Check Stage.", true);
                            Mode = TRunUnloaderMode.Stop;
                            return;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Transfer_Unloading_Tray_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloader + 203;
                            //cDEF.Run.LogWarning(cLog.RunUnloader + 203, "[Unloader] Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunUnloader + 203, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Tray Detected On Transfer. Check Transfer.", true);
                            Mode = TRunUnloaderMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 1:
                    Move_Transfer_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Transfer Ready Position X.", true);
                    Step++;
                    break;

                case 2:
                    Move_Magazine_WorkingPositionZ(Information.WorkTray);
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Magazine Work Position Z.", true);
                    Step++;
                    break;

                case 3:
                    if (cDEF.Run.UnloadPicker .Mode != TRunUnloadPickerMode.Stop)
                        break;

                        cDEF.Run.UnloadPicker.Move_Stage_EjectPositionY();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Stage Eject Position Y.", true);
                    Step++;
                    break;

                case 4:
                    if (!cDEF.Run.UnloadPicker.IsReady())
                        break;
                    StageClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Stage Clamp Unclamp.", true);
                    TransferClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Transfer Clamp Unclamp.", true);
                    Step++;
                    break;

                case 5:
                    Move_Transfer_MagazinePositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Transfer Magazine Position X.", true);
                    Step++;
                    break;

                case 6:
                    TransferClamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Transfer Clamp Clamp.", true);
                    Step++;
                    break;

                case 7:
                    Move_Transfer_StagePositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Transfer Stage Position X.", true);
                    Step++;
                    break;

                case 8:
                    TransferClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Transfer Clamp Unclamp.", true);
                    Step++;
                    break;

                case 9:
                    Move_Transfer_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Transfer Ready Position X.", true);
                    Step++;
                    break;

                case 10:
                    StageClamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Stage Clamp Clamp.", true);
                    Step++;
                    break;

                case 11:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.Stage_Unloading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloader + 204;
                            //cDEF.Run.LogWarning(cLog.RunUnloader + 204, "[Unloader] 센서 감지가 안됨. 자재 확인 바람.");
                            cDEF.Run.LogWarning(cLog.RunUnloader + 204, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Not Detected Tray. Check Stage.", true);
                            Mode = TRunUnloaderMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 12:
                    Information.ExistStage = true;
                    Information.Unloader_Magazine.Items[Information.WorkTray].Status = TrayStatus.Using;
                    cDEF.Run.UnloadPicker.Information.Unloader_Tray.Load();
                    OnUnload_DisplayInit();
                    Step++;
                    break;

                case 13:
                    cDEF.Tact.Unloader.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_UnloaderLoading)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunUnloaderMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Unload, $"[Unload Picker] Loading End - [{cDEF.Tact.Unloader.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Unloading()
        {
            if (cDEF.Run.Digital.Input[cDI.Transfer_VCMUnloading_Overload_1] || cDEF.Run.Digital.Input[cDI.Transfer_VCMUnloading_Overload_2])
                TransferX.Stop();

            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.VCMUnloading_Magazine_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloader + 201;
                            //cDEF.Run.LogWarning(cLog.RunUnloader + 201, "[Unloader] Magazine이 없습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunUnloader + 201, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] No Magazine.Check Magazine. ", true);
                            Mode = TRunUnloaderMode.Stop;
                            return;
                        }
                        if (!cDEF.Run.Digital.Input[cDI.Stage_Unloading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloader + 202;
                            //cDEF.Run.LogWarning(cLog.RunUnloader + 202, "[Unloader] Stage 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunUnloader + 202, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Tray Not Detected On Stage. Check Stage.", true);
                            Mode = TRunUnloaderMode.Stop;
                            return;
                        }
                        //if (cDEF.Run.Digital.Input[cDI.Transfer_Unloading_Tray_Check])
                        //{
                        //    cDEF.Run.LogWarning(1000 + 5, "Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                        //    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Tray Detected On Transfer. Check Transfer.", true);
                        //    Mode = TRunUnloaderMode.Stop;
                        //    return;
                        //}
                    }
                    Step++;
                    break;

                case 1:
                    Move_Transfer_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Transfer Ready Position X.", true);
                    Step++;
                    break;

                case 2:
                    Move_Magazine_WorkingPositionZ(Information.WorkTray);
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Magazine Work Position Z.", true);
                    Step++;
                    break;

                case 3:
                    if (cDEF.Run.UnloadPicker.Mode != TRunUnloadPickerMode.Stop)
                        break;


                    if (!cDEF.Run.Unloader.Is_Transfer_ReadyPositionX())
                        break;
                    if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        break;

                    cDEF.Run.UnloadPicker.Move_Stage_EjectPositionY();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Stage Eject Position.", true);
                    TransferClamp.Backward();
                    Step++;
                    break;

                case 4:
                    if (!cDEF.Run.UnloadPicker.IsReady())
                        break;
                    Move_Transfer_StagePositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Transfer Stage Position X.", true);
                    Step++;
                    break;

                case 5:
                    StageClamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Stage Clamp Unclamp.", true);
                    Step++;
                    break;

                case 6:
                    Move_Transfer_MagazinePositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Transfer Magazine Position.", true);
                    Step++;
                    break;

                case 7:
                    Move_Transfer_ReadyPositionX();
                    cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Move Transfer Ready Position X.", true);
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
                        if (cDEF.Run.Digital.Input[cDI.Stage_Unloading_Tray_End_Sensor])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloader + 202;
                            //cDEF.Run.LogWarning(cLog.RunUnloader + 202, "[Unloader] Stage 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunUnloader + 202, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Tray Detected On Stage. Check Stage.", true);
                            Mode = TRunUnloaderMode.Stop;
                            return;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Transfer_Unloading_Tray_Check])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunUnloader + 203;
                            //cDEF.Run.LogWarning(cLog.RunUnloader + 203, "[Unloader] Transfer 위에 자재가 감지 되었습니다. 확인 바랍니다.");
                            cDEF.Run.LogWarning(cLog.RunUnloader + 203, "");
                            cDEF.TaskLogAppend(TaskLog.Unload, "[Unload Picker] Tray Detected On Transfer. Check Transfer.", true);
                            Mode = TRunUnloaderMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 11:
                    Information.Unloader_Magazine.Items[Information.WorkTray].Status = TrayStatus.Finish;
                    Information.ExistStage = false;
                    Step++;
                    break;

                case 12:
                    //cDEF.Tact.Unloader.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_UnloaderUnloading)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunUnloaderMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Unload, $"[Unload Picker] Unloading End - Cycle Time : [{cDEF.Tact.Unloader.CycleTime.ToString("N3")}]", true);
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
            Sleep = cDEF.Work.Unloader.MovingDelayZ;
            MagazineZ.Absolute(cDEF.Work.TeachUnloader.ReadyPositionZ, Sleep);
        }
        public void Move_Magazine_WorkingPositionZ(int Slot)
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayZ;

            int pos = 0;
            pos = cDEF.Work.TeachUnloader.ReadyPositionZ;
            pos += Slot * cDEF.Work.Unloader.SlotPitchZ;

            MagazineZ.Absolute(pos, Sleep);
        }
        public void Move_Magazine_Relative_UpZ()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayZ;

            int pos = 0;
            if (cDEF.Work.TeachUnloader.InOutOffsetZ < cDEF.Work.Unloader.SlotPitchZ / 2)
                pos = cDEF.Work.TeachUnloader.InOutOffsetZ;
            MagazineZ.Absolute(pos, Sleep);
        }
        //Transfer
        public void Move_Transfer_ReadyPositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayX;
            TransferX.Absolute(cDEF.Work.TeachUnloader.ReadyPositionX, Sleep);
        }
        public void Move_Transfer_StagePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayX;
            TransferX.Absolute(cDEF.Work.TeachUnloader.StagePositionX, Sleep);
        }
        public void Move_Transfer_MagazinePositionX()
        {
            int Sleep;
            Sleep = cDEF.Work.Unloader.MovingDelayX;
            TransferX.Absolute(cDEF.Work.TeachUnloader.MagazinePositionX, Sleep);
        }
        //Stage
        
        #endregion

    #region CheckPosition
        public bool Is_Magazine_ReadyPositionZ()
        {
            if (IsRange((double)cDEF.Work.TeachUnloader.ReadyPositionZ, MagazineZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Magazine_WorkingPositionZ(int Slot)
        {
            int pos = cDEF.Work.TeachUnloader.ReadyPositionZ;
            pos += Slot * cDEF.Work.Unloader.SlotPitchZ;

            if (IsRange((double)pos, MagazineZ.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_ReadyPositionX()
        {
            if (IsRange((double)cDEF.Work.TeachUnloader.ReadyPositionX, TransferX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_StagePositionX()
        {
            if (IsRange((double)cDEF.Work.TeachUnloader.StagePositionX, TransferX.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_Transfer_MagazinePositionX()
        {
            if (IsRange((double)cDEF.Work.TeachUnloader.MagazinePositionX, TransferX.ActualPosition))
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

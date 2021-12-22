using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;
using System.Runtime.InteropServices.WindowsRuntime;

namespace XModule.Running
{

    public class RunIndexInformation : fpObject
    {

        #region 변수
        //public LensIndexStatus SpareStatus;
        //public LensIndexStatus VCMStatus;
        //public LensIndexStatus LensStatus;
        //public Index_Data SpareData;
        public Index_Data VCMData;
        public Index_Data LensData;


        public int IndexNum = 0;
        #endregion

        #region Property

        #endregion

        public RunIndexInformation() : base()
        {
            //SpareData = new Index_Data();
            VCMData = new Index_Data();
            LensData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunIndex.dat";
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
            //SpareData.Clear();
            VCMData.Clear();
            LensData.Clear();
            Unlock(Ignore);
        }
    }
    public enum TRunIndexMode
    {
        Stop,
        StepMove,                  //  StepMoving
    };

    
    public class RunIndex : TfpRunningModule 
    {
        private RunIndexInformation FInformation;
        private TRunIndexMode FMode;

        public int FCalc;

        public RunIndex(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunIndexInformation();
        }
        

        #region **Property**
        public RunIndexInformation Information
        {
            get { return FInformation; }
        }

        public TRunIndexMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        // Motor
        public TfpMotionItem Index
        {
            get { return GetMotions(0); }
        }
        #endregion //Property//

        private TRunIndexMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunIndexMode.Stop;
        }
        private void SetMode(TRunIndexMode Value)
        {
            if (Value == TRunIndexMode.Stop)
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
                case TRunIndexMode.Stop:
                    return "Stop";
                case TRunIndexMode.StepMove:
                    return "Step Moving";

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
                    
                    if (!cDEF.Run.CleanJig.CleanJigDown.IsBackward())
                        break;
                    if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        break;
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        break;
                    if (cDEF.Run.Digital.Output[cDO.Lens_Height_Measure])
                        break;
                    if (!cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionZ())
                        break;
                    if (!cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionZ())
                        break;
                    if (!cDEF.Run.Curing1.Contact.IsBackward())
                        break;
                    if (!cDEF.Run.Curing1.UVDown.IsBackward())
                        break;
                    if (!cDEF.Run.Curing2.UVDown.IsBackward())
                        break;
                    if (!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
                        break;
                    if (!cDEF.Run.VCMPicker.StageY.HomeEnd || !cDEF.Run.UnloadPicker.StageY.HomeEnd)
                        break;

                    if (cDEF.Work.Project.GlobalOption.UseActAction3
                        && (!cDEF.Run.Act3.ClampDown.IsBackward() || !cDEF.Run.Act3.Contact.IsBackward()))
                        break;

                    Step++;
                    break;
                case 1:
                    Index.Home();
                    Step++;
                    break;
                case 2:
                    if (!Index.HomeEnd)
                        break;
                    Step++;
                    break;
                case 3:
                    Information.IndexNum %= 12;
                    Move_Index(Information.IndexNum);
                    Step++;
                    break;
                case 4:
                    Index.Config.Update();
                    Step++;
                    break;
                case 5:
                    Step++;
                    break;
                case 6:
                    cDEF.TaskLogAppend(TaskLog.Index, "[Initialize] End", true);
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
                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        if (cDEF.Mes.Device=="" || cDEF.Mes.Operation == "" 
                            || cDEF.Mes.Product_Type =="" || cDEF.Mes.EQPName == "")
                        {
                            cDEF.Run.SetAlarmID = cLog.RunIndex + 201;
                            //MES DEVICE,OPERATION,PRODUCT_TYPE D  Empty
                            cDEF.Run.LogWarning(cLog.RunIndex + 201, "Check MES Data");
                            return false;
                        }
                    }
                    Step++;
                    break;

                case 1:
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;

                case 3:
                    cDEF.TaskLogAppend(TaskLog.Index, "[To-Run] Done.", true);
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
                    cDEF.TaskLogAppend(TaskLog.Index, "[To-Stop] Done.", true);
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
                case TRunIndexMode.StepMove:
                    Running_StepMoving();
                    break;
                
            }
        }
        
        protected override void ProcMain()
        {
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if(Product_On_Index())
                {
                    if(FinishCheck())
                    {
                        if (cDEF.Run.VCMPicker.Mode == TRunVCMPickerMode.Place)
                            return;
                        if (cDEF.Run.LensPicker.Mode == TRunLensPickerMode.Place)
                            return;
                        if (!cDEF.Run.CleanJig.CleanJigDown.IsBackward())
                            return;
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ()
                            && cDEF.Run.VCMPicker.Is_Head_IndexPlacePositionX())
                            return;
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ()
                            && cDEF.Run.LensPicker.Is_Head_IndexPlacePositionX())
                            return;
                        if (cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] 
                            || cDEF.Run.LensHeight.Mode != TRunLensHeightMode.Stop)
                            return;
                        if (!(cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionZ() || cDEF.Run.Bonder1.Is_Bonder1_CamPositionZ()))
                            return;
                        if (!(cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionZ() || cDEF.Run.Bonder2.Is_Bonder2_CamPositionZ()))
                            return;
                        if (!cDEF.Run.Curing1.Contact.IsBackward())
                            return;
                        if (!cDEF.Run.Curing1.UVDown.IsBackward())
                            return;
                        if (!cDEF.Run.Curing2.UVDown.IsBackward())
                            return;
                        //if (!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
                        //    return;

                        if (cDEF.Work.Project.GlobalOption.UseActAction3
                        && (!cDEF.Run.Act3.ClampDown.IsBackward() || !cDEF.Run.Act3.Contact.IsBackward()))
                            return;

                        cDEF.Tact.LensTact.GetTact();

                        cDEF.TaskLogAppend(TaskLog.Tact, $"Tact Time : {cDEF.Tact.LensTact.CycleTime}",true);
                        cDEF.Tact.LensTact.Start();
                        cDEF.Tact.Index.Start();
                        Mode = TRunIndexMode.StepMove;
                        return;
                    }
                }
            }

            if(cDEF.Run.Mode == TRunMode.Manual_IndexStepMove)
            {
                if (cDEF.Run.VCMPicker.Mode == TRunVCMPickerMode.Place)
                    return;
                if (!cDEF.Run.CleanJig.CleanJigDown.IsBackward())
                    return;
                if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    return;
                if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    return;
                if (cDEF.Run.Digital.Output[cDO.Lens_Height_Measure])
                    return;
                if (!(cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionZ() || cDEF.Run.Bonder1.Is_Bonder1_CamPositionZ()))
                    return;
                if (!(cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionZ() || cDEF.Run.Bonder2.Is_Bonder2_CamPositionZ()))
                    return;
                if (!cDEF.Run.Curing1.Contact.IsBackward())
                    return;
                if (!cDEF.Run.Curing1.UVDown.IsBackward())
                    return;
                if (!cDEF.Run.Curing2.UVDown.IsBackward())
                    return;
                if (!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
                    return;
                if (cDEF.Work.Project.GlobalOption.UseActAction3
                        && (!cDEF.Run.Act3.ClampDown.IsBackward() || !cDEF.Run.Act3.Contact.IsBackward()))
                    return;
                cDEF.Tact.Index.Start();
                Mode = TRunIndexMode.StepMove;
                return;
            }
        }
        
        #region Running Func
        protected void Running_StepMoving()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if(cDEF.Run.Digital.Input[cDI.VCM_Unloading])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunIndex + 200;
                        //cDEF.Run.LogWarning(cLog.RunIndex + 200, "Index 위에 자재 감지");
                        cDEF.Run.LogWarning(cLog.RunIndex + 200, "");
                        Mode = TRunIndexMode.Stop;
                        return;
                    }
                    Move_NextStep();
                    Step++;
                    break;

                case 1:
                    StatusShift();
                    cDEF.Run.Curing2.Information.FinishCuring2 = false;
                    Information.IndexNum = (int)Index.CommandPosition / cDEF.Work.Index.StepPitch;
                    Step++;
                    break;

                case 2:
                    cDEF.Tact.Index.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_IndexStepMove)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunIndexMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Index, "[Index] Step Move End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        public bool Product_On_Index()
        {
            if (cDEF.Run.CleanJig.Information.IndexData.Status != eLensIndexStatus.Empty
           || cDEF.Run.PlateAngle.Information.IndexData.Status != eLensIndexStatus.Empty
           || cDEF.Run.Curing2.Information.IndexData.Status != eLensIndexStatus.Empty
           || cDEF.Run.Curing1.Information.IndexData.Status != eLensIndexStatus.Empty
           || cDEF.Run.VisionInspect.Information.IndexData.Status != eLensIndexStatus.Empty
           || cDEF.Run.Bonder2.Information.IndexData.Status != eLensIndexStatus.Empty
           || cDEF.Run.Bonder1.Information.IndexData.Status != eLensIndexStatus.Empty
           || cDEF.Run.LensHeight.Information.IndexData.Status != eLensIndexStatus.Empty
           || Information.LensData.Status != eLensIndexStatus.Empty
           || cDEF.Run.Act3.Information.IndexData.Status != eLensIndexStatus.Empty
           || cDEF.Run.JigPlateAngle.Information.IndexData.Status != eLensIndexStatus.Empty
           || Information.VCMData.Status != eLensIndexStatus.Empty)
                return true;
            else
                return false;
        }
        public bool FinishCheck()
        {
            if(IsFinishCleanJig()
                && IsFinishPlateAngle()
                && IsFinishCuring2()
                && IsFinishCuring1()
                && IsFinishVisionInspect()
                && IsFinishBonder2()
                && IsFinishBonder1()
                && IsFinishLensHeight()
                && IsFinishLens()
                && IsFinishAct3()
                && IsFinishJigPlate()
                && IsFinishVCM())
                return true;
            else
                return false;
        }
        #region FinishCheck
        private bool IsFinishCleanJig()
        {
            return (cDEF.Run.CleanJig.Information.IndexData.Status == eLensIndexStatus.CleanJigFinish 
                || cDEF.Run.CleanJig.Information.IndexData.Status == eLensIndexStatus.Empty
                || cDEF.Run.CleanJig.Information.IndexData.Status == eLensIndexStatus.Actuating1Fail);
        }
        private bool IsFinishPlateAngle()
        {
            return (cDEF.Run.PlateAngle.Information.IndexData.Status == eLensIndexStatus.UnloadFinish 
                || cDEF.Run.PlateAngle.Information.IndexData.Status == eLensIndexStatus.Empty);
                //|| cDEF.Run.PlateAngle.Information.IndexData.Status == eLensIndexStatus.Actuating1Fail);
        }
        private bool IsFinishCuring2()
        {
            return (cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.Curing2Finish 
                || cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.Empty 
                || cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.LensHeightFail 
                || cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.VisionInspectFail
                || cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.Actuating1Fail);
        }
        private bool IsFinishCuring1()
        {
            return (cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.Curing1Finish 
                || cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.Empty 
                || cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.LensHeightFail 
                || cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.VisionInspectFail
                || cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.Actuating1Fail);
        }
        private bool IsFinishVisionInspect()
        {
            return (cDEF.Run.VisionInspect.Information.IndexData.Status == eLensIndexStatus.VisionInspectFinish 
                || cDEF.Run.VisionInspect.Information.IndexData.Status == eLensIndexStatus.Empty
                || cDEF.Run.VisionInspect.Information.IndexData.Status == eLensIndexStatus.LensHeightFail
                || cDEF.Run.VisionInspect.Information.IndexData.Status == eLensIndexStatus.VisionInspectFail);
        }
        private bool IsFinishBonder2()
        {
            return (cDEF.Run.Bonder2.Information.IndexData.Status == eLensIndexStatus.Bonder2Finish 
                || cDEF.Run.Bonder2.Information.IndexData.Status == eLensIndexStatus.Empty 
                || cDEF.Run.Bonder2.Information.IndexData.Status == eLensIndexStatus.LensHeightFail);
        }
        private bool IsFinishBonder1()
        {
            return (cDEF.Run.Bonder1.Information.IndexData.Status == eLensIndexStatus.Bonder1Finish 
                || cDEF.Run.Bonder1.Information.IndexData.Status == eLensIndexStatus.Empty 
                || cDEF.Run.Bonder1.Information.IndexData.Status == eLensIndexStatus.LensHeightFail);
        }
        private bool IsFinishLensHeight()
        {
            return (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish 
                || cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFail 
                || cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.Empty);
        }
        private bool IsFinishLens()
        {
            return (Information.LensData.Status == eLensIndexStatus.AssembleFinish
                || Information.LensData.Status == eLensIndexStatus.LensHeightFail
                || Information.LensData.Status == eLensIndexStatus.Empty);
        }
        private bool IsFinishAct3()
        {
            return (cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.Act3Finish
                || cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.Act3Fail 
                || cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.Empty);
        }
        private bool IsFinishJigPlate()
        {
            return (cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.JigPlateAngleFinish 
                || cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.JigPlateAngleFail 
                || cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.Empty);
        }
        private bool IsFinishVCM()
        {
            // 정상으로 놓았으면.
            if (Information.VCMData.Status == eLensIndexStatus.VCMLoaded)
                return true;

            if (cDEF.Run.VCMLoader.Information.ExistStage)
            {
                if (Information.VCMData.Status == eLensIndexStatus.Empty
                    && !cDEF.Run.LotEnd 
                    && !cDEF.Work.Option.IndexSkip[Information.IndexNum])
                    return false;
            }
            else
            {
                if (Information.VCMData.Status == eLensIndexStatus.Empty)
                    return true;
            }

            return (Information.VCMData.Status == eLensIndexStatus.VCMLoaded 
                || Information.VCMData.Status == eLensIndexStatus.Empty);
        }
        #endregion
        public void StatusShift()
        {
            cDEF.Run.CleanJig.Information.Clear();
            cDEF.Run.CleanJig.Information.IndexData.Assign(cDEF.Run.PlateAngle.Information.IndexData);
            cDEF.Run.PlateAngle.Information.IndexData.Assign(cDEF.Run.Curing2.Information.IndexData);
            cDEF.Run.Curing2.Information.IndexData.Assign(cDEF.Run.Curing1.Information.IndexData);
            cDEF.Run.Curing1.Information.IndexData.Assign(cDEF.Run.VisionInspect.Information.IndexData);
            cDEF.Run.VisionInspect.Information.IndexData.Assign(cDEF.Run.Bonder2.Information.IndexData);
            cDEF.Run.Bonder2.Information.IndexData.Assign(cDEF.Run.Bonder1.Information.IndexData);
            cDEF.Run.Bonder1.Information.IndexData.Assign(cDEF.Run.LensHeight.Information.IndexData);
            cDEF.Run.LensHeight.Information.IndexData.Assign(Information.LensData);
            Information.LensData.Assign(cDEF.Run.Act3.Information.IndexData);
            cDEF.Run.Act3.Information.IndexData.Assign(cDEF.Run.JigPlateAngle.Information.IndexData);
            cDEF.Run.JigPlateAngle.Information.IndexData.Assign(Information.VCMData);
            Information.VCMData.Clear();
        }
        #endregion
        #region Move Command
        public bool Move_Index(int index)
        {
            if (!cDEF.Run.CleanJig.CleanJigDown.IsBackward())
                return false;
            if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                return false;
            if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                return false;
            if (cDEF.Run.Digital.Output[cDO.Lens_Height_Measure])
                return false;
            if (!(cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionZ() || cDEF.Run.Bonder1.Is_Bonder1_CamPositionZ()))
                return false;
            if (!(cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionZ() || cDEF.Run.Bonder2.Is_Bonder2_CamPositionZ()))
                return false;
            if (!cDEF.Run.Curing1.Contact.IsBackward())
                return false;
            if (!cDEF.Run.Curing1.UVDown.IsBackward())
                return false;
            if (!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
                return false;

            if (cDEF.Work.Project.GlobalOption.UseActAction3
                && (!cDEF.Run.Act3.ClampDown.IsBackward() || !cDEF.Run.Act3.Contact.IsBackward()))
                return false;

            int Sleep;
            Sleep = cDEF.Work.Index.MovingDelay;
            int pos = index * cDEF.Work.Index.StepPitch;
            Index.Absolute(pos, Sleep);

            return true;
        }
        public bool Move_NextStep()
        {
            //if (!cDEF.Run.CleanJig.CleanJigDown.IsBackward())
            //    return false;
            //if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
            //    return false;
            //if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
            //    return false;
            //if (cDEF.Run.Digital.Output[cDO.Lens_Height_Measure])
            //    return false;
            //if (!cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionZ())
            //    return false;
            //if (!cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionZ())
            //    return false;
            //if (!cDEF.Run.Curing1.Contact.IsBackward())
            //    return false;
            //if (!cDEF.Run.Curing1.UVDown.IsBackward())
            //    return false;
            //if (!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
            //    return false;

            int Sleep;
            Sleep = cDEF.Work.Index.MovingDelay;
            int pos = cDEF.Work.Index.StepPitch;
            Index.Relative(pos, Sleep);

            return true;
        }
        #endregion



    }
}

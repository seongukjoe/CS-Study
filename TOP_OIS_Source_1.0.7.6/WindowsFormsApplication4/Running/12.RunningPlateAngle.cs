using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;
using System.Collections.Generic;

namespace XModule.Running
{

    public class RunPlateAngleInformation : fpObject
    {

        #region 변수
        private double FValue = 0.0;
        //private bool FExist = false;
        private bool FMeasureFinish = false;
        private bool FResult = false;
        //public LensIndexStatus Status;
        public Lens_Data LensData;
        public Index_Data IndexData;
        #endregion

        #region Property
        public double Value
        {
            get { return FValue; }
            set
            {
                if(FValue != value)
                {
                    FValue = value;
                    Change();
                }
            }
        }

        //public bool Exist
        //{
        //    get { return FExist; }
        //    set
        //    {
        //        if(FExist != value)
        //        {
        //            FExist = value;
        //            Change();
        //        }
        //    }
        //}

        public bool MeasureFinish
        {
            get { return FMeasureFinish; }
            set
            {
                if (FMeasureFinish != value)
                {
                    FMeasureFinish = value;
                    Change();
                }
            }
        }

        public bool Result
        {
            get { return FResult; }
            set
            {
                if (FResult != value)
                {
                    FResult = value;
                    Change();
                }
            }
        }


        #endregion

        public RunPlateAngleInformation() : base()
        {
            LensData = new Lens_Data();
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunPlateAngle.dat";
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
                    //case "FExist": FExist = Convert.ToBoolean(sArr[1]); break;
                    //case "FMeasureFinish": FExist = Convert.ToBoolean(sArr[1]); break;
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

            //FileWrite.WriteLine($"FExist,{FExist}");
            FileWrite.WriteLine($"FMeasureFinish,{FMeasureFinish}");

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
            FMeasureFinish = false;
            LensData.Clear();
            IndexData.Clear();
            Unlock(Ignore);
        }
    }
    public enum TRunPlateAngleMode
    {
        Stop,
        PickPlace,                  // Pick && Place
        Measure,                    // Measure
    };

    
    public class RunPlateAngle : TfpRunningModule 
    {
        //Evnet
        public delegate void LedStarterHandler(int led);
        public event LedStarterHandler OnLedStarter;

        public delegate void LedFinishHandler(int led);
        public event LedFinishHandler OnLedFinish;

        public delegate void PlateAngleChartHandler(int Index, double Value);
        public event PlateAngleChartHandler OnChart;

        private RunPlateAngleInformation FInformation;
        private TRunPlateAngleMode FMode;

        public int FCalc;
        
        private int ActRetryCount = 0;
        private int MeasureRetryCount = 0;

        private StringBuilder strTmpMeasure = new StringBuilder();
        private StringBuilder strMeasure = new StringBuilder();
        private StringBuilder[] strMeasureData;
        public List<double[]> ListPlateZData = new List<double[]>();

        public double[] PlateAngleData = new double[20];
        public double[] PlateZData = new double[20];
        private bool FActResult = false;
        public PlateZ_RowData Pz_RowData;
        public PlateZData Pz_Data;
        string[] DataLog;

        int OkCount = 0;
        int FailCount = 0;
        int PlateZDataIdx = 0;
        double OkMaxValue = double.MinValue;
        double FailMinValue = double.MaxValue;
        int FailIdx = 0;
        public RunPlateAngle(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunPlateAngleInformation();
        }
        

        #region **Property**
        public RunPlateAngleInformation Information
        {
            get { return FInformation; }
        }

        public TRunPlateAngleMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        public TfpMotionItem PlateAngle
        {
            get { return GetMotions(0); }
        }
        public TfpCylinderItem Up_DownCylinder
        {
            get { return GetCylinders(0); }
        }
        public TfpCylinderItem Fw_RvCylinder
        {
            get { return GetCylinders(1); }
        }
        public TfpCylinderItem Clamp
        {
            get { return GetCylinders(2); }
        }
        public TfpCylinderItem Contact
        {
            get { return GetCylinders(3); }
        }
        #endregion //Property//

        private TRunPlateAngleMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunPlateAngleMode.Stop;
        }
        private void SetMode(TRunPlateAngleMode Value)
        {
            if (Value == TRunPlateAngleMode.Stop)
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
                case TRunPlateAngleMode.Stop:
                    return "Stop";
                case TRunPlateAngleMode.PickPlace:
                    return "Pick & Place";
                case TRunPlateAngleMode.Measure:
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
                    PlateAngle.Home();
                    Step++;
                    break;
                case 1:
                    Up_DownCylinder.Backward();
                    Contact.Backward();
                    Step++;
                    break;
                case 2:
                    Fw_RvCylinder.Backward();
                    Clamp.Backward();
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
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Initialize] End", true);
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
                    Up_DownCylinder.Backward();
                    Contact.Backward();
                    Step++;
                    break;

                case 1:
                    Fw_RvCylinder.Backward();
                    Clamp.Backward();
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[To-Run] Done.", true);
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
                    Up_DownCylinder.Backward();
                    Contact.Backward();
                    Step++;
                    break;
                case 1:
                    Fw_RvCylinder.Backward();
                    Clamp.Backward();
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[To-Stop] Done.", true);
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
                case TRunPlateAngleMode.PickPlace:
                    Running_PickPlace();
                    break;

                case TRunPlateAngleMode.Measure:
                    Running_Measure();
                    break;

            }
        }


        protected override void ProcMain()
        {
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                // Pick Place
                //if (!Information.Exist)
                {   
                    if(Information.IndexData.Status == eLensIndexStatus.Curing2Finish
                         || Information.IndexData.Status == eLensIndexStatus.LensHeightFail
                         || Information.IndexData.Status == eLensIndexStatus.VisionInspectFail
                         || Information.IndexData.Status == eLensIndexStatus.Actuating1Fail
                         || Information.IndexData.Status == eLensIndexStatus.PlateAngleFail)
                    {
                        cDEF.Tact.PlateAngle.Start();
                        Mode = TRunPlateAngleMode.PickPlace;
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Pick & Place Start.", true);
                        return;
                    }
                }

                // Measure
                if(Information.LensData.Status == LensTrayStatus.Load && !Information.MeasureFinish)
                {
                    Mode = TRunPlateAngleMode.Measure;
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Measure Start.", true);
                    return;
                }

            }

            if(cDEF.Run.Mode == TRunMode.Manual_PlateAnglePickPlace)
            {
                Mode = TRunPlateAngleMode.PickPlace;
                return;
            }

            if(cDEF.Run.Mode == TRunMode.Manual_PlateAngleMeasure)
            {
                cDEF.Tact.PlateAngle.Start();
                Information.Result = true;
                Mode = TRunPlateAngleMode.Measure;
                cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] - Manual Measure Start.", true);
                return;
            }
        }

        #region Running Func
        protected void Running_PickPlace()
        {
            if (!IsReady())
                return;

            if(cDEF.Run.DetailMode == TfpRunningMode.frmToWarning && Step == 6)
            {
                Mode = TRunPlateAngleMode.Stop;
                return;
            }

            switch (Step)
            {
                case 0:
                    Up_DownCylinder.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Up-Down Cylinder Up.", true);
                    Contact.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Contact Cylinder Uncontact.", true);
                    Step++;
                    break;

                case 1:
                    Clamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder Unclamp.", true);
                    Fw_RvCylinder.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Fw-Bw Cylinder Backward.", true);
                    Step++;
                    break;


                case 2:
                    Up_DownCylinder.Forward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Up-Down Cylinder Down.", true);
                    Step++;
                    break;

                case 3:
                    cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Vacuum] = true;
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Vacuum On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < cDEF.Work.PlateAngle.VacuumDelay)
                        break;
                    Up_DownCylinder.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Up-Down Cylinder Up.", true);
                    Step++;
                    break;

                case 5:
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Vacuum_Check])
                        {
                            //cDEF.Run.LogWarning(cLog.RunPlateAngle + 200, "[SIDE ANGLE MEASURE] Vac Check Err.");
                            cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 200;
                            cDEF.Run.LogWarning(cLog.RunPlateAngle + 200, "");
                            cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Vacuum Check Err.", true);
                            Mode = TRunPlateAngleMode.Stop;
                            return;
                        }
                    }
                    
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 6:
                    if (Environment.TickCount - FCalc < 30000)
                    {
                        if (Information.LensData.Status == LensTrayStatus.Load)
                            break;
                        if (cDEF.Run.UnloadPicker.HeadX.ActualPosition < cDEF.Work.TeachUnloadPicker.ReadyPositionX)
                            break;

                        if (Information.IndexData.Status == eLensIndexStatus.Curing2Finish)
                        {
                            Information.Result = true;
                        }
                        else if (Information.IndexData.Status == eLensIndexStatus.LensHeightFail
                             || Information.IndexData.Status == eLensIndexStatus.VisionInspectFail
                             || Information.IndexData.Status == eLensIndexStatus.Actuating1Fail
                             || Information.IndexData.Status == eLensIndexStatus.PlateAngleFail)
                        {
                            Information.Result = false;

                            if (Information.IndexData.Status == eLensIndexStatus.LensHeightFail)
                            {
                                Information.IndexData.FailType = eFailType.LensHeightFail;
                            }

                            if (Information.IndexData.Status == eLensIndexStatus.VisionInspectFail)
                            {
                                Information.IndexData.FailType = eFailType.VisionInspectFail;
                            }

                            if (Information.IndexData.Status == eLensIndexStatus.Actuating1Fail)
                            {
                                Information.IndexData.FailType = eFailType.Actuating1Fail;
                            }

                            if (Information.IndexData.Status == eLensIndexStatus.PlateAngleFail)
                            {
                                Information.IndexData.FailType = eFailType.PlateAngleFail;
                            }
                        }
                       

                        cDEF.TaskLogAppend(TaskLog.PlateAngle, $"[Side Angle Measure]idex:{Information.IndexData.Index + 1} Status:{Information.IndexData.Status }  Judge:{Information.IndexData.FailType } Result:{Information.Result}", true);

                        Fw_RvCylinder.Forward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Fw-Bw Cylinder Foward.", true);
                        Step++;
                    }
                    else
                    {
                        //cDEF.Run.LogWarning(cLog.RunPlateAngle + 201, "[SIDE ANGLE MEASURE] Unload Picker Head Move Avoid Position Time Out");
                        cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 201;
                        cDEF.Run.LogWarning(cLog.RunPlateAngle + 201, "");
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Unload Picker Head Move Avoid Position Time Out.", true);
                        Mode = TRunPlateAngleMode.Stop;
                        return;
                    }
                    break;

                case 7:
                    Up_DownCylinder.Forward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Up-Down Cylinder Down.", true);
                    Step++;
                    break;

                case 8:
                    Clamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder Clamp.", true);
                    cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Vacuum Off.", true);
                    cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Blow] = true;
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Blow On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 9:
                    if (Environment.TickCount - FCalc < cDEF.Work.PlateAngle.BlowDelay)
                        break;
                    cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Blow Off.", true);
                    if (cDEF.Run.Mode == TRunMode.Manual_PlateAnglePickPlace || !cDEF.Work.Project.GlobalOption.UsePlateAngle || !Information.Result || cDEF.Run.DetailMode == TfpRunningMode.frmToStop)
                    {
                        Up_DownCylinder.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Up-Down Cylinder Up.", true);
                    }
                    Step++;
                    break;

                case 10:
                    if (cDEF.Run.Mode == TRunMode.Manual_PlateAnglePickPlace || !cDEF.Work.Project.GlobalOption.UsePlateAngle || !Information.Result || cDEF.Run.DetailMode == TfpRunningMode.frmToStop)
                    {
                        Fw_RvCylinder.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Fw-Bw Cylinder Backward.", true);
                    }
                    Step++;
                    break;

                case 11:
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Vacuum_Check])
                        {
                            //cDEF.Run.LogWarning(cLog.RunPlateAngle + 202, "[SIDE ANGLE MEASURE] Head에 자재 감지. Head 확인 바랍니다.");
                            cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 202;
                            cDEF.Run.LogWarning(cLog.RunPlateAngle + 202, "");
                            cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Detected on Side Angle Meaure Head.", true);
                            Mode = TRunPlateAngleMode.Stop;
                            return;
                        }
                    }
                    Step++;
                    break;

                case 12:
                    Information.LensData.Status = LensTrayStatus.Load;
                    Information.LensData.Index = Information.IndexData.Index;
                    Information.LensData.x = Information.IndexData.x;
                    Information.LensData.y = Information.IndexData.y;
                    Information.LensData.FailType = Information.IndexData.FailType;
                    Information.LensData.LensHeightData = Information.IndexData.LensHeightData;

                    //택타임 데이터 복사
                    TactTimeDataCopy();
                    Information.LensData.TT_LensTact = cDEF.Tact.LensTact.CycleTime;
                   Information.MeasureFinish = false;
                    Information.IndexData.Status = eLensIndexStatus.UnloadFinish;
                    Step++;
                    break;

                case 13:
                    if (cDEF.Run.Mode == TRunMode.Manual_PlateAnglePickPlace)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunPlateAngleMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, $"[Side Angle Measure] Pick & Place End - Cycle Time : [{cDEF.Tact.PlateAngle.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        protected void Running_Measure()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    DataLog = new string[cDEF.Work.Option.Measure_RetryCount];
                    Pz_Data = new PlateZData();
                    strMeasure.Clear();
                    Array.Clear(PlateZData, 0, PlateZData.Length);
                    ListPlateZData.Clear();
                    MeasureRetryCount = 1;                  
                    Step++;
                    break;

                case 1:
    
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, $"[Side Angle Measure]Idex:{Information.LensData.Index + 1} Judge:{Information.LensData.FailType }", true);
                    if (!cDEF.Work.Project.GlobalOption.UsePlateAngle || !Information.Result)
                    {
                        cDEF.Tact.PlateAngle.GetTact();
                        Clamp.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, $"[Side Angle Measure] Clamp Cylinder UnClamp.(Judge Fali)", true);
                        Information.MeasureFinish = true;
                        if (cDEF.Run.Mode == TRunMode.Manual_PlateAngleMeasure)
                            cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                        Mode = TRunPlateAngleMode.Stop;
                        return;
                    }

                    if (!cDEF.Run.Digital.Input[cDI.Actuator_2_Ready])
                    {

                        if (cDEF.Work.Project.GlobalOption.Actuator_2_Mode)
                        {
                            if (cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start])
                            {
                                cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] = false;

                                cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle]  Actuator A Start (I/O) Off.", true);
                            }
                        }
                        else
                        {
                            if (cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start])
                            {
                                cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] = false;
                                cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle]  Actuator B Start (I/O) Off.", true);
                            }
                        }

                        cDEF.Tact.PlateAngle.GetTact();
                        //cDEF.Run.LogWarning(cLog.RunPlateAngle + 205, "[Plate Angle] Actuator is not Ready");
                        cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 205;
                        cDEF.Run.LogWarning(cLog.RunPlateAngle + 205, "");
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle] Actuator is Not Ready.", true);
                        Mode = TRunPlateAngleMode.Stop;
                        return;
                    }
                    Move_PlateAngle_ReadyPosition_Double();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Move Side Angle Measure Ready Position.", true);

                    ActRetryCount = 0;
                    FActResult = true;
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < 30000)
                    {
                        if (cDEF.Run.UnloadPicker.HeadX.ActualPosition < cDEF.Work.TeachUnloadPicker.ReadyPositionX)
                            break;

                        Fw_RvCylinder.Forward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Fw-Bw Cylinder Forward.", true);
                        Step++;
                    }
                    else
                    {
                        //cDEF.Run.LogWarning(cLog.RunPlateAngle + 201, "[SIDE ANGLE MEASURE] Unload Picker Head Move Avoid Position Time Out");
                        cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 201;
                        cDEF.Run.LogWarning(cLog.RunPlateAngle + 201, "");
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Unload Picker Head Move Avoid Position Time Out.", true);
                        Mode = TRunPlateAngleMode.Stop;
                        return;
                    }
                    break;

                case 3:
                    Up_DownCylinder.Forward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Down Cylinder Clamp.", true);
                    Step++;
                    break;

                case 4:
                    Clamp.Forward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder Clamp.", true);
                    Step++;
                    //Step = 9; //Master Jig Test용 코드
                    break;

                case 5:
                    if (cDEF.Work.Project.GlobalOption.UseActAction2)
                    {
                        Contact.Forward();
                        Step++;
                    }
                    else
                        Step = 11;  
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Contact Cylinder Contact.", true);
                   
                    break;

                case 6:
                    if (cDEF.Work.Project.GlobalOption.Actuator_2_Mode)
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] = true;
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle]  Actuator A Start (I/O) On.", true);
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] = true;
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle]  Actuator B Start (I/O) On.", true);
                    }
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 7:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Actuator_2_Ready] || cDEF.Run.Digital.Input[cDI.Actuator_2_Pass] || cDEF.Run.Digital.Input[cDI.Actuator_2_Fail])
                            break;
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle] Actuator Input Ready Off OK.", true);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle] Contact Cylinder Uncontact.", true);
                        Clamp.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder Unclamp.", true);
                        if (cDEF.Run.Digital.Input[cDI.Actuator_2_OISFail])
                        {

                            cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 205;
                            cDEF.Run.LogWarning(cLog.RunPlateAngle + 205, "");
                        }
                        else if (cDEF.Run.Digital.Input[cDI.Actuator_2_AFFail])
                        {

                            cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 206;
                            cDEF.Run.LogWarning(cLog.RunPlateAngle + 206, "");
                        }
                        else
                        {
                         
                            //cDEF.Run.LogWarning(cLog.RunPlateAngle + 204, "ACTUATOR 2 PGM CHECK");
                            cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 204;
                            cDEF.Run.LogWarning(cLog.RunPlateAngle + 204, "");
                            
                        }


                        cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] = false;
                        Mode = TRunPlateAngleMode.Stop;
                        return;
                    }
                    break;

                case 8:
                    if (cDEF.Run.Digital.Input[cDI.Actuator_2_OISFail])
                    {
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle] Contact Cylinder Uncontact.", true);
                        Clamp.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder Unclamp.", true);


                        cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] = false;

                        cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 205;
                        cDEF.Run.LogWarning(cLog.RunPlateAngle + 205, "");
                        Mode = TRunPlateAngleMode.Stop;
                        return;
                    }
                    else if (cDEF.Run.Digital.Input[cDI.Actuator_2_AFFail])
                    {
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle] Contact Cylinder Uncontact.", true);
                        Clamp.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder Unclamp.", true);


                        cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] = false;

                        cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 206;
                        cDEF.Run.LogWarning(cLog.RunPlateAngle + 206, "");
                        Mode = TRunPlateAngleMode.Stop;
                        return;
                    }
                    Step++;
                    break;


                case 9:
                    if (Environment.TickCount - FCalc < cDEF.Work.PlateAngle.ActuatorTime)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Actuator_2_Pass])
                        {
                            cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] = false;
                            cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] = false;
                            cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle]  Actuator A Start (I/O) Off.", true);
                            cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle]  Actuator B Start (I/O) Off.", true);
                            cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle] Actuator Input Pass Signal.", true);
                            Step++;
                        }
                        else if (cDEF.Run.Digital.Input[cDI.Actuator_2_Fail])
                        {
                            FActResult = false;
                            //Information.LensData.FailType = eFailType.Actuating2Fail;
                            cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] = false;
                            cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] = false;
                            cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle] Actuator Input Fail Signal.", true);
                            Step++;
                        }
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle] Time Out - Actuator Signal Off.", true);

                        FActResult = false;
                       //Information.IndexData.FailType = eFailType.Actuating2Fail;
                        Step++;
                    }
                    break;

                case 10:
                    if (!FActResult)
                    {
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Contact Cylinder Uncontact.", true);
                        Clamp.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder UnClamp.", true);


                        if (cDEF.Work.Project.GlobalOption.UseActRetry)
                        {
                            if (ActRetryCount < 1)
                            {
                                FActResult  = true;
                                ActRetryCount++;
                                Step = 4;
                                break;
                            }
                        }
                       
                        Information.Result = false; 
                    }                

                    if (!Information.Result)
                    {
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Contact Cylinder Uncontact.", true);
                        Clamp.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder UnClamp.", true);

                        Information.Value = 0;
                        Information.LensData.FailType = eFailType.Actuating2Fail;
                        Information.MeasureFinish = true;

                        if (cDEF.Work.Project.GlobalOption.UseResultDummyPass)
                        {
                            Information.Result = true;
                        }

                        // Act Fail 시 면각도 측정 안함 > 차트 디스플레이 및 UnLoad 대기 동작
                        Step = 17;
                        return;
                        
                    }
                    Step++;
                    break;

                case 11:
                    Move_PlateAngle_ReadyPosition_Double();      
                    Step++;
                    break;
                case 12:
          
                    Step++;
                    break;
                case 13:
                    cDEF.SideAngleMeasuring.Send_Trigger();
                    cDEF.TaskLogAppend(TaskLog.Index, "[Side Angle Measure] Angle Measuring Send Trigger", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 14:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.SideAngleMeasuring.ReciveStatus == CmmStatus.Ok)
                        {
                            cDEF.SideAngleMeasuring.ReciveStatus = CmmStatus.Wait;
                            Step++;
                        }
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunPlateAngle + 1;
                        cDEF.Run.LogWarning(cLog.RunPlateAngle + 1, "[Side Angle Measure] Angle Measuring Send Trigger Fail");
                        Information.Value = 0;
                        Mode = TRunPlateAngleMode.Stop;
                        return;
                    }                   
                    break;

                case 15:
                    Move_PlateAngle_WorkPosition();
                    FCalc = Environment.TickCount;
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Move Side Angle Measure Work Position.", true);
                    Step++;
                    break;

                case 16:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.SideAngleMeasuring.ReciveStatus == CmmStatus.Ok ||
                            cDEF.SideAngleMeasuring.ReciveStatus == CmmStatus.Ng)
                        {
                            Information.Value = cDEF.SideAngleMeasuring.AngleValue * cDEF.Work.PlateAngle.RnRPercent + cDEF.Work.PlateAngle.RnRShift;

                            PlateAngleData[MeasureRetryCount -1] = Information.Value * 60;

                            //Information.LensData.PlateAngleData = Information.Value * 60;  //20211125  sj.shin 측정 반복 기능 추가로 변경

                            //Information.LensData.PlateZData = cDEF.SideAngleMeasuring.Side_PeakZ_Value; //210329, ksyoon
                            Array.Copy(cDEF.SideAngleMeasuring.Side_PeakZ_Value, PlateZData , cDEF.SideAngleMeasuring.Side_PeakZ_Value.Length); //210329, ksyoon

                            Pz_RowData = new PlateZ_RowData();

                            for (int i = 0; i < 20; i++)
                            {
                                Pz_RowData.Zdata[i] = PlateZData[i];
                            }

                            Pz_Data.PlateZ_Data.Add(Pz_RowData);

                            //Information.LensData.PlateZData
                            //ListPlateZData.Add(PlateZData); //20211125  sj.shin 측정 반복 기능 추가로  PlateZData wjwkd

                            Information.LensData.PlateZData = Pz_RowData.Zdata;

                            strTmpMeasure.Clear();
                            for(int i = 0; i < Information.LensData.PlateZData.Length; i++)
                            {
                                //if(i == 8)
                                if (i == cDEF.Work.PlateAngle.SideAnglePoint)
                                    strTmpMeasure.Append("(LENS-BOT Z)");
                                else if( i == cDEF.Work.PlateAngle.SideAnglePoint + cDEF.Work.PlateAngle.SideAngleLensBottomPoint)
                                    strTmpMeasure.Append("(VCM Z)");
                                strTmpMeasure.Append(Information.LensData.PlateZData[i]);
                                strTmpMeasure.Append(",");
                            }

                            strMeasure.Append(strTmpMeasure.ToString());
                            strMeasure.Append("\r\n");
                            DataLog[MeasureRetryCount - 1] = strTmpMeasure.ToString();
                            //// [2020.1105.1] Add, 마지막 "0" 찾기
                            //int pos = -1;
                            //for (int i = 0; i < Information.LensData.PlateZData.Length; i++)
                            //{
                            //    if(Information.LensData.PlateZData[i] ==0)
                            //    {
                            //        pos = i;
                            //    }
                            //}

                            //for (int i = 0; i < Information.LensData.PlateZData.Length ; i++)
                            //{
                            //    if (pos == i)
                            //    {
                            //        strTmpMeasure.Append("(VCM Z)");
                            //    }
                            //    else
                            //    {
                            //        strTmpMeasure.Append(Information.LensData.PlateZData[i]);
                            //    }
                            //    strTmpMeasure.Append(",");

                            //}
                            cDEF.TaskLogAppend(TaskLog.PlateAngle, $"[Side Angle Measure] Index : {MeasureRetryCount} Move Side Angle Recive Value : {Information.Value}.", true);
                            cDEF.TaskLogAppend(TaskLog.PlateAngleData, $"Data - ReTryCount:{MeasureRetryCount} Side Angle Raw Data Value : {cDEF.SideAngleMeasuring.AngleValue},{cDEF.SideAngleMeasuring.AngleValue * 60},R&R Gain : {cDEF.Work.PlateAngle.RnRPercent},R&R Shift : {cDEF.Work.PlateAngle.RnRShift}, R&R Data Value : {Information.LensData.PlateAngleData },(Lens Z),{strTmpMeasure.ToString()}", true); //Adding Error
                            Step++;
                        }
                    }
                    else
                    {
                        Information.Value = 0;
                        Step++;
                    }
                    break;

                case 17:
                    Move_PlateAngle_ReadyPosition_Double();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] PlateAngle Ready Position.", true);
                    Step++;
                    break;

                case 18:            
                    Step++;
                    break;

                case 19:
                    if (!cDEF.Work.Recipe.NonContactMeasure || !Information.Result)
                    {
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Contact Cylinder Uncontact.", true);
                        Up_DownCylinder.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Up/Down Cylinder Up.", true);
                    }
                    Step++;
                    break;

                case 20:
                    if (!cDEF.Work.Recipe.NonContactMeasure || !Information.Result)
                    {
                        Clamp.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder Unclamp.", true);
                        Fw_RvCylinder.Backward();
                        cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Fw Cylinder Backward.", true);
                    }
                    //if (!FActResult)
                    //    Step = 23;
                    //else
                    Step++;
                    break;
                case 21:
                    // Act #2 결과 Fail 시 Measure Retry Count 만큼 Act #2 반복 측정 / Act #2 한번만 측정하고 넘어갈 경우 하단의 Act #2 결과 Fail 주석 처리 후 case 20 주석해제.
                    if (MeasureRetryCount < cDEF.Work.Option.Measure_RetryCount)
                    {
                        MeasureRetryCount++;
                        Step = 1;
                        break;
                    }
                    Contact.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Contact Cylinder Uncontact.", true);
                    Up_DownCylinder.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Up/Down Cylinder Up.", true);

                    // Act #2 결과 Fail 시 데이터 판정 / 데이터 획득 안함
                    if (!FActResult)
                        Step = 23;
                    else
                        Step++;
                    break;


                case 22:
                    Clamp.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Clamp Cylinder Unclamp.", true);
                    Fw_RvCylinder.Backward();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Fw Cylinder Backward.", true);
                    // 면각도 측정 결과 판정
                    OkCount = 0;
                    FailCount = 0;
                    PlateZDataIdx = 0;
                    OkMaxValue = double.MinValue;
                    FailMinValue = double.MaxValue;
                    FailIdx = 0;

                    FailMinValue = PlateAngleData[0];

                    for (int i = 0; i < cDEF.Work.Option.Measure_RetryCount; i++)
                    {
                        if (PlateAngleData[i] > cDEF.Work.PlateAngle.MaxLimit
                         || PlateAngleData[i] < cDEF.Work.PlateAngle.MinLimit) //ksyoon
                        {
                            FailCount++;
                        Information.LensData.FailType = eFailType.PlateAngleFail;
                           

                            if (FailMinValue > PlateAngleData[i])
                            {
                                FailMinValue = PlateAngleData[i];
                                FailIdx = i;
                            }


                        }
                        else
                        {
                            OkCount++;

                            if (OkMaxValue < PlateAngleData[i])
                            {
                                OkMaxValue = PlateAngleData[i];
                                PlateZDataIdx = i;
                            }

                        }

                                    
                    }

                    if (cDEF.Work.Option.Measure_GoodCount <= OkCount )
                    {
                        Information.IndexData.FailType = eFailType.None;
                        Information.LensData.PlateAngleData = OkMaxValue;
                        Information.LensData.PlateZData = new double[20];
                        for (int i = 0; i < 20; i++)
                        {
                            Information.LensData.PlateZData[i] =  Pz_Data.PlateZ_Data[PlateZDataIdx].Zdata[i];
                        }

                        if (cDEF.Work.Project.GlobalOption.PlateAngleResultRowJudge)
                        {
                            if (cDEF.Work.PlateAngle.SideAnglePoint < Information.LensData.PlateZData.Length)
                            {
                                for (int i = 0; i < cDEF.Work.PlateAngle.SideAnglePoint + cDEF.Work.PlateAngle.SideAngleLensBottomPoint; i++)
                                {
                                    if(i< cDEF.Work.PlateAngle.SideAnglePoint)
                                    {
                                        if (Information.LensData.PlateZData[i] > cDEF.Work.PlateAngle.RowDataMaxLimit
                                        || Information.LensData.PlateZData[i] < cDEF.Work.PlateAngle.RowDataMinLimit)
                                        {
                                            //Information.LensData.FailType = eFailType.PlateAngleFail; //Adding Error
                                            Information.LensData.FailType = eFailType.SideHeightFail; //Adding Error
                                            cDEF.Work.PlateAngle.SideAngleFailCheck = true;
                                            if (!cDEF.Work.Project.GlobalOption.UseResultDummyPass)
                                            {
                                                Information.Result = false;
                                            }
                                            cDEF.TaskLogAppend(TaskLog.PlateAngle, $"[Side Angle Measure] Move Side Angle Z Value Limit Over, {Information.LensData.PlateZData[i]}.", true);
                                        }
                                    }
                                    else
                                    {
                                        if (Information.LensData.PlateZData[i] > cDEF.Work.PlateAngle.LensBotRowDataMaxLimit
                                        || Information.LensData.PlateZData[i] < cDEF.Work.PlateAngle.LensBotRowDataMinLimit)
                                        {
                                            if(cDEF.Work.PlateAngle.SideAngleFailCheck == true)
                                            {
                                                //
                                            }
                                            else
                                            {
                                                //Information.LensData.FailType = eFailType.PlateAngleFail; //Adding Error
                                                Information.LensData.FailType = eFailType.SideHeightFail; //Adding Error
                                                if (!cDEF.Work.Project.GlobalOption.UseResultDummyPass)
                                                {
                                                    Information.Result = false;
                                                }
                                                cDEF.TaskLogAppend(TaskLog.PlateAngle, $"[Side Angle Measure] Move Side Angle Lens Bottom Z Value Limit Over, {Information.LensData.PlateZData[i]}.", true);
                                            }
                                            
                                        }
                                    }

                                }
                                cDEF.Work.PlateAngle.SideAngleFailCheck = false;
                            }
                        }
                    }
                    else
                    {
                        Information.LensData.FailType = eFailType.PlateAngleFail;  // 결과 판정을 

                        if (!cDEF.Work.Project.GlobalOption.UseResultDummyPass)
                        {
                            Information.Result = false;
                        }
                        Information.LensData.PlateAngleData = FailMinValue;

                    }                   
                    Step++;
                    break;

                case 23:
                    if (!Information.Result)
                    {
                        if (FActResult)
                            cDEF.TaskLogAppend(TaskLog.PlateAngleData, $"Judge : NG - Index :{FailIdx + 1} Min Data :{FailMinValue},R&R Gain : {cDEF.Work.PlateAngle.RnRPercent},R&R Shift : {cDEF.Work.PlateAngle.RnRShift}, R&R Data Value : {Information.LensData.PlateAngleData },(Lens Z),{DataLog[FailIdx].ToString()}", true); //Adding Error, 추가된것, 확인 필요
                        else
                            cDEF.TaskLogAppend(TaskLog.PlateAngleData, $"Judge : NG - / Actuator #2 Fail" , true); //Adding Error, 추가된것, 확인 필요
                    }
                    else
                    {
                        cDEF.TaskLogAppend(TaskLog.PlateAngleData, $"Judge : OK - Index :{PlateZDataIdx + 1} Max Data :{OkMaxValue},R&R Gain : {cDEF.Work.PlateAngle.RnRPercent},R&R Shift : {cDEF.Work.PlateAngle.RnRShift}, R&R Data Value : {Information.LensData.PlateAngleData },(Lens Z),{DataLog[PlateZDataIdx].ToString()}", true); //Adding Error, 추가된것, 확인 필요
                    }
                    //최종 결과 판정

                    OnChart(Information.LensData.Index, Information.LensData.PlateAngleData); //ksyoon

                    Step++;
                    break;

                case 24:
                    Information.MeasureFinish = true;

                    cDEF.Tact.PlateAngle.GetTact();
                    Information.LensData.TT_PlateAngle = cDEF.Tact.PlateAngle.CycleTime;
                    Move_PlateAngle_ReadyPosition_Double();
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, "[Side Angle Measure] Move Side Angle Measure Ready Position.", true);
                    Step++;
                    break;

                case 25:
                    cDEF.Tact.LensTact.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_PlateAngleMeasure)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunPlateAngleMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.PlateAngle, $"[Side Angle Measure] Measure End - CycleTime : [{cDEF.Tact.PlateAngle.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        #endregion
        #region Move Command
        public void Move_PlateAngle_ReadyPosition()
        {
            int Sleep;
            Sleep = cDEF.Work.PlateAngle.MovingDelay;
            PlateAngle.Absolute(cDEF.Work.TeachPlateAngle.ReadyPosition, Sleep);
        }
        public void Move_PlateAngle_ReadyPosition_Double()
        {
            int Sleep;
            Sleep = cDEF.Work.PlateAngle.MovingDelay;
            PlateAngle.Absolute(cDEF.Work.TeachPlateAngle.ReadyPosition, PlateAngle.Speed.FRun.FMaximumVelocity * 2, Sleep);
        }
        public void Move_PlateAngle_WorkPosition()
        {
            int Sleep;
            Sleep = cDEF.Work.PlateAngle.MovingDelay;
            PlateAngle.Absolute(cDEF.Work.TeachPlateAngle.WorkPosition, Sleep);
        }
        #endregion

        #region CheckPosition
        public bool Is_PlateAngle_ReadyPosition()
        {
            if (IsRange((double)cDEF.Work.TeachPlateAngle.ReadyPosition, PlateAngle.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_PlateAngle_WorkPosition()
        {
            if (IsRange((double)cDEF.Work.TeachPlateAngle.WorkPosition, PlateAngle.ActualPosition))
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

        private void TactTimeDataCopy()
        {
            Information.LensData.TT_LensTact = Information.IndexData.TT_LensTact;
            Information.LensData.TT_VCMLoader = Information.IndexData.TT_VCMLoader;
            Information.LensData.TT_VCMPicker = Information.IndexData.TT_VCMPicker;
            Information.LensData.TT_LensLoader = Information.IndexData.TT_LensLoader;
            Information.LensData.TT_LensPicker = Information.IndexData.TT_LensPicker;
            Information.LensData.TT_LensPickerPick = Information.IndexData.TT_LensPickerPick;
            Information.LensData.TT_LensPickerPlace = Information.IndexData.TT_LensPickerPlace;
            Information.LensData.TT_LensPickerCam = Information.IndexData.TT_LensPickerCam;
            Information.LensData.TT_JigPlateAngle = Information.IndexData.TT_JigPlateAngle;
            Information.LensData.TT_LensHeight = Information.IndexData.TT_LensHeight;
            Information.LensData.TT_Bonder1 = Information.IndexData.TT_Bonder1;
            Information.LensData.TT_Bonder1GapMesure = Information.IndexData.TT_Bonder1GapMesure;
            Information.LensData.TT_Bonder1Cam = Information.IndexData.TT_Bonder1Cam;
            Information.LensData.TT_Bonder2 = Information.IndexData.TT_Bonder2;
            Information.LensData.TT_Bonder2GapMesure = Information.IndexData.TT_Bonder2GapMesure;
            Information.LensData.TT_Bonder2Cam = Information.IndexData.TT_Bonder2Cam;
            Information.LensData.TT_VisionInspect = Information.IndexData.TT_VisionInspect;
            Information.LensData.TT_Curing1 = Information.IndexData.TT_Curing1;
            Information.LensData.TT_Curing2 = Information.IndexData.TT_Curing2;
            Information.LensData.TT_PlateAngle = Information.IndexData.TT_PlateAngle;
            Information.LensData.TT_Unloader = Information.IndexData.TT_Unloader;
            Information.LensData.TT_UnloadPicker = Information.IndexData.TT_UnloadPicker;
            Information.LensData.TT_CleanJig = Information.IndexData.TT_CleanJig;
            Information.LensData.TT_Index = Information.IndexData.TT_Index;
            Information.LensData.TT_TopVision = Information.IndexData.TT_TopVision;
            Information.LensData.TT_LensPickerPickCam = Information.IndexData.TT_LensPickerPickCam;
        }


    }
}

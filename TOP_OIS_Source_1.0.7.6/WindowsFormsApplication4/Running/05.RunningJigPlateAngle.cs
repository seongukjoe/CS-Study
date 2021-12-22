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

    public class RunJigPlateAngleInformation : fpObject
    {

        #region 변수
        private double FValue = 0;
        //public LensIndexStatus Status;
        public Index_Data IndexData;

        public double[][] AngleData = new double[12][];
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
        #endregion

        public RunJigPlateAngleInformation() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }
        public void AngleDataClear()
        {
            for (int i = 0; i < 12; i++)
            {
                AngleData[i] = new double[6];
            }
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunJigPlateAngle.dat";
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
                    case "Value": FValue = Convert.ToInt32(sArr[1]); break;
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


           // FileWrite.WriteLine($"Value,{FValue}");

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
            FValue = 0;
            IndexData.Clear();
            Unlock(Ignore);
        }
    }
    public enum TRunJigPlateAngleMode
    {
        Stop,
        Measure,                  //  Measure
    };

    
    public class RunJigPlateAngle : TfpRunningModule 
    {
        //Evnet
        public delegate void LedStarterHandler(int led);
        public event LedStarterHandler OnLedStarter;

        public delegate void LedFinishHandler(int led);
        public event LedFinishHandler OnLedFinish;

        private RunJigPlateAngleInformation FInformation;
        private TRunJigPlateAngleMode FMode;

        public int FCalc;

        public RunJigPlateAngle(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunJigPlateAngleInformation();
        }
        

        #region **Property**
        public RunJigPlateAngleInformation Information
        {
            get { return FInformation; }
        }

        public TRunJigPlateAngleMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        // Motor
        public TfpMotionItem JigPlateAngle
        {
            get { return GetMotions(0); }
        }
        #endregion //Property//

        private TRunJigPlateAngleMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunJigPlateAngleMode.Stop;
        }
        private void SetMode(TRunJigPlateAngleMode Value)
        {
            if (Value == TRunJigPlateAngleMode.Stop)
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
                case TRunJigPlateAngleMode.Stop:
                    return "Stop";
                case TRunJigPlateAngleMode.Measure:
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
                    JigPlateAngle.Home();
                    Step++;
                    break;
                case 1:
                    if (!JigPlateAngle.HomeEnd)
                        break;
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Index, "[Initialize] Jig Plate End", true);
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
                case TRunJigPlateAngleMode.Measure:
                    Running_Measure();
                    break;
                
            }

        }

        public int ManualMeasureCount = 0;
        protected override void ProcMain()
        {
            if (cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if (Information.IndexData.Status == eLensIndexStatus.VCMLoaded)
                {
                    cDEF.Tact.JigPlateAngle.Start();
                    Mode = TRunJigPlateAngleMode.Measure;
                    cDEF.TaskLogAppend(TaskLog.Index, "[Jig Flatness] Jig Flatness Start", true);
                    return;
                }
            }

            // 단일모드
            if (cDEF.Run.Mode == TRunMode.Manual_MeasureJigPlateAngle)
            {
                Mode = TRunJigPlateAngleMode.Measure;
                return;
            }
            

            // Index Measure Mode
            if(cDEF.Run.Mode == TRunMode.Manual_Jig_Index_Measure)
            {
                if (ManualMeasureCount < 12)
                {
                    Mode = TRunJigPlateAngleMode.Measure;
                    return;
                }
                else
                    cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
            }




            
        }

        #region Running Func

        StringBuilder strData = new StringBuilder();
        protected void Running_Measure()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    // cDEF.Run.PlateAngle.Move_PlateAngle_ReadyPosition();
                    Move_JigPlateAngle_ReadyPosition();
                    cDEF.TaskLogAppend(TaskLog.Index, "[Jig Flatness] Move Jig Flatness Ready Position", true);
                    Step++;
                    break;

                case 1:
                    if (!cDEF.Run.JigPlateAngle.IsReady())
                        break;

                    if (!cDEF.Work.Project.GlobalOption.UseJigPlateAngle)
                    {
                        cDEF.Tact.JigPlateAngle.GetTact();
                        Information.IndexData.Status = eLensIndexStatus.JigPlateAngleFinish;
                        Mode = TRunJigPlateAngleMode.Stop;
                        if (cDEF.Run.Mode == TRunMode.Manual_MeasureJigPlateAngle ||
                            cDEF.Run.Mode == TRunMode.Manual_Jig_Index_Measure)
                            cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                        Mode = TRunJigPlateAngleMode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Index, $"[Jig Flatness] Measure End - [{cDEF.Tact.JigPlateAngle.CycleTime.ToString("N3")}].");
                        return;
                    }
                    else
                        Step++;
                    break;
                case 2:
                    cDEF.SideAngleMeasuring.Send_Trigger();
                    cDEF.TaskLogAppend(TaskLog.Index, "[Jig Flatness] Angle Measuring Send Trigger", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;
                case 3:
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
                        cDEF.Run.SetAlarmID = cLog.RunJigPlateAngle + 1;
                        //cDEF.Run.LogWarning(cLog.RunJigPlateAngle + 1, "[Jig Flatness] Angle Measuring Send Trigger Fail");
                        cDEF.Run.LogWarning(cLog.RunJigPlateAngle + 1, "");
                        Mode = TRunJigPlateAngleMode.Stop;
                        return;
                    }
                    break;
                case 4:
                    //cDEF.Run.PlateAngle.Move_PlateAngle_WorkPosition();
                    Move_JigPlateAngle_WorkPosition();
                    cDEF.TaskLogAppend(TaskLog.Index, "[Jig Flatness] Move Jig Flatness Work Position", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 5:
                    if (!cDEF.Run.PlateAngle.IsReady())
                        break;
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.SideAngleMeasuring.ReciveStatus == CmmStatus.Ok ||
                            cDEF.SideAngleMeasuring.ReciveStatus == CmmStatus.Ng)
                        {
                            strData.Clear();
                            Information.AngleData[(cDEF.Run.Index.Information.IndexNum + 11) % 12][0] = cDEF.SideAngleMeasuring.AngleValue;
                            strData.Append(((cDEF.Run.Index.Information.IndexNum + 11) % 12)+1);
                            strData.Append(",");
                            strData.Append(cDEF.SideAngleMeasuring.AngleValue);
                            strData.Append(",");
                            for (int i = 1; i < 6; i++)
                            {
                                Information.AngleData[(cDEF.Run.Index.Information.IndexNum + 11) % 12][i] = cDEF.SideAngleMeasuring.Side_PeakZ_Value[i - 1];
                                strData.Append(cDEF.SideAngleMeasuring.Side_PeakZ_Value[i - 1]);
                                strData.Append(",");
                            }

                            Information.Value = cDEF.SideAngleMeasuring.AngleValue;
                            cDEF.TaskLogAppend(TaskLog.JigPlateAngleData, strData.ToString(), true);
                            Step++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Information.AngleData[(cDEF.Run.Index.Information.IndexNum + 11) % 12][i] = 0;
                        }

                        Information.Value = 0;
                        Step++;
                    }
                    break;
                case 6:
                    // Spec out
                    if (Information.Value == 0)
                    {
                        // Information.IndexData.Status = eLensIndexStatus.JigPlateAngleFail;
                        Information.IndexData.Status = eLensIndexStatus.JigPlateAngleFinish;
                    }
                    else
                    {
                        Information.IndexData.Status = eLensIndexStatus.JigPlateAngleFinish;
                    }
                    Step++;
                    break;
                case 7:
                    if (cDEF.Run.Mode == TRunMode.Manual_Jig_Index_Measure)
                        cDEF.Run.Index.Mode = TRunIndexMode.StepMove;
                    Step++;
                    break;
                case 8:
                    if (cDEF.Run.Index.Mode != TRunIndexMode.Stop)
                        break;
                    if (!cDEF.Run.Index.IsReady())
                        break;

                    ManualMeasureCount++;
                    cDEF.Tact.JigPlateAngle.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_MeasureJigPlateAngle)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunJigPlateAngleMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Index, $"[Jig Flatness] JigFlatness End - CycleTime [{cDEF.Tact.JigPlateAngle.CycleTime.ToString("N3")}]", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }


        #endregion
        #region Move Command
        public void Move_JigPlateAngle_ReadyPosition()
        {
            int Sleep;
            Sleep = cDEF.Work.JigPlateAngle.MovingDelay;
            JigPlateAngle.Absolute(cDEF.Work.TeachJigPlateAngle.ReadyPosition, Sleep);
        }
        public void Move_JigPlateAngle_WorkPosition()
        {
            int Sleep;
            Sleep = cDEF.Work.JigPlateAngle.MovingDelay;
            JigPlateAngle.Absolute(cDEF.Work.TeachJigPlateAngle.WorkPosition, Sleep);
        }
        #endregion

    #region CheckPosition
        public bool Is_JigPlateAngle_ReadyPosition()
        {
            if (IsRange((double)cDEF.Work.TeachJigPlateAngle.ReadyPosition, JigPlateAngle.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_JigPlateAngle_WorkPosition()
        {
            if (IsRange((double)cDEF.Work.TeachJigPlateAngle.WorkPosition, JigPlateAngle.ActualPosition))
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

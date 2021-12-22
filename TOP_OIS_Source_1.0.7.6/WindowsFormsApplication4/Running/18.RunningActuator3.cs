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

    public class RunActuator3Information : fpObject
    {

        #region 변수
        public Index_Data IndexData;
        public bool Result = false;

        #endregion

        #region Property

        #endregion

        public RunActuator3Information() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunActuator3.dat";
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
            IndexData.Clear();
            Unlock(Ignore);
        }
    }
    public enum TRunActuator3Mode
    {
        Stop,
        Actuating,
    };

    
    public class RunActuator3 : TfpRunningModule 
    {
        private RunActuator3Information FInformation;
        private TRunActuator3Mode FMode;

        public int FCalc;
        private int RetryCount = 0;
        public RunActuator3(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunActuator3Information();
        }
        

        #region **Property**
        public RunActuator3Information Information
        {
            get { return FInformation; }
        }

        public TRunActuator3Mode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        public TfpCylinderItem Contact
        {
            get { return GetCylinders(0); }
        }
       
        public TfpCylinderItem ClampDown
        {
            get { return GetCylinders(1); }
        }

        #endregion //Property//

        private TRunActuator3Mode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunActuator3Mode.Stop;
        }
        private void SetMode(TRunActuator3Mode Value)
        {
            if (Value == TRunActuator3Mode.Stop)
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
                case TRunActuator3Mode.Stop:
                    return "Stop";
                case TRunActuator3Mode.Actuating:
                    return "Actuating";

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
                    Step++;
                    break;
                case 1:
                    if (cDEF.Work.Option.ActuatingType == 1)
                    {
                        ClampDown.Backward();
                        Contact.Backward();
                    }
                    Step++;
                    break;
                case 2:
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
                    cDEF.TaskLogAppend(TaskLog.Act3, "[Initialize] End", true);
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
                    if (cDEF.Work.Option.ActuatingType == 1)
                    {
                        ClampDown.Backward();
                        Contact.Backward();
                    }
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.Act3, "[To-Run] Done.", true);
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
                    if (cDEF.Work.Option.ActuatingType == 1)
                    {
                        ClampDown.Backward();
                        Contact.Backward();
                    }
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Act3, "[To-Stop] Done.", true);
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
                case TRunActuator3Mode.Actuating:
                    Running_Actuating();
                    break;
            }
        }
        
        protected override void ProcMain()
        {
            if (cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if (Information.IndexData.Status == eLensIndexStatus.JigPlateAngleFinish)
                {
                    cDEF.Tact.Actuator3.Start();
                    Mode = TRunActuator3Mode.Actuating;
                    cDEF.TaskLogAppend(TaskLog.Act3, $"[Act 3]Index:{Information.IndexData.Index + 1} Actuating Start.", true);
                    return;
                }
            }
         
            if (cDEF.Run.Mode == TRunMode.Manual_Act3_Actuating)
            {
                cDEF.Tact.Actuator3.Start();
                Mode = TRunActuator3Mode.Actuating;
                cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Actuating Start.", true);
                return;
            }
        }
        #region Running Func
        protected void Running_Actuating()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if (!cDEF.Work.Project.GlobalOption.Actuator_3_Mode)
                    {
                        Information.Result = true;
                        Step = 7;
                        break;
                    }

                    if (cDEF.Work.Option.ActuatingType == 1)
                    {
                        ClampDown.Backward();
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Up_Down Cylinder Up.", true);
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Contact Cylinder Uncontact.", true);
                    }
                    Information.Result = false;
                    Step++;
                    break;

                case 1:
                    if (!cDEF.Work.Project.GlobalOption.UseActAction3 || cDEF.Work.Option.ActuatingType == 0)
                    {
                        cDEF.Tact.Actuator3.GetTact();
                        Information.IndexData.Status = eLensIndexStatus.Act3Finish;

                        Mode = TRunActuator3Mode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Actuating End.", true);
                        return;
                    }
                    if (cDEF.Work.Project.GlobalOption.IndexCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.Actuator3_IndexCheck])
                        {
                            cDEF.Run.LogWarning(cLog.RunActuator3 + 203, "[Act 3] Product is UnMatching.");
                            cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Product is Unmatching.", true);
                            Mode = TRunActuator3Mode.Stop;
                            return;
                        }
                    }
                    if (!cDEF.Run.Digital.Input[cDI.Actuator_1_Ready])
                    {
                        cDEF.Run.LogWarning(cLog.RunActuator3 + 200, "[Act 3] Actuator is not Ready");
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Actuator is Not Ready.", true);
                        Mode = TRunActuator3Mode.Stop;
                        return;
                    }

                    ClampDown.Forward();
                    cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Up-Down Cylinder Dowm.", true);
                    Step++;
                    break;

                case 2:
                    Contact.Forward();
                    cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Contact Cylinder Contact.", true);
                    Step++;
                    break;

                case 3:
                    // Mode 에따라서 
                    if (cDEF.Work.Project.GlobalOption.Actuator_3_Mode)
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_3_A_Start] = true;
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3]  Actuator A Start (I/O) On.", true);
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_3_B_Start] = true;
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3]  Actuator B Start (I/O) On.", true);
                    }
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 4:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Actuator_3_Ready] || cDEF.Run.Digital.Input[cDI.Actuator_3_Pass] || cDEF.Run.Digital.Input[cDI.Actuator_3_Fail])
                            break;
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Actuator Input Ready Off OK.", true);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        ClampDown.Backward();
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Up_Down Cylinder Up.", true);
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Contact Cylinder Uncontact.", true);
                        cDEF.Run.LogWarning(cLog.RunActuator3 + 204, "");
                        Mode = TRunActuator3Mode.Stop;
                        return;
                    }
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < cDEF.Work.Curing1.ActuatorTime)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Actuator_3_Pass])
                        {
                            cDEF.Run.Digital.Output[cDO.Actuator_3_A_Start] = false;
                            cDEF.Run.Digital.Output[cDO.Actuator_3_B_Start] = false;
                            cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Actuator Input Pass OK.", true);
                            Information.Result = true;
                            Step++;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Actuator_3_Fail])
                        {
                            cDEF.Run.Digital.Output[cDO.Actuator_3_A_Start] = false;
                            cDEF.Run.Digital.Output[cDO.Actuator_3_B_Start] = false;
                            cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Actuator Input Fail Signal.", true);
                            Information.Result = false;
                            
                        }
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;
                        cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Time Out - Actuator Signal Off.", true);
                        Information.Result = false;
                        Step++;
                    }
                    break;

                case 6:
                    ClampDown.Backward();
                    cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Up_Down Cylinder Up.", true);
                    Contact.Backward();
                    cDEF.TaskLogAppend(TaskLog.Act3, "[Act 3] Contact Cylinder Uncontact.", true);
                    Step++;
                    break;

                case 7:
                    if(Information.Result)
                        Information.IndexData.Status = eLensIndexStatus.Act3Finish;
                    else
                        Information.IndexData.Status = eLensIndexStatus.Act3Fail;
                    Step++;
                    break;

                case 8:
                    if (cDEF.Run.Mode == TRunMode.Manual_Act3_Actuating)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunActuator3Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Act3, $"[Act 3] Actuating End - Cycle Time : [{cDEF.Tact.Actuator3.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        
        #endregion
    }
}

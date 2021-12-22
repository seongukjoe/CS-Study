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

    public class RunCuring1Information : fpObject
    {

        #region 변수
        //public LensIndexStatus Status;
        public Index_Data IndexData;
        public bool ActuatingFinish = false;
        public bool ActuatingResult = false;
        #endregion

        #region Property

        #endregion

        public RunCuring1Information() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunCuring1.dat";
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
    public enum TRunCuring1Mode
    {
        Stop,
        Actuating,
        Curing,                  
    };

    
    public class RunCuring1 : TfpRunningModule 
    {
        //Evnet
        public delegate void LedStarterHandler(int led);
        public event LedStarterHandler OnLedStarter;

        public delegate void LedFinishHandler(int led);
        public event LedFinishHandler OnLedFinish;

        private RunCuring1Information FInformation;
        private TRunCuring1Mode FMode;

        public int FCalc;
        private int RetryCount = 0;
        public RunCuring1(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunCuring1Information();
        }
        

        #region **Property**
        public RunCuring1Information Information
        {
            get { return FInformation; }
        }

        public TRunCuring1Mode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        public TfpCylinderItem Contact
        {
            get { return GetCylinders(0); }
        }
       
        public TfpCylinderItem UVDown
        {
            get { return GetCylinders(1); }
        }

        #endregion //Property//

        private TRunCuring1Mode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunCuring1Mode.Stop;
        }
        private void SetMode(TRunCuring1Mode Value)
        {
            if (Value == TRunCuring1Mode.Stop)
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
                case TRunCuring1Mode.Stop:
                    return "Stop";
                case TRunCuring1Mode.Actuating:
                    return "Actuating";
                case TRunCuring1Mode.Curing:
                    return "UV";

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
                    UVDown.Backward();
                    Step++;
                    break;
                case 1:
                    Contact.Backward();
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
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Initialize] End", true);
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
                    UVDown.Backward();
                    Contact.Backward();
                    Step++;
                    break;

                case 1:
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[To-Run] Done.", true);
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
                    UVDown.Backward();
                    Contact.Backward();
                    Step++;
                    break;
                case 1:
                    Step++;
                    break;
                case 2:
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[To-Stop] Done.", true);
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
                //case TRunCuring1Mode.Curing:
                //    Running_Curing();
                //    break;

                case TRunCuring1Mode.Actuating:
                    // Running_Actuating();
                    Running_ActuatingAndUV();
                    break;


            }
        }
        
        protected override void ProcMain()
        {

            //CIC _ 20200529 SSJ:Actuating 이 컨텍이 된 상태에서 UV 동작

            if (cDEF.Run.Mode == TRunMode.Main_Run)
            {
                int IndexNum = (cDEF.Run.Index.Information.IndexNum + 5) % 12;

                if (cDEF.Work.Option.IndexSkip[IndexNum])
                    return;

                if (cDEF.Work.Project.GlobalOption.UseCureVisionFail)
                {
                    if(Information.IndexData.Status == eLensIndexStatus.VisionInspectFinish || Information.IndexData.Status == eLensIndexStatus.VisionInspectFail)
                    {
                        cDEF.Tact.Curing1.Start();
                        Mode = TRunCuring1Mode.Actuating;
                        cDEF.TaskLogAppend(TaskLog.Cure1, $"[Curing 1]Index:{Information.IndexData.Index + 1} Actuating Start.", true);
                        return;
                    }
                }
                else
                {
                    if (Information.IndexData.Status == eLensIndexStatus.VisionInspectFinish)
                    {
                        cDEF.Tact.Curing1.Start();
                        Mode = TRunCuring1Mode.Actuating;
                        cDEF.TaskLogAppend(TaskLog.Cure1, $"[Curing 1]Index:{Information.IndexData.Index + 1} Actuating Start.", true);
                        return;
                    }
                }

            }
         
            if (cDEF.Run.Mode == TRunMode.Manual_Cure1ActuatingAnd1UV)
            {
                cDEF.Tact.Curing1.Start();
                Mode = TRunCuring1Mode.Actuating;
                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuating Start.", true);
                return;
            }
            #region 기존코드
            //if (cDEF.Run.Mode == TRunMode.Main_Run)
            //{
            //    if(Information.IndexData.Status == eLensIndexStatus.VisionInspectFinish
            //        && !Information.ActuatingFinish)
            //    {
            //        cDEF.Tact.Curing1.Start();
            //        Mode = TRunCuring1Mode.Actuating;
            //        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuating Start.", true);
            //        return;
            //    }

            //    if (Information.IndexData.Status == eLensIndexStatus.VisionInspectFinish
            //        && Information.ActuatingFinish)
            //    {
            //        Mode = TRunCuring1Mode.Curing;
            //        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Curing Start.", true); 
            //        return;
            //    }
            //}

            //if(cDEF.Run.Mode == TRunMode.Manual_Cure1Actuating)
            //{
            //    cDEF.Tact.Curing1.Start();
            //    Mode = TRunCuring1Mode.Actuating;
            //    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1 - Manual]  Curing 1 Actuating Start", true);
            //    return;
            //}

            //if (cDEF.Run.Mode == TRunMode.Manual_Cure1UV)
            //{
            //    Mode = TRunCuring1Mode.Curing;
            //    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1 - Manual]  Curing 1 Cure Start.", true);
            //    return;
            //}

           
            #endregion

        }

        #region Running Func

        protected void Running_ActuatingAndUV()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    UVDown.Backward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);
                    Contact.Backward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
                    Information.ActuatingResult = true;
                    RetryCount = 0;
                    Step++;
                    break;

                case 1:
                    if (!cDEF.Work.Project.GlobalOption.UseCuring1)
                    {
                        cDEF.Tact.Curing1.GetTact();
                        Information.IndexData.Status = eLensIndexStatus.Curing1Finish;

                        if (cDEF.Run.Mode == TRunMode.Manual_Cure1ActuatingAnd1UV)
                            cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                        Mode = TRunCuring1Mode.Stop;
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuating End.", true);
                        return;
                    }
                    if (cDEF.Work.Project.GlobalOption.IndexCheck)
                    {
                        if (!cDEF.Run.Digital.Input[cDI.Curing_1])
                        {
                            cDEF.Run.SetAlarmID = cLog.RunCuring1 + 203;
                            //cDEF.Run.LogWarning(cLog.RunCuring1 + 203, "[CURING 1] Product is UnMatching.");
                            cDEF.Run.LogWarning(cLog.RunCuring1 + 203, "");
                            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Product is Unmatching.", true);
                            Mode = TRunCuring1Mode.Stop;
                            return;
                        }
                    }
                    if (!cDEF.Run.Digital.Input[cDI.Actuator_1_Ready])
                    {
                        cDEF.Run.SetAlarmID = cLog.RunCuring1 + 200;
                        //cDEF.Run.LogWarning(cLog.RunCuring1 + 200, "[CURING 1] Actuator is not Ready");
                        cDEF.Run.LogWarning(cLog.RunCuring1 + 200, "");
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator is Not Ready.", true);
                        Mode = TRunCuring1Mode.Stop;
                        return;
                    }
                    Step++;
                    break;

///////////////////////////////////////Test Start Point,ksyoon,210415////////////////////////////////////////
                case 2:
                    if(!cDEF.Work.Project.GlobalOption.UsePreActuating)
                    {
                        Step = 9;
                        break;
                    }
                    if (!Contact.IsBackward())
                        Contact.Backward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
                    Step++;
                    break;

                case 3:
                    UVDown.Forward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up-Down Cylinder Dowm.", true);
                    Step++;
                    break;

                case 4:
                    Contact.Forward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Contact.", true);
                    Step++;
                    break;

                case 5:
                    UVDown.Backward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up-Down Cylinder Up.", true);
                    Step++;
                    break;

                case 6:
                    cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = true;
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1]  Actuator A Start (I/O) On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 7:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Actuator_1_Ready]
                            || cDEF.Run.Digital.Input[cDI.Actuator_1_Pass]
                            || cDEF.Run.Digital.Input[cDI.Actuator_1_Fail])
                            break;

                        // all false
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Ready Off OK.", true);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        UVDown.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);

                        if (cDEF.Run.Digital.Input[cDI.Actuator_1_OISFail])
                        {

                            cDEF.Run.SetAlarmID = cLog.RunCuring1 + 205;
                            cDEF.Run.LogWarning(cLog.RunCuring1 + 205, "");
                        }
                        else if (cDEF.Run.Digital.Input[cDI.Actuator_1_AFFail])
                        {

                            cDEF.Run.SetAlarmID = cLog.RunCuring1 + 206;
                            cDEF.Run.LogWarning(cLog.RunCuring1 + 206, "");
                        }
                        else
                        {

                            cDEF.Run.SetAlarmID = cLog.RunCuring1 + 204;
                            cDEF.Run.LogWarning(cLog.RunCuring1 + 204, "");
                        }
                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;

                        Mode = TRunCuring1Mode.Stop;
                        return;
                    }
                    break;

                case 8:
                    if (Environment.TickCount - FCalc < cDEF.Work.Curing1.ActuatorTime)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Actuator_1_Pass])
                        {
                            cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Pass OK.", true);
                            Step++;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Actuator_1_Fail])
                        {
                            if (cDEF.Work.Project.GlobalOption.UseActRetry)
                            {
                                if(RetryCount < 1)
                                {
                                    Contact.Backward();
                                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
                                    RetryCount++;
                                    Step = 2;
                                    break;
                                }
                            }
                            cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Fail Signal.", true);
                            Step++;
                        }
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Time Out - Actuator Signal Off.", true);
                        Step++;
                    }
                    
                    break;

///////////////////////////////////////Test End Point,ksyoon,210415////////////////////////////////////////
                case 9:
                    if(!Contact.IsBackward())
                        Contact.Backward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
                    Step++;
                    break;

                case 10:
                    UVDown.Forward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up-Down Cylinder Dowm.", true);
                    Step++;
                    break;

                case 11:
                    if (cDEF.Work.Project.GlobalOption.UseActAction1)
                    {
                        if (cDEF.Work.Project.GlobalOption.UseActAndCure)
                            Contact.Forward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Contact.", true);
                        Step++;
                    }
                    else
                    {
                        Step = 19;
                    }
                    break;

                case 12:
                    // Mode 에따라서 
                    if (cDEF.Work.Project.GlobalOption.Actuator_1_Mode)
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = true;
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1]  Actuator A Start (I/O) On.", true);
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = true;
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1]  Actuator B Start (I/O) On.", true);
                    }
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 13:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Actuator_1_Ready] 
                            || cDEF.Run.Digital.Input[cDI.Actuator_1_Pass] 
                            || cDEF.Run.Digital.Input[cDI.Actuator_1_Fail])
                            break;

                       // all false
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Ready Off OK.", true);
                        FCalc = Environment.TickCount;
                        Step++;
                    }
                    else
                    {
                        UVDown.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
                        
                        if (cDEF.Run.Digital.Input[cDI.Actuator_1_OISFail])
                        {
                            
                            cDEF.Run.SetAlarmID = cLog.RunCuring1 + 205;
                            cDEF.Run.LogWarning(cLog.RunCuring1 + 205, "");
                        }
                        else if (cDEF.Run.Digital.Input[cDI.Actuator_1_AFFail])
                        {
                            
                            cDEF.Run.SetAlarmID = cLog.RunCuring1 + 206;
                            cDEF.Run.LogWarning(cLog.RunCuring1 + 206, "");
                        }
                        else
                        {
                         
                            cDEF.Run.SetAlarmID = cLog.RunCuring1 + 204;
                            cDEF.Run.LogWarning(cLog.RunCuring1 + 204, "");
                        }
                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;

                        Mode = TRunCuring1Mode.Stop;
                        return;
                    }
                    break;

                case 14:
                    if (cDEF.Run.Digital.Input[cDI.Actuator_1_OISFail])
                    {
                        UVDown.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);

                        cDEF.Run.SetAlarmID = cLog.RunCuring1 + 205;
                        cDEF.Run.LogWarning(cLog.RunCuring1 + 205, "");

                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;

                        Mode = TRunCuring1Mode.Stop;
                        return;
                    }
                    else if (cDEF.Run.Digital.Input[cDI.Actuator_1_AFFail])
                    {
                        UVDown.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);
                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);

                        cDEF.Run.SetAlarmID = cLog.RunCuring1 + 206;
                        cDEF.Run.LogWarning(cLog.RunCuring1 + 206, "");

                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;

                        Mode = TRunCuring1Mode.Stop;
                        return;
                    }
                    Step++;
                    break;

                case 15:
                    if (Environment.TickCount - FCalc < cDEF.Work.Curing1.ActuatorTime)
                    {
                        if (cDEF.Run.Digital.Input[cDI.Actuator_1_Pass])
                        {
                            cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                            cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;
                            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Pass OK.", true);
                            Step++;
                        }
                        if (cDEF.Run.Digital.Input[cDI.Actuator_1_Fail])
                        {
                            cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                            cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;
                            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Fail Signal.", true);
                            Information.ActuatingResult = false;
                            Step++;
                        }
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
                        cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Time Out - Actuator Signal Off.", true);

                        Information.ActuatingResult = false; 
                        Step++;
                    }
                    break;

                case 16:
                    if (!Information.ActuatingResult)
                    {
                        if (cDEF.Work.Project.GlobalOption.UseActRetry)
                        {
                            if (RetryCount < 1)
                            {
                                Information.ActuatingResult = true;

                                UVDown.Backward();
                                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);

                                Contact.Backward();
                                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);

                                RetryCount++;
                                Step = 8;
                                break;
                            }
                        }

                        //cDEF.Tact.Curing1.GetTact();
                        //Information.IndexData.Status = eLensIndexStatus.Actuating1Fail;
                        Information.IndexData.FailType = eFailType.Actuating1Fail;

                        //if (cDEF.Run.Mode == TRunMode.Manual_Cure1ActuatingAnd1UV)
                        //    cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                        //Mode = TRunCuring1Mode.Stop;
                        //cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Fail.", true);
                        //return;
                       
                    }
                    else
                        cDEF.TaskLogAppend(TaskLog.Cure1, $"[Curing 1] Actuating End - Cycle Time : [{cDEF.Tact.Curing1.CycleTime.ToString("N3")}].", true);
                   
                    Step++;
                    break;

                case 17:
                    if (!cDEF.Work.Project.GlobalOption.UseActAndCure)
                        Contact.Backward();

                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
                    Step++;
                    break;

                case 18:
                    if (cDEF.Run.Digital.Input[cDI.UV_1_Alarm_Monitor] || !cDEF.Run.Digital.Input[cDI.UV_1_Lamp_Ready_Monitor])
                    {
                        UVDown.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);

                        Contact.Backward();
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);

                        cDEF.Run.SetAlarmID = cLog.RunCuring1 + 202;
                        //cDEF.Run.LogWarning(cLog.RunCuring1 + 202, "[CURING 1] UV Lamp is not On");
                        cDEF.Run.LogWarning(cLog.RunCuring1 + 202, "");
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] UV Lamp is Not On.", true);
                        Mode = TRunCuring1Mode.Stop;
                        return;
                    }
                    Step++;
                    break;           
                    
                case 19:
                    cDEF.Run.Digital.Output[cDO.UV_1_Start] = true;
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] UV 1 Shutter Open.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 20:
                    if (Environment.TickCount - FCalc < cDEF.Work.Curing1.CuringTime)
                        break;
                    cDEF.Run.Digital.Output[cDO.UV_1_Start] = false;
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] UV 1 Shutter Close.", true);
                    Step++;
                    break;

                case 21:
                    UVDown.Backward();
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up-Down Cylinder Up.", true);

                    Contact.Backward();  
                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);

                    if (!Information.ActuatingResult)
                    {
                        Information.IndexData.Status = eLensIndexStatus.Actuating1Fail;
                        Information.IndexData.FailType = eFailType.Actuating1Fail;
                        cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Fail.", true);
                    }
                    else
                    {
                       Information.IndexData.Status = eLensIndexStatus.Curing1Finish;
                    }
                    cDEF.Tact.Curing1.GetTact();
                    Information.IndexData.TT_Curing1 = cDEF.Tact.Curing1.CycleTime;
                    Step++;
                    break;

                case 22:
                    if (cDEF.Run.Mode == TRunMode.Manual_Cure1ActuatingAnd1UV)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

                    Mode = TRunCuring1Mode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Cure1, $"[Curing 1] Cure End - Cycle Time : [{cDEF.Tact.Curing1.CycleTime.ToString("N3")}].", true);
                    break;
                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }

        #region 기존코드
        //protected void Running_Actuating()
        //{
        //    if (!IsReady())
        //        return;

        //    switch (Step)
        //    {
        //        case 0:
        //            UVDown.Backward();
        //            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);
        //            Contact.Backward();
        //            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
        //            Information.ActuatingResult = true;
        //            Step++;
        //            break;

        //        case 1:
        //            if (!cDEF.Work.Project.GlobalOption.UseCuring1)
        //            {
        //                cDEF.Tact.Curing1.GetTact();
        //                Information.IndexData.Status = eLensIndexStatus.Curing1Finish;

        //                Mode = TRunCuring1Mode.Stop;
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuating End.", true);
        //                return;
        //            }
        //            if (cDEF.Work.Project.GlobalOption.IndexCheck)
        //            {
        //                if (!cDEF.Run.Digital.Input[cDI.Curing_1])
        //                {
        //                    cDEF.Run.LogWarning(cLog.RunCuring1 + 203, "[CURING 1] Product is UnMatching.");
        //                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Product is Unmatching.", true);
        //                    Mode = TRunCuring1Mode.Stop;
        //                    return;
        //                }
        //            }
        //            if (!cDEF.Run.Digital.Input[cDI.Actuator_1_Ready])
        //            {
        //                cDEF.Run.LogWarning(cLog.RunCuring1 + 200, "[CURING 1] Actuator is not Ready");
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator is Not Ready.", true);
        //                Mode = TRunCuring1Mode.Stop;
        //                return;
        //            }
                    
        //            UVDown.Forward();
        //            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up-Down Cylinder Dowm.", true);
                    
        //            Step++;
        //            break;

        //        case 2:
        //            Contact.Forward();
        //            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Contact.", true);
        //            Step++;
        //            break;

        //        case 3:
        //            // Mode 에따라서 
        //            if (cDEF.Work.Project.GlobalOption.Actuator_1_Mode)
        //            {
        //                cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = true;
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1]  Actuator A Start (I/O) On.", true);
        //            }
        //            else
        //            {
        //                cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = true;
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1]  Actuator B Start (I/O) On.", true);
        //            }
        //            FCalc = Environment.TickCount;
        //            Step++;
        //            break;

        //        case 4:
        //            if (Environment.TickCount - FCalc < 10000)
        //            {
        //                if (cDEF.Run.Digital.Input[cDI.Actuator_1_Ready] || cDEF.Run.Digital.Input[cDI.Actuator_1_Pass] || cDEF.Run.Digital.Input[cDI.Actuator_1_Fail])
        //                    break;
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Ready Off OK.", true);
        //                FCalc = Environment.TickCount;
        //                Step++;
        //            }
        //            else
        //            {
        //                UVDown.Backward();
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up_Down Cylinder Up.", true);
        //                Contact.Backward();
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
        //                cDEF.Run.LogWarning(cLog.RunCuring1 + 204, "");
        //                Mode = TRunCuring1Mode.Stop;
        //                return;
        //            }
        //            break;

        //        case 5:
        //            if (Environment.TickCount - FCalc < cDEF.Work.Curing1.ActuatorTime)
        //            {
        //                if (cDEF.Run.Digital.Input[cDI.Actuator_1_Pass])
        //                {
        //                    cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
        //                    cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;
        //                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Pass OK.", true);
        //                    Step++;
        //                }
        //                if (cDEF.Run.Digital.Input[cDI.Actuator_1_Fail])
        //                {
        //                    cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
        //                    cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;
        //                    cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Input Fail Signal.", true);
        //                    Information.ActuatingResult = false;
        //                }
        //            }
        //            else
        //            {
        //                cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = false;
        //                cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = false;
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Time Out - Actuator Signal Off.", true);

        //                Information.ActuatingResult = false;  //CIC _ 20200529 SSJ:타임 아웃 걸렸으면 NG로 빼야 할 듯 해서 추가
        //                Step++;
        //            }
        //            break;

        //        case 6:
        //            if (cDEF.Run.Mode == TRunMode.Manual_Cure1Actuating)
        //            {
        //                UVDown.Backward();
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1]  Up-Down Cylinder Up',", true);

        //                Contact.Backward();  //CIC _ 20200529 SSJ:Actuating 이 컨텍이 된 상태에서 UV 동작
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
        //            }

        //            //Contact.Backward();  //CIC _ 20200529 SSJ:Actuating 이 컨텍이 된 상태에서 UV 동작
        //            //cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1]  Contact Cylinder Uncontact.", true);
        //            Step++;
        //            break;

        //        case 7:
        //            if(!Information.ActuatingResult)
        //            {
        //                UVDown.Backward();

        //                Contact.Backward();  //CIC _ 20200529 SSJ:Actuating 이 컨텍이 된 상태에서 UV 동작
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);

        //                cDEF.Tact.Curing1.GetTact();
        //                Information.IndexData.Status = eLensIndexStatus.Actuating1Fail;
        //                Information.IndexData.FailType = eFailType.Actuating1Fail;
        //                Mode = TRunCuring1Mode.Stop;
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Actuator Fail.", true);
        //                return;
        //            }
        //            else
        //                Information.ActuatingFinish = true;
        //            Step++;
        //            break;

        //        case 8:
        //            if (cDEF.Run.Mode == TRunMode.Manual_Cure1Actuating)
        //                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
        //            Mode = TRunCuring1Mode.Stop;
        //            cDEF.TaskLogAppend(TaskLog.Cure1, $"[Curing 1] Actuating End - Cycle Time : [{cDEF.Tact.Curing1.CycleTime.ToString("N3")}].", true);
        //            break;

        //        default:
        //            throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
        //    }
        //    return;
        //}
        //protected void Running_Curing()
        //{
        //    if (!IsReady())
        //        return;

        //    switch (Step)
        //    {
        //        case 0:
        //            //UVDown.Backward();
        //            //cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up-Down Cylinder Up.", true);

        //            // Contact.Backward();  //CIC _ 20200529 SSJ:Actuating 이 컨텍이 된 상태에서 UV 동작
        //           // cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);
        //            Step++;
        //            break;

        //        case 1:
        //            if (cDEF.Run.Digital.Input[cDI.UV_1_Alarm_Monitor] || !cDEF.Run.Digital.Input[cDI.UV_1_Lamp_Ready_Monitor])
        //            {
        //                cDEF.Run.LogWarning(cLog.RunCuring1 + 202, "[CURING 1] UV Lamp is not On");
        //                cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] UV Lamp is Not On.", true);
        //                Mode = TRunCuring1Mode.Stop;
        //                return;
        //            }
        //            Step++;
        //            break;

        //        case 2:
        //            cDEF.Run.Digital.Output[cDO.UV_1_Start] = true;
        //            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] UV 1 Shutter Open.", true);
        //            FCalc = Environment.TickCount;
        //            Step++;
        //            break;

        //        case 3:
        //            if (Environment.TickCount - FCalc < cDEF.Work.Curing1.CuringTime)
        //                break;
        //            cDEF.Run.Digital.Output[cDO.UV_1_Start] = false;
        //            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] UV 1 Shutter Close.", true);
        //            Step++;
        //            break;

        //        case 4:
        //            UVDown.Backward();
        //            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Up-Down Cylinder Up.", true);

        //            Contact.Backward();  //CIC _ Case 0 -> 이동 : 20200529 SSJ:Actuating 이 컨텍이 된 상태에서 UV 동작
        //            cDEF.TaskLogAppend(TaskLog.Cure1, "[Curing 1] Contact Cylinder Uncontact.", true);

        //            Information.IndexData.Status = eLensIndexStatus.Curing1Finish;

        //            //CIC _ 추가 : 20200529 SSJ:Actuating 이 컨텍이 된 상태에서 UV 동작
        //            //Case 5번에 넣었느데 메뉴얼 동작시 Information.ActuatingFinish 정보를 보고 동작 하므로  미리 변경
        //            //또 동작 할까봐 불안함.. ㅎㅎㅎ 

        //            if (cDEF.Run.Mode == TRunMode.Manual_Cure1ActuatingAnd1UV)
        //                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

        //            Information.ActuatingFinish = false;
        //            Step++;
        //            break;

        //        case 5:
        //            cDEF.Tact.Curing1.GetTact();

        //            if (cDEF.Run.Mode == TRunMode.Manual_Cure1UV)
        //                cDEF.Run.DetailMode = TfpRunningMode.frmToStop;

        //            Mode = TRunCuring1Mode.Stop;
        //            cDEF.TaskLogAppend(TaskLog.Cure1, $"[Curing 1] Cure End - Cycle Time : [{cDEF.Tact.Curing1.CycleTime.ToString("N3")}].", true);
        //            break;

        //        default:
        //            throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
        //    }
        //    return;
        //}
        #endregion

        #endregion
    }
}

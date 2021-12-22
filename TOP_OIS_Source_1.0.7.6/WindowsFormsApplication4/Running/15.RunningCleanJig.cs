using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;
using System.Threading;

namespace XModule.Running
{

    public class RunCleanJigInformation : fpObject
    {

        #region 변수
        //public LensIndexStatus Status;
        public Index_Data IndexData;
        #endregion

        #region Property
        #endregion

        public RunCleanJigInformation() : base()
        {
            IndexData = new Index_Data();
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunCleanJig.dat";
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
    public enum TRunCleanJigMode
    {
        Stop,
        Clean,                  //  Cleaning
    };

    
    public class RunCleanJig : TfpRunningModule 
    {
        private RunCleanJigInformation FInformation;
        private TRunCleanJigMode FMode;

        public int FCalc;

        public RunCleanJig(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunCleanJigInformation();
        }
        

        #region **Property**
        public RunCleanJigInformation Information
        {
            get { return FInformation; }
        }

        public TRunCleanJigMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        public TfpCylinderItem CleanJigDown
        {
            get { return GetCylinders(0); }
        }
     
        #endregion //Property//

        private TRunCleanJigMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunCleanJigMode.Stop;
        }
        private void SetMode(TRunCleanJigMode Value)
        {
            if (Value == TRunCleanJigMode.Stop)
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
                case TRunCleanJigMode.Stop:
                    return "Stop";
                case TRunCleanJigMode.Clean:
                    return "Cleaning";

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
                    CleanJigDown.Backward();
                    Step++;
                    break;
                case 1:
                    cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Vacuum Off.", true);
                    cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Blow Off.", true);
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
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Initialize] End", true);
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
                    CleanJigDown.Backward();
                    Step++;
                    break;

                case 1:
                    cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Vacuum Off.", true);
                    cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Blow Off.", true);
                    Step++;
                    break;

                case 2:
                    Step++;
                    break;
                case 3:
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[To-Run] Done.", true);
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
                    CleanJigDown.Backward();
                    Step++;
                    break;

                case 1:
                    cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Vacuum Off.", true);
                    cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Blow Off.", true);
                    Step++;
                    break;

                case 2:
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[To-Stop] Done.", true);
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
                case TRunCleanJigMode.Clean:
                    Running_Cleaning();
                    break;
                
            }
        }
        
        protected override void ProcMain()
        {
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if(Information.IndexData.Status == eLensIndexStatus.UnloadFinish)
                {
                    cDEF.Tact.CleanJig.Start();
                    Mode = TRunCleanJigMode.Clean;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Clean Jig Start.", true);
                    return;
                }
            }


            if(cDEF.Run.Mode == TRunMode.Manual_Clean)
            {
                Mode = TRunCleanJigMode.Clean;
                return;
            }
        }
        
        #region Running Func
        protected void Running_Cleaning()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    if(!cDEF.Work.Project.GlobalOption.UseCleanJig)
                    {
                        cDEF.Tact.CleanJig.GetTact();
                        Information.IndexData.Status = eLensIndexStatus.CleanJigFinish;
                        Mode = TRunCleanJigMode.Stop;
                        return;
                    }
                    CleanJigDown.Forward();
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Clean Jig Cylinder Down.", true);
                    Step++;
                    break;

                case 1:
                    cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum] = true;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Vacuum On.", true);
                    cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow] = true;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Blow On.", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < cDEF.Work.CleanJig.CleanTime)
                        break;
                    Step++;
                    break;

                case 3:
                    cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum] = false;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Vacuum Off.", true);
                    cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow] = false;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, "[Clean Jig] Vacuum Off.", true);
                    CleanJigDown.Backward();
                    Step++;
                    break;

                case 4:
                    Information.IndexData.Status = eLensIndexStatus.CleanJigFinish;
                    Step++;
                    break;

                case 5:
                    cDEF.Tact.CleanJig.GetTact();
                    if (cDEF.Run.Mode == TRunMode.Manual_Clean)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunCleanJigMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.CleanJig, $"[Clean Jig] End - Cycle Time : [{cDEF.Tact.CleanJig.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        
        
        #endregion

   
    }
}

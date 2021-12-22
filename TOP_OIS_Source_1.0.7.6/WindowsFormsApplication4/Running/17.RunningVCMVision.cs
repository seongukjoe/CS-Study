using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using XModule.Standard;
using XModule.Datas;
using XModule.Working;
using XModule.Unit;

namespace XModule.Running
{

    public class RunVCMVisionInformation : fpObject
    {

        #region 변수
        private bool FInspectFinish = false;

        public int VisionResultX = 0;
        public int VisionResultY = 0;
        public int VisionResultT = 0;
        #endregion

        #region Property

        public bool InspectFinish
        {
            get { return FInspectFinish; }
            set
            {
                if(FInspectFinish != value)
                {
                    FInspectFinish = value;
                    Change();
                }
            }
        }
        


        #endregion

        public RunVCMVisionInformation() : base()
        {
            Clear(true);
            RecoveryOpen();
        }

        string Recovery_Path = Application.StartupPath + "\\Recovery\\RunVCMVision.dat";
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
            FInspectFinish = false;
            Unlock(Ignore);
        }
    }
    public enum TRunVCMVisionMode
    {
        Stop,
        Inspect,                  //  Vision Inspect
        Ready,
    };

    
    public class RunVCMVision : TfpRunningModule 
    {
        private RunVCMVisionInformation FInformation;
        private TRunVCMVisionMode FMode;

        public int FCalc;

        public RunVCMVision(fpRunning Owner, String Name, int MessageCode)
            : base(Owner,  Name, MessageCode)
        {
            FInformation = new RunVCMVisionInformation();
        }
        

        #region **Property**
        public RunVCMVisionInformation Information
        {
            get { return FInformation; }
        }

        public TRunVCMVisionMode Mode
        {
            get { return GetMode(); }
            set { SetMode(value); }
        }

        // Motor
        public TfpMotionItem VCMVision
        {
            get { return GetMotions(0); }
        }
        #endregion //Property//

        private TRunVCMVisionMode GetMode()
        {
            if (DetailMode == TfpRunningModuleMode.fmmRun)
                return FMode;
            else
                return TRunVCMVisionMode.Stop;
        }
        private void SetMode(TRunVCMVisionMode Value)
        {
            if (Value == TRunVCMVisionMode.Stop)
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
                case TRunVCMVisionMode.Stop:
                    return "Stop";
                case TRunVCMVisionMode.Inspect:
                    return "VCM Inspect";
                case TRunVCMVisionMode.Ready:
                    return "VCM Ready";

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
                    if (!cDEF.Run.LensPicker.HeadX.HomeEnd)
                        break;
                    if (!cDEF.Run.LensPicker.HeadY.HomeEnd)
                        break;
                    VCMVision.Home();
                    Step++;
                    break;
                case 1:
                    if (!VCMVision.HomeEnd)
                        return false;
                    Step++;
                    break;
                case 2:
                    Step++;
                    break;
                case 3:
                    Move_VCMVision_WorkPosition();
                    Step++;
                    break;
                case 4:
                    Information.InspectFinish = false;
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
                case TRunVCMVisionMode.Inspect:
                    Running_VCMInspect();
                    break;

                case TRunVCMVisionMode.Ready:
                    Running_Ready();
                    break;
                
            }
        }
        
        protected override void ProcMain()
        {
            if(cDEF.Run.Mode == TRunMode.Main_Run)
            {
                if(!Information.InspectFinish && cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.Act3Finish)
                {
                    cDEF.Tact.TopVision.Start();
                    Mode = TRunVCMVisionMode.Inspect;
                    cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Start.", true);
                    return;
                }

            }

            if(cDEF.Run.Mode == TRunMode.Manual_TopVisionCheck)
            {
                cDEF.Tact.TopVision.Start();
                Mode = TRunVCMVisionMode.Inspect;
                cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision - Manual] Start.", true); 
                return;
            }
            
        }

        #region Running Func
        private int InspRetryCount = 0;
        private int ReadyRetryCount = 0;

        protected void Running_VCMInspect()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 1:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.LensPicker.HeadX.ActualPosition <= cDEF.Work.TeachLensPicker.BottomCamPositionX)
                        {
                            Move_VCMVision_WorkPosition();
                            cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Move VCM Vision Work Position.", true);
                            FCalc = Environment.TickCount;
                            Step++;
                        }
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunVCMVision + 200;
                        //cDEF.Run.LogWarning(cLog.RunVCMVision + 200, "[VCM VISION] Lens Picker Move Avoid Position Time Out");
                        cDEF.Run.LogWarning(cLog.RunVCMVision + 200, "");
                        cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Lens Picker Move Avoid Position Time Out.", true);
                        Mode = TRunVCMVisionMode.Stop;
                        return;
                    }
                    break;

                case 2:
                    if (Environment.TickCount - FCalc < cDEF.Work.VCMVision.VCMVisionGrabDelay)
                        break;
                    if(!cDEF.Work.Project.GlobalOption.VisionCheck)
                    {
                        Step = 6;
                        return;
                    }
                    cDEF.Visions.Sendmsg(eVision.V1_Ready);
                    cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Send Vision V1 Ready (Inspect).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 3:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV1_Ready.Status != CmmStatus.Ok)
                            break;
                        cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Receive Vision V1 Ready OK (Inspect).", true);
                        Step++;
                    }
                    else
                    {
                        if (ReadyRetryCount < cDEF.Work.Recipe.VCMVISIONRetryCount)
                        {
                            ReadyRetryCount++;
                            Step = 2;
                            break;
                        }
                        cDEF.Run.SetAlarmID = cLog.RunVCMVision + 201;
                        //cDEF.Run.LogWarning(cLog.RunVCMVision + 201, "[VCM VISION] (Inspect) V1 Ready Vision TimeOut");
                        cDEF.Run.LogWarning(cLog.RunVCMVision + 201, "");
                        cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] (Inspect) V1 Ready Vision Time Out.", true);
                        Mode = TRunVCMVisionMode.Stop;
                        return;
                    }
                    break;

                case 4:
                    cDEF.Visions.Sendmsg(eVision.V1_Complete);
                    cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Send Vision V1 Complete (Inspect).", true);
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 5:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Visions.ackV1_Complete.Status != CmmStatus.Ok)
                            break;
                        //Point result = CalcAngle((int)(cDEF.Visions.ackV1_Complete.x), (int)(cDEF.Visions.ackV1_Complete.y));
                        if (!cDEF.Visions.ackV1_Complete.exist)
                        {
                            if (InspRetryCount < cDEF.Work.Recipe.VCMVISIONRetryCount)
                            {
                                InspRetryCount++;
                                ReadyRetryCount = 0;
                                Step = 2;
                                break;
                            }
                            cDEF.Run.SetAlarmID = cLog.RunVCMVision + 202;
                            //cDEF.Run.LogWarning(cLog.RunVCMVision + 202, "[VCM VISION] VISION NG!");
                            cDEF.Run.LogWarning(cLog.RunVCMVision + 202, "");
                            cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Vision NG.", true);
                            Mode = TRunVCMVisionMode.Stop;
                            return;
                        }
                        Information.VisionResultX = (int)(cDEF.Visions.ackV1_Complete.x);//result.X;
                        Information.VisionResultY = (int)(cDEF.Visions.ackV1_Complete.y);//result.Y;
                        cDEF.TaskLogAppend(TaskLog.Index, $"[VCM Vision] Receive Vision V1 Complete OK (Inspect). X : {Information.VisionResultX} Y : {Information.VisionResultY}", true);
                        Step++;
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunVCMVision + 203;
                        cDEF.Run.LogWarning(cLog.RunVCMVision + 203, "[VCM VISION] (Inspect) V1 Complete Vision TimeOut");
                        cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] (Inspect) V1 Complete Vision Time Out.", true);
                        Mode = TRunVCMVisionMode.Stop;
                        return;
                    }
                    break;

                case 6:
                    Move_VCMVision_ReadyPosition();
                    cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Move VCM Vision Ready Position.", true);
                    Step++;
                    break;

                case 7:
                    cDEF.Tact.TopVision.GetTact();
                    Information.InspectFinish = true;
                    if (cDEF.Run.Mode == TRunMode.Manual_TopVisionCheck)
                        cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunVCMVisionMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Lens, $"[VCM Vision] End - Cycle Time : [{cDEF.Tact.TopVision.CycleTime.ToString("N3")}].", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        protected void Running_Ready()
        {
            if (!IsReady())
                return;

            switch (Step)
            {
                case 0:
                    FCalc = Environment.TickCount;
                    Step++;
                    break;

                case 1:
                    if (Environment.TickCount - FCalc < 10000)
                    {
                        if (cDEF.Run.LensPicker.HeadX.ActualPosition <= cDEF.Work.TeachLensPicker.BottomCamPositionX)
                        {
                            Move_VCMVision_WorkPosition();
                            cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Move VCM Vision Work Position.", true);
                            Step++;
                        }
                    }
                    else
                    {
                        cDEF.Run.SetAlarmID = cLog.RunVCMVision + 200;
                        //cDEF.Run.LogWarning(cLog.RunVCMVision + 200, "[VCM VISION] Lens Picker Move Avoid Position Time Out");
                        cDEF.Run.LogWarning(cLog.RunVCMVision + 200, "");
                        cDEF.TaskLogAppend(TaskLog.Index, "[VCM Vision] Lens Picker Move Avoid Position Time Out.", true);
                        Mode = TRunVCMVisionMode.Stop;
                        return;
                    }
                    break;

                case 2:
                    Information.InspectFinish = false;
                    //if (cDEF.Run.Mode == TRunMode.Manual_ReadBCR)
                    //    cDEF.Run.DetailMode = TfpRunningMode.frmToStop;
                    Mode = TRunVCMVisionMode.Stop;
                    cDEF.TaskLogAppend(TaskLog.Lens, "[VCM Vision] End.", true);
                    break;

                default:
                    throw new Exception(" => Mode: " + ModeToString() + ", Step: " + (Step).ToString());
            }
            return;
        }
        public double getAngle(int ax, int ay, int bx, int by)
        {
            double dy = by - ay;
            double dx = bx - ax;
            double angle = Math.Atan(dy / dx) * (180.0 / Math.PI);

            if (dx < 0.0)
            {
                angle += 180.0;
            }
            else
            {
                //if (dy < 0.0) angle += 360.0;
            }

            return angle;
        }
        public double GetDistance(int ax, int ay, int bx, int by)
        {

            double dy = by - ay;
            double dx = bx - ax;

            return Math.Sqrt(dx * dx + dy * dy);

        }
        public Point GetCirclePoint(int ax, int ay, double radius, double Angle)
        {
            Point target = new Point();
            double radian = Math.PI * Angle / 180.0;
            target.X = ax + (int)((radius * Math.Cos(radian)));
            target.Y = ay + (int)((radius * Math.Sin(radian)));

            return target;
        }
        public Point CalcAngle(int AngleX, int AngleY)
        {
            double abAngle = 0;

            //  1. 해당 위치의 각도를 구한다.
            abAngle = getAngle(0, 0, AngleX, AngleY);
            //abAngle = 90 - abAngle
            //// 2. 60도 튼다.
            abAngle += 60 - AngleX / 600;

            double radius = 0;
            // 3. 이상적인 상태에서 해당 위치 와의 거리를 구한다.
            radius = GetDistance(0, 0, AngleX, AngleY);

            //  4. leftBottom 를 기준으로 2 의 거리와 4 의 각도를 갖는 위치를 계산한다.
            Point destPoint = GetCirclePoint(0, 0, radius, abAngle);

            return destPoint;

        }
        #endregion
        #region Move Command
        public void Move_VCMVision_ReadyPosition()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMVision.VCMVisionMovingDelay;
            VCMVision.Absolute(cDEF.Work.TeachVCMVision.VCMVisionReadyPosition, Sleep);
        }
        public void Move_VCMVision_WorkPosition()
        {
            int Sleep;
            Sleep = cDEF.Work.VCMVision.VCMVisionMovingDelay;
            VCMVision.Absolute(cDEF.Work.TeachVCMVision.VCMVisionWorkPosition, Sleep);
        }
        #endregion

        #region CheckPosition
        public bool Is_VCMVision_ReadyPosition()
        {
            if (IsRange((double)cDEF.Work.TeachVCMVision.VCMVisionReadyPosition, VCMVision.ActualPosition))
                return true;
            else
                return false;
        }
        public bool Is_VCMVision_WorkPosition()
        {
            if (IsRange((double)cDEF.Work.TeachVCMVision.VCMVisionWorkPosition, VCMVision.ActualPosition))
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

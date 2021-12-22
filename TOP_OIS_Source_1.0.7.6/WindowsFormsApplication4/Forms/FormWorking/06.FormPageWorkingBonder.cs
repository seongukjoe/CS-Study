using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using XModule.Standard;
using System.Threading;

namespace XModule.Forms.FormWorking
{
    public partial class FormPageWorkingBonder : TFrame
    {
        #region 변수
        bool FJogMouseDown;                         //마우스 다운
        double Bonder1_HeadX_Negative;
        double Bonder1_HeadX_Positive;

        double Bonder1_HeadY_Negative;
        double Bonder1_HeadY_Positive;

        double Bonder1_HeadZ_Negative;
        double Bonder1_HeadZ_Positive;

        double Bonder2_HeadX_Negative;
        double Bonder2_HeadX_Positive;

        double Bonder2_HeadY_Negative;
        double Bonder2_HeadY_Positive;

        double Bonder2_HeadZ_Negative;
        double Bonder2_HeadZ_Positive;

        double RelativePosition = 0.0;              //RelativePosition Value
        bool SelectJogRelative;                     //Jog-Relative 토글
        int FSpeedLevel;                            //Speed

        #endregion

        public enum eGridValue
        {
            Bonder1_JettingCount,
            Bonder1_JettingTime,
            Bonder1_IdleTime,
            Space1,
            Bonder2_JettingCount,
            Bonder2_JettingTime,
            Bonder2_IdleTime,
            Space2,
            Bonder1_MovingDelayX,
            Bonder1_MovingDelayY,
            Bonder1_MovingDelayZ,
            Space3,
            Bonder1_GrabDelay,
            Space4,
            Bonder2_MovingDelayX,
            Bonder2_MovingDelayY,
            Bonder2_MovingDelayZ,
            Space5,
            Bonder2_GrabDelay,
            Space6,
            Bonder1_ReadyPosX,
            Bonder1_ReadyPosY,
            Bonder1_ReadyPosZ,
            Space7,
            Bonder1_CamPosX,
            Bonder1_CamPosY,
            Bonder1_CamPosZ,
            Space8,
            Bonder1_IdlePosX,
            Bonder1_IdlePosY,
            Bonder1_IdlePosZ,
            Space9,
            Bonder1_SamplePosX,
            Bonder1_SamplePosY,
            Bonder1_SamplePosZ,
            Bonder1_SampleGapPosX,
            Bonder1_SampleGapPosY,
            Space10,
            Bonder1_JettingPosZ,
            Space11,
            Bonder1_CameraDistanceOffsetX,
            Bonder1_CameraDistanceOffsetY,
            Space12,
            Bonder1_TouchPosX,
            Bonder1_TouchPosY,
            Bonder1_TouchPosZ,
            Bonder1_TouchStep,
            Bonder1_TouchLimit,
            Bonder1_TouchOffset,
            Space13,
            Bonder1_AvoidPosX,
            Bonder1_AvoidPosY,
            Space14,
            Bonder1_CleanPosX,  //Grid
            Bonder1_CleanPosY,
            Bonder1_CleanPosZ,
            Space15,
            Bonder2_ReadyPosX,
            Bonder2_ReadyPosY,
            Bonder2_ReadyPosZ,
            Space16,
            Bonder2_CamPosX,
            Bonder2_CamPosY,
            Bonder2_CamPosZ,
            Space17,
            Bonder2_IdlePosX,
            Bonder2_IdlePosY,
            Bonder2_IdlePosZ,
            Space18,
            Bonder2_SamplePosX,
            Bonder2_SamplePosY,
            Bonder2_SamplePosZ,
            Bonder2_SampleGapPosX,
            Bonder2_SampleGapPosY,
            Space19,
            Bonder2_JettingPosZ,
            Space20,
            Bonder2_CameraDistanceOffsetX,
            Bonder2_CameraDistanceOffsetY,
            Space21,
            Bonder2_TouchPosX,
            Bonder2_TouchPosY,
            Bonder2_TouchPosZ,
            Bonder2_TouchStep,
            Bonder2_TouchLimit,
            Bonder2_TouchOffset,
            Space22,
            Bonder2_AvoidPosX,
            Bonder2_AvoidPosY,
            Space23,
            Bonder2_CleanPosX,  //Grid
            Bonder2_CleanPosY,
            Bonder2_CleanPosZ,
            Space24,
            Bonder1_TipCleanStartPosX,
            Bonder1_TipCleanStartPosY,
            Bonder1_TipCleanPosZ,
            Bonder1_TipCleanCntY,
            Bonder1_TipCleanPitchY,
            Space25,
            Bonder2_TipCleanStartPosX,
            Bonder2_TipCleanStartPosY,
            Bonder2_TipCleanPosZ,
            Bonder2_TipCleanCntY,
            Bonder2_TipCleanPitchY,
            Space26,
            Bonder1_GapOffsetZ,
            Bonder1_GapOffsetLimitZ,
            Bonder1_GapMeasureDelay,
            Bonder1_GapDistanceX,
            Bonder1_GapDistanceY,
            Bonder1_GapTouchPosX,
            Bonder1_GapTouchPosY,
            Bonder1_GapAdjustX,
            Bonder1_GapAdjustY,
            Bonder1_GapMeasureZ,
            Space27,
            Bonder2_GapOffsetZ,
            Bonder2_GapOffsetLimitZ,
            Bonder2_GapMeasureDelay,
            Bonder2_GapDistanceX,
            Bonder2_GapDistanceY,
            Bonder2_GapTouchPosX,
            Bonder2_GapTouchPosY,
            Bonder2_GapAdjustX,
            Bonder2_GapAdjustY,
            Bonder2_GapMeasureZ,
        }

        private enum eAxis
        {
            BonderX,
            BonderY,
            BonderZ,
        }


        public FormPageWorkingBonder()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 1000);
        }
        private void FormPageWorkingBonder_Load(object sender, EventArgs e)
        {
            Left = 131;
            Top = 60;
            Grid_Init();
            Grid_Update();
    
            FSpeedLevel = 0;
            FJogMouseDown = true;
            SelectJogRelative = true;

            Jetting1GridInit();
            Jetting2GridInit();

            if (cDEF.Work.DispSensor.DispenserType == 2)
            {
                Jetting1GridUpdate();
                Jetting2GridUpdate();
            }

            cDEF.Dispenser1.Recivemsg_Event += Dispenser1_Recivemsg_Event;
            cDEF.DispenserEcm1.Recivemsg_Event += Dispenser1_Recivemsg_Event;

            cDEF.Dispenser2.Recivemsg_Event += Dispenser1_Recivemsg_Event;
            cDEF.DispenserEcm2.Recivemsg_Event += Dispenser1_Recivemsg_Event;

            cDEF.Run.Bonder1.OnGrid_Display += Bonder1_OnGrid_Display;
            cDEF.Run.Bonder2.OnGrid_Display += Bonder1_OnGrid_Display;
        }

        private void Bonder1_OnGrid_Display()
        {
            Grid_Update();
        }

        private void Dispenser1_Recivemsg_Event(string msg)
        {
            Jetting1GridUpdate();
            Jetting2GridUpdate();
        }

        protected override void UpdateInformation()
        {
            if (!Visible)
                return;

            this.Invoke(new Action(delegate ()
            {
                if (cDEF.Run.DetailMode == TfpRunningMode.frmRun)
                    this.Enabled = false;
                else
                    this.Enabled = true;

                btnSelectJog.Text = SelectJogRelative ? "JOG" : "RELATIVE";
                btnSelectJog.ForeColor = SelectJogRelative ? Color.Red : Color.Blue;
                lbRelative_Position.Enabled = SelectJogRelative ? false : true;


                lbBonder1X_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.Bonder1.Motions[(int)eAxis.BonderX].ActualPosition, true);
                lbBonder1Y_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.Bonder1.Motions[(int)eAxis.BonderY].ActualPosition, true);
                lbBonder1Z_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.Bonder1.Motions[(int)eAxis.BonderZ].ActualPosition, true);

                lbBonder2X_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.Bonder2.Motions[(int)eAxis.BonderX].ActualPosition, true);
                lbBonder2Y_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.Bonder2.Motions[(int)eAxis.BonderY].ActualPosition, true);
                lbBonder2Z_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.Bonder2.Motions[(int)eAxis.BonderZ].ActualPosition, true);

                lbRelative_Position.Text = cFnc.GetUnitValueString((int)RelativePosition, true);


                lbBon1AutoCalCount.Text = cDEF.Work.TeachBonder1.AutoCalCount.ToString();
                lbBon2AutoCalCount.Text = cDEF.Work.TeachBonder2.AutoCalCount.ToString();
                lbBon1AutoCalSpec.Text = (cDEF.Work.TeachBonder1.AutoCalSpec/1000.0).ToString("N3");
                lbBon2AutoCalSpec.Text = (cDEF.Work.TeachBonder2.AutoCalSpec/1000.0).ToString("N3");


                if (Convert.ToBoolean(cDEF.Run.Bonder1.HeadX.FAlarm))
                {
                    lbX1.BackColor = Color.Red;
                }
                else
                {
                    lbX1.BackColor = Convert.ToBoolean(cDEF.Run.Bonder1.HeadX.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.Bonder1.HeadY.FAlarm))
                {
                    lbY1.BackColor = Color.Red;
                }
                else
                {
                    lbY1.BackColor = Convert.ToBoolean(cDEF.Run.Bonder1.HeadY.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.Bonder1.HeadZ.FAlarm))
                {
                    lbZ1.BackColor = Color.Red;
                }
                else
                {
                    lbZ1.BackColor = Convert.ToBoolean(cDEF.Run.Bonder1.HeadZ.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.Bonder2.HeadX.FAlarm))
                {
                    lbX2.BackColor = Color.Red;
                }
                else
                {
                    lbX2.BackColor = Convert.ToBoolean(cDEF.Run.Bonder2.HeadX.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.Bonder2.HeadY.FAlarm))
                {
                    lbY2.BackColor = Color.Red;
                }
                else
                {
                    lbY2.BackColor = Convert.ToBoolean(cDEF.Run.Bonder2.HeadY.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.Bonder2.HeadZ.FAlarm))
                {
                    lbZ2.BackColor = Color.Red;
                }
                else
                {
                    lbZ2.BackColor = Convert.ToBoolean(cDEF.Run.Bonder2.HeadZ.FInposition) ? Color.White : Color.Lime;
                }

                //IO Check
                lbCleanCylinderIn1_IO.BackColor = cDEF.Run.Digital.Input[cDI.Bonding_Head_1_Nozzle_Clean_Clamp] ? Color.Orange : Color.White;
                lbCleanCylinderOut1_IO.BackColor = cDEF.Run.Digital.Input[cDI.Bonding_Head_1_Nozzle_Clean_UnClamp] ? Color.Orange : Color.White;
                lbCleanCylinderIn2_IO.BackColor = cDEF.Run.Digital.Input[cDI.Bonding_Head_2_Nozzle_Clean_Clamp] ? Color.Orange : Color.White;
                lbCleanCylinderOut2_IO.BackColor = cDEF.Run.Digital.Input[cDI.Bonding_Head_2_Nozzle_Clean_UnClamp] ? Color.Orange : Color.White;
                lbHeightTouchSensor1_IO.BackColor = cDEF.Run.Digital.Input[cDI.Bonding_Head_1_Nozzle_Height_Touch_Sensor] ? Color.Orange : Color.White;
                lbHeightTouchSensor2_IO.BackColor = cDEF.Run.Digital.Input[cDI.Bonding_Head_2_Nozzle_Height_Touch_Sensor] ? Color.Orange : Color.White;

                lbCleanCylinderIn1.BackColor = cDEF.Run.Bonder1.TipClean.IsForward() ? Color.Lime : Color.White;
                lbCleanCylinderOut1.BackColor = cDEF.Run.Bonder1.TipClean.IsBackward() ? Color.Lime : Color.White;
                lbCleanCylinderIn2.BackColor = cDEF.Run.Bonder2.TipClean.IsForward() ? Color.Lime : Color.White;
                lbCleanCylinderOut2.BackColor = cDEF.Run.Bonder2.TipClean.IsBackward() ? Color.Lime : Color.White;

                //Position Check
                //Bonder #1
                lbBonder1ReadyZ.BackColor = cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionZ() ? Color.Lime : Color.White;
                lbBonder1JettingZ.BackColor = cDEF.Run.Bonder1.Is_Bonder1_JettingPositionZ() ? Color.Lime : Color.White;
                lbBonder1CameraCenterXY.BackColor = cDEF.Run.Bonder1.Is_Bonder1_CamCenterX() && cDEF.Run.Bonder1.Is_Bonder1_CamCenterY() ? Color.Lime : Color.White;
                lbBonder1CameraZ.BackColor = cDEF.Run.Bonder1.Is_Bonder1_CamPositionZ() ? Color.Lime : Color.White;
                lbBonder1ReadyX.BackColor = cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionX() ? Color.Lime : Color.White;
                lbBonder1ReadyY.BackColor = cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionY() ? Color.Lime : Color.White;

                lbBonder1CleanZ.BackColor = cDEF.Run.Bonder1.Is_Bonder1_CleanPositionZ() ? Color.Lime : Color.White;
                lbBonder1CleanXY.BackColor = cDEF.Run.Bonder1.Is_Bonder1_CleanPositionX() ? Color.Lime : Color.White;
                lbBonder1CleanXY.BackColor = cDEF.Run.Bonder1.Is_Bonder1_CleanPositionY() ? Color.Lime : Color.White;

                //Bonder #2
                lbBonder2ReadyZ.BackColor = cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionZ() ? Color.Lime : Color.White;
                lbBonder2JettingZ.BackColor = cDEF.Run.Bonder2.Is_Bonder2_JettingPositionZ() ? Color.Lime : Color.White;
                lbBonder2ReadyX.BackColor = cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionX() ? Color.Lime : Color.White;
                lbBonder2ReadyY.BackColor = cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionY() ? Color.Lime : Color.White;
                lbBonder2CameraCenterXY.BackColor = cDEF.Run.Bonder2.Is_Bonder2_CamCenterX() && cDEF.Run.Bonder2.Is_Bonder2_CamCenterY() ? Color.Lime : Color.White;
                lbBonder2CameraZ.BackColor = cDEF.Run.Bonder2.Is_Bonder2_CamPositionZ() ? Color.Lime : Color.White;

                lbBonder2CleanZ.BackColor = cDEF.Run.Bonder2.Is_Bonder2_CleanPositionZ() ? Color.Lime : Color.White;
                lbBonder2CleanXY.BackColor = cDEF.Run.Bonder2.Is_Bonder2_CleanPositionX() ? Color.Lime : Color.White;
                lbBonder2CleanXY.BackColor = cDEF.Run.Bonder2.Is_Bonder2_CleanPositionY() ? Color.Lime : Color.White;

                lbMoveGapPositionXY1.BackColor = cDEF.Run.Bonder1.Is_Bonder1_GapAdjustXY() ? Color.Lime : Color.White;
                lbMoveGapPositionZ1.BackColor = cDEF.Run.Bonder1.Is_Bonder1_GapMeasureZ() ? Color.Lime : Color.White;

                lbMoveGapPositionXY2.BackColor = cDEF.Run.Bonder2.Is_Bonder2_GapAdjustXY() ? Color.Lime : Color.White;
                lbMoveGapPositionZ2.BackColor = cDEF.Run.Bonder2.Is_Bonder2_GapMeasureZ() ? Color.Lime : Color.White;


                //Calc Bonder-Camera Distance

                lbCheckJettingIO1.BackColor = cDEF.Run.Bonder1.JettingIO ? Color.Lime : Color.White;
                lbCheckJettingIO2.BackColor = cDEF.Run.Bonder2.JettingIO ? Color.Lime : Color.White;

                if (cDEF.Work.DispSensor.DispenserType == 0)
                {
                    lbDisplaceValueBond1.Visible = false;
                    lbDisplaceValueBond2.Visible = false;
                    lbBond1DispTitle.Visible = false;
                    lbBond2DispTitle.Visible = false;
                    lbBond1ResultDispTitle.Visible = false;
                    lbBond2ResultDispTitle.Visible = false;
                    lbResultGapValue1.Visible = false;
                    lbResultGapValue2.Visible = false;
                    lbMoveGapPositionXY1.Visible = false;
                    lbMoveGapPositionZ1.Visible = false;
                    btnMoveGapPositionXY1.Visible = false;
                    btnMoveGapPositionZ1.Visible = false;
                    btnGapMeasure1.Visible = false;
                    lbMoveGapPositionXY2.Visible = false;
                    lbMoveGapPositionZ2.Visible = false;
                    btnMoveGapPositionXY2.Visible = false;
                    btnMoveGapPositionZ2.Visible = false;
                    btnGapMeasure2.Visible = false;
                    btnTouch1.Visible = false;
                    btnTouch2.Visible = false;
                    btnGapAdjust1.Visible = false;
                    btnGapAdjust2.Visible = false;

                    cDEF.Dispenser1.Send_ParamRead();
                    cDEF.Dispenser2.Send_ParamRead();
                }
                else
                {

                    if (cDEF.Serials.strValue_Bond1 == "ERR")
                    {
                        lbDisplaceValueBond1.Text = "ERR";
                        lbDisplaceValueBond1.ColorLight = Color.Red;
                    }
                    else
                    {
                        lbDisplaceValueBond1.Text = (cDEF.Serials.Value_Bond1 / 1000.0).ToString("N3");
                        lbDisplaceValueBond1.ColorLight = Color.Lime;
                    }

                    if (cDEF.Serials.strValue_Bond2 == "ERR")
                    {
                        lbDisplaceValueBond2.Text = "ERR";
                        lbDisplaceValueBond2.ColorLight = Color.Red;
                    }
                    else
                    {
                        lbDisplaceValueBond2.Text = (cDEF.Serials.Value_Bond2 / 1000.0).ToString("N3");
                        lbDisplaceValueBond2.ColorLight = Color.Lime;
                    }

                    lbResultGapValue1.Text = (cDEF.Run.Bonder1.Information.GapMeasureValue / 1000.0).ToString("N3");
                    lbResultGapValue2.Text = (cDEF.Run.Bonder2.Information.GapMeasureValue / 1000.0).ToString("N3");
                }


                
            }));
        }


        #region GridUpdate
        private void Grid_Init()
        {
            
            GridAdd("R", "Bonder #1 Jetting Count", "Bonder #1");
            GridAdd("R", "Bonder #1 Jetting Time", "Time");
            GridAdd("R", "Bonder #1 Idle Time", "Time");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 JEtting Count", "Bonder #2");
            GridAdd("R", "Bonder #2 Jetting Time", "Time");
            GridAdd("R", "Bonder #2 Idle Time", "Time");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Moving Delay X", "Bonder #1");
            GridAdd("R", "Bonder #1 Moving Delay Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Moving Delay Z", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Grab Delay", "Time");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Moving Delay X", "Bonder #2");
            GridAdd("R", "Bonder #2 Moving Delay Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Moving Delay Z", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Grab Delay", "Time");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Ready Position X", "Bonder #1");
            GridAdd("R", "Bonder #1 Ready Position Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Ready Position Z", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Camera Center Position X", "Bonder #1");
            GridAdd("R", "Bonder #1 Camera Center Position Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Camera Center Position Z", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Idle Position X", "Bonder #1");
            GridAdd("R", "Bonder #1 Idle Position Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Idle Position Z", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Sample Position X", "Bonder #1");
            GridAdd("R", "Bonder #1 Sample Position Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Sample Position Z", "Bonder #1");
            GridAdd("R", "Bonder #1 Sample Gap Position X", "Bonder #1");
            GridAdd("R", "Bonder #1 Sample Gap Position Y", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Jetting Position Z", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Camera Distance Offset X", "Bonder #1");
            GridAdd("R", "Bonder #1 Camera Distance Offset Y", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Touch Position X", "Bonder #1");
            GridAdd("R", "Bonder #1 Touch Position Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Touch Position Z", "Bonder #1");
            GridAdd("R", "Bonder #1 Touch Step", "Bonder #1");
            GridAdd("R", "Bonder #1 Touch Limit Z", "Bonder #1");
            GridAdd("R", "Bonder #1 Touch Offset Z", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Avoid Position X", "Bonder #1");
            GridAdd("R", "Bonder #1 Avoid Position Y", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Clean Position X", "Bonder #1"); //Position Grid, Next update
            GridAdd("R", "Bonder #1 Clean Position Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Clean Position Z", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Ready Position X", "Bonder #2");
            GridAdd("R", "Bonder #2 Ready Position Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Ready Position Z", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Camera Center Position X", "Bonder #2");
            GridAdd("R", "Bonder #2 Camera Center Position Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Camera Center Position Z", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Idle Position X", "Bonder #2");
            GridAdd("R", "Bonder #2 Idle Position Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Idle Position Z", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Sample Position X", "Bonder #2");
            GridAdd("R", "Bonder #2 Sample Position Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Sample Position Z", "Bonder #2");
            GridAdd("R", "Bonder #2 Sample Gap Position X", "Bonder #2");
            GridAdd("R", "Bonder #2 Sample Gap Position Y", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Jetting Position Z", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Camera Distance Offset X", "Bonder #2");
            GridAdd("R", "Bonder #2 Camera Distance Offset Y", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Touch Position X", "Bonder #2");
            GridAdd("R", "Bonder #2 Touch Position Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Touch Position Z", "Bonder #2");
            GridAdd("R", "Bonder #2 Touch Step", "Bonder #2");
            GridAdd("R", "Bonder #2 Touch Limit Z", "Bonder #2");
            GridAdd("R", "Bonder #2 Touch Offset Z", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Avoid Position X", "Bonder #2");
            GridAdd("R", "Bonder #2 Avoid Position Y", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Clean Position X", "Bonder #2"); //Position Grid, Next update
            GridAdd("R", "Bonder #2 Clean Position Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Clean Position Z", "Bonder #2");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Tip Clean Start Position X", "Bonder #1");
            GridAdd("R", "Bonder #1 Tip Clean Start Position Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Tip Clean Position Z", "Bonder #1");
            GridAdd("R", "Bonder #1 Tip Clean Count Position Y", "Count");
            GridAdd("R", "Bonder #1 Tip Clean Pitch Position Y", "Pitch");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Tip Clean Start Position X", "Bonder #2");
            GridAdd("R", "Bonder #2 Tip Clean Start Position Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Tip Clean Position Z", "Bonder #2");
            GridAdd("R", "Bonder #2 Tip Clean Count Position Y", "Count");
            GridAdd("R", "Bonder #2 Tip Clean Pitch Position Y", "Pitch");
            GridAdd_Space();
            GridAdd("R", "Bonder #1 Gap Offset Z", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Offset Limit Z", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Measure Delay", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Distance X", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Distance Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Touch X", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Touch Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Adjust X", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Adjust Y", "Bonder #1");
            GridAdd("R", "Bonder #1 Gap Measure Z", "Bonder #1");
            GridAdd_Space();
            GridAdd("R", "Bonder #2 Gap Offset Z", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Offset Limit Z", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Measure Delay", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Distance X", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Distance Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Touch X", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Touch Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Adjust X", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Adjust Y", "Bonder #2");
            GridAdd("R", "Bonder #2 Gap Measure Z", "Bonder #2");
        }
        private void Grid_Update()
        {
            for (int i = 0; i < MotionDataGrid.RowCount; i++)
            {
                switch((eGridValue)i)
                {
                    case eGridValue.Bonder1_ReadyPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_ReadyPosX].Value = ((double) cDEF.Work.TeachBonder1.ReadyPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_ReadyPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_ReadyPosY].Value = ((double)cDEF.Work.TeachBonder1.ReadyPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_ReadyPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_ReadyPosZ].Value = ((double)cDEF.Work.TeachBonder1.ReadyPositionZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_CleanPosX:   //Display, Next 
                        MotionDataGrid[3, (int)eGridValue.Bonder1_CleanPosX].Value = ((double)cDEF.Work.TeachBonder1.CleanPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_CleanPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_CleanPosY].Value = ((double)cDEF.Work.TeachBonder1.CleanPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_CleanPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_CleanPosZ].Value = ((double)cDEF.Work.TeachBonder1.CleanPositionZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_CleanPosX:   //Display, Next 
                        MotionDataGrid[3, (int)eGridValue.Bonder2_CleanPosX].Value = ((double)cDEF.Work.TeachBonder2.CleanPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_CleanPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_CleanPosY].Value = ((double)cDEF.Work.TeachBonder2.CleanPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_CleanPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_CleanPosZ].Value = ((double)cDEF.Work.TeachBonder2.CleanPositionZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_ReadyPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_ReadyPosX].Value = ((double)cDEF.Work.TeachBonder2.ReadyPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_ReadyPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_ReadyPosY].Value = ((double)cDEF.Work.TeachBonder2.ReadyPositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_ReadyPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_ReadyPosZ].Value = ((double)cDEF.Work.TeachBonder2.ReadyPositionZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_CamPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_CamPosX].Value = ((double)cDEF.Work.TeachBonder1.CamPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_CamPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_CamPosY].Value = ((double)cDEF.Work.TeachBonder1.CamPositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_CamPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_CamPosZ].Value = ((double)cDEF.Work.TeachBonder1.CamPositionZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_CamPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_CamPosX].Value = ((double)cDEF.Work.TeachBonder2.CamPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_CamPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_CamPosY].Value = ((double)cDEF.Work.TeachBonder2.CamPositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_CamPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_CamPosZ].Value = ((double)cDEF.Work.TeachBonder2.CamPositionZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_JettingCount:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_JettingCount].Value = cDEF.Work.Bonder1.JettingCount.ToString();
                        break;

                    case eGridValue.Bonder2_JettingCount:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_JettingCount].Value = cDEF.Work.Bonder2.JettingCount.ToString();
                        break;

                    case eGridValue.Bonder1_CameraDistanceOffsetX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_CameraDistanceOffsetX].Value = ((double)cDEF.Work.TeachBonder1.CameraDistanceOffsetX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_CameraDistanceOffsetY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_CameraDistanceOffsetY].Value = ((double)cDEF.Work.TeachBonder1.CameraDistanceOffsetY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_CameraDistanceOffsetX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_CameraDistanceOffsetX].Value = ((double)cDEF.Work.TeachBonder2.CameraDistanceOffsetX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_CameraDistanceOffsetY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_CameraDistanceOffsetY].Value = ((double)cDEF.Work.TeachBonder2.CameraDistanceOffsetY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_MovingDelayX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_MovingDelayX].Value = ((double)cDEF.Work.Bonder1.MovingDelayX / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder1_MovingDelayY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_MovingDelayY].Value = ((double)cDEF.Work.Bonder1.MovingDelayY / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder1_MovingDelayZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_MovingDelayZ].Value = ((double)cDEF.Work.Bonder1.MovingDelayZ / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder1_GrabDelay:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GrabDelay].Value = ((double)cDEF.Work.Bonder1.Bonder1GrabDelay / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder2_MovingDelayX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_MovingDelayX].Value = ((double)cDEF.Work.Bonder2.MovingDelayX / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder2_MovingDelayY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_MovingDelayY].Value = ((double)cDEF.Work.Bonder2.MovingDelayY / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder2_MovingDelayZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_MovingDelayZ].Value = ((double)cDEF.Work.Bonder2.MovingDelayZ / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder2_GrabDelay:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GrabDelay].Value = ((double)cDEF.Work.Bonder2.Bonder2GrabDelay / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder1_JettingTime:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_JettingTime].Value = ((double)cDEF.Work.Bonder1.JettingTime / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder2_JettingTime:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_JettingTime].Value = ((double)cDEF.Work.Bonder2.JettingTime / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder1_JettingPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_JettingPosZ].Value = ((double)cDEF.Work.TeachBonder1.JettingPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                  
                    case eGridValue.Bonder2_JettingPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_JettingPosZ].Value = ((double)cDEF.Work.TeachBonder2.JettingPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                  
                    case eGridValue.Bonder1_AvoidPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_AvoidPosX].Value = ((double)cDEF.Work.TeachBonder1.AvoidPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_AvoidPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_AvoidPosY].Value = ((double)cDEF.Work.TeachBonder1.AvoidPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_AvoidPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_AvoidPosX].Value = ((double)cDEF.Work.TeachBonder2.AvoidPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_AvoidPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_AvoidPosY].Value = ((double)cDEF.Work.TeachBonder2.AvoidPositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_IdleTime:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_IdleTime].Value = ((double)cDEF.Work.Bonder1.IdleTime / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.Bonder2_IdleTime:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_IdleTime].Value = ((double)cDEF.Work.Bonder2.IdleTime / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.Bonder1_IdlePosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_IdlePosX].Value = ((double)cDEF.Work.TeachBonder1.IdlePosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_IdlePosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_IdlePosY].Value = ((double)cDEF.Work.TeachBonder1.IdlePosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_IdlePosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_IdlePosZ].Value = ((double)cDEF.Work.TeachBonder1.IdlePosZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_IdlePosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_IdlePosX].Value = ((double)cDEF.Work.TeachBonder2.IdlePosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_IdlePosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_IdlePosY].Value = ((double)cDEF.Work.TeachBonder2.IdlePosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_IdlePosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_IdlePosZ].Value = ((double)cDEF.Work.TeachBonder2.IdlePosZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_SamplePosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_SamplePosX].Value = ((double)cDEF.Work.TeachBonder1.SamplePosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_SamplePosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_SamplePosY].Value = ((double)cDEF.Work.TeachBonder1.SamplePosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_SamplePosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_SamplePosZ].Value = ((double)cDEF.Work.TeachBonder1.SamplePosZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_SampleGapPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_SampleGapPosX].Value = ((double)cDEF.Work.TeachBonder1.SampleGapPosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_SampleGapPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_SampleGapPosY].Value = ((double)cDEF.Work.TeachBonder1.SampleGapPosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_SamplePosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_SamplePosX].Value = ((double)cDEF.Work.TeachBonder2.SamplePosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_SamplePosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_SamplePosY].Value = ((double)cDEF.Work.TeachBonder2.SamplePosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_SamplePosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_SamplePosZ].Value = ((double)cDEF.Work.TeachBonder2.SamplePosZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_SampleGapPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_SampleGapPosX].Value = ((double)cDEF.Work.TeachBonder2.SampleGapPosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_SampleGapPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_SampleGapPosY].Value = ((double)cDEF.Work.TeachBonder2.SampleGapPosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_TouchPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TouchPosX].Value = ((double)cDEF.Work.TeachBonder1.TouchPosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_TouchPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TouchPosY].Value = ((double)cDEF.Work.TeachBonder1.TouchPosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_TouchPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TouchPosZ].Value = ((double)cDEF.Work.TeachBonder1.TouchPosZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_TouchStep:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TouchStep].Value = ((double)cDEF.Work.TeachBonder1.TouchStep / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_TouchLimit:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TouchLimit].Value = ((double)cDEF.Work.TeachBonder1.TouchLimitZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_TouchOffset:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TouchOffset].Value = ((double)cDEF.Work.TeachBonder1.TouchOffsetZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_TouchPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TouchPosX].Value = ((double)cDEF.Work.TeachBonder2.TouchPosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_TouchPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TouchPosY].Value = ((double)cDEF.Work.TeachBonder2.TouchPosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_TouchPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TouchPosZ].Value = ((double)cDEF.Work.TeachBonder2.TouchPosZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_TouchStep:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TouchStep].Value = ((double)cDEF.Work.TeachBonder2.TouchStep / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_TouchLimit:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TouchLimit].Value = ((double)cDEF.Work.TeachBonder2.TouchLimitZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_TouchOffset:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TouchOffset].Value = ((double)cDEF.Work.TeachBonder2.TouchOffsetZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_TipCleanStartPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TipCleanStartPosX].Value = ((double)cDEF.Work.TeachBonder1.TipCleanStartPosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_TipCleanStartPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TipCleanStartPosY].Value = ((double)cDEF.Work.TeachBonder1.TipCleanStartPosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder1_TipCleanPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TipCleanPosZ].Value = ((double)cDEF.Work.TeachBonder1.TipCleanPosZ / 1000.0).ToString("N3") + " mm";
                        break;
                    //case eGridValue.Bonder1_TipCleanCntX:
                    //    MotionDataGrid[3, (int)eGridValue.Bonder1_TipCleanCntX].Value = cDEF.Work.TeachBonder1.TipCleanCntX.ToString("N0") + " ea";
                    //    break;
                    case eGridValue.Bonder1_TipCleanCntY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TipCleanCntY].Value = cDEF.Work.TeachBonder1.TipCleanCntY.ToString("N0") + " ea";
                        break;
                    //case eGridValue.Bonder1_TipCleanPitchX:
                    //    MotionDataGrid[3, (int)eGridValue.Bonder1_TipCleanPitchX].Value = ((double)cDEF.Work.TeachBonder1.TipCleanPitchX / 1000.0).ToString("N3") + " mm";
                    //    break;
                    case eGridValue.Bonder1_TipCleanPitchY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_TipCleanPitchY].Value = ((double)cDEF.Work.TeachBonder1.TipCleanPitchY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_TipCleanStartPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TipCleanStartPosX].Value = ((double)cDEF.Work.TeachBonder2.TipCleanStartPosX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_TipCleanStartPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TipCleanStartPosY].Value = ((double)cDEF.Work.TeachBonder2.TipCleanStartPosY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.Bonder2_TipCleanPosZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TipCleanPosZ].Value = ((double)cDEF.Work.TeachBonder2.TipCleanPosZ / 1000.0).ToString("N3") + " mm";
                        break;
                    //case eGridValue.Bonder2_TipCleanCntX:
                    //    MotionDataGrid[3, (int)eGridValue.Bonder2_TipCleanCntX].Value = cDEF.Work.TeachBonder2.TipCleanCntX.ToString("N0") + " ea";
                    //    break;
                    case eGridValue.Bonder2_TipCleanCntY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TipCleanCntY].Value = cDEF.Work.TeachBonder2.TipCleanCntY.ToString("N0") + " ea";
                        break;
                    //case eGridValue.Bonder2_TipCleanPitchX:
                    //    MotionDataGrid[3, (int)eGridValue.Bonder2_TipCleanPitchX].Value = ((double)cDEF.Work.TeachBonder2.TipCleanPitchX / 1000.0).ToString("N3") + " mm";
                    //    break;
                    case eGridValue.Bonder2_TipCleanPitchY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_TipCleanPitchY].Value = ((double)cDEF.Work.TeachBonder2.TipCleanPitchY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapOffsetZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapOffsetZ].Value = ((double)cDEF.Work.Bonder1.GapOffsetZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapOffsetLimitZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapOffsetLimitZ].Value = ((double)cDEF.Work.Bonder1.GapOffsetLimitZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapMeasureDelay:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapMeasureDelay].Value = ((double)cDEF.Work.Bonder1.GapMeasureDelay / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder1_GapDistanceX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapDistanceX].Value = ((double)cDEF.Work.TeachBonder1.GapDistanceX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapDistanceY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapDistanceY].Value = ((double)cDEF.Work.TeachBonder1.GapDistanceY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapTouchPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapTouchPosX].Value = ((double)cDEF.Work.TeachBonder1.GapTouchX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapTouchPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapTouchPosY].Value = ((double)cDEF.Work.TeachBonder1.GapTouchY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapAdjustX:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapAdjustX].Value = ((double)cDEF.Work.TeachBonder1.GapAdjustX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapAdjustY:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapAdjustY].Value = ((double)cDEF.Work.TeachBonder1.GapAdjustY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder1_GapMeasureZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder1_GapMeasureZ].Value = ((double)cDEF.Work.TeachBonder1.GapMeasureZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapOffsetZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapOffsetZ].Value = ((double)cDEF.Work.Bonder2.GapOffsetZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapOffsetLimitZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapOffsetLimitZ].Value = ((double)cDEF.Work.Bonder2.GapOffsetLimitZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapMeasureDelay:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapMeasureDelay].Value = ((double)cDEF.Work.Bonder2.GapMeasureDelay / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.Bonder2_GapDistanceX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapDistanceX].Value = ((double)cDEF.Work.TeachBonder2.GapDistanceX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapDistanceY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapDistanceY].Value = ((double)cDEF.Work.TeachBonder2.GapDistanceY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapTouchPosX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapTouchPosX].Value = ((double)cDEF.Work.TeachBonder2.GapTouchX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapTouchPosY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapTouchPosY].Value = ((double)cDEF.Work.TeachBonder2.GapTouchY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapAdjustX:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapAdjustX].Value = ((double)cDEF.Work.TeachBonder2.GapAdjustX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapAdjustY:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapAdjustY].Value = ((double)cDEF.Work.TeachBonder2.GapAdjustY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.Bonder2_GapMeasureZ:
                        MotionDataGrid[3, (int)eGridValue.Bonder2_GapMeasureZ].Value = ((double)cDEF.Work.TeachBonder2.GapMeasureZ / 1000.0).ToString("N3") + " mm";
                        break;

                }
                
            }
        }
        private void GridAdd(string section, string name, string unit)
        {
            
            string[] str = { $"{section}", $"{name}", $"{unit}", $"" };
            MotionDataGrid.Rows.Add(str);
        }
        private void GridAdd_Space()
        {
            string[] str = { $"", $"", $"" };
            MotionDataGrid.Rows.Add(str);
        }
        #endregion
        private void Jetting1GridInit()
        {
            gridDp1.Rows.Clear();
            string[] str = {"",""};
            if (cDEF.Work.DispSensor.DispenserType == 0)
            {
                str[0] = "Rise Time";
                gridDp1.Rows.Add(str);
                str[0] = "Hold Time";
                gridDp1.Rows.Add(str);
                str[0] = "Fall Time";
                gridDp1.Rows.Add(str);
                str[0] = "Delay Time";
                gridDp1.Rows.Add(str);
                str[0] = "PCT";
                gridDp1.Rows.Add(str);
                str[0] = "Pulse Num";
                gridDp1.Rows.Add(str);
                str[0] = "Work Mode";
                gridDp1.Rows.Add(str);
                str[0] = "Voltage";
                gridDp1.Rows.Add(str);
            }
            else if(cDEF.Work.DispSensor.DispenserType == 1)
            {
                str[0] = "Channel";
                gridDp1.Rows.Add(str);
                str[0] = "Mode";
                gridDp1.Rows.Add(str);
                str[0] = "Dispenser Press";
                gridDp1.Rows.Add(str);
                str[0] = "Vac Press";
                gridDp1.Rows.Add(str);
            }
            else if (cDEF.Work.DispSensor.DispenserType == 2)
            { 
                str[0] = "Frequency";
                gridDp1.Rows.Add(str);
                str[0] = "Fall Time";
                gridDp1.Rows.Add(str);
                str[0] = "Open Time";
                gridDp1.Rows.Add(str);
                str[0] = "Rise Time";
                gridDp1.Rows.Add(str);
                str[0] = "init Volt";
                gridDp1.Rows.Add(str);
                str[0] = "Open Volt";
                gridDp1.Rows.Add(str);
                str[0] = "Work Mode";
                gridDp1.Rows.Add(str);
                str[0] = "Pulse Num";
                gridDp1.Rows.Add(str);
            }
        }
        private void Jetting2GridInit()
        {
            gridDp2.Rows.Clear();
            string[] str = { "", "" };
            if (cDEF.Work.DispSensor.DispenserType == 0)
            {
                str[0] = "Rise Time";
                gridDp2.Rows.Add(str);
                str[0] = "Hold Time";
                gridDp2.Rows.Add(str);
                str[0] = "Fall Time";
                gridDp2.Rows.Add(str);
                str[0] = "Delay Time";
                gridDp2.Rows.Add(str);
                str[0] = "PCT";
                gridDp2.Rows.Add(str);
                str[0] = "Pulse Num";
                gridDp2.Rows.Add(str);
                str[0] = "Work Mode";
                gridDp2.Rows.Add(str);
                str[0] = "Voltage";
                gridDp2.Rows.Add(str);
            }
            else if (cDEF.Work.DispSensor.DispenserType == 1)
            {
                str[0] = "Channel";
                gridDp2.Rows.Add(str);
                str[0] = "Mode";
                gridDp2.Rows.Add(str);
                str[0] = "Dispenser Press";
                gridDp2.Rows.Add(str);
                str[0] = "Vac Press";
                gridDp2.Rows.Add(str);
            }
            else if (cDEF.Work.DispSensor.DispenserType == 2)
            {
                str[0] = "Frequency";
                gridDp2.Rows.Add(str);
                str[0] = "Fall Time";
                gridDp2.Rows.Add(str);
                str[0] = "Open Time";
                gridDp2.Rows.Add(str);
                str[0] = "Rise Time";
                gridDp2.Rows.Add(str);
                str[0] = "init Volt";
                gridDp2.Rows.Add(str);
                str[0] = "Open Volt";
                gridDp2.Rows.Add(str);
                str[0] = "Work Mode";
                gridDp2.Rows.Add(str);      
                str[0] = "Pulse Num";
                gridDp2.Rows.Add(str);
            }
        }
        private void Jetting1GridUpdate()
        {
            if (cDEF.Work.DispSensor.DispenserType == 0)
            {
                gridDp1[1, 0].Value = cDEF.Dispenser1.RTValue.ToString("N3");
                gridDp1[1, 1].Value = (cDEF.Dispenser1.HTValue * 10.0).ToString("N3");
                gridDp1[1, 2].Value = cDEF.Dispenser1.FTValue.ToString("N3");
                gridDp1[1, 3].Value = (cDEF.Dispenser1.DelayValue * 10.0).ToString("N3");
                gridDp1[1, 4].Value = cDEF.Dispenser1.PCTValue.ToString("") + " %";
                gridDp1[1, 5].Value = cDEF.Dispenser1.PluseNumValue.ToString("");
                gridDp1[1, 6].Value = cDEF.Dispenser1.WorkModeValue.ToString();
                switch ((int)cDEF.Dispenser1.WorkModeValue)
                {
                    case 0:
                        gridDp1[1, 6].Value = "NONE";
                        break;
                    case 1:
                        gridDp1[1, 6].Value = "POINT";
                        break;
                    case 2:
                        gridDp1[1, 6].Value = "LINE";
                        break;
                    case 3:
                        gridDp1[1, 6].Value = "CLEAR";
                        break;
                }
                gridDp1[1, 7].Value = cDEF.Dispenser1.VoltageValue.ToString("");
            }
            else if (cDEF.Work.DispSensor.DispenserType == 1)
            {
                gridDp1[1, 0].Value = 1;
                if (cDEF.DispenserEcm1.SetMode == 0)
                {
                    gridDp1[1, 1].Value = "TIME";
                }
                else if (cDEF.DispenserEcm1.SetMode == 1)
                {
                    gridDp1[1, 1].Value = "MANUAL";
                }
                gridDp1[1, 2].Value = (cDEF.DispenserEcm1.PressValue / 10.0).ToString("N3");
                gridDp1[1, 3].Value = (cDEF.DispenserEcm1.VacValue / 100.0).ToString("N3");

                gridDp1[1, 0].Style.BackColor = cDEF.DispenserEcm1.CommError ? Color.Red : Color.White;
            }
            else if (cDEF.Work.DispSensor.DispenserType == 2)
            {
             

                gridDp1[1, 0].Value = cDEF.Work.Recipe.Hz_1.ToString("N0") + " Hz";
                gridDp1[1, 1].Value = cDEF.Work.Recipe.falltime_1[0].ToString("N0") + " us";
                gridDp1[1, 2].Value = cDEF.Work.Recipe.opentime_1[0].ToString("N0") + " us";
                gridDp1[1, 3].Value = cDEF.Work.Recipe.risetime_1[0].ToString("N0") + " us";
                gridDp1[1, 4].Value = cDEF.Work.Recipe.Inivolt_1.ToString("N0") + " V";
                gridDp1[1, 5].Value = cDEF.Work.Recipe.openvolt_1[0].ToString("N0") + " V";
                switch ((int)cDEF.Work.Project.GlobalOption.JettingMode1)
                {
                    case 0:
                        gridDp1[1, 6].Value = "POINT";
                        break;
                    case 1:
                        gridDp1[1, 6].Value = "LINE";
                        break;
                    case 2:
                        gridDp1[1, 6].Value = "ARC";
                        break;
                }

                gridDp1[1, 7].Value = cDEF.Work.Recipe.nDrop_1.ToString("N0") + " Shot";
            }
        }
        private void Jetting2GridUpdate()
        {
            if (cDEF.Work.DispSensor.DispenserType == 0)
            {
                gridDp2[1, 0].Value = cDEF.Dispenser2.RTValue.ToString("N3");
                gridDp2[1, 1].Value = (cDEF.Dispenser2.HTValue * 10.0).ToString("N3");
                gridDp2[1, 2].Value = cDEF.Dispenser2.FTValue.ToString("N3");
                gridDp2[1, 3].Value = (cDEF.Dispenser2.DelayValue * 10.0).ToString("N3");
                gridDp2[1, 4].Value = cDEF.Dispenser2.PCTValue.ToString("") + " %";
                gridDp2[1, 5].Value = cDEF.Dispenser2.PluseNumValue.ToString("");
                switch ((int)cDEF.Dispenser2.WorkModeValue)
                {
                    case 0:
                        gridDp2[1, 6].Value = "NONE";
                        break;
                    case 1:
                        gridDp2[1, 6].Value = "POINT";
                        break;
                    case 2:
                        gridDp2[1, 6].Value = "LINE";
                        break;
                    case 3:
                        gridDp2[1, 6].Value = "CLEAR";
                        break;
                }

                gridDp2[1, 7].Value = cDEF.Dispenser2.VoltageValue.ToString("");
            }
            else if(cDEF.Work.DispSensor.DispenserType == 1)
            {
                gridDp2[1, 0].Value = 1;
                if (cDEF.DispenserEcm2.SetMode == 0)
                {
                    gridDp2[1, 1].Value = "TIME";
                }
                else if (cDEF.DispenserEcm2.SetMode == 1)
                {
                    gridDp2[1, 1].Value = "MANUAL";
                }
                gridDp1[1, 2].Value = (cDEF.DispenserEcm1.PressValue / 10.0).ToString("N3");
                gridDp1[1, 3].Value = (cDEF.DispenserEcm1.VacValue / 100.0).ToString("N3");

                gridDp1[1, 0].Style.BackColor = cDEF.DispenserEcm1.CommError ? Color.Red : Color.White;

                //gridDp2[1, 2].Value = cDEF.DispenserEcm2.PressValue.ToString("N3") + " kPa";
                //gridDp2[1, 3].Value = cDEF.DispenserEcm2.VacValue.ToString("N3") + " kPa";

                gridDp2[1, 2].Value = (cDEF.DispenserEcm2.PressValue / 10.0).ToString("N3");
                gridDp2[1, 3].Value = (cDEF.DispenserEcm2.VacValue / 100.0).ToString("N3");

                gridDp2[1, 0].Style.BackColor = cDEF.DispenserEcm2.CommError ? Color.Red : Color.White;
            }
            else if (cDEF.Work.DispSensor.DispenserType == 2)
            {
                gridDp2[1, 0].Value = cDEF.Work.Recipe.Hz_2.ToString("N0") + " Hz";
                gridDp2[1, 1].Value = cDEF.Work.Recipe.falltime_2[0].ToString("N0") + " us";
                gridDp2[1, 2].Value = cDEF.Work.Recipe.opentime_2[0].ToString("N0") + " us";
                gridDp2[1, 3].Value = cDEF.Work.Recipe.risetime_2[0].ToString("N0") + " us";
                gridDp2[1, 4].Value = cDEF.Work.Recipe.Inivolt_2.ToString("N0") + " V";
                gridDp2[1, 5].Value = cDEF.Work.Recipe.openvolt_2[0].ToString("N0") + " V";
                switch ((int)cDEF.Work.Project.GlobalOption.JettingMode2)
                {
                    case 0:
                        gridDp2[1, 6].Value = "POINT";
                        break;
                    case 1:
                        gridDp2[1, 6].Value = "LINE";
                        break;
                    case 2:
                        gridDp2[1, 6].Value = "ARC";
                        break;               
                }

                gridDp2[1, 7].Value = cDEF.Work.Recipe.nDrop_2.ToString("N0") + " Shot";
            }
        }
        
        #region Grid_DataSetting
        private void MotionDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row;
            double DValue = 0;
            string str = "";
            int Value = 0;
            DataGridView Grid = (DataGridView)sender;


            Bonder1_HeadX_Negative = cDEF.Run.Bonder1.HeadX.Config.FLimit.FSoftwareNegative;
            Bonder1_HeadX_Positive = cDEF.Run.Bonder1.HeadX.Config.FLimit.FSoftwarePositive;

            Bonder1_HeadY_Negative = cDEF.Run.Bonder1.HeadY.Config.FLimit.FSoftwareNegative;
            Bonder1_HeadY_Positive = cDEF.Run.Bonder1.HeadY.Config.FLimit.FSoftwarePositive;

            Bonder1_HeadZ_Negative = cDEF.Run.Bonder1.HeadZ.Config.FLimit.FSoftwareNegative;
            Bonder1_HeadZ_Positive = cDEF.Run.Bonder1.HeadZ.Config.FLimit.FSoftwarePositive;

            Bonder2_HeadX_Negative = cDEF.Run.Bonder2.HeadX.Config.FLimit.FSoftwareNegative;
            Bonder2_HeadX_Positive = cDEF.Run.Bonder2.HeadX.Config.FLimit.FSoftwarePositive;

            Bonder2_HeadY_Negative = cDEF.Run.Bonder2.HeadY.Config.FLimit.FSoftwareNegative;
            Bonder2_HeadY_Positive = cDEF.Run.Bonder2.HeadY.Config.FLimit.FSoftwarePositive;

            Bonder2_HeadZ_Negative = cDEF.Run.Bonder2.HeadZ.Config.FLimit.FSoftwareNegative;
            Bonder2_HeadZ_Positive = cDEF.Run.Bonder2.HeadZ.Config.FLimit.FSoftwarePositive;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            eGridValue eGrid = (eGridValue)Row;
            if (Col == 3)
            {
                switch (eGrid)
                {
                    #region ReadyPos
                    case eGridValue.Bonder1_ReadyPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.ReadyPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Ready Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Ready Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.ReadyPositionX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.ReadyPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_ReadyPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Ready Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Ready Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.ReadyPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_ReadyPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Ready Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Ready Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.ReadyPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.ReadyPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_ReadyPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.ReadyPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Ready Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Ready Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.ReadyPositionX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.ReadyPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_ReadyPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Ready Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Ready Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.ReadyPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_ReadyPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Ready Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Ready Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.ReadyPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.ReadyPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 1, str);
                        }
                        break;
                    #endregion ReadyPos

                    #region CleanPos
                    case eGridValue.Bonder1_CleanPosX:  //Save
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.CleanPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.CleanPositionX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.CleanPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_CleanPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.CleanPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.CleanPositionY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.CleanPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_CleanPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.CleanPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.CleanPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.CleanPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_CleanPosX:  //Save
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.CleanPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Clean Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Clean Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.CleanPositionX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.CleanPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_CleanPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.CleanPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Clean Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Clean Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.CleanPositionY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.CleanPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_CleanPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.CleanPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Clean Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Clean Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.CleanPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.CleanPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    #endregion CleanPos

                    #region TipCleanPos

                    #region TipCleanPos#1
          
                    case eGridValue.Bonder1_TipCleanStartPosX:  //Save
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TipCleanStartPosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Start Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip  Start Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TipCleanStartPosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TipCleanStartPosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_TipCleanStartPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TipCleanStartPosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TipCleanStartPosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TipCleanStartPosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_TipCleanPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TipCleanPosZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TipCleanPosZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TipCleanPosZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    //case eGridValue.Bonder1_TipCleanCntX:
                    //    Value = cDEF.Work.TeachBonder1.TipCleanCntX;

                    //   if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Clean Tip Count X", ref Value, "ea", 0, 100))
                    //        return;
                    //    {
                    //        str = String.Format("[Bonder #1] Bonder #1 Clean Tip Count X {0} to {1}", cDEF.Work.TeachBonder1.TipCleanCntX, Value);
                    //        cDEF.Work.TeachBonder1.TipCleanCntX = Value;
                    //        cDEF.Work.TeachBonder1.Save();
                    //        cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                    //    }
                    //    break;
                    case eGridValue.Bonder1_TipCleanCntY:
                        Value = cDEF.Work.TeachBonder1.TipCleanCntY;

                        if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Clean Tip Count Y", ref Value, "ea", 0, 100))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip Count X {0} to {1}", cDEF.Work.TeachBonder1.TipCleanCntY, Value);
                            cDEF.Work.TeachBonder1.TipCleanCntY = Value;
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    //case eGridValue.Bonder1_TipCleanPitchX:  //Save
                    //    DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TipCleanPitchX) / 1000;
                    //    if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Pitch X", ref DValue, " mm",0,30))
                    //        return;
                    //    {
                    //        str = String.Format("[Bonder #1] Bonder #1 Clean Tip Pitch X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TipCleanPitchX / 1000.0, DValue);
                    //        cDEF.Work.TeachBonder1.TipCleanPitchX = (int)(DValue * 1000.0);
                    //        cDEF.Work.TeachBonder1.Save();
                    //        cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                    //    }
                    //    break;
                    case eGridValue.Bonder1_TipCleanPitchY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TipCleanPitchY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Pitch Y", ref DValue, " mm",0, 30))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip Pitch Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TipCleanPitchY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TipCleanPitchY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    #endregion TipCleanPos#1

                    #region TipCleanPos#2
           
                    case eGridValue.Bonder2_TipCleanStartPosX:  //Save
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TipCleanStartPosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Start Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip  Start Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TipCleanStartPosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TipCleanStartPosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_TipCleanStartPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TipCleanStartPosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TipCleanStartPosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TipCleanStartPosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_TipCleanPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TipCleanPosZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TipCleanPosZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TipCleanPosZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    //case eGridValue.Bonder2_TipCleanCntX:
                    //    Value = cDEF.Work.TeachBonder2.TipCleanCntX;

                    //    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Clean Tip Count X", ref Value, "ea", 0, 100))
                    //        return;
                    //    {
                    //        str = String.Format("[Bonder #1] Bonder #1 Clean Tip Count X {0} to {1}", cDEF.Work.TeachBonder2.TipCleanCntX, Value);
                    //        cDEF.Work.TeachBonder2.TipCleanCntX = Value;
                    //        cDEF.Work.TeachBonder2.Save();
                    //        cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                    //    }
                    //    break;
                    case eGridValue.Bonder2_TipCleanCntY:
                        Value = cDEF.Work.TeachBonder2.TipCleanCntY;

                        if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Clean Tip Count Y", ref Value, "ea", 0, 100))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip Count X {0} to {1}", cDEF.Work.TeachBonder2.TipCleanCntY, Value);
                            cDEF.Work.TeachBonder2.TipCleanCntY = Value;
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    //case eGridValue.Bonder2_TipCleanPitchX:  //Save
                    //    DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TipCleanPitchX) / 1000;
                    //    if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Pitch X", ref DValue, " mm", 0, 30))
                    //        return;
                    //    {
                    //        str = String.Format("[Bonder #1] Bonder #1 Clean Tip Pitch X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TipCleanPitchX / 1000.0, DValue);
                    //        cDEF.Work.TeachBonder2.TipCleanPitchX = (int)(DValue * 1000.0);
                    //        cDEF.Work.TeachBonder2.Save();
                    //        cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                    //    }
                    //    break;
                    case eGridValue.Bonder2_TipCleanPitchY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TipCleanPitchY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Clean Tip Pitch Y", ref DValue, " mm", 0, 30))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Clean Tip Pitch Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TipCleanPitchY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TipCleanPitchY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    #endregion TipCleanPos#2
                    #endregion TipCleanPos


                    case eGridValue.Bonder1_CamPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.CamPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Camera Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Camera Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.CamPositionX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.CamPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 28, str);
                        }
                        break;

                    case eGridValue.Bonder1_CamPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.CamPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Camera Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Camera Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.CamPositionY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.CamPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 29, str);
                        }
                        break;

                    case eGridValue.Bonder1_CamPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.CamPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Camera Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Camera Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.CamPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.CamPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 29, str);
                        }
                        break;

                    case eGridValue.Bonder2_CamPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.CamPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Camera Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #2 Camera Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.CamPositionX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.CamPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 28, str);
                        }
                        break;

                    case eGridValue.Bonder2_CamPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.CamPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Camera Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #2 Camera Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.CamPositionY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.CamPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 29, str);
                        }
                        break;

                    case eGridValue.Bonder2_CamPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.CamPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Camera Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #2 Camera Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.CamPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.CamPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 29, str);
                        }
                        break;

                    case eGridValue.Bonder1_JettingPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.JettingPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Jetting Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Jetting Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.JettingPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.JettingPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 4, str);
                        }
                        break;
                   

                    case eGridValue.Bonder2_JettingPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.JettingPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Jetting Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Jetting Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.JettingPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.JettingPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 7, str);
                        }
                        break;
                    
                    case eGridValue.Bonder1_CameraDistanceOffsetX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.CameraDistanceOffsetX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Canera Distance Offset X", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder1.HeadX.ActualPosition - cDEF.Work.TeachBonder1.SamplePosX) / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Camera Distance Offset X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.CameraDistanceOffsetX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.CameraDistanceOffsetX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 8, str);
                        }
                        break;

                    case eGridValue.Bonder1_CameraDistanceOffsetY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.CameraDistanceOffsetY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Camera Distance Offset Position Y", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder1.HeadY.ActualPosition - cDEF.Work.TeachBonder1.SamplePosY) / 1000.0, -100,100))
                            return;
                        {
                            str = String.Format("[Bonder #1] Camera Distance Offset Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.CameraDistanceOffsetY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.CameraDistanceOffsetY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 9, str);
                        }
                        break;

                    case eGridValue.Bonder2_CameraDistanceOffsetX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.CameraDistanceOffsetX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Canera Dustance Offset X", ref DValue, " mm", "CURRENT",(cDEF.Run.Bonder2.HeadX.ActualPosition - cDEF.Work.TeachBonder2.SamplePosX) / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Bonder2] Bonder #2 Camera Distance Offset X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.CameraDistanceOffsetX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.CameraDistanceOffsetX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 10, str);
                        }
                        break;

                    case eGridValue.Bonder2_CameraDistanceOffsetY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.CameraDistanceOffsetY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Camera Distance Offset Position Y", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder2.HeadY.ActualPosition - cDEF.Work.TeachBonder2.SamplePosY) / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Bonder #2] Camera Distance Offset Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.CameraDistanceOffsetY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.CameraDistanceOffsetY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 11, str);
                        }
                        break;

                    case eGridValue.Bonder1_JettingCount:
                        Value = cDEF.Work.Bonder1.JettingCount;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Jetting Count", ref Value, " ea"))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Jetting Count {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.JettingCount , Value);
                            cDEF.Work.Bonder1.JettingCount = Value;
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 12, str);
                        }
                        break;

                    case eGridValue.Bonder2_JettingCount:
                        Value = cDEF.Work.Bonder2.JettingCount;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #2 Jetting Count", ref Value, " ea"))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Jetting Count {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.JettingCount, Value);
                            cDEF.Work.Bonder2.JettingCount = Value;
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 13, str);
                        }
                        break;

                    case eGridValue.Bonder1_MovingDelayX:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.MovingDelayX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Stage Moving Delay X", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Stage Moving Delay X {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.MovingDelayX / 1000.0, DValue);
                            cDEF.Work.Bonder1.MovingDelayX = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 30, str);
                        }
                        break;

                    case eGridValue.Bonder1_MovingDelayY:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.MovingDelayY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Moving Delay Y", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Moving Delay Y {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.MovingDelayY / 1000.0, DValue);
                            cDEF.Work.Bonder1.MovingDelayY = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 31, str);
                        }
                        break;

                    case eGridValue.Bonder1_MovingDelayZ:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.MovingDelayZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Moving Delay Z", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Moving Delay Z {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.MovingDelayZ / 1000.0, DValue);
                            cDEF.Work.Bonder1.MovingDelayZ = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 32, str);
                        }
                        break;

                    case eGridValue.Bonder1_GrabDelay:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.Bonder1GrabDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Grab Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Grab Delay {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.Bonder1GrabDelay / 1000.0, DValue);
                            cDEF.Work.Bonder1.Bonder1GrabDelay = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 32, str);
                        }
                        break;

                    case eGridValue.Bonder2_MovingDelayX:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.MovingDelayX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Stage Moving Delay X", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Stage Moving Delay X {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.MovingDelayX / 1000.0, DValue);
                            cDEF.Work.Bonder2.MovingDelayX = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 33, str);
                        }
                        break;

                    case eGridValue.Bonder2_MovingDelayY:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.MovingDelayY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Moving Delay Y", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Moving Delay Y {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.MovingDelayY / 1000.0, DValue);
                            cDEF.Work.Bonder2.MovingDelayY = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 34, str);
                        }
                        break;

                    case eGridValue.Bonder2_MovingDelayZ:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.MovingDelayZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Moving Delay Z", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Moving Delay Z {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.MovingDelayZ / 1000.0, DValue);
                            cDEF.Work.Bonder2.MovingDelayZ = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 35, str);
                        }
                        break;

                    case eGridValue.Bonder2_GrabDelay:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.Bonder2GrabDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Grab Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Grab Delay {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.Bonder2GrabDelay / 1000.0, DValue);
                            cDEF.Work.Bonder2.Bonder2GrabDelay = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 35, str);
                        }
                        break;

                    case eGridValue.Bonder1_JettingTime:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.JettingTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Jetting Time", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Jetting Time {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.JettingTime / 1000.0, DValue);
                            cDEF.Work.Bonder1.JettingTime = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 36, str);
                        }
                        break;
                    case eGridValue.Bonder2_JettingTime:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.JettingTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Jetting Time", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Jetting Time {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.JettingTime / 1000.0, DValue);
                            cDEF.Work.Bonder2.JettingTime = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 37, str);
                        }
                        break;

                    case eGridValue.Bonder1_AvoidPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.AvoidPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Avoid Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Avoid Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.AvoidPositionX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.AvoidPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_AvoidPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.AvoidPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Avoid Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Avoid Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.AvoidPositionY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.AvoidPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_AvoidPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.AvoidPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Avoid Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Avoid Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.AvoidPositionX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.AvoidPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_AvoidPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.AvoidPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Avoid Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Avoid Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.AvoidPositionY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.AvoidPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_IdleTime:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.IdleTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Idle Time", ref DValue, " sec"))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Idle Time {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.IdleTime / 1000.0, DValue);
                            cDEF.Work.Bonder1.IdleTime = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 37, str);
                        }
                        break;

                    case eGridValue.Bonder2_IdleTime:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.IdleTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Idle Time", ref DValue, " sec"))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Idle Time {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.IdleTime / 1000.0, DValue);
                            cDEF.Work.Bonder2.IdleTime = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 37, str);
                        }
                        break;

                    case eGridValue.Bonder1_IdlePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.IdlePosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Idle Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Idle Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.IdlePosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.IdlePosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_IdlePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.IdlePosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Idle Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Idle Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.IdlePosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.IdlePosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_IdlePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.IdlePosZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Idle Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Idle Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.IdlePosZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.IdlePosZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_IdlePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.IdlePosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Idle Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Idle Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.IdlePosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.IdlePosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_IdlePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.IdlePosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Idle Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #2 Idle Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.IdlePosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.IdlePosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_IdlePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.IdlePosZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Idle Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #2 Idle Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.IdlePosZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.IdlePosZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_SamplePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.SamplePosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Sample Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #1 Sample Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.SamplePosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.SamplePosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_SamplePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.SamplePosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Sample Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Sample Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.SamplePosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.SamplePosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_SamplePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.SamplePosZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Sample Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Sample Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.SamplePosZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.SamplePosZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_SampleGapPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.SampleGapPosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Sample Gap Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #1 Sample Gap Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.SampleGapPosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.SampleGapPosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_SampleGapPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.SampleGapPosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Sample Gap Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #1 Sample Gap Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.SampleGapPosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.SampleGapPosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_SamplePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.SamplePosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Sample Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Sample Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.SamplePosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.SamplePosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_SamplePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.SamplePosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Sample Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #2 Sample Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.SamplePosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.SamplePosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_SamplePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.SamplePosZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Sample Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #2 Sample Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.SamplePosZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.SamplePosZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_SampleGapPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.SampleGapPosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Sample Gap Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Sample Gap Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.SampleGapPosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.SampleGapPosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_SampleGapPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.SampleGapPosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Sample Gap Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Sample Gap Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.SampleGapPosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.SampleGapPosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_TouchPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TouchPosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Touch Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #1 Touch Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TouchPosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TouchPosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_TouchPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TouchPosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Touch Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Touch Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TouchPosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TouchPosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_TouchPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TouchPosZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Touch Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Touch Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TouchPosZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TouchPosZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_TouchStep:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TouchStep) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Touch Step Z", ref DValue, " mm", 0.0, 2.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Touch Step Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TouchStep / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TouchStep = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_TouchLimit:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TouchLimitZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Touch Limit Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Touch Limit Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TouchLimitZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TouchLimitZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder1_TouchOffset:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.TouchOffsetZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Touch Offset Z", ref DValue, " mm", 0.0, 10.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Touch Offset Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.TouchOffsetZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.TouchOffsetZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_TouchPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TouchPosX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Touch Position X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Touch Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TouchPosX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TouchPosX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_TouchPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TouchPosY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Touch Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Touch Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TouchPosY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TouchPosY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_TouchPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TouchPosZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Touch Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Touch Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TouchPosZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TouchPosZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_TouchStep:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TouchStep) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Touch Step Z", ref DValue, " mm", 0.0, 2.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Touch Step Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TouchStep / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TouchStep = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_TouchLimit:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TouchLimitZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Touch Limit Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Touch Limit Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TouchLimitZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TouchLimitZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                    case eGridValue.Bonder2_TouchOffset:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.TouchOffsetZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Touch Offset Z", ref DValue, " mm", 0.0, 10.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Touch Offset Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.TouchOffsetZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.TouchOffsetZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapOffsetZ:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.GapOffsetZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Offset Z", ref DValue, " mm", -10.0, 10.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Offset Z {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.GapOffsetZ / 1000.0, DValue);
                            cDEF.Work.Bonder1.GapOffsetZ = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapOffsetLimitZ:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.GapOffsetLimitZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Offset Limit Z", ref DValue, " mm", -10.0, 10.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Offset Limit Z {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.GapOffsetLimitZ / 1000.0, DValue);
                            cDEF.Work.Bonder1.GapOffsetLimitZ = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapMeasureDelay:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder1.GapMeasureDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Measure Delay", ref DValue, " sec", 0.0, 10.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Measure Delay {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.GapMeasureDelay / 1000.0, DValue);
                            cDEF.Work.Bonder1.GapMeasureDelay = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapDistanceX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.GapDistanceX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Distance X", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder1.HeadX.ActualPosition - cDEF.Work.TeachBonder1.GapAdjustX) / 1000.0, -50.0, 60.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Distance X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.GapDistanceX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.GapDistanceX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapDistanceY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.GapDistanceY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Distance Y", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder1.HeadY.ActualPosition - cDEF.Work.TeachBonder1.GapAdjustY) / 1000.0, -50.0, 50.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Distance Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.GapDistanceY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.GapDistanceY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapTouchPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.GapTouchX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Touch X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Touch X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.GapTouchX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.GapTouchX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapTouchPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.GapTouchY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Touch Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Touch Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.GapTouchY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.GapTouchY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapAdjustX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.GapAdjustX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Adjust X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadX.ActualPosition / 1000.0, Bonder1_HeadX_Negative / 1000.0, Bonder1_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Adjust X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.GapAdjustX / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.GapAdjustX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapAdjustY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.GapAdjustY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Adjust Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadY.ActualPosition / 1000.0, Bonder1_HeadY_Negative / 1000.0, Bonder1_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Adjust Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.GapAdjustY / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.GapAdjustY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder1_GapMeasureZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder1.GapMeasureZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Measure Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder1.HeadZ.ActualPosition / 1000.0, Bonder1_HeadZ_Negative / 1000.0, Bonder1_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #1] Bonder #1 Gap Measure Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder1.GapMeasureZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder1.GapMeasureZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapOffsetZ:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.GapOffsetZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Offset Z", ref DValue, " mm", -10.0, 10.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Offset Z {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.GapOffsetZ / 1000.0, DValue);
                            cDEF.Work.Bonder2.GapOffsetZ = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapOffsetLimitZ:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.GapOffsetLimitZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Offset Limit Z", ref DValue, " mm", 0.0, 10.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Offset Limit Z {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.GapOffsetLimitZ / 1000.0, DValue);
                            cDEF.Work.Bonder2.GapOffsetLimitZ = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapMeasureDelay:
                        DValue = Convert.ToDouble(cDEF.Work.Bonder2.GapMeasureDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Measure Delay", ref DValue, " sec", 0.0, 10.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Measure Delay {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.GapMeasureDelay / 1000.0, DValue);
                            cDEF.Work.Bonder2.GapMeasureDelay = (int)(DValue * 1000.0);
                            cDEF.Work.Bonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapDistanceX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.GapDistanceX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Distance X", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder2.HeadX.ActualPosition - cDEF.Work.TeachBonder2.GapAdjustX) / 1000.0, -50.0, 60.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Distance X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.GapDistanceX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.GapDistanceX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapDistanceY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.GapDistanceY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Distance Y", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder2.HeadY.ActualPosition - cDEF.Work.TeachBonder2.GapAdjustY) / 1000.0, -50.0, 50.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Distance Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.GapDistanceY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.GapDistanceY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapTouchPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.GapTouchX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Touch X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Touch X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.GapTouchX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.GapTouchX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapTouchPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.GapTouchY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Touch Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Touch Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.GapTouchY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.GapTouchY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapAdjustX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.GapAdjustX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Adjust X", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadX.ActualPosition / 1000.0, Bonder2_HeadX_Negative / 1000.0, Bonder2_HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Adjust X {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.GapAdjustX / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.GapAdjustX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapAdjustY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.GapAdjustY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Adjust Y", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadY.ActualPosition / 1000.0, Bonder2_HeadY_Negative / 1000.0, Bonder2_HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Adjust Y {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.GapAdjustY / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.GapAdjustY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;

                    case eGridValue.Bonder2_GapMeasureZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachBonder2.GapMeasureZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Measure Z", ref DValue, " mm", "CURRENT", cDEF.Run.Bonder2.HeadZ.ActualPosition / 1000.0, Bonder2_HeadZ_Negative / 1000.0, Bonder2_HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Bonder #2] Bonder #2 Gap Measure Z {0:0.000} to {1:0.000}", cDEF.Work.TeachBonder2.GapMeasureZ / 1000.0, DValue);
                            cDEF.Work.TeachBonder2.GapMeasureZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachBonder2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 0, str);
                        }
                        break;
                }
            }
            Grid_Update();
        }
        #endregion

        #region Jog-Relative Move

        //Relative Setting
        private void lbRelative_Position_Click(object sender, EventArgs e)
        {
            double dValue;
            dValue = RelativePosition / 1000;

            if (!XModuleMain.frmBox.fpFloatEdit("Relative Position", ref dValue, " mm", -100.0, 100.0))
                return;
            {
                RelativePosition = dValue * 1000.0;
            }
        }

        //Jog-Relative Move
        private void Jog_Relative_MouseUp(object sender, MouseEventArgs e)
        {
            int FTag = (Convert.ToInt32((sender as Glass.GlassButton).Tag));

            if (FJogMouseDown)
            {
                FJogMouseDown = false;

                int i, cnt;
                if (FTag < 6)
                {
                    cnt = cDEF.Run.Bonder1.MotionCount;
                    for (i = 0; i < cnt; i++)
                        cDEF.Run.Bonder1.Motions[i].Stop();
                }
                else
                {
                    cnt = cDEF.Run.Bonder2.MotionCount;
                    for (i = 0; i < cnt; i++)
                        cDEF.Run.Bonder2.Motions[i].Stop();
                }
            }
        }

        private void btnJog_Relative_MouseDown(object sender, MouseEventArgs e)
        {
            int FTag = (Convert.ToInt32((sender as Glass.GlassButton).Tag));
            int FDirection;

            
            if (SelectJogRelative)
            {
                FJogMouseDown = true;

                switch (FTag)
                {
                    case 0:
                        FDirection = 0;
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 1:
                        FDirection = 1;
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 2:
                        FDirection = 0;
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 3:
                        FDirection = 1;
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 4:
                        FDirection = 0;
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 5:
                        FDirection = 1;
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 6:
                        FDirection = 0;
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 7:
                        FDirection = 1;
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 8:
                        FDirection = 0;
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 9:
                        FDirection = 1;
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 10:
                        FDirection = 0;
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 11:
                        FDirection = 1;
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                }
            }
            else
            {
                switch (FTag)
                {
                    case 0:
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderX].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 1:
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderX].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 2:
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderY].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 3:
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderY].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 4:
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderZ].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 5:
                        cDEF.Run.Bonder1.Motions[(int)eAxis.BonderZ].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 6:
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderX].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 7:
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderX].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 8:
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderY].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 9:
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderY].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 10:
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderZ].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 11:
                        cDEF.Run.Bonder2.Motions[(int)eAxis.BonderZ].Relative(RelativePosition, FSpeedLevel);
                        break;
                }
            }
        }

        private void btnSelectJog_Relative_Click(object sender, EventArgs e)
        {
            SelectJogRelative = !SelectJogRelative;
        }
        private void btnSpeed_Click(object sender, EventArgs e)
        {
            if (FSpeedLevel == 0)
            {
                FSpeedLevel = 100;
                btnSpeed.ForeColor = Color.Red;
                trackBar1.Value = FSpeedLevel;
            }
            else
            {
                FSpeedLevel = 0;
                btnSpeed.ForeColor = Color.Blue;
                trackBar1.Value = FSpeedLevel;
            }
            UpdateSpeed();
        }

        private void Speed_MouseDown(object sender, MouseEventArgs e)
        {
            FSpeedLevel = trackBar1.Value;
            UpdateSpeed();
        }

        private void Speed_MouseMove(object sender, MouseEventArgs e)
        {
            FSpeedLevel = trackBar1.Value;
            UpdateSpeed();
        }

        private void Speed_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void UpdateSpeed()
        {
            switch (FSpeedLevel)
            {
                case 0:
                    btnSpeed.Text = "SLOW";
                    btnSpeed.ForeColor = Color.Blue;
                    break;

                case 100:
                    btnSpeed.Text = "FAST";
                    btnSpeed.ForeColor = Color.Red;
                    break;

                default:
                    btnSpeed.Text = "SPEED: " + (FSpeedLevel).ToString() + "%";
                    btnSpeed.ForeColor = Color.Black;
                    break;
            }

            lbSpeedValue.Text = (FSpeedLevel).ToString() + "%";
        }
        #endregion

        #region Position Move
        private void btnPosition_Move_Click(object sender, EventArgs e)
        {
            string FButton;
            string FName = (sender as Glass.GlassButton).Name;
           
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            switch (FName)
            {
                case "btnBonder1ReadyX":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Ready Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Move_Bonder1_ReadyPositionX();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 0, "[Bonder #1] Move Bonder #1 Ready Position X.");
                    }
                    break;
                case "btnBonder1ReadyY":
                    if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder2 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Ready Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Move_Bonder1_ReadyPositionY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 0, "[Bonder #1] Move Bonder #1 Ready Position Z.");
                    }
                    break;
                case "btnBonder1ReadyZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Ready Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Move_Bonder1_ReadyPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 0, "[Bonder #1] Move Bonder #1 Ready Position Z.");
                    }
                    break;

                case "btnBonder1JettingZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Jetting Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Move_Bonder1_JettingPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 2, "[Bonder #1] Move Bonder #1 Jetting Position Z.");
                    }
                    break;

                case "btnBonder1CameraZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Camera Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Move_Bonder1_CamPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 2, "[Bonder #1] Move Bonder #1 Camera Position Z.");
                    }
                    break;

                case "btnBonder1CameraCenterXY":
                    if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder2 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Camera Center Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        
                        cDEF.Run.Bonder1.Move_Bonder1_CamCenterX();
                        cDEF.Run.Bonder1.Move_Bonder1_CamCenterY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 1, "[Bonder #1] Move Bonder #1 Camera Center Position XY.");
                    }
                    break;

                case "btnBonder2ReadyX":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Ready Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_ReadyPositionX();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 20, "[Bonder #2] Move Bonder #2 Ready Position X.");
                    }
                    break;
                case "btnBonder2ReadyY":
                    if (cDEF.Work.TeachBonder1.AvoidPositionX < cDEF.Run.Bonder1.HeadX.ActualPosition)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder1 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Ready Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_ReadyPositionY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 20, "[Bonder #2] Move Bonder #2 Ready Position Z.");
                    }
                    break;
                case "btnBonder2ReadyZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Ready Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_ReadyPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 20, "[Bonder #2] Move Bonder #2 Ready Position Z.");
                    }
                    break;

                case "btnBonder2JettingZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Jetting Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_JettingPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 22, "[Bonder #2] Move Bonder #2 Jetting Position Z.");
                    }
                    break;

                case "btnBonder2CameraZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Camera Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_CamPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 22, "[Bonder #2] Move Bonder #2 Camera Position Z.");
                    }
                    break;

                case "btnBonder2CameraCenterXY":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Camera Center Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_CamCenterX();
                        cDEF.Run.Bonder2.Move_Bonder2_CamCenterY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 21, "[Bonder #2] Move Bonder #2 Camera Center Position XY.");
                    }
                    break;
                case "btnAvoidX1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Avoid Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Move_Bonder1_AvoidPositionX();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 40, "[Bonder #1] Move Bonder #1 Avoid Position X.");
                    }
                    break;
                case "btnAvoidY1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Avoid Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Move_Bonder1_AvoidPositionY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 40, "[Bonder #1] Move Bonder #1 Avoid Position Y.");
                    }
                    break;
                case "btnAvoidX2":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Avoid Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_AvoidPositionX();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 50, "[Bonder #2] Move Bonder #2 Avoid Position X.");
                    }
                    break;
                case "btnAvoidY2":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Avoid Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_AvoidPositionY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 50, "[Bonder #2] Move Bonder #2 Avoid Position Y.");
                    }
                    break;
                case "btnBonder1CleanZ": // Move 동작
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Clean Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Move_Bonder1_CleanPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 60, "[Bonder #1] Move Bonder #1 Clean Position Z.");
                    }
                    break;
                case "btnBonder1CleanXY": // Move 동작
                    if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder2 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Clean Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (!cDEF.Run.Bonder1.Is_Bonder1_CleanPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Move Bonder #1 Clean Position Z", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.Bonder1.Move_Bonder1_CleanPositionXY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 60, "[Bonder #1] Move Bonder #1 Clean Position XY.");
                    }
                    break;
                case "btnBonder2CleanZ": // Move 동작
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Clean Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Move_Bonder2_CleanPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 70, "[Bonder #2] Move Bonder #2 Clean Position Z.");
                    }
                    break;
                case "btnBonder2CleanXY": // Move 동작
                    if (cDEF.Work.TeachBonder1.AvoidPositionX < cDEF.Run.Bonder1.HeadX.ActualPosition)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder1 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Clean Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (!cDEF.Run.Bonder2.Is_Bonder2_CleanPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Move Bonder #2 Clean Position Z", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }

                        cDEF.Run.Bonder2.Move_Bonder2_CleanPositionXY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 70, "[Bonder #2] Move Bonder #2 Clean Position XY.");
                    }
                    break;
                case "btnBonder1ReadyXYZ": // Move 동작
                    if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder2 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Ready Position XYZ?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {

                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_Ready;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 103, "[Semi Auto] Ready Pos Move");
                    }
                    break;
                case "btnBonder2ReadyXYZ": // Move 동작
                    if (cDEF.Work.TeachBonder1.AvoidPositionX < cDEF.Run.Bonder1.HeadX.ActualPosition)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder1 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Ready Position XYZ?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {

                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_Ready;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 103, "[Semi Auto] Ready Pos Move");
                    }
                    break;
                case "btnMoveGapPositionXY1": // Move 동작
                    if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder2 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Gap Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {

                        cDEF.Run.Bonder1.Move_Bonder1_GapAdjustPositionXY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 103, "[Bonder1] Gap Pos Move XY");
                    }
                    break;

                case "btnMoveGapPositionZ1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #1 Gap Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {

                        cDEF.Run.Bonder1.Move_Bonder1_GapMeasure_Z(true);
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 104, "[Bonder1] Gap Pos Move Z");
                    }
                    break;

                case "btnMoveGapPositionXY2": // Move 동작
                    if (cDEF.Work.TeachBonder1.AvoidPositionX < cDEF.Run.Bonder1.HeadX.ActualPosition)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder1 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Gap Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {

                        cDEF.Run.Bonder2.Move_Bonder2_GapAdjustPositionXY();
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 105, "[Bonder2] Gap Pos Move XY");
                    }
                    break;

                case "btnMoveGapPositionZ2":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Bonder #2 Gap Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {

                        cDEF.Run.Bonder2.Move_Bonder2_GapMeasure_Z(true);
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 106, "[Bonder2] Gap Pos Move Z");
                    }
                    break;


            }
        }
        #endregion


        private void btnSemiAuto_Click(object sender, EventArgs e)
        {
            string FButton;
            string FName = (sender as Glass.GlassButton).Name;
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();


            switch (FName)
            {
                case "btnJetting1":
                case "btnJetting2":
                    break;

                case "btnCheckCamera1":
                case "btnTouch1":
                case "btnClean1":
                case "btnDummy1":
                case "btnBonder1AutoCal":
                case "btnGapMeasure1":
                case "btnGapAdjust1":
                    {
                        if (!cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("MANUAL", "Bonder #2 X is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }


                    }
                    break;

                case "btnCheckCamera2":
                case "btnTouch2":
                case "btnClean2":
                case "btnDummy2":
                case "btnBonder2AutoCa1":
                case "btnGapMeasure2":
                case "btnGapAdjust2":
                    {
                        if (!cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("MANUAL", "Bonder #1 X is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }

                    }
                    break;

                default:

                    break;

            }

            switch (FName)
            {
                case "btnJetting1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Jetting Bonder 1?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1Jetting;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 100, "[Semi Auto] Jetting Bonder 1.");
                    }
                    break;
                case "btnJetting2":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Jetting Bonder 2?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2Jetting;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 101, "[Semi Auto] Jetting Bonder 2.");
                    }
                    break;
                case "btnCheckCamera1":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Check Camera Bonder 1?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1CheckVision;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 102, "[Semi Auto] Check Camera Bonder 1.");
                    }
                    break;
                case "btnCheckCamera2":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Check Camera Bonder 2?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2CheckVision;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 103, "[Semi Auto] Check Camera Bonder 2.");
                    }
                    break;
                case "btnTouch1":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Touch Nozzle?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_Touch;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 104, "[Semi Auto] Bonder1 Touch Nozzle.");
                    }
                    break;

                case "btnTouch2":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Touch Nozzle?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_Touch;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 105, "[Semi Auto] Bonder2 Touch Nozzle.");
                    }
                    break;
                case "btnClean1":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Moving Tip Clean?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_TipClean;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 106, "[Semi Auto] Bonder1 Tip Clean.");
                    }
                    break;
                case "btnClean2":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Moving Tip Clean?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_TipClean;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 107, "[Semi Auto] Bonder1 Tip Clean.");
                    }
                    break;
                case "btnDummy1":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Moving Dummy Dischange?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_Dummy;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 106, "[Semi Auto] Bonder1 Tip Clean.");
                    }
                    break;
                case "btnDummy2":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Moving Dummy Dischange?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_Dummy;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 107, "[Semi Auto] Bonder1 Tip Clean.");
                    }
                    break;
                case "btnBonder1AutoCal":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Auto Calibration?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_AutoCal;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 108, "[Semi Auto] Bonder 1 Auto Calibration");
                    }
                    break;
                case "btnBonder2AutoCal":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Auto Calibration?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_AutoCal;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 108, "[Semi Auto] Bonder 2 Auto Calibration");
                    }
                    break;

                case "btnGapMeasure1":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Gap Measure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_GapMeasure;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 109, "[Semi Auto] Bonder 1 Gap Measure");
                    }
                    break;

                case "btnGapMeasure2":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Gap Measure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_GapMeasure;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 110, "[Semi Auto] Bonder 2 Gap Measure");
                    }
                    break;

                case "btnGapAdjust1":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Gap Touch Adjust?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_GapTouchAdjust;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 110, "[Semi Auto] Bonder 1 Gap Touch Adjust");
                    }
                    break;

                case "btnGapAdjust2":

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Gap Touch Adjust?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_GapTouchAdjust;
                        cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 110, "[Semi Auto] Bonder 2 Gap Touch Adjust");
                    }
                    break;
            }
        }




        private void FormPageWorkingVCMLoader_VisibleChanged(object sender, EventArgs e)
        {
            Grid_Update();
            if (cDEF.Work.DispSensor.DispenserType == 1)
            {
                cDEF.DispenserEcm1.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                cDEF.DispenserEcm1.SetValueStart();

                cDEF.DispenserEcm2.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                cDEF.DispenserEcm2.SetValueStart();
            }
            Jetting1GridUpdate();
            Jetting2GridUpdate();
        }

        private void gridDp1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str;
            DataGridView Grid = (DataGridView)sender;
            int Col = Grid.CurrentCell.ColumnIndex;
            int Row = Grid.CurrentCell.RowIndex;
            double DValue = 0.0;
            int Value = 0;

            if (cDEF.Work.DispSensor.DispenserType == 0)
            {
                switch (Row)
                {
                    // Rise Time
                    case 0:
                        DValue = cDEF.Dispenser1.RTValue;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Rise Time", ref DValue, " sec", 0, 1000.0))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet Rise Time {0:0.000} to {1:0.000}", cDEF.Dispenser1.RTValue, DValue);
                            cDEF.Dispenser1.Send_ParamWrite(DValue, cDEF.Dispenser1.HTValue, cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue, (int)cDEF.Dispenser1.PCTValue, (int)cDEF.Dispenser1.PluseNumValue, (int)cDEF.Dispenser1.WorkModeValue, (int)cDEF.Dispenser1.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // Hold Time
                    case 1:
                        DValue = cDEF.Dispenser1.HTValue * 10.0;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Hold Time", ref DValue, " sec", 0.2, 20.0))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet Hold Time {0:0.000} to {1:0.000}", cDEF.Dispenser1.HTValue * 10.0, DValue);
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, DValue / 10.0, cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue, (int)cDEF.Dispenser1.PCTValue, (int)cDEF.Dispenser1.PluseNumValue, (int)cDEF.Dispenser1.WorkModeValue, (int)cDEF.Dispenser1.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // Fall Time
                    case 2:
                        DValue = cDEF.Dispenser1.FTValue;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Fall Time", ref DValue, " sec", 0, 1000.0))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet Fall Time {0:0.000} to {1:0.000}", cDEF.Dispenser1.FTValue, DValue);
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue, DValue, cDEF.Dispenser1.DelayValue, (int)cDEF.Dispenser1.PCTValue, (int)cDEF.Dispenser1.PluseNumValue, (int)cDEF.Dispenser1.WorkModeValue, (int)cDEF.Dispenser1.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // Delay Time
                    case 3:
                        DValue = cDEF.Dispenser1.DelayValue * 10.0;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Delay Time", ref DValue, " sec", 0.6, 20.0))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet Delay Time {0:0.000} to {1:0.000}", cDEF.Dispenser1.DelayValue * 10, DValue);
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue, cDEF.Dispenser1.FTValue, DValue / 10.0, (int)cDEF.Dispenser1.PCTValue, (int)cDEF.Dispenser1.PluseNumValue, (int)cDEF.Dispenser1.WorkModeValue, (int)cDEF.Dispenser1.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // PCT
                    case 4:
                        Value = (int)cDEF.Dispenser1.PCTValue;
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #1 Jet PCT", ref Value, " %", 0, 100))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet PCT {0:0.000} to {1:0.000}", cDEF.Dispenser1.PCTValue, Value);
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue, cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue, Value, (int)cDEF.Dispenser1.PluseNumValue, (int)cDEF.Dispenser1.WorkModeValue, (int)cDEF.Dispenser1.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // Pulse Num
                    case 5:
                        Value = (int)cDEF.Dispenser1.PluseNumValue;
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #1 Jet Pulse Num", ref Value, " ", 0, 100))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet Pulse Num {0:0.000} to {1:0.000}", cDEF.Dispenser1.PluseNumValue, Value);
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue, cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue, (int)cDEF.Dispenser1.PCTValue, Value, (int)cDEF.Dispenser1.WorkModeValue, (int)cDEF.Dispenser1.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    //Work Mode
                    case 6:
                        Value = (int)cDEF.Dispenser1.WorkModeValue;
                        if (XModuleMain.frmBox.SelectBox("WORK MODE", "NONE,POINT,LINE,CLEAR", ref Value) == DialogResult.No)
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet Work Mode {0:0.000} to {1:0.000}", cDEF.Dispenser1.WorkModeValue, Value);
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue, cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue, (int)cDEF.Dispenser1.PCTValue, (int)cDEF.Dispenser1.PluseNumValue, Value, (int)cDEF.Dispenser1.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                            if (cDEF.Work.Project.GlobalOption.JettingMode1 == 1 || cDEF.Work.Project.GlobalOption.JettingMode1 == 2)
                                cDEF.Work.Project.GlobalOption.JettingMode1 = Value - 1;

                        }
                        break;
                    // Pulse Num
                    case 7:
                        Value = (int)cDEF.Dispenser1.VoltageValue;
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #1 Jet Voltage Value", ref Value, " ", 0, 100))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet Voltage Value {0:0.000} to {1:0.000}", cDEF.Dispenser1.VoltageValue, Value);
                            cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue, cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue, (int)cDEF.Dispenser1.PCTValue, (int)cDEF.Dispenser1.PluseNumValue, (int)cDEF.Dispenser1.WorkModeValue, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                }
            }
            else if (cDEF.Work.DispSensor.DispenserType == 1)
            {
                switch (Row)
                {
                    case 2:
                        DValue = cDEF.DispenserEcm1.PressValue / 10.0;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Press Value", ref DValue, " ", 30, 500))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Press Value {0:0.000} to {1:0.000}", cDEF.DispenserEcm1.PressValue / 10.0, Value);
                            cDEF.DispenserEcm1.PressValue = (int)(DValue * 10);
                            cDEF.DispenserEcm1.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                            cDEF.DispenserEcm1.SetValueStart();
                            cDEF.DispenserEcm1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 43, str);
                        }
                        break;

                    case 3:
                        DValue = cDEF.DispenserEcm1.VacValue / 100.0;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Vacuum Value", ref DValue, " ", 0, 20))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Vacuum Value {0:0.000} to {1:0.000}", cDEF.DispenserEcm1.VacValue / 100.0, Value);
                            cDEF.DispenserEcm1.VacValue = (int)(DValue * 100);
                            cDEF.DispenserEcm1.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                            cDEF.DispenserEcm1.SetValueStart();
                            cDEF.DispenserEcm1.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 44, str);
                        }
                        break;

                }
            }
            else if (cDEF.Work.DispSensor.DispenserType == 2)
            {
                switch (Row)
                {
                    case 0:
                        DValue = cDEF.Work.Recipe.Hz_1;
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #1 Frequency", ref DValue, " ", 10, 900))
                            return;
                        {
                            if (!JetDataCal((int)DValue, (int)cDEF.Work.Recipe.falltime_1[0], (int)cDEF.Work.Recipe.opentime_1[0], (int)cDEF.Work.Recipe.risetime_1[0]))
                            {
                                XModuleMain.frmBox.MessageBox("Parameter InterLock", TfpMessageBoxIcon.fmiInformation, TfpMessageBoxButton.fmbClose.ToString());
                                return;
                            }

                            cDEF.Work.Recipe.Hz_1 = (int)DValue;
                            str = String.Format($"[Bonder #1] TJV #1 Frequency Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.Hz_1, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 90, str);
                        }
                        break;
                    case 1:
                        DValue = cDEF.Work.Recipe.falltime_1[0];
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #1 falltime  Value", ref DValue, " ", 60, 4000))
                            return;
                        {
                            if (!JetDataCal((int)cDEF.Work.Recipe.Hz_1, (int)DValue, (int)cDEF.Work.Recipe.opentime_1[0], (int)cDEF.Work.Recipe.risetime_1[0]))
                            {
                                XModuleMain.frmBox.MessageBox("Parameter InterLock", TfpMessageBoxIcon.fmiInformation, TfpMessageBoxButton.fmbClose.ToString());
                                return;
                            }

                            cDEF.Work.Recipe.falltime_1[0] = (int)DValue;
                            str = String.Format($"[Bonder #1] TJV #1 falltime Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.falltime_1[0], Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 91, str);
                            cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 1);
                        }
                        break;
                    case 2:
                        DValue = cDEF.Work.Recipe.opentime_1[0];
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #1 opentime  Value", ref DValue, " ", 60, 4000))
                            return;
                        {

                            if (!JetDataCal((int)cDEF.Work.Recipe.Hz_1, (int)cDEF.Work.Recipe.falltime_1[0], (int)DValue, (int)cDEF.Work.Recipe.risetime_1[0]))
                            {
                                XModuleMain.frmBox.MessageBox("Parameter InterLock", TfpMessageBoxIcon.fmiInformation, TfpMessageBoxButton.fmbClose.ToString());
                                return;
                            }

                            cDEF.Work.Recipe.opentime_1[0] = (int)DValue;
                            str = String.Format($"[Bonder #1] TJV #1 opentime Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.opentime_1[0], Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 92, str);
                            cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 1);
                        }
                        break;
                    case 3:
                        DValue = cDEF.Work.Recipe.risetime_1[0];
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #1 risetime  Value", ref DValue, " ", 60, 4000))
                            return;
                        {
                            if (!JetDataCal((int)cDEF.Work.Recipe.Hz_1, (int)cDEF.Work.Recipe.falltime_1[0], (int)cDEF.Work.Recipe.opentime_1[0], (int)DValue))
                            {
                                XModuleMain.frmBox.MessageBox("Parameter InterLock", TfpMessageBoxIcon.fmiInformation, TfpMessageBoxButton.fmbClose.ToString());
                                return;
                            }

                            cDEF.Work.Recipe.risetime_1[0] = (int)DValue;
                            str = String.Format($"[Bonder #1] TJV #1 risetime Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.risetime_1[0], Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 92, str);
                            cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 1);
                        }
                        break;
                    case 4:
                        DValue = cDEF.Work.Recipe.Inivolt_1;
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #1 Inivolt  Value", ref DValue, " ", 10, 120))
                            return;
                        {
                            cDEF.Work.Recipe.Inivolt_1 = (int)DValue;
                            str = String.Format($"[Bonder #1] TJV #1 Inivolt Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.Inivolt_1, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 93, str);
                            cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 1);
                        }
                        break;
                    case 5:
                        DValue = cDEF.Work.Recipe.openvolt_1[0];
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #1 openvolt  Value", ref DValue, " ", 0, 120))
                            return;
                        {
                            cDEF.Work.Recipe.openvolt_1[0] = (int)DValue;
                            str = String.Format($"[Bonder #1] TJV #1 openvolt Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.openvolt_1[0], Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 94, str);
                            cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 1);
                        }
                        break;

                    case 6:
                        Value = cDEF.Work.Project.GlobalOption.JettingMode1;
                        if (XModuleMain.frmBox.SelectBox("WORK MODE", "POINT,LINE, ARC", ref Value) == DialogResult.No)
                            return;
                        {
                            cDEF.Work.Project.GlobalOption.JettingMode1 = Value;
                            if (Value == 1)
                                cDEF.Work.Recipe.WorkMode_1 = Value;
                            else
                                cDEF.Work.Recipe.WorkMode_1 = 2;

                            str = String.Format($"[Bonder #1] TJV #1 Jet Work Mode {0:0.000} to {1:0.000}", cDEF.Work.Recipe.WorkMode_1, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 95, str);
                            cDEF.TJV_1.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_1);
                            cDEF.Work.Project.GlobalOption.Save(cDEF.Work.Project.FileName);
                        }
                        break;
                    case 7:
                        DValue = cDEF.Work.Recipe.nDrop_1;
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #1 nDrop  Value", ref DValue, " ", 0, 10000))
                            return;
                        {
                            cDEF.Work.Recipe.nDrop_1 = (int)DValue;
                            str = String.Format($"[Bonder #1] TJV #1 nDrop Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.nDrop_1, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 94, str);
                               }
                        break;
                }
                cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                Jetting1GridUpdate();

            }
        }

        private void gridDp2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str;
            DataGridView Grid = (DataGridView)sender;
            int Col = Grid.CurrentCell.ColumnIndex;
            int Row = Grid.CurrentCell.RowIndex;
            double DValue = 0.0;
            int Value = 0;
            if (cDEF.Work.DispSensor.DispenserType == 0)
            {
                switch (Row)
                {
                    // Rise Time
                    case 0:
                        DValue = cDEF.Dispenser2.RTValue;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Rise Time", ref DValue, " sec", 0, 1000.0))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Jet Rise Time {0:0.000} to {1:0.000}", cDEF.Dispenser2.RTValue, DValue);
                            cDEF.Dispenser2.Send_ParamWrite(DValue, cDEF.Dispenser2.HTValue, cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue, (int)cDEF.Dispenser2.PCTValue, (int)cDEF.Dispenser2.PluseNumValue, (int)cDEF.Dispenser2.WorkModeValue, (int)cDEF.Dispenser2.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // Hold Time
                    case 1:
                        DValue = cDEF.Dispenser2.HTValue * 10.0;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Hold Time", ref DValue, " sec", 0.2, 20.0))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Jet Hold Time {0:0.000} to {1:0.000}", cDEF.Dispenser2.HTValue * 10.0, DValue);
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, DValue / 10.0, cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue, (int)cDEF.Dispenser2.PCTValue, (int)cDEF.Dispenser2.PluseNumValue, (int)cDEF.Dispenser2.WorkModeValue, (int)cDEF.Dispenser2.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // Fall Time
                    case 2:
                        DValue = cDEF.Dispenser2.FTValue;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Fall Time", ref DValue, " sec", 0, 1000.0))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Jet Fall Time {0:0.000} to {1:0.000}", cDEF.Dispenser2.FTValue, DValue);
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue, DValue, cDEF.Dispenser2.DelayValue, (int)cDEF.Dispenser2.PCTValue, (int)cDEF.Dispenser2.PluseNumValue, (int)cDEF.Dispenser2.WorkModeValue, (int)cDEF.Dispenser2.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // Delay Time
                    case 3:
                        DValue = cDEF.Dispenser2.DelayValue * 10.0;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Delay Time", ref DValue, " sec", 0.6, 20.0))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Jet Delay Time {0:0.000} to {1:0.000}", cDEF.Dispenser2.DelayValue * 10, DValue);
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue, cDEF.Dispenser2.FTValue, DValue / 10.0, (int)cDEF.Dispenser2.PCTValue, (int)cDEF.Dispenser2.PluseNumValue, (int)cDEF.Dispenser2.WorkModeValue, (int)cDEF.Dispenser2.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // PCT
                    case 4:
                        Value = (int)cDEF.Dispenser2.PCTValue;
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #2 Jet PCT", ref Value, " %", 0, 100))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Jet PCT {0:0.000} to {1:0.000}", cDEF.Dispenser2.PCTValue, Value);
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue, cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue, Value, (int)cDEF.Dispenser2.PluseNumValue, (int)cDEF.Dispenser2.WorkModeValue, (int)cDEF.Dispenser2.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    // Pulse Num
                    case 5:
                        Value = (int)cDEF.Dispenser2.PluseNumValue;
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #2 Jet Pulse Num", ref Value, " ", 0, 100))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Jet Pulse Num {0:0.000} to {1:0.000}", cDEF.Dispenser2.PluseNumValue, Value);
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue, cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue, (int)cDEF.Dispenser2.PCTValue, Value, (int)cDEF.Dispenser2.WorkModeValue, (int)cDEF.Dispenser2.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                    //Work Mode
                    case 6:
                        Value = (int)cDEF.Dispenser2.WorkModeValue;
                        if (XModuleMain.frmBox.SelectBox("WORK MODE", "NONE,POINT,LINE,CLEAR", ref Value) == DialogResult.No)
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Jet Work Mode {0:0.000} to {1:0.000}", cDEF.Dispenser2.WorkModeValue, Value);
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue, cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue, (int)cDEF.Dispenser2.PCTValue, (int)cDEF.Dispenser2.PluseNumValue, Value, (int)cDEF.Dispenser2.VoltageValue);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);

                            if (cDEF.Work.Project.GlobalOption.JettingMode2 == 1 || cDEF.Work.Project.GlobalOption.JettingMode2 == 2)
                                cDEF.Work.Project.GlobalOption.JettingMode2 = Value - 1;
                        }
                        break;
                    // Pulse Num
                    case 7:
                        Value = (int)cDEF.Dispenser2.VoltageValue;
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #2 Jet Voltage Value", ref Value, " ", 0, 100))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Jet Voltage Value {0:0.000} to {1:0.000}", cDEF.Dispenser2.VoltageValue, Value);
                            cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue, cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue, (int)cDEF.Dispenser2.PCTValue, (int)cDEF.Dispenser2.PluseNumValue, (int)cDEF.Dispenser2.WorkModeValue, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                        }
                        break;
                }
            }
            else if (cDEF.Work.DispSensor.DispenserType == 1)
            {
                switch (Row)
                {
                    case 2:
                        DValue = cDEF.DispenserEcm2.PressValue / 10.0;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Press Value", ref DValue, " ", 30, 500))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Press Value {0:0.000} to {1:0.000}", cDEF.DispenserEcm2.PressValue / 10.0, Value);
                            cDEF.DispenserEcm2.PressValue = (int)(DValue * 10);
                            cDEF.DispenserEcm2.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                            cDEF.DispenserEcm2.SetValueStart();
                            cDEF.DispenserEcm2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 43, str);
                        }
                        break;

                    case 3:
                        DValue = cDEF.DispenserEcm2.VacValue / 100.0;
                        if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Vacuum Value", ref DValue, " ", 0, 20))
                            return;
                        {
                            str = String.Format($"[Bonder #2] Bonder #2 Vacuum Value {0:0.000} to {1:0.000}", cDEF.DispenserEcm2.VacValue / 100.0, Value);
                            cDEF.DispenserEcm2.VacValue = (int)(DValue * 100);
                            cDEF.DispenserEcm2.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                            cDEF.DispenserEcm2.SetValueStart();
                            cDEF.DispenserEcm2.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 44, str);
                        }
                        break;
                }
            }
            else if (cDEF.Work.DispSensor.DispenserType == 2)
            {
                switch (Row)
                {
                    case 0:
                        DValue = cDEF.Work.Recipe.Hz_2;
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #2 Frequency", ref DValue, " ", 10, 900))
                            return;
                        {
                            if (!JetDataCal((int)DValue, (int)cDEF.Work.Recipe.falltime_2[0], (int)cDEF.Work.Recipe.opentime_2[0], (int)cDEF.Work.Recipe.risetime_2[0]))
                            {                              
                                XModuleMain.frmBox.MessageBox("Parameter InterLock", TfpMessageBoxIcon.fmiInformation, TfpMessageBoxButton.fmbClose.ToString());
                                return;
                            }

                            cDEF.Work.Recipe.Hz_2 = (int)DValue;
                            str = String.Format($"[Bonder #2] TJV #2 Frequency Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.Hz_2, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 100, str);
                        }
                        break;
                    case 1:
                        DValue = cDEF.Work.Recipe.falltime_2[0];
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #2 falltime  Value", ref DValue, " ", 60, 4000))
                            return;
                        {

                            if (!JetDataCal((int)cDEF.Work.Recipe.Hz_2, (int)DValue, (int)cDEF.Work.Recipe.opentime_2[0], (int)cDEF.Work.Recipe.risetime_2[0]))
                            {
                                XModuleMain.frmBox.MessageBox("Parameter InterLock", TfpMessageBoxIcon.fmiInformation, TfpMessageBoxButton.fmbClose.ToString());
                                return;
                            }

                            cDEF.Work.Recipe.falltime_2[0] = (int)DValue;
                            str = String.Format($"[Bonder #2] TJV #2 falltime Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.falltime_2[0], Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 101, str);
                            cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_2, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 0);
                        }
                        break;
                    case 2:
                        DValue = cDEF.Work.Recipe.opentime_2[0];
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #2 opentime  Value", ref DValue, " ", 60, 4000))
                            return;
                        {

                            if (!JetDataCal((int)cDEF.Work.Recipe.Hz_2, (int)cDEF.Work.Recipe.falltime_2[0], (int)DValue, (int)cDEF.Work.Recipe.risetime_2[0]))
                            {
                                XModuleMain.frmBox.MessageBox("Parameter InterLock", TfpMessageBoxIcon.fmiInformation, TfpMessageBoxButton.fmbClose.ToString());
                                return;
                            }

                            cDEF.Work.Recipe.opentime_2[0] = (int)DValue;
                            str = String.Format($"[Bonder #2] TJV #2 opentime Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.opentime_2[0], Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 102, str);
                            cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_2, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 0);
                        }
                        break;
                    case 3:
                        DValue = cDEF.Work.Recipe.risetime_2[0];
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #2 risetime  Value", ref DValue, " ", 60, 4000))
                            return;
                        {
                            if (!JetDataCal((int)cDEF.Work.Recipe.Hz_2, (int)cDEF.Work.Recipe.falltime_2[0], (int)cDEF.Work.Recipe.opentime_2[0], (int)DValue))
                            {
                                XModuleMain.frmBox.MessageBox("Parameter InterLock", TfpMessageBoxIcon.fmiInformation, TfpMessageBoxButton.fmbClose.ToString());
                                return;
                            }
                            cDEF.Work.Recipe.risetime_2[0] = (int)DValue;
                            str = String.Format($"[Bonder #2] TJV #2 risetime Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.risetime_2[0], Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 103, str);
                            cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_2, cDEF.Work.Recipe.pixelcount_1, 0);
                        }
                        break;
                    case 4:
                        DValue = cDEF.Work.Recipe.Inivolt_2;
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #2 Inivolt  Value", ref DValue, " ", 10, 120))
                            return;
                        {
                            cDEF.Work.Recipe.Inivolt_2 = (int)DValue;
                            str = String.Format($"[Bonder #2] TJV #2 Inivolt Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.Inivolt_2, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 104, str);
                            cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_2, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 0);
                        }
                        break;
                    case 5:
                        DValue = cDEF.Work.Recipe.openvolt_2[0];
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #2 openvolt  Value", ref DValue, " ", 0, 120))
                            return;
                        {
                            cDEF.Work.Recipe.openvolt_2[0] = (int)DValue;
                            str = String.Format($"[Bonder #2] TJV #2 openvolt Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.openvolt_2[0], Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 105, str);
                            cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_2, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 0);
                        }
                        break;

                    case 6:
                        Value = cDEF.Work.Project.GlobalOption.JettingMode2;
                        if (XModuleMain.frmBox.SelectBox("WORK MODE", "POINT,LINE, ARC", ref Value) == DialogResult.No)
                            return;
                        {
                            cDEF.Work.Project.GlobalOption.JettingMode2 = Value;
                            if (Value == 1)
                                cDEF.Work.Recipe.WorkMode_2 = Value;
                            else
                                cDEF.Work.Recipe.WorkMode_2 = 2;
                            str = String.Format($"[Bonder #2] TJV #2 Jet Work Mode {0:0.000} to {1:0.000}", cDEF.Work.Recipe.WorkMode_2, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 106, str);
                            cDEF.TJV_2.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_2);
                             cDEF.Work.Project.GlobalOption.Save(cDEF.Work.Project.FileName);;
                            //cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 1);
                        }
                        break;
                    case 7:
                        DValue = cDEF.Work.Recipe.nDrop_2;
                        if (!XModuleMain.frmBox.fpFloatEdit($"TJV #2 nDrop  Value", ref DValue, " ", 0, 10000))
                            return;
                        {
                            cDEF.Work.Recipe.nDrop_2 = (int)DValue;
                            str = String.Format($"[Bonder #2] TJV #2 nDrop Value {0:0.000} to {1:0.000}", cDEF.Work.Recipe.nDrop_2, Value);
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 107, str);
                        }
                        break;
                }
                cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                Jetting2GridUpdate();
            }
        }

        private void btnCleanCylinder_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            switch(FName)
            {
                case "btnCleanCylinderIn1":
                    cDEF.Run.Bonder1.TipClean.Forward();
                    cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 26, "[Bonder #1] Clean Cylinder In");
                    break;
                case "btnCleanCylinderOut1":
                    cDEF.Run.Bonder1.TipClean.Backward();
                    cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 27, "[Bonder #1] Clean Cylinder Out");
                    break;
                case "btnCleanCylinderIn2":
                    cDEF.Run.Bonder2.TipClean.Forward();
                    cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 28, "[Bonder #2] Clean Cylinder In");
                    break;
                case "btnCleanCylinderOut2":
                    cDEF.Run.Bonder2.TipClean.Backward();
                    cDEF.Run.LogEvent(cLog.Form_Bonder_Event + 29, "[Bonder #2] Clean Cylinder Out");
                    break;
            }
            
        }
        public void ChangeLanguage()
        {
            lbGridTitle.Text = cDEF.Lang.Trans("SETTING");
            lbImageTitle.Text = cDEF.Lang.Trans("IMAGE");
            lbJogTitle.Text = cDEF.Lang.Trans("JOG");
            lbSemiAutoTitle.Text = cDEF.Lang.Trans("SEMI AUTO");
            lbBonder2Title.Text = cDEF.Lang.Trans("Option");
            lbJetPosGridTitle.Text = cDEF.Lang.Trans("JETTING POSITION SETTING");
            lbBonder1Title.Text = cDEF.Lang.Trans("BONDER #1");
            lbBonder2Title.Text = cDEF.Lang.Trans("BONDER #2");
            lbCurrentPosition_1.Text = cDEF.Lang.Trans("Current Position");
            lbCurrentPosition_2.Text = cDEF.Lang.Trans("Current Position");
        }
        private void btnMoveSample_Click(object sender, EventArgs e)
        {

            if (cDEF.Run.DetailMode != TfpRunningMode.frmStop)
            {
                XModuleMain.frmBox.MessageBox("Warning", "Mode is Not Stop", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (cDEF.Work.Project.GlobalOption.UseIdle1)
            {
                XModuleMain.frmBox.MessageBox("Warning", "Option Check Plz [ Use Idle ]", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }

            string FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            string FName = (sender as Glass.GlassButton).Name;
            switch (FName)
            {
                case "btnMoveSample":
                    if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder2 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Move Sample Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_MoveSample;
                    }
                    break;
                case "btnTrigger":
                    if(cDEF.Work.DispSensor.DispenserType == 1 && cDEF.DispenserEcm1.SetMode == 0)
                    {
                        cDEF.DispenserEcm1.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                        cDEF.DispenserEcm1.PressTime = 200;
                        cDEF.DispenserEcm1.SetValueStart();
                        Thread.Sleep(100);
                    }
                    else if (cDEF.Work.DispSensor.DispenserType == 2)
                    {
                        cDEF.TJV_1.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_1);
                        cDEF.TJV_1.PDDStartSpitting(cDEF.Work.Recipe.Hz_1, cDEF.Work.Recipe.nDrop_1);
                    }
                    cDEF.Run.Bonder1.JettingIO = true;
                    Thread.Sleep(200);
                    cDEF.Run.Bonder1.JettingIO = false;
                    break;
                case "btnMoveCamera":
                    if (cDEF.Run.Bonder2.HeadX.ActualPosition < cDEF.Work.TeachBonder2.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder2 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Move Camera Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder1_MoveCam;
                    }
                    break;
                case "btnMoveSample1":
                    if (cDEF.Run.Bonder1.HeadX.ActualPosition > cDEF.Work.TeachBonder1.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder1 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Move Sample Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_MoveSample;
                    }
                    break;
                case "btnTrigger1":
                    if (cDEF.Work.DispSensor.DispenserType == 1 && cDEF.DispenserEcm2.SetMode == 0)
                    {
                        cDEF.DispenserEcm2.CMDMode = Unit.clsSuperEcm3.ECMDMode.SetValue;
                        cDEF.DispenserEcm2.PressTime = 100;
                        cDEF.DispenserEcm2.SetValueStart();
                        Thread.Sleep(100);
                    }
                    else if(cDEF.Work.DispSensor.DispenserType == 2)
                    {

                    }
                    cDEF.Run.Bonder2.JettingIO = true;
                    Thread.Sleep(100);
                    cDEF.Run.Bonder2.JettingIO = false;
                    break;
                case "btnMoveCamera1":
                    if (cDEF.Run.Bonder1.HeadX.ActualPosition > cDEF.Work.TeachBonder1.AvoidPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Bonder1 Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Move Camera Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Bonder2_MoveCam;
                    }
                    break;
            }
        }

        private void btnTipCleanCntReset1_Click(object sender, EventArgs e)
        {
            string FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            string FName = (sender as Glass.GlassButton).Name;
            switch (FName)
            {
                case "btnTipCleanCntReset1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Tip Clean Pitch Count Reset?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder1.Information.CleanPitchYCount = 0;
                    }
                    break;
                case "btnTipCleanCntReset2":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Tip Clean Pitch Count Reset?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Bonder2.Information.CleanPitchYCount = 0;
                    }
                    break;
            }
        }

        private void btnJettingIO_Click(object sender, EventArgs e)
        {
            if (cDEF.Work.DispSensor.DispenserType == 2)
                cDEF.TJV_1.PDDStartSpitting(cDEF.Work.Recipe.Hz_1, cDEF.Work.Recipe.nDrop_1);

            cDEF.Run.Bonder1.JettingIO = !cDEF.Run.Bonder1.JettingIO; //IO 쓰기 //1.
           
            bool ioreturn = cDEF.Run.Bonder1.JettingIO; //신호 확인
        }

        private void btnJettingIO2_Click(object sender, EventArgs e)
        {
 
            int z = 0;
            if (cDEF.Work.DispSensor.DispenserType == 2)
                z = cDEF.TJV_2.PDDStartSpitting(cDEF.Work.Recipe.Hz_2, cDEF.Work.Recipe.nDrop_2);

            cDEF.Run.Bonder2.JettingIO = !cDEF.Run.Bonder2.JettingIO; //IO 쓰기 //1.
                                                                      // cDEF.Run.Bonder1.Proc_JettingIO(true); //2.
            bool ioreturn = cDEF.Run.Bonder2.JettingIO; //신호 확인
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AutoCal_Click(object sender, EventArgs e)
        {
            string FName = (sender as Label).Name;
            int Value = 0;
			double dValue = 0.0;
            switch (FName)
            {
                case "lbBon1AutoCalCount":
                    Value = cDEF.Work.TeachBonder1.AutoCalCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("AUTO CAL RETRY COUNT", ref Value, " ", 0, 5))
                        return;
                    {
                        cDEF.Work.TeachBonder1.AutoCalCount = Value;
                        cDEF.Work.TeachBonder1.Save();
                    }
                    break;
                case "lbBon1AutoCalSpec":
                    dValue = cDEF.Work.TeachBonder1.AutoCalSpec / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit("AUTO CAL Spec", ref dValue, " ", 0, 5))
                        return;
                    {
                        cDEF.Work.TeachBonder1.AutoCalSpec = (int)(dValue * 1000.0);
                        cDEF.Work.TeachBonder1.Save();
                    }
                    break;
                case "lbBon2AutoCalCount":
                    Value = cDEF.Work.TeachBonder2.AutoCalCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("AUTO CAL RETRY COUNT", ref Value, " ", 0, 5))
                        return;
                    {
                        cDEF.Work.TeachBonder2.AutoCalCount = Value;
                        cDEF.Work.TeachBonder2.Save();
                    }
                    break;
                case "lbBon2AutoCalSpec":
                    dValue = cDEF.Work.TeachBonder2.AutoCalSpec / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit("AUTO CAL Spec", ref dValue, " ", 0, 5))
                        return;
                    {
                        cDEF.Work.TeachBonder2.AutoCalSpec = (int)(dValue * 1000.0);
                        cDEF.Work.TeachBonder2.Save();
                    }
                    break;
            }

        }
        private bool JetDataCal(int Hz, int FallTime, int OpenTime, int RiseTime)
        {

            if ((FallTime + OpenTime + RiseTime) + 100 > 1000000 / Hz)
                return false;
            else
                return true;                    


        }
    }
}
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using XModule.Standard;

namespace XModule.Forms.FormWorking
{
    public partial class FormPageWorkingIndex : TFrame
    {
        #region 변수
        bool FJogMouseDown;                         //마우스 다운
        double Index_Negative = 0.0;                //HeadX Negaitve 리밋
        double Index_Positive = 0.0;                //HeadX Positve 리밋
        double JigPlateAngle_Negative = 0.0;                //HeadX Negaitve 리밋
        double JigPlateAngle_Positive = 0.0;                //HeadX Positve 리밋
        double VCMVision_Negative = 0.0;                //HeadY Negaitve 리밋
        double VCMVision_Positive = 0.0;                //HeadY Positve 리밋
        double PlateAngle_Negative = 0.0;                 //Flux Z #1 Negatitve 리밋
        double PlateAngle_Positve = 0.0;                 //Flux Z #1 Positve 리밋
        double RelativePosition = 0.0;              //RelativePosition Value
        bool SelectJogRelative;                     //Jog-Relative 토글
        int FSpeedLevel;                            //Speed
        #endregion

        public enum eGridValue
        {
            JigPlatAngle_MeasureTime,
            Space1,

            //LensHeight_Default,
            //LensHeight_MinLimit,
            //LensHeight_MaxLimit,
         
            LensHeight_Default_1,
            LensHeight_Default_2,
            LensHeight_Default_3,
            LensHeight_Default_4,
            LensHeight_Default_5,
            LensHeight_Default_6,
            LensHeight_Default_7,
            LensHeight_Default_8,
            LensHeight_Default_9,
            LensHeight_Default_10,
            LensHeight_Default_11,
            LensHeight_Default_12,
            LensHeight_MinOver,
            LensHeight_MaxOver,

            LensHeight_MeasureTime,
            Space2,
            CleanJig_CleanTime,
            Space3,
            PlateAngle_Default,
            PlateAngle_MinLimit,
            PlateAngle_MaxLimit,
            PlateAngle_RowDataMinLimit,
            PlateAngle_RowDataMaxLimit,
            PlateAngle_LensBotRowDataMinLimit, //ksyoon, SideAngle
            PlateAngle_LensBotRowDataMaxLimit, //ksyoon, SideAngle
            PlateAngle_PointNumber, //ksyoon
            PlateAngle_LensBottomPointNumber, //ksyoon, Sideangle
            PlateAngle_VCMPointNumber, //ksyoon, Sideangle
            PlateAngle_MeasureTime,
            PlateAngle_VacuumDelay,
            PlateAngle_BlowDelay,
            PlateAngle_ActuatorTime,
            Space4,
            lbIndexMovingDelay,
            JigPlateAngle_DelayTime,
            PlateAngle_DelayTime,
            Space9,
            VCMVisionGrabDelay,
            VisionGrabDelay,
            Space5,
            IndexStepPitch,
            Space6,
            VCMVision_ReadyPosition,
            VCMVision_WorkPosition,
            Space7,
            JigPlateAngle_ReadyPosition,
            JigPlateAngle_WorkPosition,
            Space8,
            PlateAngle_ReadyPosition,
            PlateAngle_WorkPosition,
        }

        public FormPageWorkingIndex()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 1000);
        }
        private void FormPageWorkingIndex_Load(object sender, EventArgs e)
        {
            Left = 131;
            Top = 60;
            FSpeedLevel = 0;
            FJogMouseDown = true;
            SelectJogRelative = true;
            Grid_Init();
            Grid_Update();
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


                lbIndex_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.Index.Motions[0].ActualPosition, true);
                lbJigPlateAngle_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.JigPlateAngle.Motions[0].ActualPosition, true);
                lbPlateAngle_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.PlateAngle.Motions[0].ActualPosition, true);
                lbVCMVision_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.VCMVision.Motions[0].ActualPosition, true);
                lbIndexNum.Text = $"{cDEF.Run.Index.Information.IndexNum + 1}";
                //if (cDEF.Run.Digital.Input[cDI.Lens_Height_Unit_High_High])
                //    lbLensHeightSignal.Text = "H/H";
                //else if (cDEF.Run.Digital.Input[cDI.Lens_Height_Unit_High])
                //    lbLensHeightSignal.Text = "H";
                //else if (cDEF.Run.Digital.Input[cDI.Lens_Height_Unit_Low])
                //    lbLensHeightSignal.Text = "L";
                //else if (cDEF.Run.Digital.Input[cDI.Lens_Height_Unit_Low_Low])
                //    lbLensHeightSignal.Text = "L/L";
                lbLensHeightGo_IO.BackColor = cDEF.Run.Digital.Input[cDI.Lens_Height_Unit_Go] ? Color.Lime : Color.White;

                lbRelative_Position.Text = cFnc.GetUnitValueString((int)RelativePosition, true);
                lbDisplaceValue.ColorLight = cDEF.Run.Digital.Input[cDI.Lens_Height_Unit_Go] ? Color.Lime : Color.Red;
                if(cDEF.Serials.strValue == "ERR")
                    lbDisplaceValue.Text = "ERR";
                else
                    lbDisplaceValue.Text = cDEF.Serials.Value.ToString("N3");

                lblAngleValue.Text = cDEF.SideAngleMeasuring.AngleValue.ToString("N3");

                if (Convert.ToBoolean(cDEF.Run.Index.Index.FAlarm))
                {
                    lbT.BackColor = Color.Red;
                }
                else
                {
                    lbT.BackColor = Convert.ToBoolean(cDEF.Run.Index.Index.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.JigPlateAngle.JigPlateAngle.FAlarm))
                {
                    lbJF.BackColor = Color.Red;
                }
                else
                {
                    lbJF.BackColor = Convert.ToBoolean(cDEF.Run.JigPlateAngle.JigPlateAngle.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.PlateAngle.PlateAngle.FAlarm))
                {
                    lbSA.BackColor = Color.Red;
                }
                else
                {
                    lbSA.BackColor = Convert.ToBoolean(cDEF.Run.PlateAngle.PlateAngle.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.VCMVision.VCMVision.FAlarm))
                {
                    lbVV.BackColor = Color.Red;
                }
                else
                {
                    lbVV.BackColor = Convert.ToBoolean(cDEF.Run.VCMVision.VCMVision.FInposition) ? Color.White : Color.Lime;
                }
                //Position Check
                //lbFlux1CleanXY.BackColor = cDEF.Run.Head.IsMove_Flux1_CleanPositionXY() ? Color.Lime : Color.White;

                //btnCleanJigVac_Ejector_In.ForeColor = cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum_Ejector_In] ? Color.Red : Color.DarkSlateGray;
                //btnCleanJigVac_Ejector_Out.ForeColor = cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum_Ejector_Out] ? Color.Red : Color.DarkSlateGray;
                //btnCleanJigSuction.ForeColor = cDEF.Run.Digital.Output[cDO.JIC_Clean_Suction] ? Color.Red : Color.DarkSlateGray;
                //btnCleanJigAirBlow.ForeColor = cDEF.Run.Digital.Output[cDO.JIC_Clean_Air_Blow] ? Color.Red : Color.DarkSlateGray;
                lbVCMVision_WorkPosition.BackColor = cDEF.Run.VCMVision.Is_VCMVision_WorkPosition() ? Color.Lime : Color.White;
                lbVCMVision_ReadyPosition.BackColor = cDEF.Run.VCMVision.Is_VCMVision_ReadyPosition() ? Color.Lime : Color.White;
                lbJigPlateAngle_ReadyPosition.BackColor = cDEF.Run.JigPlateAngle.Is_JigPlateAngle_ReadyPosition() ? Color.Lime : Color.White;
                lbJigPlateAngle_WorkPosition.BackColor = cDEF.Run.JigPlateAngle.Is_JigPlateAngle_WorkPosition() ? Color.Lime : Color.White;
                lbCleanJigUp.BackColor = cDEF.Run.CleanJig.CleanJigDown.IsBackward() ? Color.Lime : Color.White;
                lbCleanJigDown.BackColor = cDEF.Run.CleanJig.CleanJigDown.IsForward() ? Color.Lime : Color.White;
                lbPlateAngleUp.BackColor = cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward() ? Color.Lime : Color.White;
                lbPlateAngleDown.BackColor = cDEF.Run.PlateAngle.Up_DownCylinder.IsForward() ? Color.Lime : Color.White;
                lbPlateAngleFw.BackColor = cDEF.Run.PlateAngle.Fw_RvCylinder.IsForward() ? Color.Lime : Color.White;
                lbPlateAngleBw.BackColor = cDEF.Run.PlateAngle.Fw_RvCylinder.IsBackward() ? Color.Lime : Color.White;
                lbPlateAngleUnclamp.BackColor = cDEF.Run.PlateAngle.Clamp.IsBackward() ? Color.Lime : Color.White;
                lbPlateAngleClamp.BackColor = cDEF.Run.PlateAngle.Clamp.IsForward() ? Color.Lime : Color.White;
                lbPlateAngleContact.BackColor = cDEF.Run.PlateAngle.Contact.IsForward() ? Color.Lime : Color.White;
                lbPlateAngleUntact.BackColor = cDEF.Run.PlateAngle.Contact.IsBackward() ? Color.Lime : Color.White;

                lbPlateAngle1Go_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Sensor1_Go] ? Color.Lime : Color.White;
                //lbPlateAngle2Go_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Sensor2_Go] ? Color.Lime : Color.White;

                lbPlateAngleReady.BackColor = cDEF.Run.PlateAngle.Is_PlateAngle_ReadyPosition() ? Color.Lime : Color.White;
                lbPlateAngleWork.BackColor = cDEF.Run.PlateAngle.Is_PlateAngle_WorkPosition() ? Color.Lime : Color.White;

                lblFaceAngleStatus.BackColor = cDEF.SideAngleMeasuring.FaceAngleStatusRun ? Color.Lime : Color.White;

                if(cDEF.Work.Option.ActuatingType == 1)
                {
                    lbActContact.BackColor = cDEF.Run.Act3.Contact.IsForward() ? Color.Lime : Color.White;
                    lbAct3ClampDown.BackColor = cDEF.Run.Act3.ClampDown.IsForward() ? Color.Lime : Color.White;
                }

                //IO Check
                btnPlateAngleVacuum.ForeColor = cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Vacuum] ? Color.Red : Color.DarkSlateGray;
                btnPlateAngleBlow.ForeColor = cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Blow] ? Color.Blue : Color.DarkSlateGray;
                btnCleanJigVacuum.ForeColor = cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum] ? Color.Red : Color.DarkSlateGray;
                btnCleanJigBlow.ForeColor = cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow] ? Color.Blue : Color.DarkSlateGray;
                lbCleanJigUp_IO.BackColor = cDEF.Run.Digital.Input[cDI.JIC_Clean_Up] ? Color.Lime : Color.White;
                lbCleanJigDown_IO.BackColor = cDEF.Run.Digital.Input[cDI.JIC_Clean_Down] ? Color.Lime : Color.White;
                lbPlateAngleVacuumCheck.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Vacuum_Check] ? Color.Lime : Color.White;
                lbPlateAngleUp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Up] ? Color.Lime : Color.White;
                lbPlateAngleDownIO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Down] ? Color.Lime : Color.White;
                lbPlateAngleFw_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Move_Forward] ? Color.Lime : Color.White;
                lbPlateAngleBw_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Move_Backward] ? Color.Lime : Color.White;
                lbPlateAngleUnclamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Unclamp] ? Color.Lime : Color.White;
                lbPlateAngleClamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Clamp] ? Color.Lime : Color.White;
                lbPlateAngleContact_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Contact] ? Color.Lime : Color.White;
                lbPlateAngleUntact_IO.BackColor = cDEF.Run.Digital.Input[cDI.Side_Angle_MeaSure_Unloading_Untact] ? Color.Lime : Color.White;
                btnLensHeightMeasure.ForeColor = cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] ? Color.Red : Color.Black;

                //Actuator Input
                lbActuatorReady_IO.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_2_Ready] ? Color.Lime : Color.White;
                lbActuatorFail_IO.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_2_Fail] ? Color.Lime : Color.White;
                lbActuatorPass_IO.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_2_Pass] ? Color.Lime : Color.White;

                //Actuator Output
                btnActuatorMode1.ForeColor = cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] ? Color.Red : Color.DarkSlateGray;
                btnActuatorMode2.ForeColor = cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] ? Color.Red : Color.DarkSlateGray;
            }));
        }

        #region GridUpdate
        private void Grid_Init()
        {
            GridAdd("R", "Jig Flatness Measure Time", "Time");
            GridAdd_Space();
            //GridAdd("R", "Lens Height Default Value", "Lens Height");
            //GridAdd("R", "Lens Height Spec Minimum", "Lens Height");
            //GridAdd("R", "Lens Height Spec Maximum", "Lens Height");
       
            GridAdd("R", "Lens Height Default Value 1", "Lens Height");
            GridAdd("R", "Lens Height Default Value 2", "Lens Height");
            GridAdd("R", "Lens Height Default Value 3", "Lens Height");
            GridAdd("R", "Lens Height Default Value 4", "Lens Height");
            GridAdd("R", "Lens Height Default Value 5", "Lens Height");
            GridAdd("R", "Lens Height Default Value 6", "Lens Height");
            GridAdd("R", "Lens Height Default Value 7", "Lens Height");
            GridAdd("R", "Lens Height Default Value 8", "Lens Height");
            GridAdd("R", "Lens Height Default Value 9", "Lens Height");
            GridAdd("R", "Lens Height Default Value 10", "Lens Height");
            GridAdd("R", "Lens Height Default Value 11", "Lens Height");
            GridAdd("R", "Lens Height Default Value 12", "Lens Height");
            GridAdd("R", "Lens Height Minimum Limit Over", "Lens Height");
            GridAdd("R", "Lens Height Maximum Limit Over", "Lens Height");

            GridAdd("R", "Lens Height Measure Time", "Time");
            GridAdd_Space();
            GridAdd("R", "Clean Jig Clean Time", "Clean Jig");
            GridAdd_Space();
            GridAdd("R", "Side Angle Measure Default Value", "Side Angle Measure");
            GridAdd("R", "Side Angle Measure Minimum Limit", "Side Angle Measure");
            GridAdd("R", "Side Angle Measure Maximum Limit", "Side Angle Measure");
            GridAdd("R", "Side Angle Measure Row Data Minimum Limit", "Side Angle Measure");
            GridAdd("R", "Side Angle Measure Row Data Maximum Limit", "Side Angle Measure");
            GridAdd("R", "Side Angle Measure Lens Bot Row Data Minimum Limit", "Side Angle Measure"); //ksyoon, SideAngle
            GridAdd("R", "Side Angle Measure Lens Bot Row Data Maximum Limit", "Side Angle Measure"); //ksyoon, SideAngle
            GridAdd("R", "Side Angle Measure Lens Point Number", "Side Angle Measure");//ksyoon
            GridAdd("R", "Side Angle Measure Lens Bottom Point Number", "Side Angle Measure");//ksyoon, SideAngle
            GridAdd("R", "Side Angle Measure VCM Point Number", "Side Angle Measure");//ksyoon, SideAngle
            GridAdd("R", "Side Angle Measure Time", "Time");
            GridAdd("R", "Side Angle Measure Vacuum Delay", "Time");
            GridAdd("R", "Side Angle Measure Blow Delay", "Time");
            GridAdd("R", "Side Angle Measure Actuator Time", "Time");
            GridAdd_Space();
            GridAdd("R", "Index Moving Delay", "Time");
            GridAdd("R", "Jig Flatness Measure Moving Delay", "Time");
            GridAdd("R", "Side Angle Measure Moving Delay", "Time");
            GridAdd_Space();
            GridAdd("R", "VCM Vision Grab Delay", "Time");
            GridAdd("R", "Vision Grab Delay", "Time");
            GridAdd_Space();
            GridAdd("R", "Index Step Pitch", "Index");
            GridAdd_Space();
            GridAdd("R", "VCM Vision Ready Position", "VCM Vision");
            GridAdd("R", "VCM Vision Work Position", "VCM Vision");
            GridAdd_Space();
            GridAdd("R", "Jig Flatness Measure Ready Position", "Jig Flatness Measure");
            GridAdd("R", "Jig Flatness Measure Work Position", "Jig Flatness Measure");
            GridAdd_Space();
            GridAdd("R", "Side Angle Measure Ready Position", "Side Angle Measure");
            GridAdd("R", "Side Angle Measure Work Position", "Side Angle Measure");
        }

        private void Grid_Update()
        {
            for (int i = 0; i < MotionDataGrid.RowCount; i++)
            {
                switch ((eGridValue)i)
                {
                    case eGridValue.IndexStepPitch:
                        MotionDataGrid[3, (int)eGridValue.IndexStepPitch].Value = ((double)cDEF.Work.Index.StepPitch / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.lbIndexMovingDelay:
                        MotionDataGrid[3, (int)eGridValue.lbIndexMovingDelay].Value = ((double)cDEF.Work.Index.MovingDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.JigPlateAngle_ReadyPosition:
                        MotionDataGrid[3, (int)eGridValue.JigPlateAngle_ReadyPosition].Value = ((double)cDEF.Work.TeachJigPlateAngle.ReadyPosition / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.JigPlateAngle_WorkPosition:
                        MotionDataGrid[3, (int)eGridValue.JigPlateAngle_WorkPosition].Value = ((double)cDEF.Work.TeachJigPlateAngle.WorkPosition / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.JigPlateAngle_DelayTime:
                        MotionDataGrid[3, (int)eGridValue.JigPlateAngle_DelayTime].Value = ((double)cDEF.Work.JigPlateAngle.MovingDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.JigPlatAngle_MeasureTime:
                        MotionDataGrid[3, (int)eGridValue.JigPlatAngle_MeasureTime].Value = ((double)cDEF.Work.JigPlateAngle.MeasureTime / 1000.0).ToString("N3");
                        break;
                    //case eGridValue.LensHeight_Default:
                    //    MotionDataGrid[3, (int)eGridValue.LensHeight_Default].Value = ((double)cDEF.Work.LensHeight.DefaultHeight / 1000.0).ToString("N3") + "";
                    //    break;
                    //case eGridValue.LensHeight_MinLimit:
                    //    MotionDataGrid[3, (int)eGridValue.LensHeight_MinLimit].Value = ((double)cDEF.Work.LensHeight.MinLimit / 1000.0).ToString("N3") + "";
                    //    break;
                    //case eGridValue.LensHeight_MaxLimit:
                    //    MotionDataGrid[3, (int)eGridValue.LensHeight_MaxLimit].Value = ((double)cDEF.Work.LensHeight.MaxLimit / 1000.0).ToString("N3") + "";
                    //    break;
                    
                    case eGridValue.LensHeight_Default_1:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_1].Value = cDEF.Work.LensHeight.DefaultHeight[0].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_2:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_2].Value = cDEF.Work.LensHeight.DefaultHeight[1].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_3:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_3].Value = cDEF.Work.LensHeight.DefaultHeight[2].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_4:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_4].Value = cDEF.Work.LensHeight.DefaultHeight[3].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_5:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_5].Value = cDEF.Work.LensHeight.DefaultHeight[4].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_6:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_6].Value = cDEF.Work.LensHeight.DefaultHeight[5].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_7:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_7].Value = cDEF.Work.LensHeight.DefaultHeight[6].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_8:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_8].Value = cDEF.Work.LensHeight.DefaultHeight[7].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_9:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_9].Value = cDEF.Work.LensHeight.DefaultHeight[8].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_10:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_10].Value = cDEF.Work.LensHeight.DefaultHeight[9].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_11:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_11].Value = cDEF.Work.LensHeight.DefaultHeight[10].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_Default_12:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_Default_12].Value = cDEF.Work.LensHeight.DefaultHeight[11].ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_MinOver:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_MinOver].Value = ((double)cDEF.Work.LensHeight.MinOver / 1000.0).ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_MaxOver:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_MaxOver].Value = ((double)cDEF.Work.LensHeight.MaxOver / 1000.0).ToString("N3") + "";
                        break;
                    case eGridValue.LensHeight_MeasureTime:
                        MotionDataGrid[3, (int)eGridValue.LensHeight_MeasureTime].Value = ((double)cDEF.Work.LensHeight.MeasureTime / 1000.0).ToString("N3");
                        break;
                    case eGridValue.CleanJig_CleanTime:
                        MotionDataGrid[3, (int)eGridValue.CleanJig_CleanTime].Value = ((double)cDEF.Work.CleanJig.CleanTime / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.PlateAngle_Default:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_Default].Value = ((double)cDEF.Work.PlateAngle.DefaultValue / 1000.0).ToString("N3") + "";
                        break;
                    case eGridValue.PlateAngle_MinLimit:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_MinLimit].Value = ((double)cDEF.Work.PlateAngle.MinLimit).ToString("N3") + " '";
                        break;
                    case eGridValue.PlateAngle_MaxLimit:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_MaxLimit].Value = ((double)cDEF.Work.PlateAngle.MaxLimit).ToString("N3") + " '";
                        break;
                    case eGridValue.PlateAngle_RowDataMinLimit:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_RowDataMinLimit].Value = ((double)cDEF.Work.PlateAngle.RowDataMinLimit).ToString("N3") + " mm";
                        break;
                    case eGridValue.PlateAngle_RowDataMaxLimit:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_RowDataMaxLimit].Value = ((double)cDEF.Work.PlateAngle.RowDataMaxLimit).ToString("N3") + " mm";
                        break;
                    case eGridValue.PlateAngle_LensBotRowDataMinLimit: //ksyoon, SideAngle
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_LensBotRowDataMinLimit].Value = ((double)cDEF.Work.PlateAngle.LensBotRowDataMinLimit).ToString("N3") + " mm";
                        break;
                    case eGridValue.PlateAngle_LensBotRowDataMaxLimit: //ksyoon, SideAngle
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_LensBotRowDataMaxLimit].Value = ((double)cDEF.Work.PlateAngle.LensBotRowDataMaxLimit).ToString("N3") + " mm";
                        break;
                    case eGridValue.PlateAngle_PointNumber: //ksyoon
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_PointNumber].Value = (cDEF.Work.PlateAngle.SideAnglePoint).ToString() + " Point";
                        break;
                    case eGridValue.PlateAngle_LensBottomPointNumber: //ksyoon, SideAngle
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_LensBottomPointNumber].Value = (cDEF.Work.PlateAngle.SideAngleLensBottomPoint).ToString() + " Point";
                        break;
                    case eGridValue.PlateAngle_VCMPointNumber: //ksyoon, SideAngle
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_VCMPointNumber].Value = (cDEF.Work.PlateAngle.SideAngleVCMPoint).ToString() + " Point";
                        break;
                    case eGridValue.PlateAngle_DelayTime:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_DelayTime].Value = ((double)cDEF.Work.PlateAngle.MovingDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.PlateAngle_MeasureTime:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_MeasureTime].Value = ((double)cDEF.Work.PlateAngle.MeasureTime / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.PlateAngle_VacuumDelay:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_VacuumDelay].Value = ((double)cDEF.Work.PlateAngle.VacuumDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.PlateAngle_BlowDelay:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_BlowDelay].Value = ((double)cDEF.Work.PlateAngle.BlowDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.PlateAngle_ActuatorTime:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_ActuatorTime].Value = ((double)cDEF.Work.PlateAngle.ActuatorTime / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.VCMVisionGrabDelay:
                        MotionDataGrid[3, (int)eGridValue.VCMVisionGrabDelay].Value = ((double)cDEF.Work.VCMVision.VCMVisionGrabDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.VisionGrabDelay:
                        MotionDataGrid[3, (int)eGridValue.VisionGrabDelay].Value = ((double)cDEF.Work.VCMVision.VisionInspectGrabDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.VCMVision_ReadyPosition:
                        MotionDataGrid[3, (int)eGridValue.VCMVision_ReadyPosition].Value = ((double)cDEF.Work.TeachVCMVision.VCMVisionReadyPosition/ 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.VCMVision_WorkPosition:
                        MotionDataGrid[3, (int)eGridValue.VCMVision_WorkPosition].Value = ((double)cDEF.Work.TeachVCMVision.VCMVisionWorkPosition / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.PlateAngle_ReadyPosition:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_ReadyPosition].Value = ((double)cDEF.Work.TeachPlateAngle.ReadyPosition / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.PlateAngle_WorkPosition:
                        MotionDataGrid[3, (int)eGridValue.PlateAngle_WorkPosition].Value = ((double)cDEF.Work.TeachPlateAngle.WorkPosition / 1000.0).ToString("N3") + " mm";
                        break;
                }
            }
        }
        private void GridAdd(string section, string name, string unit)
        {

            string[] str = {$"{section}", $"{name}", $"{unit}", $"" };
            MotionDataGrid.Rows.Add(str);
        }
        private void GridAdd_Space()
        {
            string[] str = { $"", $"", $"" };
            MotionDataGrid.Rows.Add(str);
        }
        #endregion

        #region Grid_DataSetting
        private void MotionDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row;
            double DValue = 0;
            string str = "";
            int Value = 0;
            DataGridView Grid = (DataGridView)sender;

            Index_Negative = cDEF.Run.Index.Index.Config.FLimit.FSoftwareNegative;
            Index_Positive = cDEF.Run.Index.Index.Config.FLimit.FSoftwarePositive;

            JigPlateAngle_Negative = cDEF.Run.JigPlateAngle.JigPlateAngle.Config.FLimit.FSoftwareNegative;
            JigPlateAngle_Positive = cDEF.Run.JigPlateAngle.JigPlateAngle.Config.FLimit.FSoftwarePositive;

            PlateAngle_Negative = cDEF.Run.PlateAngle.PlateAngle.Config.FLimit.FSoftwareNegative;
            PlateAngle_Positve = cDEF.Run.PlateAngle.PlateAngle.Config.FLimit.FSoftwarePositive;

            VCMVision_Negative = cDEF.Run.VCMVision.VCMVision.Config.FLimit.FSoftwareNegative;
            VCMVision_Positive = cDEF.Run.VCMVision.VCMVision.Config.FLimit.FSoftwarePositive;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            eGridValue eGrid = (eGridValue)Row;
            if (Col == 3)
            {
                switch (eGrid)
                {
                    case eGridValue.IndexStepPitch:
                        DValue = Convert.ToDouble(cDEF.Work.Index.StepPitch) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Index Step Pitch", ref DValue, " mm", -80, 80))
                            return;
                        {
                            str = String.Format("[Index] Index Step Pitch {0:0.000} to {1:0.000}", cDEF.Work.Index.StepPitch / 1000.0, DValue);
                            cDEF.Work.Index.StepPitch = (int)(DValue * 1000.0);
                            cDEF.Work.Index.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 0, str);
                        }
                        break;
                    case eGridValue.lbIndexMovingDelay:
                        DValue = Convert.ToDouble(cDEF.Work.Index.MovingDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Index Moving Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Index] Index Moving Delay {0:0.000} to {1:0.000}", cDEF.Work.Index.MovingDelay / 1000.0, DValue);
                            cDEF.Work.Index.MovingDelay = (int)(DValue * 1000.0);
                            cDEF.Work.Index.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 1, str);
                        }
                        break;
                    case eGridValue.JigPlateAngle_ReadyPosition:
                        DValue = Convert.ToDouble(cDEF.Work.TeachJigPlateAngle.ReadyPosition) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Jig Flatness Measure Ready Position", ref DValue," mm" , "CURRENT", cDEF.Run.JigPlateAngle.JigPlateAngle.ActualPosition / 1000.0,JigPlateAngle_Negative / 1000.0,JigPlateAngle_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Jig Flatness Measure] Jig Flatness Measure Ready Position {0:0.000} to {1:0.000}", cDEF.Work.TeachJigPlateAngle.ReadyPosition / 1000.0, DValue);
                            cDEF.Work.TeachJigPlateAngle.ReadyPosition = (int)(DValue * 1000.0);
                            cDEF.Work.TeachJigPlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 2, str);
                        }
                        break;
                    case eGridValue.JigPlateAngle_WorkPosition:
                        DValue = Convert.ToDouble(cDEF.Work.TeachJigPlateAngle.WorkPosition) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Jig Flatness Measure Work Position", ref DValue, " mm", "CURRENT", cDEF.Run.JigPlateAngle.JigPlateAngle.ActualPosition / 1000.0, JigPlateAngle_Negative / 1000.0, JigPlateAngle_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Jig Flatness Measure] Jig Flatness Measure Work Position {0:0.000} to {1:0.000}", cDEF.Work.TeachJigPlateAngle.WorkPosition / 1000.0, DValue);
                            cDEF.Work.TeachJigPlateAngle.WorkPosition = (int)(DValue * 1000.0);
                            cDEF.Work.TeachJigPlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 3, str);
                        }
                        break;
                    case eGridValue.JigPlateAngle_DelayTime:
                        DValue = Convert.ToDouble(cDEF.Work.JigPlateAngle.MovingDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Jig Flatness Measure Moving Delay", ref DValue, " sec", 0, 5))
                            return;
                        {
                            str = String.Format("[Jig Flatness Measure] Jig Flatness Measure Moving Delay {0:0.000} to {1:0.000}", cDEF.Work.JigPlateAngle.MovingDelay / 1000.0, DValue);
                            cDEF.Work.JigPlateAngle.MovingDelay = (int)(DValue * 1000.0);
                            cDEF.Work.JigPlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 4, str);
                        }
                        break;
                    case eGridValue.JigPlatAngle_MeasureTime:
                        DValue = Convert.ToDouble(cDEF.Work.JigPlateAngle.MeasureTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Jig Flatness Measure Time", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Jig Flatness Measure] Jig Flatness Measure Time {0:0.000} to {1:0.000}", cDEF.Work.JigPlateAngle.MeasureTime / 1000.0, DValue);
                            cDEF.Work.JigPlateAngle.MeasureTime = (int)(DValue * 1000.0);
                            cDEF.Work.JigPlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 4, str);
                        }
                        break;
                    case eGridValue.VCMVisionGrabDelay:
                        DValue = Convert.ToDouble(cDEF.Work.VCMVision.VCMVisionGrabDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("VCM Vision Grab Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Vision] VCM Vision Grab Delay {0:0.000} to {1:0.000}", cDEF.Work.VCMVision.VCMVisionGrabDelay / 1000.0, DValue);
                            cDEF.Work.VCMVision.VCMVisionGrabDelay = (int)(DValue * 1000.0);
                            cDEF.Work.VCMVision.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 4, str);
                        }
                        break;
                    case eGridValue.VisionGrabDelay:
                        DValue = Convert.ToDouble(cDEF.Work.VCMVision.VisionInspectGrabDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Vision Inspect Grab Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Vision Inspect] Vision Inspect Grab Delay {0:0.000} to {1:0.000}", cDEF.Work.VCMVision.VisionInspectGrabDelay / 1000.0, DValue);
                            cDEF.Work.VCMVision.VisionInspectGrabDelay = (int)(DValue * 1000.0);
                            cDEF.Work.VCMVision.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 4, str);
                        }
                        break;
                    //case eGridValue.LensHeight_Default:
                    //    DValue = Convert.ToDouble(cDEF.Work.LensHeight.DefaultHeight) / 1000;
                    //    if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                    //        return;
                    //    {
                    //        str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight / 1000.0, DValue);
                    //        cDEF.Work.LensHeight.DefaultHeight = (int)(DValue * 1000.0);
                    //        cDEF.Work.LensHeight.Save();
                    //        cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                    //    }
                    //    break;
                    //case eGridValue.LensHeight_MinLimit:
                    //    DValue = Convert.ToDouble(cDEF.Work.LensHeight.MinLimit) / 1000;
                    //    if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Spec Minimum", ref DValue, " mm", -50, 50))
                    //        return;
                    //    {
                    //        str = String.Format("[Lens Height] Lens Height Spec Minimum {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.MinLimit / 1000.0, DValue);
                    //        cDEF.Work.LensHeight.MinLimit = (int)(DValue * 1000.0);
                    //        cDEF.Work.LensHeight.Save();
                    //        cDEF.Run.LogData(cLog.Form_Index_Data + 6, str);
                    //    }
                    //    break;
                    //case eGridValue.LensHeight_MaxLimit:
                    //    DValue = Convert.ToDouble(cDEF.Work.LensHeight.MaxLimit) / 1000;
                    //    if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Spec Maximum", ref DValue, " mm", -50, 50))
                    //        return;
                    //    {
                    //        str = String.Format("[Lens Height] Lens Height Spec Maximum {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.MaxLimit / 1000.0, DValue);
                    //        cDEF.Work.LensHeight.MaxLimit = (int)(DValue * 1000.0);
                    //        cDEF.Work.LensHeight.Save();
                    //        cDEF.Run.LogData(cLog.Form_Index_Data + 7, str);
                    //    }
                    //    break;
                    
                    case eGridValue.LensHeight_Default_1:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[0];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[0], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[0] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_2:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[1];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[1], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[1] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_3:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[2];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[2], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[2] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_4:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[3];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[3], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[3] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_5:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[4];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[4], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[4] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_6:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[5];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[5], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[5] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_7:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[6];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[6], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[6] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_8:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[7];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[7], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[7] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_9:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[8];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[8], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[8] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_10:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[9];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[9], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[9] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_11:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[10];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[10], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[10] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_Default_12:
                        DValue = cDEF.Work.LensHeight.DefaultHeight[11];
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Default Height", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Default Height {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.DefaultHeight[11], DValue);
                            cDEF.Work.LensHeight.DefaultHeight[11] = DValue;
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 5, str);
                        }
                        break;
                    case eGridValue.LensHeight_MinOver:
                        DValue = Convert.ToDouble(cDEF.Work.LensHeight.MinOver) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Minimum Limit Over", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Minimum Limit Over {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.MinOver / 1000.0, DValue);
                            cDEF.Work.LensHeight.MinOver = (int)(DValue * 1000.0);
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 6, str);
                        }
                        break;
                    case eGridValue.LensHeight_MaxOver:
                        DValue = Convert.ToDouble(cDEF.Work.LensHeight.MaxOver) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Maximum Limit Over", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Maximum Limit Over {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.MaxOver / 1000.0, DValue);
                            cDEF.Work.LensHeight.MaxOver = (int)(DValue * 1000.0);
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 7, str);
                        }
                        break;
                    case eGridValue.LensHeight_MeasureTime:
                        DValue = Convert.ToDouble(cDEF.Work.LensHeight.MeasureTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Lens Height Measure Time", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Height] Lens Height Measure Time {0:0.000} to {1:0.000}", cDEF.Work.LensHeight.MeasureTime / 1000.0, DValue);
                            cDEF.Work.LensHeight.MeasureTime = (int)(DValue * 1000.0);
                            cDEF.Work.LensHeight.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 7, str);
                        }
                        break;
                    case eGridValue.CleanJig_CleanTime:
                        DValue = Convert.ToDouble(cDEF.Work.CleanJig.CleanTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Clean Jig Clean Time", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Clean Jig] Clean Jig Clean Time {0:0.000} to {1:0.000}", cDEF.Work.CleanJig.CleanTime / 1000.0, DValue);
                            cDEF.Work.CleanJig.CleanTime = (int)(DValue * 1000.0);
                            cDEF.Work.CleanJig.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 8, str);
                        }
                        break;
                    case eGridValue.PlateAngle_Default:
                        DValue = Convert.ToDouble(cDEF.Work.PlateAngle.DefaultValue) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Default Value", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Default Value {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.DefaultValue / 1000.0, DValue);
                            cDEF.Work.PlateAngle.DefaultValue = (int)(DValue * 1000.0);
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 9, str);
                        }
                        break;
                    case eGridValue.PlateAngle_MinLimit:
                        DValue = cDEF.Work.PlateAngle.MinLimit;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Minimum Limit", ref DValue, " '", -50, 50))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Minimum Limit {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.MinLimit, DValue);
                            cDEF.Work.PlateAngle.MinLimit = DValue;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 10, str);
                        }
                        break;
                    case eGridValue.PlateAngle_MaxLimit:
                        DValue = cDEF.Work.PlateAngle.MaxLimit;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Maximum Limit", ref DValue, " '", 0, 2000))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Maximum Limit {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.MaxLimit, DValue);
                            cDEF.Work.PlateAngle.MaxLimit = DValue;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_RowDataMinLimit:
                        DValue = cDEF.Work.PlateAngle.RowDataMinLimit;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Row Data Minimum Limit", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Row Data Minimum Limit {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.RowDataMinLimit, DValue);
                            cDEF.Work.PlateAngle.RowDataMinLimit = DValue;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 10, str);
                        }
                        break;
                    case eGridValue.PlateAngle_RowDataMaxLimit:
                        DValue = cDEF.Work.PlateAngle.RowDataMaxLimit;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Row Data Maximum Limit", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Row Data Maximum Limit {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.RowDataMaxLimit, DValue);
                            cDEF.Work.PlateAngle.RowDataMaxLimit = DValue;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_LensBotRowDataMinLimit: //ksyoon, SideAngle
                        DValue = cDEF.Work.PlateAngle.LensBotRowDataMinLimit;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Lens Bottom Row Data Minimum Limit", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Lens Bottom Row Data Minimum Limit {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.LensBotRowDataMinLimit, DValue);
                            cDEF.Work.PlateAngle.LensBotRowDataMinLimit = DValue;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 10, str);
                        }
                        break;
                    case eGridValue.PlateAngle_LensBotRowDataMaxLimit: //ksyoon, SideAngle
                        DValue = cDEF.Work.PlateAngle.LensBotRowDataMaxLimit;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Lens Bottom Row Data Maximum Limit", ref DValue, " mm", -50, 50))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Lens Bottom Row Data Maximum Limit {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.LensBotRowDataMaxLimit, DValue);
                            cDEF.Work.PlateAngle.LensBotRowDataMaxLimit = DValue;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 10, str);
                        }
                        break;
                    case eGridValue.PlateAngle_PointNumber: //ksyoon
                        Value = cDEF.Work.PlateAngle.SideAnglePoint;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Side Angle Measure Point Number", ref Value, " ea", 0, 8))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Point Number {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.SideAnglePoint, Value);
                            cDEF.Work.PlateAngle.SideAnglePoint = Value;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_LensBottomPointNumber: //ksyoon, SideAngle
                        Value = cDEF.Work.PlateAngle.SideAngleLensBottomPoint;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Side Angle Measure Lens Bottom Point Number", ref Value, " ea", 0, 8))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Lens Bottom Point Number {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.SideAngleLensBottomPoint, Value);
                            cDEF.Work.PlateAngle.SideAngleLensBottomPoint = Value;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_VCMPointNumber: //ksyoon
                        Value = cDEF.Work.PlateAngle.SideAngleVCMPoint;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Side Angle Measure VCM Point Number", ref Value, " ea", 0, 4))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure VCM Point Number {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.SideAngleVCMPoint, Value);
                            cDEF.Work.PlateAngle.SideAngleVCMPoint = Value;
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_DelayTime:
                        DValue = Convert.ToDouble(cDEF.Work.PlateAngle.MovingDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Moving Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Moving Delay {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.MovingDelay / 1000.0, DValue);
                            cDEF.Work.PlateAngle.MovingDelay = (int)(DValue * 1000.0);
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_MeasureTime:
                        DValue = Convert.ToDouble(cDEF.Work.PlateAngle.MeasureTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Time", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Time {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.MeasureTime / 1000.0, DValue);
                            cDEF.Work.PlateAngle.MeasureTime = (int)(DValue * 1000.0);
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_VacuumDelay:
                        DValue = Convert.ToDouble(cDEF.Work.PlateAngle.VacuumDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Vacuum Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Vacuum Delay {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.VacuumDelay / 1000.0, DValue);
                            cDEF.Work.PlateAngle.VacuumDelay = (int)(DValue * 1000.0);
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_BlowDelay:
                        DValue = Convert.ToDouble(cDEF.Work.PlateAngle.BlowDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Blow Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Blow Delay {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.BlowDelay / 1000.0, DValue);
                            cDEF.Work.PlateAngle.BlowDelay = (int)(DValue * 1000.0);
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.PlateAngle_ActuatorTime:
                        DValue = Convert.ToDouble(cDEF.Work.PlateAngle.ActuatorTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Actuator Time", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Actuator Time {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.ActuatorTime / 1000.0, DValue);
                            cDEF.Work.PlateAngle.ActuatorTime = (int)(DValue * 1000.0);
                            cDEF.Work.PlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 11, str);
                        }
                        break;
                    case eGridValue.VCMVision_ReadyPosition:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMVision.VCMVisionReadyPosition) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("VCM Vision Ready Position", ref DValue, " mm", "CURRENT", cDEF.Run.VCMVision.VCMVision.ActualPosition / 1000.0, VCMVision_Negative / 1000.0, VCMVision_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Vision] VCM Vision Ready Position {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMVision.VCMVisionReadyPosition / 1000.0, DValue);
                            cDEF.Work.TeachVCMVision.VCMVisionReadyPosition = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMVision.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 2, str);
                        }
                        break;
                    case eGridValue.VCMVision_WorkPosition:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMVision.VCMVisionWorkPosition) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("VCM Vision Work Position", ref DValue, " mm", "CURRENT", cDEF.Run.VCMVision.VCMVision.ActualPosition / 1000.0, VCMVision_Negative / 1000.0, VCMVision_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Vision] VCM Vision Work Position {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMVision.VCMVisionWorkPosition / 1000.0, DValue);
                            cDEF.Work.TeachVCMVision.VCMVisionWorkPosition = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMVision.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 3, str);
                        }
                        break;
                    case eGridValue.PlateAngle_ReadyPosition:
                        DValue = Convert.ToDouble(cDEF.Work.TeachPlateAngle.ReadyPosition) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Ready Position", ref DValue, " mm", "CURRENT", cDEF.Run.PlateAngle.PlateAngle.ActualPosition / 1000.0, PlateAngle_Negative / 1000.0, PlateAngle_Positve / 1000.0))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Ready Position {0:0.000} to {1:0.000}", cDEF.Work.TeachPlateAngle.ReadyPosition / 1000.0, DValue);
                            cDEF.Work.TeachPlateAngle.ReadyPosition = (int)(DValue * 1000.0);
                            cDEF.Work.TeachPlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 2, str);
                        }
                        break;
                    case eGridValue.PlateAngle_WorkPosition:
                        DValue = Convert.ToDouble(cDEF.Work.TeachPlateAngle.WorkPosition) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Side Angle Measure Work Position", ref DValue, " mm", "CURRENT", cDEF.Run.PlateAngle.PlateAngle.ActualPosition / 1000.0, PlateAngle_Negative / 1000.0, PlateAngle_Positve / 1000.0))
                            return;
                        {
                            str = String.Format("[Side Angle Measure] Side Angle Measure Work Position {0:0.000} to {1:0.000}", cDEF.Work.TeachPlateAngle.WorkPosition / 1000.0, DValue);
                            cDEF.Work.TeachPlateAngle.WorkPosition = (int)(DValue * 1000.0);
                            cDEF.Work.TeachPlateAngle.Save();
                            cDEF.Run.LogData(cLog.Form_Index_Data + 3, str);
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

                if (FTag == 0 || FTag == 1)
                {
                    cnt = cDEF.Run.Index.MotionCount;
                    for (i = 0; i < cnt; i++)
                        cDEF.Run.Index.Motions[i].Stop();
                }
                else if (FTag == 2 || FTag == 3)
                {
                    cnt = cDEF.Run.JigPlateAngle.MotionCount;
                    for (i = 0; i < cnt; i++)
                        cDEF.Run.JigPlateAngle.Motions[i].Stop();
                }
                else if (FTag == 4 || FTag == 5)
                {
                    cnt = cDEF.Run.PlateAngle.MotionCount;
                    for (i = 0; i < cnt; i++)
                        cDEF.Run.PlateAngle.Motions[i].Stop();
                }
                else if (FTag == 6 || FTag == 7)
                {
                    cnt = cDEF.Run.VCMVision.MotionCount;
                    for (i = 0; i < cnt; i++)
                        cDEF.Run.VCMVision.Motions[i].Stop();
                }
            }
        }

        private void btnJog_Relative_MouseDown(object sender, MouseEventArgs e)
        {
            if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "VCM Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "Lens Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "Unload Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.CleanJig.CleanJigDown.IsBackward())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "Clean Jig is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.Curing1.UVDown.IsBackward())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "UV Up-Down is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.Curing1.Contact.IsBackward())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "UV Contact is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (cDEF.Run.Digital.Output[cDO.Lens_Height_Measure])
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "Lens Height Measure is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "Plate Angle Up-Down is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionZ())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "Bonder #1 is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionZ())
            {
                XModuleMain.frmBox.MessageBox("MANUAL", "Bonder #2 is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            int FTag = (Convert.ToInt32((sender as Glass.GlassButton).Tag));
            int FDirection;


            if (SelectJogRelative)
            {
                FJogMouseDown = true;

                switch (FTag)
                {
                    case 0:
                        FDirection = 0;
                        cDEF.Run.Index.Motions[0].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 1:
                        FDirection = 1;
                        cDEF.Run.Index.Motions[0].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 2:
                        FDirection = 0;
                        cDEF.Run.JigPlateAngle.Motions[0].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 3:
                        FDirection = 1;
                        cDEF.Run.JigPlateAngle.Motions[0].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 4:
                        FDirection = 0;
                        cDEF.Run.PlateAngle.Motions[0].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 5:
                        FDirection = 1;
                        cDEF.Run.PlateAngle.Motions[0].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 6:
                        if (!(cDEF.Run.LensPicker.Is_Head_ReadyPositionX() && cDEF.Run.LensPicker.Is_Head_ReadyPositionX()))
                        {
                            XModuleMain.frmBox.MessageBox("MANUAL", "Lens Picker Is Not Ready Position XY", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.VCMVision.Motions[0].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 7:
                        if (!(cDEF.Run.LensPicker.Is_Head_ReadyPositionX() && cDEF.Run.LensPicker.Is_Head_ReadyPositionX()))
                        {
                            XModuleMain.frmBox.MessageBox("MANUAL", "Lens Picker Is Not Ready Position XY", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.VCMVision.Motions[0].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                }
            }
            else
            {
                switch (FTag)
                {
                    case 0:
                        cDEF.Run.Index.Motions[0].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 1:
                        cDEF.Run.Index.Motions[0].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 2:
                        cDEF.Run.JigPlateAngle.Motions[0].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 3:
                        cDEF.Run.JigPlateAngle.Motions[0].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 4:
                        FDirection = 0;
                        cDEF.Run.PlateAngle.Motions[0].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 5:
                        FDirection = 1;
                        cDEF.Run.PlateAngle.Motions[0].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 6:
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionX() && !cDEF.Run.LensPicker.Is_Head_ReadyPositionX() && RelativePosition > 1000)
                        {
                            XModuleMain.frmBox.MessageBox("MANUAL", "Lens Picker Is Not Ready Position XY", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.VCMVision.Motions[0].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 7:
                        if (!(cDEF.Run.LensPicker.Is_Head_ReadyPositionX() && cDEF.Run.LensPicker.Is_Head_ReadyPositionX()) && RelativePosition > 1000)
                        {
                            XModuleMain.frmBox.MessageBox("MANUAL", "Lens Picker Is Not Ready Position XY", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.VCMVision.Motions[0].Relative(RelativePosition, FSpeedLevel);
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
                case "btnIndex_StepMove":
                    if(!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "VCM Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ()) 
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "Lens Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ()) 
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "Unload Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.CleanJig.CleanJigDown.IsBackward())
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "Clean Jig is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.Curing1.UVDown.IsBackward())
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "UV Up-Down is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if(!cDEF.Run.Curing1.Contact.IsBackward())
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "UV Contact is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if(cDEF.Run.Digital.Output[cDO.Lens_Height_Measure])
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "Lens Height Measure is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if(!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "Plate Angle Up-Down is Not Backward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if(!cDEF.Run.Bonder1.Is_Bonder1_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "Bonder #1 is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.Bonder2.Is_Bonder2_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("MANUAL", "Bonder #2 is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Step Move Index?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        //cDEF.Run.Index.Move_NextStep();
                        cDEF.Run.Mode = Running.TRunMode.Manual_IndexStepMove;
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 0, "[Index] Index Step Move.");
                    }
                    break;
                case "btnJigPlateAngle_ReadyPosition":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Jig Flatness Measure Ready Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.JigPlateAngle.Move_JigPlateAngle_ReadyPosition();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 1, "[Jig Flatness Measure] Move Jig Flatness Measure Ready Position.");
                    }
                    break;
                case "btnJigPlateAngle_WorkPosition":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Jig Flatness Measure Work Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.JigPlateAngle.Move_JigPlateAngle_WorkPosition();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 2, "[Jig Flatness Measure] Move Jig Flatness Measure Work Position.");
                    }
                    break;
                //case "btnPlateAngle_ReadyPosition":
                //    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Side Angle Measure Ready Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                //    {
                //        cDEF.Run.PlateAngle.Move_PlateAngle_ReadyPosition();
                //        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 0, "[Jig Flatness Measure] Move Jig Flatness Measure Ready Position.");
                //    }
                //    break;
                //case "btnPlateAngle_WorkPosition":
                //    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Side Angle Measure Work Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                //    {
                //        cDEF.Run.PlateAngle.Move_PlateAngle_WorkPosition();
                //        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 0, "[Jig Flatness Measure] Move Jig Flatness Measure Work Position.");
                //    }
                //    break;
                case "btnVCMVision_ReadyPosition":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Vision Ready Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMVision.Move_VCMVision_ReadyPosition();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 0, "[Jig Flatness Measure] Move Jig Flatness Measure Ready Position.");
                    }
                    break;
                case "btnVCMVision_WorkPosition":
                    if(cDEF.Run.LensPicker.HeadX.ActualPosition >= cDEF.Work.TeachLensPicker.BottomCamPositionX)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Lens Picker is in InterLock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Vision Work Position?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMVision.Move_VCMVision_WorkPosition();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 0, "[Jig Flatness Measure] Move Jig Flatness Measure Work Position.");
                    }
                    break;
            }
        }
        #endregion

        private void btnUnit_Option_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            string FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            switch (FName)
            {
                case "btnCleanJigUp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Clean Jig Cylinder Up?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.CleanJig.CleanJigDown.Backward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 3, "[Clean Jig] Move Clean Jig Cylinder Up");
                    }
                    break;

                case "btnCleanJigDown":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Clean Jig Cylinder Down?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.CleanJig.CleanJigDown.Forward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 4, "[Clean Jig] Move Clean Jig Cylinder Down");
                    }
                    break;

                case "btnCleanJigVacuum":
                        cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum] = !cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum];
                    if (cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum])
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 5, "[Clean Jig] Clean Jig Vacuum On");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 6, "[Clean Jig] Clean Jig Vacuum Off");
                    break;

                case "btnCleanJigBlow":
                    cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow] = !cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow];
                    if(cDEF.Run.Digital.Output[cDO.Jig_Clean_Blow])
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 7, "[Clean Jig] Clean Jig Blow On");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 8, "[Clean Jig] Clean Jig Blow Off");
                    break;

                //case "btnCleanJigSuction":
                //    cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum] = !cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum];
                //    if(cDEF.Run.Digital.Output[cDO.JIC_Clean_Vacuum])
                //        cDEF.Run.LogEvent(cLog.Form_Index_Event + 9, "[Clean Jig] Clean Jig Suction On");
                //    else
                //        cDEF.Run.LogEvent(cLog.Form_Index_Event + 10, "[Clean Jig] Clean Jig Suction Off");
                //    break;

                case "btnPlateAngleVacuum":
                    cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Vacuum] = !cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Vacuum];
                    if (cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Vacuum])
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 13, "[Side Angle Measure(Unloader)] Side Angle Measure Vacuum On");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 14, "[Side Angle Measure(Unloader)] Side Angle Measure Vacuum Off");
                    break;

                case "btnPlateAngleBlow":
                    cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Blow] = !cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Blow];
                    if (cDEF.Run.Digital.Output[cDO.Side_Angle_Measure_Unloading_Blow])
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 15, "[Side Angle Measure(Unloader)] Side Angle Measure Blow ON");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 16, "[Side Angle Measure(Unloader)] Side Angle Measure Blow Off");
                    break;

                case "btnPlateAngleUp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Side Angle Measure Cylinder Move Up?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Up_DownCylinder.Backward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 17, "[Side Angle Measure(Unloader)] Side Angle Measure Cylinder Move Up");
                    }
                    break;

                case "btnPlateAngleDown":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Side Angle Measure Cylinder Move Down?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Up_DownCylinder.Forward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 18, "[Side Angle Measure(Unloader)] Side Angle Measure Cylinder Move Down");
                    }
                    break;

                case "btnPlateAngleFw":
                    if(cDEF.Run.UnloadPicker.HeadX.ActualPosition < cDEF.Work.TeachUnloadPicker.AvoidPositionX
                        && cDEF.Run.UnloadPicker.HeadY.ActualPosition > cDEF.Work.TeachUnloadPicker.AvoidPositionY)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Unload Picker is in Interlock Area", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Up/Down Cylinder is not Backward Status", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Side Angle Measure Cylinder Move Forward?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Fw_RvCylinder.Forward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 19, "[Side Angle Measure(Unloader)] Side Angle Measure Cylinder Move Forward");
                    }
                    break;

                case "btnPlateAngleBw":
                    if (!cDEF.Run.PlateAngle.Up_DownCylinder.IsBackward())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Up/Down Cylinder is not Backward Status", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Side Angle Measure Cylinder Move Backward?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Fw_RvCylinder.Backward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 20, "[Side Angle Measure(Unloader)] Side Angle Measure Cylinder Move Backward");
                    }
                    break;

                case "btnPlateAngleUnclamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Side Angle Measure Unclamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Clamp.Backward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 21, "[Side Angle Measure(Unloader)] Side Angle Measure Unclamp");
                    }
                    break;

                case "btnPlateAngleClamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Side Angle Measure Clamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Clamp.Forward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 22, "[Side Angle Measure(Unloader)] Side Angle Measure Clamp");
                    }
                    break;

                case "btnPlateAngleContact":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Side Angle Measure Contact?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Contact.Forward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 23, "[Side Angle Measure(Unloader)] Side Angle Measure Contact");
                    }
                    break;

                case "btnPlateAngleUncontact":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Side Angle Measure Untact?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Contact.Backward();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 24, "[Side Angle Measure(Unloader)] Side Angle Measure Untact");
                    }
                    break;
                case "btnLensHeightMeasure":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Lens Height Measure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Digital.Output[cDO.Lens_Height_Measure] = !cDEF.Run.Digital.Output[cDO.Lens_Height_Measure];
                        if (cDEF.Run.Digital.Output[cDO.Lens_Height_Measure])
                            cDEF.Run.LogEvent(cLog.Form_Index_Event + 24, "[Lens Height] Lens Height Measure Down");
                        else
                            cDEF.Run.LogEvent(cLog.Form_Index_Event + 24, "[Lens Height] Lens Height Measure Up");
                    }
                    break;
                case "btnPlateAngleTrigger":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Face Angle Measure Send Trigger?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.SideAngleMeasuring.Send_Trigger();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 25, "[Face Angle] Face Angle Measure Send Trigger");
                    }
                    break;
                case "btnFaceAngleStatus":
                  //  if (XModuleMain.frmBox.MessageBox("MANUAL", "Face Angle Measure Send Trigger?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.SideAngleMeasuring.Send_RunStatus();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 26, "[Face Angle] Face Angle Measure Send Trigger");
                    }
                    break;
                case "btnPlateAngleReady":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Plate Angle Ready Position Move ?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Move_PlateAngle_ReadyPosition();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 27, "[Plate Angle] Plate Angle Ready Position Move ");
                    }
                    break;
                case "btnPlateAngleWork":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Plate Angle Working Position Move ?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.PlateAngle.Move_PlateAngle_WorkPosition();
                        cDEF.Run.LogEvent(cLog.Form_Index_Event + 28, "[Plate Angle] Plate Angle Working Position Move");
                    }
                    break;

                case "btnActContact":
                    if (cDEF.Work.Option.ActuatingType != 1)
                        return;

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Act 3 Contact?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (cDEF.Run.Act3.Contact.IsBackward())
                        {
                            cDEF.Run.Act3.Contact.Forward();
                            cDEF.Run.LogEvent(cLog.Form_Index_Event + 24, "Act 3 Contact Cylinder is Forward");
                        }
                        else
                        {
                            cDEF.Run.Act3.Contact.Backward();
                            cDEF.Run.LogEvent(cLog.Form_Index_Event + 24, "Act 3 Contact Cylinder is Backward");
                        }
                    }
                    break;

                case "btnAct3ClampDown":
                    if (cDEF.Work.Option.ActuatingType != 1)
                        return;

                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Act 3 Contact?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (cDEF.Run.Act3.ClampDown.IsBackward())
                        {
                            cDEF.Run.Act3.ClampDown.Forward();
                            cDEF.Run.LogEvent(cLog.Form_Index_Event + 24, "Act 3 ClampDown Cylinder is Forward");
                        }
                        else
                        {
                            cDEF.Run.Act3.ClampDown.Backward();
                            cDEF.Run.LogEvent(cLog.Form_Index_Event + 24, "Act 3 ClampDown Cylinder is Backward");
                        }
                    }
                    break;
            }
        }

        private void btnSemiAuto_Click(object sender, EventArgs e)
        {
            string FButton;
            string FName = (sender as Glass.GlassButton).Name;
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            switch (FName)
            {
                case "btnTopVision":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Top Vision?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_TopVisionCheck;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Top Vision.");
                    }
                    break;
                case "btnSideAnglePickPlace":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Side Angle Pick & Place?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_PlateAnglePickPlace;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Side Angle Pick & Place.");
                    }
                    break;
                case "btnSideAngleMeasure":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Top Side Angle Measure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_PlateAngleMeasure;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Side Angle Measure.");
                    }
                    break;
                case "btnJigFlatnessMeasure":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Jig Flatness Measure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_MeasureJigPlateAngle;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Jig Flatness Measure.");
                    }
                    break;
                case "btnCleanJig":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Clean Jig?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Clean;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Clean Jig.");
                    }
                    break;
                case "btnVisionInspect":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Vision Inspect?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_VisionInspect;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Clean Jig.");
                    }
                    break;
                case "btnLensMeasure":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Lens Height Measure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_MeasureHeight;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Lens Height Measure.");
                    }
                    break;
                    
            }
        }

        

        private void FormPageWorkingVCMLoader_VisibleChanged(object sender, EventArgs e)
        {
            Grid_Update();
        }

        private void btnUnit_Select_Click(object sender, EventArgs e)
        {
            string FName = (sender as Button).Name;

            switch (FName)
            {
                case "btnUnit_PlateAngle":
                    panelUnitDefault.Visible = false;
                    panelPlateAngle.Visible = true;
                    panelCleanJig.Visible = false;
                    panelJigPlateAngle.Visible = false;
                    panelLensHeight.Visible = false;
                    panelIndex.Visible = false;
                    break;
                case "btnUnit_CleanJig":
                    panelUnitDefault.Visible = false;
                    panelPlateAngle.Visible = false;
                    panelCleanJig.Visible = true;
                    panelJigPlateAngle.Visible = false;
                    panelLensHeight.Visible = false;
                    panelIndex.Visible = false;
                    break;
                case "btnUnit_JigPlateAngle":
                    panelUnitDefault.Visible = false;
                    panelPlateAngle.Visible = false;
                    panelCleanJig.Visible = false;
                    panelJigPlateAngle.Visible = true;
                    panelLensHeight.Visible = false;
                    panelIndex.Visible = false;
                    break;
                case "btnUnit_LensHeight":
                    panelUnitDefault.Visible = false;
                    panelPlateAngle.Visible = false;
                    panelCleanJig.Visible = false;
                    panelJigPlateAngle.Visible = false;
                    panelLensHeight.Visible = true;
                    panelIndex.Visible = false;
                    break;
                case "btnUnit_Index":
                    panelUnitDefault.Visible = false;
                    panelPlateAngle.Visible = false;
                    panelCleanJig.Visible = false;
                    panelJigPlateAngle.Visible = false;
                    panelLensHeight.Visible = false;
                    panelIndex.Visible = true;
                    break;
            }
        }
        public void ChangeLanguage()
        {
            lbGridTitle.Text = cDEF.Lang.Trans("SETTING");
            lbImageTitle.Text = cDEF.Lang.Trans("IMAGE");
            lbJogTitle.Text = cDEF.Lang.Trans("JOG");
            lbSemiAutoTitle.Text = cDEF.Lang.Trans("SEMI AUTO");
            lbOptionTitle.Text = cDEF.Lang.Trans("Option");
            lbUnitSelectTitle.Text = cDEF.Lang.Trans("UNIT SELECT");
            lbPositionTitle.Text = cDEF.Lang.Trans("CURRENT POSITION");
            lbSignalTitle.Text = cDEF.Lang.Trans("SIGNAL");
            lbSelectUnit.Text = cDEF.Lang.Trans("SELECT UNIT");
        }

        private void lbDSensor_Click(object sender, EventArgs e)
        {
            cDEF.Serials.SendMeasure();
        }

        private void btnActuator_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            switch (FName)
            {
                case "btnActuatorMode1":
                    cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start] = !cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start];
                    if (cDEF.Run.Digital.Output[cDO.Actuator_2_A_Start])
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #2 Mode 1 Start.");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #2 Mode 1 OFF.");
                    break;
                case "btnActuatorMode2":
                    cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start] = !cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start];
                    if (cDEF.Run.Digital.Output[cDO.Actuator_2_B_Start])
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #2 Mode 2 Start.");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #2 Mode 2 OFF.");
                    break;

                case "btnActuatorMode3":
                    cDEF.Run.Digital.Output[cDO.Actuator_3_A_Start] = !cDEF.Run.Digital.Output[cDO.Actuator_3_A_Start];
                    if (cDEF.Run.Digital.Output[cDO.Actuator_3_A_Start])
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #3 Mode 1 Start.");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #3 Mode 1 OFF.");
                    break;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
} 
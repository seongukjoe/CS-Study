using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using XModule.Standard;

namespace XModule.Forms.FormWorking
{
    public partial class FormPageWorkingVCMPicker : TFrame
    {
        #region 변수
        bool FJogMouseDown;                         //마우스 다운
        double HeadX_Negative = 0.0;                //Head X Negaitve 리밋
        double HeadX_Positive = 0.0;                //Head X Positve 리밋
        double HeadY_Negative = 0.0;                //Head Y Negaitve 리밋
        double HeadY_Positive = 0.0;                //Head Y Positve 리밋
        double HeadZ_Negative = 0.0;                //Head Z Negatitve 리밋
        double HeadZ_Positive = 0.0;                //Head Z Positve 리밋
        double HeadT_Negative = 0.0;                //Head T Negatitve 리밋
        double HeadT_Positive = 0.0;                //Head T Positve 리밋
        double RelativePosition = 0.0;              //RelativePosition Value
        bool SelectJogRelative;                     //Jog-Relative 토글
        int FSpeedLevel;                            //Speed
        private int StageIndexX = 0;
        private int StageIndexY = 0;
        #endregion

        public enum eGridValue
        {
            VacuumDelay,
            BlowDelay,
            StepPlaceSpeed,
            Space1,
            MovingDelayX,
            MovingDelayY,
            MovingDelayZ,
            MovingDelayT,
            Space2,
            ReadyPosX,
            ReadyPosY,
            ReadyPosZ,
            ReadyPosT,
            Space3,
            StagePickPosX,
            StagePickPosY,
            StagePickPosZ,
            StagePickPosT,
            Space4,
            IndexPlacePosX,
            IndexPlacePosY,
            IndexPlacePosZ,
            IndexStepPlaceOffset,
            ClampPlacePosZ,
            IndexPlacePosT,
        }

        private enum eAxis
        {
            HeadX,
            HeadY,
            HeadZ,
            HeadT,
        }


        public FormPageWorkingVCMPicker()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 1000);
        }
        private void FormPageWorkingVCMPicker_Load(object sender, EventArgs e)
        {
            Left = 131;
            Top = 60;
            Grid_Init();
            Grid_Update();
            FSpeedLevel = 0;
            FJogMouseDown = true;
            SelectJogRelative = true;
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

                btnSelectJog.Text = SelectJogRelative ? cDEF.Lang.Trans("JOG") : cDEF.Lang.Trans("RELATIVE");
                btnSelectJog.ForeColor = SelectJogRelative ? Color.Red : Color.Blue;
                lbRelative_Position.Enabled = SelectJogRelative ? false : true;

                lbHeadX_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadX].ActualPosition, true);
                lbHeadY_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadY].ActualPosition, true);
                lbHeadZ_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadZ].ActualPosition, true);
                lbHeadT_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadT].ActualPosition, true);

                lbSelectStageTrayX.Text = StageIndexX.ToString();
                lbSelectStageTrayY.Text = StageIndexY.ToString();

                lbRelative_Position.Text = cFnc.GetUnitValueString((int)RelativePosition, true);

                if (Convert.ToBoolean(cDEF.Run.VCMPicker.HeadX.FAlarm))
                {
                    lbX.BackColor = Color.Red;
                }
                else
                {
                    lbX.BackColor = Convert.ToBoolean(cDEF.Run.VCMPicker.HeadX.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.VCMPicker.HeadY.FAlarm))
                {
                    lbY.BackColor = Color.Red;
                }
                else
                {
                    lbY.BackColor = Convert.ToBoolean(cDEF.Run.VCMPicker.HeadY.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.VCMPicker.HeadZ.FAlarm))
                {
                    lbZ.BackColor = Color.Red;
                }
                else
                {
                    lbZ.BackColor = Convert.ToBoolean(cDEF.Run.VCMPicker.HeadZ.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.VCMPicker.HeadT.FAlarm))
                {
                    lbT.BackColor = Color.Red;
                }
                else
                {
                    lbT.BackColor = Convert.ToBoolean(cDEF.Run.VCMPicker.HeadT.FInposition) ? Color.White : Color.Lime;
                }

                //Position Check
                lbReadyPositionXY.BackColor = cDEF.Run.VCMPicker.Is_Head_ReadyPositionX() && cDEF.Run.VCMPicker.Is_Head_ReadyPositionY() ? Color.Lime : Color.White;
                lbIndexPlacePositionXY.BackColor = cDEF.Run.VCMPicker.Is_Head_IndexPlacePositionX() && cDEF.Run.VCMPicker.Is_Head_IndexPlacePositionY() ? Color.Lime : Color.White;
                lbStagePickPositionXY.BackColor = cDEF.Run.VCMPicker.Is_Head_StageFirstPickPositionX() && cDEF.Run.VCMPicker.Is_Head_StagePickPositionY() && cDEF.Run.VCMPicker.Is_Stage_FirstPickPositionY() ? Color.Lime : Color.White;

                lbReadyPositionZ.BackColor = cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ() ? Color.Lime : Color.White;
                lbIndexPlacePositionZ.BackColor = cDEF.Run.VCMPicker.Is_Head_IndexPlacePositionZ() ? Color.Lime : Color.White;
                lbStagePickPositionZ.BackColor = cDEF.Run.VCMPicker.Is_Head_StagePickPositionZ() ? Color.Lime : Color.White;
                lbClampPlacePositionZ.BackColor = cDEF.Run.VCMPicker.Is_Head_ClampPlacePositionZ() ? Color.Lime : Color.White;

                lbReadyPositionT.BackColor = cDEF.Run.VCMPicker.Is_Head_ReadyPositionT() ? Color.Lime : Color.White;
                lbIndexPlacePositionT.BackColor = cDEF.Run.VCMPicker.Is_Head_IndexPlacePositionT() ? Color.Lime : Color.White;
                lbStagePickPositionT.BackColor = cDEF.Run.VCMPicker.Is_Head_StagePickPositionT() ? Color.Lime : Color.White;

                

                //Cylinder Status
                lbVCMClamp.BackColor = cDEF.Run.VCMPicker.VCMClamp.IsForward() ? Color.Lime : Color.White;
                lbVCMUnclamp.BackColor = cDEF.Run.VCMPicker.VCMClamp.IsBackward() ? Color.Lime : Color.White;

                //IO Check
                lbVacuumCheck.BackColor = cDEF.Run.Digital.Input[cDI.VCMLoading_Vacuum_Check] ? Color.Orange : Color.White;
                btnVacuum.ForeColor = cDEF.Run.Digital.Output[cDO.VCM_Loading_Vacuum] ? Color.Red : Color.Black;
                btnBlow.ForeColor = cDEF.Run.Digital.Output[cDO.VCM_Loading_Blow] ? Color.Blue : Color.Black;
                lbVCMClamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.VCM_Clamp_Clamp] ? Color.Orange : Color.White;
                lbVCMUnclamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.VCM_Clamp_UnClamp] ? Color.Orange : Color.White;
            }));
        }

        #region GridUpdate
        private void Grid_Init()
        {
            GridAdd("R", "Vacuum Delay", "Time");
            GridAdd("R", "Blow Delay", "Time");
            GridAdd("R", "Step Place Speed", "Speed");
            GridAdd_Space();
            GridAdd("R", "Moving Delay X", "Head X");
            GridAdd("R", "Moving Delay Y", "Head Y");
            GridAdd("R", "Moving Delay Z", "Head Z");
            GridAdd("R", "Moving Delay T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Ready Position X", "Head X");
            GridAdd("R", "Ready Position Y", "Head Y");
            GridAdd("R", "Ready Position Z", "Head Z");
            GridAdd("R", "Ready Position T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Stage First Pick Position X", "Head X");
            GridAdd("R", "Stage Pick Position Y", "Head Y");
            GridAdd("R", "Stage Pick Position Z", "Head Z");
            GridAdd("R", "Stage Pick Position T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Index Place Position X", "Head X");
            GridAdd("R", "Index Place Position Y", "Head Y");
            GridAdd("R", "Index Place Position Z", "Head Z");
            GridAdd("R", "Index Step Place Offset", "Head Z");
            GridAdd("R", "Clamp Place Position Z", "Head Z");
            GridAdd("R", "Index Place Position T", "Head T");


        }

        private void Grid_Update()
        {
            for (int i = 0; i < MotionDataGrid.RowCount; i++)
            {
                switch ((eGridValue)i)
                {
                    case eGridValue.ReadyPosX:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosX].Value = ((double)cDEF.Work.TeachVCMPicker.ReadyPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosY:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosY].Value = ((double)cDEF.Work.TeachVCMPicker.ReadyPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosZ:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosZ].Value = ((double)cDEF.Work.TeachVCMPicker.ReadyPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosT:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosT].Value = ((double)cDEF.Work.TeachVCMPicker.ReadyPositionT / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.StagePickPosX:
                        MotionDataGrid[3, (int)eGridValue.StagePickPosX].Value = ((double)cDEF.Work.TeachVCMPicker.StageFirstPickPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePickPosY:
                        MotionDataGrid[3, (int)eGridValue.StagePickPosY].Value = ((double)cDEF.Work.TeachVCMPicker.StagePickPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePickPosZ:
                        MotionDataGrid[3, (int)eGridValue.StagePickPosZ].Value = ((double)cDEF.Work.TeachVCMPicker.StagePickPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePickPosT:
                        MotionDataGrid[3, (int)eGridValue.StagePickPosT].Value = ((double)cDEF.Work.TeachVCMPicker.StagePickPositionT / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.IndexPlacePosX:
                        MotionDataGrid[3, (int)eGridValue.IndexPlacePosX].Value = ((double)cDEF.Work.TeachVCMPicker.IndexPlacePositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPlacePosY:
                        MotionDataGrid[3, (int)eGridValue.IndexPlacePosY].Value = ((double)cDEF.Work.TeachVCMPicker.IndexPlacePositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPlacePosZ:
                        MotionDataGrid[3, (int)eGridValue.IndexPlacePosZ].Value = ((double)cDEF.Work.TeachVCMPicker.IndexPlacePositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexStepPlaceOffset:
                        MotionDataGrid[3, (int)eGridValue.IndexStepPlaceOffset].Value = ((double)cDEF.Work.TeachVCMPicker.IndexStepPlaceOffset / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ClampPlacePosZ:
                        MotionDataGrid[3, (int)eGridValue.ClampPlacePosZ].Value = ((double)cDEF.Work.TeachVCMPicker.ClampPlacePositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPlacePosT:
                        MotionDataGrid[3, (int)eGridValue.IndexPlacePosT].Value = ((double)cDEF.Work.TeachVCMPicker.IndexPlacePositionT / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.MovingDelayX:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayX].Value = ((double)cDEF.Work.VCMPicker.MovingDelayX / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayY:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayY].Value = ((double)cDEF.Work.VCMPicker.MovingDelayY / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayZ:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayZ].Value = ((double)cDEF.Work.VCMPicker.MovingDelayZ / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayT:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayT].Value = ((double)cDEF.Work.VCMPicker.MovingDelayT / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.VacuumDelay:
                        MotionDataGrid[3, (int)eGridValue.VacuumDelay].Value = ((double)cDEF.Work.VCMPicker.VCMVacDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.BlowDelay:
                        MotionDataGrid[3, (int)eGridValue.BlowDelay].Value = ((double)cDEF.Work.VCMPicker.VCMBlowDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.StepPlaceSpeed:
                        MotionDataGrid[3, (int)eGridValue.StepPlaceSpeed].Value = ((double)cDEF.Work.VCMPicker.StepPlaceSpeed / 100.0).ToString() + " %";
                        break;

                }

            }
        }
        private void GridAdd(string section, string name, string unit)
        {

            string[] str = { $"{section}",$"{name}", $"{unit}", $"" };
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


            HeadX_Negative = cDEF.Run.VCMPicker.HeadX.Config.FLimit.FSoftwareNegative;
            HeadX_Positive = cDEF.Run.VCMPicker.HeadX.Config.FLimit.FSoftwarePositive;

            HeadY_Negative = cDEF.Run.VCMPicker.HeadY.Config.FLimit.FSoftwareNegative;
            HeadY_Positive = cDEF.Run.VCMPicker.HeadY.Config.FLimit.FSoftwarePositive;

            HeadZ_Negative = cDEF.Run.VCMPicker.HeadZ.Config.FLimit.FSoftwareNegative;
            HeadZ_Positive = cDEF.Run.VCMPicker.HeadZ.Config.FLimit.FSoftwarePositive;

            HeadT_Negative = cDEF.Run.VCMPicker.HeadT.Config.FLimit.FSoftwareNegative;
            HeadT_Positive = cDEF.Run.VCMPicker.HeadT.Config.FLimit.FSoftwarePositive;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            eGridValue eGrid = (eGridValue)Row;
            if (Col == 3)
            {
                switch (eGrid)
                {
                    case eGridValue.ReadyPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.ReadyPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position X", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Ready Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.ReadyPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 0, str);
                        }
                        break;

                    case eGridValue.ReadyPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.ReadyPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Ready Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.ReadyPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 1, str);
                        }
                        break;

                    case eGridValue.ReadyPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Ready Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.ReadyPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.ReadyPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 2, str);
                        }
                        break;

                    case eGridValue.ReadyPosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.ReadyPositionT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position T", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadT.ActualPosition / 1000.0, HeadT_Negative / 1000.0, HeadT_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Ready Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.ReadyPositionT / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.ReadyPositionT = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 3, str);
                        }
                        break;

                    case eGridValue.StagePickPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.StageFirstPickPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage First Pick Position X", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Stage First Pick Position Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.StageFirstPickPositionX / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.StageFirstPickPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 4, str);
                        }
                        break;

                    case eGridValue.StagePickPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.StagePickPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Pick Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Stage Pick Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.StagePickPositionY / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.StagePickPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 5, str);
                        }
                        break;

                    case eGridValue.StagePickPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.StagePickPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Pick Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Ready Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.StagePickPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.StagePickPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 6, str);
                        }
                        break;

                    case eGridValue.StagePickPosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.StagePickPositionT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Pick Position T", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadT.ActualPosition / 1000.0, HeadT_Negative / 1000.0, HeadT_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Stage Pick Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.StagePickPositionT / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.StagePickPositionT = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 7, str);
                        }
                        break;

                    case eGridValue.IndexPlacePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.IndexPlacePositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position X", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Index Place Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.IndexPlacePositionX / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.IndexPlacePositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 8, str);
                        }
                        break;

                    case eGridValue.IndexPlacePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.IndexPlacePositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Index Place Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.IndexPlacePositionY / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.IndexPlacePositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 9, str);
                        }
                        break;

                    case eGridValue.IndexPlacePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.IndexPlacePositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Index Place Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.IndexPlacePositionZ / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.IndexPlacePositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 10, str);
                        }
                        break;

                    case eGridValue.IndexStepPlaceOffset:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.IndexStepPlaceOffset) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Step Place Offset Z", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadZ.ActualPosition / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Index Step Place Offset Z {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.IndexStepPlaceOffset / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.IndexStepPlaceOffset = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 10, str);
                        }
                        break;

                    case eGridValue.ClampPlacePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.ClampPlacePositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Clamp Place Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Clamp Place Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.ClampPlacePositionZ / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.ClampPlacePositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 10, str);
                        }
                        break;

                    case eGridValue.IndexPlacePosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMPicker.IndexPlacePositionT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position T", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.HeadT.ActualPosition / 1000.0, HeadT_Negative / 1000.0, HeadT_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Index Place Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMPicker.IndexPlacePositionT / 1000.0, DValue);
                            cDEF.Work.TeachVCMPicker.IndexPlacePositionT = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 11, str);
                        }
                        break;

                    case eGridValue.MovingDelayX:
                        DValue = Convert.ToDouble(cDEF.Work.VCMPicker.MovingDelayX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay X", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Moving Delay X {0:0.000} to {1:0.000}", cDEF.Work.VCMPicker.MovingDelayX / 1000.0, DValue);
                            cDEF.Work.VCMPicker.MovingDelayX = (int)(DValue * 1000.0);
                            cDEF.Work.VCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 12, str);
                        }
                        break;

                    case eGridValue.MovingDelayY:
                        DValue = Convert.ToDouble(cDEF.Work.VCMPicker.MovingDelayY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay Y", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Moving Delay Y {0:0.000} to {1:0.000}", cDEF.Work.VCMPicker.MovingDelayY / 1000.0, DValue);
                            cDEF.Work.VCMPicker.MovingDelayY = (int)(DValue * 1000.0);
                            cDEF.Work.VCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 13, str);
                        }
                        break;

                    case eGridValue.MovingDelayZ:
                        DValue = Convert.ToDouble(cDEF.Work.VCMPicker.MovingDelayZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay Z", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Moving Delay Z {0:0.000} to {1:0.000}", cDEF.Work.VCMPicker.MovingDelayZ / 1000.0, DValue);
                            cDEF.Work.VCMPicker.MovingDelayZ = (int)(DValue * 1000.0);
                            cDEF.Work.VCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 14, str);
                        }
                        break;

                    case eGridValue.MovingDelayT:
                        DValue = Convert.ToDouble(cDEF.Work.VCMPicker.MovingDelayT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay T", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Moving Delay T {0:0.000} to {1:0.000}", cDEF.Work.VCMPicker.MovingDelayT / 1000.0, DValue);
                            cDEF.Work.VCMPicker.MovingDelayT = (int)(DValue * 1000.0);
                            cDEF.Work.VCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 15, str);
                        }
                        break;

                    case eGridValue.VacuumDelay:
                        DValue = Convert.ToDouble(cDEF.Work.VCMPicker.VCMVacDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Vacuum Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Picker] Vacuum Delay {0:0.000} to {1:0.000}", cDEF.Work.VCMPicker.VCMVacDelay / 1000.0, DValue);
                            cDEF.Work.VCMPicker.VCMVacDelay = (int)(DValue * 1000.0);
                            cDEF.Work.VCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 16, str);
                        }
                        break;

                    case eGridValue.BlowDelay:
                        DValue = Convert.ToDouble(cDEF.Work.VCMPicker.VCMBlowDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Blow Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Picker] Blow Delay {0:0.000} to {1:0.000}", cDEF.Work.VCMPicker.VCMBlowDelay / 1000.0, DValue);
                            cDEF.Work.VCMPicker.VCMBlowDelay = (int)(DValue * 1000.0);
                            cDEF.Work.VCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 17, str);
                        }
                        break;

                    case eGridValue.StepPlaceSpeed:
                        Value = cDEF.Work.VCMPicker.StepPlaceSpeed;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Head Index Step Place Speed Z", ref Value, " %", 0, 100))
                            return;
                        {
                            str = String.Format("[VCM Picker] Head Index Step Place Speed Z {0:0.000} to {1:0.000}", cDEF.Work.VCMPicker.StepPlaceSpeed, Value);
                            cDEF.Work.VCMPicker.StepPlaceSpeed = Value;
                            cDEF.Work.VCMPicker.Save();
                            cDEF.Run.LogData(cLog.Form_VCMPicker_Data + 17, str);
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

            if (FJogMouseDown)
            {
                FJogMouseDown = false;

                int i, cnt;
                cnt = cDEF.Run.VCMPicker.MotionCount;
                for (i = 0; i < cnt; i++)
                    cDEF.Run.VCMPicker.Motions[i].Stop();
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
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 3:
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 4:
                        FDirection = 0;
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 5:
                        FDirection = 1;
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 6:
                        FDirection = 0;
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadT].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 7:
                        FDirection = 1;
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadT].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;


                }
            }
            else
            {
                switch (FTag)
                {
                    case 0:
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadX].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadX].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadY].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 3:
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadY].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 4:
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadZ].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 5:
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadZ].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 6:
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadT].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 7:
                        cDEF.Run.VCMPicker.Motions[(int)eAxis.HeadT].Relative(RelativePosition, FSpeedLevel);
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
                    btnSpeed.Text = cDEF.Lang.Trans("SLOW");
                    btnSpeed.ForeColor = Color.Blue;
                    break;

                case 100:
                    btnSpeed.Text = cDEF.Lang.Trans("FAST");
                    btnSpeed.ForeColor = Color.Red;
                    break;

                default:
                    btnSpeed.Text = cDEF.Lang.Trans("SPEED") + $":{(FSpeedLevel)}%";
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
                case "btnReadyPositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Ready Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_ReadyPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 1, "[VCM Picker] Move Head Ready Position Z.");
                    }
                    break;
                case "btnStagePickPositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Stage Pick Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_StagePickPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 2, "[VCM Picker] Move Head Stage Pick Position Z.");
                    }
                    break;
                case "btnIndexPlacePositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Index Place Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_IndexPlacePositionZ();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 3, "[VCM Picker] Move Head Index Place Position Z.");
                    }
                    break;
                case "btnReadyPositionXY":
                    if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Ready Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_ReadyPositionX();
                        cDEF.Run.VCMPicker.Move_Head_ReadyPositionY();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 4, "[VCM Picker] Move Head Ready Position XY.");
                    }
                    break;
                case "btnStagePickPositionXY":
                    if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer X is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Stage Pick Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_StagePickPositionX();
                        cDEF.Run.VCMPicker.Move_Head_StagePickPositionY();
                        cDEF.Run.VCMPicker.Move_Stage_FirstPickPosition();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 5, "[VCM Picker] Move Head Stage Pick Position XY.");
                    }
                    break;
                case "btnIndexPlacePositionXY":
                    if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Index Place Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_IndexPlacePositionX();
                        cDEF.Run.VCMPicker.Move_Head_IndexPlacePositionY();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 6, "[VCM Picker] Move Head Index Place Position XY.");
                    }
                    break;
                case "btnReadyPositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Ready Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_ReadyPositionT();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 7, "[VCM Picker] Move Head Ready Position T.");
                    }
                    break;
                case "btnStagePickPositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Stage Pick Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_StagePickPositionT();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 8, "[VCM Picker] Move Head Stage Pick Position T.");
                    }
                    break;
                case "btnIndexPlacePositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Index Place Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_IndexPlacePositionT();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 9, "[VCM Picker] Move Head Index Place Position T.");
                    }
                    break;
                case "btnStageWorkXY":
                    if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer X is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Stage Work Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_WorkingPositionXY(StageIndexX, StageIndexY);
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[VCM Picker] Move VCM Picker Head Stage Work Position XY.");
                    }
                    break;

                case "btnClampPositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Clamp Place Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Head_ClampPlacePositionZ();
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 3, "[VCM Picker] Move Head Clamp Place Position Z.");
                    }
                    break;

                //case "btnIndexStepPlaceZ":
                //    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Picker Head Index Step Place Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                //    {
                //        cDEF.Run.VCMPicker.Move_Head_IndexStepPlacePositionZ();
                //        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 3, "[VCM Picker] Move Head Index Step Place Position Z.");
                //    }
                //    break;

            }
        }

        private void lbTrayIndex_Click(object sender, EventArgs e)
        {
            int StageX = 0;
            int StageY = 0;

            switch ((sender as Label).Name)
            {
                case "lbSelectStageTrayX":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Tray Index", ref StageX, "", 0, cDEF.Work.VCMLoader.TrayCountX - 1))
                        return;
                    StageIndexX = StageX;
                    break;

                case "lbSelectStageTrayY":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Tray Index", ref StageY, "", 0, cDEF.Work.VCMLoader.TrayCountY - 1))
                        return;
                    StageIndexY = StageY;
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
                case "btnPick":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Pick?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_VCMPick;
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 100, "[Semi Auto] Pick.");
                    }
                    break;
                case "btnPlace":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Place?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_VCMPlace;
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 100, "[Semi Auto] Place.");
                    }
                    break;
            }
        }

        public void ChangeLanguage()
        {
            lbGridTitle.Text = cDEF.Lang.Trans("SETTING");
            lbImageTitle.Text = cDEF.Lang.Trans("IMAGE");
            lbJogTitle.Text = cDEF.Lang.Trans("JOG");
            lbHeadXYTitle.Text = cDEF.Lang.Trans("HEAD XY");
            lbHeadZTitle.Text = cDEF.Lang.Trans("HEAD Z");
            lbHeadTTitle.Text = cDEF.Lang.Trans("HEAD T");
            lbSemiAutoTitle.Text = cDEF.Lang.Trans("SEMI AUTO");
            lbSpeed.Text = cDEF.Lang.Trans("Speed");
            btnSpeed.Text = cDEF.Lang.Trans("SPEED");
            lbCurrentPositionXY.Text = cDEF.Lang.Trans("Current Position");
            lbCurrentPositionT.Text = cDEF.Lang.Trans("Current Position");
            lbCurrentPositionZ.Text = cDEF.Lang.Trans("Current Position");
            btnVacuum.Text = cDEF.Lang.Trans("VACUUM");
            btnBlow.Text = cDEF.Lang.Trans("BLOW");
            btnReadyPositionXY.Text = cDEF.Lang.Trans("READY POSITION XY");
            btnStagePickPositionXY.Text = cDEF.Lang.Trans("STAGE PICK POSITION XY");
            btnIndexPlacePositionXY.Text = cDEF.Lang.Trans("INDEX PLACE POSITION XY");
            btnReadyPositionZ.Text = cDEF.Lang.Trans("READY POSITION Z");
            btnStagePickPositionZ.Text = cDEF.Lang.Trans("STAGE PICK POSITION Z");
            btnIndexPlacePositionZ.Text = cDEF.Lang.Trans("INDEX PLACE POSITION Z");
            btnReadyPositionT.Text = cDEF.Lang.Trans("READY POSITION T");
            btnStagePickPositionT.Text = cDEF.Lang.Trans("STAGE PICK POSITION T");
            btnIndexPlacePositionT.Text = cDEF.Lang.Trans("INDEX PLACE POSITION T");

        }

        private void FormPageWorkingVCMPicker_VisibleChanged(object sender, EventArgs e)
        {
            Grid_Update();
            ChangeLanguage();
        }

        private void btnVacEjector_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            if (FName == "btnVacuum")
            {
                cDEF.Run.Digital.Output[cDO.VCM_Loading_Vacuum] = !cDEF.Run.Digital.Output[cDO.VCM_Loading_Vacuum];
                if (cDEF.Run.Digital.Output[cDO.VCM_Loading_Vacuum])
                    cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 10, "[VCM Picker] Vacuum On");
                else
                    cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 11, "[VCM Picker] Vacuum Off");
            }
            else
            {
                cDEF.Run.Digital.Output[cDO.VCM_Loading_Blow] = !cDEF.Run.Digital.Output[cDO.VCM_Loading_Blow];
                if (cDEF.Run.Digital.Output[cDO.VCM_Loading_Blow])
                    cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 12, "[VCM Picker] Blow On");
                else
                    cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 13, "[VCM Picker] Blow Off");
            }
        }

        private void btnVCMClamp_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            if (FName == "btnVCMClamp")
            {
                cDEF.Run.VCMPicker.VCMClamp.Forward();
                cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 14, "[VCM Picker] VCM Clamp Clamp");
            }
            else
            {
                cDEF.Run.VCMPicker.VCMClamp.Backward();
                cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 15, "[VCM Picker] VCM Clamp Unclamp");
            }
        }
    }
}
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using XModule.Standard;

namespace XModule.Forms.FormWorking
{
    public partial class FormPageWorkingUnloaderPicker : TFrame
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
        private int NGTrayIndexX = 0;
        private int NGTrayIndexY = 0;
        #endregion

        public enum eGridValue
        {
            VacuumDelay,
            BlowDelay,
            StepPlaceSpeed,
            NGStepPlaceSpeed,
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
            StagePlacePosX,
            StagePlacePosY,
            StagePlacePosZ,
            StageStepPlaceOffset,
            StagePlacePosT,
            Space4,
            IndexPickPosX,
            IndexPickPosY,
            IndexPickPosZ,
            IndexPickPosT,
            Space5,
            NGTrayPlacePosX,
            //NGTrayPlacePosY,
            NGTrayPlacePosZ,
            NGTrayStepPlaceOffset,
            NGTrayPlacePosT,
            Space6,
            AvoidPositionX,
            AvoidPositionY,
        }

        private enum eAxis
        {
            HeadX,
            HeadY,
            HeadZ,
            HeadT
            
        }


        public FormPageWorkingUnloaderPicker()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 1000);
        }
        private void FormPageWorkingUnloaderPicker_Load(object sender, EventArgs e)
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

                btnSelectJog.Text = SelectJogRelative ? "JOG" : "RELATIVE";
                btnSelectJog.ForeColor = SelectJogRelative ? Color.Red : Color.Blue;
                lbRelative_Position.Enabled = SelectJogRelative ? false : true;


                lbHeadZ_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadZ].ActualPosition, true);
                lbHeadY_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadY].ActualPosition, true);
                lbHeadX_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadX].ActualPosition, true);
                lbHeadT_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadT].ActualPosition, true);

                lbRelative_Position.Text = cFnc.GetUnitValueString((int)RelativePosition, true);

                lbSelectStageTrayX.Text = StageIndexX.ToString();
                lbSelectStageTrayY.Text = StageIndexY.ToString();
                lbSelectNGTrayX.Text = NGTrayIndexX.ToString();
                lbSelectNGTrayY.Text = NGTrayIndexY.ToString();

                if (Convert.ToBoolean(cDEF.Run.UnloadPicker.HeadX.FAlarm))
                {
                    lbX.BackColor = Color.Red;
                }
                else
                {
                    lbX.BackColor = Convert.ToBoolean(cDEF.Run.UnloadPicker.HeadX.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.UnloadPicker.HeadY.FAlarm))
                {
                    lbY.BackColor = Color.Red;
                }
                else
                {
                    lbY.BackColor = Convert.ToBoolean(cDEF.Run.UnloadPicker.HeadY.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.UnloadPicker.HeadZ.FAlarm))
                {
                    lbZ.BackColor = Color.Red;
                }
                else
                {
                    lbZ.BackColor = Convert.ToBoolean(cDEF.Run.UnloadPicker.HeadZ.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.UnloadPicker.HeadT.FAlarm))
                {
                    lbT.BackColor = Color.Red;
                }
                else
                {
                    lbT.BackColor = Convert.ToBoolean(cDEF.Run.UnloadPicker.HeadT.FInposition) ? Color.White : Color.Lime;
                }
                //Position Check
                //lbFlux1CleanXY.BackColor = cDEF.Run.Head.IsMove_Flux1_CleanPositionXY() ? Color.Lime : Color.White;
                lbReadyPositionXY.BackColor = cDEF.Run.UnloadPicker.Is_Head_ReadyPositionX() && cDEF.Run.UnloadPicker.Is_Head_ReadyPositionY() ? Color.Lime : Color.White;
                lbIndexPickPositionXY.BackColor = cDEF.Run.UnloadPicker.Is_Head_IndexPickPositionX() && cDEF.Run.UnloadPicker.Is_Head_IndexPickPositionY() ? Color.Lime : Color.White;
                lbStagePlacePositionXY.BackColor = cDEF.Run.UnloadPicker.Is_Head_StageFirstPlacePositionX() && cDEF.Run.UnloadPicker.Is_Head_StagePlacePositionY() ? Color.Lime : Color.White;

                //lbReadyPositionY.BackColor = cDEF.Run.UnloadPicker.Is_Head_ReadyPositionY() ? Color.Lime : Color.White;
                //lbIndexPickPositionY.BackColor = cDEF.Run.UnloadPicker.Is_Head_IndexPickPositionY() ? Color.Lime : Color.White;
                //lbStagePlacePositionY.BackColor = cDEF.Run.UnloadPicker.Is_Head_StagePlacePositionY() ? Color.Lime : Color.White;

                lbReadyPositionZ.BackColor = cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ() ? Color.Lime : Color.White;
                lbIndexPickPositionZ.BackColor = cDEF.Run.UnloadPicker.Is_Head_IndexPickPositionZ() ? Color.Lime : Color.White;
                lbStagePlacePositionZ.BackColor = cDEF.Run.UnloadPicker.Is_Head_StagePlacePositionZ() ? Color.Lime : Color.White;

                lbReadyPositionT.BackColor = cDEF.Run.UnloadPicker.Is_Head_ReadyPositionT() ? Color.Lime : Color.White;
                lbIndexPickPositionT.BackColor = cDEF.Run.UnloadPicker.Is_Head_IndexPickPositionT() ? Color.Lime : Color.White;
                lbStagePlacePositionT.BackColor = cDEF.Run.UnloadPicker.Is_Head_StagePlacePositionT() ? Color.Lime : Color.White;

                //IO Check
                lbVacuumCheck.BackColor = cDEF.Run.Digital.Input[cDI.VCMUnloading_Vacuum_Check] ? Color.Orange : Color.White;
                btnVacuum.ForeColor = cDEF.Run.Digital.Output[cDO.VCM_Unloading_Vacuum] ? Color.Red : Color.Black;
                btnBlow.ForeColor = cDEF.Run.Digital.Output[cDO.VCM_Unloading_Blow] ? Color.Blue : Color.Black;
            }));
        }

        #region GridUpdate
        private void Grid_Init()
        {
            GridAdd("R", "Vacuum Delay", "Time");
            GridAdd("R", "Blow Delay", "Time");
            GridAdd("R", "Step Place Speed", "Speed");
            GridAdd("R", "NG Tray Step Place Speed", "Speed");
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
            GridAdd("R", "Stage First Place Position X", "Head X");
            GridAdd("R", "Stage Place Position Y", "Head Y");
            GridAdd("R", "Stage Place Position Z", "Head Z");
            GridAdd("R", "Stage Step Place Offset", "Head Z");
            GridAdd("R", "Stage Place Position T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Index Pick Position X", "Head X");
            GridAdd("R", "Index Pick Position Y", "Head Y");
            GridAdd("R", "Index Pick Position Z", "Head Z");
            GridAdd("R", "Index Pick Position T", "Head T");
            GridAdd_Space();
            GridAdd("R", "NG Tray Place Position X", "Head X");
            //GridAdd("T", "NG Tray Place Position Y", "Head Y");
            GridAdd("R", "NG Tray Place Position Z", "Head Z");
            GridAdd("R", "NG Tray Step Place Offset", "Head Z");
            GridAdd("R", "NG Tray Place Position T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Avoid Position X", "Head X");
            GridAdd("R", "Avoid Position Y", "Head Y");
        }

        private void Grid_Update()
        {
            for (int i = 0; i < MotionDataGrid.RowCount; i++)
            {
                switch((eGridValue)i)
                {
                    case eGridValue.ReadyPosX:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosX].Value = ((double)cDEF.Work.TeachUnloadPicker.ReadyPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosY:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosY].Value = ((double)cDEF.Work.TeachUnloadPicker.ReadyPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosZ:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosZ].Value = ((double)cDEF.Work.TeachUnloadPicker.ReadyPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosT:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosT].Value = ((double)cDEF.Work.TeachUnloadPicker.ReadyPositionT / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.StagePlacePosX:
                        MotionDataGrid[3, (int)eGridValue.StagePlacePosX].Value = ((double)cDEF.Work.TeachUnloadPicker.StageFirstPlacePositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePlacePosY:
                        MotionDataGrid[3, (int)eGridValue.StagePlacePosY].Value = ((double)cDEF.Work.TeachUnloadPicker.StagePlacePositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePlacePosZ:
                        MotionDataGrid[3, (int)eGridValue.StagePlacePosZ].Value = ((double)cDEF.Work.TeachUnloadPicker.StagePlacePositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StageStepPlaceOffset:
                        MotionDataGrid[3, (int)eGridValue.StageStepPlaceOffset].Value = ((double)cDEF.Work.TeachUnloadPicker.StageStepPlaceOffset / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePlacePosT:
                        MotionDataGrid[3, (int)eGridValue.StagePlacePosT].Value = ((double)cDEF.Work.TeachUnloadPicker.StagePlacePositionT / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.IndexPickPosX:
                        MotionDataGrid[3, (int)eGridValue.IndexPickPosX].Value = ((double)cDEF.Work.TeachUnloadPicker.IndexPickPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPickPosY:
                        MotionDataGrid[3, (int)eGridValue.IndexPickPosY].Value = ((double)cDEF.Work.TeachUnloadPicker.IndexPickPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPickPosZ:
                        MotionDataGrid[3, (int)eGridValue.IndexPickPosZ].Value = ((double)cDEF.Work.TeachUnloadPicker.IndexPickPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPickPosT:
                        MotionDataGrid[3, (int)eGridValue.IndexPickPosT].Value = ((double)cDEF.Work.TeachUnloadPicker.IndexPickPositionT / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.MovingDelayX:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayX].Value = ((double)cDEF.Work.UnloadPicker.MovingDelayX / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayY:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayY].Value = ((double)cDEF.Work.UnloadPicker.MovingDelayY / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayZ:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayZ].Value = ((double)cDEF.Work.UnloadPicker.MovingDelayZ / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayT:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayT].Value = ((double)cDEF.Work.UnloadPicker.MovingDelayT / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.VacuumDelay:
                        MotionDataGrid[3, (int)eGridValue.VacuumDelay].Value = ((double)cDEF.Work.UnloadPicker.UnloaderVacDelay / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.BlowDelay:
                        MotionDataGrid[3, (int)eGridValue.BlowDelay].Value = ((double)cDEF.Work.UnloadPicker.UnloaderBlowDelay / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.StepPlaceSpeed:
                        MotionDataGrid[3, (int)eGridValue.StepPlaceSpeed].Value = ((double)cDEF.Work.UnloadPicker.StepPlaceSpeed).ToString("N3") + " %";
                        break;
                    case eGridValue.NGStepPlaceSpeed:
                        MotionDataGrid[3, (int)eGridValue.NGStepPlaceSpeed].Value = ((double)cDEF.Work.UnloadPicker.NgStepPlaceSpeed).ToString("N3") + " %";
                        break;

                    case eGridValue.NGTrayPlacePosX:
                        MotionDataGrid[3, (int)eGridValue.NGTrayPlacePosX].Value = ((double)cDEF.Work.TeachUnloadPicker.NGTrayPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    //case eGridValue.NGTrayPlacePosY:
                    //    MotionDataGrid[3, (int)eGridValue.NGTrayPlacePosY].Value = ((double)cDEF.Work.TeachUnloadPicker.NGTrayPositionY / 1000.0).ToString("N3") + " mm";
                    //    break;

                    case eGridValue.NGTrayPlacePosZ:
                        MotionDataGrid[3, (int)eGridValue.NGTrayPlacePosZ].Value = ((double)cDEF.Work.TeachUnloadPicker.NGTrayPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.NGTrayStepPlaceOffset:
                        MotionDataGrid[3, (int)eGridValue.NGTrayStepPlaceOffset].Value = ((double)cDEF.Work.TeachUnloadPicker.NGTrayStepPlaceOffset / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.NGTrayPlacePosT:
                        MotionDataGrid[3, (int)eGridValue.NGTrayPlacePosT].Value = ((double)cDEF.Work.TeachUnloadPicker.NGTrayPositionT / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.AvoidPositionX:
                        MotionDataGrid[3, (int)eGridValue.AvoidPositionX].Value = ((double)cDEF.Work.TeachUnloadPicker.AvoidPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.AvoidPositionY:
                        MotionDataGrid[3, (int)eGridValue.AvoidPositionY].Value = ((double)cDEF.Work.TeachUnloadPicker.AvoidPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                }
            }
        }
        private void GridAdd(string section,string name, string unit)
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


            HeadX_Negative = cDEF.Run.UnloadPicker.HeadX.Config.FLimit.FSoftwareNegative;
            HeadX_Positive = cDEF.Run.UnloadPicker.HeadX.Config.FLimit.FSoftwarePositive;

            HeadY_Negative = cDEF.Run.UnloadPicker.HeadY.Config.FLimit.FSoftwareNegative;
            HeadY_Positive = cDEF.Run.UnloadPicker.HeadY.Config.FLimit.FSoftwarePositive;

            HeadZ_Negative = cDEF.Run.UnloadPicker.HeadZ.Config.FLimit.FSoftwareNegative;
            HeadZ_Positive = cDEF.Run.UnloadPicker.HeadZ.Config.FLimit.FSoftwarePositive;

            HeadT_Negative = cDEF.Run.UnloadPicker.HeadT.Config.FLimit.FSoftwareNegative;
            HeadT_Positive = cDEF.Run.UnloadPicker.HeadT.Config.FLimit.FSoftwarePositive;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            eGridValue eGrid = (eGridValue)Row;
            if (Col == 3)
            {
                switch (eGrid)
                {
                    case eGridValue.ReadyPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.ReadyPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position X", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Ready Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.ReadyPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 0, str);
                        }
                        break;

                    case eGridValue.ReadyPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.ReadyPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Ready Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.ReadyPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 1, str);
                        }
                        break;

                    case eGridValue.ReadyPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Ready Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.ReadyPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.ReadyPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 2, str);
                        }
                        break;

                    case eGridValue.ReadyPosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.ReadyPositionT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position T", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadT.ActualPosition / 1000.0, HeadT_Negative / 1000.0, HeadT_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Ready Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.ReadyPositionT / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.ReadyPositionT = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 3, str);
                        }
                        break;

                    case eGridValue.StagePlacePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.StageFirstPlacePositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Place Position X", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Stage Place Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.StageFirstPlacePositionX / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.StageFirstPlacePositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 4, str);
                        }
                        break;

                    case eGridValue.StagePlacePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.StagePlacePositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Place Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Stage Place Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.StagePlacePositionY / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.StagePlacePositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 5, str);
                        }
                        break;

                    case eGridValue.StagePlacePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.StagePlacePositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Place Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Stage Place Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.StagePlacePositionZ / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.StagePlacePositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 6, str);
                        }
                        break;

                    case eGridValue.StageStepPlaceOffset:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.StageStepPlaceOffset) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Step Place Offset", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadZ.ActualPosition / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Stage Step Place Offset {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.StageStepPlaceOffset / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.StageStepPlaceOffset = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 6, str);
                        }
                        break;

                    case eGridValue.StagePlacePosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.StagePlacePositionT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Place Position T", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadT.ActualPosition / 1000.0, HeadT_Negative / 1000.0, HeadT_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Stage Place Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.StagePlacePositionT / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.StagePlacePositionT = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 7, str);
                        }
                        break;

                    case eGridValue.IndexPickPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.IndexPickPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Pick Position X", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Index Pick Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.IndexPickPositionX / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.IndexPickPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 8, str);
                        }
                        break;

                    case eGridValue.IndexPickPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.IndexPickPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Pick Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Index Pick Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.IndexPickPositionY / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.IndexPickPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 9, str);
                        }
                        break;

                    case eGridValue.IndexPickPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.IndexPickPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Pick Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Index Pick Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.IndexPickPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.IndexPickPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 10, str);
                        }
                        break;

                    case eGridValue.IndexPickPosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.IndexPickPositionT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Pick Position T", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadT.ActualPosition / 1000.0, HeadT_Negative / 1000.0, HeadT_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Index Pick Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.IndexPickPositionT / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.IndexPickPositionT = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 11, str);
                        }
                        break;

                    case eGridValue.MovingDelayX:
                        DValue = Convert.ToDouble(cDEF.Work.UnloadPicker.MovingDelayX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Moving Delay X", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Unload Picker] Stage Moving Delay X {0:0.000} to {1:0.000}", cDEF.Work.UnloadPicker.MovingDelayX / 1000.0, DValue);
                            cDEF.Work.UnloadPicker.MovingDelayX = (int)(DValue * 1000.0);
                            cDEF.Work.UnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 12, str);
                        }
                        break;

                    case eGridValue.MovingDelayY:
                        DValue = Convert.ToDouble(cDEF.Work.UnloadPicker.MovingDelayY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay Y", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Moving Delay Y {0:0.000} to {1:0.000}", cDEF.Work.UnloadPicker.MovingDelayY / 1000.0, DValue);
                            cDEF.Work.UnloadPicker.MovingDelayY = (int)(DValue * 1000.0);
                            cDEF.Work.UnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 13, str);
                        }
                        break;

                    case eGridValue.MovingDelayZ:
                        DValue = Convert.ToDouble(cDEF.Work.UnloadPicker.MovingDelayZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay Z", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Moving Delay Z {0:0.000} to {1:0.000}", cDEF.Work.UnloadPicker.MovingDelayZ / 1000.0, DValue);
                            cDEF.Work.UnloadPicker.MovingDelayZ = (int)(DValue * 1000.0);
                            cDEF.Work.UnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 14, str);
                        }
                        break;

                    case eGridValue.MovingDelayT:
                        DValue = Convert.ToDouble(cDEF.Work.UnloadPicker.MovingDelayT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay T", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Moving Delay T {0:0.000} to {1:0.000}", cDEF.Work.UnloadPicker.MovingDelayT / 1000.0, DValue);
                            cDEF.Work.UnloadPicker.MovingDelayT = (int)(DValue * 1000.0);
                            cDEF.Work.UnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 15, str);
                        }
                        break;

                    case eGridValue.VacuumDelay:
                        DValue = Convert.ToDouble(cDEF.Work.UnloadPicker.UnloaderVacDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Vacuum Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Unload Picker] Vacuum Delay {0:0.000} to {1:0.000}", cDEF.Work.UnloadPicker.UnloaderVacDelay / 1000.0, DValue);
                            cDEF.Work.UnloadPicker.UnloaderVacDelay = (int)(DValue * 1000.0);
                            cDEF.Work.UnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 16, str);
                        }
                        break;

                    case eGridValue.BlowDelay:
                        DValue = Convert.ToDouble(cDEF.Work.UnloadPicker.UnloaderBlowDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Blow Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Unload Picker] Blow Delay {0:0.000} to {1:0.000}", cDEF.Work.UnloadPicker.UnloaderBlowDelay / 1000.0, DValue);
                            cDEF.Work.UnloadPicker.UnloaderBlowDelay = (int)(DValue * 1000.0);
                            cDEF.Work.UnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 17, str);
                        }
                        break;

                    case eGridValue.StepPlaceSpeed:
                        Value = cDEF.Work.UnloadPicker.StepPlaceSpeed;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Stage Step Place Speed", ref Value, " %", 0, 100))
                            return;
                        {
                            str = String.Format("[Unload Picker] Stage Step Place Speed {0:0.000} to {1:0.000}", cDEF.Work.UnloadPicker.StepPlaceSpeed , Value);
                            cDEF.Work.UnloadPicker.StepPlaceSpeed = Value;
                            cDEF.Work.UnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 17, str);
                        }
                        break;

                    case eGridValue.NGStepPlaceSpeed:
                        Value = cDEF.Work.UnloadPicker.NgStepPlaceSpeed;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Ng Tray Step Place Speed", ref Value, " %", 0, 100))
                            return;
                        {
                            str = String.Format("[Unload Picker] Ng Tray Step Place Speed {0:0.000} to {1:0.000}", cDEF.Work.UnloadPicker.NgStepPlaceSpeed, Value);
                            cDEF.Work.UnloadPicker.NgStepPlaceSpeed = Value;
                            cDEF.Work.UnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 17, str);
                        }
                        break;

                    case eGridValue.NGTrayPlacePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.NGTrayPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head NG Tray Position X", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head NG Tray Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.NGTrayPositionX / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.NGTrayPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 4, str);
                        }
                        break;

                    //case eGridValue.NGTrayPlacePosY:
                    //    DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.NGTrayPositionY) / 1000;
                    //    if (!XModuleMain.frmBox.fpFloatEdit("Head NG Tray Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                    //        return;
                    //    {
                    //        str = String.Format("[Unload Picker] Head NG Tray Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.NGTrayPositionY / 1000.0, DValue);
                    //        cDEF.Work.TeachUnloadPicker.NGTrayPositionY = (int)(DValue * 1000.0);
                    //        cDEF.Work.TeachUnloadPicker.Save();
                    //        cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 5, str);
                    //    }
                    //    break;

                    case eGridValue.NGTrayPlacePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.NGTrayPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head NG Tray Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head NG Tray Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.NGTrayPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.NGTrayPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 6, str);
                        }
                        break;

                    case eGridValue.NGTrayStepPlaceOffset:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.NGTrayStepPlaceOffset) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head NG Tray Step Place Offset", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadZ.ActualPosition / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head NG Tray Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.NGTrayStepPlaceOffset / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.NGTrayStepPlaceOffset = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 6, str);
                        }
                        break;

                    case eGridValue.NGTrayPlacePosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.NGTrayPositionT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head NG Tray Position T", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadT.ActualPosition / 1000.0, HeadT_Negative / 1000.0, HeadT_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head NG Tray Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.NGTrayPositionT / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.NGTrayPositionT = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 7, str);
                        }
                        break;

                    case eGridValue.AvoidPositionX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.AvoidPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Avoid Position X", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Avoid Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.AvoidPositionX / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.AvoidPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 4, str);
                        }
                        break;

                    case eGridValue.AvoidPositionY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachUnloadPicker.AvoidPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Avoid Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.UnloadPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Unload Picker] Head Avoid Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachUnloadPicker.AvoidPositionY / 1000.0, DValue);
                            cDEF.Work.TeachUnloadPicker.AvoidPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachUnloadPicker.Save();
                            cDEF.Run.LogData(cLog.Form_UnloaderPicker_Data + 5, str);
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
                cnt = cDEF.Run.UnloadPicker.MotionCount;
                for (i = 0; i < cnt; i++)
                    cDEF.Run.UnloadPicker.Motions[i].Stop();
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
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 1:
                        FDirection = 1;
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 2:
                        FDirection = 0;
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 3:
                        FDirection = 1;
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 4:
                        FDirection = 0;
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 5:
                        FDirection = 1;
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 6:
                        FDirection = 0;
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadT].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 7:
                        FDirection = 1;
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadT].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                }
            }
            else
            {
                switch (FTag)
                {
                    case 0:
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadX].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadX].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadY].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 3:
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadY].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 4:
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadZ].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 5:
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadZ].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 6:
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadT].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 7:
                        cDEF.Run.UnloadPicker.Motions[(int)eAxis.HeadT].Relative(RelativePosition, FSpeedLevel);
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
                case "btnReadyPositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Ready Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_ReadyPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 0, "[Unload Picker] Move Head Ready Position Z.");
                    }
                    break;
                case "btnStagePlacePositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Stage Place Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (cDEF.Work.Option.PlaceOverrideUse == 0)
                            cDEF.Run.UnloadPicker.Move_Head_StagePlacePositionZ();
                        else
                            cDEF.Run.UnloadPicker.Move_Head_PlaceOverridePositionZ();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 1, "[Unload Picker] Move Head Stage Place Position Z.");
                    }
                    break;
                case "btnIndexPickPositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Index Pick Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_IndexPickPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 2, "[Unload Picker] Move Head Index Pick Position Z.");
                    }
                    break;
                case "btnReadyPositionXY":
                    if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Ready Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_ReadyPositionX();
                        cDEF.Run.UnloadPicker.Move_Head_ReadyPositionY();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 3, "[Unload Picker] Move Head Ready Position XY.");
                    }
                    break;
                case "btnStagePlacePositionXY":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Stage Place Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if(!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.Unloader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer X is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Move_Head_StageFirstPlacePositionX();
                        cDEF.Run.UnloadPicker.Move_Head_StagePlacePositionY();
                        cDEF.Run.UnloadPicker.Move_Stage_FirstPickPositionY();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 4, "[Unload Picker] Move Head Stage Place Position XY.");
                    }
                    break;
                case "btnIndexPickPositionXY":
                    if(cDEF.Run.PlateAngle.Fw_RvCylinder.IsForward())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Side Angle Measure Cylinder is Forward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Index Pick Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_IndexPickPositionX();
                        cDEF.Run.UnloadPicker.Move_Head_IndexPickPositionY();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 5, "[Unload Picker] Move Head Index Pick Position XY.");
                    }
                    break;
                case "btnReadyPositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Ready Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_ReadyPositionT();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 6, "[Unload Picker] Move Head Ready Position T.");
                    }
                    break;
                case "btnStagePlacePositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Stage Place Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_StagePlacePositionT();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 7, "[Unload Picker] Move Head Stage Place Position T.");
                    }
                    break;
                case "btnIndexPickPositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Index Pick Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_IndexPickPositionT();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Unload Picker] Move Head Index Pick Position T.");
                    }
                    break;
                case "btnNGTrayZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head NG Tray Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (cDEF.Work.Option.PlaceOverrideUse == 0)
                            cDEF.Run.UnloadPicker.Move_Head_NGTrayPositionZ();
                        else
                            cDEF.Run.UnloadPicker.Move_Head_NG_PlaceOverridePositionZ();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Unload Picker] Move Unloader Picker Head NG Tray Position Z.");
                    }
                    break;
                case "btnNGTrayT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head NG Tray Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_NGTrayPositionT();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Unload Picker] Move Unloader Picker Head NG Tray Position T.");
                    }
                    break;
                case "btnNGTrayXY":
                    if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head NG Tray Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_NGTrayPositionX();
                        //cDEF.Run.UnloadPicker.Move_Head_NGTrayPositionY(); //ksyoon, 210915
                        cDEF.Run.UnloadPicker.Move_Head_StagePlacePositionY();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Unload Picker] Move Unloader Picker Head NG Tray Position XY.");
                    }
                    break;
                case "btnStageWorkXY":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head Work Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.Unloader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer X is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Move_Head_WorkPositionXY(StageIndexX, StageIndexY);
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Unload Picker] Move Unloader Picker Head NG Tray Stage Work Position XY.");
                    }
                    break;
                case "btnNGTrayWorkXY":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unloader Picker Head NG Tray Work Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.Unloader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer X is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.UnloadPicker.Move_Head_NGTray_WorkPositionXY(NGTrayIndexX, NGTrayIndexY);
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Unload Picker] Move Unloader Picker Head NG Tray Work Position XY.");
                    }
                    break;
                case "btnAvoidXY":
                    if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Unload Picker Head Avoid Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Move_Head_AvoidPositionXY();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[VCM Picker] Move Unload Picker Head Avoid Position XY.");
                    }
                    break;
            }
        }

        private void lbTrayIndex_Click(object sender, EventArgs e)
        {
            int StageX = 0;
            int StageY = 0;
            int NGTrayX = 0;
            int NGTrayY = 0;

            switch ((sender as Label).Name)
            {
                case "lbSelectStageTrayX":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Tray Index", ref StageX, "", 0, cDEF.Work.Unloader.TrayCountX - 1))
                        return;
                    StageIndexX = StageX;
                    break;

                case "lbSelectStageTrayY":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Tray Index", ref StageY, "", 0, cDEF.Work.Unloader.TrayCountY - 1))
                            return;
                    StageIndexY = StageY;
                    break;

                case "lbSelectNGTrayX":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Tray Index", ref NGTrayX, "", 0, cDEF.Work.Unloader.NG_TrayCountX - 1))
                            return;
                    NGTrayIndexX = NGTrayX;
                    break;

                case "lbSelectNGTrayY":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Tray Index", ref NGTrayY, "", 0, 1))
                            return;
                    NGTrayIndexY = NGTrayY;
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
                    //if (cDEF.Run.PlateAngle.Fw_RvCylinder.IsForward())
                    //{
                    //    XModuleMain.frmBox.MessageBox("Warning", "Side Angle Measure Cylinder is Forward", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                    //    return;
                    //}
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Pick?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_UnloadPick;
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 100, "[Semi Auto] Pick.");
                    }
                    break;
                case "btnStagePlace":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Place?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Information.Result = true;
                        cDEF.Run.Mode = Running.TRunMode.Manual_UnloadStagePlace;
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 100, "[Semi Auto] Place.");
                    }
                    break;
                case "btnNGTrayPlace":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Place?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Information.Result = false;
                        cDEF.Run.Mode = Running.TRunMode.Manual_UnloadStagePlace;
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
            btnStagePlacePositionXY.Text = cDEF.Lang.Trans("STAGE PLACE POSITION XY");
            btnIndexPickPositionXY.Text = cDEF.Lang.Trans("INDEX PICK POSITION XY");
            btnReadyPositionZ.Text = cDEF.Lang.Trans("READY POSITION Z");
            btnStagePlacePositionZ.Text = cDEF.Lang.Trans("STAGE PLACE POSITION Z");
            btnIndexPickPositionZ.Text = cDEF.Lang.Trans("INDEX PICK POSITION Z");
            btnReadyPositionT.Text = cDEF.Lang.Trans("READY POSITION T");
            btnStagePlacePositionT.Text = cDEF.Lang.Trans("STAGE PLACE POSITION T");
            btnIndexPickPositionT.Text = cDEF.Lang.Trans("INDEX PICK POSITION T");
        }

        private void FormPageWorkingVCMPicker_VisibleChanged(object sender, EventArgs e)
        {
            Grid_Update();
        }

        private void btnVacEjector_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            if (FName == "btnVacuum")
            {
                cDEF.Run.Digital.Output[cDO.VCM_Unloading_Vacuum] = !cDEF.Run.Digital.Output[cDO.VCM_Unloading_Vacuum];
                if (cDEF.Run.Digital.Output[cDO.VCM_Unloading_Vacuum])
                    cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 9, "[Unload Picker] Vacuum On");
                else
                    cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 9, "[Unload Picker] Vacuum Off");
            }
            else
            {
                cDEF.Run.Digital.Output[cDO.VCM_Unloading_Blow] = !cDEF.Run.Digital.Output[cDO.VCM_Unloading_Blow];
                if (cDEF.Run.Digital.Output[cDO.VCM_Unloading_Blow])
                    cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 10, "[Unload Picker] Vacuum Ejector Out");
                else
                    cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 10, "[Unload Picker] Vacuum Ejector Out");
            }
        }

    }
}
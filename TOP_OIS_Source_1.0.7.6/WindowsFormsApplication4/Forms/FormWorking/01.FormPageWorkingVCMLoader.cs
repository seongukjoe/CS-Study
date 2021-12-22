using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using XModule.Standard;

namespace XModule.Forms.FormWorking
{
    public partial class FormPageWorkingVCMLoader : TFrame
    {
        #region 변수
        bool FJogMouseDown;                         //마우스 다운
        double Magazine_Negative = 0.0;                //HeadX Negaitve 리밋
        double Magazine_Positive = 0.0;                //HeadX Positve 리밋
        double Transfer_Negative = 0.0;                //HeadY Negaitve 리밋
        double Transfer_Positive = 0.0;                //HeadY Positve 리밋
        double Stage_Negative = 0.0;                 //Flux Z #1 Negatitve 리밋
        double Stage_Positive = 0.0;                 //Flux Z #1 Positve 리밋
        double RelativePosition = 0.0;              //RelativePosition Value
        bool SelectJogRelative;                     //Jog-Relative 토글
        int FSpeedLevel;                            //Speed
        #endregion

        public enum eGridValue
        {
            TrayCountX,
            TrayCountY,
            Space1,
            TrayPitchX,
            TrayPitchY,
            Space2,
            SlotCount,
            SlotPitch,
            Space3,
            MovingDelayX,
            MovingDelayY,
            MovingDelayZ,
            Space4,
            ReadyPosX,
            StagePosX,
            MagazinePosX,
            Space5,
            ReadyPosY,
            EjectPosY,
            StageFirstPickPosY,
            StageMagazineChangePosY,
            //WorkPosY,
            Space6,
            ReadyPosZ,
            InOutOffsetZ,
        }

        private enum eAxis
        {
            MagazineZ,
            TransferX,
        }


        public FormPageWorkingVCMLoader()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 1000);
        }
        private void FormPageWorkingVCMLoader_Load(object sender, EventArgs e)
        {
            Left = 131;
            Top = 60;
            Grid_Init();
            Grid_Update();
            FSpeedLevel = 0;
            FJogMouseDown = true;
            SelectJogRelative = true;
            ListVCMButton = new List<Button>();
            Manual_SlotInit();
            
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

                btnSelectJog.ForeColor = SelectJogRelative ? Color.Red : Color.Blue;
                btnSelectJog.Text = SelectJogRelative ? cDEF.Lang.Trans("JOG") : cDEF.Lang.Trans("RELATIVE");
                lbRelative_Position.Enabled = SelectJogRelative ? false : true;
                //lbMagazineSlot.Text = Slot.ToString();

                //if(cDEF.Run.VCMLoader.Is_Magazine_WorkingPositionZ(ListVCMButton.Find(""))

                lbMagazineZ_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.VCMLoader.Motions[(int)eAxis.MagazineZ].ActualPosition, true);
                lbTransferX_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.VCMLoader.Motions[(int)eAxis.TransferX].ActualPosition, true);
                lbStageY_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.VCMPicker.Motions[4].ActualPosition, true);

                lbRelative_Position.Text = cFnc.GetUnitValueString((int)RelativePosition, true);

                if (Convert.ToBoolean(cDEF.Run.VCMLoader.MagazineZ.FAlarm))
                {
                    lbZ.BackColor = Color.Red;
                }
                else
                {
                    lbZ.BackColor = Convert.ToBoolean(cDEF.Run.VCMLoader.MagazineZ.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.VCMLoader.TransferX.FAlarm))
                {
                    lbX.BackColor = Color.Red;
                }
                else
                {
                    lbX.BackColor = Convert.ToBoolean(cDEF.Run.VCMLoader.TransferX.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.VCMPicker.StageY.FAlarm))
                {
                    lbY.BackColor = Color.Red;
                }
                else
                {
                    lbY.BackColor = Convert.ToBoolean(cDEF.Run.VCMPicker.StageY.FInposition) ? Color.White : Color.Lime;
                }



                //Position Check
                lbReadyPositionZ.BackColor = cDEF.Run.VCMLoader.Is_Magazine_ReadyPositionZ() ? Color.Lime : Color.White;
                //lbWorkPositionZ.BackColor = cDEF.Run.VCMLoader.Is_Magazine_WorkingPositionZ(Slot) ? Color.Lime : Color.White;
                lbReadyPositionX.BackColor = cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX() ? Color.Lime : Color.White;
                lbStagePositionX.BackColor = cDEF.Run.VCMLoader.Is_Transfer_StagePositionX() ? Color.Lime : Color.White;
                lbMagazinePositionX.BackColor = cDEF.Run.VCMLoader.Is_Transfer_MagazinePositionX() ? Color.Lime : Color.White;
                lbReadyPositionY.BackColor = cDEF.Run.VCMPicker.Is_Stage_ReadyPositionY() ? Color.Lime : Color.White;
                lbEjectPositionY.BackColor = cDEF.Run.VCMPicker.Is_Stage_EjectPositionY() ? Color.Lime : Color.White;
                lbMagazineChange.BackColor = cDEF.Run.VCMPicker.Is_Stage_MagazineChangePositionY() ? Color.Lime : Color.White;
                //lbWorkPositionY.BackColor = cDEF.Run.VCMPicker.Is_Stage_WorkPositionY() ? Color.Lime : Color.White;

                //Cylinder Check
                lbTransferUnclamp.BackColor = cDEF.Run.VCMLoader.TransferClamp.IsBackward() ? Color.Lime : Color.White;
                lbTransferClamp.BackColor = cDEF.Run.VCMLoader.TransferClamp.IsForward() ? Color.Lime : Color.White;
               

                //IO Check
                lbTransferOverload1.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_VCMLoading_Overload_1] ? Color.Orange : Color.White;
                lbTransferOverload2.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_VCMLoading_Overload_2] ? Color.Orange : Color.White;
                lbTransferUnclamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_Loading_Tray_UnClamp] ? Color.Orange : Color.White;
                lbTransferClamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_Loading_Tray_Clamp] ? Color.Orange : Color.White;
                lbTransferTrayExist.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_Loading_Tray_Check] ? Color.Orange : Color.White;
                lbMagazineExist.BackColor = cDEF.Run.Digital.Input[cDI.VCMLoading_Magazine_Check] ? Color.Orange : Color.White;
                lbStageUnclamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_UnClamp] ? Color.Orange : Color.White;
                lbStageClamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_Clamp] ? Color.Orange : Color.White;
                lbStageTrayEnd.BackColor = cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_End_Sensor] ? Color.Orange : Color.White;
                lbStageTrayContact.BackColor = cDEF.Run.Digital.Input[cDI.Stage_VCMLoading_Tray_ConTact_Sensor] ? Color.Orange : Color.White;
                lbStageUnclamp.BackColor = cDEF.Run.VCMLoader.StageClamp.IsBackward() ? Color.Lime : Color.White;
                lbStageClamp.BackColor = cDEF.Run.VCMLoader.StageClamp.IsForward() ? Color.Lime : Color.White;

            }));
        }

        #region GridUpdate
        private void Grid_Init()
        {
            GridAdd("R", "Tray Count X", "Tray");
            GridAdd("R", "Tray Count Y", "Tray");
            GridAdd_Space();
            GridAdd("R", "Tray Pitch X", "Tray");
            GridAdd("R", "Tray Pitch Y", "Tray");
            GridAdd_Space();
            GridAdd("R", "Magazine Slot Count", "Magazine");
            GridAdd("R", "Magazine Slot Pitch", "Magazine");
            GridAdd_Space();
            GridAdd("R", "Transfer Moving Delay X", "Transfer X");
            GridAdd("R", "Stage Moving Delay Y", "Stage Y");
            GridAdd("R", "Magazine Moving Delay Z", "Magazine Z");
            GridAdd_Space();
            GridAdd("R", "Transfer Ready Position X", "Transfer X");
            GridAdd("R", "Transfer Stage Position X", "Transfer X");
            GridAdd("R", "Transfer Magazine Position X", "Transfer X");
            GridAdd_Space();
            GridAdd("R", "Stage Ready Position Y", "Stage Y");
            GridAdd("R", "Stage Eject Position Y", "Stage Y");
            GridAdd("R", "Stage First Pick Position", "Stage Y");
            GridAdd("R", "Stage Magazine Change Position", "Stage Y");
            GridAdd_Space();
            GridAdd("R", "Magazine Ready Position Z", "Magazine Z");
            GridAdd("R", "Magazine In/Out Offset Z", "Magazine");
        }

        private void Grid_Update()
        {
            for (int i = 0; i < MotionDataGrid.RowCount; i++)
            {
                switch((eGridValue)i)
                {
                    case eGridValue.ReadyPosZ:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosZ].Value = ((double)cDEF.Work.TeachVCMLoader.ReadyPositionZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.ReadyPosX:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosX].Value = ((double)cDEF.Work.TeachVCMLoader.ReadyPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.StagePosX:
                        MotionDataGrid[3, (int)eGridValue.StagePosX].Value = ((double)cDEF.Work.TeachVCMLoader.StagePositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.MagazinePosX:
                        MotionDataGrid[3, (int)eGridValue.MagazinePosX].Value = ((double)cDEF.Work.TeachVCMLoader.MagazinePositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.SlotCount:
                        MotionDataGrid[3, (int)eGridValue.SlotCount].Value = cDEF.Work.VCMLoader.SlotCount;
                        break;

                    case eGridValue.SlotPitch:
                        MotionDataGrid[3, (int)eGridValue.SlotPitch].Value = ((double)cDEF.Work.VCMLoader.SlotPitchZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.InOutOffsetZ:
                        MotionDataGrid[3, (int)eGridValue.InOutOffsetZ].Value = ((double)cDEF.Work.TeachVCMLoader.InOutOffsetZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.TrayCountX:
                        MotionDataGrid[3, (int)eGridValue.TrayCountX].Value = cDEF.Work.VCMLoader.TrayCountX;
                        break;

                    case eGridValue.TrayCountY:
                        MotionDataGrid[3, (int)eGridValue.TrayCountY].Value = cDEF.Work.VCMLoader.TrayCountY;
                        break;

                    case eGridValue.TrayPitchX:
                        MotionDataGrid[3, (int)eGridValue.TrayPitchX].Value = ((double)cDEF.Work.VCMLoader.TrayPitchX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.TrayPitchY:
                        MotionDataGrid[3, (int)eGridValue.TrayPitchY].Value = ((double)cDEF.Work.VCMLoader.TrayPitchY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.MovingDelayZ:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayZ].Value = ((double)cDEF.Work.VCMLoader.MovingDelayZ / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayX:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayX].Value = ((double)cDEF.Work.VCMLoader.MovingDelayX / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayY:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayY].Value = ((double)cDEF.Work.VCMLoader.MovingDelayY / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.ReadyPosY:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosY].Value = ((double)cDEF.Work.TeachVCMLoader.ReadyPositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.EjectPosY:
                        MotionDataGrid[3, (int)eGridValue.EjectPosY].Value = ((double)cDEF.Work.TeachVCMLoader.EjectPositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.StageFirstPickPosY:
                        MotionDataGrid[3, (int)eGridValue.StageFirstPickPosY].Value = ((double)cDEF.Work.TeachVCMLoader.StageFirstPickPositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.StageMagazineChangePosY:
                        MotionDataGrid[3, (int)eGridValue.StageMagazineChangePosY].Value = ((double)cDEF.Work.TeachVCMLoader.StageMagazineChangePositionY / 1000.0).ToString("N3") + " mm";
                        break;
                        //case eGridValue.WorkPosY:
                        //    MotionDataGrid[3, (int)eGridValue.WorkPosY].Value = ((double)cDEF.Work.TeachVCMLoader.WorkPositionY / 1000.0).ToString("N3") + " mm";
                        //    break;

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

        #region Grid_DataSetting
        private void MotionDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row;
            double DValue = 0;
            string str = "";
            int Value = 0;
            DataGridView Grid = (DataGridView)sender;


            Magazine_Negative = cDEF.Run.VCMLoader.MagazineZ.Config.FLimit.FSoftwareNegative;
            Magazine_Positive = cDEF.Run.VCMLoader.MagazineZ.Config.FLimit.FSoftwarePositive;

            Transfer_Negative = cDEF.Run.VCMLoader.TransferX.Config.FLimit.FSoftwareNegative;
            Transfer_Positive = cDEF.Run.VCMLoader.TransferX.Config.FLimit.FSoftwarePositive;

            Stage_Negative = cDEF.Run.VCMPicker.StageY.Config.FLimit.FSoftwareNegative;
            Stage_Positive = cDEF.Run.VCMPicker.StageY.Config.FLimit.FSoftwarePositive;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            eGridValue eGrid = (eGridValue)Row;
            if (Col == 3)
            {
                switch (eGrid)
                {
                    case eGridValue.ReadyPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Magazine Ready Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.VCMLoader.MagazineZ.ActualPosition / 1000.0, Magazine_Negative / 1000.0, Magazine_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Loader] Magazine Ready Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.ReadyPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.ReadyPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 0, str);
                        }
                        break;

                    case eGridValue.ReadyPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.ReadyPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Transfer Ready Position X", ref DValue, " mm", "CURRENT", cDEF.Run.VCMLoader.TransferX.ActualPosition / 1000.0, Transfer_Negative / 1000.0, Transfer_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Loader] Transfer Ready Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.ReadyPositionX / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.ReadyPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 1, str);
                        }
                        break;

                    case eGridValue.StagePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.StagePositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Transfer Stage Position X", ref DValue, " mm", "CURRENT", cDEF.Run.VCMLoader.TransferX.ActualPosition / 1000.0, Transfer_Negative / 1000.0, Transfer_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Loader] Transfer Stage Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.StagePositionX / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.StagePositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 2, str);
                        }
                        break;

                    case eGridValue.MagazinePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.MagazinePositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Transfer Magazine Position X", ref DValue, " mm", "CURRENT", cDEF.Run.VCMLoader.TransferX.ActualPosition / 1000.0, Transfer_Negative / 1000.0, Transfer_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Loader] Transfer Magazine Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.MagazinePositionX / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.MagazinePositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 3, str);
                        }
                        break;

                   

                    case eGridValue.SlotCount:
                        Value = cDEF.Work.VCMLoader.SlotCount;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Magazine Slot Count", ref Value, " ea", 5, 20))
                            return;
                        {
                            str = String.Format("[VCM Loader] Magazine Slot Count {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.SlotCount, Value);
                            cDEF.Work.VCMLoader.SlotCount = Value;
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.VCMLoader.Information.VCM_Magazine.Init(cDEF.Work.VCMLoader.SlotCount, cDEF.Work.VCMLoader.TrayCountX, cDEF.Work.VCMLoader.TrayCountY);
                            cDEF.frmPageOperation.VCMMagazineObjectInit();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 7, str);
                        }
                        break;

                    case eGridValue.SlotPitch:
                        DValue = Convert.ToDouble(cDEF.Work.VCMLoader.SlotPitchZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Magazine Slot Pitch Z", ref DValue, " mm", 0, 100))
                            return;
                        {
                            str = String.Format("[VCM Loader] Magazine Slot Pitch Z {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.SlotPitchZ / 1000.0, DValue);
                            cDEF.Work.VCMLoader.SlotPitchZ = (int)(DValue * 1000.0);
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 8, str);
                        }
                        break;

                    case eGridValue.InOutOffsetZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.InOutOffsetZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Magazine In/Out Offset Z", ref DValue, " mm", 0, 5))
                            return;
                        {
                            str = String.Format("[VCM Loader] Magazine In/Out Offset Z {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.InOutOffsetZ / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.InOutOffsetZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 9, str);
                        }
                        break;

                    case eGridValue.TrayCountX:
                        Value = cDEF.Work.VCMLoader.TrayCountX;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Tray Count X", ref Value, " ea", 5, 100))
                            return;
                        {
                            str = String.Format("[VCM Loader] Tray Count X {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.TrayCountX, Value);
                            cDEF.Work.VCMLoader.TrayCountX = Value;
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 10, str);
                        }
                        break;

                    case eGridValue.TrayCountY:
                        Value = cDEF.Work.VCMLoader.TrayCountY;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Tray Count Y", ref Value, " ea", 5, 100))
                            return;
                        {
                            str = String.Format("[VCM Loader] Tray Count Y {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.TrayCountY, Value);
                            cDEF.Work.VCMLoader.TrayCountY = Value;
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 11, str);
                        }
                        break;

                    case eGridValue.TrayPitchX:
                        DValue = Convert.ToDouble(cDEF.Work.VCMLoader.TrayPitchX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Tray Pitch X", ref DValue, " mm", 0, 30))
                            return;
                        {
                            str = String.Format("[VCM Loader] Tray Pitch X {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.TrayPitchX / 1000.0, DValue);
                            cDEF.Work.VCMLoader.TrayPitchX = (int)(DValue * 1000.0);
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 12, str);
                        }
                        break;

                    case eGridValue.TrayPitchY:
                        DValue = Convert.ToDouble(cDEF.Work.VCMLoader.TrayPitchY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Tray Pitch Y", ref DValue, " mm", 0, 30))
                            return;
                        {
                            str = String.Format("[VCM Loader] Tray Pitch Y {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.TrayPitchY / 1000.0, DValue);
                            cDEF.Work.VCMLoader.TrayPitchY = (int)(DValue * 1000.0);
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 13, str);
                        }
                        break;

                    case eGridValue.MovingDelayZ:
                        DValue = Convert.ToDouble(cDEF.Work.VCMLoader.MovingDelayZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Magazine Moving Delay Z", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Loader] Magazine Moving Delay Z {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.MovingDelayZ / 1000.0, DValue);
                            cDEF.Work.VCMLoader.MovingDelayZ = (int)(DValue * 1000.0);
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 14, str);
                        }
                        break;

                    case eGridValue.MovingDelayX:
                        DValue = Convert.ToDouble(cDEF.Work.VCMLoader.MovingDelayX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Transfer Moving Delay X", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Loader] Transfer Moving Delay X {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.MovingDelayX / 1000.0, DValue);
                            cDEF.Work.VCMLoader.MovingDelayX = (int)(DValue * 1000.0);
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 15, str);
                        }
                        break;

                    case eGridValue.MovingDelayY:
                        DValue = Convert.ToDouble(cDEF.Work.VCMLoader.MovingDelayY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Moving Delay Y", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[VCM Loader] Stage Moving Delay Y {0:0.000} to {1:0.000}", cDEF.Work.VCMLoader.MovingDelayY / 1000.0, DValue);
                            cDEF.Work.VCMLoader.MovingDelayY = (int)(DValue * 1000.0);
                            cDEF.Work.VCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 15, str);
                        }
                        break;

                    case eGridValue.ReadyPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.ReadyPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Ready Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.StageY.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Loader] Stage Ready Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.ReadyPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 4, str);
                        }
                        break;

                    case eGridValue.EjectPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.EjectPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Eject Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.StageY.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Loader] Stage Eject Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.EjectPositionY / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.EjectPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 5, str);
                        }
                        break;

                    case eGridValue.StageFirstPickPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.StageFirstPickPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage First Pick Position", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.StageY.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Loader] Stage First Pick Position {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.StageFirstPickPositionY / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.StageFirstPickPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 5, str);
                        }
                        break;

                    case eGridValue.StageMagazineChangePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.StageMagazineChangePositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Magazine Change Position", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.StageY.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[VCM Loader] Stage Magazine Change Position {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.StageMagazineChangePositionY / 1000.0, DValue);
                            cDEF.Work.TeachVCMLoader.StageMagazineChangePositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachVCMLoader.Save();
                            cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 5, str);
                        }
                        break;

                        //case eGridValue.WorkPosY:
                        //    DValue = Convert.ToDouble(cDEF.Work.TeachVCMLoader.WorkPositionY) / 1000;
                        //    if (!XModuleMain.frmBox.fpFloatEdit("Stage Work Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.VCMPicker.StageY.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                        //        return;
                        //    {
                        //        str = String.Format("[VCM Loader] Stage Work Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachVCMLoader.WorkPositionY / 1000.0, DValue);
                        //        cDEF.Work.TeachVCMLoader.WorkPositionY = (int)(DValue * 1000.0);
                        //        cDEF.Work.TeachVCMLoader.Save();
                        //        cDEF.Run.LogData(cLog.Form_VCMLoader_Data + 6, str);
                        //    }
                        //    break;

                }
            }
            Grid_Update();
            Manual_SlotInit();
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
                cnt = cDEF.Run.VCMLoader.MotionCount;
                for (i = 0; i < cnt; i++)
                    cDEF.Run.VCMLoader.Motions[i].Stop();
            }
        }

        private void Jog_Relative_MouseUp2(object sender, MouseEventArgs e)
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
                        if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.VCMLoader.Motions[(int)eAxis.MagazineZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.VCMLoader.Motions[(int)eAxis.MagazineZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.VCMPicker.Is_Stage_EjectPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.VCMLoader.Motions[(int)eAxis.TransferX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 3:
                        if (!cDEF.Run.VCMPicker.Is_Stage_EjectPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.VCMLoader.Motions[(int)eAxis.TransferX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 8:
                        FDirection = 0;
                        if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMPicker.Motions[4].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 9:
                        if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.VCMPicker.Motions[4].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                }
            }
            else
            {
                switch (FTag)
                {
                    case 0:
                        if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMLoader.Motions[(int)eAxis.MagazineZ].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMLoader.Motions[(int)eAxis.MagazineZ].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.VCMPicker.Is_Stage_EjectPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMLoader.Motions[(int)eAxis.TransferX].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 3:
                        if (!cDEF.Run.VCMPicker.Is_Stage_EjectPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMLoader.Motions[(int)eAxis.TransferX].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 8:
                        if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMPicker.Motions[4].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 9:
                        if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.VCMPicker.Motions[4].Relative(RelativePosition, FSpeedLevel);
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
                    btnSpeed.Text = cDEF.Lang.Trans("SPEED") +$":{(FSpeedLevel)}%";
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
                    if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Magazine Ready Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.Move_Magazine_ReadyPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 1, "[VCM Loader] Move Magazine Ready Position Z.");
                    }
                    break;
                case "btnReadyPositionX":
                    if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Transfer Ready Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.Move_Transfer_ReadyPositionX();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 2, "[VCM Loader] Move Transfer Ready Position X.");
                    }
                    break;
                case "btnStagePositionX":
                    if (!cDEF.Run.VCMPicker.Is_Stage_EjectPositionY())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Transfer Stage Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.Move_Transfer_StagePositionX();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 3, "[VCM Loader] Move Transfer Stage Position X.");
                    }
                    break;
                case "btnMagazinePositionX":
                    if (!cDEF.Run.VCMPicker.Is_Stage_EjectPositionY())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.VCMPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Transfer Magazine Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.Move_Transfer_MagazinePositionX();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 4, "[VCM Loader] Move Transfer Magazine Position X.");
                    }
                    break;
               
                case "btnRelativeUpPositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Magazine Relative Up Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.Move_Magazine_Relative_UpZ();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 8, "[VCM Loader] Move Magazine Relative Up Position Z.");
                    }
                    break;

                case "btnReadyPositionY":
                    if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Stage Ready Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Stage_ReadyPositionY();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 5, "[VCM Loader] Move Stage Ready Position Y.");
                    }
                    break;
                case "btnEjectPositionY":
                    if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Stage Eject Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Stage_EjectPositionY();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 6, "[VCM Loader] Move Stage Eject Position Y.");
                    }
                    break;

                case "btnMagazineChange":
                    if (!cDEF.Run.VCMLoader.Is_Transfer_ReadyPositionX())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Stage Magazine Change Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Move_Stage_MagazineChagnePosition();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 6, "[VCM Loader] Move Stage Magazine Change Position Y.");
                    }
                    break;

                    //case "btnWorkPositionY":
                    //    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move VCM Loader Stage Work Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    //    {
                    //        cDEF.Run.VCMPicker.Move_Stage_WorkPositionY();
                    //        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 7, "[VCM Loader] Move Stage Work Position Y.");
                    //    }
                    //    break;
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
                case "btnLoading":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Load Stage?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_VCMLoading;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Loading.");
                    }
                    break;
                case "btnUnloading":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Unload Stage?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_VCMUnloading;
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 100, "[Semi Auto] Unloading.");
                    }
                    break;
            }
        }
        private List<Button> ListVCMButton;

        private void Manual_SlotInit()
        {
            ListVCMButton.Clear();
            pnVCMMagazine.Controls.Clear();

            Label labeltitle = new Label();
            labeltitle.Name = "lbVCM_MagazineTitle";
            labeltitle.Font = new Font("Tahoma", 9, FontStyle.Bold);
            labeltitle.Text = "MANUAL SLOT MOVE";
            labeltitle.TextAlign = ContentAlignment.MiddleCenter;
            labeltitle.FlatStyle = FlatStyle.Standard;
            labeltitle.BorderStyle = BorderStyle.None;
            labeltitle.AutoSize = false;
            labeltitle.BackColor = Color.Gainsboro;
            labeltitle.ForeColor = Color.Navy;
            labeltitle.Size = new Size(pnVCMMagazine.Width - 2, 24);
            labeltitle.Location = new Point(1, 1);
            pnVCMMagazine.Controls.Add(labeltitle);

            for (int i = 0; i < cDEF.Work.VCMLoader.SlotCount; i++)
            {
                Button button = new Button();
                button.TabIndex = i;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.FlatStyle = FlatStyle.Flat;
                //button.BorderStyle = BorderStyle.FixedSingle;
                button.AutoSize = false;
                //Cst Size
                button.Height = (pnVCMMagazine.Height - 34) / cDEF.Work.VCMLoader.SlotCount;
                button.Width = pnVCMMagazine.Width + 2;
                int Top = 30 + (i * button.Height) + 2;
                button.Location = new Point(-1, Top);
                button.ForeColor = Color.Black;
                button.BackColor = Color.White;
                button.Visible = true;
                button.Click += Manual_Move_Slot;
                pnVCMMagazine.Controls.Add(button);
                ListVCMButton.Add(button);
                if (!cDEF.Work.Recipe.LoaderMagazineDirection)
                {
                    button.Name = $"lbVCM_Slot{cDEF.Work.VCMLoader.SlotCount - i}";
                    button.Text = $"Slot #{cDEF.Work.VCMLoader.SlotCount - i}";
                    button.Tag = cDEF.Work.VCMLoader.SlotCount - (i + 1);
                }
                else
                {
                    button.Name = $"lbVCM_Slot{i+1}";
                    button.Text = $"Slot #{i+1}";
                    button.Tag = i;
                }
            }
        }
        private void Manual_Move_Slot(object sender, EventArgs e)
        {
            string FButton;
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            int FTag = Convert.ToInt32((sender as Button).Tag);
            if (XModuleMain.frmBox.MessageBox("MANUAL", $"Move VCM Loader Magazine Slot #{FTag + 1} Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
            {
                cDEF.Run.VCMLoader.Move_Magazine_WorkingPositionZ(FTag); // Slot Count 로 받을것 
                cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 0, $"[VCM Loader] Move Magazine Slot #{FTag + 1} Position Z.");
            }
        }
    private void FormPageWorkingVCMLoader_VisibleChanged(object sender, EventArgs e)
        {
            Grid_Update();
            Manual_SlotInit();
        }

        private void btn_Clamp_Click(object sender, EventArgs e)
        {
            string FName;
            string FButton;
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            FName = (sender as Glass.GlassButton).Name;

            switch(FName)
            {
                case "btnTransferUnclamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Transfer Unclamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.TransferClamp.Backward();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 9, "[VCM Loader] Move Transfer Unclamp.");
                    }
                    break;
                case "btnTransferClamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Transfer Clamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.TransferClamp.Forward();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 10, "[VCM Loader] Move Transfer Clamp.");
                    }
                    break;
                case "btnStageUnclamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Stage Unclamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.StageClamp.Backward();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 11, "[VCM Loader] Move Stage Unclamp.");
                    }
                    break;
                case "btnStageClamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Stage Clamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMLoader.StageClamp.Forward();
                        cDEF.Run.LogEvent(cLog.Form_VCMLoader_Event + 12, "[VCM Loader] Move Stage Clamp.");
                    }
                    break;
            }
        }
        public void ChangeLanguage()
        {
            lbGridTitle.Text = cDEF.Lang.Trans("SETTING");
            lbImageTitle.Text = cDEF.Lang.Trans("IMAGE");
            lbJogTitle.Text = cDEF.Lang.Trans("JOG");
            lbMagazineTitle.Text = cDEF.Lang.Trans("MAGAZINE");
            lbTransferTitle.Text = cDEF.Lang.Trans("TRANSFER");
            lbStageTitle.Text = cDEF.Lang.Trans("STAGE");
            lbCurrentPositionY.Text = cDEF.Lang.Trans("Current Position");
            btnReadyPositionY.Text = cDEF.Lang.Trans("READY POSITION Y");
            //btnWorkPositionY.Text = cDEF.Lang.Trans("WORK POSITION Y");
            //btnEjectPositionY.Text = cDEF.Lang.Trans("");
            btnStageClamp.Text = cDEF.Lang.Trans("CLAMP");
            btnStageUnclamp.Text = cDEF.Lang.Trans("UNCLAMP");
            lbSemiAutoTitle.Text = cDEF.Lang.Trans("SEMI AUTO");
            lbSpeed.Text = cDEF.Lang.Trans("Speed");
            btnSpeed.Text = cDEF.Lang.Trans("SPEED");
            lbCurrentPositionX.Text = cDEF.Lang.Trans("Current Position");
            
            lbCurrentPositionZ.Text = cDEF.Lang.Trans("Current Position");
            lbStage_Unclamp.Text = cDEF.Lang.Trans("Stage Unclamp");
            lbStage_Clamp.Text = cDEF.Lang.Trans("Stage Clamp");
            lbStage_TrayContact.Text = cDEF.Lang.Trans("Stage Tray Contact");
            lbStage_TrayEnd.Text = cDEF.Lang.Trans("Stage Tray End");
            lbMagazine_Exist.Text = cDEF.Lang.Trans("Magazine Exist");
            lbTransfer_Overload1.Text = cDEF.Lang.Trans("Transfer Overload 1");
            lbTransfer_Overload2.Text = cDEF.Lang.Trans("Transfer Overload 2");
            lbTransfer_Unclamp.Text = cDEF.Lang.Trans("Transfer Unclamp");
            lbTransfer_Clamp.Text = cDEF.Lang.Trans("Transfer Clamp");
            lbTransfer_TrayExist.Text = cDEF.Lang.Trans("Transfer Tray Exist");
            btnReadyPositionZ.Text = cDEF.Lang.Trans("READY POSITION Z");
            btnRelativeUpPositionZ.Text = cDEF.Lang.Trans("RELATIVE UP POSITION Z");
            btnReadyPositionX.Text = cDEF.Lang.Trans("READY POSITION X");
            btnMagazinePositionX.Text = cDEF.Lang.Trans("MAGAZINE POSITION X");
            btnStagePositionX.Text = cDEF.Lang.Trans("STAGE POSITION X");
            btnTransferUnclamp.Text = cDEF.Lang.Trans("UNCLAMP");
            btnTransferClamp.Text = cDEF.Lang.Trans("CLAMP");
            
        }

        
    }
}
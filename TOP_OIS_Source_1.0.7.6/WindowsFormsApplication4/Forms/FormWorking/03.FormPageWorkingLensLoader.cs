using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using XModule.Standard;

namespace XModule.Forms.FormWorking
{
    public partial class FormPageWorkingLensLoader : TFrame
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
            EjectPosX,
            StageFirstPickPosX,
            StageMagazineChangePosX,
            //WorkPosX,
            Space5,
            ReadyPosY,
            StagePosY,
            MagazinePosY,
            Space6,
            ReadyPosZ,
            InOutOffsetZ,
        }

        private enum eAxis
        {
            MagazineZ,
            TransferY,
        }


        public FormPageWorkingLensLoader()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 1000);
        }
        private void FormPageWorkingLensLoader_Load(object sender, EventArgs e)
        {
            Left = 131;
            Top = 60;
            Grid_Init();
            Grid_Update();
            FSpeedLevel = 0;
            FJogMouseDown = true;
            SelectJogRelative = true;
            ListLensButton = new List<Button>();
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

                btnSelectJog.Text = SelectJogRelative ? cDEF.Lang.Trans("JOG") : cDEF.Lang.Trans("RELATIVE");
                btnSelectJog.ForeColor = SelectJogRelative ? Color.Red : Color.Blue;
                lbRelative_Position.Enabled = SelectJogRelative ? false : true;


                lbMagazineZ_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.LensLoader.Motions[(int)eAxis.MagazineZ].ActualPosition, true);
                lbTransferY_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.LensLoader.Motions[(int)eAxis.TransferY].ActualPosition, true);
                lbStageX_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.LensPicker.Motions[4].ActualPosition, true);

                lbRelative_Position.Text = cFnc.GetUnitValueString((int)RelativePosition, true);
                
                if (Convert.ToBoolean(cDEF.Run.LensLoader.MagazineZ.FAlarm))
                {
                    lbZ.BackColor = Color.Red;
                }
                else
                {
                    lbZ.BackColor = Convert.ToBoolean(cDEF.Run.LensLoader.MagazineZ.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.LensLoader.TransferY.FAlarm))
                {
                    lbY.BackColor = Color.Red;
                }
                else
                {
                    lbY.BackColor = Convert.ToBoolean(cDEF.Run.LensLoader.TransferY.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.LensPicker.StageX.FAlarm))
                {
                    lbX.BackColor = Color.Red;
                }
                else
                {
                    lbX.BackColor = Convert.ToBoolean(cDEF.Run.LensPicker.StageX.FInposition) ? Color.White : Color.Lime;
                }


                //Position Check
                lbReadyPositionZ.BackColor = cDEF.Run.LensLoader.Is_Magazine_ReadyPositionZ() ? Color.Lime : Color.White;
                //lbWorkPositionZ.BackColor = cDEF.Run.LensLoader.Is_Magazine_WorkingPositionZ(Slot) ? Color.Lime : Color.White;

                lbReadyPositionY.BackColor = cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY() ? Color.Lime : Color.White;
                lbStagePositionY.BackColor = cDEF.Run.LensLoader.Is_Transfer_StagePositionY() ? Color.Lime : Color.White;
                lbMagazinePositionY.BackColor = cDEF.Run.LensLoader.Is_Transfer_MagazinePositionY() ? Color.Lime : Color.White;

                lbReadyPositionX.BackColor = cDEF.Run.LensPicker.Is_Stage_ReadyPositionX() ? Color.Lime : Color.White;
                lbEjectPositionX.BackColor = cDEF.Run.LensPicker.Is_Stage_EjectPositionX() ? Color.Lime : Color.White;
                lbMagazineChange.BackColor = cDEF.Run.LensPicker.Is_Stage_MagazineChangePositionX() ? Color.Lime : Color.White;

                //Cylinder Status
                lbTransferUnclamp.BackColor = cDEF.Run.LensLoader.TransferClamp.IsBackward() ? Color.Lime : Color.White;
                lbTransferClamp.BackColor = cDEF.Run.LensLoader.TransferClamp.IsForward() ? Color.Lime : Color.White;
                lbStageUnclamp.BackColor = cDEF.Run.LensLoader.StageClamp.IsBackward() ? Color.Lime : Color.White;
                lbStageClamp.BackColor = cDEF.Run.LensLoader.StageClamp.IsForward() ? Color.Lime : Color.White;

                //IO Check
                lbTransferOverload1.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_LensLoading_Overload_1] ? Color.Orange : Color.White;
                lbTransferOverload2.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_LensLoading_Overload_2] ? Color.Orange : Color.White;
                lbTransferUnclamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_Lens_Tray_UnClamp] ? Color.Orange : Color.White;
                lbTransferClamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_Lens_Tray_Clamp] ? Color.Orange : Color.White;
                lbTransferTrayExist.BackColor = cDEF.Run.Digital.Input[cDI.Transfer_Lens_Tray_Check] ? Color.Orange : Color.White;
                lbMagazineExist.BackColor = cDEF.Run.Digital.Input[cDI.LENSLoading_Magazine_Check] ? Color.Orange : Color.White;
                lbStageUnclamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Stage_LensLoading_Tray_UnClamp] ? Color.Orange : Color.White;
                lbStageClamp_IO.BackColor = cDEF.Run.Digital.Input[cDI.Stage_LensLoading_Tray_Clamp] ? Color.Orange : Color.White;
                lbStageTrayEnd.BackColor = cDEF.Run.Digital.Input[cDI.Stage_LensLoading_Tray_End_Sensor] ? Color.Orange : Color.White;
                lbStageTrayContact.BackColor = cDEF.Run.Digital.Input[cDI.LENSLoading_Magazine_Check] ? Color.Orange : Color.White;

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
            GridAdd("R", "Stage Moving Delay X", "Stage X");
            GridAdd("R", "Transfer Moving Delay Y", "Transfer Y");
            GridAdd("R", "Magazine Moving Delay Z", "Magazine Z");
            GridAdd_Space();
            GridAdd("R", "Stage Ready Position X", "Stage X");
            GridAdd("R", "Stage Eject Position X", "Stage X");
            GridAdd("R", "Stage First Pick Position X", "Stage X");
            GridAdd("R", "Stage Magazine Change Position X", "Stage X");
            //GridAdd("Stage Work Position X", "Stage X");
            GridAdd_Space();
            GridAdd("R", "Transfer Ready Position Y", "Transfer Y");
            GridAdd("R", "Transfer Stage Position Y", "Transfer Y");
            GridAdd("R", "Transfer Magazine Position Y", "Transfer Y");
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
                        MotionDataGrid[3, (int)eGridValue.ReadyPosZ].Value = ((double)cDEF.Work.TeachLensLoader.ReadyPositionZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.ReadyPosY:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosY].Value = ((double)cDEF.Work.TeachLensLoader.ReadyPositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.StagePosY:
                        MotionDataGrid[3, (int)eGridValue.StagePosY].Value = ((double)cDEF.Work.TeachLensLoader.StagePositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.MagazinePosY:
                        MotionDataGrid[3, (int)eGridValue.MagazinePosY].Value = ((double)cDEF.Work.TeachLensLoader.MagazinePositionY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.ReadyPosX:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosX].Value = ((double)cDEF.Work.TeachLensLoader.ReadyPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.EjectPosX:
                        MotionDataGrid[3, (int)eGridValue.EjectPosX].Value = ((double)cDEF.Work.TeachLensLoader.EjectPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.StageFirstPickPosX:
                        MotionDataGrid[3, (int)eGridValue.StageFirstPickPosX].Value = ((double)cDEF.Work.TeachLensLoader.StageFirstPickPositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.StageMagazineChangePosX:
                        MotionDataGrid[3, (int)eGridValue.StageMagazineChangePosX].Value = ((double)cDEF.Work.TeachLensLoader.StageMagazineChangePositionX / 1000.0).ToString("N3") + " mm";
                        break;

                    //case eGridValue.WorkPosX:
                    //    MotionDataGrid[3, (int)eGridValue.WorkPosX].Value = ((double)cDEF.Work.TeachLensLoader.WorkPositionX / 1000.0).ToString("N3") + " mm";
                    //    break;

                    case eGridValue.SlotCount:
                        MotionDataGrid[3, (int)eGridValue.SlotCount].Value = cDEF.Work.LensLoader.SlotCount;
                        break;

                    case eGridValue.SlotPitch:
                        MotionDataGrid[3, (int)eGridValue.SlotPitch].Value = ((double)cDEF.Work.LensLoader.SlotPitchZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.InOutOffsetZ:
                        MotionDataGrid[3, (int)eGridValue.InOutOffsetZ].Value = ((double)cDEF.Work.TeachLensLoader.InOutOffsetZ / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.TrayCountX:
                        MotionDataGrid[3, (int)eGridValue.TrayCountX].Value = cDEF.Work.LensLoader.TrayCountX;
                        break;

                    case eGridValue.TrayCountY:
                        MotionDataGrid[3, (int)eGridValue.TrayCountY].Value = cDEF.Work.LensLoader.TrayCountY;
                        break;

                    case eGridValue.TrayPitchX:
                        MotionDataGrid[3, (int)eGridValue.TrayPitchX].Value = ((double)cDEF.Work.LensLoader.TrayPitchX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.TrayPitchY:
                        MotionDataGrid[3, (int)eGridValue.TrayPitchY].Value = ((double)cDEF.Work.LensLoader.TrayPitchY / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.MovingDelayZ:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayZ].Value = ((double)cDEF.Work.LensLoader.MovingDelayZ / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayY:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayY].Value = ((double)cDEF.Work.LensLoader.MovingDelayY / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayX:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayX].Value = ((double)cDEF.Work.LensLoader.MovingDelayX / 1000.0).ToString("N3") + " sec";
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


            Magazine_Negative = cDEF.Run.LensLoader.MagazineZ.Config.FLimit.FSoftwareNegative;
            Magazine_Positive = cDEF.Run.LensLoader.MagazineZ.Config.FLimit.FSoftwarePositive;

            Transfer_Negative = cDEF.Run.LensLoader.TransferY.Config.FLimit.FSoftwareNegative;
            Transfer_Positive = cDEF.Run.LensLoader.TransferY.Config.FLimit.FSoftwarePositive;

            Stage_Negative = cDEF.Run.LensPicker.StageX.Config.FLimit.FSoftwareNegative;
            Stage_Positive = cDEF.Run.LensPicker.StageX.Config.FLimit.FSoftwarePositive;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            eGridValue eGrid = (eGridValue)Row;
            if (Col == 3)
            {
                switch (eGrid)
                {
                    case eGridValue.ReadyPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Magazine Ready Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.LensLoader.MagazineZ.ActualPosition / 1000.0, Magazine_Negative / 1000.0, Magazine_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Loader] Magazine Ready Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.ReadyPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.ReadyPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 0, str);
                        }
                        break;

                    case eGridValue.ReadyPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.ReadyPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Transfer Ready Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.LensLoader.TransferY.ActualPosition / 1000.0, Transfer_Negative / 1000.0, Transfer_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Loader] Transfer Ready Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.ReadyPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 1, str);
                        }
                        break;

                    case eGridValue.StagePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.StagePositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Transfer Stage Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.LensLoader.TransferY.ActualPosition / 1000.0, Transfer_Negative / 1000.0, Transfer_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Loader] Transfer Stage Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.StagePositionY / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.StagePositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 2, str);
                        }
                        break;

                    case eGridValue.MagazinePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.MagazinePositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Transfer Magazine Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.LensLoader.TransferY.ActualPosition / 1000.0, Transfer_Negative / 1000.0, Transfer_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Loader] Transfer Magazine Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.MagazinePositionY / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.MagazinePositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 3, str);
                        }
                        break;

                    case eGridValue.ReadyPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.ReadyPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Ready Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.StageX.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Loader] Stage Ready Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.ReadyPositionX / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.ReadyPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 4, str);
                        }
                        break;

                    case eGridValue.EjectPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.EjectPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Eject Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.StageX.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Loader] Stage Eject Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.EjectPositionX / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.EjectPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 5, str);
                        }
                        break;

                    case eGridValue.StageFirstPickPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.StageFirstPickPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage First Pick Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.StageX.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Loader] Stage First Pick Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.StageFirstPickPositionX / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.StageFirstPickPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 5, str);
                        }
                        break;

                    case eGridValue.StageMagazineChangePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.StageMagazineChangePositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Magazine Change Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.StageX.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Loader] Stage Magazine Change Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.StageMagazineChangePositionX / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.StageMagazineChangePositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 5, str);
                        }
                        break;

                    //case eGridValue.WorkPosX:
                    //    DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.WorkPositionX) / 1000;
                    //    if (!XModuleMain.frmBox.fpFloatEdit("Stage Work Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.StageX.ActualPosition / 1000.0, Stage_Negative / 1000.0, Stage_Positive / 1000.0))
                    //        return;
                    //    {
                    //        str = String.Format("[Lens Loader] Stage Work Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.WorkPositionX / 1000.0, DValue);
                    //        cDEF.Work.TeachLensLoader.WorkPositionX = (int)(DValue * 1000.0);
                    //        cDEF.Work.TeachLensLoader.Save();
                    //        cDEF.Run.LogData(cLog.Form_LensLoader_Data + 6, str);
                    //    }
                    //    break;

                    case eGridValue.SlotCount:
                        Value = cDEF.Work.LensLoader.SlotCount;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Magazine Slot Count", ref Value, " ea", 5, 20))
                            return;
                        {
                            str = String.Format("[Lens Loader] Magazine Slot Count {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.SlotCount, Value);
                            cDEF.Work.LensLoader.SlotCount = Value;
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LensLoader.Information.Lens_Magazine.Init(cDEF.Work.LensLoader.SlotCount, cDEF.Work.LensLoader.TrayCountX, cDEF.Work.LensLoader.TrayCountY);
                            cDEF.frmPageOperation.LensMagazineObjectInit();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 7, str);
                        }
                        break;

                    case eGridValue.SlotPitch:
                        DValue = Convert.ToDouble(cDEF.Work.LensLoader.SlotPitchZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Magazine Slot Pitch Z", ref DValue, " mm", 0, 100))
                            return;
                        {
                            str = String.Format("[Lens Loader] Magazine Slot Pitch Z {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.SlotPitchZ / 1000.0, DValue);
                            cDEF.Work.LensLoader.SlotPitchZ = (int)(DValue * 1000.0);
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 8, str);
                        }
                        break;

                    case eGridValue.InOutOffsetZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensLoader.InOutOffsetZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Magazine In/Out Offset Z", ref DValue, " mm", 0, 5))
                            return;
                        {
                            str = String.Format("[Lens Loader] Magazine In/Out Offset Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensLoader.InOutOffsetZ / 1000.0, DValue);
                            cDEF.Work.TeachLensLoader.InOutOffsetZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 9, str);
                        }
                        break;

                    case eGridValue.TrayCountX:
                        Value = cDEF.Work.LensLoader.TrayCountX;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Tray Count X", ref Value, " ea", 5, 100))
                            return;
                        {
                            str = String.Format("[Lens Loader] Tray Count X {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.TrayCountX, Value);
                            cDEF.Work.LensLoader.TrayCountX = Value;
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 10, str);
                        }
                        break;

                    case eGridValue.TrayCountY:
                        Value = cDEF.Work.LensLoader.TrayCountY;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Tray Count Y", ref Value, " ea", 5, 100))
                            return;
                        {
                            str = String.Format("[Lens Loader] Tray Count Y {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.TrayCountY, Value);
                            cDEF.Work.LensLoader.TrayCountY = Value;
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 11, str);
                        }
                        break;

                    case eGridValue.TrayPitchX:
                        DValue = Convert.ToDouble(cDEF.Work.LensLoader.TrayPitchX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Tray Pitch X", ref DValue, " mm", 0, 30))
                            return;
                        {
                            str = String.Format("[Lens Loader] Tray Pitch X {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.TrayPitchX / 1000.0, DValue);
                            cDEF.Work.LensLoader.TrayPitchX = (int)(DValue * 1000.0);
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 12, str);
                        }
                        break;

                    case eGridValue.TrayPitchY:
                        DValue = Convert.ToDouble(cDEF.Work.LensLoader.TrayPitchY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Tray Pitch Y", ref DValue, " mm", 0, 30))
                            return;
                        {
                            str = String.Format("[Lens Loader] Tray Pitch Y {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.TrayPitchY / 1000.0, DValue);
                            cDEF.Work.LensLoader.TrayPitchY = (int)(DValue * 1000.0);
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 13, str);
                        }
                        break;

                    case eGridValue.MovingDelayZ:
                        DValue = Convert.ToDouble(cDEF.Work.LensLoader.MovingDelayZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Magazine Moving Delay Z", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Loader] Magazine Moving Delay Z {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.MovingDelayZ / 1000.0, DValue);
                            cDEF.Work.LensLoader.MovingDelayZ = (int)(DValue * 1000.0);
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 14, str);
                        }
                        break;

                    case eGridValue.MovingDelayY:
                        DValue = Convert.ToDouble(cDEF.Work.LensLoader.MovingDelayY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Transfer Moving Delay Y", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Loader] Transfer Moving Delay Y {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.MovingDelayY / 1000.0, DValue);
                            cDEF.Work.LensLoader.MovingDelayY = (int)(DValue * 1000.0);
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 15, str);
                        }
                        break;

                    case eGridValue.MovingDelayX:
                        DValue = Convert.ToDouble(cDEF.Work.LensLoader.MovingDelayX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Moving Delay X", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Loader] Stage Moving Delay X {0:0.000} to {1:0.000}", cDEF.Work.LensLoader.MovingDelayX / 1000.0, DValue);
                            cDEF.Work.LensLoader.MovingDelayX = (int)(DValue * 1000.0);
                            cDEF.Work.LensLoader.Save();
                            cDEF.Run.LogData(cLog.Form_LensLoader_Data + 16, str);
                        }
                        break;
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
                cnt = cDEF.Run.LensLoader.MotionCount;
                for (i = 0; i < cnt; i++)
                    cDEF.Run.LensLoader.Motions[i].Stop();
            }
        }
        private void Jog_Relative_MouseUp2(object sender, MouseEventArgs e)
        {
            if (FJogMouseDown)
            {
                FJogMouseDown = false;

                int i, cnt;
                cnt = cDEF.Run.LensPicker.MotionCount;
                for (i = 0; i < cnt; i++)
                    cDEF.Run.LensPicker.Motions[i].Stop();
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
                        if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.LensLoader.Motions[(int)eAxis.MagazineZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.LensLoader.Motions[(int)eAxis.MagazineZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.LensPicker.Is_Stage_EjectPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.LensLoader.Motions[(int)eAxis.TransferY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 3:
                        FDirection = 1;
                        
                        cDEF.Run.LensLoader.Motions[(int)eAxis.TransferY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 4:
                        if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.LensPicker.Motions[4].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 5:
                        if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.LensPicker.Motions[4].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                }
            }
            else
            {
                switch (FTag)
                {
                    case 0:
                        if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensLoader.Motions[(int)eAxis.MagazineZ].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensLoader.Motions[(int)eAxis.MagazineZ].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.LensPicker.Is_Stage_EjectPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensLoader.Motions[(int)eAxis.TransferY].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 3:
                        if (!cDEF.Run.LensPicker.Is_Stage_EjectPositionX())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensLoader.Motions[(int)eAxis.TransferY].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 4:
                        if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensPicker.Motions[4].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 5:
                        if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensPicker.Motions[4].Relative(RelativePosition, FSpeedLevel);
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
                    btnSpeed.Text = cDEF.Lang.Trans("SPEED") + $":{FSpeedLevel}%";
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
                    if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Magazine Ready Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.Move_Magazine_ReadyPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 1, "[Lens Loader] Move Magazine Ready Position Z.");
                    }
                    break;
                case "btnReadyPositionY":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Transfer Ready Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.Move_Transfer_ReadyPositionY();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 2, "[Lens Loader] Move Transfer Ready Position Y.");
                    }
                    break;
                case "btnStagePositionY":
                    if (!cDEF.Run.LensPicker.Is_Stage_EjectPositionX())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Transfer Stage Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.Move_Transfer_StagePositionY();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 3, "[Lens Loader] Move Transfer Stage Position Y.");
                    }
                    break;
                case "btnMagazinePositionY":
                    if (!cDEF.Run.LensPicker.Is_Stage_EjectPositionX())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Stage is Not Eject Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Transfer Magazine Position Y?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.Move_Transfer_MagazinePositionY();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 4, "[Lens Loader] Move Transfer Magazine Position Y.");
                    }
                    break;
                case "btnReadyPositionX":
                    if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Stage Ready Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Stage_ReadyPositionX();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 5, "[Lens Loader] Move Stage Ready Position X.");
                    }
                    break;
                case "btnEjectPositionX":
                    if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Stage Eject Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Stage_EjectPositionX();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 6, "[Lens Loader] Move Stage Eject Position X.");
                    }
                    break;
                case "btnMagazineChange":
                    if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Stage Magazine Change Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Stage_MagazineChangePositionX();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 6, "[Lens Loader] Move Stage Magazine Change Position X.");
                    }
                    break;
                case "btnWorkPositionX":
                    if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Stage Work Position X?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Stage_WorkPositionX();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 7, "[Lens Loader] Move Stage Work Position X.");
                    }
                    break;
                case "btnRelativeUpPositionZ":
                    if (!cDEF.Run.LensLoader.Is_Transfer_ReadyPositionY())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Transfer is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Loader Magazine Relative Up Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.Move_Magazine_Relative_UpZ();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 8, "[Lens Loader] Move Magazine Relative Up Position Z.");
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
                case "btnLoading":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Load Stage?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_LensLoading;
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 100, "[Semi Auto] Loading.");
                    }
                    break;
                case "btnUnloading":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Unload Stage?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_LensUnloading;
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 100, "[Semi Auto] Unloading.");
                    }
                    break;
            }
        }

        private List<Button> ListLensButton;

        public void Manual_SlotInit()
        {
            ListLensButton.Clear();
            pnLensMagazine.Controls.Clear();

            Label labeltitle = new Label();
            labeltitle.Name = "lbLens_MagazineTitle";
            labeltitle.Font = new Font("Tahoma", 9, FontStyle.Bold);
            labeltitle.Text = "MANUAL SLOT MOVE";
            labeltitle.TextAlign = ContentAlignment.MiddleCenter;
            labeltitle.FlatStyle = FlatStyle.Standard;
            labeltitle.BorderStyle = BorderStyle.None;
            labeltitle.AutoSize = false;
            labeltitle.BackColor = Color.Gainsboro;
            labeltitle.ForeColor = Color.Navy;
            labeltitle.Size = new Size(pnLensMagazine.Width - 2, 24);
            labeltitle.Location = new Point(1, 1);
            pnLensMagazine.Controls.Add(labeltitle);

            for (int i = 0; i < cDEF.Work.LensLoader.SlotCount; i++)
            {
                Button button = new Button();
                button.TabIndex = i;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.FlatStyle = FlatStyle.Flat;
                //button.BorderStyle = BorderStyle.FixedSingle;
                button.AutoSize = false;
                //Cst Size
                button.ForeColor = Color.Black;
                button.BackColor = Color.White;
                button.Visible = true;
                button.Click += Manual_Move_Slot;
                button.Height = (pnLensMagazine.Height - 34) / cDEF.Work.LensLoader.SlotCount;
                button.Width = pnLensMagazine.Width + 2;
                int Top = 30 + (i * button.Height) + 2;
                button.Location = new Point(-1, Top);
                pnLensMagazine.Controls.Add(button);
                ListLensButton.Add(button);

                if (!cDEF.Work.Recipe.LensMagazineDirection)
                {
                    button.Name = $"lbVCM_Slot{cDEF.Work.LensLoader.SlotCount - i}";
                    button.Text = $"Slot #{cDEF.Work.LensLoader.SlotCount - i}";
                    button.Tag = cDEF.Work.LensLoader.SlotCount - (i + 1);
                }
                else
                {
                    button.Name = $"lbVCM_Slot{i + 1}";
                    button.Text = $"Slot #{i + 1}";
                    button.Tag = i;
                }
            }
        }

        private void Manual_Move_Slot(object sender, EventArgs e)
        {
            string FButton;
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            int FTag = Convert.ToInt32((sender as Button).Tag);

            if (XModuleMain.frmBox.MessageBox("MANUAL", $"Move Lens Loader Magazine Slot #{FTag + 1} Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
            {
                cDEF.Run.LensLoader.Move_Magazine_WorkingPositionZ(FTag);
                cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 0, $"[Lens Loader] Move Magazine Slot #{FTag + 1} Position Z.");
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

            switch (FName)
            {
                case "btnTransferUnclamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Transfer Unclamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.TransferClamp.Backward();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 9, "[Lens Loader] Move Transfer Unclamp.");
                    }
                    break;
                case "btnTransferClamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Transfer Clamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.TransferClamp.Forward();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 10, "[Lens Loader] Move Transfer Clamp.");
                    }
                    break;
                case "btnStageUnclamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Stage Unclamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.StageClamp.Backward();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 11, "[Lens Loader] Move Stage Unclamp.");
                    }
                    break;
                case "btnStageClamp":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Stage Clamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensLoader.StageClamp.Forward();
                        cDEF.Run.LogEvent(cLog.Form_LensLoader_Event + 12, "[Lens Loader] Move Stage Clamp.");
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
            lbSemiAutoTitle.Text = cDEF.Lang.Trans("SEMI AUTO");
            lbSpeed.Text = cDEF.Lang.Trans("Speed");
            btnSpeed.Text = cDEF.Lang.Trans("SPEED");
            lbCurrentPositionX.Text = cDEF.Lang.Trans("Current Position");
            lbCurrentPositionY.Text = cDEF.Lang.Trans("Current Position");
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
            btnReadyPositionY.Text = cDEF.Lang.Trans("READY POSITION Y");
            btnMagazinePositionY.Text = cDEF.Lang.Trans("MAGAZINE POSITION Y");
            btnStagePositionY.Text = cDEF.Lang.Trans("STAGE POSITION Y");
            btnTransferUnclamp.Text = cDEF.Lang.Trans("UNCLAMP");
            btnTransferClamp.Text = cDEF.Lang.Trans("CLAMP");
            btnReadyPositionX.Text = cDEF.Lang.Trans("READY POSITION X");
            //btnEjectPositionX.Text = cDEF.Lang.Trans("WORK POSITION X");
            btnStageClamp.Text = cDEF.Lang.Trans("CLAMP");
            btnStageUnclamp.Text = cDEF.Lang.Trans("UNCLAMP");
        }

       
    }
}
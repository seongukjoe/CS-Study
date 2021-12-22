using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using XModule.Standard;

namespace XModule.Forms.FormWorking
{
    public partial class FormPageWorkingLensPicker : TFrame
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
        int Slot;                                   //메뉴얼 슬롯 세팅
        private int StageIndexX = 0;
        private int StageIndexY = 0;
        #endregion

        public enum eGridValue
        {
            VacuumDelay,
            BlowDelay,
            StepPlaceSpeed,
            //BottomCamStepSpeed,
            Space1,
            MovingDelayX,
            MovingDelayY,
            MovingDelayZ,
            MovingDelayT,
            Space2,
            UpperGrabDelay,
            UnderGrabDelay,
            Space3,
            ReadyPosX,
            ReadyPosY,
            ReadyPosZ,
            ReadyPosT,
            Space4,
            StagePickPosX,
            StagePickPosY,
            StagePickPosZ,
            StageStepPickOffset,
            StagePickPosT,
            Space5,
            IndexPlacePosX,
            IndexPlacePosY,
            IndexPlacePosZ,
            IndexStepPlaceOffset,
            IndexPlacePosT,
            Space6,
            LockPositionT,
            LockingUp,
            Space7,
            BottomCamPosX,
            BottomCamPosY,
            BottomCamPosZ,
            //BottomCamStepPos,
            BottomCamPosT,
            LensOffsetT,
            Space8,
            CameraDistanceOffsetX,
            CameraDistanceOffsetY,
            PlaceUserOffsetX,
            PlaceUserOffsetY,
            //PlaceUserOffsetX_1,
            //PlaceUserOffsetY_1,
            //PlaceUserOffsetX_2,
            //PlaceUserOffsetY_2,
            //PlaceUserOffsetX_3,
            //PlaceUserOffsetY_3,
            //PlaceUserOffsetX_4,
            //PlaceUserOffsetY_4,
            //PlaceUserOffsetX_5,
            //PlaceUserOffsetY_5,
            //PlaceUserOffsetX_6,
            //PlaceUserOffsetY_6,
            //PlaceUserOffsetX_7,
            //PlaceUserOffsetY_7,
            //PlaceUserOffsetX_8,
            //PlaceUserOffsetY_8,
            //PlaceUserOffsetX_9,
            //PlaceUserOffsetY_9,
            //PlaceUserOffsetX_10,
            //PlaceUserOffsetY_10,
            //PlaceUserOffsetX_11,
            //PlaceUserOffsetY_11,
            //PlaceUserOffsetX_12,
            //PlaceUserOffsetY_12,

        }

        private enum eAxis
        {
            HeadX,
            HeadY,
            HeadZ,
            HeadT
        }


        public FormPageWorkingLensPicker()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 1000);
        }
        private void FormPageWorkingLensPicker_Load(object sender, EventArgs e)
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



                lbHeadX_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.LensPicker.Motions[(int)eAxis.HeadX].ActualPosition, true);
                lbHeadY_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.LensPicker.Motions[(int)eAxis.HeadY].ActualPosition, true);
                lbHeadZ_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.LensPicker.Motions[(int)eAxis.HeadZ].ActualPosition, true);
                lbHeadT_Position.Text = cFnc.GetUnitValueString((int)cDEF.Run.LensPicker.Motions[(int)eAxis.HeadT].ActualPosition, true);

                lbSelectStageTrayX.Text = StageIndexX.ToString();
                lbSelectStageTrayY.Text = StageIndexY.ToString();

                lbRelative_Position.Text = cFnc.GetUnitValueString((int)RelativePosition, true);

                if (Convert.ToBoolean(cDEF.Run.LensPicker.HeadX.FAlarm))
                {
                    lbX.BackColor = Color.Red;
                }
                else
                {
                    lbX.BackColor = Convert.ToBoolean(cDEF.Run.LensPicker.HeadX.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.LensPicker.HeadY.FAlarm))
                {
                    lbY.BackColor = Color.Red;
                }
                else
                {
                    lbY.BackColor = Convert.ToBoolean(cDEF.Run.LensPicker.HeadY.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.LensPicker.HeadZ.FAlarm))
                {
                    lbZ.BackColor = Color.Red;
                }
                else
                {
                    lbZ.BackColor = Convert.ToBoolean(cDEF.Run.LensPicker.HeadZ.FInposition) ? Color.White : Color.Lime;
                }
                if (Convert.ToBoolean(cDEF.Run.LensPicker.HeadT.FAlarm))
                {
                    lbT.BackColor = Color.Red;
                }
                else
                {
                    lbT.BackColor = Convert.ToBoolean(cDEF.Run.LensPicker.HeadT.FInposition) ? Color.White : Color.Lime;
                }
                //Position Check
                lbReadyPositionXY.BackColor = cDEF.Run.LensPicker.Is_Head_ReadyPositionX() && cDEF.Run.LensPicker.Is_Head_ReadyPositionY() ? Color.Lime : Color.White;
                lbIndexPlacePositionXY.BackColor = cDEF.Run.LensPicker.Is_Head_IndexPlacePositionX() && cDEF.Run.LensPicker.Is_Head_IndexPlacePositionY() ? Color.Lime : Color.White;
                lbStagePickPositionXY.BackColor = cDEF.Run.LensPicker.Is_Head_StagePickPositionX() && cDEF.Run.LensPicker.Is_Head_StageFirstPickPositionY()? Color.Lime : Color.White;
                lbBottomCamXY.BackColor = cDEF.Run.LensPicker.Is_Head_BottomCamPositionX() && cDEF.Run.LensPicker.Is_Head_BottomCamPositionY() ? Color.Lime : Color.White;

                lbReadyPositionZ.BackColor = cDEF.Run.LensPicker.Is_Head_ReadyPositionZ() ? Color.Lime : Color.White;
                lbIndexPlacePositionZ.BackColor = cDEF.Run.LensPicker.Is_Head_IndexPlacePositionZ() ? Color.Lime : Color.White;
                lbStagePickPositionZ.BackColor = cDEF.Run.LensPicker.Is_Head_StagePickPositionZ() ? Color.Lime : Color.White;
                lbBottomCamZ.BackColor = cDEF.Run.LensPicker.Is_Head_BottomCamPositionZ() ? Color.Lime : Color.White;

                lbReadyPositionT.BackColor = cDEF.Run.LensPicker.Is_Head_ReadyPositionT() ? Color.Lime : Color.White;
                lbIndexPlacePositionT.BackColor = cDEF.Run.LensPicker.Is_Head_IndexPlacePositionT() ? Color.Lime : Color.White;
                lbStagePickPositionT.BackColor = cDEF.Run.LensPicker.Is_Head_StagePickPositionT() ? Color.Lime : Color.White;
                lbBottomCamT.BackColor = cDEF.Run.LensPicker.Is_Head_StageFirstPickPositionY() ? Color.Lime : Color.White;

                //IO Check
                lbVacuumCheck.BackColor = cDEF.Run.Digital.Input[cDI.LensInsert_Vacuum_Check] ? Color.Orange : Color.White;
                btnVacuum.ForeColor = cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum] ? Color.Red : Color.Black;
                btnBlow.ForeColor = cDEF.Run.Digital.Output[cDO.Lens_Insert_Blow] ? Color.Blue : Color.Black;

                
                lbTorqueZ.Text = cDEF.Run.LensPicker.HeadZ.TorqueValue.ToString();
                lbTorqueT.Text = cDEF.Run.LensPicker.HeadT.TorqueValue.ToString();

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
            //GridAdd("R", "Bottom Cam Step Place Speed", "Speed");
            GridAdd_Space();
            GridAdd("R", "Vision Upper Grab Delay", "TIme");
            GridAdd("R", "Vision Under Grab Delay", "TIme");
            GridAdd_Space();
            GridAdd("R", "Ready Position X", "Head X");
            GridAdd("R", "Ready Position Y", "Head Y");
            GridAdd("R", "Ready Position Z", "Head Z");
            GridAdd("R", "Ready Position T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Stage Pick Position X", "Head X");
            GridAdd("R", "Stage First Pick Position Y", "Head Y");
            GridAdd("R", "Stage Pick Position Z", "Head Z");
            GridAdd("R", "Stage Step Pick Offset Z", "Head Z");
            GridAdd("R", "Stage Pick Position T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Index Place Position X", "Head X");
            GridAdd("R", "Index Place Position Y", "Head Y");
            GridAdd("R", "Index Place Position Z", "Head Z");
            GridAdd("R", "Index Step Place Offset Z", "Head Z");
            GridAdd("R", "Index Place Position T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Lock Position T", "Head T");
            GridAdd("R", "Locking Up Z", "Head Z");
            GridAdd_Space();
            GridAdd("R", "Bottom Cam Position X", "Head X");
            GridAdd("R", "Bottom Cam Position Y", "Head Y");
            GridAdd("R", "Bottom Cam Position Z", "Head Z");
            //GridAdd("T", "Bottom Cam Step Position Z", "Head Z");
            GridAdd("R", "Bottom Cam Position T", "Head T");
            GridAdd("R", "Lens Offset T", "Head T");
            GridAdd_Space();
            GridAdd("R", "Camera Distance Offset X", "Head X");
            GridAdd("R", "Camera Distance Offset Y", "Head Y");
            GridAdd("R", "Place User Offset X", "Head X");
            GridAdd("R", "Place User Offset Y", "Head Y");
            
        }

        private void Grid_Update()
        {
            for (int i = 0; i < MotionDataGrid.RowCount; i++)
            {
                switch((eGridValue)i)
                {
                    case eGridValue.ReadyPosX:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosX].Value = ((double)cDEF.Work.TeachLensPicker.ReadyPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosY:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosY].Value = ((double)cDEF.Work.TeachLensPicker.ReadyPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosZ:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosZ].Value = ((double)cDEF.Work.TeachLensPicker.ReadyPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.ReadyPosT:
                        MotionDataGrid[3, (int)eGridValue.ReadyPosT].Value = ((double)cDEF.Work.TeachLensPicker.ReadyPositionT / 100.0).ToString("N3") + " ˚";
                        break;

                    case eGridValue.StagePickPosX:
                        MotionDataGrid[3, (int)eGridValue.StagePickPosX].Value = ((double)cDEF.Work.TeachLensPicker.StagePickPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePickPosY:
                        MotionDataGrid[3, (int)eGridValue.StagePickPosY].Value = ((double)cDEF.Work.TeachLensPicker.StageFirstPickPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePickPosZ:
                        MotionDataGrid[3, (int)eGridValue.StagePickPosZ].Value = ((double)cDEF.Work.TeachLensPicker.StagePickPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StageStepPickOffset:
                        MotionDataGrid[3, (int)eGridValue.StageStepPickOffset].Value = ((double)cDEF.Work.TeachLensPicker.StageStepPickOffset / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.StagePickPosT:
                        MotionDataGrid[3, (int)eGridValue.StagePickPosT].Value = ((double)cDEF.Work.TeachLensPicker.StagePickPositionT / 100.0).ToString("N3") + " ˚";
                        break;

                    case eGridValue.IndexPlacePosX:
                        MotionDataGrid[3, (int)eGridValue.IndexPlacePosX].Value = ((double)cDEF.Work.TeachLensPicker.IndexPlacePositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPlacePosY:
                        MotionDataGrid[3, (int)eGridValue.IndexPlacePosY].Value = ((double)cDEF.Work.TeachLensPicker.IndexPlacePositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPlacePosZ:
                        MotionDataGrid[3, (int)eGridValue.IndexPlacePosZ].Value = ((double)cDEF.Work.TeachLensPicker.IndexPlacePositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexStepPlaceOffset:
                        MotionDataGrid[3, (int)eGridValue.IndexStepPlaceOffset].Value = ((double)cDEF.Work.TeachLensPicker.IndexStepPlaceOffset / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.IndexPlacePosT:
                        MotionDataGrid[3, (int)eGridValue.IndexPlacePosT].Value = ((double)cDEF.Work.TeachLensPicker.IndexPlacePositionT / 100.0).ToString("N3") + " ˚";
                        break;

                    case eGridValue.LockPositionT:
                        MotionDataGrid[3, (int)eGridValue.LockPositionT].Value = ((double)cDEF.Work.TeachLensPicker.LockingPositionT / 100.0).ToString("N3") + " ˚";
                        break;
                    case eGridValue.LockingUp:
                        MotionDataGrid[3, (int)eGridValue.LockingUp].Value = ((double)cDEF.Work.TeachLensPicker.LockingUp / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.BottomCamPosX:
                        MotionDataGrid[3, (int)eGridValue.BottomCamPosX].Value = ((double)cDEF.Work.TeachLensPicker.BottomCamPositionX / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.BottomCamPosY:
                        MotionDataGrid[3, (int)eGridValue.BottomCamPosY].Value = ((double)cDEF.Work.TeachLensPicker.BottomCamPositionY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.BottomCamPosZ:
                        MotionDataGrid[3, (int)eGridValue.BottomCamPosZ].Value = ((double)cDEF.Work.TeachLensPicker.BottomCamPositionZ / 1000.0).ToString("N3") + " mm";
                        break;
                    //case eGridValue.BottomCamStepPos:
                    //    MotionDataGrid[3, (int)eGridValue.BottomCamStepPos].Value = ((double)cDEF.Work.TeachLensPicker.BottomCamStepPositionZ / 1000.0).ToString("N3") + " mm";
                    //    break;
                    case eGridValue.BottomCamPosT:
                        MotionDataGrid[3, (int)eGridValue.BottomCamPosT].Value = ((double)cDEF.Work.TeachLensPicker.BottomCamPositionT / 100.0).ToString("N3") + " ˚";
                        break;
                    case eGridValue.LensOffsetT:
                        MotionDataGrid[3, (int)eGridValue.LensOffsetT].Value = ((double)cDEF.Work.TeachLensPicker.LensOffsetT / 100.0).ToString("N3") + " ˚";
                        break;
                    case eGridValue.MovingDelayX:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayX].Value = ((double)cDEF.Work.LensPicker.MovingDelayX / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayY:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayY].Value = ((double)cDEF.Work.LensPicker.MovingDelayY / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.MovingDelayZ:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayZ].Value = ((double)cDEF.Work.LensPicker.MovingDelayZ / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.UpperGrabDelay:
                        MotionDataGrid[3, (int)eGridValue.UpperGrabDelay].Value = ((double)cDEF.Work.LensPicker.LensUpperGrabDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.UnderGrabDelay:
                        MotionDataGrid[3, (int)eGridValue.UnderGrabDelay].Value = ((double)cDEF.Work.LensPicker.LensUnderGrabDelay / 1000.0).ToString("N3") + " sec";
                        break;

                    case eGridValue.MovingDelayT:
                        MotionDataGrid[3, (int)eGridValue.MovingDelayT].Value = ((double)cDEF.Work.LensPicker.MovingDelayT / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.VacuumDelay:
                        MotionDataGrid[3, (int)eGridValue.VacuumDelay].Value = ((double)cDEF.Work.LensPicker.LensVacDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.BlowDelay:
                        MotionDataGrid[3, (int)eGridValue.BlowDelay].Value = ((double)cDEF.Work.LensPicker.LensBlowDelay / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.StepPlaceSpeed:
                        MotionDataGrid[3, (int)eGridValue.StepPlaceSpeed].Value = ((double)cDEF.Work.LensPicker.StepPlaceSpeed).ToString() + " %";
                        break;
                    //case eGridValue.BottomCamStepSpeed:
                    //    MotionDataGrid[3, (int)eGridValue.BottomCamStepSpeed].Value = ((double)cDEF.Work.LensPicker.BottomCamStepSpeed / 1000.0).ToString() + " %";
                    //    break;

                    case eGridValue.CameraDistanceOffsetX:
                        MotionDataGrid[3, (int)eGridValue.CameraDistanceOffsetX].Value = ((double)cDEF.Work.TeachLensPicker.CameraDistanceOffsetX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.CameraDistanceOffsetY:
                        MotionDataGrid[3, (int)eGridValue.CameraDistanceOffsetY].Value = ((double)cDEF.Work.TeachLensPicker.CameraDistanceOffsetY / 1000.0).ToString("N3") + " mm";
                        break;
                    case eGridValue.PlaceUserOffsetX:
                        MotionDataGrid[3, (int)eGridValue.PlaceUserOffsetX].Value = ((double)cDEF.Work.Recipe.IndexOffsetX / 1000.0).ToString("N3") + " mm";
                        break;

                    case eGridValue.PlaceUserOffsetY:
                        MotionDataGrid[3, (int)eGridValue.PlaceUserOffsetY].Value = ((double)cDEF.Work.Recipe.IndexOffsetY / 1000.0).ToString("N3") + " mm";
                        break;
                }
                
            }
        }
        private void GridAdd(string section, string name, string unit)
        {
            
            string[] str = { $"{section}",$"{name}", $"{unit}" };
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


            HeadX_Negative = cDEF.Run.LensPicker.HeadX.Config.FLimit.FSoftwareNegative;
            HeadX_Positive = cDEF.Run.LensPicker.HeadX.Config.FLimit.FSoftwarePositive;

            HeadY_Negative = cDEF.Run.LensPicker.HeadY.Config.FLimit.FSoftwareNegative;
            HeadY_Positive = cDEF.Run.LensPicker.HeadY.Config.FLimit.FSoftwarePositive;

            HeadZ_Negative = cDEF.Run.LensPicker.HeadZ.Config.FLimit.FSoftwareNegative;
            HeadZ_Positive = cDEF.Run.LensPicker.HeadZ.Config.FLimit.FSoftwarePositive;

            HeadT_Negative = cDEF.Run.LensPicker.HeadT.Config.FLimit.FSoftwareNegative;
            HeadT_Positive = cDEF.Run.LensPicker.HeadT.Config.FLimit.FSoftwarePositive;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            eGridValue eGrid = (eGridValue)Row;
            if (Col == 3)
            {
                switch (eGrid)
                {
                    case eGridValue.ReadyPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.ReadyPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Ready Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.ReadyPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 0, str);
                        }
                        break;

                    case eGridValue.ReadyPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.ReadyPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Ready Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.ReadyPositionY / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.ReadyPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 1, str);
                        }
                        break;

                    case eGridValue.ReadyPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.ReadyPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Ready Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.ReadyPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.ReadyPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 2, str);
                        }
                        break;

                    case eGridValue.ReadyPosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.ReadyPositionT) / 100;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Ready Position T", ref DValue, " ˚", "CURRENT", cDEF.Run.LensPicker.HeadT.ActualPosition / 100.0, HeadT_Negative / 100.0, HeadT_Positive / 100.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Ready Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.ReadyPositionT / 100.0, DValue);
                            cDEF.Work.TeachLensPicker.ReadyPositionT = (int)(DValue * 100.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 3, str);
                        }
                        break;

                    case eGridValue.StagePickPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.StagePickPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Pick Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Stage Pick Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.StagePickPositionX / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.StagePickPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 4, str);
                        }
                        break;

                    case eGridValue.StagePickPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.StageFirstPickPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Pick Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Stage Pick Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.StageFirstPickPositionY / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.StageFirstPickPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 5, str);
                        }
                        break;

                    case eGridValue.StagePickPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.StagePickPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Pick Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Stage Pick Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.StagePickPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.StagePickPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 6, str);
                        }
                        break;

                    case eGridValue.StageStepPickOffset:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.StageStepPickOffset) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Step Pick Offset Z", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadZ.ActualPosition / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Stage Step Pick Offset Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.StageStepPickOffset / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.StageStepPickOffset = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 10, str);
                        }
                        break;

                    case eGridValue.StagePickPosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.StagePickPositionT) / 100;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Pick Position T", ref DValue, " ˚", "CURRENT", cDEF.Run.LensPicker.HeadT.ActualPosition / 100.0, HeadT_Negative / 100.0, HeadT_Positive / 100.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Stage Pick Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.StagePickPositionT / 100.0, DValue);
                            cDEF.Work.TeachLensPicker.StagePickPositionT = (int)(DValue * 100.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 7, str);
                        }
                        break;

                    case eGridValue.IndexPlacePosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.IndexPlacePositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Index Place Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.IndexPlacePositionX / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.IndexPlacePositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 8, str);
                        }
                        break;

                    case eGridValue.IndexPlacePosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.IndexPlacePositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Index Place Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.IndexPlacePositionY / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.IndexPlacePositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 9, str);
                        }
                        break;

                    case eGridValue.IndexPlacePosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.IndexPlacePositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Index Place Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.IndexPlacePositionZ / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.IndexPlacePositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 10, str);
                        }
                        break;

                    case eGridValue.IndexStepPlaceOffset:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.IndexStepPlaceOffset) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Step Place Offset Z", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadZ.ActualPosition / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Index Step Place Offset Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.IndexStepPlaceOffset / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.IndexStepPlaceOffset = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 10, str);
                        }
                        break;

                    case eGridValue.IndexPlacePosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.IndexPlacePositionT) / 100;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position T", ref DValue, " ˚", "CURRENT", cDEF.Run.LensPicker.HeadT.ActualPosition / 100.0, HeadT_Negative / 100.0, HeadT_Positive / 100.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Index Place Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.IndexPlacePositionT / 100.0, DValue);
                            cDEF.Work.TeachLensPicker.IndexPlacePositionT = (int)(DValue * 100.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 11, str);
                        }
                        break;

                    case eGridValue.LockPositionT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.LockingPositionT) / 100;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Index Place Position T", ref DValue, " ˚", "CURRENT", cDEF.Run.LensPicker.HeadT.ActualPosition / 100.0, HeadT_Negative / 100.0, HeadT_Positive / 100.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Index Lock Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.LockingPositionT / 100.0, DValue);
                            cDEF.Work.TeachLensPicker.LockingPositionT = (int)(DValue * 100.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 11, str);
                        }
                        break;

                    case eGridValue.LockingUp:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.LockingUp) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Stage Pick Position Z", ref DValue, " mm"))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Stage Pick Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.LockingUp / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.LockingUp = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 6, str);
                        }
                        break;

                    case eGridValue.BottomCamPosX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.BottomCamPositionX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Bottom Cam Position X", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadX.ActualPosition / 1000.0, HeadX_Negative / 1000.0, HeadX_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Bottom Cam Place Position X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.BottomCamPositionX / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.BottomCamPositionX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 8, str);
                        }
                        break;

                    case eGridValue.BottomCamPosY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.BottomCamPositionY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Bottom Cam Position Y", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadY.ActualPosition / 1000.0, HeadY_Negative / 1000.0, HeadY_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Bottom Cam Position Y {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.BottomCamPositionY / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.BottomCamPositionY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 9, str);
                        }
                        break;

                    case eGridValue.BottomCamPosZ:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.BottomCamPositionZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Bottom Cam Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Bottom Cam Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.BottomCamPositionZ / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.BottomCamPositionZ = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 10, str);
                        }
                        break;

                    //case eGridValue.BottomCamStepPos:
                    //    DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.BottomCamStepPositionZ) / 1000;
                    //    if (!XModuleMain.frmBox.fpFloatEdit("Head Bottom Cam Step Position Z", ref DValue, " mm", "CURRENT", cDEF.Run.LensPicker.HeadZ.ActualPosition / 1000.0, HeadZ_Negative / 1000.0, HeadZ_Positive / 1000.0))
                    //        return;
                    //    {
                    //        str = String.Format("[Lens Picker] Head Bottom Cam Step Position Z {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.BottomCamStepPositionZ / 1000.0, DValue);
                    //        cDEF.Work.TeachLensPicker.BottomCamStepPositionZ = (int)(DValue * 1000.0);
                    //        cDEF.Work.TeachLensPicker.Save();
                    //        cDEF.Run.LogData(cLog.Form_LensPicker_Data + 10, str);
                    //    }
                    //    break;

                    case eGridValue.BottomCamPosT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.BottomCamPositionT) / 100;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Bottom Cam Position T", ref DValue, " ˚", "CURRENT", cDEF.Run.LensPicker.HeadT.ActualPosition / 100.0, HeadT_Negative / 100.0, HeadT_Positive / 100.0))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Bottom Cam Position T {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.BottomCamPositionT / 100.0, DValue);
                            cDEF.Work.TeachLensPicker.BottomCamPositionT = (int)(DValue * 100.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 11, str);
                        }
                        break;

                    case eGridValue.LensOffsetT:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.LensOffsetT) / 100;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Bottom Cam Lens Offset T", ref DValue, " ˚", "CURRENT", cDEF.Run.LensPicker.HeadT.ActualPosition / 100.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Bottom Cam Lens Offset T {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.LensOffsetT / 100.0, DValue);
                            cDEF.Work.TeachLensPicker.LensOffsetT = (int)(DValue * 100.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 11, str);
                        }
                        break;

                    case eGridValue.MovingDelayX:
                        DValue = Convert.ToDouble(cDEF.Work.LensPicker.MovingDelayX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Stage Moving Delay X", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Picker] Stage Moving Delay X {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.MovingDelayX / 1000.0, DValue);
                            cDEF.Work.LensPicker.MovingDelayX = (int)(DValue * 1000.0);
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 12, str);
                        }
                        break;

                    case eGridValue.MovingDelayY:
                        DValue = Convert.ToDouble(cDEF.Work.LensPicker.MovingDelayY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay Y", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Moving Delay Y {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.MovingDelayY / 1000.0, DValue);
                            cDEF.Work.LensPicker.MovingDelayY = (int)(DValue * 1000.0);
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 13, str);
                        }
                        break;

                    case eGridValue.MovingDelayZ:
                        DValue = Convert.ToDouble(cDEF.Work.LensPicker.MovingDelayZ) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay Z", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Moving Delay Z {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.MovingDelayZ / 1000.0, DValue);
                            cDEF.Work.LensPicker.MovingDelayZ = (int)(DValue * 1000.0);
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 14, str);
                        }
                        break;

                    case eGridValue.UpperGrabDelay:
                        DValue = Convert.ToDouble(cDEF.Work.LensPicker.LensUpperGrabDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Vision Upper Grab Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Picker] Vision Upper Grab Delay {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.LensUpperGrabDelay / 1000.0, DValue);
                            cDEF.Work.LensPicker.LensUpperGrabDelay = (int)(DValue * 1000.0);
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 14, str);
                        }
                        break;

                    case eGridValue.UnderGrabDelay:
                        DValue = Convert.ToDouble(cDEF.Work.LensPicker.LensUnderGrabDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Vision Under Grab Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Picker] Vision Under Grab Delay{0:0.000} to {1:0.000}", cDEF.Work.LensPicker.LensUnderGrabDelay / 1000.0, DValue);
                            cDEF.Work.LensPicker.LensUnderGrabDelay = (int)(DValue * 1000.0);
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 14, str);
                        }
                        break;

                    case eGridValue.StepPlaceSpeed:
                        Value = cDEF.Work.LensPicker.StepPlaceSpeed;
                        if (!XModuleMain.frmBox.fpIntegerEdit("Index Step Place Speed Z", ref Value, " %", 0, 100))
                            return;
                        {
                            str = String.Format("[Lens Picker] Index Step Place Speed Z {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.StepPlaceSpeed , Value);
                            cDEF.Work.LensPicker.StepPlaceSpeed = Value;
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 14, str);
                        }
                        break;

                    //case eGridValue.BottomCamStepSpeed:
                    //    Value = cDEF.Work.LensPicker.BottomCamStepSpeed);
                    //    if (!XModuleMain.frmBox.fpIntegerEdit("Head Bottom Camera Step Place Speed Z", ref Value, " %", 0, 100))
                    //        return;
                    //    {
                    //        str = String.Format("[Lens Picker] Head Bottom Camera Step Place Speed Z {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.BottomCamStepSpeed,Value);
                    //        cDEF.Work.LensPicker.BottomCamStepSpeed = Value;
                    //        cDEF.Work.LensPicker.Save();
                    //        cDEF.Run.LogData(cLog.Form_LensPicker_Data + 14, str);
                    //    }
                    //    break;

                    case eGridValue.MovingDelayT:
                        DValue = Convert.ToDouble(cDEF.Work.LensPicker.MovingDelayT) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Head Moving Delay T", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Picker] Head Moving Delay T {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.MovingDelayT / 1000.0, DValue);
                            cDEF.Work.LensPicker.MovingDelayT = (int)(DValue * 1000.0);
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 15, str);
                        }
                        break;

                    case eGridValue.VacuumDelay:
                        DValue = Convert.ToDouble(cDEF.Work.LensPicker.LensVacDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Vacuum Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Picker] Vacuum Delay {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.LensVacDelay / 1000.0, DValue);
                            cDEF.Work.LensPicker.LensVacDelay = (int)(DValue * 1000.0);
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 16, str);
                        }
                        break;

                    case eGridValue.BlowDelay:
                        DValue = Convert.ToDouble(cDEF.Work.LensPicker.LensBlowDelay) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Blow Delay", ref DValue, " sec", 0, 10))
                            return;
                        {
                            str = String.Format("[Lens Picker] Blow Delay {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.LensBlowDelay / 1000.0, DValue);
                            cDEF.Work.LensPicker.LensBlowDelay = (int)(DValue * 1000.0);
                            cDEF.Work.LensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 17, str);
                        }
                        break;
                    case eGridValue.CameraDistanceOffsetX:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.CameraDistanceOffsetX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Camera Distance Offset X", ref DValue, " mm", "CURRENT", (cDEF.Run.LensPicker.HeadX.ActualPosition - cDEF.Work.TeachLensPicker.StagePickPositionX) / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Lens Picker] Camera Distance Offset X {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.CameraDistanceOffsetX / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.CameraDistanceOffsetX = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 4, str);
                        }
                        break;

                    case eGridValue.CameraDistanceOffsetY:
                        DValue = Convert.ToDouble(cDEF.Work.TeachLensPicker.CameraDistanceOffsetY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Camera Distance Offset Y", ref DValue, " mm", "CURRENT", (cDEF.Run.LensPicker.HeadY.ActualPosition - cDEF.Work.TeachLensPicker.StageFirstPickPositionY) / 1000.0, -100, 100))
                            return;
                        {
                            str = String.Format("[Lens Picker] Camera Distance Offset Y {0:0.000} to {1:0.000}", cDEF.Work.TeachLensPicker.CameraDistanceOffsetY / 1000.0, DValue);
                            cDEF.Work.TeachLensPicker.CameraDistanceOffsetY = (int)(DValue * 1000.0);
                            cDEF.Work.TeachLensPicker.Save();
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 4, str);
                        }
                        break;
                    case eGridValue.PlaceUserOffsetX:
                        DValue = Convert.ToDouble(cDEF.Work.Recipe.IndexOffsetX) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Place Offset X", ref DValue, " mm", "CURRENT", cDEF.Work.Recipe.IndexOffsetX / 1000.0, -2, 2))
                            return;
                        {
                            str = String.Format("[Lens Picker] Place Offset X {0:0.000} to {1:0.000}", cDEF.Work.Recipe.IndexOffsetX / 1000.0, DValue);
                            cDEF.Work.Recipe.IndexOffsetX = (int)(DValue * 1000.0);
                            cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 18, str);
                        }
                        break;
                    case eGridValue.PlaceUserOffsetY:
                        DValue = Convert.ToDouble(cDEF.Work.Recipe.IndexOffsetY) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Place Offset Y", ref DValue, " mm", "CURRENT", cDEF.Work.Recipe.IndexOffsetY / 1000.0, -2, 2))
                            return;
                        {
                            str = String.Format("[Lens Picker] Place Offset Y {0:0.000} to {1:0.000}", cDEF.Work.Recipe.IndexOffsetY / 1000.0, DValue);
                            cDEF.Work.Recipe.IndexOffsetY = (int)(DValue * 1000.0);
                            cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                            cDEF.Run.LogData(cLog.Form_LensPicker_Data + 19, str);
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
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadX].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 0;
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 3:
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        FDirection = 1;
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadY].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 4:
                        FDirection = 0;
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 5:
                        FDirection = 1;
                        cDEF.Run.LensPicker .Motions[(int)eAxis.HeadZ].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 6:
                        FDirection = 0;
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadT].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                    case 7:
                        FDirection = 1;
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadT].Jog((TfpMotionJogDirection)FDirection, FSpeedLevel);
                        break;
                }
            }
            else
            {
                switch (FTag)
                {
                    case 0:
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ() && RelativePosition > 1000)
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadX].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 1:
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ() && RelativePosition > 1000)
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadX].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 2:
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ() && RelativePosition > 1000)
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadY].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 3:
                        if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ() && RelativePosition > 1000)
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadY].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 4:
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadZ].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 5:
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadZ].Relative(RelativePosition, FSpeedLevel);
                        break;
                    case 6:
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadT].Relative(-RelativePosition, FSpeedLevel);
                        break;
                    case 7:
                        cDEF.Run.LensPicker.Motions[(int)eAxis.HeadT].Relative(RelativePosition, FSpeedLevel);
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
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Ready Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_ReadyPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 1, "[Lens Picker] Move Head Ready Position Z.");
                    }
                    break;
                case "btnStagePickPositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Stage Pick Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (cDEF.Work.Option.PickOverrideUse == 0)
                            cDEF.Run.LensPicker.Move_Head_StagePickPositionZ();
                        else
                            cDEF.Run.LensPicker.Move_Head_StagePickOverridePositionZ();
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 2, "[Lens Picker] Move Head Stage Pick Position Z.");
                    }
                    break;
                case "btnIndexPlacePositionZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Index Place Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        if (cDEF.Work.Option.PlaceOverrideUse == 0)
                            cDEF.Run.LensPicker.Move_Head_IndexPlacePositionZ();
                        else
                            cDEF.Run.LensPicker.Move_Head_IndexPlaceOverridePositionZ();
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 3, "[Lens Picker] Move Head Index Place Position Z.");
                    }
                    break;
                case "btnReadyPositionXY":
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Ready Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_ReadyPositionX();
                        cDEF.Run.LensPicker.Move_Head_ReadyPositionY();
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 4, "[Lens Picker] Move Head Ready Position XY.");
                    }
                    break;
                case "btnStagePickPositionXY":
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.VCMVision.Is_VCMVision_ReadyPosition())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "VCM Vision is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Stage Pick Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_StagePickPositionX();
                        cDEF.Run.LensPicker.Move_Head_StageFirstPickPositionY();
                        cDEF.Run.LensPicker.Move_Stage_FirstPickPositionX();
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 5, "[Lens Picker] Move Head Stage Pick Position XY.");
                    }
                    break;
                case "btnIndexPlacePositionXY":
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.VCMVision.Is_VCMVision_ReadyPosition())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "VCM Vision is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Index Place Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_IndexPlacePositionX();
                        cDEF.Run.LensPicker.Move_Head_IndexPlacePositionY();
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 6, "[Lens Picker] Move Head Index Place Position XY.");
                    }
                    break;
                case "btnReadyPositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Ready Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_ReadyPositionT();
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 7, "[Lens Picker] Move Head Ready Position T.");
                    }
                    break;
                case "btnStagePickPositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Stage Pick Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_StagePickPositionT(0);
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 8, "[Lens Picker] Move Head Stage Pick Position T.");
                    }
                    break;
                case "btnIndexPlacePositionT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Index Place Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_IndexPlacePositionT(0);
                        cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 9, "[Lens Picker] Move Head Index Place Position T.");
                    }
                    break;
                case "btnStageWorkXY":
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.VCMVision.Is_VCMVision_ReadyPosition())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "VCM Vision is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Stage Work Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_WorkPositionXY(StageIndexX, StageIndexY, 0,0);
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Lens Picker] Move Lens Picker Head Stage Work Position XY.");
                    }
                    break;
                case "btnBottomCamXY":
                    if (!cDEF.Run.LensPicker.Is_Head_ReadyPositionZ())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Head Z is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (!cDEF.Run.VCMVision.Is_VCMVision_ReadyPosition())
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "VCM Vision is Not Ready Position", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Bottom Camera Position XY?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_BottomCamPositionX();
                        cDEF.Run.LensPicker.Move_Head_BottomCamPositionY();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Lens Picker] Move Lens Picker Head Bottom Camera Position XY.");
                    }
                    break;
                case "btnBottomCamZ":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Bottom Camera Position Z?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_BottomCamPositionZ();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Lens Picker] Move Lens Picker Head Bottom Camera Position Z.");
                    }
                    break;
                case "btnBottomCamT":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Bottom Camera Position T?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_BottomCamPositionT();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Lens Picker] Move Lens Picker Head Bottom Camera Position T.");
                    }
                    break;

                case "btnCameraRelative":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Camera Relative?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_CameraRelativeXY();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Lens Picker] Move Lens Picker Head Camera Relative.");
                    }
                    break;

                case "btnPickerRelative":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Lens Picker Head Picker Relative?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Move_Head_PickerRelativeXY();
                        cDEF.Run.LogEvent(cLog.Form_UnloaderPicker_Event + 8, "[Lens Picker] Move Lens Picker Head Picker Relative.");
                    }
                    break;
            }
        }

        private void lbTrayIndex_Click(object sender, EventArgs e)
        {
            int StageX = 0;
            int StageY = 0;

            switch ((sender as Label).Name)
            {
                case "lbSelectStageTrayX":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Tray Index", ref StageX, "", 0, cDEF.Work.LensLoader.TrayCountX - 1))
                        return;
                    StageIndexX = StageX;
                    break;

                case "lbSelectStageTrayY":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Tray Index", ref StageY, "", 0, cDEF.Work.LensLoader.TrayCountY - 1))
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
                        cDEF.Run.Mode = Running.TRunMode.Manual_LensPick;
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 100, "[Semi Auto] Pick.");
                    }
                    break;
                case "btnPlace":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Place?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_LensPlace;
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 100, "[Semi Auto] Place.");
                    }
                    break;
                case "btnBottomCheck":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Bottom Check?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_LensBottomCheck;
                        cDEF.Run.LogEvent(cLog.Form_VCMPicker_Event + 100, "[Semi Auto] Bottom Check.");
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

        private void FormPageWorkingLensPicker_VisibleChanged(object sender, EventArgs e)
        {
            Grid_Update();
        }

        private void btnVacEjector_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            if (FName == "btnVacuum")
            {
                cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum] = !cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum];
                if (cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum])
                    cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 10, "[Lens Picker] Vacuum On.");
                else
                    cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 11, "[Lens Picker] Vacuum Off.");
            }
            else
            {
                cDEF.Run.Digital.Output[cDO.Lens_Insert_Blow] = !cDEF.Run.Digital.Output[cDO.Lens_Insert_Blow];
                if(cDEF.Run.Digital.Output[cDO.Lens_Insert_Blow])
                    cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 12, "[Lens Picker] Blow On.");
                else
                    cDEF.Run.LogEvent(cLog.Form_LensPicker_Event + 13, "[Lens Picker] Blow Off.");
            }
        }


        private void Vision_Command_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            switch(FName)
            {
                case "btnV1Ready":
                    cDEF.Visions.Sendmsg(Unit.eVision.V1_Ready);
                    break;
                case "btnV2Ready":
                    cDEF.Visions.Sendmsg(Unit.eVision.V2_Ready);
                    break;
                case "btnV3Ready":
                    cDEF.Visions.Sendmsg(Unit.eVision.V3_Ready);
                    break;
                case "btnV1Start":
                    cDEF.Visions.Sendmsg(Unit.eVision.V1_Complete);
                    break;
                case "btnV2Start":
                    cDEF.Visions.Sendmsg(Unit.eVision.V2_Complete);
                    break;
                case "btnV3Start":
                    cDEF.Visions.Sendmsg(Unit.eVision.V3_Complete);
                    break;
            }
        }
    }
}
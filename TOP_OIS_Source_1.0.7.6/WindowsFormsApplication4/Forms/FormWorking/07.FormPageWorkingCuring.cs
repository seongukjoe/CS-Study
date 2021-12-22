using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using XModule.Standard;

namespace XModule.Forms.FormWorking
{
    public partial class FormPageWorkingCuring : TFrame
    {
        public enum eGridValue
        {
            Curing1_Time,
            Actuator_Time1,
            Space1,
            Curing2_Time,
        }
        public FormPageWorkingCuring()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 1000);
        }
        private void FormPageWorkingCuring_Load(object sender, EventArgs e)
        {
            Left = 131;
            Top = 60;
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

                //UV #1 Input
                lbLampReady1_IO.BackColor = cDEF.Run.Digital.Input[cDI.UV_1_Lamp_Ready_Monitor] ? Color.Lime : Color.White;
                lbUVAlarm1_IO.BackColor = cDEF.Run.Digital.Input[cDI.UV_1_Alarm_Monitor] ? Color.Lime : Color.White;
                lbShutterOpen1_IO.BackColor = cDEF.Run.Digital.Input[cDI.UV_1_Shutter_Open_Monitor] ? Color.Lime : Color.White;
                lbShutterClose1_IO.BackColor = cDEF.Run.Digital.Input[cDI.UV_1_Shutter_Close_Monitor] ? Color.Lime : Color.White;

                //UV #1 Output
                btnLampOn1.ForeColor = cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On] ? Color.Red : Color.Black;
                btnLampOff1.ForeColor = cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On] ? Color.Black : Color.Blue;
                btnShutterOpen1.ForeColor = cDEF.Run.Digital.Output[cDO.UV_1_Start] ? Color.Red : Color.Black;
                btnShutterClose1.ForeColor = cDEF.Run.Digital.Output[cDO.UV_1_Start] ? Color.Black : Color.Blue;

                //UV #2 Input
                lbLampReady2_IO.BackColor = cDEF.Run.Digital.Input[cDI.UV_2_Lamp_Ready_Monitor] ? Color.Lime : Color.White;
                lbUVAlarm2_IO.BackColor = cDEF.Run.Digital.Input[cDI.UV_2_Alarm_Monitor] ? Color.Lime : Color.White;
                lbShutterOpen2_IO.BackColor = cDEF.Run.Digital.Input[cDI.UV_2_Shutter_Open_Monitor] ? Color.Lime : Color.White;
                lbShutterClose2_IO.BackColor = cDEF.Run.Digital.Input[cDI.UV_2_Shutter_Close_Monitor] ? Color.Lime : Color.White;

                //UV #2 Output
                btnLampOn2.ForeColor = cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On] ? Color.Red : Color.Black;
                btnLampOff2.ForeColor = cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On] ? Color.Black : Color.Blue;
                btnShutterOpen2.ForeColor = cDEF.Run.Digital.Output[cDO.UV_2_Start] ? Color.Red : Color.Black;
                btnShutterClose2.ForeColor = cDEF.Run.Digital.Output[cDO.UV_2_Start] ? Color.Black : Color.Blue;

                //Cylinder IO
                lbContact_IO_1.BackColor = cDEF.Run.Digital.Input[cDI.UV_Contact] ? Color.Lime : Color.White;
                lbUntact_IO_1.BackColor = cDEF.Run.Digital.Input[cDI.UV_Untact] ? Color.Lime : Color.White;
                lbUVClampDown1.BackColor = cDEF.Run.Digital.Input[cDI.UV_Clamp_Down] ? Color.Lime : Color.White;
                lbUVClampUp1.BackColor = cDEF.Run.Digital.Input[cDI.UV_Clamp_Up] ? Color.Lime : Color.White;

                //Cylinder
                lbUVClampUp1.BackColor = cDEF.Run.Curing1.UVDown.IsBackward() ? Color.Lime : Color.White;
                lbUVClampDown1.BackColor = cDEF.Run.Curing1.UVDown.IsForward() ? Color.Lime : Color.White;
                lbContact1.BackColor = cDEF.Run.Curing1.Contact.IsForward() ? Color.Lime : Color.White;
                lbUntact1.BackColor = cDEF.Run.Curing1.Contact.IsBackward() ? Color.Lime : Color.White;

                //Actuator Input
                lbActuatorReady_IO.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_1_Ready] ? Color.Lime : Color.White;
                lbActuatorFail_IO.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_1_Fail] ? Color.Lime : Color.White;
                lbActuatorPass_IO.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_1_Pass] ? Color.Lime : Color.White;

                //Actuator Output
                btnActuatorMode1.ForeColor = cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] ? Color.Red : Color.DarkSlateGray;
                btnActuatorMode2.ForeColor = cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] ? Color.Red : Color.DarkSlateGray;

                lbUVClampUp2.BackColor = cDEF.Run.Curing2.UVDown.IsBackward() ? Color.Lime : Color.White;
                lbUVClampDown2.BackColor = cDEF.Run.Curing2.UVDown.IsForward() ? Color.Lime : Color.White;
                //lbContact2.BackColor = cDEF.Run.Curing2.Contact.IsForward() ? Color.Lime : Color.White;
                //lbUntact2.BackColor = cDEF.Run.Curing2.Contact.IsBackward() ? Color.Lime : Color.White;

            }));
        }


        #region GridUpdate
        private void Grid_Init()
        {
            GridAdd("R","Curing Time", "Curing #1");
            GridAdd("R", "Actuator Time", "Curing #1");
            GridAdd_Space();
            GridAdd("R","Curing TIme", "Curing #2");
        }

        private void Grid_Update()
        {
            for (int i = 0; i < MotionDataGrid.RowCount; i++)
            {
                switch ((eGridValue)i)
                {
                    case eGridValue.Curing1_Time:
                        MotionDataGrid[3, (int)eGridValue.Curing1_Time].Value = ((double)cDEF.Work.Curing1.CuringTime / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.Actuator_Time1:
                        MotionDataGrid[3, (int)eGridValue.Actuator_Time1].Value = ((double)cDEF.Work.Curing1.ActuatorTime / 1000.0).ToString("N3") + " sec";
                        break;
                    case eGridValue.Curing2_Time:
                        MotionDataGrid[3, (int)eGridValue.Curing2_Time].Value = ((double)cDEF.Work.Curing2.CuringTime / 1000.0).ToString("N3") + " sec";
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

        #region Grid_DataSetting
        private void MotionDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row;
            double DValue = 0;
            string str = "";
            int Value = 0;
            DataGridView Grid = (DataGridView)sender;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            eGridValue eGrid = (eGridValue)Row;
            if (Col == 3)
            {
                switch (eGrid)
                {
                    case eGridValue.Curing1_Time:
                        DValue = Convert.ToDouble(cDEF.Work.Curing1.CuringTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Curing Time", ref DValue, " sec", 0,50))
                            return;
                        {
                            str = String.Format("[Curing #1] Curing Time {0:0.000} to {1:0.000}", cDEF.Work.Curing1.CuringTime / 1000.0, DValue);
                            cDEF.Work.Curing1.CuringTime = (int)(DValue * 1000.0);
                            cDEF.Work.Curing1.Save();
                            cDEF.Run.LogData(cLog.Form_Curing_Data + 0, str);
                        }
                        break;
                    case eGridValue.Actuator_Time1:
                        DValue = Convert.ToDouble(cDEF.Work.Curing1.ActuatorTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Actuator Time", ref DValue, " sec", 0, 30))
                            return;
                        {
                            str = String.Format("[Curing #1] Actuator Time {0:0.000} to {1:0.000}", cDEF.Work.Curing1.ActuatorTime / 1000.0, DValue);
                            cDEF.Work.Curing1.ActuatorTime = (int)(DValue * 1000.0);
                            cDEF.Work.Curing1.Save();
                            cDEF.Run.LogData(cLog.Form_Curing_Data + 0, str);
                        }
                        break;
                  
                    case eGridValue.Curing2_Time:
                        DValue = Convert.ToDouble(cDEF.Work.Curing2.CuringTime) / 1000;
                        if (!XModuleMain.frmBox.fpFloatEdit("Curing Time", ref DValue, " sec", 0, 50))
                            return;
                        {
                            str = String.Format("[Curing #2] Curing Time {0:0.000} to {1:0.000}", cDEF.Work.Curing2.CuringTime / 1000.0, DValue);
                            cDEF.Work.Curing2.CuringTime = (int)(DValue * 1000.0);
                            cDEF.Work.Curing2.Save();
                            cDEF.Run.LogData(cLog.Form_Curing_Data + 1, str);
                        }
                        break;
                   
                }
            }
            Grid_Update();
        }
        #endregion

        private void btnCylinder_Click(object sender, EventArgs e)
        {
            string FButton;
            string FName = (sender as Glass.GlassButton).Name;
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            switch (FName)
            {
                case "btnUVClampUP_1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move UV Clamp #1 Up?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Curing1.UVDown.Backward();
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 0, "[Curing] Move UV Clamp #1 Up.");
                    }
                    break;
                case "btnUVClampDown_1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move UV Clamp #1 Down?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Curing1.UVDown.Forward();
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 1, "[Curing] Move UV Clamp #1 Down.");
                    }
                    break;
                case "btnUntact1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Untact #1?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Curing1.Contact.Backward();
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 2, "[Curing] Move Untact #1.");
                    }
                    break;
                case "btnContact1":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Contact #1?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Curing1.Contact.Forward();
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 3, "[Curing] Move Contact #1.");
                    }
                    break;

                case "btnUVClampUP_2":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move UV Clamp #2 Up?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Curing2.UVDown.Backward();
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 4, "[Curing] Move UV Clamp #2 Up.");
                    }
                    break;

                case "btnUVClampDown_2":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move UV Clamp #2 Down?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Curing2.UVDown.Forward();
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 5, "[Curing] Move UV Clamp #2 Down.");
                    }
                    break;

                //case "btnUntact2":
                //    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Untact #2?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                //    {
                //        cDEF.Run.Curing2.Contact.Backward();
                //        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 6, "[Curing] Move Untact #2.");
                //    }
                //    break;

                //case "btnContact2":
                //    if (XModuleMain.frmBox.MessageBox("MANUAL", "Move Contact #2?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                //    {
                //        cDEF.Run.Curing2.Contact.Forward();
                //        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 7, "[Curing] Move Contact #2.");
                //    }
                //    break;

            }
        }

        private void btnSemiAuto_Click(object sender, EventArgs e)
        {
            string FButton;
            string FName = (sender as Glass.GlassButton).Name;
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            switch (FName)
            {
                //case "btnActuating":
                //    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to UV#1 Actuating?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                //    {
                //        cDEF.Run.Mode = Running.TRunMode.Manual_Cure1Actuating;
                //        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 100, "[Semi Auto] UV #1 Actuating.");
                //    }
                //    break;
                //case "btnCure1":
                //    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to UV #1 Cure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                //    {
                //        cDEF.Run.Mode = Running.TRunMode.Manual_Cure1UV;
                //        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 101, "[Semi Auto] UV #1 Cure.");
                //    }
                //    break;
                case "btnCure2":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to UV #2 Cure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Manual_Cure2UV;
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 102, "[Semi Auto] UV #2 Cure.");
                    }
                    break;
                case "btnActuatingAndCure":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to UV#1 Actuating And Cure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                       // CIC _ 20200529 SSJ: Actuating 이 컨텍이 된 상태에서 UV 동작
                       
                        cDEF.Run.Mode = Running.TRunMode.Manual_Cure1ActuatingAnd1UV;

                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 103, "[Semi Auto] Actuating And Cure.");
                    }
                    break;
            }
        }

        private void bntCuringOption_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;
            string FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            switch(FName)
            {
                case "btnLampOff1":
                    if (cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On])
                    {
                        cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On] = !cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On];
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 8, "[Curing] UV #1 Lamp Off.");
                    }
                    break;
                case "btnLampOn1":
                    if (!cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On])
                    {
                        cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On] = !cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On];
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 9, "[Curing] UV #1 Lamp On.");
                    }
                    break;
                case "btnShutterOpen1":
                    if (!cDEF.Run.Digital.Output[cDO.UV_1_Start])
                    {
                        cDEF.Run.Digital.Output[cDO.UV_1_Start] = !cDEF.Run.Digital.Output[cDO.UV_1_Start];
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 10, "[Curing] UV #1 Shutter Close.");
                    }
                    break;
                case "btnShutterClose1":
                    if (cDEF.Run.Digital.Output[cDO.UV_1_Start])
                    {
                        cDEF.Run.Digital.Output[cDO.UV_1_Start] = !cDEF.Run.Digital.Output[cDO.UV_1_Start];
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 11, "[Curing] UV #1 Shutter Open.");
                    }
                    break;
                case "btnLampOff2":
                    if (cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On])
                    {
                        cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On] = !cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On];
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 12, "[Curing] UV #2 Lamp Off.");
                    }
                    break;
                case "btnLampOn2":
                    if (!cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On])
                    {
                        cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On] = !cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On];
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 13, "[Curing] UV #2 Lamp On.");
                    }
                    break;
                case "btnShutterClose2":
                    if (cDEF.Run.Digital.Output[cDO.UV_2_Start])
                    {
                        cDEF.Run.Digital.Output[cDO.UV_2_Start] = !cDEF.Run.Digital.Output[cDO.UV_2_Start];
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 14, "[Curing] UV #2 Shutter Close.");
                    }
                    break;
                case "btnShutterOpen2":
                    if (!cDEF.Run.Digital.Output[cDO.UV_2_Start])
                    {
                        cDEF.Run.Digital.Output[cDO.UV_2_Start] = !cDEF.Run.Digital.Output[cDO.UV_2_Start];
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] UV #2 Shutter Open.");
                    }
                    break;
            }
        }

        public void ChangeLanguage()
        {
            lbGridTitle.Text = cDEF.Lang.Trans("SETTING");
            lbImage1Title.Text = cDEF.Lang.Trans("CURING #1");
            lbImage2Title.Text = cDEF.Lang.Trans("CURING #2");
            lbCuringOptionTitle.Text = cDEF.Lang.Trans("CURING OPTION");
            lbCylinder1Title.Text = cDEF.Lang.Trans("CYLINDER #1");
        }

        private void btnActuator_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;

            switch(FName)
            {
                case "btnActuatorMode1":
                    cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start] = !cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start];
                    if(cDEF.Run.Digital.Output[cDO.Actuator_1_A_Start])
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #1 Mode 1 Start.");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #1 Mode 1 OFF.");
                    break;
                case "btnActuatorMode2":
                    cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start] = !cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start];
                    if (cDEF.Run.Digital.Output[cDO.Actuator_1_B_Start])
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #1 Mode 2 Start.");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Curing_Event + 15, "[Curing] Actuator #1 Mode 2 OFF.");
                    break;
            }
        }
    }
}
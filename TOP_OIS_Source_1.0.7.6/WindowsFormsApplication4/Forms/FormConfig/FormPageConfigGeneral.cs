using System;
using System.Drawing;
using System.Windows.Forms;
using XModule.Standard;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using XModule.Datas;

namespace XModule.Forms.FormConfig
{
    public partial class FormPageConfigGeneral : TFrame
    {
        int CIndex = 0;
        private double HeadX_Negative = 0.0;                //HeadX Negaitve 리밋
        private double HeadX_Positive = 0.0;                //HeadX Positve 리밋
        private double HeadY_Negative = 0.0;                //HeadY Negaitve 리밋
        private double HeadY_Positive = 0.0;

        public FormPageConfigGeneral()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FormPageConfigGeneral_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
            if (cDEF.Work.DispSensor.DispenserType == 1)
            {
                TJVGbox.Visible = false;
                JettingGbox.Visible = true;
                    
            }
            else if (cDEF.Work.DispSensor.DispenserType == 2)
            {
                TJVGbox.Visible = true;
                JettingGbox.Visible = false;
            }
        }

        protected override void UpdateInformation()
        {
            if (!Visible)
                return;

            this.Invoke(new Action(delegate ()
            {
                //lbLaser_IP.Text = cDEF.Work.Laser.Laser_IP;
                //lbLaser_PortNumber.Text = cDEF.Work.Laser.Laser_SocketPortNumber.ToString();

                #region Vision
                lblVcmIP.Text = cDEF.Work.Vision.VcmTop_IP;
                lblVcmPort.Text = cDEF.Work.Vision.VcmTop_Port.ToString();
                lblVcm.ForeColor = cDEF.Visions.ConnectedV3 ? Color.Lime : Color.Black;

                lblLensTopIP.Text = cDEF.Work.Vision.LensTop_IP;
                lblLensTopPort.Text = cDEF.Work.Vision.LensTop_Port.ToString();
                lblLensTop.ForeColor = cDEF.Visions.ConnectedV1 ? Color.Lime : Color.Black;

                lblLensBtmIP.Text = cDEF.Work.Vision.LensBottom_IP;
                lblLensBtmPort.Text = cDEF.Work.Vision.LensBottom_Port.ToString();
                lblLensBtm.ForeColor = cDEF.Visions.ConnectedV2 ? Color.Lime : Color.Black;

                lblDipping1IP.Text = cDEF.Work.Vision.Dipping1_IP;
                lblDipping1Port.Text = cDEF.Work.Vision.Dipping1_Port.ToString();
                lblDipping1.ForeColor = cDEF.Visions.ConnectedV4 ? Color.Lime : Color.Black;

                lblDipping2IP.Text = cDEF.Work.Vision.Dipping2_IP;
                lblDipping2Port.Text = cDEF.Work.Vision.Dipping2_Port.ToString();
                lblDipping2.ForeColor = cDEF.Visions.ConnectedV5 ? Color.Lime : Color.Black;

                lblInspectIP.Text = cDEF.Work.Vision.Inspect_IP;
                lblInspectPort.Text = cDEF.Work.Vision.Inspect_Port.ToString();
                lblInspect.ForeColor = cDEF.Visions.ConnectedV6 ? Color.Lime : Color.Black;

                #endregion Vision

                #region Measuring
                lblFaceAngleIP.Text = cDEF.Work.Measuring.FaceAngle_IP;
                lblFaceAnglePort.Text = cDEF.Work.Measuring.FaceAngle_Port.ToString();
                lblFaceAngle.ForeColor = cDEF.SideAngleMeasuring.IsConnected() ? Color.Lime : Color.Black;
                #endregion Measuring

                #region Seiral

                lblPizco1Com.Text = cDEF.Work.DispSensor.Pizco1_Port;
                lblPizco2Com.Text = cDEF.Work.DispSensor.Pizco2_Port;

                if (cDEF.Work.DispSensor.DispenserType == 0)
                {
                    lblPizco1.ForeColor = cDEF.Dispenser1.IsConnected() ? Color.Lime : Color.Black;
                    lblPizco2.ForeColor = cDEF.Dispenser2.IsConnected() ? Color.Lime : Color.Black;
                }
                else
                {
                    lblPizco1.ForeColor = cDEF.DispenserEcm1.IsConnected() ? Color.Lime : Color.Black;
                    lblPizco2.ForeColor = cDEF.DispenserEcm2.IsConnected() ? Color.Lime : Color.Black;
                }


                lblLensHeightCom.Text = cDEF.Work.DeviceLensHeight.ComPort;
                lblLensHeight.ForeColor = cDEF.Serials.LensHeight.IsOpen ? Color.Lime : Color.Black;

                #endregion

                if (cDEF.Work.Option.DoorAlarmDispMode == 0)
                    btnDoorMode.Text = "DISABLE";
                else if (cDEF.Work.Option.DoorAlarmDispMode == 1)
                    btnDoorMode.Text = "OUTPUT";
                else if (cDEF.Work.Option.DoorAlarmDispMode == 2)
                    btnDoorMode.Text = "BLINK";

                if (cDEF.Work.Option.PlaceOverrideUse == 0)
                    btnLensPlaceMode.Text = "NORMAL";
                else if (cDEF.Work.Option.PlaceOverrideUse == 1)
                    btnLensPlaceMode.Text = "OVERRIDE";
                else if (cDEF.Work.Option.PlaceOverrideUse == 1)
                    btnLensPlaceMode.Text = "SECOND STEP";

                if (cDEF.Work.Option.PickOverrideUse == 0)
                    btnLensPickMode.Text = "NORMAL";
                else if (cDEF.Work.Option.PickOverrideUse == 1)
                    btnLensPickMode.Text = "OVERRIDE";
                else if (cDEF.Work.Option.PickOverrideUse == 1)
                    btnLensPickMode.Text = "SECOND STEP";


                btnActuatorPath1.Text = cDEF.Work.Option.ActuatorPath1;
                btnActuatorPath2.Text = cDEF.Work.Option.ActuatorPath2;
                btnAgentPath.Text = cDEF.Work.Option.MESAgentPath;    //2021-04-28 Modify

                cb_Index_Skip1.Checked = cDEF.Work.Option.IndexSkip[0];
                cb_Index_Skip2.Checked = cDEF.Work.Option.IndexSkip[1];
                cb_Index_Skip3.Checked = cDEF.Work.Option.IndexSkip[2];
                cb_Index_Skip4.Checked = cDEF.Work.Option.IndexSkip[3];
                cb_Index_Skip5.Checked = cDEF.Work.Option.IndexSkip[4];
                cb_Index_Skip6.Checked = cDEF.Work.Option.IndexSkip[5];
                cb_Index_Skip7.Checked = cDEF.Work.Option.IndexSkip[6];
                cb_Index_Skip8.Checked = cDEF.Work.Option.IndexSkip[7];
                cb_Index_Skip9.Checked = cDEF.Work.Option.IndexSkip[8];
                cb_Index_Skip10.Checked = cDEF.Work.Option.IndexSkip[9];
                cb_Index_Skip11.Checked = cDEF.Work.Option.IndexSkip[10];
                cb_Index_Skip12.Checked = cDEF.Work.Option.IndexSkip[11];

                lbTorqueLimit.Text = cDEF.Work.Option.TorqueLimit.ToString();
                lbPickTorqueLimit.Text = cDEF.Work.Option.TorqueLimitPick.ToString();
                lbThetaTorqueLimit.Text = cDEF.Work.Option.TorqueLimitTheta.ToString();

                btnLensPickerTDirection.Text = cDEF.Work.Option.LensPickerUpperTDirectionCCW ? "CCW" : "CW";

                lbProductDiameter.Text = cDEF.Work.Option.ProductDiameter.ToString();

                lbTJVIP_1.Text = $"192.168.1{cDEF.Work.Recipe.TJV_IP_1.ToString("D2")}";
                lbTJVIP_2.Text = $"192.168.1{cDEF.Work.Recipe.TJV_IP_2.ToString("D2")}";

                lbTJV_1_Status.ForeColor = cDEF.TJV_1.Connect ? Color.Lime : Color.Black;
                lbTJV_2_Status.ForeColor = cDEF.TJV_2.Connect ? Color.Lime : Color.Black;

            }));
        }

        private void btnWaferCal_Click(object sender, EventArgs e)
        {

        }

        private void btnWaferAlign_Click(object sender, EventArgs e)
        {

        }
        private void BtnHeadXYRepeatTest_Click(object sender, EventArgs e)
        {


        }
        private void btnChipLocateMove_Click(object sender, EventArgs e)
        {

        }

        private void HeadXY_RepeatSet_Click(object sender, EventArgs e)
        {

        }

        private void WaferCalibrationSettting_Click(object sender, EventArgs e)
        {

        }
        private void lbTableWidhtLength_Click(object sender, EventArgs e)
        {

        }

        private void VisionInterFaceSettingIP_Click(object sender, EventArgs e)
        {
            string Ftag = (sender as Label).Name.ToString();
            string Temp = "";

            switch (Ftag)
            {
                case "lblVcmIP":
                    Temp = cDEF.Work.Vision.VcmTop_IP;
                    if (cDEF.fTextEdit.TextEdit("[Vision] Vision VCM Top IP", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.Vision.VcmTop_IP = Temp;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblLensTopIP":
                    Temp = cDEF.Work.Vision.LensTop_IP;
                    if (cDEF.fTextEdit.TextEdit("[Vision] Vision Lens Top IP", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.Vision.LensTop_IP = Temp;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblLensBtmIP":
                    Temp = cDEF.Work.Vision.LensBottom_IP;
                    if (cDEF.fTextEdit.TextEdit("[Vision] Vision Lens Bottom IP", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.Vision.LensBottom_IP = Temp;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblDipping1IP":
                    Temp = cDEF.Work.Vision.Dipping1_IP;
                    if (cDEF.fTextEdit.TextEdit("[Vision] Vision Dipping 1 IP", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.Vision.Dipping1_IP = Temp;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblDipping2IP":
                    Temp = cDEF.Work.Vision.Dipping2_IP;
                    if (cDEF.fTextEdit.TextEdit("[Vision] Vision Dipping 2 IP", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.Vision.Dipping2_IP = Temp;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblInspectIP":
                    Temp = cDEF.Work.Vision.Inspect_IP;
                    if (cDEF.fTextEdit.TextEdit("[Vision] Vision Inspect IP", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.Vision.Inspect_IP = Temp;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblFaceAngleIP":
                    Temp = cDEF.Work.Measuring.FaceAngle_IP;
                    if (cDEF.fTextEdit.TextEdit("[Measuring] Face Angle Measuring IP", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.Measuring.FaceAngle_IP = Temp;
                        cDEF.Work.Measuring.Save();
                    }
                    break;
            }
        }

        private void VisionInterFaceSettingPort_Click(object sender, EventArgs e)
        {
            string Ftag = (sender as Label).Name.ToString();

            int Value = 0;

            switch (Ftag)
            {
                case "lblVcmPort":
                    Value = cDEF.Work.Vision.VcmTop_Port;
                    if (XModuleMain.frmBox.fpIntegerEdit("[Vision] Vision VCM Top Port", ref Value, ""))
                    {
                        cDEF.Work.Vision.VcmTop_Port = Value;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblLensTopPort":
                    Value = cDEF.Work.Vision.LensTop_Port;
                    if (XModuleMain.frmBox.fpIntegerEdit("[Vision] Vision Lens Top Port", ref Value, ""))
                    {
                        cDEF.Work.Vision.LensTop_Port = Value;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblLensBtmPort":
                    Value = cDEF.Work.Vision.LensBottom_Port;
                    if (XModuleMain.frmBox.fpIntegerEdit("[Vision] Vision Lens Bottom Port", ref Value, ""))
                    {
                        cDEF.Work.Vision.LensBottom_Port = Value;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblDipping1Port":
                    Value = cDEF.Work.Vision.Dipping1_Port;
                    if (XModuleMain.frmBox.fpIntegerEdit("[Vision] Vision Dipping 1 Port", ref Value, ""))
                    {
                        cDEF.Work.Vision.Dipping1_Port = Value;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblDipping2Port":
                    Value = cDEF.Work.Vision.Dipping2_Port;
                    if (XModuleMain.frmBox.fpIntegerEdit("[Vision] Vision Dipping 2 Port", ref Value, ""))
                    {
                        cDEF.Work.Vision.Dipping2_Port = Value;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblInspectPort":
                    Value = cDEF.Work.Vision.Inspect_Port;
                    if (XModuleMain.frmBox.fpIntegerEdit("[Vision] Vision Inspect Port", ref Value, ""))
                    {
                        cDEF.Work.Vision.Inspect_Port = Value;
                        cDEF.Work.Vision.Save();
                    }
                    break;
                case "lblFaceAnglePort":
                    Value = cDEF.Work.Measuring.FaceAngle_Port;
                    if (XModuleMain.frmBox.fpIntegerEdit("[Vision] Face Angle Port", ref Value, ""))
                    {
                        cDEF.Work.Measuring.FaceAngle_Port = Value;
                        cDEF.Work.Measuring.Save();
                    }
                    break;
            }

        }

        private void DeviceConnect_Click(object sender, EventArgs e)
        {
            string Ftag = (sender as Button).Tag.ToString();
            cDEF.Visions.ReadStart(Ftag);
        }

        private void DeviceDeConnect_Click(object sender, EventArgs e)
        {
            string Ftag = (sender as Button).Tag.ToString();
            cDEF.Visions.ReadStop(Ftag);
        }

        public void ChangeLanguage()
        {
            tabPageSetting.Text = cDEF.Lang.Trans("   Setting   ");
            tabPageInterface.Text = cDEF.Lang.Trans("   InterFace   ");
        }

        private void btnDoorMode_Click(object sender, EventArgs e)
        {
            int Value = -1;
            if (XModuleMain.frmBox.SelectBox("DOOR DISPLAY MODE", "DISABLE,OUTPUT,BLINK", ref Value) == DialogResult.No)
                return;

            cDEF.Work.Option.DoorAlarmDispMode = Value;
            cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
        }

        private void btnLensPlaceMode_Click(object sender, EventArgs e)
        {
            int Value = -1;
            if (XModuleMain.frmBox.SelectBox("LENS PLACE MODE", "NOMAL,OVERRIDE,SECOND STEP", ref Value) == DialogResult.No)
                return;

            cDEF.Work.Option.PlaceOverrideUse = Value;
            cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
        }
        private void btnLensPickMode_Click(object sender, EventArgs e)
        {
            int Value = -1;
            if (XModuleMain.frmBox.SelectBox("LENS PICK MODE", "NOMAL,OVERRIDE,SECOND STEP", ref Value) == DialogResult.No)
                return;

            cDEF.Work.Option.PickOverrideUse = Value;
            cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
        }
        private void btnActuatorPath1_Click(object sender, EventArgs e)
        {


            OpenFileDialog of = new OpenFileDialog();
            //of.Filter = ".exe";
            of.InitialDirectory = "D:\\KSTAR\\";

            string tag = (sender as Glass.GlassButton).Tag.ToString();
            switch (tag)
            {
                case "Act1":
                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        cDEF.Work.Option.ActuatorPath1 = of.FileName;
                        cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
                    }
                    break;

                case "Act2":
                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        cDEF.Work.Option.ActuatorPath2 = of.FileName;
                        cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
                    }
                    break;
                case "MES":    //2021-04-28 Modify
                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        cDEF.Work.Option.MESAgentPath = of.FileName;
                        cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
                    }
                    break;
            }




        }

        private void cb_Index_Skip1_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as CheckBox).Tag);
            cDEF.Work.Option.IndexSkip[tag] = !cDEF.Work.Option.IndexSkip[tag];
            cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
        }


        private void btnConFaceAngle_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as Button).Tag);
            if (tag == 0)
                cDEF.SideAngleMeasuring.DeConnected();
            else
            {
                if (cDEF.SideAngleMeasuring.IsConnected())
                {
                    cDEF.SideAngleMeasuring.DeConnected();
                }
                cDEF.SideAngleMeasuring.Init(cDEF.Work.Measuring.FaceAngle_IP, cDEF.Work.Measuring.FaceAngle_Port, "SideAngle");
            }
        }

        private void lblComSetting_Click(object sender, EventArgs e)
        {
            string Ftag = (sender as Label).Name;

            string Temp = "";
            switch (Ftag)
            {
                case "lblPizco1Com":
                    Temp = cDEF.Work.DispSensor.Pizco1_Port;
                    if (cDEF.fTextEdit.TextEdit("[Device] Displacement Sensor 1 Port Name", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.DispSensor.Pizco1_Port = Temp;
                        cDEF.Work.DispSensor.Save();
                    }
                    break;
                case "lblPizco2Com":
                    Temp = cDEF.Work.DispSensor.Pizco1_Port;
                    if (cDEF.fTextEdit.TextEdit("[Device] Displacement Sensor 2 Port Name", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.DispSensor.Pizco2_Port = Temp;
                        cDEF.Work.DispSensor.Save();
                    }
                    break;
                case "lblLensHeightCom":
                    Temp = cDEF.Work.DeviceLensHeight.ComPort;
                    if (cDEF.fTextEdit.TextEdit("[Device] Displacement Sensor 2 Port Name", ref Temp, "\\/:*?\"<>|"))
                    {
                        cDEF.Work.DeviceLensHeight.ComPort = Temp;
                        cDEF.Work.DeviceLensHeight.Save();
                    }
                    break;


            }
        }

        private void btnConPizco_Click(object sender, EventArgs e)
        {
            string Ftag = (sender as Button).Name;

            string Temp = "";
            switch (Ftag)
            {
                case "btnConPizco1":
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                    {
                        if (cDEF.Dispenser1.IsConnected())
                        {
                            cDEF.Dispenser1.DeConnected();
                        }
                        cDEF.Dispenser1.Init(cDEF.Work.DispSensor.Pizco1_Port);
                    }
                    else
                    {
                        if (cDEF.DispenserEcm1.IsConnected())
                        {
                            cDEF.DispenserEcm1.DeConnected();
                        }
                        cDEF.DispenserEcm1.Init(cDEF.Work.DispSensor.Pizco1_Port);
                    }
                    break;
                case "btnConPizco2":
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                    {
                        if (cDEF.Dispenser2.IsConnected())
                        {
                            cDEF.Dispenser2.DeConnected();

                        }
                        cDEF.Dispenser2.Init(cDEF.Work.DispSensor.Pizco2_Port);
                    }
                    else
                    {
                        if (cDEF.DispenserEcm2.IsConnected())
                        {
                            cDEF.DispenserEcm2.DeConnected();

                        }
                        cDEF.DispenserEcm2.Init(cDEF.Work.DispSensor.Pizco2_Port);
                    }
                    break;
                case "btnDConPizco1":
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                    {
                        cDEF.Dispenser1.DeConnected();
                    }
                    else
                    {
                        cDEF.DispenserEcm1.DeConnected();
                    }
                    break;
                case "btnDConPizco2":
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                    {
                        cDEF.Dispenser2.DeConnected();
                    }
                    else
                    {
                        cDEF.DispenserEcm2.DeConnected();
                    }
                    break;
            }
        }

        private void btnConLensHeight_Click(object sender, EventArgs e)
        {
            //
            int tag = Convert.ToInt32((sender as Button).Tag);
            if (tag == 0)
                cDEF.Serials.LensHeight.Close();
            else
            {
                if (cDEF.Serials.LensHeight.IsOpen)
                {
                    cDEF.Serials.LensHeight.Close();
                }
                cDEF.Serials.LensHeight.Open();
            }
        }

        private void lblVcm_Click(object sender, EventArgs e)
        {
        }

        private void lbTorqueLimit_Click(object sender, EventArgs e)
        {
            int Value = cDEF.Work.Option.TorqueLimit;
            if (XModuleMain.frmBox.fpIntegerEdit("Torque Limit", ref Value, ""))
            {
                cDEF.Work.Option.TorqueLimit = Value;
                cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
            }
        }

        private void btnLensPickerTDirection_Click(object sender, EventArgs e)
        {
            cDEF.Work.Option.LensPickerUpperTDirectionCCW = !cDEF.Work.Option.LensPickerUpperTDirectionCCW;
            cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
        }

        private void lbPickTorqueLimit_Click(object sender, EventArgs e)
        {
            int Value = cDEF.Work.Option.TorqueLimitPick;
            if (XModuleMain.frmBox.fpIntegerEdit("Torque Limit", ref Value, ""))
            {
                cDEF.Work.Option.TorqueLimitPick = Value;
                cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
            }
        }

        private void lbThetaTorqueLimit_Click(object sender, EventArgs e)
        {
            int Value = cDEF.Work.Option.TorqueLimitTheta;
            if (XModuleMain.frmBox.fpIntegerEdit("Torque Limit Theta", ref Value, ""))
            {
                cDEF.Work.Option.TorqueLimitTheta = Value;
                cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
            }
        }

        private void lbProductDiameter_Click(object sender, EventArgs e)
        {
            double Value = cDEF.Work.Option.ProductDiameter;
            if (XModuleMain.frmBox.fpFloatEdit("Product Diameter", ref Value, ""))
            {
                cDEF.Work.Option.ProductDiameter = Value;
                cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
                cDEF.frmPageRecipeProject.VcmImgInit();
            }
        }
        private void FormPageConfigGeneral_VisibleChanged(object sender, EventArgs e)
        {
            cb_Index_Skip1.Checked = cDEF.Work.Option.IndexSkip[0];
            cb_Index_Skip2.Checked = cDEF.Work.Option.IndexSkip[1];
            cb_Index_Skip3.Checked = cDEF.Work.Option.IndexSkip[2];
            cb_Index_Skip4.Checked = cDEF.Work.Option.IndexSkip[3];
            cb_Index_Skip5.Checked = cDEF.Work.Option.IndexSkip[4];
            cb_Index_Skip6.Checked = cDEF.Work.Option.IndexSkip[5];
            cb_Index_Skip7.Checked = cDEF.Work.Option.IndexSkip[6];
            cb_Index_Skip8.Checked = cDEF.Work.Option.IndexSkip[7];
            cb_Index_Skip9.Checked = cDEF.Work.Option.IndexSkip[8];
            cb_Index_Skip10.Checked = cDEF.Work.Option.IndexSkip[9];
            cb_Index_Skip11.Checked = cDEF.Work.Option.IndexSkip[10];
            cb_Index_Skip12.Checked = cDEF.Work.Option.IndexSkip[11];
        }

        private void lbTJVIP_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as Label).Tag);
            int Value = 0;
            switch (tag)
            {
                case 0:
                    Value = cDEF.Work.Recipe.TJV_IP_1;
                    if (XModuleMain.frmBox.fpIntegerEdit("TJV #1 IP Node", ref Value, ""))
                    {
                        cDEF.Work.Recipe.TJV_IP_1 = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                    }
                    break;
                case 1:
                     Value = cDEF.Work.Recipe.TJV_IP_2;
                    if (XModuleMain.frmBox.fpIntegerEdit("TJV #2 IP Node", ref Value, ""))
                    {
                        cDEF.Work.Recipe.TJV_IP_2 = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                    }
                    break;


            }

         
        }

        private void btnTJV_Click(object sender, EventArgs e)
        {
            string Ftag = (sender as Button).Name;

            string Temp = "";
            switch (Ftag)
            {
                case "btnTJVConnect_1":
                    cDEF.TJV_1.Init(cDEF.Work.Recipe.TJV_IP_1);
                    break;
                case "btnTJVConnect_2":
                    cDEF.TJV_2.Init(cDEF.Work.Recipe.TJV_IP_2);
                    break;           
            }
        }
    }
}

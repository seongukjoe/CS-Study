using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XModule.Standard;


namespace XModule.Forms.FormConfig
{
    public partial class FrmPageConfigCylinder : Form
    {
        private int FSelected = 0;

        public FrmPageConfigCylinder()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FrmPageConfigCylinder_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;

            //if (grid.RowCount != TfpCylinder.Cylinder.Count)
            //    grid.RowCount = TfpCylinder.Cylinder.Count;

            //for (i = 0; i < TfpCylinder.Cylinder.Count; i++)
            //{
            //    CylinderItem = TfpCylinder.Cylinder.Items[i];

            //    Temp = (i).ToString();
            //    if (grid.Rows[i].Cells[0].Value != null)
            //        GridValue = grid.Rows[i].Cells[0].Value.ToString();
            //    if (GridValue != Temp)
            //        grid.Rows[i].Cells[0].Value = Temp;

            //    Temp = CylinderItem.Key.ToUpper();
            //    if (grid.Rows[i].Cells[1].Value != null)
            //        GridValue = grid.Rows[i].Cells[1].Value.ToString();
            //    if (GridValue != Temp)
            //        grid.Rows[i].Cells[1].Value = Temp;
            //    grid.Rows[i].Cells[1].Style.BackColor = Color.White;
            //    grid.Rows[i].Cells[1].Style.ForeColor = Color.LightGray;

            //    if (CylinderItem.Monitoring == TfpCylinderMonitoring.fcmDisable)
            //        Temp = "DISABLE";
            //    else if (CylinderItem.Monitoring == TfpCylinderMonitoring.fcmEnable)
            //        Temp = "ENABLE";
            //    else
            //        Temp = "POSITION";
            //    if (grid.Rows[i].Cells[2].Value != null)
            //        GridValue = grid.Rows[i].Cells[2].Value.ToString();
            //    if (GridValue != Temp)
            //        grid.Rows[i].Cells[2].Value = Temp;
            //    if (Temp == "DISABLE")
            //    {
            //        grid.Rows[i].Cells[1].Style.BackColor = Color.White;
            //        grid.Rows[i].Cells[1].Style.ForeColor = Color.LightGray;
            //    }
            //    else
            //    {
            //        grid.Rows[i].Cells[1].Style.BackColor = Color.White;
            //        grid.Rows[i].Cells[1].Style.ForeColor = Color.Black;
            //    }
            //}

            UpdateDetailInformation();
        }
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            int Col, Row = 0;

            Col = grid.CurrentCell.ColumnIndex;
            Row = grid.CurrentCell.RowIndex;
            if (Col > -1 && Row > -1)
            {
                if (FSelected != Row)
                {
                    FSelected = Row;
                    UpdateDetailInformation();
                }
            }
        }
        void ConfigurationClick(object sender, EventArgs e)
        {
	        TfpCylinderItem CylinderItem = cDEF.Run.Cylinder.Items[FSelected];
	        String Temp = "";
	        int i, Value;

	        switch(Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        for(i = 0; i < cDEF.Run.Cylinder.Count; i ++)
			        {
				        if(i != 0)
					        Temp += ",";
				        Temp += cDEF.Run.Cylinder.Items[i].Key.ToUpper();
			        }
			        if(XModuleMain.frmBox.SelectBox("INDEX", Temp, ref FSelected) == DialogResult.No)
				        return;
			        UpdateDetailInformation();
			        break;
		        case 1:
			        Value = (int)CylinderItem.Monitoring;
			        if(XModuleMain.frmBox.SelectBox("MONITORING", "DISABLE,ENABLE,POSITION", ref Value) == DialogResult.No)
				        return;
			        CylinderItem.Monitoring = (TfpCylinderMonitoring)Value;
                    cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        break;
		        case 2:
			        Value = (int)CylinderItem.TimeOut;
			        if(!XModuleMain.frmBox.fpIntegerEdit("TIME OUT", ref Value, " ms", 0, 60000))
				        return;
			        CylinderItem.TimeOut = (ulong)Value;
                    cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        break;
		        case 3:
			        Value = -1;
			        if(XModuleMain.frmBox.SelectBox("FORWARD OUTPUT", "DISABLE,ENABLE",ref Value) == DialogResult.No)
				        break;
			        if(Value == 0)
			        {
				        CylinderItem.ForwardConfig.Output = -1;
                        cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        }
			        else
			        {
				        for(i = 0; i < cDEF.Run.Digital.OutputCount; i ++)
				        {
					        if(i != 0)
						        Temp += ",";
                            Temp += cDEF.Run.Digital.OutputItems[i].Text.ToUpper() +" ( " + (i).ToString() + " )";
				        }
				        Value = CylinderItem.ForwardConfig.Output;
				        if(XModuleMain.frmBox.SelectBox("FORWARD OUTPUT", Temp,ref Value) == DialogResult.No)
					        return;
				        CylinderItem.ForwardConfig.Output = Value;
                        cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        }
			        break;
		        case 4:
			        Value = -1;
			        if(XModuleMain.frmBox.SelectBox("FORWARD INPUT", "DISABLE,ENABLE",ref Value) == DialogResult.No)
				        break;
			        if(Value == 0)
			        {
				        CylinderItem.ForwardConfig.Input = -1;
                        cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        }
			        else
			        {
				        for(i = 0; i < cDEF.Run.Digital.InputCount; i ++)
				        {
					        if(i != 0)
						        Temp += ",";
					        Temp += cDEF.Run.Digital.InputItems[i].Text.ToUpper() +" ( " + (i).ToString() + " )";
				        }
				        Value = CylinderItem.ForwardConfig.Input;
				        if(XModuleMain.frmBox.SelectBox("FORWARD INPUT", Temp,ref Value) == DialogResult.No)
					        return;
				        CylinderItem.ForwardConfig.Input = Value;
                        cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        }
			        break;
		        case 5:
			        Value = (int)CylinderItem.ForwardConfig.Sleep;
			        if(!XModuleMain.frmBox.fpIntegerEdit("FORWARD SLEEP",ref Value, " ms", 0, 60000))
				        return;
			        CylinderItem.ForwardConfig.Sleep = (ulong)Value;
                    cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        break;
		        case 6:
			        Value = -1;
			        if(XModuleMain.frmBox.SelectBox("BACKWARD OUTPUT", "DISABLE,ENABLE",ref Value) == DialogResult.No)
				        break;
			        if(Value == 0)
			        {
				        CylinderItem.BackwardConfig.Output = -1;
                        cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        }
			        else
			        {
				        for(i = 0; i < cDEF.Run.Digital.OutputCount; i ++)
				        {
					        if(i != 0)
						        Temp += ",";
                            Temp += cDEF.Run.Digital.OutputItems[i].Text.ToUpper() + " ( " + (i).ToString() + " )";
				        }
				        Value = CylinderItem.BackwardConfig.Output;
				        if(XModuleMain.frmBox.SelectBox("BACKWARD OUTPUT", Temp,ref Value) == DialogResult.No)
					        return;
				        CylinderItem.BackwardConfig.Output = Value;
                        cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        }
			        break;
		        case 7:
			        Value = -1;
			        if(XModuleMain.frmBox.SelectBox("BACKWARD INPUT", "DISABLE,ENABLE",ref Value) == DialogResult.No) 
				        break;
			        if(Value == 0)
			        {
				        CylinderItem.BackwardConfig.Input = -1;
                        cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        }
			        else
			        {
				        for(i = 0; i < cDEF.Run.Digital.InputCount; i ++)
				        {
					        if(i != 0)
						        Temp += ",";
                            Temp += cDEF.Run.Digital.InputItems[i].Text.ToUpper() +" ( " + (i).ToString() + " )";
				        }
				        Value = CylinderItem.BackwardConfig.Input;
				        if(XModuleMain.frmBox.SelectBox("FORWARD INPUT", Temp,ref Value) == DialogResult.No)
					        return;
				        CylinderItem.BackwardConfig.Input = Value;
                        cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        }
			        break;
		        case 8:
			        Value = (int)CylinderItem.BackwardConfig.Sleep;
			        if(!XModuleMain.frmBox.fpIntegerEdit("FORWARD SLEEP", ref Value, " ms", 0, 60000))
				        return;
			        CylinderItem.BackwardConfig.Sleep = (ulong)Value;
                    cDEF.Run.Cylinder.Save(cPath.FILE_CYLINDER);
			        break;
	        }
            UpdateDetailInformation();
        }
        void ActionClick(object sender, EventArgs e)
        {
            TfpCylinderItem CylinderItem = cDEF.Run.Cylinder.Items[FSelected];

            switch (Convert.ToInt32((sender as Glass.GlassButton).Tag))
            {
                case 0:
                    CylinderItem.Active = true;
                    break;
                case 1:
                    CylinderItem.Active = false;
                    break;
                case 2:
                    if (0 <= CylinderItem.ForwardConfig.Output && CylinderItem.ForwardConfig.Output < cDEF.Run.Digital.OutputCount)
                        cDEF.Run.Digital.Output[CylinderItem.ForwardConfig.Output] = false;
                    if (0 <= CylinderItem.BackwardConfig.Output && CylinderItem.BackwardConfig.Output < cDEF.Run.Digital.OutputCount)
                        cDEF.Run.Digital.Output[CylinderItem.BackwardConfig.Output] = false;
                    break;
                case 3:
                    CylinderItem.RepeatMode = !CylinderItem.RepeatMode;
                    break;
                case 4:
                    CylinderItem.Forward();
                    break;
                case 5:
                    CylinderItem.Backward();
                    break;
                case 6:
                    CylinderItem.RepeatStart();
                    break;
                case 7:
                    CylinderItem.RepeatStop();
                    break;
            }
        }

        private void UpdateDetailInformation()
        {
            int i;
            String Temp;
            String GridValue = "";
            TfpCylinderItem CylinderItem;

            if (grid.RowCount != cDEF.Run.Cylinder.Count)
                grid.RowCount = cDEF.Run.Cylinder.Count;

            for (i = 0; i < cDEF.Run.Cylinder.Count; i++)
            {
                CylinderItem = cDEF.Run.Cylinder.Items[i];

                Temp = (i).ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;

                Temp = CylinderItem.Key.ToUpper();
                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[1].Value = Temp;

                if (CylinderItem.Monitoring == TfpCylinderMonitoring.fcmDisable)
                    Temp = "DISABLE";
                else if (CylinderItem.Monitoring == TfpCylinderMonitoring.fcmEnable)
                    Temp = "ENABLE";
                else
                    Temp = "POSITION";
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;
                if (Temp == "DISABLE")
                {
                    grid.Rows[i].Cells[2].Style.BackColor = Color.White;
                    grid.Rows[i].Cells[2].Style.ForeColor = Color.LightGray;
                }
                else
                {
                    grid.Rows[i].Cells[2].Style.BackColor = Color.White;
                    grid.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                }
            }

            CylinderItem = cDEF.Run.Cylinder.Items[FSelected];

            btnKey.Text = CylinderItem.Key.ToUpper();
            if (CylinderItem.Monitoring == TfpCylinderMonitoring.fcmDisable)
            {
                btnMonitoring.Text = "DISABLE";
                btnMonitoring.ForeColor = Color.Gray;
            }
            else
            {
                if (CylinderItem.Monitoring == TfpCylinderMonitoring.fcmEnable)
                    btnMonitoring.Text = "ENABLE";
                else
                    btnMonitoring.Text = "POSITION";
                btnMonitoring.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            btnTimeOut.Text = XModuleMain.frmBox.fpIntToStr((int)CylinderItem.TimeOut) + " msec";
            if (CylinderItem.ForwardConfig.Output < 0 || cDEF.Run.Digital.OutputCount <= CylinderItem.ForwardConfig.Output)
            {
                btnForwardOutput.Text = "DISABLE";
                btnForwardOutput.ForeColor = Color.Gray;
            }
            else
            {
                btnForwardOutput.Text = cDEF.Run.Digital.OutputItems[CylinderItem.ForwardConfig.Output].Text + " ( " + CylinderItem.ForwardConfig.Output + " )";
                btnForwardOutput.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            if (CylinderItem.ForwardConfig.Input < 0 || cDEF.Run.Digital.InputCount <= CylinderItem.ForwardConfig.Input)
            {
                btnForwardInput.Text = "DISABLE";
                btnForwardInput.ForeColor = Color.Gray; 
            }
            else
            {
                btnForwardInput.Text = cDEF.Run.Digital.InputItems[CylinderItem.ForwardConfig.Input].Text + " ( " + CylinderItem.ForwardConfig.Input + " )";
                btnForwardInput.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            if (CylinderItem.ForwardConfig.Sleep == 0)
            {
                btnForwardSleep.Text = "DISABLE";
                btnForwardSleep.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            else
            {
                btnForwardSleep.Text = XModuleMain.frmBox.fpIntToStr((int)CylinderItem.ForwardConfig.Sleep) + " msec";
                btnForwardSleep.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            if (CylinderItem.BackwardConfig.Output < 0 || cDEF.Run.Digital.OutputCount <= CylinderItem.BackwardConfig.Output)
            {
                btnBackwardOutput.Text = "DISABLE";
                btnBackwardOutput.ForeColor = Color.Gray;
            }
            else
            {
                btnBackwardOutput.Text = cDEF.Run.Digital.OutputItems[CylinderItem.BackwardConfig.Output].Text + " ( " + CylinderItem.BackwardConfig.Output + " )";
                btnBackwardOutput.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            if (CylinderItem.BackwardConfig.Input < 0 || cDEF.Run.Digital.InputCount <= CylinderItem.BackwardConfig.Input)
            {
                btnBackwardInput.Text = "DISABLE";
                btnBackwardInput.ForeColor = Color.Gray;
            }
            else
            {
                btnBackwardInput.Text = cDEF.Run.Digital.InputItems[CylinderItem.BackwardConfig.Input].Text + " ( " + CylinderItem.BackwardConfig.Input + " )";
                btnBackwardInput.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            if (CylinderItem.BackwardConfig.Sleep == 0)
            {
                btnBackwardSleep.Text = "DISABLE";
                btnBackwardSleep.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            else
            {
                btnBackwardSleep.Text = XModuleMain.frmBox.fpIntToStr((int)CylinderItem.BackwardConfig.Sleep) + " msec";
                btnBackwardSleep.ForeColor = System.Drawing.SystemColors.WindowText;
            }               
        }
        private void UpdateInformation()
        {
            if (!Visible)
                return;


            if (cDEF.Run.Cylinder == null)
                return;

            int i;
            String Temp;
            String GridValue = "";
            TfpCylinderItem CylinderItem;

            for (i = 0; i < cDEF.Run.Cylinder.Count; i++)
            {
                CylinderItem = cDEF.Run.Cylinder.Items[i];

                if (CylinderItem.ForwardConfig.Output < 0 || cDEF.Run.Digital.OutputCount <= CylinderItem.ForwardConfig.Output)
                    Temp = "---";
                else
                    Temp = (cDEF.Run.Digital.Output[CylinderItem.ForwardConfig.Output]) ? "ON" : "OFF";
                if (grid.Rows[i].Cells[3].Value != null)
                    GridValue = grid.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[3].Value = Temp;
                if (Temp == "ON")
                {
                    grid.Rows[i].Cells[3].Style.BackColor = Color.Red;
                    grid.Rows[i].Cells[3].Style.ForeColor = Color.White;
                }
                else
                {
                    grid.Rows[i].Cells[3].Style.BackColor = Color.White;
                    grid.Rows[i].Cells[3].Style.ForeColor = Color.LightGray;
                }

                if (CylinderItem.ForwardConfig.Input < 0 || cDEF.Run.Digital.InputCount <= CylinderItem.ForwardConfig.Input)
                    Temp = "---";
                else
                    Temp = (cDEF.Run.Digital.Input[CylinderItem.ForwardConfig.Input]) ? "ON" : "OFF";
                if (grid.Rows[i].Cells[4].Value != null)
                    GridValue = grid.Rows[i].Cells[4].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[4].Value = Temp;
                if (Temp == "ON")
                {
                    grid.Rows[i].Cells[4].Style.BackColor = Color.Blue;
                    grid.Rows[i].Cells[4].Style.ForeColor = Color.White;
                }
                else
                {
                    grid.Rows[i].Cells[4].Style.BackColor = Color.White;
                    grid.Rows[i].Cells[4].Style.ForeColor = Color.LightGray;
                }

                if (CylinderItem.BackwardConfig.Output < 0 || cDEF.Run.Digital.OutputCount <= CylinderItem.BackwardConfig.Output)
                    Temp = "---";
                else
                    Temp = (cDEF.Run.Digital.Output[CylinderItem.BackwardConfig.Output]) ? "ON" : "OFF";
                if (grid.Rows[i].Cells[5].Value != null)
                    GridValue = grid.Rows[i].Cells[5].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[5].Value = Temp;
                if (Temp == "ON")
                {
                    grid.Rows[i].Cells[5].Style.BackColor = Color.Red;
                    grid.Rows[i].Cells[5].Style.ForeColor = Color.White;
                }
                else
                {
                    grid.Rows[i].Cells[5].Style.BackColor = Color.White;
                    grid.Rows[i].Cells[5].Style.ForeColor = Color.LightGray;
                }

                if (CylinderItem.BackwardConfig.Input < 0 || cDEF.Run.Digital.InputCount <= CylinderItem.BackwardConfig.Input)
                    Temp = "---";
                else
                    Temp = (cDEF.Run.Digital.Input[CylinderItem.BackwardConfig.Input]) ? "ON" : "OFF";
                if (grid.Rows[i].Cells[6].Value != null)
                    GridValue = grid.Rows[i].Cells[6].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[6].Value = Temp;
                if (Temp == "ON")
                {
                    grid.Rows[i].Cells[6].Style.BackColor = Color.Blue;
                    grid.Rows[i].Cells[6].Style.ForeColor = Color.White;
                }
                else
                {
                    grid.Rows[i].Cells[6].Style.BackColor = Color.White;
                    grid.Rows[i].Cells[6].Style.ForeColor = Color.LightGray;
                }

                if (CylinderItem.Action == TfpCylinderAction.fcaNone)
                    Temp = "NONE";
                else if (CylinderItem.Action == TfpCylinderAction.fcaInitializeError)
                    Temp = "INITIALIZE ERROR";
                else if (CylinderItem.Action == TfpCylinderAction.fcaForward)
                    Temp = "FORWARD";
                else if (CylinderItem.Action == TfpCylinderAction.fcaForwardError)
                    Temp = "FORWARD ERROR";
                else if (CylinderItem.Action == TfpCylinderAction.fcaForwardStart || CylinderItem.Action == TfpCylinderAction.fcaForwardCheck || CylinderItem.Action == TfpCylinderAction.fcaForwardSleep)
                    Temp = "FORWARD MOVING";
                else if (CylinderItem.Action == TfpCylinderAction.fcaForwardTimeOut)
                    Temp = "FORWARD TIMEOUT";
                else if (CylinderItem.Action == TfpCylinderAction.fcaBackward)
                    Temp = "BACKWARD";
                else if (CylinderItem.Action == TfpCylinderAction.fcaBackwardError)
                    Temp = "BACKWARD ERROR";
                else if (CylinderItem.Action == TfpCylinderAction.fcaBackwardStart || CylinderItem.Action == TfpCylinderAction.fcaBackwardCheck || CylinderItem.Action == TfpCylinderAction.fcaBackwardSleep)
                    Temp = "BACKWARD MOVING";
                else
                    Temp = "BACKWARD TIMEOUT";
                if (grid.Rows[i].Cells[7].Value != null)
                    GridValue = grid.Rows[i].Cells[7].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[7].Value = Temp;
                if (Temp == "NONE")
                    grid.Rows[i].Cells[7].Style.ForeColor = Color.Gray;
                else if (Temp == "ERROR" || Temp == "TIMEOUT")
                    grid.Rows[i].Cells[7].Style.ForeColor = Color.Red;
                else if (Temp == "FORWARD" || Temp == "BACKWARD")
                    grid.Rows[i].Cells[7].Style.ForeColor = System.Drawing.SystemColors.WindowText;
                else
                    grid.Rows[i].Cells[7].Style.ForeColor = Color.Maroon;

            }
            CylinderItem = cDEF.Run.Cylinder.Items[FSelected];
            if (CylinderItem.Action == TfpCylinderAction.fcaNone)
            {
                lbStatus.Text = "NONE";
                lbStatus.ForeColor = Color.Gray;
            }
            else if (CylinderItem.Action == TfpCylinderAction.fcaInitializeError)
            {
                lbStatus.Text = "INITIALIZE ERROR";
                lbStatus.ForeColor = Color.Red;
            }
            else if (CylinderItem.Action == TfpCylinderAction.fcaForward)
            {
                lbStatus.Text = "FORWARD";
                lbStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            else if (CylinderItem.Action == TfpCylinderAction.fcaForwardError)
            {
                lbStatus.Text = "FORWARD ERROR";
                lbStatus.ForeColor = Color.Red;
            }
            else if (CylinderItem.Action == TfpCylinderAction.fcaForwardStart || CylinderItem.Action == TfpCylinderAction.fcaForwardCheck || CylinderItem.Action == TfpCylinderAction.fcaForwardSleep)
            {
                lbStatus.Text = "FORWARD MOVING";
                lbStatus.ForeColor = Color.Maroon;
            }
            else if (CylinderItem.Action == TfpCylinderAction.fcaForwardTimeOut)
            {
                lbStatus.Text = "FORWARD TIMEOUT";
                lbStatus.ForeColor = Color.Red;
            }
            else if (CylinderItem.Action == TfpCylinderAction.fcaBackward)
            {
                lbStatus.Text = "BACKWARD";
                lbStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            else if (CylinderItem.Action == TfpCylinderAction.fcaBackwardError)
            {
                lbStatus.Text = "BACKWARD ERROR";
                lbStatus.ForeColor = Color.Red;
            }
            else if (CylinderItem.Action == TfpCylinderAction.fcaBackwardStart || CylinderItem.Action == TfpCylinderAction.fcaBackwardCheck || CylinderItem.Action == TfpCylinderAction.fcaBackwardSleep)
            {
                lbStatus.Text = "BACKWARD MOVING";
                lbStatus.ForeColor = Color.Maroon;
            }
            else
            {
                lbStatus.Text = "BACKWARD TIMEOUT";
                lbStatus.ForeColor = Color.Red;
            }
            btnMonitoring.Enabled = !CylinderItem.Active;
            if (CylinderItem.ForwardConfig.Output < 0 || cDEF.Run.Digital.OutputCount <= CylinderItem.ForwardConfig.Output)
                lbForwardOutputState.BackColor = Color.Black;
            else
                lbForwardOutputState.BackColor = cDEF.Run.Digital.Output[CylinderItem.ForwardConfig.Output] ? Color.Red : Color.Black;
            btnForwardOutput.Enabled = !CylinderItem.Active;
            if (CylinderItem.ForwardConfig.Input < 0 || cDEF.Run.Digital.InputCount <= CylinderItem.ForwardConfig.Input)
                lbForwardInputState.BackColor = Color.Black;
            else
                lbForwardInputState.BackColor = cDEF.Run.Digital.Input[CylinderItem.ForwardConfig.Input] ? Color.Blue : Color.Black;
            btnForwardInput.Enabled = !CylinderItem.Active;
            if (CylinderItem.BackwardConfig.Output < 0 || cDEF.Run.Digital.OutputCount <= CylinderItem.BackwardConfig.Output)
                lbBackwardOutputState.BackColor = Color.Black;
            else
                lbBackwardOutputState.BackColor = cDEF.Run.Digital.Output[CylinderItem.BackwardConfig.Output] ? Color.Red : Color.Black;
            btnBackwardOutput.Enabled = !CylinderItem.Active;
            if (CylinderItem.BackwardConfig.Input < 0 || cDEF.Run.Digital.InputCount <= CylinderItem.BackwardConfig.Input)
                lbBackwardInputState.BackColor = Color.Black;
            else
                lbBackwardInputState.BackColor = cDEF.Run.Digital.Input[CylinderItem.BackwardConfig.Input] ? Color.Blue : Color.Black;
            btnBackwardInput.Enabled = !CylinderItem.Active;
            btnEnable.Enabled = !CylinderItem.Active;
            btnDisable.Enabled = CylinderItem.Active;
            btnAllOutputOff.Enabled = !CylinderItem.Active;
            btnForward.Enabled = CylinderItem.IsBackward();
            btnForward.Visible = !CylinderItem.RepeatMode;
            btnBackward.Enabled = CylinderItem.IsForward();
            btnBackward.Visible = !CylinderItem.RepeatMode;
            btnRepeat.BackColor = (CylinderItem.RepeatMode) ? Color.Blue : Color.Black;
            btnRepeat.Enabled = CylinderItem.IsDone();
            btnStart.Enabled = CylinderItem.IsDone();
            btnStart.Visible = CylinderItem.RepeatMode;
            btnStop.Enabled = CylinderItem.Repeat;
            btnStop.Visible = CylinderItem.RepeatMode;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            UpdateInformation();
            timer1.Enabled = true;
        }
        public void ChangeLanguage()
        {
            btnEnable.Text = cDEF.Lang.Trans("ACTIVATE");
            btnDisable.Text = cDEF.Lang.Trans("DEACTIVATE");
            btnAllOutputOff.Text = cDEF.Lang.Trans("OUTPUT OFF");
            groupboxConfig.Text = cDEF.Lang.Trans("CONFIGURATION");
            gorupboxInform.Text = cDEF.Lang.Trans("Information");
            groupbox_ForInform.Text = cDEF.Lang.Trans("Forward Information");
            groupbox_BackInform.Text = cDEF.Lang.Trans("Backward Information");

        }
    }
}

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
    public partial class FrmPageConfigLamp : TFrame
    {
        int FSelected;
        public FrmPageConfigLamp()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FormPageConfigLamp_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
            FSelected = 0;

            if (grid.RowCount != cDEF.Run.Lamp.Count)
                grid.RowCount = cDEF.Run.Lamp.Count;

            UpdateDetailInformation();
        }
        private void UpdateDetailInformation()
        {
            int i, j;
            String Temp;
            String GridValue = "";

            if (cDEF.Run.Lamp.Count == 0)
                return;

            for (i = 0; i < cDEF.Run.Lamp.Count; i++)
            {
                Temp = (i + 1).ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;

                Temp = cDEF.Run.Lamp.Items[i].Key.ToUpper();
                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[1].Value = Temp;

                if (cDEF.Run.Lamp.Items[i].Target == -1)
                    Temp = "DISABLE";
                else
                    Temp = cDEF.Run.Digital.OutputItems[cDEF.Run.Lamp.Items[i].Target].Text + "[" + (cDEF.Run.Lamp.Items[i].Target).ToString() + "]";
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;

                Temp = "";
                for(j = 0; j < (int)TfpLampStyle.flsEnd - 1; j ++)
                {
                    //if (TfpLamp.Lamp.Items[i].FOutput[j] == null)
                    //    Temp = "DISABLE";
                    //else
                    //{
                        
                        if (cDEF.Run.Lamp.Items[i].FOutput[j] == TfpLampStyle.flsNone)  
                        {
                            if (Temp != String.Empty)
                                Temp += ", ";
                            Temp += "N";
                        }
                        if (cDEF.Run.Lamp.Items[i].FOutput[j] == TfpLampStyle.flsAlarm)
                        {
                            if (Temp != String.Empty)
                                Temp += ", ";
                            Temp += "A";
                        }
                        if (cDEF.Run.Lamp.Items[i].FOutput[j] == TfpLampStyle.flsInitialize)
                        {
                            if (Temp != String.Empty)
                                Temp += ", ";
                            Temp += "I";
                        }
                        if (cDEF.Run.Lamp.Items[i].FOutput[j] == TfpLampStyle.flsStop)
                        {
                            if (Temp != String.Empty)
                                Temp += ", ";
                            Temp += "S";
                        }
                        if (cDEF.Run.Lamp.Items[i].FOutput[j] == TfpLampStyle.flsWarning)
                        {
                            if (Temp != String.Empty)
                                Temp += ", ";
                            Temp += "W";
                        }
                        if (cDEF.Run.Lamp.Items[i].FOutput[j] == TfpLampStyle.flsRun)
                        {
                            if (Temp != String.Empty)
                                Temp += ", ";
                            Temp += "R";
                        }
                    //}
                }
               
                if (grid.Rows[i].Cells[3].Value != null)
                    GridValue = grid.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[3].Value = Temp;
            }

            btnIndex.Text = cDEF.Run.Lamp.Items[FSelected].Key.ToUpper();
            if (cDEF.Run.Lamp.Items[FSelected].Target < 0 || cDEF.Run.Digital.OutputCount <= cDEF.Run.Lamp.Items[FSelected].Target)
            {
                btnTarget.Text = "DISABLE";
                btnTarget.ForeColor = Color.Gray;
            }
            else
            {
                btnTarget.Text = cDEF.Run.Digital.OutputItems[cDEF.Run.Lamp.Items[FSelected].Target].Text + " ( " + cDEF.Run.Lamp.Items[FSelected].Target + " )";
                btnTarget.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            if (!cDEF.Run.Lamp.Items[FSelected].Buzzer)
            {
                btnBuzzer.Text = "DISABLE";
                btnBuzzer.ForeColor = Color.Gray;
            }
            else
            {
                btnBuzzer.Text = "ENABLE";
                btnBuzzer.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            Temp = "";
            for (j = 0; j < (int)TfpLampStyle.flsEnd - 1; j++)
            {
                 if (cDEF.Run.Lamp.Items[FSelected].FBlink[j] == TfpLampStyle.flsNone)
                 {
                     if (Temp != String.Empty)
                         Temp += ", ";
                     Temp += "NONE";
                 }
                 if (cDEF.Run.Lamp.Items[FSelected].FBlink[j] == TfpLampStyle.flsAlarm)
                 {
                     if (Temp != String.Empty)
                         Temp += ", ";
                     Temp += "ALARM";
                 }
                 if (cDEF.Run.Lamp.Items[FSelected].FBlink[j] == TfpLampStyle.flsInitialize)
                 {
                     if (Temp != String.Empty)
                         Temp += ", ";
                     Temp += "INITIALIZE";
                 }
                 if (cDEF.Run.Lamp.Items[FSelected].FBlink[j] == TfpLampStyle.flsStop)
                 {
                     if (Temp != String.Empty)
                         Temp += ", ";
                     Temp += "STOP";
                 }
                 if (cDEF.Run.Lamp.Items[FSelected].FBlink[j] == TfpLampStyle.flsWarning)
                 {
                     if (Temp != String.Empty)
                         Temp += ", ";
                     Temp += "WARNING";
                 }
                 if (cDEF.Run.Lamp.Items[FSelected].FBlink[j] == TfpLampStyle.flsRun)
                 {
                     if (Temp != String.Empty)
                         Temp += ", ";
                     Temp += "RUN";
                 }
                 btnBlink.Text = Temp;
                 btnBlink.ForeColor = System.Drawing.SystemColors.WindowText;
            }

            Temp = "";
            for (j = 0; j < (int)TfpLampStyle.flsEnd - 1; j++)
            {
                    
                if (cDEF.Run.Lamp.Items[FSelected].FOutput[j] == TfpLampStyle.flsNone)
                {
                    if (Temp != String.Empty)
                        Temp += ", ";
                    Temp += "NONE";
                }
                if (cDEF.Run.Lamp.Items[FSelected].FOutput[j] == TfpLampStyle.flsAlarm)
                {
                    if (Temp != String.Empty)
                        Temp += ", ";
                    Temp += "ALARM";
                }
                if (cDEF.Run.Lamp.Items[FSelected].FOutput[j] == TfpLampStyle.flsInitialize)
                {
                    if (Temp != String.Empty)
                        Temp += ", ";
                    Temp += "INITIALIZE";
                }
                if (cDEF.Run.Lamp.Items[FSelected].FOutput[j] == TfpLampStyle.flsStop)
                {
                    if (Temp != String.Empty)
                        Temp += ", ";
                    Temp += "STOP";
                }
                if (cDEF.Run.Lamp.Items[FSelected].FOutput[j] == TfpLampStyle.flsWarning)
                {
                    if (Temp != String.Empty)
                        Temp += ", ";
                    Temp += "WARNING";
                }
                if (cDEF.Run.Lamp.Items[FSelected].FOutput[j] == TfpLampStyle.flsRun)
                {
                    if (Temp != String.Empty)
                        Temp += ", ";
                    Temp += "RUN";
                }
                btnOutput.Text = Temp;
                btnOutput.ForeColor = System.Drawing.SystemColors.WindowText;          
            }       
        }
        protected override void UpdateInformation()
        {
            if (!Visible)
                return;

            if (cDEF.Run.Lamp == null)
                return;

            if (cDEF.Run.Lamp.Count == 0)
                return;

            int i;
            String Temp;
            String GridValue = "";

            this.Invoke(new Action(delegate()
            {
                for (i = 0; i < cDEF.Run.Lamp.Count; i++)
                {
                    if (0 <= cDEF.Run.Lamp.Items[i].Target && cDEF.Run.Lamp.Items[i].Target < cDEF.Run.Digital.OutputCount && cDEF.Run.Digital.Output[cDEF.Run.Lamp.Items[i].Target])
                        Temp = "ON";
                    else
                        Temp = "";
                    if (grid.Rows[i].Cells[4].Value != null)
                        GridValue = grid.Rows[i].Cells[4].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[4].Value = Temp;
                }
                if (cDEF.Run.Lamp.Items[FSelected].Target < 0 || cDEF.Run.Digital.OutputCount <= cDEF.Run.Lamp.Items[FSelected].Target)
                    lbTargetState.BackColor = Color.Black;
                else
                    lbTargetState.BackColor = (cDEF.Run.Digital.Output[cDEF.Run.Lamp.Items[FSelected].Target]) ? Color.Red : Color.Black;

            }));
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

        private void ConfigClick(object sender, EventArgs e)
        {
	        int i, Value = -1;
	        String Temp = "";
	        bool[] BooleanValues = new bool[6];

	        switch(Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        for(i = 0; i < cDEF.Run.Lamp.Count; i ++)
			        {
				        if(i != 0)
					        Temp += ",";
				        Temp += cDEF.Run.Lamp.Items[i].Key.ToUpper();
			        }
                    if (XModuleMain.frmBox.SelectBox("INDEX", Temp, ref FSelected) == DialogResult.No)
                        return;
			        UpdateDetailInformation();
			        break;
		        case 1:
			        Value = -1;
                    if(XModuleMain.frmBox.SelectBox("TARGET", "DISABLE,ENABLE",ref Value) == DialogResult.No)
                        return;
			        if(Value == 0)
			        {
                        cDEF.Run.Lamp.Items[FSelected].Target = -1;
                        cDEF.Run.Lamp.Save(cPath.FILE_LAMP);
			        }
			        else
			        {
				        for(i = 0; i < cDEF.Run.Digital.OutputCount; i ++)
				        {
					        if(i != 0)
						        Temp += ",";
                            Temp += cDEF.Run.Digital.OutputItems[i].Text.ToUpper() + "( " + (i).ToString() + " )";
				        }
				        Value = cDEF.Run.Lamp.Items[FSelected].Target;
				        if(XModuleMain.frmBox.SelectBox("TARGET", Temp, ref Value) == DialogResult.No)
                            return;
                        cDEF.Run.Lamp.Items[FSelected].Target = Value;
                        cDEF.Run.Lamp.Save(cPath.FILE_LAMP);
			        }
			        break;
		        case 2:
			        Value = Convert.ToInt32(cDEF.Run.Lamp.Items[FSelected].Buzzer);
			        if(XModuleMain.frmBox.SelectBox("BUZZER", "DISABLE,ENABLE",ref Value, 2) == DialogResult.No)
                        return;
                    cDEF.Run.Lamp.Items[FSelected].Buzzer = Convert.ToBoolean(Value);
                    cDEF.Run.Lamp.Save(cPath.FILE_LAMP);
			        break;
		        case 3:
                    for (i = 0; i < 6; i++)
                        BooleanValues[i] = Convert.ToBoolean((int)cDEF.Run.Lamp.Items[FSelected].FBlink[i]);
                    XModuleMain.frmBox.fpScriptBoxClear();
                    XModuleMain.frmBox.fpScriptBoxAdd("NONE", "", "", ref BooleanValues[0], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("ALARM", "", "", ref BooleanValues[1], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("INITIALIZE", "", "", ref BooleanValues[2], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("STOP", "", "", ref BooleanValues[3], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("WARNING", "", "", ref BooleanValues[4], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("RUN", "", "", ref BooleanValues[5], "");

			        if (XModuleMain.frmBox.fpScriptBox("BLINK") == false)
                        return;

			        for(i = 0; i < 6; i ++)
                    {
                        if (Convert.ToBoolean(XModuleMain.bValue[i]))
                            cDEF.Run.Lamp.Items[FSelected].FBlink[i] = (TfpLampStyle) i + 1;
                        else
                            cDEF.Run.Lamp.Items[FSelected].FBlink[i] = (TfpLampStyle)0;
                    }

                    cDEF.Run.Lamp.Save(cPath.FILE_LAMP);
			        break;
		        case 4:
                    for (i = 0; i < 6; i++)
                        BooleanValues[i] = Convert.ToBoolean((int)cDEF.Run.Lamp.Items[FSelected].FOutput[i]);
                    XModuleMain.frmBox.fpScriptBoxClear();
                    XModuleMain.frmBox.fpScriptBoxAdd("NONE", "", "", ref BooleanValues[0], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("ALARM", "", "", ref BooleanValues[1], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("INITIALIZE", "", "", ref BooleanValues[2], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("STOP", "", "", ref BooleanValues[3], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("WARNING", "", "", ref BooleanValues[4], "");
                    XModuleMain.frmBox.fpScriptBoxAdd("RUN", "", "", ref BooleanValues[5], "");

                    if (XModuleMain.frmBox.fpScriptBox("OUTPUT") == false)
                        return;

			        Value = 0;
			        for(i = 0; i < 6; i ++)
                    {
                        if (Convert.ToBoolean(XModuleMain.bValue[i]))
                            cDEF.Run.Lamp.Items[FSelected].FOutput[i] = (TfpLampStyle)i + 1;
                        else
                            cDEF.Run.Lamp.Items[FSelected].FOutput[i] = (TfpLampStyle)0;
                    }
                    cDEF.Run.Lamp.Save(cPath.FILE_LAMP);
			        break;
	        }
            UpdateDetailInformation();
        }
        public void ChangeLanguage()
        {
            lbItems.Text = cDEF.Lang.Trans("ITEMS");
            lbConfiguration.Text = cDEF.Lang.Trans("CONFIGURATION");

        }
    }
}

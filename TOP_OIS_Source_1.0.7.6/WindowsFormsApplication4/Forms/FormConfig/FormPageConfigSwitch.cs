using System;
using System.Drawing;
using System.Windows.Forms;
using XModule.Standard;


namespace XModule.Forms.FormConfig
{
    public partial class FrmPageConfigSwitch : TFrame
    {
        int FSelected;
        public FrmPageConfigSwitch()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FrmPageConfigSwitch_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
            FSelected = 0;

            grid.RowCount = cDEF.Run.Switch.Count;
            UpdateDetailInformation();
        }
        private void UpdateDetailInformation()
        {
            TfpSwitchItem SwitchItem;
            String Temp;
            String GridValue = "";
            int i;

            if (cDEF.Run.Switch.Count == 0)
                return;

            for (i = 0; i < cDEF.Run.Switch.Count; i++)
            {
                SwitchItem = cDEF.Run.Switch.Items[i];

                Temp = i.ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;

                Temp = SwitchItem.Key.ToUpper();
                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[1].Value = Temp;

                if (SwitchItem.Input == -1)
                    Temp = "DISABLE";
                else
                    Temp = cDEF.Run.Digital.InputItems[SwitchItem.Input].Text.ToUpper() + "[" + (SwitchItem.Input).ToString() + "]";
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;

                if (SwitchItem.Style == TfpSwitchStyle.fssNone)
                    Temp = "NONE";
                else if (SwitchItem.Style == TfpSwitchStyle.fssEmergency)
                    Temp = "EMERGENCY";
                else if (SwitchItem.Style == TfpSwitchStyle.fssWarning)
                    Temp = "WARNING";
                else if (SwitchItem.Style == TfpSwitchStyle.fssStart)
                    Temp = "START";
                else if (SwitchItem.Style == TfpSwitchStyle.fssStop)
                    Temp = "STOP";
                else
                    Temp = "RESET";
                if (grid.Rows[i].Cells[3].Value != null)
                    GridValue = grid.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[3].Value = Temp;
            }

            SwitchItem = cDEF.Run.Switch.Items[FSelected];

            btnIndex.Text = SwitchItem.Key.ToUpper();
            if (SwitchItem.Input == -1)
            {
                btnInput.Text = "DISABLE";
                btnInput.ForeColor = Color.Gray;
            }
            else
            {
                btnInput.Text = cDEF.Run.Digital.InputItems[SwitchItem.Input].Text;
                btnInput.ForeColor = Color.Gray;
            }
            if (SwitchItem.Style == TfpSwitchStyle.fssNone)
            {
                btnStyle.Text = "NONE";
                btnStyle.ForeColor = Color.Gray;
            }
            else
            {
                if (SwitchItem.Style == TfpSwitchStyle.fssEmergency)
                    btnStyle.Text = "EMERGENCY";
                else if (SwitchItem.Style == TfpSwitchStyle.fssWarning)
                    btnStyle.Text = "WARNING";
                else if (SwitchItem.Style == TfpSwitchStyle.fssStart)
                    btnStyle.Text = "START";
                else if (SwitchItem.Style == TfpSwitchStyle.fssStop)
                    btnStyle.Text = "STOP";
                else
                    btnStyle.Text = "RESET";
                btnStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }
        protected override void UpdateInformation()
        {
            if (!Visible)
                return;

            if (cDEF.Run.Switch == null)
                return;

            if (cDEF.Run.Switch.Count == 0)
                return;

            int i;
            String Temp;
            String GridValue = "";
            this.Invoke(new Action(delegate()
            {
                for (i = 0; i < cDEF.Run.Switch.Count; i++)
                {
                    if (cDEF.Run.Switch.Items[i].Input != -1 && cDEF.Run.Digital.Input[cDEF.Run.Switch.Items[i].Input])
                        Temp = "ON";
                    else
                        Temp = "";
                    if (grid.Rows[i].Cells[4].Value != null)
                        GridValue = grid.Rows[i].Cells[4].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[4].Value = Temp;
                }
                if (cDEF.Run.Switch.Items[FSelected].Input < 0 || cDEF.Run.Digital.InputCount <= cDEF.Run.Switch.Items[FSelected].Input)
                    lbInputState.BackColor = Color.Black;
                else
                    lbInputState.BackColor = (cDEF.Run.Digital.Input[cDEF.Run.Switch.Items[FSelected].Input]) ? Color.Blue : Color.Black;
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
        private void ConfigurationClick(object sender, EventArgs e)
        {
	        int i, Value;
	        String Temp = "";

            switch (Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        for(i = 0; i < cDEF.Run.Switch.Count; i ++)
			        {
				        if(i != 0)
					        Temp += ",";
				        Temp += cDEF.Run.Switch.Items[i].Key + (i + 1).ToString();
			        }

                    if (XModuleMain.frmBox.SelectBox("INDEX", Temp, ref FSelected) == DialogResult.No)
                        return;

			        break;
		        case 1:
			        Value = -1;
                    if (XModuleMain.frmBox.SelectBox("INPUT", "DISABLE,ENABLE", ref Value) == DialogResult.No)
                        return;
			        if(Value == 0)
			        {
                        cDEF.Run.Switch.Items[FSelected].Input = -1;
                        cDEF.Run.Switch.Save(cPath.FILE_SWITCH);
			        }
			        else
			        {
				        for(i = 0; i < cDEF.Run.Digital.InputCount; i ++)
				        {
					        if(i != 0)
						        Temp += ",";
                            Temp += cDEF.Run.Digital.InputItems[i].Text.ToUpper() + "( " + (i).ToString() + " )";
				        }
            	        Value = cDEF.Run.Switch.Items[FSelected].Input;
                        if (XModuleMain.frmBox.SelectBox("INPUT", Temp, ref Value) == DialogResult.No)
                            return;
                        cDEF.Run.Switch.Items[FSelected].Input = Value;
                        cDEF.Run.Switch.Save(cPath.FILE_SWITCH);
			        }
			        break;
		        case 2:
			        Value = (int)cDEF.Run.Switch.Items[FSelected].Style;
                    if (XModuleMain.frmBox.SelectBox("STYLE", "NONE,EMERGENCY,WARNING,START,STOP,RESET", ref Value) == DialogResult.No)
                        return;

                    cDEF.Run.Switch.Items[FSelected].Style = (TfpSwitchStyle) Value;
                    cDEF.Run.Switch.Save(cPath.FILE_SWITCH);
			        break;
	        }
            UpdateDetailInformation();
        }

        private void a1Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ChangeLanguage()
        {
            lbItems.Text = cDEF.Lang.Trans("ITEMS");
            lbConfiguration.Text = cDEF.Lang.Trans("CONFIGURATION");

        }
    }
}

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
    public partial class FrmPageConfigDigitalLink : Form
    {
        public FrmPageConfigDigitalLink()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FrmPageCofigDigitalLink_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
            int i;
            String Temp;
            String GridValue = "";
            TfpDigitalInput DigitalInput;
            TfpDigitalOutput DigitalOutput;

            if (sgInput.RowCount != cDEF.Run.Digital.InputCount)
                sgInput.RowCount = cDEF.Run.Digital.InputCount;

            for (i = 0; i < cDEF.Run.Digital.InputCount; i++)
            {
                DigitalInput = cDEF.Run.Digital.InputItems[i];
                Temp = (i).ToString();
                if (sgInput.Rows[i].Cells[0].Value != null)
                    GridValue = sgInput.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[0].Value = $"X0{i/32 +1}.  {i} "; //i.ToString("X");//Temp;

                if (DigitalInput.Target == -1)
                    Temp = "---";
                else
                    Temp = (DigitalInput.Target).ToString();

                if (sgInput.Rows[i].Cells[1].Value != null)
                    GridValue = sgInput.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[1].Value = Temp;
                if (Temp == "---" || Temp == (i).ToString())
                {
                    sgInput.Rows[i].Cells[1].Style.BackColor = Color.White;
                    sgInput.Rows[i].Cells[1].Style.ForeColor = Color.LightGray;
                }
                else
                {
                    sgInput.Rows[i].Cells[1].Style.BackColor = Color.Red;
                    sgInput.Rows[i].Cells[1].Style.ForeColor = Color.White;
                }

                Temp = DigitalInput.Text;
                if (sgInput.Rows[i].Cells[2].Value != null)
                    GridValue = sgInput.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[2].Value = Temp;
                sgInput.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                sgInput.Rows[i].Cells[2].Style.Padding = new Padding(3,0,0,0);

                if (DigitalInput.Target == -1)
                    Temp = "---";
                else
                    Temp = (DigitalInput.Reverse) ? "ON" : "OFF";
                if (sgInput.Rows[i].Cells[3].Value != null)
                    GridValue = sgInput.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[3].Value = Temp;
                if (Temp == "ON")
                {
                    sgInput.Rows[i].Cells[3].Style.BackColor = Color.Red;
                    sgInput.Rows[i].Cells[3].Style.ForeColor = Color.White;
                }
                else
                {
                    sgInput.Rows[i].Cells[3].Style.BackColor = Color.White;
                    sgInput.Rows[i].Cells[3].Style.ForeColor = Color.LightGray;
                }
            }

            if (sgOutput.RowCount != cDEF.Run.Digital.OutputCount)
                sgOutput.RowCount = cDEF.Run.Digital.OutputCount;

            for (i = 0; i < cDEF.Run.Digital.OutputCount; i++)
            {
                DigitalOutput = cDEF.Run.Digital.OutputItems[i];
                Temp = (i).ToString();
                if (sgOutput.Rows[i].Cells[0].Value != null)
                    GridValue = sgOutput.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    sgOutput.Rows[i].Cells[0].Value = $"Y0{i / 32 + 1}.  {i} "; //i.ToString("X");//Temp;
                if (DigitalOutput.Target == -1)
                    Temp = "---";
                else
                    Temp = (DigitalOutput.Target).ToString();
                if (sgOutput.Rows[i].Cells[1].Value != null)
                    GridValue = sgOutput.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    sgOutput.Rows[i].Cells[1].Value = Temp;
                if (Temp == "---" || Temp == (i).ToString())
                {
                    sgOutput.Rows[i].Cells[1].Style.BackColor = Color.White;
                    sgOutput.Rows[i].Cells[1].Style.ForeColor = Color.LightGray;
                }
                else
                {
                    sgOutput.Rows[i].Cells[1].Style.BackColor = Color.Red;
                    sgOutput.Rows[i].Cells[1].Style.ForeColor = Color.White;
                }

                Temp = DigitalOutput.Text;
                if (sgOutput.Rows[i].Cells[2].Value != null)
                    GridValue = sgOutput.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    sgOutput.Rows[i].Cells[2].Value = Temp;
                sgOutput.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                sgOutput.Rows[i].Cells[2].Style.Padding = new Padding(3, 0, 0, 0);
            }

        }
        private void UpdateDetailInformation()
        {
            int i;
            String Temp;
            String GridValue = "";
            TfpDigitalInput DigitalInput;
            TfpDigitalOutput DigitalOutput;

            if (sgInput.RowCount != cDEF.Run.Digital.InputCount)
                sgInput.RowCount = cDEF.Run.Digital.InputCount;

            for (i = 0; i < cDEF.Run.Digital.InputCount; i++)
            {
                DigitalInput = cDEF.Run.Digital.InputItems[i];
                Temp = (i).ToString();
                if (sgInput.Rows[i].Cells[0].Value != null)
                    GridValue = sgInput.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[0].Value = $"X0{i / 32 + 1}.  {i} "; //i.ToString("X");//Temp;

                if (DigitalInput.Target == -1)
                    Temp = "---";
                else
                    Temp = (DigitalInput.Target).ToString();
                if (sgInput.Rows[i].Cells[1].Value != null)
                    GridValue = sgInput.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[1].Value = Temp;
                if (Temp == "---" || Temp == (i).ToString())
                {
                    sgInput.Rows[i].Cells[1].Style.BackColor = Color.White;
                    sgInput.Rows[i].Cells[1].Style.ForeColor = Color.LightGray;
                }   
                else
                {
                    sgInput.Rows[i].Cells[1].Style.BackColor = Color.Red;
                    sgInput.Rows[i].Cells[1].Style.ForeColor = Color.White;
                }

                Temp = DigitalInput.Text;
                if (sgInput.Rows[i].Cells[2].Value != null)
                    GridValue = sgInput.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[2].Value = Temp;
                sgInput.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                sgInput.Rows[i].Cells[2].Style.Padding = new Padding(3, 0, 0, 0);

                if (DigitalInput.Target == -1)
                    Temp = "---";
                else
                    Temp = (DigitalInput.Reverse) ? "ON" : "OFF";
                if (sgInput.Rows[i].Cells[3].Value != null)
                    GridValue = sgInput.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[3].Value = Temp;
                if (Temp == "ON")
                {
                    sgInput.Rows[i].Cells[3].Style.BackColor = Color.Red;
                    sgInput.Rows[i].Cells[3].Style.ForeColor = Color.White;
                }
                else
                {
                    sgInput.Rows[i].Cells[3].Style.BackColor = Color.White;
                    sgInput.Rows[i].Cells[3].Style.ForeColor = Color.LightGray;
                }
            }

            if (sgOutput.RowCount != cDEF.Run.Digital.OutputCount)
                sgOutput.RowCount = cDEF.Run.Digital.OutputCount;

            for (i = 0; i < cDEF.Run.Digital.OutputCount; i++)
            {
                DigitalOutput = cDEF.Run.Digital.OutputItems[i];
                Temp = (i).ToString();
                if (sgOutput.Rows[i].Cells[0].Value != null)
                    GridValue = sgOutput.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    sgOutput.Rows[i].Cells[0].Value = $"Y0{i / 32 + 1}.  {i} "; //i.ToString("X");//Temp;

                if (DigitalOutput.Target == -1)
                    Temp = "---";
                else
                    Temp = (DigitalOutput.Target).ToString();
                if (sgOutput.Rows[i].Cells[1].Value != null)
                    GridValue = sgOutput.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    sgOutput.Rows[i].Cells[1].Value = Temp;
                if (Temp == "---" || Temp == (i).ToString())
                {
                    sgOutput.Rows[i].Cells[1].Style.BackColor = Color.White;
                    sgOutput.Rows[i].Cells[1].Style.ForeColor = Color.LightGray;
                }
                else
                {
                    sgOutput.Rows[i].Cells[1].Style.BackColor = Color.Red;
                    sgOutput.Rows[i].Cells[1].Style.ForeColor = Color.White;
                }


                Temp = DigitalOutput.Text;
                if (sgOutput.Rows[i].Cells[2].Value != null)
                    GridValue = sgOutput.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    sgOutput.Rows[i].Cells[2].Value = Temp;
                sgOutput.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                sgOutput.Rows[i].Cells[2].Style.Padding = new Padding(3, 0, 0, 0);
            }

            
        }
        private void GridDblClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row, Value = 0;
            String Temp;

            DataGridView Grid = (DataGridView)sender;

            Col = Grid.CurrentCell.ColumnIndex;
            Row = Grid.CurrentCell.RowIndex;

            //if (Grid.Row != Row)
            //    return;

            switch (Col)
            {
                case 1:
                    if (Convert.ToInt32(Grid.Tag) == 0)
                    {
                        Value = cDEF.Run.Digital.InputItems[Row].Target;
                        if (!XModuleMain.frmBox.fpIntegerEdit("INPUT TARGET #" + (Row).ToString(), ref Value, "", "CURRENT", Row, 0, cDEF.Run.Digital.InputCount))
                            return;
                        {
                            cDEF.Run.Digital.InputItems[Row].Target = Value;
                            cDEF.Run.Digital.Save(cPath.FILE_DIGITAL);
                        }
                    }
                    else if (Convert.ToInt32(Grid.Tag) == 1)
                    {
                        Value = cDEF.Run.Digital.OutputItems[Row].Target;
                        if (XModuleMain.frmBox.fpIntegerEdit("OUTPUT TARGET #" + (Row).ToString(), ref Value, "", "CURRENT", Row, 0, cDEF.Run.Digital.OutputCount))
                        {
                            cDEF.Run.Digital.OutputItems[Row].Target = Value;
                            cDEF.Run.Digital.Save(cPath.FILE_DIGITAL);
                        }
                    }
                    
                    break;
                case 2:
                    if (Convert.ToInt32(Grid.Tag) == 0)
                    {
                        Temp = cDEF.Run.Digital.InputItems[Row].Text;
                        if (cDEF.fTextEdit.TextEdit("INPUT DESCRIPTION - #" + (Row).ToString(), ref Temp, "\\/:*?\"<>|"))
                        {
                            cDEF.Run.Digital.InputItems[Row].Text = Temp;
                            cDEF.Run.Digital.Save(cPath.FILE_DIGITAL);
                        }
                    }
                    else
                    {
                        Temp = cDEF.Run.Digital.OutputItems[Row].Text;
                        if (cDEF.fTextEdit.TextEdit("OUTPUT DESCRIPTION - #" + (Row).ToString(), ref Temp, "\\/:*?\"<>|"))
                        {
                            cDEF.Run.Digital.OutputItems[Row].Text = Temp;
                            cDEF.Run.Digital.Save(cPath.FILE_DIGITAL);
                        }
                    }
                    break;
                case 3:
                    if (Convert.ToInt32(Grid.Tag) == 0)
                    {
                        Value = Convert.ToInt32(cDEF.Run.Digital.InputItems[Row].Reverse);
                        if (XModuleMain.frmBox.SelectBox("INPUT REVERSE #" + (Row), "OFF,ON",ref Value) == DialogResult.Yes)
                        {
                            cDEF.Run.Digital.InputItems[Row].Reverse = Convert.ToBoolean(Value);
                            cDEF.Run.Digital.Save(cPath.FILE_DIGITAL);
                        }
                    }
                    else
                        cDEF.Run.Digital.Output[Row] = !cDEF.Run.Digital.Output[Row];
                    break;
            }
            UpdateDetailInformation();
        }
        private void UpdateInformation()
        {
	        if(!Visible)
		        return;

            if (cDEF.Run.Digital == null)
                return;

	        int i;
	        String Temp = "";
            String GridValue = "";

	        for(i = 0; i < cDEF.Run.Digital.InputCount; i ++)
	        {
                if (cDEF.Run.Digital.InputItems[i].Target == -1)
			        Temp = "---";
		        else
                    Temp = (cDEF.Run.Digital.Input[i]) ? "ON" : "OFF";
                if (sgInput.Rows[i].Cells[4].Value != null)
                    GridValue = sgInput.Rows[i].Cells[4].Value.ToString();
                if (GridValue != Temp)
                    sgInput.Rows[i].Cells[4].Value = Temp;

                if (Temp == "ON")
                {
                    sgInput.Rows[i].Cells[4].Style.BackColor = Color.Blue;
                    sgInput.Rows[i].Cells[4].Style.ForeColor = Color.White;
                }
                else
                {
                    sgInput.Rows[i].Cells[4].Style.BackColor = Color.White;
                    sgInput.Rows[i].Cells[4].Style.ForeColor = Color.LightGray;
                }
	        }
	        for(i = 0; i < cDEF.Run.Digital.OutputCount; i ++)
	        {
                if (cDEF.Run.Digital.OutputItems[i].Target == -1)
			        Temp = "---";
		        else
                    Temp = (cDEF.Run.Digital.Output[i]) ? "ON" : "OFF";
                if (sgOutput.Rows[i].Cells[3].Value != null)
                    GridValue = sgOutput.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    sgOutput.Rows[i].Cells[3].Value = Temp;

                if (Temp == "ON")
                {
                    sgOutput.Rows[i].Cells[3].Style.BackColor = Color.Red;
                    sgOutput.Rows[i].Cells[3].Style.ForeColor = Color.White;
                }
                else
                {
                    sgOutput.Rows[i].Cells[3].Style.BackColor = Color.White;
                    sgOutput.Rows[i].Cells[3].Style.ForeColor = Color.LightGray;
                }
	        }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            UpdateInformation();
            timer1.Enabled = true;
        }
        public void ChangeLanguage()
        {
            lbInputTitle.Text = cDEF.Lang.Trans("INPUT LIST");
            lbOutputTitle.Text = cDEF.Lang.Trans("OUTPUT LIST");
        }

      
    }
}

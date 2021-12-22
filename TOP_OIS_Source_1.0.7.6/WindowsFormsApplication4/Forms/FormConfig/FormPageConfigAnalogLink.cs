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
    public partial class FrmPageConfigAnalogLink : Form
    {
        public FrmPageConfigAnalogLink()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FrmPageCofigAnalogLink_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
            int i;
            String Temp;
            String GridValue = "";
            TfpAnalogInput AnalogInput;

            

            if (sgAInput1.RowCount != cDEF.Run.Analog.AnalogCount)
                sgAInput1.RowCount = cDEF.Run.Analog.AnalogCount;

            for (i = 0; i < cDEF.Run.Analog.AnalogCount; i++)
            {
                AnalogInput = cDEF.Run.Analog.AnalogItems[i];
                Temp = (i).ToString();
                if (sgAInput1.Rows[i].Cells[0].Value != null)
                    GridValue = sgAInput1.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    sgAInput1.Rows[i].Cells[0].Value = Temp;

                Temp = AnalogInput.Text;
                if (sgAInput1.Rows[i].Cells[1].Value != null)
                    GridValue = sgAInput1.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    sgAInput1.Rows[i].Cells[1].Value = Temp;
                sgAInput1.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                sgAInput1.Rows[i].Cells[1].Style.Padding = new Padding(3, 0, 0, 0);

            }

        }
        private void UpdateDetailInformation()
        {
            int i;
            String Temp;
            String GridValue = "";
            TfpAnalogInput AnalogInput;

            if (sgAInput1.RowCount != cDEF.Run.Analog.AnalogCount)
                sgAInput1.RowCount = cDEF.Run.Analog.AnalogCount;

            for (i = 0; i < cDEF.Run.Analog.AnalogCount; i++)
            {
                AnalogInput = cDEF.Run.Analog.AnalogItems[i];
                Temp = (i).ToString();

                if (sgAInput1.Rows[i].Cells[0].Value != null)
                    GridValue = sgAInput1.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    sgAInput1.Rows[i].Cells[0].Value = Temp;

                Temp = AnalogInput.Text;
                if (sgAInput1.Rows[i].Cells[1].Value != null)
                    GridValue = sgAInput1.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    sgAInput1.Rows[i].Cells[1].Value = Temp;
                sgAInput1.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                sgAInput1.Rows[i].Cells[1].Style.Padding = new Padding(3, 0, 0, 0);
            }
        }
        private void GridDblClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row;
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
                        Temp = cDEF.Run.Analog.AnalogItems[Row].Text;
                        if (cDEF.fTextEdit.TextEdit("ANALOG INPUT DESCRIPTION - #" + (Row).ToString(), ref Temp, "\\/:*?\"<>|"))
                        {
                            cDEF.Run.Analog.AnalogItems[Row].Text = Temp;
                            cDEF.Run.Analog.Save(cPath.FILE_ANALOG);
                        }
                    }
                    break;
            }
            UpdateDetailInformation();
        }
        private void UpdateInformation()
        {
	        if(!Visible)
		        return;

            if (cDEF.Run.Analog == null)
                return;

	        int i;
	        String Temp = "";
	        
            for (i = 0; i < cDEF.Run.Analog.AnalogCount; i++)
            {
                Temp = Math.Round(cDEF.Run.Analog.AnalogInput[i],3).ToString();
                sgAInput1.Rows[i].Cells[2].Value = Temp;
                sgAInput1.Rows[i].Cells[2].Style.BackColor = Color.White;
                sgAInput1.Rows[i].Cells[2].Style.ForeColor = Color.RoyalBlue;
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
            lbTitle.Text = cDEF.Lang.Trans("ANALOG INPUT LIST");
        }
        private void sgAInput1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

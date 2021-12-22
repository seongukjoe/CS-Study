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
    public partial class FrmPageConfigDigital : Form
    {
        private int FSelected = 0;

        public FrmPageConfigDigital()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FrmPageConfigDigital_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
            FSelected = 0;
            int i;
            String GridValue = "";
            String Temp;
            TfpDigitalItem DigitalItem;

            gridStatus.RowCount = 2;
            grid.RowTemplate.Height = 30;
            gridStatus.RowTemplate.Height = 30;

            gridStatus.Rows[0].Cells[0].Value = "LOW";
            gridStatus.Rows[1].Cells[0].Value = "HIGH";

            if (grid.RowCount != cDEF.Run.Digital.Count + 1)
                grid.RowCount = cDEF.Run.Digital.Count;


            for (i = 0; i < cDEF.Run.Digital.Count; i++)
            {
                DigitalItem = cDEF.Run.Digital.Items[i];
                Temp = i.ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;

                switch(DigitalItem.Style)
                {
                    case TfpDigitalStyle.fdsInput:
                        Temp = "INPUT";
                        break;
                    case TfpDigitalStyle.fdsOutput:
                        Temp = "OUTPUT";
                        break;
                    case TfpDigitalStyle.fdsInputMLII:
                        Temp = "INPUT MLII";
                        break;
                    case TfpDigitalStyle.fdsOutputMLII:
                        Temp = "OUTPUT MLII";
                        break;
                    case TfpDigitalStyle.fdsInOut32P:
                        Temp = "IN/OUT";
                        break;
                }


                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[1].Value = Temp;

                Temp = DigitalItem.Key.ToUpper();
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;

                Temp = DigitalItem.No.ToString();
                if (grid.Rows[i].Cells[3].Value != null)
                    GridValue = grid.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[3].Value = Temp;
            }
 
            UpdateDetailInformation();
        }
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void gridStatusDblClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row = 0;

            Col = gridStatus.CurrentCell.ColumnIndex;
            Row = gridStatus.CurrentCell.RowIndex;

            if (Col != 0)
            {
                TfpDigitalItem DigitalItem = cDEF.Run.Digital.Items[FSelected];
                if (DigitalItem.Style == TfpDigitalStyle.fdsOutput)
                {
                    int Index = (Row) * 16 + (Col - 1);
                    DigitalItem.SetBit(Index, !DigitalItem.GetBit(Index));
                }
            }
        }
        void ActionClick(object sender, EventArgs e)
        {
	        TfpDigitalItem DigitalItem = cDEF.Run.Digital.Items[FSelected];

            switch (Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        DigitalItem.Active = true;
			        break;
		        case 1:
			        DigitalItem.Active = false;
			        break;
	        }
        }
        void ConfigurationClick(object sender, EventArgs e)
        {
	        int i;
            String Items = "";
	        TfpDigitalItem DigitalItem;

	        switch(Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        for(i = 0; i < cDEF.Run.Digital.Count; i ++)
			        {
				        if(i > 0)
					        Items += ",";
				        Items += cDEF.Run.Digital.Items[i].Key.ToUpper()+ "|" + (i + 1).ToString();
			        }
			        if(XModuleMain.frmBox.SelectBox("INDEX", Items, ref FSelected) == DialogResult.No)
				        break;
			        UpdateDetailInformation();
			        break;
		        case 1:
			        DigitalItem = cDEF.Run.Digital.Items[FSelected];
			        i = DigitalItem.No;
			        if(!XModuleMain.frmBox.fpIntegerEdit("BOARD NO.",ref i, "", 0, 99))
				        return;
			        DigitalItem.No = i;
                    cDEF.Run.Digital.Save(cPath.FILE_DIGITAL);
			        UpdateDetailInformation();
			        break;
	        }
        }


        private void UpdateDetailInformation()
        {
            int i;
            String GridValue = "";
            String Temp;
            String StyleTemp= String.Empty;
            TfpDigitalItem DigitalItem;

            for (i = 0; i < cDEF.Run.Digital.Count; i++)
            {
                DigitalItem = cDEF.Run.Digital.Items[i];
                Temp = i.ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;

                switch (DigitalItem.Style)
                {
                    case TfpDigitalStyle.fdsInput:
                        StyleTemp = "INPUT";
                        break;
                    case TfpDigitalStyle.fdsOutput:
                        StyleTemp = "OUTPUT";
                        break;
                    case TfpDigitalStyle.fdsInputMLII:
                        StyleTemp = "INPUT MLII";
                        break;
                    case TfpDigitalStyle.fdsOutputMLII:
                        StyleTemp = "OUTPUT MLII";
                        break;
                    case TfpDigitalStyle.fdsInOut32P:
                        StyleTemp = "IN/OUT";
                        break;
                }
                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != StyleTemp)
                    grid.Rows[i].Cells[1].Value = StyleTemp;

                Temp = DigitalItem.Key.ToUpper();
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;

                Temp = DigitalItem.No.ToString();
                if (grid.Rows[i].Cells[3].Value != null)
                    GridValue = grid.Rows[i].Cells[3].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[3].Value = Temp;
            }

	        DigitalItem = cDEF.Run.Digital.Items[FSelected];

	        lbIndexValue.Text = (FSelected + 1).ToString();
	        lbDescriptionValue.Text = DigitalItem.Key.ToUpper();
            switch (DigitalItem.Style)
            {
                case TfpDigitalStyle.fdsInput:
                    StyleTemp = "INPUT";
                    break;
                case TfpDigitalStyle.fdsOutput:
                    StyleTemp = "OUTPUT";
                    break;
                case TfpDigitalStyle.fdsInputMLII:
                    StyleTemp = "INPUT MLII";
                    break;
                case TfpDigitalStyle.fdsOutputMLII:
                    StyleTemp = "OUTPUT MLII";
                    break;
                case TfpDigitalStyle.fdsInOut32P:
                    StyleTemp = "IN/OUT";
                    break;
            }
            lbStyleValue.Text = StyleTemp;
	        lbNoValue.Text = DigitalItem.No.ToString();
            gridStatus.Columns[0].HeaderText = (cDEF.Run.Digital.Items[FSelected].Style == TfpDigitalStyle.fdsInput) ? "INPUT" : "OUTPUT";
        }
        public void UpdateInformation()
        {
	        if(!Visible)
		        return;

	        int i;
	        String Temp;
            String GridValue = "";
	        TfpDigitalItem DigitalItem;

	        for(i = 0; i < cDEF.Run.Digital.Count; i ++)
	        {
                DigitalItem = cDEF.Run.Digital.Items[i];

		         Temp = (DigitalItem.Active) ? "READY" : "NONE";
                 if (grid.Rows[i].Cells[4].Value != null)
                     GridValue = grid.Rows[i].Cells[4].Value.ToString();
                 if (GridValue != Temp)
                     grid.Rows[i].Cells[4].Value = Temp;
	        }
	        DigitalItem = cDEF.Run.Digital.Items[FSelected];
	        for(i = 0; i < 32; i ++)
	        {
		        if(DigitalItem.Style == TfpDigitalStyle.fdsInput)
                    Temp = (DigitalItem.GetBit(i)) ? "ION" : "OFF";
		        else
                    Temp = (DigitalItem.GetBit(i)) ? "OON" : "OFF";

                if (gridStatus.Rows[i / 16].Cells[1 + i % 16].Value != null)
                    GridValue = gridStatus.Rows[i / 16].Cells[1 + i % 16].Value.ToString();
                if (GridValue != Temp)
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Value = Temp;
     
                if (Temp == "ION")
                {
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Style.BackColor = Color.Blue;
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Style.ForeColor = Color.White;
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Value = "ON";
                }
                else if (Temp == "OON")
                {
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Style.BackColor = Color.Red;
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Style.ForeColor = Color.White;
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Value = "ON";
                }
                else
                {
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Style.BackColor = Color.White;
                    gridStatus.Rows[i / 16].Cells[1 + i % 16].Style.ForeColor = Color.Black;
                }
	        }
	        btnNo.Enabled = !DigitalItem.Active;
	        btnEnable.Enabled = !DigitalItem.Active;
	        btnDisable.Enabled = DigitalItem.Active;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            UpdateInformation();
            timer1.Enabled = true;
        }
        public void ChangeLanguage()
        {
            btnNo.Text = cDEF.Lang.Trans("Set");
            btnEnable.Text = cDEF.Lang.Trans("ENABLE");
            btnDisable.Text = cDEF.Lang.Trans("DISABLE");

        }
    }
}

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

namespace XModule.Forms.FormHistory
{
    public partial class FrmPageHistoryList : Form
    {
        public FrmPageHistoryList()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FormPageHistoryList_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
        }

        private void FormPageHistoryList_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                String Temp = "";
                String GridValue = "";
               // TfpLogListItem ListItem;
                int Count = cDEF.Run.Log.List.Count;

                if (Count == 0)
                    grid.Visible = false;
                else
                    grid.RowCount = Count + 1;

                for(int i = 0; i < Count; i++)
                {
                    Temp = cDEF.Run.Log.List.Items[i].Code.ToString();
                    if (grid.Rows[i].Cells[0].Value != null)
                        GridValue = grid.Rows[i].Cells[0].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[0].Value = Temp;

                    Temp = cDEF.Run.Log.List.Items[i].GetTitle();
                    if (grid.Rows[i].Cells[1].Value != null)
                        GridValue = grid.Rows[i].Cells[1].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[1].Value = Temp;

                }
            }
        }
    }
}

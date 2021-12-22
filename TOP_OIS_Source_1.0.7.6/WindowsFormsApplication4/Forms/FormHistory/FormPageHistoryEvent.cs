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
    public partial class FrmPageHistoryEvent : Form
    {
        public FrmPageHistoryEvent()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);
        }

        private void FormPageHistoryEvent_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
        }

        private void FormPageHistoryEvent_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                String Temp = "";
                String GridValue = "";
                //TfpLogListItem ListItem;
                int Count = cDEF.Run.Log.Event.Count;

                if (Count == 0)
                    grid.Visible = false;
                else
                    grid.RowCount = Count + 1;

                for (int i = 0, j = Count - 1; i < Count; i++, j--)
                {
                    Temp = i.ToString();
                    if (grid.Rows[i].Cells[0].Value != null)
                        GridValue = grid.Rows[i].Cells[0].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[0].Value = Temp;

                    Temp = cDEF.Run.Log.Event.Items[j].Time.ToString("yyyy/MM/dd - HH:mm:ss");
                    if (grid.Rows[i].Cells[1].Value != null)
                        GridValue = grid.Rows[i].Cells[1].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[1].Value = Temp;

                    Temp = cDEF.Run.Log.Event.Items[j].UserName;
                    if (grid.Rows[i].Cells[2].Value != null)
                        GridValue = grid.Rows[i].Cells[2].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[2].Value = Temp;

                    Temp = cDEF.Run.Log.Event.Items[j].Code.ToString();
                    if (grid.Rows[i].Cells[3].Value != null)
                        GridValue = grid.Rows[i].Cells[3].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[3].Value = Temp;

                    //ListItem = cDEF.Run.Log.List.Find(cDEF.Run.Log.Event.Items[j].Code);

                    //if (ListItem == null)
                    //    Temp = "";
                    //else
                    //    Temp = ListItem.GetTitle(cDEF.Run.Log.Event.Items[j].Value);
                    Temp = cDEF.Run.Log.Event.Items[j].Value;

                    if (grid.Rows[i].Cells[4].Value != null)
                        GridValue = grid.Rows[i].Cells[4].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[4].Value = Temp;
                }
            }
        }
        public void ChangeLanguage()
        {
            lbTitle.Text = cDEF.Lang.Trans("EVENT");
        }
    }
}

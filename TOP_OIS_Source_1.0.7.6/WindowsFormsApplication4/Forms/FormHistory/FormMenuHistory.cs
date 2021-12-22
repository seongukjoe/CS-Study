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
    public partial class FrmMenuHistory : Form
    {
        Button oldBtn = null;

        public FrmMenuHistory()
        {
            InitializeComponent();
            SetBounds(0, 0, 130, 995);
        }

        private void FormMenuHistory_Load(object sender, EventArgs e)
        {
            Left = 0;//cDEF.frmMain.Width - this.Width;
            Top = cSystem.formPageTop;
            Visible = false;
            btnAlarm.BackColor = Color.White;
            oldBtn = btnAlarm;
        }

        private void FormMenuHistory_MouseDown(object sender, MouseEventArgs e)
        {
            Button Btn = (sender as Button);
            String FTemp = cDEF.eLoginLevelBuffer.ToString();
            int Tag = Convert.ToInt32(Btn.Tag);
        }

        private void btnDownChange()
        {
            if (oldBtn == null)
                return;
           
            switch (Convert.ToInt32(oldBtn.Tag))
            {
                case 0:
                    cDEF.frmPageHistoryAlarm.Visible = false;
                    break;

                case 1:
                    cDEF.frmPageHistoryWarning.Visible = false;
                    break;

                case 2:
                    cDEF.frmPageHistoryEvent.Visible = false;
                    break;

                case 3:
                    cDEF.frmPageHistoryData.Visible = false;
                    break;

                case 4:
                    
                    cDEF.frmPageHistoryList.Visible = false;
                    break;

                case 5:
                    //cDEF.frmPageRecipeStage.Visible = false;
                    break;
            }
            

        }

        private void FormMenuHistory_MouseUp(object sender, MouseEventArgs e)
        {
            Button Btn = (sender as Button);
            if (oldBtn == Btn)
                return;
            Btn.BackColor = Color.LightSkyBlue;
            oldBtn.BackColor = Color.White;
            btnDownChange();
            oldBtn = Btn;
            bool visible = false;
            switch (Convert.ToInt32(Btn.Tag))
            {
                case 0:
                    cDEF.frmPageHistoryAlarm.Visible = true;
                    break;

                case 1:
                    cDEF.frmPageHistoryWarning.Visible = true;
                    break;

                case 2:
                    cDEF.frmPageHistoryEvent.Visible = true;
                    break;

                case 3:
                    cDEF.frmPageHistoryData.Visible = true;
                    break;

                case 4:
                    //visible = true;
                    cDEF.frmPageHistoryList.Visible = true;
                    break;

                case 5:
                    //cDEF.frmPageRecipeStage.Visible = true;
                    break;
            }
            btnInsert.Visible = visible;
            btnModify.Visible = visible;
            btnDelete.Visible = visible;
        }

        private void FormMenuHistory_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (btnAlarm.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageHistoryAlarm.Visible = true;

                else if (btnWarning.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageHistoryWarning.Visible = true;

                else if (btnEvent.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageHistoryEvent.Visible = true;

                else if (btnData.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageHistoryData.Visible = true;

                else if (btnMessageEdit.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageHistoryList.Visible = true;
                else
                    cDEF.frmPageHistoryAlarm.Visible = true;

            }
            else
            {
                cDEF.frmPageHistoryAlarm.Visible = false;
                cDEF.frmPageHistoryWarning.Visible = false;
                cDEF.frmPageHistoryEvent.Visible = false;
                cDEF.frmPageHistoryData.Visible = false;
                cDEF.frmPageHistoryList.Visible = false;
            }
        }

        public void ChnageLanguage()
        {
            btnAlarm.Text = cDEF.Lang.Trans("ALARM");
            btnWarning.Text = cDEF.Lang.Trans("WARNING");
            btnEvent.Text = cDEF.Lang.Trans("EVENT");
            btnData.Text = cDEF.Lang.Trans("DATA");
        }
        private void EditingClick(object sender, EventArgs e)
        {

        }
    }
}

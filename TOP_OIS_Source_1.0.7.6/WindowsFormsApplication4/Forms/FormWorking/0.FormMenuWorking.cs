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

namespace XModule.Forms.FormWorking
{
    public partial class FrmMenuWorking : TFrame
    {
        static int show;
        Button oldBtn = null;
        public FrmMenuWorking()
        {
            InitializeComponent();
            SetBounds(0, 0, 130, 995);
        }

        private void FrmMenuWorking_Load(object sender, EventArgs e)
        {
            Left = 0;//cDEF.frmMain.Width - this.Width;
            Top = cSystem.formPageTop;
            Visible = false;
            btnVCMLoader.BackColor = Color.LightSkyBlue;
            oldBtn = btnVCMLoader;
        }
        protected override void UpdateInformation()
        {
            if (!Visible)
                return;

            if (show++ >= 5)
            {
                this.Invoke(new Action(delegate ()
                {
                    switch (cDEF.eLoginLevel)
                    {

                        case (eLogLevel)0: // Operator
                            if (btnVCMLoader.Visible == true)
                                btnVCMLoader.Visible = false;
                            break;

                        case (eLogLevel)1: // Maintenance
                            if (btnVCMLoader.Visible == false)
                                btnVCMLoader.Visible = true;
                            break;

                        case (eLogLevel)2: // Engineer
                            if (btnVCMLoader.Visible == false)
                                btnVCMLoader.Visible = true;
                            break;

                        case (eLogLevel)3: // Admin
                            if (btnVCMLoader.Visible == false)
                                btnVCMLoader.Visible = true;
                            break;

                        default:
                            break;

                    }

                }));
                show = 0;
            }
        }
        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            Button Btn = (sender as Button);
            String FTemp = cDEF.eLoginLevelBuffer.ToString();
            int Tag = Convert.ToInt32(Btn.Tag);
        }

        private void btnDownChange(bool change)
        {
            if (oldBtn == null)
                return;


            switch (Convert.ToInt32(oldBtn.Tag))
            {
                case 0:
                    cDEF.frmPageWorkVCMLoader.Visible = false;
                    break;
                case 1:
                    cDEF.frmPageWorkVCMPicker.Visible = false;
                    break;
                case 2:
                    cDEF.frmPageWorkLensLoader.Visible = false;
                    break;
                case 3:
                    cDEF.frmPageWorkLensPicker.Visible = false;
                    break;
                case 4:
                    cDEF.frmPageWorkIndex.Visible = false;
                    break;
                case 5:
                    cDEF.frmPageWorkUnloaderPicker.Visible = false;
                    break;
                case 6:
                    cDEF.frmPageWorkBonder.Visible = false;
                    break;
                case 7:
                    cDEF.frmPageWorkUnloader.Visible = false;
                    break;
                case 8:
                    cDEF.frmPageWorkCuring.Visible = false;
                    break;

            }
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            Button Btn = (sender as Button);
            if (oldBtn == Btn)
                return;
            Btn.BackColor = Color.LightSkyBlue;
            oldBtn.BackColor = Color.WhiteSmoke;
            bool change = false;
            if (Convert.ToInt32(Btn.Tag) > 3)
                change = true;
            btnDownChange(change);
            oldBtn = Btn;
            switch (Convert.ToInt32(Btn.Tag))
            {
                case 0:
                    cDEF.frmPageWorkVCMLoader.Visible = true;
                    break;
                case 1:
                    cDEF.frmPageWorkVCMPicker.Visible = true;
                    break;
                case 2:
                    cDEF.frmPageWorkLensLoader.Visible = true;
                    break;
                case 3:
                    cDEF.frmPageWorkLensPicker.Visible = true;
                    break;
                case 4:
                    cDEF.frmPageWorkIndex.Visible = true;
                    break;
                case 5:
                    cDEF.frmPageWorkUnloaderPicker.Visible = true;
                    break;
                case 6:
                    cDEF.frmPageWorkBonder.Visible = true;
                    break;
                case 7:
                    cDEF.frmPageWorkUnloader.Visible = true;
                    break;
                case 8:
                    cDEF.frmPageWorkCuring.Visible = true;
                    break;
            }
        }

        private void FrmMenuWorking_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (btnVCMLoader.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkVCMLoader.Visible = true;
                else if (btnVCMPicker.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkVCMPicker.Visible = true;
                else if (btnLensLoader.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkLensLoader.Visible = true;
                else if (btnLensPicker.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkLensPicker.Visible = true;
                else if (btnIndex.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkIndex.Visible = true;
                else if (btnBonder.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkBonder.Visible = true;
                else if (btnCuring.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkCuring.Visible = true;
                else if (btnUnloader.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkUnloader.Visible = true;
                else if (btnUnloaderPicker.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageWorkUnloaderPicker.Visible = true;

                else
                    cDEF.frmPageWorkVCMLoader.Visible = true;


            }
            else
            {
                cDEF.frmPageWorkVCMLoader.Visible = false;
                cDEF.frmPageWorkVCMPicker.Visible = false;
                cDEF.frmPageWorkLensLoader.Visible = false;
                cDEF.frmPageWorkLensPicker.Visible = false;
                cDEF.frmPageWorkIndex.Visible = false;
                cDEF.frmPageWorkBonder.Visible = false;
                cDEF.frmPageWorkCuring.Visible = false;
                cDEF.frmPageWorkUnloader.Visible = false;
                cDEF.frmPageWorkUnloaderPicker.Visible = false;
            }
        }

        public void ChangeLanguage()
        {
            btnVCMLoader.Text = cDEF.Lang.Trans("VCM LOADER");
            btnVCMPicker.Text = cDEF.Lang.Trans("VCM PICKER");
            btnLensLoader.Text = cDEF.Lang.Trans("LENS LOADER");
            btnLensPicker.Text = cDEF.Lang.Trans("LENS PICKER");
            btnIndex.Text = cDEF.Lang.Trans("INDEX");
            btnBonder.Text = cDEF.Lang.Trans("BONDER");
            btnCuring.Text = cDEF.Lang.Trans("CURING");
            btnUnloader.Text = cDEF.Lang.Trans("UNLOADER");
            btnUnloaderPicker.Text = cDEF.Lang.Trans("UNLOAD PICKER");
        }
    }
}

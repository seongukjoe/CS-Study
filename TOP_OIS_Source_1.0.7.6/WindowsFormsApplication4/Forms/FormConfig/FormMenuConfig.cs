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
using XModule.Forms;

namespace XModule.Forms.FormConfig
{
    public partial class FrmMenuConfig : Form
    {
        Button oldBtn = null;

        public FrmMenuConfig()
        {
            InitializeComponent();
            SetBounds(0, 0, 130, 998);

        }

        private void FrmMenuConfig_Load(object sender, EventArgs e)
        {
            Left = 0;//cDEF.frmMain.Width - this.Width;
            Top = cSystem.formPageTop;
            Visible = false;
            btnMotion.BackColor = Color.White;
            oldBtn = btnMotion;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0xb019:
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            Button Btn = (sender as Button);
            String FTemp = cDEF.eLoginLevelBuffer.ToString();
            int Tag = Convert.ToInt32(Btn.Tag);  
        }

        private void btnUser_Click(object sender, EventArgs e)
        {      
            cDEF.frmLogin.ShowDialog();
        }
        private void btnDownChange()
        {
            if (oldBtn == null)
                return;
            switch (Convert.ToInt32(oldBtn.Tag))
            {
                case 0:
                    cDEF.frmPageConfigGeneral.Visible = false;
                    break;

                case 1:
                    cDEF.frmPageConfigMotion.Visible = false;
                    break;

                case 2:
                    cDEF.frmPageConfigDigital.Visible = false;
                    break;

                case 3:
                    cDEF.frmPageConfigDigitalLink.Visible = false;
                    break;

                case 4:
                    cDEF.frmPageConfigAnalog.Visible = false;
                    break;

                case 5:
                    cDEF.frmPageConfigAnalogLink.Visible = false;
                    break;

                case 6:
                    cDEF.frmPageConfigCylinder.Visible = false;
                    break;

                case 7:
                    cDEF.frmPageConfigLamp.Visible = false;
                    break;

                case 8:
                    cDEF.frmPageConfigSwitch.Visible = false;
                    break;
            }

        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            Button Btn = (sender as Button);
            if(oldBtn == Btn)
              return;
            Btn.BackColor = Color.LightSkyBlue;
            oldBtn.BackColor = Color.White;    
            btnDownChange();
            oldBtn = Btn;
            switch (Convert.ToInt32(Btn.Tag))
            {
                case 0:
                    cDEF.frmPageConfigGeneral.Visible = true;
                    break;

                case 1:
                    cDEF.frmPageConfigMotion.Visible = true;
                    break;

                case 2:
                    cDEF.frmPageConfigDigital.Visible = true;
                    break;

                case 3:
                    cDEF.frmPageConfigDigitalLink.Visible = true;
                    break;

                case 4:
                    cDEF.frmPageConfigAnalog.Visible = true;
                    break;

                case 5:
                    cDEF.frmPageConfigAnalogLink.Visible = true;
                    break;

                case 6:
                    cDEF.frmPageConfigCylinder.Visible = true;
                    break;

                case 7:
                    cDEF.frmPageConfigLamp.Visible = true;
                    break;

                case 8:
                    cDEF.frmPageConfigSwitch.Visible = true;
                    break;

                
            }
        }

        private void FrmMenuConfig_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (btnMotion.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigMotion.Visible = true;

                else if (btnDigital.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigDigital.Visible = true;

                else if (btnDigitalLink.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigDigitalLink.Visible = true;

                else if (btnAnalog.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigAnalog.Visible = true;

                else if (btnAnalogLink.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigAnalogLink.Visible = true;

                else if (btnCylinder.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigCylinder.Visible = true;

                else if (btnLamp.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigLamp.Visible = true;

                else if (btnSwitch.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigSwitch.Visible = true;

                else if (btnGeneral.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageConfigGeneral.Visible = true;

                else 
                    cDEF.frmPageConfigMotion.Visible = true;

             
                //Form 추가 필요.

                    //else if (btnLightSource.BackColor == Color.White)


                    //else if (btnGeneral.BackColor == Color.White)


                    //else if (btnBarCode.BackColor == Color.White)

            }
            else
            {
                cDEF.frmPageConfigMotion.Visible = false;
                cDEF.frmPageConfigDigital.Visible = false;
                cDEF.frmPageConfigDigitalLink.Visible = false;
                cDEF.frmPageConfigAnalog.Visible = false;
                cDEF.frmPageConfigAnalogLink.Visible = false;
                cDEF.frmPageConfigCylinder.Visible = false;
                cDEF.frmPageConfigLamp.Visible = false;
                cDEF.frmPageConfigSwitch.Visible = false;
                cDEF.frmPageConfigGeneral.Visible = false;
            }
        }

        public void ChangeLanguage()
        {
            btnGeneral.Text = cDEF.Lang.Trans("DEVICE");
            btnMotion.Text = cDEF.Lang.Trans("MOTION");
            btnDigital.Text = cDEF.Lang.Trans("DIGITAL");
            btnDigitalLink.Text = cDEF.Lang.Trans("IO");
            btnAnalog.Text = cDEF.Lang.Trans("ANALOG");
            btnCylinder.Text = cDEF.Lang.Trans("CYLINDER");
            btnLamp.Text = cDEF.Lang.Trans("LAMP");
            btnSwitch.Text = cDEF.Lang.Trans("SWITCH");

        }
        private void btnAnalog_Click(object sender, EventArgs e)
        {

        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {

        }

        private void btnLamp_Click(object sender, EventArgs e)
        {

        }

        private void btnCylinder_Click(object sender, EventArgs e)
        {

        }
    }
}

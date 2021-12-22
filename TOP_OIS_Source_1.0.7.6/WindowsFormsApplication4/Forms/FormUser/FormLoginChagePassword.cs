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

namespace XModule.Forms.FormUser
{
    public partial class FrmLoginChagePassword : Form
    {
        Control ctlSender;
        public FrmLoginChagePassword()
        {
            InitializeComponent();
        }
        public void Ini()
        {
            if (cDEF.eLoginLevelBuffer == eLogLevel.MAINTENANCE) lblPassWord.Text = "[ENGINEER] PASSWORD CHANGE";
            else if (cDEF.eLoginLevelBuffer == eLogLevel.ADMIN) lblPassWord.Text = "[ADMIN] PASSWORD CHANGE";

            txtOldPassWord.Text = "";
            txtNewPassWord.Text = "";
            txtCheckNewPassWord.Text = "";
        }

        private void swPassword_Click(object sender, EventArgs e)
        {
            ctlSender = (Button)sender;
            string sOldPass = string.Empty;
            string sNewPass = string.Empty;
            string sCheckPass = string.Empty;
            string sOldChkPass = string.Empty;

            try
            {
                if (ctlSender.Name == "btn_Save")
                {
                    sOldPass = txtOldPassWord.Text.Trim();
                    sNewPass = txtNewPassWord.Text.Trim();
                    sCheckPass = txtCheckNewPassWord.Text.Trim();

                    if (cDEF.eLoginLevelBuffer == eLogLevel.MAINTENANCE) sOldChkPass = cDEF.stPassWord.ENG;
                    else if (cDEF.eLoginLevelBuffer == eLogLevel.ADMIN) sOldChkPass = cDEF.stPassWord.SUP;
                    else return;

                    if (sOldPass == sOldChkPass)
                    {
                        if (sNewPass == sCheckPass)
                        {
                            if (cDEF.eLoginLevelBuffer == eLogLevel.MAINTENANCE) cDEF.stPassWord.ENG = sNewPass;
                            else if (cDEF.eLoginLevelBuffer == eLogLevel.ADMIN) cDEF.stPassWord.SUP = sNewPass;
                            cUtilLocal.WRLoginPassword();
                            this.Hide();
                            MessageBox.Show("PASSWORD CHANGED SUCCESSFULLY !");
                        }
                        else
                        {
                            MessageBox.Show("New Password Fail !" + etc.CrLf);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Old Password Fail !" + etc.CrLf + "PASSWORD FAIL !");
                    }
                }
                else if (ctlSender.Name == "btn_Exit") this.Hide();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}

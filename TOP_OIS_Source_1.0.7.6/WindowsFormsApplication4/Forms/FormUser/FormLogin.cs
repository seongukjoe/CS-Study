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
    public partial class FrmLogin : Form
    {
        Control ctlSender;

        public FrmLogin()
        {
            InitializeComponent();
            SetBounds(0, 0, 600, 600);
        }

        public void Ini()
        {
            cDEF.eLoginLevelBuffer = eLogLevel.NONE;
            txtPassWord.Text = "";
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Left = cDEF.frmMain.Left + (cDEF.frmMain.Width - this.Width) / 2;
            Top = cDEF.frmMain.Top + (cDEF.frmMain.Height - this.Height) / 2;
            Visible = false;
            txtPassWord.Text = "";

            if (cDEF.eLoginLevel == eLogLevel.SUPER)
            {
                cDEF.eLoginLevelBuffer = eLogLevel.ADMIN;
                btn_Master.Text = "MASTER";
            }
            cDEF.eLoginLevel = eLogLevel.NONE;

        }
        private void UpdateInformation()
        {
            if (!Visible)
                return;

            btn_Opp.BackColor = cDEF.eLoginLevelBuffer == eLogLevel.OPERATOR ? Color.LightGoldenrodYellow : Color.White;
            btn_Maint.BackColor = cDEF.eLoginLevelBuffer == eLogLevel.MAINTENANCE ? Color.LightGoldenrodYellow : Color.White;
            btn_Master.BackColor = (cDEF.eLoginLevelBuffer == eLogLevel.ADMIN || cDEF.eLoginLevelBuffer == eLogLevel.SUPER) ? Color.LightGoldenrodYellow : Color.White;

            if (cDEF.eLoginLevelBuffer == eLogLevel.OPERATOR || cDEF.eLoginLevelBuffer == eLogLevel.MAINTENANCE || cDEF.eLoginLevelBuffer == eLogLevel.ADMIN) BTN_PasswordChenge.Enabled = true;
            else BTN_PasswordChenge.Enabled = false;

            if (cDEF.eLoginLevelBuffer == eLogLevel.MAINTENANCE || cDEF.eLoginLevelBuffer == eLogLevel.ADMIN || cDEF.eLoginLevelBuffer == eLogLevel.SUPER) BTN_PasswordChenge.Enabled = true;
            else BTN_PasswordChenge.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            UpdateInformation();
            timer1.Enabled = true;


        }

        private void swLoginLevel_Click(object sender, EventArgs e)
        {
            ctlSender = (Button)sender;
            try
            {
                if (ctlSender.Name == "btn_Opp")
                {
                    cDEF.eLoginLevelBuffer = eLogLevel.OPERATOR;
                    btn_Master.Text = "MASTER";
                }
                else if (ctlSender.Name == "btn_Maint")
                {
                    cDEF.eLoginLevelBuffer = eLogLevel.MAINTENANCE;
                    btn_Master.Text = "MASTER";
                }
                else if (ctlSender.Name == "btn_Master"
                        && Control.ModifierKeys == Keys.Control
                        && cDEF.eLoginLevelBuffer != eLogLevel.SUPER)
                {
                    cDEF.eLoginLevelBuffer = eLogLevel.SUPER;
                    btn_Master.Text = "SUPER";
                }
                else if (ctlSender.Name == "btn_Master"
                        && Control.ModifierKeys == Keys.Control
                        && cDEF.eLoginLevelBuffer == eLogLevel.SUPER)
                {
                    cDEF.eLoginLevelBuffer = eLogLevel.ADMIN;
                    btn_Master.Text = "MASTER";
                }
                else if (ctlSender.Name == "btn_Master")
                {
                    cDEF.eLoginLevelBuffer = eLogLevel.ADMIN;
                    btn_Master.Text = "MASTER";
                }
                else cDEF.eLoginLevelBuffer = eLogLevel.NONE;     
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOGIN LEVEL SELECT FAIL " + ex.ToString());
            }
        }

        private void swLogin_Clcik(object sender, EventArgs e)
        {
            ctlSender = (Button)sender;
            string sPassWord = string.Empty;

            try
            {
                if (ctlSender.Name == "BTN_LOGIN")
                {
                    sPassWord = txtPassWord.Text.Trim();
                    if (cDEF.eLoginLevelBuffer == eLogLevel.OPERATOR)
                    {
                        cDEF.eLoginLevel = cDEF.eLoginLevelBuffer;
                        Close();
                    }
                    else if (cDEF.eLoginLevelBuffer == eLogLevel.MAINTENANCE)
                    {
                        if (cDEF.stPassWord.ENG == sPassWord)
                        {
                            cDEF.eLoginLevel = cDEF.eLoginLevelBuffer;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Enter enginner mode password again !" + etc.CrLf + "PASSWORD FAIL !");
                        }
                    }
                    else if (cDEF.eLoginLevelBuffer == eLogLevel.ADMIN)
                    {
                        if (sPassWord == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            cDEF.eLoginLevel = cDEF.eLoginLevelBuffer;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Enter Master mode password aggin !" + etc.CrLf + "PASSWORD FAIL !");
                        }
                    }
                    else if (cDEF.eLoginLevelBuffer == eLogLevel.SUPER)
                    {
                        if (cDEF.stPassWord.SUP == sPassWord)
                        {
                            cDEF.eLoginLevel = cDEF.eLoginLevelBuffer;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Enter Master mode password aggin !" + etc.CrLf + "PASSWORD FAIL !");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select login !");
                        Close();
                    }
                }
                else if (ctlSender.Name == "BTN_PasswordChenge")
                {
                    if (cDEF.eLoginLevelBuffer == eLogLevel.OPERATOR || cDEF.eLoginLevelBuffer == eLogLevel.MAINTENANCE || cDEF.eLoginLevelBuffer == eLogLevel.ADMIN)
                    {
                        cDEF.frmLoginChagePassword.Show();
                        cDEF.frmLoginChagePassword.BringToFront();
                        cDEF.frmLoginChagePassword.Ini();
                    }
                    else
                    {
                        MessageBox.Show("Select login !");
                        Close();
                    }
                        
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOGIN FAIL " + ex.ToString());
            }
        }
        private void FrmLogin_Click(object sender, EventArgs e)
        {
            cDEF.eLoginLevelBuffer = eLogLevel.NONE;
        }
    }
}

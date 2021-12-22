using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using XModule.Standard;
using Owf.Controls;
using XModule.Forms.FormOperation;

namespace XModule.Forms
{
    public partial class FrmTitle : TFrame
    {
        int FTick = 0;
       // int FLength = 0;
       // String FTemp = "";
        FormOperation.FormInformation FormEq = new FormInformation(); 

        public FrmTitle() : base ()
        {
            InitializeComponent();  
        }

        private void FrmTitle_Load(object sender, EventArgs e) 
        {
            Left = 0;
            Top = 0;
            Visible = false;
            SetBounds(0, 0, 920, 60);
        }

        int sDelay = 0;
        protected override void UpdateInformation()
        {
            if (!Visible)
                return;
            if ((500 / 50) >= FTick)
            {

                this.Invoke(new Action(delegate()
                {
                    string strSpeed = string.Empty;
                    int mtrspd = (int)cDEF.Run.Motion.FPower;
                    if (mtrspd != 100)
                        strSpeed = string.Format($" Power ({mtrspd}%)");

                    lbModeValue.Text = cDEF.Run.DetailModeToString()+ strSpeed;
                    lbRecipeName.Text = cDEF.Work.Project.FileName;

                    if (lblSimual.Visible != cDEF.Run.Motion.Simul)
                        lblSimual.Visible = cDEF.Run.Motion.Simul;
                }));
                FTick = 0;
            }
            else
                FTick++;
        }
        private void SamsungLogo_Click(object sender, EventArgs e)
        {

            if (cDEF.Run.DetailMode == TfpRunningMode.frmRun)
            {
                String FButton = TfpMessageBoxButton.fmbClose.ToString();
                XModuleMain.frmBox.MessageBox("로그인 상태", "설비가 동작중입니다. \r\n설비를 멈추고 로그인을 부탁드립니다.", TfpMessageBoxIcon.fmiInformation, FButton);
                return;
            }

            cDEF.frmLogin.ShowDialog();
            FormEq.ShowDialog();
        }
    }
    
}

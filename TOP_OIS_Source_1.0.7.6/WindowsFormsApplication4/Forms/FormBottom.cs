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


namespace XModule.Forms
{
    public partial class FrmBottom : TFrame
    {
        int FTick = 0;
        int FLength = 0;
        String FTemp = "";

        public FrmBottom() : base ()
        {
            InitializeComponent();
            SetBounds(0, 0, 1920, 22);
        }

        private void FrmTitle_Load(object sender, EventArgs e) 
        {
            Left = 0;
            Top = cDEF.frmMain.Height - this.Height;
            Visible = true;
        }

        protected override void UpdateInformation()
        {
            if (!Visible)
                return;
            if ((500 / 50) >= FTick)
            {

                this.Invoke(new Action(delegate()
                {
                    FLength = 35 - cDEF.eLoginLevelBuffer.ToString().Length;
                    FTemp = "";
                    for (int i = 0; i < FLength; i++)
                        FTemp += " ";
                    lbUserName.Text = FTemp + cDEF.eLoginLevel.ToString();
                    lbTimeValue.Text = DateTime.Now.ToString("                yyyy-MM-dd tt hh:mm:ss");
                    lbFileVersion.Text = cPath.Version;

                    switch(cDEF.Run.SicCheckNo)
                    {
                        case 0: lbSicCheck.Text = "No Lock Key"; lbSicCheck.ForeColor = Color.Red; break;
                        case 1: lbSicCheck.Text = "Lock Key AnotherLock."; lbSicCheck.ForeColor = Color.Red; break;
                        case 2: lbSicCheck.Text = ""; lbSicCheck.ForeColor = Color.Lime; break;

                    }
                    

                }));
                FTick = 0;
            }
            else
                FTick++;
        }
        private void label6_Click(object sender, EventArgs e)
        {
            cDEF.fTaskLog.Hide();
            cDEF.fTaskLog.Show();
        }
    }
}

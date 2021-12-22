using System;
using System.Drawing;
using System.Windows.Forms;
using XModule.Standard;
using XModule.Unit;

namespace XModule.Forms.FormWorking
{
    public partial class FormPagePM : TFrame
    {
        public FormPagePM()
        {
            InitializeComponent();
            SetBounds(0,0, 1920, 995);
        }

        private void FormPageWorkingInterface_Load(object sender, EventArgs e)
        {
            Left = 0;
            Top = cSystem.formPageTop;
            Visible = false;
            
        }

        protected override void UpdateInformation()
        {
            if (!Visible)
                return;

            this.Invoke(new Action(delegate ()
            {
                

            }));
        }

        private void Vacuum_Ondelay_Setting_Click(object sender, EventArgs e)
        {
            
        }

        private void Vacuum_OffDelay_Setting_Click(object sender, EventArgs e)
        {
          
        }

        private void btnDoor_Click(object sender, EventArgs e)
        {
            cDEF.frmDoor.ShowDialog();
        }

        

    }
}

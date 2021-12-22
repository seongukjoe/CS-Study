using System;
using System.Drawing;
using System.Windows.Forms;
using XModule.Standard;

namespace XModule.Forms.FormRecipe
{
    public partial class FrmMenuRecipe : Form
    {
        //Glass.GlassButton oldBtn = null;
        Button oldBtn = null;

        public FrmMenuRecipe()
        {
            InitializeComponent();
            SetBounds(0, 0, 130, 998);
        }

        private void FormMenuRecipe_Load(object sender, EventArgs e)
        {
            Left = 0;// cDEF.frmMain.Width - this.Width;
            Top = cSystem.formPageTop;
            Visible = false;
            btnProject.BackColor = Color.White;
            oldBtn = btnProject;
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            //Glass.GlassButton Btn = (sender as Glass.GlassButton);
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
                    cDEF.frmPageRecipeProject.Visible = false;
                    break;

            }
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            //Glass.GlassButton Btn = (sender as Glass.GlassButton);
            Button Btn = (sender as Button);
            if (oldBtn == Btn)
                return;
            Btn.BackColor = Color.LightSkyBlue;
            oldBtn.BackColor = Color.White;
            btnDownChange();
            oldBtn = Btn;
            switch (Convert.ToInt32(Btn.Tag))
            {
                case 0:
                    cDEF.frmPageRecipeProject.Visible = true;
                    break;
            }
        }

        private void FormMenuRecipe_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (btnProject.BackColor == Color.LightSkyBlue)
                    cDEF.frmPageRecipeProject.Visible = true;
                else
                    cDEF.frmPageRecipeProject.Visible = true;
                

            }
            else
            {
                cDEF.frmPageRecipeProject.Visible = false;
            }
        }
        public void ChangeLanguage()
        {
            btnProject.Text = cDEF.Lang.Trans("PROJECT");
        }
    }
}

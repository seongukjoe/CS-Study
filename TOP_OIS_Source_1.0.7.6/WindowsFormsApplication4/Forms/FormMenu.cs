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

namespace XModule.Forms
{
    public partial class FrmMenu : TFrame
    {
        static int show;
        CheckBox oldBtn = null;
        public FrmMenu()
        {
            InitializeComponent();
            SetBounds(0, 0, 1000, 60);
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            Left = 920;
            Top = 0;
            Visible = true;

            cDEF.frmPageOperation.Visible = true;
            btnOperator.CheckState = CheckState.Checked;
            oldBtn = btnOperator;

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

        protected override void UpdateInformation()
        {
            if (!Visible)
                return;

            if (show++ >= 5)
            {
                this.Invoke(new Action(delegate ()
                {
                    //로그인 레벨 사용 가능한 메뉴 설정
                    switch (cDEF.eLoginLevel)
                    {

                        case (eLogLevel)0: // Operator
                            if (btnLog.Visible == false)
                                btnLog.Visible = true;

                            if(btnWorking.Visible == true)
                                btnWorking.Visible = false;

                            if (btnRecipe.Visible == true)
                                btnRecipe.Visible = false;

                            if (btnSetup.Visible == true)
                                btnSetup.Visible = false;

                            btnLog.Enabled = true;
                            btnOperator.Enabled = true;
                            break;

                        case (eLogLevel)1: // Maintenance
                            if (btnLog.Visible == false)
                                btnLog.Visible = true;

                            if (btnRecipe.Visible == false)
                                btnRecipe.Visible = true;

                            if (btnWorking.Visible == true)
                                btnWorking.Visible = false;

                            if (btnSetup.Visible == true)
                                btnSetup.Visible = false;

                            btnOperator.Enabled = true;
                            btnRecipe.Enabled = true;
                            btnLog.Enabled = true;
                            btnExit.Enabled = true;
                            break;

                        case (eLogLevel)2: // Master
                            if (btnLog.Visible == false)
                                btnLog.Visible = true;

                            if (btnWorking.Visible == false)
                                btnWorking.Visible = true;

                            if (btnRecipe.Visible == false)
                                btnRecipe.Visible = true;

                            if (btnSetup.Visible == true)
                                btnSetup.Visible = false;

                            btnOperator.Enabled = true;
                            btnRecipe.Enabled = true;
                            btnLog.Enabled = true;
                            btnWorking.Enabled = true;
                            btnExit.Enabled = true;
                            break;

                        case (eLogLevel)3: // SUPER
                            if (btnLog.Visible == false)
                                btnLog.Visible = true;

                            if (btnWorking.Visible == false)
                                btnWorking.Visible = true;

                            if (btnRecipe.Visible == false)
                                btnRecipe.Visible = true;

                            if (btnSetup.Visible == false)
                                btnSetup.Visible = true;
                            btnOperator.Enabled = true;
                            btnRecipe.Enabled = true;
                            btnWorking.Enabled = true;
                            btnLog.Enabled = true;
                            btnSetup.Enabled = true;
                            btnExit.Enabled = true;
                            break;


                        default:
                            break;

                    }



                    //Run 모드 일때 PM창 비활성화
                    if (cDEF.Run.DetailMode == TfpRunningMode.frmRun)
                    {
                        btnPMmode.Enabled = false;
                    }
                    else
                        btnPMmode.Enabled = true;


                }));
                show = 0;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            String FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            if (XModuleMain.frmBox.MessageBox("Program Exit", "Do you want to Exit Program?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
            {
                cDEF.Run.LogEvent(90010, "Program Exit");

                cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = false;
                cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = false;
                cDEF.frmMain.Tag = 1;
                cDEF.frmMain.FormMain_FormClosed(this, null);
                btnExit.CheckState = CheckState.Unchecked;
            }
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            //if (cDEF.eLoginLevel == eLogLevel.NONE)
            //{
            //    String FButton = TfpMessageBoxButton.fmbClose.ToString();
            //    XModuleMain.frmBox.MessageBox("로그인 상태", "로그인이 안되었습니다. \r\n로그인 상태 확인 또는 로그인을 부탁드립니다.", TfpMessageBoxIcon.fmiInformation, FButton);
            //    return;
            //}
            
            //CheckBox Btn = (sender as CheckBox);
         
            //int Tag = Convert.ToInt32(Btn.Tag);

            
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
           
            
        }

        private void btnDownChange()
        {
            if (oldBtn == null)
                return;
            switch (Convert.ToInt32(oldBtn.Tag))
            {
                case 0:
                    cDEF.frmPageOperation.Visible = false;
                    break;

                case 2:
                    cDEF.frmMenuWorking.Visible = false;
                    break;

                case 3:
                    cDEF.frmMenuHistory.Visible = false;
                    break;

                case 4:
                    cDEF.frmMenuRecipe.Visible = false;
                    break;

                case 5:
                    cDEF.frmMenuConfig.Visible = false;
                    break;
                case 6:
                    cDEF.frmPagePM.Visible = false;
                    break;

            }
        }
        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (cDEF.frmPagePM.Visible)
                cDEF.frmLogin.ShowDialog();

            if (cDEF.eLoginLevel == eLogLevel.NONE)
            {
                String FButton = TfpMessageBoxButton.fmbClose.ToString();
                XModuleMain.frmBox.MessageBox("Log In Status", "Log In Fail. \r\nCheck Log In Status OR Log In Please.", TfpMessageBoxIcon.fmiInformation, FButton);
                return;
            }

            CheckBox Btn = (sender as CheckBox);
            if (oldBtn == Btn)
                return;
            Btn.CheckState = CheckState.Checked;

            if (Convert.ToInt32(Btn.Tag) == 6 && cDEF.Work.Project.GlobalOption.UseMES)
            {
                int Selected = -1;
                if (XModuleMain.frmBox.SelectBox("PM MODE", "LOAD_DOWN , MODEL_CHANGE , BM_START , MATR_DOWN ", ref Selected) == DialogResult.No)
                    return;

                if (cDEF.Work.Project.GlobalOption.UseMES)
                {
                    cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.SETUP;
                    cDEF.Run.MesStatusMsg = "PM";
                    cDEF.Mes.Send_EquipStatus();
                    cDEF.Mes.Send_PMStatus((Unit.ePMStatus)Selected);
                }
            }
            btnDownChange();
            oldBtn = Btn;
            switch (Convert.ToInt32(Btn.Tag))
            {
                case 0:
                    btnWorking.CheckState = CheckState.Unchecked;
                    btnLog.CheckState = CheckState.Unchecked;
                    btnRecipe.CheckState = CheckState.Unchecked;
                    btnSetup.CheckState = CheckState.Unchecked;
                    btnPMmode.CheckState = CheckState.Unchecked;
                    cDEF.frmPageOperation.Visible = Btn.Checked;
                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        if (cDEF.Run.MESEQPStatus != Unit.eMESEqpStatus.IDLE )
                        {
                            cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.IDLE;
                            cDEF.Run.MesStatusMsg = "AUTO";
                            cDEF.Mes.Send_EquipStatus();
                        }

                    }
                    break;

                case 2:
                    //메뉴 워킹
                    btnOperator.CheckState = CheckState.Unchecked;
                    btnLog.CheckState = CheckState.Unchecked;
                    btnRecipe.CheckState = CheckState.Unchecked;
                    btnSetup.CheckState = CheckState.Unchecked;
                    btnPMmode.CheckState = CheckState.Unchecked;
                    cDEF.frmMenuWorking.Visible = Btn.Checked;
                    //cDEF.frmPageWorkingCamera.Visible = Btn.Checked;
                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        if (cDEF.Run.MESEQPStatus != Unit.eMESEqpStatus.SETUP || cDEF.Run.MesStatusMsg != "STOP")
                        {
                            cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.SETUP;
                            cDEF.Run.MesStatusMsg = "STOP";
                            cDEF.Mes.Send_EquipStatus();
                        }
                       
                    }
                    break;

                case 3:
                    //메뉴 히스토리
                    btnOperator.CheckState = CheckState.Unchecked;
                    btnWorking.CheckState = CheckState.Unchecked;
                    btnRecipe.CheckState = CheckState.Unchecked;
                    btnSetup.CheckState = CheckState.Unchecked;
                    btnPMmode.CheckState = CheckState.Unchecked;
                    cDEF.frmMenuHistory.Visible = Btn.Checked;
                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        if (cDEF.Run.MESEQPStatus != Unit.eMESEqpStatus.SETUP || cDEF.Run.MesStatusMsg != "STOP")
                        {
                            cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.SETUP;
                            cDEF.Run.MesStatusMsg = "STOP";
                            cDEF.Mes.Send_EquipStatus();
                        }

                    }
                    break;

                case 4:
                    //메뉴 레시피
                    btnOperator.CheckState = CheckState.Unchecked;
                    btnWorking.CheckState = CheckState.Unchecked;
                    btnLog.CheckState = CheckState.Unchecked;
                    btnSetup.CheckState = CheckState.Unchecked;
                    btnPMmode.CheckState = CheckState.Unchecked;
                    cDEF.frmMenuRecipe.Visible = Btn.Checked;
                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        if (cDEF.Run.MESEQPStatus != Unit.eMESEqpStatus.SETUP || cDEF.Run.MesStatusMsg != "STOP")
                        {
                            cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.SETUP;
                            cDEF.Run.MesStatusMsg = "STOP";
                            cDEF.Mes.Send_EquipStatus();
                        }

                    }
                    break;

                case 5:
                    //메뉴 설정(Config)
                    btnOperator.CheckState = CheckState.Unchecked;
                    btnWorking.CheckState = CheckState.Unchecked;
                    btnLog.CheckState = CheckState.Unchecked;
                    btnRecipe.CheckState = CheckState.Unchecked;
                    btnPMmode.CheckState = CheckState.Unchecked;
                    cDEF.frmMenuConfig.Visible = Btn.Checked;
                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        if (cDEF.Run.MESEQPStatus != Unit.eMESEqpStatus.SETUP || cDEF.Run.MesStatusMsg != "STOP")
                        {
                            cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.SETUP;
                            cDEF.Run.MesStatusMsg = "STOP";
                            cDEF.Mes.Send_EquipStatus();
                        }

                    }
                    break;

                case 6:

                   

                    btnOperator.CheckState = CheckState.Unchecked;
                    btnWorking.CheckState = CheckState.Unchecked;
                    btnLog.CheckState = CheckState.Unchecked;
                    btnRecipe.CheckState = CheckState.Unchecked;
                    btnSetup.CheckState = CheckState.Unchecked;
                    cDEF.frmPagePM.Visible = btnPMmode.Checked;

                    btnOperator.Enabled = false;
                    btnRecipe.Enabled = false;
                    btnWorking.Enabled = false;
                    btnLog.Enabled = false;
                    btnSetup.Enabled = false;
                    btnExit.Enabled = false;
                    break;
                case 7:
                    btnOperator.CheckState = CheckState.Unchecked;
                    btnWorking.CheckState = CheckState.Unchecked;
                    btnLog.CheckState = CheckState.Unchecked;
                    btnRecipe.CheckState = CheckState.Unchecked;
                    btnSetup.CheckState = CheckState.Unchecked;
                    btnPMmode.CheckState = CheckState.Unchecked;
                    break;
            }
        }
        public void ChangeLanguage()
        {
            btnOperator.Text = cDEF.Lang.Trans("    AUTO");
            btnRecipe.Text = cDEF.Lang.Trans("RECIPE");
            btnWorking.Text = cDEF.Lang.Trans("TEACH");
            btnSetup.Text = cDEF.Lang.Trans("SETUP");
            btnLog.Text = cDEF.Lang.Trans("STATISTICS");
            btnPMmode.Text = cDEF.Lang.Trans("PM");
            btnExit.Text = cDEF.Lang.Trans("EXIT");

        }
    }
}

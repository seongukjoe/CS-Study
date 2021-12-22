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
using XModule.Standard;
using XModule.Forms;
using XModule.Working;
using XModule.Running;
using static XModule.Standard.cUtilLocal;

namespace XModule
{
    public partial class FrmMain : Form
    {
        //object FDatas;

        public FrmMain()
        {
            InitializeComponent();
            
            SetBounds(0, 0, 1920, 1080);

            Screen[] scs = Screen.AllScreens;

            //this.Location = scs[1].Bounds.Location;

            SetBounds(Location.X, Location.Y, 1920, 1080);

        }

        private void Proc_AlarmDefineList()
        {
            cDEF.AlarmDefineList = new AlarmDefin[cDEF.Run.Log.List.Items.Count];
            for (int i = 0; i < cDEF.Run.Log.List.Items.Count; i++)
            {
                TfpLogListItem item = cDEF.Run.Log.List.Items[i];

                AlarmDefin aData = new AlarmDefin();
                aData.Code = item.Code;
                aData.Grade = "A";
                aData.Text = item.Text;

                cDEF.AlarmDefineList[i] = aData;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            

            FormParent();
            FormHide();

            FrmProgress Progress = new FrmProgress();
            Progress.Tag = 0;
   
            if (Progress.ShowDialog() != DialogResult.OK)
            {
                this.Tag = 2; 
            }
            cDEF.frmTitle.Visible = true;
            cDEF.frmMenu.Visible = true;
            cDEF.frmBottom.Visible = true;
            //cDEF.frmMenuOperation.Visible = true;
            cUtilLocal.RDLoginPassword();
            cDEF.frmLogin.TopMost = true;
            cDEF.frmLogin.ShowDialog();
            XModuleMain.frmBox.fpMessageBoxIcon(imageList1);
            timer1.Enabled = true;

            Proc_AlarmDefineList();
        }
        static public void KillProgram()
        {
            Process[] pLIST;
            pLIST = Process.GetProcessesByName("XModule.vshost");
            foreach (Process Proc in pLIST) 
                Proc.Kill();
            pLIST = Process.GetProcessesByName("XModule");
            foreach (Process proc in pLIST) 
                proc.Kill();

            pLIST = Process.GetProcessesByName("Sic_Run");
            foreach (Process proc in pLIST)
                proc.Kill();

            pLIST = Process.GetProcessesByName("Sic_Run.vshost");
            foreach (Process proc in pLIST)
                proc.Kill();

        }

        public void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cDEF.Work.Project.GlobalOption.UseMES)
            {
                cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.DISCONNECT;
                cDEF.Run.MesStatusMsg = "EXIT PROGRAM";
                cDEF.Mes.Send_EquipStatus();
            }

            timer1.Enabled = false;
            FrmProgress Progress = new FrmProgress();
		    Visible = false;
            Progress.Tag = 1;
            Progress.ShowDialog();
            KillProgram();
        }

        private void FormParent()
        {
            cDEF.frmTitle.MdiParent = this;
            cDEF.frmTitle.Parent = this.panel1;

            cDEF.frmMenu.MdiParent = this;
            cDEF.frmMenu.Parent = this.panel1;

            cDEF.frmBottom.MdiParent = this;
            cDEF.frmBottom.Parent = this.panel1;

            cDEF.frmPageOperation.MdiParent = this;
            cDEF.frmPageOperation.Parent = this.panel1;

            //History
            cDEF.frmMenuHistory.MdiParent = this;
            cDEF.frmMenuHistory.Parent = this.panel1;
            cDEF.frmPageHistoryWarning.MdiParent = this;
            cDEF.frmPageHistoryWarning.Parent = this.panel1;
            cDEF.frmPageHistoryAlarm.MdiParent = this;
            cDEF.frmPageHistoryAlarm.Parent = this.panel1;
            cDEF.frmPageHistoryData.MdiParent = this;
            cDEF.frmPageHistoryData.Parent = this.panel1;
            cDEF.frmPageHistoryEvent.MdiParent = this;
            cDEF.frmPageHistoryEvent.Parent = this.panel1;

            //Recipe
            cDEF.frmMenuRecipe.MdiParent = this;
            cDEF.frmMenuRecipe.Parent = this.panel1;

            cDEF.frmPageRecipeProject.MdiParent = this;
            cDEF.frmPageRecipeProject.Parent = this.panel1;

            //Working
            cDEF.frmMenuWorking.MdiParent = this;
            cDEF.frmMenuWorking.Parent = this.panel1;

            cDEF.frmPageWorkVCMLoader.MdiParent = this;
            cDEF.frmPageWorkVCMLoader.Parent = this.panel1;

            cDEF.frmPageWorkVCMPicker.MdiParent = this;
            cDEF.frmPageWorkVCMPicker.Parent = this.panel1;

            cDEF.frmPageWorkLensLoader.MdiParent = this;
            cDEF.frmPageWorkLensLoader.Parent = this.panel1;

            cDEF.frmPageWorkLensPicker.MdiParent = this;
            cDEF.frmPageWorkLensPicker.Parent = this.panel1;

            cDEF.frmPageWorkIndex.MdiParent = this;
            cDEF.frmPageWorkIndex.Parent = this.panel1;

            cDEF.frmPageWorkBonder.MdiParent = this;
            cDEF.frmPageWorkBonder.Parent = this.panel1;

            cDEF.frmPageWorkCuring.MdiParent = this;
            cDEF.frmPageWorkCuring.Parent = this.panel1;

            cDEF.frmPageWorkUnloader.MdiParent = this;
            cDEF.frmPageWorkUnloader.Parent = this.panel1;

            cDEF.frmPageWorkUnloaderPicker.MdiParent = this;
            cDEF.frmPageWorkUnloaderPicker.Parent = this.panel1;
            //PM
            cDEF.frmPagePM.MdiParent = this;
            cDEF.frmPagePM.Parent = this.panel1;

            //Cofing
            cDEF.frmMenuConfig.MdiParent = this;
            cDEF.frmMenuConfig.Parent = this.panel1;

            cDEF.frmPageConfigMotion.MdiParent = this;
            cDEF.frmPageConfigMotion.Parent = this.panel1;

            cDEF.frmPageConfigDigital.MdiParent = this;
            cDEF.frmPageConfigDigital.Parent = this.panel1;

            cDEF.frmPageConfigDigitalLink.MdiParent = this;
            cDEF.frmPageConfigDigitalLink.Parent = this.panel1;

            cDEF.frmPageConfigAnalog.MdiParent = this;
            cDEF.frmPageConfigAnalog.Parent = this.panel1;

            cDEF.frmPageConfigAnalogLink.MdiParent = this;
            cDEF.frmPageConfigAnalogLink.Parent = this.panel1;

            cDEF.frmPageConfigCylinder.MdiParent = this;
            cDEF.frmPageConfigCylinder.Parent = this.panel1;

            cDEF.frmPageConfigLamp.MdiParent = this;
            cDEF.frmPageConfigLamp.Parent = this.panel1;

            cDEF.frmPageConfigSwitch.MdiParent = this;
            cDEF.frmPageConfigSwitch.Parent = this.panel1;

            cDEF.frmPageConfigGeneral.MdiParent = this;
            cDEF.frmPageConfigGeneral.Parent = this.panel1;

        }

        private void FormHide()
        {
            cDEF.frmTitle.Hide();
            cDEF.frmMenu.Hide();
            cDEF.frmBottom.Hide();
            //operator
            cDEF.frmPageOperation.Hide();
            //History
            cDEF.frmMenuHistory.Hide();
           
            cDEF.frmPageHistoryAlarm.Hide();
            cDEF.frmPageHistoryWarning.Hide();
            cDEF.frmPageHistoryData.Hide();
            cDEF.frmPageHistoryEvent.Hide();
            //Recipe
            cDEF.frmMenuRecipe.Hide();
            cDEF.frmPageRecipeProject.Hide();
            cDEF.frmPagePM.Hide();

        }

        private void swClose_Click(object sender, EventArgs e)
        {
            //int Value = 0;
            //int Temp = 0;

            //if (TfpStandardFormBox.frmBox.SelectBox("INPUT SOURCE", "ENCODER,PULSE OUTPUT,GREEN,YEELOW",ref Value, 4) == DialogResult.No)
            //    return;
            //Temp = Value;

            //String FButton = TfpMessageBoxButton.fmbClose.ToString();//TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            //if (TfpStandardFormBox.frmBox.MessageBox("Auto진행", "초기화가 안되었습니다.\r\n초기화가 되어 있는지 확인 부탁드립니다.", TfpMessageBoxIcon.fmiInformation, FButton) == 0)
            //    return;


            //int Value = 10;
            //if (TfpStandardFormBox.frmBox.fpIntegerEdit("언로딩 대기시간",ref Value, " ms", 0, 10000000))
            //{

            //}
            //else
            //    Value = -1;

            //bool FDatas = new bool();

            //if (FDatas == null)
            //    FDatas = "";

            //String REsult;
            //REsult = Convert.ToString(FDatas);


            int[] Value = new int[3];
            //byte buf = 0x01;
            Value[0] = 100;
            Value[1] = 50;
            Value[2] = 0;
            double[] dValue = new double[2];
            dValue[0] = 100;
            dValue[1] = 50;
            int CurrentX = 20;
            int CurrentY = 10;
            int MinX = 0;
            int MaxX = 10000;
            int MinY = 1;
            int MaxY = 1000000;
            double dCurrentX = 20;
            double dCurrentY = 10;
            double dMinX = 0;
            double dMaxX = 10000;
            double dMinY = 1;
            double dMaxY = 1000000;
            bool bValue = new bool();
            bValue = false;     
            int[] iFTemp = new int[10];
            double[] dFTemp = new double[10];
            bool bFtem = new bool();

            XModuleMain.frmBox.fpScriptBoxClear();
            XModuleMain.frmBox.fpScriptBoxAdd("UnLoading X", "UnLoading X", "", ref Value[0], " um", "CURRENT", CurrentX, MinX, MaxX);
            XModuleMain.frmBox.fpScriptBoxAdd("Loading X", "Loading Y", "", ref XModuleMain.dValue[0], " um", "CURRENT", dCurrentX, dMinX, dMaxX);
            XModuleMain.frmBox.fpScriptBoxAdd("UnLoading Y", "UnLoading Y", "", ref Value[1], " um", "CURRENT", CurrentY, MinY, MaxY);
            XModuleMain.frmBox.fpScriptBoxAdd("Loading Y", "Loading Y", "", ref dValue[1], " um", "CURRENT", dCurrentY, dMinY, dMaxY);
            XModuleMain.frmBox.fpScriptBoxAdd("빈 설정", "", "8인치,2인치,4인치,5인치", ref Value[2], 4);
            XModuleMain.frmBox.fpScriptBoxAdd("NONE", "", "", ref bValue, "");
      
            if (XModuleMain.frmBox.fpScriptBox("UnLoading") == false)
                return;

            iFTemp[0] = XModuleMain.iValue[0];
            dFTemp[0] = XModuleMain.dValue[0];
            iFTemp[1] = XModuleMain.iValue[1];
            dFTemp[1] = XModuleMain.dValue[1];
            iFTemp[2] = XModuleMain.iValue[2];
            bFtem = XModuleMain.bValue[0];

            String sValue = "DARK";
            String SResult;

            if (cDEF.fTextEdit.TextEdit("Working Direction Name", ref sValue) == true)
            {
                SResult = sValue;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if(cDEF.Digital.Input[])
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Visible)
                return;
            this.Text = cDEF.Run.CycleTime.ToString();

            panel_ToStop.Visible = (cDEF.Run.DetailMode == TfpRunningMode.frmToStop);

            if (cDEF.Run.DetailMode == TfpRunningMode.frmNone &&
              cDEF.Run.Digital.Input[cDI.Front_Emergency_Switch])
            {
                pnlMotionStatus.Visible = false;

                for (int m = 0; m < 30; m++)  //하나의 보드에 노드 수량
                {
                    uint nodechkval = CAXL.AxlGetModuleNodeStatus(0, m);
                    if (nodechkval != 0)
                    {
                        pnlMotionStatus.Visible = true;
                    }
                }

            }

            if ((cDEF.Run.DetailMode == TfpRunningMode.frmNone ||
                cDEF.Run.DetailMode == TfpRunningMode.frmStop) &&
                cDEF.Run.CheckBeforeInitialize)
            {
                if (!CheckEQPStart)
                {
                    StepProc_CheckEQP = 0;
                    CheckEQPStart = true;
                }

                Proc_CheckEQP();

            }
        }

        public int StepProc_CheckEQP = 0;
        bool CheckEQPStart = false;
        int IOActiveCount = 0;
        int MotionCount = 0;
        int FCalc = 0;
        /// <summary>
        /// 초기화 전 장비 모션 & IO 상태 체크
        /// </summary>
        private void Proc_CheckEQP()
        {
            switch (StepProc_CheckEQP)
            {

                case 0:

                    for (int m = 0; m < 37; m++)
                    {
                        uint nodechkval = CAXL.AxlGetModuleNodeStatus(0, m);
                        if (nodechkval != 0)
                            break;
                    }
                    StepProc_CheckEQP++;
                    IOActiveCount = 0;
                    MotionCount = 0;
                    FCalc = Environment.TickCount;
                    break;
                case 1:
                    if (Environment.TickCount - FCalc < 600)
                        break;

                    TfpDigitalItem DigitalItem = cDEF.Run.Digital.Items[IOActiveCount];
                    if (!DigitalItem.Active)
                    {
                        DigitalItem.Active = true;
                    }
                    IOActiveCount++;

      
                    if (IOActiveCount == cDEF.Run.Digital.Count)
                    {
                        StepProc_CheckEQP++;
                        FCalc = Environment.TickCount;
                    }

                    break;
                case 2:
                    {
                        if (Environment.TickCount - FCalc < 500)
                            break;

                        TfpMotionItem Item = cDEF.Run.Motion.FItems[MotionCount];
                        Item.Active = false;
                    
                        MotionCount++;

                        if (MotionCount == cDEF.Run.Motion.FItems.Count)
                        {
                            StepProc_CheckEQP++;
                            MotionCount = 0;
                            FCalc = Environment.TickCount;
                        }
                    }
                    break;
                case 3:
                    {
                        if (Environment.TickCount - FCalc < 500)
                            break;

                        TfpMotionItem Item = cDEF.Run.Motion.FItems[MotionCount];
                        Item.Active = false;
                        if (!Item.Active)
                        {
                            Item.Active = true;
                            Item.ServoOn = 0;
                        }
                        if (Convert.ToBoolean(Item.FAlarm))
                            Item.ServoOn = 0;

                        MotionCount++;

                        if (MotionCount == cDEF.Run.Motion.FItems.Count)
                        {
                            StepProc_CheckEQP++;
                            MotionCount = 0;
                            FCalc = Environment.TickCount;
                        }
                    }
                    break;
                case 4:
                    {
                        if (Environment.TickCount - FCalc < 500)
                            break;

                        TfpMotionItem Item = cDEF.Run.Motion.FItems[MotionCount];
                        if (Item.ServoOn == 0)
                        {
                    
                            if (Item.Config.FInputSource == TfpMotionInputSource.fmisEncoder)
                                Item.ServoOn = 1;
                        }

                        MotionCount++;

                        if (MotionCount == cDEF.Run.Motion.FItems.Count)
                        {
                            StepProc_CheckEQP++;
                            MotionCount = 0;
                            FCalc = Environment.TickCount;
                        }
                    }
                    break;
                case 5:
                    {
                        if (Environment.TickCount - FCalc < 500)
                            break;

                        TfpMotionItem Item = cDEF.Run.Motion.FItems[MotionCount];
                        if (Convert.ToBoolean(Item.FAlarm))
                        {
                            Item.AlarmReset(1);
                            Item.Reset();
                        }

                        MotionCount++;

                        if (MotionCount == cDEF.Run.Motion.FItems.Count)
                        {
                            StepProc_CheckEQP++;
                            MotionCount = 0;
                            FCalc = Environment.TickCount;
                        }
                    }
                    break;

                case 6:

                    cDEF.Run.CheckBeforeInitialize = false;

                    cDEF.Run.EasyMode = TfpRunningEasyMode.femInitialize;
                    StepProc_CheckEQP = 0;
                    CheckEQPStart = false;
                    break;

                default:
                    {
                        CheckEQPStart = false;
                        cDEF.Run.CheckBeforeInitialize = false;
                        XModuleMain.frmBox.MessageBox("Warning", "Machine Check Fail", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                    }
                    break;
            }
        }
    }
}

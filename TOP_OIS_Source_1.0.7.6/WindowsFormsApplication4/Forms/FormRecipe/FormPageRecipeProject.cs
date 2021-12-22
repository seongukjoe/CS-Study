using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;
using XModule.Working;
using XModule.Datas;

namespace XModule.Forms.FormRecipe
{
    public partial class FrmPageRecipeProject : TFrame
    {
        int Selected;
        int PLineSelectedNum1 = 0;
        int PLineSelectedNum2 = 0;
        int PArcSelectedNum1 = 0;
        int PArcSelectedNum2 = 0;
        private List<Label> LabelLst = new List<Label>();
        public FrmPageRecipeProject()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 995);
          
        }
        private void FormPageRecipeProject_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            Visible = false;
            Selected = 0;
            VcmImgInit();
            UpdateDetailInformation();

            if(cDEF.Work.DispSensor.DispenserType == 2)
            {
                btnUseCooling1.Visible = true;
                btnUseCooling2.Visible = true;
                lbAlCooling1.Visible = true;
                lbAlCooling2.Visible = true;
            }
            else
            {
                btnUseCooling1.Visible = false;
                btnUseCooling2.Visible = false;
                lbAlCooling1.Visible = false;
                lbAlCooling2.Visible = false;
            }

            //Jetting1GridInit();
            //Jetting2GridInit();

            //Jetting1LineGridInit();
            //Jetting2LineGridInit();

            //Jetting1PLineGridInit();
            //Jetting2PLineGridInit();
            //LabelLst.Clear();
            //LabelInit();
            //VCM 이미지 표시
        }

        protected override void UpdateInformation()
        {
            if (!Visible)
                return;

            this.Invoke(new Action(delegate ()
            {
                {
                    // Option
                    lbUseJigPlateAngle.BackColor = cDEF.Work.Project.GlobalOption.UseJigPlateAngle ? Color.Lime : Color.White;
                    lbUsePlateAngle.BackColor = cDEF.Work.Project.GlobalOption.UsePlateAngle ? Color.Lime : Color.White;
                    lbUseCleanJig.BackColor = cDEF.Work.Project.GlobalOption.UseCleanJig ? Color.Lime : Color.White;
                    lbUseLensHeight.BackColor = cDEF.Work.Project.GlobalOption.UseLensHeight ? Color.Lime : Color.White;
                    lbUseBonder1.BackColor = cDEF.Work.Project.GlobalOption.UseBonder1 ? Color.Lime : Color.White;
                    lbUseBonder2.BackColor = cDEF.Work.Project.GlobalOption.UseBonder2 ? Color.Lime : Color.White;
                    lbUseCuring1.BackColor = cDEF.Work.Project.GlobalOption.UseCuring1 ? Color.Lime : Color.White;
                    lbUseCuring2.BackColor = cDEF.Work.Project.GlobalOption.UseCuring2 ? Color.Lime : Color.White;
                    lbUseVision.BackColor = cDEF.Work.Project.GlobalOption.UseVision ? Color.Lime : Color.White;
                    lbUseLensPicker.BackColor = cDEF.Work.Project.GlobalOption.UseLensPicker ? Color.Lime : Color.White;
                    lbUseAct3.BackColor = cDEF.Work.Project.GlobalOption.UseActAction3 ? Color.Lime : Color.White;

                    lbVisionResult.BackColor = cDEF.Work.Project.GlobalOption.VisionResult ? Color.Lime : Color.White;
                    lbLensHeightResult.BackColor = cDEF.Work.Project.GlobalOption.LensHeightResult ? Color.Lime : Color.White;
                    lbJigFlatnessResult.BackColor = cDEF.Work.Project.GlobalOption.JigPlateAngleResult ? Color.Lime : Color.White;
                    lbSideAngleResult.BackColor = cDEF.Work.Project.GlobalOption.PlateAngleResult ? Color.Lime : Color.White;
                    lbUseResultRowDataJudge.BackColor = cDEF.Work.Project.GlobalOption.PlateAngleResultRowJudge ? Color.Lime : Color.White;

                    lbVacuumCheck.BackColor = cDEF.Work.Project.GlobalOption.VacuumCheck ? Color.Lime : Color.White;
                    lbTrayCheck.BackColor = cDEF.Work.Project.GlobalOption.TrayCheck ? Color.Lime : Color.White;
                    lbIndexCheck.BackColor = cDEF.Work.Project.GlobalOption.IndexCheck ? Color.Lime : Color.White;
                    lbVisionCheck.BackColor = cDEF.Work.Project.GlobalOption.VisionCheck ? Color.Lime : Color.White;

                    lbLensHeightSoftWareJudge.BackColor = cDEF.Work.Project.GlobalOption.LensHeightSoftWareJudge ? Color.Lime : Color.White;

                    lbLoaderMagazine.Text = cDEF.Work.Recipe.LoaderMagazineDirection ? "From Top" : "From Bottom";
                    lbLensMagazine.Text = cDEF.Work.Recipe.LensMagazineDirection ? "From Top" : "From Bottom";
                    lbUnloaderMagazine.Text = cDEF.Work.Recipe.UnLoaderMagazineDirection ? "From Top" : "From Bottom";
                    lbLensInsertTorque.BackColor = cDEF.Work.Recipe.LensInsertTorqueUse ? Color.Lime : Color.White;
                    lbLensPickUpTorque.BackColor = cDEF.Work.Recipe.LensPickUpTorqueUse ? Color.Lime : Color.White;
                    lbLensThetaTorque.BackColor = cDEF.Work.Recipe.LensPickUpThetaTorqueUse ? Color.Lime : Color.White;

                    lbUseIdle1.BackColor = cDEF.Work.Project.GlobalOption.UseIdle1 ? Color.Lime : Color.White;
                    lbUseIdle2.BackColor = cDEF.Work.Project.GlobalOption.UseIdle2 ? Color.Lime : Color.White;

                    lbAlCooling1.BackColor = cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1 ? Color.Lime : Color.White;
                    lbAlCooling2.BackColor = cDEF.Work.Project.GlobalOption.Use_TJV_Cooling2 ? Color.Lime : Color.White;

                    lbUsePlusenum_1.BackColor = cDEF.Work.Project.GlobalOption.UsePlusenum1 ? Color.Lime : Color.White;
                    lbUsePlusenum_2.BackColor = cDEF.Work.Project.GlobalOption.UsePlusenum2 ? Color.Lime : Color.White;


                    lblUseTipClean1.BackColor = cDEF.Work.Project.GlobalOption.UseTipClean1 ? Color.Lime : Color.White;
                    lblUseTipClean2.BackColor = cDEF.Work.Project.GlobalOption.UseTipClean2 ? Color.Lime : Color.White;
                    lbUseDummy1.BackColor = cDEF.Work.Project.GlobalOption.UseDummy1 ? Color.Lime : Color.White;
                    lbUseDummy2.BackColor = cDEF.Work.Project.GlobalOption.UseDummy2 ? Color.Lime : Color.White;

                    lblResultDummyPass.BackColor = cDEF.Work.Project.GlobalOption.UseResultDummyPass ? Color.Lime : Color.White;

                    lbUseGap.BackColor = cDEF.Work.Project.GlobalOption.UseGap ? Color.Lime : Color.White;
                    lbUseSecondaryCorrection.BackColor = cDEF.Work.Project.GlobalOption.UseSecondaryCorrection ? Color.Lime : Color.White;

                    lblUseActNotAction1.BackColor = cDEF.Work.Project.GlobalOption.UseActAction1 ? Color.Lime : Color.White;
                    lblUseActNotAction2.BackColor = cDEF.Work.Project.GlobalOption.UseActAction2 ? Color.Lime : Color.White;
                    lbPreAct.BackColor = cDEF.Work.Project.GlobalOption.UsePreActuating ? Color.Lime : Color.White;

                    //Actuator Option
                    lbActuator_1_Mode.Text = cDEF.Work.Project.GlobalOption.Actuator_1_Mode ? "Actuator #1 Mode 1" : "Actuator #1 Mode 2";
                    lbActuator_2_Mode.Text = cDEF.Work.Project.GlobalOption.Actuator_2_Mode ? "Actuator #2 Mode 1" : "Actuator #2 Mode 2";
                    lbActuator_3_Mode.Text = cDEF.Work.Project.GlobalOption.Actuator_3_Mode ? "Actuator #3 Mode 1" : "Actuator #3 Mode 2";

                    btnAssembleMode.Text = cDEF.Work.Project.GlobalOption.UseLockType ? "LOCK TYPE" : "NORMAL TYPE";


                    if (cDEF.Work.Project.GlobalOption.JettingMode1 == 0)
                        btnJettingMode_1.Text = "POINT";
                    else if (cDEF.Work.Project.GlobalOption.JettingMode1 == 1)
                        btnJettingMode_1.Text = "LINE";
                    else if (cDEF.Work.Project.GlobalOption.JettingMode1 == 2)
                        btnJettingMode_1.Text = "ARC";

                    if (cDEF.Work.Project.GlobalOption.JettingMode2 == 0)
                        btnJettingMode_2.Text = "POINT";
                    else if (cDEF.Work.Project.GlobalOption.JettingMode2 == 1)
                        btnJettingMode_2.Text = "LINE";
                    else if (cDEF.Work.Project.GlobalOption.JettingMode2 == 2)
                        btnJettingMode_2.Text = "ARC";


                    lbSecondaryLimitValue.Text = $"{cDEF.Work.LensPicker.SecondaryCorrLimit} ˚";
                    lblbtnUseActAndCure.BackColor = cDEF.Work.Project.GlobalOption.UseActAndCure ? Color.Lime : Color.White;

                    lbTorqueLimitZ.Text = cFnc.GetUnitValueString((int)cDEF.Work.Recipe.LensInsertTorqueLimit, true);
                    lbTorqueTLimit.Text = $"{cDEF.Work.Recipe.LensInsertTorqueLimitTheta / 100.0} ˚";


                    //패턴라인 
                    lblPatCreate1.Text = cDEF.Work.Bonder1Pattern.JetPatternLineCount.ToString();
                    lblPatCreate2.Text = cDEF.Work.Bonder2Pattern.JetPatternLineCount.ToString();

                    lblArcCreate1.Text = cDEF.Work.Bonder1ARC.JetPatternArcCount.ToString();
                    lblArcCreate2.Text = cDEF.Work.Bonder2ARC.JetPatternArcCount.ToString();

                    btnPatCurNum1.Text = (PLineSelectedNum1 + 1).ToString();
                    btnPatCurNum2.Text = (PLineSelectedNum2 + 1).ToString();

                    btnArcCurNum1.Text = (PArcSelectedNum1 + 1).ToString();
                    btnArcCurNum2.Text = (PArcSelectedNum2 + 1).ToString();

                    lblLensHeight.Text = cDEF.Work.Recipe.LensHeight.ToString();
                    lblLensAllowMin.Text = cDEF.Work.Recipe.LensHeightAllowMin.ToString();
                    lblLensAllowMax.Text = cDEF.Work.Recipe.LensHeightAllowMax.ToString();

                    lblUseActRetry.BackColor = cDEF.Work.Project.GlobalOption.UseActRetry ? Color.Lime : Color.White;

                    //Vision Retry Count
                    lbVCMVISIONRetryCount.Text = cDEF.Work.Recipe.VCMVISIONRetryCount.ToString();
                    lbLensUpperRetryCount.Text = cDEF.Work.Recipe.LensUpperRetryCount.ToString();
                    lbLensBottomRetryCount.Text = cDEF.Work.Recipe.LensUnderRetryCount.ToString();
                    lbBonder1RetryCount.Text = cDEF.Work.Recipe.Bonder1RetryCount.ToString();
                    lbBonder2RetryCount.Text = cDEF.Work.Recipe.Bonder2RetryCount.ToString();
                    lbVisionRetryCount.Text = cDEF.Work.Recipe.VIsionInspectRetryCount.ToString();

                    lbRnRGain.Text = cDEF.Work.PlateAngle.RnRPercent.ToString();
                    lbRnRShift.Text = cDEF.Work.PlateAngle.RnRShift.ToString("N3");

                    lblDummyTime1.Text = cDEF.Work.Recipe.DummyTime1.ToString() + " ms";
                    lblDummyTime2.Text = cDEF.Work.Recipe.DummyTime2.ToString() + " ms";

                    lblDummyPeriodCount1.Text = cDEF.Work.Recipe.DummyPeriodCount1.ToString() + " ea";
                    lblDummyPeriodCount2.Text = cDEF.Work.Recipe.DummyPeriodCount2.ToString() + " ea";

                    lbUseCureVisionFail.BackColor = cDEF.Work.Project.GlobalOption.UseCureVisionFail ? Color.Lime : Color.White;

                    lbResetTime1.Text = cDEF.Work.Option.ResetTime1.ToString("00:00");
                    lbResetTime2.Text = cDEF.Work.Option.ResetTime2.ToString("00:00");

                    lbBonder1GapPosX.Text = (cDEF.Work.Bonder1.GapPosX / 1000.0).ToString("N3");
                    lbBonder1GapPosY.Text = (cDEF.Work.Bonder1.GapPosY / 1000.0).ToString("N3");
                    lbBonder2GapPosX.Text = (cDEF.Work.Bonder2.GapPosX / 1000.0).ToString("N3");
                    lbBonder2GapPosY.Text = (cDEF.Work.Bonder2.GapPosY / 1000.0).ToString("N3");


                    lblUseMES.BackColor = cDEF.Work.Project.GlobalOption.UseMES ? Color.Lime : Color.White;
                    lblVisionVer.Text = cDEF.Work.Project.GlobalOption.VisionVer;

                    lbPlateAngleRetry.Text = cDEF.Work.Option.Measure_RetryCount.ToString();
                    lbPlateAngleGood.Text = cDEF.Work.Option.Measure_GoodCount.ToString();
                }

            }));
        }
        private void UpdateDetailInformation()
        {
            String Temp = "";
            String GridValue = "";
            DirectoryInfo Info = new DirectoryInfo(cPath.FILE_PROJECT);
            int Count = Info.GetFiles().Length;
            int i = 0;

            grid.RowCount = Count;

            foreach (var Item in Info.GetFiles())
            {
                Temp = i.ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;

                Temp = Item.Name.Substring(0, Item.Name.Length - 4).ToUpper();
                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[1].Value = Temp;

                Temp = Item.CreationTime.ToString("yyyy-MM-dd");
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;

                i++;

            }

            Jetting1GridInit();
            Jetting2GridInit();            
            PLineSelectedNum1 = 0;
            PLineSelectedNum2 = 0;
            PArcSelectedNum1 = 0;
            PArcSelectedNum2 = 0;
            Jetting1PLineGridInit();
            Jetting2PLineGridInit();
            Jetting1PArcGridInit();
            Jetting2PArcGridInit();


        }
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row = 0;

            Col = grid.CurrentCell.ColumnIndex;
            Row = grid.CurrentCell.RowIndex;
            if (Col > -1 && Row > -1)
            {
                if (Selected != Row)
                    Selected = Row;
            }
        }
        private void ConfigurationClick(object sender, EventArgs e)
        {
            String FIleName = "";
            String FTemp = "";
            string Old = string.Empty;
            string FButton;

            switch (Convert.ToInt32((sender as Glass.GlassButton).Tag))
            {
                case 0: //Delete
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Selected Recipe?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        FIleName = grid.Rows[Selected].Cells[1].Value.ToString();
                        File.Delete(cPath.FILE_PROJECT + FIleName + ".ini");
                        DirectoryInfo di = new DirectoryInfo(cPath.FILE_PROJECT + FIleName + "\\");
                        if (di.Exists)
                        {
                            di.Delete(true);
                        }
                        UpdateDetailInformation();
                    }
                    cDEF.Run.LogEvent(cLog.Form_Recipe_Event, "[Form Recipe] Recipe Delete Click.");
                    break;

                case 1: //Save As
                    if (grid.RowCount <= Selected)
                        Selected = grid.RowCount - 1;
                    FTemp = grid.Rows[Selected].Cells[1].Value.ToString();
                    if (cDEF.fTextEdit.TextEdit("File Name", ref FTemp))
                    {
                        FTemp = FTemp.ToUpper();
                        cDEF.Work.Project.FileName = FTemp;
                        cDEF.Work.Project.Save(FTemp);
                        cDEF.Work.Project.SaveDefault();
                        cDEF.Work.Recipe.Save(FTemp);
                        //cDEF.Run.VCMLoader.Information.VCM_Magazine.Init(cDEF.Work.VCMLoader.SlotCount, cDEF.Work.VCMLoader.TrayCountX, cDEF.Work.VCMLoader.TrayCountY);
                        //cDEF.frmPageOperation.VCMMagazineObjectInit();
                        //cDEF.Run.LensLoader.Information.Lens_Magazine.Init(cDEF.Work.LensLoader.SlotCount, cDEF.Work.LensLoader.TrayCountX, cDEF.Work.LensLoader.TrayCountY);
                        //cDEF.frmPageOperation.LensMagazineObjectInit();
                        //cDEF.Run.Unloader.Information.Unloader_Magazine.Init(cDEF.Work.Unloader.SlotCount, cDEF.Work.Unloader.TrayCountX, cDEF.Work.Unloader.TrayCountY);
                        //cDEF.frmPageOperation.UnloadMagazineObjectInit();
                        UpdateDetailInformation();

                       
                    }
                    cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 1, "[Form Recipe] Recipe Save Click");
                    break;

                case 2: //Open
                    if (grid.RowCount <= Selected)
                        Selected = grid.RowCount - 1;
                    FIleName = grid.Rows[Selected].Cells[1].Value.ToString();
                    if (!cDEF.Work.Project.Open(FIleName))
                    {
                    }

                    VcmImgInit();
                    cDEF.Run.VCMLoader.Information.VCM_Magazine.Init(cDEF.Work.VCMLoader.SlotCount, cDEF.Work.VCMLoader.TrayCountX, cDEF.Work.VCMLoader.TrayCountY);
                    cDEF.frmPageOperation.VCMMagazineObjectInit();
                    cDEF.Run.LensLoader.Information.Lens_Magazine.Init(cDEF.Work.LensLoader.SlotCount, cDEF.Work.LensLoader.TrayCountX, cDEF.Work.LensLoader.TrayCountY);
                    cDEF.frmPageOperation.LensMagazineObjectInit();
                    cDEF.Run.Unloader.Information.Unloader_Magazine.Init(cDEF.Work.Unloader.SlotCount, cDEF.Work.Unloader.TrayCountX, cDEF.Work.Unloader.TrayCountY);
                    cDEF.frmPageOperation.UnloadMagazineObjectInit();
                    cDEF.Work.Project.SaveDefault();
                    cDEF.Work.Save(cDEF.Work.Project.FileName);
                    cDEF.Visions.RecipeName = cDEF.Work.Project.FileName;
                    Jetting1GridInit();
                    Jetting2GridInit();
                    PLineSelectedNum1 = 0;
                    PLineSelectedNum2 = 0;
                    PArcSelectedNum1 = 0;
                    PArcSelectedNum2 = 0;
                    Jetting1PLineGridInit();
                    Jetting2PLineGridInit();
                    Jetting1PArcGridInit();
                    Jetting2PArcGridInit();
                    cDEF.Lang.Load();
                    cDEF.ChangeLanguage();
                    cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 2, "[Form Recipe] Recipe Open Click.");
                    break;

                case 3: //Rename
                    if (grid.RowCount <= Selected)
                        Selected = grid.RowCount - 1;
                    FTemp = grid.Rows[Selected].Cells[1].Value.ToString();
                    Old = FTemp;
                    if (cDEF.fTextEdit.TextEdit("File Name", ref FTemp))
                    {
                        FTemp = FTemp.ToUpper();
                        cDEF.Work.Project.FileName = FTemp;
                        cDEF.Work.Project.Save(FTemp);
                        cDEF.Work.Project.SaveDefault();
                        cDEF.Work.Recipe.Save(FTemp);

                        File.Delete(cPath.FILE_PROJECT + Old + ".ini");
                        DirectoryInfo di = new DirectoryInfo(cPath.FILE_PROJECT + Old + "\\");
                        if (di.Exists)
                        {
                            di.Delete(true);
                        }
                        UpdateDetailInformation();
                    }
                    cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 1, "[Form Recipe] Recipe Rename Click");
                    break;
            }

          

        }


        private void glassButton_OptionDryRun_Click(object sender, EventArgs e)
        {
            int tag = 0;
            string FName = (sender as Glass.GlassButton).Name;
            int FSelected = -1;
            string Items = string.Empty;

            switch (FName)
            {
                case "btnVacuumCheck":
                    cDEF.Work.Project.GlobalOption.VacuumCheck = !cDEF.Work.Project.GlobalOption.VacuumCheck;
                    if (cDEF.Work.Project.GlobalOption.VacuumCheck)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 3, "[Form Recipe] Vacuum Check ON");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 4, "[Form Recipe] Vacuum Check OFF");
                    break;
                case "btnIndexCheck":
                    cDEF.Work.Project.GlobalOption.IndexCheck = !cDEF.Work.Project.GlobalOption.IndexCheck;
                    if (cDEF.Work.Project.GlobalOption.IndexCheck)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 5, "[Form Recipe] Index Check ON");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 6, "[Form Recipe] Index Check OFF");
                    break;
                case "btnTrayCheck":
                    cDEF.Work.Project.GlobalOption.TrayCheck = !cDEF.Work.Project.GlobalOption.TrayCheck;
                    if (cDEF.Work.Project.GlobalOption.TrayCheck)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 7, "[Form Recipe] Tray Check ON");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 8, "[Form Recipe] Tray Check OFF");
                    break;
                case "btnVisionCheck":
                    cDEF.Work.Project.GlobalOption.VisionCheck = !cDEF.Work.Project.GlobalOption.VisionCheck;
                    if (cDEF.Work.Project.GlobalOption.VisionCheck)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 7, "[Form Recipe] Vision Check ON");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 8, "[Form Recipe] Vision Check OFF");
                    break;
                case "btnUseVision":
                    cDEF.Work.Project.GlobalOption.UseVision = !cDEF.Work.Project.GlobalOption.UseVision;
                    if (cDEF.Work.Project.GlobalOption.UseVision)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 9, "[Form Recipe] Set Use Vision Inspect");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 10, "[Form Recipe] Set Use Vision Inspect");
                    break;
                case "btnUseJigPlateAngle":
                    cDEF.Work.Project.GlobalOption.UseJigPlateAngle = !cDEF.Work.Project.GlobalOption.UseJigPlateAngle;
                    if (cDEF.Work.Project.GlobalOption.UseJigPlateAngle)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 11, "[Form Recipe] Set Use Jig Flatness");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 12, "[Form Recipe] Set Not Use Jig Flatness");
                    break;
                case "btnUsePlateAngle":
                    cDEF.Work.Project.GlobalOption.UsePlateAngle = !cDEF.Work.Project.GlobalOption.UsePlateAngle;
                    if (cDEF.Work.Project.GlobalOption.UsePlateAngle)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 13, "[Form Recipe] Set Use Side Angle");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 14, "[Form Recipe] Set Not Use Side Angle");
                    break;
                case "btnUseCleanJig":
                    cDEF.Work.Project.GlobalOption.UseCleanJig = !cDEF.Work.Project.GlobalOption.UseCleanJig;
                    if (cDEF.Work.Project.GlobalOption.UseCleanJig)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 15, "[Form Recipe] Set Use CleanJig");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 16, "[Form Recipe] Set Not Use CleanJig");
                    break;
                case "btnUseLensHeight":
                    cDEF.Work.Project.GlobalOption.UseLensHeight = !cDEF.Work.Project.GlobalOption.UseLensHeight;
                    if (cDEF.Work.Project.GlobalOption.UseLensHeight)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 17, "[Form Recipe] Set Use Lens Height");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 18, "[Form Recipe] Set Not Use Lens Height");
                    break;
                case "btnUseBonder1":
                    cDEF.Work.Project.GlobalOption.UseBonder1 = !cDEF.Work.Project.GlobalOption.UseBonder1;
                    if (cDEF.Work.Project.GlobalOption.UseBonder1)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 19, "[Form Recipe] Set Use Bonder #1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 20, "[Form Recipe] Set Not Use Bonder #1");
                    break;
                case "btnUseBonder2":
                    cDEF.Work.Project.GlobalOption.UseBonder2 = !cDEF.Work.Project.GlobalOption.UseBonder2;
                    if (cDEF.Work.Project.GlobalOption.UseBonder2)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 21, "[Form Recipe] Set Use Bonder #2");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 22, "[Form Recipe] Set Not Use Bonder #2");
                    break;
                case "btnUseCuring1":
                    cDEF.Work.Project.GlobalOption.UseCuring1 = !cDEF.Work.Project.GlobalOption.UseCuring1;
                    if (cDEF.Work.Project.GlobalOption.UseCuring1)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 23, "[Form Recipe] Set Use Curing #1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 24, "[Form Recipe] Set Not Use Curing #1");
                    break;
                case "btnUseCuring2":
                    cDEF.Work.Project.GlobalOption.UseCuring2 = !cDEF.Work.Project.GlobalOption.UseCuring2;
                    if (cDEF.Work.Project.GlobalOption.UseCuring2)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 25, "[Form Recipe] Set Use Curing #2");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 26, "[Form Recipe] Set Not Use Curing #2");
                    break;
                case "btnVisionResult":
                    cDEF.Work.Project.GlobalOption.VisionResult = !cDEF.Work.Project.GlobalOption.VisionResult;
                    if (cDEF.Work.Project.GlobalOption.VisionResult)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 27, "[Form Recipe] Set Use Vision Result");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 28, "[Form Recipe] Set Not Use Vision Result");
                    break;
                case "btnLensHeightReslut":
                    cDEF.Work.Project.GlobalOption.LensHeightResult = !cDEF.Work.Project.GlobalOption.LensHeightResult;
                    if (cDEF.Work.Project.GlobalOption.LensHeightResult)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 29, "[Form Recipe] Set Use Lens Height Result");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 30, "[Form Recipe] Set Not Use Lens Height Result");
                    break;
                case "btnJigPlateAngleResult":
                    cDEF.Work.Project.GlobalOption.JigPlateAngleResult = !cDEF.Work.Project.GlobalOption.JigPlateAngleResult;
                    if (cDEF.Work.Project.GlobalOption.JigPlateAngleResult)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 31, "[Form Recipe] Set Use Jig Flatness Result");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 32, "[Form Recipe] Set Not Use Jig Flatness Result");
                    break;
                case "btnSideAngleResult":
                    cDEF.Work.Project.GlobalOption.PlateAngleResult = !cDEF.Work.Project.GlobalOption.PlateAngleResult;
                    if (cDEF.Work.Project.GlobalOption.PlateAngleResult)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 33, "[Form Recipe] Set Use Side Angle Result");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 34, "[Form Recipe] Set Not Use Side Angle Result");
                    break;
                case "btnLensHeightSoftWareJudge":
                    cDEF.Work.Project.GlobalOption.LensHeightSoftWareJudge = !cDEF.Work.Project.GlobalOption.LensHeightSoftWareJudge;
                    if (cDEF.Work.Project.GlobalOption.LensHeightSoftWareJudge)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 35, "[Form Recipe] Set LensHeight SoftWare Judge");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 36, "[Form Recipe] Set LensHeight HardWare Judge");
                    break;
                case "btnUseIdle1":
                    cDEF.Work.Project.GlobalOption.UseIdle1 = !cDEF.Work.Project.GlobalOption.UseIdle1;
                    if (cDEF.Work.Project.GlobalOption.UseIdle1)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 37, "[Form Recipe] Set Use Idle #1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 38, "[Form Recipe] Set Not Use Idle #1");
                    break;
                case "btnUseIdle2":
                    cDEF.Work.Project.GlobalOption.UseIdle2 = !cDEF.Work.Project.GlobalOption.UseIdle2;
                    if (cDEF.Work.Project.GlobalOption.UseIdle2)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 37, "[Form Recipe] Set Use Idle #2");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 38, "[Form Recipe] Set Not Use Idle #2");
                    break;
                case "btnUsePlusenum_1":
                    cDEF.Work.Project.GlobalOption.UsePlusenum1 = !cDEF.Work.Project.GlobalOption.UsePlusenum1;
                    if (cDEF.Work.Project.GlobalOption.UsePlusenum1)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 39, "[Form Recipe] Set Use Plusenum #1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 40, "[Form Recipe] Set Not Use Plusenum #1");
                    break;
                case "btnUsePlusenum_2":
                    cDEF.Work.Project.GlobalOption.UsePlusenum2 = !cDEF.Work.Project.GlobalOption.UsePlusenum2;
                    if (cDEF.Work.Project.GlobalOption.UsePlusenum2)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 48, "[Form Recipe] Set Use Plusenum #2");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 49, "[Form Recipe] Set Not Use Plusenum #2");
                    break;

                case "btnUseTipClean1":
                    cDEF.Work.Project.GlobalOption.UseTipClean1 = !cDEF.Work.Project.GlobalOption.UseTipClean1;
                    if (cDEF.Work.Project.GlobalOption.UseTipClean1)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 37, "[Form Recipe] Set Use Tip Clean #1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 38, "[Form Recipe] Set Not Use Tip Clean #1");
                    break;
                case "btnUseTipClean2":
                    cDEF.Work.Project.GlobalOption.UseTipClean2 = !cDEF.Work.Project.GlobalOption.UseTipClean2;
                    if (cDEF.Work.Project.GlobalOption.UseTipClean2)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 37, "[Form Recipe] Set Use Tip Clean #2");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 38, "[Form Recipe] Set Not Use Tip Clean #2");
                    break;


                case "btnActuator_1_Mode":
                    cDEF.Work.Project.GlobalOption.Actuator_1_Mode = !cDEF.Work.Project.GlobalOption.Actuator_1_Mode;
                    if (cDEF.Work.Project.GlobalOption.Actuator_1_Mode)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 41, "[Form Recipe] Set Actuator #1 Mode 1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 42, "[Form Recipe] Set Actuator #1 Mode 2");
                    break;
                case "btnActuator_2_Mode":
                    cDEF.Work.Project.GlobalOption.Actuator_2_Mode = !cDEF.Work.Project.GlobalOption.Actuator_2_Mode;
                    if (cDEF.Work.Project.GlobalOption.Actuator_2_Mode)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 43, "[Form Recipe] Set Actuator #2 Mode 1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 44, "[Form Recipe] Set Actuator #2 Mode 2");
                    break;

                case "btnActuator_3_Mode":
                    cDEF.Work.Project.GlobalOption.Actuator_3_Mode = !cDEF.Work.Project.GlobalOption.Actuator_3_Mode;
                    if (cDEF.Work.Project.GlobalOption.Actuator_3_Mode)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 43, "[Form Recipe] Set Actuator #3 Mode 1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 44, "[Form Recipe] Set Actuator #3 Mode 2");
                    break;

                case "btnJettingMode_1":
                    {
                        int Selected = -1;
            
            
                        if (cDEF.Work.DispSensor.DispenserType == 0 || cDEF.Work.DispSensor.DispenserType == 1)
                        {
                            if (XModuleMain.frmBox.SelectBox("Jetting Mode #1", "POINT, LINE, ARC", ref Selected) == DialogResult.No)
                                return;
                            cDEF.Work.Project.GlobalOption.JettingMode1 = Selected;
                            if (Selected == 1 || Selected == 2)
                            {
                                //패턴라인 : 작업모드 라인 사용
                                Selected = 1;
                            }
                            if (cDEF.Work.DispSensor.DispenserType == 0)
                            {
                                cDEF.Dispenser1.Send_ParamWrite(cDEF.Dispenser1.RTValue, cDEF.Dispenser1.HTValue, cDEF.Dispenser1.FTValue, cDEF.Dispenser1.DelayValue,
                                    (int)cDEF.Dispenser1.PCTValue, (int)cDEF.Dispenser1.PluseNumValue, Selected + 1, (int)cDEF.Dispenser1.VoltageValue);
                            }

                            else if (cDEF.Work.DispSensor.DispenserType == 1)
                            {
                                cDEF.DispenserEcm1.CMDMode = Unit.clsSuperEcm3.ECMDMode.ChangeMode;
                                cDEF.DispenserEcm1.SetMode = Selected;
                                cDEF.DispenserEcm1.SetValueStart();
                            }
                        }
                        else if (cDEF.Work.DispSensor.DispenserType == 2)
                        {
                            if (XModuleMain.frmBox.SelectBox("Jetting Mode #1", "POINT, LINE, ARC", ref Selected) == DialogResult.No)
                                return;
                            cDEF.Work.Project.GlobalOption.JettingMode1 = Selected;

                            if (Selected == 0)
                                cDEF.Work.Recipe.WorkMode_1 = 1;
                            else
                                cDEF.Work.Recipe.WorkMode_1 = 2;
                            cDEF.TJV_1.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_1);
                            cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        }
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 45, "[Form Recipe] Set Actuator #2 Mode 2");
                    }
                    break;
                case "btnJettingMode_2":
                    {
                        int Selected = -1;
                        if (cDEF.Work.DispSensor.DispenserType == 0 || cDEF.Work.DispSensor.DispenserType == 1)
                        {
                            if (XModuleMain.frmBox.SelectBox("Jetting Mode #2", "POINT, LINE, ARC", ref Selected) == DialogResult.No)
                                return;
                            cDEF.Work.Project.GlobalOption.JettingMode2 = Selected;
                            if (Selected == 1 || Selected == 2)
                            {
                                //패턴라인 : 작업모드 라인 사용
                                Selected = 1;
                            }
                            if (cDEF.Work.DispSensor.DispenserType == 0)
                            {
                                cDEF.Dispenser2.Send_ParamWrite(cDEF.Dispenser2.RTValue, cDEF.Dispenser2.HTValue, cDEF.Dispenser2.FTValue, cDEF.Dispenser2.DelayValue,
                                (int)cDEF.Dispenser2.PCTValue, (int)cDEF.Dispenser2.PluseNumValue, Selected + 1, (int)cDEF.Dispenser2.VoltageValue);
                            }
                            else
                            {
                                cDEF.DispenserEcm2.CMDMode = Unit.clsSuperEcm3.ECMDMode.ChangeMode;
                                cDEF.DispenserEcm2.SetMode = Selected;
                                cDEF.DispenserEcm2.SetValueStart();
                            }
                        }
                        else if (cDEF.Work.DispSensor.DispenserType == 2)
                        {
                            if (XModuleMain.frmBox.SelectBox("Jetting Mode #2", "POINT, LINE, ARC", ref Selected) == DialogResult.No)
                                return;
                            cDEF.Work.Project.GlobalOption.JettingMode2 = Selected;

                            if (Selected == 0)
                                cDEF.Work.Recipe.WorkMode_2 = 1;
                            else
                                cDEF.Work.Recipe.WorkMode_2 = 2;
                            cDEF.TJV_2.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_2);
                            cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);

                        }
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 45, "[Form Recipe] Set Actuator #2 Mode 2");
                    }
                    break;
                case "btnUseActAndCure":
                    cDEF.Work.Project.GlobalOption.UseActAndCure = !cDEF.Work.Project.GlobalOption.UseActAndCure;
                    if (cDEF.Work.Project.GlobalOption.UseActAndCure)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 46, "[Form Recipe] Set Use ACTUATING And #1 Cure");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 47, "[Form Recipe] Set Not Use ACTUATING And #1 Cure");
                    break;
                case "btnUseActRetry":
                    cDEF.Work.Project.GlobalOption.UseActRetry = !cDEF.Work.Project.GlobalOption.UseActRetry;
                    if (cDEF.Work.Project.GlobalOption.UseActRetry)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 48, "[Form Recipe] Set Use ACTUATING Retry");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 49, "[Form Recipe] Set Not Use ACTUATING Retry");
                    break;
                case "BtnResultDummyPass":
                    cDEF.Work.Project.GlobalOption.UseResultDummyPass = !cDEF.Work.Project.GlobalOption.UseResultDummyPass;
                    if (cDEF.Work.Project.GlobalOption.UseResultDummyPass)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 50, "[Form Recipe] Set Use Result DummyPass");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 51, "[Form Recipe] Set Not Use Result DummyPass");
                    break;

                case "btnUseDummy1":
                    cDEF.Work.Project.GlobalOption.UseDummy1 = !cDEF.Work.Project.GlobalOption.UseDummy1;
                    if (cDEF.Work.Project.GlobalOption.UseDummy1)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 52, "[Form Recipe] Set Use Dummy #1");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 53, "[Form Recipe] Set Not Use Dummy #1");
                    break;
                case "btnUseDummy2":
                    cDEF.Work.Project.GlobalOption.UseDummy2 = !cDEF.Work.Project.GlobalOption.UseDummy2;
                    if (cDEF.Work.Project.GlobalOption.UseDummy2)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 54, "[Form Recipe] Set Use Dummy #2");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 55, "[Form Recipe] Set Not Use Dummy #2");
                    break;
                case "btnUseCureVisionFail":
                    cDEF.Work.Project.GlobalOption.UseCureVisionFail = !cDEF.Work.Project.GlobalOption.UseCureVisionFail;
                    if (cDEF.Work.Project.GlobalOption.UseCureVisionFail)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 54, "[Form Recipe] Set Use Cure for Vision Inspect Fail");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 55, "[Form Recipe] Set Not Use Cure for Vision Inspect Fail");
                    break;

                case "btnAssembleMode":
                    cDEF.Work.Project.GlobalOption.UseLockType = !cDEF.Work.Project.GlobalOption.UseLockType;
                    if (cDEF.Work.Project.GlobalOption.UseLockType)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 60, "[Form Recipe] Assemble Mode Set Use Lock Type");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 61, "[Form Recipe] Assemble Mode Set Use Normal Type");
                    break;

                case "btnUseGap":
                    cDEF.Work.Project.GlobalOption.UseGap = !cDEF.Work.Project.GlobalOption.UseGap;
                    if (cDEF.Work.Project.GlobalOption.UseGap)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 62, "[Form Recipe] Use Gap Set Use");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 63, "[Form Recipe] Use Gap Set Not Use");
                    break;

                case "btnSecondaryCorrection":
                    cDEF.Work.Project.GlobalOption.UseSecondaryCorrection = !cDEF.Work.Project.GlobalOption.UseSecondaryCorrection;
                    if (cDEF.Work.Project.GlobalOption.UseSecondaryCorrection)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 62, "[Form Recipe] Use Secondary Correction Set Use");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 63, "[Form Recipe] Use Secondary Correction Set Not Use");
                    break;

                case "btnUseActNotAction1":
                    cDEF.Work.Project.GlobalOption.UseActAction1 = !cDEF.Work.Project.GlobalOption.UseActAction1;
                    if (cDEF.Work.Project.GlobalOption.UseActAction1)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 64, "[Form Recipe] Use ACTUATING 1 NOT ACTION Set Use");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 65, "[Form Recipe] Use ACTUATING 1 NOT ACTION Set Not Use");
                    break;
                case "btnUseActNotAction2":
                    cDEF.Work.Project.GlobalOption.UseActAction2 = !cDEF.Work.Project.GlobalOption.UseActAction2;
                    if (cDEF.Work.Project.GlobalOption.UseActAction2)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 66, "[Form Recipe] Use ACTUATING 2 NOT ACTION Set Use");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 67, "[Form Recipe] Use ACTUATING 2 NOT ACTION Set Not Use");
                    break;
                case "btnUseResultRowDataJudge":
                    cDEF.Work.Project.GlobalOption.PlateAngleResultRowJudge = !cDEF.Work.Project.GlobalOption.PlateAngleResultRowJudge;
                    if (cDEF.Work.Project.GlobalOption.PlateAngleResultRowJudge)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 66, "[Form Recipe] Use PlateAngle Result RowData Judge Set");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 67, "[Form Recipe] PlateAngle Result RowData Judge Set Not Use");
                    break;
                case "btnUseMES":
                    cDEF.Work.Project.GlobalOption.UseMES = !cDEF.Work.Project.GlobalOption.UseMES;
                    if (cDEF.Work.Project.GlobalOption.UseMES)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 68, "[Form Recipe] Use MES Set Use");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 69, "[Form Recipe] Use MES Set Not Use");

                    cDEF.Mes.Send_MESOtpion();
                    break;

                case "btnPreAct":
                    cDEF.Work.Project.GlobalOption.UsePreActuating = !cDEF.Work.Project.GlobalOption.UsePreActuating;
                    if (cDEF.Work.Project.GlobalOption.UsePreActuating)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 68, "[Form Recipe] Use PreActuating Set Use");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 69, "[Form Recipe] Use PreActuating Set Not Use");
                    break;

                case "btnUseLensPicker":
                    cDEF.Work.Project.GlobalOption.UseLensPicker = !cDEF.Work.Project.GlobalOption.UseLensPicker;
                    if (cDEF.Work.Project.GlobalOption.UseLensPicker)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 68, "[Form Recipe] Use Lens Picker Set Use");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 69, "[Form Recipe] Use Lens Picker Set Not Use");
                    break;

                case "btnUseAct3":
                    cDEF.Work.Project.GlobalOption.UseActAction3 = !cDEF.Work.Project.GlobalOption.UseActAction3;
                    if (cDEF.Work.Project.GlobalOption.UseActAction3)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 68, "[Form Recipe] Use ACT 3 Set Use");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 69, "[Form Recipe] Use ACT 3 Set Not Use");
                    break;
                case "btnUseCooling1":
                    cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1 = !cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1;
                    if (cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                    {
                        cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = true;
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 90, "[Form Recipe] Set Use Use TJV #1 Cooling");
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = false;
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 91, "[Form Recipe] Set Not  Use Use TJV #1 Cooling");
                    }

                    break;
                case "btnUseCooling2":
                    cDEF.Work.Project.GlobalOption.Use_TJV_Cooling2 = !cDEF.Work.Project.GlobalOption.Use_TJV_Cooling2;
                    if (cDEF.Work.Project.GlobalOption.Use_TJV_Cooling2)
                    {
                        cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = true;
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 92, "[Form Recipe] Set Use Use TJV #2 Cooling");
                    }
                    else
                    {
                        cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = false;
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 93, "[Form Recipe] Set Not  Use Use TJV #2 Cooling");
                    }
                    break;
            }
            cDEF.Work.Project.GlobalOption.Save(cDEF.Work.Project.FileName);

        }

        private void BtnLanguage_Click(object sender, EventArgs e)
        {
            int Value;
            Value = cDEF.Work.Option.Language;
            if (XModuleMain.frmBox.SelectBox("STYLE", "KOREA,ENGLISH,CHINA,VIETNAM", ref Value) == DialogResult.No)
                return;
            cDEF.Lang.type = (eLanguage)Value;
            cDEF.Work.Option.Language = Value;
            cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
            cDEF.Lang.Load();
            cDEF.ChangeLanguage();
        }


        public void ChangeLanguage()
        {
            btnOpen.Text = cDEF.Lang.Trans("OPEN");
            btnSaveAs.Text = cDEF.Lang.Trans("SAVE AS");
            btnDelete.Text = cDEF.Lang.Trans("DELETE");
            gbMachineOption.Text = cDEF.Lang.Trans("Machine Option");
            gbRunOption.Text = cDEF.Lang.Trans("Run Option");
            gbLanguageOption.Text = cDEF.Lang.Trans("Language Option");
            gbSequenceOption.Text = cDEF.Lang.Trans("Sequence Option");
            tabPageGeneral.Text = cDEF.Lang.Trans("   General   ");
            btnUseJigPlateAngle.Text = cDEF.Lang.Trans("USE JIG FLATNESS");
            btnUsePlateAngle.Text = cDEF.Lang.Trans("USE SIDE ANGLE");
            btnUseCleanJig.Text = cDEF.Lang.Trans("USE CLEAN JIG");
            btnUseLensHeight.Text = cDEF.Lang.Trans("USE LENS HEIGHT");

            if (cDEF.Work.Option.Language == (int)eLanguage.KOREA)
            {
                btnLanguage.Text = cDEF.Lang.Trans("KOREAN");
            }
            else if (cDEF.Work.Option.Language == (int)eLanguage.CHINA)
            {
                btnLanguage.Text = cDEF.Lang.Trans("CHINESE");
            }
            else if (cDEF.Work.Option.Language == (int)eLanguage.ENGLISH)
            {
                btnLanguage.Text = cDEF.Lang.Trans("ENGLISH");
            }
            else if (cDEF.Work.Option.Language == (int)eLanguage.VIETNAM)
            {
                btnLanguage.Text = cDEF.Lang.Trans("VIETNAMESE");
            }
        }

        private void btnLoaderMagazine_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;
            string FileName = grid.Rows[Selected].Cells[1].Value.ToString();
            
            switch (FName)
            {
                case "btnLoaderMagazine":
                    cDEF.Work.Recipe.LoaderMagazineDirection = !cDEF.Work.Recipe.LoaderMagazineDirection;
                    if (cDEF.Work.Recipe.LoaderMagazineDirection)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 35, "Set Loader Magazine Work Direction From Top");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 36, "Set Loader Magazine Work Direction From Bottom");
                    break;
                case "btnLensMagazine":
                    cDEF.Work.Recipe.LensMagazineDirection = !cDEF.Work.Recipe.LensMagazineDirection;
                    if (cDEF.Work.Recipe.LensMagazineDirection)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 37, "Set Lens Insert Magazine Work Direction From Top");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 38, "Set Lens Insert Magazine Work Direction From Bottom");
                    break;
                case "btnUnloaderMagazine":
                    //cDEF.Run.LensPicker.HeadZ.AbsolueTorqueUse = !cDEF.Run.LensPicker.HeadZ.AbsolueTorqueUse;
                    cDEF.Work.Recipe.UnLoaderMagazineDirection = !cDEF.Work.Recipe.UnLoaderMagazineDirection;
                    if (cDEF.Work.Recipe.UnLoaderMagazineDirection)
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 39, "Set UnLoader Magazine Work Direction From Top");
                    else
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 40, "Set UnLoader Magazine Work Direction From Bottom");
                    break;

                case "btnLensInsertTorque":
                    cDEF.Work.Recipe.LensInsertTorqueUse = !cDEF.Work.Recipe.LensInsertTorqueUse;
                    if (cDEF.Work.Recipe.LensInsertTorqueUse)
                    {
                        
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 39, "Lens Insert Torque Use");
                    }
                    else
                    {
                        cDEF.Run.LensPicker.HeadZ.TorqueUse = false;
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 40, "Lens Insert Torque Not Use");
                    }
                    break;
                case "btnLensPickUpTorque":
                    cDEF.Work.Recipe.LensPickUpTorqueUse = !cDEF.Work.Recipe.LensPickUpTorqueUse;
                    if (cDEF.Work.Recipe.LensPickUpTorqueUse)
                    {

                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 39, "Lens Pick Up Torque Use");
                    }
                    else
                    {
                        cDEF.Run.LensPicker.HeadZ.TorqueUse = false;
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 40, "Lens Pick Up Torque Not Use");
                    }
                    break;

                case "btnLensThetaTorque":
                    cDEF.Work.Recipe.LensPickUpThetaTorqueUse = !cDEF.Work.Recipe.LensPickUpThetaTorqueUse;
                    if (cDEF.Work.Recipe.LensPickUpThetaTorqueUse)
                    {
                        cDEF.Run.LensPicker.HeadT.SetTorquePara(cDEF.Work.Option.TorqueLimitTheta, 5000, true);
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 41, "Lens Pick Up Theta Torque Use");
                    }
                    else
                    {
                        cDEF.Run.LensPicker.HeadT.TorqueUse = false;
                        cDEF.Run.LogEvent(cLog.Form_Recipe_Event + 42, "Lens Pick Up Theta Torque Not Use");
                    }
                    break;
            }
            cDEF.Work.Recipe.Save(FileName);
        }
        private void Jetting1GridInit()
        {
            gridDp1.Rows.Clear();
            int Index = 1;
            float posX = 0;
            float posY = 0;
            if(cDEF.Work.DispSensor.DispenserType == 0)
                gridDp1.Columns[7].HeaderText = "Pluse";
            else
                gridDp1.Columns[7].HeaderText = "DPTime";

            VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
            Graphics grPhoto = Graphics.FromImage(VCMbitmap);
            grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);

            Point result;
            foreach (JettingData Jd in cDEF.Work.Bonder1Point.JetData)
            {
                if (cDEF.Work.DispSensor.DispenserType == 0)
                {
                    string[] str = { $"{Index}", $"{Jd.Radius}", $"{Jd.Angle}", $"{Jd.Enable}", $"{Jd.Delay / 1000.0}", $"{Jd.ZOffset / 1000.0}", $"{Jd.ZUp / 1000.0}", $"{Jd.PluseNum }" };
                    gridDp1.Rows.Add(str);
                }
                else
                {
                    string[] str = { $"{Index}", $"{Jd.Radius}", $"{Jd.Angle}", $"{Jd.Enable}", $"{Jd.Delay / 1000.0}", $"{Jd.ZOffset / 1000.0}", $"{Jd.ZUp / 1000.0}", $"{Jd.DpTime }" };
                    gridDp1.Rows.Add(str);
                }

                // VCM 이미지 표시
                result = cDEF.Run.Bonder1.GetCirclePoint(0, 0, Jd.Radius, Jd.Angle);
                posX = (float)(result.X / 1000.0);
                posY = (float)(result.Y / 1000.0);
                if (Jd.Enable)
                    DispPoint(posX, posY, Index);

                Index++;
            }
            picVCM.Image = VCMbitmap;
            picVCM.Refresh();
        }
        private void Jetting2GridInit()
        {
            gridDp2.Rows.Clear();
            int Index = 1;
            float posX = 0;
            float posY = 0;

            if (cDEF.Work.DispSensor.DispenserType == 0)
                gridDp2.Columns[7].HeaderText = "Pluse";
            else
                gridDp2.Columns[7].HeaderText = "DPTime";

            VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
            Graphics grPhoto = Graphics.FromImage(VCMbitmap);
            grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);

            Point result;
            foreach (JettingData Jd in cDEF.Work.Bonder2Point.JetData)
            {
                if (cDEF.Work.DispSensor.DispenserType == 0)
                {
                    string[] str = { $"{Index}", $"{Jd.Radius}", $"{Jd.Angle}", $"{Jd.Enable}", $"{Jd.Delay / 1000.0}", $"{Jd.ZOffset / 1000.0}", $"{Jd.ZUp / 1000.0}", $"{Jd.PluseNum }" };
                    gridDp2.Rows.Add(str);
                }
                else
                {
                    string[] str = { $"{Index}", $"{Jd.Radius}", $"{Jd.Angle}", $"{Jd.Enable}", $"{Jd.Delay / 1000.0}", $"{Jd.ZOffset / 1000.0}", $"{Jd.ZUp / 1000.0}", $"{Jd.DpTime }" };
                    gridDp2.Rows.Add(str);
                }

                // VCM 이미지 표시
                result = cDEF.Run.Bonder2.GetCirclePoint(0, 0, Jd.Radius, Jd.Angle);
                posX = (float)(result.X / 1000.0);
                posY = (float)(result.Y / 1000.0);
                if (Jd.Enable)
                    DispPoint(posX, posY, Index);

                Index++;
            }

            picVCM.Image = VCMbitmap;
            picVCM.Refresh();
        }

        #region Pattern Grid
        private void Jetting1PLineGridInit()
        {
            gridPLineDp1.Rows.Clear();
            if (cDEF.Work.Bonder1Pattern.JetPatternLineData.Count > 0)
            {
                int Index = 1;

                float posX = 0;
                float posX1 = 0;
                float posY = 0;
                float posY1 = 0;
                bool PosBind = false;

                if (tabControl1.SelectedIndex == 3)
                {
                    VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
                    Graphics grPhoto = Graphics.FromImage(VCMbitmap);
                    grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);
                }
                foreach (JettingLineData Jd in cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData)
                {
                    string[] str = { $"{Index}", $"{Jd.XPos / 1000.0}", $"{Jd.YPos / 1000.0}", $"{Jd.ZPos / 1000.0}", $"{Jd.LineSpeed / 1000.0}", $"{Jd.ZSpeed / 1000.0}", $"{Jd.Shot}" };
                    gridPLineDp1.Rows.Add(str);

                    if (tabControl1.SelectedIndex == 3)
                    {
                        // VCM 이미지 표시
                        if (Jd.Shot)
                        {
                            if (PosBind)
                            {
                                posX1 = (float)(Jd.XPos / 1000.0);
                                posY1 = (float)(Jd.YPos / 1000.0);
                                DispLine(posX, posY, posX1, posY1, Index, 1);
                                PosBind = false;
                            }
                            else
                            {
                                posX = (float)(Jd.XPos / 1000.0);
                                posY = (float)(Jd.YPos / 1000.0);
                                PosBind = true;
                            }
                        }
                    }
                    Index++;
                }
                if (tabControl1.SelectedIndex == 3)
                {
                    picVCM1.Image = VCMbitmap;
                    picVCM1.Refresh();
                }
            }
            PatternLineDraw_All();
        }
        private void Jetting2PLineGridInit()
        {
            gridPLineDp2.Rows.Clear();

            if (cDEF.Work.Bonder2Pattern.JetPatternLineData.Count > 0)
            {
                int Index = 1;

                float posX = 0;
                float posX1 = 0;
                float posY = 0;
                float posY1 = 0;
                bool PosBind = false;

                if (tabControl1.SelectedIndex == 3)
                {
                    VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
                    Graphics grPhoto = Graphics.FromImage(VCMbitmap);
                    grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);
                }

                foreach (JettingLineData Jd in cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData)
                {
                    string[] str = { $"{Index}", $"{Jd.XPos / 1000.0}", $"{Jd.YPos / 1000.0}", $"{Jd.ZPos / 1000.0}", $"{Jd.LineSpeed / 1000.0}", $"{Jd.ZSpeed / 1000.0}", $"{Jd.Shot}" };
                    gridPLineDp2.Rows.Add(str);

                    if (tabControl1.SelectedIndex == 3)
                    {
                        // VCM 이미지 표시
                        if (Jd.Shot)
                        {
                            if (PosBind)
                            {
                                posX1 = (float)(Jd.XPos / 1000.0);
                                posY1 = (float)(Jd.YPos / 1000.0);
                                DispLine(posX, posY, posX1, posY1, Index, 2);
                                PosBind = false;
                            }
                            else
                            {
                                posX = (float)(Jd.XPos / 1000.0);
                                posY = (float)(Jd.YPos / 1000.0);
                                PosBind = true;
                            }
                        }
                    }
                    Index++;
                }
                if (tabControl1.SelectedIndex == 3)
                {
                    picVCM2.Image = VCMbitmap;
                    picVCM2.Refresh();
                }
            }
            PatternLineDraw_All();
        }
        private void gridDp1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str;
            DataGridView Grid = (DataGridView)sender;
            int Col = Grid.CurrentCell.ColumnIndex;
            int Row = Grid.CurrentCell.RowIndex;
            double DValue = 0.0;
            lblSelectRowIdx1.Text = (Row + 1).ToString();
            switch (Col)
            {
                // Radius
                case 1:
                    DValue = cDEF.Work.Bonder1Point.JetData[Row].Radius;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Radius", ref DValue, " mm", 0, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Radius {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Point.JetData[Row].Radius , DValue);
                        cDEF.Work.Bonder1Point.JetData[Row].Radius = DValue;
                        cDEF.Work.Bonder1Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 36, str);
                    }
                    break;

                // Angle
                case 2:
                    DValue = cDEF.Work.Bonder1Point.JetData[Row].Angle;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Angle", ref DValue, " º", -360, 360))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Angle {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Point.JetData[Row].Angle, DValue);
                        cDEF.Work.Bonder1Point.JetData[Row].Angle = DValue;
                        cDEF.Work.Bonder1Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 37, str);
                    }
                    break;

                // Enable
                case 3:
                    cDEF.Work.Bonder1Point.JetData[Row].Enable = !cDEF.Work.Bonder1Point.JetData[Row].Enable;
                    cDEF.Work.Bonder1Point.Save();
                    str = $"[Bonder #1] Bonder #1 Jet Enable {cDEF.Work.Bonder1Point.JetData[Row].Enable} Changed.";
                    cDEF.Run.LogData(cLog.Form_Bonder_Data + 38, str);
                    break;

                // Delay
                case 4:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder1Point.JetData[Row].Delay) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Delay", ref DValue, " Sec", 0, 5))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Delay {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Point.JetData[Row].Delay / 1000.0, DValue);
                        cDEF.Work.Bonder1Point.JetData[Row].Delay = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 39, str);
                    }
                    break;

                // ZOffset
                case 5:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder1Point.JetData[Row].ZOffset) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet ZOffset", ref DValue, " mm", 0, 5))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet ZOffset {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Point.JetData[Row].ZOffset / 1000.0, DValue);
                        cDEF.Work.Bonder1Point.JetData[Row].ZOffset = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 40, str);
                    }
                    break;

                // ZUp
                case 6:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder1Point.JetData[Row].ZUp) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet ZUp", ref DValue, " mm", 0, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet ZUp {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Point.JetData[Row].ZUp / 1000.0, DValue);
                        cDEF.Work.Bonder1Point.JetData[Row].ZUp = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                    }
                    break;
                case 7:
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                    {
                        int IValue = Convert.ToInt32(cDEF.Work.Bonder1Point.JetData[Row].PluseNum);
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #1 Jet PluseNum ", ref IValue, "", 0, 1000))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet PluseNum {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Point.JetData[Row].PluseNum, IValue);
                            cDEF.Work.Bonder1Point.JetData[Row].PluseNum = IValue;
                            cDEF.Work.Bonder1Point.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 42, str);
                        }
                    }
                    else
                    {
                        int IValue = Convert.ToInt32(cDEF.Work.Bonder1Point.JetData[Row].DpTime);
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #1 Dispensing Time ", ref IValue, " msec", 0, 1000))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Dispensing Time {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Point.JetData[Row].DpTime, IValue);
                            cDEF.Work.Bonder1Point.JetData[Row].DpTime = IValue;
                            cDEF.Work.Bonder1Point.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 43, str);
                        }
                    }
                    break;
            }
            Jetting1GridInit();
        }
        private void gridDp2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str;
            DataGridView Grid = (DataGridView)sender;
            int Col = Grid.CurrentCell.ColumnIndex;
            int Row = Grid.CurrentCell.RowIndex;
            double DValue = 0.0;

            lblSelectRowIdx2.Text = (Row + 1).ToString();

            switch (Col)
            {
                // Radius
                case 1:
                    DValue = cDEF.Work.Bonder2Point.JetData[Row].Radius;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Radius", ref DValue, " mm", 0, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Radius {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Point.JetData[Row].Radius, DValue);
                        cDEF.Work.Bonder2Point.JetData[Row].Radius = DValue;
                        cDEF.Work.Bonder2Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 36, str);
                    }
                    break;

                // Angle
                case 2:
                    DValue = cDEF.Work.Bonder2Point.JetData[Row].Angle;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Angle", ref DValue, " º", -360, 360))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Angle {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Point.JetData[Row].Angle, DValue);
                        cDEF.Work.Bonder2Point.JetData[Row].Angle = DValue;
                        cDEF.Work.Bonder2Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 37, str);
                    }
                    break;

                // Enable
                case 3:
                    cDEF.Work.Bonder2Point.JetData[Row].Enable = !cDEF.Work.Bonder2Point.JetData[Row].Enable;
                    cDEF.Work.Bonder2Point.Save();
                    str = $"[Bonder #2] Bonder #2 Jet Enable {cDEF.Work.Bonder2Point.JetData[Row].Enable} Changed.";
                    cDEF.Run.LogData(cLog.Form_Bonder_Data + 38, str);
                    break;

                // Delay
                case 4:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder2Point.JetData[Row].Delay) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Delay", ref DValue, " Sec", 0, 5))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Delay {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Point.JetData[Row].Delay / 1000.0, DValue);
                        cDEF.Work.Bonder2Point.JetData[Row].Delay = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 39, str);
                    }
                    break;

                // ZOffset
                case 5:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder2Point.JetData[Row].ZOffset) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet ZOffset", ref DValue, " mm", 0, 5))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet ZOffset {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Point.JetData[Row].ZOffset / 1000.0, DValue);
                        cDEF.Work.Bonder2Point.JetData[Row].ZOffset = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 40, str);
                    }
                    break;

                // ZUp
                case 6:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder2Point.JetData[Row].ZUp) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet ZUp", ref DValue, " mm", 0, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet ZUp {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Point.JetData[Row].ZUp / 1000.0, DValue);
                        cDEF.Work.Bonder2Point.JetData[Row].ZUp = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2Point.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                    }
                    break;
                case 7:
                    if (cDEF.Work.DispSensor.DispenserType == 0)
                    {
                        int IValue = Convert.ToInt32(cDEF.Work.Bonder2Point.JetData[Row].PluseNum);
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #1 Jet PluseNum ", ref IValue, "", 0, 1000))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Jet PluseNum {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Point.JetData[Row].PluseNum, IValue);
                            cDEF.Work.Bonder2Point.JetData[Row].PluseNum = IValue;
                            cDEF.Work.Bonder2Point.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 42, str);
                        }
                    }
                    else
                    {
                        int IValue = Convert.ToInt32(cDEF.Work.Bonder2Point.JetData[Row].DpTime);
                        if (!XModuleMain.frmBox.fpIntegerEdit($"Bonder #1 Dispensing Time ", ref IValue, " msec", 0, 1000))
                            return;
                        {
                            str = String.Format($"[Bonder #1] Bonder #1 Dispensing Time {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Point.JetData[Row].DpTime, IValue);
                            cDEF.Work.Bonder2Point.JetData[Row].DpTime = IValue;
                            cDEF.Work.Bonder2Point.Save();
                            cDEF.Run.LogData(cLog.Form_Bonder_Data + 43, str);
                        }
                    }
                    break;
            }
            Jetting2GridInit();
        }
        #endregion  Pattern Grid

        #region Arc Grid
        private void Jetting1PArcGridInit(int Row = 0)
        {
            try
            {
                gridPArcDp1.Rows.Clear();
                if (cDEF.Work.Bonder1ARC.JetPatternArcData.Count > 0)
                {
                    int Index = 1;

                    float posX = 0;
                    float posX1 = 0;
                    float posY = 0;
                    float posY1 = 0;
                    bool PosBind = false;

       
                    if (tabControl1.SelectedIndex == 4)
                    {
                        VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
                        Graphics grPhoto = Graphics.FromImage(VCMbitmap);
                        grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);
                    }

                    foreach (JettingArcData Jd in cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData)
                    {
                        if (Jd.JetType == eJetType.Line)
                        {
                            string[] str = { $"{Index}", $"{Jd.XPos / 1000.0}", $"{Jd.YPos / 1000.0}", $"{Jd.ZPos / 1000.0}","-" ,$"{Jd.LineSpeed / 1000.0}", $"{Jd.Shot}" , "LINE"};
                            gridPArcDp1.Rows.Add(str);
                        }
                        else if(Jd.JetType == eJetType.Arc)
                        {
                            string[] str = { $"{Index}", $"-", $"-", $"{Jd.ZPos / 1000.0}", $"{Jd.Angle}",$"{Jd.LineSpeed / 1000.0}", $"{Jd.Shot}", "ARC" };
                            gridPArcDp1.Rows.Add(str);
                        }

                        if (tabControl1.SelectedIndex == 4)
                        {
                            if (Index >= 1)
                            {
                                if (Jd.JetType == eJetType.Arc)
                                {
                                    if (Jd.Shot)
                                    {
                                        DispArc(posX, posY, (float)Jd.Angle, Index, 1);
                                        PosBind = false;
                                    }
                                }
                                else if(Jd.JetType == eJetType.Line)
                                {
                                    if (Jd.Shot)
                                    {
                                        //if (PosBind)
                                        {
                                            posX1 = (float)(Jd.XPos / 1000.0);
                                            posY1 = (float)(Jd.YPos / 1000.0);
                                            DispLine(posX, posY, posX1, posY1, Index, 1);
                                            posX = (float)(Jd.XPos / 1000.0);
                                            posY = (float)(Jd.YPos / 1000.0);

                                            PosBind = false;
                                        }
                                        //else
                                        //{
                                        //    posX = (float)(Jd.XPos / 1000.0);
                                        //    posY = (float)(Jd.YPos / 1000.0);
                                        //    PosBind = true;
                                        //}
                                    }
                                    posX = (float)(Jd.XPos / 1000.0);
                                    posY = (float)(Jd.YPos / 1000.0);
                                }
                            }
                        }
                        Index++;
                    }

                    if (tabControl1.SelectedIndex == 4)
                    {
                        picVCMArc1.Image = VCMbitmap;
                        picVCMArc1.Refresh();
                    }

                    gridPArcDp1.FirstDisplayedScrollingRowIndex = Row;
                    //int rowidx = -1;
                    //int.TryParse(lblSelectRowIdx1PArc.Text, out rowidx);
                    //if (rowidx != 0)
                    //{
                    //    rowidx--;
                    //    gridPArcDp1.FirstDisplayedScrollingRowIndex = rowidx;
                    //    gridPArcDp1.Rows[rowidx].Selected = true;

                    //}
                }

            }
            catch(Exception e)
            {

            }
            PatternArcDraw_All();
}
        private void Jetting2PArcGridInit(int Row = 0)
        {
            try
            {
                gridPArcDp2.Rows.Clear();

                if (cDEF.Work.Bonder2ARC.JetPatternArcData.Count > 0)
                {
                    int Index = 1;
                    float posX = 0;
                    float posX1 = 0;
                    float posY = 0;
                    float posY1 = 0;
                    bool PosBind = false;



                    if (tabControl1.SelectedIndex == 4)
                    {
                        VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
                        Graphics grPhoto = Graphics.FromImage(VCMbitmap);
                        grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);
                    }

                    foreach (JettingArcData Jd in cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData)
                    {
                        if (Jd.JetType == eJetType.Line)
                        {
                            string[] str = { $"{Index}", $"{Jd.XPos / 1000.0}", $"{Jd.YPos / 1000.0}", $"{Jd.ZPos / 1000.0}", "-", $"{Jd.LineSpeed / 1000.0}", $"{Jd.Shot}", "LINE" };
                            gridPArcDp2.Rows.Add(str);
                        }
                        else if (Jd.JetType == eJetType.Arc)
                        {
                            string[] str = { $"{Index}", $"-", $"-", $"{Jd.ZPos / 1000.0}", $"{Jd.Angle}", $"{Jd.LineSpeed / 1000.0}", $"{Jd.Shot}", "ARC" };
                            gridPArcDp2.Rows.Add(str);
                        }

                        if (tabControl1.SelectedIndex == 4)
                        {
                            if (Index >= 1)
                            {
                                if (Jd.JetType == eJetType.Arc)
                                {
                                    if (Jd.Shot)
                                    {
                                        DispArc(posX, posY, (float)Jd.Angle, Index, 2);
                                        PosBind = false;
                                    }
                                }
                                else if (Jd.JetType == eJetType.Line)
                                {
                                    if (Jd.Shot)
                                    {
                                        //if (PosBind)
                                        {
                                            posX1 = (float)(Jd.XPos / 1000.0);
                                            posY1 = (float)(Jd.YPos / 1000.0);
                                            DispLine(posX, posY, posX1, posY1, Index, 1);
                                            posX = (float)(Jd.XPos / 1000.0);
                                            posY = (float)(Jd.YPos / 1000.0);

                                            PosBind = false;
                                        }
                                        //else
                                        //{
                                        //    posX = (float)(Jd.XPos / 1000.0);
                                        //    posY = (float)(Jd.YPos / 1000.0);
                                        //    PosBind = true;
                                        //}
                                    }
                                    posX = (float)(Jd.XPos / 1000.0);
                                    posY = (float)(Jd.YPos / 1000.0);
                                }
                            }
                        }
                        Index++;
                    }
                    if (tabControl1.SelectedIndex == 4)
                    {
                        picVCMArc2.Image = VCMbitmap;
                        picVCMArc2.Refresh();
                    }

                    gridPArcDp2.FirstDisplayedScrollingRowIndex = Row;
                    //int rowidx = -1;
                    //int.TryParse(lblSelectRowIdx2PArc.Text, out rowidx);
                    //if (rowidx != 0)
                    //{
                    //    rowidx--;

                    //    gridPArcDp2.Rows[rowidx].Selected = true;

                    //}
                }
            }
            catch(Exception e)
            {

            }
            PatternArcDraw_All();
        }
        private void gridPArcDp1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str;
            DataGridView Grid = (DataGridView)sender;
            int Col = Grid.CurrentCell.ColumnIndex;
            int Row = Grid.CurrentCell.RowIndex;
            double DValue = 0.0;
            lblSelectRowIdx1PArc.Text = (Row + 1).ToString();
            switch (Col)
            {
                // X Pos
                case 1:
                    DValue = cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].XPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet X Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet X Position {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].XPos / 1000.0, DValue);
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].XPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 50, str);
                    }
                    break;

                // Y Pos
                case 2:
                    DValue = cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].YPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Y Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Y Position {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].YPos / 1000.0, DValue);
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].YPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 51, str);
                    }
                    break;
                // Z Pos
                case 3:
                    DValue = cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].ZPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Z Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Z Position {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].ZPos / 1000.0, DValue);
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].ZPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 52, str);
                    }
                    break;
                // Z Angle
                case 4:
                    DValue = cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].Angle;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Z Angle", ref DValue, " ", -360, 360))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Z Angle {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].Angle, DValue);
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].Angle = DValue;
                        cDEF.Work.Bonder1ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 52, str);
                    }
                    break;
                // Arc Speed
                case 5:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].LineSpeed) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Arc Speed", ref DValue, " mm/sec", 0, 500))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Arc Speed {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].LineSpeed / 1000.0, DValue);
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].LineSpeed = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 54, str);
                    }
                    break;

                // Shot
                case 6:
                    cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].Shot = !cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].Shot;
                    cDEF.Work.Bonder1ARC.Save();
                    str = $"[Bonder #1] Bonder #1 Jet ArcData Shot { cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].Shot} Changed.";
                    cDEF.Run.LogData(cLog.Form_Bonder_Data + 56, str);
                    break;

                    // Type
                case 7:
                    if (cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].JetType == eJetType.Arc)
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].JetType = eJetType.Line;
                    else
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].JetType = eJetType.Arc;
                    cDEF.Work.Bonder1ARC.Save();
                    str = $"[Bonder #1] Bonder #1 Jet ArcData Type { (cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData[Row].JetType == eJetType.Arc ? "ARC":"LINE")} Changed.";
                    cDEF.Run.LogData(cLog.Form_Bonder_Data + 56, str);
                    break;
            }
            //Jetting1PArcGridInit();
            Jetting1PArcGridInit(gridPArcDp1.FirstDisplayedScrollingRowIndex);
        }

        private void gridPArcDp2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str;
            DataGridView Grid = (DataGridView)sender;
            int Col = Grid.CurrentCell.ColumnIndex;
            int Row = Grid.CurrentCell.RowIndex;
            double DValue = 0.0;
            lblSelectRowIdx2PArc.Text = (Row + 1).ToString();
            switch (Col)
            {
                // X Pos
                case 1:
                    DValue = cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].XPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet X Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet X Position {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].XPos / 1000.0, DValue);
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].XPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 50, str);
                    }
                    break;

                // Y Pos
                case 2:
                    DValue = cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].YPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Y Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Y Position {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].YPos / 1000.0, DValue);
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].YPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 51, str);
                    }
                    break;
                
                // Z Pos
                case 3:
                    DValue = cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].ZPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Z Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Z Position {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].ZPos / 1000.0, DValue);
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].ZPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 52, str);
                    }
                    break;
                case 4:
                    DValue = cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].Angle;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Z Angle", ref DValue, " ", -360, 360))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #2 Jet Z Angle {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].Angle, DValue);
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].Angle = DValue;
                        cDEF.Work.Bonder2ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 52, str);
                    }
                    break;
                // Arc Speed
                case 5:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].LineSpeed) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Arc Speed", ref DValue, " mm/sec", 0, 500))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Arc Speed {Row + 1} {0:0.000} to {1:0.000}"
                            , cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].LineSpeed / 1000.0, DValue);
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].LineSpeed = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2ARC.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 54, str);
                    }
                    break;

                // Shot
                case 6:
                    cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].Shot = !cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].Shot;
                    cDEF.Work.Bonder2ARC.Save();
                    str = $"[Bonder #2] Bonder #2 Jet ArcData Shot { cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].Shot} Changed.";
                    cDEF.Run.LogData(cLog.Form_Bonder_Data + 56, str);
                    break;

                // Type
                case 7:
                    if (cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].JetType == eJetType.Arc)
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].JetType = eJetType.Line;
                    else
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].JetType = eJetType.Arc;
                    cDEF.Work.Bonder2ARC.Save();
                    str = $"[Bonder #2] Bonder #2 Jet ArcData Type { (cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData[Row].JetType == eJetType.Arc ? "ARC" : "LINE")} Changed.";
                    cDEF.Run.LogData(cLog.Form_Bonder_Data + 56, str);
                    break;
            }
            Jetting2PArcGridInit(gridPArcDp2.FirstDisplayedScrollingRowIndex);
        }
        #endregion  Arc Grid
        private void btnItemAdd_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;
            string FButton = string.Empty;
            JettingData jd;
            JettingLineData jld;
            int sidx;
            switch (FName)
            {
                case "btnItemAdd":
                    jd = new JettingData();
                    cDEF.Work.Bonder1Point.JetData.Add(jd);
                    cDEF.Work.Bonder1Point.Save();
                    Jetting1GridInit();
                    break;
                case "btnSelectItemAdd":
                    jd = new JettingData();
                    
                    int.TryParse(lblSelectRowIdx1.Text, out sidx);
                    //sidx += 2;

                    cDEF.Work.Bonder1Point.JetData.Insert(sidx, jd);
                    cDEF.Work.Bonder1Point.Save();
                    Jetting1GridInit();
                    break;
                case "btnItemDelete":
                    if (cDEF.Work.Bonder1Point.JetData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {

                        cDEF.Work.Bonder1Point.JetData.RemoveAt(cDEF.Work.Bonder1Point.JetData.Count - 1);
                        cDEF.Work.Bonder1Point.Save();
                        Jetting1GridInit();
                    }
                    break;
                case "btnSelectItemDelete":
                    if (cDEF.Work.Bonder1Point.JetData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        int.TryParse(lblSelectRowIdx1.Text, out sidx);
                        sidx -= 1;
                        cDEF.Work.Bonder1Point.JetData.RemoveAt(sidx);
                        cDEF.Work.Bonder1Point.Save();
                        Jetting1GridInit();
                    }
                    break;

                case "btnItemAdd1":
                    jd = new JettingData();
                    cDEF.Work.Bonder2Point.JetData.Add(jd);
                    cDEF.Work.Bonder2Point.Save();
                    Jetting2GridInit();
                    break;

                case "btnSelectItemAdd1":
                    jd = new JettingData();
                    int.TryParse(lblSelectRowIdx2.Text, out sidx);
                    //sidx += 2;

                    cDEF.Work.Bonder2Point.JetData.Insert(sidx, jd);
                    cDEF.Work.Bonder2Point.Save();
                    Jetting2GridInit();
                    break;

                case "btnItemDelete1":
                    if (cDEF.Work.Bonder2Point.JetData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {

                        cDEF.Work.Bonder2Point.JetData.RemoveAt(cDEF.Work.Bonder2Point.JetData.Count - 1);
                        cDEF.Work.Bonder2Point.Save();
                        Jetting2GridInit();
                    }
                    break;
                case "btnSelectItemDelete1":
                    if (cDEF.Work.Bonder2Point.JetData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        int.TryParse(lblSelectRowIdx2.Text, out sidx);
                        sidx -= 1;
                        cDEF.Work.Bonder2Point.JetData.RemoveAt(sidx);
                        cDEF.Work.Bonder2Point.Save();
                        Jetting2GridInit();
                    }
                    break;
            }
        }


        private void btnPLineItemAdd_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;
            string FButton = string.Empty;
            JettingData jd;
            JettingLineData jld;
            int sidx;
            switch (FName)
            {
                
                case "btnPLine1ItemAdd":
                    jld = new JettingLineData();
                    cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData.Add(jld);
                    cDEF.Work.Bonder1Pattern.Save();
                    Jetting1PLineGridInit();
                    break;
                case "btnPLine1SelectItemAdd":
                    jld = new JettingLineData();
                    int.TryParse(lblSelectRowIdx1PLine.Text, out sidx);
                    //sidx += 2;
                    cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData.Insert(sidx, jld);
                    cDEF.Work.Bonder1Pattern.Save();
                    Jetting1PLineGridInit();
                    break;
                case "btnPLine1ItemDelete":
                    if (cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Line Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData.RemoveAt(cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData.Count - 1);

                        cDEF.Work.Bonder1Pattern.Save();
                        Jetting1PLineGridInit();
                    }
                    break;
                case "btnPLine1SelectItemDelete":
                    if (cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Line Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        int.TryParse(lblSelectRowIdx1PLine.Text, out sidx);
                        sidx -= 1;
                        cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData.RemoveAt(sidx);
                        cDEF.Work.Bonder1Pattern.Save();
                        Jetting1PLineGridInit();
                    }
                    break;
                case "btnPLine2ItemAdd":
                    jld = new JettingLineData();
                    cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData.Add(jld);
                    cDEF.Work.Bonder2Pattern.Save();
                    Jetting2PLineGridInit();
                    break;
                case "btnPLine2SelectItemAdd":
                    jld = new JettingLineData();
                    int.TryParse(lblSelectRowIdx2PLine.Text, out sidx);
                    //sidx += 2;
                    cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData.Insert(sidx, jld);
                    cDEF.Work.Bonder2Pattern.Save();
                    Jetting2PLineGridInit();
                    break;
                case "btnPLine2ItemDelete":
                    if (cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Line Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData.RemoveAt(cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData.Count - 1);
                        cDEF.Work.Bonder2Pattern.Save();
                        Jetting2PLineGridInit();
                    }
                    break;
                case "btnPLine2SelectItemDelete":
                    if (cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Line Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        int.TryParse(lblSelectRowIdx2PLine.Text, out sidx);
                        sidx -= 1;
                        cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData.RemoveAt(sidx);
                        cDEF.Work.Bonder2Pattern.Save();
                        Jetting2PLineGridInit();
                    }
                    break;
            }
        }

        private void btnPArcItemAdd_Click(object sender, EventArgs e)
        {
          
            string FName = (sender as Glass.GlassButton).Name;
            string FButton = string.Empty;
            JettingData jd;
            JettingArcData jld;
            int sidx;
            switch (FName)
            {

                case "btnPArc1ItemAdd":
                    if (cDEF.Work.Bonder1ARC.JetPatternArcData.Count == 0)
                    {
                        return;
                    }
                    jld = new JettingArcData();
                    cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData.Add(jld);
                    cDEF.Work.Bonder1ARC.Save();
                    Jetting1PArcGridInit();
                    break;                   
                case "btnPArc1SelectItemAdd":
                    if (cDEF.Work.Bonder1ARC.JetPatternArcData.Count == 0)
                    {
                        return;
                    }
                    jld = new JettingArcData();
                    int.TryParse(lblSelectRowIdx1PArc.Text, out sidx);
                    //sidx += 2;
                    cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData.Insert(sidx, jld);
                    cDEF.Work.Bonder1ARC.Save();
                    Jetting1PArcGridInit();
                    break;
                case "btnPArc1ItemDelete":
                    if (cDEF.Work.Bonder1ARC.JetPatternArcData.Count == 0)
                    {
                        return;
                    }
                    if (PArcSelectedNum1 >= gridPArcDp1.RowCount && gridPArcDp1.RowCount > 0)
                        PArcSelectedNum1 = gridPArcDp1.RowCount - 1;
                    if (cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Arc Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData.RemoveAt(cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData.Count - 1);
                        if(PArcSelectedNum1 > 0)
                            PArcSelectedNum1--;
                        cDEF.Work.Bonder1ARC.Save();
                        Jetting1PArcGridInit();
                    }
                    break;
                case "btnPArc1SelectItemDelete":
                    if (cDEF.Work.Bonder1ARC.JetPatternArcData.Count == 0)
                    {
                        return;
                    }
                    if (cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Arc Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Selected Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        int.TryParse(lblSelectRowIdx1PArc.Text, out sidx);
                        sidx -= 1;
                        cDEF.Work.Bonder1ARC.JetPatternArcData[PArcSelectedNum1].JetArcData.RemoveAt(sidx);
                        if (PArcSelectedNum1 > 0)
                            PArcSelectedNum1--;
                        cDEF.Work.Bonder1ARC.Save();
                        Jetting1PArcGridInit();
                    }
                    break;
                case "btnPArc2ItemAdd":
                    if (cDEF.Work.Bonder2ARC.JetPatternArcData.Count == 0)
                    {
                        return;
                    }
                    jld = new JettingArcData();
                    cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData.Add(jld);
                    cDEF.Work.Bonder2ARC.Save();
                    Jetting2PArcGridInit();
                    break;
                case "btnPArc2SelectItemAdd":
                    if (cDEF.Work.Bonder2ARC.JetPatternArcData.Count == 0)
                    {
                        return;
                    }
                    jld = new JettingArcData();
                    int.TryParse(lblSelectRowIdx2PArc.Text, out sidx);
                    //sidx += 2;
                    cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData.Insert(sidx, jld);
                    cDEF.Work.Bonder2ARC.Save();
                    Jetting2PArcGridInit();
                    break;
                case "btnPArc2ItemDelete":
                    if (cDEF.Work.Bonder2ARC.JetPatternArcData.Count == 0)
                    {
                        return;
                    }
                    if (PArcSelectedNum2 >= gridPArcDp2.RowCount && gridPArcDp2.RowCount > 0)
                        PArcSelectedNum2 = gridPArcDp2.RowCount - 1;
                    if (cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Arc Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Last Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData.RemoveAt(cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData.Count - 1);
                        if (PArcSelectedNum2 > 0)
                            PArcSelectedNum2--;
                        cDEF.Work.Bonder2ARC.Save();
                        Jetting2PArcGridInit();
                    }
                    break;
                case "btnPArc2SelectItemDelete":
                    if (cDEF.Work.Bonder2ARC.JetPatternArcData.Count == 0)
                    {
                        return;
                    }
                    if (cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData.Count == 0)
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Jetting Arc Data is Empty", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Delete", "Do You Want to Delete Selected Item?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        int.TryParse(lblSelectRowIdx2PArc.Text, out sidx);
                        sidx -= 1;
                        cDEF.Work.Bonder2ARC.JetPatternArcData[PArcSelectedNum2].JetArcData.RemoveAt(sidx);
                        if (PArcSelectedNum2 > 0)
                            PArcSelectedNum2--;
                        cDEF.Work.Bonder2ARC.Save();
                        Jetting2PArcGridInit();
                    }
                    break;
            }
        }
        private void gridPLineDp1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str;
            DataGridView Grid = (DataGridView)sender;
            int Col = Grid.CurrentCell.ColumnIndex;
            int Row = Grid.CurrentCell.RowIndex;
            double DValue = 0.0;
            lblSelectRowIdx1PLine.Text = (Row + 1).ToString();
            switch (Col)
            {
                // X Pos
                case 1:
                    DValue = cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].XPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet X Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet X Position {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].XPos / 1000.0, DValue);
                        cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].XPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 36, str);
                    }
                    break;

                // Y Pos
                case 2:
                    DValue = cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].YPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Y Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Y Position {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].YPos / 1000.0, DValue);
                        cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].YPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 37, str);
                    }
                    break;

                // Z Pos
                case 3:
                    DValue = cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].ZPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Z Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Z Position {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].ZPos / 1000.0, DValue);
                        cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].ZPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 38, str);
                    }
                    break;

                // Line Speed
                case 4:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].LineSpeed) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Line Speed", ref DValue, " mm/sec", 0, 500))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Line Speed {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].LineSpeed / 1000.0, DValue);
                        cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].LineSpeed = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 39, str);
                    }
                    break;

                // Z Speed
                case 5:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].ZSpeed) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #1 Jet Z Speed", ref DValue, " mm/sec", 0, 500))
                        return;
                    {
                        str = String.Format($"[Bonder #1] Bonder #1 Jet Z Speed {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].ZSpeed / 1000.0, DValue);
                        cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].ZSpeed = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 40, str);
                    }
                    break;

                // Shot
                case 6:
                    cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].Shot = !cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].Shot;
                    cDEF.Work.Bonder1Pattern.Save();
                    str = $"[Bonder #1] Bonder #1 Jet LineData Shot { cDEF.Work.Bonder1Pattern.JetPatternLineData[PLineSelectedNum1].JetLineData[Row].Shot} Changed.";
                    cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                    break;
            }
            Jetting1PLineGridInit();
        }

        private void gridPLineDp2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string str;
            DataGridView Grid = (DataGridView)sender;
            int Col = Grid.CurrentCell.ColumnIndex;
            int Row = Grid.CurrentCell.RowIndex;
            double DValue = 0.0;
            lblSelectRowIdx2PLine.Text = (Row + 1).ToString();
            switch (Col)
            {
                // X Pos
                case 1:
                    DValue = cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].XPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet X Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet X Position {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].XPos / 1000.0, DValue);
                        cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].XPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 36, str);
                    }
                    break;

                // Y Pos
                case 2:
                    DValue = cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].YPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Y Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Y Position {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].YPos / 1000.0, DValue);
                        cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].YPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 37, str);
                    }
                    break;

                // Z Pos
                case 3:
                    DValue = cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].ZPos / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Z Position", ref DValue, " mm", -10, 10))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Z Position {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].ZPos / 1000.0, DValue);
                        cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].ZPos = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 38, str);
                    }
                    break;

                // Line Speed
                case 4:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].LineSpeed) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Line Speed", ref DValue, " mm/sec", 0, 500))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Line Speed {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].LineSpeed / 1000.0, DValue);
                        cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].LineSpeed = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 39, str);
                    }
                    break;

                // Z Speed
                case 5:
                    DValue = Convert.ToDouble(cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].ZSpeed) / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit($"Bonder #2 Jet Z Speed", ref DValue, " mm/sec", 0, 500))
                        return;
                    {
                        str = String.Format($"[Bonder #2] Bonder #2 Jet Z Speed {Row + 1} {0:0.000} to {1:0.000}", cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].ZSpeed / 1000.0, DValue);
                        cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].ZSpeed = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2Pattern.Save();
                        cDEF.Run.LogData(cLog.Form_Bonder_Data + 40, str);
                    }
                    break;

                // Shot
                case 6:
                    cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].Shot = !cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].Shot;
                    cDEF.Work.Bonder2Pattern.Save();
                    str = $"[Bonder #1] Bonder #2 Jet LineData Shot { cDEF.Work.Bonder2Pattern.JetPatternLineData[PLineSelectedNum2].JetLineData[Row].Shot} Changed.";
                    cDEF.Run.LogData(cLog.Form_Bonder_Data + 41, str);
                    break;
            }
            Jetting2PLineGridInit();
        }

        private void lbTorqueLimitZ_Click(object sender, EventArgs e)
        {
            double DValue = Convert.ToDouble(cDEF.Work.Recipe.LensInsertTorqueLimit) / 1000;
            if (!XModuleMain.frmBox.fpFloatEdit("Lens Insert Torque Limit", ref DValue, " mm", -10, 10))
                return;
            {
                string str = String.Format("[Form Recipe] Lens Insert Torque Limit {0:0.000} to {1:0.000}", cDEF.Work.Recipe.LensInsertTorqueLimit / 1000.0, DValue);
                cDEF.Work.Recipe.LensInsertTorqueLimit = (int)(DValue * 1000.0);
                cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                cDEF.Run.LogData(cLog.Form_Recipe_Data + 4, str);
            }
        }

        private void lblPatCreate1_Click(object sender, EventArgs e)
        {

            string FName = (sender as Label).Name;
            int iValue=0;
            switch (FName)
            {
                #region Pattern
                case "lblPatCreate1":
                    iValue = cDEF.Work.Bonder1Pattern.JetPatternLineCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit($"Lens Pattern Count", ref iValue, " ", 0, 20))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Pattern Count {0} to {1}", cDEF.Work.Bonder1Pattern.JetPatternLineCount, iValue);
                        cDEF.Work.Bonder1Pattern.JetPatternLineCount = iValue;

                        if (iValue == 0)
                        {
                            cDEF.Work.Bonder1Pattern.JetPatternLineData.Clear();
                            PLineSelectedNum1 = 0;
                        }
                        else if (cDEF.Work.Bonder1Pattern.JetPatternLineData.Count < iValue)
                        {
                            for (int i = cDEF.Work.Bonder1Pattern.JetPatternLineData.Count; i < iValue; i++)
                            {
                                JettingPatternLineData jpld = new JettingPatternLineData();
                                cDEF.Work.Bonder1Pattern.JetPatternLineData.Add(jpld);
                            }
                        }
                        else if (iValue < cDEF.Work.Bonder1Pattern.JetPatternLineData.Count)
                        {
                            for (int i = cDEF.Work.Bonder1Pattern.JetPatternLineData.Count ; i > iValue ; i--)
                            {
                                cDEF.Work.Bonder1Pattern.JetPatternLineData.RemoveAt(i - 1);
                            }
                            if (PLineSelectedNum1 > (cDEF.Work.Bonder1Pattern.JetPatternLineData.Count-1))
                            {
                                PLineSelectedNum1 = cDEF.Work.Bonder1Pattern.JetPatternLineData.Count - 1;
                            }
                        }
                       
                       
                        cDEF.Work.Bonder1.Save();

                        Jetting1PLineGridInit();
                    }

                    break;
                case "lblPatCreate2":
                    iValue = cDEF.Work.Bonder2Pattern.JetPatternLineCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit($"Lens Pattern Count", ref iValue, " ", 0, 20))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Pattern Count {0} to {1}", cDEF.Work.Bonder2Pattern.JetPatternLineCount, iValue);
                        cDEF.Work.Bonder2Pattern.JetPatternLineCount = iValue;

                        if (iValue == 0)
                        {
                            cDEF.Work.Bonder2Pattern.JetPatternLineData.Clear();
                            PLineSelectedNum2 = 0;
                        }
                        else if (cDEF.Work.Bonder2Pattern.JetPatternLineData.Count < iValue)
                        {
                            
                            for (int i = cDEF.Work.Bonder2Pattern.JetPatternLineData.Count; i < iValue; i++)
                            {
                                JettingPatternLineData jpld = new JettingPatternLineData();
                                cDEF.Work.Bonder2Pattern.JetPatternLineData.Add(jpld);
                            }
                        }
                        else if (iValue < cDEF.Work.Bonder2Pattern.JetPatternLineData.Count)
                        {
                            for (int i = cDEF.Work.Bonder2Pattern.JetPatternLineData.Count; i > iValue; i--)
                            {
                                cDEF.Work.Bonder2Pattern.JetPatternLineData.RemoveAt(i - 1);
                            }
                            if (PLineSelectedNum2 > (cDEF.Work.Bonder2Pattern.JetPatternLineData.Count - 1))
                            {
                                PLineSelectedNum2 = cDEF.Work.Bonder2Pattern.JetPatternLineData.Count - 1;
                            }
                        }
                        cDEF.Work.Bonder2.Save();

                        Jetting2PLineGridInit();
                    }
                    break;
                #endregion Pattern

                #region ARC
                case "lblArcCreate1":
                    iValue = cDEF.Work.Bonder1ARC.JetPatternArcCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit($"Lens Pattern Count", ref iValue, " ", 0, 20))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Pattern Count {0} to {1}", cDEF.Work.Bonder1ARC.JetPatternArcCount, iValue);
                        cDEF.Work.Bonder1ARC.JetPatternArcCount = iValue;

                        if (iValue == 0)
                        {
                            cDEF.Work.Bonder1ARC.JetPatternArcData.Clear();
                            PLineSelectedNum1 = 0;
                        }
                        else if (cDEF.Work.Bonder1ARC.JetPatternArcData.Count < iValue)
                        {
                            for (int i = cDEF.Work.Bonder1ARC.JetPatternArcData.Count; i < iValue; i++)
                            {
                                JettingPatternArcData jpld = new JettingPatternArcData();
                                cDEF.Work.Bonder1ARC.JetPatternArcData.Add(jpld);
                            }
                        }
                        else if (iValue < cDEF.Work.Bonder1ARC.JetPatternArcData.Count)
                        {
                            for (int i = cDEF.Work.Bonder1ARC.JetPatternArcData.Count; i > iValue; i--)
                            {
                                cDEF.Work.Bonder1ARC.JetPatternArcData.RemoveAt(i - 1);
                            }
                            if (PLineSelectedNum1 > (cDEF.Work.Bonder1ARC.JetPatternArcData.Count - 1))
                            {
                                PLineSelectedNum1 = cDEF.Work.Bonder1ARC.JetPatternArcData.Count - 1;
                            }
                        }


                        cDEF.Work.Bonder1ARC.Save();

                        Jetting1PArcGridInit();
                    }

                    break;
                case "lblArcCreate2":
                    iValue = cDEF.Work.Bonder2ARC.JetPatternArcCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit($"Lens Pattern Count", ref iValue, " ", 0, 20))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Pattern Count {0} to {1}", cDEF.Work.Bonder2ARC.JetPatternArcCount, iValue);
                        cDEF.Work.Bonder2ARC.JetPatternArcCount = iValue;

                        if (iValue == 0)
                        {
                            cDEF.Work.Bonder2ARC.JetPatternArcData.Clear();
                            PLineSelectedNum2 = 0;
                        }
                        else if (cDEF.Work.Bonder2ARC.JetPatternArcData.Count < iValue)
                        {

                            for (int i = cDEF.Work.Bonder2ARC.JetPatternArcData.Count; i < iValue; i++)
                            {
                                JettingPatternArcData jpld = new JettingPatternArcData();
                                cDEF.Work.Bonder2ARC.JetPatternArcData.Add(jpld);
                            }
                        }
                        else if (iValue < cDEF.Work.Bonder2ARC.JetPatternArcData.Count)
                        {
                            for (int i = cDEF.Work.Bonder2ARC.JetPatternArcData.Count; i > iValue; i--)
                            {
                                cDEF.Work.Bonder2ARC.JetPatternArcData.RemoveAt(i - 1);
                            }
                            if (PLineSelectedNum2 > (cDEF.Work.Bonder2ARC.JetPatternArcData.Count - 1))
                            {
                                PLineSelectedNum2 = cDEF.Work.Bonder2ARC.JetPatternArcData.Count - 1;
                            }
                        }
                        cDEF.Work.Bonder2ARC.Save();

                        Jetting2PLineGridInit();
                    }
                    break;
                    #endregion ARC
            }
        }

        private void btnPat_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;
            int Selected = -1;
            
            switch (FName)
            {
                case "btnPatPre1":
                    
                    PLineSelectedNum1--;
                    if (PLineSelectedNum1 < 0)
                    {
                        PLineSelectedNum1 = 0;
                    }
                    Jetting1PLineGridInit();
                    break;
                case "btnPatCurNum1":
                    {
                        StringBuilder strTemp = new StringBuilder();
                        for (int i = 1; i < cDEF.Work.Bonder1Pattern.JetPatternLineCount + 1; i++)
                        {
                            strTemp.Append(i );
                            if (cDEF.Work.Bonder1Pattern.JetPatternLineCount > i)
                                strTemp.Append(",");
                        }

                        if (XModuleMain.frmBox.SelectBox("Jetting Mode #1", strTemp.ToString() , ref Selected) == DialogResult.No)
                            return;
                        PLineSelectedNum1 = Selected;
                    }
                    Jetting1PLineGridInit();
                    break;
                case "btnPatNext1":
                    PLineSelectedNum1++;
                    if (cDEF.Work.Bonder1Pattern.JetPatternLineCount  <= PLineSelectedNum1 )
                    {
                        PLineSelectedNum1 = cDEF.Work.Bonder1Pattern.JetPatternLineCount - 1;
                    }
                    Jetting1PLineGridInit();
                    break;
                case "btnPatPre2":
                    PLineSelectedNum2--;
                    if (PLineSelectedNum2 < 0)
                    {
                        PLineSelectedNum2 = 0;
                    }
                    Jetting2PLineGridInit();
                    break;
                case "btnPatCurNum2":
                    {
                        StringBuilder strTemp = new StringBuilder();
                        for (int i = 1; i < cDEF.Work.Bonder2Pattern.JetPatternLineCount + 1; i++)
                        {
                            strTemp.Append(i);
                            if (cDEF.Work.Bonder2Pattern.JetPatternLineCount > i)
                                strTemp.Append(",");
                        }

                        if (XModuleMain.frmBox.SelectBox("Jetting Mode #1", strTemp.ToString(), ref Selected) == DialogResult.No)
                            return;
                        PLineSelectedNum2 = Selected;
                    }
                    Jetting2PLineGridInit();
                    break;
                case "btnPatNext2":
                    PLineSelectedNum2++;
                    if (cDEF.Work.Bonder2Pattern.JetPatternLineCount <= PLineSelectedNum2)
                    {
                        PLineSelectedNum2 = cDEF.Work.Bonder2Pattern.JetPatternLineCount - 1;
                    }
                    Jetting2PLineGridInit();
                    break;
            }
        }

        private void btnArc_Click(object sender, EventArgs e)
        {
            string FName = (sender as Glass.GlassButton).Name;
            int Selected = -1;

            switch (FName)
            {
                case "btnArcPre1":

                    PArcSelectedNum1--;
                    if (PArcSelectedNum1 < 0)
                    {
                        PArcSelectedNum1 = 0;
                    }
                    Jetting1PArcGridInit();
                    break;
                case "btnArcCurNum1":
                    {
                        StringBuilder strTemp = new StringBuilder();
                        for (int i = 1; i < cDEF.Work.Bonder1ARC.JetPatternArcCount + 1; i++)
                        {
                            strTemp.Append(i);
                            if (cDEF.Work.Bonder1ARC.JetPatternArcCount > i)
                                strTemp.Append(",");
                        }

                        if (XModuleMain.frmBox.SelectBox("Jetting Mode #1", strTemp.ToString(), ref Selected) == DialogResult.No)
                            return;
                        PArcSelectedNum1 = Selected;
                    }
                    Jetting1PArcGridInit();
                    break;
                case "btnArcNext1":
                    PArcSelectedNum1++;
                    if (cDEF.Work.Bonder1ARC.JetPatternArcCount <= PArcSelectedNum1)
                    {
                        PArcSelectedNum1 = cDEF.Work.Bonder1ARC.JetPatternArcCount - 1;
                    }
                    Jetting1PArcGridInit();
                    break;
                case "btnArcPre2":
                    PArcSelectedNum2--;
                    if (PArcSelectedNum2 < 0)
                    {
                        PArcSelectedNum2 = 0;
                    }
                    Jetting2PArcGridInit();
                    break;
                case "btnArcCurNum2":
                    {
                        StringBuilder strTemp = new StringBuilder();
                        for (int i = 1; i < cDEF.Work.Bonder2ARC.JetPatternArcCount + 1; i++)
                        {
                            strTemp.Append(i);
                            if (cDEF.Work.Bonder2ARC.JetPatternArcCount > i)
                                strTemp.Append(",");
                        }

                        if (XModuleMain.frmBox.SelectBox("Jetting Mode #1", strTemp.ToString(), ref Selected) == DialogResult.No)
                            return;
                        PArcSelectedNum2 = Selected;
                    }
                    Jetting2PArcGridInit();
                    break;
                case "btnArcNext2":
                    PArcSelectedNum2++;
                    if (cDEF.Work.Bonder2ARC.JetPatternArcCount <= PArcSelectedNum2)
                    {
                        PArcSelectedNum2 = cDEF.Work.Bonder2ARC.JetPatternArcCount - 1;
                    }
                    Jetting2PArcGridInit();
                    break;
            }
        }
        private void lblLensHeight_Click(object sender, EventArgs e)
        {
           string FName = (sender as Label).Name;
            double DValue = 0;
            switch (FName)
            {
                case "lblLensHeight":
                     DValue = cDEF.Work.Recipe.LensHeight;
                    if (!XModuleMain.frmBox.fpFloatEdit("Lens Height", ref DValue, " mm", -10, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Height {0:0.000} to {1:0.000}", cDEF.Work.Recipe.LensHeight, DValue);
                        cDEF.Work.Recipe.LensHeight = DValue;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 4, str);
                    }
                    break;
                case "lblLensAllowMin":
                    DValue = cDEF.Work.Recipe.LensHeightAllowMin;
                    if (!XModuleMain.frmBox.fpFloatEdit("Lens Height", ref DValue, " mm", -10, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Height Allow Min{0:0.000} to {1:0.000}", cDEF.Work.Recipe.LensHeightAllowMin, DValue);
                        cDEF.Work.Recipe.LensHeightAllowMin = DValue;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 4, str);
                    }
                    break;
                case "lblLensAllowMax":
                    DValue = cDEF.Work.Recipe.LensHeightAllowMax;
                    if (!XModuleMain.frmBox.fpFloatEdit("Lens Height", ref DValue, " mm", -10, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Height Allow  Max{0:0.000} to {1:0.000}", cDEF.Work.Recipe.LensHeightAllowMax, DValue);
                        cDEF.Work.Recipe.LensHeightAllowMax = DValue;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 4, str);
                    }
                    break;
            }
        }

        private void VISIONRetryCount_Click(object sender, EventArgs e)
        {
            string FName = (sender as Label).Name;
            int Value = 0;
            switch (FName)
            {
                case "lbVCMVISIONRetryCount":
                    Value = cDEF.Work.Recipe.VCMVISIONRetryCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("VCM Vision Retry Count", ref Value, "", 0, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] VCM Vision Retry Count {0:0.000} to {1:0.000}", cDEF.Work.Recipe.VCMVISIONRetryCount, Value);
                        cDEF.Work.Recipe.VCMVISIONRetryCount = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 5, str);
                    }
                    break;
                case "lbLensUpperRetryCount":
                    Value = cDEF.Work.Recipe.LensUpperRetryCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Lens Upper Vision Retry Count", ref Value, "", 0, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Upper Count {0:0.000} to {1:0.000}", cDEF.Work.Recipe.LensUpperRetryCount, Value);
                        cDEF.Work.Recipe.LensUpperRetryCount = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 6, str);
                    }
                    break;
                case "lbLensBottomRetryCount":
                    Value = cDEF.Work.Recipe.LensUnderRetryCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Lens Bottom Vision Retry Count", ref Value, "", 0, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Lens Bottom Vision Retry Count {0:0.000} to {1:0.000}", cDEF.Work.Recipe.LensUnderRetryCount, Value);
                        cDEF.Work.Recipe.LensUnderRetryCount = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 7, str);
                    }
                    break;
                case "lbBonder1RetryCount":
                    Value = cDEF.Work.Recipe.Bonder1RetryCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder 1 Vision Retry Count", ref Value, "", 0, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder 1 Vision Retry Count {0:0.000} to {1:0.000}", cDEF.Work.Recipe.Bonder1RetryCount, Value);
                        cDEF.Work.Recipe.Bonder1RetryCount = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 8, str);
                    }
                    break;
                case "lbBonder2RetryCount":
                    Value = cDEF.Work.Recipe.Bonder2RetryCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder 2 Vision Retry Count", ref Value, "", 0, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder 2 Vision Retry Count {0:0.000} to {1:0.000}", cDEF.Work.Recipe.Bonder2RetryCount, Value);
                        cDEF.Work.Recipe.Bonder2RetryCount = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 9, str);
                    }
                    break;
                case "lbVisionRetryCount":
                    Value = cDEF.Work.Recipe.VIsionInspectRetryCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Vision Inspect Retry Count", ref Value, "", 0, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Vision Inspect Retry Count {0:0.000} to {1:0.000}", cDEF.Work.Recipe.VIsionInspectRetryCount, Value);
                        cDEF.Work.Recipe.VIsionInspectRetryCount = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 10, str);
                    }
                    break;
                case "lblDummyTime1":
                    Value = cDEF.Work.Recipe.DummyTime1;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Dummy Time", ref Value, " ms", 0, 10000))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder #1 Dummy Time {0:0.000} to {1:0.000}", cDEF.Work.Recipe.DummyTime1, Value);
                        cDEF.Work.Recipe.DummyTime1 = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 11, str);
                    }
                    break;
                case "lblDummyTime2":
                    Value = cDEF.Work.Recipe.DummyTime2;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #2 Dummy Time", ref Value, " ms", 0, 10000))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder #2 Dummy Time {0:0.000} to {1:0.000}", cDEF.Work.Recipe.DummyTime2, Value);
                        cDEF.Work.Recipe.DummyTime2= Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 12, str);
                    }
                    break;

                case "lblDummyPeriodCount1":
                    Value = cDEF.Work.Recipe.DummyPeriodCount1;

                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Dummy Period Count", ref Value, " ea", 0, 10000))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder #1 Dummy Period Count {0:0.000} to {1:0.000}", cDEF.Work.Recipe.DummyPeriodCount1, Value);
                        cDEF.Work.Recipe.DummyPeriodCount1 = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 13, str);
                    }
                    break;

                case "lblDummyPeriodCount2":
                    Value = cDEF.Work.Recipe.DummyPeriodCount2;

                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #2 Dummy Period Count", ref Value, " ea", 0, 10000))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder #2 Dummy Period Count {0:0.000} to {1:0.000}", cDEF.Work.Recipe.DummyPeriodCount2, Value);
                        cDEF.Work.Recipe.DummyPeriodCount2 = Value;
                        cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 14, str);
                    }
                    break;
            }
        }

        private void lbRnRPercnet_Click(object sender, EventArgs e)
        {
            string FName = (sender as Label).Name;
            double dValue = 0.0;
            switch (FName)
            {
                case "lbRnRGain":
                    dValue = cDEF.Work.PlateAngle.RnRPercent;
                    if (!XModuleMain.frmBox.fpFloatEdit("Plate Angle R&R Gain", ref dValue, " ", 0, 1))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Plate Angle R&R Gain {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.RnRPercent, dValue);
                        cDEF.Work.PlateAngle.RnRPercent = dValue;
                        cDEF.Work.PlateAngle.Save();
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 11, str);
                    }
                    break;

                case "lbRnRShift":
                    dValue = cDEF.Work.PlateAngle.RnRShift;
                    if (!XModuleMain.frmBox.fpFloatEdit("Plate Angle R&R Shift", ref dValue, " "))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Plate Angle R&R Shift {0:0.000} to {1:0.000}", cDEF.Work.PlateAngle.RnRShift, dValue);
                        cDEF.Work.PlateAngle.RnRShift = dValue;
                        cDEF.Work.PlateAngle.Save();
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 12, str);
                    }
                    break;

            }
        }

        #region 이미지표시
        Bitmap Orgbm;
        private Bitmap VCMbitmap;
        double pixReX = 30;
        double pixReY = 30;

        public void VcmImgInit()
        {
            //if (!File.Exists(Application.StartupPath + "\\VCMIMG318.png"))
            //    return;
            try
            {
                if (!File.Exists(cPath.FILE_PROJECT + cDEF.Work.Project.FileName + $"\\LensImage.png"))
                    return;
                Bitmap LensImage = new Bitmap(cPath.FILE_PROJECT + cDEF.Work.Project.FileName + $"\\LensImage.png");
                Orgbm = LensImage;
                //Orgbm = (Bitmap)System.Drawing.Image.FromFile("LensImage.png", true);
                VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
                Graphics grPhoto = Graphics.FromImage(VCMbitmap);
                grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);

                pixReX = VCMbitmap.Width / cDEF.Work.Option.ProductDiameter;  //10.6
                pixReY = VCMbitmap.Height / cDEF.Work.Option.ProductDiameter;

                picVCM.Image = VCMbitmap;
                picVCM1.Image = VCMbitmap;
                picVCM2.Image = VCMbitmap;
                picVCMArc1.Image = VCMbitmap;
                picVCMArc2.Image = VCMbitmap;

                PointDrawlbl();
                picVCM.Refresh();
            }
            catch
            {

            }

            
        }

        private void PointDrawlbl()
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    Graphics g = Graphics.FromImage(VCMbitmap);

                    #region DrawLine
                    Pen bluePen = new Pen(Color.Blue, 2);
                    Font dispFont = new Font("맑은고딕", 50, FontStyle.Bold, GraphicsUnit.Pixel);

                    int ReviseValue = 1;

                    PointF drawWS = new PointF();
                    drawWS.X = ReviseValue;
                    drawWS.Y = (VCMbitmap.Height / 2);
                    PointF drawWE = new PointF();
                    drawWE.X = VCMbitmap.Width - ReviseValue;
                    drawWE.Y = (VCMbitmap.Height / 2);

                    PointF drawHS = new PointF();
                    drawHS.X = VCMbitmap.Width / 2;
                    drawHS.Y = ReviseValue;
                    PointF drawHE = new PointF();
                    drawHE.X = VCMbitmap.Width / 2;
                    drawHE.Y = VCMbitmap.Height - ReviseValue;

                    g.DrawLine(bluePen, drawWS, drawWE);
                    g.DrawLine(bluePen, drawHS, drawHE);
                    g.DrawString("-", dispFont, Brushes.White, drawWS.X, drawWS.Y + 10);
                    g.DrawString("+", dispFont, Brushes.White, drawWE.X - 50, drawWE.Y + 10);

                    g.DrawString("-", dispFont, Brushes.White, drawHS.X + 10, drawHS.Y);
                    g.DrawString("+", dispFont, Brushes.White, drawHE.X + 10, drawHE.Y - 50);

                    #endregion DrawLine

                    bluePen.Dispose();
                    dispFont.Dispose();
                    g.Dispose();
                 
                }));
            }
            catch
            {

            }
        }
        private void PointDraw(PointF Line1,int PointNum)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    int dispStrX = 20;
                    int dispStrY = 15;


                    Graphics g = Graphics.FromImage(VCMbitmap);

                    #region DrawLine
                    Pen bluePen = new Pen(Color.Blue, 2);
                    Font dispFont = new Font("맑은고딕", 50, FontStyle.Bold, GraphicsUnit.Pixel);
                    Font dispFont2 = new Font("tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel);

                    int ReviseValue = 1;

                    PointF drawWS = new PointF();
                    drawWS.X = ReviseValue;
                    drawWS.Y = (VCMbitmap.Height / 2);
                    PointF drawWE = new PointF();
                    drawWE.X = VCMbitmap.Width - ReviseValue;
                    drawWE.Y = (VCMbitmap.Height / 2);

                    PointF drawHS = new PointF();
                    drawHS.X = VCMbitmap.Width / 2;
                    drawHS.Y = ReviseValue;
                    PointF drawHE = new PointF();
                    drawHE.X = VCMbitmap.Width / 2;
                    drawHE.Y = VCMbitmap.Height - ReviseValue;

                    g.DrawLine(bluePen, drawWS, drawWE);
                    g.DrawLine(bluePen, drawHS, drawHE);
                    g.DrawString("-", dispFont, Brushes.White, drawWS.X, drawWS.Y + 10);
                    g.DrawString("+", dispFont, Brushes.White, drawWE.X - 50, drawWE.Y + 10);

                    g.DrawString("-", dispFont, Brushes.White, drawHS.X + 10, drawHS.Y);
                    g.DrawString("+", dispFont, Brushes.White, drawHE.X + 10, drawHE.Y - 50);

                    #endregion DrawLine

                    Pen ReadPen = new Pen(Color.Red, 3);

                    g.DrawEllipse(ReadPen, Line1.X, Line1.Y, 3, 3);

                    if (Line1.X > drawWS.X)
                    {
                        dispStrX = 20;
                    }

                    g.DrawString($"{PointNum}", dispFont2, Brushes.Lime, Line1.X, Line1.Y + dispStrY);

                    ReadPen.Dispose();
                    bluePen.Dispose();
                    dispFont.Dispose();
                    dispFont2.Dispose();
                    g.Dispose();
                    picVCM.Refresh();
                }));
            }
            catch
            {

            }
        }
        private void PointDrawline(PointF Line1, PointF Line2, int RowIdx, int BonderNum)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    int dispStrX = 20;
                    int dispStrY = 15;


                    Graphics g = Graphics.FromImage(VCMbitmap);

                    #region DrawLine
                    Pen bluePen = new Pen(Color.Blue, 2);
                    Font dispFont = new Font("맑은고딕", 50, FontStyle.Bold, GraphicsUnit.Pixel);
                    Font dispFont2 = new Font("tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel);

                    int ReviseValue = 1;

                    PointF drawWS = new PointF();
                    drawWS.X = ReviseValue;
                    drawWS.Y = (VCMbitmap.Height / 2);
                    PointF drawWE = new PointF();
                    drawWE.X = VCMbitmap.Width - ReviseValue;
                    drawWE.Y = (VCMbitmap.Height / 2);

                    PointF drawHS = new PointF();
                    drawHS.X = VCMbitmap.Width / 2;
                    drawHS.Y = ReviseValue;
                    PointF drawHE = new PointF();
                    drawHE.X = VCMbitmap.Width / 2;
                    drawHE.Y = VCMbitmap.Height - ReviseValue;

                    g.DrawLine(bluePen, drawWS, drawWE);
                    g.DrawLine(bluePen, drawHS, drawHE);
                    g.DrawString("-", dispFont, Brushes.White, drawWS.X, drawWS.Y + 10);
                    g.DrawString("+", dispFont, Brushes.White, drawWE.X - 50, drawWE.Y + 10);

                    g.DrawString("-", dispFont, Brushes.White, drawHS.X + 10, drawHS.Y);
                    g.DrawString("+", dispFont, Brushes.White, drawHE.X + 10, drawHE.Y - 50);

                    #endregion DrawLine

                    Pen ReadPen = new Pen(Color.Red, 2);

                    g.DrawLine(ReadPen, Line1, Line2);

                    Font dispFont1 = new Font("맑은고딕", 12, FontStyle.Bold, GraphicsUnit.Pixel);

                    if (Line1.X > drawWS.X)
                    {
                        dispStrX = -20;
                    }
                    
                    g.DrawString($"{BonderNum}_{RowIdx-1}", dispFont2, Brushes.Lime, Line1.X + dispStrX, Line1.Y + dispStrY);

                    g.DrawString($"{BonderNum}_{RowIdx}", dispFont2, Brushes.Lime, Line2.X + dispStrX, Line2.Y + dispStrY);

                    ReadPen.Dispose();
                    bluePen.Dispose();
                    dispFont.Dispose();
                    dispFont1.Dispose();
                    dispFont2.Dispose();
                    g.Dispose();
                    picVCM.Refresh();
                }));
            }
            catch
            {

            }
        }



        PointF[] Drawpoints = new PointF[3];
        private void PointDrawArc(PointF Center,double firstAngle, double StartAngle, double EndAngle, int RowIdx, int BonderNum, PointF pos1, PointF pos2)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {

                    int dispStrX = 2;
                    int dispStrY = 2;


                    Graphics g = Graphics.FromImage(VCMbitmap);

                    #region DrawLine
                    Pen bluePen = new Pen(Color.Blue, 2);
                    Font dispFont = new Font("맑은고딕", 50, FontStyle.Bold, GraphicsUnit.Pixel);
                    Font dispFont2 = new Font("tahoma", 18, FontStyle.Bold, GraphicsUnit.Pixel);

                    int ReviseValue = 1;

                    PointF drawWS = new PointF();
                    drawWS.X = ReviseValue;
                    drawWS.Y = (VCMbitmap.Height / 2);
                    PointF drawWE = new PointF();
                    drawWE.X = VCMbitmap.Width - ReviseValue;
                    drawWE.Y = (VCMbitmap.Height / 2);

                    PointF drawHS = new PointF();
                    drawHS.X = VCMbitmap.Width / 2;
                    drawHS.Y = ReviseValue;
                    PointF drawHE = new PointF();
                    drawHE.X = VCMbitmap.Width / 2;
                    drawHE.Y = VCMbitmap.Height - ReviseValue;

                    g.DrawLine(bluePen, drawWS, drawWE);
                    g.DrawLine(bluePen, drawHS, drawHE);


                    g.DrawString("-", dispFont, Brushes.White, drawWS.X, drawWS.Y + 10);
                    g.DrawString("+", dispFont, Brushes.White, drawWE.X - 50, drawWE.Y + 10);

                    g.DrawString("-", dispFont, Brushes.White, drawHS.X + 10, drawHS.Y);
                    g.DrawString("+", dispFont, Brushes.White, drawHE.X + 10, drawHE.Y - 50);

                    #endregion DrawLine

                    Pen ReadPen = new Pen(Color.Red, 2);
                    Pen BluePen = new Pen(Color.Blue, 2);


                    double MidAngle =  (StartAngle + EndAngle) / 2.0;

                    if ( 90 < Math.Abs(StartAngle) && 
                       90 < Math.Abs(EndAngle)  &&
                       Math.Abs(MidAngle) < 90)
                    {
                        MidAngle = CMath.AngleFlipX(MidAngle);
                    }
                   
                    double Angle1 = (StartAngle + MidAngle) / 2.0;
                    double Angle2 = (MidAngle + EndAngle) / 2.0;

                 
                    double Radius1 = CMath.GetDistance(pos1, Center);
                    double Radius2 = CMath.GetDistance(pos2, Center);
                    double MidRadius = (Radius1 + Radius2) / 2.0;
                    double Radius3 = (Radius1 + MidRadius) / 2.0;
                    double Radius4 = (MidRadius + Radius2) / 2.0;

                    double [] MidPointX = new double[3];
                    double [] MidPointY = new double[3];

                    CMath.GetPosCircle(Center.X, Center.Y, Radius3, Angle1, ref MidPointX[0], ref MidPointY[0]);
                    CMath.GetPosCircle(Center.X, Center.Y, MidRadius, MidAngle, ref MidPointX[1], ref MidPointY[1]);
                    CMath.GetPosCircle(Center.X, Center.Y, Radius4, Angle2, ref MidPointX[2], ref MidPointY[2]);

                    Drawpoints[0].X = pos1.X;
                    Drawpoints[0].Y = pos1.Y;
                    //Drawpoints[1].X = (float)-(MidPointX[0]);
                    //Drawpoints[1].Y = (float)-(MidPointY[0]);
                    Drawpoints[1].X = (float)-(MidPointX[1]);
                    Drawpoints[1].Y = (float)-(MidPointY[1]);
                    //Drawpoints[3].X = (float)-(MidPointX[2]);
                    //Drawpoints[3].Y = (float)-(MidPointY[2]);
                    Drawpoints[2].X = pos2.X;
                    Drawpoints[2].Y = pos2.Y;

                    
                    //Draw lines to screen.
                    g.DrawCurve(ReadPen, Drawpoints,1f);

                    if (pos1.X > drawWS.X)
                    {
                        dispStrX = -10;
                    }


                    if (Math.Abs(StartAngle - EndAngle) > 5)
                    {
                      g.DrawString($"{RowIdx}", dispFont2, Brushes.Lime, Drawpoints[2].X + dispStrX, Drawpoints[2].Y + dispStrY);
                    }
                    ReadPen.Dispose();

                    bluePen.Dispose();
                    dispFont.Dispose();
                    dispFont2.Dispose();
                    g.Dispose();
                    picVCM.Refresh();
                }));
            }
            catch
            {

            }
        }
        private void PointDrawArc(PointF Center, Rectangle rect, double StartAngle, double EndAngle, int RowIdx, int BonderNum, PointF pos1, PointF pos2)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {

                    int dispStrX = 2;
                    int dispStrY = 2;


                    Graphics g = Graphics.FromImage(VCMbitmap);

                    #region DrawLine
                    Pen bluePen = new Pen(Color.Blue, 2);
                    Font dispFont = new Font("맑은고딕", 50, FontStyle.Bold, GraphicsUnit.Pixel);
                    Font dispFont2 = new Font("tahoma", 18, FontStyle.Bold, GraphicsUnit.Pixel);

                    int ReviseValue = 1;

                    PointF drawWS = new PointF();
                    drawWS.X = ReviseValue;
                    drawWS.Y = (VCMbitmap.Height / 2);
                    PointF drawWE = new PointF();
                    drawWE.X = VCMbitmap.Width - ReviseValue;
                    drawWE.Y = (VCMbitmap.Height / 2);

                    PointF drawHS = new PointF();
                    drawHS.X = VCMbitmap.Width / 2;
                    drawHS.Y = ReviseValue;
                    PointF drawHE = new PointF();
                    drawHE.X = VCMbitmap.Width / 2;
                    drawHE.Y = VCMbitmap.Height - ReviseValue;

                    g.DrawLine(bluePen, drawWS, drawWE);
                    g.DrawLine(bluePen, drawHS, drawHE);


                    g.DrawString("-", dispFont, Brushes.White, drawWS.X, drawWS.Y + 10);
                    g.DrawString("+", dispFont, Brushes.White, drawWE.X - 50, drawWE.Y + 10);

                    g.DrawString("-", dispFont, Brushes.White, drawHS.X + 10, drawHS.Y);
                    g.DrawString("+", dispFont, Brushes.White, drawHE.X + 10, drawHE.Y - 50);

                    #endregion DrawLine

                    Pen ReadPen = new Pen(Color.Red, 2);
                    Pen BluePen = new Pen(Color.Blue, 2);


                    //Draw lines to screen.
                    if (EndAngle != 0 && StartAngle !=0)
                        g.DrawArc(ReadPen,rect,(float) StartAngle, (float)EndAngle);

                    if (pos1.X > drawWS.X)
                    {
                        dispStrX = -10;
                    }


                    if (Math.Abs(StartAngle - EndAngle) > 5)
                    {
                        g.DrawString($"{BonderNum}_{RowIdx}", dispFont2, Brushes.Aqua, (pos1.X +  pos2.X)/2 + dispStrX, (pos1.Y + pos2.Y) /2 + dispStrY);
                    }
                    ReadPen.Dispose();

                    bluePen.Dispose();
                    dispFont.Dispose();
                    dispFont2.Dispose();
                    g.Dispose();
                    picVCM.Refresh();
                }));
            }
            catch
            {

            }
        }

        private void DispLine(float PosX, float PosY, float PosX1, float PosY1, int RowIdx, int BonderNum)
        {
            PosX *= (float)pixReX;
            PosX1 *= (float)pixReX;
            PosY *= (float)pixReY;
            PosY1 *= (float)pixReX;

            PointF PosCenter = new PointF();
            PosCenter.X = Orgbm.Width / 2;
            PosCenter.Y = Orgbm.Height / 2;


            PointF DrawPos1 = new PointF();
            DrawPos1.X = PosCenter.X + PosX;
            DrawPos1.Y = PosCenter.Y + PosY;
            PointF DrawPos2 = new PointF();
            DrawPos2.X = PosCenter.X + PosX1;
            DrawPos2.Y = PosCenter.Y + PosY1;

            PointDrawline(DrawPos1, DrawPos2, RowIdx, BonderNum);
        }

        private double OldEndAngle;

        private void DispArc(float PosX, float PosY, float PosX2, float PosY2, int RowIdx, int BonderNum)
        {
            PosX *= (float)pixReX;
           // PosX1 *= (float)pixReX;
            PosY *= (float)pixReY;
            //PosY1 *= (float)pixReX;

            PosX2 *= (float)pixReX;
            // PosX1 *= (float)pixReX;
            PosY2 *= (float)pixReY;
            //PosY1 *= (float)pixReX;

            PointF PosCenter = new PointF();
            PosCenter.X = Orgbm.Width / 2;
            PosCenter.Y = Orgbm.Height / 2;


            PointF DrawPos1 = new PointF();
            DrawPos1.X = PosCenter.X + PosX;
            DrawPos1.Y = PosCenter.Y + PosY;
            PointF DrawPos2 = new PointF();
            DrawPos2.X = PosCenter.X + PosX2;
            DrawPos2.Y = PosCenter.Y + PosY2;

            //double firAngle = CMath.AngleFlipX( CMath.GetAngle(DrawPos1 , PosCenter));
            //double StartAngle = CMath.AngleFlipX(CMath.GetAngle(DrawPos1, PosCenter));
            //double EndAngle = CMath.AngleFlipX(CMath.GetAngle(DrawPos2, PosCenter));

            double firAngle =CMath.GetAngle(DrawPos1, PosCenter);
            double StartAngle = CMath.GetAngle( PosCenter, DrawPos1);
            double EndAngle =CMath.GetAngle(PosCenter, DrawPos2);

            PointDrawArc(PosCenter, firAngle, StartAngle, EndAngle, RowIdx, BonderNum, DrawPos1, DrawPos2);

        }
        private void DispArc(float PosX, float PosY, float Angle, int RowIdx, int BonderNum)
        {
            PosX *= (float)pixReX;  // org
            PosY *= (float)pixReY;  // org

            double tarPosX = 0;
            double tarPosY = 0;

            tarPosX = (PosX * Math.Cos(Angle * Math.PI / 180.0)) - (PosY * Math.Sin(Angle * Math.PI / 180.0));
            tarPosY = (PosX * Math.Sin(Angle * Math.PI / 180.0)) + (PosY * Math.Cos(Angle * Math.PI / 180.0));

            PointF PosCenter = new PointF();
            PosCenter.X = Orgbm.Width / 2;
            PosCenter.Y = Orgbm.Height / 2;

            double half = Math.Sqrt( Math.Pow(PosX, 2) + Math.Pow(PosY, 2));

             PointF DrawPos1 = new PointF();
            DrawPos1.X = PosCenter.X + PosX;
            DrawPos1.Y = PosCenter.Y + PosY;

            PointF DrawPos2 = new PointF();
            DrawPos2.X = PosCenter.X + (float)tarPosX;
            DrawPos2.Y = PosCenter.Y + (float)tarPosY;

            Rectangle rect = new Rectangle();
            rect.X = (int)PosCenter.X - (int)half;
            rect.Y = (int)PosCenter.Y - (int)half;
            rect.Width = (int)half * 2;
            rect.Height = (int)half * 2;

            double StartAngle = CMath.GetAngle(DrawPos1, PosCenter);
            double EndAngle = CMath.GetAngle(DrawPos2, PosCenter);

            if (Math.Abs(StartAngle) >= 0 && Math.Abs(StartAngle) <= 360)
            PointDrawArc(PosCenter,rect, StartAngle, Angle, RowIdx, BonderNum, DrawPos1, DrawPos2);

        }
        private void DispPoint(float PosX, float PosY, int PointNum)
        {
            PosX *= (float)pixReX;
            PosY *= (float)pixReY;

            PointF PosCenter = new PointF();
            PosCenter.X = Orgbm.Width / 2;
            PosCenter.Y = Orgbm.Height / 2;

            PointF DrawPos1 = new PointF();
            DrawPos1.X = PosCenter.X + PosX;
            DrawPos1.Y = PosCenter.Y + PosY;

            PointDraw(DrawPos1, PointNum);
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tabidx = (sender as TabControl).SelectedIndex;
            switch (tabidx)
            {
                case 2:
                    {
                        int Index = 1;
                        float posX = 0;
                        float posY = 0;
                        VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
                        Graphics grPhoto = Graphics.FromImage(VCMbitmap);
                        grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);

                        Point result;

                        foreach (JettingData Jd in cDEF.Work.Bonder1Point.JetData)
                        {
                            // VCM 이미지 표시
                            result = cDEF.Run.Bonder1.GetCirclePoint(0, 0, Jd.Radius, Jd.Angle);
                            posX = (float)(result.X / 1000.0);
                            posY = (float)(result.Y / 1000.0);
                            if (Jd.Enable)
                                DispPoint(posX, posY, Index);

                            Index++;
                        }
                        picVCM.Image = VCMbitmap;
                        picVCM.Refresh();
                    }
                    break;
                
                case 3:
                    {
                        PatternLineDraw_All();
                    }
                    break;
                case 4:
                    {
                        PatternArcDraw_All();
                    }
                    break;
                default:
                    break;
            }
        }

        private void PatternArcDraw_All()
        {
          
            bool PosBind2 = false;

            bool PosBind = false;
          
            VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
            Graphics grPhoto = Graphics.FromImage(VCMbitmap);
            grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);
            PointF Start = new PointF();
            PointF ArcPos = new PointF();
            for (int i = 0; i < cDEF.Work.Bonder1ARC.JetPatternArcCount; i++)
            {
                for (int j =1; j < cDEF.Work.Bonder1ARC.JetPatternArcData[i].JetArcData.Count ; j++)
                {
                    JettingArcData Jd = cDEF.Work.Bonder1ARC.JetPatternArcData[i].JetArcData[j];

                    if (Jd.JetType == eJetType.Arc)
                    {
                        if (Jd.Shot)
                        {
                            DispArc(Start.X, Start.Y, (float)Jd.Angle, j + 1, 1);
                        }
                        else
                            PosBind = false;
                    }
                    else if(Jd.JetType == eJetType.Line)
                    {
                        Start.X = (float)((double)Jd.XPos / 1000.0);
                        Start.Y = (float)((double)Jd.YPos / 1000.0);
                    }
                }
              
            }
            for (int i = 0; i < cDEF.Work.Bonder2ARC.JetPatternArcCount; i++)
            {
                for (int j = 1; j < cDEF.Work.Bonder2ARC.JetPatternArcData[i].JetArcData.Count; j++)
                {
                    JettingArcData Jd = cDEF.Work.Bonder2ARC.JetPatternArcData[i].JetArcData[j];

                    if (Jd.JetType == eJetType.Arc)
                    {
                        if (Jd.Shot)
                        {
                            DispArc(Start.X, Start.Y, (float)Jd.Angle, j + 1, 2);
                        }
                        else
                            PosBind = false;
                    }
                    else if (Jd.JetType == eJetType.Line)
                    {
                        Start.X = (float)((double)Jd.XPos / 1000.0);
                        Start.Y = (float)((double)Jd.YPos / 1000.0);
                    }

                }
            }
            picVCM.Image = VCMbitmap;
            picVCM.Refresh();
        }

        private void PatternLineDraw_All()
        {
            int Index = 1;
            float posX = 0;
            float posX1 = 0;
            float posY = 0;
            float posY1 = 0;
            bool PosBind = false;

            VCMbitmap = new Bitmap(Orgbm.Width, Orgbm.Height);
            Graphics grPhoto = Graphics.FromImage(VCMbitmap);
            grPhoto.DrawImage(Orgbm, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height), 0, 0, VCMbitmap.Width, VCMbitmap.Height, GraphicsUnit.Pixel);

            for (int i = 0; i < cDEF.Work.Bonder1Pattern.JetPatternLineCount; i++)
            {
                foreach (JettingLineData Jd in cDEF.Work.Bonder1Pattern.JetPatternLineData[i].JetLineData)
                {
                    // VCM 이미지 표시
                    if (Jd.Shot)
                    {
                        if (PosBind)
                        {
                            posX1 = (float)(Jd.XPos / 1000.0);
                            posY1 = (float)(Jd.YPos / 1000.0);
                            DispLine(posX, posY, posX1, posY1, Index, 1);
                            PosBind = false;
                        }
                        else
                        {
                            posX = (float)(Jd.XPos / 1000.0);
                            posY = (float)(Jd.YPos / 1000.0);
                            PosBind = true;
                        }
                    }
                    Index++;
                }
                Index = 1;
            }
            for (int i = 0; i < cDEF.Work.Bonder2Pattern.JetPatternLineCount; i++)
            {
                foreach (JettingLineData Jd in cDEF.Work.Bonder2Pattern.JetPatternLineData[i].JetLineData)
                {
                    // VCM 이미지 표시
                    if (Jd.Shot)
                    {
                        if (PosBind)
                        {
                            posX1 = (float)(Jd.XPos / 1000.0);
                            posY1 = (float)(Jd.YPos / 1000.0);
                            DispLine(posX, posY, posX1, posY1, Index, 2);
                            PosBind = false;
                        }
                        else
                        {
                            posX = (float)(Jd.XPos / 1000.0);
                            posY = (float)(Jd.YPos / 1000.0);
                            PosBind = true;
                        }
                    }
                    Index++;
                }
                Index = 1;
            }
            picVCM.Image = VCMbitmap;
            picVCM.Refresh();
        }

        private void lbBond1LineTitle_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as Label).Tag);

            switch(tag)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    Jetting1GridInit();
                    break;
                case 3:
                    Jetting2GridInit();
                    break;
            }
        }


        private void lbResetTime_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as Label).Tag);
            int Value = 0;
            string str = string.Empty;
            switch (tag)
            {
                case 0:
                    Value = cDEF.Work.Option.ResetTime1;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Reset Time 1", ref Value, ""))
                        return;
                    {
                        str = String.Format("[FormOper] Reset Time 1 {0:0.000} to {1:0.000}", cDEF.Work.Option.ResetTime1, Value);
                        cDEF.Work.Option.ResetTime1 = Value;
                        cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Operator_Data + 12, str);
                    }
                    break;

                case 1:
                    Value = cDEF.Work.Option.ResetTime2;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Reset Time 2", ref Value, ""))
                        return;
                    {
                        str = String.Format("[FormOper] Reset Time 2 {0:0.000} to {1:0.000}", cDEF.Work.Option.ResetTime2, Value);
                        cDEF.Work.Option.ResetTime2 = Value;
                        cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Operator_Data + 13, str);
                    }
                    break;
            }
        }

        private void lbTorqueTLimit_Click(object sender, EventArgs e)
        {
            double DValue = Convert.ToDouble(cDEF.Work.Recipe.LensInsertTorqueLimitTheta) / 100;
            if (!XModuleMain.frmBox.fpFloatEdit("Lens Insert Theta Torque Limit", ref DValue, " ˚", -10, 10))
                return;
            {
                string str = String.Format("[Form Recipe] Lens Insert Theta Torque Limit {0:0.000} to {1:0.000}", cDEF.Work.Recipe.LensInsertTorqueLimitTheta / 100.0, DValue);
                cDEF.Work.Recipe.LensInsertTorqueLimitTheta = (int)(DValue * 100.0);
                cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);
                cDEF.Run.LogData(cLog.Form_Recipe_Data + 4, str);
            }
        }

        private void lbGapPosX_Click(object sender, EventArgs e)
        {
            double DValue = 0.0;

            string Name = (sender as Label).Name;
            switch(Name)
            {
                case "lbBonder1GapPosX":
                    DValue = cDEF.Work.Bonder1.GapPosX / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Position X", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder1.HeadX.ActualPosition - cDEF.Work.TeachBonder1.CamPositionX) / 1000.0, -50.0, 50.0))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder1 Gap Pos X {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.GapPosX / 1000.0, DValue);
                        cDEF.Work.Bonder1.GapPosX = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1.Save();
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 14, str);
                    }
                    break;

                case "lbBonder1GapPosY":
                    DValue = cDEF.Work.Bonder1.GapPosY / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit("Bonder #1 Gap Position Y", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder1.HeadY.ActualPosition - cDEF.Work.TeachBonder1.CamPositionY) / 1000.0, -50.0, 50.0))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder1 Gap Pos Y {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.GapPosY / 1000.0, DValue);
                        cDEF.Work.Bonder1.GapPosY = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder1.Save();
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 15, str);
                    }
                    break;

                case "lbBonder2GapPosX":
                    DValue = cDEF.Work.Bonder2.GapPosX / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Position X", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder2.HeadX.ActualPosition - cDEF.Work.TeachBonder2.CamPositionX) / 1000.0, -50.0, 50.0))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder2 Gap Pos X {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.GapPosX / 1000.0, DValue);
                        cDEF.Work.Bonder2.GapPosX = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2.Save();
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 14, str);
                    }
                    break;

                case "lbBonder2GapPosY":
                    DValue = cDEF.Work.Bonder2.GapPosY / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit("Bonder #2 Gap Position Y", ref DValue, " mm", "CURRENT", (cDEF.Run.Bonder2.HeadY.ActualPosition - cDEF.Work.TeachBonder2.CamPositionY) / 1000.0, -50.0, 50.0))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Bonder2 Gap Pos Y {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.GapPosY / 1000.0, DValue);
                        cDEF.Work.Bonder2.GapPosY = (int)(DValue * 1000.0);
                        cDEF.Work.Bonder2.Save();
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 15, str);
                    }
                    break;
            }

        }

        private void lbSecondaryLimitValue_Click(object sender, EventArgs e)
        {
            double DValue = cDEF.Work.LensPicker.SecondaryCorrLimit;
            if (!XModuleMain.frmBox.fpFloatEdit("Secondary Correction Limit Value", ref DValue, " mm"))
                return;
            {
                string str = String.Format("[Form Recipe] Secondary Correction Limit Value {0:0.000} to {1:0.000}", cDEF.Work.LensPicker.SecondaryCorrLimit , DValue);
                cDEF.Work.LensPicker.SecondaryCorrLimit = DValue;
                cDEF.Work.LensPicker.Save();
                cDEF.Run.LogData(cLog.Form_Recipe_Data + 15, str);
            }
        }

        private void lblVisionVer_Click(object sender, EventArgs e)
        {
            string Temp = cDEF.Work.Project.GlobalOption.VisionVer;
            if (cDEF.fTextEdit.TextEdit("[MESINFO] Vision Program Version", ref Temp, "\\/:*?\"<>|"))
            {
                cDEF.Work.Project.GlobalOption.VisionVer = Temp;
                 cDEF.Work.Project.GlobalOption.Save(cDEF.Work.Project.FileName);;
            }
        }

        private void gbSequenceOption_Enter(object sender, EventArgs e)
        {

        }

        private void btnUseContact_Click(object sender, EventArgs e)
        {
            cDEF.Work.Recipe.NonContactMeasure = !cDEF.Work.Recipe.NonContactMeasure;
            cDEF.Work.Recipe.Save(cDEF.Work.Project.FileName);

        }

        private void PlateAngleOptionSet_Click(object sender, EventArgs e)
        {
            int Value = 0;

            string Name = (sender as Label).Name;
            switch (Name)
            {
                case "lbPlateAngleRetry":
                    Value = cDEF.Work.Option.Measure_RetryCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Gap Position X", ref Value, " Ea", "CURRENT", cDEF.Work.Option.Measure_RetryCount, 1, 10))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Plate Angle Measure Retry Count", cDEF.Work.Option.Measure_RetryCount, Value);
                        cDEF.Work.Option.Measure_RetryCount = Value;
                        cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 98, str);
                    }
                    break;

                case "lbPlateAngleGood":
                    Value = cDEF.Work.Option.Measure_GoodCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Gap Position X", ref Value, " Ea", "CURRENT", cDEF.Work.Option.Measure_GoodCount, 1, cDEF.Work.Option.Measure_RetryCount))
                        return;
                    {
                        string str = String.Format("[Form Recipe] Plate Angle Measure Retry Count", cDEF.Work.Option.Measure_GoodCount, Value);
                        cDEF.Work.Option.Measure_GoodCount = Value;
                        cDEF.Work.Option.Save(cDEF.Work.Project.FileName);
                        cDEF.Run.LogData(cLog.Form_Recipe_Data + 99, str);
                    }
                    break;

            }

        }
    }
}

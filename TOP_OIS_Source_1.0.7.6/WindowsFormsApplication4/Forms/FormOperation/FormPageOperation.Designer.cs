namespace XModule.Forms.FormOperation
{
    partial class FrmPageOperation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPageOperation));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ImgLstState = new System.Windows.Forms.ImageList(this.components);
            this.lbLedTactTime = new System.Windows.Forms.Label();
            this.lbEQID = new System.Windows.Forms.Label();
            this.label_EqpID = new System.Windows.Forms.Label();
            this.lbMachineMode = new System.Windows.Forms.Label();
            this.lbVCMLoaderMode = new System.Windows.Forms.Label();
            this.lbVCMPickerMode = new System.Windows.Forms.Label();
            this.lbVCMLoaderStep = new System.Windows.Forms.Label();
            this.lbVCMPickerStep = new System.Windows.Forms.Label();
            this.lbTactTimeLabel = new System.Windows.Forms.Label();
            this.a1Panel11 = new Owf.Controls.A1Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnMainIndex = new System.Windows.Forms.Panel();
            this.lbAct3Fail = new System.Windows.Forms.Label();
            this.lbAct3Pass = new System.Windows.Forms.Label();
            this.lbAct3Ready = new System.Windows.Forms.Label();
            this.lbAct3Mode = new System.Windows.Forms.Label();
            this.lbDisplaceValueLensZTorque = new SevenSegment.SevenSegmentArray();
            this.lbDisplaceValueBond2 = new SevenSegment.SevenSegmentArray();
            this.lbDisplaceValueBond1 = new SevenSegment.SevenSegmentArray();
            this.btnUnloadTHome = new Glass.GlassButton();
            this.pnBond2 = new System.Windows.Forms.Panel();
            this.btnBond2Reset = new Glass.GlassButton();
            this.lbBond2JetCount = new System.Windows.Forms.Label();
            this.lbBond2TotalCount = new System.Windows.Forms.Label();
            this.proBonder2 = new XModule.Forms.FormOperation.VerticalProgressBar();
            this.label15 = new System.Windows.Forms.Label();
            this.pnBonder1 = new System.Windows.Forms.Panel();
            this.btnBond1Reset = new Glass.GlassButton();
            this.lbBond1JetCount = new System.Windows.Forms.Label();
            this.lbBond1TotalCount = new System.Windows.Forms.Label();
            this.proBonder1 = new XModule.Forms.FormOperation.VerticalProgressBar();
            this.lbBond1Title = new System.Windows.Forms.Label();
            this.lbUVLamp2LifeTime = new System.Windows.Forms.Label();
            this.lbUVLamp1LifeTime = new System.Windows.Forms.Label();
            this.btnDailyCountReset = new Glass.GlassButton();
            this.label12 = new System.Windows.Forms.Label();
            this.lbDailyNGCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbDailyOKCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbDailyTotalCount = new System.Windows.Forms.Label();
            this.btnTestButton = new Glass.GlassButton();
            this.pnUnloadTrayChange = new System.Windows.Forms.Panel();
            this.pbUnloadTray = new System.Windows.Forms.ProgressBar();
            this.lbUnloadTrayChangeText = new System.Windows.Forms.Label();
            this.pnVCMTrayChange = new System.Windows.Forms.Panel();
            this.pbVcmTray = new System.Windows.Forms.ProgressBar();
            this.lbVcmTrayChangeText = new System.Windows.Forms.Label();
            this.pnLensTrayChange = new System.Windows.Forms.Panel();
            this.pbLensTray = new System.Windows.Forms.ProgressBar();
            this.lbLensTrayChangeText = new System.Windows.Forms.Label();
            this.lbSideAngleValue = new SevenSegment.SevenSegmentArray();
            this.lbIndexNum = new System.Windows.Forms.Label();
            this.lbAct2Fail = new System.Windows.Forms.Label();
            this.lbAct2Pass = new System.Windows.Forms.Label();
            this.lbAct2Ready = new System.Windows.Forms.Label();
            this.lbDisplaceValue = new SevenSegment.SevenSegmentArray();
            this.lbAct2Mode = new System.Windows.Forms.Label();
            this.lbAct1Fail = new System.Windows.Forms.Label();
            this.lbAct1Pass = new System.Windows.Forms.Label();
            this.lbAct1Ready = new System.Windows.Forms.Label();
            this.lbAct1Mode = new System.Windows.Forms.Label();
            this.lbInnerLight = new System.Windows.Forms.Label();
            this.btnInnerLight = new Glass.GlassButton();
            this.btnUVLamp2 = new Glass.GlassButton();
            this.btnUvLamp = new Glass.GlassButton();
            this.lbDoor8 = new System.Windows.Forms.Label();
            this.lbDoor7 = new System.Windows.Forms.Label();
            this.lbDoor3 = new System.Windows.Forms.Label();
            this.lbDoor4 = new System.Windows.Forms.Label();
            this.lbDoor6 = new System.Windows.Forms.Label();
            this.lbDoor5 = new System.Windows.Forms.Label();
            this.lbDoor2 = new System.Windows.Forms.Label();
            this.lbDoor1 = new System.Windows.Forms.Label();
            this.lbVCMLoadSensor = new System.Windows.Forms.Label();
            this.lbUnloadSensor = new System.Windows.Forms.Label();
            this.lbCure2Sensor = new System.Windows.Forms.Label();
            this.lbCure1Sensor = new System.Windows.Forms.Label();
            this.lbLensHeightSensor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbUnloadTrayExist = new System.Windows.Forms.Label();
            this.lbVCMTrayExist = new System.Windows.Forms.Label();
            this.lbLensTrayExist = new System.Windows.Forms.Label();
            this.lbPlateAngleExist = new System.Windows.Forms.Label();
            this.pnUnloadMagazine = new System.Windows.Forms.Panel();
            this.pnLensMagazine = new System.Windows.Forms.Panel();
            this.pnVCMMagazine = new System.Windows.Forms.Panel();
            this.lbLensHeadStatus = new System.Windows.Forms.Label();
            this.lbUnloadHeadStatus = new System.Windows.Forms.Label();
            this.lbVCMHeadStatus = new System.Windows.Forms.Label();
            this.lbPlateAngleStatus = new System.Windows.Forms.Label();
            this.lbCure2Status = new System.Windows.Forms.Label();
            this.lbCure1Status = new System.Windows.Forms.Label();
            this.lbVisionInsStatus = new System.Windows.Forms.Label();
            this.lbBonder2Status = new System.Windows.Forms.Label();
            this.lbBonder1Status = new System.Windows.Forms.Label();
            this.lbLensHeightStatus = new System.Windows.Forms.Label();
            this.lbLensInsertStatus = new System.Windows.Forms.Label();
            this.lbAct3Status = new System.Windows.Forms.Label();
            this.lbJigPlateAngleStatus = new System.Windows.Forms.Label();
            this.lbVCMLoadStatus = new System.Windows.Forms.Label();
            this.lbCleanJigStatus = new System.Windows.Forms.Label();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.lbNone = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lbExist = new System.Windows.Forms.Label();
            this.lbError = new System.Windows.Forms.Label();
            this.lbReady = new System.Windows.Forms.Label();
            this.picMainIndex = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbActuating3Total = new System.Windows.Forms.Label();
            this.lbSideHeightTotal = new System.Windows.Forms.Label();
            this.lbActuating2Total = new System.Windows.Forms.Label();
            this.lbSideAngleTotal = new System.Windows.Forms.Label();
            this.lbActuating1Total = new System.Windows.Forms.Label();
            this.lbVisionTotal = new System.Windows.Forms.Label();
            this.lbLensHeightTotal = new System.Windows.Forms.Label();
            this.lbOKTotal = new System.Windows.Forms.Label();
            this.btnYieldReset = new Glass.GlassButton();
            this.label3 = new System.Windows.Forms.Label();
            this.Yield_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lbSideAngleTrend = new System.Windows.Forms.Label();
            this.lbSideAngleTrendAverage = new System.Windows.Forms.Label();
            this.lbLensHeightSetProdcut = new System.Windows.Forms.Label();
            this.lbLensHeightSetPorductNumber = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SideAngle_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lbLensHeightTrend = new System.Windows.Forms.Label();
            this.lbLensHeightTrendAverage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LensHeight_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblZValue1 = new System.Windows.Forms.Label();
            this.lblZValue4 = new System.Windows.Forms.Label();
            this.lblZValue2 = new System.Windows.Forms.Label();
            this.lblZValue3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.blImgAngleValue_3 = new System.Windows.Forms.Label();
            this.blImgAngleValue_4 = new System.Windows.Forms.Label();
            this.blImgAngleValue_5 = new System.Windows.Forms.Label();
            this.blImgAngleValue_6 = new System.Windows.Forms.Label();
            this.blImgAngleValue_7 = new System.Windows.Forms.Label();
            this.blImgAngleValue_8 = new System.Windows.Forms.Label();
            this.blImgAngleValue_9 = new System.Windows.Forms.Label();
            this.blImgAngleValue_10 = new System.Windows.Forms.Label();
            this.blImgAngleValue_11 = new System.Windows.Forms.Label();
            this.blImgAngleValue_0 = new System.Windows.Forms.Label();
            this.blImgAngleValue_1 = new System.Windows.Forms.Label();
            this.blImgAngleValue_2 = new System.Windows.Forms.Label();
            this.btnAngle_All = new Glass.GlassButton();
            this.btnAngle_Single = new Glass.GlassButton();
            this.tpanel__Measuring = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.lbNGValue = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbNGType = new System.Windows.Forms.Label();
            this.lbNGTrayIndexTitle = new System.Windows.Forms.Label();
            this.lbNGIndex = new System.Windows.Forms.Label();
            this.btnNgSkip = new Glass.GlassButton();
            this.picNGMap = new System.Windows.Forms.PictureBox();
            this.lbNGTrayInfo = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnUnloadReset = new Glass.GlassButton();
            this.btnUnloadTraySkip = new Glass.GlassButton();
            this.lbUnloaderSlotNoTitle = new System.Windows.Forms.Label();
            this.lbUnloaderSlotNo = new System.Windows.Forms.Label();
            this.lbUnloaderTrayIndexYTitle = new System.Windows.Forms.Label();
            this.lbUnloaderTrayIndexY = new System.Windows.Forms.Label();
            this.lbUnloaderTrayIndexXTitle = new System.Windows.Forms.Label();
            this.lbUnloaderTrayIndexX = new System.Windows.Forms.Label();
            this.picUnloadMap = new System.Windows.Forms.PictureBox();
            this.lbUnloaderTrayInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLensReset = new Glass.GlassButton();
            this.btnLensSkip = new Glass.GlassButton();
            this.lbLensSlotNoTitle = new System.Windows.Forms.Label();
            this.lbLensSlotNo = new System.Windows.Forms.Label();
            this.lbLensTrayIndexYTitle = new System.Windows.Forms.Label();
            this.lbLensTrayIndexY = new System.Windows.Forms.Label();
            this.lbLensTrayIndexXTitle = new System.Windows.Forms.Label();
            this.lbLensTrayIndexX = new System.Windows.Forms.Label();
            this.picLensMap = new System.Windows.Forms.PictureBox();
            this.lbLensTrayInfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnVcmReset = new Glass.GlassButton();
            this.btnVcmSkip = new Glass.GlassButton();
            this.lbVCMSlotNoTitle = new System.Windows.Forms.Label();
            this.lbVCMSlotNo = new System.Windows.Forms.Label();
            this.lbVCMTrayIndexYTitle = new System.Windows.Forms.Label();
            this.lbVCMTrayIndexY = new System.Windows.Forms.Label();
            this.lbVCMTrayIndexXTitle = new System.Windows.Forms.Label();
            this.lbVCMTrayIndexX = new System.Windows.Forms.Label();
            this.picVCMMap = new System.Windows.Forms.PictureBox();
            this.lbVCMTrayInfo = new System.Windows.Forms.Label();
            this.btnTaskLog = new Glass.GlassButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblUseMES = new System.Windows.Forms.Label();
            this.lbDSensor = new System.Windows.Forms.Label();
            this.lbUVLamp = new System.Windows.Forms.Label();
            this.lbInterface = new System.Windows.Forms.Label();
            this.lbACTUATOR = new System.Windows.Forms.Label();
            this.lbMainAir = new System.Windows.Forms.Label();
            this.lbMC = new System.Windows.Forms.Label();
            this.lbDispenser = new System.Windows.Forms.Label();
            this.lbVision = new System.Windows.Forms.Label();
            this.btnLotEnd = new Glass.GlassButton();
            this.btnStop = new Glass.GlassButton();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.lbActuating3Mode = new System.Windows.Forms.Label();
            this.lbTopVisionStep = new System.Windows.Forms.Label();
            this.lbTopVisionMode = new System.Windows.Forms.Label();
            this.lbIndexStep = new System.Windows.Forms.Label();
            this.lbIndexMode = new System.Windows.Forms.Label();
            this.lbCleanJigStep = new System.Windows.Forms.Label();
            this.lbCleanJigMode = new System.Windows.Forms.Label();
            this.lbUnloadPickerStep = new System.Windows.Forms.Label();
            this.lbUnloadPickerMode = new System.Windows.Forms.Label();
            this.lbUnloaderStep = new System.Windows.Forms.Label();
            this.lbUnloaderMode = new System.Windows.Forms.Label();
            this.lbPlateAngleStep = new System.Windows.Forms.Label();
            this.lbPlateAngleMode = new System.Windows.Forms.Label();
            this.lbCuring2Step = new System.Windows.Forms.Label();
            this.lbCuring2Mode = new System.Windows.Forms.Label();
            this.lbCuring1Step = new System.Windows.Forms.Label();
            this.lbCuring1Mode = new System.Windows.Forms.Label();
            this.lbVisionInspectStep = new System.Windows.Forms.Label();
            this.lbVisionInspectMode = new System.Windows.Forms.Label();
            this.lbBonder2Step = new System.Windows.Forms.Label();
            this.lbBond2Mode = new System.Windows.Forms.Label();
            this.lbBonder1Step = new System.Windows.Forms.Label();
            this.lbBond1Mode = new System.Windows.Forms.Label();
            this.lbLensHeightStep = new System.Windows.Forms.Label();
            this.lbLensHeightMode = new System.Windows.Forms.Label();
            this.lbJigPlateStep = new System.Windows.Forms.Label();
            this.lbJigPlateMode = new System.Windows.Forms.Label();
            this.lbLensPickerStep = new System.Windows.Forms.Label();
            this.lbLensPickerMode = new System.Windows.Forms.Label();
            this.lbLensLoaderStep = new System.Windows.Forms.Label();
            this.lbLensLoaderMode = new System.Windows.Forms.Label();
            this.lbSequenceStatus = new System.Windows.Forms.Label();
            this.lbCycleTime = new System.Windows.Forms.Label();
            this.btnStart = new Glass.GlassButton();
            this.btnEnable = new Glass.GlassButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.lblmeseqpid = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblProdType = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblOperation = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNGTrayReset = new Glass.GlassButton();
            this.gbUnloaderMagazine = new System.Windows.Forms.GroupBox();
            this.btnUnloadMagazineFull = new Glass.GlassButton();
            this.label18 = new System.Windows.Forms.Label();
            this.btnUnloader_Magazine = new Glass.GlassButton();
            this.gbLensMagazine = new System.Windows.Forms.GroupBox();
            this.btnLensMagazineFull = new Glass.GlassButton();
            this.label16 = new System.Windows.Forms.Label();
            this.btnLens_Magazine = new Glass.GlassButton();
            this.gbVCMMagazine = new System.Windows.Forms.GroupBox();
            this.btnVCMMagazineFull = new Glass.GlassButton();
            this.lbFluxFeeder = new System.Windows.Forms.Label();
            this.btnVCM_Magazine = new Glass.GlassButton();
            this.lbMagazineControl = new System.Windows.Forms.Label();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.a1Panel11.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnMainIndex.SuspendLayout();
            this.pnBond2.SuspendLayout();
            this.pnBonder1.SuspendLayout();
            this.pnUnloadTrayChange.SuspendLayout();
            this.pnVCMTrayChange.SuspendLayout();
            this.pnLensTrayChange.SuspendLayout();
            this.gbStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMainIndex)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Yield_Chart)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SideAngle_Chart)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LensHeight_Chart)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNGMap)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUnloadMap)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLensMap)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVCMMap)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbUnloaderMagazine.SuspendLayout();
            this.gbLensMagazine.SuspendLayout();
            this.gbVCMMagazine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // ImgLstState
            // 
            this.ImgLstState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgLstState.ImageStream")));
            this.ImgLstState.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgLstState.Images.SetKeyName(0, "Glass_None.png");
            this.ImgLstState.Images.SetKeyName(1, "Glass_Exist.png");
            this.ImgLstState.Images.SetKeyName(2, "Glass_Processing.png");
            this.ImgLstState.Images.SetKeyName(3, "Glass_Processed.png");
            this.ImgLstState.Images.SetKeyName(4, "Distable.ico");
            this.ImgLstState.Images.SetKeyName(5, "fault.ico");
            this.ImgLstState.Images.SetKeyName(6, "fire.ico");
            this.ImgLstState.Images.SetKeyName(7, "normal.ico");
            // 
            // lbLedTactTime
            // 
            this.lbLedTactTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbLedTactTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLedTactTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLedTactTime.ForeColor = System.Drawing.Color.Black;
            this.lbLedTactTime.Location = new System.Drawing.Point(3, 720);
            this.lbLedTactTime.Name = "lbLedTactTime";
            this.lbLedTactTime.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbLedTactTime.Size = new System.Drawing.Size(220, 35);
            this.lbLedTactTime.TabIndex = 1702;
            this.lbLedTactTime.Text = "0.5 sec";
            this.lbLedTactTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbEQID
            // 
            this.lbEQID.BackColor = System.Drawing.Color.Gainsboro;
            this.lbEQID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbEQID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEQID.ForeColor = System.Drawing.Color.Black;
            this.lbEQID.Location = new System.Drawing.Point(3, 26);
            this.lbEQID.Name = "lbEQID";
            this.lbEQID.Size = new System.Drawing.Size(91, 23);
            this.lbEQID.TabIndex = 1712;
            this.lbEQID.Tag = "";
            this.lbEQID.Text = "EQ ID";
            this.lbEQID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_EqpID
            // 
            this.label_EqpID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label_EqpID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_EqpID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EqpID.ForeColor = System.Drawing.Color.Black;
            this.label_EqpID.Location = new System.Drawing.Point(103, 26);
            this.label_EqpID.Name = "label_EqpID";
            this.label_EqpID.Size = new System.Drawing.Size(120, 23);
            this.label_EqpID.TabIndex = 1713;
            this.label_EqpID.Tag = "";
            this.label_EqpID.Text = "# 1";
            this.label_EqpID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMachineMode
            // 
            this.lbMachineMode.BackColor = System.Drawing.Color.Gainsboro;
            this.lbMachineMode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMachineMode.ForeColor = System.Drawing.Color.Navy;
            this.lbMachineMode.Location = new System.Drawing.Point(0, -1);
            this.lbMachineMode.Name = "lbMachineMode";
            this.lbMachineMode.Size = new System.Drawing.Size(225, 23);
            this.lbMachineMode.TabIndex = 1716;
            this.lbMachineMode.Tag = "";
            this.lbMachineMode.Text = "MACHINE STATUS";
            this.lbMachineMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVCMLoaderMode
            // 
            this.lbVCMLoaderMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbVCMLoaderMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMLoaderMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMLoaderMode.ForeColor = System.Drawing.Color.Black;
            this.lbVCMLoaderMode.Location = new System.Drawing.Point(3, 77);
            this.lbVCMLoaderMode.Name = "lbVCMLoaderMode";
            this.lbVCMLoaderMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbVCMLoaderMode.Size = new System.Drawing.Size(220, 32);
            this.lbVCMLoaderMode.TabIndex = 1719;
            this.lbVCMLoaderMode.Text = "STEP";
            this.lbVCMLoaderMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbVCMLoaderMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbVCMPickerMode
            // 
            this.lbVCMPickerMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbVCMPickerMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMPickerMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMPickerMode.ForeColor = System.Drawing.Color.Black;
            this.lbVCMPickerMode.Location = new System.Drawing.Point(3, 110);
            this.lbVCMPickerMode.Name = "lbVCMPickerMode";
            this.lbVCMPickerMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbVCMPickerMode.Size = new System.Drawing.Size(220, 32);
            this.lbVCMPickerMode.TabIndex = 1720;
            this.lbVCMPickerMode.Text = "STEP";
            this.lbVCMPickerMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbVCMPickerMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbVCMLoaderStep
            // 
            this.lbVCMLoaderStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbVCMLoaderStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMLoaderStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMLoaderStep.ForeColor = System.Drawing.Color.Black;
            this.lbVCMLoaderStep.Location = new System.Drawing.Point(3, 77);
            this.lbVCMLoaderStep.Name = "lbVCMLoaderStep";
            this.lbVCMLoaderStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbVCMLoaderStep.Size = new System.Drawing.Size(95, 22);
            this.lbVCMLoaderStep.TabIndex = 1722;
            this.lbVCMLoaderStep.Text = "VCM LOADER";
            this.lbVCMLoaderStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVCMPickerStep
            // 
            this.lbVCMPickerStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbVCMPickerStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMPickerStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMPickerStep.ForeColor = System.Drawing.Color.Black;
            this.lbVCMPickerStep.Location = new System.Drawing.Point(3, 110);
            this.lbVCMPickerStep.Name = "lbVCMPickerStep";
            this.lbVCMPickerStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbVCMPickerStep.Size = new System.Drawing.Size(95, 20);
            this.lbVCMPickerStep.TabIndex = 1723;
            this.lbVCMPickerStep.Text = "VCM PICKER";
            this.lbVCMPickerStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTactTimeLabel
            // 
            this.lbTactTimeLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.lbTactTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTactTimeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTactTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.lbTactTimeLabel.Location = new System.Drawing.Point(3, 720);
            this.lbTactTimeLabel.Name = "lbTactTimeLabel";
            this.lbTactTimeLabel.Size = new System.Drawing.Size(95, 20);
            this.lbTactTimeLabel.TabIndex = 1725;
            this.lbTactTimeLabel.Text = "TACT TIME";
            this.lbTactTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // a1Panel11
            // 
            this.a1Panel11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.a1Panel11.BorderColor = System.Drawing.Color.Silver;
            this.a1Panel11.BorderWidth = 2;
            this.a1Panel11.Controls.Add(this.tabControl1);
            this.a1Panel11.Controls.Add(this.panel5);
            this.a1Panel11.Controls.Add(this.panel4);
            this.a1Panel11.Controls.Add(this.panel3);
            this.a1Panel11.Controls.Add(this.panel2);
            this.a1Panel11.Controls.Add(this.btnTaskLog);
            this.a1Panel11.Controls.Add(this.panel9);
            this.a1Panel11.Controls.Add(this.btnLotEnd);
            this.a1Panel11.Controls.Add(this.btnStop);
            this.a1Panel11.Controls.Add(this.panel7);
            this.a1Panel11.Controls.Add(this.btnStart);
            this.a1Panel11.Controls.Add(this.btnEnable);
            this.a1Panel11.Controls.Add(this.panel1);
            this.a1Panel11.GradientEndColor = System.Drawing.Color.WhiteSmoke;
            this.a1Panel11.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.a1Panel11.Image = null;
            this.a1Panel11.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel11.Location = new System.Drawing.Point(-1, -9);
            this.a1Panel11.Name = "a1Panel11";
            this.a1Panel11.RoundCornerRadius = 12;
            this.a1Panel11.ShadowOffSet = 1;
            this.a1Panel11.Size = new System.Drawing.Size(1930, 1100);
            this.a1Panel11.TabIndex = 192;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(234, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1154, 808);
            this.tabControl1.TabIndex = 1886;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnMainIndex);
            this.tabPage1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1146, 777);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Index Layout";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnMainIndex
            // 
            this.pnMainIndex.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnMainIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnMainIndex.Controls.Add(this.lbAct3Fail);
            this.pnMainIndex.Controls.Add(this.lbAct3Pass);
            this.pnMainIndex.Controls.Add(this.lbAct3Ready);
            this.pnMainIndex.Controls.Add(this.lbAct3Mode);
            this.pnMainIndex.Controls.Add(this.lbDisplaceValueLensZTorque);
            this.pnMainIndex.Controls.Add(this.lbDisplaceValueBond2);
            this.pnMainIndex.Controls.Add(this.lbDisplaceValueBond1);
            this.pnMainIndex.Controls.Add(this.btnUnloadTHome);
            this.pnMainIndex.Controls.Add(this.pnBond2);
            this.pnMainIndex.Controls.Add(this.pnBonder1);
            this.pnMainIndex.Controls.Add(this.lbUVLamp2LifeTime);
            this.pnMainIndex.Controls.Add(this.lbUVLamp1LifeTime);
            this.pnMainIndex.Controls.Add(this.btnDailyCountReset);
            this.pnMainIndex.Controls.Add(this.label12);
            this.pnMainIndex.Controls.Add(this.lbDailyNGCount);
            this.pnMainIndex.Controls.Add(this.label8);
            this.pnMainIndex.Controls.Add(this.lbDailyOKCount);
            this.pnMainIndex.Controls.Add(this.label10);
            this.pnMainIndex.Controls.Add(this.lbDailyTotalCount);
            this.pnMainIndex.Controls.Add(this.btnTestButton);
            this.pnMainIndex.Controls.Add(this.pnUnloadTrayChange);
            this.pnMainIndex.Controls.Add(this.pnVCMTrayChange);
            this.pnMainIndex.Controls.Add(this.pnLensTrayChange);
            this.pnMainIndex.Controls.Add(this.lbSideAngleValue);
            this.pnMainIndex.Controls.Add(this.lbIndexNum);
            this.pnMainIndex.Controls.Add(this.lbAct2Fail);
            this.pnMainIndex.Controls.Add(this.lbAct2Pass);
            this.pnMainIndex.Controls.Add(this.lbAct2Ready);
            this.pnMainIndex.Controls.Add(this.lbDisplaceValue);
            this.pnMainIndex.Controls.Add(this.lbAct2Mode);
            this.pnMainIndex.Controls.Add(this.lbAct1Fail);
            this.pnMainIndex.Controls.Add(this.lbAct1Pass);
            this.pnMainIndex.Controls.Add(this.lbAct1Ready);
            this.pnMainIndex.Controls.Add(this.lbAct1Mode);
            this.pnMainIndex.Controls.Add(this.lbInnerLight);
            this.pnMainIndex.Controls.Add(this.btnInnerLight);
            this.pnMainIndex.Controls.Add(this.btnUVLamp2);
            this.pnMainIndex.Controls.Add(this.btnUvLamp);
            this.pnMainIndex.Controls.Add(this.lbDoor8);
            this.pnMainIndex.Controls.Add(this.lbDoor7);
            this.pnMainIndex.Controls.Add(this.lbDoor3);
            this.pnMainIndex.Controls.Add(this.lbDoor4);
            this.pnMainIndex.Controls.Add(this.lbDoor6);
            this.pnMainIndex.Controls.Add(this.lbDoor5);
            this.pnMainIndex.Controls.Add(this.lbDoor2);
            this.pnMainIndex.Controls.Add(this.lbDoor1);
            this.pnMainIndex.Controls.Add(this.lbVCMLoadSensor);
            this.pnMainIndex.Controls.Add(this.lbUnloadSensor);
            this.pnMainIndex.Controls.Add(this.lbCure2Sensor);
            this.pnMainIndex.Controls.Add(this.lbCure1Sensor);
            this.pnMainIndex.Controls.Add(this.lbLensHeightSensor);
            this.pnMainIndex.Controls.Add(this.label6);
            this.pnMainIndex.Controls.Add(this.label5);
            this.pnMainIndex.Controls.Add(this.label4);
            this.pnMainIndex.Controls.Add(this.lbUnloadTrayExist);
            this.pnMainIndex.Controls.Add(this.lbVCMTrayExist);
            this.pnMainIndex.Controls.Add(this.lbLensTrayExist);
            this.pnMainIndex.Controls.Add(this.lbPlateAngleExist);
            this.pnMainIndex.Controls.Add(this.pnUnloadMagazine);
            this.pnMainIndex.Controls.Add(this.pnLensMagazine);
            this.pnMainIndex.Controls.Add(this.pnVCMMagazine);
            this.pnMainIndex.Controls.Add(this.lbLensHeadStatus);
            this.pnMainIndex.Controls.Add(this.lbUnloadHeadStatus);
            this.pnMainIndex.Controls.Add(this.lbVCMHeadStatus);
            this.pnMainIndex.Controls.Add(this.lbPlateAngleStatus);
            this.pnMainIndex.Controls.Add(this.lbCure2Status);
            this.pnMainIndex.Controls.Add(this.lbCure1Status);
            this.pnMainIndex.Controls.Add(this.lbVisionInsStatus);
            this.pnMainIndex.Controls.Add(this.lbBonder2Status);
            this.pnMainIndex.Controls.Add(this.lbBonder1Status);
            this.pnMainIndex.Controls.Add(this.lbLensHeightStatus);
            this.pnMainIndex.Controls.Add(this.lbLensInsertStatus);
            this.pnMainIndex.Controls.Add(this.lbAct3Status);
            this.pnMainIndex.Controls.Add(this.lbJigPlateAngleStatus);
            this.pnMainIndex.Controls.Add(this.lbVCMLoadStatus);
            this.pnMainIndex.Controls.Add(this.lbCleanJigStatus);
            this.pnMainIndex.Controls.Add(this.gbStatus);
            this.pnMainIndex.Controls.Add(this.picMainIndex);
            this.pnMainIndex.Font = new System.Drawing.Font("Tahoma", 9F);
            this.pnMainIndex.Location = new System.Drawing.Point(-3, -24);
            this.pnMainIndex.Name = "pnMainIndex";
            this.pnMainIndex.Size = new System.Drawing.Size(1153, 816);
            this.pnMainIndex.TabIndex = 1843;
            // 
            // lbAct3Fail
            // 
            this.lbAct3Fail.BackColor = System.Drawing.Color.Red;
            this.lbAct3Fail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct3Fail.Location = new System.Drawing.Point(346, 402);
            this.lbAct3Fail.Name = "lbAct3Fail";
            this.lbAct3Fail.Size = new System.Drawing.Size(16, 10);
            this.lbAct3Fail.TabIndex = 1942;
            this.lbAct3Fail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct3Pass
            // 
            this.lbAct3Pass.BackColor = System.Drawing.Color.Lime;
            this.lbAct3Pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct3Pass.Location = new System.Drawing.Point(308, 402);
            this.lbAct3Pass.Name = "lbAct3Pass";
            this.lbAct3Pass.Size = new System.Drawing.Size(16, 10);
            this.lbAct3Pass.TabIndex = 1941;
            this.lbAct3Pass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct3Ready
            // 
            this.lbAct3Ready.BackColor = System.Drawing.Color.Lime;
            this.lbAct3Ready.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct3Ready.Location = new System.Drawing.Point(270, 402);
            this.lbAct3Ready.Name = "lbAct3Ready";
            this.lbAct3Ready.Size = new System.Drawing.Size(16, 10);
            this.lbAct3Ready.TabIndex = 1940;
            this.lbAct3Ready.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct3Mode
            // 
            this.lbAct3Mode.BackColor = System.Drawing.Color.White;
            this.lbAct3Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct3Mode.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbAct3Mode.Location = new System.Drawing.Point(260, 389);
            this.lbAct3Mode.Name = "lbAct3Mode";
            this.lbAct3Mode.Size = new System.Drawing.Size(112, 26);
            this.lbAct3Mode.TabIndex = 1939;
            this.lbAct3Mode.Text = "Actuator";
            this.lbAct3Mode.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbDisplaceValueLensZTorque
            // 
            this.lbDisplaceValueLensZTorque.ArrayCount = 6;
            this.lbDisplaceValueLensZTorque.ColorBackground = System.Drawing.Color.Empty;
            this.lbDisplaceValueLensZTorque.ColorDark = System.Drawing.Color.Empty;
            this.lbDisplaceValueLensZTorque.ColorLight = System.Drawing.Color.Lime;
            this.lbDisplaceValueLensZTorque.DecimalShow = true;
            this.lbDisplaceValueLensZTorque.ElementPadding = new System.Windows.Forms.Padding(4);
            this.lbDisplaceValueLensZTorque.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisplaceValueLensZTorque.Location = new System.Drawing.Point(154, 391);
            this.lbDisplaceValueLensZTorque.Name = "lbDisplaceValueLensZTorque";
            this.lbDisplaceValueLensZTorque.Size = new System.Drawing.Size(103, 24);
            this.lbDisplaceValueLensZTorque.TabIndex = 1938;
            this.lbDisplaceValueLensZTorque.Text = "-0.1234";
            // 
            // lbDisplaceValueBond2
            // 
            this.lbDisplaceValueBond2.ArrayCount = 6;
            this.lbDisplaceValueBond2.ColorBackground = System.Drawing.Color.Empty;
            this.lbDisplaceValueBond2.ColorDark = System.Drawing.Color.Empty;
            this.lbDisplaceValueBond2.ColorLight = System.Drawing.Color.Lime;
            this.lbDisplaceValueBond2.DecimalShow = true;
            this.lbDisplaceValueBond2.ElementPadding = new System.Windows.Forms.Padding(4);
            this.lbDisplaceValueBond2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisplaceValueBond2.Location = new System.Drawing.Point(670, 215);
            this.lbDisplaceValueBond2.Name = "lbDisplaceValueBond2";
            this.lbDisplaceValueBond2.Size = new System.Drawing.Size(103, 24);
            this.lbDisplaceValueBond2.TabIndex = 1937;
            this.lbDisplaceValueBond2.Text = "-0.1234";
            // 
            // lbDisplaceValueBond1
            // 
            this.lbDisplaceValueBond1.ArrayCount = 6;
            this.lbDisplaceValueBond1.ColorBackground = System.Drawing.Color.Empty;
            this.lbDisplaceValueBond1.ColorDark = System.Drawing.Color.Empty;
            this.lbDisplaceValueBond1.ColorLight = System.Drawing.Color.Lime;
            this.lbDisplaceValueBond1.DecimalShow = true;
            this.lbDisplaceValueBond1.ElementPadding = new System.Windows.Forms.Padding(4);
            this.lbDisplaceValueBond1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisplaceValueBond1.Location = new System.Drawing.Point(528, 164);
            this.lbDisplaceValueBond1.Name = "lbDisplaceValueBond1";
            this.lbDisplaceValueBond1.Size = new System.Drawing.Size(103, 24);
            this.lbDisplaceValueBond1.TabIndex = 1936;
            this.lbDisplaceValueBond1.Text = "-0.1234";
            // 
            // btnUnloadTHome
            // 
            this.btnUnloadTHome.BackColor = System.Drawing.Color.Silver;
            this.btnUnloadTHome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnloadTHome.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnUnloadTHome.GlowColor = System.Drawing.Color.Transparent;
            this.btnUnloadTHome.InnerBorderColor = System.Drawing.Color.White;
            this.btnUnloadTHome.Location = new System.Drawing.Point(1031, 230);
            this.btnUnloadTHome.Name = "btnUnloadTHome";
            this.btnUnloadTHome.OuterBorderColor = System.Drawing.Color.Black;
            this.btnUnloadTHome.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUnloadTHome.ShineColor = System.Drawing.Color.Silver;
            this.btnUnloadTHome.Size = new System.Drawing.Size(116, 42);
            this.btnUnloadTHome.TabIndex = 1935;
            this.btnUnloadTHome.TabStop = false;
            this.btnUnloadTHome.Tag = "0";
            this.btnUnloadTHome.Text = "UNLOAD AXIS T HOME";
            this.btnUnloadTHome.Click += new System.EventHandler(this.btnUnloadTHome_Click);
            // 
            // pnBond2
            // 
            this.pnBond2.Controls.Add(this.btnBond2Reset);
            this.pnBond2.Controls.Add(this.lbBond2JetCount);
            this.pnBond2.Controls.Add(this.lbBond2TotalCount);
            this.pnBond2.Controls.Add(this.proBonder2);
            this.pnBond2.Controls.Add(this.label15);
            this.pnBond2.Location = new System.Drawing.Point(840, 48);
            this.pnBond2.Name = "pnBond2";
            this.pnBond2.Size = new System.Drawing.Size(112, 140);
            this.pnBond2.TabIndex = 1934;
            // 
            // btnBond2Reset
            // 
            this.btnBond2Reset.BackColor = System.Drawing.Color.Silver;
            this.btnBond2Reset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBond2Reset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBond2Reset.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnBond2Reset.GlowColor = System.Drawing.Color.Transparent;
            this.btnBond2Reset.InnerBorderColor = System.Drawing.Color.White;
            this.btnBond2Reset.Location = new System.Drawing.Point(30, 51);
            this.btnBond2Reset.Name = "btnBond2Reset";
            this.btnBond2Reset.OuterBorderColor = System.Drawing.Color.Black;
            this.btnBond2Reset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBond2Reset.ShineColor = System.Drawing.Color.Silver;
            this.btnBond2Reset.Size = new System.Drawing.Size(82, 59);
            this.btnBond2Reset.TabIndex = 1936;
            this.btnBond2Reset.TabStop = false;
            this.btnBond2Reset.Tag = "1";
            this.btnBond2Reset.Text = "RESET";
            this.btnBond2Reset.Click += new System.EventHandler(this.btnJettingCountReset_Click);
            // 
            // lbBond2JetCount
            // 
            this.lbBond2JetCount.BackColor = System.Drawing.Color.White;
            this.lbBond2JetCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBond2JetCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbBond2JetCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbBond2JetCount.Location = new System.Drawing.Point(30, 110);
            this.lbBond2JetCount.Name = "lbBond2JetCount";
            this.lbBond2JetCount.Size = new System.Drawing.Size(82, 30);
            this.lbBond2JetCount.TabIndex = 1935;
            this.lbBond2JetCount.Text = "1000";
            this.lbBond2JetCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbBond2TotalCount
            // 
            this.lbBond2TotalCount.BackColor = System.Drawing.Color.White;
            this.lbBond2TotalCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBond2TotalCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbBond2TotalCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbBond2TotalCount.Location = new System.Drawing.Point(30, 21);
            this.lbBond2TotalCount.Name = "lbBond2TotalCount";
            this.lbBond2TotalCount.Size = new System.Drawing.Size(82, 30);
            this.lbBond2TotalCount.TabIndex = 1934;
            this.lbBond2TotalCount.Tag = "1";
            this.lbBond2TotalCount.Text = "1000000";
            this.lbBond2TotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbBond2TotalCount.Click += new System.EventHandler(this.JettingCount_Click);
            // 
            // proBonder2
            // 
            this.proBonder2.Dock = System.Windows.Forms.DockStyle.Left;
            this.proBonder2.Location = new System.Drawing.Point(0, 21);
            this.proBonder2.Name = "proBonder2";
            this.proBonder2.Size = new System.Drawing.Size(30, 119);
            this.proBonder2.TabIndex = 1933;
            this.proBonder2.Value = 30;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 21);
            this.label15.TabIndex = 1932;
            this.label15.Text = "Bonder #2";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnBonder1
            // 
            this.pnBonder1.Controls.Add(this.btnBond1Reset);
            this.pnBonder1.Controls.Add(this.lbBond1JetCount);
            this.pnBonder1.Controls.Add(this.lbBond1TotalCount);
            this.pnBonder1.Controls.Add(this.proBonder1);
            this.pnBonder1.Controls.Add(this.lbBond1Title);
            this.pnBonder1.Location = new System.Drawing.Point(714, 48);
            this.pnBonder1.Name = "pnBonder1";
            this.pnBonder1.Size = new System.Drawing.Size(112, 140);
            this.pnBonder1.TabIndex = 1933;
            // 
            // btnBond1Reset
            // 
            this.btnBond1Reset.BackColor = System.Drawing.Color.Silver;
            this.btnBond1Reset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBond1Reset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBond1Reset.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnBond1Reset.GlowColor = System.Drawing.Color.Transparent;
            this.btnBond1Reset.InnerBorderColor = System.Drawing.Color.White;
            this.btnBond1Reset.Location = new System.Drawing.Point(30, 51);
            this.btnBond1Reset.Name = "btnBond1Reset";
            this.btnBond1Reset.OuterBorderColor = System.Drawing.Color.Black;
            this.btnBond1Reset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBond1Reset.ShineColor = System.Drawing.Color.Silver;
            this.btnBond1Reset.Size = new System.Drawing.Size(82, 59);
            this.btnBond1Reset.TabIndex = 1936;
            this.btnBond1Reset.TabStop = false;
            this.btnBond1Reset.Tag = "0";
            this.btnBond1Reset.Text = "RESET";
            this.btnBond1Reset.Click += new System.EventHandler(this.btnJettingCountReset_Click);
            // 
            // lbBond1JetCount
            // 
            this.lbBond1JetCount.BackColor = System.Drawing.Color.White;
            this.lbBond1JetCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBond1JetCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbBond1JetCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbBond1JetCount.Location = new System.Drawing.Point(30, 110);
            this.lbBond1JetCount.Name = "lbBond1JetCount";
            this.lbBond1JetCount.Size = new System.Drawing.Size(82, 30);
            this.lbBond1JetCount.TabIndex = 1935;
            this.lbBond1JetCount.Text = "1000";
            this.lbBond1JetCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbBond1TotalCount
            // 
            this.lbBond1TotalCount.BackColor = System.Drawing.Color.White;
            this.lbBond1TotalCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBond1TotalCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbBond1TotalCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbBond1TotalCount.Location = new System.Drawing.Point(30, 21);
            this.lbBond1TotalCount.Name = "lbBond1TotalCount";
            this.lbBond1TotalCount.Size = new System.Drawing.Size(82, 30);
            this.lbBond1TotalCount.TabIndex = 1934;
            this.lbBond1TotalCount.Tag = "0";
            this.lbBond1TotalCount.Text = "1000000";
            this.lbBond1TotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbBond1TotalCount.Click += new System.EventHandler(this.JettingCount_Click);
            // 
            // proBonder1
            // 
            this.proBonder1.Dock = System.Windows.Forms.DockStyle.Left;
            this.proBonder1.Location = new System.Drawing.Point(0, 21);
            this.proBonder1.Name = "proBonder1";
            this.proBonder1.Size = new System.Drawing.Size(30, 119);
            this.proBonder1.TabIndex = 1933;
            this.proBonder1.Value = 30;
            // 
            // lbBond1Title
            // 
            this.lbBond1Title.BackColor = System.Drawing.Color.White;
            this.lbBond1Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBond1Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbBond1Title.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbBond1Title.Location = new System.Drawing.Point(0, 0);
            this.lbBond1Title.Name = "lbBond1Title";
            this.lbBond1Title.Size = new System.Drawing.Size(112, 21);
            this.lbBond1Title.TabIndex = 1932;
            this.lbBond1Title.Text = "Bonder #1";
            this.lbBond1Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbUVLamp2LifeTime
            // 
            this.lbUVLamp2LifeTime.BackColor = System.Drawing.Color.White;
            this.lbUVLamp2LifeTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUVLamp2LifeTime.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbUVLamp2LifeTime.Location = new System.Drawing.Point(694, 293);
            this.lbUVLamp2LifeTime.Name = "lbUVLamp2LifeTime";
            this.lbUVLamp2LifeTime.Size = new System.Drawing.Size(276, 26);
            this.lbUVLamp2LifeTime.TabIndex = 1932;
            this.lbUVLamp2LifeTime.Text = "UV LAMP 2 LIFE TIME WARNING !";
            this.lbUVLamp2LifeTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbUVLamp1LifeTime
            // 
            this.lbUVLamp1LifeTime.BackColor = System.Drawing.Color.White;
            this.lbUVLamp1LifeTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUVLamp1LifeTime.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbUVLamp1LifeTime.Location = new System.Drawing.Point(694, 261);
            this.lbUVLamp1LifeTime.Name = "lbUVLamp1LifeTime";
            this.lbUVLamp1LifeTime.Size = new System.Drawing.Size(276, 26);
            this.lbUVLamp1LifeTime.TabIndex = 1931;
            this.lbUVLamp1LifeTime.Text = "UV LAMP 1 LIFE TIME WARNING !";
            this.lbUVLamp1LifeTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnDailyCountReset
            // 
            this.btnDailyCountReset.BackColor = System.Drawing.Color.White;
            this.btnDailyCountReset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDailyCountReset.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnDailyCountReset.GlowColor = System.Drawing.Color.Transparent;
            this.btnDailyCountReset.InnerBorderColor = System.Drawing.Color.White;
            this.btnDailyCountReset.Location = new System.Drawing.Point(292, 48);
            this.btnDailyCountReset.Name = "btnDailyCountReset";
            this.btnDailyCountReset.OuterBorderColor = System.Drawing.Color.Black;
            this.btnDailyCountReset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDailyCountReset.Size = new System.Drawing.Size(92, 26);
            this.btnDailyCountReset.TabIndex = 1930;
            this.btnDailyCountReset.TabStop = false;
            this.btnDailyCountReset.Tag = "0";
            this.btnDailyCountReset.Text = "RESET";
            this.btnDailyCountReset.Click += new System.EventHandler(this.btnDailyCountReset_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Gainsboro;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(101, 94);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label12.Size = new System.Drawing.Size(94, 22);
            this.label12.TabIndex = 1929;
            this.label12.Text = "NG";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDailyNGCount
            // 
            this.lbDailyNGCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDailyNGCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDailyNGCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDailyNGCount.ForeColor = System.Drawing.Color.Black;
            this.lbDailyNGCount.Location = new System.Drawing.Point(196, 94);
            this.lbDailyNGCount.Name = "lbDailyNGCount";
            this.lbDailyNGCount.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDailyNGCount.Size = new System.Drawing.Size(92, 22);
            this.lbDailyNGCount.TabIndex = 1928;
            this.lbDailyNGCount.Text = "3";
            this.lbDailyNGCount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(101, 71);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label8.Size = new System.Drawing.Size(94, 22);
            this.label8.TabIndex = 1927;
            this.label8.Text = "OK";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDailyOKCount
            // 
            this.lbDailyOKCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDailyOKCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDailyOKCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDailyOKCount.ForeColor = System.Drawing.Color.Black;
            this.lbDailyOKCount.Location = new System.Drawing.Point(196, 71);
            this.lbDailyOKCount.Name = "lbDailyOKCount";
            this.lbDailyOKCount.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDailyOKCount.Size = new System.Drawing.Size(92, 22);
            this.lbDailyOKCount.TabIndex = 1926;
            this.lbDailyOKCount.Text = "3";
            this.lbDailyOKCount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Gainsboro;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(101, 48);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label10.Size = new System.Drawing.Size(94, 22);
            this.label10.TabIndex = 1925;
            this.label10.Text = "TOTAL";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDailyTotalCount
            // 
            this.lbDailyTotalCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDailyTotalCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDailyTotalCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDailyTotalCount.ForeColor = System.Drawing.Color.Black;
            this.lbDailyTotalCount.Location = new System.Drawing.Point(196, 48);
            this.lbDailyTotalCount.Name = "lbDailyTotalCount";
            this.lbDailyTotalCount.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDailyTotalCount.Size = new System.Drawing.Size(92, 22);
            this.lbDailyTotalCount.TabIndex = 1924;
            this.lbDailyTotalCount.Text = "3";
            this.lbDailyTotalCount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnTestButton
            // 
            this.btnTestButton.BackColor = System.Drawing.Color.Silver;
            this.btnTestButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnTestButton.GlowColor = System.Drawing.Color.Transparent;
            this.btnTestButton.InnerBorderColor = System.Drawing.Color.White;
            this.btnTestButton.Location = new System.Drawing.Point(1031, 602);
            this.btnTestButton.Name = "btnTestButton";
            this.btnTestButton.OuterBorderColor = System.Drawing.Color.Black;
            this.btnTestButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnTestButton.ShineColor = System.Drawing.Color.Silver;
            this.btnTestButton.Size = new System.Drawing.Size(116, 42);
            this.btnTestButton.TabIndex = 1923;
            this.btnTestButton.TabStop = false;
            this.btnTestButton.Tag = "0";
            this.btnTestButton.Text = "TEST";
            this.btnTestButton.Click += new System.EventHandler(this.btnTestButton_Click);
            // 
            // pnUnloadTrayChange
            // 
            this.pnUnloadTrayChange.BackColor = System.Drawing.Color.White;
            this.pnUnloadTrayChange.Controls.Add(this.pbUnloadTray);
            this.pnUnloadTrayChange.Controls.Add(this.lbUnloadTrayChangeText);
            this.pnUnloadTrayChange.ForeColor = System.Drawing.Color.Navy;
            this.pnUnloadTrayChange.Location = new System.Drawing.Point(620, 510);
            this.pnUnloadTrayChange.Name = "pnUnloadTrayChange";
            this.pnUnloadTrayChange.Size = new System.Drawing.Size(355, 251);
            this.pnUnloadTrayChange.TabIndex = 1922;
            // 
            // pbUnloadTray
            // 
            this.pbUnloadTray.Location = new System.Drawing.Point(30, 163);
            this.pbUnloadTray.Name = "pbUnloadTray";
            this.pbUnloadTray.Size = new System.Drawing.Size(302, 49);
            this.pbUnloadTray.TabIndex = 2;
            // 
            // lbUnloadTrayChangeText
            // 
            this.lbUnloadTrayChangeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloadTrayChangeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbUnloadTrayChangeText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbUnloadTrayChangeText.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lbUnloadTrayChangeText.Location = new System.Drawing.Point(0, 0);
            this.lbUnloadTrayChangeText.Name = "lbUnloadTrayChangeText";
            this.lbUnloadTrayChangeText.Size = new System.Drawing.Size(355, 251);
            this.lbUnloadTrayChangeText.TabIndex = 0;
            this.lbUnloadTrayChangeText.Text = "Tray Changing ...";
            this.lbUnloadTrayChangeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnVCMTrayChange
            // 
            this.pnVCMTrayChange.BackColor = System.Drawing.Color.White;
            this.pnVCMTrayChange.Controls.Add(this.pbVcmTray);
            this.pnVCMTrayChange.Controls.Add(this.lbVcmTrayChangeText);
            this.pnVCMTrayChange.ForeColor = System.Drawing.Color.Navy;
            this.pnVCMTrayChange.Location = new System.Drawing.Point(92, 510);
            this.pnVCMTrayChange.Name = "pnVCMTrayChange";
            this.pnVCMTrayChange.Size = new System.Drawing.Size(355, 251);
            this.pnVCMTrayChange.TabIndex = 1922;
            // 
            // pbVcmTray
            // 
            this.pbVcmTray.Location = new System.Drawing.Point(26, 163);
            this.pbVcmTray.Name = "pbVcmTray";
            this.pbVcmTray.Size = new System.Drawing.Size(302, 49);
            this.pbVcmTray.TabIndex = 2;
            // 
            // lbVcmTrayChangeText
            // 
            this.lbVcmTrayChangeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVcmTrayChangeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVcmTrayChangeText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbVcmTrayChangeText.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lbVcmTrayChangeText.Location = new System.Drawing.Point(0, 0);
            this.lbVcmTrayChangeText.Name = "lbVcmTrayChangeText";
            this.lbVcmTrayChangeText.Size = new System.Drawing.Size(355, 251);
            this.lbVcmTrayChangeText.TabIndex = 0;
            this.lbVcmTrayChangeText.Text = "Tray Changing ...";
            this.lbVcmTrayChangeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnLensTrayChange
            // 
            this.pnLensTrayChange.BackColor = System.Drawing.Color.White;
            this.pnLensTrayChange.Controls.Add(this.pbLensTray);
            this.pnLensTrayChange.Controls.Add(this.lbLensTrayChangeText);
            this.pnLensTrayChange.ForeColor = System.Drawing.Color.Navy;
            this.pnLensTrayChange.Location = new System.Drawing.Point(100, 130);
            this.pnLensTrayChange.Name = "pnLensTrayChange";
            this.pnLensTrayChange.Size = new System.Drawing.Size(355, 251);
            this.pnLensTrayChange.TabIndex = 1921;
            // 
            // pbLensTray
            // 
            this.pbLensTray.Location = new System.Drawing.Point(22, 164);
            this.pbLensTray.Name = "pbLensTray";
            this.pbLensTray.Size = new System.Drawing.Size(302, 49);
            this.pbLensTray.TabIndex = 1;
            // 
            // lbLensTrayChangeText
            // 
            this.lbLensTrayChangeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensTrayChangeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLensTrayChangeText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbLensTrayChangeText.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lbLensTrayChangeText.Location = new System.Drawing.Point(0, 0);
            this.lbLensTrayChangeText.Name = "lbLensTrayChangeText";
            this.lbLensTrayChangeText.Size = new System.Drawing.Size(355, 251);
            this.lbLensTrayChangeText.TabIndex = 0;
            this.lbLensTrayChangeText.Text = "Tray Changing ...";
            this.lbLensTrayChangeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSideAngleValue
            // 
            this.lbSideAngleValue.ArrayCount = 6;
            this.lbSideAngleValue.ColorBackground = System.Drawing.Color.Empty;
            this.lbSideAngleValue.ColorDark = System.Drawing.Color.Empty;
            this.lbSideAngleValue.ColorLight = System.Drawing.Color.Lime;
            this.lbSideAngleValue.DecimalShow = true;
            this.lbSideAngleValue.ElementPadding = new System.Windows.Forms.Padding(4);
            this.lbSideAngleValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSideAngleValue.Location = new System.Drawing.Point(668, 505);
            this.lbSideAngleValue.Name = "lbSideAngleValue";
            this.lbSideAngleValue.Size = new System.Drawing.Size(103, 24);
            this.lbSideAngleValue.TabIndex = 1920;
            this.lbSideAngleValue.Text = "-0.1234";
            // 
            // lbIndexNum
            // 
            this.lbIndexNum.BackColor = System.Drawing.Color.LemonChiffon;
            this.lbIndexNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbIndexNum.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbIndexNum.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbIndexNum.ImageIndex = 7;
            this.lbIndexNum.ImageList = this.ImgLstState;
            this.lbIndexNum.Location = new System.Drawing.Point(517, 382);
            this.lbIndexNum.Name = "lbIndexNum";
            this.lbIndexNum.Size = new System.Drawing.Size(47, 39);
            this.lbIndexNum.TabIndex = 1919;
            this.lbIndexNum.Text = "12";
            this.lbIndexNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct2Fail
            // 
            this.lbAct2Fail.BackColor = System.Drawing.Color.Red;
            this.lbAct2Fail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct2Fail.Location = new System.Drawing.Point(671, 633);
            this.lbAct2Fail.Name = "lbAct2Fail";
            this.lbAct2Fail.Size = new System.Drawing.Size(16, 10);
            this.lbAct2Fail.TabIndex = 1918;
            this.lbAct2Fail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct2Pass
            // 
            this.lbAct2Pass.BackColor = System.Drawing.Color.Lime;
            this.lbAct2Pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct2Pass.Location = new System.Drawing.Point(633, 633);
            this.lbAct2Pass.Name = "lbAct2Pass";
            this.lbAct2Pass.Size = new System.Drawing.Size(16, 10);
            this.lbAct2Pass.TabIndex = 1917;
            this.lbAct2Pass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct2Ready
            // 
            this.lbAct2Ready.BackColor = System.Drawing.Color.Lime;
            this.lbAct2Ready.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct2Ready.Location = new System.Drawing.Point(595, 633);
            this.lbAct2Ready.Name = "lbAct2Ready";
            this.lbAct2Ready.Size = new System.Drawing.Size(16, 10);
            this.lbAct2Ready.TabIndex = 1916;
            this.lbAct2Ready.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDisplaceValue
            // 
            this.lbDisplaceValue.ArrayCount = 6;
            this.lbDisplaceValue.ColorBackground = System.Drawing.Color.Empty;
            this.lbDisplaceValue.ColorDark = System.Drawing.Color.Empty;
            this.lbDisplaceValue.ColorLight = System.Drawing.Color.Lime;
            this.lbDisplaceValue.DecimalShow = true;
            this.lbDisplaceValue.ElementPadding = new System.Windows.Forms.Padding(4);
            this.lbDisplaceValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisplaceValue.Location = new System.Drawing.Point(451, 295);
            this.lbDisplaceValue.Name = "lbDisplaceValue";
            this.lbDisplaceValue.Size = new System.Drawing.Size(103, 24);
            this.lbDisplaceValue.TabIndex = 1869;
            this.lbDisplaceValue.Text = "-0.1234";
            // 
            // lbAct2Mode
            // 
            this.lbAct2Mode.BackColor = System.Drawing.Color.White;
            this.lbAct2Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct2Mode.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbAct2Mode.Location = new System.Drawing.Point(587, 620);
            this.lbAct2Mode.Name = "lbAct2Mode";
            this.lbAct2Mode.Size = new System.Drawing.Size(112, 26);
            this.lbAct2Mode.TabIndex = 1915;
            this.lbAct2Mode.Text = "Actuator";
            this.lbAct2Mode.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbAct1Fail
            // 
            this.lbAct1Fail.BackColor = System.Drawing.Color.Red;
            this.lbAct1Fail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct1Fail.Location = new System.Drawing.Point(824, 403);
            this.lbAct1Fail.Name = "lbAct1Fail";
            this.lbAct1Fail.Size = new System.Drawing.Size(16, 10);
            this.lbAct1Fail.TabIndex = 1914;
            this.lbAct1Fail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct1Pass
            // 
            this.lbAct1Pass.BackColor = System.Drawing.Color.Lime;
            this.lbAct1Pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct1Pass.Location = new System.Drawing.Point(786, 403);
            this.lbAct1Pass.Name = "lbAct1Pass";
            this.lbAct1Pass.Size = new System.Drawing.Size(16, 10);
            this.lbAct1Pass.TabIndex = 1913;
            this.lbAct1Pass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct1Ready
            // 
            this.lbAct1Ready.BackColor = System.Drawing.Color.Lime;
            this.lbAct1Ready.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct1Ready.Location = new System.Drawing.Point(748, 403);
            this.lbAct1Ready.Name = "lbAct1Ready";
            this.lbAct1Ready.Size = new System.Drawing.Size(16, 10);
            this.lbAct1Ready.TabIndex = 1912;
            this.lbAct1Ready.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAct1Mode
            // 
            this.lbAct1Mode.BackColor = System.Drawing.Color.White;
            this.lbAct1Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbAct1Mode.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbAct1Mode.Location = new System.Drawing.Point(740, 390);
            this.lbAct1Mode.Name = "lbAct1Mode";
            this.lbAct1Mode.Size = new System.Drawing.Size(112, 26);
            this.lbAct1Mode.TabIndex = 1911;
            this.lbAct1Mode.Text = "Actuator";
            this.lbAct1Mode.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbInnerLight
            // 
            this.lbInnerLight.BackColor = System.Drawing.Color.Lime;
            this.lbInnerLight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbInnerLight.Location = new System.Drawing.Point(1032, 725);
            this.lbInnerLight.Name = "lbInnerLight";
            this.lbInnerLight.Size = new System.Drawing.Size(112, 12);
            this.lbInnerLight.TabIndex = 1910;
            this.lbInnerLight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInnerLight
            // 
            this.btnInnerLight.BackColor = System.Drawing.Color.Silver;
            this.btnInnerLight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInnerLight.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnInnerLight.GlowColor = System.Drawing.Color.Transparent;
            this.btnInnerLight.InnerBorderColor = System.Drawing.Color.White;
            this.btnInnerLight.Location = new System.Drawing.Point(1030, 738);
            this.btnInnerLight.Name = "btnInnerLight";
            this.btnInnerLight.OuterBorderColor = System.Drawing.Color.Black;
            this.btnInnerLight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnInnerLight.ShineColor = System.Drawing.Color.Silver;
            this.btnInnerLight.Size = new System.Drawing.Size(116, 58);
            this.btnInnerLight.TabIndex = 1909;
            this.btnInnerLight.TabStop = false;
            this.btnInnerLight.Tag = "0";
            this.btnInnerLight.Text = "INNER LIGHT";
            this.btnInnerLight.Click += new System.EventHandler(this.btnInnerLight_Click);
            // 
            // btnUVLamp2
            // 
            this.btnUVLamp2.BackColor = System.Drawing.Color.Silver;
            this.btnUVLamp2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUVLamp2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnUVLamp2.GlowColor = System.Drawing.Color.Transparent;
            this.btnUVLamp2.InnerBorderColor = System.Drawing.Color.White;
            this.btnUVLamp2.Location = new System.Drawing.Point(1031, 464);
            this.btnUVLamp2.Name = "btnUVLamp2";
            this.btnUVLamp2.OuterBorderColor = System.Drawing.Color.Black;
            this.btnUVLamp2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUVLamp2.ShineColor = System.Drawing.Color.Silver;
            this.btnUVLamp2.Size = new System.Drawing.Size(116, 58);
            this.btnUVLamp2.TabIndex = 1908;
            this.btnUVLamp2.TabStop = false;
            this.btnUVLamp2.Tag = "0";
            this.btnUVLamp2.Text = "UV LAMP 2 ON";
            this.btnUVLamp2.Click += new System.EventHandler(this.btnUVLamp2_Click);
            // 
            // btnUvLamp
            // 
            this.btnUvLamp.BackColor = System.Drawing.Color.Silver;
            this.btnUvLamp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUvLamp.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnUvLamp.GlowColor = System.Drawing.Color.Transparent;
            this.btnUvLamp.InnerBorderColor = System.Drawing.Color.White;
            this.btnUvLamp.Location = new System.Drawing.Point(1031, 403);
            this.btnUvLamp.Name = "btnUvLamp";
            this.btnUvLamp.OuterBorderColor = System.Drawing.Color.Black;
            this.btnUvLamp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUvLamp.ShineColor = System.Drawing.Color.Silver;
            this.btnUvLamp.Size = new System.Drawing.Size(116, 58);
            this.btnUvLamp.TabIndex = 1907;
            this.btnUvLamp.TabStop = false;
            this.btnUvLamp.Tag = "0";
            this.btnUvLamp.Text = "UV LAMP 1 ON";
            this.btnUvLamp.Click += new System.EventHandler(this.btnUvLamp_Click);
            // 
            // lbDoor8
            // 
            this.lbDoor8.BackColor = System.Drawing.Color.Orange;
            this.lbDoor8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor8.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDoor8.Location = new System.Drawing.Point(533, 24);
            this.lbDoor8.Name = "lbDoor8";
            this.lbDoor8.Size = new System.Drawing.Size(459, 13);
            this.lbDoor8.TabIndex = 1905;
            this.lbDoor8.Text = "DOOR 8";
            this.lbDoor8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoor7
            // 
            this.lbDoor7.BackColor = System.Drawing.Color.Orange;
            this.lbDoor7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor7.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDoor7.Location = new System.Drawing.Point(63, 24);
            this.lbDoor7.Name = "lbDoor7";
            this.lbDoor7.Size = new System.Drawing.Size(459, 13);
            this.lbDoor7.TabIndex = 1904;
            this.lbDoor7.Text = "DOOR 7";
            this.lbDoor7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoor3
            // 
            this.lbDoor3.BackColor = System.Drawing.Color.Orange;
            this.lbDoor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor3.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDoor3.Location = new System.Drawing.Point(63, 38);
            this.lbDoor3.Name = "lbDoor3";
            this.lbDoor3.Size = new System.Drawing.Size(13, 369);
            this.lbDoor3.TabIndex = 1903;
            this.lbDoor3.Text = "DOOR 3";
            this.lbDoor3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoor4
            // 
            this.lbDoor4.BackColor = System.Drawing.Color.Orange;
            this.lbDoor4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor4.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDoor4.Location = new System.Drawing.Point(63, 415);
            this.lbDoor4.Name = "lbDoor4";
            this.lbDoor4.Size = new System.Drawing.Size(13, 369);
            this.lbDoor4.TabIndex = 1902;
            this.lbDoor4.Text = "DOOR 4";
            this.lbDoor4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoor6
            // 
            this.lbDoor6.BackColor = System.Drawing.Color.Orange;
            this.lbDoor6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor6.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDoor6.Location = new System.Drawing.Point(979, 38);
            this.lbDoor6.Name = "lbDoor6";
            this.lbDoor6.Size = new System.Drawing.Size(13, 369);
            this.lbDoor6.TabIndex = 1901;
            this.lbDoor6.Text = "DOOR 6";
            this.lbDoor6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoor5
            // 
            this.lbDoor5.BackColor = System.Drawing.Color.Orange;
            this.lbDoor5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor5.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDoor5.Location = new System.Drawing.Point(979, 415);
            this.lbDoor5.Name = "lbDoor5";
            this.lbDoor5.Size = new System.Drawing.Size(13, 369);
            this.lbDoor5.TabIndex = 1900;
            this.lbDoor5.Text = "DOOR 5";
            this.lbDoor5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoor2
            // 
            this.lbDoor2.BackColor = System.Drawing.Color.Orange;
            this.lbDoor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDoor2.Location = new System.Drawing.Point(533, 784);
            this.lbDoor2.Name = "lbDoor2";
            this.lbDoor2.Size = new System.Drawing.Size(459, 13);
            this.lbDoor2.TabIndex = 1899;
            this.lbDoor2.Text = "DOOR 2";
            this.lbDoor2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDoor1
            // 
            this.lbDoor1.BackColor = System.Drawing.Color.Orange;
            this.lbDoor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDoor1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDoor1.Location = new System.Drawing.Point(63, 784);
            this.lbDoor1.Name = "lbDoor1";
            this.lbDoor1.Size = new System.Drawing.Size(459, 13);
            this.lbDoor1.TabIndex = 1898;
            this.lbDoor1.Text = "DOOR 1";
            this.lbDoor1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVCMLoadSensor
            // 
            this.lbVCMLoadSensor.BackColor = System.Drawing.Color.Gray;
            this.lbVCMLoadSensor.Location = new System.Drawing.Point(469, 513);
            this.lbVCMLoadSensor.Name = "lbVCMLoadSensor";
            this.lbVCMLoadSensor.Size = new System.Drawing.Size(13, 8);
            this.lbVCMLoadSensor.TabIndex = 1897;
            // 
            // lbUnloadSensor
            // 
            this.lbUnloadSensor.BackColor = System.Drawing.Color.Gray;
            this.lbUnloadSensor.Location = new System.Drawing.Point(600, 513);
            this.lbUnloadSensor.Name = "lbUnloadSensor";
            this.lbUnloadSensor.Size = new System.Drawing.Size(13, 8);
            this.lbUnloadSensor.TabIndex = 1896;
            // 
            // lbCure2Sensor
            // 
            this.lbCure2Sensor.BackColor = System.Drawing.Color.Gray;
            this.lbCure2Sensor.Location = new System.Drawing.Point(649, 467);
            this.lbCure2Sensor.Name = "lbCure2Sensor";
            this.lbCure2Sensor.Size = new System.Drawing.Size(13, 8);
            this.lbCure2Sensor.TabIndex = 1895;
            // 
            // lbCure1Sensor
            // 
            this.lbCure1Sensor.BackColor = System.Drawing.Color.Gray;
            this.lbCure1Sensor.Location = new System.Drawing.Point(668, 399);
            this.lbCure1Sensor.Name = "lbCure1Sensor";
            this.lbCure1Sensor.Size = new System.Drawing.Size(13, 8);
            this.lbCure1Sensor.TabIndex = 1894;
            // 
            // lbLensHeightSensor
            // 
            this.lbLensHeightSensor.BackColor = System.Drawing.Color.Gray;
            this.lbLensHeightSensor.Location = new System.Drawing.Point(473, 281);
            this.lbLensHeightSensor.Name = "lbLensHeightSensor";
            this.lbLensHeightSensor.Size = new System.Drawing.Size(13, 8);
            this.lbLensHeightSensor.TabIndex = 1893;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(341, 343);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 14);
            this.label6.TabIndex = 1892;
            this.label6.Text = "HEAD";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(647, 607);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 14);
            this.label5.TabIndex = 1891;
            this.label5.Text = "HEAD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(417, 612);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 14);
            this.label4.TabIndex = 1890;
            this.label4.Text = "HEAD";
            // 
            // lbUnloadTrayExist
            // 
            this.lbUnloadTrayExist.BackColor = System.Drawing.Color.Lime;
            this.lbUnloadTrayExist.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloadTrayExist.Location = new System.Drawing.Point(711, 536);
            this.lbUnloadTrayExist.Name = "lbUnloadTrayExist";
            this.lbUnloadTrayExist.Size = new System.Drawing.Size(66, 61);
            this.lbUnloadTrayExist.TabIndex = 1889;
            this.lbUnloadTrayExist.Text = "SLOT 1";
            this.lbUnloadTrayExist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbUnloadTrayExist.Click += new System.EventHandler(this.lbUnloadTrayExist_Click);
            // 
            // lbVCMTrayExist
            // 
            this.lbVCMTrayExist.BackColor = System.Drawing.Color.Lime;
            this.lbVCMTrayExist.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMTrayExist.Location = new System.Drawing.Point(326, 536);
            this.lbVCMTrayExist.Name = "lbVCMTrayExist";
            this.lbVCMTrayExist.Size = new System.Drawing.Size(66, 61);
            this.lbVCMTrayExist.TabIndex = 1888;
            this.lbVCMTrayExist.Text = "SLOT 1";
            this.lbVCMTrayExist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbVCMTrayExist.Click += new System.EventHandler(this.lbVCMTrayExist_Click);
            // 
            // lbLensTrayExist
            // 
            this.lbLensTrayExist.BackColor = System.Drawing.Color.Lime;
            this.lbLensTrayExist.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensTrayExist.Location = new System.Drawing.Point(273, 240);
            this.lbLensTrayExist.Name = "lbLensTrayExist";
            this.lbLensTrayExist.Size = new System.Drawing.Size(66, 55);
            this.lbLensTrayExist.TabIndex = 1887;
            this.lbLensTrayExist.Text = "SLOT 1";
            this.lbLensTrayExist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLensTrayExist.Click += new System.EventHandler(this.lbLensTrayExist_Click);
            // 
            // lbPlateAngleExist
            // 
            this.lbPlateAngleExist.BackColor = System.Drawing.Color.Transparent;
            this.lbPlateAngleExist.ImageIndex = 5;
            this.lbPlateAngleExist.ImageList = this.ImgLstState;
            this.lbPlateAngleExist.Location = new System.Drawing.Point(652, 530);
            this.lbPlateAngleExist.Name = "lbPlateAngleExist";
            this.lbPlateAngleExist.Size = new System.Drawing.Size(26, 34);
            this.lbPlateAngleExist.TabIndex = 1886;
            this.lbPlateAngleExist.Click += new System.EventHandler(this.lbPlateAngleExist_Click);
            // 
            // pnUnloadMagazine
            // 
            this.pnUnloadMagazine.BackColor = System.Drawing.Color.Gray;
            this.pnUnloadMagazine.Location = new System.Drawing.Point(825, 510);
            this.pnUnloadMagazine.Name = "pnUnloadMagazine";
            this.pnUnloadMagazine.Size = new System.Drawing.Size(150, 250);
            this.pnUnloadMagazine.TabIndex = 1884;
            // 
            // pnLensMagazine
            // 
            this.pnLensMagazine.BackColor = System.Drawing.Color.Gray;
            this.pnLensMagazine.Location = new System.Drawing.Point(102, 130);
            this.pnLensMagazine.Name = "pnLensMagazine";
            this.pnLensMagazine.Size = new System.Drawing.Size(150, 250);
            this.pnLensMagazine.TabIndex = 1883;
            // 
            // pnVCMMagazine
            // 
            this.pnVCMMagazine.BackColor = System.Drawing.Color.Gray;
            this.pnVCMMagazine.Location = new System.Drawing.Point(102, 510);
            this.pnVCMMagazine.Name = "pnVCMMagazine";
            this.pnVCMMagazine.Size = new System.Drawing.Size(150, 250);
            this.pnVCMMagazine.TabIndex = 1882;
            // 
            // lbLensHeadStatus
            // 
            this.lbLensHeadStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbLensHeadStatus.ImageIndex = 5;
            this.lbLensHeadStatus.ImageList = this.ImgLstState;
            this.lbLensHeadStatus.Location = new System.Drawing.Point(346, 311);
            this.lbLensHeadStatus.Name = "lbLensHeadStatus";
            this.lbLensHeadStatus.Size = new System.Drawing.Size(26, 26);
            this.lbLensHeadStatus.TabIndex = 1460;
            this.lbLensHeadStatus.Click += new System.EventHandler(this.lbLensHeadStatus_Click);
            // 
            // lbUnloadHeadStatus
            // 
            this.lbUnloadHeadStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbUnloadHeadStatus.ImageIndex = 5;
            this.lbUnloadHeadStatus.ImageList = this.ImgLstState;
            this.lbUnloadHeadStatus.Location = new System.Drawing.Point(652, 578);
            this.lbUnloadHeadStatus.Name = "lbUnloadHeadStatus";
            this.lbUnloadHeadStatus.Size = new System.Drawing.Size(26, 26);
            this.lbUnloadHeadStatus.TabIndex = 1459;
            this.lbUnloadHeadStatus.Click += new System.EventHandler(this.lbUnloadHeadStatus_Click);
            // 
            // lbVCMHeadStatus
            // 
            this.lbVCMHeadStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbVCMHeadStatus.ImageIndex = 5;
            this.lbVCMHeadStatus.ImageList = this.ImgLstState;
            this.lbVCMHeadStatus.Location = new System.Drawing.Point(421, 576);
            this.lbVCMHeadStatus.Name = "lbVCMHeadStatus";
            this.lbVCMHeadStatus.Size = new System.Drawing.Size(26, 34);
            this.lbVCMHeadStatus.TabIndex = 1458;
            this.lbVCMHeadStatus.Click += new System.EventHandler(this.lbVCMHeadStatus_Click);
            // 
            // lbPlateAngleStatus
            // 
            this.lbPlateAngleStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbPlateAngleStatus.ImageIndex = 5;
            this.lbPlateAngleStatus.ImageList = this.ImgLstState;
            this.lbPlateAngleStatus.Location = new System.Drawing.Point(605, 523);
            this.lbPlateAngleStatus.Name = "lbPlateAngleStatus";
            this.lbPlateAngleStatus.Size = new System.Drawing.Size(26, 26);
            this.lbPlateAngleStatus.TabIndex = 1457;
            this.lbPlateAngleStatus.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbCure2Status
            // 
            this.lbCure2Status.BackColor = System.Drawing.Color.Transparent;
            this.lbCure2Status.ImageIndex = 5;
            this.lbCure2Status.ImageList = this.ImgLstState;
            this.lbCure2Status.Location = new System.Drawing.Point(664, 470);
            this.lbCure2Status.Name = "lbCure2Status";
            this.lbCure2Status.Size = new System.Drawing.Size(26, 26);
            this.lbCure2Status.TabIndex = 1456;
            this.lbCure2Status.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbCure1Status
            // 
            this.lbCure1Status.BackColor = System.Drawing.Color.Transparent;
            this.lbCure1Status.ImageIndex = 6;
            this.lbCure1Status.ImageList = this.ImgLstState;
            this.lbCure1Status.Location = new System.Drawing.Point(687, 390);
            this.lbCure1Status.Name = "lbCure1Status";
            this.lbCure1Status.Size = new System.Drawing.Size(26, 26);
            this.lbCure1Status.TabIndex = 1455;
            this.lbCure1Status.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbVisionInsStatus
            // 
            this.lbVisionInsStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbVisionInsStatus.ImageIndex = 5;
            this.lbVisionInsStatus.ImageList = this.ImgLstState;
            this.lbVisionInsStatus.Location = new System.Drawing.Point(662, 312);
            this.lbVisionInsStatus.Name = "lbVisionInsStatus";
            this.lbVisionInsStatus.Size = new System.Drawing.Size(26, 26);
            this.lbVisionInsStatus.TabIndex = 1454;
            this.lbVisionInsStatus.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbBonder2Status
            // 
            this.lbBonder2Status.BackColor = System.Drawing.Color.Transparent;
            this.lbBonder2Status.ImageIndex = 5;
            this.lbBonder2Status.ImageList = this.ImgLstState;
            this.lbBonder2Status.Location = new System.Drawing.Point(607, 261);
            this.lbBonder2Status.Name = "lbBonder2Status";
            this.lbBonder2Status.Size = new System.Drawing.Size(26, 26);
            this.lbBonder2Status.TabIndex = 1453;
            this.lbBonder2Status.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbBonder1Status
            // 
            this.lbBonder1Status.BackColor = System.Drawing.Color.Transparent;
            this.lbBonder1Status.ImageIndex = 5;
            this.lbBonder1Status.ImageList = this.ImgLstState;
            this.lbBonder1Status.Location = new System.Drawing.Point(528, 238);
            this.lbBonder1Status.Name = "lbBonder1Status";
            this.lbBonder1Status.Size = new System.Drawing.Size(26, 26);
            this.lbBonder1Status.TabIndex = 1452;
            this.lbBonder1Status.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbLensHeightStatus
            // 
            this.lbLensHeightStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbLensHeightStatus.ImageIndex = 5;
            this.lbLensHeightStatus.ImageList = this.ImgLstState;
            this.lbLensHeightStatus.Location = new System.Drawing.Point(452, 253);
            this.lbLensHeightStatus.Name = "lbLensHeightStatus";
            this.lbLensHeightStatus.Size = new System.Drawing.Size(26, 26);
            this.lbLensHeightStatus.TabIndex = 1451;
            this.lbLensHeightStatus.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbLensInsertStatus
            // 
            this.lbLensInsertStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbLensInsertStatus.ImageIndex = 5;
            this.lbLensInsertStatus.ImageList = this.ImgLstState;
            this.lbLensInsertStatus.Location = new System.Drawing.Point(395, 311);
            this.lbLensInsertStatus.Name = "lbLensInsertStatus";
            this.lbLensInsertStatus.Size = new System.Drawing.Size(26, 26);
            this.lbLensInsertStatus.TabIndex = 1450;
            this.lbLensInsertStatus.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbAct3Status
            // 
            this.lbAct3Status.BackColor = System.Drawing.Color.Transparent;
            this.lbAct3Status.ImageIndex = 5;
            this.lbAct3Status.ImageList = this.ImgLstState;
            this.lbAct3Status.Location = new System.Drawing.Point(375, 390);
            this.lbAct3Status.Name = "lbAct3Status";
            this.lbAct3Status.Size = new System.Drawing.Size(26, 26);
            this.lbAct3Status.TabIndex = 1449;
            this.lbAct3Status.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbJigPlateAngleStatus
            // 
            this.lbJigPlateAngleStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbJigPlateAngleStatus.ImageIndex = 5;
            this.lbJigPlateAngleStatus.ImageList = this.ImgLstState;
            this.lbJigPlateAngleStatus.Location = new System.Drawing.Point(397, 466);
            this.lbJigPlateAngleStatus.Name = "lbJigPlateAngleStatus";
            this.lbJigPlateAngleStatus.Size = new System.Drawing.Size(26, 26);
            this.lbJigPlateAngleStatus.TabIndex = 1448;
            this.lbJigPlateAngleStatus.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbVCMLoadStatus
            // 
            this.lbVCMLoadStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbVCMLoadStatus.ImageIndex = 5;
            this.lbVCMLoadStatus.ImageList = this.ImgLstState;
            this.lbVCMLoadStatus.Location = new System.Drawing.Point(449, 522);
            this.lbVCMLoadStatus.Name = "lbVCMLoadStatus";
            this.lbVCMLoadStatus.Size = new System.Drawing.Size(26, 26);
            this.lbVCMLoadStatus.TabIndex = 1447;
            this.lbVCMLoadStatus.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // lbCleanJigStatus
            // 
            this.lbCleanJigStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbCleanJigStatus.ImageIndex = 5;
            this.lbCleanJigStatus.ImageList = this.ImgLstState;
            this.lbCleanJigStatus.Location = new System.Drawing.Point(528, 544);
            this.lbCleanJigStatus.Name = "lbCleanJigStatus";
            this.lbCleanJigStatus.Size = new System.Drawing.Size(26, 26);
            this.lbCleanJigStatus.TabIndex = 1446;
            this.lbCleanJigStatus.Click += new System.EventHandler(this.lbIndexStatus_Click);
            // 
            // gbStatus
            // 
            this.gbStatus.Controls.Add(this.lbNone);
            this.gbStatus.Controls.Add(this.label35);
            this.gbStatus.Controls.Add(this.label36);
            this.gbStatus.Controls.Add(this.label37);
            this.gbStatus.Controls.Add(this.label38);
            this.gbStatus.Controls.Add(this.lbExist);
            this.gbStatus.Controls.Add(this.lbError);
            this.gbStatus.Controls.Add(this.lbReady);
            this.gbStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gbStatus.Location = new System.Drawing.Point(1030, 29);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new System.Drawing.Size(117, 160);
            this.gbStatus.TabIndex = 1445;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "제품 상태";
            // 
            // lbNone
            // 
            this.lbNone.AutoSize = true;
            this.lbNone.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbNone.Location = new System.Drawing.Point(49, 34);
            this.lbNone.Name = "lbNone";
            this.lbNone.Size = new System.Drawing.Size(59, 15);
            this.lbNone.TabIndex = 48;
            this.lbNone.Text = "제품 없음";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.ImageIndex = 4;
            this.label35.ImageList = this.ImgLstState;
            this.label35.Location = new System.Drawing.Point(14, 28);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(26, 26);
            this.label35.TabIndex = 12;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.ImageIndex = 7;
            this.label36.ImageList = this.ImgLstState;
            this.label36.Location = new System.Drawing.Point(14, 124);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(26, 26);
            this.label36.TabIndex = 13;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.ImageIndex = 6;
            this.label37.ImageList = this.ImgLstState;
            this.label37.Location = new System.Drawing.Point(14, 91);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(26, 26);
            this.label37.TabIndex = 14;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.ImageIndex = 5;
            this.label38.ImageList = this.ImgLstState;
            this.label38.Location = new System.Drawing.Point(14, 58);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(26, 26);
            this.label38.TabIndex = 15;
            // 
            // lbExist
            // 
            this.lbExist.AutoSize = true;
            this.lbExist.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbExist.Location = new System.Drawing.Point(49, 130);
            this.lbExist.Name = "lbExist";
            this.lbExist.Size = new System.Drawing.Size(59, 15);
            this.lbExist.TabIndex = 49;
            this.lbExist.Text = "공정 완료";
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbError.Location = new System.Drawing.Point(49, 97);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(59, 15);
            this.lbError.TabIndex = 50;
            this.lbError.Text = "공정 에러";
            // 
            // lbReady
            // 
            this.lbReady.AutoSize = true;
            this.lbReady.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbReady.Location = new System.Drawing.Point(49, 64);
            this.lbReady.Name = "lbReady";
            this.lbReady.Size = new System.Drawing.Size(59, 15);
            this.lbReady.TabIndex = 51;
            this.lbReady.Text = "공정 준비";
            // 
            // picMainIndex
            // 
            this.picMainIndex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picMainIndex.BackgroundImage")));
            this.picMainIndex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picMainIndex.Location = new System.Drawing.Point(44, 23);
            this.picMainIndex.Name = "picMainIndex";
            this.picMainIndex.Size = new System.Drawing.Size(981, 778);
            this.picMainIndex.TabIndex = 1444;
            this.picMainIndex.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.panel8);
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1146, 777);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chart";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lbActuating3Total);
            this.panel8.Controls.Add(this.lbSideHeightTotal);
            this.panel8.Controls.Add(this.lbActuating2Total);
            this.panel8.Controls.Add(this.lbSideAngleTotal);
            this.panel8.Controls.Add(this.lbActuating1Total);
            this.panel8.Controls.Add(this.lbVisionTotal);
            this.panel8.Controls.Add(this.lbLensHeightTotal);
            this.panel8.Controls.Add(this.lbOKTotal);
            this.panel8.Controls.Add(this.btnYieldReset);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.Yield_Chart);
            this.panel8.Location = new System.Drawing.Point(9, 354);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1134, 420);
            this.panel8.TabIndex = 1835;
            // 
            // lbActuating3Total
            // 
            this.lbActuating3Total.BackColor = System.Drawing.Color.White;
            this.lbActuating3Total.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbActuating3Total.Location = new System.Drawing.Point(1014, 122);
            this.lbActuating3Total.Name = "lbActuating3Total";
            this.lbActuating3Total.Size = new System.Drawing.Size(115, 16);
            this.lbActuating3Total.TabIndex = 1844;
            this.lbActuating3Total.Text = "0";
            this.lbActuating3Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSideHeightTotal
            // 
            this.lbSideHeightTotal.BackColor = System.Drawing.Color.White;
            this.lbSideHeightTotal.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbSideHeightTotal.Location = new System.Drawing.Point(1014, 138);
            this.lbSideHeightTotal.Name = "lbSideHeightTotal";
            this.lbSideHeightTotal.Size = new System.Drawing.Size(115, 16);
            this.lbSideHeightTotal.TabIndex = 1843;
            this.lbSideHeightTotal.Text = "0";
            this.lbSideHeightTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbActuating2Total
            // 
            this.lbActuating2Total.BackColor = System.Drawing.Color.White;
            this.lbActuating2Total.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbActuating2Total.Location = new System.Drawing.Point(1014, 106);
            this.lbActuating2Total.Name = "lbActuating2Total";
            this.lbActuating2Total.Size = new System.Drawing.Size(115, 16);
            this.lbActuating2Total.TabIndex = 1842;
            this.lbActuating2Total.Text = "0";
            this.lbActuating2Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSideAngleTotal
            // 
            this.lbSideAngleTotal.BackColor = System.Drawing.Color.White;
            this.lbSideAngleTotal.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbSideAngleTotal.Location = new System.Drawing.Point(1014, 90);
            this.lbSideAngleTotal.Name = "lbSideAngleTotal";
            this.lbSideAngleTotal.Size = new System.Drawing.Size(115, 16);
            this.lbSideAngleTotal.TabIndex = 1841;
            this.lbSideAngleTotal.Text = "0";
            this.lbSideAngleTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbActuating1Total
            // 
            this.lbActuating1Total.BackColor = System.Drawing.Color.White;
            this.lbActuating1Total.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbActuating1Total.Location = new System.Drawing.Point(1014, 74);
            this.lbActuating1Total.Name = "lbActuating1Total";
            this.lbActuating1Total.Size = new System.Drawing.Size(115, 16);
            this.lbActuating1Total.TabIndex = 1840;
            this.lbActuating1Total.Text = "0";
            this.lbActuating1Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVisionTotal
            // 
            this.lbVisionTotal.BackColor = System.Drawing.Color.White;
            this.lbVisionTotal.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbVisionTotal.Location = new System.Drawing.Point(1014, 58);
            this.lbVisionTotal.Name = "lbVisionTotal";
            this.lbVisionTotal.Size = new System.Drawing.Size(115, 16);
            this.lbVisionTotal.TabIndex = 1839;
            this.lbVisionTotal.Text = "0";
            this.lbVisionTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensHeightTotal
            // 
            this.lbLensHeightTotal.BackColor = System.Drawing.Color.White;
            this.lbLensHeightTotal.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbLensHeightTotal.Location = new System.Drawing.Point(1014, 43);
            this.lbLensHeightTotal.Name = "lbLensHeightTotal";
            this.lbLensHeightTotal.Size = new System.Drawing.Size(115, 16);
            this.lbLensHeightTotal.TabIndex = 1838;
            this.lbLensHeightTotal.Text = "0";
            this.lbLensHeightTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbOKTotal
            // 
            this.lbOKTotal.BackColor = System.Drawing.Color.White;
            this.lbOKTotal.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbOKTotal.Location = new System.Drawing.Point(1014, 28);
            this.lbOKTotal.Name = "lbOKTotal";
            this.lbOKTotal.Size = new System.Drawing.Size(115, 16);
            this.lbOKTotal.TabIndex = 1837;
            this.lbOKTotal.Text = "0";
            this.lbOKTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnYieldReset
            // 
            this.btnYieldReset.BackColor = System.Drawing.Color.Silver;
            this.btnYieldReset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYieldReset.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnYieldReset.GlowColor = System.Drawing.Color.Transparent;
            this.btnYieldReset.InnerBorderColor = System.Drawing.Color.White;
            this.btnYieldReset.Location = new System.Drawing.Point(999, 222);
            this.btnYieldReset.Name = "btnYieldReset";
            this.btnYieldReset.OuterBorderColor = System.Drawing.Color.Black;
            this.btnYieldReset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnYieldReset.ShineColor = System.Drawing.Color.Silver;
            this.btnYieldReset.Size = new System.Drawing.Size(120, 35);
            this.btnYieldReset.TabIndex = 1836;
            this.btnYieldReset.TabStop = false;
            this.btnYieldReset.Tag = "0";
            this.btnYieldReset.Text = "RESET";
            this.btnYieldReset.Click += new System.EventHandler(this.btnYieldReset_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1134, 23);
            this.label3.TabIndex = 1833;
            this.label3.Tag = "";
            this.label3.Text = "COUNT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Yield_Chart
            // 
            this.Yield_Chart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Yield_Chart.BorderlineWidth = 2;
            this.Yield_Chart.BorderSkin.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Yield_Chart.BorderSkin.PageColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            chartArea1.AxisX.Maximum = 13D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.ScaleBreakStyle.MaxNumberOfBreaks = 1;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.Yield_Chart.ChartAreas.Add(chartArea1);
            legend1.AutoFitMinFontSize = 8;
            legend1.IsEquallySpacedItems = true;
            legend1.LegendItemOrder = System.Windows.Forms.DataVisualization.Charting.LegendItemOrder.ReversedSeriesOrder;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend1.MaximumAutoSize = 100F;
            legend1.Name = "Legend1";
            legend1.Position.Auto = false;
            legend1.Position.Height = 35F;
            legend1.Position.Width = 11F;
            legend1.Position.X = 88F;
            legend1.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Tall;
            legend1.TextWrapThreshold = 50;
            legend1.TitleAlignment = System.Drawing.StringAlignment.Near;
            legend1.TitleSeparator = System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.DoubleLine;
            this.Yield_Chart.Legends.Add(legend1);
            this.Yield_Chart.Location = new System.Drawing.Point(3, 25);
            this.Yield_Chart.Name = "Yield_Chart";
            this.Yield_Chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.Yield_Chart.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Cyan,
        System.Drawing.Color.Orange,
        System.Drawing.Color.Gold,
        System.Drawing.Color.Purple,
        System.Drawing.Color.Red,
        System.Drawing.Color.Lime,
        System.Drawing.Color.Violet,
        System.Drawing.Color.Blue};
            this.Yield_Chart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.ChartArea = "ChartArea1";
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "OK";
            series2.ChartArea = "ChartArea1";
            series2.IsValueShownAsLabel = true;
            series2.Legend = "Legend1";
            series2.Name = "Lens Height";
            series3.ChartArea = "ChartArea1";
            series3.IsValueShownAsLabel = true;
            series3.Legend = "Legend1";
            series3.Name = "Vision Inspect";
            series4.ChartArea = "ChartArea1";
            series4.IsValueShownAsLabel = true;
            series4.Legend = "Legend1";
            series4.Name = "Actuating 1";
            series5.ChartArea = "ChartArea1";
            series5.IsValueShownAsLabel = true;
            series5.Legend = "Legend1";
            series5.Name = "Side Angle";
            series6.ChartArea = "ChartArea1";
            series6.IsValueShownAsLabel = true;
            series6.Legend = "Legend1";
            series6.Name = "Actuating 2";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Actuating 3";
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Side Height";
            this.Yield_Chart.Series.Add(series1);
            this.Yield_Chart.Series.Add(series2);
            this.Yield_Chart.Series.Add(series3);
            this.Yield_Chart.Series.Add(series4);
            this.Yield_Chart.Series.Add(series5);
            this.Yield_Chart.Series.Add(series6);
            this.Yield_Chart.Series.Add(series7);
            this.Yield_Chart.Series.Add(series8);
            this.Yield_Chart.Size = new System.Drawing.Size(1023, 395);
            this.Yield_Chart.TabIndex = 0;
            this.Yield_Chart.Text = "chart1";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel11);
            this.panel6.Controls.Add(this.panel10);
            this.panel6.Location = new System.Drawing.Point(6, 6);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1134, 346);
            this.panel6.TabIndex = 2;
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.lbSideAngleTrend);
            this.panel11.Controls.Add(this.lbSideAngleTrendAverage);
            this.panel11.Controls.Add(this.lbLensHeightSetProdcut);
            this.panel11.Controls.Add(this.lbLensHeightSetPorductNumber);
            this.panel11.Controls.Add(this.label7);
            this.panel11.Controls.Add(this.SideAngle_Chart);
            this.panel11.Location = new System.Drawing.Point(569, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(565, 344);
            this.panel11.TabIndex = 1837;
            // 
            // lbSideAngleTrend
            // 
            this.lbSideAngleTrend.BackColor = System.Drawing.Color.Gainsboro;
            this.lbSideAngleTrend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSideAngleTrend.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbSideAngleTrend.Location = new System.Drawing.Point(419, 122);
            this.lbSideAngleTrend.Name = "lbSideAngleTrend";
            this.lbSideAngleTrend.Size = new System.Drawing.Size(120, 35);
            this.lbSideAngleTrend.TabIndex = 1839;
            this.lbSideAngleTrend.Text = "Trend Average";
            this.lbSideAngleTrend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSideAngleTrendAverage
            // 
            this.lbSideAngleTrendAverage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSideAngleTrendAverage.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbSideAngleTrendAverage.Location = new System.Drawing.Point(419, 163);
            this.lbSideAngleTrendAverage.Name = "lbSideAngleTrendAverage";
            this.lbSideAngleTrendAverage.Size = new System.Drawing.Size(120, 35);
            this.lbSideAngleTrendAverage.TabIndex = 1838;
            this.lbSideAngleTrendAverage.Text = "0";
            this.lbSideAngleTrendAverage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLensHeightSetProdcut
            // 
            this.lbLensHeightSetProdcut.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensHeightSetProdcut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensHeightSetProdcut.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbLensHeightSetProdcut.Location = new System.Drawing.Point(419, 221);
            this.lbLensHeightSetProdcut.Name = "lbLensHeightSetProdcut";
            this.lbLensHeightSetProdcut.Size = new System.Drawing.Size(120, 35);
            this.lbLensHeightSetProdcut.TabIndex = 1835;
            this.lbLensHeightSetProdcut.Text = "Set Products Number";
            this.lbLensHeightSetProdcut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensHeightSetPorductNumber
            // 
            this.lbLensHeightSetPorductNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensHeightSetPorductNumber.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbLensHeightSetPorductNumber.Location = new System.Drawing.Point(419, 262);
            this.lbLensHeightSetPorductNumber.Name = "lbLensHeightSetPorductNumber";
            this.lbLensHeightSetPorductNumber.Size = new System.Drawing.Size(120, 35);
            this.lbLensHeightSetPorductNumber.TabIndex = 1834;
            this.lbLensHeightSetPorductNumber.Text = "100";
            this.lbLensHeightSetPorductNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbLensHeightSetPorductNumber.Click += new System.EventHandler(this.SetPorductNumber_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(563, 23);
            this.label7.TabIndex = 1833;
            this.label7.Tag = "";
            this.label7.Text = "Side Angle Value Trend";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SideAngle_Chart
            // 
            this.SideAngle_Chart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SideAngle_Chart.BorderlineWidth = 2;
            this.SideAngle_Chart.BorderSkin.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SideAngle_Chart.BorderSkin.PageColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.Name = "ChartArea1";
            this.SideAngle_Chart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.SideAngle_Chart.Legends.Add(legend2);
            this.SideAngle_Chart.Location = new System.Drawing.Point(5, 26);
            this.SideAngle_Chart.Name = "SideAngle_Chart";
            this.SideAngle_Chart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series9.Legend = "Legend1";
            series9.Name = "Side Angle Value";
            this.SideAngle_Chart.Series.Add(series9);
            this.SideAngle_Chart.Size = new System.Drawing.Size(554, 310);
            this.SideAngle_Chart.TabIndex = 0;
            this.SideAngle_Chart.Text = "chart1";
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.lbLensHeightTrend);
            this.panel10.Controls.Add(this.lbLensHeightTrendAverage);
            this.panel10.Controls.Add(this.label2);
            this.panel10.Controls.Add(this.LensHeight_Chart);
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(565, 344);
            this.panel10.TabIndex = 1836;
            // 
            // lbLensHeightTrend
            // 
            this.lbLensHeightTrend.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensHeightTrend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensHeightTrend.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbLensHeightTrend.Location = new System.Drawing.Point(421, 189);
            this.lbLensHeightTrend.Name = "lbLensHeightTrend";
            this.lbLensHeightTrend.Size = new System.Drawing.Size(120, 35);
            this.lbLensHeightTrend.TabIndex = 1837;
            this.lbLensHeightTrend.Text = "Trend Average";
            this.lbLensHeightTrend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensHeightTrendAverage
            // 
            this.lbLensHeightTrendAverage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensHeightTrendAverage.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbLensHeightTrendAverage.Location = new System.Drawing.Point(421, 229);
            this.lbLensHeightTrendAverage.Name = "lbLensHeightTrendAverage";
            this.lbLensHeightTrendAverage.Size = new System.Drawing.Size(120, 35);
            this.lbLensHeightTrendAverage.TabIndex = 1836;
            this.lbLensHeightTrendAverage.Text = "0";
            this.lbLensHeightTrendAverage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(563, 23);
            this.label2.TabIndex = 1833;
            this.label2.Tag = "";
            this.label2.Text = "LensHeight Value Trend";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LensHeight_Chart
            // 
            this.LensHeight_Chart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LensHeight_Chart.BorderlineWidth = 2;
            this.LensHeight_Chart.BorderSkin.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LensHeight_Chart.BorderSkin.PageColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisX.Maximum = 100D;
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.AxisY.Interval = 1D;
            chartArea3.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.RoyalBlue;
            chartArea3.AxisY.LabelStyle.Interval = 0D;
            chartArea3.AxisY.Maximum = 1D;
            chartArea3.AxisY.Minimum = -1D;
            chartArea3.AxisY.MinorGrid.Enabled = true;
            chartArea3.AxisY.MinorGrid.Interval = 0.1D;
            chartArea3.AxisY.MinorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisY.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisY.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea3.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisY.MinorTickMark.Enabled = true;
            chartArea3.AxisY.MinorTickMark.Interval = 0.2D;
            chartArea3.AxisY.MinorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisY.MinorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea3.AxisY.TitleForeColor = System.Drawing.Color.Gray;
            chartArea3.Name = "ChartArea1";
            this.LensHeight_Chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.LensHeight_Chart.Legends.Add(legend3);
            this.LensHeight_Chart.Location = new System.Drawing.Point(3, 26);
            this.LensHeight_Chart.Name = "LensHeight_Chart";
            this.LensHeight_Chart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series10.Color = System.Drawing.Color.Red;
            series10.Legend = "Legend1";
            series10.MarkerColor = System.Drawing.Color.Blue;
            series10.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series10.Name = "Lens Height Value";
            this.LensHeight_Chart.Series.Add(series10);
            this.LensHeight_Chart.Size = new System.Drawing.Size(554, 310);
            this.LensHeight_Chart.TabIndex = 0;
            this.LensHeight_Chart.Text = "chart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblZValue1);
            this.tabPage3.Controls.Add(this.lblZValue4);
            this.tabPage3.Controls.Add(this.lblZValue2);
            this.tabPage3.Controls.Add(this.lblZValue3);
            this.tabPage3.Controls.Add(this.pictureBox2);
            this.tabPage3.Controls.Add(this.blImgAngleValue_3);
            this.tabPage3.Controls.Add(this.blImgAngleValue_4);
            this.tabPage3.Controls.Add(this.blImgAngleValue_5);
            this.tabPage3.Controls.Add(this.blImgAngleValue_6);
            this.tabPage3.Controls.Add(this.blImgAngleValue_7);
            this.tabPage3.Controls.Add(this.blImgAngleValue_8);
            this.tabPage3.Controls.Add(this.blImgAngleValue_9);
            this.tabPage3.Controls.Add(this.blImgAngleValue_10);
            this.tabPage3.Controls.Add(this.blImgAngleValue_11);
            this.tabPage3.Controls.Add(this.blImgAngleValue_0);
            this.tabPage3.Controls.Add(this.blImgAngleValue_1);
            this.tabPage3.Controls.Add(this.blImgAngleValue_2);
            this.tabPage3.Controls.Add(this.btnAngle_All);
            this.tabPage3.Controls.Add(this.btnAngle_Single);
            this.tabPage3.Controls.Add(this.tpanel__Measuring);
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1146, 777);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Measuring";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblZValue1
            // 
            this.lblZValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblZValue1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblZValue1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZValue1.ImageIndex = 5;
            this.lblZValue1.Location = new System.Drawing.Point(201, 670);
            this.lblZValue1.Name = "lblZValue1";
            this.lblZValue1.Size = new System.Drawing.Size(72, 24);
            this.lblZValue1.TabIndex = 1721;
            this.lblZValue1.Text = "#1";
            this.lblZValue1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblZValue4
            // 
            this.lblZValue4.BackColor = System.Drawing.Color.Transparent;
            this.lblZValue4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblZValue4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZValue4.ImageIndex = 5;
            this.lblZValue4.Location = new System.Drawing.Point(106, 670);
            this.lblZValue4.Name = "lblZValue4";
            this.lblZValue4.Size = new System.Drawing.Size(72, 24);
            this.lblZValue4.TabIndex = 1721;
            this.lblZValue4.Text = "#4";
            this.lblZValue4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblZValue2
            // 
            this.lblZValue2.BackColor = System.Drawing.Color.Transparent;
            this.lblZValue2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblZValue2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZValue2.ImageIndex = 5;
            this.lblZValue2.Location = new System.Drawing.Point(201, 587);
            this.lblZValue2.Name = "lblZValue2";
            this.lblZValue2.Size = new System.Drawing.Size(72, 24);
            this.lblZValue2.TabIndex = 1721;
            this.lblZValue2.Text = "#2";
            this.lblZValue2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblZValue3
            // 
            this.lblZValue3.BackColor = System.Drawing.Color.Transparent;
            this.lblZValue3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblZValue3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZValue3.ImageIndex = 5;
            this.lblZValue3.Location = new System.Drawing.Point(106, 587);
            this.lblZValue3.Name = "lblZValue3";
            this.lblZValue3.Size = new System.Drawing.Size(72, 24);
            this.lblZValue3.TabIndex = 1721;
            this.lblZValue3.Text = "#3";
            this.lblZValue3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::XModule.Properties.Resources.jigplateimg;
            this.pictureBox2.Location = new System.Drawing.Point(8, 511);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(365, 255);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1732;
            this.pictureBox2.TabStop = false;
            // 
            // blImgAngleValue_3
            // 
            this.blImgAngleValue_3.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_3.ImageIndex = 5;
            this.blImgAngleValue_3.Location = new System.Drawing.Point(802, 496);
            this.blImgAngleValue_3.Name = "blImgAngleValue_3";
            this.blImgAngleValue_3.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_3.TabIndex = 1731;
            this.blImgAngleValue_3.Text = "0";
            this.blImgAngleValue_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_4
            // 
            this.blImgAngleValue_4.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_4.ImageIndex = 5;
            this.blImgAngleValue_4.Location = new System.Drawing.Point(863, 450);
            this.blImgAngleValue_4.Name = "blImgAngleValue_4";
            this.blImgAngleValue_4.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_4.TabIndex = 1730;
            this.blImgAngleValue_4.Text = "0";
            this.blImgAngleValue_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_5
            // 
            this.blImgAngleValue_5.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_5.ImageIndex = 6;
            this.blImgAngleValue_5.Location = new System.Drawing.Point(886, 370);
            this.blImgAngleValue_5.Name = "blImgAngleValue_5";
            this.blImgAngleValue_5.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_5.TabIndex = 1729;
            this.blImgAngleValue_5.Text = "0";
            this.blImgAngleValue_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_6
            // 
            this.blImgAngleValue_6.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_6.ImageIndex = 5;
            this.blImgAngleValue_6.Location = new System.Drawing.Point(861, 292);
            this.blImgAngleValue_6.Name = "blImgAngleValue_6";
            this.blImgAngleValue_6.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_6.TabIndex = 1728;
            this.blImgAngleValue_6.Text = "0";
            this.blImgAngleValue_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_7
            // 
            this.blImgAngleValue_7.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_7.ImageIndex = 5;
            this.blImgAngleValue_7.Location = new System.Drawing.Point(802, 252);
            this.blImgAngleValue_7.Name = "blImgAngleValue_7";
            this.blImgAngleValue_7.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_7.TabIndex = 1727;
            this.blImgAngleValue_7.Text = "0";
            this.blImgAngleValue_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_8
            // 
            this.blImgAngleValue_8.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_8.ImageIndex = 5;
            this.blImgAngleValue_8.Location = new System.Drawing.Point(727, 218);
            this.blImgAngleValue_8.Name = "blImgAngleValue_8";
            this.blImgAngleValue_8.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_8.TabIndex = 1726;
            this.blImgAngleValue_8.Text = "0";
            this.blImgAngleValue_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_9
            // 
            this.blImgAngleValue_9.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_9.ImageIndex = 5;
            this.blImgAngleValue_9.Location = new System.Drawing.Point(647, 244);
            this.blImgAngleValue_9.Name = "blImgAngleValue_9";
            this.blImgAngleValue_9.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_9.TabIndex = 1725;
            this.blImgAngleValue_9.Text = "0";
            this.blImgAngleValue_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_10
            // 
            this.blImgAngleValue_10.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_10.ImageIndex = 5;
            this.blImgAngleValue_10.Location = new System.Drawing.Point(594, 291);
            this.blImgAngleValue_10.Name = "blImgAngleValue_10";
            this.blImgAngleValue_10.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_10.TabIndex = 1724;
            this.blImgAngleValue_10.Text = "0";
            this.blImgAngleValue_10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_11
            // 
            this.blImgAngleValue_11.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_11.ImageIndex = 5;
            this.blImgAngleValue_11.Location = new System.Drawing.Point(574, 370);
            this.blImgAngleValue_11.Name = "blImgAngleValue_11";
            this.blImgAngleValue_11.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_11.TabIndex = 1723;
            this.blImgAngleValue_11.Text = "0";
            this.blImgAngleValue_11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_0
            // 
            this.blImgAngleValue_0.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_0.ImageIndex = 5;
            this.blImgAngleValue_0.Location = new System.Drawing.Point(596, 446);
            this.blImgAngleValue_0.Name = "blImgAngleValue_0";
            this.blImgAngleValue_0.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_0.TabIndex = 1722;
            this.blImgAngleValue_0.Text = "0";
            this.blImgAngleValue_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_1
            // 
            this.blImgAngleValue_1.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_1.ImageIndex = 5;
            this.blImgAngleValue_1.Location = new System.Drawing.Point(646, 495);
            this.blImgAngleValue_1.Name = "blImgAngleValue_1";
            this.blImgAngleValue_1.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_1.TabIndex = 1721;
            this.blImgAngleValue_1.Text = "0";
            this.blImgAngleValue_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blImgAngleValue_2
            // 
            this.blImgAngleValue_2.BackColor = System.Drawing.Color.Transparent;
            this.blImgAngleValue_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blImgAngleValue_2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blImgAngleValue_2.ImageIndex = 5;
            this.blImgAngleValue_2.Location = new System.Drawing.Point(727, 524);
            this.blImgAngleValue_2.Name = "blImgAngleValue_2";
            this.blImgAngleValue_2.Size = new System.Drawing.Size(76, 23);
            this.blImgAngleValue_2.TabIndex = 1720;
            this.blImgAngleValue_2.Text = "0";
            this.blImgAngleValue_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAngle_All
            // 
            this.btnAngle_All.BackColor = System.Drawing.Color.Silver;
            this.btnAngle_All.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAngle_All.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnAngle_All.GlowColor = System.Drawing.Color.Transparent;
            this.btnAngle_All.InnerBorderColor = System.Drawing.Color.White;
            this.btnAngle_All.Location = new System.Drawing.Point(258, 23);
            this.btnAngle_All.Name = "btnAngle_All";
            this.btnAngle_All.OuterBorderColor = System.Drawing.Color.Black;
            this.btnAngle_All.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAngle_All.ShineColor = System.Drawing.Color.Silver;
            this.btnAngle_All.Size = new System.Drawing.Size(205, 44);
            this.btnAngle_All.TabIndex = 1718;
            this.btnAngle_All.TabStop = false;
            this.btnAngle_All.Tag = "All";
            this.btnAngle_All.Text = "Angle All Measuring";
            this.btnAngle_All.Click += new System.EventHandler(this.btnAngle_Single_Click);
            // 
            // btnAngle_Single
            // 
            this.btnAngle_Single.BackColor = System.Drawing.Color.Silver;
            this.btnAngle_Single.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAngle_Single.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnAngle_Single.GlowColor = System.Drawing.Color.Transparent;
            this.btnAngle_Single.InnerBorderColor = System.Drawing.Color.White;
            this.btnAngle_Single.Location = new System.Drawing.Point(24, 23);
            this.btnAngle_Single.Name = "btnAngle_Single";
            this.btnAngle_Single.OuterBorderColor = System.Drawing.Color.Black;
            this.btnAngle_Single.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAngle_Single.ShineColor = System.Drawing.Color.Silver;
            this.btnAngle_Single.Size = new System.Drawing.Size(205, 44);
            this.btnAngle_Single.TabIndex = 1717;
            this.btnAngle_Single.TabStop = false;
            this.btnAngle_Single.Tag = "Single";
            this.btnAngle_Single.Text = "Angle Single Measuring";
            this.btnAngle_Single.Click += new System.EventHandler(this.btnAngle_Single_Click);
            // 
            // tpanel__Measuring
            // 
            this.tpanel__Measuring.ColumnCount = 7;
            this.tpanel__Measuring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tpanel__Measuring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tpanel__Measuring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tpanel__Measuring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tpanel__Measuring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tpanel__Measuring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tpanel__Measuring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tpanel__Measuring.Location = new System.Drawing.Point(21, 77);
            this.tpanel__Measuring.Name = "tpanel__Measuring";
            this.tpanel__Measuring.RowCount = 13;
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tpanel__Measuring.Size = new System.Drawing.Size(485, 302);
            this.tpanel__Measuring.TabIndex = 1716;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(261, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(981, 767);
            this.pictureBox1.TabIndex = 1719;
            this.pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.lbNGValue);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.lbNGType);
            this.panel5.Controls.Add(this.lbNGTrayIndexTitle);
            this.panel5.Controls.Add(this.lbNGIndex);
            this.panel5.Controls.Add(this.btnNgSkip);
            this.panel5.Controls.Add(this.picNGMap);
            this.panel5.Controls.Add(this.lbNGTrayInfo);
            this.panel5.Location = new System.Drawing.Point(1390, 827);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(527, 107);
            this.panel5.TabIndex = 1885;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Gainsboro;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(427, 25);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label13.Size = new System.Drawing.Size(89, 22);
            this.label13.TabIndex = 1921;
            this.label13.Text = "VALUE";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNGValue
            // 
            this.lbNGValue.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbNGValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbNGValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNGValue.ForeColor = System.Drawing.Color.Black;
            this.lbNGValue.Location = new System.Drawing.Point(427, 46);
            this.lbNGValue.Name = "lbNGValue";
            this.lbNGValue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbNGValue.Size = new System.Drawing.Size(89, 22);
            this.lbNGValue.TabIndex = 1920;
            this.lbNGValue.Text = "-";
            this.lbNGValue.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(321, 25);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label9.Size = new System.Drawing.Size(100, 22);
            this.label9.TabIndex = 1919;
            this.label9.Text = "NG TYPE";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNGType
            // 
            this.lbNGType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbNGType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbNGType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNGType.ForeColor = System.Drawing.Color.Black;
            this.lbNGType.Location = new System.Drawing.Point(321, 46);
            this.lbNGType.Name = "lbNGType";
            this.lbNGType.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbNGType.Size = new System.Drawing.Size(100, 22);
            this.lbNGType.TabIndex = 1918;
            this.lbNGType.Text = "-";
            this.lbNGType.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbNGTrayIndexTitle
            // 
            this.lbNGTrayIndexTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbNGTrayIndexTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbNGTrayIndexTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNGTrayIndexTitle.ForeColor = System.Drawing.Color.Black;
            this.lbNGTrayIndexTitle.Location = new System.Drawing.Point(262, 25);
            this.lbNGTrayIndexTitle.Name = "lbNGTrayIndexTitle";
            this.lbNGTrayIndexTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbNGTrayIndexTitle.Size = new System.Drawing.Size(52, 22);
            this.lbNGTrayIndexTitle.TabIndex = 1917;
            this.lbNGTrayIndexTitle.Text = "INDEX";
            this.lbNGTrayIndexTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNGIndex
            // 
            this.lbNGIndex.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbNGIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbNGIndex.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNGIndex.ForeColor = System.Drawing.Color.Black;
            this.lbNGIndex.Location = new System.Drawing.Point(262, 46);
            this.lbNGIndex.Name = "lbNGIndex";
            this.lbNGIndex.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbNGIndex.Size = new System.Drawing.Size(52, 22);
            this.lbNGIndex.TabIndex = 1916;
            this.lbNGIndex.Text = "-";
            this.lbNGIndex.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnNgSkip
            // 
            this.btnNgSkip.BackColor = System.Drawing.Color.Silver;
            this.btnNgSkip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNgSkip.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnNgSkip.GlowColor = System.Drawing.Color.Transparent;
            this.btnNgSkip.InnerBorderColor = System.Drawing.Color.White;
            this.btnNgSkip.Location = new System.Drawing.Point(447, 71);
            this.btnNgSkip.Name = "btnNgSkip";
            this.btnNgSkip.OuterBorderColor = System.Drawing.Color.Black;
            this.btnNgSkip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNgSkip.ShineColor = System.Drawing.Color.Silver;
            this.btnNgSkip.Size = new System.Drawing.Size(68, 27);
            this.btnNgSkip.TabIndex = 1911;
            this.btnNgSkip.TabStop = false;
            this.btnNgSkip.Tag = "0";
            this.btnNgSkip.Text = "SKIP";
            this.btnNgSkip.Click += new System.EventHandler(this.btnVcmSkip_Click);
            // 
            // picNGMap
            // 
            this.picNGMap.BackColor = System.Drawing.Color.Black;
            this.picNGMap.Location = new System.Drawing.Point(2, 24);
            this.picNGMap.Name = "picNGMap";
            this.picNGMap.Size = new System.Drawing.Size(240, 80);
            this.picNGMap.TabIndex = 1445;
            this.picNGMap.TabStop = false;
            this.picNGMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picNGMap_MouseClick);
            // 
            // lbNGTrayInfo
            // 
            this.lbNGTrayInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.lbNGTrayInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNGTrayInfo.ForeColor = System.Drawing.Color.Navy;
            this.lbNGTrayInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbNGTrayInfo.Location = new System.Drawing.Point(-1, 0);
            this.lbNGTrayInfo.Name = "lbNGTrayInfo";
            this.lbNGTrayInfo.Size = new System.Drawing.Size(527, 22);
            this.lbNGTrayInfo.TabIndex = 1443;
            this.lbNGTrayInfo.Text = "NG TRAY INFORMATION";
            this.lbNGTrayInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnUnloadReset);
            this.panel4.Controls.Add(this.btnUnloadTraySkip);
            this.panel4.Controls.Add(this.lbUnloaderSlotNoTitle);
            this.panel4.Controls.Add(this.lbUnloaderSlotNo);
            this.panel4.Controls.Add(this.lbUnloaderTrayIndexYTitle);
            this.panel4.Controls.Add(this.lbUnloaderTrayIndexY);
            this.panel4.Controls.Add(this.lbUnloaderTrayIndexXTitle);
            this.panel4.Controls.Add(this.lbUnloaderTrayIndexX);
            this.panel4.Controls.Add(this.picUnloadMap);
            this.panel4.Controls.Add(this.lbUnloaderTrayInfo);
            this.panel4.Location = new System.Drawing.Point(1390, 551);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(527, 270);
            this.panel4.TabIndex = 1884;
            // 
            // btnUnloadReset
            // 
            this.btnUnloadReset.BackColor = System.Drawing.Color.Silver;
            this.btnUnloadReset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnloadReset.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnUnloadReset.GlowColor = System.Drawing.Color.Transparent;
            this.btnUnloadReset.InnerBorderColor = System.Drawing.Color.White;
            this.btnUnloadReset.Location = new System.Drawing.Point(262, 106);
            this.btnUnloadReset.Name = "btnUnloadReset";
            this.btnUnloadReset.OuterBorderColor = System.Drawing.Color.Black;
            this.btnUnloadReset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUnloadReset.ShineColor = System.Drawing.Color.Silver;
            this.btnUnloadReset.Size = new System.Drawing.Size(68, 33);
            this.btnUnloadReset.TabIndex = 1909;
            this.btnUnloadReset.TabStop = false;
            this.btnUnloadReset.Tag = "0";
            this.btnUnloadReset.Text = "RESET";
            this.btnUnloadReset.Click += new System.EventHandler(this.btnVcmReset_Click);
            // 
            // btnUnloadTraySkip
            // 
            this.btnUnloadTraySkip.BackColor = System.Drawing.Color.Silver;
            this.btnUnloadTraySkip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnloadTraySkip.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnUnloadTraySkip.GlowColor = System.Drawing.Color.Transparent;
            this.btnUnloadTraySkip.InnerBorderColor = System.Drawing.Color.White;
            this.btnUnloadTraySkip.Location = new System.Drawing.Point(447, 106);
            this.btnUnloadTraySkip.Name = "btnUnloadTraySkip";
            this.btnUnloadTraySkip.OuterBorderColor = System.Drawing.Color.Black;
            this.btnUnloadTraySkip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUnloadTraySkip.ShineColor = System.Drawing.Color.Silver;
            this.btnUnloadTraySkip.Size = new System.Drawing.Size(68, 33);
            this.btnUnloadTraySkip.TabIndex = 1910;
            this.btnUnloadTraySkip.TabStop = false;
            this.btnUnloadTraySkip.Tag = "0";
            this.btnUnloadTraySkip.Text = "SKIP";
            this.btnUnloadTraySkip.Click += new System.EventHandler(this.btnVcmSkip_Click);
            // 
            // lbUnloaderSlotNoTitle
            // 
            this.lbUnloaderSlotNoTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbUnloaderSlotNoTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloaderSlotNoTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderSlotNoTitle.ForeColor = System.Drawing.Color.Black;
            this.lbUnloaderSlotNoTitle.Location = new System.Drawing.Point(261, 228);
            this.lbUnloaderSlotNoTitle.Name = "lbUnloaderSlotNoTitle";
            this.lbUnloaderSlotNoTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbUnloaderSlotNoTitle.Size = new System.Drawing.Size(94, 22);
            this.lbUnloaderSlotNoTitle.TabIndex = 1732;
            this.lbUnloaderSlotNoTitle.Text = "SLOT NO";
            this.lbUnloaderSlotNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUnloaderSlotNo
            // 
            this.lbUnloaderSlotNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbUnloaderSlotNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloaderSlotNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderSlotNo.ForeColor = System.Drawing.Color.Black;
            this.lbUnloaderSlotNo.Location = new System.Drawing.Point(261, 228);
            this.lbUnloaderSlotNo.Name = "lbUnloaderSlotNo";
            this.lbUnloaderSlotNo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbUnloaderSlotNo.Size = new System.Drawing.Size(254, 35);
            this.lbUnloaderSlotNo.TabIndex = 1731;
            this.lbUnloaderSlotNo.Text = "3";
            this.lbUnloaderSlotNo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbUnloaderTrayIndexYTitle
            // 
            this.lbUnloaderTrayIndexYTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbUnloaderTrayIndexYTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloaderTrayIndexYTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderTrayIndexYTitle.ForeColor = System.Drawing.Color.Black;
            this.lbUnloaderTrayIndexYTitle.Location = new System.Drawing.Point(262, 63);
            this.lbUnloaderTrayIndexYTitle.Name = "lbUnloaderTrayIndexYTitle";
            this.lbUnloaderTrayIndexYTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbUnloaderTrayIndexYTitle.Size = new System.Drawing.Size(94, 22);
            this.lbUnloaderTrayIndexYTitle.TabIndex = 1730;
            this.lbUnloaderTrayIndexYTitle.Text = "Y INDEX";
            this.lbUnloaderTrayIndexYTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUnloaderTrayIndexY
            // 
            this.lbUnloaderTrayIndexY.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbUnloaderTrayIndexY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloaderTrayIndexY.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderTrayIndexY.ForeColor = System.Drawing.Color.Black;
            this.lbUnloaderTrayIndexY.Location = new System.Drawing.Point(262, 63);
            this.lbUnloaderTrayIndexY.Name = "lbUnloaderTrayIndexY";
            this.lbUnloaderTrayIndexY.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbUnloaderTrayIndexY.Size = new System.Drawing.Size(254, 35);
            this.lbUnloaderTrayIndexY.TabIndex = 1729;
            this.lbUnloaderTrayIndexY.Text = "3";
            this.lbUnloaderTrayIndexY.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbUnloaderTrayIndexXTitle
            // 
            this.lbUnloaderTrayIndexXTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbUnloaderTrayIndexXTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloaderTrayIndexXTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderTrayIndexXTitle.ForeColor = System.Drawing.Color.Black;
            this.lbUnloaderTrayIndexXTitle.Location = new System.Drawing.Point(262, 27);
            this.lbUnloaderTrayIndexXTitle.Name = "lbUnloaderTrayIndexXTitle";
            this.lbUnloaderTrayIndexXTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbUnloaderTrayIndexXTitle.Size = new System.Drawing.Size(94, 22);
            this.lbUnloaderTrayIndexXTitle.TabIndex = 1728;
            this.lbUnloaderTrayIndexXTitle.Text = "X INDEX";
            this.lbUnloaderTrayIndexXTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUnloaderTrayIndexX
            // 
            this.lbUnloaderTrayIndexX.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbUnloaderTrayIndexX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloaderTrayIndexX.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderTrayIndexX.ForeColor = System.Drawing.Color.Black;
            this.lbUnloaderTrayIndexX.Location = new System.Drawing.Point(262, 27);
            this.lbUnloaderTrayIndexX.Name = "lbUnloaderTrayIndexX";
            this.lbUnloaderTrayIndexX.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbUnloaderTrayIndexX.Size = new System.Drawing.Size(254, 35);
            this.lbUnloaderTrayIndexX.TabIndex = 1727;
            this.lbUnloaderTrayIndexX.Text = "3";
            this.lbUnloaderTrayIndexX.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // picUnloadMap
            // 
            this.picUnloadMap.BackColor = System.Drawing.Color.Black;
            this.picUnloadMap.Location = new System.Drawing.Point(2, 27);
            this.picUnloadMap.Name = "picUnloadMap";
            this.picUnloadMap.Size = new System.Drawing.Size(240, 240);
            this.picUnloadMap.TabIndex = 1445;
            this.picUnloadMap.TabStop = false;
            this.picUnloadMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picVCMMap_MouseDoubleClick);
            // 
            // lbUnloaderTrayInfo
            // 
            this.lbUnloaderTrayInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.lbUnloaderTrayInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderTrayInfo.ForeColor = System.Drawing.Color.Navy;
            this.lbUnloaderTrayInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbUnloaderTrayInfo.Location = new System.Drawing.Point(-1, 0);
            this.lbUnloaderTrayInfo.Name = "lbUnloaderTrayInfo";
            this.lbUnloaderTrayInfo.Size = new System.Drawing.Size(527, 22);
            this.lbUnloaderTrayInfo.TabIndex = 1443;
            this.lbUnloaderTrayInfo.Text = "UNLOADER TRAY INFORMATION";
            this.lbUnloaderTrayInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnLensReset);
            this.panel3.Controls.Add(this.btnLensSkip);
            this.panel3.Controls.Add(this.lbLensSlotNoTitle);
            this.panel3.Controls.Add(this.lbLensSlotNo);
            this.panel3.Controls.Add(this.lbLensTrayIndexYTitle);
            this.panel3.Controls.Add(this.lbLensTrayIndexY);
            this.panel3.Controls.Add(this.lbLensTrayIndexXTitle);
            this.panel3.Controls.Add(this.lbLensTrayIndexX);
            this.panel3.Controls.Add(this.picLensMap);
            this.panel3.Controls.Add(this.lbLensTrayInfo);
            this.panel3.Location = new System.Drawing.Point(1390, 281);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(527, 269);
            this.panel3.TabIndex = 1883;
            // 
            // btnLensReset
            // 
            this.btnLensReset.BackColor = System.Drawing.Color.Silver;
            this.btnLensReset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLensReset.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnLensReset.GlowColor = System.Drawing.Color.Transparent;
            this.btnLensReset.InnerBorderColor = System.Drawing.Color.White;
            this.btnLensReset.Location = new System.Drawing.Point(262, 98);
            this.btnLensReset.Name = "btnLensReset";
            this.btnLensReset.OuterBorderColor = System.Drawing.Color.Black;
            this.btnLensReset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLensReset.ShineColor = System.Drawing.Color.Silver;
            this.btnLensReset.Size = new System.Drawing.Size(68, 33);
            this.btnLensReset.TabIndex = 1909;
            this.btnLensReset.TabStop = false;
            this.btnLensReset.Tag = "0";
            this.btnLensReset.Text = "RESET";
            this.btnLensReset.Click += new System.EventHandler(this.btnVcmReset_Click);
            // 
            // btnLensSkip
            // 
            this.btnLensSkip.BackColor = System.Drawing.Color.Silver;
            this.btnLensSkip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLensSkip.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnLensSkip.GlowColor = System.Drawing.Color.Transparent;
            this.btnLensSkip.InnerBorderColor = System.Drawing.Color.White;
            this.btnLensSkip.Location = new System.Drawing.Point(448, 104);
            this.btnLensSkip.Name = "btnLensSkip";
            this.btnLensSkip.OuterBorderColor = System.Drawing.Color.Black;
            this.btnLensSkip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLensSkip.ShineColor = System.Drawing.Color.Silver;
            this.btnLensSkip.Size = new System.Drawing.Size(68, 33);
            this.btnLensSkip.TabIndex = 1909;
            this.btnLensSkip.TabStop = false;
            this.btnLensSkip.Tag = "0";
            this.btnLensSkip.Text = "SKIP";
            this.btnLensSkip.Click += new System.EventHandler(this.btnVcmSkip_Click);
            // 
            // lbLensSlotNoTitle
            // 
            this.lbLensSlotNoTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensSlotNoTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensSlotNoTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensSlotNoTitle.ForeColor = System.Drawing.Color.Black;
            this.lbLensSlotNoTitle.Location = new System.Drawing.Point(261, 229);
            this.lbLensSlotNoTitle.Name = "lbLensSlotNoTitle";
            this.lbLensSlotNoTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbLensSlotNoTitle.Size = new System.Drawing.Size(94, 22);
            this.lbLensSlotNoTitle.TabIndex = 1732;
            this.lbLensSlotNoTitle.Text = "SLOT NO";
            this.lbLensSlotNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensSlotNo
            // 
            this.lbLensSlotNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbLensSlotNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensSlotNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensSlotNo.ForeColor = System.Drawing.Color.Black;
            this.lbLensSlotNo.Location = new System.Drawing.Point(261, 229);
            this.lbLensSlotNo.Name = "lbLensSlotNo";
            this.lbLensSlotNo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbLensSlotNo.Size = new System.Drawing.Size(254, 35);
            this.lbLensSlotNo.TabIndex = 1731;
            this.lbLensSlotNo.Text = "3";
            this.lbLensSlotNo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbLensTrayIndexYTitle
            // 
            this.lbLensTrayIndexYTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensTrayIndexYTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensTrayIndexYTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensTrayIndexYTitle.ForeColor = System.Drawing.Color.Black;
            this.lbLensTrayIndexYTitle.Location = new System.Drawing.Point(262, 62);
            this.lbLensTrayIndexYTitle.Name = "lbLensTrayIndexYTitle";
            this.lbLensTrayIndexYTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbLensTrayIndexYTitle.Size = new System.Drawing.Size(94, 22);
            this.lbLensTrayIndexYTitle.TabIndex = 1730;
            this.lbLensTrayIndexYTitle.Text = "Y INDEX";
            this.lbLensTrayIndexYTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensTrayIndexY
            // 
            this.lbLensTrayIndexY.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbLensTrayIndexY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensTrayIndexY.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensTrayIndexY.ForeColor = System.Drawing.Color.Black;
            this.lbLensTrayIndexY.Location = new System.Drawing.Point(262, 62);
            this.lbLensTrayIndexY.Name = "lbLensTrayIndexY";
            this.lbLensTrayIndexY.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbLensTrayIndexY.Size = new System.Drawing.Size(254, 35);
            this.lbLensTrayIndexY.TabIndex = 1729;
            this.lbLensTrayIndexY.Text = "3";
            this.lbLensTrayIndexY.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbLensTrayIndexXTitle
            // 
            this.lbLensTrayIndexXTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensTrayIndexXTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensTrayIndexXTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensTrayIndexXTitle.ForeColor = System.Drawing.Color.Black;
            this.lbLensTrayIndexXTitle.Location = new System.Drawing.Point(262, 26);
            this.lbLensTrayIndexXTitle.Name = "lbLensTrayIndexXTitle";
            this.lbLensTrayIndexXTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbLensTrayIndexXTitle.Size = new System.Drawing.Size(94, 22);
            this.lbLensTrayIndexXTitle.TabIndex = 1728;
            this.lbLensTrayIndexXTitle.Text = "X INDEX";
            this.lbLensTrayIndexXTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensTrayIndexX
            // 
            this.lbLensTrayIndexX.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbLensTrayIndexX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensTrayIndexX.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensTrayIndexX.ForeColor = System.Drawing.Color.Black;
            this.lbLensTrayIndexX.Location = new System.Drawing.Point(262, 26);
            this.lbLensTrayIndexX.Name = "lbLensTrayIndexX";
            this.lbLensTrayIndexX.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbLensTrayIndexX.Size = new System.Drawing.Size(254, 35);
            this.lbLensTrayIndexX.TabIndex = 1727;
            this.lbLensTrayIndexX.Text = "3";
            this.lbLensTrayIndexX.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // picLensMap
            // 
            this.picLensMap.BackColor = System.Drawing.Color.Black;
            this.picLensMap.Location = new System.Drawing.Point(3, 26);
            this.picLensMap.Name = "picLensMap";
            this.picLensMap.Size = new System.Drawing.Size(240, 240);
            this.picLensMap.TabIndex = 1445;
            this.picLensMap.TabStop = false;
            this.picLensMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picVCMMap_MouseDoubleClick);
            // 
            // lbLensTrayInfo
            // 
            this.lbLensTrayInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensTrayInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensTrayInfo.ForeColor = System.Drawing.Color.Navy;
            this.lbLensTrayInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbLensTrayInfo.Location = new System.Drawing.Point(-1, 0);
            this.lbLensTrayInfo.Name = "lbLensTrayInfo";
            this.lbLensTrayInfo.Size = new System.Drawing.Size(527, 22);
            this.lbLensTrayInfo.TabIndex = 1443;
            this.lbLensTrayInfo.Text = "LENS TRAY INFORMATION";
            this.lbLensTrayInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnVcmReset);
            this.panel2.Controls.Add(this.btnVcmSkip);
            this.panel2.Controls.Add(this.lbVCMSlotNoTitle);
            this.panel2.Controls.Add(this.lbVCMSlotNo);
            this.panel2.Controls.Add(this.lbVCMTrayIndexYTitle);
            this.panel2.Controls.Add(this.lbVCMTrayIndexY);
            this.panel2.Controls.Add(this.lbVCMTrayIndexXTitle);
            this.panel2.Controls.Add(this.lbVCMTrayIndexX);
            this.panel2.Controls.Add(this.picVCMMap);
            this.panel2.Controls.Add(this.lbVCMTrayInfo);
            this.panel2.Location = new System.Drawing.Point(1390, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(527, 268);
            this.panel2.TabIndex = 1882;
            // 
            // btnVcmReset
            // 
            this.btnVcmReset.BackColor = System.Drawing.Color.Silver;
            this.btnVcmReset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVcmReset.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnVcmReset.GlowColor = System.Drawing.Color.Transparent;
            this.btnVcmReset.InnerBorderColor = System.Drawing.Color.White;
            this.btnVcmReset.Location = new System.Drawing.Point(261, 100);
            this.btnVcmReset.Name = "btnVcmReset";
            this.btnVcmReset.OuterBorderColor = System.Drawing.Color.Black;
            this.btnVcmReset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnVcmReset.ShineColor = System.Drawing.Color.Silver;
            this.btnVcmReset.Size = new System.Drawing.Size(68, 33);
            this.btnVcmReset.TabIndex = 1909;
            this.btnVcmReset.TabStop = false;
            this.btnVcmReset.Tag = "0";
            this.btnVcmReset.Text = "RESET";
            this.btnVcmReset.Click += new System.EventHandler(this.btnVcmReset_Click);
            // 
            // btnVcmSkip
            // 
            this.btnVcmSkip.BackColor = System.Drawing.Color.Silver;
            this.btnVcmSkip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVcmSkip.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnVcmSkip.GlowColor = System.Drawing.Color.Transparent;
            this.btnVcmSkip.InnerBorderColor = System.Drawing.Color.White;
            this.btnVcmSkip.Location = new System.Drawing.Point(450, 100);
            this.btnVcmSkip.Name = "btnVcmSkip";
            this.btnVcmSkip.OuterBorderColor = System.Drawing.Color.Black;
            this.btnVcmSkip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnVcmSkip.ShineColor = System.Drawing.Color.Silver;
            this.btnVcmSkip.Size = new System.Drawing.Size(68, 33);
            this.btnVcmSkip.TabIndex = 1908;
            this.btnVcmSkip.TabStop = false;
            this.btnVcmSkip.Tag = "0";
            this.btnVcmSkip.Text = "SKIP";
            this.btnVcmSkip.Click += new System.EventHandler(this.btnVcmSkip_Click);
            // 
            // lbVCMSlotNoTitle
            // 
            this.lbVCMSlotNoTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbVCMSlotNoTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMSlotNoTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMSlotNoTitle.ForeColor = System.Drawing.Color.Black;
            this.lbVCMSlotNoTitle.Location = new System.Drawing.Point(261, 229);
            this.lbVCMSlotNoTitle.Name = "lbVCMSlotNoTitle";
            this.lbVCMSlotNoTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbVCMSlotNoTitle.Size = new System.Drawing.Size(94, 22);
            this.lbVCMSlotNoTitle.TabIndex = 1728;
            this.lbVCMSlotNoTitle.Text = "SLOT NO";
            this.lbVCMSlotNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVCMSlotNo
            // 
            this.lbVCMSlotNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbVCMSlotNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMSlotNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMSlotNo.ForeColor = System.Drawing.Color.Black;
            this.lbVCMSlotNo.Location = new System.Drawing.Point(261, 229);
            this.lbVCMSlotNo.Name = "lbVCMSlotNo";
            this.lbVCMSlotNo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbVCMSlotNo.Size = new System.Drawing.Size(254, 35);
            this.lbVCMSlotNo.TabIndex = 1727;
            this.lbVCMSlotNo.Text = "3";
            this.lbVCMSlotNo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbVCMTrayIndexYTitle
            // 
            this.lbVCMTrayIndexYTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbVCMTrayIndexYTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMTrayIndexYTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMTrayIndexYTitle.ForeColor = System.Drawing.Color.Black;
            this.lbVCMTrayIndexYTitle.Location = new System.Drawing.Point(262, 62);
            this.lbVCMTrayIndexYTitle.Name = "lbVCMTrayIndexYTitle";
            this.lbVCMTrayIndexYTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbVCMTrayIndexYTitle.Size = new System.Drawing.Size(94, 22);
            this.lbVCMTrayIndexYTitle.TabIndex = 1726;
            this.lbVCMTrayIndexYTitle.Text = "Y INDEX";
            this.lbVCMTrayIndexYTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVCMTrayIndexY
            // 
            this.lbVCMTrayIndexY.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbVCMTrayIndexY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMTrayIndexY.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMTrayIndexY.ForeColor = System.Drawing.Color.Black;
            this.lbVCMTrayIndexY.Location = new System.Drawing.Point(262, 62);
            this.lbVCMTrayIndexY.Name = "lbVCMTrayIndexY";
            this.lbVCMTrayIndexY.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbVCMTrayIndexY.Size = new System.Drawing.Size(254, 35);
            this.lbVCMTrayIndexY.TabIndex = 1725;
            this.lbVCMTrayIndexY.Text = "3";
            this.lbVCMTrayIndexY.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbVCMTrayIndexXTitle
            // 
            this.lbVCMTrayIndexXTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lbVCMTrayIndexXTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMTrayIndexXTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMTrayIndexXTitle.ForeColor = System.Drawing.Color.Black;
            this.lbVCMTrayIndexXTitle.Location = new System.Drawing.Point(262, 26);
            this.lbVCMTrayIndexXTitle.Name = "lbVCMTrayIndexXTitle";
            this.lbVCMTrayIndexXTitle.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbVCMTrayIndexXTitle.Size = new System.Drawing.Size(94, 22);
            this.lbVCMTrayIndexXTitle.TabIndex = 1724;
            this.lbVCMTrayIndexXTitle.Text = "X INDEX";
            this.lbVCMTrayIndexXTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVCMTrayIndexX
            // 
            this.lbVCMTrayIndexX.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbVCMTrayIndexX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVCMTrayIndexX.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMTrayIndexX.ForeColor = System.Drawing.Color.Black;
            this.lbVCMTrayIndexX.Location = new System.Drawing.Point(262, 26);
            this.lbVCMTrayIndexX.Name = "lbVCMTrayIndexX";
            this.lbVCMTrayIndexX.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbVCMTrayIndexX.Size = new System.Drawing.Size(254, 35);
            this.lbVCMTrayIndexX.TabIndex = 1723;
            this.lbVCMTrayIndexX.Text = "3";
            this.lbVCMTrayIndexX.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // picVCMMap
            // 
            this.picVCMMap.BackColor = System.Drawing.Color.Black;
            this.picVCMMap.Location = new System.Drawing.Point(3, 25);
            this.picVCMMap.Name = "picVCMMap";
            this.picVCMMap.Size = new System.Drawing.Size(240, 240);
            this.picVCMMap.TabIndex = 1444;
            this.picVCMMap.TabStop = false;
            this.picVCMMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picVCMMap_MouseDoubleClick);
            // 
            // lbVCMTrayInfo
            // 
            this.lbVCMTrayInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.lbVCMTrayInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVCMTrayInfo.ForeColor = System.Drawing.Color.Navy;
            this.lbVCMTrayInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbVCMTrayInfo.Location = new System.Drawing.Point(-1, 0);
            this.lbVCMTrayInfo.Name = "lbVCMTrayInfo";
            this.lbVCMTrayInfo.Size = new System.Drawing.Size(527, 22);
            this.lbVCMTrayInfo.TabIndex = 1443;
            this.lbVCMTrayInfo.Text = "VCM TRAY INFORMATION";
            this.lbVCMTrayInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTaskLog
            // 
            this.btnTaskLog.BackColor = System.Drawing.Color.Silver;
            this.btnTaskLog.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaskLog.ForeColor = System.Drawing.Color.Black;
            this.btnTaskLog.GlowColor = System.Drawing.Color.Silver;
            this.btnTaskLog.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTaskLog.InnerBorderColor = System.Drawing.Color.Silver;
            this.btnTaskLog.Location = new System.Drawing.Point(1817, 935);
            this.btnTaskLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTaskLog.Name = "btnTaskLog";
            this.btnTaskLog.OuterBorderColor = System.Drawing.Color.Silver;
            this.btnTaskLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnTaskLog.ShineColor = System.Drawing.Color.Silver;
            this.btnTaskLog.Size = new System.Drawing.Size(100, 69);
            this.btnTaskLog.TabIndex = 1881;
            this.btnTaskLog.TabStop = false;
            this.btnTaskLog.Tag = "4";
            this.btnTaskLog.Text = "TASK LOG";
            this.btnTaskLog.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.lblUseMES);
            this.panel9.Controls.Add(this.lbDSensor);
            this.panel9.Controls.Add(this.lbUVLamp);
            this.panel9.Controls.Add(this.lbInterface);
            this.panel9.Controls.Add(this.lbACTUATOR);
            this.panel9.Controls.Add(this.lbMainAir);
            this.panel9.Controls.Add(this.lbMC);
            this.panel9.Controls.Add(this.lbDispenser);
            this.panel9.Controls.Add(this.lbVision);
            this.panel9.Location = new System.Drawing.Point(2, 774);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(228, 230);
            this.panel9.TabIndex = 1876;
            // 
            // lblUseMES
            // 
            this.lblUseMES.BackColor = System.Drawing.Color.White;
            this.lblUseMES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUseMES.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUseMES.ForeColor = System.Drawing.Color.Black;
            this.lblUseMES.Location = new System.Drawing.Point(3, 197);
            this.lblUseMES.Name = "lblUseMES";
            this.lblUseMES.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblUseMES.Size = new System.Drawing.Size(220, 22);
            this.lblUseMES.TabIndex = 1888;
            this.lblUseMES.Text = "MES AGENT";
            this.lblUseMES.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDSensor
            // 
            this.lbDSensor.BackColor = System.Drawing.Color.White;
            this.lbDSensor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDSensor.ForeColor = System.Drawing.Color.Black;
            this.lbDSensor.Location = new System.Drawing.Point(3, 173);
            this.lbDSensor.Name = "lbDSensor";
            this.lbDSensor.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDSensor.Size = new System.Drawing.Size(220, 22);
            this.lbDSensor.TabIndex = 1887;
            this.lbDSensor.Text = "DSENSOR";
            this.lbDSensor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUVLamp
            // 
            this.lbUVLamp.BackColor = System.Drawing.Color.White;
            this.lbUVLamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUVLamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUVLamp.ForeColor = System.Drawing.Color.Black;
            this.lbUVLamp.Location = new System.Drawing.Point(3, 149);
            this.lbUVLamp.Name = "lbUVLamp";
            this.lbUVLamp.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbUVLamp.Size = new System.Drawing.Size(220, 22);
            this.lbUVLamp.TabIndex = 1886;
            this.lbUVLamp.Text = "UV LAMP";
            this.lbUVLamp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbInterface
            // 
            this.lbInterface.BackColor = System.Drawing.Color.Gainsboro;
            this.lbInterface.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInterface.ForeColor = System.Drawing.Color.Navy;
            this.lbInterface.Location = new System.Drawing.Point(0, 0);
            this.lbInterface.Name = "lbInterface";
            this.lbInterface.Size = new System.Drawing.Size(225, 24);
            this.lbInterface.TabIndex = 1868;
            this.lbInterface.Tag = "";
            this.lbInterface.Text = "INTERFACE";
            this.lbInterface.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbACTUATOR
            // 
            this.lbACTUATOR.BackColor = System.Drawing.Color.White;
            this.lbACTUATOR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbACTUATOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbACTUATOR.ForeColor = System.Drawing.Color.Black;
            this.lbACTUATOR.Location = new System.Drawing.Point(3, 125);
            this.lbACTUATOR.Name = "lbACTUATOR";
            this.lbACTUATOR.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbACTUATOR.Size = new System.Drawing.Size(220, 22);
            this.lbACTUATOR.TabIndex = 1885;
            this.lbACTUATOR.Text = "ACTUATOR";
            this.lbACTUATOR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMainAir
            // 
            this.lbMainAir.BackColor = System.Drawing.Color.White;
            this.lbMainAir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbMainAir.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMainAir.ForeColor = System.Drawing.Color.Black;
            this.lbMainAir.Location = new System.Drawing.Point(3, 53);
            this.lbMainAir.Name = "lbMainAir";
            this.lbMainAir.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbMainAir.Size = new System.Drawing.Size(220, 22);
            this.lbMainAir.TabIndex = 1882;
            this.lbMainAir.Text = "MAIN AIR";
            this.lbMainAir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMC
            // 
            this.lbMC.BackColor = System.Drawing.Color.White;
            this.lbMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMC.ForeColor = System.Drawing.Color.Black;
            this.lbMC.Location = new System.Drawing.Point(3, 29);
            this.lbMC.Name = "lbMC";
            this.lbMC.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbMC.Size = new System.Drawing.Size(220, 22);
            this.lbMC.TabIndex = 1884;
            this.lbMC.Text = "MC";
            this.lbMC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDispenser
            // 
            this.lbDispenser.BackColor = System.Drawing.Color.White;
            this.lbDispenser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDispenser.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDispenser.ForeColor = System.Drawing.Color.Black;
            this.lbDispenser.Location = new System.Drawing.Point(3, 77);
            this.lbDispenser.Name = "lbDispenser";
            this.lbDispenser.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDispenser.Size = new System.Drawing.Size(220, 22);
            this.lbDispenser.TabIndex = 1881;
            this.lbDispenser.Text = "DISPENSER";
            this.lbDispenser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVision
            // 
            this.lbVision.BackColor = System.Drawing.Color.White;
            this.lbVision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVision.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVision.ForeColor = System.Drawing.Color.Black;
            this.lbVision.Location = new System.Drawing.Point(3, 101);
            this.lbVision.Name = "lbVision";
            this.lbVision.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbVision.Size = new System.Drawing.Size(220, 22);
            this.lbVision.TabIndex = 1883;
            this.lbVision.Text = "VISION";
            this.lbVision.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLotEnd
            // 
            this.btnLotEnd.BackColor = System.Drawing.Color.Silver;
            this.btnLotEnd.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLotEnd.ForeColor = System.Drawing.Color.Black;
            this.btnLotEnd.GlowColor = System.Drawing.Color.Silver;
            this.btnLotEnd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLotEnd.InnerBorderColor = System.Drawing.Color.Silver;
            this.btnLotEnd.Location = new System.Drawing.Point(1711, 935);
            this.btnLotEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLotEnd.Name = "btnLotEnd";
            this.btnLotEnd.OuterBorderColor = System.Drawing.Color.Silver;
            this.btnLotEnd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLotEnd.ShineColor = System.Drawing.Color.Silver;
            this.btnLotEnd.Size = new System.Drawing.Size(100, 69);
            this.btnLotEnd.TabIndex = 1880;
            this.btnLotEnd.TabStop = false;
            this.btnLotEnd.Tag = "3";
            this.btnLotEnd.Text = "LOT END";
            this.btnLotEnd.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Silver;
            this.btnStop.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.GlowColor = System.Drawing.Color.Silver;
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStop.InnerBorderColor = System.Drawing.Color.Silver;
            this.btnStop.Location = new System.Drawing.Point(1605, 935);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.OuterBorderColor = System.Drawing.Color.Silver;
            this.btnStop.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnStop.ShineColor = System.Drawing.Color.Silver;
            this.btnStop.Size = new System.Drawing.Size(100, 69);
            this.btnStop.TabIndex = 1879;
            this.btnStop.TabStop = false;
            this.btnStop.Tag = "2";
            this.btnStop.Text = "STOP";
            this.btnStop.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label20);
            this.panel7.Controls.Add(this.lbActuating3Mode);
            this.panel7.Controls.Add(this.lbTopVisionStep);
            this.panel7.Controls.Add(this.lbTopVisionMode);
            this.panel7.Controls.Add(this.lbIndexStep);
            this.panel7.Controls.Add(this.lbIndexMode);
            this.panel7.Controls.Add(this.lbCleanJigStep);
            this.panel7.Controls.Add(this.lbCleanJigMode);
            this.panel7.Controls.Add(this.lbUnloadPickerStep);
            this.panel7.Controls.Add(this.lbUnloadPickerMode);
            this.panel7.Controls.Add(this.lbUnloaderStep);
            this.panel7.Controls.Add(this.lbUnloaderMode);
            this.panel7.Controls.Add(this.lbPlateAngleStep);
            this.panel7.Controls.Add(this.lbPlateAngleMode);
            this.panel7.Controls.Add(this.lbCuring2Step);
            this.panel7.Controls.Add(this.lbCuring2Mode);
            this.panel7.Controls.Add(this.lbCuring1Step);
            this.panel7.Controls.Add(this.lbCuring1Mode);
            this.panel7.Controls.Add(this.lbVisionInspectStep);
            this.panel7.Controls.Add(this.lbVisionInspectMode);
            this.panel7.Controls.Add(this.lbBonder2Step);
            this.panel7.Controls.Add(this.lbBond2Mode);
            this.panel7.Controls.Add(this.lbBonder1Step);
            this.panel7.Controls.Add(this.lbBond1Mode);
            this.panel7.Controls.Add(this.lbLensHeightStep);
            this.panel7.Controls.Add(this.lbLensHeightMode);
            this.panel7.Controls.Add(this.lbJigPlateStep);
            this.panel7.Controls.Add(this.lbJigPlateMode);
            this.panel7.Controls.Add(this.lbLensPickerStep);
            this.panel7.Controls.Add(this.lbLensPickerMode);
            this.panel7.Controls.Add(this.lbMachineMode);
            this.panel7.Controls.Add(this.lbEQID);
            this.panel7.Controls.Add(this.label_EqpID);
            this.panel7.Controls.Add(this.lbLensLoaderStep);
            this.panel7.Controls.Add(this.lbLensLoaderMode);
            this.panel7.Controls.Add(this.lbVCMLoaderStep);
            this.panel7.Controls.Add(this.lbVCMPickerStep);
            this.panel7.Controls.Add(this.lbTactTimeLabel);
            this.panel7.Controls.Add(this.lbSequenceStatus);
            this.panel7.Controls.Add(this.lbCycleTime);
            this.panel7.Controls.Add(this.lbVCMLoaderMode);
            this.panel7.Controls.Add(this.lbVCMPickerMode);
            this.panel7.Controls.Add(this.lbLedTactTime);
            this.panel7.Location = new System.Drawing.Point(2, 12);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(228, 759);
            this.panel7.TabIndex = 1737;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Gainsboro;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(3, 638);
            this.label20.Name = "label20";
            this.label20.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.label20.Size = new System.Drawing.Size(95, 20);
            this.label20.TabIndex = 1874;
            this.label20.Text = "ACTUATING 3";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbActuating3Mode
            // 
            this.lbActuating3Mode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbActuating3Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbActuating3Mode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbActuating3Mode.ForeColor = System.Drawing.Color.Black;
            this.lbActuating3Mode.Location = new System.Drawing.Point(3, 638);
            this.lbActuating3Mode.Name = "lbActuating3Mode";
            this.lbActuating3Mode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbActuating3Mode.Size = new System.Drawing.Size(220, 32);
            this.lbActuating3Mode.TabIndex = 1873;
            this.lbActuating3Mode.Text = "STEP";
            this.lbActuating3Mode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbTopVisionStep
            // 
            this.lbTopVisionStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbTopVisionStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTopVisionStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTopVisionStep.ForeColor = System.Drawing.Color.Black;
            this.lbTopVisionStep.Location = new System.Drawing.Point(3, 605);
            this.lbTopVisionStep.Name = "lbTopVisionStep";
            this.lbTopVisionStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbTopVisionStep.Size = new System.Drawing.Size(95, 20);
            this.lbTopVisionStep.TabIndex = 1872;
            this.lbTopVisionStep.Text = "TOP VISION";
            this.lbTopVisionStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTopVisionMode
            // 
            this.lbTopVisionMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbTopVisionMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTopVisionMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTopVisionMode.ForeColor = System.Drawing.Color.Black;
            this.lbTopVisionMode.Location = new System.Drawing.Point(3, 605);
            this.lbTopVisionMode.Name = "lbTopVisionMode";
            this.lbTopVisionMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbTopVisionMode.Size = new System.Drawing.Size(220, 32);
            this.lbTopVisionMode.TabIndex = 1871;
            this.lbTopVisionMode.Text = "STEP";
            this.lbTopVisionMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbIndexStep
            // 
            this.lbIndexStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbIndexStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbIndexStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIndexStep.ForeColor = System.Drawing.Color.Black;
            this.lbIndexStep.Location = new System.Drawing.Point(3, 572);
            this.lbIndexStep.Name = "lbIndexStep";
            this.lbIndexStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbIndexStep.Size = new System.Drawing.Size(95, 20);
            this.lbIndexStep.TabIndex = 1870;
            this.lbIndexStep.Text = "INDEX";
            this.lbIndexStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbIndexMode
            // 
            this.lbIndexMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbIndexMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbIndexMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIndexMode.ForeColor = System.Drawing.Color.Black;
            this.lbIndexMode.Location = new System.Drawing.Point(3, 572);
            this.lbIndexMode.Name = "lbIndexMode";
            this.lbIndexMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbIndexMode.Size = new System.Drawing.Size(220, 32);
            this.lbIndexMode.TabIndex = 1869;
            this.lbIndexMode.Text = "STEP";
            this.lbIndexMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbIndexMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbCleanJigStep
            // 
            this.lbCleanJigStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbCleanJigStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCleanJigStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCleanJigStep.ForeColor = System.Drawing.Color.Black;
            this.lbCleanJigStep.Location = new System.Drawing.Point(3, 539);
            this.lbCleanJigStep.Name = "lbCleanJigStep";
            this.lbCleanJigStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbCleanJigStep.Size = new System.Drawing.Size(95, 20);
            this.lbCleanJigStep.TabIndex = 1868;
            this.lbCleanJigStep.Text = "CLEAN JIG";
            this.lbCleanJigStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCleanJigMode
            // 
            this.lbCleanJigMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbCleanJigMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCleanJigMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCleanJigMode.ForeColor = System.Drawing.Color.Black;
            this.lbCleanJigMode.Location = new System.Drawing.Point(3, 539);
            this.lbCleanJigMode.Name = "lbCleanJigMode";
            this.lbCleanJigMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbCleanJigMode.Size = new System.Drawing.Size(220, 32);
            this.lbCleanJigMode.TabIndex = 1867;
            this.lbCleanJigMode.Text = "STEP";
            this.lbCleanJigMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbCleanJigMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbUnloadPickerStep
            // 
            this.lbUnloadPickerStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbUnloadPickerStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloadPickerStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloadPickerStep.ForeColor = System.Drawing.Color.Black;
            this.lbUnloadPickerStep.Location = new System.Drawing.Point(3, 506);
            this.lbUnloadPickerStep.Name = "lbUnloadPickerStep";
            this.lbUnloadPickerStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbUnloadPickerStep.Size = new System.Drawing.Size(95, 20);
            this.lbUnloadPickerStep.TabIndex = 1866;
            this.lbUnloadPickerStep.Text = "UNLOAD PICKER";
            this.lbUnloadPickerStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUnloadPickerMode
            // 
            this.lbUnloadPickerMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbUnloadPickerMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloadPickerMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloadPickerMode.ForeColor = System.Drawing.Color.Black;
            this.lbUnloadPickerMode.Location = new System.Drawing.Point(3, 506);
            this.lbUnloadPickerMode.Name = "lbUnloadPickerMode";
            this.lbUnloadPickerMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbUnloadPickerMode.Size = new System.Drawing.Size(220, 32);
            this.lbUnloadPickerMode.TabIndex = 1865;
            this.lbUnloadPickerMode.Text = "STEP";
            this.lbUnloadPickerMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbUnloadPickerMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbUnloaderStep
            // 
            this.lbUnloaderStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbUnloaderStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloaderStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderStep.ForeColor = System.Drawing.Color.Black;
            this.lbUnloaderStep.Location = new System.Drawing.Point(3, 473);
            this.lbUnloaderStep.Name = "lbUnloaderStep";
            this.lbUnloaderStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbUnloaderStep.Size = new System.Drawing.Size(95, 20);
            this.lbUnloaderStep.TabIndex = 1864;
            this.lbUnloaderStep.Text = "UNLOADER";
            this.lbUnloaderStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUnloaderMode
            // 
            this.lbUnloaderMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbUnloaderMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUnloaderMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnloaderMode.ForeColor = System.Drawing.Color.Black;
            this.lbUnloaderMode.Location = new System.Drawing.Point(3, 473);
            this.lbUnloaderMode.Name = "lbUnloaderMode";
            this.lbUnloaderMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbUnloaderMode.Size = new System.Drawing.Size(220, 32);
            this.lbUnloaderMode.TabIndex = 1863;
            this.lbUnloaderMode.Text = "STEP";
            this.lbUnloaderMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbUnloaderMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbPlateAngleStep
            // 
            this.lbPlateAngleStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbPlateAngleStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPlateAngleStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlateAngleStep.ForeColor = System.Drawing.Color.Black;
            this.lbPlateAngleStep.Location = new System.Drawing.Point(3, 440);
            this.lbPlateAngleStep.Name = "lbPlateAngleStep";
            this.lbPlateAngleStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbPlateAngleStep.Size = new System.Drawing.Size(95, 20);
            this.lbPlateAngleStep.TabIndex = 1862;
            this.lbPlateAngleStep.Text = "SIDE PLATE ANGLE";
            this.lbPlateAngleStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPlateAngleMode
            // 
            this.lbPlateAngleMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbPlateAngleMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPlateAngleMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlateAngleMode.ForeColor = System.Drawing.Color.Black;
            this.lbPlateAngleMode.Location = new System.Drawing.Point(3, 440);
            this.lbPlateAngleMode.Name = "lbPlateAngleMode";
            this.lbPlateAngleMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbPlateAngleMode.Size = new System.Drawing.Size(220, 32);
            this.lbPlateAngleMode.TabIndex = 1861;
            this.lbPlateAngleMode.Text = "STEP";
            this.lbPlateAngleMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbPlateAngleMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbCuring2Step
            // 
            this.lbCuring2Step.BackColor = System.Drawing.Color.Gainsboro;
            this.lbCuring2Step.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCuring2Step.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCuring2Step.ForeColor = System.Drawing.Color.Black;
            this.lbCuring2Step.Location = new System.Drawing.Point(3, 407);
            this.lbCuring2Step.Name = "lbCuring2Step";
            this.lbCuring2Step.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbCuring2Step.Size = new System.Drawing.Size(95, 20);
            this.lbCuring2Step.TabIndex = 1860;
            this.lbCuring2Step.Text = "CURING 2";
            this.lbCuring2Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCuring2Mode
            // 
            this.lbCuring2Mode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbCuring2Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCuring2Mode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCuring2Mode.ForeColor = System.Drawing.Color.Black;
            this.lbCuring2Mode.Location = new System.Drawing.Point(3, 407);
            this.lbCuring2Mode.Name = "lbCuring2Mode";
            this.lbCuring2Mode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbCuring2Mode.Size = new System.Drawing.Size(220, 32);
            this.lbCuring2Mode.TabIndex = 1859;
            this.lbCuring2Mode.Text = "STEP";
            this.lbCuring2Mode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbCuring2Mode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbCuring1Step
            // 
            this.lbCuring1Step.BackColor = System.Drawing.Color.Gainsboro;
            this.lbCuring1Step.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCuring1Step.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCuring1Step.ForeColor = System.Drawing.Color.Black;
            this.lbCuring1Step.Location = new System.Drawing.Point(3, 374);
            this.lbCuring1Step.Name = "lbCuring1Step";
            this.lbCuring1Step.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbCuring1Step.Size = new System.Drawing.Size(95, 20);
            this.lbCuring1Step.TabIndex = 1858;
            this.lbCuring1Step.Text = "CURING 1";
            this.lbCuring1Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCuring1Mode
            // 
            this.lbCuring1Mode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbCuring1Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCuring1Mode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCuring1Mode.ForeColor = System.Drawing.Color.Black;
            this.lbCuring1Mode.Location = new System.Drawing.Point(3, 374);
            this.lbCuring1Mode.Name = "lbCuring1Mode";
            this.lbCuring1Mode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbCuring1Mode.Size = new System.Drawing.Size(220, 32);
            this.lbCuring1Mode.TabIndex = 1857;
            this.lbCuring1Mode.Text = "STEP";
            this.lbCuring1Mode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbCuring1Mode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbVisionInspectStep
            // 
            this.lbVisionInspectStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbVisionInspectStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVisionInspectStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVisionInspectStep.ForeColor = System.Drawing.Color.Black;
            this.lbVisionInspectStep.Location = new System.Drawing.Point(3, 341);
            this.lbVisionInspectStep.Name = "lbVisionInspectStep";
            this.lbVisionInspectStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbVisionInspectStep.Size = new System.Drawing.Size(95, 20);
            this.lbVisionInspectStep.TabIndex = 1856;
            this.lbVisionInspectStep.Text = "VISION INSPECT";
            this.lbVisionInspectStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVisionInspectMode
            // 
            this.lbVisionInspectMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbVisionInspectMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVisionInspectMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVisionInspectMode.ForeColor = System.Drawing.Color.Black;
            this.lbVisionInspectMode.Location = new System.Drawing.Point(3, 341);
            this.lbVisionInspectMode.Name = "lbVisionInspectMode";
            this.lbVisionInspectMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbVisionInspectMode.Size = new System.Drawing.Size(220, 32);
            this.lbVisionInspectMode.TabIndex = 1855;
            this.lbVisionInspectMode.Text = "STEP";
            this.lbVisionInspectMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbVisionInspectMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbBonder2Step
            // 
            this.lbBonder2Step.BackColor = System.Drawing.Color.Gainsboro;
            this.lbBonder2Step.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBonder2Step.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBonder2Step.ForeColor = System.Drawing.Color.Black;
            this.lbBonder2Step.Location = new System.Drawing.Point(3, 308);
            this.lbBonder2Step.Name = "lbBonder2Step";
            this.lbBonder2Step.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbBonder2Step.Size = new System.Drawing.Size(95, 20);
            this.lbBonder2Step.TabIndex = 1854;
            this.lbBonder2Step.Text = "BONDER 2";
            this.lbBonder2Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbBond2Mode
            // 
            this.lbBond2Mode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbBond2Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBond2Mode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBond2Mode.ForeColor = System.Drawing.Color.Black;
            this.lbBond2Mode.Location = new System.Drawing.Point(3, 308);
            this.lbBond2Mode.Name = "lbBond2Mode";
            this.lbBond2Mode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbBond2Mode.Size = new System.Drawing.Size(220, 32);
            this.lbBond2Mode.TabIndex = 1853;
            this.lbBond2Mode.Text = "STEP";
            this.lbBond2Mode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbBond2Mode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbBonder1Step
            // 
            this.lbBonder1Step.BackColor = System.Drawing.Color.Gainsboro;
            this.lbBonder1Step.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBonder1Step.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBonder1Step.ForeColor = System.Drawing.Color.Black;
            this.lbBonder1Step.Location = new System.Drawing.Point(3, 275);
            this.lbBonder1Step.Name = "lbBonder1Step";
            this.lbBonder1Step.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbBonder1Step.Size = new System.Drawing.Size(95, 20);
            this.lbBonder1Step.TabIndex = 1852;
            this.lbBonder1Step.Text = "BONDER 1";
            this.lbBonder1Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbBond1Mode
            // 
            this.lbBond1Mode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbBond1Mode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBond1Mode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBond1Mode.ForeColor = System.Drawing.Color.Black;
            this.lbBond1Mode.Location = new System.Drawing.Point(3, 275);
            this.lbBond1Mode.Name = "lbBond1Mode";
            this.lbBond1Mode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbBond1Mode.Size = new System.Drawing.Size(220, 32);
            this.lbBond1Mode.TabIndex = 1851;
            this.lbBond1Mode.Text = "STEP";
            this.lbBond1Mode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbBond1Mode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbLensHeightStep
            // 
            this.lbLensHeightStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensHeightStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensHeightStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensHeightStep.ForeColor = System.Drawing.Color.Black;
            this.lbLensHeightStep.Location = new System.Drawing.Point(3, 242);
            this.lbLensHeightStep.Name = "lbLensHeightStep";
            this.lbLensHeightStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbLensHeightStep.Size = new System.Drawing.Size(95, 20);
            this.lbLensHeightStep.TabIndex = 1850;
            this.lbLensHeightStep.Text = "LENS HEIGHT";
            this.lbLensHeightStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensHeightMode
            // 
            this.lbLensHeightMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbLensHeightMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensHeightMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensHeightMode.ForeColor = System.Drawing.Color.Black;
            this.lbLensHeightMode.Location = new System.Drawing.Point(3, 242);
            this.lbLensHeightMode.Name = "lbLensHeightMode";
            this.lbLensHeightMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbLensHeightMode.Size = new System.Drawing.Size(220, 32);
            this.lbLensHeightMode.TabIndex = 1849;
            this.lbLensHeightMode.Text = "STEP";
            this.lbLensHeightMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbLensHeightMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbJigPlateStep
            // 
            this.lbJigPlateStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbJigPlateStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbJigPlateStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbJigPlateStep.ForeColor = System.Drawing.Color.Black;
            this.lbJigPlateStep.Location = new System.Drawing.Point(3, 209);
            this.lbJigPlateStep.Name = "lbJigPlateStep";
            this.lbJigPlateStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbJigPlateStep.Size = new System.Drawing.Size(95, 20);
            this.lbJigPlateStep.TabIndex = 1848;
            this.lbJigPlateStep.Text = "JIG FLATNESS";
            this.lbJigPlateStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbJigPlateMode
            // 
            this.lbJigPlateMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbJigPlateMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbJigPlateMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbJigPlateMode.ForeColor = System.Drawing.Color.Black;
            this.lbJigPlateMode.Location = new System.Drawing.Point(3, 209);
            this.lbJigPlateMode.Name = "lbJigPlateMode";
            this.lbJigPlateMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbJigPlateMode.Size = new System.Drawing.Size(220, 32);
            this.lbJigPlateMode.TabIndex = 1847;
            this.lbJigPlateMode.Text = "STEP";
            this.lbJigPlateMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbJigPlateMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbLensPickerStep
            // 
            this.lbLensPickerStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensPickerStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensPickerStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensPickerStep.ForeColor = System.Drawing.Color.Black;
            this.lbLensPickerStep.Location = new System.Drawing.Point(3, 176);
            this.lbLensPickerStep.Name = "lbLensPickerStep";
            this.lbLensPickerStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbLensPickerStep.Size = new System.Drawing.Size(95, 20);
            this.lbLensPickerStep.TabIndex = 1846;
            this.lbLensPickerStep.Text = "LENS PICKER";
            this.lbLensPickerStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensPickerMode
            // 
            this.lbLensPickerMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbLensPickerMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensPickerMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensPickerMode.ForeColor = System.Drawing.Color.Black;
            this.lbLensPickerMode.Location = new System.Drawing.Point(3, 176);
            this.lbLensPickerMode.Name = "lbLensPickerMode";
            this.lbLensPickerMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbLensPickerMode.Size = new System.Drawing.Size(220, 32);
            this.lbLensPickerMode.TabIndex = 1845;
            this.lbLensPickerMode.Text = "STEP";
            this.lbLensPickerMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbLensPickerMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbLensLoaderStep
            // 
            this.lbLensLoaderStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lbLensLoaderStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensLoaderStep.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensLoaderStep.ForeColor = System.Drawing.Color.Black;
            this.lbLensLoaderStep.Location = new System.Drawing.Point(3, 143);
            this.lbLensLoaderStep.Name = "lbLensLoaderStep";
            this.lbLensLoaderStep.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lbLensLoaderStep.Size = new System.Drawing.Size(95, 20);
            this.lbLensLoaderStep.TabIndex = 1844;
            this.lbLensLoaderStep.Text = "LENS LOADER";
            this.lbLensLoaderStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLensLoaderMode
            // 
            this.lbLensLoaderMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbLensLoaderMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLensLoaderMode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLensLoaderMode.ForeColor = System.Drawing.Color.Black;
            this.lbLensLoaderMode.Location = new System.Drawing.Point(3, 143);
            this.lbLensLoaderMode.Name = "lbLensLoaderMode";
            this.lbLensLoaderMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbLensLoaderMode.Size = new System.Drawing.Size(220, 32);
            this.lbLensLoaderMode.TabIndex = 1843;
            this.lbLensLoaderMode.Text = "STEP";
            this.lbLensLoaderMode.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbLensLoaderMode.Click += new System.EventHandler(this.lbVCMLoaderMode_Click);
            // 
            // lbSequenceStatus
            // 
            this.lbSequenceStatus.BackColor = System.Drawing.Color.Gainsboro;
            this.lbSequenceStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSequenceStatus.ForeColor = System.Drawing.Color.Navy;
            this.lbSequenceStatus.Location = new System.Drawing.Point(0, 52);
            this.lbSequenceStatus.Name = "lbSequenceStatus";
            this.lbSequenceStatus.Size = new System.Drawing.Size(225, 23);
            this.lbSequenceStatus.TabIndex = 1832;
            this.lbSequenceStatus.Tag = "";
            this.lbSequenceStatus.Text = "SQUENCE STATUS";
            this.lbSequenceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCycleTime
            // 
            this.lbCycleTime.BackColor = System.Drawing.Color.Gainsboro;
            this.lbCycleTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCycleTime.ForeColor = System.Drawing.Color.Navy;
            this.lbCycleTime.Location = new System.Drawing.Point(2, 692);
            this.lbCycleTime.Name = "lbCycleTime";
            this.lbCycleTime.Size = new System.Drawing.Size(225, 24);
            this.lbCycleTime.TabIndex = 1834;
            this.lbCycleTime.Tag = "";
            this.lbCycleTime.Text = "CYCLE TIME";
            this.lbCycleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Silver;
            this.btnStart.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.Blue;
            this.btnStart.GlowColor = System.Drawing.Color.Silver;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStart.InnerBorderColor = System.Drawing.Color.Silver;
            this.btnStart.Location = new System.Drawing.Point(1499, 935);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.OuterBorderColor = System.Drawing.Color.Silver;
            this.btnStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnStart.ShineColor = System.Drawing.Color.Silver;
            this.btnStart.Size = new System.Drawing.Size(100, 69);
            this.btnStart.TabIndex = 1878;
            this.btnStart.TabStop = false;
            this.btnStart.Tag = "1";
            this.btnStart.Text = "START";
            this.btnStart.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnEnable
            // 
            this.btnEnable.BackColor = System.Drawing.Color.Silver;
            this.btnEnable.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnable.ForeColor = System.Drawing.Color.Black;
            this.btnEnable.GlowColor = System.Drawing.Color.Silver;
            this.btnEnable.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnable.InnerBorderColor = System.Drawing.Color.Silver;
            this.btnEnable.Location = new System.Drawing.Point(1393, 935);
            this.btnEnable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.OuterBorderColor = System.Drawing.Color.Silver;
            this.btnEnable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEnable.ShineColor = System.Drawing.Color.Silver;
            this.btnEnable.Size = new System.Drawing.Size(100, 69);
            this.btnEnable.TabIndex = 1877;
            this.btnEnable.TabStop = false;
            this.btnEnable.Tag = "0";
            this.btnEnable.Text = "INITIAL";
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.lblmeseqpid);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.lblProdType);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lblOperation);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.lblDevice);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.gbUnloaderMagazine);
            this.panel1.Controls.Add(this.gbLensMagazine);
            this.panel1.Controls.Add(this.gbVCMMagazine);
            this.panel1.Controls.Add(this.lbMagazineControl);
            this.panel1.Location = new System.Drawing.Point(234, 827);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1153, 177);
            this.panel1.TabIndex = 1841;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Gainsboro;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(914, 98);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(95, 20);
            this.label17.TabIndex = 1895;
            this.label17.Text = "EQP ID";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblmeseqpid
            // 
            this.lblmeseqpid.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblmeseqpid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblmeseqpid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmeseqpid.ForeColor = System.Drawing.Color.Black;
            this.lblmeseqpid.Location = new System.Drawing.Point(914, 98);
            this.lblmeseqpid.Name = "lblmeseqpid";
            this.lblmeseqpid.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblmeseqpid.Size = new System.Drawing.Size(235, 28);
            this.lblmeseqpid.TabIndex = 1894;
            this.lblmeseqpid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Gainsboro;
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(913, 130);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(95, 20);
            this.label21.TabIndex = 1893;
            this.label21.Text = "Prod Type";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProdType
            // 
            this.lblProdType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblProdType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProdType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdType.ForeColor = System.Drawing.Color.Black;
            this.lblProdType.Location = new System.Drawing.Point(913, 130);
            this.lblProdType.Name = "lblProdType";
            this.lblProdType.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblProdType.Size = new System.Drawing.Size(235, 28);
            this.lblProdType.TabIndex = 1892;
            this.lblProdType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Gainsboro;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(913, 63);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 20);
            this.label19.TabIndex = 1891;
            this.label19.Text = "Operation";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOperation
            // 
            this.lblOperation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblOperation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOperation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperation.ForeColor = System.Drawing.Color.Black;
            this.lblOperation.Location = new System.Drawing.Point(913, 63);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblOperation.Size = new System.Drawing.Size(235, 28);
            this.lblOperation.TabIndex = 1890;
            this.lblOperation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Gainsboro;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(913, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 20);
            this.label14.TabIndex = 1889;
            this.label14.Text = "Device";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDevice
            // 
            this.lblDevice.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDevice.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevice.ForeColor = System.Drawing.Color.Black;
            this.lblDevice.Location = new System.Drawing.Point(913, 30);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblDevice.Size = new System.Drawing.Size(235, 28);
            this.lblDevice.TabIndex = 1888;
            this.lblDevice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Gainsboro;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Navy;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(910, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(242, 23);
            this.label11.TabIndex = 1887;
            this.label11.Text = "MACHINO INFORMATION";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnNGTrayReset);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(697, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 128);
            this.groupBox1.TabIndex = 1886;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NG TRAY";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 38);
            this.label1.TabIndex = 1704;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNGTrayReset
            // 
            this.btnNGTrayReset.BackColor = System.Drawing.Color.Silver;
            this.btnNGTrayReset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNGTrayReset.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnNGTrayReset.GlowColor = System.Drawing.Color.Transparent;
            this.btnNGTrayReset.InnerBorderColor = System.Drawing.Color.White;
            this.btnNGTrayReset.Location = new System.Drawing.Point(28, 31);
            this.btnNGTrayReset.Name = "btnNGTrayReset";
            this.btnNGTrayReset.OuterBorderColor = System.Drawing.Color.Black;
            this.btnNGTrayReset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNGTrayReset.ShineColor = System.Drawing.Color.Silver;
            this.btnNGTrayReset.Size = new System.Drawing.Size(130, 35);
            this.btnNGTrayReset.TabIndex = 1702;
            this.btnNGTrayReset.TabStop = false;
            this.btnNGTrayReset.Tag = "0";
            this.btnNGTrayReset.Text = "RESET";
            this.btnNGTrayReset.Click += new System.EventHandler(this.btnNGTrayReset_Click);
            // 
            // gbUnloaderMagazine
            // 
            this.gbUnloaderMagazine.Controls.Add(this.btnUnloadMagazineFull);
            this.gbUnloaderMagazine.Controls.Add(this.label18);
            this.gbUnloaderMagazine.Controls.Add(this.btnUnloader_Magazine);
            this.gbUnloaderMagazine.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbUnloaderMagazine.Location = new System.Drawing.Point(474, 33);
            this.gbUnloaderMagazine.Name = "gbUnloaderMagazine";
            this.gbUnloaderMagazine.Size = new System.Drawing.Size(207, 128);
            this.gbUnloaderMagazine.TabIndex = 1885;
            this.gbUnloaderMagazine.TabStop = false;
            this.gbUnloaderMagazine.Text = "UNLOADER MAGAZINE";
            // 
            // btnUnloadMagazineFull
            // 
            this.btnUnloadMagazineFull.BackColor = System.Drawing.Color.Silver;
            this.btnUnloadMagazineFull.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnloadMagazineFull.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnUnloadMagazineFull.GlowColor = System.Drawing.Color.Transparent;
            this.btnUnloadMagazineFull.InnerBorderColor = System.Drawing.Color.White;
            this.btnUnloadMagazineFull.Location = new System.Drawing.Point(47, 74);
            this.btnUnloadMagazineFull.Name = "btnUnloadMagazineFull";
            this.btnUnloadMagazineFull.OuterBorderColor = System.Drawing.Color.Black;
            this.btnUnloadMagazineFull.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUnloadMagazineFull.ShineColor = System.Drawing.Color.Silver;
            this.btnUnloadMagazineFull.Size = new System.Drawing.Size(130, 35);
            this.btnUnloadMagazineFull.TabIndex = 1705;
            this.btnUnloadMagazineFull.TabStop = false;
            this.btnUnloadMagazineFull.Tag = "0";
            this.btnUnloadMagazineFull.Text = "FULL";
            this.btnUnloadMagazineFull.Click += new System.EventHandler(this.btnUnloadMagazineFull_Click);
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label18.Location = new System.Drawing.Point(29, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(15, 38);
            this.label18.TabIndex = 1704;
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUnloader_Magazine
            // 
            this.btnUnloader_Magazine.BackColor = System.Drawing.Color.Silver;
            this.btnUnloader_Magazine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnloader_Magazine.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnUnloader_Magazine.GlowColor = System.Drawing.Color.Transparent;
            this.btnUnloader_Magazine.InnerBorderColor = System.Drawing.Color.White;
            this.btnUnloader_Magazine.Location = new System.Drawing.Point(47, 32);
            this.btnUnloader_Magazine.Name = "btnUnloader_Magazine";
            this.btnUnloader_Magazine.OuterBorderColor = System.Drawing.Color.Black;
            this.btnUnloader_Magazine.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUnloader_Magazine.ShineColor = System.Drawing.Color.Silver;
            this.btnUnloader_Magazine.Size = new System.Drawing.Size(130, 35);
            this.btnUnloader_Magazine.TabIndex = 1702;
            this.btnUnloader_Magazine.TabStop = false;
            this.btnUnloader_Magazine.Tag = "0";
            this.btnUnloader_Magazine.Text = "INPUT";
            this.btnUnloader_Magazine.Click += new System.EventHandler(this.btnUnloader_Magazine_Click);
            // 
            // gbLensMagazine
            // 
            this.gbLensMagazine.Controls.Add(this.btnLensMagazineFull);
            this.gbLensMagazine.Controls.Add(this.label16);
            this.gbLensMagazine.Controls.Add(this.btnLens_Magazine);
            this.gbLensMagazine.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLensMagazine.Location = new System.Drawing.Point(251, 33);
            this.gbLensMagazine.Name = "gbLensMagazine";
            this.gbLensMagazine.Size = new System.Drawing.Size(207, 128);
            this.gbLensMagazine.TabIndex = 1884;
            this.gbLensMagazine.TabStop = false;
            this.gbLensMagazine.Text = "LENS MAGAZINE";
            // 
            // btnLensMagazineFull
            // 
            this.btnLensMagazineFull.BackColor = System.Drawing.Color.Silver;
            this.btnLensMagazineFull.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLensMagazineFull.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnLensMagazineFull.GlowColor = System.Drawing.Color.Transparent;
            this.btnLensMagazineFull.InnerBorderColor = System.Drawing.Color.White;
            this.btnLensMagazineFull.Location = new System.Drawing.Point(47, 74);
            this.btnLensMagazineFull.Name = "btnLensMagazineFull";
            this.btnLensMagazineFull.OuterBorderColor = System.Drawing.Color.Black;
            this.btnLensMagazineFull.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLensMagazineFull.ShineColor = System.Drawing.Color.Silver;
            this.btnLensMagazineFull.Size = new System.Drawing.Size(130, 35);
            this.btnLensMagazineFull.TabIndex = 1705;
            this.btnLensMagazineFull.TabStop = false;
            this.btnLensMagazineFull.Tag = "0";
            this.btnLensMagazineFull.Text = "FULL";
            this.btnLensMagazineFull.Click += new System.EventHandler(this.btnLensMagazineFull_Click);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label16.Location = new System.Drawing.Point(29, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 38);
            this.label16.TabIndex = 1704;
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLens_Magazine
            // 
            this.btnLens_Magazine.BackColor = System.Drawing.Color.Silver;
            this.btnLens_Magazine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLens_Magazine.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnLens_Magazine.GlowColor = System.Drawing.Color.Transparent;
            this.btnLens_Magazine.InnerBorderColor = System.Drawing.Color.White;
            this.btnLens_Magazine.Location = new System.Drawing.Point(47, 32);
            this.btnLens_Magazine.Name = "btnLens_Magazine";
            this.btnLens_Magazine.OuterBorderColor = System.Drawing.Color.Black;
            this.btnLens_Magazine.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnLens_Magazine.ShineColor = System.Drawing.Color.Silver;
            this.btnLens_Magazine.Size = new System.Drawing.Size(130, 35);
            this.btnLens_Magazine.TabIndex = 1702;
            this.btnLens_Magazine.TabStop = false;
            this.btnLens_Magazine.Tag = "0";
            this.btnLens_Magazine.Text = "INPUT";
            this.btnLens_Magazine.Click += new System.EventHandler(this.btnLens_Magazine_Click);
            // 
            // gbVCMMagazine
            // 
            this.gbVCMMagazine.Controls.Add(this.btnVCMMagazineFull);
            this.gbVCMMagazine.Controls.Add(this.lbFluxFeeder);
            this.gbVCMMagazine.Controls.Add(this.btnVCM_Magazine);
            this.gbVCMMagazine.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbVCMMagazine.Location = new System.Drawing.Point(28, 33);
            this.gbVCMMagazine.Name = "gbVCMMagazine";
            this.gbVCMMagazine.Size = new System.Drawing.Size(207, 128);
            this.gbVCMMagazine.TabIndex = 1883;
            this.gbVCMMagazine.TabStop = false;
            this.gbVCMMagazine.Text = "VCM MAGAZINE";
            // 
            // btnVCMMagazineFull
            // 
            this.btnVCMMagazineFull.BackColor = System.Drawing.Color.Silver;
            this.btnVCMMagazineFull.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVCMMagazineFull.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnVCMMagazineFull.GlowColor = System.Drawing.Color.Transparent;
            this.btnVCMMagazineFull.InnerBorderColor = System.Drawing.Color.White;
            this.btnVCMMagazineFull.Location = new System.Drawing.Point(47, 78);
            this.btnVCMMagazineFull.Name = "btnVCMMagazineFull";
            this.btnVCMMagazineFull.OuterBorderColor = System.Drawing.Color.Black;
            this.btnVCMMagazineFull.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnVCMMagazineFull.ShineColor = System.Drawing.Color.Silver;
            this.btnVCMMagazineFull.Size = new System.Drawing.Size(130, 35);
            this.btnVCMMagazineFull.TabIndex = 1705;
            this.btnVCMMagazineFull.TabStop = false;
            this.btnVCMMagazineFull.Tag = "0";
            this.btnVCMMagazineFull.Text = "FULL";
            this.btnVCMMagazineFull.Click += new System.EventHandler(this.btnVCMMagazineFull_Click);
            // 
            // lbFluxFeeder
            // 
            this.lbFluxFeeder.BackColor = System.Drawing.Color.White;
            this.lbFluxFeeder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbFluxFeeder.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbFluxFeeder.Location = new System.Drawing.Point(29, 31);
            this.lbFluxFeeder.Name = "lbFluxFeeder";
            this.lbFluxFeeder.Size = new System.Drawing.Size(15, 38);
            this.lbFluxFeeder.TabIndex = 1704;
            this.lbFluxFeeder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnVCM_Magazine
            // 
            this.btnVCM_Magazine.BackColor = System.Drawing.Color.Silver;
            this.btnVCM_Magazine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVCM_Magazine.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnVCM_Magazine.GlowColor = System.Drawing.Color.Transparent;
            this.btnVCM_Magazine.InnerBorderColor = System.Drawing.Color.White;
            this.btnVCM_Magazine.Location = new System.Drawing.Point(47, 32);
            this.btnVCM_Magazine.Name = "btnVCM_Magazine";
            this.btnVCM_Magazine.OuterBorderColor = System.Drawing.Color.Black;
            this.btnVCM_Magazine.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnVCM_Magazine.ShineColor = System.Drawing.Color.Silver;
            this.btnVCM_Magazine.Size = new System.Drawing.Size(130, 35);
            this.btnVCM_Magazine.TabIndex = 1702;
            this.btnVCM_Magazine.TabStop = false;
            this.btnVCM_Magazine.Tag = "0";
            this.btnVCM_Magazine.Text = "INPUT";
            this.btnVCM_Magazine.Click += new System.EventHandler(this.btnVCM_Magazine_Click);
            // 
            // lbMagazineControl
            // 
            this.lbMagazineControl.BackColor = System.Drawing.Color.Gainsboro;
            this.lbMagazineControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMagazineControl.ForeColor = System.Drawing.Color.Navy;
            this.lbMagazineControl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbMagazineControl.Location = new System.Drawing.Point(-1, 0);
            this.lbMagazineControl.Name = "lbMagazineControl";
            this.lbMagazineControl.Size = new System.Drawing.Size(905, 23);
            this.lbMagazineControl.TabIndex = 1443;
            this.lbMagazineControl.Text = "MAGAZINE CONTROL";
            this.lbMagazineControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // FrmPageOperation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1000);
            this.Controls.Add(this.a1Panel11);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPageOperation";
            this.Text = "FormMenuOperation";
            this.Load += new System.EventHandler(this.FormPageOperation_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmPageOperation_VisibleChanged);
            this.a1Panel11.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnMainIndex.ResumeLayout(false);
            this.pnMainIndex.PerformLayout();
            this.pnBond2.ResumeLayout(false);
            this.pnBonder1.ResumeLayout(false);
            this.pnUnloadTrayChange.ResumeLayout(false);
            this.pnVCMTrayChange.ResumeLayout(false);
            this.pnLensTrayChange.ResumeLayout(false);
            this.gbStatus.ResumeLayout(false);
            this.gbStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMainIndex)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Yield_Chart)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SideAngle_Chart)).EndInit();
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LensHeight_Chart)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picNGMap)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picUnloadMap)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLensMap)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picVCMMap)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbUnloaderMagazine.ResumeLayout(false);
            this.gbLensMagazine.ResumeLayout(false);
            this.gbVCMMagazine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ImageList ImgLstState;
        private System.Windows.Forms.Label lbLedTactTime;
        private System.Windows.Forms.Label lbEQID;
        private System.Windows.Forms.Label label_EqpID;
        private System.Windows.Forms.Label lbMachineMode;
        private System.Windows.Forms.Label lbVCMLoaderMode;
        private System.Windows.Forms.Label lbVCMPickerMode;
        private System.Windows.Forms.Label lbVCMLoaderStep;
        private System.Windows.Forms.Label lbVCMPickerStep;
        private System.Windows.Forms.Label lbTactTimeLabel;
        private Owf.Controls.A1Panel a1Panel11;
        private System.Windows.Forms.Label lbCycleTime;
        private System.Windows.Forms.Label lbSequenceStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbMagazineControl;
        private System.Windows.Forms.Label lbLensLoaderStep;
        private System.Windows.Forms.Label lbLensLoaderMode;
        private System.Windows.Forms.Panel panel7;
        private Glass.GlassButton btnLotEnd;
        private Glass.GlassButton btnStop;
        private Glass.GlassButton btnStart;
        private Glass.GlassButton btnEnable;
        private System.Windows.Forms.Label lbUVLamp;
        private System.Windows.Forms.Label lbACTUATOR;
        private System.Windows.Forms.Label lbMC;
        private System.Windows.Forms.Label lbVision;
        private System.Windows.Forms.Label lbMainAir;
        private System.Windows.Forms.Label lbDispenser;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lbInterface;
        private System.Windows.Forms.Label lbDSensor;
        private Glass.GlassButton btnTaskLog;
        private System.Windows.Forms.Label lbIndexStep;
        private System.Windows.Forms.Label lbIndexMode;
        private System.Windows.Forms.Label lbCleanJigStep;
        private System.Windows.Forms.Label lbCleanJigMode;
        private System.Windows.Forms.Label lbUnloadPickerStep;
        private System.Windows.Forms.Label lbUnloadPickerMode;
        private System.Windows.Forms.Label lbUnloaderStep;
        private System.Windows.Forms.Label lbUnloaderMode;
        private System.Windows.Forms.Label lbPlateAngleStep;
        private System.Windows.Forms.Label lbPlateAngleMode;
        private System.Windows.Forms.Label lbCuring2Step;
        private System.Windows.Forms.Label lbCuring2Mode;
        private System.Windows.Forms.Label lbCuring1Step;
        private System.Windows.Forms.Label lbCuring1Mode;
        private System.Windows.Forms.Label lbVisionInspectStep;
        private System.Windows.Forms.Label lbVisionInspectMode;
        private System.Windows.Forms.Label lbBonder2Step;
        private System.Windows.Forms.Label lbBond2Mode;
        private System.Windows.Forms.Label lbBonder1Step;
        private System.Windows.Forms.Label lbBond1Mode;
        private System.Windows.Forms.Label lbLensHeightStep;
        private System.Windows.Forms.Label lbLensHeightMode;
        private System.Windows.Forms.Label lbJigPlateStep;
        private System.Windows.Forms.Label lbJigPlateMode;
        private System.Windows.Forms.Label lbLensPickerStep;
        private System.Windows.Forms.Label lbLensPickerMode;
        private System.Windows.Forms.GroupBox gbUnloaderMagazine;
        private System.Windows.Forms.Label label18;
        private Glass.GlassButton btnUnloader_Magazine;
        private System.Windows.Forms.GroupBox gbLensMagazine;
        private System.Windows.Forms.Label label16;
        private Glass.GlassButton btnLens_Magazine;
        private System.Windows.Forms.GroupBox gbVCMMagazine;
        private System.Windows.Forms.Label lbFluxFeeder;
        private Glass.GlassButton btnVCM_Magazine;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox picUnloadMap;
        private System.Windows.Forms.Label lbUnloaderTrayInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picLensMap;
        private System.Windows.Forms.Label lbLensTrayInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbVCMTrayInfo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox picNGMap;
        private System.Windows.Forms.Label lbNGTrayInfo;
        private System.Windows.Forms.PictureBox picVCMMap;
        private System.Windows.Forms.Label lbUnloaderSlotNoTitle;
        private System.Windows.Forms.Label lbUnloaderSlotNo;
        private System.Windows.Forms.Label lbUnloaderTrayIndexYTitle;
        private System.Windows.Forms.Label lbUnloaderTrayIndexY;
        private System.Windows.Forms.Label lbUnloaderTrayIndexXTitle;
        private System.Windows.Forms.Label lbUnloaderTrayIndexX;
        private System.Windows.Forms.Label lbLensSlotNoTitle;
        private System.Windows.Forms.Label lbLensSlotNo;
        private System.Windows.Forms.Label lbLensTrayIndexYTitle;
        private System.Windows.Forms.Label lbLensTrayIndexY;
        private System.Windows.Forms.Label lbLensTrayIndexXTitle;
        private System.Windows.Forms.Label lbLensTrayIndexX;
        private System.Windows.Forms.Label lbVCMSlotNoTitle;
        private System.Windows.Forms.Label lbVCMSlotNo;
        private System.Windows.Forms.Label lbVCMTrayIndexYTitle;
        private System.Windows.Forms.Label lbVCMTrayIndexY;
        private System.Windows.Forms.Label lbVCMTrayIndexXTitle;
        private System.Windows.Forms.Label lbVCMTrayIndexX;
        private Glass.GlassButton btnUnloadMagazineFull;
        private Glass.GlassButton btnLensMagazineFull;
        private Glass.GlassButton btnVCMMagazineFull;
        private System.Windows.Forms.Label lbTopVisionStep;
        private System.Windows.Forms.Label lbTopVisionMode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Glass.GlassButton btnNGTrayReset;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel pnMainIndex;
        private System.Windows.Forms.Label lbIndexNum;
        private System.Windows.Forms.Label lbAct2Fail;
        private System.Windows.Forms.Label lbAct2Pass;
        private System.Windows.Forms.Label lbAct2Ready;
        private SevenSegment.SevenSegmentArray lbDisplaceValue;
        private System.Windows.Forms.Label lbAct2Mode;
        private System.Windows.Forms.Label lbAct1Fail;
        private System.Windows.Forms.Label lbAct1Pass;
        private System.Windows.Forms.Label lbAct1Ready;
        private System.Windows.Forms.Label lbAct1Mode;
        private System.Windows.Forms.Label lbInnerLight;
        private Glass.GlassButton btnInnerLight;
        private Glass.GlassButton btnUVLamp2;
        private Glass.GlassButton btnUvLamp;
        private System.Windows.Forms.Label lbDoor8;
        private System.Windows.Forms.Label lbDoor7;
        private System.Windows.Forms.Label lbDoor3;
        private System.Windows.Forms.Label lbDoor4;
        private System.Windows.Forms.Label lbDoor6;
        private System.Windows.Forms.Label lbDoor5;
        private System.Windows.Forms.Label lbDoor2;
        private System.Windows.Forms.Label lbDoor1;
        private System.Windows.Forms.Label lbVCMLoadSensor;
        private System.Windows.Forms.Label lbUnloadSensor;
        private System.Windows.Forms.Label lbCure2Sensor;
        private System.Windows.Forms.Label lbCure1Sensor;
        private System.Windows.Forms.Label lbLensHeightSensor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbUnloadTrayExist;
        private System.Windows.Forms.Label lbVCMTrayExist;
        private System.Windows.Forms.Label lbLensTrayExist;
        private System.Windows.Forms.Label lbPlateAngleExist;
        private System.Windows.Forms.Panel pnUnloadMagazine;
        private System.Windows.Forms.Panel pnLensMagazine;
        private System.Windows.Forms.Panel pnVCMMagazine;
        private System.Windows.Forms.Label lbLensHeadStatus;
        private System.Windows.Forms.Label lbUnloadHeadStatus;
        private System.Windows.Forms.Label lbVCMHeadStatus;
        private System.Windows.Forms.Label lbPlateAngleStatus;
        private System.Windows.Forms.Label lbCure2Status;
        private System.Windows.Forms.Label lbCure1Status;
        private System.Windows.Forms.Label lbVisionInsStatus;
        private System.Windows.Forms.Label lbBonder2Status;
        private System.Windows.Forms.Label lbBonder1Status;
        private System.Windows.Forms.Label lbLensHeightStatus;
        private System.Windows.Forms.Label lbLensInsertStatus;
        private System.Windows.Forms.Label lbAct3Status;
        private System.Windows.Forms.Label lbJigPlateAngleStatus;
        private System.Windows.Forms.Label lbVCMLoadStatus;
        private System.Windows.Forms.Label lbCleanJigStatus;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.Label lbNone;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lbExist;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.Label lbReady;
        private System.Windows.Forms.PictureBox picMainIndex;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart LensHeight_Chart;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart Yield_Chart;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataVisualization.Charting.Chart SideAngle_Chart;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lbLensHeightSetProdcut;
        private System.Windows.Forms.Label lbLensHeightSetPorductNumber;
        private System.Windows.Forms.Label lbSideAngleTrend;
        private System.Windows.Forms.Label lbSideAngleTrendAverage;
        private System.Windows.Forms.Label lbLensHeightTrend;
        private System.Windows.Forms.Label lbLensHeightTrendAverage;
        private System.Windows.Forms.TabPage tabPage3;
        private Glass.GlassButton btnAngle_All;
        private Glass.GlassButton btnAngle_Single;
        private System.Windows.Forms.TableLayoutPanel tpanel__Measuring;
        private System.Windows.Forms.Label blImgAngleValue_3;
        private System.Windows.Forms.Label blImgAngleValue_4;
        private System.Windows.Forms.Label blImgAngleValue_5;
        private System.Windows.Forms.Label blImgAngleValue_6;
        private System.Windows.Forms.Label blImgAngleValue_7;
        private System.Windows.Forms.Label blImgAngleValue_8;
        private System.Windows.Forms.Label blImgAngleValue_9;
        private System.Windows.Forms.Label blImgAngleValue_10;
        private System.Windows.Forms.Label blImgAngleValue_11;
        private System.Windows.Forms.Label blImgAngleValue_0;
        private System.Windows.Forms.Label blImgAngleValue_2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblZValue1;
        private System.Windows.Forms.Label lblZValue4;
        private System.Windows.Forms.Label lblZValue2;
        private System.Windows.Forms.Label lblZValue3;
        private System.Windows.Forms.Label blImgAngleValue_1;
        private Glass.GlassButton btnVcmSkip;
        private Glass.GlassButton btnLensSkip;
        private SevenSegment.SevenSegmentArray lbSideAngleValue;
        private Glass.GlassButton btnUnloadTraySkip;
        private Glass.GlassButton btnNgSkip;
        private Glass.GlassButton btnYieldReset;
        private System.Windows.Forms.Panel pnUnloadTrayChange;
        private System.Windows.Forms.Label lbUnloadTrayChangeText;
        private System.Windows.Forms.Panel pnVCMTrayChange;
        private System.Windows.Forms.Label lbVcmTrayChangeText;
        private System.Windows.Forms.Panel pnLensTrayChange;
        private System.Windows.Forms.Label lbLensTrayChangeText;
        private Glass.GlassButton btnTestButton;
        private System.Windows.Forms.ProgressBar pbLensTray;
        private System.Windows.Forms.ProgressBar pbUnloadTray;
        private System.Windows.Forms.ProgressBar pbVcmTray;
        private Glass.GlassButton btnUnloadReset;
        private Glass.GlassButton btnLensReset;
        private Glass.GlassButton btnVcmReset;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbDailyNGCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbDailyOKCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbDailyTotalCount;
        private Glass.GlassButton btnDailyCountReset;
        private System.Windows.Forms.Label lbUVLamp2LifeTime;
        private System.Windows.Forms.Label lbUVLamp1LifeTime;
        private System.Windows.Forms.Panel pnBonder1;
        private Glass.GlassButton btnBond1Reset;
        private System.Windows.Forms.Label lbBond1JetCount;
        private System.Windows.Forms.Label lbBond1TotalCount;
        private VerticalProgressBar proBonder1;
        private System.Windows.Forms.Label lbBond1Title;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Panel pnBond2;
        private Glass.GlassButton btnBond2Reset;
        private System.Windows.Forms.Label lbBond2JetCount;
        private System.Windows.Forms.Label lbBond2TotalCount;
        private VerticalProgressBar proBonder2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbActuating2Total;
        private System.Windows.Forms.Label lbSideAngleTotal;
        private System.Windows.Forms.Label lbActuating1Total;
        private System.Windows.Forms.Label lbVisionTotal;
        private System.Windows.Forms.Label lbLensHeightTotal;
        private System.Windows.Forms.Label lbOKTotal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbNGValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbNGType;
        private System.Windows.Forms.Label lbNGTrayIndexTitle;
        private System.Windows.Forms.Label lbNGIndex;
        private Glass.GlassButton btnUnloadTHome;
        private SevenSegment.SevenSegmentArray lbDisplaceValueBond2;
        private SevenSegment.SevenSegmentArray lbDisplaceValueBond1;
        private SevenSegment.SevenSegmentArray lbDisplaceValueLensZTorque;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblProdType;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblOperation;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblUseMES;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblmeseqpid;
        private System.Windows.Forms.Label lbAct3Fail;
        private System.Windows.Forms.Label lbAct3Pass;
        private System.Windows.Forms.Label lbAct3Ready;
        private System.Windows.Forms.Label lbAct3Mode;
        private System.Windows.Forms.Label lbSideHeightTotal;
        private System.Windows.Forms.Label lbActuating3Total;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lbActuating3Mode;
        //private Cognex.VisionPro.Display.CogDisplay cogDisplay1;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XModule.Running;
using XModule.Standard;
using XModule.Unit;
using System.IO;
using XModule.Datas;

namespace XModule.Forms.FormOperation
{
    public partial class FrmPageOperation : TFrame
    {
        OpenFileDialog FOpenDialog = new OpenFileDialog();
        public FrmPageOperation()
        {
            InitializeComponent();
            SetBounds(0, 0, 1920, 995);

            ListVCMLabel = new List<Label>();
            ListLensLabel = new List<Label>();
            ListUnloadLabel = new List<Label>();

            AngleDataDisp();
        }
        private Bitmap VCMbitmap;
        private Bitmap Lensbitmap;
        private Bitmap Unloadbitmap;
        private Bitmap Ngbitmap;

        

        private int LensHeightChartMaximum_X = 100;
        private bool TactDisplay = false;

        private void AngleDataDisp()
        {

            for (int i = 1; i < 7; i++)
            {
                Label NewLbl = new Label();
                NewLbl.BackColor = System.Drawing.Color.Gainsboro;
                NewLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                NewLbl.Dock = System.Windows.Forms.DockStyle.Fill;
                NewLbl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NewLbl.ForeColor = System.Drawing.Color.Black;
                NewLbl.Margin = new System.Windows.Forms.Padding(2);
                NewLbl.Name = $"lblColName{i}";
                NewLbl.Size = new System.Drawing.Size(94, 20);
                NewLbl.TabIndex = 1714;
                NewLbl.Tag = "";
                if (i == 1)
                    NewLbl.Text = $"Angle";
                else if (i == 6)
                {
                    NewLbl.Text = $"Deviation";
                }
                else
                    NewLbl.Text = $"Z #{i-1}";
                NewLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                this.tpanel__Measuring.Controls.Add(NewLbl, i, 0);
            }

            for (int i = 1; i < 13; i++)
            {
                Label NewLbl = new Label();
                NewLbl.BackColor = System.Drawing.Color.Gainsboro;
                NewLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                NewLbl.Dock = System.Windows.Forms.DockStyle.Fill;
                NewLbl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NewLbl.ForeColor = System.Drawing.Color.Black;
                NewLbl.Margin = new System.Windows.Forms.Padding(2);
                NewLbl.Name = $"lblRowName{i}";
                NewLbl.Size = new System.Drawing.Size(94, 20);
                NewLbl.TabIndex = 1714;
                NewLbl.Tag = "";
                NewLbl.Text = $"# {i}";
                NewLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                this.tpanel__Measuring.Controls.Add(NewLbl, 0,i);
            }
            for (int i = 1; i < 13; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    Label NewLbl = new Label();
                    NewLbl.BackColor = System.Drawing.Color.WhiteSmoke;
                    NewLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    NewLbl.Dock = System.Windows.Forms.DockStyle.Fill;
                    NewLbl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    NewLbl.ForeColor = System.Drawing.Color.Black;                   
                    NewLbl.Margin = new System.Windows.Forms.Padding(2);
                    NewLbl.Name = $"blAngleValue_{i}_{j}";
                    NewLbl.Size = new System.Drawing.Size(58, 20);
                    NewLbl.TabIndex = 1715;
                    NewLbl.Tag = "";
                    NewLbl.Text = "0";
                    NewLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    this.tpanel__Measuring.Controls.Add(NewLbl, j, i);
                }
            }
           

        }
        private void FormPageOperation_Load(object sender, EventArgs e)
        {
            Left = 0;
            Top = cSystem.formPageTop;
            Visible = false;

            FormTaskLog frmTaskLog = new FormTaskLog();
            frmTaskLog.Text = string.Empty;

            cDEF.fTaskLog.Show();
            cDEF.fTaskLog.Hide();


            VCMbitmap = new Bitmap((int)picVCMMap.Width, (int)picVCMMap.Height);
            picVCMMap.Image = VCMbitmap;
            Lensbitmap = new Bitmap((int)picLensMap.Width, (int)picLensMap.Height);
            picLensMap.Image = Lensbitmap;
            Unloadbitmap = new Bitmap((int)picUnloadMap.Width, (int)picUnloadMap.Height);
            picUnloadMap.Image = Unloadbitmap;
            Ngbitmap = new Bitmap((int)picNGMap.Width, (int)picNGMap.Height);
            picNGMap.Image = Ngbitmap;

            VCMMagazineObjectInit();
            LensMagazineObjectInit();
            UnloadMagazineObjectInit();

            VCMTrayMapInit();
            LensTrayMapInit();
            UnloadTrayMapInit();
            NgTrayMapInit();

            //추후 삭제
            cDEF.Work.Project.GlobalOption.UseLensPicker = true;

            // Event
            cDEF.Run.VCMPicker.OnVCM_Display += VCMPicker_OnVCM_Display;
            cDEF.Run.LensPicker.OnLens_Display += LensPicker_OnLens_Display;
            cDEF.Run.UnloadPicker.OnVCM_Display += UnloadPicker_OnVCM_Display;
            cDEF.Run.UnloadPicker.OnNg_Display += UnloadPicker_OnNg_Display;

            cDEF.Run.VCMLoader.OnVCM_DisplayInit += VCMLoader_OnVCM_DisplayInit;
            cDEF.Run.LensLoader.OnLens_DisplayInit += LensLoader_OnLens_DisplayInit;
            cDEF.Run.Unloader.OnUnload_DisplayInit += Unloader_OnUnload_DisplayInit;

            cDEF.Run.LensHeight.OnChart += LensHeight_OnChart;
            cDEF.Run.UnloadPicker.OnYieldChart += UnloadPicker_OnYieldChart;

            cDEF.Run.PlateAngle.OnChart += SideAngle_OnChart;

            

            cDEF.Run.UnloadPicker.ChartDisplayTest(0, eFailType.AssembleFail);

            proBonder1.Style = ProgressBarStyle.Blocks;

            SideAngle_Chart_Init();

        }




        int[] OKCount = new int[12];
        int[] LensHeigtCount = new int[12];
        int[] VisionInspectCount = new int[12];
        int[] Act1Count = new int[12];
        int[] PlateAngleCount = new int[12];
        int[] Act2Count = new int[12];
        int[] Act3Count = new int[12];
        int[] SideHeightCount = new int[12]; //Adding Error

        StringBuilder strChatTmp = new StringBuilder();
        private void YieldCountClear()
        {
            for(int i = 0; i < 12; i++)
            {
                OKCount[i] = 0;
                LensHeigtCount[i] = 0;
                VisionInspectCount[i] = 0;
                Act1Count[i] = 0;
                PlateAngleCount[i] = 0;
                Act2Count[i] = 0;
                Act3Count[i] = 0;
                SideHeightCount[i] = 0; //Adding Error
            }
        }
        private void UnloadPicker_OnYieldChart(int Index, eFailType FailType)
        {
            this.Invoke(new Action(delegate ()
            {
               
                switch (FailType)
                {
                    case eFailType.None:
                        cDEF.Run.UnloadPicker.Information.ChartOkCount[Index]++;
                        cDEF.Run.UnloadPicker.Information.DailyOKCount++;
                        break;
                    case eFailType.LensHeightFail:
                        cDEF.Run.UnloadPicker.Information.ChartLensHeightFailCount[Index]++;
                        cDEF.Run.UnloadPicker.Information.DailyNGCount++;
                        break;
                    case eFailType.VisionInspectFail:
                        cDEF.Run.UnloadPicker.Information.ChartVisionInspectFailCount[Index]++;
                        cDEF.Run.UnloadPicker.Information.DailyNGCount++;
                        break;
                    case eFailType.Actuating1Fail:
                        cDEF.Run.UnloadPicker.Information.ChartAct1FailCount[Index]++;
                        cDEF.Run.UnloadPicker.Information.DailyNGCount++;
                        break;
                    case eFailType.PlateAngleFail:
                        cDEF.Run.UnloadPicker.Information.ChartSidAngleFailCount[Index]++;
                        cDEF.Run.UnloadPicker.Information.DailyNGCount++;
                        break;
                    case eFailType.Actuating2Fail:
                        cDEF.Run.UnloadPicker.Information.ChartAct2FailCount[Index]++;
                        cDEF.Run.UnloadPicker.Information.DailyNGCount++;
                        break;
                    //case eFailType.Actuating3Fail:
                    //    cDEF.Run.UnloadPicker.Information.ChartAct3FailCount[Index]++;
                    //    cDEF.Run.UnloadPicker.Information.DailyNGCount++;
                    //    break;
                    case eFailType.SideHeightFail: //Adding Error
                        cDEF.Run.UnloadPicker.Information.ChartSideHeightFailCount[Index]++;
                        cDEF.Run.UnloadPicker.Information.DailyNGCount++;
                        break;
                }
                YieldCountClear();
                strChatTmp.Clear();
                for (int i = 0; i < 12; i++)
                {
                    strChatTmp.Append(i + 1);
                    strChatTmp.Append(",");
                    strChatTmp.Append("OK:");
                    strChatTmp.Append(cDEF.Run.UnloadPicker.Information.ChartOkCount[i].ToString());
                    strChatTmp.Append(",");
                    strChatTmp.Append("LensHeight:");
                    strChatTmp.Append(cDEF.Run.UnloadPicker.Information.ChartLensHeightFailCount[i].ToString());
                    strChatTmp.Append(",");
                    strChatTmp.Append("VisionInspect:");
                    strChatTmp.Append(cDEF.Run.UnloadPicker.Information.ChartVisionInspectFailCount[i].ToString());
                    strChatTmp.Append(",");
                    strChatTmp.Append("Act1:");
                    strChatTmp.Append(cDEF.Run.UnloadPicker.Information.ChartAct1FailCount[i].ToString());
                    strChatTmp.Append(",");
                    strChatTmp.Append("Act2:");
                    strChatTmp.Append(cDEF.Run.UnloadPicker.Information.ChartAct2FailCount[i].ToString());
                    strChatTmp.Append(",");
                    //strChatTmp.Append("Act3:");
                    //strChatTmp.Append(cDEF.Run.UnloadPicker.Information.ChartAct3FailCount[i].ToString());
                    //strChatTmp.Append(",");
                    strChatTmp.Append("PlateAngle:");
                    strChatTmp.Append(cDEF.Run.UnloadPicker.Information.ChartSidAngleFailCount[i].ToString());
                    strChatTmp.Append(",");
                    strChatTmp.Append("SideHeight:"); //Adding Error
                    strChatTmp.Append(cDEF.Run.UnloadPicker.Information.ChartSideHeightFailCount[i].ToString());
                    strChatTmp.Append(",");


                    OKCount[i] = cDEF.Run.UnloadPicker.Information.ChartOkCount[i];
                    LensHeigtCount[i] = cDEF.Run.UnloadPicker.Information.ChartLensHeightFailCount[i];
                    VisionInspectCount[i] = cDEF.Run.UnloadPicker.Information.ChartVisionInspectFailCount[i];
                    Act1Count[i] = cDEF.Run.UnloadPicker.Information.ChartAct1FailCount[i];
                    Act2Count[i] = cDEF.Run.UnloadPicker.Information.ChartAct2FailCount[i];
                    Act3Count[i] = cDEF.Run.UnloadPicker.Information.ChartAct3FailCount[i];
                    PlateAngleCount[i] = cDEF.Run.UnloadPicker.Information.ChartSidAngleFailCount[i];
                    SideHeightCount[i] = cDEF.Run.UnloadPicker.Information.ChartSideHeightFailCount[i]; //Adding Error


                }

                cDEF.TaskLogAppend(TaskLog.YieldChartData, strChatTmp.ToString(), true);


                Yield_Chart.Series.Clear();
                Yield_Chart.Series.Add("OK");
                Yield_Chart.Series[0].Points.DataBindY(OKCount);
                Yield_Chart.Series.Add("Lens Height");
                Yield_Chart.Series[1].Points.DataBindY(LensHeigtCount);
                Yield_Chart.Series.Add("Vision Inspect");
                Yield_Chart.Series[2].Points.DataBindY(VisionInspectCount);
                Yield_Chart.Series.Add("Actuating 1");
                Yield_Chart.Series[3].Points.DataBindY(Act1Count);
                Yield_Chart.Series.Add("Side Angle");
                Yield_Chart.Series[4].Points.DataBindY(PlateAngleCount);
                Yield_Chart.Series.Add("Actuating 2");
                Yield_Chart.Series[5].Points.DataBindY(Act2Count);
                Yield_Chart.Series.Add("Side Height"); //Adding Error
                Yield_Chart.Series[6].Points.DataBindY(SideHeightCount);

                Yield_Chart.Series[0].LabelFormat = "N0";
                Yield_Chart.Series[1].LabelFormat = "N0";
                Yield_Chart.Series[1].IsValueShownAsLabel = true;
                Yield_Chart.Series[2].LabelFormat = "N0";
                Yield_Chart.Series[2].IsValueShownAsLabel = true;
                Yield_Chart.Series[3].LabelFormat = "N0";
                Yield_Chart.Series[3].IsValueShownAsLabel = true;
                Yield_Chart.Series[4].LabelFormat = "N0";
                Yield_Chart.Series[4].IsValueShownAsLabel = true;
                Yield_Chart.Series[5].LabelFormat = "N0";
                Yield_Chart.Series[5].IsValueShownAsLabel = true;
                Yield_Chart.Series[6].LabelFormat = "N0"; //Adding Error
                Yield_Chart.Series[6].IsValueShownAsLabel = true;

                int OKTotal = 0;
                int LensHeightFailTotal = 0;
                int VisionFailTotal = 0;
                int Actuating1FailTotal = 0;
                int SideAngleFailTotal = 0;
                int Actuating2FailTotal = 0;
                int Actuating3FailTotal = 0;
                int SideHeightFailTotal = 0; //Adding Error

                for (int i = 0; i < 12; i++)
                {
                    OKTotal += cDEF.Run.UnloadPicker.Information.ChartOkCount[i];
                    LensHeightFailTotal += cDEF.Run.UnloadPicker.Information.ChartLensHeightFailCount[i];
                    VisionFailTotal += cDEF.Run.UnloadPicker.Information.ChartVisionInspectFailCount[i];
                    Actuating1FailTotal += cDEF.Run.UnloadPicker.Information.ChartAct1FailCount[i];
                    SideAngleFailTotal += cDEF.Run.UnloadPicker.Information.ChartSidAngleFailCount[i];
                    Actuating2FailTotal += cDEF.Run.UnloadPicker.Information.ChartAct2FailCount[i];
                    Actuating3FailTotal += cDEF.Run.UnloadPicker.Information.ChartAct3FailCount[i];
                    SideHeightFailTotal += cDEF.Run.UnloadPicker.Information.ChartSideHeightFailCount[i]; //Adding Error
                }

                int Total = OKTotal + LensHeightFailTotal + Actuating1FailTotal + SideAngleFailTotal + Actuating2FailTotal + Actuating3FailTotal + VisionFailTotal + SideHeightFailTotal; //Adding Error

                lbOKTotal.Text = $"{OKTotal}   {((double)OKTotal / (double)Total * 100.0).ToString("N2")} %";
                lbVisionTotal.Text = $"{VisionFailTotal}   {((double)VisionFailTotal / (double)Total * 100.0).ToString("N2")} %";
                lbLensHeightTotal.Text = $"{LensHeightFailTotal}   {((double)LensHeightFailTotal / (double)Total * 100.0).ToString("N2")} %";
                lbActuating1Total.Text = $"{Actuating1FailTotal}   {((double)Actuating1FailTotal / (double)Total * 100.0).ToString("N2")} %";
                lbSideAngleTotal.Text = $"{SideAngleFailTotal}   {((double)SideAngleFailTotal / (double)Total * 100.0).ToString("N2")} %";
                lbActuating2Total.Text = $"{Actuating2FailTotal}   {((double)Actuating2FailTotal / (double)Total * 100.0).ToString("N2")} %";
                lbActuating3Total.Text = $"{Actuating3FailTotal}   {((double)Actuating3FailTotal / (double)Total * 100.0).ToString("N2")} %";
                lbSideHeightTotal.Text = $"{SideHeightFailTotal}   {((double)SideHeightFailTotal / (double)Total * 100.0).ToString("N2")} %"; //Adding Error
            }));
        }



        private void LensHeight_OnChart(int Index, double Value)
        {
            this.Invoke(new Action(delegate ()
            {
                cDEF.Run.UnloadPicker.Information.LensHeightData.Add(Value);

                if (cDEF.Run.UnloadPicker.Information.LensHeightData.Count > LensHeightChartMaximum_X)
                    cDEF.Run.UnloadPicker.Information.LensHeightData.RemoveAt(0);

                //if (LensHeight_Chart.Series[0].Points.Count > LensHeightChartMaximum_X)
                    //LensHeight_Chart.Series[0].Points.RemoveAt(0);

                LensHeight_Chart.Series.Clear();
                LensHeight_Chart.Series.Add("Lens Height");
                LensHeight_Chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                LensHeight_Chart.Series[0].Color = Color.Red;
                LensHeight_Chart.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                LensHeight_Chart.Series[0].MarkerColor = Color.Blue;
                
                double Total = 0.0;
                foreach(double d in cDEF.Run.UnloadPicker.Information.LensHeightData)
                {
                    LensHeight_Chart.Series[0].Points.AddXY(LensHeight_Chart.Series[0].Points.Count, d);
                    Total += d;
                }

                double yield = Total / cDEF.Run.UnloadPicker.Information.LensHeightData.Count;

                lbLensHeightTrendAverage.Text = yield.ToString("N3");

            }));
        }

        private void SideAngle_OnChart(int Index, double Value)
        {
            this.Invoke(new Action(delegate ()
            {
                if (Value > 40.0)
                    Value = 40.0;

                if (Value < -40.0)
                    Value = -40.0;

                cDEF.Run.UnloadPicker.Information.SideAngleData.Add(Value);

                if (cDEF.Run.UnloadPicker.Information.SideAngleData.Count > LensHeightChartMaximum_X)
                    cDEF.Run.UnloadPicker.Information.SideAngleData.RemoveAt(0);

                SideAngle_Chart.Series.Clear();
                SideAngle_Chart.Series.Add("Side Angle");
                SideAngle_Chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                SideAngle_Chart.Series[0].Color = Color.Red;
                SideAngle_Chart.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                SideAngle_Chart.Series[0].MarkerColor = Color.Blue;

                double Total = 0.0;
                foreach (double d in cDEF.Run.UnloadPicker.Information.SideAngleData)
                {
                    SideAngle_Chart.Series[0].Points.AddXY(SideAngle_Chart.Series[0].Points.Count, d);
                    Total += d;
                }

                double yield = Total / cDEF.Run.UnloadPicker.Information.SideAngleData.Count;

                lbSideAngleTrendAverage.Text = yield.ToString("N3");


                cDEF.Run.UnloadPicker.Information.SideAngleCountSave();

            }));
        }
        private void SideAngle_Chart_Init()
        {
            this.Invoke(new Action(delegate ()
            {
                SideAngle_Chart.Series.Clear();
                SideAngle_Chart.Series.Add("Side Angle");
                SideAngle_Chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                SideAngle_Chart.Series[0].Color = Color.Red;
                SideAngle_Chart.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                SideAngle_Chart.Series[0].MarkerColor = Color.Blue;

                double Total = 0.0;
                foreach (double d in cDEF.Run.UnloadPicker.Information.SideAngleData)
                {
                    SideAngle_Chart.Series[0].Points.AddXY(SideAngle_Chart.Series[0].Points.Count, d);
                    Total += d;
                }

                double yield = Total / cDEF.Run.UnloadPicker.Information.SideAngleData.Count;

                lbSideAngleTrendAverage.Text = yield.ToString("N3");

            }));
        }

        private void Unloader_OnUnload_DisplayInit()
        {
            UnloadTrayMapInit();
        }

        private void LensLoader_OnLens_DisplayInit()
        {
            LensTrayMapInit();
        }

        private void VCMLoader_OnVCM_DisplayInit()
        {
            VCMTrayMapInit();
        }

        private void UnloadPicker_OnNg_Display(int x, int y, eFailType failType)
        {
            NgTrayMapDisplay(x, y, failType);
        }

        private void UnloadPicker_OnVCM_Display(int x, int y, LensTrayStatus status)
        {
            UnloadTrayMapDisplay(x, y, status);
        }

        private void LensPicker_OnLens_Display(int x, int y, LensTrayStatus status)
        {
            LensTrayMapDisplay(x, y, status);
        }

        private void VCMPicker_OnVCM_Display(int x, int y, LensTrayStatus status)
        {
            VCMTrayMapDisplay(x, y, status);
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

            this.Invoke(new Action(delegate ()
            {
              
                    label_EqpID.Text = cDEF.Work.Project.MachineName;
                //Connect
                if (TactDisplay)
                {
                    #region ModeToString
                    lbVCMLoaderMode.Text = $"{cDEF.Run.VCMLoader.ModeToString()} / {cDEF.Run.VCMLoader.Step} ";
                    lbVCMPickerMode.Text = $"{cDEF.Run.VCMPicker.ModeToString()} / {cDEF.Run.VCMPicker.Step} ";
                    lbLensLoaderMode.Text = $"{cDEF.Run.LensLoader.ModeToString()} / {cDEF.Run.LensLoader.Step} ";
                    lbLensPickerMode.Text = $"{cDEF.Run.LensPicker.ModeToString()} / {cDEF.Run.LensPicker.Step} ";
                    lbJigPlateMode.Text = $"{cDEF.Run.JigPlateAngle.ModeToString()} / {cDEF.Run.JigPlateAngle.Step} ";
                    lbLensHeightMode.Text = $"{cDEF.Run.LensHeight.ModeToString()} / {cDEF.Run.LensHeight.Step} ";
                    lbBond1Mode.Text = $"{cDEF.Run.Bonder1.ModeToString()} / {cDEF.Run.Bonder1.Step} ";
                    lbBond2Mode.Text = $"{cDEF.Run.Bonder2.ModeToString()} / {cDEF.Run.Bonder2.Step} ";
                    lbVisionInspectMode.Text = $"{cDEF.Run.VisionInspect.ModeToString()} / {cDEF.Run.VisionInspect.Step} ";
                    lbCuring1Mode.Text = $"{cDEF.Run.Curing1.ModeToString()} / {cDEF.Run.Curing1.Step} ";
                    lbCuring2Mode.Text = $"{cDEF.Run.Curing2.ModeToString()} / {cDEF.Run.Curing2.Step} ";
                    lbPlateAngleMode.Text = $"{cDEF.Run.PlateAngle.ModeToString()} / {cDEF.Run.PlateAngle.Step} ";
                    lbUnloaderMode.Text = $"{cDEF.Run.Unloader.ModeToString()} / {cDEF.Run.Unloader.Step} ";
                    lbUnloadPickerMode.Text = $"{cDEF.Run.UnloadPicker.ModeToString()} / {cDEF.Run.UnloadPicker.Step} ";
                    lbCleanJigMode.Text = $"{cDEF.Run.CleanJig.ModeToString()} / {cDEF.Run.CleanJig.Step} ";
                    lbIndexMode.Text = $"{cDEF.Run.Index.ModeToString()} / {cDEF.Run.Index.Step} ";
                    lbTopVisionMode.Text = $"{cDEF.Run.VCMVision.ModeToString()} / {cDEF.Run.VCMVision.Step} ";
                    lbActuating3Mode.Text = $"{cDEF.Run.Act3.ModeToString()} / {cDEF.Run.Act3.Step} ";

                    lbVCMLoaderMode.BackColor = cDEF.Run.VCMLoader.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbVCMPickerMode.BackColor = cDEF.Run.VCMPicker.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbLensLoaderMode.BackColor = cDEF.Run.LensLoader.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbLensPickerMode.BackColor = cDEF.Run.LensPicker.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbJigPlateMode.BackColor = cDEF.Run.JigPlateAngle.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbLensHeightMode.BackColor = cDEF.Run.LensHeight.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbBond1Mode.BackColor = cDEF.Run.Bonder1.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbBond2Mode.BackColor = cDEF.Run.Bonder2.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbVisionInspectMode.BackColor = cDEF.Run.VisionInspect.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbCuring1Mode.BackColor = cDEF.Run.Curing1.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbCuring2Mode.BackColor = cDEF.Run.Curing2.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbPlateAngleMode.BackColor = cDEF.Run.PlateAngle.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbUnloaderMode.BackColor = cDEF.Run.Unloader.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbUnloadPickerMode.BackColor = cDEF.Run.UnloadPicker.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbCleanJigMode.BackColor = cDEF.Run.CleanJig.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbIndexMode.BackColor = cDEF.Run.Index.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbTopVisionMode.BackColor = cDEF.Run.VCMVision.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    lbActuating3Mode.BackColor = cDEF.Run.Act3.Step != 0 ? Color.LightGreen : Color.WhiteSmoke;
                    #endregion
                }
                else
                {
                    #region Tact
                    lbVCMLoaderMode.Text = $"{cDEF.Tact.VCMLoader.CycleTime.ToString("N3")} sec";
                    lbVCMPickerMode.Text = $"{cDEF.Tact.VCMPicker.CycleTime.ToString("N3")} sec";
                    lbLensLoaderMode.Text = $"{cDEF.Tact.LensLoader.CycleTime.ToString("N3")} sec";
                    lbLensPickerMode.Text = $"{cDEF.Tact.LensPicker.CycleTime.ToString("N3")} sec";
                    lbJigPlateMode.Text = $"{cDEF.Tact.JigPlateAngle.CycleTime.ToString("N3")} sec";
                    lbLensHeightMode.Text = $"{cDEF.Tact.LensHeight.CycleTime.ToString("N3")} sec";
                    lbBond1Mode.Text = $"{cDEF.Tact.Bonder1.CycleTime.ToString("N3")} sec";
                    lbBond2Mode.Text = $"{cDEF.Tact.Bonder2.CycleTime.ToString("N3")} sec";
                    lbVisionInspectMode.Text = $"{cDEF.Tact.VisionInspect.CycleTime.ToString("N3")} sec";
                    lbCuring1Mode.Text = $"{cDEF.Tact.Curing1.CycleTime.ToString("N3")} sec";
                    lbCuring2Mode.Text = $"{cDEF.Tact.Curing2.CycleTime.ToString("N3")} sec";
                    lbPlateAngleMode.Text = $"{cDEF.Tact.PlateAngle.CycleTime.ToString("N3")} sec";
                    lbUnloaderMode.Text = $"{cDEF.Tact.Unloader.CycleTime.ToString("N3")} sec";
                    lbUnloadPickerMode.Text = $"{cDEF.Tact.UnloadPicker.CycleTime.ToString("N3")} sec";
                    lbCleanJigMode.Text = $"{cDEF.Tact.CleanJig.CycleTime.ToString("N3")} sec";
                    lbIndexMode.Text = $"{cDEF.Tact.Index.CycleTime.ToString("N3")} sec";
                    lbTopVisionMode.Text = $"{cDEF.Tact.TopVision.CycleTime.ToString("N3")} sec";
                    lbActuating3Mode.Text = $"{cDEF.Tact.Actuator3.CycleTime.ToString("N3")} sec";
                    #endregion
                }
                lbLedTactTime.Text = $"{cDEF.Tact.LensTact.CycleTime.ToString("N3")} sec";

                // Magazine Display
                for (int i = 0; i < cDEF.Run.VCMLoader.Information.VCM_Magazine.Items.Count; i++)
                {
                    //if (cDEF.Work.Recipe.LoaderMagazineDirection)
                    //{
                    //    if (cDEF.Run.VCMLoader.Information.VCM_Magazine.Items[i].Status == TrayStatus.Empty)
                    //        ListVCMLabel[i].BackColor = Color.Gray;
                    //    else if (cDEF.Run.VCMLoader.Information.VCM_Magazine.Items[i].Status == TrayStatus.Load)
                    //        ListVCMLabel[i].BackColor = Color.Aqua;
                    //    else if (cDEF.Run.VCMLoader.Information.VCM_Magazine.Items[i].Status == TrayStatus.Finish)
                    //        ListVCMLabel[i].BackColor = Color.Fuchsia;
                    //}
                    //else
                    //{
                    if (cDEF.Run.VCMLoader.Information.VCM_Magazine.Items[i].Status == TrayStatus.Empty)
                        ListVCMLabel[cDEF.Work.VCMLoader.SlotCount - (i + 1)].BackColor = Color.Gray;
                    else if (cDEF.Run.VCMLoader.Information.VCM_Magazine.Items[i].Status == TrayStatus.Load)
                        ListVCMLabel[cDEF.Work.VCMLoader.SlotCount - (i + 1)].BackColor = Color.Aqua;
                    else if (cDEF.Run.VCMLoader.Information.VCM_Magazine.Items[i].Status == TrayStatus.Finish)
                        ListVCMLabel[cDEF.Work.VCMLoader.SlotCount - (i + 1)].BackColor = Color.Fuchsia;
                    else
                        ListVCMLabel[cDEF.Work.VCMLoader.SlotCount - (i + 1)].BackColor = Color.Lime;
                    //}
                }

                for (int j = 0; j < cDEF.Run.LensLoader.Information.Lens_Magazine.Items.Count; j++)
                {
                    //if (cDEF.Work.Recipe.LensMagazineDirection)
                    //{
                    //    if (cDEF.Run.LensLoader.Information.Lens_Magazine.Items[j].Status == TrayStatus.Empty)
                    //        ListLensLabel[j].BackColor = Color.Gray;
                    //    else if (cDEF.Run.LensLoader.Information.Lens_Magazine.Items[j].Status == TrayStatus.Load)
                    //        ListLensLabel[j].BackColor = Color.Aqua;
                    //    else if (cDEF.Run.LensLoader.Information.Lens_Magazine.Items[j].Status == TrayStatus.Finish)
                    //        ListLensLabel[j].BackColor = Color.Fuchsia;
                    //}
                    //else
                    //{
                    if (cDEF.Run.LensLoader.Information.Lens_Magazine.Items[j].Status == TrayStatus.Empty)
                        ListLensLabel[cDEF.Work.LensLoader.SlotCount - (j + 1)].BackColor = Color.Gray;
                    else if (cDEF.Run.LensLoader.Information.Lens_Magazine.Items[j].Status == TrayStatus.Load)
                        ListLensLabel[cDEF.Work.LensLoader.SlotCount - (j + 1)].BackColor = Color.Aqua;
                    else if (cDEF.Run.LensLoader.Information.Lens_Magazine.Items[j].Status == TrayStatus.Finish)
                        ListLensLabel[cDEF.Work.LensLoader.SlotCount - (j + 1)].BackColor = Color.Fuchsia;
                    else
                        ListLensLabel[cDEF.Work.LensLoader.SlotCount - (j + 1)].BackColor = Color.Lime;
                    //}
                }

                for (int k = 0; k < cDEF.Run.Unloader.Information.Unloader_Magazine.Items.Count; k++)
                {
                    //if (cDEF.Work.Recipe.UnLoaderMagazineDirection)
                    //{
                    //    if (cDEF.Run.Unloader.Information.Unloader_Magazine.Items[k].Status == TrayStatus.Empty)
                    //        ListUnloadLabel[k].BackColor = Color.Gray;
                    //    else if (cDEF.Run.Unloader.Information.Unloader_Magazine.Items[k].Status == TrayStatus.Load)
                    //        ListUnloadLabel[k].BackColor = Color.Aqua;
                    //    else if (cDEF.Run.Unloader.Information.Unloader_Magazine.Items[k].Status == TrayStatus.Finish)
                    //        ListUnloadLabel[k].BackColor = Color.Fuchsia;
                    //}
                    //else
                    //{
                    if (cDEF.Run.Unloader.Information.Unloader_Magazine.Items[k].Status == TrayStatus.Empty)
                        ListUnloadLabel[cDEF.Work.Unloader.SlotCount - (k + 1)].BackColor = Color.Gray;
                    else if (cDEF.Run.Unloader.Information.Unloader_Magazine.Items[k].Status == TrayStatus.Load)
                        ListUnloadLabel[cDEF.Work.Unloader.SlotCount - (k + 1)].BackColor = Color.Aqua;
                    else if (cDEF.Run.Unloader.Information.Unloader_Magazine.Items[k].Status == TrayStatus.Finish)
                        ListUnloadLabel[cDEF.Work.Unloader.SlotCount - (k + 1)].BackColor = Color.Fuchsia;
                    else
                        ListUnloadLabel[cDEF.Work.Unloader.SlotCount - (k + 1)].BackColor = Color.Lime;
                    //}
                }


                lbVCMTrayIndexX.Text = cDEF.Run.VCMPicker.WorkLens.x.ToString();
                lbVCMTrayIndexY.Text = cDEF.Run.VCMPicker.WorkLens.y.ToString();
                lbLensTrayIndexX.Text = cDEF.Run.LensPicker.WorkLens.x.ToString();
                lbLensTrayIndexY.Text = cDEF.Run.LensPicker.WorkLens.y.ToString();
                lbUnloaderTrayIndexX.Text = cDEF.Run.UnloadPicker.WorkLens.x.ToString();
                lbUnloaderTrayIndexY.Text = cDEF.Run.UnloadPicker.WorkLens.y.ToString();

                if (cDEF.Work.Recipe.LoaderMagazineDirection)
                {
                    lbVCMSlotNo.Text = (cDEF.Work.VCMLoader.SlotCount - cDEF.Run.VCMLoader.Information.WorkTray).ToString();
                }
                else
                {
                    lbVCMSlotNo.Text = (cDEF.Run.VCMLoader.Information.WorkTray + 1).ToString();
                }
                if (cDEF.Work.Recipe.LensMagazineDirection)
                {
                    lbLensSlotNo.Text = (cDEF.Work.LensLoader.SlotCount - cDEF.Run.LensLoader.Information.WorkTray).ToString();
                }
                else
                {
                    lbLensSlotNo.Text = (cDEF.Run.LensLoader.Information.WorkTray + 1).ToString();
                }
                if (cDEF.Work.Recipe.UnLoaderMagazineDirection)
                {
                    lbUnloaderSlotNo.Text = (cDEF.Work.Unloader.SlotCount - cDEF.Run.Unloader.Information.WorkTray).ToString();
                }
                else
                {
                    lbUnloaderSlotNo.Text = (cDEF.Run.Unloader.Information.WorkTray + 1).ToString();
                }
                #region Sensor

                if(cDEF.Work.DispSensor.DispenserType == 0)
                    lbDispenser.BackColor = cDEF.Dispenser1.IsConnected() ? Color.Lime : Color.White;
                else if (cDEF.Work.DispSensor.DispenserType == 1)
                    lbDispenser.BackColor = cDEF.DispenserEcm1.IsConnected() ? Color.Lime : Color.White;
                else if (cDEF.Work.DispSensor.DispenserType == 2)
                    lbDispenser.BackColor = cDEF.TJV_1.Connect && cDEF.TJV_2.Connect ? Color.Lime : Color.White;

                lbDSensor.BackColor = cDEF.Serials.LensHeight.IsOpen ? Color.Lime : Color.White;
                lbMainAir.BackColor = cDEF.Run.Digital.Input[cDI.Main_AirW_Sensor] ? Color.White : Color.Lime;
                lbMC.BackColor = cDEF.Run.Digital.Input[cDI.MC_On_Off] ? Color.Lime : Color.White;
               
                if (!cDEF.Work.Project.GlobalOption.UseCuring1 && !cDEF.Work.Project.GlobalOption.UseCuring2)
                {
                    lbUVLamp.BackColor = Color.Lime;
                }
                else if ((cDEF.Work.Project.GlobalOption.UseCuring1 && cDEF.Run.Digital.Input[cDI.UV_1_Lamp_Ready_Monitor])
                         && (cDEF.Work.Project.GlobalOption.UseCuring2 && cDEF.Run.Digital.Input[cDI.UV_2_Lamp_Ready_Monitor]))                
                {
                    lbUVLamp.BackColor = Color.Lime;
                }
                else if ((cDEF.Work.Project.GlobalOption.UseCuring1 && cDEF.Run.Digital.Input[cDI.UV_1_Lamp_Ready_Monitor])
                         && (!cDEF.Work.Project.GlobalOption.UseCuring2))
                {
                    lbUVLamp.BackColor = Color.Lime;
                }
                else if ((cDEF.Work.Project.GlobalOption.UseCuring2 && cDEF.Run.Digital.Input[cDI.UV_2_Lamp_Ready_Monitor])
                        && (!cDEF.Work.Project.GlobalOption.UseCuring1))
                {
                    lbUVLamp.BackColor = Color.Lime;
                }
                else
                    lbUVLamp.BackColor = Color.White;

                //lbACTUATOR.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_1_Ready] 
                //                        && cDEF.Run.Digital.Input[cDI.Actuator_2_Ready] ? Color.Lime : Color.White;

                lbACTUATOR.BackColor = cUtilLocal.IsApplicationAlreadyRunning() ? Color.Lime : Color.White;
                

                lbVision.BackColor = cDEF.Visions.IsConnect() ? Color.Lime : Color.White;


                lbVCMLoadSensor.BackColor = cDEF.Run.Digital.Input[cDI.VCM_Loading] ? Color.Lime : Color.Gray;
                lbLensHeightSensor.BackColor = cDEF.Run.Digital.Input[cDI.Lens_Height_Check] ? Color.Lime : Color.Gray;
                lbCure1Sensor.BackColor = cDEF.Run.Digital.Input[cDI.Curing_1] ? Color.Lime : Color.Gray;
                lbCure2Sensor.BackColor = cDEF.Run.Digital.Input[cDI.Curing_2] ? Color.Lime : Color.Gray;
                lbUnloadSensor.BackColor = cDEF.Run.Digital.Input[cDI.VCM_Unloading] ? Color.Lime : Color.Gray;

                lbDisplaceValue.ColorLight = cDEF.Run.Digital.Input[cDI.Lens_Height_Unit_Go] ? Color.Lime : Color.Red;
                if (cDEF.Serials.strValue == "ERR")
                {
                    lbDisplaceValue.Text = "ERR";
                }
                else
                {
                    lbDisplaceValue.Text = cDEF.Serials.Value.ToString("N3");
                }

                if (cDEF.Work.DispSensor.DispenserType == 0)
                {
                    lbDisplaceValueBond1.Visible = false;
                    lbDisplaceValueBond2.Visible = false;
                }
                else
                {

                    if (cDEF.Serials.strValue_Bond1 == "ERR")
                    {
                        lbDisplaceValueBond1.Text = "ERR";
                        lbDisplaceValueBond1.ColorLight = Color.Red;
                    }
                    else
                    {
                        lbDisplaceValueBond1.Text = (cDEF.Serials.Value_Bond1 / 1000.0).ToString("N3");
                        lbDisplaceValueBond1.ColorLight = Color.Lime;
                    }

                    if (cDEF.Serials.strValue_Bond2 == "ERR")
                    {
                        lbDisplaceValueBond2.Text = "ERR";
                        lbDisplaceValueBond2.ColorLight = Color.Red;
                    }
                    else
                    {
                        lbDisplaceValueBond2.Text = (cDEF.Serials.Value_Bond2 / 1000.0).ToString("N3");
                        lbDisplaceValueBond2.ColorLight = Color.Lime;
                    }
                }
                // [2020.1112.1] Add / Display Lens Z Torque 
                lbDisplaceValueLensZTorque.Text = (cDEF.Run.LensPicker.HeadZ.TorqueValue).ToString();
                lbDisplaceValueLensZTorque.ColorLight = Color.Red;


                if (cDEF.Run.PlateAngle.Information.Value * 60 > cDEF.Work.PlateAngle.MaxLimit || cDEF.Run.PlateAngle.Information.Value * 60 < cDEF.Work.PlateAngle.MinLimit) //ksyoon
                {
                    lbSideAngleValue.Text = "ERR";
                    lbSideAngleValue.ColorLight = Color.Red;
                }
                else
                {
                    lbSideAngleValue.ColorLight = Color.Lime;
                    lbSideAngleValue.Text = (cDEF.Run.PlateAngle.Information.Value * 60).ToString("N3");
                }

                #endregion

                #region Stage Display
                lbLensTrayExist.BackColor = cDEF.Run.LensLoader.Information.ExistStage ? Color.Lime : Color.Gray;
                lbVCMTrayExist.BackColor = cDEF.Run.VCMLoader.Information.ExistStage ? Color.Lime : Color.Gray;
                lbUnloadTrayExist.BackColor = cDEF.Run.Unloader.Information.ExistStage ? Color.Lime : Color.Gray;


                if (cDEF.Run.LensLoader.Information.ExistStage)
                {
                    if (cDEF.Work.Recipe.LensMagazineDirection)
                        lbLensTrayExist.Text = $"SLOT {cDEF.Work.LensLoader.SlotCount - cDEF.Run.LensLoader.Information.WorkTray}";
                    else
                        lbLensTrayExist.Text = $"SLOT {cDEF.Run.LensLoader.Information.WorkTray + 1}";
                }
                else
                    lbLensTrayExist.Text = "";

                if (cDEF.Run.VCMLoader.Information.ExistStage)
                {
                    if (cDEF.Work.Recipe.LoaderMagazineDirection)
                        lbVCMTrayExist.Text = $"SLOT {cDEF.Work.VCMLoader.SlotCount - cDEF.Run.VCMLoader.Information.WorkTray}";
                    else
                        lbVCMTrayExist.Text = $"SLOT {cDEF.Run.VCMLoader.Information.WorkTray + 1}";
                }
                    
                else
                    lbVCMTrayExist.Text = "";

                if (cDEF.Run.Unloader.Information.ExistStage)
                {
                    if (cDEF.Work.Recipe.UnLoaderMagazineDirection)
                        lbUnloadTrayExist.Text = $"SLOT {cDEF.Work.Unloader.SlotCount - cDEF.Run.Unloader.Information.WorkTray}";
                    else
                        lbUnloadTrayExist.Text = $"SLOT {cDEF.Run.Unloader.Information.WorkTray + 1}";
                }
                else
                    lbUnloadTrayExist.Text = "";
                #endregion
                #region Head Display
                lbVCMHeadStatus.ImageIndex = cDEF.Run.VCMPicker.Information.HeadStatus == TrayStatus.Empty ? 4 : 7;
                lbLensHeadStatus.ImageIndex = cDEF.Run.LensPicker.Information.HeadStatus == TrayStatus.Empty ? 4 : 7;
                lbUnloadHeadStatus.ImageIndex = cDEF.Run.UnloadPicker.Information.HeadLensData.Status == LensTrayStatus.Empty ? 4 : 7;
                lbPlateAngleExist.ImageIndex = cDEF.Run.PlateAngle.Information.LensData.Status == LensTrayStatus.Empty ? 4 : 7;
                #endregion
                #region Index Display
                if (cDEF.Run.CleanJig.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbCleanJigStatus.ImageIndex = 4;
                else if (cDEF.Run.CleanJig.Information.IndexData.Status == eLensIndexStatus.UnloadFinish)
                    lbCleanJigStatus.ImageIndex = 5;
                else if (cDEF.Run.CleanJig.Information.IndexData.Status == eLensIndexStatus.CleanJigFinish)
                    lbCleanJigStatus.ImageIndex = 7;
                else
                    lbCleanJigStatus.ImageIndex = 6;

                if (cDEF.Run.Index.Information.VCMData.Status == eLensIndexStatus.Empty)
                    lbVCMLoadStatus.ImageIndex = 4;
                else if (cDEF.Run.Index.Information.VCMData.Status == eLensIndexStatus.VCMLoaded)
                    lbVCMLoadStatus.ImageIndex = 7;
                else
                    lbVCMLoadStatus.ImageIndex = 6;

                if (cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbJigPlateAngleStatus.ImageIndex = 4;
                else if (cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.VCMLoaded)
                    lbJigPlateAngleStatus.ImageIndex = 5;
                else if (cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.JigPlateAngleFinish)
                    lbJigPlateAngleStatus.ImageIndex = 7;
                else
                    lbJigPlateAngleStatus.ImageIndex = 6;

                if (cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbAct3Status.ImageIndex = 4;
                else if (cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.JigPlateAngleFinish)
                    lbAct3Status.ImageIndex = 5;
                else if (cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.Act3Finish)
                    lbAct3Status.ImageIndex = 7;
                else
                    lbAct3Status.ImageIndex = 6;

                if (cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.Empty)
                    lbLensInsertStatus.ImageIndex = 4;
                else if (cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.Act3Finish)
                    lbLensInsertStatus.ImageIndex = 5;
                else if (cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.AssembleFinish)
                    lbLensInsertStatus.ImageIndex = 7;
                else
                    lbLensInsertStatus.ImageIndex = 6;

                if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbLensHeightStatus.ImageIndex = 4;
                else if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.AssembleFinish)
                    lbLensHeightStatus.ImageIndex = 5;
                else if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                    lbLensHeightStatus.ImageIndex = 7;
                else
                    lbLensHeightStatus.ImageIndex = 6;

                if (cDEF.Run.Bonder1.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbBonder1Status.ImageIndex = 4;
                else if (cDEF.Run.Bonder1.Information.IndexData.Status == eLensIndexStatus.LensHeightFinish)
                    lbBonder1Status.ImageIndex = 5;
                else if (cDEF.Run.Bonder1.Information.IndexData.Status == eLensIndexStatus.Bonder1Finish)
                    lbBonder1Status.ImageIndex = 7;
                else
                    lbBonder1Status.ImageIndex = 6;

                if (cDEF.Run.Bonder2.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbBonder2Status.ImageIndex = 4;
                else if (cDEF.Run.Bonder2.Information.IndexData.Status == eLensIndexStatus.Bonder1Finish)
                    lbBonder2Status.ImageIndex = 5;
                else if (cDEF.Run.Bonder2.Information.IndexData.Status == eLensIndexStatus.Bonder2Finish)
                    lbBonder2Status.ImageIndex = 7;
                else
                    lbBonder2Status.ImageIndex = 6;

                if (cDEF.Run.VisionInspect.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbVisionInsStatus.ImageIndex = 4;
                else if (cDEF.Run.VisionInspect.Information.IndexData.Status == eLensIndexStatus.Bonder2Finish)
                    lbVisionInsStatus.ImageIndex = 5;
                else if (cDEF.Run.VisionInspect.Information.IndexData.Status == eLensIndexStatus.VisionInspectFinish)
                    lbVisionInsStatus.ImageIndex = 7;
                else
                    lbVisionInsStatus.ImageIndex = 6;

                if (cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbCure1Status.ImageIndex = 4;
                else if (cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.VisionInspectFinish)
                    lbCure1Status.ImageIndex = 5;
                else if (cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.Curing1Finish)
                    lbCure1Status.ImageIndex = 7;
                else
                    lbCure1Status.ImageIndex = 6;

                if (cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbCure2Status.ImageIndex = 4;
                else if (cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.Curing1Finish)
                    lbCure2Status.ImageIndex = 5;
                else if (cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.Curing2Finish)
                    lbCure2Status.ImageIndex = 7;
                else
                    lbCure2Status.ImageIndex = 6;

                if (cDEF.Run.PlateAngle.Information.IndexData.Status == eLensIndexStatus.Empty)
                    lbPlateAngleStatus.ImageIndex = 4;
                else if (cDEF.Run.PlateAngle.Information.IndexData.Status == eLensIndexStatus.Curing2Finish)
                    lbPlateAngleStatus.ImageIndex = 5;
                else if (cDEF.Run.PlateAngle.Information.IndexData.Status == eLensIndexStatus.UnloadFinish)
                    lbPlateAngleStatus.ImageIndex = 7;
                else
                    lbPlateAngleStatus.ImageIndex = 6;




                #endregion

                #region Door Display
                if (cDEF.Work.Option.DoorAlarmDispMode == 0)
                {
                    lbDoor1.BackColor = Color.White;
                    lbDoor2.BackColor = Color.White;
                    lbDoor4.BackColor = Color.White;
                    lbDoor3.BackColor = Color.White;
                    lbDoor5.BackColor = Color.White;
                    lbDoor6.BackColor = Color.White;
                    lbDoor7.BackColor = Color.White;
                    lbDoor8.BackColor = Color.White;
                }
                else if (cDEF.Work.Option.DoorAlarmDispMode == 1)
                {
                    lbDoor1.BackColor = cDEF.Run.Digital.Input[cDI.Front_Door1_State] ? Color.White : Color.OrangeRed;
                    lbDoor2.BackColor = cDEF.Run.Digital.Input[cDI.Front_Door2_State] ? Color.White : Color.OrangeRed;
                    lbDoor4.BackColor = cDEF.Run.Digital.Input[cDI.Left_Side_Door3_State] ? Color.White : Color.OrangeRed;
                    lbDoor3.BackColor = cDEF.Run.Digital.Input[cDI.Left_Side_Door4_State] ? Color.White : Color.OrangeRed;
                    lbDoor5.BackColor = cDEF.Run.Digital.Input[cDI.Right_Side_Door5_State] ? Color.White : Color.OrangeRed;
                    lbDoor6.BackColor = cDEF.Run.Digital.Input[cDI.Right_Side_Door6_State] ? Color.White : Color.OrangeRed;
                    lbDoor7.BackColor = cDEF.Run.Digital.Input[cDI.Rear_Door7_State] ? Color.White : Color.OrangeRed;
                    lbDoor8.BackColor = cDEF.Run.Digital.Input[cDI.Rear_Door8_State] ? Color.White : Color.OrangeRed;
                }
                else if (cDEF.Work.Option.DoorAlarmDispMode == 2)
                {
                    if (!cDEF.Run.Digital.Input[cDI.Front_Door1_State])
                        lbDoor1.BackColor = cDEF.Run.Blink ? Color.OrangeRed : Color.Aqua;
                    else
                        lbDoor1.BackColor = Color.White;

                    if (!cDEF.Run.Digital.Input[cDI.Front_Door2_State])
                        lbDoor2.BackColor = cDEF.Run.Blink ? Color.OrangeRed : Color.Aqua;
                    else
                        lbDoor2.BackColor = Color.White;

                    if (!cDEF.Run.Digital.Input[cDI.Left_Side_Door3_State])
                        lbDoor4.BackColor = cDEF.Run.Blink ? Color.OrangeRed : Color.Aqua;
                    else
                        lbDoor4.BackColor = Color.White;

                    if (!cDEF.Run.Digital.Input[cDI.Left_Side_Door4_State])
                        lbDoor3.BackColor = cDEF.Run.Blink ? Color.OrangeRed : Color.Aqua;
                    else
                        lbDoor3.BackColor = Color.White;

                    if (!cDEF.Run.Digital.Input[cDI.Right_Side_Door5_State])
                        lbDoor5.BackColor = cDEF.Run.Blink ? Color.OrangeRed : Color.Aqua;
                    else
                        lbDoor5.BackColor = Color.White;

                    if (!cDEF.Run.Digital.Input[cDI.Right_Side_Door6_State])
                        lbDoor6.BackColor = cDEF.Run.Blink ? Color.OrangeRed : Color.Aqua;
                    else
                        lbDoor6.BackColor = Color.White;

                    if (!cDEF.Run.Digital.Input[cDI.Rear_Door7_State])
                        lbDoor7.BackColor = cDEF.Run.Blink ? Color.OrangeRed : Color.Aqua;
                    else
                        lbDoor7.BackColor = Color.White;

                    if (!cDEF.Run.Digital.Input[cDI.Rear_Door8_State])
                        lbDoor8.BackColor = cDEF.Run.Blink ? Color.OrangeRed : Color.Aqua;
                    else
                        lbDoor8.BackColor = Color.White;

                }

                #endregion
                //btnUvLamp.Text = cDEF.Run.UnloadPicker.Information.ChartTotalCount.ToString();
                btnUvLamp.ForeColor = cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On] ? Color.Orange : Color.Black;
                btnUVLamp2.ForeColor = cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On] ? Color.Orange : Color.Black;

                lbAct1Mode.Text = cDEF.Work.Project.GlobalOption.Actuator_1_Mode ? "Mode 1" : "Mode 2";
                lbAct1Ready.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_1_Ready] ? Color.Lime : Color.White;
                lbAct1Pass.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_1_Pass] ? Color.Lime : Color.White;
                lbAct1Fail.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_1_Fail] ? Color.Red : Color.White;

                lbAct2Mode.Text = cDEF.Work.Project.GlobalOption.Actuator_2_Mode ? "Mode 1" : "Mode 2";
                lbAct2Ready.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_2_Ready] ? Color.Lime : Color.White;
                lbAct2Pass.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_2_Pass] ? Color.Lime : Color.White;
                lbAct2Fail.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_2_Fail] ? Color.Red : Color.White;

                lbAct3Mode.Text = cDEF.Work.Project.GlobalOption.Actuator_3_Mode ? "Mode 1" : "Mode 2";
                lbAct3Ready.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_3_Ready] ? Color.Lime : Color.White;
                lbAct3Pass.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_3_Pass] ? Color.Lime : Color.White;
                lbAct3Fail.BackColor = cDEF.Run.Digital.Input[cDI.Actuator_3_Fail] ? Color.Red : Color.White;

                lbInnerLight.BackColor = cDEF.Run.Digital.Output[cDO.InnerLight] ? Color.Lime : Color.Gray;

                btnLotEnd.ForeColor = cDEF.Run.LotEnd ? Color.OrangeRed : Color.Black;
                lbIndexNum.Text = $"{cDEF.Run.Index.Information.IndexNum + 1}";

                //Chart
                lbLensHeightSetPorductNumber.Text = LensHeightChartMaximum_X.ToString();

                Yield_Chart.Series[0].IsValueShownAsLabel = true;

                #region Measuring

                int VcmIndex = (cDEF.Run.Index.Information.IndexNum + 11) % 12;

                for (int i = 0; i < 12; i++)
                {
                    if (cDEF.Run.JigPlateAngle.Information.AngleData[i] != null)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            tpanel__Measuring.Controls[$"blAngleValue_{i + 1}_{j+1}"].Text = cDEF.Run.JigPlateAngle.Information.AngleData[i][j].ToString("N3");
                        }

                        int imgIndx = (VcmIndex + i ) % 12;
                        tabPage3.Controls[$"blImgAngleValue_{i}"].Text =$"#{imgIndx+1}:{cDEF.Run.JigPlateAngle.Information.AngleData[imgIndx][0].ToString("N3")}";
                        
                    }
                }

                for (int i = 1; i < 5; i++)
                {
                    tabPage3.Controls[$"lblZValue{i}"].Text = $"#{i}: {cDEF.SideAngleMeasuring.Jig_PeakZ_Value[i - 1].ToString("N3")}";
                }


                #endregion Measuring

                #region Tray Change Image
                pnVCMTrayChange.Visible = (cDEF.Run.VCMLoader.Mode == TRunVCMLoaderMode.Loading || cDEF.Run.VCMLoader.Mode == TRunVCMLoaderMode.Unloading) ? true : false;
                pnLensTrayChange.Visible = (cDEF.Run.LensLoader.Mode == TRunLensLoaderMode.Loading || cDEF.Run.LensLoader.Mode == TRunLensLoaderMode.Unloading) ? true : false;
                pnUnloadTrayChange.Visible = (cDEF.Run.Unloader.Mode == TRunUnloaderMode.Loading || cDEF.Run.Unloader.Mode == TRunUnloaderMode.Unloading) ? true : false;

                lbVcmTrayChangeText.Text = (cDEF.Run.VCMLoader.Mode == TRunVCMLoaderMode.Loading) ? "Tray Loading..." : "Tray Unloading...";
                lbLensTrayChangeText.Text = (cDEF.Run.LensLoader.Mode == TRunLensLoaderMode.Loading) ? "Tray Loading..." : "Tray Unloading...";
                lbUnloadTrayChangeText.Text = (cDEF.Run.Unloader.Mode == TRunUnloaderMode.Loading) ? "Tray Loading..." : "Tray Unloading...";

                int Temp = (int)(((double)cDEF.Run.VCMLoader.Step / 12.0) * 100.0);
                if (Temp > 100) Temp = 100;
                pbVcmTray.Value = Temp;

                Temp = (int)(((double)cDEF.Run.LensLoader.Step / 12.0) * 100.0);
                if (Temp > 100) Temp = 100;
                pbLensTray.Value = Temp;

                Temp = (int)(((double)cDEF.Run.Unloader.Step / 12.0) * 100.0);
                if (Temp > 100) Temp = 100;
                pbUnloadTray.Value = Temp;

                lbDailyTotalCount.Text = cDEF.Run.UnloadPicker.Information.DailyTotalCount.ToString();
                lbDailyOKCount.Text = cDEF.Run.UnloadPicker.Information.DailyOKCount.ToString();
                lbDailyNGCount.Text = cDEF.Run.UnloadPicker.Information.DailyNGCount.ToString();

                lbUVLamp1LifeTime.Visible = cDEF.Run.Digital.Input[cDI.UV_1_LampLife] ? true : false;
                lbUVLamp2LifeTime.Visible = cDEF.Run.Digital.Input[cDI.UV_2_LampLife] ? true : false;

                lbUVLamp1LifeTime.BackColor = cDEF.Run.Blink ? Color.Red : Color.White;
                lbUVLamp2LifeTime.BackColor = cDEF.Run.Blink ? Color.Red : Color.White;

                lbBond1TotalCount.Text = string.Format("{0:###,###,###,###,###,###,###}", cDEF.Work.Bonder1.JettingCount);
                lbBond2TotalCount.Text = string.Format("{0:###,###,###,###,###,###,###}", cDEF.Work.Bonder2.JettingCount);

                lbBond1JetCount.Text = string.Format("{0:###,###,###,###,###,###,###}", cDEF.Run.Bonder1.Information.JettingCount);
                lbBond2JetCount.Text = string.Format("{0:###,###,###,###,###,###,###}", cDEF.Run.Bonder2.Information.JettingCount);
                Temp = 0;
                if(cDEF.Work.Bonder1.JettingCount > 0)
                    Temp = (int)(((double)cDEF.Run.Bonder1.Information.JettingCount / (double)cDEF.Work.Bonder1.JettingCount) * 100);
                if (Temp > 100) Temp = 100;
                proBonder1.Value = 100 - Temp;

                Temp = 0;
                if (cDEF.Work.Bonder2.JettingCount > 0)
                    Temp = (int)(((double)cDEF.Run.Bonder2.Information.JettingCount / (double)cDEF.Work.Bonder2.JettingCount) * 100);
                if (Temp > 100) Temp = 100;
                proBonder2.Value = 100 - Temp;
                #endregion

#if Notebook
                btnTestButton.Visible = true;
                btnTestButton.Text = cDEF.Run.UnloadPicker.Information.ChartOkCount[2].ToString();
#else
                btnTestButton.Visible = false;
#endif

                btnUnloadTHome.Visible = cDEF.Run.UnloadPicker.Information.HeadOverLoad ? true : false;

               
                if (cDEF.Work.Project.GlobalOption.UseMES)
                {
                    lblDevice.Text = cDEF.Mes.Device;
                    lblOperation.Text = cDEF.Mes.Operation;
                    lblProdType.Text = cDEF.Mes.Product_Type;
                    lblmeseqpid.Text = cDEF.Mes.EQPName;
                    if (cDEF.Mes.ConStatsus)
                    {
                        lblUseMES.BackColor = Color.Lime;
                    }
                    else
                        lblUseMES.BackColor = Color.White;
                }
                else
                {
                    lblUseMES.BackColor = Color.White;
                }
            }));
        }
        private List<Label> ListVCMLabel;
        private List<Label> ListLensLabel;
        private List<Label> ListUnloadLabel;
        public void VCMMagazineObjectInit()
        {
            ListVCMLabel.Clear();
            pnVCMMagazine.Controls.Clear();

            Label labeltitle = new Label();
            labeltitle.Name = "lbVCM_MagazineTitle";
            labeltitle.Font = new Font("Tahoma", 9, FontStyle.Bold);
            labeltitle.Text = "VCM MAGAZINE";
            labeltitle.TextAlign = ContentAlignment.MiddleCenter;
            labeltitle.FlatStyle = FlatStyle.Standard;
            labeltitle.BorderStyle = BorderStyle.None;
            labeltitle.AutoSize = false;
            labeltitle.BackColor = Color.Gainsboro;
            labeltitle.ForeColor = Color.Navy;
            labeltitle.Size = new Size(pnVCMMagazine.Width - 2, 24);
            labeltitle.Location = new Point(1, 1);
            pnVCMMagazine.Controls.Add(labeltitle);

            for (int i = 0; i < cDEF.Work.VCMLoader.SlotCount; i++)
            {
                Label label = new Label();
                label.TabIndex = i;
                if (!cDEF.Work.Recipe.LoaderMagazineDirection)
                {
                label.Name = $"lbVCM_Slot{cDEF.Work.VCMLoader.SlotCount - i}";
                label.Text = $"Slot #{cDEF.Work.VCMLoader.SlotCount - i}";
                }
                else
                {
                label.Name = $"lbVCM_Slot{i + 1}";
                label.Text = $"Slot #{i + 1}";
                 }

                label.TextAlign = ContentAlignment.MiddleCenter;
                label.FlatStyle = FlatStyle.Flat;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.AutoSize = false;

                //Cst Size
                label.Width = pnVCMMagazine.Width - 10;
                label.Height = (pnVCMMagazine.Height - 34  ) / cDEF.Work.VCMLoader.SlotCount; //- (1* cDEF.Work.VCMLoader.SlotCount)
                label.ForeColor = Color.Black;
                label.BackColor = Color.Aqua;
                
                int Top = 30 + (i * label.Height) + 2;
                label.Location = new Point(5, Top);
                label.Visible = true;

                label.Click += new EventHandler(VCM_Magazine_Click);

                pnVCMMagazine.Controls.Add(label);
                ListVCMLabel.Add(label);
            }
        }
        public void LensMagazineObjectInit()
        {
            ListLensLabel.Clear();
            pnLensMagazine.Controls.Clear();

            Label labeltitle = new Label();
            labeltitle.Name = "lbLens_MagazineTitle";
            labeltitle.Font = new Font("Tahoma", 9, FontStyle.Bold);
            labeltitle.Text = "LENS MAGAZINE";
            labeltitle.TextAlign = ContentAlignment.MiddleCenter;
            labeltitle.FlatStyle = FlatStyle.Standard;
            labeltitle.BorderStyle = BorderStyle.None;
            labeltitle.AutoSize = false;
            labeltitle.BackColor = Color.Gainsboro;
            labeltitle.ForeColor = Color.Navy;
            labeltitle.Size = new Size(pnLensMagazine.Width - 2, 24);
            labeltitle.Location = new Point(1, 1);
            pnLensMagazine.Controls.Add(labeltitle);

            for (int i = 0; i < cDEF.Work.LensLoader.SlotCount; i++)
            {
                Label label = new Label();
                label.TabIndex = i;
                if (!cDEF.Work.Recipe.LensMagazineDirection)
                {
                    label.Name = $"lbLens_Slot{cDEF.Work.LensLoader.SlotCount - i}";
                    label.Text = $"Slot #{cDEF.Work.LensLoader.SlotCount - i}";
                }
                else
                {
                    label.Name = $"lbLens_Slot{i + 1}";
                    label.Text = $"Slot #{i + 1}";
                }
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.FlatStyle = FlatStyle.Flat;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.AutoSize = false;

                //Cst Size
                label.Width = pnLensMagazine.Width - 10;
                label.Height = (pnLensMagazine.Height - 34) / cDEF.Work.LensLoader.SlotCount; 
                label.ForeColor = Color.Black;
                label.BackColor = Color.Aqua;

                int Top = 30 + (i * label.Height) + 2;
                label.Location = new Point(5, Top);
                label.Visible = true;

                label.Click += new EventHandler(Lens_Magazine_Click);

                pnLensMagazine.Controls.Add(label);
                ListLensLabel.Add(label);
            }
        }
        public void UnloadMagazineObjectInit()
        {
            ListUnloadLabel.Clear();
            pnUnloadMagazine.Controls.Clear();

            Label labeltitle = new Label();
            labeltitle.Name = "lbVCM_MagazineTitle";
            labeltitle.Font = new Font("Tahoma", 9, FontStyle.Bold);
            labeltitle.Text = "UNLOAD MAGAZINE";
            labeltitle.TextAlign = ContentAlignment.MiddleCenter;
            labeltitle.FlatStyle = FlatStyle.Standard;
            labeltitle.BorderStyle = BorderStyle.None;
            labeltitle.AutoSize = false;
            labeltitle.BackColor = Color.Gainsboro;
            labeltitle.ForeColor = Color.Navy;
            labeltitle.Size = new Size(pnUnloadMagazine.Width - 2, 24);
            labeltitle.Location = new Point(1, 1);
            pnUnloadMagazine.Controls.Add(labeltitle);

            for (int i = 0; i < cDEF.Work.Unloader.SlotCount; i++)
            {
                Label label = new Label();
                label.TabIndex = i;
                if (!cDEF.Work.Recipe.UnLoaderMagazineDirection)
                {
                    label.Name = $"lbUnload_Slot{cDEF.Work.Unloader.SlotCount - i}";
                    label.Text = $"Slot #{cDEF.Work.Unloader.SlotCount - i}";
                }
                else
                {
                    label.Name = $"lbUnload_Slot{i + 1}";
                    label.Text = $"Slot #{i + 1}";
                }
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.FlatStyle = FlatStyle.Flat;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.AutoSize = false;

                //Cst Size
                label.Width = pnUnloadMagazine.Width - 10;
                label.Height = (pnUnloadMagazine.Height - 34) / cDEF.Work.Unloader.SlotCount;
                label.ForeColor = Color.Black;
                label.BackColor = Color.Aqua;

                label.Click += new EventHandler(Unloader_Magazine_Click);

                int Top = 30 + (i * label.Height) + 2;
                label.Location = new Point(5, Top);
                label.Visible = true;           pnUnloadMagazine.Controls.Add(label);
                ListUnloadLabel.Add(label);
            }
        }
        private int DisplayMove(double AMCurrent, double AMBegin, double AMWidth, double AUBegin, double AUWidth)
        {
            if (AMWidth == 0)
                return 0;

            return Convert.ToInt32(AUBegin - ((AUWidth / AMWidth) * (AMCurrent - AMBegin)));
        }
#region Tray Display
        public void VCMTrayMapInit()
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    int Boundary = 10;
                    int gap = 5;
                    Graphics g = Graphics.FromImage(VCMbitmap);
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, VCMbitmap.Width, VCMbitmap.Height));

                    int CountX = cDEF.Work.VCMLoader.TrayCountX;
                    int CountY = cDEF.Work.VCMLoader.TrayCountY;

                    int SizeX = (int)((double)(picVCMMap.Width - Boundary * 2) / (double)(CountX)) - gap;
                    int SizeY = (int)((double)(picVCMMap.Height - Boundary * 2) / (double)(CountY)) - gap;

                    for (int y = 0; y < CountY; y++)
                    {
                        for (int x = 0; x < CountX; x++)
                        {
                            int orgx = Boundary + (x * (SizeX + gap));
                            int orgy = Boundary + (y * (SizeY + gap));
                            g.DrawRectangle(new Pen(Brushes.White), new Rectangle(orgx, orgy, SizeX, SizeY));
                            g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        }
                    }
                    g.Dispose();
                    picVCMMap.Refresh();
                }));
            }
            catch
            {

            }
        }
        public void LensTrayMapInit()
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    int Boundary = 10;
                    int gap = 5;
                    Graphics g = Graphics.FromImage(Lensbitmap);
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, Lensbitmap.Width, Lensbitmap.Height));

                    int CountX = cDEF.Work.LensLoader.TrayCountX;
                    int CountY = cDEF.Work.LensLoader.TrayCountY;

                    int SizeX = (int)((double)(picLensMap.Width - Boundary * 2) / (double)(CountX)) - gap;
                    int SizeY = (int)((double)(picLensMap.Height - Boundary * 2) / (double)(CountY)) - gap;

                    for (int y = 0; y < CountY; y++)
                    {
                        for (int x = 0; x < CountX; x++)
                        {
                            int orgx = Boundary + (x * (SizeX + gap));
                            int orgy = Boundary + (y * (SizeY + gap));
                            g.DrawEllipse(new Pen(Brushes.White), new Rectangle(orgx, orgy, SizeX, SizeY));
                            g.FillEllipse(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        }
                    }
                    g.Dispose();
                    picLensMap.Refresh();
                }));
            }
            catch
            {
            }
        }
        public void UnloadTrayMapInit()
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    int Boundary = 10;
                    int gap = 5;
                    Graphics g = Graphics.FromImage(Unloadbitmap);
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, Unloadbitmap.Width, Unloadbitmap.Height));

                    int CountX = cDEF.Work.Unloader.TrayCountX;
                    int CountY = cDEF.Work.Unloader.TrayCountY;

                    int SizeX = (int)((double)(picUnloadMap.Width - Boundary * 2) / (double)(CountX)) - gap;
                    int SizeY = (int)((double)(picUnloadMap.Height - Boundary * 2) / (double)(CountY)) - gap;

                    for (int y = 0; y < CountY; y++)
                    {
                        for (int x = 0; x < CountX; x++)
                        {
                            int orgx = Boundary + (x * (SizeX + gap));
                            int orgy = Boundary + (y * (SizeY + gap));
                            g.DrawRectangle(new Pen(Brushes.White), new Rectangle(orgx, orgy, SizeX, SizeY));
                            g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                            g.DrawEllipse(new Pen(Brushes.White), new Rectangle(orgx, orgy, SizeX, SizeY));
                            g.FillEllipse(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        }
                    }
                    g.Dispose();
                    picUnloadMap.Refresh();
                }));
            }
            catch
            {

            }
        }
        public void NgTrayMapInit()
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    int Boundary = 10;
                    int gap = 15;
                    int gapy = 5;
                    Graphics g = Graphics.FromImage(Ngbitmap);
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, Ngbitmap.Width, Ngbitmap.Height));

                    int CountX = cDEF.Work.Unloader.NG_TrayCountX;
                    int CountY = cDEF.Work.Unloader.NG_TrayCountY;

                    int SizeX = (int)((double)(picNGMap.Width - Boundary * 2) / (double)(CountX)) - gap;
                    int SizeY = (int)((double)(picNGMap.Height - Boundary * 2) / (double)(CountY)) - gapy;

                    for (int y = 0; y < CountY; y++)
                    {
                        for (int x = 0; x < CountX; x++)
                        {
                            int orgx = 6 + Boundary + (x * (SizeX + gap));
                            int orgy = 6 + Boundary + (y * (SizeY + gapy));
                            //int orgy = picNGMap.Height / 2 - SizeX / 2;
                            g.DrawRectangle(new Pen(Brushes.White), new Rectangle(orgx, orgy, SizeX, SizeY));
                            g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                            g.DrawEllipse(new Pen(Brushes.White), new Rectangle(orgx, orgy, SizeX, SizeY));
                            g.FillEllipse(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        }
                    }
                    g.Dispose();
                    picNGMap.Refresh();
                }));
            }
            catch
            {

            }
        }
        public void VCMTrayMapDisplay(int IndexX, int IndexY, LensTrayStatus status)
        {
            this.Invoke(new Action(delegate ()
            {
                int Boundary = 10;
                int gap = 5;
                Graphics g = Graphics.FromImage(VCMbitmap);

                int CountX = cDEF.Work.VCMLoader.TrayCountX;
                int CountY = cDEF.Work.VCMLoader.TrayCountY;

                int SizeX = (int)((double)(picVCMMap.Width - Boundary * 2) / (double)(CountX)) - gap;
                int SizeY = (int)((double)(picVCMMap.Height - Boundary * 2) / (double)(CountY)) - gap;

                int orgx = Boundary + (IndexX * (SizeX + gap));
                int orgy = Boundary + (IndexY * (SizeY + gap));

                if (status == LensTrayStatus.Empty)
                    g.FillRectangle(Brushes.Black, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                else if (status == LensTrayStatus.Load)
                    g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                else if (status == LensTrayStatus.Finish)
                    g.FillRectangle(Brushes.Fuchsia, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));

                lbVCMTrayIndexXTitle.Text = IndexX.ToString();
                lbVCMTrayIndexYTitle.Text = IndexY.ToString();
                g.Dispose();
                picVCMMap.Refresh();
            }));
        }

        public void LensTrayMapDisplay(int IndexX, int IndexY, LensTrayStatus status)
        {
            this.Invoke(new Action(delegate ()
            {
                int Boundary = 10;
                int gap = 5;
                Graphics g = Graphics.FromImage(Lensbitmap);

                int CountX = cDEF.Work.LensLoader.TrayCountX;
                int CountY = cDEF.Work.LensLoader.TrayCountY;

                int SizeX = (int)((double)(picLensMap.Width - Boundary * 2) / (double)(CountX)) - gap;
                int SizeY = (int)((double)(picLensMap.Height - Boundary * 2) / (double)(CountY)) - gap;

                int orgx = Boundary + (IndexX * (SizeX + gap));
                int orgy = Boundary + (IndexY * (SizeY + gap));

                if (status == LensTrayStatus.Empty)
                    g.FillEllipse(Brushes.Black, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                else if (status == LensTrayStatus.Load)
                    g.FillEllipse(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                else if (status == LensTrayStatus.Finish)
                    g.FillEllipse(Brushes.Fuchsia, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                g.Dispose();
                picLensMap.Refresh();
            }));
        }

        public void UnloadTrayMapDisplay(int IndexX, int IndexY, LensTrayStatus status)
        {
            this.Invoke(new Action(delegate ()
            {
                int Boundary = 10;
                int gap = 5;
                Graphics g = Graphics.FromImage(Unloadbitmap);

                int CountX = cDEF.Work.Unloader.TrayCountX;
                int CountY = cDEF.Work.Unloader.TrayCountY;

                int SizeX = (int)((double)(picUnloadMap.Width - Boundary * 2) / (double)(CountX)) - gap;
                int SizeY = (int)((double)(picUnloadMap.Height - Boundary * 2) / (double)(CountY)) - gap;

                int orgx = Boundary + (IndexX * (SizeX + gap));
                int orgy = Boundary + (IndexY * (SizeY + gap));

                if (status == LensTrayStatus.Empty)
                {
                    g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                    g.FillEllipse(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                }
                else if (status == LensTrayStatus.Load)
                {
                    g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                    g.DrawEllipse(new Pen(Brushes.White), new Rectangle(orgx, orgy, SizeX, SizeY));
                    g.FillEllipse(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                }
                else if (status == LensTrayStatus.Finish)
                {
                    g.FillRectangle(Brushes.Blue, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                    g.FillEllipse(Brushes.Yellow, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                }
                g.Dispose();
                picUnloadMap.Refresh();
            }));
        }

        public void NgTrayMapDisplay(int IndexX, int IndexY, eFailType failType)
        {
            this.Invoke(new Action(delegate ()
            {
                int Boundary = 10;
                int gap = 15;
                int gapy = 5;
                Graphics g = Graphics.FromImage(Ngbitmap);

                int CountX = cDEF.Work.Unloader.NG_TrayCountX;
                int CountY = cDEF.Work.Unloader.NG_TrayCountY;

                int SizeX = (int)((double)(picNGMap.Width - Boundary * 2) / (double)(CountX)) - gap;
                int SizeY = (int)((double)(picNGMap.Height - Boundary * 2) / (double)(CountY)) - gapy;

                int orgx = 6 + Boundary + (IndexX * (SizeX + gap));
                int orgy = 6 + Boundary + (IndexY * (SizeY + gapy));

                switch(failType)
                {
                    case eFailType.None:
                        g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        g.FillEllipse(Brushes.Cyan, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        break;

                    case eFailType.AssembleFail:
                    case eFailType.LensHeightFail:
                        g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        g.FillEllipse(Brushes.Orange, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        break;
                    case eFailType.VisionInspectFail:
                        g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        g.FillEllipse(Brushes.Gold, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        break;
                    case eFailType.Actuating1Fail:
                        g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        g.FillEllipse(Brushes.Purple, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        break;
                    case eFailType.PlateAngleFail:
                        g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        g.FillEllipse(Brushes.Red, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        break;
                    case eFailType.Actuating2Fail:
                        g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        g.FillEllipse(Brushes.Lime, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        break;
                    case eFailType.SideHeightFail: //Adding Error
                        g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        g.FillEllipse(Brushes.Blue, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        break;
                    default:
                        g.FillRectangle(Brushes.Gray, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        g.FillEllipse(Brushes.Red, new Rectangle(orgx + 1, orgy + 1, SizeX - 2, SizeY - 2));
                        break;
                }

                g.Dispose();
                picNGMap.Refresh();
            }));
        }
#endregion


        String FButton;
        private void btnEnable_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32((sender as Glass.GlassButton).Tag))
            {
                case 0: // Initialze

                    //SkindeAngle Picker 베큠 체크해서 자재 정보 확인 후 홈 동작
                    if (cDEF.Run.Digital.Input[cDI.Side_Angle_Measure_Unloading_Vacuum_Check])
                    {
                        XModuleMain.frmBox.MessageBox("Warning", " Product Detected ", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }

                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (cDEF.Run.DetailMode == TfpRunningMode.frmNone || cDEF.Run.DetailMode == TfpRunningMode.frmStop)
                    {
                        if (XModuleMain.frmBox.MessageBox("INITILIZE", "Start Initialize?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        {
                      
                            //foreach (TfpMotionItem Item in cDEF.Run.Motion.FItems)
                            //{
                            //    Item.Active = false;
                            //}
                            //foreach (TfpMotionItem Item in cDEF.Run.Motion.FItems)
                            //{
                            //    Item.Active = true;
                            //}
                            //foreach (TfpMotionItem Item in cDEF.Run.Motion.FItems)
                            //{
                            //    Item.ServoOn = 0;
                            //}
                            //foreach (TfpMotionItem Item in cDEF.Run.Motion.FItems)
                            //{
                            //    Item.ServoOn = 1;
                            //}
                            //foreach (TfpMotionItem Item in cDEF.Run.Motion.FItems)
                            //{
                            //    if (Convert.ToBoolean(Item.FAlarm))
                            //    {
                            //        Item.AlarmReset(1);
                            //        Item.Reset();
                            //    }
                            //}
                            //cDEF.Run.EasyMode = TfpRunningEasyMode.femInitialize;
                            cDEF.Run.CheckBeforeInitialize = true;
                        }
                    }
                    cDEF.Run.LogEvent(cLog.Form_Operator_Event, "[Form Operation] Initialize Button Click.");
                    break;

                case 1:
                    if (cDEF.Work.Project.GlobalOption.UseCuring1)
                    {
                        if (cDEF.Run.Digital.Input[cDI.UV_1_Alarm_Monitor] || !cDEF.Run.Digital.Input[cDI.UV_1_Lamp_Ready_Monitor])
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Check UV LAMP Status.", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }

                        if (!cDEF.Run.Digital.Input[cDI.Actuator_1_Ready] || !cDEF.Run.Digital.Input[cDI.Actuator_2_Ready])
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Check Actuator Status.", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                    }

                    if (cDEF.Work.Project.GlobalOption.UseIdle1 || cDEF.Work.Project.GlobalOption.UseIdle2)
                    {
                        if (cDEF.Work.Project.GlobalOption.UseIdle1)
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Check Bonder 1 Idle Status.", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                        if (cDEF.Work.Project.GlobalOption.UseIdle2)
                        {
                            XModuleMain.frmBox.MessageBox("Warning", "Check Bonder 2 Idle Status.", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                            return;
                        }
                    }

                    if (cDEF.Run.UnloadPicker.Information.HeadOverLoad || (!cDEF.Run.UnloadPicker.Information.HeadOverLoad && !cDEF.Run.UnloadPicker.HeadT.HomeEnd))
                    {
                        XModuleMain.frmBox.MessageBox("Warning", "Click Unload Axis T Home Button.", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                        return;
                    }

                    FButton = TfpMessageBoxButton.fmbYes.ToString();

                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.RUN;
                        cDEF.Run.MesStatusMsg = "VCM TRAY LOADING";
                        cDEF.Mes.Send_EquipStatus();
                    }
                    if (cDEF.Run.Mode == Running.TRunMode.Main_Stop)
                    {
                        cDEF.Run.Mode = Running.TRunMode.Main_Run;
                    }
                    cDEF.Run.LogEvent(cLog.Form_Operator_Event + 1, "[Form Operation] Run Button Click.");
                    break;
                case 2:
                    if (cDEF.Run.DetailMode == TfpRunningMode.frmToInitialize || cDEF.Run.DetailMode == TfpRunningMode.frmInitialize)
                        cDEF.Run.DetailMode = TfpRunningMode.frmNone;
                    else if (cDEF.Run.DetailMode >= TfpRunningMode.frmToRun)
                    {
                        cDEF.Run.EasyMode = TfpRunningEasyMode.femStop;
                    }
                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        if (cDEF.Run.MESEQPStatus != Unit.eMESEqpStatus.IDLE)
                        {
                            cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.IDLE;
                            cDEF.Run.MesStatusMsg = "AUTO";
                            cDEF.Mes.Send_EquipStatus();
                        }

                    }
                    cDEF.Run.LogEvent(cLog.Form_Operator_Event + 2, "[Form Operation] Stop Button Click.");
                    break;

                case 3:
                    cDEF.Run.LotEnd = !cDEF.Run.LotEnd;
                    break;

                case 4:
                    cDEF.fTaskLog.Hide();
                    cDEF.fTaskLog.Show();
                    break;
            }
        }
        private void CountClear_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as Label).Tag);

            switch(tag)
            {
                case 0:
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Do you want to Clear Pcb Count?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                    }
                    break;

                case 1:
                    FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                    if (XModuleMain.frmBox.MessageBox("Do you want to Clear Led Count?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                    }
                    break;
            }
        }
        public void ChangeLanguage()
        {
            btnStart.Text = cDEF.Lang.Trans("START");
            btnStop.Text = cDEF.Lang.Trans("STOP");
            btnEnable.Text = cDEF.Lang.Trans("INITIAL");
            btnLotEnd.Text = cDEF.Lang.Trans("LOT END");
            btnTaskLog.Text = cDEF.Lang.Trans("TASK LOG");
            gbVCMMagazine.Text = cDEF.Lang.Trans("VCM MAGAZINE");
            gbLensMagazine.Text = cDEF.Lang.Trans("LENS MAGAZINE");
            gbUnloaderMagazine.Text = cDEF.Lang.Trans("UNLOADER MAGAZINE");
            lbMagazineControl.Text = cDEF.Lang.Trans("MAGAZINE CONTROL");
            btnVCM_Magazine.Text = cDEF.Lang.Trans("INPUT");
            btnLens_Magazine.Text = cDEF.Lang.Trans("INPUT");
            btnUnloader_Magazine.Text = cDEF.Lang.Trans("INPUT");
            lbMachineMode.Text = cDEF.Lang.Trans("MACHINE STATUS");
            lbEQID.Text = cDEF.Lang.Trans("EQ ID");
            //lbLayout.Text = cDEF.Lang.Trans("LAYOUT");
            lbSequenceStatus.Text = cDEF.Lang.Trans("SEQUENCE STATUS");
            lbCycleTime.Text = cDEF.Lang.Trans("CYCLE TIME");
            lbInterface.Text = cDEF.Lang.Trans("INTERFACE");
            lbTactTimeLabel.Text = cDEF.Lang.Trans("TACT TIME");
            lbVCMLoaderStep.Text = cDEF.Lang.Trans("VCM LOADER");
            lbVCMPickerStep.Text = cDEF.Lang.Trans("VCM PICKER");
            lbLensLoaderStep.Text = cDEF.Lang.Trans("LENS LOADER");
            lbLensPickerStep.Text = cDEF.Lang.Trans("LENS PICKER");
            lbJigPlateStep.Text = cDEF.Lang.Trans("JIG FLATNESS");
            lbLensHeightStep.Text = cDEF.Lang.Trans("LENS HEIGHT");
            lbBonder1Step.Text = cDEF.Lang.Trans("BONDER 1");
            lbBonder2Step.Text = cDEF.Lang.Trans("BONDER 2");
            lbVisionInspectStep.Text = cDEF.Lang.Trans("VISION INSPECT");
            lbCuring1Step.Text = cDEF.Lang.Trans("CURING 1");
            lbCuring2Step.Text = cDEF.Lang.Trans("CURING 2");
            lbPlateAngleStep.Text = cDEF.Lang.Trans("SIDE ANGLE");
            lbUnloaderStep.Text = cDEF.Lang.Trans("UNLOADER");
            lbUnloadPickerStep.Text = cDEF.Lang.Trans("UNLOAD PICKER");
            lbCleanJigStep.Text = cDEF.Lang.Trans("CLEAN JIG");
            lbIndexStep.Text = cDEF.Lang.Trans("INDEX");
            gbStatus.Text = cDEF.Lang.Trans("PRODUCT STATUS");
            lbNone.Text = cDEF.Lang.Trans("None Product");
            lbReady.Text = cDEF.Lang.Trans("Ready");
            lbError.Text = cDEF.Lang.Trans("Error");
            lbExist.Text = cDEF.Lang.Trans("Exist");
            lbVCMTrayInfo.Text = cDEF.Lang.Trans("VCM TRAY INFORMATION");
            lbLensTrayInfo.Text = cDEF.Lang.Trans("LENS TRAY INFORMATION");
            lbUnloaderTrayInfo.Text = cDEF.Lang.Trans("UNLOADER TRAY INFORMATION");
            lbNGTrayInfo.Text = cDEF.Lang.Trans("NG TRAY INFORMATION");
            lbVCMTrayIndexXTitle.Text = cDEF.Lang.Trans("X INDEX");
            lbVCMTrayIndexYTitle.Text = cDEF.Lang.Trans("Y INDEX");
            lbVCMSlotNoTitle.Text = cDEF.Lang.Trans("SLOT NO");
            lbLensTrayIndexXTitle.Text = cDEF.Lang.Trans("X INDEX");
            lbLensTrayIndexYTitle.Text = cDEF.Lang.Trans("Y INDEX");
            lbLensSlotNoTitle.Text = cDEF.Lang.Trans("SLOT NO");
            lbUnloaderTrayIndexXTitle.Text = cDEF.Lang.Trans("X INDEX");
            lbUnloaderTrayIndexYTitle.Text = cDEF.Lang.Trans("Y INDEX");
            lbUnloaderSlotNoTitle.Text = cDEF.Lang.Trans("SLOT NO");
            lbNGTrayIndexTitle.Text = cDEF.Lang.Trans("INDEX");
            //lbDoorAble.Text = cDEF.Lang.Trans("DOOR ABLE");
            //lbLeftDoor.Text = cDEF.Lang.Trans("LEFT DOOR");
            //lbRightDoor.Text = cDEF.Lang.Trans("RIGHT DOOR");
            //lbDSensor.Text = cDEF.Lang.Trans("DSENSOR");
            //lbTactTimeLabel.Text = cDEF.Lang.Trans("LED TACT TIME");
        }

        private void btnVCM_Magazine_Click(object sender, EventArgs e)
        {
            int Value = 0;
            if (!XModuleMain.frmBox.fpIntegerEdit("Magazine Set Load Count", ref Value, " ea", 0, cDEF.Work.VCMLoader.SlotCount))
                return;
            {
                cDEF.Run.VCMLoader.Information.VCM_Magazine.Clear();
                cDEF.Run.VCMLoader.Information.VCM_Magazine.VCMLoad(Value);
            }
        }

        private void btnLens_Magazine_Click(object sender, EventArgs e)
        {
            int Value = 0;
            if (!XModuleMain.frmBox.fpIntegerEdit("Magazine Set Load Count", ref Value, " ea", 0, cDEF.Work.LensLoader.SlotCount))
                return;
            {
                cDEF.Run.LensLoader.Information.Lens_Magazine.Clear();
                cDEF.Run.LensLoader.Information.Lens_Magazine.LensLoad(Value);
            }
        }

        private void btnUnloader_Magazine_Click(object sender, EventArgs e)
        {
            int Value = 0;
            if (!XModuleMain.frmBox.fpIntegerEdit("Magazine Set Load Count", ref Value, " ea", 0, cDEF.Work.Unloader.SlotCount))
                return;
            {
                cDEF.Run.Unloader.Information.Unloader_Magazine.Clear();
                cDEF.Run.Unloader.Information.Unloader_Magazine.UnloadLoad(Value);
            }
        }
        private void btnVCMMagazineFull_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            //if (cDEF.Run.DetailMode == TfpRunningMode.frmStop)
            {
                if (XModuleMain.frmBox.MessageBox("SET MAGAZINE", "FULL MAGAZINE?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                {
                    cDEF.Run.VCMLoader.Information.VCM_Magazine.Clear();
                    cDEF.Run.VCMLoader.Information.VCM_Magazine.VCMLoad(cDEF.Run.VCMLoader.Information.VCM_Magazine.Items.Count);
                }
            }
        }

        private void btnLensMagazineFull_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            //if (cDEF.Run.DetailMode == TfpRunningMode.frmStop)
            {
                if (XModuleMain.frmBox.MessageBox("SET MAGAZINE", "FULL MAGAZINE?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                {
                    cDEF.Run.LensLoader.Information.Lens_Magazine.Clear();
                    cDEF.Run.LensLoader.Information.Lens_Magazine.LensLoad(cDEF.Run.LensLoader.Information.Lens_Magazine.Items.Count);
                }
            }
        }

        private void btnUnloadMagazineFull_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            //if (cDEF.Run.DetailMode == TfpRunningMode.frmStop)
            {
                if (XModuleMain.frmBox.MessageBox("SET MAGAZINE", "FULL MAGAZINE?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                {
                    cDEF.Run.Unloader.Information.Unloader_Magazine.Clear();
                    cDEF.Run.Unloader.Information.Unloader_Magazine.UnloadLoad(cDEF.Run.Unloader.Information.Unloader_Magazine.Items.Count);
                }
            }
        }
      

        private void lbVCMLoaderMode_Click(object sender, EventArgs e)
        {
            TactDisplay = !TactDisplay;
        }

        private void FrmPageOperation_VisibleChanged(object sender, EventArgs e)
        {
            VCMMagazineObjectInit();
            LensMagazineObjectInit();
            UnloadMagazineObjectInit();
            ChangeLanguage();
        }

        private void lbLensTrayExist_Click(object sender, EventArgs e)
        {
            cDEF.Run.LensLoader.Information.ExistStage = !cDEF.Run.LensLoader.Information.ExistStage;
        }

        private void lbVCMTrayExist_Click(object sender, EventArgs e)
        {
            cDEF.Run.VCMLoader.Information.ExistStage = !cDEF.Run.VCMLoader.Information.ExistStage;
        }

        private void lbUnloadTrayExist_Click(object sender, EventArgs e)
        {
            cDEF.Run.Unloader.Information.ExistStage = !cDEF.Run.Unloader.Information.ExistStage;
        }

        private void btnNGTrayReset_Click(object sender, EventArgs e)
        {
      
            cDEF.Run.UnloadPicker.Information.NG_tray.Clear();
            NgTrayMapInit();
        }

        private void lbLensHeadStatus_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            if (cDEF.Run.LensPicker.Information.HeadStatus == TrayStatus.Empty)
            {
                if (XModuleMain.frmBox.MessageBox("Lens Head Status", "Do you want to Head Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    cDEF.Run.LensPicker.Information.HeadStatus = TrayStatus.Load;
            }
            else
            {
                if (XModuleMain.frmBox.MessageBox("Lens Head Status", "Do you want to Head Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    cDEF.Run.LensPicker.Information.HeadStatus = TrayStatus.Empty;
            }
        }

        private void lbVCMHeadStatus_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            if (cDEF.Run.VCMPicker.Information.HeadStatus == TrayStatus.Empty)
            {
                if (XModuleMain.frmBox.MessageBox("VCM Head Status", "Do you want to Head Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    cDEF.Run.VCMPicker.Information.HeadStatus = TrayStatus.Load;
            }
            else
            {
                if (XModuleMain.frmBox.MessageBox("VCM Head Status", "Do you want to Head Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    cDEF.Run.VCMPicker.Information.HeadStatus = TrayStatus.Empty;
            }
        }

        private void lbUnloadHeadStatus_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            if (cDEF.Run.UnloadPicker.Information.HeadLensData.Status == LensTrayStatus.Empty)
            {
                if (XModuleMain.frmBox.MessageBox("Unloader Head Status", "Do you want to Head Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    cDEF.Run.UnloadPicker.Information.HeadLensData.Status = LensTrayStatus.Load;
            }

            else
            {
                if (XModuleMain.frmBox.MessageBox("Unloader Head Status", "Do you want to Head Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    cDEF.Run.UnloadPicker.Information.HeadLensData.Status = LensTrayStatus.Empty;
            }
        }

        private void lbPlateAngleExist_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            if (cDEF.Run.PlateAngle.Information.LensData.Status == LensTrayStatus.Empty)
            {
                if (XModuleMain.frmBox.MessageBox("Plate Angle Status", "Do you want to Plate Angle Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                {
                    cDEF.Run.PlateAngle.Information.LensData.Status = LensTrayStatus.Load;
                    cDEF.Run.PlateAngle.Information.LensData.Index = 1;
                }
            }
            else
            {
                if (XModuleMain.frmBox.MessageBox("Plate Angle Status", "Do you want to Plate Angle Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    cDEF.Run.PlateAngle.Information.LensData.Status = LensTrayStatus.Empty;
            }
        }

        private void lbIndexStatus_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            string FName = (sender as Label).Name;

            switch(FName)
            {
                case "lbVCMLoadStatus":
                    if (cDEF.Run.Index.Information.VCMData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index VCM Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.Index.Information.VCMData.Status = eLensIndexStatus.VCMLoaded;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index VCM Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.Index.Information.VCMData.Status = eLensIndexStatus.Empty;
                    }
                    break;
                case "lbJigPlateAngleStatus":
                    if (cDEF.Run.JigPlateAngle.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Jig Flatness Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.JigPlateAngle.Information.IndexData.Status = eLensIndexStatus.VCMLoaded;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Jig Flatness Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.JigPlateAngle.Information.IndexData.Status = eLensIndexStatus.Empty;
                    }
                    break;
                case "lbAct3Status":
                    if (cDEF.Run.Act3.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Spare Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.Index.Information.SpareData.Status = eLensIndexStatus.JigPlateAngleFinish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Spare Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.Act3.Information.IndexData.Status = eLensIndexStatus.Empty;
                    }
                    break;
                case "lbLensInsertStatus":
                    if (cDEF.Run.Index.Information.LensData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Lens Insert Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.Index.Information.LensData.Status = eLensIndexStatus.JigPlateAngleFinish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Lens Insert Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        {
                            cDEF.Run.Index.Information.LensData.Status = eLensIndexStatus.Empty;
                            //cDEF.Run.Digital.Output[cDO.Lens_Insert_Vacuum] = false;
                        }
                    }
                    break;
                case "lbLensHeightStatus":
                    if (cDEF.Run.LensHeight.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Lens Height Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.LensHeight.Information.IndexData.Status = eLensIndexStatus.AssembleFinish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Lens Height Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.LensHeight.Information.IndexData.Status = eLensIndexStatus.Empty;
                    }
                    break;
                case "lbBonder1Status":
                    if (cDEF.Run.Bonder1.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Bonder 1 Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.Bonder1.Information.IndexData.Status = eLensIndexStatus.LensHeightFinish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Bonder 1 Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        {
                            cDEF.Run.Bonder1.Information.IndexData.Status = eLensIndexStatus.Empty;
                            if (cDEF.Work.Project.GlobalOption.JettingMode1 == 0)    // Point
                            {
                                cDEF.Work.Bonder1Point.JettingDataInit();
                            }
                            else if (cDEF.Work.Project.GlobalOption.JettingMode1 == 2)
                            {
                                cDEF.Work.Bonder1Pattern.JettingPatternInit();
                            }
                            cDEF.Run.Bonder1.Information.CheckVisionFinish = false;
                        }
                    }
                    break;
                case "lbBonder2Status":
                    if (cDEF.Run.Bonder2.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Bonder 2 Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.Bonder2.Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Bonder 2 Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        {
                            cDEF.Run.Bonder2.Information.IndexData.Status = eLensIndexStatus.Empty;
                            if (cDEF.Work.Project.GlobalOption.JettingMode2 == 0)    // Point
                            {
                                cDEF.Work.Bonder2Point.JettingDataInit();
                            }
                            else if (cDEF.Work.Project.GlobalOption.JettingMode2 == 2)
                            {
                                cDEF.Work.Bonder2Pattern.JettingPatternInit();
                            }
                            cDEF.Run.Bonder2.Information.CheckVisionFinish = false;
                        }
                    }
                    break;
                case "lbVisionInsStatus":
                    if (cDEF.Run.VisionInspect.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Vision Inspect Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.VisionInspect.Information.IndexData.Status = eLensIndexStatus.Bonder2Finish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Vision Inspect Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.VisionInspect.Information.IndexData.Status = eLensIndexStatus.Empty;
                    }
                    break;
                case "lbCure1Status":
                    if (cDEF.Run.Curing1.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Curing 1 Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.Curing1.Information.IndexData.Status = eLensIndexStatus.VisionInspectFinish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Curing 1 Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.Curing1.Information.IndexData.Status = eLensIndexStatus.Empty;
                    }
                    break;
                case "lbCure2Status":
                    if (cDEF.Run.Curing2.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Curing 2 Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.Curing2.Information.IndexData.Status = eLensIndexStatus.Curing1Finish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Curing 2 Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.Curing2.Information.IndexData.Status = eLensIndexStatus.Empty;
                    }
                    break;
                case "lbPlateAngleStatus":
                    if (cDEF.Run.PlateAngle.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Side Angle Measure Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.PlateAngle.Information.IndexData.Status = eLensIndexStatus.Curing2Finish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Side Angle Measure Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.PlateAngle.Information.IndexData.Status = eLensIndexStatus.Empty;
                    }
                    break;
                case "lbCleanJigStatus":
                    if (cDEF.Run.CleanJig.Information.IndexData.Status == eLensIndexStatus.Empty)
                    {
                        //if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Clean Jig Load?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                        //    cDEF.Run.CleanJig.Information.IndexData.Status = eLensIndexStatus.UnloadFinish;
                    }
                    else
                    {
                        if (XModuleMain.frmBox.MessageBox("Index Status", "Do you want to Index Clena Jig Empty?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            cDEF.Run.CleanJig.Information.IndexData.Status = eLensIndexStatus.Empty;
                    }
                    break;
            }
        }

        private void btnUvLamp_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            if (cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On])
            {
                if (XModuleMain.frmBox.MessageBox("UV Lamp", "Do you want to Off UV Lamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 0)
                {
                    return;
                }
            }
            cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On] = !cDEF.Run.Digital.Output[cDO.UV_1_Lamp_On];
        }

        private void btnUVLamp2_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            if (cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On])
            {
                if (XModuleMain.frmBox.MessageBox("UV Lamp", "Do you want to Off UV Lamp?", TfpMessageBoxIcon.fmiQuestion, FButton) == 0)
                {
                    return;
                }
            }
            cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On] = !cDEF.Run.Digital.Output[cDO.UV_2_Lamp_On];

            //cDEF.Run.LensPicker.WorkLens.Status = LensTrayStatus.Finish;
        }

        private void btnInnerLight_Click(object sender, EventArgs e)
        {
            cDEF.Run.Digital.Output[cDO.InnerLight] = !cDEF.Run.Digital.Output[cDO.InnerLight];
        }

        private void SetPorductNumber_Click(object sender, EventArgs e)
        {
            string FName = (sender as Label).Name;

            int Value = LensHeightChartMaximum_X;

            switch (FName)
            {
                case "lbLensHeightSetPorductNumber":
                    if (!XModuleMain.frmBox.fpIntegerEdit("Product Number", ref Value, "", 1, 10000))
                        return;
                    LensHeightChartMaximum_X = Value;
                    LensHeight_Chart.ChartAreas[0].AxisX.Maximum = LensHeightChartMaximum_X;
                    SideAngle_Chart.ChartAreas[0].AxisX.Maximum = LensHeightChartMaximum_X;
                    break;

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
#if Notebook

            Random r = new Random();

            UnloadPicker_OnYieldChart(r.Next(0,11), eFailType.None);
            UnloadPicker_OnYieldChart(r.Next(0,11), eFailType.LensHeightFail);
            UnloadPicker_OnYieldChart(r.Next(0,11), eFailType.PlateAngleFail);
            UnloadPicker_OnYieldChart(r.Next(0,11), eFailType.VisionInspectFail);
            UnloadPicker_OnYieldChart(r.Next(0,11), eFailType.Actuating2Fail);
            UnloadPicker_OnYieldChart(r.Next(0,11), eFailType.Actuating1Fail);
            UnloadPicker_OnYieldChart(r.Next(0,11), eFailType.SideHeightFail); //Adding Error
#endif
        }

        private void btnAngle_Single_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Jig Flatness Measure?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
            {
                string FName = (sender as Glass.GlassButton).Tag.ToString();

                cDEF.Run.JigPlateAngle.Information.AngleDataClear();

                switch (FName)
                {
                    case "Single":
                        cDEF.Run.Mode = TRunMode.Manual_MeasureJigPlateAngle;
                        break;
                    case "All":
                        cDEF.Run.JigPlateAngle.ManualMeasureCount = 0;
                        cDEF.Run.Mode = TRunMode.Manual_Jig_Index_Measure;
                        break;

                }
            }
        }

        private void btnVcmSkip_Click(object sender, EventArgs e)
        {
            //cDEF.Run.VCMPicker.Information.VCM_Tray.Load();
            // cDEF.Run.LensPicker.Information.Lens_Tray.Load();
            // cDEF.Run.UnloadPicker.Information.Unloader_Tray.Load();

            //VCMLoader_OnVCM_DisplayInit();
            //  LensLoader_OnLens_DisplayInit();
            // Unloader_OnUnload_DisplayInit();

            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            string FName = (sender as Glass.GlassButton).Name;
            switch (FName)
            {
                case "btnVcmSkip":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip Vcm?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.GetVCM();

                        if (cDEF.Run.VCMPicker.WorkLens.Status == LensTrayStatus.Load)
                        {
                            cDEF.Run.VCMPicker.WorkLens.Status = LensTrayStatus.Finish;

                            VCMPicker_OnVCM_Display(cDEF.Run.VCMPicker.WorkLens.x, cDEF.Run.VCMPicker.WorkLens.y, LensTrayStatus.Finish);
                        }
                    }
                    break;
                case "btnLensSkip":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip Lens?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.GetLens();
                        if (cDEF.Run.LensPicker.WorkLens.Status == LensTrayStatus.Load)
                        {
                            cDEF.Run.LensPicker.WorkLens.Status = LensTrayStatus.Finish;
                            LensPicker_OnLens_Display(cDEF.Run.LensPicker.WorkLens.x, cDEF.Run.LensPicker.WorkLens.y, LensTrayStatus.Finish);
                        }
                    }
                    break;
                case "btnUnloadTraySkip":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip UnLoad Tray?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.GetEmptyTray();
                        if (cDEF.Run.UnloadPicker.WorkLens.Status == LensTrayStatus.Load)
                        {
                            cDEF.Run.UnloadPicker.WorkLens.Status = LensTrayStatus.Finish;
                            UnloadPicker_OnVCM_Display(cDEF.Run.UnloadPicker.WorkLens.x, cDEF.Run.UnloadPicker.WorkLens.y, LensTrayStatus.Finish);
                        }
                    }
                    break;
                case "btnNgSkip":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip UnLoad NG Tray?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.GetEmptyNGTray();
                        if (cDEF.Run.UnloadPicker.WorkLens.Status == LensTrayStatus.Load ||
                            cDEF.Run.UnloadPicker.WorkLens.Status == LensTrayStatus.Empty)
                        {
                            cDEF.Run.UnloadPicker.WorkLens.Status = LensTrayStatus.Finish;
                            UnloadPicker_OnNg_Display(cDEF.Run.UnloadPicker.WorkLens.x, cDEF.Run.UnloadPicker.WorkLens.y, eFailType.None);
                        }
                    }
                    break;

            }
        }
#region LensMapClear


        public bool VcmFinish(int IndexX, int IndexY, float clkx, float clky)
        {
            int Boundary = 10;
            int gap = 5;
          

            int CountX = cDEF.Work.VCMLoader.TrayCountX;
            int CountY = cDEF.Work.VCMLoader.TrayCountY;

            int SizeX = (int)((double)(picVCMMap.Width - Boundary * 2) / (double)(CountX)) - gap;
            int SizeY = (int)((double)(picVCMMap.Height - Boundary * 2) / (double)(CountY)) - gap;

            int orgx = Boundary + (IndexX * (SizeX + gap)) - 1;
            int orgy = Boundary + (IndexY * (SizeY + gap)) - 1;

            Size mapSize = new Size((SizeX + gap)+3, (SizeY + gap)+3);
            Point mapPoint = new Point(orgx, orgy);


            return MapClickChk(mapSize, mapPoint, clkx, clky);
        }

        public bool LensFinish(int IndexX, int IndexY, float clkx, float clky)
        {

            int Boundary = 10;
            int gap = 5;

            int CountX = cDEF.Work.LensLoader.TrayCountX;
            int CountY = cDEF.Work.LensLoader.TrayCountY;

            int SizeX = (int)((double)(picLensMap.Width - Boundary * 2) / (double)(CountX)) - gap;
            int SizeY = (int)((double)(picLensMap.Height - Boundary * 2) / (double)(CountY)) - gap;

            int orgx = Boundary + (IndexX * (SizeX + gap))-1;
            int orgy = Boundary + (IndexY * (SizeY + gap))-1;

            Size mapSize = new Size((SizeX + gap) + 3, (SizeY + gap) + 3);
            Point mapPoint = new Point(orgx, orgy);


            return MapClickChk(mapSize, mapPoint, clkx, clky);

        }
        public bool UnloadFinish(int IndexX, int IndexY, float clkx, float clky)
        {

            int Boundary = 10;
            int gap = 5;

            int CountX = cDEF.Work.VCMLoader.TrayCountX;
            int CountY = cDEF.Work.VCMLoader.TrayCountY;

            int SizeX = (int)((double)(picUnloadMap.Width - Boundary * 2) / (double)(CountX)) - gap;
            int SizeY = (int)((double)(picUnloadMap.Height - Boundary * 2) / (double)(CountY)) - gap;

            int orgx = Boundary + (IndexX * (SizeX + gap)) - 1;
            int orgy = Boundary + (IndexY * (SizeY + gap)) - 1;

            Size mapSize = new Size((SizeX + gap) + 3, (SizeY + gap) + 3);
            Point mapPoint = new Point(orgx, orgy);


            return MapClickChk(mapSize, mapPoint, clkx, clky);
        }

        private bool MapClickChk(Size mapSize, Point temp,  float clkx, float clky)
        {
            using (System.Drawing.Drawing2D.GraphicsPath gP = new System.Drawing.Drawing2D.GraphicsPath())
            {

                gP.AddEllipse(new Rectangle(temp, mapSize));
                return gP.IsVisible(clkx, clky);
            }
        }
#endregion



        private void picVCMMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cDEF.Run.DetailMode == TfpRunningMode.frmRun)
                return;

            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            string FName = (sender as PictureBox).Name;
            if (Control.ModifierKeys == Keys.Shift)
            {
                switch (FName)
                {
                    case "picVCMMap":
                        {
                            if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip Vcm?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            {
                                int SizeX = picVCMMap.Width - 25;
                                int SizeY = picVCMMap.Height - 25;

                                if ((e.X > 10 && e.Y > 10) &&
                                    (SizeX > e.X && SizeY > e.Y))

                                {
                                    foreach (Lens_Data rd in cDEF.Run.VCMPicker.Information.VCM_Tray.Items)
                                    {
                                        if (rd.Status == LensTrayStatus.Load)
                                        {
                                            rd.Status = LensTrayStatus.Finish;

                                            VCMPicker_OnVCM_Display(rd.x, rd.y, LensTrayStatus.Finish);
                                            if (VcmFinish(rd.x, rd.y, e.X, e.Y))
                                                break;
                                        }
                                        else if (rd.Status == LensTrayStatus.Finish && VcmFinish(rd.x, rd.y, e.X, e.Y))
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                    case "picLensMap":
                        {
                            if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip Lens?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            {
                                int SizeX = picLensMap.Width - 25;
                                int SizeY = picLensMap.Height - 25;

                                if ((e.X > 10 && e.Y > 10) &&
                                    (SizeX > e.X && SizeY > e.Y))

                                {
                                    foreach (Lens_Data rd in cDEF.Run.LensPicker.Information.Lens_Tray.Items)
                                    {
                                        if (rd.Status == LensTrayStatus.Load)
                                        {
                                            rd.Status = LensTrayStatus.Finish;

                                            LensPicker_OnLens_Display(rd.x, rd.y, LensTrayStatus.Finish);
                                            if (LensFinish(rd.x, rd.y, e.X, e.Y))
                                                break;
                                        }
                                        else if (rd.Status == LensTrayStatus.Finish && LensFinish(rd.x, rd.y, e.X, e.Y))
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                    case "picUnloadMap":
                        {
                            if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip UnLoad Tray?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            {
                                int SizeX = picUnloadMap.Width - 25;
                                int SizeY = picUnloadMap.Height - 25;

                                if ((e.X > 10 && e.Y > 10) &&
                                    (SizeX > e.X && SizeY > e.Y))

                                {
                                    foreach (Lens_Data rd in cDEF.Run.UnloadPicker.Information.Unloader_Tray.Items)
                                    {
                                        if (rd.Status == LensTrayStatus.Load)
                                        {
                                            rd.Status = LensTrayStatus.Finish;

                                            UnloadPicker_OnVCM_Display(rd.x, rd.y, LensTrayStatus.Finish);
                                            if (UnloadFinish(rd.x, rd.y, e.X, e.Y))
                                                break;
                                        }
                                        else if (rd.Status == LensTrayStatus.Finish && UnloadFinish(rd.x, rd.y, e.X, e.Y))
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                switch (FName)
                {
                    case "picVCMMap":
                        {
                            if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip Vcm?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            {
                                int MaxSizeX = picVCMMap.Width - 25;
                                int MaxSizeY = picVCMMap.Height - 25;

                                if ((e.X > 10 && e.Y > 10) &&
                                    (MaxSizeX > e.X && MaxSizeY > e.Y))

                                {
                                    foreach (Lens_Data rd in cDEF.Run.VCMPicker.Information.VCM_Tray.Items)
                                    {
                                        if (rd.Status == LensTrayStatus.Load)
                                        {
                                            int CountX = cDEF.Work.VCMLoader.TrayCountX;
                                            int CountY = cDEF.Work.VCMLoader.TrayCountY;

                                            for (int y = 0; y < CountY; y++)
                                            {
                                                for (int x = 0; x < CountX; x++)
                                                {

                                                    int SizeX = (int)((double)(picVCMMap.Width - 10 * 2) / (double)(CountX)) - 5;
                                                    int SizeY = (int)((double)(picVCMMap.Height - 10 * 2) / (double)(CountY)) - 5;
                                                    int orgx = 10 + (x * (SizeX + 5));
                                                    int orgy = 10 + (y * (SizeY + 5));

                                                    if ((e.X > orgx && e.X < orgx + SizeX) && (e.Y > orgy && e.Y < orgy + SizeY))
                                                    {
                                                        if (rd.x == x && rd.y == y)
                                                        {
                                                            rd.Status = LensTrayStatus.Finish;
                                                            VCMPicker_OnVCM_Display(rd.x, rd.y, LensTrayStatus.Finish);

                                                            if (VcmFinish(rd.x, rd.y, e.X, e.Y))
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (rd.Status == LensTrayStatus.Finish && VcmFinish(rd.x, rd.y, e.X, e.Y))
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                    case "picLensMap":
                        {
                            if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip Lens?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            {
                                int MaxSizeX = picLensMap.Width - 25;
                                int MaxSizeY = picLensMap.Height - 25;

                                if ((e.X > 10 && e.Y > 10) &&
                                    (MaxSizeX > e.X && MaxSizeY > e.Y))

                                {
                                    foreach (Lens_Data rd in cDEF.Run.LensPicker.Information.Lens_Tray.Items)
                                    {
                                        if (rd.Status == LensTrayStatus.Load)
                                        {
                                            int CountX = cDEF.Work.LensLoader.TrayCountX;
                                            int CountY = cDEF.Work.LensLoader.TrayCountY;
                                            for (int y = 0; y < CountY; y++)
                                            {
                                                for (int x = 0; x < CountX; x++)
                                                {
                                                    int SizeX = (int)((double)(picLensMap.Width - 10 * 2) / (double)(CountX)) - 5;
                                                    int SizeY = (int)((double)(picLensMap.Height - 10 * 2) / (double)(CountY)) - 5;

                                                    int orgx = 10 + (x * (SizeX + 5));
                                                    int orgy = 10 + (y * (SizeY + 5));

                                                    if ((e.X > orgx && e.X < orgx + SizeX) && (e.Y > orgy && e.Y < orgy + SizeY))
                                                    {
                                                        if (rd.x == x && rd.y == y)
                                                        {
                                                            rd.Status = LensTrayStatus.Finish;

                                                            LensPicker_OnLens_Display(rd.x, rd.y, LensTrayStatus.Finish);

                                                            if (LensFinish(rd.x, rd.y, e.X, e.Y))
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (rd.Status == LensTrayStatus.Finish && LensFinish(rd.x, rd.y, e.X, e.Y))
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                    case "picUnloadMap":
                        {
                            if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Skip UnLoad Tray?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                            {
                                int MaxSizeX = picUnloadMap.Width - 25;
                                int MaxSizeY = picUnloadMap.Height - 25;

                                if ((e.X > 10 && e.Y > 10) &&
                                    (MaxSizeX > e.X && MaxSizeY > e.Y))

                                {
                                    foreach (Lens_Data rd in cDEF.Run.UnloadPicker.Information.Unloader_Tray.Items)
                                    {
                                        if (rd.Status == LensTrayStatus.Load)
                                        {
                                            int CountX = cDEF.Work.Unloader.TrayCountX;
                                            int CountY = cDEF.Work.Unloader.TrayCountY;

                                            for (int y = 0; y < CountY; y++)
                                            {
                                                for (int x = 0; x < CountX; x++)
                                                {
                                                    int SizeX = (int)((double)(picUnloadMap.Width - 10 * 2) / (double)(CountX)) - 5;
                                                    int SizeY = (int)((double)(picUnloadMap.Height - 10 * 2) / (double)(CountY)) - 5;

                                                    int orgx = 10 + (x * (SizeX + 5));
                                                    int orgy = 10 + (y * (SizeY + 5));

                                                    if ((e.X > orgx && e.X < orgx + SizeX) && (e.Y > orgy && e.Y < orgy + SizeY))
                                                    {
                                                        if (rd.x == x && rd.y == y)
                                                        {
                                                            rd.Status = LensTrayStatus.Finish;

                                                            UnloadPicker_OnVCM_Display(rd.x, rd.y, LensTrayStatus.Finish);
                                                            if (UnloadFinish(rd.x, rd.y, e.X, e.Y))
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (rd.Status == LensTrayStatus.Finish && UnloadFinish(rd.x, rd.y, e.X, e.Y))
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                }

            }
        }

        private void btnYieldReset_Click(object sender, EventArgs e)
        {
            cDEF.Run.UnloadPicker.Information.ChartDataClear();
            cDEF.Run.UnloadPicker.ChartDisplayTest(0, eFailType.AssembleFail);
            cDEF.TaskLogAppend(TaskLog.YieldChartData, "DATA CLEAR" , true);
        }

        private void btnTestButton_Click(object sender, EventArgs e)
        {
            cDEF.DispenserEcm1.PressValue = 1622;
            cDEF.DispenserEcm1.PressTime = 1000;
            cDEF.DispenserEcm1.VacValue = 400;
            cDEF.DispenserEcm1.SetValueStart();
        }

        private void btnVcmReset_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();

            string FName = (sender as Glass.GlassButton).Name;
            switch (FName)
            {
                case "btnVcmReset":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Reset Vcm?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.VCMPicker.Information.VCM_Tray.Load();
                        VCMTrayMapInit();
                    }
                    break;
                case "btnLensReset":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Reset Lens?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.LensPicker.Information.Lens_Tray.Load();
                        LensTrayMapInit();
                    }
                    break;
                case "btnUnloadReset":
                    if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Reset UnLoad Tray?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
                    {
                        cDEF.Run.UnloadPicker.Information.Unloader_Tray.Load();
                        UnloadTrayMapInit();
                    }
                    break;
            }
        }

        private void btnDailyCountReset_Click(object sender, EventArgs e)
        {
            FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
            if (XModuleMain.frmBox.MessageBox("MANUAL", "Do you want to Reset Count?", TfpMessageBoxIcon.fmiQuestion, FButton) == 1)
            {
                cDEF.Run.UnloadPicker.Information.DailyDataClear("MANUAL");
                
            }
        }

        private void JettingCount_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as Label).Tag);
            int Value = 0;
            string str = string.Empty;
            switch(tag)
            {
                case 0:
                    Value = cDEF.Work.Bonder1.JettingCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #1 Jetting Total Count", ref Value, " ea"))
                        return;
                    {
                        str = String.Format("[FormOper] Bonder #1 Jetting Total Count {0:0.000} to {1:0.000}", cDEF.Work.Bonder1.JettingCount, Value);
                        cDEF.Work.Bonder1.JettingCount = Value;
                        cDEF.Work.Bonder1.Save();
                        cDEF.Run.LogData(cLog.Form_Operator_Data + 12, str);
                    }
                    break;

                case 1:
                    Value = cDEF.Work.Bonder2.JettingCount;
                    if (!XModuleMain.frmBox.fpIntegerEdit("Bonder #2 Jetting Total Count", ref Value, " ea"))
                        return;
                    {
                        str = String.Format("[FormOper] Bonder #2 Jetting Total Count {0:0.000} to {1:0.000}", cDEF.Work.Bonder2.JettingCount, Value);
                        cDEF.Work.Bonder2.JettingCount = Value;
                        cDEF.Work.Bonder2.Save();
                        cDEF.Run.LogData(cLog.Form_Operator_Data + 13, str);
                    }
                    break;
            }
        }

        private void btnJettingCountReset_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as Glass.GlassButton).Tag);

            switch(tag)
            {
                case 0:
                    cDEF.Run.Bonder1.Information.JettingCount = 0;
                    break;

                case 1:
                    cDEF.Run.Bonder2.Information.JettingCount = 0;
                    break;
            }
        }

        private void VCM_Magazine_Click(object sender, EventArgs e)
        {
            int Value = 0;
            bool[] bValue = new bool[cDEF.Run.VCMLoader.Information.VCM_Magazine.Items.Count];
            if (Control.ModifierKeys == Keys.Control)
            {
                XModuleMain.frmBox.fpScriptBoxClear();
                if (!cDEF.Work.Recipe.LoaderMagazineDirection)
                {
                    for (int i = 0; i < cDEF.Run.VCMLoader.Information.VCM_Magazine.Items.Count; i++)
                    {
                        XModuleMain.frmBox.fpScriptBoxAdd($"SLOT {i + 1}", "Load", "", ref bValue[i], "");
                    }
                }
                else
                {
                    for (int i = cDEF.Run.VCMLoader.Information.VCM_Magazine.Items.Count; i > 0; i--)
                    {
                        XModuleMain.frmBox.fpScriptBoxAdd($"SLOT {i}", "Load", "", ref bValue[i - 1], "");
                    }
                }
                if (XModuleMain.frmBox.fpScriptBox("Magazine") == false)
                    return;
                cDEF.Run.VCMLoader.Information.VCM_Magazine.Clear();
                for (int i = 0; i < cDEF.Run.VCMLoader.Information.VCM_Magazine.Items.Count; i++)
                {
                    if (XModuleMain.bValue[i])
                    {
                        cDEF.Run.VCMLoader.Information.VCM_Magazine.Items[i].Load();
                    }
                }
            }
            //else
            //{
            //    if (!XModuleMain.frmBox.fpIntegerEdit("Magazine Set Load Count", ref Value, " ea", 0, cDEF.Work.VCMLoader.SlotCount))
            //        return;
            //    {
            //        cDEF.Run.VCMLoader.Information.VCM_Magazine.Clear();
            //        cDEF.Run.VCMLoader.Information.VCM_Magazine.VCMLoad(Value);
            //    }
            //}
        }

        private void Unloader_Magazine_Click(object sender, EventArgs e)
        {
            int Value = 0;
            bool[] bValue = new bool[cDEF.Run.Unloader.Information.Unloader_Magazine.Items.Count];
            if (Control.ModifierKeys == Keys.Control)
            {
                XModuleMain.frmBox.fpScriptBoxClear();
                if (!cDEF.Work.Recipe.UnLoaderMagazineDirection)
                {
                    for (int i = 0; i < cDEF.Run.Unloader.Information.Unloader_Magazine.Items.Count; i++)
                    {
                        XModuleMain.frmBox.fpScriptBoxAdd($"SLOT {i + 1}", "Load", "", ref bValue[i], "");
                    }
                }
                else
                {
                    for (int i = cDEF.Run.Unloader.Information.Unloader_Magazine.Items.Count; i > 0; i--)
                    {
                        XModuleMain.frmBox.fpScriptBoxAdd($"SLOT {i}", "Load", "", ref bValue[i - 1], "");
                    }
                }
                if (XModuleMain.frmBox.fpScriptBox("Magazine") == false)
                    return;
                cDEF.Run.Unloader.Information.Unloader_Magazine.Clear();
                for (int i = 0; i < cDEF.Run.Unloader.Information.Unloader_Magazine.Items.Count; i++)
                {
                    if (XModuleMain.bValue[i])
                    {
                        cDEF.Run.Unloader.Information.Unloader_Magazine.Items[i].Load();
                    }
                }
            }
            //else
            //{
            //    if (!XModuleMain.frmBox.fpIntegerEdit("Magazine Set Load Count", ref Value, " ea", 0, cDEF.Work.VCMLoader.SlotCount))
            //        return;
            //    {
            //        cDEF.Run.Unloader.Information.Unloader_Magazine.Clear();
            //        cDEF.Run.Unloader.Information.Unloader_Magazine.VCMLoad(Value);
            //    }
            //}
        }

        private void Lens_Magazine_Click(object sender, EventArgs e)
        {
            int Value = 0;
            bool[] bValue = new bool[cDEF.Run.LensLoader.Information.Lens_Magazine.Items.Count];
            if (Control.ModifierKeys == Keys.Control)
            {
                XModuleMain.frmBox.fpScriptBoxClear();
                if (!cDEF.Work.Recipe.LensMagazineDirection)
                {
                    for (int i = 0; i < cDEF.Run.LensLoader.Information.Lens_Magazine.Items.Count; i++)
                    {
                        XModuleMain.frmBox.fpScriptBoxAdd($"SLOT {i + 1}", "Load", "", ref bValue[i], "");
                    }
                }
                else
                {
                    for (int i = cDEF.Run.LensLoader.Information.Lens_Magazine.Items.Count; i > 0; i--)
                    {
                        XModuleMain.frmBox.fpScriptBoxAdd($"SLOT {i}", "Load", "", ref bValue[i - 1], "");
                    }
                }
                if (XModuleMain.frmBox.fpScriptBox("Magazine") == false)
                    return;
                cDEF.Run.LensLoader.Information.Lens_Magazine.Clear();
                for (int i = 0; i < cDEF.Run.LensLoader.Information.Lens_Magazine.Items.Count; i++)
                {
                    if (XModuleMain.bValue[i])
                    {
                        cDEF.Run.LensLoader.Information.Lens_Magazine.Items[i].Load();
                    }
                }
            }
            //else
            //{
            //    if (!XModuleMain.frmBox.fpIntegerEdit("Magazine Set Load Count", ref Value, " ea", 0, cDEF.Work.VCMLoader.SlotCount))
            //        return;
            //    {
            //        cDEF.Run.LensLoader.Information.Lens_Magazine.Clear();
            //        cDEF.Run.LensLoader.Information.Lens_Magazine.VCMLoad(Value);
            //    }
            //}
        }

        private void picNGMap_MouseClick(object sender, MouseEventArgs e)
        {
            int MaxSizeX = picLensMap.Width - 25;
            int MaxSizeY = picLensMap.Height - 25;

            if ((e.X > 10 && e.Y > 10) &&
                (MaxSizeX > e.X && MaxSizeY > e.Y))

            {
                foreach (Lens_Data rd in cDEF.Run.UnloadPicker.Information.NG_tray.Items)
                {
                    if (rd.Status != LensTrayStatus.Load)
                    {
                        int CountX = cDEF.Work.Unloader.NG_TrayCountX;
                        int CountY = cDEF.Work.Unloader.NG_TrayCountY;

                        for (int y = 0; y < CountY; y++)
                        {
                            for (int x = 0; x < CountX; x++)
                            {
                                int SizeX = (int)((double)(picNGMap.Width - 10 * 2) / (double)(CountX)) - 15;
                                int SizeY = (int)((double)(picNGMap.Height - 10 * 2) / (double)(CountY)) - 5;

                                int orgx = 6 + 10 + (x * (SizeX + 15));
                                int orgy = 6 + 10 + (y * (SizeY + 5));

                                if ((e.X > orgx && e.X < orgx + SizeX) && (e.Y > orgy && e.Y < orgy + SizeY))
                                {
                                    if (rd.x == x && rd.y == y)
                                    {
                                        if (rd.FailType != eFailType.None)
                                        {
                                            lbNGType.Text = rd.FailType.ToString();
                                            lbNGIndex.Text = rd.Index.ToString();

                                            if (rd.FailType == eFailType.LensHeightFail)
                                                lbNGValue.Text = rd.LensHeightData.ToString();
                                            else if (rd.FailType == eFailType.PlateAngleFail)
                                            {
                                                lbNGType.Text = "SideAngleFail";
                                                lbNGValue.Text = rd.PlateAngleData.ToString();
                                            }
                                            else
                                                lbNGValue.Text = "-";
                                        }
                                        else
                                        {
                                            lbNGType.Text = "-";
                                            lbNGIndex.Text = "-";
                                            lbNGValue.Text = "-";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnUnloadTHome_Click(object sender, EventArgs e)
        {
            if (cDEF.Run.DetailMode != TfpRunningMode.frmStop)
            {
                XModuleMain.frmBox.MessageBox("Warning", "Check STOP Status.", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            if (!cDEF.Run.UnloadPicker.Is_Head_ReadyPositionZ())
            {
                XModuleMain.frmBox.MessageBox("Warning", "Unload Picker Z Is Not Ready Position.", TfpMessageBoxIcon.fmiWarning, TfpMessageBoxButton.fmbClose.ToString());
                return;
            }
            cDEF.Run.UnloadPicker.HeadT.Home();
            cDEF.Run.UnloadPicker.Information.HeadOverLoad = false;
        }


    }
}

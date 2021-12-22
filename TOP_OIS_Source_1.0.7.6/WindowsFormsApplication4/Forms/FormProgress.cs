using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using XModule.Standard;
using XModule.Unit;
using System.IO;

namespace XModule
{
    public enum PROGRESS
    {
        PROGRESS_NONE = 0,
        PROGRESS_HISTORY,
        PROGRESS_MOTION,
        PROGRESS_DIGITAL,
        PROGRESS_CYLINDER,
        PROGRESS_CONFIG,
        PROGRESS_WORKING,
        PROGRESS_SEQUENCE,
        PROGRESS_LASER,
        PROGRESS_END   
    }

    public enum PROGRESS_STEP
    {
        STEP = 10,
    }
    public partial class FrmProgress : Form
    {
        public PROGRESS FMode = PROGRESS.PROGRESS_NONE;
        public int FStyle;
        public int FStep;

        public FrmProgress()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if ((int)Tag == 0)
            {
                FMode = PROGRESS.PROGRESS_NONE;
                FStep = 0;
                FStyle = 0;
            }
            else
            {
                FMode = PROGRESS.PROGRESS_END;
                FStep = (int)PROGRESS_STEP.STEP - 1;
                FStyle = 1;
            }
            Tag = 0;

            lbMachineName.Text = cPath.MahcineName;

            this.timer1.Interval = 2;

            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Visible)
                return;

            this.timer1.Enabled = false;

            switch (FMode)
            {
                case PROGRESS.PROGRESS_NONE:
                    switch (FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                                lbStatus.Text = "INITIALIZE START";
                            else
                            {

                                Tag = 1;
                                cDEF.frmMain.DialogResult = DialogResult.Cancel;
                                Close();
                                return;
                            }
                            break;

                        case 1:
                            if (FStyle == 1)
                            {
                                lbStatus.Text = "GOOD BYE...!";

                            }
                            break;
                    }
                        break;

                case PROGRESS.PROGRESS_HISTORY:
			        switch(FStep)
			        {
				        case 0:
					        if(FStyle == 0)
					        {
						        lbStatus.Text = "INITIALIZE HISTORY MODULE";
					        }
					        break;

				        case 1:
					        if(FStyle != 0)
					        {
					        }
					        break;

				        case 2:
					        if(FStyle == 0)
					        {

                                if (!cDEF.Run.Log.List.Open(cPath.FILE_LOG))
                                {
                                    String FButton = TfpMessageBoxButton.fmbNo.ToString() + "," + TfpMessageBoxButton.fmbYes.ToString();
                                    if (XModuleMain.frmBox.MessageBox("이력관리 데이터베이스", "이력관리 데이터베이스를 읽는 중 오류가 발생 하였습니다.", TfpMessageBoxIcon.fmiError, FButton) != 0)
                                    {
                                        FStep = (int)PROGRESS_STEP.STEP - 1;
                                        FStyle = 1;
                                        timer1.Enabled = true;
                                        return;
                                    }
                                }
                                if (!cDEF.Run.Log.Message.Open(cPath.PATH_LOG))
                                {
                                    String FButton = TfpMessageBoxButton.fmbClose.ToString();
                                    XModuleMain.frmBox.MessageBox("이력관리 데이터베이스", "이력관리 데이터베이스를 읽는 중 오류가 발생 하였습니다.\r\n", TfpMessageBoxIcon.fmiError, FButton);
                                    FStep = (int)PROGRESS_STEP.STEP - 1;
                                    FStyle = 1;
                                    timer1.Enabled = true;
                                    return;
                                }
                            }
                            
					        break;

				        case 3:
					        if(FStyle == 0)
					        {
                                cDEF.Run.Log.AddEvent(0, "");
					        }
					        else
                                cDEF.Run.Log.AddEvent(1, "");
					        break;

				        case (int)PROGRESS_STEP.STEP - 1:
					        if(FStyle == 1)
					        {
						        lbStatus.Text = "TERMINATE HISTORY MODULE";
					        }
					        break;
			        }
			        break;

                case PROGRESS.PROGRESS_MOTION:
                    switch(FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                                lbStatus.Text = "INITIALIZE MOTION START";
                            else
                            {
                            }
                            break;

                        case 1:
                            if (FStyle == 0)
                            {
#if Notebook
                                cDEF.Run.Motion.Simul = true;
#endif
                                cDEF.Run.Motion.Active = true;
                            }
                            else
                                cDEF.Run.Motion.Active = false;
                            break;

                        case 2:
                            if(FStyle == 0)
                            {
                                //모션 추가
                                cDEF.Run.Motion.Add("Index T");             //0

                                cDEF.Run.Motion.Add("VCM Head X");          //1
                                cDEF.Run.Motion.Add("VCM Head Y");          //2
                                cDEF.Run.Motion.Add("VCM Head Z");          //3
                                cDEF.Run.Motion.Add("VCM Head T");          //4
                                cDEF.Run.Motion.Add("Unload Head T");       //5

                                cDEF.Run.Motion.Add("Unload Head X");       //6
                                cDEF.Run.Motion.Add("Unload Head Y");       //7
                                cDEF.Run.Motion.Add("Unload Head Z");       //8
                                

                                cDEF.Run.Motion.Add("Lens Head X");         //9
                                cDEF.Run.Motion.Add("Lens Head Y");         //10
                                cDEF.Run.Motion.Add("Lens Head Z");         //11
                                cDEF.Run.Motion.Add("Lens Head T");         //12

                                cDEF.Run.Motion.Add("VCM Transfer X");      //13
                                cDEF.Run.Motion.Add("Unload Transfer X");   //14
                                cDEF.Run.Motion.Add("Lens Transfer Y");     //15

                                cDEF.Run.Motion.Add("VCM Stage Y");         //16
                                cDEF.Run.Motion.Add("Unload Stage Y");      //17
                                cDEF.Run.Motion.Add("Lens Stage X");        //18

                                cDEF.Run.Motion.Add("VCM Magazine Z");      //19
                                cDEF.Run.Motion.Add("Unload Magazine Z");   //20
                                cDEF.Run.Motion.Add("Lens Magazine Z");     //21

                                cDEF.Run.Motion.Add("Bond1 Head X");        //22
                                cDEF.Run.Motion.Add("Bond1 Head Y");        //23
                                cDEF.Run.Motion.Add("Bond1 Head Z");        //24

                                cDEF.Run.Motion.Add("Bond2 Head X");        //25
                                cDEF.Run.Motion.Add("Bond2 Head Y");        //26
                                cDEF.Run.Motion.Add("Bond2 Head Z");        //27

                                cDEF.Run.Motion.Add("Plate Angle Y");       //28

                                cDEF.Run.Motion.Add("Lens Insert Top Vision");           //29

                                cDEF.Run.Motion.Add("Jig PlateAngle Y");    //30

                                

                            }
                            break;
                        case 3:
                            if(FStyle == 0)
                            {
                                cDEF.Run.Motion.OpenConfig(cPath.FILE_MOTION_CONFIG);
                                cDEF.Run.Motion.OpenSpeed(cPath.FILE_MOTION_SPEED);
                            }
                            break;    

                        case 4:
                            if (FStyle == 0)
                            {
                                for (int i = 0; i < cDEF.Run.Motion.FItems.Count; i++)
                                {
                                    cDEF.Run.Motion.FItems[i].Active = true;
                                    cDEF.Run.Motion.FItems[i].ServoOn = 1;
                                }

                            }
                            else
                            {
                                for (int i = 0; i < cDEF.Run.Motion.FItems.Count; i++)
                                {
                                    cDEF.Run.Motion.FItems[i].FServoOn = 0;
                                    cDEF.Run.Motion.FItems[i].Active = false;
                                }
                            }
                            break;

                        case (int)PROGRESS_STEP.STEP - 1:
                            if(FStyle == 1)
                            {
                                lbStatus.Text= "TERMINATE MOTION MODULE";
                            }    
                            break;

                    }
                    break;

                case PROGRESS.PROGRESS_DIGITAL:

                    switch(FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                                lbStatus.Text = "INITIALIZE DIGITAL START";
                            else
                            {
                            }
                            break;

                        case 1:
                            if (FStyle == 0)
                            {
#if Notebook
                                cDEF.Run.Digital.Simul = true;
#endif
                                cDEF.Run.Digital.Active = true;
                                cDEF.Run.Analog.Active = true;
                            }
                            else
                            {
                                cDEF.Run.Digital.Active = false;
                                cDEF.Run.Analog.Active = false;
                            }
                            break;

                        case 2:
                            if (FStyle == 0)
                            {
                                //디지털 IO
                                cDEF.Run.Digital.Add(TfpDigitalStyle.fdsInput, "Input Module #1 (0 ~ 31: 32 Point)");
                                cDEF.Run.Digital.Add(TfpDigitalStyle.fdsInput, "Input Module #2 (32 ~ 63: 32 Point)");
                                cDEF.Run.Digital.Add(TfpDigitalStyle.fdsInput, "Input Module #3 (64 ~ 95: 32 Point)");
                                cDEF.Run.Digital.Add(TfpDigitalStyle.fdsInput, "Input Module #4 (96 ~ 127: 32 Point)");
                                cDEF.Run.Digital.Add(TfpDigitalStyle.fdsOutput, "Output Module #1 (0 ~ 63: 32 Point)");
                                cDEF.Run.Digital.Add(TfpDigitalStyle.fdsOutput, "Output Module #2 (32 ~ 63: 32 Point)");
                                //cDEF.Run.Digital.Add(TfpDigitalStyle.fdsOutput, "Output Module #3 (32 ~ 63: 32 Point)");

                                //아날로그
                                cDEF.Run.Analog.Add(TfpAnalogStyle.fdsAI4RB, "Analog Module #1 (0 ~ 3: 4 Point)", 4);
                            }
                            break;

                        case 3:
                            if (FStyle == 0)
                            {
                                cDEF.Run.Digital.Open(cPath.FILE_DIGITAL);

                                cDEF.Run.Analog.Open(cPath.FILE_ANALOG);
                            }
                            break;

                        case 4:
                            if (FStyle == 0)
                            {
                                for (int i = 0; i < cDEF.Run.Digital.Count; i++)
                                {
                                    if (cDEF.Run.Digital.Items[i].Style == TfpDigitalStyle.fdsOutput)
                                        cDEF.Run.Digital.Items[i].ItemInit();
                                }
                            }
                            break;

                        case 5:
                            if (FStyle == 0)
                            {
                                for (int i = 0; i < cDEF.Run.Digital.Count; i++)
                                    cDEF.Run.Digital.Items[i].Active = true;

                                for (int i = 0; i < cDEF.Run.Analog.Count; i++)
                                    cDEF.Run.Analog.Items[i].Active = true;
                            }
                            else
                            {
                                for (int i = 0; i < cDEF.Run.Digital.Count; i++)
                                {
                                    cDEF.Run.Digital.Items[i].Active = false;
                                }

                                for (int i = 0; i < cDEF.Run.Analog.Count; i++)
                                {
                                    cDEF.Run.Analog.Items[i].Active = false;
                                }
                            }
                            break;

                        

                        case (int)PROGRESS_STEP.STEP - 1:
                            if(FStyle == 1)
                            {
                                lbStatus.Text= "TERMINATE DIGITAL MODULE";
                            }    
                            break;
                    }
                    break;

                case PROGRESS.PROGRESS_CYLINDER:
                    switch (FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                                lbStatus.Text = "INITIALIZE CYLINDER START";
                            else
                            {
                            }
                            break;

                        case 1:
                            if (FStyle == 0)
                            {
                                //실리더 추가
                                cDEF.Run.Cylinder.Add("VCM Transfer Clamp");        //1
                                cDEF.Run.Cylinder.Add("VCM Stage Clamp");           //2
                                cDEF.Run.Cylinder.Add("Lens Transfer Clamp");       //3
                                cDEF.Run.Cylinder.Add("Lens Stage Clamp");          //4
                                cDEF.Run.Cylinder.Add("Bonder1 Tip Clean");         //5
                                cDEF.Run.Cylinder.Add("Bonder2 Tip Clean");         //6
                                cDEF.Run.Cylinder.Add("Cure1 Contact");           //7
                                cDEF.Run.Cylinder.Add("Cure1 UV Down");             //9
                                //cDEF.Run.Cylinder.Add("Cure2 Contact");           //10
                                //cDEF.Run.Cylinder.Add("Cure2 UV Down");             //12
                                cDEF.Run.Cylinder.Add("Jig Clean Down");            //13
                                cDEF.Run.Cylinder.Add("Unload Transfer Clamp");     //14
                                cDEF.Run.Cylinder.Add("Unload Stage Clamp");        //15
                                cDEF.Run.Cylinder.Add("Plate Angle Down");
                                cDEF.Run.Cylinder.Add("Plate Angle FW");
                                cDEF.Run.Cylinder.Add("Plate Angle Clamp");
                                cDEF.Run.Cylinder.Add("Plate Angle Contact");
                                cDEF.Run.Cylinder.Add("VCM Clamp");
                                cDEF.Run.Cylinder.Add("Cure2 UV Down");

                                cDEF.Run.Cylinder.Add("Act3 Contact");
                                cDEF.Run.Cylinder.Add("Act3 ClampDown");
                            }
                            break;

                        case 2:
                            if (FStyle == 0)
                            {
                                cDEF.Run.Cylinder.Open(cPath.FILE_CYLINDER);
                                for (int i = 0; i < cDEF.Run.Cylinder.Count; i++)
                                    cDEF.Run.Cylinder.Items[i].Active = true;
                            }
                            break;

                        case (int)PROGRESS_STEP.STEP - 1:
                            if (FStyle == 1)
                            {
                                lbStatus.Text = "TERMINATE CYLINDER MODULE";                
                            }
                            break;

                    }
                    break;

                case PROGRESS.PROGRESS_CONFIG:
                    switch (FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                                lbStatus.Text = "INITIALIZE CONFIG MODULE";
                            break;

                        case 1:
                            if (FStyle != 0)
                            {
                                cDEF.Run.Lamp.Delete();
                                cDEF.Run.Switch.Delete();
                            }
                            break;

                        case 2:
                            if (FStyle == 0)
                            {
                                // 공백 입력 금지.
                                //램프 출력
                                cDEF.Run.Lamp.Add("Start_Switch_Lamp");
                                cDEF.Run.Lamp.Add("Stop_Switch_Lamp");
                                cDEF.Run.Lamp.Add("Home_Switch_Lamp");
                                cDEF.Run.Lamp.Add("Reset_Switch_Lamp");
                                cDEF.Run.Lamp.Add("Tower_Lamp_Red");
                                cDEF.Run.Lamp.Add("Tower_Lamp_Yellow");
                                cDEF.Run.Lamp.Add("Tower_Lamp_Green");
                                cDEF.Run.Lamp.Add("Buzzer");


                                ////스위치 입력
                                cDEF.Run.Switch.Add("Start_Switch", 900);
                                cDEF.Run.Switch.Add("Stop_Switch", 901);
                                cDEF.Run.Switch.Add("Home_Switch", 902);
                                cDEF.Run.Switch.Add("Reset_Switch", 903);
                                cDEF.Run.Switch.Add("Front_Emg_Switch", 904);
                                cDEF.Run.Switch.Add("Rear_Emg_Switch", 905);
                                cDEF.Run.Switch.Add("Main_Air_Error", 906); 
                                cDEF.Run.Switch.Add("VCM_Transfer_Overload_1",907);
                                cDEF.Run.Switch.Add("VCM_Transfer_Overload_2", 908);
                                cDEF.Run.Switch.Add("Lens_Transfer_Overload_1", 909);
                                cDEF.Run.Switch.Add("Lens_Transfer_Overload_2", 910);
                                cDEF.Run.Switch.Add("Unload_Transfer_Overload_1", 911);
                                cDEF.Run.Switch.Add("Unload_Transfer_Overload_2", 912);
                                cDEF.Run.Switch.Add("VCM_Head_Overload", 913);
                                cDEF.Run.Switch.Add("Unload_Head_Overload", 914);

                                cDEF.Run.Switch.Add("Door", 915);
                            }
                            break;

                        case 3:
                            if (FStyle == 0)
                            {
                                cDEF.Run.Lamp.Open(cPath.FILE_LAMP);
                                cDEF.Run.Switch.Open(cPath.FILE_SWITCH);
                            }
                            break;

                        case (int)PROGRESS_STEP.STEP - 1:
                            if (FStyle == 1)
                            {
                                lbStatus.Text = "TERMINATE CONFIG MODULE";
                            }
                            break;

                    }
                    break;

                case PROGRESS.PROGRESS_WORKING:
                    switch (FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                                lbStatus.Text = "INITIALIZE WORKING MODULE";
                            break;

                        case 1:
                            if (FStyle != 0)
                            {
                              //  cDEF.Work = null;
                            }
                            break;

                        case 2:
                            if (FStyle == 0)
                            {
                                cDEF.Work.Open(cPath.FILE_WORKING);
                                cDEF.Work.Project.OpenDefault();
                            }
                            break;

                        case (int)PROGRESS_STEP.STEP - 1:
                            if (FStyle == 1)
                            {
                                lbStatus.Text = "TERMINATE WORKING MODULE";
                            }
                            break;

                    }
                    break;


                case PROGRESS.PROGRESS_SEQUENCE:
                    switch (FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                                lbStatus.Text = "INITIALIZE SEQUENCE MODULE";
                            break;

                        case 1:
                            if (FStyle != 0)
                            {
                                cDEF.Run.Delete();
                            }
                            break;

                        case 2:
                            if (FStyle == 0)
                            {
                                // Recovery open
                            }
                            break;

                        case 3:
                            if (FStyle == 0)
                            {
                                //모션 추가
                                //Motor
                                cDEF.Run.VCMLoader.AddMotion(cDEF.Run.Motion.Find("VCM Magazine Z"));
                                cDEF.Run.VCMLoader.AddMotion(cDEF.Run.Motion.Find("VCM Transfer X"));
                                

                                cDEF.Run.VCMPicker.AddMotion(cDEF.Run.Motion.Find("VCM Head X"));
                                cDEF.Run.VCMPicker.AddMotion(cDEF.Run.Motion.Find("VCM Head Y"));
                                cDEF.Run.VCMPicker.AddMotion(cDEF.Run.Motion.Find("VCM Head Z"));
                                cDEF.Run.VCMPicker.AddMotion(cDEF.Run.Motion.Find("VCM Head T"));
                                cDEF.Run.VCMPicker.AddMotion(cDEF.Run.Motion.Find("VCM Stage Y"));

                                cDEF.Run.LensLoader.AddMotion(cDEF.Run.Motion.Find("Lens Magazine Z"));
                                cDEF.Run.LensLoader.AddMotion(cDEF.Run.Motion.Find("Lens Transfer Y"));
                                

                                cDEF.Run.LensPicker.AddMotion(cDEF.Run.Motion.Find("Lens Head X"));
                                cDEF.Run.LensPicker.AddMotion(cDEF.Run.Motion.Find("Lens Head Y"));
                                cDEF.Run.LensPicker.AddMotion(cDEF.Run.Motion.Find("Lens Head Z"));
                                cDEF.Run.LensPicker.AddMotion(cDEF.Run.Motion.Find("Lens Head T"));
                                cDEF.Run.LensPicker.AddMotion(cDEF.Run.Motion.Find("Lens Stage X"));

                                cDEF.Run.JigPlateAngle.AddMotion(cDEF.Run.Motion.Find("Jig PlateAngle Y"));

                                cDEF.Run.Bonder1.AddMotion(cDEF.Run.Motion.Find("Bond1 Head X"));
                                cDEF.Run.Bonder1.AddMotion(cDEF.Run.Motion.Find("Bond1 Head Y"));
                                cDEF.Run.Bonder1.AddMotion(cDEF.Run.Motion.Find("Bond1 Head Z"));

                                cDEF.Run.Unloader.AddMotion(cDEF.Run.Motion.Find("Unload Magazine Z"));
                                cDEF.Run.Unloader.AddMotion(cDEF.Run.Motion.Find("Unload Transfer X"));
                                

                                cDEF.Run.UnloadPicker.AddMotion(cDEF.Run.Motion.Find("Unload Head X"));
                                cDEF.Run.UnloadPicker.AddMotion(cDEF.Run.Motion.Find("Unload Head Y"));
                                cDEF.Run.UnloadPicker.AddMotion(cDEF.Run.Motion.Find("Unload Head Z"));
                                cDEF.Run.UnloadPicker.AddMotion(cDEF.Run.Motion.Find("Unload Head T"));
                                cDEF.Run.UnloadPicker.AddMotion(cDEF.Run.Motion.Find("Unload Stage Y"));

                                cDEF.Run.PlateAngle.AddMotion(cDEF.Run.Motion.Find("Plate Angle Y"));

                                cDEF.Run.Index.AddMotion(cDEF.Run.Motion.Find("Index T"));

                                cDEF.Run.Bonder2.AddMotion(cDEF.Run.Motion.Find("Bond2 Head X"));
                                cDEF.Run.Bonder2.AddMotion(cDEF.Run.Motion.Find("Bond2 Head Y"));
                                cDEF.Run.Bonder2.AddMotion(cDEF.Run.Motion.Find("Bond2 Head Z"));

                                cDEF.Run.VCMVision.AddMotion(cDEF.Run.Motion.Find("Lens Insert Top Vision"));

                                //실린더
                                cDEF.Run.VCMLoader.AddCylinder(cDEF.Run.Cylinder.Find("VCM Transfer Clamp"));
                                cDEF.Run.VCMLoader.AddCylinder(cDEF.Run.Cylinder.Find("VCM Stage Clamp"));
                                cDEF.Run.LensLoader.AddCylinder(cDEF.Run.Cylinder.Find("Lens Transfer Clamp"));
                                cDEF.Run.LensLoader.AddCylinder(cDEF.Run.Cylinder.Find("Lens Stage Clamp"));
                                cDEF.Run.Bonder1.AddCylinder(cDEF.Run.Cylinder.Find("Bonder1 Tip Clean"));
                                cDEF.Run.Bonder2.AddCylinder(cDEF.Run.Cylinder.Find("Bonder2 Tip Clean"));
                                cDEF.Run.Curing1.AddCylinder(cDEF.Run.Cylinder.Find("Cure1 Contact"));
                                cDEF.Run.Curing1.AddCylinder(cDEF.Run.Cylinder.Find("Cure1 UV Down"));
                                //cDEF.Run.Curing2.AddCylinder(cDEF.Run.Cylinder.Find("Cure2 Contact"));
                                cDEF.Run.Curing2.AddCylinder(cDEF.Run.Cylinder.Find("Cure2 UV Down"));
                                cDEF.Run.CleanJig.AddCylinder(cDEF.Run.Cylinder.Find("Jig Clean Down"));
                                cDEF.Run.Unloader.AddCylinder(cDEF.Run.Cylinder.Find("Unload Transfer Clamp"));
                                cDEF.Run.Unloader.AddCylinder(cDEF.Run.Cylinder.Find("Unload Stage Clamp"));
                                cDEF.Run.PlateAngle.AddCylinder(cDEF.Run.Cylinder.Find("Plate Angle Down"));
                                cDEF.Run.PlateAngle.AddCylinder(cDEF.Run.Cylinder.Find("Plate Angle FW"));
                                cDEF.Run.PlateAngle.AddCylinder(cDEF.Run.Cylinder.Find("Plate Angle Clamp"));
                                cDEF.Run.PlateAngle.AddCylinder(cDEF.Run.Cylinder.Find("Plate Angle Contact"));
                                cDEF.Run.VCMPicker.AddCylinder(cDEF.Run.Cylinder.Find("VCM Clamp"));

                                if (cDEF.Work.Option.ActuatingType == 1)
                                {
                                    cDEF.Run.Act3.AddCylinder(cDEF.Run.Cylinder.Find("Act3 Contact"));
                                    cDEF.Run.Act3.AddCylinder(cDEF.Run.Cylinder.Find("Act3 ClampDown"));
                                }

                            }
                            break;

                        case 4:
                            if (FStyle == 0)
                            {
                                cDEF.Run.VCMLoader.Information.VCM_Magazine.Init(cDEF.Work.VCMLoader.SlotCount, cDEF.Work.VCMLoader.TrayCountX, cDEF.Work.VCMLoader.TrayCountY);
                                cDEF.Run.VCMPicker.Information.VCM_Tray.Init(cDEF.Work.VCMLoader.TrayCountX, cDEF.Work.VCMLoader.TrayCountY);
                                cDEF.Run.LensLoader.Information.Lens_Magazine.Init(cDEF.Work.LensLoader.SlotCount, cDEF.Work.LensLoader.TrayCountX, cDEF.Work.LensLoader.TrayCountY);
                                cDEF.Run.LensPicker.Information.Lens_Tray.Init(cDEF.Work.LensLoader.TrayCountX, cDEF.Work.LensLoader.TrayCountY);
                                cDEF.Run.Unloader.Information.Unloader_Magazine.Init(cDEF.Work.Unloader.SlotCount, cDEF.Work.Unloader.TrayCountX, cDEF.Work.Unloader.TrayCountY);
                                cDEF.Run.UnloadPicker.Information.Unloader_Tray.Init(cDEF.Work.Unloader.TrayCountX, cDEF.Work.Unloader.TrayCountY);
                                cDEF.Run.UnloadPicker.Information.NG_tray.Init(cDEF.Work.Unloader.NG_TrayCountX, cDEF.Work.Unloader.NG_TrayCountY);
                            }
                            break;

                        case (int)PROGRESS_STEP.STEP - 1:
                            if (FStyle == 1)
                            {
                                lbStatus.Text = "TERMINATE SEQUENCE MODULE";
                            }
                            break;

                    }
                    break;
                case PROGRESS.PROGRESS_LASER:
                    switch (FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                                lbStatus.Text = "INITIALIZE LASER MODULE";
                            break;

                        case 1:
                            // Dispenser
                            if (FStyle == 0)
                            {
                                // cDEF.Dispenser1.Init("COM2");
                                if (cDEF.Work.DispSensor.DispenserType == 0)  // Jetting
                                {
                                    cDEF.Dispenser1.Init(cDEF.Work.DispSensor.Pizco1_Port);
                                    cDEF.Dispenser2.Init(cDEF.Work.DispSensor.Pizco2_Port);
                                }
                                else if (cDEF.Work.DispSensor.DispenserType == 1)  // Jetting
                                {
                                    cDEF.DispenserEcm1.Init(cDEF.Work.DispSensor.Pizco1_Port);
                                    cDEF.DispenserEcm2.Init(cDEF.Work.DispSensor.Pizco2_Port);                                    
                                }
                                else if (cDEF.Work.DispSensor.DispenserType == 2)  // Jetting
                                {
                                    // 통신 연결
                                    cDEF.TJV_1.Init(cDEF.Work.Recipe.TJV_IP_1);
                                    cDEF.TJV_2.Init(cDEF.Work.Recipe.TJV_IP_2);

                                }

                                cDEF.SideAngleMeasuring.Init(cDEF.Work.Measuring.FaceAngle_IP, cDEF.Work.Measuring.FaceAngle_Port, "SideAngle");
                            }
                            else
                            {
                                cDEF.Visions.Visions.Final();
                            }
                            break;

                        case 2:
                            //Analog
                            if (FStyle == 0)
                            {
                                cDEF.Run.Analog.AnalogItems[0].SetRange(0, 10);

                                // Dispenser
                                if (cDEF.Work.DispSensor.DispenserType == 1)
                                {
                                    cDEF.DispenserEcm1.CMDMode = clsSuperEcm3.ECMDMode.ChangeMode;
                                    cDEF.DispenserEcm1.SetMode = cDEF.Work.Project.GlobalOption.JettingMode1;
                                    cDEF.DispenserEcm1.SetValueStart();

                                    cDEF.DispenserEcm2.CMDMode = clsSuperEcm3.ECMDMode.ChangeMode;
                                    cDEF.DispenserEcm2.SetMode = cDEF.Work.Project.GlobalOption.JettingMode2;
                                    cDEF.DispenserEcm2.SetValueStart();
                                }
                                if (cDEF.Work.DispSensor.DispenserType == 2 && cDEF.TJV_1.Connect && cDEF.TJV_2.Connect)
                                {
                                    // Data Upload
                                    cDEF.TJV_1.DispensorWave(cDEF.Work.Recipe.Inivolt_1, cDEF.Work.Recipe.falltime_1, cDEF.Work.Recipe.openvolt_1, cDEF.Work.Recipe.opentime_1, cDEF.Work.Recipe.risetime_1, cDEF.Work.Recipe.pixelcount_1, 0);
                                    cDEF.TJV_2.DispensorWave(cDEF.Work.Recipe.Inivolt_2, cDEF.Work.Recipe.falltime_2, cDEF.Work.Recipe.openvolt_2, cDEF.Work.Recipe.opentime_2, cDEF.Work.Recipe.risetime_2, cDEF.Work.Recipe.pixelcount_2, 0);

                                    // Trigger Mode Set
                                    cDEF.TJV_1.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_1);
                                    cDEF.TJV_2.PDDSetTriggerMode(cDEF.Work.Recipe.WorkMode_2);

                                    // Ready to Spitting.
                                    cDEF.TJV_1.PDDStartSpitting(cDEF.Work.Recipe.Hz_1, cDEF.Work.Recipe.nDrop_1);
                                    cDEF.TJV_2.PDDStartSpitting(cDEF.Work.Recipe.Hz_2, cDEF.Work.Recipe.nDrop_2);

                                    // TJV Sequence : Init > Data Upload > Trigger Mode Set > Start Spitting > I/O On _ Off
                                }

                            }
                            break;

                        case 3:
                            // Actuator
                            if (FStyle == 0)
                            {
                                if (cDEF.Work.DispSensor.DispenserType == 2)
                                {
                                    if (cDEF.Work.Project.GlobalOption.Use_TJV_Cooling1)
                                        cDEF.Run.Digital.Output[cDO.TJV_1_Cooling] = true;
                                    if (cDEF.Work.Project.GlobalOption.Use_TJV_Cooling2)
                                        cDEF.Run.Digital.Output[cDO.TJV_2_Cooling] = true;

                                
                                }


#if !Notebook



                                //if (cDEF.Work.Project.GlobalOption.Use_TJV_Cooling)
                                //    // Air Cooling On
                                //else
                                //    // Air Cooling OfF

                                //MES Agent 실행
                                //2021-04-28 Modify

                                if (cDEF.Work.Option.MESAgentPath != "")
                                {
                                    if (!isProcess(Path.GetFileNameWithoutExtension(cDEF.Work.Option.MESAgentPath)))
                                    {
                                        Process.Start(cDEF.Work.Option.MESAgentPath);
                                    }
                                }
                                
#endif
                            }
                            else
                            {

                                if (cDEF.Work.DispSensor.DispenserType == 2 && cDEF.TJV_1.Connect && cDEF.TJV_2.Connect)
                                {
                                    cDEF.TJV_1.PDDStopSpitting();
                                    cDEF.TJV_2.PDDStopSpitting();


                                }
                                try
                                {
                                    //MES Agent 종료
                                    //2021-04-28 Modify
                                    if (cDEF.Work.Option.MESAgentPath != "")
                                    {
                                        Process[] pLIST;
                                        pLIST = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(cDEF.Work.Option.MESAgentPath));

                                        foreach (Process Proc in pLIST)
                                        {
                                            Proc.Kill();
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            
                            }
                            break;

                        case (int)PROGRESS_STEP.STEP - 1:
                            if (FStyle == 1)
                            {
                                lbStatus.Text = "TERMINATE LASER MODULE";
                            }
                            break;

                    }
                    break;

                case PROGRESS.PROGRESS_END:
                    switch (FStep)
                    {
                        case 0:
                            if (FStyle == 0)
                            {
                                lbStatus.Text = "INITIALIZE SUCCESS";
#if !Notebook
                                cDEF.Mes.Start();
                                if (cDEF.Work.Project.GlobalOption.UseMES)
                                {
                                    cDEF.Run.MESEQPStatus = Unit.eMESEqpStatus.SETUP;
                                    cDEF.Run.MesStatusMsg = "START PROGRAM";
                                    cDEF.Mes.Send_EquipStatus();
                                }
#endif
                            }
                            else
                            {
                                cDEF.Run.RunEnd();
                                if (XModuleMain.frame.mmTimer != null)
                                    XModuleMain.frame.mmTimer = null;
                            }                              
                            break;

                        case 1:
                            if (FStyle == 0)
                                XModuleMain.frame.fpBeginUpdate();//mmTimer.Start();
                            else
                                XModuleMain.frame.fpEndUpdate();//mmTimer.Stop();
					        break;

                        case 2:
                            if (FStyle == 0)
                            {
                                cDEF.Lang.Load();                              
                                cDEF.ChangeLanguage();
                            }
                            else
                            {
                            }
                            break;

                        case 3:
                            if (FStyle == 0)
                            {
                                Tag = 1;
                                FStyle = 1;
                                
                                Close();
                            }
                            else
                                lbStatus.Text = "TERMINATE START";
                            break;
                    }
                    break;
                       
            }
            if (FStyle == 0)
            {
                if (FStep + 1 == (int)PROGRESS_STEP.STEP)
                {
                    FMode ++;
                    FStep = 0;
                }
                else
                    FStep ++;
            }
            else
            {
                if (FStep == 0)
                {
                    FMode --;
                    FStep = (int)PROGRESS_STEP.STEP - 1;
                }
                else
                    FStep --;
            }
            this.timer1.Enabled = true;
        }

        private bool isProcess(string name)
        {
            Process[] pLIST;
            pLIST = Process.GetProcessesByName(name);

            foreach (Process Proc in pLIST)
            {
                return true;
            }
            return false;
        }
        private void frmProgress_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
            
        }
    }
}

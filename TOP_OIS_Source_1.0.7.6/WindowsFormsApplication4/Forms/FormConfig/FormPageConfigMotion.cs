using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using XModule.Standard;


namespace XModule.Forms.FormConfig
{
    public partial class FrmPageConfigMotion : TFrame
    {

        private int FSelected = 0;
        private int FSpeedLevel = 0;
		private int FPowerLevel = 0;


		public FrmPageConfigMotion()
        {
            InitializeComponent();
            SetBounds(0, 0, 1790, 998);

        }

        private void FrmPageConfigMotion_Load(object sender, EventArgs e)
        {
            Left = cSystem.formPageLeft;
            Top = cSystem.formPageTop;
            int i;
            String Temp = "";
            String GridValue = "";
            TfpMotionItem MotionItem;
 
            grid.RowTemplate.Height = 30;

            if (grid.RowCount != cDEF.Run.Motion.Count + 1)
                grid.RowCount = cDEF.Run.Motion.Count;

            for (i = 0; i < cDEF.Run.Motion.Count; i++)
            {
                MotionItem = cDEF.Run.Motion.FItems[i];
                Temp = i.ToString();
                if (grid.Rows[i].Cells[0].Value != null)
                    GridValue = grid.Rows[i].Cells[0].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[0].Value = Temp;
                Temp = MotionItem.Key.ToUpper();
                if (grid.Rows[i].Cells[1].Value != null)
                    GridValue = grid.Rows[i].Cells[1].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[1].Value = Temp;
                Temp = MotionItem.Config.FAxis.ToString();
                if (grid.Rows[i].Cells[2].Value != null)
                    GridValue = grid.Rows[i].Cells[2].Value.ToString();
                if (GridValue != Temp)
                    grid.Rows[i].Cells[2].Value = Temp;
            }
               
            UpdateDetailInformation();
        }
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Col, Row = 0;

            Col = grid.CurrentCell.ColumnIndex;
            Row = grid.CurrentCell.RowIndex;
            if (Col > -1 && Row > -1)
            {
                if (FSelected != Row)
                {
                    FSelected = Row;
                    UpdateDetailInformation();
                }
            }
            
        }

        void btnPositionClearClick(object sender, EventArgs e)
        {
	        TfpMotionItem MotionItem = cDEF.Run.Motion.FItems[FSelected];

	        if(MotionItem.Active)
	        {
                cDEF.Run.Motion.FItems[FSelected].CommandPosition = 0;
                cDEF.Run.Motion.FItems[FSelected].ActualPosition = 0;
	        }
        }

        void RepeatClick(object sender, EventArgs e)
        {
	        TfpMotionItem MotionItem = cDEF.Run.Motion.FItems[FSelected];

            switch(Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
		        {
			        double Value = MotionItem.FRepeatPosition[0] / 1000.0;

			        if(XModuleMain.frmBox.fpFloatEdit("FIRST POSITION",ref Value, " mm", "CURRENT", MotionItem.CommandPosition / 1000, -500000, 500000))
			        {
				        MotionItem.FRepeatPosition[0] = Value * 1000.0;
                            cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
			        }
			        break;
		        }

		        case 1:
		        {
			        int Value = MotionItem.FRepeatSleep[0];

                    if (XModuleMain.frmBox.fpIntegerEdit("FIRST POSITION MOVING SLEEP",ref Value, " ms", 0, 60000))
			        {
				        MotionItem.FRepeatSleep[0] = Value;
                            cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
			        }
			        break;
		        }

		        case 2:
                {
                    double Value = MotionItem.FRepeatPosition[1] / 1000.0;

                    if (XModuleMain.frmBox.fpFloatEdit("SECOND POSITION", ref Value, " mm", "CURRENT", MotionItem.CommandPosition / 1000, -500000, 500000))
                    {
                        MotionItem.FRepeatPosition[1] = Value * 1000.0;
                            cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
                    }
                    break;
                }

		        case 3:
		        {
                    int Value = MotionItem.FRepeatSleep[1];

                    if (XModuleMain.frmBox.fpIntegerEdit("SECOND POSITION MOVING SLEEP", ref Value, " ms", 0, 60000))
                    {
                        MotionItem.FRepeatSleep[1] = Value;
                            cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
                    }
                    break;
		        }

		        case 4:
			        MotionItem.FRepeatMode = !MotionItem.FRepeatMode;
			        break;

		        case 5:
			        MotionItem.Absolute(MotionItem.FRepeatPosition[0], MotionItem.FRepeatSleep[0]);
			        break;

		        case 6:
			        MotionItem.Absolute(MotionItem.FRepeatPosition[1], MotionItem.FRepeatSleep[0]);
			        break;

		        case 7:
			        MotionItem.RepeatStart();
			        break;

		        case 8:
			        MotionItem.RepeatStop();
			        break;
	        }
        }

        public void JogClick(object sender, EventArgs e)
        {
            if(Convert.ToInt32((sender as Glass.GlassButton).Tag) == 0)
	        {
		        if(FSpeedLevel == 100)
			        FSpeedLevel = 0;
		        else
			        FSpeedLevel = 100;

                UpdateSpeed();
	        }
	        else
                cDEF.Run.Motion.FItems[FSelected].Stop();
        }

        void JogMouseDown(object sender, MouseEventArgs e)
        {
	        TfpMotionItem MotionItem = cDEF.Run.Motion.FItems[FSelected];

	        MotionItem.Jog((Convert.ToInt32((sender as Glass.GlassButton).Tag) == 1) ? TfpMotionJogDirection.fmjdForward : TfpMotionJogDirection.fmjdBackward, FSpeedLevel);
        }
        void JogMouseUp(object sender, MouseEventArgs e)
        {
            cDEF.Run.Motion.FItems[FSelected].Stop();
        }
        void spSpeedMouseDown(object sender, MouseEventArgs e)
        {
                FSpeedLevel = trackBar2.Value;
		        UpdateSpeed();
        }

        void spSpeedMouseMove(object sender, MouseEventArgs e)
        {
            FSpeedLevel = trackBar2.Value;
            UpdateSpeed();
        }

        public void ConfigurationClick(object sender, EventArgs e)
        {
            TfpMotionItem MotionItem = cDEF.Run.Motion.FItems[FSelected];
            String Temp = "";
            int i, iValue = 0;
            int FTemp;
            double dValue = 0.0;
            FTemp = Convert.ToInt32((sender as Glass.GlassButton).Tag);
            switch (FTemp)
            {
                case 0:
                    for (i = 0; i < cDEF.Run.Motion.Count; i++)
                    {
                        if (i > 0)
                            Temp += ",";
                        Temp += cDEF.Run.Motion.FItems[i].Key.ToUpper();
                    }
                    if (XModuleMain.frmBox.SelectBox("INDEX", Temp, ref FSelected) == DialogResult.No)
                        return;
                    break;
                case 1:
                    iValue = MotionItem.Config.FAxis;
                    if (!XModuleMain.frmBox.fpIntegerEdit("BOARD NO.", ref iValue, "", 0, 1000))
                        return;
                    MotionItem.Config.FAxis = iValue;
                    cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
                    break;
                case 2:
                    dValue = MotionItem.Config.FScale;
                    if (!XModuleMain.frmBox.fpFloatEdit("SCALE", ref dValue, " x"))
                        return;
                    MotionItem.Config.FScale = dValue;
                    cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
                    break;
                case 3:
                    iValue = MotionItem.Config.FPower;
                    if (!XModuleMain.frmBox.fpIntegerEdit("POWER", ref iValue, " %", 0, 100))
                        return;
                    MotionItem.Config.FPower = iValue;
                    cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
                    break;
                case 4:
                    dValue = MotionItem.Config.FUnit;
                    if (!XModuleMain.frmBox.fpFloatEdit("UNIT", ref dValue, " x"))
                        return;
                    MotionItem.Config.FUnit = dValue;
                    cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
                    break;
                case 5:
                    iValue = Convert.ToInt32(MotionItem.Config.FPulse) / 1000;
                    if (!XModuleMain.frmBox.fpIntegerEdit("mm", ref iValue, " mm", 0, 1000000))
                        return;
                    MotionItem.Config.FPulse = iValue * 1000;
                    cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
                    break;

            }
            UpdateDetailInformation();
        }

        public void ConfigClick(object sender, EventArgs e)
        {
	        TfpMotionItem MotionItem = cDEF.Run.Motion.FItems[FSelected];
	        String Temp;
	        int Value;
            double dValue;

	        switch(Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        Value = (int)MotionItem.Config.FOutputMode;
                    Temp = "1PULSE-HIGH/CW(LOW)/CCW(HIGH),1PULSE-HIGH/CW(HIGH)/CCW(LOW),1PULSE-LOW/CW(LOW)/CCW(HIGH),1PULSE-LOW/CW(HIGH)/CCW(LOW)";
                    Temp += ",2PULSE-HIGH/CCW/CW,2PULSE-LOW/CCW/CW,2PULSE-HIGH/CW/CCW,2PULSE-LOW/CW/CCW,2PULSE(90')-CW/CCW,2PULSE(90')-CCW/CW";
			        if(XModuleMain.frmBox.SelectBox("OUTPUT MODE", Temp, ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FOutputMode = (TfpMotionOutput) Value;
			        break;
		        case 1:
			        Value = (int)MotionItem.Config.FInputMode;
                    Temp = "OBVERSE UP/DOWN,OBVERSE SQR1,OBVERSE SQR2,OBVERSE SQR4,REVERSE UP/DOWN,REVERSE SQR1,REVERSE SQR2,REVERSE SQR4";
			        if(XModuleMain.frmBox.SelectBox("INPUT MODE", Temp,ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FInputMode = (TfpMotionInput) Value;
			        break;
		        case 2:
			        Value = (int)MotionItem.Config.FInputSource;
			        if(XModuleMain.frmBox.SelectBox("INPUT SOURCE", "ENCODER,PULSE OUTPUT",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FInputSource = (TfpMotionInputSource) Value;
			        break;
		        case 3:
			        Value = (int)MotionItem.Config.FZPhaseSignal;
			        if(XModuleMain.frmBox.SelectBox("Z PHASE LEVEL", "LOW,HIGH", ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FZPhaseSignal = (TfpMotionSignal) Value;
			        break;
		        case 4:
			        Value = (int)MotionItem.Config.FServoSignal;
			        if(XModuleMain.frmBox.SelectBox("SERVO LEVEL", "LOW,HIGH",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FServoSignal = (TfpMotionSignal) Value;
			        break;
		        case 5:
			        Value = MotionItem.Config.FMaximumVelocity;
			        if(!XModuleMain.frmBox.fpIntegerEdit("MAXIMUM VELOCITY",ref Value, "", 0,1500000))
				        return;
			        MotionItem.Config.FMaximumVelocity = Value;
			        break;
		        case 6:
			        Value = (int)MotionItem.Config.FEmergencySignal.FLevel;
			        if(XModuleMain.frmBox.SelectBox("EMERGENCY SIGNAL LEVEL", "ACTIVE LOW,ACTIVE HIGH,DISABLE,MAINTENANCE",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FEmergencySignal.FLevel = (TfpMotionLevel) Value;
			        break;
		        case 7:
			        Value = (int)MotionItem.Config.FEmergencySignal.FStopMode;
			        if(XModuleMain.frmBox.SelectBox("EMERGENCY SIGNAL STOP MODE", "EMERGENCY,SLOW DOWN",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FEmergencySignal.FStopMode = (TfpMotionStopMode) Value;
			        break;
		        case 8:
			        Value = (int)MotionItem.Config.FInposition.FLevel;
			        if(XModuleMain.frmBox.SelectBox("INPOSITION LEVEL", "ACTIVE LOW,ACTIVE HIGH,DISABLE,MAINTENANCE",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FInposition.FLevel = (TfpMotionLevel) Value;
			        break;
		        case 9:
			        Value = Convert.ToInt32(MotionItem.Config.FInposition.FSoftware);
			        if(XModuleMain.frmBox.SelectBox("INPOSITION SOFTWARE", "DISABLE,ENABLE",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FInposition.FSoftware = Convert.ToBoolean((TfpMotionLevel) Value);
			        break;
		        case 10:
			        dValue = MotionItem.Config.FInposition.FSoftwareLength / 1000.0;
			        if(!XModuleMain.frmBox.fpFloatEdit("INPOSITION SOFTWARE LENGTH",ref dValue, "", 0, 1000))
				        return;
			        MotionItem.Config.FInposition.FSoftwareLength = (int)(dValue * 1000.0);
			        break;
		        case 11:
			        Value = (int)MotionItem.Config.FHome.FSignal;
			        if(XModuleMain.frmBox.SelectBox("HOME SIGNAL","LOW,HIGH",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FHome.FSignal = (TfpMotionSignal) Value;
			        break;
		        case 12:
			        Value = (int)MotionItem.Config.FHome.FMode;
                    Temp = "HOME SENSOR,NEGATIVE LIMIT,NEGATIVE LIMIT ENCODER Z,POSITIVE LIMIT,POSITIVE LIMIT ENCODER Z,ENCODER Z PHASE,NO SENSOR";
			        if(XModuleMain.frmBox.SelectBox("HOME SIGNAL", Temp,ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FHome.FMode = (TfpMotionHomeMode) Value;
			        break;
		        case 13:
			        Value = (int)MotionItem.Config.FAlarm.FResetSignal;
			        if(XModuleMain.frmBox.SelectBox("ALARM RESET SIGNAL", "LOW,HIGH",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FAlarm.FResetSignal = (TfpMotionSignal) Value;
			        break;
		        case 14:
			        Value = (int)MotionItem.Config.FAlarm.FLevel;
			        if(XModuleMain.frmBox.SelectBox("ALARM LEVEL", "ACTIVE LOW,ACTIVE HIGH,DISABLE,MAINTENANCE",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FAlarm.FLevel = (TfpMotionLevel) Value;
			        break;
		        case 15:
			        Value = (int)MotionItem.Config.FLimit.FStopMode;
			        if(XModuleMain.frmBox.SelectBox("LIMIT STOP MODE", "EMERGENCY,SLOW DOWN",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FLimit.FStopMode = (TfpMotionStopMode) Value;
			        break;
		        case 16:
			        Value = (int)MotionItem.Config.FLimit.FNegativeLevel;
			        if(XModuleMain.frmBox.SelectBox("LIMIT NEGATIVE LEVEL", "ACTIVE LOW,ACTIVE HIGH,DISABLE,MAINTENANCE",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FLimit.FNegativeLevel = (TfpMotionLevel) Value;
			        break;
		        case 17:
			        Value = (int)MotionItem.Config.FLimit.FPositiveLevel;
			        if(XModuleMain.frmBox.SelectBox("LIMIT POSITIVE LEVEL", "ACTIVE LOW,ACTIVE HIGH,DISABLE,MAINTENANCE",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FLimit.FPositiveLevel = (TfpMotionLevel) Value;
			        break;
		        case 18:
			        Value = (int)MotionItem.Config.FLimit.FSoftware;
			        if(XModuleMain.frmBox.SelectBox("LIMIT SOFTWARE", "DISABLE,ENABLE",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FLimit.FSoftware = (uint)Value;
			        break;
		        case 19:
			        dValue = MotionItem.Config.FLimit.FSoftwareNegative / 1000.0;
			        if(!XModuleMain.frmBox.fpFloatEdit("LIMIT SOFTWARE NEGATIVE",ref dValue, " mm"))
				        return;
                    MotionItem.Config.FLimit.FSoftwareNegative = dValue * 1000.0;
			        break;
		        case 20:
                    dValue = (int)MotionItem.Config.FLimit.FSoftwarePositive / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit("LIMIT SOFTWARE POSITIVE", ref dValue, " mm"))
				        return;
                    MotionItem.Config.FLimit.FSoftwarePositive = dValue * 1000.0;
			        break;
		        case 21:
			        Value = MotionItem.Config.FPositionClear.FEnabled;
			        if(XModuleMain.frmBox.SelectBox("POSITION CLEAR ENABLED", "FALSE,TRUE",ref Value) == DialogResult.No)
				        return;
			        MotionItem.Config.FPositionClear.FEnabled = Value;
			        break;
		        case 22:
			        dValue = MotionItem.Config.FPositionClear.FPulse / 1000.0;
                    if (!XModuleMain.frmBox.fpFloatEdit("POSITION CLEAR PULSE", ref dValue, " mm", -900,900))
				        return;
			        MotionItem.Config.FPositionClear.FPulse = dValue * 1000.0;
			        break;
	        }
            MotionItem.Config.Update();
            UpdateDetailInformation();
            cDEF.Run.Motion.SaveConfig(cPath.FILE_MOTION_CONFIG);
        }

        public void SpeedClick(object sender, EventArgs e)
        {
	        TfpMotionItem MotionItem = cDEF.Run.Motion.FItems[FSelected];
	        String Temp;
	        int Value;
	        double dValue;    

	        switch(Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        dValue = MotionItem.Speed.FHome.FFirstVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("HOME FIRST VELOCITY",ref dValue, "", 0, 1000000))
				        return;
			        MotionItem.Speed.FHome.FFirstVelocity = dValue;
			        break;
		        case 1:
			        dValue = MotionItem.Speed.FHome.FSecondVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("HOME SECOND VELOCITY",ref dValue, "", 0, 1000000))
				        return;
			        MotionItem.Speed.FHome.FSecondVelocity = dValue;
			        break;
		        case 2:
			        dValue = MotionItem.Speed.FHome.FThirdVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("HOME THIRD VELOCITY",ref dValue, "", 0, 1000000))
				        return;
			        MotionItem.Speed.FHome.FThirdVelocity = dValue;
			        break;
		        case 3:
			        dValue = MotionItem.Speed.FHome.FLastVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("HOME - LAST VELOCITY",ref dValue, "", 0, 100000))
				        return;
			        MotionItem.Speed.FHome.FLastVelocity = dValue;
			        break;
		        case 4:
			        dValue = MotionItem.Speed.FHome.FFirstAccelerator;
			        if(!XModuleMain.frmBox.fpFloatEdit("HOME FIRST ACCELERATOR",ref dValue, " msec", 0, 60000))
				        return;
			        MotionItem.Speed.FHome.FFirstAccelerator = dValue;
			        break;
		        case 5:
			        dValue = MotionItem.Speed.FHome.FSecondAccelerator;
			        if(!XModuleMain.frmBox.fpFloatEdit("HOME SECOND ACCELERATOR",ref dValue, " msec", 0, 60000))
				        return;
			        MotionItem.Speed.FHome.FSecondAccelerator = dValue;
			        break;
		        case 6:
			        Value = MotionItem.Speed.FHome.FClearTime;
			        if(!XModuleMain.frmBox.fpIntegerEdit("HOME CLEAR TIME",ref Value, " msec", 0, 60000))
				        return;
			        MotionItem.Speed.FHome.FClearTime = Value;
			        break;
		        case 7:
			        dValue = MotionItem.Speed.FHome.FOffset / 1000.0;
			        if(!XModuleMain.frmBox.fpFloatEdit("HOME OFFSET",ref dValue, " mm"))
				        return;
			        MotionItem.Speed.FHome.FOffset = dValue * 1000.0;
			        break;
		        case 8:
			        Value = MotionItem.Config.FHome.FTimeOut;
			        if(!XModuleMain.frmBox.fpIntegerEdit("HOME TIMEOUT",ref Value, " msec", 0, 600000))
				        return;
			        MotionItem.Config.FHome.FTimeOut = Value;
			        break;
		        case 9:
			        dValue = MotionItem.Speed.FSlow.FStartVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("SLOW START VELOCITY",ref dValue, "", 0, 100000))
				        return;
			        MotionItem.Speed.FSlow.FStartVelocity = dValue;
			        break;
		        case 10:
			        dValue = MotionItem.Speed.FSlow.FMaximumVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("SLOW - MAXIMUM VELOCITY",ref dValue, "", 0, 100000))
				        return;
			        MotionItem.Speed.FSlow.FMaximumVelocity = dValue;
			        break;
		        case 11:
			        dValue = MotionItem.Speed.FSlow.FAccelerator;
			        if(!XModuleMain.frmBox.fpFloatEdit("SLOW ACCELERATOR",ref dValue, " msec", 0, 6000000))
				        return;
			        MotionItem.Speed.FSlow.FAccelerator = dValue;
			        break;
		        case 12:
			        dValue = MotionItem.Speed.FSlow.FDecelerator;
			        if(!XModuleMain.frmBox.fpFloatEdit("SLOW DECELERATOR",ref dValue, " msec", 0, 60000))
				        return;
			        MotionItem.Speed.FSlow.FDecelerator = dValue;
			        break;
		        case 13:
			        dValue = MotionItem.Speed.FFast.FStartVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("FAST START VELOCITY",ref dValue, "", 0, 10000))
				        return;
			        MotionItem.Speed.FFast.FStartVelocity = dValue;
			        break;
		        case 14:
			        dValue = MotionItem.Speed.FFast.FMaximumVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("FAST MAXIMUM VELOCITY",ref dValue, "", 0, 100000))
				        return;
			        MotionItem.Speed.FFast.FMaximumVelocity = dValue;
			        break;
		        case 15:
			        dValue = MotionItem.Speed.FFast.FAccelerator;
			        if(!XModuleMain.frmBox.fpFloatEdit("FAST ACCELERATOR",ref dValue, " msec", 0, 60000))
				        return;
			        MotionItem.Speed.FFast.FAccelerator = dValue;
			        break;
		        case 16:
			        dValue = MotionItem.Speed.FFast.FDecelerator;
			        if(!XModuleMain.frmBox.fpFloatEdit("FAST DECELERATOR",ref dValue, " msec", 0, 60000))
				        return;
			        MotionItem.Speed.FFast.FDecelerator = dValue;
			        break;
		        case 17:
			        dValue = MotionItem.Speed.FRun.FStartVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("RUN START VELOCITY",ref dValue, "", 0, 1000000))
				        return;
			        MotionItem.Speed.FRun.FStartVelocity = dValue;
			        break;
		        case 18:
			        dValue = MotionItem.Speed.FRun.FMaximumVelocity;
			        if(!XModuleMain.frmBox.fpFloatEdit("RUN MAXIMUM VELOCITY",ref dValue, "", 0, 10000000))
				        return;
			        MotionItem.Speed.FRun.FMaximumVelocity = dValue;
			        break;
		        case 19:
			        dValue = MotionItem.Speed.FRun.FAccelerator;
			        if(!XModuleMain.frmBox.fpFloatEdit("RUN ACCELERATOR",ref dValue, " msec", 0, 60000))
				        return;
			        MotionItem.Speed.FRun.FAccelerator = dValue;
			        break;
		        case 20:
			        dValue = MotionItem.Speed.FRun.FDecelerator;
			        if(!XModuleMain.frmBox.fpFloatEdit("RUN DECELERATOR",ref dValue, " msec", 0, 60000))
				        return;
			        MotionItem.Speed.FRun.FDecelerator = dValue;
			        break;
		        case 21:
			        Value = (int)MotionItem.Speed.FRun.FProfile;

                    Temp = "SYMMETRY TRAPEZOIDE,ASYMMETRY TRAPEZOIDE,SYMMETRY S CURVE,ASYMMETRY S CURVE,M3_SYMMETRY TRAPEZOIDE,M3_ASYMMETRY TRAPEZOIDE,M3_SYMMETRY S CURVE,M3_ASYMMETRY S CURVE";

			        if(XModuleMain.frmBox.SelectBox("RUN PROFILE", Temp, ref Value) == DialogResult.No)
				        return;
			        MotionItem.Speed.FRun.FProfile = (TfpMotionProfile) Value;
			        break;
		        case 22:
			        dValue = MotionItem.Speed.FRun.FAcceleratorJerk;
			        if(!XModuleMain.frmBox.fpFloatEdit("RUN ACCELERATOR JERK",ref dValue, " mm", 0, 100))
				        return;
			        MotionItem.Speed.FRun.FAcceleratorJerk = dValue;
			        break;
		        case 23:
			        dValue = MotionItem.Speed.FRun.FDeceleratorJerk;
			        if(!XModuleMain.frmBox.fpFloatEdit("RUN DECELERATOR JERK",ref dValue, " mm", 0, 100))
				        return;
			        MotionItem.Speed.FRun.FDeceleratorJerk = dValue;
			        break;
		        case 24:
			        Value = MotionItem.Config.FInposition.FTimeOut;
			        if(!XModuleMain.frmBox.fpIntegerEdit("RUN TIMEOUT",ref Value, " msec", 0, 600000))
				        return;
			        MotionItem.Config.FInposition.FTimeOut = Value;
			        break;
		        case 25:
		        {
			        Value = -1;
			        if(XModuleMain.frmBox.SelectBox("SPEED ITEMS", "PATTERN #1,PATTERN #2,PATTERN #3,PATTERN #4,PATTERN #5",ref Value) == DialogResult.No)
				        return;
			        int Enabled = Convert.ToInt32(MotionItem.Speed.FItems[Value].FEnable);
			        double Length = MotionItem.Speed.FItems[Value].FLength;
			        double StartVelocity = MotionItem.Speed.FItems[Value].FStartVelocity;
			        double MaximumVelocity = MotionItem.Speed.FItems[Value].FMaximumVelocity;
			        double Accelerator = MotionItem.Speed.FItems[Value].FAccelerator;
			        double Decelerator = MotionItem.Speed.FItems[Value].FDecelerator;
			        int Profile = (int)MotionItem.Speed.FItems[Value].FProfile;
			        double AcceleratorJerk = MotionItem.Speed.FItems[Value].FAcceleratorJerk;
			        double DeceleratorJerk = MotionItem.Speed.FItems[Value].FDeceleratorJerk;
                        XModuleMain.frmBox.fpScriptBoxClear();
                        XModuleMain.frmBox.fpScriptBoxAdd("ENABLED", "", "FALSE,TRUE",ref Enabled, 16);
                        XModuleMain.frmBox.fpScriptBoxAdd("LENGTH", "", "",ref Length, " pulse", 0, 1000000);
                        XModuleMain.frmBox.fpScriptBoxAdd("START VELOCITY", "", "",ref StartVelocity, "", 0, 1000000);
                        XModuleMain.frmBox.fpScriptBoxAdd("MAXIMUM VELOCITY", "", "",ref MaximumVelocity, "", 0, 1000000);
                        XModuleMain.frmBox.fpScriptBoxAdd("ACCELERATOR", "", "",ref Accelerator, " msec", 0, 60000);
                        XModuleMain.frmBox.fpScriptBoxAdd("DECELERATOR", "", "",ref Decelerator, " msec", 0, 60000);
                    
                    Temp = "SYMMETRY TRAPEZOIDE,ASYMMETRY TRAPEZOIDE,SYMMETRY S CURVE,ASYMMETRY S CURVE,M3_SYMMETRY TRAPEZOIDE,M3_ASYMMETRY TRAPEZOIDE,M3_SYMMETRY S CURVE,M3_ASYMMETRY S CURVE";

                        XModuleMain.frmBox.fpScriptBoxAdd("PROFILE", "", Temp, ref Profile, 16);
                        XModuleMain.frmBox.fpScriptBoxAdd("ACCELERATOR JERK", "", "",ref AcceleratorJerk, " %", 0, 100);
                        XModuleMain.frmBox.fpScriptBoxAdd("DECELERATOR JERK", "", "",ref DeceleratorJerk, " %", 0, 100);
			        if(!XModuleMain.frmBox.fpScriptBox("SPEED PATTERN #" + (Value).ToString())) 
				        return;

			        MotionItem.Speed.FItems[Value].FEnable = Convert.ToBoolean(XModuleMain.iValue[0]);
                    MotionItem.Speed.FItems[Value].FLength = XModuleMain.dValue[0];
                    MotionItem.Speed.FItems[Value].FStartVelocity = XModuleMain.dValue[1];
                    MotionItem.Speed.FItems[Value].FMaximumVelocity = XModuleMain.dValue[2];
                    MotionItem.Speed.FItems[Value].FAccelerator = XModuleMain.dValue[3];
                    MotionItem.Speed.FItems[Value].FDecelerator = XModuleMain.dValue[4];
                    MotionItem.Speed.FItems[Value].FProfile = (TfpMotionProfile)XModuleMain.iValue[1];
                    MotionItem.Speed.FItems[Value].FAcceleratorJerk = XModuleMain.dValue[5];
                    MotionItem.Speed.FItems[Value].FDeceleratorJerk = XModuleMain.dValue[6];
			        break;
		        }
                


            }
            UpdateDetailInformation();
            cDEF.Run.Motion.SaveSpeed(cPath.FILE_MOTION_SPEED);
        }
        public void ActionClick(object sender, EventArgs e)
        {
	        TfpMotionItem MotionItem = cDEF.Run.Motion.FItems[FSelected];
	        int i;

            switch (Convert.ToInt32((sender as Glass.GlassButton).Tag))
	        {
		        case 0:
			        MotionItem.Active = true;
			        break;
		        case 1:
			        MotionItem.Active = false;
			        break;
		        case 2:
			        MotionItem.Home();
			        break;
		        case 3:
                    cDEF.Run.Motion.Stop();
			        break;
		        case 4:
			        MotionItem.ServoOn = 1;
			        break;
		        case 5:
			        MotionItem.ServoOn = 0;
			        break;
		        case 6:
			        for(i = 0; i < cDEF.Run.Motion.Count; i ++)
                        cDEF.Run.Motion.FItems[i].ServoOn = 0;
			        break;
                case 7:
                    MotionItem.AlarmReset(1);
                    MotionItem.Reset();
                    break;

                case 8:
                    for (i = 0; i < cDEF.Run.Motion.Count; i++)
                        cDEF.Run.Motion.FItems[i].Active = true;
                    break;

                case 9:
                    for (i = 0; i < cDEF.Run.Motion.Count; i++)
                        cDEF.Run.Motion.FItems[i].Active = false;
                    break;

                case 10:
                    for (i = 0; i < cDEF.Run.Motion.Count; i++)
                        cDEF.Run.Motion.FItems[i].ServoOn = 1;
                    break;

                case 11:
                    for (i = 0; i < cDEF.Run.Motion.Count; i++)
                        cDEF.Run.Motion.FItems[i].ServoOn = 0;
                    break;

            }
        }

        public void UpdateDetailInformation()
        {
	        TfpMotionItem MotionItem = cDEF.Run.Motion.FItems[FSelected];

            btnKey.Text = MotionItem.Key.ToUpper();
	        btnBoardNo.Text = MotionItem.Config.FAxis.ToString();
	        btnScale.Text = MotionItem.Config.FScale.ToString() + " x";
	        btnPower.Text = MotionItem.Config.FPower.ToString() + " %";
            btnUnit.Text = MotionItem.Config.FUnit.ToString() + " x";
            btnPulse.Text = Convert.ToString(MotionItem.Config.FPulse / 1000) + " mm";
	        if(MotionItem.Config.FOutputMode == TfpMotionOutput.fmoOneHighLowHigh)
		        btnOutputMode.Text = "1PULSE-HIGH/CW(LOW)/CCW(HIGH)";
            else if (MotionItem.Config.FOutputMode == TfpMotionOutput.fmoOneHighHighLow)
		        btnOutputMode.Text = "1PULSE-HIGH/CW(HIGH)/CCW(LOW)";
            else if (MotionItem.Config.FOutputMode == TfpMotionOutput.fmoOneLowLowHigh)
		        btnOutputMode.Text = "1PULSE-LOW/CW(LOW)/CCW(HIGH)";
            else if (MotionItem.Config.FOutputMode == TfpMotionOutput.fmoOneLowHighLow)
		        btnOutputMode.Text = "1PULSE-LOW/CW(HIGH)/CCW(LOW)";
            else if (MotionItem.Config.FOutputMode == TfpMotionOutput.fmoTwoCcwCwHigh)
		        btnOutputMode.Text = "2PULSE-HIGH/CCW/CW";
            else if (MotionItem.Config.FOutputMode == TfpMotionOutput.fmoTwoCcwCwLow)
		        btnOutputMode.Text = "2PULSE-LOW/CCW/CW";
            else if (MotionItem.Config.FOutputMode == TfpMotionOutput.fmoTwoCwCcwHigh)
		        btnOutputMode.Text = "2PULSE-HIGH/CW/CCW";
            else if (MotionItem.Config.FOutputMode == TfpMotionOutput.fmoTwoCwCcwLow)
		        btnOutputMode.Text = "2PULSE-LOW/CW/CCW";
            else if (MotionItem.Config.FOutputMode == TfpMotionOutput.fmoTwoPhase)
		        btnOutputMode.Text = "2PULSE(90')-CW/CCW";
	        else
		        btnOutputMode.Text = "2PULSE(90')-CCW/CW";
	        if(MotionItem.Config.FInputMode == TfpMotionInput.fmiObverseUpDown)
		        btnInputMode.Text = "OBVERSE UP/DOWN";
            else if (MotionItem.Config.FInputMode == TfpMotionInput.fmiObverseSqr1)
		        btnInputMode.Text = "OBVERSE SQR1";
            else if (MotionItem.Config.FInputMode == TfpMotionInput.fmiObverseSqr2)
		        btnInputMode.Text = "OBVERSE SQR2";
            else if (MotionItem.Config.FInputMode == TfpMotionInput.fmiObverseSqr4)
		        btnInputMode.Text = "OBVERSE SQR4";
            else if (MotionItem.Config.FInputMode == TfpMotionInput.fmiReverseUpDown)
		        btnInputMode.Text = "REVERSE UP/DOWN";
            else if (MotionItem.Config.FInputMode == TfpMotionInput.fmiReverseSqr1)
		        btnInputMode.Text = "REVERSE SQR1";
            else if (MotionItem.Config.FInputMode == TfpMotionInput.fmiReverseSqr2)
		        btnInputMode.Text = "REVERSE SQR2";
	        else
		        btnInputMode.Text = "REVERSE SQR4";
	        if(MotionItem.Config.FInputSource == TfpMotionInputSource.fmisEncoder)
		        btnInputSource.Text = "ENCODER";
	        else
		        btnInputSource.Text = "PULSE OUTPUT";
	        if(MotionItem.Config.FZPhaseSignal == TfpMotionSignal.fmsLow)
		        btnZPhaseLevel.Text = "LOW";
	        else
		        btnZPhaseLevel.Text = "HIGH";
	        if(MotionItem.Config.FServoSignal == TfpMotionSignal.fmsLow)
		        btnServoLevel.Text = "LOW";
	        else
		        btnServoLevel.Text = "HIGH";
	        btnMaximumVelocity.Text = XModuleMain.frmBox.fpIntToStr(MotionItem.Config.FMaximumVelocity);
	        if(MotionItem.Config.FEmergencySignal.FLevel == TfpMotionLevel.fmlActiveLow)
		        btnEmergencyLevel.Text = "ACTIVE LOW";
	        else if(MotionItem.Config.FEmergencySignal.FLevel == TfpMotionLevel.fmlActiveHigh)
		        btnEmergencyLevel.Text = "ACTIVE HIGH";
	        else if(MotionItem.Config.FEmergencySignal.FLevel == TfpMotionLevel.fmlDisable)
		        btnEmergencyLevel.Text = "DISABLE";
	        else
		        btnEmergencyLevel.Text = "MAINTENANCE";
	        if(MotionItem.Config.FEmergencySignal.FStopMode == TfpMotionStopMode.fmsmEmergency)
		        btnEmergencyStopMode.Text = "EMERGENCY";
	        else
		        btnEmergencyStopMode.Text = "SLOW DOWN";
	        if(MotionItem.Config.FInposition.FLevel == TfpMotionLevel.fmlActiveLow)
		        btnInpositionLevel.Text = "ACTIVE LOW";
	        else if(MotionItem.Config.FInposition.FLevel == TfpMotionLevel.fmlActiveHigh)
		        btnInpositionLevel.Text = "ACTIVE HIGH";
	        else if(MotionItem.Config.FInposition.FLevel == TfpMotionLevel.fmlDisable)
		        btnInpositionLevel.Text = "DISABLE";
	        else
		        btnInpositionLevel.Text = "MAINTENANCE";
	        btnInpositionSoftware.Text = (MotionItem.Config.FInposition.FSoftware) ? "ENABLE" : "DISABLE";
	        btnInpositionSoftwareLength.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Config.FInposition.FSoftwareLength) + " mm";
	        if(MotionItem.Config.FHome.FSignal == TfpMotionSignal.fmsLow)
		        btnHomeSignal.Text = "LOW";
	        else
		        btnHomeSignal.Text = "HIGH";
            if (MotionItem.Config.FHome.FMode == TfpMotionHomeMode.fmhmHomeSensor)
                btnHomeMode.Text = "HOME SENSOR";
            else if (MotionItem.Config.FHome.FMode == TfpMotionHomeMode.fmhmNegativeLimit)
                btnHomeMode.Text = "NEGATIVE LIMIT";
            else if (MotionItem.Config.FHome.FMode == TfpMotionHomeMode.fmhmNegativeLimitEncoderZ)
                btnHomeMode.Text = "NEGATIVE LIMIT ENCODER Z";
            else if (MotionItem.Config.FHome.FMode == TfpMotionHomeMode.fmhmPositiveLimit)
                btnHomeMode.Text = "POSITIVE LIMIT";
            else if (MotionItem.Config.FHome.FMode == TfpMotionHomeMode.fmhmPositiveLimitEncoderZ)
                btnHomeMode.Text = "POSITIVE LIMIT ENCODER Z";
            else if (MotionItem.Config.FHome.FMode == TfpMotionHomeMode.fmhmEncoderZPhase)
                btnHomeMode.Text = "ENCODER Z PHASE";
            else
                btnHomeMode.Text = "NO SENSOR";
	        if(MotionItem.Config.FAlarm.FResetSignal == TfpMotionSignal.fmsLow)
		        btnAlarmResetSignal.Text = "LOW";
	        else
		        btnAlarmResetSignal.Text = "HIGH";
	        if(MotionItem.Config.FAlarm.FLevel == TfpMotionLevel.fmlActiveLow)
		        btnAlarmLevel.Text = "ACTIVE LOW";
	        else if(MotionItem.Config.FAlarm.FLevel == TfpMotionLevel.fmlActiveHigh)
		        btnAlarmLevel.Text = "ACTIVE HIGH";
	        else if(MotionItem.Config.FAlarm.FLevel == TfpMotionLevel.fmlDisable)
		        btnAlarmLevel.Text = "DISABLE";
	        else
		        btnAlarmLevel.Text = "MAINTENANCE";
	        if(MotionItem.Config.FLimit.FStopMode == TfpMotionStopMode.fmsmEmergency)
		        btnLimitStopMode.Text = "EMERGENCY";
	        else
		        btnLimitStopMode.Text = "SLOW DOWN";
	        if(MotionItem.Config.FLimit.FNegativeLevel == TfpMotionLevel.fmlActiveLow)
		        btnLimitNegativeLevel.Text = "ACTIVE LOW";
	        else if(MotionItem.Config.FLimit.FNegativeLevel == TfpMotionLevel.fmlActiveHigh)
		        btnLimitNegativeLevel.Text = "ACTIVE HIGH";
	        else if(MotionItem.Config.FLimit.FNegativeLevel == TfpMotionLevel.fmlDisable)
		        btnLimitNegativeLevel.Text = "DISABLE";
	        else
		        btnLimitNegativeLevel.Text = "MAINTENANCE";
	        if(MotionItem.Config.FLimit.FPositiveLevel == TfpMotionLevel.fmlActiveLow)
		        btnLimitPositiveLevel.Text = "ACTIVE LOW";
	        else if(MotionItem.Config.FLimit.FPositiveLevel == TfpMotionLevel.fmlActiveHigh)
		        btnLimitPositiveLevel.Text = "ACTIVE HIGH";
	        else if(MotionItem.Config.FLimit.FPositiveLevel == TfpMotionLevel.fmlDisable)
		        btnLimitPositiveLevel.Text = "DISABLE";
	        else
		        btnLimitPositiveLevel.Text = "MAINTENANCE";
	        btnLimitSoftware.Text = Convert.ToBoolean((MotionItem.Config.FLimit.FSoftware)) ? "ENABLE" : "DISABLE";
            btnLimitSoftwareNegative.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Config.FLimit.FSoftwareNegative / 1000.0)  + " mm";
            btnLimitSoftwarePositive.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Config.FLimit.FSoftwarePositive / 1000.0)  + " mm";
	        btnPositionClearEnabled.Text = Convert.ToBoolean((MotionItem.Config.FPositionClear.FEnabled)) ? "TRUE" : "FALSE";
            btnPositionClearPulse.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Config.FPositionClear.FPulse / 1000.0)  + " mm";
            btnHomeFirstVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FHome.FFirstVelocity);
            btnHomeSecondVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FHome.FSecondVelocity);
            btnHomeThirdVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FHome.FThirdVelocity);
            btnHomeLastVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FHome.FLastVelocity);
            btnHomeFirstAccelerator.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FHome.FFirstAccelerator) + " msec";
            btnHomeSecondAccelerator.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FHome.FSecondAccelerator) + " msec";
            btnHomeClearTime.Text = XModuleMain.frmBox.fpIntToStr(MotionItem.Speed.FHome.FClearTime) + " msec";
            btnHomeOffset.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FHome.FOffset) + " pulse";
            btnHomeTimeOut.Text = XModuleMain.frmBox.fpIntToStr((int)MotionItem.Config.FHome.FTimeOut) + " msec";
            btnSlowStartVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FSlow.FStartVelocity);
            btnSlowMaximumVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FSlow.FMaximumVelocity);
            btnSlowAccelerator.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FSlow.FAccelerator) + " msec";
            btnSlowDecelerator.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FSlow.FDecelerator) + " msec";
            btnFastStartVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FFast.FStartVelocity);
            btnFastMaximumVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FFast.FMaximumVelocity);
            btnFastAccelerator.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FFast.FAccelerator) + " msec";
            btnFastDecelerator.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FFast.FDecelerator) + " msec";
            btnRunStartVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FRun.FStartVelocity);
            btnRunMaximumVelocity.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FRun.FMaximumVelocity);
            btnRunAccelerator.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FRun.FAccelerator) + " msec";
            btnRunDecelerator.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.Speed.FRun.FDecelerator) + " msec";
            btnRunTimeOut.Text = XModuleMain.frmBox.fpIntToStr(MotionItem.Config.FInposition.FTimeOut) + " msec";
	        if(MotionItem.Speed.FRun.FProfile == TfpMotionProfile.fmpSymmetryTrapezoide)
		        btnRunProfile.Text = "SYMMETRY TRAPEZOIDE";
	        else if(MotionItem.Speed.FRun.FProfile == TfpMotionProfile.fmpAsymmetryTrapezoide)
		        btnRunProfile.Text = "ASYMMETRY TRAPEZOIDE";
	        else if(MotionItem.Speed.FRun.FProfile == TfpMotionProfile.fmpQuasiSCurve)
		        btnRunProfile.Text = "QUASI S CURVE";
	        else if(MotionItem.Speed.FRun.FProfile == TfpMotionProfile.fmpSymmetrySCurve)
		        btnRunProfile.Text = "SYMMETRY S CURVE";
	        else
		        btnRunProfile.Text = "ASYMMETRY S CURVE";
            btnRunAcceleratorJerk.Text = MotionItem.Speed.FRun.FAcceleratorJerk.ToString() + " %";
	        btnRunDeceleratorJerk.Text = MotionItem.Speed.FRun.FDeceleratorJerk.ToString() + " %";
	        btnRunItems.Enabled = Convert.ToBoolean(MotionItem.Speed.Count);
            trackBar2.Value = 50;


            //lbSpeedBall.Top = lbSpeedBar.Top + (((double) lbSpeedBar.Height) / 100.0 * ((double) (100 - FSpeedLevel))) - (lbSpeedBall.Height / 2);
            UpdateSpeed();
        }

        public void UpdateSpeed()
        {
	        switch(FSpeedLevel)
	        {
		        case 0:
			        lbSpeedState.BackColor = Color.Blue;
			        btnSpeed.Text = "SLOW";
			        break;
		        case 100:
			        lbSpeedState.BackColor = Color.Red;
			        btnSpeed.Text = "FAST";
			        break;
		        default:
                    lbSpeedState.BackColor = Color.Black;
                    btnSpeed.Text = "POWER: " + FSpeedLevel.ToString() + "%";
			        break;
	        }

	        lbSpeedValue.Text = FSpeedLevel.ToString() + "%";
        }

        protected override void UpdateInformation()
        {
	        if(!Visible)
		        return;

	        TfpMotionItem MotionItem;
	        String Temp = "";
            String GridValue = "";
            bool MotionDone;
	        int i;
            this.Invoke(new Action(delegate()
            {
             

                for (i = 0; i < cDEF.Run.Motion.Count; i++)
                {
                    MotionItem = cDEF.Run.Motion.FItems[i];
                    Temp = i.ToString();
                    if (grid.Rows[i].Cells[0].Value != null)
                        GridValue = grid.Rows[i].Cells[0].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[0].Value = Temp;
                    Temp = MotionItem.Key.ToUpper();
                    if (grid.Rows[i].Cells[1].Value != null)
                        GridValue = grid.Rows[i].Cells[1].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[1].Value = Temp;
                    Temp = MotionItem.Config.FAxis.ToString();
                    if (grid.Rows[i].Cells[2].Value != null)
                        GridValue = grid.Rows[i].Cells[2].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[2].Value = Temp;
                }

                for (i = 0; i < cDEF.Run.Motion.Count; i++)
                {
                    MotionItem = cDEF.Run.Motion.FItems[i];
                    Temp = (MotionItem.Active) ? "READY" : "NONE";
                    grid.Rows[i].Cells[3].Style.BackColor = (MotionItem.Active) ? Color.Blue : Color.White;
                    grid.Rows[i].Cells[3].Style.ForeColor = (MotionItem.Active) ? Color.White : Color.Gray;
                    if (grid.Rows[i].Cells[3].Value != null)
                        GridValue = grid.Rows[i].Cells[3].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[3].Value = Temp;


                    Temp = Convert.ToString(Math.Round(MotionItem.CommandPosition / 1000.0, 3))+ " mm";
                    if (grid.Rows[i].Cells[4].Value != null)
                        GridValue = grid.Rows[i].Cells[4].Value.ToString();
                    if (GridValue != Temp)
                    {
                        grid.Rows[i].Cells[4].Value = Temp;
                        grid.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    Temp = Convert.ToString(Math.Round(MotionItem.ActualPosition / 1000.0, 3)) + " mm";
                    if (grid.Rows[i].Cells[5].Value != null)
                        GridValue = grid.Rows[i].Cells[5].Value.ToString();
                    if (GridValue != Temp)
                    {
                        grid.Rows[i].Cells[5].Value = Temp;
                        grid.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    Temp = Convert.ToBoolean(MotionItem.FDone) ? "ON" : "OFF";
                    grid.Rows[i].Cells[6].Style.BackColor = (Convert.ToBoolean(MotionItem.FDone)) ? Color.Blue : Color.White;
                    grid.Rows[i].Cells[6].Style.ForeColor = (Convert.ToBoolean(MotionItem.FDone)) ? Color.White : Color.Gray;
                    if (grid.Rows[i].Cells[6].Value != null)
                        GridValue = grid.Rows[i].Cells[6].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[6].Value = Temp;

                    Temp = Convert.ToBoolean(MotionItem.FInpositionDone) ? "ON" : "OFF";
                    grid.Rows[i].Cells[7].Style.BackColor = (Convert.ToBoolean(MotionItem.FInpositionDone)) ? Color.Blue : Color.White;
                    grid.Rows[i].Cells[7].Style.ForeColor = (Convert.ToBoolean(MotionItem.FInpositionDone)) ? Color.White : Color.Gray;
                    if (grid.Rows[i].Cells[7].Value != null)
                        GridValue = grid.Rows[i].Cells[7].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[7].Value = Temp;

                    Temp = (MotionItem.FHomeEnd) ? "ON" : "OFF";
                    grid.Rows[i].Cells[8].Style.BackColor = (Convert.ToBoolean(MotionItem.FHomeEnd)) ? Color.Blue : Color.White;
                    grid.Rows[i].Cells[8].Style.ForeColor = (Convert.ToBoolean(MotionItem.FHomeEnd)) ? Color.White : Color.Gray;
                    if (grid.Rows[i].Cells[8].Value != null)
                        GridValue = grid.Rows[i].Cells[8].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[8].Value = Temp;

                    Temp = Convert.ToBoolean(MotionItem.FAlarm) ? "ON" : "OFF";
                    grid.Rows[i].Cells[9].Style.BackColor = (Convert.ToBoolean(MotionItem.FAlarm)) ? Color.Red : Color.White;
                    grid.Rows[i].Cells[9].Style.ForeColor = (Convert.ToBoolean(MotionItem.FAlarm)) ? Color.Gray : Color.Black;
                    if (grid.Rows[i].Cells[9].Value != null)
                        GridValue = grid.Rows[i].Cells[9].Value.ToString();
                    if (GridValue != Temp)
                        grid.Rows[i].Cells[9].Value = Temp;
                }
                MotionItem = cDEF.Run.Motion.FItems[FSelected];
                MotionDone = Convert.ToBoolean(MotionItem.FDone) && ((Convert.ToBoolean(MotionItem.FDone) && !MotionItem.FRepeatMode));
                btnBoardNo.Enabled = !MotionItem.Active;
                btnScale.Enabled = !MotionItem.Active;
                if (MotionItem.Active)
                {
                    lbCommandValue.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.CommandPosition / 1000.0) + " mm";
                    lbCommandValue.ForeColor = System.Drawing.SystemColors.WindowText;
                    lbActualValue.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.ActualPosition / 1000.0) + " mm";
                    lbActualValue.ForeColor = System.Drawing.SystemColors.WindowText;
                    btnPositionClear.Enabled = true;
                    lbErrorValue.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.FErrorLength / 1000.0) + " mm";
                    lbErrorValue.ForeColor = System.Drawing.SystemColors.WindowText;
                    lbStartValue.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.FStartPosition / 1000.0) + " mm";
                    lbStartValue.ForeColor = System.Drawing.SystemColors.WindowText;
                    lbTargetValue.Text = XModuleMain.frmBox.fpIDoubleToStr(MotionItem.FTargetPosition / 1000.0) + " mm";
                    lbTargetValue.ForeColor = System.Drawing.SystemColors.WindowText;
                    lbServoOnState.BackColor = Convert.ToBoolean(MotionItem.FServoOn) ? Color.Blue : Color.Black;
                    lbAlarmState.BackColor = Convert.ToBoolean(MotionItem.FAlarm) ? Color.Red : Color.Black;
                    lbNegativeLimitState.BackColor = Convert.ToBoolean(MotionItem.FNegativeLimit) ? Color.Red : Color.Black;
                    lbHomeSwitchState.BackColor = Convert.ToBoolean(MotionItem.FHomeSwitch) ? Color.Blue : Color.Black;
                    lbPositiveLimitState.BackColor = Convert.ToBoolean(MotionItem.FPositiveLimit) ? Color.Red : Color.Black;
                    lbDoneState.BackColor = Convert.ToBoolean(MotionItem.FDone) ? Color.Blue : Color.Black;
                    lbInpositionState.BackColor = Convert.ToBoolean(MotionItem.FInposition) ? Color.Blue : Color.Black;
                    lbInpositionDoneState.BackColor = Convert.ToBoolean(MotionItem.FInpositionDone) ? Color.Blue : Color.Black;
                    lbInpositionTimeOutState.BackColor = Convert.ToBoolean(MotionItem.FInpositionTimeOut) ? Color.Red : Color.Black;
                    lbHomeEndState.BackColor = (MotionItem.FHomeEnd) ? Color.Blue : Color.Black;
                    lbHomeTimeOutState.BackColor = (MotionItem.FHomeTimeOut) ? Color.Red : Color.Black;
                }
                else
                {
                    lbCommandValue.Text = "--- mm";
                    lbCommandValue.ForeColor = Color.Gray;
                    lbActualValue.Text = "--- mm";
                    lbActualValue.ForeColor = Color.Gray;
                    btnPositionClear.Enabled = false;
                    lbErrorValue.Text = "--- mm";
                    lbErrorValue.ForeColor = Color.Gray;
                    lbStartValue.Text = "--- mm";
                    lbStartValue.ForeColor = Color.Gray;
                    lbTargetValue.Text = "--- mm";
                    lbTargetValue.ForeColor = Color.Gray;
                    lbServoOnState.BackColor = Color.Black;
                    lbAlarmState.BackColor = Color.Black;
                    lbNegativeLimitState.BackColor = Color.Black;
                    lbHomeSwitchState.BackColor = Color.Black;
                    lbPositiveLimitState.BackColor = Color.Black;
                    lbDoneState.BackColor = Color.Black;
                    lbInpositionState.BackColor = Color.Black;
                    lbInpositionDoneState.BackColor = Color.Black;
                    lbInpositionTimeOutState.BackColor = Color.Black;
                    lbHomeEndState.BackColor = Color.Black;
                    lbHomeTimeOutState.BackColor = Color.Black;
                }
                btnRepeatFirstPosition.Text = Convert.ToString(MotionItem.FRepeatPosition[0] / 1000.0) + " mm";
                btnRepeatFirstSleep.Text = MotionItem.FRepeatSleep[0].ToString() + " ms";
                btnRepeatSecondPosition.Text = Convert.ToString(MotionItem.FRepeatPosition[1] / 1000.0) + " mm";
                btnRepeatSecondSleep.Text = MotionItem.FRepeatSleep[1].ToString() + " ms";
                lbRepeatState.BackColor = (MotionItem.FRepeatMode) ? Color.Red : Color.Black;
                btnRepeat.Enabled = Convert.ToBoolean(MotionItem.FInpositionDone);
                btnRepeatFirst.Visible = !MotionItem.FRepeatMode;
                btnRepeatFirst.Enabled = Convert.ToBoolean(MotionItem.FInpositionDone);
                btnRepeatSecond.Visible = !MotionItem.FRepeatMode;
                btnRepeatSecond.Enabled = Convert.ToBoolean(MotionItem.FInpositionDone);
                btnRepeatStart.Visible = MotionItem.FRepeatMode;
                btnRepeatStart.Enabled = Convert.ToBoolean(MotionItem.FInpositionDone) && MotionItem.FRepeatMode;
                btnRepeatStop.Visible = MotionItem.FRepeatMode;
                btnRepeatStop.Enabled = !Convert.ToBoolean(MotionItem.FInpositionDone) && MotionItem.FRepeatMode;
                //btnBackward.Enabled = MotionDone;
                btnStop.Enabled = MotionItem.Active;
                //btnForward.Enabled = MotionDone;
                btnEnable.Enabled = !MotionItem.Active;
                btnDisable.Enabled = MotionItem.Active;
                btnHome.Enabled = Convert.ToBoolean(MotionItem.FDone) && !MotionItem.FPositionMoving && !MotionItem.FHomeMoving;
                btnServoOn.Enabled = MotionItem.FActive && !Convert.ToBoolean(MotionItem.FServoOn);
                btnServoOff.Enabled = MotionItem.FActive && Convert.ToBoolean(MotionItem.FServoOn);
            }));    
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //UpdateInformation();
            timer1.Enabled = true;
        }
        public void ChangeLanguage()
        {
            groupBoxInform.Text = cDEF.Lang.Trans("Information");
            btnEnable.Text = cDEF.Lang.Trans("ENABLE");
            btnDisable.Text = cDEF.Lang.Trans("DISABLE");
            btnHome.Text = cDEF.Lang.Trans("HOME");
            btnReset.Text = cDEF.Lang.Trans("RESET");
            btnAllStop.Text = cDEF.Lang.Trans("ALL STOP");
            btnAllServoOff.Text = cDEF.Lang.Trans("ALL SERVO OFF");
            btnServoOn.Text = cDEF.Lang.Trans("SERVO ON");
            btnServoOff.Text = cDEF.Lang.Trans("SERVO OFF");
            btnAllEnable.Text = cDEF.Lang.Trans("ALL   ENABLE");
            btnAllDisable.Text = cDEF.Lang.Trans("ALL DISABLE");
            btnAllServoOn.Text = cDEF.Lang.Trans("ALL     SERVO ON");
            btnServoOffAll.Text = cDEF.Lang.Trans("ALL     SERVO OFF");
            groupBoxPosition.Text = cDEF.Lang.Trans("POSITION");
            groupBoxStatus.Text = cDEF.Lang.Trans("STATUS");
            groupBoxRepeat.Text = cDEF.Lang.Trans("REPEAT");
            groupBoxJog.Text = cDEF.Lang.Trans("JOG");
            btnPositionClear.Text = cDEF.Lang.Trans("CLEAR");
            groupBoxConfig.Text = cDEF.Lang.Trans("CONFIG");
            groupBoxEmergencySignal.Text = cDEF.Lang.Trans("EMERGENCY SIGNAL");
            groupBoxInPosition.Text = cDEF.Lang.Trans("INPOSITION");
            groupBoxHome.Text = cDEF.Lang.Trans("HOME");
            groupBoxAlarm.Text = cDEF.Lang.Trans("ALARM");
            groupBoxSensorLimit.Text = cDEF.Lang.Trans("SENSOR && LIMIT");
            groupBoxPositionClear.Text = cDEF.Lang.Trans("POSITION CLEAR");
            groupBoxHome_.Text = cDEF.Lang.Trans("HOME");
            groupBoxSlow.Text = cDEF.Lang.Trans("SLOW");
            groupBoxFast.Text = cDEF.Lang.Trans("FAST");
            groupBoxRun.Text = cDEF.Lang.Trans("RUN");
            Status.Text = cDEF.Lang.Trans("STATUS");
            Config.Text = cDEF.Lang.Trans("CONFIG");
            Speed.Text = cDEF.Lang.Trans("SPEED");
        }

		private void trackBarPower_MouseDown(object sender, MouseEventArgs e)
		{
			FPowerLevel = trackBarPower.Value;
			lbPowerLevel.Text = $"POWER : {FPowerLevel} %";
			cDEF.Run.Motion.FPower = FPowerLevel;
		}

		private void trackBarPower_MouseMove(object sender, MouseEventArgs e)
		{
			FPowerLevel = trackBarPower.Value;
			lbPowerLevel.Text = $"POWER : {FPowerLevel} %";
			cDEF.Run.Motion.FPower = FPowerLevel;
		}
	}
}

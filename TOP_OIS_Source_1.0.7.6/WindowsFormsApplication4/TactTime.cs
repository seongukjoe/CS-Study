using System;

namespace XModule
{
    public class TactTimeItem
    {
        public DateTime StartTime;
        private TimeSpan FCycleTime;
        public TactTimeItem()
        {
            StartTime = new DateTime();
            FCycleTime = new TimeSpan();
        }
        public void Start()
        {
            StartTime = DateTime.Now;
        }
        public void GetTact()
        {
            FCycleTime = DateTime.Now - StartTime;
        }
        public double CycleTime
        {
            get { return FCycleTime.TotalSeconds; }
        }

    }
    public class TactTime
    {
        public TactTimeItem LensTact;   // 배출택
        public TactTimeItem VCMLoader;
        public TactTimeItem VCMPicker;
        public TactTimeItem LensLoader;
        public TactTimeItem LensPicker;
        public TactTimeItem LensPickerPick;
        public TactTimeItem LensPickerPickCam;
        public TactTimeItem LensPickerPlace;
        public TactTimeItem LensPickerCam;
        public TactTimeItem JigPlateAngle;
        public TactTimeItem LensHeight;
        public TactTimeItem Bonder1;
        public TactTimeItem Bonder1GapMesure;
        public TactTimeItem Bonder1Cam;
        public TactTimeItem Bonder2;
        public TactTimeItem Bonder2GapMesure;
        public TactTimeItem Bonder2Cam;
        public TactTimeItem VisionInspect;
        public TactTimeItem Curing1;
        public TactTimeItem Curing2;
        public TactTimeItem PlateAngle;
        public TactTimeItem Unloader;
        public TactTimeItem UnloadPicker;
        public TactTimeItem CleanJig;
        public TactTimeItem Index;
        public TactTimeItem TopVision;
        public TactTimeItem Actuator3;
        public TactTime()
        {
            LensTact = new TactTimeItem();

            VCMLoader = new TactTimeItem();
            VCMPicker = new TactTimeItem();
            LensLoader = new TactTimeItem();
            LensPicker = new TactTimeItem();
            LensPickerPick = new TactTimeItem();
            LensPickerPickCam = new TactTimeItem();
            LensPickerPlace = new TactTimeItem();
            LensPickerCam = new TactTimeItem();
            JigPlateAngle = new TactTimeItem();
            LensHeight = new TactTimeItem();
            Bonder1 = new TactTimeItem();
            Bonder1Cam = new TactTimeItem();
            Bonder1GapMesure = new TactTimeItem();
            Bonder2 = new TactTimeItem();
            Bonder2Cam = new TactTimeItem();
            Bonder2GapMesure = new TactTimeItem();
            VisionInspect = new TactTimeItem();
            Curing1 = new TactTimeItem();
            Curing2 = new TactTimeItem();
            PlateAngle = new TactTimeItem();
            Unloader = new TactTimeItem();
            UnloadPicker = new TactTimeItem();
            CleanJig = new TactTimeItem();
            Index = new TactTimeItem();
            TopVision = new TactTimeItem();
            Actuator3 = new TactTimeItem();
        }



    }
}

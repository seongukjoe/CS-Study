using System;
using System.IO;
using XModule.Standard;


namespace XModule.Working
{
    public class TWorking
    {
        private WorkProject FProject;
        private WorkRecipe FRecipe;
        private WorkingOption FOption;
        private MesStandardInfo FMesStandInfo;    //2021-04-28 Modify

        private WorkingVCMLoader FVCMLoader;
        private WorkingVCMPicker FVCMPicker;
        private WorkingLensLoader FLensLoader;
        private WorkingLensPicker FLensPicker;
        private WorkingJigPlateAngle FJigPlateAngle;
        private WorkingLensHeight FLensHeight;
        private WorkingCleanJig FCleanJig;
        private WorkingPlateAngle FPlateAngle;
        private WorkingBonder1 FBonder1;
        private WorkingBonder1JetData FBonder1Point;
        private WorkingBonder1JetPTLine FBonder1Pattern;
        private WorkingBonder1JetPTArc FBonder1ARC;
        private WorkingBonder2 FBonder2;
        private WorkingBonder2JetData FBonder2Point;
        private WorkingBonder2JetPTLine FBonder2Pattern;
        private WorkingBonder2JetPTArc FBonder2ARC;
        private WorkingCuring1 FCuring1;
        private WorkingCuring2 FCuring2;
        private WorkingDeviceVision FVision;
        private WorkingDeviceDispSensor FDispSensor;

        private WorkingDeviceMeasuring FMeasuring;
        private WorkingDeviceLensHeight FDeviceLensHeight;

        private WorkingUnloader FUnloader;
        private WorkingUnloadPicker FUnloadPicker;
        private WorkingIndex FIndex;
        private WorkingVisionInspect FVCMVision;
        private String FLanguageFile;

        // Teaching
        private WorkingTeachingVCMLoader FTeachVCMLoader;
        private WorkingTeachingVCMPicker FTeachVCMPicker;
        private WorkingTeachingLensLoader FTeachLensLoader;
        private WorkingTeachingLensPicker FTeachLensPicker;
        private WorkingTeachingJigPlateAngle FTeachJigPlateAngle;
        private WorkingTeachingPlateAngle FTeachPlateAngle;
        private WorkingTeachingBonder1 FTeachBonder1;
        private WorkingTeachingBonder2 FTeachBonder2;
        private WorkingTeachingUnloader FTeachUnloader;
        private WorkingTeachingUnloadPicker FTeachUnloadPicker;
        private WorkingTeachingVisionInspect FTeachVCMVision;

        public TWorking()
        {
            //Recipe
            FProject = new WorkProject();
            FRecipe = new WorkRecipe();
            FOption = new WorkingOption();
            FMesStandInfo = new MesStandardInfo();    //2021-04-28 Modify

            FVCMLoader = new WorkingVCMLoader(cDefString.VCMLoader);
            FVCMPicker = new WorkingVCMPicker(cDefString.VCMPicker);
            FLensLoader = new WorkingLensLoader(cDefString.LensLoader);
            FLensPicker = new WorkingLensPicker(cDefString.LensPicker);
            FJigPlateAngle = new WorkingJigPlateAngle(cDefString.JigPlateAngle);
            FLensHeight = new WorkingLensHeight(cDefString.LensHeight);
            FCleanJig = new WorkingCleanJig(cDefString.CleanJig);
            FPlateAngle = new WorkingPlateAngle(cDefString.PlateAngle);
            FBonder1 = new WorkingBonder1(cDefString.Bonder1);
            FBonder1Point = new WorkingBonder1JetData(cDefString.Bonder1Point);
            FBonder1Pattern = new WorkingBonder1JetPTLine(cDefString.Bonder1Pattern);
            FBonder1ARC = new WorkingBonder1JetPTArc(cDefString.Bonder1Arc);
            FBonder2 = new WorkingBonder2(cDefString.Bonder2);
            FBonder2Point = new WorkingBonder2JetData(cDefString.Bonder2Point);
            FBonder2Pattern = new WorkingBonder2JetPTLine(cDefString.Bonder2Pattern);
            FBonder2ARC = new WorkingBonder2JetPTArc(cDefString.Bonder2Arc);
            FCuring1 = new WorkingCuring1(cDefString.Curing1);
            FCuring2 = new WorkingCuring2(cDefString.Curing2);
            FVision = new WorkingDeviceVision();
            FDispSensor = new WorkingDeviceDispSensor();

            FMeasuring = new WorkingDeviceMeasuring();
            FDeviceLensHeight = new WorkingDeviceLensHeight();

            FUnloader = new WorkingUnloader(cDefString.Unloader);
            FUnloadPicker = new WorkingUnloadPicker(cDefString.UnloadPicker);

            FIndex = new WorkingIndex(cDefString.Index);
            FVCMVision = new WorkingVisionInspect(cDefString.VisionInspect);

            //Teaching

            FTeachVCMLoader = new WorkingTeachingVCMLoader(cDefString.VCMLoader);
            FTeachVCMPicker = new WorkingTeachingVCMPicker(cDefString.VCMPicker);
            FTeachLensLoader = new WorkingTeachingLensLoader(cDefString.LensLoader);
            FTeachLensPicker = new WorkingTeachingLensPicker(cDefString.LensPicker);
            FTeachJigPlateAngle = new WorkingTeachingJigPlateAngle(cDefString.JigPlateAngle);
            FTeachPlateAngle = new WorkingTeachingPlateAngle(cDefString.PlateAngle);
            FTeachBonder1 = new WorkingTeachingBonder1(cDefString.Bonder1); 
            FTeachBonder2 = new WorkingTeachingBonder2(cDefString.Bonder2);
            FTeachUnloader = new WorkingTeachingUnloader(cDefString.Unloader);
            FTeachUnloadPicker = new WorkingTeachingUnloadPicker(cDefString.UnloadPicker);
            FTeachVCMVision = new WorkingTeachingVisionInspect(cDefString.VisionInspect);

            Clear();
        }
        public WorkProject Project
        {
            get { return FProject; }
        }
        public WorkRecipe Recipe
        {
            get { return FRecipe; }
        }
        public WorkingOption Option
        {
            get { return FOption; }
        }
        /// <summary>
        ///  2021-04-28 Modify
        /// </summary>
        public MesStandardInfo MesStandInfo 
        {
            get { return FMesStandInfo; }
        }
        public WorkingVCMLoader VCMLoader
        {
            get { return FVCMLoader; }
        }
        public WorkingVCMPicker VCMPicker
        {
            get { return FVCMPicker; }
        }
        public WorkingLensLoader LensLoader
        {
            get { return FLensLoader; }
        }
        public WorkingLensPicker LensPicker
        {
            get { return FLensPicker; }
        }
        public WorkingJigPlateAngle JigPlateAngle
        {
            get { return FJigPlateAngle; }
        }
        public WorkingLensHeight LensHeight
        {
            get { return FLensHeight; }
        }
        public WorkingCleanJig CleanJig
        {
            get { return FCleanJig; }
        }
        public WorkingPlateAngle PlateAngle
        {
            get { return FPlateAngle; }
        }
        public WorkingBonder1 Bonder1
        {
            get { return FBonder1; }
        }
        public WorkingBonder1JetData Bonder1Point
        {
            get { return FBonder1Point; }
        }
        public WorkingBonder1JetPTLine Bonder1Pattern
        {
            get { return FBonder1Pattern; }
        }
        public WorkingBonder1JetPTArc Bonder1ARC
        {
            get { return FBonder1ARC; }
        }
        public WorkingBonder2 Bonder2
        {
            get { return FBonder2; }
        }
        public WorkingBonder2JetData Bonder2Point
        {
            get { return FBonder2Point; }
        }
        public WorkingBonder2JetPTLine Bonder2Pattern
        {
            get { return FBonder2Pattern; }
        }
        public WorkingBonder2JetPTArc Bonder2ARC
        {
            get { return FBonder2ARC; }
        }
        public WorkingCuring1 Curing1
        {
            get { return FCuring1; }
        }
        public WorkingCuring2 Curing2
        {
            get { return FCuring2; }
        }
        public WorkingUnloader Unloader
        {
            get { return FUnloader; }
        }
        public WorkingUnloadPicker UnloadPicker
        {
            get { return FUnloadPicker; }
        }
        public WorkingIndex Index
        {
            get { return FIndex; }
        }
        public WorkingVisionInspect VCMVision
        {
            get { return FVCMVision; }
        }
        public WorkingDeviceVision Vision
        {
            get { return FVision; }
        }
        public WorkingDeviceDispSensor DispSensor
        {
            get { return FDispSensor; }


        }
        public WorkingDeviceMeasuring Measuring
        {
            get { return FMeasuring; }
        }

        public WorkingDeviceLensHeight DeviceLensHeight
        {
            get { return FDeviceLensHeight; }
        }

        public WorkingTeachingVCMLoader TeachVCMLoader
        {
            get { return FTeachVCMLoader; }
        }
        public WorkingTeachingVCMPicker TeachVCMPicker
        {
            get { return FTeachVCMPicker; }
        }
        public WorkingTeachingLensLoader TeachLensLoader
        {
            get { return FTeachLensLoader; }
        }
        public WorkingTeachingLensPicker TeachLensPicker
        {
            get { return FTeachLensPicker; }
        }
        public WorkingTeachingJigPlateAngle TeachJigPlateAngle
        {
            get { return FTeachJigPlateAngle; }
        }
        public WorkingTeachingPlateAngle TeachPlateAngle
        {
            get { return FTeachPlateAngle; }
        }
        public WorkingTeachingBonder1 TeachBonder1
        {
            get { return FTeachBonder1; }
        }
        public WorkingTeachingBonder2 TeachBonder2
        {
            get { return FTeachBonder2; }
        }
        public WorkingTeachingUnloader TeachUnloader
        {
            get { return FTeachUnloader; }
        }
        public WorkingTeachingUnloadPicker TeachUnloadPicker
        {
            get { return FTeachUnloadPicker; }
        }
        public WorkingTeachingVisionInspect TeachVCMVision
        {
            get { return FTeachVCMVision; }
        }
        public String LanguageFile
        {
            get { return FLanguageFile; }
            set { SetLanguageFile(value); }
        }
        private void SetLanguageFile(String Value)
        {
            if (FLanguageFile != Value)
                FLanguageFile = Value;
        }
        
        public void Clear()
        {
            FLanguageFile = "";
        }
        public bool Open(string FileName)
        {
            string Section = string.Empty;

            if (!File.Exists(FileName))
                return false;

            Clear();

            fpIni Files = new fpIni(FileName);

            Section = "Working";
            FLanguageFile = Files.ReadString(Section, "LanguageFile", "");


            return true;
        }

        public bool Save(String FileName)
        {
            String Section = "";

            //if (!File.Exists(FileName))
            //    File.Create(FileName); 

            fpIni Files = new fpIni(FileName);

            Section = "Working";
            Files.WriteString(Section, "LanguageFile", FLanguageFile);

            FRecipe.Save(FileName);
            return true;
        }
        public int GetTime()
        {
            DateTime time1 = new DateTime(2020, 1, 1);
            TimeSpan result = DateTime.Now - time1;
            return Convert.ToInt32(result.Days);
        }


    }
}

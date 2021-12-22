using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using XModule.Standard;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace XModule.Working
{
    //---------------- Work GOLBAL Option--------------------------------------------------------------
    public class WorkGlobal
    {
        #region Function
        //Dry Run Option
        public bool VacuumCheck = true;
        public bool IndexCheck = true;
        public bool TrayCheck = true;
        public bool VisionCheck = true;
        

        //Sequence Option
        public bool UseBonder1 = true;
        public bool UseBonder2 = true;
        public bool UseJigPlateAngle = true;
        public bool UsePlateAngle = true;
        public bool UseLensHeight = true;
        public bool UseCleanJig = true;
        public bool UseCuring1 = true;
        public bool UseCuring2 = true;
        public bool UseVision = true;
        public bool UseLensPicker = true;

        //Actuator Option
        public bool Actuator_1_Mode = true;
        public bool Actuator_2_Mode = true;
        public bool Actuator_3_Mode = true;

        //Result Option
        public bool VisionResult = true;
        public bool LensHeightResult = true;
        public bool JigPlateAngleResult = true;
        public bool PlateAngleResult = true;
        public bool PlateAngleResultRowJudge = true;

        // LensHeight Option
        public bool LensHeightSoftWareJudge = false;

        // Dispenser Option
        public bool UseIdle1 = true;
        public bool UseIdle2 = true;
        public int JettingMode1 = 0;     // 0: Point , 1: Line
        public int JettingMode2 = 0;     // 0: Point , 1: Line


        public bool UseTipClean1 = false;
        public bool UseTipClean2 = false;
        public bool UseDummy1 = false;
        public bool UseDummy2 = false;
        public bool UseCureVisionFail = false;


        public bool UsePlusenum1 = false;
        public bool UsePlusenum2 = false;

        public bool UseActAndCure = false;

        public bool UseActRetry = false;
        public bool UseResultDummyPass = false;

        public bool UseLockType = false;
        public bool UseGap = false;
        public bool UseSecondaryCorrection = false;
        public bool UseActAction1 = false;
        public bool UseActAction2 = false;
        public bool UseActAction3 = false;
        public bool UsePreActuating = false;

        public bool UseMES = false;

        public string EQPID = "";
        public string VisionVer = "";
        public bool Use_TJV_Cooling1 = false;
        public bool Use_TJV_Cooling2 = false;
        #endregion


        public WorkGlobal()
        {
            Clear();
    
        }
        public void Clear()
        {
            
        }

        public bool Open(String FileName)
        {
            string Section = string.Empty;
            string FullPath = cPath.FILE_PROJECT + FileName + "\\RecipeOption.ini";

            if (!File.Exists(FullPath))
                return false;

            fpIni Files = new fpIni(FullPath);
            Section = "RecipeOption";

            //Dry Run Option
            VacuumCheck = Files.ReadBoolean(Section, "VacuumCheck", false);
            IndexCheck = Files.ReadBoolean(Section, "IndexCheck", false);
            TrayCheck = Files.ReadBoolean(Section, "TrayCheck", false);
            VisionCheck = Files.ReadBoolean(Section, "VisionCheck", false);


            //Sequence Option
            UseJigPlateAngle = Files.ReadBoolean(Section, "UseJigPlateAngle", false);
            UsePlateAngle = Files.ReadBoolean(Section, "UsePlateAngle", false);
            UseCleanJig = Files.ReadBoolean(Section, "UseCleanJig", false);
            UseLensHeight = Files.ReadBoolean(Section, "UseLensHeight", false);
            UseBonder1 = Files.ReadBoolean(Section, "UseBonder1", false);
            UseBonder2 = Files.ReadBoolean(Section, "UseBonder2", false);
            UseCuring1 = Files.ReadBoolean(Section, "UseCuring1", false);
            UseCuring2 = Files.ReadBoolean(Section, "UseCuring2", false);
            UseVision = Files.ReadBoolean(Section, "UseVision", false);
            UseLensPicker = Files.ReadBoolean(Section, "UseLensPicker", false);

            UseActAndCure = Files.ReadBoolean(Section, "UseActAndCure", false);

            UseActRetry = Files.ReadBoolean(Section, "UseActRetry", false);

            UseResultDummyPass = Files.ReadBoolean(Section, "UseResultDummyPass", false);
            UseLockType = Files.ReadBoolean(Section, "UseLockType", false);

            //Actuator Option

            Actuator_1_Mode = Files.ReadBoolean(Section, "Actuator_1_Mode", false);
            Actuator_2_Mode = Files.ReadBoolean(Section, "Actuator_2_Mode", false);
            Actuator_3_Mode = Files.ReadBoolean(Section, "Actuator_3_Mode", false);

            UseActAction1 = Files.ReadBoolean(Section, "UseActAction1", false);
            UseActAction2 = Files.ReadBoolean(Section, "UseActAction2", false);
            UseActAction3 = Files.ReadBoolean(Section, "UseActAction3", false);
            UsePreActuating = Files.ReadBoolean(Section, "UsePreActuating", false);

            //Result Option
            VisionResult = Files.ReadBoolean(Section, "VisionResult", false);
            LensHeightResult = Files.ReadBoolean(Section, "LensHeightResult", false);
            JigPlateAngleResult = Files.ReadBoolean(Section, "JigPlateAngleResult", false);
            PlateAngleResult = Files.ReadBoolean(Section, "PlateAngleResult", false);
            PlateAngleResultRowJudge = Files.ReadBoolean(Section, "PlateAngleResultRowJudge", false);

            // LensHeight
            LensHeightSoftWareJudge = Files.ReadBoolean(Section, "LensHeightSoftWareJudge", false);

            // Dispenser
            UseIdle1 = Files.ReadBoolean(Section, "UseIdle1", false);
            UseIdle2 = Files.ReadBoolean(Section, "UseIdle2", false);

            UsePlusenum1 = Files.ReadBoolean(Section, "UsePlusenum1", false);
            UsePlusenum2 = Files.ReadBoolean(Section, "UsePlusenum2", false);

            UseTipClean1 = Files.ReadBoolean(Section, "UseTipClean1", false);
            UseTipClean2 = Files.ReadBoolean(Section, "UseTipClean2", false);
            UseDummy1 = Files.ReadBoolean(Section, "UseDummy1", false);
            UseDummy2 = Files.ReadBoolean(Section, "UseDummy2", false);

            UseCureVisionFail = Files.ReadBoolean(Section, "UseCureVisionFail", false);

            JettingMode1 = Files.ReadInteger(Section, "JettingMode1", 0);
            JettingMode2 = Files.ReadInteger(Section, "JettingMode2", 0);

            UseGap = Files.ReadBoolean(Section, "UseGap", false);
            UseSecondaryCorrection = Files.ReadBoolean(Section, "UseSecondaryCorrection", false);

            UseMES = Files.ReadBoolean(Section, "UseMES", false);

            EQPID = Files.ReadString(Section, "EQPID", "");
            VisionVer = Files.ReadString(Section, "VisionVer", "");
            Use_TJV_Cooling1 = Files.ReadBoolean(Section, "Use_TJV_Cooling1", false);
            Use_TJV_Cooling2 = Files.ReadBoolean(Section, "Use_TJV_Cooling2", false);
            return true;
        }

        public bool Save(String FileName)
        {
            String Section = "RecipeOption";
            string FullPath = cPath.FILE_PROJECT + FileName + "\\RecipeOption.ini";
            fpIni Files = new fpIni(FullPath);

            Section = "RecipeOption";

            Files.WriteBoolean(Section, "VacuumCheck", VacuumCheck);
            Files.WriteBoolean(Section, "IndexCheck", IndexCheck);
            Files.WriteBoolean(Section, "TrayCheck", TrayCheck);
            Files.WriteBoolean(Section, "VisionCheck", VisionCheck);


            Files.WriteBoolean(Section, "UseBonder1", UseBonder1);
            Files.WriteBoolean(Section, "UseBonder2", UseBonder2);
            Files.WriteBoolean(Section, "UseCuring1", UseCuring1);
            Files.WriteBoolean(Section, "UseCuring2", UseCuring2);
            Files.WriteBoolean(Section, "UseJigPlateAngle", UseJigPlateAngle);
            Files.WriteBoolean(Section, "UsePlateAngle", UsePlateAngle);
            Files.WriteBoolean(Section, "UseCleanJig", UseCleanJig);
            Files.WriteBoolean(Section, "UseLensHeight", UseLensHeight);
            Files.WriteBoolean(Section, "UseVision", UseVision);
            Files.WriteBoolean(Section, "UseLensPicker", UseLensPicker);

            Files.WriteBoolean(Section, "UseActAndCure", UseActAndCure);
            Files.WriteBoolean(Section, "UseActRetry", UseActRetry);

            Files.WriteBoolean(Section, "UseResultDummyPass", UseResultDummyPass);
            Files.WriteBoolean(Section, "UseLockType", UseLockType);

            //Actuator Option
            Files.WriteBoolean(Section, "Actuator_1_Mode", Actuator_1_Mode);
            Files.WriteBoolean(Section, "Actuator_2_Mode", Actuator_2_Mode);
            Files.WriteBoolean(Section, "Actuator_3_Mode", Actuator_3_Mode);

            Files.WriteBoolean(Section, "UseActAction1", UseActAction1);
            Files.WriteBoolean(Section, "UseActAction2", UseActAction2);
            Files.WriteBoolean(Section, "UseActAction3", UseActAction3);
            Files.WriteBoolean(Section, "UsePreActuating", UsePreActuating);

            Files.WriteBoolean(Section, "VisionResult", VisionResult);
            Files.WriteBoolean(Section, "LensHeightResult", LensHeightResult);
            Files.WriteBoolean(Section, "JigPlateAngleResult", JigPlateAngleResult);
            Files.WriteBoolean(Section, "PlateAngleResult", PlateAngleResult);
            Files.WriteBoolean(Section, "PlateAngleResultRowJudge", PlateAngleResultRowJudge);

            Files.WriteBoolean(Section, "LensHeightSoftWareJudge", LensHeightSoftWareJudge);

            Files.WriteBoolean(Section, "UseIdle1", UseIdle1);
            Files.WriteBoolean(Section, "UseIdle2", UseIdle2);

            Files.WriteInteger(Section, "JettingMode1", JettingMode1);
            Files.WriteInteger(Section, "JettingMode2", JettingMode2);

            Files.WriteBoolean(Section, "UsePlusenum1", UsePlusenum1);
            Files.WriteBoolean(Section, "UsePlusenum2", UsePlusenum2);

  
            Files.WriteBoolean(Section, "UseTipClean1", UseTipClean1);
            Files.WriteBoolean(Section, "UseTipClean2", UseTipClean2);
            Files.WriteBoolean(Section, "UseDummy1", UseDummy1);
            Files.WriteBoolean(Section, "UseDummy2", UseDummy2);

            Files.WriteBoolean(Section, "UseCureVisionFail", UseCureVisionFail);

            Files.WriteBoolean(Section, "UseGap", UseGap);
            Files.WriteBoolean(Section, "UseSecondaryCorrection", UseSecondaryCorrection);

            Files.WriteBoolean(Section, "UseMES", UseMES);
            Files.WriteString(Section, "EQPID", EQPID);
            Files.WriteString(Section, "VisionVer", VisionVer);
            Files.WriteBoolean(Section, "Use_TJV_Cooling1", Use_TJV_Cooling1);
            Files.WriteBoolean(Section, "Use_TJV_Cooling2", Use_TJV_Cooling2);
            return true;
        }
    }

    /// <summary>
    /// 2021-04-28 Modify
    /// </summary>
    public class MesStandardInfo 
    {
        #region Function
       
        public string DeviceCode = "";
        public string OperationCode = "";
        public string EQPCode = "";
        public string Product_TypeCode = "";
        public string Device = "";
        public string Operation = "";
        public string EQPName = "";
        public string Product_Type = "";

        #endregion


        public MesStandardInfo()
        {
            Clear();
            Open();
        }
        public void Clear()
        {
            DeviceCode = "";
            OperationCode = "";
            EQPCode = "";
            Product_TypeCode = "";
            Device = "";
            Operation = "";
            EQPName = "";
            Product_Type = "";
        }

        public bool Open()
        {
            string sPath = Application.StartupPath + "\\Config\\MesStandardInfo.Config";
            string Section = string.Empty;

            if (!File.Exists(sPath))
                return false;
            fpIni Files = new fpIni(sPath);
            Section = "Info";


            DeviceCode = Files.ReadString(Section, "DeviceCode", "");
            OperationCode = Files.ReadString(Section, "OperationCode", "");
            EQPCode = Files.ReadString(Section, "EQPCode", "");
            Product_TypeCode = Files.ReadString(Section, "Product_TypeCode", "");
            Device = Files.ReadString(Section, "Device", "");
            Operation = Files.ReadString(Section, "Operation", "");
            EQPName = Files.ReadString(Section, "EQPName", "");
            Product_Type = Files.ReadString(Section, "Product_Type", "");

            return true;
        }

        public bool Save()
        {
            String Section = "";
            string sPath = Application.StartupPath + "\\Config\\MesStandardInfo.Config";
            fpIni Files = new fpIni(sPath);


            Section = "Info";
       
            Files.WriteString(Section, "DeviceCode", DeviceCode);
            Files.WriteString(Section, "OperationCode", OperationCode);
            Files.WriteString(Section, "EQPCode", EQPCode);
            Files.WriteString(Section, "Product_TypeCode", Product_TypeCode);
            Files.WriteString(Section, "Device", Device);
            Files.WriteString(Section, "Operation", Operation);
            Files.WriteString(Section, "EQPName", EQPName);
            Files.WriteString(Section, "Product_Type", Product_Type);

            return true;
        }
    }

    /// <summary>
    ///  WorkProject
    /// </summary>
    /// 

    public class WorkRecipe
    {
        public bool LoaderMagazineDirection = true;
        public bool LensMagazineDirection = true;
        public bool UnLoaderMagazineDirection = true;
        public bool LensInsertTorqueUse = true;
        public bool LensPickUpTorqueUse = true;
        public bool LensPickUpThetaTorqueUse = true;
        public int LensInsertTorqueLimit = 0;
        public int LensInsertTorqueLimitTheta = 0;
        public double LensHeight = 0;
        public double LensHeightAllowMin = 0;
        public double LensHeightAllowMax = 0;

        public int VCMVISIONRetryCount = 0;
        public int LensUpperRetryCount = 0;
        public int LensUnderRetryCount = 0;
        public int Bonder1RetryCount = 0;
        public int Bonder2RetryCount = 0;
        public int VIsionInspectRetryCount = 0;

        public int DummyTime1 = 100;
        public int DummyTime2 = 100;
        public int DummyPeriodCount1 = 100;
        public int DummyPeriodCount2 = 100;

        public double Inivolt_1;
        public double[] falltime_1 = new double[10];
        public double[] openvolt_1 = new double[10];
        public double[] opentime_1 = new double[10];
        public double[] risetime_1 = new double[10];
        public int[] pixelcount_1 = new int[10];
        public int rowcount_1 = 1;
        public int TJV_IP_1;
        public double Hz_1;
        public int nDrop_1;
        public int WorkMode_1;

        public double Inivolt_2;
        public double[] falltime_2 = new double[10];
        public double[] openvolt_2 = new double[10];
        public double[] opentime_2 = new double[10];
        public double[] risetime_2 = new double[10];
        public int[] pixelcount_2 = new int[10];
        public int rowcount_2 = 1;
        public int TJV_IP_2;
        public double Hz_2;
        public int nDrop_2;
        public int WorkMode_2;
        public bool NonContactMeasure = false;

        public int IndexOffsetX = 0;
        public int IndexOffsetY = 0;



        public void Clear()
       {

            DummyTime1 = 100;
            DummyTime2 = 100;
            DummyPeriodCount1 = 100;
            DummyPeriodCount2 = 100;

            rowcount_2 = 1;
            rowcount_1 = 1;

            for(int i = 0; i < 10; i++)
            {
                pixelcount_1[i] = 0;
                pixelcount_2[i] = 0;
            }
        }
        public bool Open(String FileName)
        {
            string Section = string.Empty;
            string FullPath = cPath.FILE_PROJECT + FileName + "\\Recipe.ini";

            if (!File.Exists(FullPath))
                return false;


            fpIni Files = new fpIni(FullPath);
            Section = "Recipe";

            LoaderMagazineDirection = Files.ReadBoolean(Section, "LoaderMagazineDirection", false);

            LensMagazineDirection = Files.ReadBoolean(Section, "LensMagazineDirection", false);

            UnLoaderMagazineDirection = Files.ReadBoolean(Section, "UnLoaderMagazineDirection", false);

            LensInsertTorqueUse = Files.ReadBoolean(Section, "LensInsertTorqueUse", false);

            LensPickUpTorqueUse = Files.ReadBoolean(Section, "LensPickUpTorqueUse", false);

            LensPickUpThetaTorqueUse = Files.ReadBoolean(Section, "LensPickUpThetaTorqueUse", false);

            LensInsertTorqueLimit = Files.ReadInteger(Section, "LensInsertTorqueLimit", 0);

            LensInsertTorqueLimitTheta = Files.ReadInteger(Section, "LensInsertTorqueLimitTheta", 0);

            LensHeight = Files.ReadDouble(Section, "LensHeight", 0);

            LensHeightAllowMin = Files.ReadDouble(Section, "LensHeightAllowMin", 0);

            LensHeightAllowMax = Files.ReadDouble(Section, "LensHeightAllowMax", 0);

            VCMVISIONRetryCount = Files.ReadInteger(Section, "VCMVISIONRetryCount", 0);

            LensUpperRetryCount = Files.ReadInteger(Section, "LensUpperRetryCount", 0);

            LensUnderRetryCount = Files.ReadInteger(Section, "LensUnderRetryCount", 0);

            Bonder1RetryCount = Files.ReadInteger(Section, "Bonder1RetryCount", 0);

            Bonder2RetryCount = Files.ReadInteger(Section, "Bonder2RetryCount", 0);

            VIsionInspectRetryCount = Files.ReadInteger(Section, "VIsionInspectRetryCount", 0);

            DummyTime1 = Files.ReadInteger(Section, "DummyTime1", 100);
            DummyTime2 = Files.ReadInteger(Section, "DummyTime2", 100);

            DummyPeriodCount1 = Files.ReadInteger(Section, "DummyPeriodCount1", 100);
            DummyPeriodCount2 = Files.ReadInteger(Section, "DummyPeriodCount2", 100);

            if (cDEF.Work.Project.GlobalOption.UseMES)
            {
                cDEF.Mes.Send_RecipeChange();
            }

            TJV_IP_1 = Files.ReadInteger(Section, "TJV_IP_1", 0);
            Inivolt_1 = Files.ReadDouble(Section, "Inivolt_1", 0.0);

            for (int i = 0; i < 10; i++)
            {
                falltime_1[i] = Files.ReadDouble(Section, "falltime_1" + i.ToString(), 0.0);
                openvolt_1[i] = Files.ReadDouble(Section, "openvolt_1" + i.ToString(), 0.0);
                opentime_1[i] = Files.ReadDouble(Section, "opentime_1" + i.ToString(), 0.0);
                risetime_1[i] = Files.ReadDouble(Section, "risetime_1" + i.ToString(), 0.0);
                pixelcount_1[i] = Files.ReadInteger(Section, "pixelcount_1" + i.ToString(), 0);
            }

            rowcount_1 = Files.ReadInteger(Section, "rowcount_1", 1);
            Hz_1 = Files.ReadDouble(Section, "Hz_1", 0.0);
            nDrop_1 = Files.ReadInteger(Section, "nDrop_1", 0);
            WorkMode_1 = Files.ReadInteger(Section, "WorkMode_1", 0);


            TJV_IP_2 = Files.ReadInteger(Section, "TJV_IP_2", 0);
            Inivolt_2 = Files.ReadDouble(Section, "Inivolt_2", 0.0);

            for (int i = 0; i < 10; i++)
            {
                falltime_2[i] = Files.ReadDouble(Section, "falltime_2" + i.ToString(), 0.0);
                openvolt_2[i] = Files.ReadDouble(Section, "openvolt_2" + i.ToString(), 0.0);
                opentime_2[i] = Files.ReadDouble(Section, "opentime_2" + i.ToString(), 0.0);
                risetime_2[i] = Files.ReadDouble(Section, "risetime_2" + i.ToString(), 0.0);
                pixelcount_2[i] = Files.ReadInteger(Section, "pixelcount_2" + i.ToString(), 0);
            }

            rowcount_2 = Files.ReadInteger(Section, "rowcount_2", 1);
            Hz_2 = Files.ReadDouble(Section, "Hz_2", 0.0);
            nDrop_2 = Files.ReadInteger(Section, "nDrop_2", 0);
            WorkMode_2 = Files.ReadInteger(Section, "WorkMode_2", 0);


            NonContactMeasure = Files.ReadBoolean(Section, "NonContactMeasure", false);

            IndexOffsetX = Files.ReadInteger(Section, "IndexOffsetX", 0);
            IndexOffsetY = Files.ReadInteger(Section, "IndexOffsetY", 0);

            return true;
        }

        public bool Save(String FileName)
        {
            String Section = "Recipe";
            string FullPath = cPath.FILE_PROJECT + FileName + "\\Recipe.ini";
            fpIni Files = new fpIni(FullPath);
           
            Files.WriteBoolean(Section, "LoaderMagazineDirection", LoaderMagazineDirection);

            Files.WriteBoolean(Section, "LesnMagazineDirection", LensMagazineDirection);

            Files.WriteBoolean(Section, "UnLoaderMagazineDirection", UnLoaderMagazineDirection);

            Files.WriteBoolean(Section, "LensInsertTorqueUse", LensInsertTorqueUse);

            Files.WriteInteger(Section, "LensInsertTorqueLimit", LensInsertTorqueLimit);

            Files.WriteInteger(Section, "LensInsertTorqueLimitTheta", LensInsertTorqueLimitTheta);

            Files.WriteBoolean(Section, "LensPickUpTorqueUse", LensPickUpTorqueUse);

            Files.WriteBoolean(Section, "LensPickUpThetaTorqueUse", LensPickUpThetaTorqueUse);

            Files.WriteDouble(Section, "LensHeight", LensHeight);

            Files.WriteDouble(Section, "LensHeightAllowMin", LensHeightAllowMin);

            Files.WriteDouble(Section, "LensHeightAllowMax", LensHeightAllowMax);

            Files.WriteDouble(Section, "VCMVISIONRetryCount", VCMVISIONRetryCount);

            Files.WriteDouble(Section, "LensUpperRetryCount", LensUpperRetryCount);

            Files.WriteDouble(Section, "LensUnderRetryCount", LensUnderRetryCount);

            Files.WriteDouble(Section, "Bonder1RetryCount", Bonder1RetryCount);

            Files.WriteDouble(Section, "Bonder2RetryCount", Bonder2RetryCount);

            Files.WriteDouble(Section, "VIsionInspectRetryCount", VIsionInspectRetryCount);

            Files.WriteInteger(Section, "DummyTime1", DummyTime1);
            Files.WriteInteger(Section, "DummyTime2", DummyTime2);

            Files.WriteInteger(Section, "DummyPeriodCount1", DummyPeriodCount1);
            Files.WriteInteger(Section, "DummyPeriodCount2", DummyPeriodCount2);

            Files.WriteInteger(Section, "TJV_IP_1", TJV_IP_1);

            Files.WriteDouble(Section, "Inivolt_1", Inivolt_1);

            for (int i = 0; i < 10; i++)
            {
                Files.WriteDouble(Section, "falltime_1" + i.ToString(), falltime_1[i]);
                Files.WriteDouble(Section, "openvolt_1" + i.ToString(), openvolt_1[i]);
                Files.WriteDouble(Section, "opentime_1" + i.ToString(), opentime_1[i]);
                Files.WriteDouble(Section, "risetime_1" + i.ToString(), risetime_1[i]);
                Files.WriteInteger(Section, "pixelcount_1" + i.ToString(), pixelcount_1[i]);
            }

            Files.WriteInteger(Section, "rowcount_1", rowcount_1);

            Files.WriteDouble(Section, "Hz_1", Hz_1);
            Files.WriteInteger(Section, "nDrop_1", nDrop_1);
            Files.WriteInteger(Section, "TJV_IP_2", TJV_IP_2);
            
            Files.WriteInteger(Section, "WorkMode_1", WorkMode_1);
            Files.WriteDouble(Section, "Inivolt_2", Inivolt_2);

            for (int i = 0; i < 10; i++)
            {
                Files.WriteDouble(Section, "falltime_2" + i.ToString(), falltime_2[i]);
                Files.WriteDouble(Section, "openvolt_2" + i.ToString(), openvolt_2[i]);
                Files.WriteDouble(Section, "opentime_2" + i.ToString(), opentime_2[i]);
                Files.WriteDouble(Section, "risetime_2" + i.ToString(), risetime_2[i]);
                Files.WriteInteger(Section, "pixelcount_2" + i.ToString(), pixelcount_2[i]);
            }

            Files.WriteInteger(Section, "rowcount_2", rowcount_2);

            Files.WriteDouble(Section, "Hz_2", Hz_2);
            Files.WriteInteger(Section, "nDrop_2", nDrop_2);
            Files.WriteInteger(Section, "WorkMode_2", WorkMode_2);

            Files.WriteBoolean(Section, "NonContactMeasure", NonContactMeasure);

            Files.WriteInteger(Section, "IndexOffsetX", IndexOffsetX);
            Files.WriteInteger(Section, "IndexOffsetY", IndexOffsetY);
            return true;
        }
    }

    public class WorkProject
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private String FFileName;
        private String FSubFileName;
        private String FFilePath;
                
        private WorkGlobal FGlobal;

        private String FMachineName;
        public int cnt;

        public String FileName
        {
            get { return FFileName; }
            set { SetFileName(value); }
        }
        public String SubFileName
        {
            get { return FSubFileName; }
            set { SetSubFileName(value); }
        }
        public String FilePath
        {
            get { return FFilePath; }
            set { SetFilePath(value); }
        }
        public WorkGlobal GlobalOption
        {
            get { return FGlobal; }
        }
       

        public String MachineName
        {
            get { return FMachineName; }
            set { FMachineName = value; }
        }


        public WorkProject()
        {
            FGlobal = new WorkGlobal();

            Clear();
        }
   
        private void SetFileName(String Value)
        {
            if (FFileName != Value)
                FFileName = Value;
        }
        private void SetSubFileName(String Value)
        {
            if (FSubFileName != Value)
                FSubFileName = Value;
        }
        private void SetFilePath(String Value)
        {
            if (FFilePath != Value)
                FFilePath = Value;
        }
        
        public void Clear()
        {
            FFileName = "";
            FFilePath = "";
            FMachineName = "";
        }

        public bool OpenDefault()
        {
            String FileNames = "";
            String FilePaths = "";
            String Section;

            if (!File.Exists(cPath.FILE_DESKTOP))
                return false;

            fpIni Files = new fpIni(cPath.FILE_DESKTOP);

            Section = "Project";
            FileNames = Files.ReadString(Section, "FileName", "");
            FilePaths = Files.ReadString(Section, "FilePath", "");
            FMachineName = Files.ReadString(Section, "FMachineName", "");
            cnt = Files.ReadInteger(Section, "TimeOut", 0);

            return Open(FileNames);
        }
        public bool SaveDefault()
        {
            String Section = string.Empty;
            String FileNames = "";
           // String FilePaths = "";

            fpIni Files = new fpIni(cPath.FILE_DESKTOP);

            Section = "Project";
            if(FileNames == "")
                Files.WriteString(Section, "FileName", FFileName);

            if (FilePath == "")
                Files.WriteString(Section, "FilePath", FFilePath);

            Files.WriteString(Section, "FMachineName", FMachineName);

            return true;
        }

        public bool Open(string FileNames)
        {
            string Section = string.Empty;
            string FullPath = "";

            if (FileNames == "")
                FullPath = cPath.FILE_DEFUALT;
            else
                FullPath = cPath.FILE_PROJECT + FileNames; 

            //Clear();

            FFileName = FileNames;

            cDEF.Work.Recipe.Open(FFileName);
            cDEF.Work.Project.GlobalOption.Open(FFileName);
            cDEF.Work.Option.Open(FFileName);
            cDEF.Work.VCMLoader.RecipeOpen(FFileName);
            cDEF.Work.VCMPicker.RecipeOpen(FFileName);
            cDEF.Work.LensLoader.RecipeOpen(FFileName);
            cDEF.Work.LensPicker.RecipeOpen(FFileName);
            cDEF.Work.LensHeight.RecipeOpen(FFileName);
            cDEF.Work.JigPlateAngle.RecipeOpen(FFileName);
            cDEF.Work.Bonder1.RecipeOpen(FFileName);
            cDEF.Work.Bonder1Point.RecipeOpen(FFileName);
            cDEF.Work.Bonder1Pattern.RecipeOpen(FFileName);
            cDEF.Work.Bonder1ARC.RecipeOpen(FFileName);
            cDEF.Work.Bonder2.RecipeOpen(FFileName);
            cDEF.Work.Bonder2Point.RecipeOpen(FFileName);
            cDEF.Work.Bonder2Pattern.RecipeOpen(FFileName);
            cDEF.Work.Bonder2ARC.RecipeOpen(FFileName);
            cDEF.Work.VCMVision.RecipeOpen(FFileName);
            cDEF.Work.Curing1.RecipeOpen(FFileName);
            cDEF.Work.Curing2.RecipeOpen(FFileName);
            cDEF.Work.PlateAngle.RecipeOpen(FFileName);
            cDEF.Work.Unloader.RecipeOpen(FFileName);
            cDEF.Work.UnloadPicker.RecipeOpen(FFileName);
            cDEF.Work.CleanJig.RecipeOpen(FFileName);
            cDEF.Work.Index.RecipeOpen(FFileName);

            cDEF.Work.TeachVCMLoader.RecipeOpen(FFileName);
            cDEF.Work.TeachVCMPicker.RecipeOpen(FFileName);
            cDEF.Work.TeachLensLoader.RecipeOpen(FFileName);
            cDEF.Work.TeachLensPicker.RecipeOpen(FFileName);
            cDEF.Work.TeachJigPlateAngle.RecipeOpen(FFileName);
            cDEF.Work.TeachBonder1.RecipeOpen(FFileName);
            cDEF.Work.TeachBonder2.RecipeOpen(FFileName);
            cDEF.Work.TeachVCMVision.RecipeOpen(FFileName);
            cDEF.Work.TeachPlateAngle.RecipeOpen(FFileName);
            cDEF.Work.TeachUnloader.RecipeOpen(FFileName);
            cDEF.Work.TeachUnloadPicker.RecipeOpen(FFileName);
            Actuator1FileSave();
            Actuator2FileSave();
            return true;
        }

        public bool Save(String FileNames = "")
        {
            String Section = "";

            string sDirPath;
            sDirPath = Application.StartupPath + "\\Project\\" + FileNames ;
            DirectoryInfo di = new DirectoryInfo(sDirPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            if(FileNames == "")
            {
                if (FFileName == "")
                    FileNames = cPath.FILE_DEFUALT;
                else
                    FileNames = cPath.FILE_PROJECT + cDEF.Work.Project.FFileName + ".ini";
            }
            else
            {
                FFileName = FileNames;
                FileNames = cPath.FILE_PROJECT + cDEF.Work.Project.FFileName;
            }

            fpIni Files = new fpIni(cPath.FILE_DEFUALT);
            Section = "Project";
            Files.WriteString(Section, "FileName", FFileName);
            Files.WriteString(Section, "FilePath", FFilePath);

            fpIni sFiles = new fpIni(FileNames +"ini");
            Section = "Project";
            Files.WriteString(Section, "FileName", FFileName);
            Files.WriteString(Section, "FilePath", FFilePath);

            WritePrivateProfileString(Section, "FileName", FFileName, FileNames + ".ini");
            WritePrivateProfileString(Section, "FilePath", FFilePath, FileNames + ".ini");

            cDEF.Work.Recipe.Save(FFileName);
            cDEF.Work.Project.GlobalOption.Save(FFileName);
            cDEF.Work.Option.Save(FFileName);
            cDEF.Work.VCMLoader.RecipeSave(FFileName);
            cDEF.Work.VCMPicker.RecipeSave(FFileName);
            cDEF.Work.LensLoader.RecipeSave(FFileName);
            cDEF.Work.LensPicker.RecipeSave(FFileName);
            cDEF.Work.LensHeight.RecipeSave(FFileName);
            cDEF.Work.JigPlateAngle.RecipeSave(FFileName);
            cDEF.Work.Bonder1.RecipeSave(FFileName);
            cDEF.Work.Bonder1Point.RecipeSave(FFileName);
            cDEF.Work.Bonder1Pattern.RecipeSave(FFileName);
            cDEF.Work.Bonder1ARC.RecipeSave(FFileName);
            cDEF.Work.Bonder2.RecipeSave(FFileName);
            cDEF.Work.Bonder2Point.RecipeSave(FFileName);
            cDEF.Work.Bonder2Pattern.RecipeSave(FFileName);
            cDEF.Work.Bonder2ARC.RecipeSave(FFileName);
            cDEF.Work.VCMVision.RecipeSave(FFileName);
            cDEF.Work.Curing1.RecipeSave(FFileName);
            cDEF.Work.Curing2.RecipeSave(FFileName);
            cDEF.Work.PlateAngle.RecipeSave(FFileName);
            cDEF.Work.Unloader.RecipeSave(FFileName);
            cDEF.Work.UnloadPicker.RecipeSave(FFileName);
            cDEF.Work.CleanJig.RecipeSave(FFileName);
            cDEF.Work.Index.RecipeSave(FFileName);

            cDEF.Work.TeachVCMLoader.RecipeSave(FFileName);
            cDEF.Work.TeachVCMPicker.RecipeSave(FFileName);
            cDEF.Work.TeachLensLoader.RecipeSave(FFileName);
            cDEF.Work.TeachLensPicker.RecipeSave(FFileName);
            cDEF.Work.TeachJigPlateAngle.RecipeSave(FFileName);
            cDEF.Work.TeachBonder1.RecipeSave(FFileName);
            cDEF.Work.TeachBonder2.RecipeSave(FFileName);
            cDEF.Work.TeachVCMVision.RecipeSave(FFileName);
            cDEF.Work.TeachPlateAngle.RecipeSave(FFileName);
            cDEF.Work.TeachUnloader.RecipeSave(FFileName);
            cDEF.Work.TeachUnloadPicker.RecipeSave(FFileName);
            Actuator1FileSave();
            Actuator2FileSave();
            return true;
        }

        public void Actuator1FileSave()
        {
#if !Notebook
            FileStream File;
            StreamWriter FileWrite;

            string Path = "C:\\MasterFile\\Master_Name1.mst";

            File = new FileStream(Path, FileMode.Create);
            FileWrite = new StreamWriter(File);

            FileWrite.WriteLine($"{FFileName}_01");

            FileWrite.Close();
            File.Close();
#endif

        }
        public void Actuator2FileSave()
        {
#if !Notebook
            FileStream File;
            StreamWriter FileWrite;

            string Path = "C:\\MasterFile\\Master_Name2.mst";

            File = new FileStream(Path, FileMode.Create);
            FileWrite = new StreamWriter(File);

            FileWrite.WriteLine($"{FFileName}_02");

            FileWrite.Close();
            File.Close();
#endif
        }
    }
}

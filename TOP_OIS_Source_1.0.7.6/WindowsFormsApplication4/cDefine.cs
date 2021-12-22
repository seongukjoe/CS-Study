using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using freeLicence;
using System.Runtime.InteropServices;
using XModule.Forms;
using XModule.Forms.FormUser;
using XModule.Forms.FormOperation;
using XModule.Forms.FormRecipe;
using XModule.Forms.FormWorking;
using XModule.Forms.FormHistory;
using XModule.Forms.FormConfig;
using XModule.Resource;
using XModule.Working;
using XModule.Running;
using XModule.Datas;
using XModule.Unit;


namespace XModule.Standard
{
    [Serializable]
    public class XY
    {
        public int x;
        public int y;
        public XY()
        {
            x = 0;
            y = 0;
        }
    }
    [Serializable]
    public class XYZ
    {
        public int x;
        public int y;
        public int z;
        public XYZ()
        {
            x = 0;
            y = 0;
            z = 0;
        }
    }
    [Serializable]
    public class XYT
    {
        public int x;
        public int y;
        public int t;
        public XYT()
        {
            x = 0;
            y = 0;
            t = 0;
        }
    }
    [Serializable]
    public class TZ
    {
        public int z;
        public int t;
        public TZ()
        {
            z = 0;
            t = 0;
        }
    }
    
    public static class cAPI
    {
        public static Class1 brDll = new Class1();
    }

    public static class etc
    {
        public const char CrLf = '\n';
        public const char cspCr = '\r';
        public const char cspTab = '\t';

    }

    public static class cSystem
    {
        public const int formPageLeft = 130;
        public const int formPageTop = 60;
    }

    public static class cPath
    {
        public const string Version         = "1.0.7.6"; 
        public const string Company         = "SIC";
        public const string MahcineName     = "ASSEMBLER";

        /////////////////////////////////////////////////////////////////////////////////////
        public static string FILE_PASSWORD = Application.StartupPath + "\\Config\\PassWord.txt";
        public static string FILE_DESKTOP = Application.StartupPath + "\\Config\\Desktop.xml";
        public static string FILE_DEFUALT = Application.StartupPath + "\\Default.xml";

        public static string FILE_MOTION_CONFIG = Application.StartupPath + "\\Config\\MotionConfig.xml";
        public static string FILE_MOTION_SPEED = Application.StartupPath + "\\Config\\MotionSpeed.xml";
        public static string FILE_DIGITAL = Application.StartupPath + "\\Config\\Digital.xml";
        public static string FILE_ANALOG = Application.StartupPath + "\\Config\\Analog.xml";
        public static string FILE_CYLINDER = Application.StartupPath + "\\Config\\Cylinder.xml";

        

        public static string FILE_LAMP = Application.StartupPath + "\\Config\\Lamp.xml";
        public static string FILE_SWITCH = Application.StartupPath + "\\Config\\Switch.xml";
        public static string FILE_LOG = Application.StartupPath + "\\Config\\Log.Config";
        public static string FILE_WORKING  = Application.StartupPath + "\\Config\\Working.ini";
        public static string FILE_PROJECT = Application.StartupPath + "\\PROJECT\\";
        public static string FILE_PROJECT_MODEL = Application.StartupPath + "\\PROJECT\\Model\\";
        
        ///////////////////////////////////////////////////////////////////////////////////////
        public static string RECOVERY_INSPECTOR = Application.StartupPath + "\\Recovery\\RunInspector.xml";
        ///////////////////////////////////////////////////////////////////////////////////////

        public static string FILE_EQ_INFORMATION = Application.StartupPath + "\\Config\\";
        public static string FILE_CONFIG_GLOBAL_OPTION = Application.StartupPath + "\\Config\\";
        public static string FILE_IMAGE = Application.StartupPath + "\\Config\\Base\\Base.bmp";
        public static string FILE_TCPIP = Application.StartupPath + "\\Config\\Tcp.dat";
        public static string FILE_232 = Application.StartupPath + "\\Config\\PC232.dat";
        public static string FILE_MES = Application.StartupPath + "\\Config\\EQ1.cfg";
        public static string FILE_MESSave = "D:\\MES\\EQUIP\\";

        public static string PATH_LOG ="c:\\TOP\\Log\\";
        public static string FILE_TASK_LOG = "c:\\TOP\\Log";
        public static string FILE_TIME_LOG = "c:\\TOP\\Log\\Time\\";
        public static string FILE_PROCESS_LOG = "c:\\TOP\\Log\\Process\\";

        //public static string PATH_LOG = Application.StartupPath + "\\Log\\";
        //public static string FILE_TASK_LOG = Application.StartupPath + "\\Log";
        //public static string FILE_TIME_LOG = Application.StartupPath + "\\Log\\Time\\";
        //public static string FILE_PROCESS_LOG = Application.StartupPath + "\\Log\\Process\\";

        public static string FILE_MesEquipFile = "d:\\MES\\EQUIP\\";

    }
    public static class cDefString
    {
        #region string Define
        public readonly static string General           = "GENERAL";
        public readonly static string VCMLoader         = "VCM_LOADER";
        public readonly static string VCMPicker         = "VCM_PICKER";
        public readonly static string LensLoader        = "LENS_LOADER";
        public readonly static string LensPicker        = "LENS_PICKER";
        public readonly static string JigPlateAngle     = "JIG_PLATE_ANGLE";
        public readonly static string LensHeight        = "LENS_HEIGHT";
        public readonly static string Bonder1           = "BONDER_1";
        public readonly static string Bonder2           = "BONDER_2";
        public readonly static string VisionInspect     = "VISION_INSPECT";
        public readonly static string Curing1           = "CURING_1";
        public readonly static string Curing2           = "CURING_2";
        public readonly static string PlateAngle        = "PLATE_ANGLE";
        public readonly static string Unloader          = "UNLOADER";
        public readonly static string UnloadPicker      = "UNLOAD_PICKER";
        public readonly static string CleanJig          = "CLEAN_JIG";
        public readonly static string Index             = "INDEX";
        public readonly static string Bonder1Point = "BONDER_1_POINT";
        public readonly static string Bonder1Line = "BONDER_1_LINE";
        public readonly static string Bonder2Point = "BONDER_2_POINT";
        public readonly static string Bonder2Line = "BONDER_2_LINE";
        public readonly static string Bonder1Pattern = "BONDER_1_PATTERN";
        public readonly static string Bonder2Pattern = "BONDER_2_PATTERN";
        public readonly static string Bonder1Arc = "BONDER_1_Arc";
        public readonly static string Bonder2Arc = "BONDER_2_Arc";
        #endregion
    }
    public enum TMessages
    {
        FM_RECOVERY_SAVE = 100,
    }
    public enum TaskLog
    {
        VCM,
        Act3,
        Lens,
        Unload,
        Index,
        CleanJig,
        JigPlateAngle,
        LensHeight,
        LensHeightData,
        Bonder1,
        Bonder2,
        Inspect,
        Cure1,
        Cure2,
        PlateAngle,
        Tact,
        JigPlateAngleData,
        PlateAngleData,
        LowData,
        YieldChartData,

        TempPlaceData,
        TempTorqueFaileData,
        Count,
        Keyence,
        LOG_MAX
    }


    /// <summary>
    /// 통신 응답 상태
    /// </summary>
    public enum CmmStatus
    {
        Ok,
        Ng,
        NoResponse,
        Wait
    }

    #region "DEFINE"
    public static class cDEF
    {   
        public static string MOTION_POSITIVE_LIMIT = "+ Limit   ";
        public static string MOTION_NEGATIVE_LIMIT = "- Limit   ";

        public const int MAXIMUM_MODULE_COUNT = 10;
        public const int CAMERA_WIDTH = 640;
        public const int CAMERA_HEIGHT = 480;
        public const bool DispMM = true;
         
        public const int VB_OFF = 1;
        public const int VACUUM_ON = 2;
        public const int BLOW_ON = 3;

        

        #region "FORM"     
        public static FrmProgress frmProgress = new FrmProgress(); 
        public static FrmMain frmMain = new FrmMain();
        public static FrmTitle frmTitle = new FrmTitle();
        public static FrmBottom frmBottom = new FrmBottom();
        public static FrmMenu frmMenu = new FrmMenu();
        public static FrmLogin frmLogin = new FrmLogin();
        
        public static FrmLoginChagePassword frmLoginChagePassword = new FrmLoginChagePassword();

        //PM
        public static FormPagePM frmPagePM = new FormPagePM();
        public static FormDoor frmDoor = new FormDoor();

        //etc
        //public static FormTaskLog frmTaskLog = new FormTaskLog();

        //Operation
        public static FrmPageOperation frmPageOperation = new FrmPageOperation();

        //History
        public static FrmMenuHistory frmMenuHistory = new FrmMenuHistory();
        public static FrmPageHistoryAlarm frmPageHistoryAlarm = new FrmPageHistoryAlarm();
        public static FrmPageHistoryWarning frmPageHistoryWarning = new FrmPageHistoryWarning();
        public static FrmPageHistoryData frmPageHistoryData = new FrmPageHistoryData();
        public static FrmPageHistoryEvent frmPageHistoryEvent = new FrmPageHistoryEvent();
        public static FrmPageHistoryList frmPageHistoryList = new FrmPageHistoryList();

        //
        //Recipe
        public static FrmMenuRecipe frmMenuRecipe = new FrmMenuRecipe();
        public static FrmPageRecipeProject frmPageRecipeProject = new FrmPageRecipeProject();

        //Working
        public static FrmMenuWorking frmMenuWorking = new FrmMenuWorking();
        public static FormPageWorkingVCMLoader frmPageWorkVCMLoader = new FormPageWorkingVCMLoader();
        public static FormPageWorkingVCMPicker frmPageWorkVCMPicker = new FormPageWorkingVCMPicker();
        public static FormPageWorkingLensLoader frmPageWorkLensLoader = new FormPageWorkingLensLoader();
        public static FormPageWorkingLensPicker frmPageWorkLensPicker = new FormPageWorkingLensPicker();
        public static FormPageWorkingIndex frmPageWorkIndex = new FormPageWorkingIndex();
        public static FormPageWorkingBonder frmPageWorkBonder = new FormPageWorkingBonder();
        public static FormPageWorkingCuring frmPageWorkCuring = new FormPageWorkingCuring();
        public static FormPageWorkingUnloader frmPageWorkUnloader = new FormPageWorkingUnloader();
        public static FormPageWorkingUnloaderPicker frmPageWorkUnloaderPicker = new FormPageWorkingUnloaderPicker();
        //Config
        public static FrmMenuConfig frmMenuConfig = new FrmMenuConfig();
        public static FrmPageConfigMotion frmPageConfigMotion = new FrmPageConfigMotion();
        public static FrmPageConfigDigital frmPageConfigDigital = new FrmPageConfigDigital();
        public static FrmPageConfigDigitalLink frmPageConfigDigitalLink = new FrmPageConfigDigitalLink();
        public static FrmPageConfigAnalog frmPageConfigAnalog = new FrmPageConfigAnalog();
        public static FrmPageConfigAnalogLink frmPageConfigAnalogLink = new FrmPageConfigAnalogLink();
        public static FrmPageConfigCylinder frmPageConfigCylinder = new FrmPageConfigCylinder();
        public static FrmPageConfigLamp frmPageConfigLamp = new FrmPageConfigLamp();
        public static FrmPageConfigSwitch frmPageConfigSwitch = new FrmPageConfigSwitch();
        public static FormPageConfigGeneral frmPageConfigGeneral = new FormPageConfigGeneral();

        #endregion "FORM"

        #region "StandardForm"
        public static frmTextEdit fTextEdit = new frmTextEdit();
        public static FormTaskLog fTaskLog = new FormTaskLog();
        #endregion"StandardForm"


        public static fpAPI WAPI = new fpAPI();
        public static TRun Run = new TRun(2, 500);

        //Working
        public static TWorking Work = new TWorking();        
        public static cVision Visions = new cVision();
        public static cSerials Serials = new cSerials();

        public static clsSuperEcm3 DispenserEcm1 = new clsSuperEcm3("SuperEcm1");
        public static clsSuperEcm3 DispenserEcm2 = new clsSuperEcm3("SuperEcm2");

        public static PJS100 Dispenser1 = new PJS100();
        public static PJS100 Dispenser2 = new PJS100();

        public static CNetLjX8000 SideAngleMeasuring = new CNetLjX8000();

        //public static DisplaceSensor DispSen = new DisplaceSensor();
        public static FormTaskLog Ftask = new FormTaskLog();

        public static Language Lang = new Language();
        public static TactTime Tact = new TactTime();


        public static CMES Mes = new CMES();
        public static cUtilLocal.AlarmDefin[] AlarmDefineList;
        public static TJVClass TJV_1 = new TJVClass();
        public static TJVClass2 TJV_2 = new TJVClass2();
        public static void ChangeLanguage()
        {
            frmPageOperation.ChangeLanguage();
            frmPageRecipeProject.ChangeLanguage();
            frmMenuRecipe.ChangeLanguage();
            frmMenuWorking.ChangeLanguage();
            frmPageWorkVCMLoader.ChangeLanguage();
            frmPageWorkVCMPicker.ChangeLanguage();
            frmPageWorkLensLoader.ChangeLanguage();
            frmPageWorkLensPicker.ChangeLanguage();
            frmPageWorkIndex.ChangeLanguage();
            frmPageWorkBonder.ChangeLanguage();
            frmPageWorkCuring.ChangeLanguage();
            frmPageWorkUnloader.ChangeLanguage();
            frmPageWorkUnloaderPicker.ChangeLanguage();
            frmMenuConfig.ChangeLanguage();
            frmPageConfigGeneral.ChangeLanguage();
            frmPageConfigMotion.ChangeLanguage();
            frmPageConfigDigital.ChangeLanguage();
            frmPageConfigDigitalLink.ChangeLanguage();
            frmPageConfigCylinder.ChangeLanguage();
            frmPageConfigSwitch.ChangeLanguage();
            frmPageConfigLamp.ChangeLanguage();
            frmPageConfigAnalogLink.ChangeLanguage();
            frmMenuHistory.ChnageLanguage();
            frmPageHistoryAlarm.ChangeLanguage();
            frmPageHistoryWarning.ChangeLanguage();
            frmPageHistoryEvent.ChangeLanguage();
            frmPageHistoryData.ChangeLanguage();
            frmMenu.ChangeLanguage();
        }

        public static fpTaskLog TaskLog = new fpTaskLog();

        public static  CLogWrite MesLog = new CLogWrite();
        public static void TaskLogAppend(TaskLog task, string msg, bool UseFileSave = false)
        {
            try
            {
                TaskLog.WriteLog((int)task, DateTime.Now.ToString("fff") + " " + msg);
                if (UseFileSave)
                {
                    string path = $"{cPath.FILE_TASK_LOG}\\{task.ToString()}";
                    TaskLog.WriteFileLog(path, task.ToString(), eLoginLevel.ToString(), DateTime.Now.ToString("fff") + " " + msg);
                }
            }
            catch(Exception ex)
            {

            }
        }

        public static eLogLevel eLoginLevel         = eLogLevel.NONE;       //로그인 레벨 상태
        public static eLogLevel eLoginLevelBuffer   = eLogLevel.NONE;       //로그인 레벨 버퍼
        public static stPassword stPassWord;                                //로그인 패스워드
    }

    public enum eLogLevel
    {
        NONE            = -99,
        OPERATOR        = 0,        // 오퍼레이터 모드
        MAINTENANCE,                // 엔지니어 모드Maintenance
        ADMIN,                      // 관리자 모드
        SUPER                       // Hidden 모드
    }
    
    public enum eLanguage   
    {
        KOREA = 0,
        ENGLISH,
        CHINA,
        VIETNAM
    };

    public struct stPassword
    {
        public string OP, ENG, SUP, SUPER;
    }

    #endregion "DEFINE"
    

    public static class cFnc
    {
        
        public static string GetUnitValueString(int Value, bool Use_mm = false)
        {
            if (Use_mm)
                return ((double)Value / 1000.0).ToString() + " mm";
            return Value.ToString() + " mm";
        }
        public static string GetUnitValueString(int Value, string str)
        {
            return Value.ToString() + str;
        }
        public static string GetUnitValueString(double Value, string str)
        {
            return Value.ToString() + str;
        }

        public static void Delay(int ms)
        {
            int time = Environment.TickCount;
            do
            {
                if (Environment.TickCount - time >= ms)
                    return;
            } while (true);
        }

        public static void ScreenSave(string FileName)
        {
            // 주화면의 크기 정보 읽기
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            // 2nd screen = Screen.AllScreens[1]

            // 픽셀 포맷 정보 얻기 (Optional)
            int bitsPerPixel = Screen.PrimaryScreen.BitsPerPixel;
            PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
            if (bitsPerPixel <= 16)
            {
                pixelFormat = PixelFormat.Format16bppRgb565;
            }
            if (bitsPerPixel == 24)
            {
                pixelFormat = PixelFormat.Format24bppRgb;
            }

            // 화면 크기만큼의 Bitmap 생성
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, pixelFormat);

            // Bitmap 이미지 변경을 위해 Graphics 객체 생성
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                // 화면을 그대로 카피해서 Bitmap 메모리에 저장
                gr.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
            }

            // Bitmap 데이타를 파일로 저장
            bmp.Save(FileName);
            bmp.Dispose();

        }

    }

    public static class cLog
    {
        public static int RunVCMLoader = 1000;
        public static int RunVCMPicker = 1500;
        public static int RunLensLoader = 2000;
        public static int RunLensPicker = 2500;
        public static int RunJigPlateAngle = 3000;
        public static int RunLensHeight = 3500;
        public static int RunBonder1 = 4000;
        public static int RunBonder2 = 4500;
        public static int RunInspectVision = 5000;
        public static int RunCuring1 = 5500;
        public static int RunCuring2 = 6000;
        public static int RunPlateAngle = 6500;
        public static int RunUnloader = 7000;
        public static int RunUnloaderPicker = 7500;
        public static int RunCleanJig = 8000;
        public static int RunIndex = 8500;
        public static int RunVCMVision = 9000;
        public static int RunActuator3 = 9500;

        public static int Form_Operator_Event = 110000;
        public static int Form_Operator_Data = 115000;
        public static int Form_Recipe_Event = 120000;
        public static int Form_Recipe_Data = 125000;

        public static int Form_VCMLoader_Event = 20000;
        public static int Form_VCMLoader_Data = 25000;
        public static int Form_VCMPicker_Event = 30000;
        public static int Form_VCMPicker_Data = 35000;
        public static int Form_LensLoader_Event = 40000;
        public static int Form_LensLoader_Data = 45000;
        public static int Form_LensPicker_Event = 50000;
        public static int Form_LensPicker_Data = 55000;
        public static int Form_Index_Event = 60000;
        public static int Form_Index_Data = 65000;
        public static int Form_Bonder_Event = 70000;
        public static int Form_Bonder_Data = 75000;
        public static int Form_Curing_Event = 80000;
        public static int Form_Curing_Data = 85000;
        public static int Form_Unloader_Event = 90000;
        public static int Form_Unloader_Data = 95000;
        public static int Form_UnloaderPicker_Event = 100000;
        public static int Form_UnloaderPicker_Data = 105000;
    }


    #region "IO"
    public static class cDI
    {
        // PC IN32 4EA

        public const int Start_Switch = 0;                                  
        public const int Stop_Switch = 1;
        public const int OK_Switch = 2;                                     // OK? 왜?
        public const int Home_Switch = 3;
        public const int Front_Emergency_Switch = 4;
        public const int Rear_Emergency_Switch = 5;
        public const int MC_On_Off = 6;

        public const int VCMLoading_Vacuum_Check = 7;                 
        //public const int VCMLoading_Vacuum_Press_Switch = 8;                // Press Switch? 머냐? 사용 필요 X

        public const int VCMUnloading_Vacuum_Check = 9;
//        public const int VCMUnloading_Vacuum_Press_Switch = 10;

        //public const int VCMLoading_Overload_Sensor = 11;
        //public const int VCMUnloading_Overload_Sensor = 12;

        public const int LensInsert_Vacuum_Check = 13;
        //public const int LensInsert_Vacuum_Press_Switch = 14;

        public const int Transfer_VCMLoading_Overload_1 = 15;
        public const int Transfer_VCMLoading_Overload_2 = 16;

        public const int Transfer_VCMUnloading_Overload_1 = 17;
        public const int Transfer_VCMUnloading_Overload_2 = 18;

        public const int Transfer_LensLoading_Overload_1 = 19;
        public const int Transfer_LensLoading_Overload_2 = 20;

        public const int Transfer_Loading_Tray_Clamp = 21;
        public const int Transfer_Loading_Tray_UnClamp = 22;

        public const int Transfer_Unloading_Tray_Clamp = 23;
        public const int Transfer_Unloading_Tray_UnClamp = 24;

        public const int Transfer_Lens_Tray_Clamp = 25;
        public const int Transfer_Lens_Tray_UnClamp = 26;

        public const int Transfer_Loading_Tray_Check = 27;
        public const int Transfer_Unloading_Tray_Check = 28;
        public const int Transfer_Lens_Tray_Check = 29;

        public const int Stage_VCMLoading_Tray_Clamp = 30;
        public const int Stage_VCMLoading_Tray_UnClamp = 31;


        public const int Stage_LensLoading_Tray_Clamp = 32;
        public const int Stage_LensLoading_Tray_UnClamp = 33;

        public const int Stage_VCMLoading_Tray_End_Sensor = 34;
        public const int Stage_VCMLoading_Tray_ConTact_Sensor = 35;

        public const int Stage_LensLoading_Tray_End_Sensor = 36;
        public const int Stage_LensLoading_Tray_Contact_Sensor = 37;

        public const int Stage_VCMUnloading_Tray_Clamp = 38; 
        public const int Stage_VCMUnloading_Tray_UnClamp = 39;

        public const int Stage_Unloading_Tray_End_Sensor = 40;
        public const int Stage_Unloading_Tray_Contact_Sensor = 41;

        public const int Stage_VCMUnLoading_NG_Tray_Check_Sensor = 42;

        public const int Stage_Unloading_Overload = 43;

        public const int VCMLoading_Magazine_Check = 44;
        public const int VCMUnloading_Magazine_Check = 45;
        public const int LENSLoading_Magazine_Check = 46;

        public const int Side_Angle_Measure_Unloading_Vacuum_Check = 47;
        //public const int Side_Angle_Measure_Unloading_Vacuum_Switch = 48;

        public const int Side_Angle_Measure_Unloading_Move_Forward = 49;
        public const int Side_Angle_Measure_Unloading_Move_Backward = 50;

        public const int Side_Angle_Measure_Unloading_Up = 51;
        public const int Side_Angle_Measure_Unloading_Down = 52;

        public const int Side_Angle_Measure_Unloading_Clamp = 53;
        public const int Side_Angle_Measure_Unloading_Unclamp = 54;
        public const int Side_Angle_Measure_Unloading_Contact = 55;
        public const int Side_Angle_MeaSure_Unloading_Untact = 56;

        public const int Air_Dispenser_1_Alarm1 = 57;
        public const int Air_Dispenser_1_Alarm2 = 58;
        public const int Lens_Height_Unit_Go = 59;
        public const int Air_Dispenser_2_Alarm1 = 60;
        public const int Air_Dispenser_2_Alarm2 = 61;

        public const int Bonding_Head_1_Nozzle_Height_Touch_Sensor = 62;
        public const int Bonding_Head_2_Nozzle_Height_Touch_Sensor = 63;


        public const int Bonding_Head_1_Nozzle_Clean_Clamp = 64;
        public const int Bonding_Head_1_Nozzle_Clean_UnClamp = 65;
        public const int Bonding_Head_2_Nozzle_Clean_Clamp = 66;
        public const int Bonding_Head_2_Nozzle_Clean_UnClamp = 67;

        public const int UV_Clamp_Up = 68;
        public const int UV_Clamp_Down = 69;
        public const int UV_Contact = 70;
        public const int UV_Untact = 71;

        public const int UV_1_Shutter_Open_Monitor = 72;
        public const int UV_1_Lamp_Ready_Monitor= 73;
        public const int UV_1_Alarm_Monitor = 74;
        public const int UV_1_Shutter_Close_Monitor = 75;

        //public const int Bonding_Head_2_PJC_100_Controller_IO_4 = 75;
        public const int UV_1_LampLife = 76;
        //public const int UV_Clamp_Down = 77;

        public const int UV_2_Shutter_Open_Monitor = 78;
        public const int UV_2_Lamp_Ready_Monitor = 79;
        public const int UV_2_Alarm_Monitor = 80;
        public const int UV_2_Shutter_Close_Monitor = 81;
        
        //public const int UV_Controller_Shutter_Open_Close = 81;
        public const int UV_2_LampLife = 82;
        //public const int UV_Controller_Various_Alarms = 83;

        public const int JIC_Clean_Up = 84;
        public const int JIC_Clean_Down = 85;
        public const int JIC_Clean_Vacuum_Check = 86;

        public const int VCM_Clamp_Clamp = 87;
        public const int VCM_Clamp_UnClamp = 88;

        public const int Actuator_1_Ready = 89;
        public const int Actuator_1_Pass = 90;
        public const int Actuator_1_Fail = 91;
        public const int Actuator_1_OISFail = 92;
        public const int Actuator_1_AFFail = 93;

        //public const int  = 94;

        public const int Actuator_2_Ready = 95;
        public const int Actuator_2_Pass = 96;
        public const int Actuator_2_Fail = 97;
        public const int Actuator_2_OISFail = 98;
        public const int Actuator_2_AFFail = 99;

        //public const int  = 100;
        public const int Actuator3_IndexCheck = 100;

        public const int Side_Angle_Sensor1_Go = 101;

        public const int Actuator_3_Ready = 102;

        public const int Bonder1ECM_End = 103;
        public const int Bonder2ECM_End = 104;
        public const int Bonder1ECM_DSO = 105;
        public const int Bonder2ECM_DSO = 106;

        public const int Actuator_3_Pass = 107;
        public const int Actuator_3_Fail = 108;
        public const int Actuator_3_OISFail = 109;
        public const int Actuator_3_AFFail = 110;


        public const int UV_Clamp_Up2 = 111;
        public const int UV_Clamp_Down2 = 112;

        public const int VCM_Loading = 113;
        public const int Lens_Height_Check = 114;
        public const int Curing_1 = 115;
        public const int Curing_2 = 116;
        public const int VCM_Unloading = 117;

        public const int Front_Door1_State = 118;
        public const int Front_Door2_State = 119;
        public const int Left_Side_Door3_State = 120;
        public const int Left_Side_Door4_State = 121;
        public const int Right_Side_Door5_State = 122;
        public const int Right_Side_Door6_State = 123;
        public const int Rear_Door7_State = 124;
        public const int Rear_Door8_State = 125;

        public const int Main_AirW_Sensor = 126;

    }
    public static class cDO
    {
        // PC OUT32 3EA
        public const int Start_Switch_Lamp = 0;
        public const int Stop_Switch_Lamp = 1;
        public const int OK_Switch_Lamp = 2;
        public const int Home_Switch_Lamp = 3;

        public const int Tower_Lamp_Red = 4;
        public const int Tower_Lamp_Yellow = 5;
        public const int Tower_Lamp_Green = 6;
        public const int Tower_Lamp_Buzzer = 7;

        public const int VCM_Loading_Vacuum = 8;
        public const int VCM_Loading_Blow = 9;

        public const int VCM_Unloading_Vacuum = 10;
        public const int VCM_Unloading_Blow = 11;

        public const int Lens_Insert_Vacuum = 12;
        public const int Lens_Insert_Blow= 13;

        public const int Transfer_Loading_Tray_Clamp = 14;
        public const int Transfer_Unloading_Tray_Clamp = 15;
        public const int Transfer_Lens_Tray_Clamp = 16;

        public const int Stage_VCM_Loading_Tray_Clamp = 17;
        public const int Stage_Lens_Loading_Tray_Clamp = 18;
        public const int Stage_VCM_Unloading_Tray_Clamp = 19;

        public const int Side_Angle_Measure_Unloading_Vacuum = 20;
        public const int Side_Angle_Measure_Unloading_Blow  = 21;
        public const int Side_Angle_Measure_Unloading_Move_FW_RV = 22;
        public const int Side_Angle_Measure_Unloading_Up_Down = 23;
        public const int Side_Angle_Measure_Unloading_Clamp = 24;
        public const int Side_Angle_Measure_Unloading_Contact = 25;

        public const int Actuator_3_A_Start = 26;
        public const int Actuator_3_B_Start = 27;
        public const int Actuator_3_Spare1 = 28;
        public const int Actuator_3_Spare2 = 29;
        public const int Actuator_3_Spare3 = 30;

        public const int Bonding_Head_1_Nozzle_Clean_Cylinder1_In_Out = 31;
        public const int Bonding_Head_2_Nozzle_Clean_Cylinder1_In_Out= 32;

        public const int Bonder_Dispensor_1_Jetting =33;
        public const int Bonder_Dispensor_2_Jetting =34;

        //public const int UV_Clamp_Up_Down_2 = 35;
        //public const int UV_Electric_Contact_Untact_2 = 36;

        public const int UV_1_Start = 37;               //Shutter Open
        public const int UV_1_Lamp_On = 38;
        
        //public const int Bonding_Head_2_PJC_100_Controller_IO_3 = 39;

        public const int UV_2_Start = 40;               //Shutter Open
        public const int UV_2_Lamp_On = 41;

        //public const int UV_Electric_Contact_Untact = 42;

        public const int JIC_Clean_Up_Down = 43;
        public const int JIC_Clean_Vacuum = 44;
        //public const int JIC_Clean_Blow = 45;
        public const int Lens_Height_Measure = 46;
        public const int Jig_Clean_Blow = 47;

        public const int VCM_Clamp = 48;

        public const int Actuator_1_A_Start = 49;
        public const int Actuator_1_B_Start = 50;
        public const int Actuator_1_Spare1 = 51;
        public const int Actuator_1_Spare2 = 52;
        public const int Actuator_1_Spare3 = 53;

        public const int Actuator_2_A_Start = 54;
        public const int Actuator_2_B_Start = 55;
        public const int Actuator_2_Spare1 = 56;
        public const int Actuator_2_Spare2 = 57;
        public const int Actuator_2_Spare3 = 58;

        public const int TJV_1_Cooling = 60;
        public const int TJV_2_Cooling = 62;

        public const int InnerLight = 63;

        //public const int  = 59;

        //public const int 면각도 및 평탄도 컨트롤러 I/O #1 = 60;
        //public const int 면각도 및 평탄도 컨트롤러 I/O #2 = 61;
        //public const int 면각도 및 평탄도 컨트롤러 I/O #3 = 62;
        //public const int 면각도 및 평탄도 컨트롤러 I/O #4 = 63;
        //public const int 면각도 및 평탄도 컨트롤러 I/O #5 = 64;
        //public const int 면각도 및 평탄도 컨트롤러 I/O #6 = 65;
    }

    public static class cAI
    {
        public const int Led_Pick_Up_Flow = 0;
    }
    #endregion "IO"
}

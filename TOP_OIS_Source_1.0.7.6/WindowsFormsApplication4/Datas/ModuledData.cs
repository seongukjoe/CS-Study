using System;
using System.Collections;
using System.Collections.Generic;
using XModule.Standard;

namespace XModule.Datas
{
    public enum TrayStatus
    {
        Empty,
        Load,
        Using,
        Finish
    }
    public enum LensTrayStatus
    {
        Empty,
        Load,
        Picked,
        Finish,
    }
    public enum eLensIndexStatus
    {
        Empty,
        VCMLoaded,
        JigPlateAngleFinish,
        JigPlateAngleFail,
        Act3Finish,
        Act3Fail,
        AssembleFinish,
        LensHeightFinish,
        LensHeightFail,
        Bonder1Finish,
        Bonder2Finish,
        VisionInspectFinish,
        VisionInspectFail,
        Curing1Finish,
        Actuating1Fail,
        Curing2Finish,
        PlateAngleFinish,
        PlateAngleFail,
        UnloadFinish,
        CleanJigFinish,
        Finish,
    }
    public enum eFailType
    {
        None,
        Actuating3Fail,
        AssembleFail,
        LensHeightFail,
        VisionInspectFail,
        Actuating1Fail,
        Actuating2Fail,
        PlateAngleFail,
        SideHeightFail, //Adding Error
    }
    public enum eJetType
    {
        Line,
        Arc,
    }

    [Serializable]
    public class Lens_Data
    {
        public Lens_Data()
        {
            Clear();
        }
        
        public LensTrayStatus Status;
        public eFailType FailType;
        public int Index;
        public int x;       // x 좌표
        public int y;       // y 좌표
        public bool Enable; // 사용 유무
        public double LensHeightData = 0.0;
        public double PlateAngleData = 0.0;


        public double[] PlateZData = new double[20];

        public double TT_LensTact;   // 배출택
        public double TT_VCMLoader;
        public double TT_VCMPicker;
        public double TT_LensLoader;
        public double TT_LensPicker;
        public double TT_LensPickerPick;
        public double TT_LensPickerPickCam;
        public double TT_LensPickerPlace;
        public double TT_LensPickerCam;
        public double TT_JigPlateAngle;
        public double TT_LensHeight;
        public double TT_Bonder1;
        public double TT_Bonder1GapMesure;
        public double TT_Bonder1Cam;
        public double TT_Bonder2;
        public double TT_Bonder2GapMesure;
        public double TT_Bonder2Cam;
        public double TT_VisionInspect;
        public double TT_Curing1;
        public double TT_Curing2;
        public double TT_PlateAngle;
        public double TT_Unloader;
        public double TT_UnloadPicker;
        public double TT_CleanJig;
        public double TT_Index;
        public double TT_TopVision;

        public void Clear()
        {
            Status = LensTrayStatus.Empty;
            FailType = eFailType.None;
            Index = 0;
            x = 0;
            y = 0;
            Enable = false;
            LensHeightData = 0.0;
            PlateAngleData = 0.0;

            TT_LensTact = 0; 
            TT_VCMLoader = 0;
            TT_VCMPicker = 0;
            TT_LensLoader = 0;
            TT_LensPicker = 0;
            TT_LensPickerPick = 0;
            TT_LensPickerPickCam = 0;
            TT_LensPickerPlace = 0;
            TT_LensPickerCam = 0;
            TT_JigPlateAngle = 0;
            TT_LensHeight = 0;
            TT_Bonder1 = 0;
            TT_Bonder1GapMesure = 0;
            TT_Bonder1Cam = 0;
            TT_Bonder2 = 0;
            TT_Bonder2GapMesure = 0;
            TT_Bonder2Cam = 0;
            TT_VisionInspect = 0;
            TT_Curing1 = 0;
            TT_Curing2 = 0;
            TT_PlateAngle = 0;
            TT_Unloader = 0;
            TT_UnloadPicker = 0;
            TT_CleanJig = 0;
            TT_Index = 0;
            TT_TopVision = 0;

            Array.Clear(PlateZData, 0, PlateZData.Length);
        }
        public void Assign(Lens_Data InputData)
        {
            Status = InputData.Status;
            FailType = InputData.FailType;
            Index = InputData.Index;
            x = InputData.x;
            y = InputData.y;
            LensHeightData = InputData.LensHeightData;
            PlateAngleData = InputData.PlateAngleData;

            //PlateZData = InputData.PlateZData; //210329, ksyoon

            TT_LensTact = InputData.TT_LensTact;
            TT_VCMLoader = InputData.TT_VCMLoader;
            TT_VCMPicker = InputData.TT_VCMPicker;
            TT_LensLoader = InputData.TT_LensLoader;
            TT_LensPicker = InputData.TT_LensPicker;
            TT_LensPickerPick = InputData.TT_LensPickerPick;
            TT_LensPickerPickCam = InputData.TT_LensPickerPickCam;
            TT_LensPickerPlace = InputData.TT_LensPickerPlace;
            TT_LensPickerCam = InputData.TT_LensPickerCam;
            TT_JigPlateAngle = InputData.TT_JigPlateAngle;
            TT_LensHeight = InputData.TT_LensHeight;
            TT_Bonder1 = InputData.TT_Bonder1;
            TT_Bonder1GapMesure = InputData.TT_Bonder1GapMesure;
            TT_Bonder1Cam = InputData.TT_Bonder1Cam;
            TT_Bonder2 = InputData.TT_Bonder2;
            TT_Bonder2GapMesure = InputData.TT_Bonder2GapMesure;
            TT_Bonder2Cam = InputData.TT_Bonder2Cam;
            TT_VisionInspect = InputData.TT_VisionInspect;
            TT_Curing1 = InputData.TT_Curing1;
            TT_Curing2 = InputData.TT_Curing2;
            TT_PlateAngle = InputData.TT_PlateAngle;
            TT_Unloader = InputData.TT_Unloader;
            TT_UnloadPicker = InputData.TT_UnloadPicker;
            TT_CleanJig = InputData.TT_CleanJig;
            TT_Index = InputData.TT_Index;
            TT_TopVision = InputData.TT_TopVision;


        }
    }
    [Serializable]
    public class Index_Data
    {
        public eLensIndexStatus Status;
        public int Index;                       // Index Number
        public eFailType FailType;              // Faile Type
        public int x;       // x 좌표
        public int y;       // y 좌표
        public double LensHeightData = 0.0;
        public double PlateAngleData = 0.0;

        public double[] PlateZData = new double[20];

        public double TT_LensTact;   // 배출택
        public double TT_VCMLoader;
        public double TT_VCMPicker;
        public double TT_LensLoader;
        public double TT_LensPicker;
        public double TT_LensPickerPick;
        public double TT_LensPickerPickCam;
        public double TT_LensPickerPlace;
        public double TT_LensPickerCam;
        public double TT_JigPlateAngle;
        public double TT_LensHeight;
        public double TT_Bonder1;
        public double TT_Bonder1GapMesure;
        public double TT_Bonder1Cam;
        public double TT_Bonder2;
        public double TT_Bonder2GapMesure;
        public double TT_Bonder2Cam;
        public double TT_VisionInspect;
        public double TT_Curing1;
        public double TT_Curing2;
        public double TT_PlateAngle;
        public double TT_Unloader;
        public double TT_UnloadPicker;
        public double TT_CleanJig;
        public double TT_Index;
        public double TT_TopVision;

        public Index_Data()
        {
            Clear();
        }
        public void Clear()
        {
            Status = eLensIndexStatus.Empty;
            Index = 0;
            FailType = eFailType.None;
            x = 0;
            y = 0;
            LensHeightData = 0.0;
            PlateAngleData = 0.0;


            TT_LensTact = 0;
            TT_VCMLoader = 0;
            TT_VCMPicker = 0;
            TT_LensLoader = 0;
            TT_LensPicker = 0;
            TT_LensPickerPick = 0;
            TT_LensPickerPickCam = 0;
            TT_LensPickerPlace = 0;
            TT_LensPickerCam = 0;
            TT_JigPlateAngle = 0;
            TT_LensHeight = 0;
            TT_Bonder1 = 0;
            TT_Bonder1GapMesure = 0;
            TT_Bonder1Cam = 0;
            TT_Bonder2 = 0;
            TT_Bonder2GapMesure = 0;
            TT_Bonder2Cam = 0;
            TT_VisionInspect = 0;
            TT_Curing1 = 0;
            TT_Curing2 = 0;
            TT_PlateAngle = 0;
            TT_Unloader = 0;
            TT_UnloadPicker = 0;
            TT_CleanJig = 0;
            TT_Index = 0;
            TT_TopVision = 0;

            Array.Clear(PlateZData, 0, PlateZData.Length);
        }
        public void Assign(Index_Data InputData)
        {
            Status = InputData.Status;
            Index = InputData.Index;
            FailType = InputData.FailType;
            x = InputData.x;
            y = InputData.y;
            LensHeightData = InputData.LensHeightData;
            PlateAngleData = InputData.PlateAngleData;

            PlateZData = InputData.PlateZData;

            TT_LensTact = InputData.TT_LensTact;
            TT_VCMLoader = InputData.TT_VCMLoader;
            TT_VCMPicker = InputData.TT_VCMPicker;
            TT_LensLoader = InputData.TT_LensLoader;
            TT_LensPicker = InputData.TT_LensPicker;
            TT_LensPickerPick = InputData.TT_LensPickerPick;
            TT_LensPickerPickCam = InputData.TT_LensPickerPickCam;
            TT_LensPickerPlace = InputData.TT_LensPickerPlace;
            TT_LensPickerCam = InputData.TT_LensPickerCam;
            TT_JigPlateAngle = InputData.TT_JigPlateAngle;
            TT_LensHeight = InputData.TT_LensHeight;
            TT_Bonder1 = InputData.TT_Bonder1;
            TT_Bonder1GapMesure = InputData.TT_Bonder1GapMesure;
            TT_Bonder1Cam = InputData.TT_Bonder1Cam;
            TT_Bonder2 = InputData.TT_Bonder2;
            TT_Bonder2GapMesure = InputData.TT_Bonder2GapMesure;
            TT_Bonder2Cam = InputData.TT_Bonder2Cam;
            TT_VisionInspect = InputData.TT_VisionInspect;
            TT_Curing1 = InputData.TT_Curing1;
            TT_Curing2 = InputData.TT_Curing2;
            TT_PlateAngle = InputData.TT_PlateAngle;
            TT_Unloader = InputData.TT_Unloader;
            TT_UnloadPicker = InputData.TT_UnloadPicker;
            TT_CleanJig = InputData.TT_CleanJig;
            TT_Index = InputData.TT_Index;
            TT_TopVision = InputData.TT_TopVision;

        }
    }
    [Serializable]
    public class Tray_Data
    {
        public TrayStatus Status;
        public List<Lens_Data> Items;
        public int Slot;
        public Tray_Data()
        {
            Status = TrayStatus.Empty;
            Slot = 0;
            Items = new List<Lens_Data>();
        }
        public void Init(int CountX, int CountY)
        {
            Items.Clear();
            for (int y = 0; y < CountY; y++)
            {
                for (int x = 0; x < CountX; x++)
                {
                    Lens_Data lens = new Lens_Data();
                    lens.Index = x + y * CountX;
                    lens.x = x;
                    lens.y = y;
                    Items.Add(lens);
                }
                //if (y % 2 == 0)
                //{
                //    for (int x = 0; x < CountX; x++)
                //    {
                //        Lens_Data lens = new Lens_Data();
                //        lens.Index = x + y * CountX;
                //        lens.x = x;
                //        lens.y = y;
                //        Items.Add(lens);
                //    }
                //}
                //else
                //{
                //    for (int x = CountX; x > 0; x--)
                //    {
                //        Lens_Data lens = new Lens_Data();
                //        lens.Index = CountX - (x - 1) + y * CountX;
                //        lens.x = x - 1;
                //        lens.y = y;
                //        Items.Add(lens);
                //    }
                //}
            }
        }

        public void Clear()
        {
            Status = TrayStatus.Empty;
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Status = LensTrayStatus.Empty;
            }
        }
        public void Load()
        {
            Status = TrayStatus.Load;
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Status = LensTrayStatus.Load;
            }
        }
        public bool IsFinish()
        {
            foreach(Lens_Data lens in Items)
            {
                if (lens.Status == LensTrayStatus.Load)
                    return false;
            }
            return true;
        }
    }

    public class Magazine_Data
    {
        public List<Tray_Data> Items;
        public Magazine_Data()
        {
            Items = new List<Tray_Data>();
        }
        public void Init(int SlotCount, int TrayCountX, int TrayCountY)
        {
            Items.Clear();
            for (int i = 0; i < SlotCount; i++)
            {
                Tray_Data tray = new Tray_Data();
                tray.Init(TrayCountX, TrayCountY);
                tray.Slot = i;
                tray.Status = TrayStatus.Empty;
                Items.Add(tray);
            }
        }
        public void Clear()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Clear();
            }
        }
        public void VCMLoad(int slot)
        {
            if (slot > Items.Count)
                return;

            if (cDEF.Work.Recipe.LoaderMagazineDirection)
            {
                for (int i = Items.Count - 1; i > Items.Count - 1 - slot; i--)
                {
                    Items[i].Load();
                }
            }
            else
            {
                for (int i = 0; i < slot; i++)
                {
                    Items[i].Load();
                }
            }
            
        }
        public void LensLoad(int slot)
        {
            if (slot > Items.Count)
                return;

            if (cDEF.Work.Recipe.LensMagazineDirection)
            {
                for (int i = Items.Count - 1; i > Items.Count - 1 - slot; i--)
                {
                    Items[i].Load();
                }
            }
            else
            {
                for (int i = 0; i < slot; i++)
                {
                    Items[i].Load();
                }
            }

        }
        public void UnloadLoad(int slot)
        {
            if (slot > Items.Count)
                return;

            if (cDEF.Work.Recipe.UnLoaderMagazineDirection)
            {
                for (int i = Items.Count - 1; i > Items.Count - 1 - slot; i--)
                {
                    Items[i].Load();
                }
            }
            else
            {
                for (int i = 0; i < slot; i++)
                {
                    Items[i].Load();
                }
            }

        }
    }

    public class JettingData
    {
        public bool Finish;

        public bool Enable;
        public double Radius;
        public double Angle;
        public int Delay;
        public int ZOffset;
        public int ZUp;
        public int PluseNum;
        public int DpTime;

        public JettingData()
        {
            Finish = false;

            Enable = false;
            Radius = 0.0;
            Angle = 0.0;
            Delay = 0;
            ZOffset = 0;
            ZUp = 0;
            PluseNum = 0;
            DpTime = 0;
        }
    }

    public class JettingLineData
    {
        public int XPos = 0;
        public int YPos = 0;
        public int ZPos = 0;

        public int LineSpeed = 0;
        public int ZSpeed = 0;

        public bool Shot = false;

        public JettingLineData()
        {
           

            XPos = 0;
            YPos = 0;
            ZPos = 0;

            LineSpeed = 0;
            ZSpeed = 0;

            Shot = false;

        }
    }
    public class PlateZ_RowData
    {
        public double[] Zdata = new double[20];

        public PlateZ_RowData()
        {
            for(int i = 0; i < 20; i++)
            {
                Zdata[i] = 0.0;
            }
        }
    }
    public class JettingPatternLineData
    {
        public bool Finish;

        public List<JettingLineData> JetLineData;

        public JettingPatternLineData()
        {
            Finish = false;

            JetLineData = new List<JettingLineData>();
        }
    }
    public class PlateZData
    {
        public bool Finish;

        public List<PlateZ_RowData> PlateZ_Data;

        public PlateZData()
        {
            PlateZ_Data = new List<PlateZ_RowData>();
        }
    }

    public class JettingArcData
    {
        public int XPos = 0;
        public int YPos = 0;
        public int EXPos = 0;
        public int EYPos = 0;
        public int Radius = 0;
        public double Angle = 0;
        public int ZPos = 0;

        public int LineSpeed = 0;
        public int ZSpeed = 0;

        public bool Shot = false;
        public eJetType JetType;

        public JettingArcData()
        {
            XPos = 0;
            YPos = 0;
            EXPos = 0;
            EYPos = 0;
            Radius = 0;
            ZPos = 0;

            LineSpeed = 0;
            ZSpeed = 0;

            Shot = false;

            JetType = eJetType.Arc;
        }
    }

    public class JettingPatternArcData
    {
        public bool Finish;

        public List<JettingArcData> JetArcData;

        public JettingPatternArcData()
        {
            Finish = false;

            JetArcData = new List<JettingArcData>();
        }
    }
    /// <summary>
    /// 비전 통신 응답 데이터
    /// </summary>
    public class VisioReciveData
    {
        public CmmStatus Status;
        public string barcode;
        public bool exist;
        public string fileName;
        public double x;
        public double y;
        public double t;
        public double score;

        public VisioReciveData()
        {
            Status = CmmStatus.NoResponse;
            barcode = string.Empty;
            exist = false;
            fileName = string.Empty;
            x = 0;
            y = 0;
            t = 0;
        }
    }
}




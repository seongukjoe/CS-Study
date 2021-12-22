using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIC_COM;
using XModule.Datas;
using XModule.Standard;

namespace XModule.Unit
{
    public enum eVision
    {
        V1_Ready,
        V1_Complete,
        V2_Ready,
        V2_Complete,
        V3_Ready,
        V3_Complete,
        V4_Ready,
        V4_Complete,
        V5_Ready,
        V5_Complete,
        V6_Ready,
        V6_Complete,
        V7_Ready,
        V7_Complete,
        V8_Ready,
        V8_Complete,
        Recipe
    }
    public class cVision
    {
        public cPcTcps Visions = null;
        public bool ConnectedV1 = false;
        public bool ConnectedV2 = false;
        public bool ConnectedV3 = false;
        public bool ConnectedV4 = false;
        public bool ConnectedV5 = false;
        public bool ConnectedV6 = false;

        public int BottomIndex = 1;

        public VisioReciveData ackV1_Ready = new VisioReciveData();
        public VisioReciveData ackV1_Complete = new VisioReciveData();
        public VisioReciveData ackV1_Recipe = new VisioReciveData();
        public VisioReciveData ackV2_Ready = new VisioReciveData();
        public VisioReciveData ackV2_Complete = new VisioReciveData();
        public VisioReciveData ackV3_Ready = new VisioReciveData();
        public VisioReciveData ackV3_Complete = new VisioReciveData();
        public VisioReciveData ackV4_Ready = new VisioReciveData();
        public VisioReciveData ackV4_Complete = new VisioReciveData();
        public VisioReciveData ackV5_Ready = new VisioReciveData();
        public VisioReciveData ackV5_Complete = new VisioReciveData();
        public VisioReciveData ackV6_Ready = new VisioReciveData();
        public VisioReciveData ackV6_Complete = new VisioReciveData();
        public VisioReciveData ackV7_Ready = new VisioReciveData();
        public VisioReciveData ackV7_Complete = new VisioReciveData();
        public VisioReciveData ackV8_Ready = new VisioReciveData();
        public VisioReciveData ackV8_Complete = new VisioReciveData();

        //public VisioReciveData ackRecipe = new VisioReciveData();

        public string RecipeName = string.Empty;


        const char STX = '$';
        const char ETX = '#';
        const char COMMA = ',';
        const char CR = '\r';
        const char LF = '\n';

        public cVision()
        {
            Visions = new cPcTcps(cPath.FILE_TCPIP);
            Visions.HClient["LENS_TOP"].OnStart += CVisionV1_OnStart;
            Visions.HClient["LENS_TOP"].OnRecieve += CVision_OnRecieve; 
            Visions.HClient["LENS_TOP"].OnStop += CVisionV1_OnStop; 


            Visions.HClient["LENS_BOTTOM"].OnStart += CVisionV2_OnStart;
            Visions.HClient["LENS_BOTTOM"].OnRecieve += CVision_OnRecieve; 
            Visions.HClient["LENS_BOTTOM"].OnStop += CVisionV2_OnStop; 

            Visions.HClient["VCM_TOP"].OnStart += CVisionV3_OnStart;
            Visions.HClient["VCM_TOP"].OnRecieve += CVision_OnRecieve; 
            Visions.HClient["VCM_TOP"].OnStop += CVisionV3_OnStop; 

            Visions.HClient["DIPPING_1"].OnStart += CVisionV4_OnStart;
            Visions.HClient["DIPPING_1"].OnRecieve += CVision_OnRecieve; 
            Visions.HClient["DIPPING_1"].OnStop += CVisionV4_OnStop; 

            Visions.HClient["DIPPING_2"].OnStart += CVisionV5_OnStart;
            Visions.HClient["DIPPING_2"].OnRecieve += CVision_OnRecieve; 
            Visions.HClient["DIPPING_2"].OnStop += CVisionV5_OnStop; 
//
            Visions.HClient["INSPECT"].OnStart += CVisionV6_OnStart;
            Visions.HClient["INSPECT"].OnRecieve += CVision_OnRecieve; 
            Visions.HClient["INSPECT"].OnStop += CVisionV6_OnStop; 
            Visions.Final();
            #if !Notebook
            Visions.StartClient();
            #endif
        }
        private void CVisionV1_OnStart(object sender, EventArgs e)
        {
            ConnectedV1 = true;
        }
        private void CVisionV2_OnStart(object sender, EventArgs e)
        {
            ConnectedV2 = true;
        }
        private void CVisionV3_OnStart(object sender, EventArgs e)
        {
            ConnectedV3 = true;
        }
        private void CVisionV4_OnStart(object sender, EventArgs e)
        {
            ConnectedV4 = true;
        }
        private void CVisionV5_OnStart(object sender, EventArgs e)
        {
            ConnectedV5 = true;
        }
        private void CVisionV6_OnStart(object sender, EventArgs e)
        {
            ConnectedV6 = true;
        }
        private void CVisionV1_OnStop(object sender, EventArgs e)
        {
            ConnectedV1 = false;
        }
        private void CVisionV2_OnStop(object sender, EventArgs e)
        {
            ConnectedV2 = false;
        }
        private void CVisionV3_OnStop(object sender, EventArgs e)
        {
            ConnectedV3 = false;
        }
        private void CVisionV4_OnStop(object sender, EventArgs e)
        {
            ConnectedV4 = false;
        }
        private void CVisionV5_OnStop(object sender, EventArgs e)
        {
            ConnectedV5 = false;
        }
        private void CVisionV6_OnStop(object sender, EventArgs e)
        {
            ConnectedV6 = false;
        }
        public bool IsConnect()
        {
            return (ConnectedV1 && ConnectedV2 && ConnectedV3 && ConnectedV4 && ConnectedV5 && ConnectedV6);
        }


        public void ReadStart(string strflag)
        {
            switch (strflag)
            {
                case "LENS_TOP":
                    Visions.HClient["LENS_TOP"].ReadStart();
                    break;
                case "LENS_BOTTOM":
                    Visions.HClient["LENS_BOTTOM"].ReadStart();
                    break;
                case "VCM_TOP":
                    Visions.HClient["VCM_TOP"].ReadStart();
                    break;
                case "DIPPING_1":
                    Visions.HClient["DIPPING_1"].ReadStart();
                    break;
                case "DIPPING_2":
                    Visions.HClient["DIPPING_2"].ReadStart();
                    break;
                case "INSPECT":
                    Visions.HClient["INSPECT"].ReadStart();
                    break;
            }

        }
        public void ReadStop(string strflag)
        {
            switch (strflag)
            {
                case "LENS_TOP":
                    Visions.HClient["LENS_TOP"].ReadStop();
                    break;
                case "LENS_BOTTOM":
                    Visions.HClient["LENS_BOTTOM"].ReadStop();
                    break;
                case "VCM_TOP":
                    Visions.HClient["VCM_TOP"].ReadStop();
                    break;
                case "DIPPING_1":
                    Visions.HClient["DIPPING_1"].ReadStop();
                    break;
                case "DIPPING_2":
                    Visions.HClient["DIPPING_2"].ReadStop();
                    break;
                case "INSPECT":
                    Visions.HClient["INSPECT"].ReadStop();
                    break;
            }

        }
        private void CVision_OnRecieve(object sender, string msg)
        {
            try
            {
                int stx = msg.IndexOf("$");
                int etx = msg.IndexOf("#");
                if (etx > 0)
                {
                    string sub = msg.Substring(stx, etx - stx);
                    string[] sArr = sub.Split(',');
                    if (sArr[0].Contains("V1"))
                    {
                        if (sArr[1].Contains("READY"))
                        {
                            ackV1_Ready.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("COMPLETE"))
                        {
                            if (sArr[2].Contains("OK"))
                            {
                                ackV1_Complete.exist = true;
                                ackV1_Complete.x = Convert.ToDouble(sArr[3]);
                                ackV1_Complete.y = Convert.ToDouble(sArr[4]);
                                ackV1_Complete.t = Convert.ToDouble(sArr[5]);
                            }
                            else if (sArr[2].Contains("NG"))
                            {
                                ackV1_Complete.exist = false;
                                ackV1_Complete.x = 0;
                                ackV1_Complete.y = 0;
                                ackV1_Complete.t = 0;
                            }
                            ackV1_Complete.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("RECIPE"))
                        {
                            if (sArr[2].Contains("OK"))
                            {
                                ackV1_Recipe.exist = true;
                            }
                            else
                            {
                                ackV1_Recipe.exist = false;
                            }
                            ackV1_Recipe.Status = CmmStatus.Ok;
                        }
                    }
                    else if (sArr[0].Contains("V2"))
                    {
                        if (sArr[1].Contains("READY"))
                        {
                            ackV2_Ready.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("COMPLETE"))
                        {
                            
                            if (sArr[2].Contains("OK"))
                            {
                                ackV2_Complete.exist = true;
                                ackV2_Complete.x = Convert.ToDouble(sArr[3]);
                                ackV2_Complete.y = Convert.ToDouble(sArr[4]);
                                ackV2_Complete.t = Convert.ToDouble(sArr[5]);
                            }
                            else if (sArr[2].Contains("NG"))
                            {
                                ackV2_Complete.exist = false;
                                ackV2_Complete.x = 0;
                                ackV2_Complete.y = 0;
                                ackV2_Complete.t = 0;
                            }
                            ackV2_Complete.Status = CmmStatus.Ok;
                        }
                    }
                    else if (sArr[0].Contains("V3"))
                    {
                        if (sArr[1].Contains("READY"))
                        {
                            ackV3_Ready.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("COMPLETE"))
                        {
                            if (sArr[2].Contains("OK"))
                            {
                                ackV3_Complete.exist = true;
                                ackV3_Complete.x = Convert.ToDouble(sArr[3]);
                                ackV3_Complete.y = Convert.ToDouble(sArr[4]);
                                ackV3_Complete.t = Convert.ToDouble(sArr[5]);
                            }
                            else if (sArr[2].Contains("NG"))
                            {
                                ackV3_Complete.exist = false;
                                ackV3_Complete.x = 0;
                                ackV3_Complete.y = 0;
                                ackV3_Complete.t = 0;
                            }
                            ackV3_Complete.Status = CmmStatus.Ok;
                        }
                    }
                    else if (sArr[0].Contains("V4"))
                    {
                        if (sArr[1].Contains("READY"))
                        {
                            ackV4_Ready.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("COMPLETE"))
                        {
                            if (sArr[2].Contains("OK"))
                            {
                                ackV4_Complete.exist = true;
                                ackV4_Complete.x = Convert.ToDouble(sArr[3]);
                                ackV4_Complete.y = Convert.ToDouble(sArr[4]);
                                ackV4_Complete.t = Convert.ToDouble(sArr[5]);
                            }
                            else if (sArr[2].Contains("NG"))
                            {
                                ackV4_Complete.exist = false;
                                ackV4_Complete.x = 0;
                                ackV4_Complete.y = 0;
                                ackV4_Complete.t = 0;
                            }
                            ackV4_Complete.Status = CmmStatus.Ok;
                        }
                    }
                    else if (sArr[0].Contains("V5"))
                    {
                        if (sArr[1].Contains("READY"))
                        {
                            ackV5_Ready.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("COMPLETE"))
                        {
                            
                            if (sArr[2].Contains("OK"))
                            {
                                ackV5_Complete.exist = true;
                                ackV5_Complete.x = Convert.ToDouble(sArr[3]);
                                ackV5_Complete.y = Convert.ToDouble(sArr[4]);
                                ackV5_Complete.t = Convert.ToDouble(sArr[5]);
                            }
                            else if (sArr[2].Contains("NG"))
                            {
                                ackV5_Complete.exist = false;
                                ackV5_Complete.x = 0;
                                ackV5_Complete.y = 0;
                                ackV5_Complete.t = 0;
                            }
                            ackV5_Complete.Status = CmmStatus.Ok;
                        }
                    }
                    else if (sArr[0].Contains("V6"))
                    {
                        if (sArr[1].Contains("READY"))
                        {
                            ackV6_Ready.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("COMPLETE"))
                        {
                            ackV6_Complete.Status = CmmStatus.Ok;
                            if (sArr[2].Contains("OK"))
                            {
                                ackV6_Complete.exist = true;
                                ackV6_Complete.x = 0;
                                ackV6_Complete.y = 0;
                                ackV6_Complete.t = 0;
                            }
                            else if (sArr[2].Contains("NG"))
                            {
                                ackV6_Complete.exist = false;
                                ackV6_Complete.x = 0;
                                ackV6_Complete.y = 0;
                                ackV6_Complete.t = 0;
                            }
                            else if (sArr[2].Contains("NONE"))
                            {
                                ackV6_Complete.exist = false;
                                ackV6_Complete.x = 0;
                                ackV6_Complete.y = 0;
                                ackV6_Complete.t = 0;
                            }
                        }
                    }
                    else if (sArr[0].Contains("V7"))
                    {
                        if (sArr[1].Contains("READY"))
                        {
                            ackV7_Ready.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("COMPLETE"))
                        {
                            if (sArr[2].Contains("OK"))
                            {
                                ackV7_Complete.exist = true;
                                ackV7_Complete.x = Convert.ToDouble(sArr[3]);
                                ackV7_Complete.y = Convert.ToDouble(sArr[4]);
                                ackV7_Complete.t = Convert.ToDouble(sArr[5]);
                            }
                            else if (sArr[2].Contains("NG"))
                            {
                                ackV7_Complete.exist = false;
                                ackV7_Complete.x = 0;
                                ackV7_Complete.y = 0;
                                ackV7_Complete.t = 0;
                            }
                            ackV7_Complete.Status = CmmStatus.Ok;
                        }
                    }
                    else if (sArr[0].Contains("V8"))
                    {
                        if (sArr[1].Contains("READY"))
                        {
                            ackV8_Ready.Status = CmmStatus.Ok;
                        }
                        else if (sArr[1].Contains("COMPLETE"))
                        {
                            if (sArr[2].Contains("OK"))
                            {
                                ackV8_Complete.exist = true;
                                ackV8_Complete.x = Convert.ToDouble(sArr[3]);
                                ackV8_Complete.y = Convert.ToDouble(sArr[4]);
                                ackV8_Complete.t = Convert.ToDouble(sArr[5]);
                            }
                            else if (sArr[2].Contains("NG"))
                            {
                                ackV8_Complete.exist = false;
                                ackV8_Complete.x = 0;
                                ackV8_Complete.y = 0;
                                ackV8_Complete.t = 0;
                            }
                            ackV8_Complete.Status = CmmStatus.Ok;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Sendmsg(eVision Index)
        {
            string format = string.Empty;
            switch (Index)
            {
                case eVision.V1_Ready:
                    ackV1_Ready.Status = CmmStatus.NoResponse;
                    format = $"$V1,READY,{ ((cDEF.Run.Index.Information.IndexNum + 9) % 12) + 1},{ ((cDEF.Run.Index.Information.IndexNum + 7) % 12) + 1},{ ((cDEF.Run.Index.Information.IndexNum + 6) % 12) + 1},{ ((cDEF.Run.Index.Information.IndexNum + 5) % 12) + 1}#";
                    break;
                case eVision.V1_Complete:
                    ackV1_Complete.Status = CmmStatus.NoResponse;
                    format = "$V1,START#";
                    break;
                case eVision.V2_Ready:
                    ackV2_Ready.Status = CmmStatus.NoResponse;
                    format = "$V2,READY#";
                    break;
                case eVision.V2_Complete:
                    ackV2_Complete.Status = CmmStatus.NoResponse;
                    format = "$V2,START#";
                    break;
                case eVision.V3_Ready:
                    ackV3_Ready.Status = CmmStatus.NoResponse;
                    format = "$V3,READY#";
                    break;
                case eVision.V3_Complete:
                    ackV3_Complete.Status = CmmStatus.NoResponse;
                    format = $"$V3,START,{BottomIndex}#";
                    break;
                case eVision.V4_Ready:
                    ackV4_Ready.Status = CmmStatus.NoResponse;
                    format = "$V4,READY#";
                    break;
                case eVision.V4_Complete:
                    ackV4_Complete.Status = CmmStatus.NoResponse;
                    format = "$V4,START#";
                    break;
                case eVision.V5_Ready:
                    ackV5_Ready.Status = CmmStatus.NoResponse;
                    format = "$V5,READY#";
                    break;
                case eVision.V5_Complete:
                    ackV5_Complete.Status = CmmStatus.NoResponse;
                    format = "$V5,START#";
                    break;
                case eVision.V6_Ready:
                    ackV6_Ready.Status = CmmStatus.NoResponse;
                    format = "$V6,READY#";
                    break;
                case eVision.V6_Complete:
                    ackV6_Complete.Status = CmmStatus.NoResponse;
                    format = "$V6,START#";
                    break;
                case eVision.V7_Ready:
                    ackV7_Ready.Status = CmmStatus.NoResponse;
                    format = "$V7,READY#";
                    break;
                case eVision.V7_Complete:
                    ackV7_Complete.Status = CmmStatus.NoResponse;
                    format = "$V7,START#";
                    break;
                case eVision.V8_Ready:
                    ackV8_Ready.Status = CmmStatus.NoResponse;
                    format = "$V8,READY#";
                    break;
                case eVision.V8_Complete:
                    ackV8_Complete.Status = CmmStatus.NoResponse;
                    format = "$V8,START#";
                    break;
                case eVision.Recipe:
                    RecipeName = cDEF.Work.Project.FileName;

                    ackV1_Recipe.Status = CmmStatus.NoResponse;
                    format = "$V1,RECIPE," + RecipeName + ",#";
                    break;

            }
            byte[] data = Encoding.ASCII.GetBytes(format);
            switch (Index)
            {
                case eVision.V1_Ready:
                case eVision.V1_Complete:
                case eVision.Recipe:
                    Visions.HClient["LENS_TOP"].Send(data);
                    break;

                case eVision.V2_Ready:
                case eVision.V2_Complete:
                    Visions.HClient["LENS_BOTTOM"].Send(data);
                    break;

                case eVision.V3_Ready:
                case eVision.V3_Complete:
                    Visions.HClient["VCM_TOP"].Send(data);
                    break;

                case eVision.V4_Ready:
                case eVision.V4_Complete:
                    Visions.HClient["DIPPING_1"].Send(data);
                    break;

                case eVision.V5_Ready:
                case eVision.V5_Complete:
                    Visions.HClient["DIPPING_2"].Send(data);
                    break;

                case eVision.V6_Ready:
                case eVision.V6_Complete:
                    Visions.HClient["INSPECT"].Send(data);
                    break;

                case eVision.V7_Ready:
                case eVision.V7_Complete:
                    Visions.HClient["DIPPING_1"].Send(data);
                    break;

                case eVision.V8_Ready:
                case eVision.V8_Complete:
                    Visions.HClient["DIPPING_2"].Send(data);
                    break;
            }
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XModule.Standard;
using XSmNetW;
using XValueNet;

namespace XModule.Unit
{
    public enum eMESEqpStatus
    {
        RUN,
        IDLE,
        DOWN,
        SETUP,
        DISCONNECT

    }
    public enum ePMStatus
    {
        LOAD_DOWN,
        MODEL_CHANGE,
        BM_START,
        MATR_DOWN,
    }
    public enum eAlarmStatus
    {
        Set,
        Clear,
        AllClear
    }

    public class CMES
    {
        // 공유메모리 변수 선언
#if !Notebook
        private XSmNet4CS _xcom = null;
#endif
        public string Device { get; set; }
        public string Operation { get; set; }
        public string EQPName { get; set; }

        public bool ConStatsus { get; set; }

        public string Product_Type { get; set; }

        public string DeviceCode { get; set; }
        public string OperationCode { get; set; }
        public string EQPCode { get; set; }
        public string Product_TypeCode { get; set; }
        public CMES()
        {
#if !Notebook
            // 공유메모리 변수 instance
            this._xcom = new XSmNet4CS();

            // 공유메모리 변수 이벤트 핸들러 등록
            this._xcom.OnConnected += _xcom_OnConnected;
            this._xcom.OnDisconnected += _xcom_OnDisconnected;
            this._xcom.OnReceived += _xcom_OnReceived;

            // 공유메모리 Initialize 및 Start 까지 해야함.
            Initialize();
           // Start();
#endif
        }
        /// <summary>
        /// 공유메모리 dll initialize
        /// </summary>
        private void Initialize()
        {
            // cfg 파일이 있어야 한다. 프로젝트에 포함시켰음. (EQ1.cfg)

#if !Notebook
            int res = this._xcom.Initialize(cPath.FILE_MES);
            this._xcom.BinaryType = true;
#endif

            //2021-04-28 Modify
            DeviceCode = cDEF.Work.MesStandInfo.DeviceCode;
            OperationCode = cDEF.Work.MesStandInfo.OperationCode;
            EQPCode = cDEF.Work.MesStandInfo.EQPCode;
            Product_TypeCode = cDEF.Work.MesStandInfo.Product_TypeCode;
            Device = cDEF.Work.MesStandInfo.Device;
            Operation = cDEF.Work.MesStandInfo.Operation;
            EQPName = cDEF.Work.MesStandInfo.EQPName;
            Product_Type = cDEF.Work.MesStandInfo.Product_Type;
            // -----------


            ConStatsus = false;


        }

        /// <summary>
        /// 공유메모리 dll 종료
        /// </summary>
        private void UnInitialize()
        {
#if !Notebook
            int ret = this._xcom.Terminate();
#endif
        }

        /// <summary>
        /// 공유메모리 dll Start
        /// </summary>
        public void Start()
        {
#if !Notebook
            int ret = this._xcom.Start();
#endif
        }

        private void Stop()
        {
#if !Notebook
            int ret = this._xcom.Stop();
#endif
        }

        /// <summary>
        /// XValueNet dll 을 이용하여 Parsing
        /// </summary>
        /// <param name="pcValue"></param>
        /// <param name="nSize"></param>
#if !Notebook
        private void _xcom_OnReceived(byte[] pcValue, int nSize)
        {
            int count = 0;
            string msgName = "";
            string temp = "";
            int itemp = 0;
            // XValueClr 변수 선언
            XValueClr xValue = new XValueClr();

            // XValueClr 을 이용하여 전송받은 byte[] 를 XValueClr 클래스에 Load
            int ret = xValue.LoadFromSECS2(ref pcValue, nSize);

            // 사양서에 맞게 코딩
            count = xValue.GetCount();          // LIST 2
            {
                XValueClr val1 = new XValueClr();
                xValue.GetFirstItem(ref val1); msgName = val1.GetString();

                // 메시지 이름으로 분류하여 Parsing
                if (msgName == "MES_STANDARD_INFO")
                {
                    xValue.GetNextItem(ref val1); count = val1.GetCount();              // LIST 3
                    {
                        XValueClr val2 = new XValueClr();
                        val1.GetFirstItem(ref val2); DeviceCode = val2.GetString();
                        val1.GetNextItem(ref val2); OperationCode = val2.GetString();
                        val1.GetNextItem(ref val2); EQPCode = val2.GetString();
                        val1.GetNextItem(ref val2); Product_TypeCode = val2.GetString();
                        val1.GetNextItem(ref val2); Device = val2.GetString();
                        val1.GetNextItem(ref val2); Operation = val2.GetString();
                        val1.GetNextItem(ref val2); EQPName = val2.GetString();
                        val1.GetNextItem(ref val2); Product_Type = val2.GetString();

                        //2021-04-28 Modify
                        if (DeviceCode == "" || DeviceCode == "null")
                            DeviceCode = cDEF.Work.MesStandInfo.DeviceCode;
                        else
                            cDEF.Work.MesStandInfo.DeviceCode = DeviceCode;

                        if (OperationCode == "" || OperationCode == "null")
                            OperationCode = cDEF.Work.MesStandInfo.OperationCode;
                        else
                            cDEF.Work.MesStandInfo.OperationCode = OperationCode;

                        if (EQPCode == "" || EQPCode == "null")
                            EQPCode = cDEF.Work.MesStandInfo.EQPCode;
                        else
                            cDEF.Work.MesStandInfo.EQPCode = EQPCode;

                        if (Product_TypeCode == "" || Product_TypeCode == "null")
                            Product_TypeCode = cDEF.Work.MesStandInfo.Product_TypeCode;
                        else
                            cDEF.Work.MesStandInfo.Product_TypeCode = Product_TypeCode;

                        if (Device == "" || Device == "null")
                            Device = cDEF.Work.MesStandInfo.Device;
                        else
                            cDEF.Work.MesStandInfo.Device = Device;

                        if (Operation == "" || Operation == "null")
                            Operation = cDEF.Work.MesStandInfo.Operation;
                        else
                            cDEF.Work.MesStandInfo.Operation = Operation;

                        if (EQPName == "" || EQPName == "null")
                            EQPName = cDEF.Work.MesStandInfo.EQPName;
                        else
                            cDEF.Work.MesStandInfo.EQPName = EQPName;

                        if (Product_Type == "" || Product_Type == "null")
                            Product_Type = cDEF.Work.MesStandInfo.Product_Type;
                        else
                            cDEF.Work.MesStandInfo.Product_Type = Product_Type;

                        cDEF.Work.MesStandInfo.Save();

                        //--------------------
                    }
                }
                else if (msgName == "MES_CONNECTION_STATUS")
                {
                    xValue.GetNextItem(ref val1); count = val1.GetCount();              // LIST 3
                    {
                        byte[] tempbyte;
                        XValueClr val2 = new XValueClr();
                        val1.GetFirstItem(ref val2); temp = val2.GetString();
                        val1.GetNextItem(ref val2); tempbyte = val2.GetU1();

                        if (tempbyte.Length > 0)
                            ConStatsus = tempbyte[0] == 0 ? false : true;


                    }
                }

                //if (msgName == "WORK_POSSIBLE_RESULT")
                //{
                //    xValue.GetNextItem(ref val1); count = val1.GetCount();              // LIST 3
                //    {
                //        XValueClr val2 = new XValueClr();
                //        val1.GetFirstItem(ref val2); temp = val2.GetString();        // EQUIPMENT_ID
                //        val1.GetNextItem(ref val2); temp = val2.GetString();        // MATERIAL_NAME
                //        val1.GetNextItem(ref val2); temp = val2.GetString();        // BARCODE
                //    }
                //}
                //else if (msgName == "RECIPE_REGISTER")
                //{
                //    xValue.GetNextItem(ref val1); count = val1.GetCount();            // LIST 2
                //    {
                //        XValueClr val2 = new XValueClr();
                //        val1.GetFirstItem(ref val2); temp = val2.GetString();        // EQUIPMENT_ID
                //        val1.GetNextItem(ref val2); count = val2.GetCount();           // LIST 1
                //        {
                //            XValueClr val3 = new XValueClr();
                //            val2.GetFirstItem(ref val3); temp = val3.GetString();    // RESULT
                //        }
                //    }
                //}
            }
        }
#endif
/// <summary>
/// 공유메모리 연결종료 이벤트 (아래 코드를 넣어준다. 그렇지 않을 경우 연결이 끊어졌을 때 이벤트 계속 발생)
/// </summary>

#if !Notebook
        private void _xcom_OnDisconnected()
        {
            Stop();
            UnInitialize();

            System.Threading.Thread.Sleep(5000);

            Initialize();
            Start();
        }

        /// <summary>
        /// 공유메모리 연결 이벤트
        /// </summary>
        private void _xcom_OnConnected()
        {
            // TODO
            Send_MESOtpion();
            Send_PCInfoData();
          
            Send_EquipStatus();
            Send_AlarmStatus(eAlarmStatus.AllClear,0,"");
        }
#endif
        public void Send_EquipStatus()
        {
#if !Notebook
            XValueClr xValue = new XValueClr();
            XValueClr xList1 = new XValueClr();
            XValueClr xList2 = new XValueClr();
            XValueClr xList3 = new XValueClr();
            XValueClr xTemp = new XValueClr();

            // 함수테스트
            xValue.SetList();           // LIST 2
            {
                xTemp.SetString("COLLECTION_EVENT"); xValue.Add(xTemp);             // MESSAGE NAME //고정
                xList1.SetList();                                                   // LIST 3
                {
                    xTemp.SetString(cDEF.Work.Project.GlobalOption.EQPID); xList1.Add(xTemp);      // EQUIPMENT ID //장비 아이디를 1번이라도 못 받으면 알람
                    xTemp.SetU1(1); xList1.Add(xTemp);                              // CEID = 1 
                    xList2.SetList();                                               // LIST 2
                    {

                        xTemp.SetU1(100); xList2.Add(xTemp);                       // RPTID
                        xList3.SetList();                                           // LIST 1
                        {
                            byte st = (byte)(cDEF.Run.MESEQPStatus+1);
                            xTemp.SetU1(st); xList3.Add(xTemp);                      // NEW_STATE

                            xTemp.SetString(cDEF.Run.MesStatusMsg); xList3.Add(xTemp);            //추가 상태 정보를 전송 함

                        }

                        xList2.Add(xList3);
                    }
                    xList1.Add(xList2);
                }
                xValue.Add(xList1);
            }

            int len = xValue.GetByteLength();
            byte[] sendData = new byte[len];

            xValue.MakeSECS2Stream(ref sendData, len);

            int ret = this._xcom.SendMsg(sendData, sendData.Length);
#endif
        }
        public void Send_PMStatus(ePMStatus pmStatus)
        {
#if !Notebook
            XValueClr xValue = new XValueClr();
            XValueClr xList1 = new XValueClr();
            XValueClr xList2 = new XValueClr();
            XValueClr xList3 = new XValueClr();
            XValueClr xTemp = new XValueClr();

            // 함수테스트
            xValue.SetList();           // LIST 2
            {
                xTemp.SetString("COLLECTION_EVENT"); xValue.Add(xTemp);             // MESSAGE NAME //고정
                xList1.SetList();                                                   // LIST 3
                {
                    xTemp.SetString(cDEF.Work.Project.GlobalOption.EQPID); xList1.Add(xTemp);      // EQUIPMENT ID //장비 아이디를 1번이라도 못 받으면 알람
                    xTemp.SetU1(2); xList1.Add(xTemp);                              // CEID = 2
                    xList2.SetList();                                               // LIST 2
                    {

                        xTemp.SetU1(101); xList2.Add(xTemp);                       // RPTID
                        xList3.SetList();                                           // LIST 1
                        {
                            byte st = (byte)(pmStatus+1);
                            xTemp.SetU1(st); xList3.Add(xTemp);                      // NEW_STATE

                            xTemp.SetString(pmStatus.ToString()); xList3.Add(xTemp);            //추가 상태 정보를 전송 함
                        }

                        xList2.Add(xList3);
                    }
                    xList1.Add(xList2);
                }
                xValue.Add(xList1);
            }

            int len = xValue.GetByteLength();
            byte[] sendData = new byte[len];

            xValue.MakeSECS2Stream(ref sendData, len);

            int ret = this._xcom.SendMsg(sendData, sendData.Length);
#endif
        }
        public void Send_AlarmStatus(eAlarmStatus AlarmStatus, int AlarmID, string strMsg)
        {
#if !Notebook
            XValueClr xValue = new XValueClr();
            XValueClr xList1 = new XValueClr();
            XValueClr xList2 = new XValueClr();
            XValueClr xList3 = new XValueClr();
            XValueClr xTemp = new XValueClr();


            // 함수테스트
            xValue.SetList();           // LIST 2
            {
                xTemp.SetString("COLLECTION_EVENT"); xValue.Add(xTemp);             // MESSAGE NAME //고정
                xList1.SetList();                                                   // LIST 3
                {
                    xTemp.SetString(cDEF.Work.Project.GlobalOption.EQPID); xList1.Add(xTemp);      // EQUIPMENT ID //장비 아이디를 1번이라도 못 받으면 알람
                    xTemp.SetU1(3); xList1.Add(xTemp);                              // CEID = 3
                    xList2.SetList();                                               // LIST 2
                    {

                        xTemp.SetU1(102); xList2.Add(xTemp);                       // RPTID
                        xList3.SetList();                                           // LIST 1
                        {
                            byte st = (byte)(AlarmStatus+1);
                            xTemp.SetU1(st); xList3.Add(xTemp);                      // NEW_STATE
                            xTemp.SetU2((ushort)AlarmID); xList3.Add(xTemp);                      // NEW_STATE

                            xTemp.SetString(strMsg); xList3.Add(xTemp);            //추가 상태 정보를 전송 함
                        }

                        xList2.Add(xList3);
                    }
                    xList1.Add(xList2);
                }
                xValue.Add(xList1);
            }

            int len = xValue.GetByteLength();
            byte[] sendData = new byte[len];

            xValue.MakeSECS2Stream(ref sendData, len);

            int ret = this._xcom.SendMsg(sendData, sendData.Length);
#endif
        }
        public void Send_PCInfoData()
        {
#if !Notebook
            XValueClr xValue = new XValueClr();
            XValueClr xList1 = new XValueClr();
            XValueClr xList2 = new XValueClr();
            XValueClr xList3 = new XValueClr();
            XValueClr xTemp = new XValueClr();

            // 함수테스트
            xValue.SetList();           // LIST 2
            {
                xTemp.SetString("EQUIP_PC_INFO"); xValue.Add(xTemp);             // MESSAGE NAME //고정
                xList1.SetList();                                                   // LIST 3
                {
                    xTemp.SetString(cDEF.Work.Project.GlobalOption.EQPID); xList1.Add(xTemp);      // EQUIPMENT ID //장비 아이디를 1번이라도 못 받으면 알람
                    xList2.SetList();                                               // LIST 2
                    {

                        xTemp.SetString(""); xList2.Add(xTemp);            //OS
                        xTemp.SetString(""); xList2.Add(xTemp);            //BIT
                        xTemp.SetString(""); xList2.Add(xTemp);            //IP
                        xTemp.SetString(""); xList2.Add(xTemp);            //MAC ADDRESS
                        xTemp.SetString(cPath.Version); xList2.Add(xTemp); //핸들러 버전
                        xTemp.SetString(cDEF.Work.Project.GlobalOption.VisionVer); xList2.Add(xTemp); //비전 버전
                    }
                    xList1.Add(xList2);
                }
                xValue.Add(xList1);
            }

            int len = xValue.GetByteLength();
            byte[] sendData = new byte[len];

            xValue.MakeSECS2Stream(ref sendData, len);

            int ret = this._xcom.SendMsg(sendData, sendData.Length);
#endif
        }
        public void Send_MESOtpion()
        {
#if !Notebook
            XValueClr xValue = new XValueClr();
            XValueClr xList1 = new XValueClr();
            XValueClr xList2 = new XValueClr();
            XValueClr xList3 = new XValueClr();
            XValueClr xTemp = new XValueClr();

            // 함수테스트
            xValue.SetList();           // LIST 2
            {
                xTemp.SetString("MES_USE_OPTION"); xValue.Add(xTemp);             // MESSAGE NAME //고정
                xList1.SetList();                                                   // LIST 3
                {
                    xTemp.SetString(cDEF.Work.Project.GlobalOption.EQPID); xList1.Add(xTemp);      // EQUIPMENT ID //장비 아이디를 1번이라도 못 받으면 알람

                    if (cDEF.Work.Project.GlobalOption.UseMES)
                    {
                        xTemp.SetU1(1); xList1.Add(xTemp);                              //MES USE
                    }
                    else
                        xTemp.SetU1(0); xList1.Add(xTemp);                              //MES USE

                }
                xValue.Add(xList1);
            }

            int len = xValue.GetByteLength();
            byte[] sendData = new byte[len];

            xValue.MakeSECS2Stream(ref sendData, len);

            int ret = this._xcom.SendMsg(sendData, sendData.Length);
#endif
        }

        public void Send_RecipeChange()
        {
#if !Notebook
            //2021-04-28 Modify
            //cDEF.Mes.Device = "";
            //  cDEF.Mes.Operation = "";
            //cDEF.Mes.Product_Type = "";
            //cDEF.Mes.EQPName = "";
            XValueClr xValue = new XValueClr();
            XValueClr xList1 = new XValueClr();
            XValueClr xList2 = new XValueClr();
            XValueClr xList3 = new XValueClr();
            XValueClr xTemp = new XValueClr();

            // 함수테스트
            xValue.SetList();           // LIST 2
            {
                xTemp.SetString("RECIPE_CHANGE_EVENT"); xValue.Add(xTemp);             // MESSAGE NAME //고정
                xList1.SetList();                                                   // LIST 3
                {
                    xTemp.SetString(cDEF.Work.Project.GlobalOption.EQPID); xList1.Add(xTemp);      // EQUIPMENT ID //장비 아이디를 1번이라도 못 받으면 알람
                 
                }
                xValue.Add(xList1);
            }

            int len = xValue.GetByteLength();
            byte[] sendData = new byte[len];

            xValue.MakeSECS2Stream(ref sendData, len);

            int ret = this._xcom.SendMsg(sendData, sendData.Length);
#endif
        }
    }
}

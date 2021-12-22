using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XModule.Standard;

namespace XModule.Unit
{

    public class ClientSocket : C_LibraryGlobal
    {

        private Socket worksocket = null;
        private AsyncCallback ReceiveHandler;
        private AsyncCallback SendHandler;
        private AsyncCallback AcceptHandler;

        Socket_Properties _Socket_PROP;
       
        public ClientSocket()
        {
            Init();
        }

        public ClientSocket(Socket_Properties prop)
        {
            _Socket_PROP = prop;
            Init();
        }
        public Socket_Properties Socket_PROP
        {
            set { Socket_PROP = value; }
        }


        private void Init()
        {
            // 비동기 작업에 사용될 대리자를 초기화합니다.
            AcceptHandler = new AsyncCallback(handleAccept);
            ReceiveHandler = new AsyncCallback(handleDataReceive);
            SendHandler = new AsyncCallback(handleDataSend);
        }

        public bool IsConnected
        {
            get
            {
                try
                {
                    if (worksocket == null) return false;
                    if (worksocket.Connected == false) return false;

                    bool poll = (worksocket.Poll(1000, SelectMode.SelectRead));
                    bool available = (worksocket.Available == 0);
                    return !(poll && available);
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool ConnectToServer()
        {


            // TCP 통신을 위한 소켓을 생성합니다.
            worksocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            IPAddress ipAddress = IPAddress.Parse(_Socket_PROP.hostName);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _Socket_PROP.hostPort);
            worksocket.BeginConnect(ipEndPoint, AcceptHandler, null);


            try
            {

                //네트워크가 끊어질 경우 오류 이벤트 뜸
                int size = sizeof(UInt32);
                UInt32 on = 1;
                UInt32 keepAliveInterval = 5000; //Send a packet once every 10 seconds.
                UInt32 retryInterval = 1000; //If no response, resend every second.
                byte[] inArray = new byte[size * 3];
                Array.Copy(BitConverter.GetBytes(on), 0, inArray, 0, size);
                Array.Copy(BitConverter.GetBytes(keepAliveInterval), 0, inArray, size, size);
                Array.Copy(BitConverter.GetBytes(retryInterval), 0, inArray, size * 2, size);

                System.Threading.Thread.Sleep(2000);

                if (worksocket != null)
                    worksocket.IOControl(IOControlCode.KeepAliveValues, inArray, null);


            }
            catch (SocketException exc)
            {
                if (exc.SocketErrorCode != SocketError.Success)
                {
                    switch (exc.SocketErrorCode)
                    {
                        case SocketError.ConnectionAborted:
                            //Console.WriteLine("Socket connected Error(Connection Aborted)");
                            CloseToServer();
                            break;
                        case SocketError.ConnectionRefused:
                            //Console.WriteLine("Socket connected Error(Connection Refused)");
                            CloseToServer();
                            break;
                        case SocketError.ConnectionReset:
                            // Console.WriteLine("Socket connected Error(Connection Reset)");
                            CloseToServer();
                            break;
                        case SocketError.NotConnected:
                            //Console.WriteLine("Socket connected Error(Not Connected)");
                            CloseToServer();
                            break;
                        default:
                            //  Console.WriteLine($"Socket connected Error({ex.SocketErrorCode.ToString()})");
                            CloseToServer();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                // Console.WriteLine($"Socket connected exception Error({e.ToString()})");
                if (IsConnected)
                    CloseToServer();
            }


            return IsConnected;
        }

        public bool CloseToServer()
        {
            if (worksocket == null) return true;
            bool result = true;

            try
            {
                if (_Socket_PROP.iniFilepath != null)
                {
                    if (ThreadLoopSend.ThreadState == System.Threading.ThreadState.Running)
                    {
                        ThreadLoopSend.Abort();
                        ThreadLoopSend.Join();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }

            try
            {
                if (IsConnected)
                    worksocket.Shutdown(SocketShutdown.Both);
            }
            finally
            {
                //Console.WriteLine(ex);
                if (worksocket != null)
                {
                    try
                    {
                       // worksocket.Close();
                        worksocket.Dispose();
                        worksocket = null;
                    }
                    catch (Exception )
                    {
                    }
               
                }
                Console.WriteLine($"[DISCONNECT]\t");
            }

            return result;
        }

        public override void LoopSend()
        {


            while (worksocket.Connected)
            {
                if (Senddata)
                {
                    if (loopCmdDatastr.Count == 0)
                    {
                        SetParameterLoop(_Socket_PROP.bsendtypestr, _Socket_PROP.reciveuse);
                    }

                    //명령어 전송
                    if (_Socket_PROP.bsendtypestr)
                    {
                        SendMessageStr(loopCmdDatastr[0].Key);
                        //응답 대기 여부
                        if (loopCmdDatastr[0].Value)
                        {
                            Senddata = false;
                        }
                        else
                        {
                            loopCmdDatastr.RemoveAt(0);
                            Senddata = true;
                            RetryCnt = 0;
                        }
                    }
                    else
                    {
                        SendMessageByte(loopCmdDatabyte[0].Key);
                        //응답 대기 여부
                        if (loopCmdDatabyte[0].Value)
                        {
                            Senddata = false;
                        }
                        else
                        {
                            loopCmdDatabyte.RemoveAt(0);
                            Senddata = true;
                            RetryCnt = 0;
                        }
                    }


                    timeStamp.Restart();
                }
                else
                {
                    //타이머 응답 대기 및 Retry
                    if (timeStamp.ElapsedMilliseconds > _Socket_PROP.ReciveWaitTime)
                    {
                        RetryCnt++;

                        if (RetryCnt > 5)
                        {
                            if (_Socket_PROP.bsendtypestr)
                            {
                                if (loopCmdDatastr.Count > 0)
                                    loopCmdDatastr.RemoveAt(0);
                            }
                            else
                            {
                                if (loopCmdDatabyte.Count > 0)
                                    loopCmdDatabyte.RemoveAt(0);
                            }
                            RetryCnt = 0;
                        }
                        Senddata = true;
                    }
                }
                Thread.Sleep(_Socket_PROP.LoopWatitTime);
            }

            CloseToServer();
            ConnectToServer();


        }

        public void SendMessageStr(string message)
        {
            // 추가 정보를 넘기기 위한 변수 선언
            // 크기를 설정하는게 의미가 없습니다.
            // 왜냐하면 바로 밑의 코드에서 문자열을 유니코드 형으로 변환한 바이트 배열을 반환하기 때문에
            // 최소한의 크기르 배열을 초기화합니다.
            AsyncObject ao = new AsyncObject(1);

            // 문자열을 바이트 배열으로 변환
            ao.Buffer = _Socket_PROP.eEncoding.GetBytes(message);

            ao.WorkingSocket = worksocket;

            // 전송 시작!
            try
            {
                if (worksocket != null)
                {


                    worksocket.BeginSend(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, SendHandler, ao);
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("전송 중 오류 발생!\n메세지: {0}", ex.Message);
                if (!Senddata)
                {
                    RetryCnt++;

                    if (RetryCnt > 5)
                    {
                        if (_Socket_PROP.bsendtypestr)
                        {
                            if (loopCmdDatastr.Count > 0)
                                loopCmdDatastr.RemoveAt(0);
                        }
                        else
                        {
                            if (loopCmdDatabyte.Count > 0)
                                loopCmdDatabyte.RemoveAt(0);
                        }
                        RetryCnt = 0;
                    }
                    Senddata = true;
                }
            }
        }

        public void SendMessageByte(byte[] message)
        {
            // 추가 정보를 넘기기 위한 변수 선언
            // 크기를 설정하는게 의미가 없습니다.
            // 왜냐하면 바로 밑의 코드에서 문자열을 유니코드 형으로 변환한 바이트 배열을 반환하기 때문에
            // 최소한의 크기르 배열을 초기화합니다.
            AsyncObject ao = new AsyncObject(1);

            ao.Buffer = message;

            ao.WorkingSocket = worksocket;

            // 전송 시작!
            try
            {
                worksocket.BeginSend(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, SendHandler, ao);
            }
            catch (Exception ex)
            {
                if (!Senddata)
                {
                    RetryCnt++;

                    if (RetryCnt > 5)
                    {
                        if (_Socket_PROP.bsendtypestr)
                        {
                            if (loopCmdDatastr.Count > 0)
                                loopCmdDatastr.RemoveAt(0);
                        }
                        else
                        {
                            if (loopCmdDatabyte.Count > 0)
                                loopCmdDatabyte.RemoveAt(0);
                        }
                        RetryCnt = 0;
                    }
                    Senddata = true;
                }
            }
        }


        /// <summary>
        ///  서버와 연결되었을 때 호출
        /// </summary>
        /// <param name="ar"></param>
        private void handleAccept(IAsyncResult ar)
        {
            try
            {
                // 4096 바이트의 크기를 갖는 바이트 배열을 가진 AsyncObject 클래스 생성
                AsyncObject ao = new AsyncObject(4096);

                // 작업 중인 소켓을 저장하기 위해 sockClient 할당
                ao.WorkingSocket = worksocket;

                //  worksocket.EndConnect(ar);
                // 비동기적으로 들어오는 자료를 수신하기 위해 BeginReceive 메서드 사용!
                if (worksocket != null)
                    worksocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, ReceiveHandler, ao);

                //LOOPSend
                if (_Socket_PROP.iniFilepath != null)
                {
                    Senddata = true;
                    SetParameter(_Socket_PROP.iniFilepath);
                    ThreadLoopSend = new Thread(new ThreadStart(LoopSend));
                    ThreadLoopSend.Start();

                }

               

            }
            catch (Exception ex)
            {
               
                if (ex.HResult == -2146232798)
                {
                    // handleClose(ar);
                    if (worksocket != null)
                    {
                        if (worksocket.Connected == true)
                            CloseToServer();
                        ErrMsg("Lose connection", ex.Message);
                    }
                }
                else
                {
                    if (worksocket != null)
                    {
                        CloseToServer();
                        ErrMsg("Server Connection Faile", ex.Message);
                    }
                }
            }
        }

        private void handleClose(IAsyncResult ar)
        {

            AsyncObject ao = (AsyncObject)ar.AsyncState;

            ao.WorkingSocket.Close();

        }


        private void handleDataReceive(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            AsyncObject ao = (AsyncObject)ar.AsyncState;
            if (IsConnected == false)
            {
                CloseToServer();
                return;
            }

            // 받은 바이트 수 저장할 변수 선언
            Int32 recvBytes = 0;

            try
            {
                // 자료를 수신하고, 수신받은 바이트를 가져옵니다.
                recvBytes = ao.WorkingSocket.EndReceive(ar);
            }
            catch (System.Net.Sockets.SocketException ex)
            {

                // Error Code 10053 : "현재 연결은 사용자의 호스트 시스템의 소프트웨어의 의해 중단되었습니다"
                // Error Code 10054 : Client 연결 끊김
                if (ex.ErrorCode == 10053 || ex.ErrorCode == 10054)
                    handleClose(ar);

                return;
            }
            catch (Exception ex)
            { 
                // 예외가 발생하면 함수 종료!
               
                if (ex.HResult == -2146232798)
                {
                    if (IsConnected)
                        CloseToServer();

                }
                else
                    ErrMsg("Data Reacive  Faile", ex.Message);

                return;
            }

            // 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
            if (recvBytes > 0)
            {
                // 공백 문자들이 많이 발생할 수 있으므로, 받은 바이트 수 만큼 배열을 선언하고 복사한다.
                Byte[] msgByte = new Byte[recvBytes];
                Array.Copy(ao.Buffer, msgByte, recvBytes);

                // 받은 메세지를 출력
                //  Console.WriteLine("메세지 받음: {0}", Encoding.Unicode.GetString(msgByte));
                //ReciveMsg(_Socket_PROP.eEncoding.GetString(msgByte));
                ReciveMsgByte(msgByte);
            }

            try
            {
                // 자료 처리가 끝났으면~
                // 이제 다시 데이터를 수신받기 위해서 수신 대기를 해야 합니다.
                // Begin~~ 메서드를 이용해 비동기적으로 작업을 대기했다면
                // 반드시 대리자 함수에서 End~~ 메서드를 이용해 비동기 작업이 끝났다고 알려줘야 합니다!
                ao.WorkingSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, ReceiveHandler, ao);

                if (!Senddata)
                {
                    //명령어 전송
                    if (_Socket_PROP.bsendtypestr)
                    {
                        loopCmdDatastr.RemoveAt(0);
                        Senddata = true;
                        RetryCnt = 0;
                    }
                    else
                    {
                        loopCmdDatabyte.RemoveAt(0);
                        Senddata = true;
                        RetryCnt = 0;
                    }

                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
               
                // Error Code 10053 : "현재 연결은 사용자의 호스트 시스템의 소프트웨어의 의해 중단되었습니다"
                // Error Code 10054 : Client 연결 끊김
                if (ex.ErrorCode == 10053 || ex.ErrorCode == 10054)
                {
                    handleClose(ar);
                    ErrMsg("Lose connection", ex.Message);
                }

            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146232798)
                {

                    if (IsConnected)
                        handleClose(ar);
                }
                else
                    ErrMsg("Data Reacive  Faile", ex.Message);

                return;

            }
        }

        private void handleDataSend(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 보낸 바이트 수를 저장할 변수 선언
            Int32 sentBytes;

            try
            {
                // 자료를 전송하고, 전송한 바이트를 가져옵니다.
                sentBytes = ao.WorkingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                ErrMsg("Data Send Faile", ex.Message);
                return;
            }

            if (sentBytes > 0)
            {
                Byte[] msgByte = new Byte[sentBytes];
                Array.Copy(ao.Buffer, msgByte, sentBytes);

            }
        }
    }
}

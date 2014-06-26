using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MTouchPadServerShell
{
    class TouchServer : ITouchServer
    {
        private bool _isServiceShutDown;
        private Thread _serviceThread;
        private ITouchCommandHandler _touchCommandHandler;
        private EndPoint _currentClient;
        public void Start(Action<bool> startResult)
        {
            try
            {
                _serviceThread = new Thread(() =>
                                     {
                                         var serverEndpoint = new IPEndPoint(IPAddress.Any, ServerPort);
                                         var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                                       //  serverSocket.ReceiveTimeout = 3000;
                                         serverSocket.Bind(serverEndpoint);
                                         EndPoint client = new IPEndPoint(IPAddress.Any, 0);
                                         var dataFromRemote = new byte[512];

                                         _touchCommandHandler = new TouchCommandHandler();
                                         _touchCommandHandler.WhoAmIEvent += (o, e) =>
                                                                                 {
                                                                                     serverSocket.SendTo(TouchCommandHandler.WrapLogicData(TouchCommandType.WhoAmIFromServer, new byte[0]), client);
                                                                                 };
                                         _touchCommandHandler.DeviceConnected += (o, e) =>
                                                                                 {
                                                                                     _currentClient = e.DevicEndPoint;
                                                                                 };
                                         _touchCommandHandler.DeviceDisconnected += (o, e) =>
                                                                                    {
                                                                                        _currentClient = null;
                                                                                    };
                                         while (!_isServiceShutDown)
                                         {
                                             try
                                             {
                                                 var receiveBytesCount = 0;
                                                 var endPoint = client;//TODO 分辨不同的客户端

                                                 receiveBytesCount = serverSocket.ReceiveFrom(dataFromRemote, ref endPoint);
                                                 client = endPoint;
                                                 string data="";
                                                 int index = 0;
                                                 dataFromRemote.ToList().ForEach(item =>
                                                                                 {
                                                                                     if (index<receiveBytesCount)
                                                                                     {
                                                                                         data += item.ToString("X") + " ";
                                                                                         index++;
                                                                                     }
                                                                                     
                                                                                 });
                                                 Debug.WriteLine("Receive data: " + data+"  " + DateTime.Now.ToString("hh:mm:ss,fff"));
                                                 _touchCommandHandler.HandleRawDatagram(dataFromRemote, endPoint);
                                             }
                                             catch (Exception ex)
                                             {
                                                 Debug.WriteLine(ex);
                                             }
                                         }
                                     });
                _serviceThread.IsBackground = true;
                _serviceThread.Start();
                startResult(true);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                startResult(false);
            }
        }

        public void Stop()
        {
            _isServiceShutDown = true;
            _serviceThread.Abort();
        }

        private int _serverPort = 4321;
        public int ServerPort
        {
            get { return _serverPort; }
        }
    }
}

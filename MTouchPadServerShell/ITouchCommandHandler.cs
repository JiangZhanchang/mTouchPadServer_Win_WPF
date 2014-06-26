using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MTouchPadServerShell
{
    interface ITouchCommandHandler
    {
        byte[] CommandHeader { get; }
        void HandleRawDatagram(byte[] rBytes, EndPoint endPoint);
        event EventHandler WhoAmIEvent;
        event EventHandler<DeviceConnectStateEventArgs> DeviceConnected;
        event EventHandler<DeviceConnectStateEventArgs> DeviceDisconnected;
    }

    class DeviceConnectStateEventArgs : EventArgs
    {
        public EndPoint DevicEndPoint { get; private set; }

        public DeviceConnectStateEventArgs(EndPoint devicEndPoint)
        {
            DevicEndPoint = devicEndPoint;
        }
    }
}

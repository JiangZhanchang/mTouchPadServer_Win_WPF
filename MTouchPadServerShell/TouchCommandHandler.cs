using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Cursor = System.Windows.Forms.Cursor;

namespace MTouchPadServerShell
{
    class TouchCommandHandler : ITouchCommandHandler
    {
        private readonly byte[] _commandHeader = { 0xEC, 0xEF };
        public byte[] CommandHeader { get { return _commandHeader; } }
        public event EventHandler WhoAmIEvent;

        public event EventHandler<DeviceConnectStateEventArgs> DeviceConnected;
        public event EventHandler<DeviceConnectStateEventArgs> DeviceDisconnected;

        public void HandleRawDatagram(byte[] rBytes, EndPoint endPoint)
        {
            if (rBytes.Length < _commandHeader.Length + 1 || rBytes[0] != _commandHeader[0] || rBytes[1] != _commandHeader[1])
                return;
            int dataIndex = CommandHeader.Length;
            var commandType = (TouchCommandType)rBytes[dataIndex];
            //var ms = new MemoryStream(rBytes, CommandHeader.Length, rBytes.Count() - CommandHeader.Length);
            //var bsReader = new BsonReader(ms);
            //var bsSerailizer = new JsonSerializer();

            switch (commandType)
            {
                case TouchCommandType.WhoAmIFromClient:
                    if (WhoAmIEvent != null)
                        WhoAmIEvent(this, new EventArgs());
                    break;

                case TouchCommandType.WhoAmIFromServer:
                    break;

                case TouchCommandType.Connect:
                    if (DeviceConnected != null)
                        DeviceConnected(this, new DeviceConnectStateEventArgs(endPoint));
                    break;

                case TouchCommandType.Disconnect:
                    if (DeviceDisconnected != null)
                        DeviceDisconnected(this, new DeviceConnectStateEventArgs(endPoint));
                    break;

                case TouchCommandType.MouseLeftButtonDown:
                    HandleMouseLeftButtonDown();
                    break;

                case TouchCommandType.MouseLeftButtonUp:
                    HandleMouseLeftButtonUp();
                    break;
                case TouchCommandType.MouseRightButtonDown:
                    HandleMouseRightButtonDown();
                    break;

                case TouchCommandType.MouseRightButtonUp:
                    HandleMouseRightButtonUp();
                    break;

                case TouchCommandType.MouseLeftButtonClick:
                    HandleMouseLeftButtonDown();
                    HandleMouseLeftButtonUp();
                    break;

                case TouchCommandType.MouseRightButtonClick:
                    HandleMouseRightButtonDown();
                    HandleMouseRightButtonUp();
                    break;

                case TouchCommandType.MouseMove:
                    //var mouseMoveData = bsSerailizer.Deserialize<IMouseMove>(bsReader);
                    //if (mouseMoveData == null)
                    //    break;

                    dataIndex++;
                    var x = BitConverter.ToSingle(rBytes, dataIndex);
                    dataIndex += 4;
                    var y = BitConverter.ToSingle(rBytes, dataIndex);
                    Debug.WriteLine("Move:{0},{1}",x.ToString("F3"),y.ToString("F3"));
                    HandleMouseMove((int)x,(int) y);
                    break;
                case TouchCommandType.MouseDrag:
                    //var mouseDragData = bsSerailizer.Deserialize<MouseMove>(bsReader);
                    //if (mouseDragData == null)
                    //    break;

                     dataIndex++;
                    var dragX = BitConverter.ToInt32(rBytes, dataIndex);
                    dataIndex += 4;
                    var dragY = BitConverter.ToInt32(rBytes, dataIndex);
                    HandleMouseDrag(dragX, dragY);
                    break;
            }
        }

        void HandleMouseLeftButtonDown()
        {
            MouseOperate.MouseButtonLeftDown();
        }

        void HandleMouseLeftButtonUp()
        {
            MouseOperate.MouseButtonLeftUp();
        }

        void HandleMouseRightButtonDown()
        {
            MouseOperate.MouseButtonRightDown();
        }

        void HandleMouseRightButtonUp()
        {
            MouseOperate.MouseButtonRightUp();
        }

        void HandleMouseMove(int directionX, int directionY)
        {
            MouseOperate.MouseMove(directionX, directionY);
        }

        void HandleMouseDrag(int directionX, int directionY)
        {

        }

        public static byte[] WrapLogicData(TouchCommandType commandType, byte[] rawData)
        {
            var finalBytes = new List<byte>();
            finalBytes.AddRange(new TouchCommandHandler().CommandHeader);
            finalBytes.Add((byte)commandType);
            finalBytes.AddRange(rawData);
            return finalBytes.ToArray();
        }
    }
}

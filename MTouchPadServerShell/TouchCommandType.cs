using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTouchPadServerShell
{
    enum TouchCommandType
    {
        WhoAmIFromClient = 0xEE,
        WhoAmIFromServer = 0xEF,
        Connect = 0xED,
        Disconnect = 0xEC,
        MouseLeftButtonDown = 0x01,
        MouseLeftButtonUp = 0x02,
        MouseRightButtonDown = 0x03,
        MouseRightButtonUp = 0x04,
        MouseMove = 0x05,
        MouseDrag = 0x06,
        MouseLeftButtonClick=0x07,
        MouseRightButtonClick=0x08,
    }
}

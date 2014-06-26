using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MTouchPadServerShell
{
    static class MouseOperate
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        // ReSharper disable InconsistentNaming
        private const int MOUSEEVENT_LEFTDOWN = 0x02;

        private const int MOUSEEVENT_LEFTUP = 0x04;
        private const int MOUSEEVENT_RIGHTDOWN = 0x08;
        private const int MOUSEEVENT_RIGHTUP = 0x10;
        // ReSharper restore InconsistentNaming

        public static void MouseButtonLeftDown()
        {
            mouse_event(SystemInformation.MouseButtonsSwapped ? MOUSEEVENT_RIGHTDOWN : MOUSEEVENT_LEFTDOWN,
                        Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        public static void MouseButtonLeftUp()
        {
            mouse_event(SystemInformation.MouseButtonsSwapped ? MOUSEEVENT_RIGHTUP : MOUSEEVENT_LEFTUP, Cursor.Position.X,
                        Cursor.Position.Y, 0, 0);
        }

        public static void MouseButtonRightDown()
        {
            mouse_event(SystemInformation.MouseButtonsSwapped ? MOUSEEVENT_LEFTDOWN : MOUSEEVENT_RIGHTDOWN,
                        Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        public static void MouseButtonRightUp()
        {
            mouse_event(SystemInformation.MouseButtonsSwapped ? MOUSEEVENT_LEFTUP : MOUSEEVENT_RIGHTUP, Cursor.Position.X,
                        Cursor.Position.Y, 0, 0);
        }

        public static void MouseMove(int x, int y)
        {
            Cursor.Position.Offset(x, y);
        }

        public static void MouseDrag(int x, int y)
        {
            
        }
    }
}

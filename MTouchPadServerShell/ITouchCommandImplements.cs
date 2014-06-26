using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTouchPadServerShell
{
    class MouseLeftButtonDown : IMouseLeftButtonDown
    {

    }

    class MouseLeftButtonUp : IMouseLeftButtonUp
    {

    }

    class MouseRightButtonDown : IMouseLeftButtonDown
    {

    }

    class MouseRightButtonUp : IMouseLeftButtonUp
    {

    }

    class MouseMove : IMouseMove
    {
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
    }

    class MouseDrag : IMouseDrag
    {
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
    }
}

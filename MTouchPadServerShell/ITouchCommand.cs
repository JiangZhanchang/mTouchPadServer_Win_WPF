using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTouchPadServerShell
{
    interface IMouseLeftButtonDown
    {

    }

    interface IMouseLeftButtonUp
    {

    }

    interface IMouseRightButtonDown
    {

    }

    interface IMouseRightButtonUp
    {

    }

    interface IMouseMove
    {
        int DirectionX { get; set; }
        int DirectionY { get; set; }
    }

    interface IMouseDrag : IMouseMove
    {

    }
}

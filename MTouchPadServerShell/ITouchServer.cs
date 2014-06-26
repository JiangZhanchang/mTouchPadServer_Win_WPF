using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTouchPadServerShell
{
    interface ITouchServer
    {
        int ServerPort { get; }
        void Start(Action<bool> startResult);
        void Stop();

    }
}

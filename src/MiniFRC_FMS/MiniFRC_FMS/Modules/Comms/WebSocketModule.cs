﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms
{
    [ModuleInitPriority(3)]
    internal class WebSocketModule : BaseModule
    {
        protected override bool Init()
        {
            return true;
        }
    }
}

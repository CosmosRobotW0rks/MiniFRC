using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.Models
{
    internal enum TeamColor : byte
    {
        NONE,
        RED,
        BLUE
    }

    internal enum DeviceType : byte
    {
        NONE = 0,
        Speaker,
        Amp,
        Source,
        Trap,
        DriverStation,
        Stage,
        Fan
    }

    internal enum AuDisPage : byte
    {
        NONE = 0,
        Match,
        AfterMatch,
        Scores,
        CalculatingPoints

    }
}

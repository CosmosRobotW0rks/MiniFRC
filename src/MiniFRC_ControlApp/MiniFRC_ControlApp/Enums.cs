using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_ControlApp
{
    internal enum TeamColor : byte
    {
        NONE = 0,
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


    internal enum PointSource
    {
        NONE = 0,
        Speaker,
        Amp,
        Stage,
        Trap,
        Penalty
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

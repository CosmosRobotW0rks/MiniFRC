using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.Models
{
    internal enum PointSource
    {
        Other = 0,
        Speaker
    }

    internal class Point
    {
        public PointSource PointSource { get; private set; }
        public int Points { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Point(PointSource pointSource, int points)
        {
            PointSource = pointSource;
            Points = points;
            Timestamp = DateTime.Now;
        }
    }
}

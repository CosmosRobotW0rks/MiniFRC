using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.Models
{
    internal enum PointSource
    {
        NONE = 0,
        Speaker,
        Amp,
        Stage,
        Trap,
        Penalty
    }




    internal class PointCollection
    {
        internal class Point
        {
            public PointSource PointSource { get; private set; }
            public int Points { get; private set; }
            public DateTime Timestamp { get; private set; }
            public int PointID { get; private set; }

            public Point(PointSource pointSource, int points, int pointID)
            {
                PointSource = pointSource;
                Points = points;
                Timestamp = DateTime.Now;
                PointID = pointID;
            }
        }

        public event EventHandler<Point>? PointAdded;
        public event EventHandler<int>? PointRemoved;

        private List<Point> Points = new List<Point>();
        public int PointsSum => Points.Select(x => x.Points).Sum();
        int lastPointID = 0;


        public int GetPointsSumOfPointSource(PointSource source)
        {
            return Points.Where(x => x.PointSource == source).Select(x => x.Points).Sum();
        }

        public int AddPoint(PointSource pointSource, int points)
        {
            Point p = new Point(pointSource, points, lastPointID++);
            Points.Add(p);
            PointAdded?.Invoke(this, p);
            return p.PointID;
        }

        public bool DeletePointByID(int pointID)
        {
            var point = Points.Find(x => x.PointID == pointID);
            if (point == null) return false;
            bool s = Points.Remove(point);
            if(s) PointRemoved?.Invoke(this, pointID);
            return s;
        }

        public void DeletePoints(Func<Point, bool> condition)
        {
            Points.Where(condition).ToList().ForEach(x => DeletePointByID(x.PointID));
        }

        public int[] GetAftermatchPointArray()
        {
            int speakerPoints = GetPointsSumOfPointSource(PointSource.Speaker);
            int ampPoints = GetPointsSumOfPointSource(PointSource.Amp);
            int stagePoints = GetPointsSumOfPointSource(PointSource.Stage);
            int trapPoints = GetPointsSumOfPointSource(PointSource.Trap);
            int penalty = GetPointsSumOfPointSource(PointSource.Penalty);

            return [ speakerPoints, ampPoints, stagePoints, trapPoints, penalty ];
        }
    }
}
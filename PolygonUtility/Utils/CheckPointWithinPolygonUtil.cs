using System;
using PolygonUtility.Models;

namespace PolygonUtility.Utils
{
	public class CheckPointWithinPolygonUtil
	{
        
		public CheckPointWithinPolygonUtil()
		{
		}

        public bool IsPointInPolygon(Point p, List<LineSegment> lines)
        {


            bool inside = false;
            foreach(var line in lines)
            {
                if ((line.Start.Y > p.Y) != (line.End.Y > p.Y) &&
                     p.X < (line.End.X - line.Start.X) * (p.Y - line.Start.Y) / (line.End.Y - line.Start.Y) + line.Start.X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        public bool IsPointInPolygon(Point p, List<Point> polygon)
        {

            bool inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                bool r = (polygon[i].Y > p.Y) != (polygon[j].Y > p.Y);
                if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                     p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}


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
                float startX = line.Start.X; float startY = line.Start.Y;
                float endX = line.End.X; float endY = line.End.Y;
                float testX = p.X; float testY = p.Y;
                if ((line.Start.Y > p.Y) != (line.End.Y > p.Y) &&
                     testX < (endX- startX) * (testY - startY) / (endY- startY) + startX)
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


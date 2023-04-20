using System;
using PolygonUtility.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PolygonUtility.Utils
{
    public class CheckPointWithinPolygonUtil
    {
        public bool IsPointInPolygon(Point p, List<Point> points)
        {
            bool between(float p, float a, float b) => p >= a && p <= b || p <= a && p >= b;
            bool inside = false;
            for (int i = 0, j = points.Count - 1; i < points.Count; j = i++)
            {
                if (points[i].Equals(p) || points[j].Equals(p)) return false;
                float startX = points[j].X; float startY = points[j].Y;
                float endX = points[i].X; float endY = points[i].Y;
                float testX = p.X; float testY = p.Y;
                if (startY == endY && endY == testY && between(testX, startX, endX)) return false;
                if ((endY > testY != (startY > testY) &&
                     testX < (endX - startX) * (testY - startY) / (endY - startY) + startX))
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }

        
}
        //public bool IsPointInPolygon(Point p, List<Point> points)
        //{
        //    bool inside = false;
        //    for (int i = 0, j = points.Count - 1; i < points.Count; j = i++)
        //    {
        //        float startX = line.Start.X; float startY = line.Start.Y;
        //        float endX = line.End.X; float endY = line.End.Y;
        //        float testX = p.X; float testY = p.Y;
        //        if ((points[i].Y > p.Y) != (points[j].Y > p.Y) &&
        //             p.X < (points[j].X - points[i].X) * (p.Y - points[i].Y) / (points[j].Y - points[i].Y) + points[i].X)
        //        {
        //            inside = !inside;
        //        }
        //    }

        //    return inside;
        //}


using System;
using PolygonUtility.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PolygonUtility.Utils
{
	public class IntersectionCheckUtil
	{

        public Event DoLinesIntersect(LineSegment line1, LineSegment line2)
        {
            return CheckIntersect(line1.Start, line1.End, line2.Start, line2.End);
        }

        public Event DoLinesIntersect(Point p1, Point p2, Point p3, Point p4)
        {
            return CheckIntersect(p1, p2, p3, p4);
        }

        private Event CheckIntersect(Point p1, Point p2, Point p3, Point p4)
        {
            int d1 = Direction(p3, p4, p1);
            int d2 = Direction(p3, p4, p2);
            int d3 = Direction(p1, p2, p3);
            int d4 = Direction(p1, p2, p4);

            if (d1 != d2 && d3 != d4) return FindIntersectionPoint(p1,p2,p3,p4);

            Event result = null;
            if (d1 == 0 && IsOnSegment(p3, p4, p1, out result))
            {
                return result;
            }

            if (d2 == 0 && IsOnSegment(p3, p4, p2, out result))
            {
                return result;
            }

            if (d3 == 0 && IsOnSegment(p1, p2, p3, out result))
            {
                return result;
            }

            if (d4 == 0 && IsOnSegment(p1, p2, p4, out result))
            {
                return result;
            }

            return result;
        }


        public static int Direction(Point p, Point q, Point r)
        {
            float val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0; // collinear

            return val > 0 ? 1 : 2; // clock or counterclock wise
        }

        public bool IsOnSegment(Point p1, Point p2, Point q, out Event? intersectionPoint)
        {
            intersectionPoint = null;
            if (q.X <= Math.Max(p1.X, p2.X) && q.X >= Math.Min(p1.X, p2.X) &&
                q.Y <= Math.Max(p1.Y, p2.Y) && q.Y >= Math.Min(p1.Y, p2.Y))
            {
                intersectionPoint = new Event(q, PointType.INTERSECTION, -1);
                return true;
            }
            return false;
        }

        public static Event FindIntersectionPoint(Point p1, Point p2, Point p3, Point p4)
        {
            bool line1IsVertical = IsVerticalLine(p1, p2);
            bool line2IsVertical = IsVerticalLine(p3, p4);
            if (!line1IsVertical && !line2IsVertical)
            {
                float g1 = FindGradient(p1, p2);
                float g2 = FindGradient(p3, p4);
                float c1 = p1.Y - (g1 * p1.X);
                float c2 = p3.Y - (g2 * p3.X);
                float x = (c2 - c1) / (g1 - g2);
                float y = g1 * x + c1;
                return new Event(new Point(x, y, -1), PointType.INTERSECTION, -1);
            }
            else if (line1IsVertical)
            {
                float g = FindGradient(p3, p4);
                float c = p3.Y - (g * p3.X);
                float x = p1.X;
                float y = g * x + c;
                return new Event(new Point(x, y, -1), PointType.INTERSECTION, -1);

            }
            else
            {
                float g = FindGradient(p1, p2);
                float c = p1.Y - (g * p1.X);
                float x = p3.X;
                float y = g * x + c;
                return new Event(new Point(x, y, -1), PointType.INTERSECTION, -1);
            }
        }

        public static float FindGradient(Point p1, Point p2)
        {
            return (p1.Y - p2.Y) / (p1.X - p2.X);
        }

        public static bool IsVerticalLine(Point p1, Point p2)
        {
            return p1.X == p2.X;
        }

    }
}


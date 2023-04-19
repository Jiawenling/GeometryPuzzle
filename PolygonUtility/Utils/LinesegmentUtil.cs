using System;
using PolygonUtility.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PolygonUtility.Utils
{
	public class LinesegmentUtil
	{

        public bool DoLinesIntersect(LineSegment line1, LineSegment line2)
        {
            return CheckIntersect(line1.Start, line1.End, line2.Start, line2.End);
        }

        public bool DoLinesIntersect(Point p1, Point p2, Point p3, Point p4)
        {
            return CheckIntersect(p1, p2, p3, p4);
        }

        private bool CheckIntersect(Point p1, Point p2, Point p3, Point p4)
        {
            int d1 = GetDirection(p3, p4, p1);
            int d2 = GetDirection(p3, p4, p2);
            int d3 = GetDirection(p1, p2, p3);
            int d4 = GetDirection(p1, p2, p4);

            if (d1 != d2 && d3 != d4) return true;
            //return FindIntersectionPoint(p1, p2, p3, p4);
            Event result = null;
            if (d1 == 0 && IsOnSegment(p3, p4, p1))
            {
                return true;
            }

            if (d2 == 0 && IsOnSegment(p3, p4, p2))
            {
                return true;
            }

            if (d3 == 0 && IsOnSegment(p1, p2, p3))
            {
                return true;
            }

            if (d4 == 0 && IsOnSegment(p1, p2, p4))
            {
                return true;
            }

            return false;
        }

        public bool CheckIfTwoLinesAreVerticesOfPolygon(LineSegment line1, LineSegment line2)
        {
            return IsVerticeWithSameEndPoints(line1, line2) ||
                IsVerticeWithSameStartPoints(line1, line2) ||
                AreAdjacentVertices(line1, line2) ||
                AreAdjacentVertices(line2, line1);
        }

        private bool IsVerticeWithSameStartPoints(LineSegment line1, LineSegment line2)
        {
            return line1.Start.Equals(line2.Start) &&
                (!IsOnSegment(line1.Start, line1.End, line2.End) &&
                !IsOnSegment(line2.Start, line2.End, line1.End));
        }

        private bool IsVerticeWithSameEndPoints(LineSegment line1, LineSegment line2)
        {
            return line1.End.Equals(line2.End) &&
                (!IsOnSegment(line1.Start, line1.End, line2.Start) &&
                !IsOnSegment(line2.Start, line2.End, line1.Start));
        }

        private bool AreAdjacentVertices(LineSegment line1, LineSegment line2)
        {
            return line1.Start.Equals(line2.End) &&
                (!IsOnSegment(line1.Start, line1.End, line2.Start)) &&
                !IsOnSegment(line2.Start, line2.End, line1.End);
        }

        public int GetDirection(Point a, Point b, Point p)
        {
            //float val = (q.Y - p.Y) * (r.X - q.X) -
            //        (q.X - p.X) * (r.Y - q.Y);
            float val = (b.X - a.X) * (p.Y - a.Y) - (b.Y - a.Y) * (p.X - a.X);
            if (val == 0) return 0; 

            return val > 0 ? 1 : 2; //+ve p is left
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

        public bool IsOnSegment(Point p1, Point p2, Point q)
        {
            if (q.X <= Math.Max(p1.X, p2.X) && q.X >= Math.Min(p1.X, p2.X) &&
                q.Y <= Math.Max(p1.Y, p2.Y) && q.Y >= Math.Min(p1.Y, p2.Y))
                return true;
            return false;
        }

        public Event FindIntersectionPoint(Point p1, Point p2, Point p3, Point p4)
        {
            bool line1IsVertical = IsVerticalLine(p1, p2);
            bool line2IsVertical = IsVerticalLine(p3, p4);
            if (!line1IsVertical && !line2IsVertical)
            {
                float g1 = FindGradient(p1, p2);
                float g2 = FindGradient(p3, p4);
                float c1 = FindXIntercept(p1, g1);
                float c2 = FindXIntercept(p3, g2);
                int x = (int)Math.Round((c2 - c1) / (g1 - g2));
                int y = (int)(g1 * x + c1);
                return new Event(new Point(x, y, -1), PointType.INTERSECTION, -1);
            }
            else if (line1IsVertical)
            {
                float g = FindGradient(p3, p4);
                float c = FindXIntercept(p3, g);
                int x = p1.X;
                int y = (int)Math.Round(g * x + c);
                return new Event(new Point(x, y, -1), PointType.INTERSECTION, -1);

            }
            else
            {
                float g = FindGradient(p1, p2);
                float c = FindXIntercept(p1, g);
                int x = p3.X;
                int y = (int)Math.Round(g * x + c);
                return new Event(new Point(x, y, -1), PointType.INTERSECTION, -1);
            }
        }

        public float FindXIntercept(Point p1, float gradient)
        {
            return p1.Y - (gradient * p1.X);
        }

        public float FindGradient(Point p1, Point p2)
        {
            return (float)(p1.Y - p2.Y) / (float)(p1.X - p2.X);
        }

        public bool IsVerticalLine(Point p1, Point p2)
        {
            return p1.X == p2.X;
        }

        public bool PointIsAboveLineSegment(Point p, float gradient, float xIntercept)
        {
            return p.Y > (gradient * p.X + xIntercept);
        }

    }
}


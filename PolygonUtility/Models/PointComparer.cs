using System;
namespace PolygonUtility.Models
{
	public class PointComparer: Comparer<Point>
	{
        public override int Compare(Point? a, Point? b)
        {
            if (a.X.CompareTo(b.X) != 0) return a.X.CompareTo(b.X);
            if (a.Y.CompareTo(b.Y) != 0) return a.Y.CompareTo(b.Y);
            if (a.CurrentPointNo.CompareTo(b.CurrentPointNo) != 0)
                return a.CurrentPointNo.CompareTo(b.CurrentPointNo);
            return 0;
        }
    }
}


using System;
namespace PolygonUtility.Models
{
	public class LineSegmentComparer: IComparer<LineSegment>
	{
        public int Compare(LineSegment A, LineSegment B)
        {
            Point a = A.Start;
            Point b = B.Start;
            if (a.Y.CompareTo(b.Y) != 0) return a.Y.CompareTo(b.Y);
            if (a.CurrentPointNo.CompareTo(b.CurrentPointNo) != 0)
                return a.CurrentPointNo.CompareTo(b.CurrentPointNo);
            return 0;
        }
    }
}


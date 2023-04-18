using System;
namespace PolygonUtility.Models
{
	public class LineSegmentComparer: Comparer<LineSegment>
	{
        public override int Compare(LineSegment A, LineSegment B)
        {
            Point a = A.Start;
            Point b = B.Start;
            if (a.Y.CompareTo(b.Y) != 0) return a.Y.CompareTo(b.Y);
            if (A.CurrentLineNo.CompareTo(B.CurrentLineNo) != 0)
                return A.CurrentLineNo.CompareTo(B.CurrentLineNo);
            return 0;
        }
    }
}


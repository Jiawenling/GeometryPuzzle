using System;
namespace PolygonUtility.Models
{
	public class LineSegment
	{
		public Point Start;
		public Point End;
		public int CurrentLineNo;
		public LineSegment(Point start, Point end, int currentLineNo)
		{
			Start = start;
			End = end;
			CurrentLineNo = currentLineNo;
		}
	}
}


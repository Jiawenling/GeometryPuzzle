using System;
namespace PolygonUtility.Models
{
	public class LineSegment
	{
		public Point Start;
		public Point End;
		public int CurrentLineNo;
		public LineSegment(Point p1, Point p2, int currentLineNo)
		{
			if(p1.X < p2.X)
			{
				Start = p1;
				End = p2;
			}
			else if (p1.X > p2.X)
			{
				Start = p2;
				End = p1;
			}
			else if (p1.Y < p2.Y)
			{
                Start = p1;
                End = p2;
            }
			else
			{
                Start = p2;
                End = p1;
            }
			CurrentLineNo = currentLineNo;
		}

        public bool Equals(LineSegment linesegment)
		{
			return this.CurrentLineNo == linesegment.CurrentLineNo;
		}

        public override bool Equals(object? obj)
        {
			var o = (obj as LineSegment);
            return this.CurrentLineNo == o.CurrentLineNo;
        }

        public override int GetHashCode()
        {
			return CurrentLineNo.GetHashCode();
        }

    }
}


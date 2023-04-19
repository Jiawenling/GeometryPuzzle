using System;
namespace PolygonUtility.Models
{
	public class Point
	{
		public int X { get; set; }
        public int Y { get; set; }
		public int CurrentPointNo { get; set;  }

        public Point(int x, int y, int currentCoordinate)
		{
			X = x;
			Y = y;
			CurrentPointNo = currentCoordinate;
		}

        public override string? ToString()
        {
			return $"({(int)this.X},{(int)this.Y})";
		}
    }

	public enum PointType
	{
		START, END, INTERSECTION
	}
}


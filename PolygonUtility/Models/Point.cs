using System;
namespace PolygonUtility.Models
{
	public class Point
	{
		public float X { get; set; }
        public float Y { get; set; }
		public int CurrentPointNo { get; set;  }

        public Point(float x, float y, int currentCoordinate)
		{
			X = x;
			Y = y;
			CurrentPointNo = currentCoordinate;
		}

    }

	public enum PointType
	{
		START, END, INTERSECTION
	}
}


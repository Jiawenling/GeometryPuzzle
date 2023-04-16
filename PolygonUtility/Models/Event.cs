using System;
namespace PolygonUtility.Models
{
	public class Event
	{
		public Point Point;
		public PointType PointType;
		public int LineSegmentIndex;

        public Event(Point point, PointType pointType, int lineSegmentIndex)
        {
            Point = point;
            PointType = pointType;
            LineSegmentIndex = lineSegmentIndex;
        }
    }
}


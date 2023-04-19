using System;
using PolygonUtility.Models;

namespace PolygonUtility.Utils
{
	public static class PointsToLineSegmentUtil
	{
		public static List<LineSegment> ConvertToPolygonVertices(List<Point> points)
		{
			var lines = new List<LineSegment>();
            int count = points.Count;
            for (int i = 0; i < points.Count - 1; i++)
            {
                lines.Add(new LineSegment(points[i], points[i + 1], i));
            }
            //last line segment connecting last and first point
            var newLine = new LineSegment(points[count - 1], points[0], count - 1);
            lines.Add(newLine);
            return lines;

        }
    }
}


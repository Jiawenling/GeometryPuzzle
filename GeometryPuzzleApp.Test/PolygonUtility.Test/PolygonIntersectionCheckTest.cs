using System;
using FluentAssertions;
using Moq;
using PolygonUtility;
using PolygonUtility.Models;
using PolygonUtility.PolygonIntersectionCheckUtility;
using PolygonUtility.Utils;

namespace GeometryPuzzleApp.Test.PolygonUtility.Test
{
	public class PolygonIntersectionCheckTest
	{

        [Fact]
        public void PolygonIntersectionCheckUtility_IsNewLineIntersecting_returnsFalseOnNonIntersectingLines()
        {
            var points = new List<Point>()
            {
                new Point(1, 1, 1),
                new Point(5, 1, 2),
                new Point(5, 5, 3),

            };
            var lines = new List<LineSegment>();
            int count = points.Count;
            for (int i = 0; i < points.Count - 1; i++)
            {
                lines.Add(new LineSegment(points[i], points[i + 1], i));
            }
            var newLine = new LineSegment(points[count - 1], new Point(6, 3, 4), count - 1);
            var polygonUtil = new PolygonIntersectionCheckUtil();

            var result = polygonUtil.IsNewLineIntersecting(lines, newLine);

            result.Should().BeFalse();
        }

        [Fact]
        public void PolygonIntersectionCheckUtility_IsNewLineIntersecting_returnsTrueOnIntersectingLines()
        {
            var points = new List<Point>()
            {
                new Point(1, 1, 1),
                new Point(5, 1, 2),
                new Point(5, 5, 3),
  
            };

            var lines = PointsToLineSegmentUtil.ConvertToPolygonVertices(points);
            var count = lines.Count;
            var newLine = new LineSegment(points[count - 1], new Point(4, 0, 4), count - 1);
            var polygonUtil = new PolygonIntersectionCheckUtil();
            var result = polygonUtil.IsNewLineIntersecting(lines, newLine);

            result.Should().BeTrue();
        }

        [Fact]
        public void PolygonIntersectionCheckUtility_IsNewLineIntersecting_returnsTrueOnIntersectingPolygon()
        {
            var points = new List<Point>()
            {
                new Point(8, 3, 1),
                new Point(5, 6, 2),
                new Point(2, 1, 3),
                new Point(3, 5, 4),
                new Point(7, 6, 5),
            };

            var polygonUtil = new PolygonIntersectionCheckUtil();
            var result = polygonUtil.IsPolygonSelfIntersecting(points);

            result.Should().BeTrue();
        }


        [Fact]
		public void PolygonIntersectionCheckUtility_IsNewLineIntersecting_returnsFalseOnValidPolygon()
		{
            var points = new List<Point>()
            {
                new Point(1, 6, 1),
                new Point(6, 6, 2),
                new Point(6, 2, 3),
                new Point(4, 5, 4),
                new Point(2, 1, 5),
            };

            var polygonUtil = new PolygonIntersectionCheckUtil();
            var result = polygonUtil.IsPolygonSelfIntersecting(points);

            result.Should().BeFalse();
		}

    }
}


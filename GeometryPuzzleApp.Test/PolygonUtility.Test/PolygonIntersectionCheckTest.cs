using System;
using FluentAssertions;
using PolygonUtility.Models;
using PolygonUtility.PolygonIntersectionCheckUtility;

namespace GeometryPuzzleApp.Test.PolygonUtility.Test
{
	public class PolygonIntersectionCheckTest
	{
        private List<Point> points;
        private List<LineSegment> lines;
        private LineSegment newLine;

        [Fact]
        public void PolygonIntersectionCheckUtility_IsNewLineIntersecting_returnsFalseOnNonIntersectingLines()
        {
            points = new List<Point>()
            {
                new Point(1, 1, 1),
                new Point(5, 1, 2),
                new Point(5, 5, 3),

            };
            lines = new List<LineSegment>();
            int count = points.Count;
            for (int i = 0; i < points.Count - 1; i++)
            {
                lines.Add(new LineSegment(points[i], points[i + 1], i));
            }
            newLine = new LineSegment(points[count - 1], new Point(6, 3, 4), count - 1);

            var polygonUtil = new PolygonIntersectionCheckUtil(lines, newLine);

            var result = polygonUtil.IsNewLineIntersecting();

            result.Should().BeFalse();
        }

        [Fact]
        public void PolygonIntersectionCheckUtility_IsNewLineIntersecting_returnsTrueOnIntersectingLines()
        {
            points = new List<Point>()
            {
                new Point(1, 1, 1),
                new Point(5, 1, 2),
                new Point(5, 5, 3),
  
            };
            lines = new List<LineSegment>();
            int count = points.Count;
            for (int i = 0; i < points.Count - 1; i++)
            {
                lines.Add(new LineSegment(points[i], points[i + 1], i));
            }
            newLine = new LineSegment(points[count - 1], new Point(4,0,4), count - 1);

            var polygonUtil = new PolygonIntersectionCheckUtil(lines, newLine);

            var result = polygonUtil.IsNewLineIntersecting();

            result.Should().BeTrue();
        }

        [Fact]
        public void PolygonIntersectionCheckUtility_IsNewLineIntersecting_returnsTrueOnIntersectingPolygon()
        {
            points = new List<Point>()
            {
                new Point(8, 3, 1),
                new Point(5, 6, 2),
                new Point(2, 1, 3),
                new Point(3, 5, 4),
                new Point(7, 6, 5),
            };
            lines = new List<LineSegment>();
            int count = points.Count;
            for (int i = 0; i < points.Count - 1; i++)
            {
                lines.Add(new LineSegment(points[i], points[i + 1], i));
            }
            newLine = new LineSegment(points[count - 1], points[0], count - 1);
            //lines.Add(newLine);

            var polygonUtil = new PolygonIntersectionCheckUtil(lines, newLine);

            var result = polygonUtil.IsNewLineIntersecting();

            result.Should().BeTrue();
        }


        [Fact]
		public void PolygonIntersectionCheckUtility_IsNewLineIntersecting_returnsFalseOnValidPolygon()
		{
            points = new List<Point>()
            {
                new Point(1, 6, 1),
                new Point(6, 6, 2),
                new Point(6, 2, 3),
                new Point(4, 5, 4),
                new Point(2, 1, 5),
            };
            lines = new List<LineSegment>();
            int count = points.Count;
            for (int i = 0; i < points.Count-1; i++)
            {
                lines.Add(new LineSegment(points[i], points[i + 1], i));
            }
            newLine = new LineSegment(points[count - 1], points[0], count - 1);
            //lines.Add(newLine);

            var polygonUtil = new PolygonIntersectionCheckUtil(lines, newLine);

			var result = polygonUtil.IsNewLineIntersecting();

			result.Should().BeFalse();
		}

    }
}


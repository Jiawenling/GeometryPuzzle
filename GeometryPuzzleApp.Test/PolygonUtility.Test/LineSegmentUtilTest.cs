using System;
using FluentAssertions;
using PolygonUtility.Models;
using PolygonUtility.Utils;

namespace GeometryPuzzleApp.Test.PolygonUtility.Test
{
	public class LineSegmentUtilTest
	{
		private LinesegmentUtil _util;
		public LineSegmentUtilTest()
		{
			_util = new LinesegmentUtil();
		}

		[Fact]
		public void IntersectionCheckUtil_IsOnSegment_ReturnsTrueForColinearPoints()
		{
			Point p1 = new Point(-2, 1, -1);
			Point p2 = new Point(2, 5, -1);
			Point p3 = new Point(1, 4, -1);
			var result = _util.IsOnSegment(p1, p2, p3, out Event intersectionPoint);
			result.Should().BeTrue();
			intersectionPoint.Should().NotBeNull();
			intersectionPoint.Point.X.Should().Be(p3.X);
		}

        [Fact]
        public void IntersectionCheckUtil_GetIntersection_ReturnsIntersectionEventOnPolygonVertices()
        {
            Point p1 = new Point(-4, 6, -1); //line y = -x+1
            Point p2 = new Point(2, 6, -1);
            Point p3 = new Point(4, 5, -1); //line y = x+ 3
			LineSegment l1 = new LineSegment(p1, p2, -1);
			LineSegment l2 = new LineSegment(p2, p3, -1);
			Point expected = new Point(2, 6, -1);

			var result = _util.DoLinesIntersect(l1,l2);
			result.Should().BeOfType<Event>();
			Assert.Equivalent(expected, result.Point);
        }

		[Fact]
        public void IntersectionCheckUtil_GetIntersection_ReturnsIntersectionEvent()
        {
            Point p1 = new Point(-2, 3, -1); //line y = -x+1
            Point p2 = new Point(2, -1, -1);
            Point p3 = new Point(2, 5, -1); //line y = x+ 3
            Point p4 = new Point(-3, 0, -1);
            Point expected = new Point(-1, 2, -1);

            var result = _util.DoLinesIntersect(p1, p2, p3, p4);
            result.Should().BeOfType<Event>();
            Assert.Equivalent(expected, result.Point);
        }

        [Fact]
        public void IntersectionCheckUtil_GetIntersection_ReturnsNullEventOnNoIntersection()
        {
            Point p1 = new Point(-4, 6, -1); //line y = -x+1
            Point p2 = new Point(2, 6, -1);
            Point p3 = new Point(4, 5, -1); //line y = x+ 3
            Point p4 = new Point(5, 4, -1);
            Point expected = new Point(-1, 2, -1);

            var result = _util.DoLinesIntersect(p1, p2, p3, p4);
            result.Should().BeNull();
        }

        [Fact]
        public void IntersectionCheckUtil_GetIntersection_ReturnsEventIntersectionWithVerticalLine()
        {
            Point p1 = new Point(-4, 6, -1); //line y = -x+1
            Point p2 = new Point(2, 6, -1);
            Point p3 = new Point(2, 6, -1);
            Point p4 = new Point(2, -3, -1);
            Point expected = p2;

            var result = _util.DoLinesIntersect(p1, p2, p3, p4);
            result.Should().BeOfType<Event>();
            Assert.Equivalent(expected, result.Point);
        }
    }
}


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
        public void LinesegmentUtil_IsOnSegment_ReturnsTrueForColinearPoints()
        {
            Point p1 = new Point(-2, 1, -1);
            Point p2 = new Point(2, 5, -1);
            Point p3 = new Point(1, 4, -1);
            var result = _util.IsOnSegment(p1, p2, p3);
            result.Should().BeTrue();
            //intersectionPoint.Should().NotBeNull();
            //intersectionPoint.Point.X.Should().Be(p3.X);
        }

        [Fact]
        public void LinesegmentUtil_IsOnSegment_ReturnsFalseForPointsOfRightAngledTriangle()
        {
            Point p1 = new Point(1, 1, -1);
            Point p2 = new Point(5, 5, -1);
            Point p3 = new Point(5, 1, -1);
            var result = _util.IsOnSegment(p1, p2, p3);
            result.Should().BeFalse();
            //intersectionPoint.Should().NotBeNull();
            //intersectionPoint.Point.X.Should().Be(p3.X);
        }

        [Fact]
        public void LineSegmentUtil_PointIsAboveLine_returnsExpectedResults()
        {
            //(-76.0,51.0), (-50.0,12.0), (8.0,-22.0), (30.0,32.0), (67.0,-79.0)
            List<Point> points = new List<Point>()
            {
                new Point(-76,51,-1),
                new Point(-50,12, -1),
                new Point(8,-22,-1),
                new Point(30, 32,-1),
                new Point(67,-79,-1)
            };
            List<Point> above = new List<Point>();
            List<Point> below = new List<Point>();

            float grad = _util.FindGradient(points[0], points[4]);
            float xInt = _util.FindXIntercept(points[0], grad);
            foreach (Point p in points)
            {
                if (p.Equals(points[0]) || p.Equals(points[4])) continue;
                if (_util.PointIsAboveLineSegment(p, grad, xInt))
                    above.Add(p);
                else below.Add(p);
            }

            above.Count().Should().Be(2);
            below.Count().Should().Be(1);
            
        }

        [Fact]
        public void LinesegmentUtil_CheckIfVerticesSelfIntersectPolygon_returnsFalseForValidPolygonVertices()
        {
            List<Point> points = new List<Point>()
            {
                new Point(1,1,-1),
                new Point(2,2,-1),
                new Point(3,-1,-1),
                new Point(4,2,-1),
                new Point(5,1,-1)
            };
            List<LineSegment> lines = PointsToLineSegmentUtil.ConvertToPolygonVertices(points);
            List<bool> results = new List<bool>()
            {
                _util.CheckIfVerticesSelfIntersect(lines[0], lines[1]),
                _util.CheckIfVerticesSelfIntersect(lines[1], lines[2]),
                _util.CheckIfVerticesSelfIntersect(lines[2], lines[3]),
                _util.CheckIfVerticesSelfIntersect(lines[3], lines[4])

            };
            results.Should().OnlyContain(x => x == false);

        }


        [Fact]
        public void LinesegmentUtil_GetIntersection_ReturnsIntersectionEvent()
        {
            Point p1 = new Point(-2, 3, -1); //line y = -x+1
            Point p2 = new Point(2, -1, -1);
            Point p3 = new Point(2, 5, -1); //line y = x+ 3
            Point p4 = new Point(-3, 0, -1);
            Point expected = new Point(-1, 2, -1);

            var result = _util.DoLinesIntersect(p1, p2, p3, p4);
            result.Should().BeTrue();
        }

        [Fact]
        public void LinesegmentUtil_GetIntersection_ReturnsFalseOnNoIntersection()
        {
            Point p1 = new Point(-4, 6, -1); //line y = -x+1
            Point p2 = new Point(2, 6, -1);
            Point p3 = new Point(4, 5, -1); //line y = x+ 3
            Point p4 = new Point(5, 4, -1);
            Point expected = new Point(-1, 2, -1);

            var result = _util.DoLinesIntersect(p1, p2, p3, p4);
            result.Should().BeFalse();
        }

        [Fact]
        public void LinesegmentUtil_GetIntersection_ReturnsTrueIntersectionWithVerticalLine()
        {
            Point p1 = new Point(-4, 6, -1); 
            Point p2 = new Point(2, 6, -1);
            Point p3 = new Point(2, 6, -1);
            Point p4 = new Point(2, -3, -1);
            Point expected = p2;

            var result = _util.DoLinesIntersect(p1, p2, p3, p4);
            result.Should().BeTrue();
        }
    }
}


using System;
using FluentAssertions;
using PolygonUtility.Models;
using PolygonUtility.Utils;

namespace GeometryPuzzleApp.Test.PolygonUtility.Test
{
	public class CheckPointWithinPolygonTest
	{
		public CheckPointWithinPolygonTest()
		{
		}

		[Fact]
		public void CheckPointWithinPolygon_IsPointInPolygon_returnsTrueWhenWithin1()
		{
			var points = new List<Point>()
            {
                new Point(1, 6, 1),
                new Point(6, 6, 2),
                new Point(6, 2, 3),
                new Point(4, 5, 4),
                new Point(2, 1, 5),
            };
			CheckPointWithinPolygonUtil util = new CheckPointWithinPolygonUtil();
			bool result = util.IsPointInPolygon(new Point(3, 5, -1), points);
			result.Should().BeTrue();
        }

        [Fact]
        public void CheckPointWithinPolygon_IsPointInPolygon_returnsFalseWhenPointOnVertice()
        {
            var points = new List<Point>()
            {
                new Point(1, 6, 1),
                new Point(6, 6, 2),
                new Point(6, 2, 3),
                new Point(4, 5, 4),
                new Point(2, 1, 5),
            };
            CheckPointWithinPolygonUtil util = new CheckPointWithinPolygonUtil();
            bool result = util.IsPointInPolygon(new Point(3, 6, -1), points);
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckPointWithinPolygon_IsPointInPolygon_returnsFalseWhenPointOnEdge()
        {
            var points = new List<Point>()
            {
                new Point(1, 6, 1),
                new Point(6, 6, 2),
                new Point(6, 2, 3),
                new Point(4, 5, 4),
                new Point(2, 1, 5),
            };
            CheckPointWithinPolygonUtil util = new CheckPointWithinPolygonUtil();
            bool result = util.IsPointInPolygon(new Point(1, 6, -1), points);
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckPointWithinPolygon_IsPointInPolygon_returnsFalseWhenOutside()
        {
            var points = new List<Point>()
            {
                new Point(1, 6, 1),
                new Point(6, 6, 2),
                new Point(6, 2, 3),
                new Point(4, 5, 4),
                new Point(2, 1, 5),
            };
            CheckPointWithinPolygonUtil util = new CheckPointWithinPolygonUtil();
            bool result = util.IsPointInPolygon(new Point(4, 4, -1), points);
            //bool result = util.IsPointInPolygon(new Point(4, 4, -1), points);
            result.Should().BeFalse();
        }
    }
}


using System;
using FluentAssertions;
using GeometryPuzzleApp.ShapeGenerators;
using PolygonUtility;
using PolygonUtility.Models;
using PolygonUtility.PolygonIntersectionCheckUtility;
using PolygonUtility.Utils;

namespace GeometryPuzzleApp.Test.ShapeGenerator
{
	public class RandomShapeGeneratorTest
	{
		public RandomShapeGeneratorTest()
		{
		}

		[Fact]
		public void RandomShapeGenerator_GetPointsOfPolygon_returnsCorrectNumberOfPoints()
		{
			RandomShapeGenerator generator = new RandomShapeGenerator();
			generator.NoOfPoints = 5;
			var points = generator.GetPointsOfPolygon();
			var count = points.Count();

			count.Should().Be(5);
			count.Should().Be(generator.NoOfPoints);
		}

        [Fact]
        public void RandomShapeGenerator_GetPointsOfPolygon_returnsValidPolygon()
        {
            RandomShapeGenerator generator = new RandomShapeGenerator();
			generator.NoOfPoints = 8;
            var points = generator.GetPointsOfPolygon();
			PolygonIntersectionCheckUtil util = new PolygonIntersectionCheckUtil();
			var result = util.IsPolygonSelfIntersecting(points);
			result.Should().BeFalse();
        }
    }
}


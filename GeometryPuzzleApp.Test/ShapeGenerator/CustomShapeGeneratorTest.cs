using FluentAssertions;
using GeometryPuzzleApp.ShapeGenerators;
using Xunit;
using GeometryPuzzleApp;

namespace GeometryPuzzleApp.Test.ShapeGenerator
{
	public class CustomShapeGeneratorTest
	{
		private readonly CustomShapeGenerator _customShape;

		public CustomShapeGeneratorTest()
		{
			_customShape = new CustomShapeGenerator();
		}

		[Fact]
		public void CustomShapeGenerator_AddPoints_ReturnsFalseOnDuplicate()
		{
			_customShape.AddPoints(1, 1);
			bool result = _customShape.AddPoints(1, 1);
			result.Should().BeFalse();

		}

		[Fact]
		public void CustomShapeGenerator_AddPoints_ReturnsFalseOnIntersectingCoordinates()
		{
			_customShape.AddPoints(5, 1);
			_customShape.AddPoints(5, 5);
			var result = _customShape.AddPoints(4, 0);
			result.Should().BeFalse();
		}
	}
}


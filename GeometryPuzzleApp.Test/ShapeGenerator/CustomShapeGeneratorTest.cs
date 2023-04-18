//using FluentAssertions;
//using GeometryPuzzleApp.ShapeGenerators;
//using Xunit;
//using GeometryPuzzleApp;

//namespace GeometryPuzzleApp.Test.ShapeGenerator
//{
//	public class CustomShapeGeneratorTest
//	{
//		private readonly CustomShapeGenerator _customShape;

//		public CustomShapeGeneratorTest()
//		{
//			_customShape = new CustomShapeGenerator();
//		}

//		[Fact]
//		public void CustomShapeGenerator_ValidateCoordinates_ReturnsFalseOnDuplicate()
//		{
//			_customShape.AddCoordinates(1, 2);
//            bool result = _customShape.AddCoordinates(1, 2);
//			result.Should().BeFalse();

//        }

//        public void CustomShapeGenerator_ValidateCoordinates_ReturnsFalseOnIntersectingCoordinates()
//        {

//        }
//    }
//}


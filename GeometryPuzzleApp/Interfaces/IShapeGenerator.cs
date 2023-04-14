using System;
namespace GeometryPuzzleApp.Interfaces
{
	public interface IShapeGenerator
	{
		bool AddCoordinates(int x, int y);
		bool CheckCoordinates(int x, int y);
	}
}


using System;
using PolygonUtility.Models;

namespace GeometryPuzzleApp.Interfaces
{
	public interface ICustomShapeGenerator: IShapeGenerator
	{
		bool AddPoints(int x, int y);
		bool NewLineIsValid(LineSegment newLine);
        bool LastLineIsValid();
        bool IsValidAndCompleteShape();
    }
}


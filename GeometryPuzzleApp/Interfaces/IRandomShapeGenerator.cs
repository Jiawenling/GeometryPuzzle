using System;
using PolygonUtility.Models;

namespace GeometryPuzzleApp.Interfaces
{
	public interface IRandomShapeGenerator: IShapeGenerator
	{
        List<Point> GeneratePoints();
    }
}


using System;
using PolygonUtility.Models;

namespace GeometryPuzzleApp.Interfaces
{
	public interface IShapeGenerator
	{
		List<Point> GetPointsOfPolygon();
	}
}


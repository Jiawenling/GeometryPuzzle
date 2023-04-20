using System;
using PolygonUtility.Models;

namespace GeometryPuzzleApp.Interfaces
{
	public interface IRunMode
	{
		void Start();
		bool CheckPointWithin(Point point);
	}
}


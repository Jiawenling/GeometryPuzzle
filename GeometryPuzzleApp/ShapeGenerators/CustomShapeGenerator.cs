using System;
using GeometryPuzzleApp.Interfaces;

namespace GeometryPuzzleApp.ShapeGenerators
{
	public class CustomShapeGenerator: IShapeGenerator
	{
        private HashSet<(int, int)> _coordinateSet = new HashSet<(int, int)>();
        private List<(int, int)> _coordinates = new List<(int, int)>();


        public CustomShapeGenerator()
		{
		}

        public bool AddCoordinates(int x, int y)
        {
            if (!ValidateCoordinates(x, y)) return false;
            _coordinateSet.Add((x, y));
            _coordinates.Add((x, y));
            return true;
        }


        public bool CheckCoordinates(int x, int y)
        {
            return true;
        }


        private bool ValidateCoordinates(int x, int y)
        {
            if (_coordinateSet.Contains((x, y))) return false;
            if (_coordinateSet.Count < 3) return true;
            return true;
        }

        public void ResetAllCoordinates()
        {
            _coordinates = new List<(int, int)>();
            _coordinateSet = new HashSet<(int, int)>();
        }
    }

}


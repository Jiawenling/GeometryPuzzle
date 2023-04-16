using System;
using GeometryPuzzleApp.Interfaces;
using PolygonUtility.Models;
using PolygonUtility.PolygonIntersectionCheckUtility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeometryPuzzleApp.ShapeGenerators
{
	public class CustomShapeGenerator: IShapeGenerator
	{
        private HashSet<(int, int)> _coordinateSet = new HashSet<(int, int)>();
        private List<Point> _coordinates = new List<Point>();
        private int _currentCoordinate = 0;
        private PolygonIntersectionCheckUtility _util;
        public CustomShapeGenerator()
		{
            _util = new PolygonIntersectionCheckUtility();
		}

        public bool AddCoordinates(int x, int y)
        {
            if (!ValidateCoordinates(x, y)) return false;
            _coordinateSet.Add((x, y));
            _coordinates.Add(new Point(x,y, _currentCoordinate));
            _currentCoordinate += 1;
            return true;
        }


        public bool CheckCoordinates(int x, int y)
        {
            return true;
        }

        public void ResetAllCoordinates()
        {
            _coordinates = new List<Point>();
            _coordinateSet = new HashSet<(int, int)>();
        }

        public bool ValidateCoordinates(int x, int y)
        {
            if (_coordinateSet.Contains((x, y))) return false;
            if (_coordinateSet.Count < 3) return true;
            return true;
        }

        static bool onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }




    }

}


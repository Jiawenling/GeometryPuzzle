using System;
using GeometryPuzzleApp.Interfaces;
using PolygonUtility.Models;
using PolygonUtility.PolygonIntersectionCheckUtility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeometryPuzzleApp.ShapeGenerators
{
    public class CustomShapeGenerator : IShapeGenerator
    {
        private HashSet<(float, float)> _pointsSet = new HashSet<(float, float)>();
        private List<LineSegment> _lines = new List<LineSegment>();
        private List<Point> _points = new List<Point>();
        private int _currentCoordinate = 0;
        private PolygonIntersectionCheckUtil _util;
        public CustomShapeGenerator()
        {
            //_util = new PolygonIntersectionCheckUtility();
        }

        public bool AddPoints(float x, float y)
        {
            if (_pointsSet.Contains((x, y))) return false;
            if (_pointsSet.Count <= 2) return true;

            var newPoint = new Point(x, y, _currentCoordinate);
            var newLine = GetNewLineSegment(newPoint);
            if (!NewLineIsValid(newLine)) return false;
            AddNewValidPoint(newPoint, newLine);
            _currentCoordinate += 1;
            return true;
        }

        private void AddNewValidPoint(Point newPoint, LineSegment newLine)
        {
            _pointsSet.Add((newPoint.X, newPoint.Y));
            _points.Add(newPoint);
            _lines.Add(newLine);
        }

        private LineSegment GetNewLineSegment(Point point)
        {
            return new LineSegment(point, _points.Last(), _points.Count);
        }

        public bool CheckCoordinates(int x, int y)
        {
            return true;
        }

        public bool NewLineIsValid(LineSegment newLine)
        {
            if (_pointsSet.Count < 2) return true;
            PolygonIntersectionCheckUtil util = new PolygonIntersectionCheckUtil(_lines, newLine);
            return !util.IsNewLineIntersecting();
        }

        public List<Point> GetPointsOfPolygon()
        {
            return _points;
        }
    }

}


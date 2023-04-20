using System;
using GeometryPuzzleApp.Interfaces;
using PolygonUtility.Models;
using PolygonUtility.PolygonIntersectionCheckUtility;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace GeometryPuzzleApp.ShapeGenerators
{
    public class CustomShapeGenerator : ICustomShapeGenerator
    {
        private HashSet<(float, float)> _pointsSet = new HashSet<(float, float)>();
        private List<LineSegment> _lines = new List<LineSegment>();
        private List<Point> _points = new List<Point>();
        private int _currentCoordinate = 0;
        private PolygonIntersectionCheckUtil _util;

        public CustomShapeGenerator()
        {
            _util = new PolygonIntersectionCheckUtil();
        }

        public bool AddPoints(int x, int y)
        {
            if (_pointsSet.Contains((x, y))) return false;
            var newPoint = new Point(x, y, _currentCoordinate);
            if (!_pointsSet.Any())
            {
                AddNewValidPoint(newPoint, null);
                return true;
            }

            var newLine = GetNewLineSegment(newPoint);
            if (_pointsSet.Count == 1)
            {
                AddNewValidPoint(newPoint, newLine);
                return true;
            }

            if (!NewLineIsValid(newLine)) return false;

            AddNewValidPoint(newPoint, newLine);
            return true;
        }

        public bool NewLineIsValid(LineSegment newLine)
        {
            if (_pointsSet.Count < 2) return true;
            return !_util.IsNewLineIntersecting(_lines, newLine);
        }

        public List<Point> GetPointsOfPolygon()
        {
            return _points;
        }

        public bool IsCompleteShape()
        {
            return _points.Count >= 3;
        }

        private void AddNewValidPoint(Point newPoint, LineSegment newLine)
        {
            _pointsSet.Add((newPoint.X, newPoint.Y));
            _points.Add(newPoint);
            if(newLine!=null) _lines.Add(newLine);
            _currentCoordinate += 1;
        }

        private LineSegment GetNewLineSegment(Point point)
        {
            return new LineSegment(point, _points.Last(), _points.Count);
        }
    }

}


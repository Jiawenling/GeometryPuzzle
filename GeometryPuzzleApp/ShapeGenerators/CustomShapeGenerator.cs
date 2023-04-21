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
                _points.Add(newPoint);
                _pointsSet.Add(((float)x,(float)y));
                _currentCoordinate += 1;
                return true;
            }

            if (_pointsSet.Count == 1)
            {
                var newLine = GetNewLineSegment(newPoint);
                AddNewValidPoint(newPoint, newLine);
                return true;
            }

            var newL = GetNewLineSegment(newPoint);
            if (!NewLineIsValid(newL)) return false;
            AddNewValidPoint(newPoint, newL);
            return true;
        }

        public bool IsPointOfPolygon(Point point)
        {
            return _pointsSet.Contains((point.X,point.Y));
        }

        public bool NewLineIsValid(LineSegment newLine)
        {
            if (_pointsSet.Count < 2) return true;
            return !_util.IsNewLineIntersecting(_lines, newLine);
        }

        public bool LastLineIsValid()
        {
            LineSegment lastLine = GetLineConnectingFirstAndLastPoint();
            return NewLineIsValid(lastLine);
        }

        public List<Point> GetPointsOfPolygon()
        {
            return _points;
        }

        public bool IsValidAndCompleteShape()
        {
            if (_points.Count < 3) return false;
            var firstItem = _points.First();
            return !_points.All(p => p.X.Equals(firstItem.X)) &&
                    !_points.All(w => w.Y.Equals(firstItem.Y));
        }

        private void AddNewValidPoint(Point newPoint, LineSegment newLine)
        {
            _pointsSet.Add((newPoint.X, newPoint.Y));
            _points.Add(newPoint);
            _lines.Add(newLine);
            _currentCoordinate += 1;
        }

        private LineSegment GetNewLineSegment(Point point)
        {
            return new LineSegment(point, _points.Last(), _points.Count);
        }

        private LineSegment GetLineConnectingFirstAndLastPoint()
        {
            return new LineSegment(_points.First(), _points.Last(), _points.Count);
        }
    }

}


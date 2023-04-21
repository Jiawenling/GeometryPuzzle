using System;
using System.Collections.Generic;
using GeometryPuzzleApp.Interfaces;
using PolygonUtility.Models;
using PolygonUtility.Utils;
using PolygonUtility.PolygonIntersectionCheckUtility;
using System.Configuration;

namespace GeometryPuzzleApp.ShapeGenerators
{
    public class RandomShapeGenerator : IRandomShapeGenerator
    {
        public int NoOfPoints { get; set; }
        private Random _random;
        private LinesegmentUtil _linesegmentUtil;
        private PolygonIntersectionCheckUtil _polygonUtil;
        private const string EXCLUSIVEMAXVALUE_KEY = "ExclusiveMaxValue";
        private const string INCLUSIVEMINVALUE_KEY = "InclusiveMinValue";
        private int _exclusiveMax;
        private int _inclusiveMin;
        private HashSet<(int, int)> _pointsSet;

        public RandomShapeGenerator()
        {
            _random = new Random();
            NoOfPoints = _random.Next(3, 9);
            _linesegmentUtil = new LinesegmentUtil();
            _polygonUtil = new PolygonIntersectionCheckUtil();
            if (GetLimitsOfRandomNumberGeneration(EXCLUSIVEMAXVALUE_KEY, out int maxValue))
                _exclusiveMax = maxValue;
            if (GetLimitsOfRandomNumberGeneration(INCLUSIVEMINVALUE_KEY, out int minValue))
                _inclusiveMin = minValue;
            if(_inclusiveMin >= _exclusiveMax || _exclusiveMax - _inclusiveMin < 10)
            {
                _exclusiveMax = 100;
                _inclusiveMin = -100;
            }
        }

        public List<Point> GetPointsOfPolygon()
        {
            List<Point> points = GeneratePoints();
            List<Point> aboveLine = new List<Point>();
            List<Point> belowLine = new List<Point>();
            PointComparer comparer = new PointComparer();
            points.Sort(comparer);
            Point leftMax = points.First();
            Point rightMax = points.Last();
            float gradient = _linesegmentUtil.FindGradient(leftMax, rightMax);
            float xIntercept = _linesegmentUtil.FindXIntercept(leftMax, gradient);
            foreach(var point in points)
            {
                if (point.Equals(leftMax) || point.Equals(rightMax)) continue;
                if (_linesegmentUtil.PointIsAboveLineSegment(point, gradient, xIntercept))
                    aboveLine.Add(point);
                else belowLine.Add(point);
            }
            List<Point> result = new List<Point>();
            result.Add(leftMax);
            result.AddRange(aboveLine); // add points from above in ascending order
            result.Add(rightMax);

            for(int i = belowLine.Count-1; i >= 0; i--)
            {
                result.Add(belowLine[i]);
            }
            string print = string.Join(',', result);
            if (_polygonUtil.IsPolygonSelfIntersecting(result))
            {
                _polygonUtil.ResetUtil();
                return GetPointsOfPolygon();
            }
            return result;
        }

        public List<Point> GeneratePoints()
        {
            List<Point> points = new List<Point>();
            _pointsSet = new HashSet<(int, int)>();
            int pointNo = 0;
            while (points.Count != NoOfPoints)
            {
                int x = _random.Next(_inclusiveMin,_exclusiveMax);
                int y = _random.Next(_inclusiveMin,_exclusiveMax);
                if (_pointsSet.Add((x, y)))
                {
                    points.Add(new Point(x, y, pointNo));
                    pointNo += 1;
                }
            }
            return points;
        }

        private bool GetLimitsOfRandomNumberGeneration(string key, out int result)
        {
            string a = ConfigurationManager.AppSettings.Get(key);
            if (int.TryParse(a, out result)) return true;
            return false;
        }

        public bool IsPointOfPolygon(Point point)
        {
            return _pointsSet.Contains((point.X, point.Y));

        }
    }
}


using System;
using System.Collections.Generic;
using GeometryPuzzleApp.Interfaces;
using PolygonUtility.Models;
using PolygonUtility.Utils;
using System.Linq;
using PolygonUtility.PolygonIntersectionCheckUtility;

namespace GeometryPuzzleApp.ShapeGenerators
{
    public class RandomShapeGenerator : IShapeGenerator
    {
        public int NoOfPoints { get; set; }
        private Random _random;
        private LinesegmentUtil _linesegmentUtil;
        private PolygonIntersectionCheckUtil _polygonUtil;

        public RandomShapeGenerator()
        {
            _random = new Random();
            NoOfPoints = _random.Next(3, 9);
            _linesegmentUtil = new LinesegmentUtil();
            _polygonUtil = new PolygonIntersectionCheckUtil();
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
            //aboveLine.Sort(comparer);
            //belowLine.Sort(comparer);

            result.Add(leftMax);
            result.AddRange(aboveLine); // add points from above in ascending order
            result.Add(rightMax);

            for(int i = belowLine.Count-1; i >= 0; i--)
            {
                result.Add(belowLine[i]);
            }
            //if (PolygonIsValid(result)) return result;
            //else return GetPointsOfPolygon();
            return result;
        }

        private bool PolygonIsValid(List<Point> result)
        {
            List<LineSegment> lines = PointsToLineSegmentUtil.ConvertToPolygonVertices(result);
            var newLine = lines.Last();
            lines.RemoveAt(lines.Count-1);
            return !_polygonUtil.IsNewLineIntersecting(lines, newLine);
        }

        public List<Point> GeneratePoints()
        {
            List<Point> points = new List<Point>();
            HashSet<(int, int)> pointsSet = new HashSet<(int, int)>();
            int pointNo = 0;
            while (points.Count != NoOfPoints)
            {
                int x = _random.Next(-100,100);
                int y = _random.Next(-100,100);
                if (pointsSet.Add((x, y)))
                {
                    points.Add(new Point(x, y, pointNo));
                    pointNo += 1;
                }
            }
            return points;
        }
    }
}


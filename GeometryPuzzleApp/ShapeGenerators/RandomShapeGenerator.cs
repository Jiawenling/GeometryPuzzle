using System;
using System.Collections.Generic;
using GeometryPuzzleApp.Interfaces;
using PolygonUtility.Models;
using PolygonUtility.Utils;

namespace GeometryPuzzleApp.ShapeGenerators
{
    public class RandomShapeGenerator : IShapeGenerator
    {
        private Random _random;
        private int _noOfPoints;
        private LinesegmentUtil _linesegmentUtil;

        public RandomShapeGenerator()
        {
            _random = new Random();
            _noOfPoints = _random.Next(3, 9);
        }

        public List<Point> GetPointsOfPolygon()
        {
            List<Point> points = GeneratePoints();
            List<Point> above = new List<Point>();
            List<Point> below = new List<Point>();
            Point leftMax = points.MinBy(a => a.X);
            Point rightMax = points.MaxBy(a => a.X);
            foreach(var point in points)
            {
                if (point.Equals(leftMax) || point.Equals(rightMax)) continue;
                int direction = _linesegmentUtil.GetDirection(leftMax,rightMax, point);
                if (direction > 1) above.Add(point);
                else below.Add(point);
            }
            above.OrderBy(z=> z.X);
            below.OrderByDescending(a=> a.X);
            List<Point> result = new List<Point>();
            result.Add(leftMax);
            result.AddRange(above);
            result.Add(rightMax);
            result.AddRange(below);
            return result;
        }

        public List<Point> GeneratePoints()
        {
            List<Point> points = new List<Point>();
            HashSet<(int, int)> pointsSet = new HashSet<(int, int)>();
            int pointNo = 0;
            while (points.Count != _noOfPoints)
            {
                int x = _random.Next();
                int y = _random.Next();
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


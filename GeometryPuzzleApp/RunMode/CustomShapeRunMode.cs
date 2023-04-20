using System;
using GeometryPuzzleApp.Interfaces;
using GeometryPuzzleApp.Util;
using PolygonUtility.Models;
using PolygonUtility.Utils;

namespace GeometryPuzzleApp.RunMode
{
	public class CustomShapeRunMode: IRunMode
	{
		private ICustomShapeGenerator _shapeGenerator;
		private ConsoleMessageUtil _messageUtil;
        private ProcessInputUtil _inputUtil;
        private CheckPointWithinPolygonUtil _pointWithinUtil;

        public CustomShapeRunMode(ICustomShapeGenerator shapeGenerator, ConsoleMessageUtil messageUtil, ProcessInputUtil inputUtil, CheckPointWithinPolygonUtil pointWithinUtil)
		{
			_shapeGenerator = shapeGenerator;
			_messageUtil = messageUtil;
            _inputUtil = inputUtil;
            _pointWithinUtil = pointWithinUtil;
		}

        public void Start()
        {
            int pointNo = 1;
            while (!_shapeGenerator.IsCompleteShape())
            {
                _messageUtil.PromptForInput(pointNo);
                Point? p = GetCoordinates(pointNo, false);
                if (p == null) continue;
                if (!_shapeGenerator.AddPoints(p.X, p.Y)) _messageUtil.InvalidCoordinates(p.X, p.Y);
                else pointNo += 1;
                List<Point> points1 = _shapeGenerator.GetPointsOfPolygon();
                _messageUtil.ShapeCompleteOrIncomplete(points1);
            }

            while (true)
            {
                _messageUtil.PromptForCompleteOrInput(pointNo);
                Point? p = GetCoordinates(pointNo, true);
                if (p == null) break;
                if (!_shapeGenerator.AddPoints(p.X, p.Y)) _messageUtil.InvalidCoordinates(p.X, p.Y);
                else pointNo+= 1;
                List<Point> points2 = _shapeGenerator.GetPointsOfPolygon();
                _messageUtil.ShapeComplete(points2);
            }

            List<Point> points = _shapeGenerator.GetPointsOfPolygon();
            var message = _messageUtil.ShapeFinalized(points);
            Console.WriteLine(message);
            _messageUtil.PromptForTestOrQuit();

            while (true)
            {
                Point? point = GetCoordinates(-1, true);
                if (point == null) break;
                AnswerToPuzzle(point, message);
            }

            _messageUtil.EndingMessage();
        }

        public bool CheckPointWithin(Point point)
        {
            List<Point> points = _shapeGenerator.GetPointsOfPolygon();
            return _pointWithinUtil.IsPointInPolygon(point, points);
        }

        private void AnswerToPuzzle(Point point, string message)
        {
            Console.WriteLine(message);
            if (CheckPointWithin(point)) _messageUtil.PointWithinShape(point.X, point.Y);
            else _messageUtil.PointOutsideOfShape(point.X, point.Y);
            _messageUtil.PromptForTestOrQuit();
        }

        private Point? GetCoordinates(int pointNo, bool optionToQuit)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null) continue;

                if (optionToQuit && _inputUtil.IsHexKey(input)) return null;
                Point? point = _inputUtil.GetCoordinates(input, pointNo);
                if (point == null) continue;
                else return point;
            }
        }
    }
}


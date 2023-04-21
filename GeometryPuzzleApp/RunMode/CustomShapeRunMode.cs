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
        private int _pointNo;
        private string _finalShapePoints;

        public CustomShapeRunMode(ICustomShapeGenerator shapeGenerator, ConsoleMessageUtil messageUtil, ProcessInputUtil inputUtil, CheckPointWithinPolygonUtil pointWithinUtil)
		{
			_shapeGenerator = shapeGenerator;
			_messageUtil = messageUtil;
            _inputUtil = inputUtil;
            _pointWithinUtil = pointWithinUtil;
            _pointNo = 1;
            _finalShapePoints = "";
        }

        public void Start()
        {
            bool shapeIsValidAndComplete = false;
            while (!shapeIsValidAndComplete)
            {
                _messageUtil.PromptForInput(_pointNo);
                if (!_inputUtil.ContinueToGetInputs(_pointNo, false, out Point p)) continue;
                TryAddPoint(p);
                List<Point> points1 = _shapeGenerator.GetPointsOfPolygon();
                shapeIsValidAndComplete = _shapeGenerator.IsValidAndCompleteShape();
                _messageUtil.ShapeCompleteOrIncomplete(points1, shapeIsValidAndComplete);
            }

            while (true)
            {
                _messageUtil.PromptForCompleteOrInput(_pointNo);
                if (!_inputUtil.ContinueToGetInputs(_pointNo, true, out Point p)) break;
                TryAddPoint(p);
                List<Point> points2 = _shapeGenerator.GetPointsOfPolygon();
                _messageUtil.ShapeComplete(points2);
            }

            List<Point> points = _shapeGenerator.GetPointsOfPolygon();
            _finalShapePoints = _messageUtil.ShapeFinalized(points);
            
            Console.WriteLine(_finalShapePoints);
            _messageUtil.PromptForTestOrQuit();

            while (true)
            {
                if (!_inputUtil.ContinueToGetInputs(-1, true, out Point point)) break;
                bool pointIsWithin = CheckPointWithin(point);
                _messageUtil.AnswerToPuzzle(point, _finalShapePoints, pointIsWithin);
            }

            _messageUtil.EndingMessage();
        }

        public bool CheckPointWithin(Point point)
        {
            List<Point> points = _shapeGenerator.GetPointsOfPolygon();
            if (_shapeGenerator.IsPointOfPolygon(point)) return false;
            return _pointWithinUtil.IsPointInPolygon(point, points);
        }

        private void TryAddPoint(Point p)
        {
            if (!_shapeGenerator.AddPoints(p.X, p.Y)) _messageUtil.InvalidCoordinates(p.X, p.Y);
            else _pointNo += 1;
        }




    }
}


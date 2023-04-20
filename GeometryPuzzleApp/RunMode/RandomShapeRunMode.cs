using System;
using GeometryPuzzleApp.Interfaces;
using GeometryPuzzleApp.Util;
using PolygonUtility.Models;
using PolygonUtility.Utils;

namespace GeometryPuzzleApp.RunMode
{
	public class RandomShapeRunMode: IRunMode
	{
        private IRandomShapeGenerator _shapeGenerator;
        private ConsoleMessageUtil _messageUtil;
        private CheckPointWithinPolygonUtil _pointWithinUtil;
        private ProcessInputUtil _inputUtil;
        private string _finalShapePoints;

        public RandomShapeRunMode(IRandomShapeGenerator shapeGenerator, ConsoleMessageUtil messageUtil, ProcessInputUtil inputUtil, CheckPointWithinPolygonUtil pointWithinUtil)
		{
            _shapeGenerator = shapeGenerator;
            _messageUtil = messageUtil;
            _pointWithinUtil = pointWithinUtil;
            _inputUtil = inputUtil;
            _finalShapePoints = "";
		}

        public void Start()
        {
            List<Point> points = _shapeGenerator.GeneratePoints();
            _finalShapePoints = _messageUtil.ShapeFinalized(points);
            _messageUtil.RandomShape(points);
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
            return _pointWithinUtil.IsPointInPolygon(point, points);
        }
    }
}


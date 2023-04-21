using System;
using System.Text;
using PolygonUtility.Models;

namespace GeometryPuzzleApp.Util
{
	public class ConsoleMessageUtil
	{
        private const string WELCOMEMESSAGE = "Welcome to the GIC geometry puzzle app\r\n[1] Create a custom shape\r\n[2] Generate a random shape";
        private const string ENDINGMESSAGE = "Thank you for playing the GIC geometry puzzle app\r\nHave a nice day!";

        public void WelcomeMessage() => Console.WriteLine(WELCOMEMESSAGE);

        public void EndingMessage() => Console.WriteLine(ENDINGMESSAGE);

        public void NotAValidInput()=>
            Console.WriteLine("\r\nThis is not a valid input. Please try again\r\n");

        public void PromptForInput(int pointNo) =>
			Console.WriteLine($"\r\nPlease enter coordinates {pointNo} in x y format\r\n");

        public void PromptForCompleteOrInput(int pointNo) =>
            Console.WriteLine($"\r\nPlease enter # to finalize your shape or enter coordinates {pointNo} in x y format\r\n");

        public void PromptForTestOrQuit()=>
            Console.WriteLine("\r\nPlease key in test coordinates in x y format or enter # to quit the game\r\n");

        public void ShapeCompleteOrIncomplete(List<Point> points, bool isValidAndComplete)
        {
            if (!isValidAndComplete) ShapeIncomplete(points);
            else ShapeComplete(points);
        }

        public void ShapeIncomplete(List<Point> points)
		{
			Console.WriteLine("\r\nYour current shape is incomplete");
			PrintPoints(points);
			//PromptForInput(points.Count);
		}

        public void ShapeComplete(List<Point> points)
        {
            Console.WriteLine("\r\nYour current shape is valid and is complete");
            PrintPoints(points);
			//PromptForCompleteOrInput(points.Count);
        }

        public void LastLineInvalid() => Console.WriteLine("\r\nThe last line connecting first and last points does not form a valid polygon. Please input another coordinate.");

        public string ShapeFinalized(List<Point> points)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Your finalized shape is");
            for (int i = 0; i < points.Count; i++)
            {
                sb.AppendLine($"{i + 1}:({points[i].X},{points[i].Y})");
            };
            return sb.ToString();
        }

        public void RandomShape(List<Point> points)
        {
            Console.WriteLine("\r\nYour random shape is");
            PrintPoints(points);
            //PromptForTestOrQuit();
        }

        public void InvalidCoordinates(int x, int y)
        {
            Console.WriteLine($"\r\nNew coordinates({x},{y}) is invalid!!");
            Console.WriteLine("Not adding new coordinate to the current shape\r\n");
        }

        public void AnswerToPuzzle(Point point, string finalShapePoints, bool pointIsWithin)
        {
            Console.WriteLine(finalShapePoints);
            if (pointIsWithin) PointWithinShape(point.X, point.Y);
            else PointOutsideOfShape(point.X, point.Y);
            PromptForTestOrQuit();
        }

        public void PointOutsideOfShape(int x, int y)
        {
            Console.WriteLine($"Sorry, coordinates ({x},{y}) is outside of your finalized shape");
        }

        public void PointWithinShape(int x, int y)
        {
            Console.WriteLine($"Coordinates ({x},{y}) is within your finalized shape");
        }

        public void PrintPoints(List<Point> points)
		{
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < points.Count; i++)
            {
                sb.AppendLine($"{i + 1}:({points[i].X},{points[i].Y})");
            };
			Console.WriteLine(sb.ToString());
            Console.WriteLine("");
        }
	}
}


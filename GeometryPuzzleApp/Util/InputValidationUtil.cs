using System;
using PolygonUtility.Models;

namespace GeometryPuzzleApp.Util
{
	public class ProcessInputUtil
	{
		public Point? GetCoordinates(string line, int pointNo)
		{
			line = line.Trim();
			var c = line.Split(' ');
			if (c.Count() < 2) return null;
			if(!int.TryParse(c[0], out int x)) return null;
            if(!int.TryParse(c[1], out int y)) return null;
			return new Point(x, y, pointNo);
        }

		public bool IsHexKey(string line)
		{
			line = line.Trim();
			return line == "#";
		}
    }
}


using System;
using C5;
using PolygonUtility.Models;
using PolygonUtility.Utils;

namespace PolygonUtility.PolygonIntersectionCheckUtility
{
	public class PolygonIntersectionCheckUtil
    {
		private PriorityQueue<Event, Event> _events;
        private SortedList<LineSegment,LineSegment> _sweepLine;
        private LinesegmentUtil _util;

        public PolygonIntersectionCheckUtil()
        {
            _events = new PriorityQueue<Event, Event>(new EventComparer());
            _sweepLine = new SortedList<LineSegment, LineSegment>(new LineSegmentComparer());
            _util = new LinesegmentUtil();
        }

        public bool IsPolygonSelfIntersecting(List<Point> points)
        {
            var lines = PointsToLineSegmentUtil.ConvertToPolygonVertices(points);
            var newLine = lines.Last();
            lines.RemoveAt(lines.Count - 1);
            return IsNewLineIntersecting(lines, newLine);
        }


        public bool IsNewLineIntersecting(List<LineSegment> existingLines, LineSegment newLine)
        {
            List<LineSegment> lines = new List<LineSegment>();
            lines.AddRange(existingLines);
            lines.Add(newLine);
            ResetUtil();
            InitialiseEventQueue(lines);
            int lineCount = lines.Count;
            while (_events.Count > 0)
            {
                var e = _events.Dequeue();
                if (e.PointType == PointType.START)
                {
                    var currentLine = lines[e.LineSegmentIndex];
                    if (CurrentLineIntersectsAdjacentLines(currentLine, lineCount)) return true;
                }
                else if (e.PointType == PointType.END)
                {
                    var currentLine = lines[e.LineSegmentIndex];
                    if (e.Point.Equals(newLine.End))
                    {
                        var l = GetNextLineSegment(currentLine);
                        if (l == null) return false;

                        if (IsNewLineIntersectsExistingLines(currentLine, l, lineCount)) return true;
                        return false;
                    }
                    var prevLine = GetPrevLineSegment(currentLine);
                    var nextLine = GetNextLineSegment(currentLine);

                    if (prevLine!=null && nextLine!=null &&
                        IsNewLineIntersectsExistingLines(prevLine, nextLine, lineCount)) return true;
                    _sweepLine.Remove(currentLine);
                }
            }
            return false;
        }

        public void ResetUtil()
        {
            _events = new PriorityQueue<Event, Event>(new EventComparer());
            _sweepLine = new SortedList<LineSegment, LineSegment>(new LineSegmentComparer());
        }

        private bool CurrentLineIntersectsAdjacentLines(LineSegment currentLine, int lineCount)
        {
            _sweepLine.Add(currentLine, currentLine);
            var prevLine = GetPrevLineSegment(currentLine);
            if (prevLine != null &&
                IsNewLineIntersectsExistingLines(currentLine, prevLine, lineCount)) return true;

            var nextLine = GetNextLineSegment(currentLine);
            if (nextLine != null &&
                IsNewLineIntersectsExistingLines(currentLine, nextLine, lineCount)) return true;
            return false;
        }

        private void InitialiseEventQueue(List<LineSegment> lines)
        {
            var lineCount = lines.Count;
            for (int i = 0; i < lines.Count; i++) {
                Event start = new Event(lines[i].Start, PointType.START, i);
                Event end = new Event(lines[i].End, PointType.END, i);

                _events.Enqueue(start, start);
                _events.Enqueue(end, end);
            };
        }
        private bool IsNewLineIntersectsExistingLines(LineSegment line1, LineSegment line2, int lineCount)
        {
            if (line1 == null || line2 == null) return false;
            if (!_util.CheckIfLinesAreVertices(line1, line2)) return _util.DoLinesIntersect(line1, line2);
            return _util.CheckIfVerticesSelfIntersect(line1, line2);
        }


        private bool AreVertices(LineSegment line1, LineSegment line2, int lineCount)
        {
            int lineNo = Math.Abs(line1.CurrentLineNo - line2.CurrentLineNo);
            //// if not adjacent line or first+last line
            if (lineNo != 1 && lineNo != lineCount - 1) return false;
            return true;
        }

        private LineSegment? GetPrevLineSegment(LineSegment line)
        {
            var ind = _sweepLine.IndexOfKey(line);
            return ind != -1 && ind!=0? _sweepLine.Values[ind - 1] : null;
        }

        private LineSegment? GetNextLineSegment(LineSegment line)
        {
            var ind = _sweepLine.IndexOfKey(line);
            return ind != -1 && ind != _sweepLine.Count-1 ? _sweepLine.Values[ind + 1] : null;
        }
    }
}


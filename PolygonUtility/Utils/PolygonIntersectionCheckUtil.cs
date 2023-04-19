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

        private bool _stopAfterThisRound;

        public PolygonIntersectionCheckUtil()
        {
            _events = new PriorityQueue<Event, Event>(new EventComparer());
            _sweepLine = new SortedList<LineSegment, LineSegment>(new LineSegmentComparer());
            _util = new LinesegmentUtil();
            _stopAfterThisRound = false;
        }


        public bool IsNewLineIntersecting(List<LineSegment> existingLines, LineSegment newLine)
        {
            existingLines.Add(newLine);
            InitialiseEventQueue(existingLines);
            int lineCount = existingLines.Count;
            while (_events.Count > 0)
            {
                var e = _events.Dequeue();
                if (e.PointType == PointType.START)
                {
                    var currentLine = existingLines[e.LineSegmentIndex];
                    _sweepLine.Add(currentLine, currentLine);
                    var prevLine = GetPrevLineSegment(currentLine);
                    if (IsNewLineIntersectsExistingLine(currentLine, prevLine, lineCount)) return true;

                    var nextLine = GetNextLineSegment(currentLine);
                    if (IsNewLineIntersectsExistingLine(currentLine, nextLine, lineCount)) return true; 
                }
                else if (e.PointType == PointType.END)
                {
                    var currentLine = existingLines[e.LineSegmentIndex];
                    if (e.Point.Equals(newLine.End))
                    {
                        var l = GetNextLineSegment(currentLine);
                        if (l == null) return false;

                        if (IsNewLineIntersectsExistingLine(currentLine, l, lineCount)) return true;
                        return false;
                    }
                    var prevLine = GetPrevLineSegment(currentLine);
                    var nextLine = GetNextLineSegment(currentLine);

                    if (IsNewLineIntersectsExistingLine(prevLine, nextLine, lineCount)) return true;
                    _sweepLine.Remove(currentLine);
                }
            }
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
        private bool IsNewLineIntersectsExistingLine(LineSegment line1, LineSegment line2, int lineCount)
        {
            if (line1 == null || line2 == null) return false;
            if (IsVerticePoint(line1, line2, lineCount)) return false;
            return _util.DoLinesIntersect(line1, line2);
        }


        private bool IsVerticePoint(LineSegment line1, LineSegment line2, int lineCount)
        {
            int lineNo = Math.Abs(line1.CurrentLineNo - line2.CurrentLineNo);
            // if not adjacent line or first+last line
            if (lineNo != 1 && lineNo != lineCount - 1) return false;
            return _util.CheckIfTwoLinesAreVerticesOfPolygon(line1, line2);
        }

        private LineSegment GetPrevLineSegment(LineSegment line)
        {
            var ind = _sweepLine.IndexOfKey(line);
            return ind != -1 && ind!=0? _sweepLine.Values[ind - 1] : null;
        }

        private LineSegment GetNextLineSegment(LineSegment line)
        {
            var ind = _sweepLine.IndexOfKey(line);
            return ind != -1 && ind != _sweepLine.Count-1 ? _sweepLine.Values[ind + 1] : null;
        }
    }
}


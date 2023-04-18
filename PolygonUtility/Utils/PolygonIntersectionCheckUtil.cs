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
		private List<LineSegment> _lines;
        private LineSegment _newLine;
        private System.Collections.Generic.HashSet<(float, float)> _vertices;
        private bool _stopAfterThisRound;
        public PolygonIntersectionCheckUtil(List<LineSegment> existingLines, LineSegment newLine)
        {
            _lines = existingLines;
            _newLine = newLine;
            _events = new PriorityQueue<Event, Event>(new EventComparer());
            _vertices = new System.Collections.Generic.HashSet<(float, float)>();
            _sweepLine = new SortedList<LineSegment, LineSegment>(new LineSegmentComparer());
            _util = new LinesegmentUtil();
            _stopAfterThisRound = false;
            InitialiseEventQueue();
            IntialiseVerticesSet();
        }

        private void InitialiseEventQueue()
        {
            _lines.Add(_newLine);
            var lineCount = _lines.Count;
            for (int i = 0; i < _lines.Count; i++) {
                Event start = new Event(_lines[i].Start, PointType.START, i);
                Event end = new Event(_lines[i].End, PointType.END, i);

                _events.Enqueue(start, start);
                _events.Enqueue(end, end);
            };
        }

        private void IntialiseVerticesSet()
        {
            _lines.ForEach(a =>
            {
                _vertices.Add((a.Start.X, a.Start.Y));
                _vertices.Add((a.End.X, a.End.Y));
            });
            _vertices.Add((_newLine.Start.X, _newLine.Start.Y));
            _vertices.Add((_newLine.End.X, _newLine.End.Y));
        }

        //public void AddToQueue(LineSegment line, int lineIndex)
        //{
        //    Event start = new Event(line.Start, PointType.START, lineIndex);
        //    Event end = new Event(line.Start, PointType.END, lineIndex);

        //    _events.Enqueue(start, start);
        //    _events.Enqueue(end, end);
        //}

        public bool IsNewLineIntersecting()
        {
            while (_events.Count > 0)
            {
                var e = _events.Dequeue();
                if (e.PointType == PointType.START)
                {
                    var currentLine = _lines[e.LineSegmentIndex];
                    _sweepLine.Add(currentLine, currentLine);
                    var prevLine = GetPrevLineSegment(currentLine);
                    if (IsNewLineIntersectsExistingLine(currentLine, prevLine)) return true;

                    var nextLine = GetNextLineSegment(currentLine);
                    if (IsNewLineIntersectsExistingLine(currentLine, nextLine)) return true; 
                }
                else if (e.PointType == PointType.END)
                {
                    var currentLine = _lines[e.LineSegmentIndex];
                    if (e.Point.Equals(_newLine.End))
                    {
                        var l = GetNextLineSegment(currentLine);
                        if (l==null) return false; 

                        if (IsNewLineIntersectsExistingLine(currentLine, l)) return true;
                        return false;
                    }
                    var prevLine = GetPrevLineSegment(currentLine);
                    var nextLine = GetNextLineSegment(currentLine);

                    if (IsNewLineIntersectsExistingLine(prevLine, nextLine)) return true;
                    _sweepLine.Remove(currentLine);
                }
            }
            return false;
        }

        private bool IsNewLineIntersectsExistingLine(LineSegment line1, LineSegment line2)
        {
            if (line1 == null || line2 == null) return false;
            Event res = _util.DoLinesIntersect(line1, line2);
            if (res == null) return false;
            if (!IsVerticePoint(res)) return true;
            return false;
        }

        private bool IsVerticePoint(Event result)
        {
            Point res = result.Point;
            return _vertices.Contains((res.X, res.Y));
        }

        private LineSegment GetPrevLineSegment(LineSegment line)
        {
            var ind = _sweepLine.IndexOfKey(line);
            return ind != -1 && ind != 0 ? _sweepLine.Values[ind - 1] : null;
        }

        private LineSegment GetNextLineSegment(LineSegment line)
        {
            var ind = _sweepLine.IndexOfKey(line);
            return ind != -1 && ind != _sweepLine.Count-1 ? _sweepLine.Values[ind + 1] : null;
        }
    }
}


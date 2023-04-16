using System;
using C5;
using PolygonUtility.Models;
using PolygonUtility.Utils;

namespace PolygonUtility.PolygonIntersectionCheckUtility
{
	public class PolygonIntersectionCheckUtility
    {
		private PriorityQueue<Event, Event> _events;
        //private PriorityQueue<LineSegment, LineSegment> _sweepLine;
        private TreeDictionary<LineSegment,LineSegment> _sweepLine;
		private List<LineSegment> _lines;
        private LineSegment _newLine;
        private bool _stopAfterThisRound;
        private IntersectionCheckUtil _util;
        public PolygonIntersectionCheckUtility()
        {
            _events = new PriorityQueue<Event, Event>(new EventComparer());
            _sweepLine = new TreeDictionary<LineSegment, LineSegment>(new LineSegmentComparer());
            //_lines = lines;
            //_newLine = newLine;
            //InitialiseEventQueue(lines);
            _util = new IntersectionCheckUtil();
            _stopAfterThisRound = false;
        }

        private void InitialiseEventQueue(List<LineSegment> lines)
        {
            var lineCount = lines.Count;
            for (int i = 0; i < lines.Count; i++) {
                Event start = new Event(lines[i].Start, PointType.START, i);
                Event end = new Event(lines[i].Start, PointType.END, i);

                _events.Enqueue(start, start);
                _events.Enqueue(end, end);
            };
        }

        //public void AddToQueue(LineSegment line, int lineIndex)
        //{
        //    Event start = new Event(line.Start, PointType.START, lineIndex);
        //    Event end = new Event(line.Start, PointType.END, lineIndex);

        //    _events.Enqueue(start, start);
        //    _events.Enqueue(end, end);
        //}

        public void IsNewLineIntersecting()
        {
            while (_events.Count > 0 && !_stopAfterThisRound)
            {
                var e = _events.Dequeue();
                if (e.PointType == PointType.START)
                {
                    var currentLine = _lines[e.LineSegmentIndex];
                    _sweepLine.Add(currentLine, currentLine);
                    if (_sweepLine.TryPredecessor(currentLine, out C5.KeyValuePair<LineSegment, LineSegment> prevLine))
                        _util.DoLinesIntersect(currentLine, prevLine.Key);
                    if (_sweepLine.TrySuccessor(currentLine, out C5.KeyValuePair<LineSegment, LineSegment> nextLine))
                        _util.DoLinesIntersect(currentLine, nextLine.Key);
                }
                if (e.PointType == PointType.END)
                {
                    if (e.Equals(_newLine.End))
                        _stopAfterThisRound = true;
                }
                else
                {

                }
            }
        }
    }
}


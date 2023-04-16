﻿using System;
namespace PolygonUtility.Models
{
	public class EventComparer: IComparer<Event>
	{

        public int Compare(Event p, Event q)
        {
            Point a = p.Point;
            Point b = q.Point;
            if (a.X.CompareTo(b.X) != 0) return a.X.CompareTo(b.X);
            if (a.Y.CompareTo(b.Y) != 0) return a.Y.CompareTo(b.Y);
            if (a.CurrentPointNo.CompareTo(b.CurrentPointNo) != 0)
                return a.CurrentPointNo.CompareTo(b.CurrentPointNo);
            return 0;
        }

    }
}


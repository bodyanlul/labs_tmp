using System.Collections.Generic;
using System.Linq;

namespace ShadowLab
{
    public class Segment
    {
        public double Begin, End;

        public double Length => End - Begin;

        public Segment(double begin, double end)
        {
            Begin = begin;
            End = end;
        }

        public bool Intersection(Segment segment)
        {
            return !(Begin > segment.End) && !(End < segment.Begin);
        }
    }

    public class Shadow
    {
        private readonly List<Segment> _segments;

        public Shadow()
        {
            _segments = new List<Segment>();
        }

        public bool AddSegment(Segment segment)
        {
            if (segment.Begin >= segment.End)
                return false;
            var add = true;
            for (var i = 0; i < _segments.Count; i++)
            {
                if (!segment.Intersection(_segments[i]))
                    continue;

                // Расширение segments
                if (segment.Begin < _segments[i].Begin)
                    _segments[i].Begin = segment.Begin;

                if (segment.End > _segments[i].End)
                    _segments[i].End = segment.End;

                var tmp = _segments[i];
                _segments.Remove(_segments[i]);
                AddSegment(tmp);
                add = false;
            }

            if (add) _segments.Add(segment);

            return true;
        }

        public double GetLength()
        {
            return _segments.Sum(t => t.Length);
        }
    }
}

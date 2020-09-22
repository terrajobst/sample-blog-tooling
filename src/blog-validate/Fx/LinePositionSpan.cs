using System;

namespace BlogValidator
{
    public struct LinePositionSpan : IEquatable<LinePositionSpan>
    {
        public LinePositionSpan(LinePosition start, LinePosition end)
        {
            Start = start;
            End = end;
        }

        public bool IsEmpty => Start == End;
        public LinePosition Start { get; }
        public LinePosition End { get; }

        public override bool Equals(object obj)
        {
            return obj is LinePositionSpan span && Equals(span);
        }

        public bool Equals(LinePositionSpan other)
        {
            return Start.Equals(other.Start) &&
                   End.Equals(other.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public static bool operator ==(LinePositionSpan left, LinePositionSpan right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LinePositionSpan left, LinePositionSpan right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return IsEmpty ? $"{Start}" : $"{Start},{End}";
        }
    }
}

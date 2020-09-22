using System;

namespace BlogValidator
{
    public struct LinePosition : IEquatable<LinePosition>
    {
        public LinePosition(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; }
        public int Column { get; }

        public override bool Equals(object obj)
        {
            return obj is LinePosition position && Equals(position);
        }

        public bool Equals(LinePosition other)
        {
            return Line == other.Line &&
                   Column == other.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Line, Column);
        }

        public static bool operator ==(LinePosition left, LinePosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LinePosition left, LinePosition right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Line + 1},{Column + 1}";
        }
    }
}

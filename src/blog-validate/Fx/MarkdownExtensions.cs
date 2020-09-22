using System;

using Markdig.Syntax;

namespace BlogValidator
{
    public static class MarkdownExtensions
    {
        public static LinePosition GetLinePosition(this MarkdownDocument document, int index)
        {
            for (var i = document.LineStartIndexes.Count - 1; i >= 0; i--)
            {
                var lineStart = document.LineStartIndexes[i];
                if (lineStart <= index)
                {
                    var line = i;
                    var column = index - lineStart;
                    return new LinePosition(line, column);
                }
            }

            throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        public static LinePositionSpan GetLinePosition(this MarkdownDocument document, SourceSpan span)
        {
            var start = document.GetLinePosition(span.Start);
            var end = document.GetLinePosition(span.End);
            return new LinePositionSpan(start, end);
        }
    }
}

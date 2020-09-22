using Markdig.Syntax;

namespace BlogValidator
{
    internal class Diagnostic
    {
        public Diagnostic(bool isWarning, string id, MarkdownDocument document, string fileName, SourceSpan span, string message)
        {
            IsWarning = isWarning;
            Id = id;
            FileName = fileName;
            Span = span;
            LinePositionSpan = document.GetLinePosition(span);
            Message = message;
        }

        public bool IsWarning { get; }
        public string Id { get; }
        public string FileName { get; }
        public SourceSpan Span { get; }
        public LinePositionSpan LinePositionSpan { get; }
        public string Message { get; }
    }
}

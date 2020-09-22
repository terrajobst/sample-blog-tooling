using System;
using System.Collections.Generic;
using System.Linq;

using Markdig.Extensions.Yaml;
using Markdig.Syntax;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BlogValidator
{
    internal sealed class ValidationContext
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public ValidationContext(MarkdownDocument document, string fileName)
        {
            if (document.FirstOrDefault() is YamlFrontMatterBlock frontMatter)
            {
                var yaml = string.Join(Environment.NewLine, frontMatter.Lines);
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();

                FrontMatter = deserializer.Deserialize<FrontMatter>(yaml);
            }

            Document = document;
            FileName = fileName;
        }

        public FrontMatter FrontMatter { get; }
        public MarkdownDocument Document { get; }
        public string FileName { get; }
        public IReadOnlyList<Diagnostic> Diagnostics => _diagnostics;

        private void Report(bool isWarning, string id, SourceSpan span, string message)
        {
            _diagnostics.Add(new Diagnostic(isWarning, id, Document, FileName, span, message));
        }

        public void Error(string id, MarkdownObject o, string message)
        {
            Report(isWarning: false, id, o.Span, message);
        }

        public void Error(string id, SourceSpan span, string message)
        {
            Report(isWarning: false, id, span, message);
        }

        public void Warning(string id, MarkdownObject o, string message)
        {
            Report(isWarning: true, id, o.Span, message);
        }

        public void Warning(string id, SourceSpan span, string message)
        {
            Report(isWarning: true, id, span, message);
        }
    }
}

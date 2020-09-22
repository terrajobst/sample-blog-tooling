using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Markdig;
using Markdig.Parsers;

namespace BlogValidator
{
    internal sealed class Validator
    {
        private readonly IReadOnlyList<ValidationRule> _rules;
        private readonly MarkdownPipeline _pipeline;

        public Validator()
        {
            _rules = GetRules();
            _pipeline = new MarkdownPipelineBuilder()
                .UsePipeTables()
                .UseYamlFrontMatter()
                .UsePreciseSourceLocation()
                .Build();
        }

        private static IReadOnlyList<ValidationRule> GetRules()
        {
            return typeof(ValidationRule).Assembly
                                         .GetTypes()
                                         .Where(t => !t.IsAbstract && typeof(ValidationRule).IsAssignableFrom(t))
                                         .Select(t => (ValidationRule)Activator.CreateInstance(t))
                                         .ToArray();
        }

        public IReadOnlyList<Diagnostic> Validate(string fileName)
        {
            var markdown = File.ReadAllText(fileName);
            var document = MarkdownParser.Parse(markdown, _pipeline);
            var context = new ValidationContext(document, fileName);

            foreach (var rule in _rules)
                rule.Validate(context);

            return context.Diagnostics;
        }
    }
}

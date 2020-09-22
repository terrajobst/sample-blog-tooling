using System.Linq;

using Markdig.Syntax;

namespace BlogValidator
{
    internal sealed class VR02_MetadataRequired : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            if (context.FrontMatter != null)
            {
                var block = context.Document.First();
                var diagnosticSpan = new SourceSpan(block.Span.End, 0);

                if (string.IsNullOrEmpty(context.FrontMatter.PostTitle))
                    context.Error("VR02", diagnosticSpan, "Must specify post_title");

                if (string.IsNullOrEmpty(context.FrontMatter.Summary))
                    context.Error("VR02", diagnosticSpan, "Must specify summary");

                if (string.IsNullOrEmpty(context.FrontMatter.Username))
                    context.Error("VR02", diagnosticSpan, "Must specify username");
            }
        }
    }
}

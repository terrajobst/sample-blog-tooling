using System.Linq;

using Markdig.Syntax;

namespace BlogValidator
{
    internal sealed class VR16_TitleShouldBe60OrLess : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            if (context.FrontMatter != null)
            {
                var block = context.Document.First();
                var diagnosticSpan = new SourceSpan(block.Span.End, 0);

                if (context.FrontMatter.PostTitle?.Length > 60)
                    context.Warning("VR16", diagnosticSpan, "post_title should be 60 characters or less");
            }
        }
    }
}

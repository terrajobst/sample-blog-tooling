
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace BlogValidator
{
    internal sealed class VR11_LinkNeedsText : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var links = context.Document.Descendants<LinkInline>();

            foreach (var link in links)
            {
                if (link.IsImage)
                    continue;

                if (string.IsNullOrEmpty(link.FirstChild?.ToString()))
                    context.Error("VR11", link, "Link must have text");
            }
        }
    }
}

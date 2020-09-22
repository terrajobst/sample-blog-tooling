using System.Linq;

using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace BlogValidator
{
    internal sealed class VR07_ImagesMustHaveAltText : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var images = context.Document.Descendants<LinkInline>()
                                         .Where(i => i.IsImage);

            foreach (var image in images)
            {
                if (string.IsNullOrWhiteSpace(image.FirstChild?.ToString()))
                    context.Error("VR07", image, "Image must have alt text");
            }
        }
    }
}

using System.Linq;

using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace BlogValidator
{
    internal sealed class VR15_ImageAltTextShouldBe125OrLess : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var images = context.Document.Descendants<LinkInline>()
                                         .Where(i => i.IsImage);

            foreach (var image in images)
            {
                var altText = image.FirstChild?.ToString();
                if (string.IsNullOrEmpty(altText))
                    continue;
                
                if (altText.Length > 125)
                    context.Error("VR15", image, "Image alt text should 125 characters or less");
            }
        }
    }
}

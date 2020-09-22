using System;
using System.Collections.Generic;
using System.Linq;

using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace BlogValidator
{
    internal sealed class VR12_ImageAltTextShouldNotBeDuplicated : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var images = context.Document.Descendants<LinkInline>()
                                         .Where(i => i.IsImage)
                                         .GroupBy(i => i.Url);

            var altTextSet = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var imageGroup in images)
            {
                var image = imageGroup.First();
                var altText = image.FirstChild?.ToString();
                if (string.IsNullOrEmpty(altText))
                    continue;

                if (!altTextSet.Add(altText))
                    context.Warning("VR12", image, "The alt text was already used with a different URL. Copy/paste error?");
            }
        }
    }
}

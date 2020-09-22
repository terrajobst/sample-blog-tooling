using System.Linq;

using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace BlogValidator
{
    internal sealed class VR13_LinkTextShouldNotIncludeHere : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var links = context.Document.Descendants<LinkInline>()
                                        .Where(i => !i.IsImage);

            foreach (var link in links)
            {
                var text = link.FirstChild?.ToString();
                if (string.IsNullOrEmpty(text))
                    continue;

                if (text.Contains("here"))
                    context.Warning("VR13", link, "Links shouldn't include 'here' or 'click here'. Instead, describe what is being linked to.");
            }
        }
    }
}

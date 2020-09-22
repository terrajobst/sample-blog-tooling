using System;
using System.Globalization;
using System.Linq;

using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace BlogValidator
{
    internal sealed class VR10_LinkShouldNotIncludeLocale : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var links = context.Document.Descendants<LinkInline>();

            foreach (var link in links)
            {
                var url = new Uri(link.Url, UriKind.RelativeOrAbsolute);
                if (!url.IsAbsoluteUri)
                    continue;

                var isMicrosoftDotCom = url.Host.Equals("microsoft.com", StringComparison.OrdinalIgnoreCase) ||
                                        url.Host.EndsWith(".microsoft.com", StringComparison.OrdinalIgnoreCase);

                var locale = CultureInfo.GetCultures(CultureTypes.AllCultures)
                                        .Where(c => !string.IsNullOrEmpty(c.Name))
                                        .FirstOrDefault(c => url.Segments.Any(s => s.Equals(c.Name + "/", StringComparison.OrdinalIgnoreCase)));

                if (isMicrosoftDotCom && locale != null)
                    context.Error("VR10", link, $"The host '{url.Host} shouldn't use locales. Remove '{locale.Name}' from the URL.");
            }
        }
    }
}

using System;
using System.Net;

using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace BlogValidator
{
    internal sealed class VR14_LinkShouldResolve : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var links = context.Document.Descendants<LinkInline>();

            foreach (var link in links)
            {
                var url = new Uri(link.Url, UriKind.RelativeOrAbsolute);
                if (!url.IsAbsoluteUri)
                    continue;

                var request = WebRequest.Create(url);
                try
                {
                    using (request.GetResponse())
                    {
                    }
                }
                catch (Exception ex)
                {
                    context.Warning("VR14", link, $"URL '{url}' doesn't resolve: {ex.Message}");
                }
            }
        }
    }
}

using System.Linq;

using Markdig.Syntax;

namespace BlogValidator
{
    internal sealed class VR04_HeadersMustStartAtH2 : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var h1Blocks = context.Document.Descendants<HeadingBlock>()
                                           .Where(b => b.Level == 1);

            foreach (var h1 in h1Blocks)
                context.Error("VR04", h1, "Headers must start at H2");
        }
    }
}

using System.Linq;

using Markdig.Syntax;

namespace BlogValidator
{
    internal sealed class VR06_FencedCodeBlocksMustHaveLanguage: ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var unfencedBlocks = context.Document.Descendants<FencedCodeBlock>()
                                                 .Where(b => string.IsNullOrEmpty(b.Info));

            foreach (var block in unfencedBlocks)
                context.Error("VR06", block, "Fenced code blocks should specify a language");
        }
    }
}

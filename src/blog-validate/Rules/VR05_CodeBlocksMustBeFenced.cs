using System.Linq;

using Markdig.Extensions.Yaml;
using Markdig.Syntax;

namespace BlogValidator
{
    internal sealed class VR05_CodeBlocksMustBeFenced : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var unfencedBlocks = context.Document.Descendants<CodeBlock>()
                                                 .Where(b => !(b is FencedCodeBlock) &&
                                                             !(b is YamlFrontMatterBlock));

            foreach (var block in unfencedBlocks)
                context.Error("VR05", block, "You should use fenced code blocks");
        }
    }
}

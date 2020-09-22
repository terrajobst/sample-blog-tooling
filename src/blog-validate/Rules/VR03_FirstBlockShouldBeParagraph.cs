using System.Linq;

using Markdig.Syntax;

namespace BlogValidator
{
    internal sealed class VR03_FirstBlockShouldBeParagraph : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var firstBlock = context.Document.Skip(context.FrontMatter == null ? 0 : 1).FirstOrDefault();
            if (firstBlock != null)
            {
                if (!(firstBlock is ParagraphBlock))
                {
                    context.Warning("VR03", firstBlock, "First block should be a paragraph with the introduction");
                }
            }
        }
    }
}

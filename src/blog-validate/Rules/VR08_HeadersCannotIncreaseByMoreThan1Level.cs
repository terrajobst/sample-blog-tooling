using Markdig.Syntax;

namespace BlogValidator
{
    internal sealed class VR08_HeadersCannotIncreaseByMoreThan1Level : ValidationRule
    {
        public override void Validate(ValidationContext context)
        {
            var headers = context.Document.Descendants<HeadingBlock>();
            var previousLevel = 2;

            foreach (var header in headers)
            {
                var delta = header.Level - previousLevel;
                if (delta > 1)
                    context.Error("VR08", header, "Headings cannot increase by more than one level");

                previousLevel = header.Level;
            }
        }
    }
}

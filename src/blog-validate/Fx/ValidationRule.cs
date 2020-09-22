namespace BlogValidator
{
    internal abstract class ValidationRule
    {
        public abstract void Validate(ValidationContext context);
    }
}

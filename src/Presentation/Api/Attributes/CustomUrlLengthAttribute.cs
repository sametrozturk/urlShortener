using System.ComponentModel.DataAnnotations;

public class CustomUrlLengthAttribute : ValidationAttribute
{
    private readonly int _minLength = 1;
    private readonly int _maxLength = 6;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string customHash)
        {
            if (customHash.Length >= _minLength && customHash.Length <= _maxLength)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("The Custom Hash must be length of 1-6");
    }
}
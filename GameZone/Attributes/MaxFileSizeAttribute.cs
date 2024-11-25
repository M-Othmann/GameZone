namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _allowedSize;

        public MaxFileSizeAttribute(int allowedSize)
        {
            _allowedSize = allowedSize;
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {

                if (file.Length > _allowedSize)
                {
                    return new ValidationResult($"Maximum allowed is {_allowedSize} bytes");
                }
                    

            }
            return ValidationResult.Success;

        }
    }
    
    
}

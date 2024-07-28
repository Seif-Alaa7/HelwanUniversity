using Data;
using Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HelwanUniversity.Vaildations
{
    public class UniquePresidentAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var context = validationContext.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            if (context == null)
            {
                return new ValidationResult("Database context is not available.");
            }

            if (value != null)
            {
                var Jop = (JobTitle)Enum.Parse(typeof(JobTitle), value.ToString());

                if (Jop == JobTitle.President)
                {
                    var JopName = Jop.ToString();
                    var JopExists = context.HighBoards.Any(s => s.JobTitle.ToString() == JopName);

                    if (JopExists)
                    {
                        return new ValidationResult("The President Jop is already associated with another Account");
                    }
                }
            }
            return ValidationResult.Success!;
        }
    }
}

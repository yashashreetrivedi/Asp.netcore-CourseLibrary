using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class AuthorCreationDto : IValidatableObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateIfBirth { get; set; }
        public string MainCategory { get;set;}

        public ICollection<CourseCreationDto> Courses { get; set; } =
            new List<CourseCreationDto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(FirstName == MainCategory)
            {
                yield return new ValidationResult(
                    "This provided descriptino should be diggerent",
                    new[] { "AuthorCreationDto" });
            }
          
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public abstract class courseForManipulationDto
    {
        [Required(ErrorMessage ="please fill this field")]
        [MaxLength(100, ErrorMessage = "NO more than 100 characters")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "NO more than 100 characters")]
        public virtual string Description { get; set; }
    }
}

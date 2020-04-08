using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.API.Models
{
    public class CourseForUpdateDto: courseForManipulationDto
    {
      
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}

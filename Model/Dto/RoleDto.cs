using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model.Dto
{
    public class RoleDto
    {
        [Required]
        public String Name { get; set; } = null!;
    }
}

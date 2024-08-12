using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class UserDto
    {
        [Required]
        public String UserName { get; set; } = null!;
        [Required]
        public String FirstName { get; set; } = null!;
        [Required]
        public String LastName { get; set; } = null!;
        [Required, EmailAddress]
        public String Email { get; set; } = null!;
        [Required]
        public String PhoneNumber { get; set; } = null!;
    }
}

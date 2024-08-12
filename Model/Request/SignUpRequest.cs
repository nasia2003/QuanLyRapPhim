using Model.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class SignUpRequest
    {
        [Required]
        public UserDto userDto {  get; set; } = null!;
        [Required]
        public String Password { get; set; } = null!;
        [Required]
        public String ConfirmedPassword { get; set; } = null!;
    }
}

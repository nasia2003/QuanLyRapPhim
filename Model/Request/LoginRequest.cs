using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class LoginRequest
    {
        [Required]
        public String UserName { get; set; } = null!;
        [Required]
        public String Password { get; set; } = null!;
    }
}

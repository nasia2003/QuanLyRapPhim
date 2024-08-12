using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class UserResponse
    {
        public UserDto userDto { get; set; } = null!;
        public RoleDto roleDto { get; set; } = null!;
    }
}

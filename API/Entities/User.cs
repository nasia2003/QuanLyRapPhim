using API.Data;
using Microsoft.AspNetCore.Identity;
using Model.Dto;
using Model.Request;

namespace API.Entities
{
    public class User : IdentityUser<Guid>
    {
        public String FirstName { get; set; } = null!;
        public String LastName { get; set; } = null!;

        public User() 
        {
        }

        public User(UserDto userDto)
        {
            UserName = userDto.UserName;
            FirstName = userDto.FirstName;
            LastName = userDto.LastName;
            Email = userDto.Email;
            PhoneNumber = userDto.PhoneNumber;
        }

        public UserDto ToDto()
        {
            UserDto userDto = new UserDto();
            userDto.UserName = UserName!;
            userDto.FirstName = FirstName;
            userDto.LastName = LastName;
            userDto.Email = Email!;
            userDto.PhoneNumber = PhoneNumber!;
            return userDto;
        }
    }
}

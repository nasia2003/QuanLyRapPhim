using Microsoft.AspNetCore.Identity;
using Model.Dto;

namespace API.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Role() { }
        public Role(RoleDto roleDto)
        {
            Update(roleDto);
        }

        public RoleDto ToDto()
        {
            RoleDto roleDto = new RoleDto();
            roleDto.Name = Name!;
            return roleDto;
        }

        public void Update(RoleDto roleDto)
        {
            Name = roleDto.Name;
        }
    }
}

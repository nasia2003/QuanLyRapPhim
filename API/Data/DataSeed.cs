using API.Entities;
using Microsoft.AspNetCore.Identity;
using Model.Dto;

namespace API.Data
{
    public class DataSeed
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DataSeed(CinemaDbContext context,
                        UserManager<User> userManager,
                        RoleManager<Role> roleManager) 
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_context.Roles.Any())
            {
                var roleDto = new RoleDto();
                var roleList = new List<String>() { StateRole.Admin, StateRole.Employee };
                foreach (var roleName in roleList)
                {
                    roleDto.Name = roleName;
                    var roleFinal = new Role(roleDto);
                    await _roleManager.CreateAsync(roleFinal);
                }
            }

            if (!_context.Users.Any())
            {
                var password = "Abc@123";
                var user = new User()
                {
                    UserName = "admin",
                    FirstName = "Nguyễn",
                    LastName = "Dũng",
                    Email = "admin@example.com",
                    PhoneNumber = "1234567890",
                };
                await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, StateRole.Admin);
            }
        }
    }
}

using API.Entities;
using API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Model.Request;
using Model.Response;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserController(IUserRepository userRepository,
                              UserManager<User> userManager,
                              RoleManager<Role> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(Pagination query)
        {
            var result = _userRepository.GetUserList(query);
            var list = new List<UserResponse>();
            foreach(var x in result)
            {
                var rolesFromDb = await _userManager.GetRolesAsync(x);  
                var role = rolesFromDb.SingleOrDefault();
                list.Add(new UserResponse { userDto = x.ToDto(), roleDto = new RoleDto { Name = role! } });
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Danh sách User",
                Data = list
            });
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid ID)
        {
            var userFromDB = await _userManager.FindByIdAsync(ID.ToString());
            if (userFromDB == null)
                return BadRequest($"{ID} is not found!");

            var rolesFromDb = await _userManager.GetRolesAsync(userFromDB);
            var role = rolesFromDb.SingleOrDefault();
            return Ok(new UserResponse() { userDto = userFromDB.ToDto(), roleDto = new RoleDto { Name = role! } });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SignUpRequest userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new User(userRequest.userDto);
            var password = userRequest.Password;
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return BadRequest("Something's wrong");
            await _userManager.AddToRoleAsync(user, StateRole.Employee);
            return Ok(new UserResponse
            {
                userDto = user.ToDto(),
                roleDto = new RoleDto { Name = StateRole.Employee }
            });
        }

        [HttpPut]
        [Route("{ID}")]
        public async Task<IActionResult> Update([FromRoute] Guid ID, SignUpRequest userRequest)
        {
            var userFromDB = await _userManager.FindByIdAsync(ID.ToString());
            if (userFromDB == null)
                return BadRequest($"{ID} is not found!");
            var userNew = new User(userRequest.userDto);
            await _userManager.UpdateAsync(userNew);
            var rolesFromDb = await _userManager.GetRolesAsync(userFromDB);
            var role = rolesFromDb.SingleOrDefault();
            return Ok(new UserResponse() { userDto = userFromDB.ToDto(), roleDto = new RoleDto { Name = role! } });
        }

        [HttpPut]
        [Route("ChangeRole/{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> ChangeRole([FromRoute] Guid ID, [FromBody] String roleName)
        {
            var userFromDB = await _userManager.FindByIdAsync(ID.ToString());
            if (userFromDB == null)
                return BadRequest($"{ID} is not found!");
            var rolesFromDb = await _userManager.GetRolesAsync(userFromDB);
            var role = rolesFromDb.SingleOrDefault();
            if (role != roleName)
            {
                await _userManager.RemoveFromRoleAsync(userFromDB, role!);
                await _userManager.AddToRoleAsync(userFromDB, roleName);
                return Ok(new UserResponse() { userDto = userFromDB.ToDto(), roleDto = new RoleDto { Name = roleName } });
            }
            return Ok(new UserResponse() { userDto = userFromDB.ToDto(), roleDto = new RoleDto { Name = role! } });
        }

        [HttpDelete]
        [Route("{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Delete([FromRoute] Guid ID)
        {
            var userFromDB = await _userManager.FindByIdAsync(ID.ToString());
            if (userFromDB == null)
                return BadRequest($"{ID} is not found!");
            var rolesFromDB = await _userManager.GetRolesAsync(userFromDB);
            var role = rolesFromDB.SingleOrDefault();
            await _userManager.RemoveFromRoleAsync(userFromDB, role!);
            return Ok(new UserResponse() { userDto = userFromDB.ToDto(), roleDto = new RoleDto { Name = role! } });
        }
    }
}

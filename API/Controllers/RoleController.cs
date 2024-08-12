using API.Entities;
using API.Implement;
using API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _roleRepository.GetRoleList();
            var roleDtoList = list.Select(x => x.ToDto()).ToList();
            return Ok(roleDtoList);
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid ID)
        {
            var role = await _roleRepository.GetByID(ID);
            if (role == null)
                return BadRequest($"{ID} is not found!");
            return Ok(role.ToDto());
        }

        [HttpPost]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var role = new Role(roleDto);
            var roleNew = await _roleRepository.Create(role);
            return Ok(roleNew.ToDto());
        }

        [HttpPut]
        [Route("{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Update([FromRoute] Guid ID, RoleDto roleDto)
        {
            var roleFromDB = await _roleRepository.GetByID(ID);
            if (roleFromDB == null)
                return BadRequest($"{ID} is not found!");
            roleFromDB.Update(roleDto);
            var roleNew = await _roleRepository.Update(roleFromDB);
            return Ok(roleNew.ToDto());
        }

        [HttpDelete]
        [Route("{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Delete([FromRoute] Guid ID)
        {
            var roleFromDB = await _roleRepository.GetByID(ID);
            if (roleFromDB == null)
                return BadRequest($"{ID} is not found!");
            var roleNew = await _roleRepository.Delete(roleFromDB);
            return Ok(roleNew.ToDto());
        }
    }
}

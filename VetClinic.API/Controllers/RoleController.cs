﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.Role;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            RoleManager = roleManager;
            Mapper = mapper;
        }

        public RoleManager<IdentityRole> RoleManager { get; }
        public IMapper Mapper { get; }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var roles = RoleManager.Roles;
            IEnumerable<RoleDto> roleDtos = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleDto>>(roles);

            return Ok(roleDtos);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            RoleDto roleDto = Mapper.Map<IdentityRole, RoleDto>(role);

            return Ok(roleDto);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateRoleDto dto)
        {
            IdentityRole role = Mapper.Map<CreateRoleDto, IdentityRole>(dto);

            _ = await RoleManager.CreateAsync(role);

            return Created("/roles/", dto);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> PutAsync(string id, [FromBody] CreateRoleDto dto)
        {
            IdentityRole inputRole = Mapper.Map<CreateRoleDto, IdentityRole>(dto);

            var role = await RoleManager.FindByIdAsync(id);

            if(role == null)
            {
                return NotFound();
            }

            role.Name = inputRole.Name;
            role.NormalizedName = inputRole.Name.ToUpper();

            _ = await RoleManager.UpdateAsync(role);

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            _ = await RoleManager.DeleteAsync(role);

            return NoContent();
        }
    }
}

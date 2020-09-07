namespace ApiRest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Constants;
    using Models.Enums;
    using Repository.Entities;
    using Repository.Interfaces;

    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Policy = Policies.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IUserRepository _userRepository;

        public AdminController(
            IMapper autoMapper,
            IUserRepository userRepository)
        {
            _autoMapper = autoMapper;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserModel model)
        {
            var data = _autoMapper.Map<User>(model);

            _userRepository.Add(data);

            if (await _userRepository.SaveChangesAsync())
            {
                return Created("", data);
            }
            else
            {
                return BadRequest("Failed to create new User.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<UserModel[]>> Get()
        {
            var data = await _userRepository.GetAuthUsersAsync(includeRole: true, includeBalances: true);

            return _autoMapper.Map<UserModel[]>(data);
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<RoleModel[]>> GetRoles()
        {
            var data = await _userRepository.GetRolesAsync();

            return _autoMapper.Map<RoleModel[]>(data);
        }
    }
}
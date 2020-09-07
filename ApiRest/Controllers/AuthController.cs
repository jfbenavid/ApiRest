namespace ApiRest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
    using Models;
    using Models.Constants;
    using Models.Enums;
    using Repository.Entities;
    using Repository.Interfaces;

    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Policy = Policies.Admin)]
    public class AuthController : ControllerBase
    {
        private readonly JwtConfigModel _jwt;
        private readonly IAuthUserRepository _userRepository;
        private readonly IJwtUtils _utilities;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _autoMapper;

        public AuthController(
            IOptions<JwtConfigModel> jwt,
            IAuthUserRepository userRepository,
            IJwtUtils utilities,
            LinkGenerator linkGenerator,
            IMapper autoMapper)
        {
            _jwt = jwt.Value;
            _userRepository = userRepository;
            _utilities = utilities;
            _linkGenerator = linkGenerator;
            _autoMapper = autoMapper;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserModel model)
        {
            IActionResult response = Unauthorized();

            var user = await _userRepository.GetAuthUserAsync(model.Username, model.Password);

            if (user != null)
            {
                return Ok(new
                {
                    access_token = _utilities.GenerateJwt(user),
                });
            }

            return response;
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
            var data = await _userRepository.GetAuthUsersAsync(includeRoles: true);

            return _autoMapper.Map<UserModel[]>(data);
        }

        [HttpPatch("{user}/{newRole}")]
        public async Task<IActionResult> Patch(string user, [EnumDataType(typeof(Roles))] int newRole)
        {
            try
            {
                var authUser = await _userRepository.GetAuthUserAsync(user);
                if (authUser == null)
                {
                    return BadRequest("User not Found.");
                }

                await _userRepository.ChangeRoleAsync(user, newRole);

                if (await _userRepository.SaveChangesAsync())
                {
                    return Ok("Updated");
                }
                else
                {
                    return BadRequest("Failed to update the resource; please review the role you are using is not already configurated for the user.");
                }
            }
            catch (Exception)
            {
                return NotFound("The user could not be found.");
            }
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<RoleModel[]>> GetRoles()
        {
            var data = await _userRepository.GetRolesAsync();

            return _autoMapper.Map<RoleModel[]>(data);
        }
    }
}
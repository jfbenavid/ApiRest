namespace ApiRest.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Constants;
    using Models.Enums;
    using Repository.Interfaces;

    [ApiController]
    [Route("api/users/{user}/[Controller]")]
    [Authorize(Policy = Policies.NonAdmin)]
    public class RolesController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public RolesController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPatch("{newRole}")]
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
    }
}
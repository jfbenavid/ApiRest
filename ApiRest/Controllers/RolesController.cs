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

    /// <summary>
    /// Controller to manage the roles for the users.
    /// </summary>
    [ApiController]
    [Route("api/users/{username}/[Controller]")]
    [Authorize(Policy = Policies.NonAdmin)]
    public class RolesController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Creates a new instance of <see cref="RolesController"/>
        /// </summary>
        public RolesController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Changes the role for a user.
        /// </summary>
        [HttpPatch("{newRole}")]
        public async Task<IActionResult> Patch(string username, [EnumDataType(typeof(Roles))] int newRole)
        {
            try
            {
                var authUser = await _userRepository.GetUserInfoAsync(username);
                if (authUser == null)
                {
                    return BadRequest("User not Found.");
                }

                await _userRepository.ChangeRoleAsync(username, newRole);

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
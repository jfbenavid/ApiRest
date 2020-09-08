namespace ApiRest.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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
        /// <param name="username">Username which will be updated.</param>
        /// <param name="newRole">Id of the new role to assign.</param>
        [HttpPatch("{newRole}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                    return Ok();
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
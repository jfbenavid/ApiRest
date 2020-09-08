namespace ApiRest.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Constants;
    using Repository.Entities;
    using Repository.Interfaces;

    /// <summary>
    /// Controller to manage all related to user and roles.
    /// This is only accessible with an administrator grant.
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Policy = Policies.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Creates a new instance of <see cref="AdminController"/>.
        /// </summary>
        public AdminController(
            IMapper autoMapper,
            IUserRepository userRepository)
        {
            _autoMapper = autoMapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Creates a <see cref="User"/> in the database.
        /// </summary>
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

        /// <summary>
        /// Gets all the information related to <see cref="User"/>, included roles and balances.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<UserModel[]>> Get()
        {
            var data = await _userRepository.GetAuthUsersAsync(includeRole: true, includeBalances: true);

            return _autoMapper.Map<UserModel[]>(data);
        }

        /// <summary>
        /// Gets all the <see cref="Role"/> in the database.
        /// </summary>
        [HttpGet("Roles")]
        public async Task<ActionResult<RoleModel[]>> GetRoles()
        {
            var data = await _userRepository.GetRolesAsync();

            return _autoMapper.Map<RoleModel[]>(data);
        }
    }
}
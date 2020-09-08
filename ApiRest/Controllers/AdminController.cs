namespace ApiRest.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
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
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// Creates a new instance of <see cref="AdminController"/>.
        /// </summary>
        public AdminController(
            IMapper autoMapper,
            IUserRepository userRepository,
            LinkGenerator linkGenerator)
        {
            _autoMapper = autoMapper;
            _userRepository = userRepository;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Creates a <see cref="User"/> in the database.
        /// </summary>
        /// <param name="model">Information about the user to create.</param>
        /// <remarks>
        /// SAMPLE REQUEST \
        /// POST api/Admin \
        /// { \
        ///     "email": "email",
        ///     "roleId": 1,
        ///     "username": "name",
        ///     "password": "pass"
        /// }
        /// </remarks>
        /// <response code="200">Returns information created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserModel>> Post(CreateUserModel model)
        {
            var user = await _userRepository.GetUserInfoAsync(model.Username);
            if (user != null)
            {
                return BadRequest("The user already exists.");
            }

            var data = _autoMapper.Map<User>(model);
            var roleTask = _userRepository.GetRoleByIdAsync(data.RoleId);
            _userRepository.Add(data);

            if (await _userRepository.SaveChangesAsync())
            {
                data.Role = await roleTask;
                var url = _linkGenerator.GetPathByAction(HttpContext, "Get");
                return Created(url, _autoMapper.Map<UserModel>(data));
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserModel[]>> Get()
        {
            var data = await _userRepository.GetAllUsersAsync(includeRole: true, includeBalances: true);

            return _autoMapper.Map<UserModel[]>(data);
        }

        /// <summary>
        /// Gets all the <see cref="Role"/> in the database.
        /// </summary>
        [HttpGet("Roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RoleModel[]>> GetRoles()
        {
            var data = await _userRepository.GetRolesAsync();

            return _autoMapper.Map<RoleModel[]>(data);
        }
    }
}
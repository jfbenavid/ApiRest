namespace ApiRest.Controllers
{
    using System.Threading.Tasks;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Models;
    using Repository.Interfaces;

    /// <summary>
    /// Controller to manage all related to Login in the api.
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _utilities;

        /// <summary>
        /// Creates a new instance of <see cref="AuthController"/>.
        /// </summary>
        public AuthController(
            IUserRepository userRepository,
            IJwtUtils utilities)
        {
            _userRepository = userRepository;
            _utilities = utilities;
        }

        /// <summary>
        /// Creates and returns a Jwt to access the endpoints.
        /// </summary>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            IActionResult response = Unauthorized();

            var user = await _userRepository.GetUserAsync(model.Username, model.Password);

            if (user != null)
            {
                return Ok(new
                {
                    access_token = _utilities.GenerateJwt(user),
                });
            }

            return response;
        }
    }
}
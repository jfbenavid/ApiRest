namespace ApiRest.Controllers
{
    using System.Threading.Tasks;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Models;
    using Repository.Interfaces;

    /// <summary>
    /// comment test
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _utilities;

        public AuthController(
            IUserRepository userRepository,
            IJwtUtils utilities)
        {
            _userRepository = userRepository;
            _utilities = utilities;
        }

        /// <summary>
        /// Returns a Jwt to access all the endpoints.
        /// </summary>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
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
    }
}
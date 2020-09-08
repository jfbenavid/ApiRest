namespace ApiRest.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
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
        private readonly JwtConfigModel _jwtConfig;

        /// <summary>
        /// Creates a new instance of <see cref="AuthController"/>.
        /// </summary>
        public AuthController(
            IUserRepository userRepository,
            IJwtUtils utilities,
            IOptions<JwtConfigModel> jwtConfig)
        {
            _userRepository = userRepository;
            _utilities = utilities;
            _jwtConfig = jwtConfig.Value;
        }

        /// <summary>
        /// Creates and returns a Jwt to access the endpoints.
        /// </summary>
        /// <param name="model">Needed information to login and get the token.</param>
        /// <response code="200">Returns the json web token.</response>
        /// <response code="401">The information provided does not match with the database info.</response>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginModel model)
        {
            IActionResult response = Unauthorized();

            var user = await _userRepository.GetUserAsync(model.Username, model.Password);

            if (user != null)
            {
                return Ok(new JwtModel
                {
                    AccessToken = _utilities.GenerateJwt(user),
                    ExpirationTime = TimeSpan.FromMinutes(_jwtConfig.MinutesAlive)
                });
            }

            return response;
        }
    }
}
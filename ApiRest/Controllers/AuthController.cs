namespace ApiRest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Models;
    using Repository.Interfaces;

    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtConfigModel _jwt;
        private readonly IAuthUserRepository _userRepository;

        public AuthController(IOptions<JwtConfigModel> jwt, IAuthUserRepository userRepository)
        {
            _jwt = jwt.Value;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthModel model)
        {
            IActionResult response = Unauthorized();

            var user = await _userRepository.GetAuthUserAsync(model.Username, model.Password);

            if (user != null)
            {
                return Ok(new
                {
                    access_token = Utilities.GenerateJwt(user, _jwt),
                });
            }

            return response;
        }
    }
}
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
    }
}
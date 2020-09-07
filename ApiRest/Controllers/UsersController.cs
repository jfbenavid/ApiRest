namespace ApiRest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Constants;

    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Policy = Policies.NonAdmin)]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Public message!");
        }
    }
}
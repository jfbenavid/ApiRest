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
    using Models.Enums;

    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Policy = Policies.Admin)]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Secret Message!");
        }
    }
}
namespace ApiRest.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Constants;
    using Repository.Entities;
    using Repository.Interfaces;

    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Policy = Policies.NonAdmin)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(
            IUserRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("balances/all/{username?}")]
        public async Task<ActionResult<BalanceSheetModel[]>> Get(string username = null)
        {
            username ??= GetCurrentUserName();
            var balances = await _repository.GetBalanceSheetsAsync(username);

            return _mapper.Map<BalanceSheetModel[]>(balances);
        }

        [HttpGet("balances/{username?}")]
        public async Task<ActionResult<BalanceSheetTransferModel>> GetCurrentBalance(string username = null)
        {
            username ??= GetCurrentUserName();
            var balances = await _repository.GetBalanceSheetsAsync(username);

            var balance = balances.Sum(b => b.Amount);

            return new BalanceSheetTransferModel
            {
                Amount = balance,
                Username = username
            };
        }

        [HttpPost]
        public async Task<IActionResult> TransferBalance(BalanceSheetTransferModel model)
        {
            if (model.Amount <= 0)
            {
                return BadRequest("The amount to transfer cannot be less or equals to zero.");
            }

            var user = await _repository.GetAuthUserAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var balanceSheet = new BalanceSheet
            {
                Amount = model.Amount,
                UserId = user.UserId
            };

            _repository.Add(balanceSheet);

            if (await _repository.SaveChangesAsync())
            {
                return Created("", balanceSheet);
            }
            else
            {
                return BadRequest("The Balance could not be transfered.");
            }
        }

        private string GetCurrentUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToArray();
            return claims[0].Value;
        }
    }
}
namespace ApiRest.Controllers
{
    using System.Linq;
    using System.Security.Claims;
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
    /// Controller to manage the Users.
    /// </summary>
    [ApiController]
    [Route("api/Users/{username?}/[Controller]")]
    [Authorize(Policy = Policies.NonAdmin)]
    public class BalancesController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// Creates a new instance of <see cref="BalancesController"/>.
        /// </summary>
        public BalancesController(
            IUserRepository repository,
            IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Gets all the balances for a <see cref="User"/>.
        /// </summary>
        /// <param name="username">Username which you want to get the information.</param>
        [HttpGet("all")]
        public async Task<ActionResult<BalanceSheetModel[]>> Get(string username = null)
        {
            username ??= GetCurrentUserName();
            var balances = await _repository.GetBalanceSheetsByUsernameAsync(username);

            return _mapper.Map<BalanceSheetModel[]>(balances);
        }

        /// <summary>
        /// Gets the total amount of the balances for a <see cref="User"/>.
        /// </summary>
        /// <param name="username">Username which you want to get the information.</param>
        [HttpGet]
        public async Task<ActionResult<BalanceSheetTransferModel>> GetCurrentBalance(string username = null)
        {
            username ??= GetCurrentUserName();
            var balances = await _repository.GetBalanceSheetsByUsernameAsync(username);

            var balance = balances.Sum(b => b.Amount);

            return new BalanceSheetTransferModel
            {
                Amount = balance,
                Username = username
            };
        }

        /// <summary>
        /// Creates a new <see cref="BalanceSheet"/> for a <see cref="User"/>.
        /// </summary>
        /// <param name="username">Username to transfer the balance.</param>
        /// <param name="model">Information to transfer.</param>
        /// <remarks>
        /// SAMPLE REQUEST \
        /// POST /api/Users/1/Balances \
        /// { \
        ///     "amount": 150
        /// }
        /// </remarks>
        /// <response code="200">Information created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceSheetTransferModel))]
        public async Task<IActionResult> TransferBalance(string username, BalanceSheetTransferModel model)
        {
            if (model.Amount == 0)
            {
                return BadRequest("The amount to transfer cannot be less or equals to zero.");
            }

            var user = await _repository.GetUserInfoAsync(username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var balanceSheet = new BalanceSheet
            {
                Amount = model.Amount,
                UserId = user.UserId,
            };

            _repository.Add(balanceSheet);

            if (await _repository.SaveChangesAsync())
            {
                balanceSheet.User = user;
                var uri = _linkGenerator.GetPathByAction(HttpContext, "Get", values: new { username });
                return Created(uri, _mapper.Map<BalanceSheetTransferModel>(balanceSheet));
            }
            else
            {
                return BadRequest("The Balance could not be transfered.");
            }
        }

        /// <summary>
        /// Deletes a BalanceSheet from the database.
        /// </summary>
        /// <param name="balanceId">Id for the balance to delete.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int balanceId)
        {
            var balance = await _repository.GetBalanceSheetByIdAsync(balanceId);
            if (balance == null)
            {
                return NotFound("Failed to find the balance to delete.");
            }

            _repository.Delete(balance);

            if (await _repository.SaveChangesAsync())
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to delete the balance.");
            }
        }

        /// <summary>
        /// Gets and returns the username from the claims in the header.
        /// </summary>
        private string GetCurrentUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToArray();
            return claims[0].Value;
        }
    }
}
using CognitiveComplexity.Examples.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.CQRS.Commands.RegisterUser;
using Service.CQRS.Commands.RegisterUserResolved;
using Shared.ResponseObjects;

namespace CognitiveComplexity.Examples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register/complex")]
        public async Task<IActionResult> RegisterUserComplex([FromBody] RegisterUserComplexCommand command)
        {
            var result = await _mediator.Send(command);
            return ActionResult(result);
        }

        [HttpPost("register/resolved")]
        public async Task<IActionResult> RegisterUserResolved([FromBody] RegisterUserResolvedCommand command)
        {
            var result = await _mediator.Send(command);
            return ActionResult(result);
        }
    }
}

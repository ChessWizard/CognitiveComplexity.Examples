using Microsoft.AspNetCore.Mvc;
using Shared.ResponseObjects;

namespace CognitiveComplexity.Examples.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController
    {
        public IActionResult ActionResult<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.HttpStatusCode
            };
        }
    }
}

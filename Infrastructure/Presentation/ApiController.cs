
using Shared.ErrorModel;
using System.Net;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")] //baseUrl/api/basket
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
    public class ApiController : ControllerBase
    {
        
    }
}

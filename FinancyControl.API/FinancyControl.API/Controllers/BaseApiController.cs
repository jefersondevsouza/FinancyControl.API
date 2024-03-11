using Microsoft.AspNetCore.Mvc;

namespace FinancyControl.API.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}

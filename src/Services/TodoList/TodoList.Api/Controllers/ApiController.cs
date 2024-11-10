using Microsoft.AspNetCore.Mvc;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}

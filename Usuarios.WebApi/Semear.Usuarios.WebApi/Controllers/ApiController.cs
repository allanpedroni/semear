using Microsoft.AspNetCore.Mvc;

namespace Semear.Usuarios.WebApi.Controllers
{
    //TODO: To refactor ApiController
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class ApiController : NonVersionedApiController
    {
    }
}

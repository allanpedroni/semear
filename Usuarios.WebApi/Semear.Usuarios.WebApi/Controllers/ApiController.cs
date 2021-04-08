using Microsoft.AspNetCore.Mvc;

namespace Semear.Usuarios.WebApi.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class ApiController : NonVersionedApiController
    {
    }
}

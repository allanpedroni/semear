using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Semear.Usuarios.WebApi.Controllers
{
    [ApiController, Authorize]
    public abstract class NonVersionedApiController : ControllerBase
    {
    }
}

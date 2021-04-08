using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Semear.Autenticacao.WebApi.Controllers
{
    [ApiController, Authorize]
    public abstract class NonVersionedApiController : ControllerBase
    {
    }
}

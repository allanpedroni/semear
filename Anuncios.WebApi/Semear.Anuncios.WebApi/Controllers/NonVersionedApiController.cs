using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Semear.Anuncios.WebApi.Controllers
{
    //TODO: To refactor NonVersionedApiController
    [ApiController, Authorize]
    public abstract class NonVersionedApiController : ControllerBase
    {
    }
}

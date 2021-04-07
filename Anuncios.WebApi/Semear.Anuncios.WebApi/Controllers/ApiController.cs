using Microsoft.AspNetCore.Mvc;

namespace Semear.Anuncios.WebApi.Controllers
{
    //TODO: To refactor ApiController
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class ApiController : NonVersionedApiController
    {
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Semear.Anuncios.WebApi.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class ApiController : NonVersionedApiController
    {
    }
}

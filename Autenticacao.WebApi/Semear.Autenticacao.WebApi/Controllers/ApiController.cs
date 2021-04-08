using Microsoft.AspNetCore.Mvc;

namespace Semear.Autenticacao.WebApi.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class ApiController : NonVersionedApiController
    {
    }
}

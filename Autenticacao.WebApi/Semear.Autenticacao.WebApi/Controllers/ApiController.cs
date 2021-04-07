using Microsoft.AspNetCore.Mvc;

namespace Semear.Autenticacao.WebApi.Controllers
{
    //TODO: To refactor ApiController
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class ApiController : NonVersionedApiController
    {
    }
}

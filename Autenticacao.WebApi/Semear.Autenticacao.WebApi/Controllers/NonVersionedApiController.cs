﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Semear.Autenticacao.WebApi.Controllers
{
    //TODO: To refactor NonVersionedApiController
    [ApiController, Authorize]
    public abstract class NonVersionedApiController : ControllerBase
    {
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.IApplication;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Minem.Tupa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController(ISectorApplication service) : ControllerBase
    {
        private readonly ISectorApplication _service = service;

        [AllowAnonymous]
        [HttpGet("sector")]
        public async Task<ActionResult> ObtenerSectores()
        {
            var respuesta = await _service.ObtenerSectores();
            return Ok(respuesta);
        }

    }
}

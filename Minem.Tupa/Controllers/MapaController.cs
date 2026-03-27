using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Application;
using Minem.Tupa.Dto.Observacion;
using Minem.Tupa.IApplication;

namespace Minem.Tupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapaController(IMapaApplication service) : ControllerBase
    {
        private readonly IMapaApplication _service = service;

        [AllowAnonymous]
        [HttpGet("tipo-actividad")]
        public async Task<ActionResult> ObtenerTipoActividad([FromQuery] int tipo)
        {
            var respuesta = await _service.ObtenerTipoActividad(tipo);
            return Ok(respuesta);
        }


    }
}

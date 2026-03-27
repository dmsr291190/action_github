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
    public class ObservacionController(IObservacionApplication service) : ControllerBase
    {
        private readonly IObservacionApplication _service = service;

        [AllowAnonymous]
        [HttpPost("insertar-observacion")]
        public async Task<ActionResult> InsertarObservacion([FromBody] ObservacionRequestDto request)
        {
            var respuesta = await _service.InsertarObservacion(request);
            return Ok(respuesta);
        }
 
        [AllowAnonymous]
        [HttpGet("consulta-observacion")]
        public async Task<ActionResult> ConsultaObservacion([FromQuery] int Codmaesolicitud, string capitulo, int Iddetobshistjsonpadre)
        {
            var respuesta = await _service.ConsultaObshistjson(Codmaesolicitud, capitulo, Iddetobshistjsonpadre);
            return Ok(respuesta);
        }

        [HttpPut("update-observacion")]
        public async Task<ActionResult> ActualizarObservacion([FromBody] ObservacionRequestDto request)
        {
            var respuesta = await _service.ActualizarObservacion(request);
            return Ok(respuesta);
        }

        [HttpDelete("delete-observacion")]
        public async Task<ActionResult> EliminarObservacion([FromQuery] int Iddetobshistjson)
        {
            //request.RegUsuaRegistra = User.Identity.GetUserId();
            var respuesta = await _service.EliminarObservacion(Iddetobshistjson);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("observacion-solicitud")]
        public async Task<ActionResult> ObservacionesSolicitud([FromQuery] int Codmaesolicitud)
        {
            var respuesta = await _service.ObservacionesSolicitud(Codmaesolicitud);
            return Ok(respuesta);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Application;
using Minem.Tupa.Dto.AutorizacionQuemaGas;
using Minem.Tupa.IApplication;

namespace Minem.Tupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacionQuemaGasController(IAutorizacionQuemaGasApplication service) : ControllerBase
    {
        private readonly IAutorizacionQuemaGasApplication _service = service;

        [AllowAnonymous]
        [HttpPut("actualizar-aqg-informe")]
        public async Task<ActionResult> ActualizarInformeJustificacion([FromBody] InformeJustificacionDto request)
        {
            var response = await _service.GuardarInforme(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("insertar-aqg-informe")]
        public async Task<ActionResult> InsertarInformeJustificacion([FromBody] InformeJustificacionDto request)
        {
            var response = await _service.GuardarInforme(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("informe/{solicitudId}")]
        public async Task<ActionResult> ObtenerInforme(long solicitudId)
        {
            var response = await _service.ObtenerInforme(solicitudId);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("lotes")]
        public async Task<ActionResult> ObtenerLotes()
        {
            var response = await _service.ObtenerLotes();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("motivos")]
        public async Task<ActionResult> ObtenerMotivos()
        {
            var response = await _service.ObtenerMotivos();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("buscar-quemador")]
        public async Task<ActionResult> ObtenerQuemadorPorSerie([FromQuery] BuscarQuemadorRequestDto request)
        {
            var response = await _service.ObtenerQuemadorPorSerie(request.usuarioId, request.serie, request.nombre);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("obtener-cantidad-observaciones/{informeId}")]
        public async Task<ActionResult> ObtenerCantidadObservaciones(long informeId)
        {
            var response = await _service.ObtenerCantidadObservaciones(informeId);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("obtener-observacion-capitulo/{idInforme}/{capitulo}")]
        public async Task<ActionResult> ObtenerProyectosObservacionCapitulo(long idInforme, string capitulo)
        {
            var respuesta = await _service.ObtenerProyectosObservacionCapitulo(idInforme, capitulo);
            return Ok(respuesta);
        }
    }
}

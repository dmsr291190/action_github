using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Application;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.IApplication;
using System.Threading.Tasks;

namespace Minem.Tupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItsController(IItsApplication service) : ControllerBase
    {
        private readonly IItsApplication _service = service;

        [AllowAnonymous]
        [HttpPost("insertar-its-proyecto")]
        public async Task<ActionResult> InsertarProyectoITS([FromBody] ItsProyectoDto request)
        {
            var respuesta = await _service.InsertarProyectoITS(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-proyecto")]
        public async Task<ActionResult> ObtenerProyecto([FromQuery] ItsProyectoDto request)
        {
            var respuesta = await _service.ObtenerProyecto(request);
            return Ok(respuesta);
        }
        
        [AllowAnonymous]
        [HttpPost("insertar-its-proyecto-archivo")]
        public async Task<ActionResult> InsertarProyectoArchivoITS([FromBody] ItsProyectoArchivoDto request)
        {
            var respuesta = await _service.InsertarProyectoArchivoITS(request);

            if (respuesta.Data != 0)
            {


            }
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-proyecto-archivos")]
        public async Task<ActionResult> ObtenerProyectoArchivos([FromQuery] ItsProyectoArchivoDto request)
        {
            var respuesta = await _service.ObtenerProyectoArchivos(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("eliminar-proyecto-archivos")]
        public async Task<ActionResult> EliminarProyectoArchivos([FromBody] ItsProyectoArchivoEliminarDto request)
        {
            var respuesta = await _service.EliminarProyectoArchivos(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("insertar-its-representante")]
        public async Task<ActionResult> InsertarRepresentanteITS([FromBody] ItsProyectoRepresentanteDto request)
        {
            var respuesta = await _service.InsertarRepresentanteITS(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-representante")]
        public async Task<ActionResult> ObtenerRepresentante([FromQuery] ItsProyectoRepresentanteDto request)
        {
            var respuesta = await _service.ObtenerRepresentante(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("insertar-its-profesional")]
        public async Task<ActionResult> InsertarProfesionalITS([FromBody] ItsProyectoProfesionalDto request)
        {
            var respuesta = await _service.InsertarProfesionalITS(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-profesional")]
        public async Task<ActionResult> ObtenerProfesional([FromQuery] ItsProyectoProfesionalDto request)
        {
            var respuesta = await _service.ObtenerProfesional(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("eliminar-profesional")]
        public async Task<ActionResult> EliminarProfesional([FromBody] ItsProyectoProfesionalDto request)
        {
            var respuesta = await _service.EliminarProfesional(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-padre")]
        public async Task<ActionResult> ObtenerPadre([FromQuery] long CodMaeSolicitud)
        {
            var respuesta = await _service.ObtenerPadre(CodMaeSolicitud);
            return Ok(respuesta);
        }
        
        [AllowAnonymous]
        [HttpPost("insertar-its-mapa")]
        public async Task<ActionResult> InsertarMapa([FromBody] ItsMapaDto request)
        {
            var respuesta = await _service.InsertarMapa(request);
            return Ok(respuesta);
        }
        
        [AllowAnonymous]
        [HttpPut("actualizar-its-mapa")]
        public async Task<ActionResult> ActualizarMapa([FromBody] ItsMapaDto request)
        {
            var respuesta = await _service.ActualizarMapa(request);
            return Ok(respuesta);
        }
        
        [AllowAnonymous]
        [HttpGet("obtener-its-mapa")]
        public async Task<ActionResult> ObtenerMapa([FromQuery] int IdProyecto)
        {
            var respuesta = await _service.ObtenerMapa(IdProyecto);
            return Ok(respuesta);
        }
        [AllowAnonymous]
        [HttpGet("obtener-its-mapa-con-solicitud")]
        public async Task<ActionResult> ObtenerMapaConSolicitud([FromQuery] int codMaeSolicitud)
        {
            var respuesta = await _service.ObtenerMapaConSolicitud(codMaeSolicitud);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpDelete("eliminar-its-mapa")]
        public async Task<ActionResult> EliminarMapa([FromQuery] int IdProyecto)
        {
            var respuesta = await _service.EliminarMapa(IdProyecto);
            return Ok(respuesta);
        }


        [AllowAnonymous]
        [HttpGet("observacion/por-proyecto-capitulo/{idProyecto}/{capitulo}")]
        public async Task<ActionResult> ObtenerProyectosObservacionCapitulo(long idProyecto, string capitulo)
        {
            var respuesta = await _service.ObtenerProyectosObservacionCapitulo(idProyecto, capitulo);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("observacion/total-por-proyecto-capitulo/{idProyecto}")]
        public async Task<ActionResult> ObtenerProyectoTotalObservacionesPorCapitulo(long idProyecto)
        {
            var respuesta = await _service.ObtenerProyectoTotalObservacionesPorCapitulo(idProyecto);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-reunion-its/{codMaeSolicitud}")]
        public async Task<ActionResult> ObtenerReunionIts(long codMaeSolicitud)
        {
            var respuesta = await _service.ObtenerReunionIts(codMaeSolicitud);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-respuestas-observacion-its/{codMaeObservacion}")]
        public async Task<ActionResult> ObtenerRespuestasObservacionIts(long codMaeObservacion)
        {
            var respuesta = await _service.ObtenerRespuestasObservacionIts(codMaeObservacion);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("insertar-reunion-adjunto")]
        public async Task<ActionResult> InsertarReunionAdjunto([FromBody] ItsReunionAdjuntoDto request)
        {
            var respuesta = await _service.InsertarReunionAdjunto(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-reunion-adjuntos/{idReunion}")]
        public async Task<ActionResult> ObtenerReunionAdjuntos(int idReunion)
        {
            var respuesta = await _service.ObtenerReunionAdjuntos(idReunion);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("eliminar-reunion-adjunto")]
        public async Task<ActionResult> EliminarReunionAdjunto([FromBody] ItsReunionAdjuntoEliminarDto request)
        {
            var respuesta = await _service.EliminarReunionAdjuntos(request);
            return Ok(respuesta);
        }
    }
}

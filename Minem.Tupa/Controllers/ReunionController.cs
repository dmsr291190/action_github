using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Application;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Dto.Reunion;
using Minem.Tupa.IApplication;
using System.Threading.Tasks;

namespace Minem.Tupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReunionController(IReunionApplication service) : ControllerBase
    {
        private readonly IReunionApplication _service = service;

        // --- REUNION_SOLICITUD ---
        [AllowAnonymous]
        [HttpPost("insertar-reunion-solicitud")]
        public async Task<ActionResult> InsertarReunionSolicitud([FromBody] ReunionSolicitudDto request)
        {
            var respuesta = await _service.InsertarSolicitudReunion(request);

            if (respuesta.Data != 0)
            {
                foreach (var rep in request.representantesTitular)
                {
                    await _service.InsertarReunionParticipante(respuesta.Data, "Titular-Minero", request.usuarioregistra, rep); //request.idreunionsolicitud
                }

                foreach (var rep in request.representantesConsultora)
                {
                    await _service.InsertarReunionParticipante(respuesta.Data, "Consultora", request.usuarioregistra, rep); //request.idreunionsolicitud
                }

                foreach (var rep in request.correos)
                {
                    await _service.InsertarReunionCorreo(respuesta.Data, request.usuarioregistra, rep); //request.idreunionsolicitud
                }

                foreach (var rep in request.objetivos)
                {
                    await _service.InsertarReunionObjetico(respuesta.Data, request.usuarioregistra, rep); //request.idreunionsolicitud
                }

            }
            


            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-reunion")]
        public async Task<ActionResult> ObtenerReunion([FromQuery] ReunionRequestDto request)
        {
            var respuesta = await _service.ObtenerReunion(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("historial-reuniones")]
        public async Task<ActionResult> GetHistorialReuniones([FromQuery] long CodMaeSolicitud)
        {
            var respuesta = await _service.GetHistorialReuniones(CodMaeSolicitud);
            return Ok(respuesta);
        }

        /*
        [HttpPut("actualizar-reunion-solicitud")]
        public async Task<ActionResult> ActualizarReunionSolicitud([FromBody] ReunionSolicitudDto request)
        {
            var respuesta = await _service.ActualizarReunionSolicitud(request);
            return Ok(respuesta);
        }

        [HttpPut("actualizar-estado-reunion-solicitud")]
        public async Task<ActionResult> ActualizarEstadoReunionSolicitud([FromQuery] long IdReunionSolicitud, [FromQuery] long UsuarioModifica)
        {
            //var respuesta = await _service.ActualizarEstadoReunionSolicitud(IdReunionSolicitud, UsuarioModifica);
            //return Ok(respuesta);
            return null;
        }

        [HttpGet("obtener-reunion-solicitud")]
        public async Task<ActionResult> ObtenerReunionSolicitud([FromQuery] long IdReunionSolicitud)
        {
            var respuesta = await _service.ObtenerReunionSolicitud(IdReunionSolicitud);
            return Ok(respuesta);
        }

        [HttpGet("obtener-reunion-solicitud-por-codmaesolicitud")]
        public async Task<ActionResult> ObtenerReunionSolicitudPorCodMaeSolicitud([FromQuery] long CodMaeSolicitud)
        { /*
            var respuesta = await _service.ObtenerReunionSolicitudPorCodMaeSolicitud(CodMaeSolicitud);
            return Ok(respuesta);
            */ /*
            return null;
        }

        // --- REUNION_PARTICIPANTE ---
        [HttpPost("insertar-reunion-participante")]
        public async Task<ActionResult> InsertarReunionParticipante([FromBody] ReunionParticipanteDto request)
        {
            var respuesta = await _service.InsertarReunionParticipante(request);
            return Ok(respuesta);
        }

        [HttpPut("actualizar-reunion-participante")]
        public async Task<ActionResult> ActualizarReunionParticipante([FromBody] ReunionParticipanteDto request)
        {
            var respuesta = await _service.ActualizarReunionParticipante(request);
            return Ok(respuesta);
        }

        [HttpPut("actualizar-estado-reunion-participante")]
        public async Task<ActionResult> ActualizarEstadoReunionParticipante([FromQuery] long IdReunionParticipante, [FromQuery] long UsuarioModifica)
        {   /*
            var respuesta = await _service.ActualizarEstadoReunionParticipante(IdReunionParticipante, UsuarioModifica);
            return Ok(respuesta);
            */ /*
            return null;
        }

        [HttpGet("obtener-reunion-participantes")]
        public async Task<ActionResult> ObtenerReunionParticipantes([FromQuery] long IdReunionSolicitud)
        {   /*
            var respuesta = await _service.ObtenerReunionParticipantes(IdReunionSolicitud);
            return Ok(respuesta);
            */ /*
            return null;
        }

        // --- REUNION_CORREO ---
        [HttpPost("insertar-reunion-correo")]
        public async Task<ActionResult> InsertarReunionCorreo([FromBody] ReunionCorreoDto request)
        {
            var respuesta = await _service.InsertarReunionCorreo(request);
            return Ok(respuesta);
        }

        [HttpPut("actualizar-estado-reunion-correo")]
        public async Task<ActionResult> ActualizarEstadoReunionCorreo([FromQuery] long IdReunionCorreo, [FromQuery] long UsuarioModifica)
        {
            /*
            var respuesta = await _service.ActualizarEstadoReunionCorreo(IdReunionCorreo, UsuarioModifica);
            return Ok(respuesta);
            */ /*
            return null;    
        }

        [HttpGet("obtener-reunion-correos")]
        public async Task<ActionResult> ObtenerReunionCorreos([FromQuery] long IdReunionSolicitud)
        {   /*
            var respuesta = await _service.ObtenerReunionCorreos(IdReunionSolicitud);
            return Ok(respuesta);
            */ /*
            return null;
        }

        // --- REUNION_OBJETIVO ---
        [HttpPost("insertar-reunion-objetivo")]
        public async Task<ActionResult> InsertarReunionObjetivo([FromBody] ReunionObjetivoDto request)
        {   
            var respuesta = await _service.InsertarReunionObjetivo(request);
            return Ok(respuesta);
        }

        [HttpPut("actualizar-reunion-objetivo")]
        public async Task<ActionResult> ActualizarReunionObjetivo([FromBody] ReunionObjetivoDto request)
        {
            var respuesta = await _service.ActualizarReunionObjetivo(request);
            return Ok(respuesta);
        }

        [HttpPut("actualizar-estado-reunion-objetivo")]
        public async Task<ActionResult> ActualizarEstadoReunionObjetivo([FromQuery] long IdReunionObjetivo, [FromQuery] long UsuarioModifica)
        {   /*
            var respuesta = await _service.ActualizarEstadoReunionObjetivo(IdReunionObjetivo, UsuarioModifica);
            return Ok(respuesta);
            */ /*
            return null;    
        }

        [HttpGet("obtener-reunion-objetivos")]
        public async Task<ActionResult> ObtenerReunionObjetivos([FromQuery] long IdReunionSolicitud)
        {

            /*var respuesta = await _service.ObtenerReunionObjetivos(IdReunionSolicitud);
            return Ok(respuesta);
            */ /*
            return null; 
        }*/
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.IApplication;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Minem.Tupa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TramiteController(ITramiteApplication service) : ControllerBase
    {
        private readonly ITramiteApplication _service = service;

        [AllowAnonymous]
        [HttpPut("generar")]
        public async Task<ActionResult> GenerarTramite([FromBody] GenerarTramiteRequestDto request)
        {
            var respuesta = await _service.GenerarTramite(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtenernotas")]
        public async Task<ActionResult> ObtenerNotas([FromQuery] ObtenerNotasRequestDto request)
        {
            var respuesta = await _service.ObtenerNotas(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtenerdocs")]
        public async Task<ActionResult> ObtenerNotas([FromQuery] ObtenerDocumentosRequestDto request)
        {
            var respuesta = await _service.ObtenerDocumentos(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("tramite")]
        public async Task<ActionResult> ObtenerTramite([FromQuery] ObtenerTramiteRequestDto request)
        {
            var respuesta = await _service.ObtenerTramite(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("enviar")]
        public async Task<ActionResult> EnviarSolicitud([FromBody] EnviarSolicitudRequestDto request)
        {
            var respuesta = await _service.EnviarSolicitud(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("bandeja")]
        public async Task<ActionResult> ObtenerMisTramites()
        {
            var respuesta = await _service.ObtenerMisTramites();
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-dia-aprobado")]
        public async Task<ActionResult> ObtenerDIAAprobado()
        {
            var respuesta = await _service.ObtenerDIAAprobado();
            return Ok(respuesta);
        }
        [AllowAnonymous]
        [HttpPut("actualizar-padre-its")]
        public async Task<ActionResult> ActualizarPadreITS(long codMaeSolicitud, long codMaeSolicitudPadre)
        {
            var respuesta = await _service.ActualizarPadreITS(codMaeSolicitud, codMaeSolicitudPadre);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("adjuntaradicional")]
        public async Task<ActionResult> GenerarDocumentoAdicional([FromBody] GenerarDocumentoAdicionalRequestDto request)
        {
            var respuesta = await _service.GenerarDocumentoAdicional(request);
            return Ok(respuesta);
        }
        
        [AllowAnonymous]
        [HttpGet("obtener-docs-adicionales")]
        public async Task<ActionResult> ObtenerDocumentosAdicionales([FromQuery] long idTramite)
        {
            var respuesta = await _service.ObtenerDocumentosAdicionales(idTramite);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("eliminar-documento-adicional")]
        public async Task<ActionResult> EliminarDocumentoAdicional([FromQuery] long request)
        {
            var respuesta = await _service.EliminarDocumentoAdicional(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("actualizar-id-estudio")]
        public async Task<ActionResult> ActualizarIdEstudio([FromBody] ActualizarEstudioRequestDto request)
        {
            var respuesta = await _service.ActualizarIdEstudio(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("anular")]
        public async Task<ActionResult> AnularTramite([FromBody] AnularTramiteRequestDto request)
        {
            var respuesta = await _service.AnularTramite(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("tipo-comunicacion")]
        public async Task<ActionResult> ObtenerTipoComunicacion()
        {
            var respuesta = await _service.ObtenerTipoComunicacion();
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("tipo-documento")]
        public async Task<ActionResult> ObtenerTipoDocumento()
        {
            var respuesta = await _service.ObtenerTipoDocumento();
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("registrar-nombre-proyecto")]
        public async Task<ActionResult> RegistrarNombreProyecto([FromBody] RegistrarNombreProyectoRequestDto request)
        {
            var respuesta = await _service.RegistrarNombreProyecto(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("validar-nombre-proyecto")]
        public async Task<ActionResult> ValidarNombreProyecto(long idTramite, string nombreProyecto)
        {
            var respuesta = await _service.ValidarNombreProyecto(idTramite, nombreProyecto);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("enviar-tramite-adicional")]
        public async Task<ActionResult> EnviarTramiteAdicional([FromBody] EnviarTramiteAdicionalRequestDto request)
        {//TipoComunicacion
            var respuesta = await _service.EnviarTramiteAdicional(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("registrar-estudio")]
        public async Task<ActionResult> RegistrarEstudio([FromBody] RegistrarEstudioRequestDto request)
        {
            var respuesta = await _service.RegistrarEstudio(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("trazabilidad")]
        public async Task<ActionResult> VerTrazabilidad(long codMaeSolicitud)
        {
            var respuesta = await _service.VerTrazabilidad(codMaeSolicitud);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("situacion-solicitud-final")]
        public async Task<ActionResult> ObtenerSituacionSolicitudFinal([FromQuery] int codMaeSolicitud)
        {
            var respuesta = await _service.ObtenerSituacionSolicitudFinal(codMaeSolicitud);
            return Ok(respuesta);
        }
                
        [AllowAnonymous]
        [HttpGet("actualizar-situacion-admisibilidad-tumaesolicitud")]
        public async Task<ActionResult> ActualizarSituacionAdmisibilidadTumaesolicitud(int codMaeSolicitud, int codSituacion, int codEtapa)
        {
            var respuesta = await _service.ActualizarSituacionAdmisibilidadTumaesolicitud(codMaeSolicitud, codSituacion, codEtapa);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("actualizar-situacion-admisibilidad-funcionario")]
        public async Task<ActionResult> ActualizarSituacionAdmisibilidadFuncionario(RegistrarAdmisibilidadFuncionarioDto request)
        {
            var respuesta = await _service.ActualizarSituacionAdmisibilidadFuncionario(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("actualizar-archivo-requisito2")]
        public async Task<ActionResult> ActualizarArchivoRequisito2(int codMaeSolicitud, int codMaeRequisito, int codMaeEstado, int idArchivo, string nomArchivo, string extArchivo)
        {
            var respuesta = await _service.ActualizarArchivoRequisito2(codMaeSolicitud, codMaeRequisito, codMaeEstado, idArchivo, nomArchivo, extArchivo);
            return Ok(respuesta);
        }
        
        [AllowAnonymous]
        [HttpGet("obtener-archivo-requisito2")]
        public async Task<ActionResult> ObtenerArchivoRequisito2(int codMaeSolicitud)
        {
            var respuesta = await _service.ObtenerArchivoRequisito2(codMaeSolicitud);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("pago/{idPagoSolicitud}")]
        public async Task<ActionResult> ObtenerDatoPagoTramite(int idPagoSolicitud)
        {
            var respuesta = await _service.ObtenerDatoPagoTramite(idPagoSolicitud);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("pago")]
        public async Task<ActionResult> RegistrarPagoSolicitud([FromBody] RegistrarPagoSolicitudRequestDto request)
        {
            var respuesta = await _service.RegistrarPagoSolicitud(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("guardar-archivo-requisito")]
        public async Task<ActionResult> GuardarArcchivoRequisito([FromBody] ArchivoRequisitoSolicitudDto request)
        {
            var respuesta = await _service.GuardarArchivoRequisitoSolicitud(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPut("actualizar-estado-requisito")]
        public async Task<ActionResult> ActualizarDetalleSolicitud([FromBody] ActualizarDetalleSolicitudDto request)
        {
            var respuesta = await _service.ActualizarDetalleSolicitud(request);
            return Ok(respuesta);
        }
    }
}

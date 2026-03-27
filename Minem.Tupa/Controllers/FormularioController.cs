using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.IApplication;
using Minem.Tupa.Utils;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Minem.Tupa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioController(IFormularioApplication service, IHttpContextAccessor httpContextAccessor,
        IDocumentoApplication documentoApplication
        ) : ControllerBase
    {
        private readonly IFormularioApplication _service = service;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IDocumentoApplication _documentoApplication = documentoApplication;

        [AllowAnonymous]
        [HttpPost("guardar")]
        public async Task<ActionResult> Guardar([FromBody] GuardarFormularioRequestDto request)
        {
            Console.WriteLine("guardar: " + request);
            var respuesta = await _service.GuardarFormulario(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("formulario-dia")]
        public async Task<ActionResult> ObtenerFormularioDia([FromQuery] long CodMaeSolicitud)
        {
            var respuesta = await _service.ObtenerFormularioDia(CodMaeSolicitud);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("plantilla-dia")]
        public async Task<ActionResult> DescargarPlantilla([FromBody] DescargarPlantillaDiaRequestDto request)
        {
            var respuesta = await _service.DescargarDocumento(request);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpPost("guardar-resumen-ejecutivo")]
        public async Task<ActionResult> GuardarResumenEjecutivo([FromBody] GuardarFormularioRequestDto request)
        {
            var respuesta = await _service.GuardarResumenEjecutivo(request);
            return Ok(respuesta);
        }

        #region Observacion de Opinantes

        [AllowAnonymous]
        [HttpGet("transaction-opinantes/{codMaeSolicitud}/{codSolicitudExpediente}")]
        public async Task<ActionResult> GetTransactionListOpinantes(int codMaeSolicitud, int codSolicitudExpediente)
        {
            var respuesta = await _service.GetTransactionListOpinantes(codMaeSolicitud, codSolicitudExpediente);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("listar-documentosinstitucion-opinantes/{codMaeSolicitud}/{codSolicitudExpediente}")]
        public async Task<ActionResult> ListarDocumentosInstitucion(int codMaeSolicitud, int codSolicitudExpediente)
        {
            var respuesta = await _service.ListarDocumentosInstitucion(codMaeSolicitud, codSolicitudExpediente);
            return Ok(respuesta);
        }

        [HttpGet("listar-documentosinstitucionadjuntos-opinantes/{codMaeSolicitud}/{codSolicitudExpediente}")]
        public async Task<ActionResult> ListarDocumentosInstitucionAdjuntos(int codMaeSolicitud, int codSolicitudExpediente)
        {
            var respuesta = await _service.ListarDocumentosInstitucionAdjuntos(codMaeSolicitud, codSolicitudExpediente);
            return Ok(respuesta);
        }

        [HttpGet("transaction-resume-data/{codMaeSolicitud}")]
        public async Task<ActionResult> GetTransactionResumeData(int codMaeSolicitud)
        {
            var respuesta = await _service.GetTransactionResumeData(codMaeSolicitud);
            return Ok(respuesta);
        }
        #endregion

        [HttpGet("generar-pdf/{codMaeSolicitud}")]
        public async Task<ActionResult> GetGenerarPdf(int codMaeSolicitud)
        {
            var respuesta = await _documentoApplication.GenerarDocumentoFormulario(codMaeSolicitud);
            return Ok(respuesta);
        }
    }
}

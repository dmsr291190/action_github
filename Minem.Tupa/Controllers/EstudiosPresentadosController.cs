using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Dto.Documento;
using Minem.Tupa.Dto.EstudiosPresentados;
using Minem.Tupa.IApplication;

namespace Minem.Tupa.Api.Controllers
{
    [Route("api/estudios-presentados")]
    [ApiController]
    public class EstudiosPresentadosController(IEstudiosPresentadosApplication estudiosPresentadosApplication) : ControllerBase
    {
        private readonly IEstudiosPresentadosApplication _estudiosPresentadosApplication = estudiosPresentadosApplication;

        [HttpGet("bandeja")]
        public async Task<ActionResult> GetBandeja([FromQuery] BandejaEstudiosPresentadosRequestDto request)
        {
            var respuesta = await _estudiosPresentadosApplication.GetBandeja(request);
            return Ok(respuesta);
        }

        [HttpGet("situacion")]
        public async Task<ActionResult> GetSituacion()
        {
            var respuesta = await _estudiosPresentadosApplication.GetSituacion();
            return Ok(respuesta);
        }

        [HttpGet("tipo-estudio")]
        public async Task<ActionResult> GetTipoEstudio()
        {
            var respuesta = await _estudiosPresentadosApplication.GetTipoEstudio();
            return Ok(respuesta);
        }

        [HttpGet("listar-tipo-estudio")]
        public async Task<ActionResult> GetListarTipoEstudio()
        {
            var respuesta = await _estudiosPresentadosApplication.GetListarTipoEstudio();
            return Ok(respuesta);
        }

        [HttpGet("listar-tipo-estudio-tupa/{CodMaeTupa}")]
        public async Task<ActionResult> GetListarTipoEstudioTupa(string CodMaeTupa)
        {
            var respuesta = await _estudiosPresentadosApplication.GetListarTipoEstudioTupa(CodMaeTupa);
            return Ok(respuesta);
        }

        [HttpPost("guardar-aporte")]
        public async Task<ActionResult> GuardarAporte([FromBody] SolicitudAporteRequestDto request)
        {
            var respuesta = await _estudiosPresentadosApplication.GuardarAporte(request);
            return Ok(respuesta);
        }

        [HttpGet("informe/documentos-despachados/{idSolicitud}")]
        public async Task<ActionResult> GetDocumentosDespachados(int idSolicitud)
        {
            var respuesta = await _estudiosPresentadosApplication.GetDocumentosDespachados(idSolicitud);
            return Ok(respuesta);
        }

        [HttpGet("descarga-bloque/zip")]
        public async Task<ActionResult> DescargaEnBloqueZip([FromQuery] DescargaBloqueRequestDto request)
        {
            var response = await _estudiosPresentadosApplication.DescargaEnBloqueZip(request);
            if (response.Data == null)
                return NotFound("Document not found");

            return File(response.Data, "application/zip", response.Message);
        }

        [HttpGet("descarga-documento/zip")]
        public async Task<IActionResult> DescargaDocumentoZip([FromQuery] List<int> listaIdDocumentos)
        {
            if (listaIdDocumentos == null || !listaIdDocumentos.Any())
                return BadRequest("Debe enviar al menos un ID de documento.");

            var request = new DescargaBloqueRequestDto
            {
                ListaIdDocumentos = listaIdDocumentos
            };

            var response = await _estudiosPresentadosApplication.DescargaDocumentoZip(request);
            if (response.Data == null)
                return NotFound("Document not found");

            return File(response.Data, "application/zip", response.Message);
        }

        [HttpGet("descarga-bloque/{idSolicitud}")]
        public async Task<ActionResult> DescargaEnBloque(int idSolicitud)
        {
            var response = await _estudiosPresentadosApplication.DescargaEnBloque(idSolicitud);
            return Ok(response);
        }

        [HttpGet("descarga-bloque/indice-excel")]
        public async Task<ActionResult> DescargaEnBloqueIndiceExcel([FromQuery] DescargaBloqueRequestDto request)
        {
            var response = await _estudiosPresentadosApplication.DescargaEnBloqueIndiceExcel(request);
            if (response.Data == null)
                return NotFound("Document not found");

            //Response.Headers.Append("Content-Disposition", $"attachment; filename=\"{response.Message}\"");
            return File(response.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", response.Message);
        }
    }
}

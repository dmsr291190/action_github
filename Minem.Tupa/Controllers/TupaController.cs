using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.IApplication;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Minem.Tupa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TupaController(ITupaApplication service) : ControllerBase
    {
        private readonly ITupaApplication _service = service;

        [AllowAnonymous]
        [HttpGet("tupa-por-sector")]
        public async Task<ActionResult> ObtenerTupaPorSector([FromQuery] ObtenerTupaPorSectorRequest request)
        {
            var respuesta = await _service.ObtenerTupaPorSector(request.idSector, request.tipoPersona);
            return Ok(respuesta);
        }


        [AllowAnonymous]
        [HttpGet("tupa-por-codigo")]
        public async Task<ActionResult> ObtenerTupaPorCodigo([FromQuery] ObtenerTupaPorCodigo request)
        {
            var respuesta = await _service.ObtenerTupaPorCodigo(request.codigoTupa);
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-tupa")]
        public async Task<ActionResult> ObtenerTupa()
        {
            var respuesta = await _service.ObtenerTupa();
            return Ok(respuesta);
        }

        [AllowAnonymous]
        [HttpGet("obtener-documentos-despachados/{codMaeSolicitud}")]
        public async Task<ActionResult> ObtenerDocumentosDespachados(long codMaeSolicitud)
        {
            var respuesta = await _service.ObtenerDocumentosDespachados(codMaeSolicitud);
            return Ok(respuesta);
        }
    }
}

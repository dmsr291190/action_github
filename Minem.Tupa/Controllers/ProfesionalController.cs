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
    public class ProfesionalController(IProfesionalApplication service) : ControllerBase
    {
        private readonly IProfesionalApplication _service = service;

        
        [AllowAnonymous]
        [HttpGet("obtenerprofesiones")]
        public async Task<ActionResult> ObtenerProfesiones()
        {
            var respuesta = await _service.ObtenerProfesiones();
            return Ok(respuesta);
        }
    }
}

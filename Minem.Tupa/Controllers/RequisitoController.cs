using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Dto.Requisito;
using Minem.Tupa.IApplication;

namespace Minem.Tupa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitoController(IRequisitoApplication service) : ControllerBase
    {
        private readonly IRequisitoApplication _service = service;

        [AllowAnonymous]
        [HttpGet("requisito-por-tupa")]
        public async Task<ActionResult> ObtenerRequisitos([FromQuery] ObtenerRequisitoRequest request)
        {
            var respuesta = await _service.ObtenerRequisitos(request.codigoTupa);
            return Ok(respuesta);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Minem.Tupa.Application;

namespace Minem.Tupa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController: ControllerBase
    {
        private readonly ArchivoApplication _service;
        private readonly IWebHostEnvironment _env;

        public ArchivoController(ArchivoApplication service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }


        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var fileBytes = await _service.GetFileAsync(fileName);

            if (fileBytes == null)
            {
                return NotFound();
            }

            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpGet("descargar-pdf-its")]
        public IActionResult DescargarPDF()
        {
            {
                var ruta = Path.Combine(_env.ContentRootPath, "Plantilla", "FormatoDeSolicitud_ITS.pdf");

                if (!System.IO.File.Exists(ruta))
                {
                    return NotFound("Archivo no encontrado.");
                }

                var fileBytes = System.IO.File.ReadAllBytes(ruta);
                return File(fileBytes, "application/pdf", "FormatoDeSolicitud_ITS.pdf");
            }
        }
    }
}

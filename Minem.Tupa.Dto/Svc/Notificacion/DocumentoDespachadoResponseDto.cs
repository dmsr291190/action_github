using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Svc.Notificacion
{
    public class DocumentoDespachadoResponseDto
    {
        public int IdAdjunto { get; set; }
        public DateTime FechaDespacho { get; set; }
        public int? Id { get; set; }
        public string UriArchivo { get; set; } = string.Empty;
        public string NombreArchivo { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public bool EsInterno { get; set; }
        public string base64Documento { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
    }
}

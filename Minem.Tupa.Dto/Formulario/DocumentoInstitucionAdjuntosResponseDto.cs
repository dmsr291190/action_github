using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Formulario
{
    public class DocumentoInstitucionAdjuntosResponseDto
    {
        public int IdSolicitudOpinante { get; set; }
        public int IdSolicitudOpinanteRespuestaAdj { get; set; }
        public int IdSolicitudOpinanteRespuesta { get; set; }
        public string NombreDocumento { get; set; }
        public int IdArchivo { get; set; }
        public int Estado { get; set; }
        public decimal UsuarioRegistra { get; set; }
        public DateTime FechaRegistra { get; set; }
        public decimal UsuarioModifica { get; set; }
        public DateTime FechaModifica { get; set; }
    }
}

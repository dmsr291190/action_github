using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Reunion
{

    public class ReunionRequestDto { 
        public long? IdReunionSolicitud { get; set; } 
        public long? CodMaeSolicitud { get; set; }
        public string? Propuesta { get; set; }
        public string? Consultora { get; set; } 
        public string? TipoReunion { get; set; } 
        public string? NombreArchivo { get; set; } 
        public long? Estado { get; set; } 
        public long? UsuarioRegistra { get; set; } 
        public string? FechaRegistra { get; set; } 
        public long? UsuarioModifica { get; set; } 
        public string? FechaModifica { get; set; } 
        public string? PropuestaFecha1 { get; set; } 
        public string? PropuestaFecha2 { get; set; } 
        public long? IdArchivo { get; set; } 
        public long? EstadoReunion { get; set; } }
}

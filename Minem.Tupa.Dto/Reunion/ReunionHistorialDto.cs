using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Reunion
{
    public class ReunionHistorialDto
    {
        public int IdReunionSolicitud { get; set; }
        public string Comentario { get; set; }
        public string FechaRegistra { get; set; }
        public string EstadoReunion { get; set; }
    }
}
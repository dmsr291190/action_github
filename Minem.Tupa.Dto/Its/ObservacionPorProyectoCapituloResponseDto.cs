using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Its
{
    public class ObservacionPorProyectoCapituloResponseDto
    {
        public int? IdObservacion { get; set; }
        public int IdProyecto { get; set; }
        public int? IdObservacionPadre { get; set; }
        public string Observacion { get; set; }
        public string Capitulo { get; set; }

        public int? Orden { get; set; }
        public int? EstadoObservacion { get; set; }
        public int UsuarioRegistra { get; set; }
        public int? UsuarioModifica { get; set; }
        public string NombreUsuarioCrea { get; set; }
        public string FechaCreacion { get; set; }
        public string NombreUsuarioMod { get; set; }
        public string FechaUltimaMod { get; set; }
    }
}

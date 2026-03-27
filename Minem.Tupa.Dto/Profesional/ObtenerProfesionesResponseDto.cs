using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Profesional
{
    public class ObtenerProfesionesResponseDto
    {
        public long ProfesionId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaModif { get; set; }
    }
}

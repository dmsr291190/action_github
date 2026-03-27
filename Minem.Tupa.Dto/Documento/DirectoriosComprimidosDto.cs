using Minem.Tupa.Dto.Formulario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Documento
{
    public class DirectoriosComprimidosDto
    {
        public string directorio { get; set; }
        public int IdEstructuraCapituloAdjuntos { get; set; }
        public List<Adjunto> Adjuntos { get; set; }
    }
}

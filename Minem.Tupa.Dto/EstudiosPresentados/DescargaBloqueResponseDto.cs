using Minem.Tupa.Dto.Documento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.EstudiosPresentados
{
    public class DescargaBloqueResponseDto
    {
        public DatosFormularioDto DatosFormulario { get; set; }
        public List<EstructuraIndiceDto> ListaEstructuraIndice { get; set; }
        public int TotalAdjuntos { get; set; }
    }
    public class DatosFormularioDto
    {
        public string NumeroExpediente { get; set; }
        public string TipoIga { get; set; }
        public string TitulaMinero { get; set; }
        public string NombreProyecto { get; set; }
    }
}

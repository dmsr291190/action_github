using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Its
{
    public class ObservacionesPorProyectoYCapRenposeDto
    {
        public bool EstaObservado { get; set; }
        public int TotalObservados { get; set; }
        public List<ObservacionesPorCapitulo> CapitulosObservados { get; set; }
    }

    public class ObservacionesPorCapitulo
    {
        public string Capitulo { get; set; }
        public int TotalObservado { get; set; }
    }
}

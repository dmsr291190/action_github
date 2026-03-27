using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Its
{
    public class ObservacionesProyectoPorCapitulo_Response_Entity
    {
        public int EstaObservado { get; set; }
        public string Capitulo { get; set; }
        public int TotalObservadoCapitulo { get; set; }
        public int TotalObservados { get; set; }
    }
}

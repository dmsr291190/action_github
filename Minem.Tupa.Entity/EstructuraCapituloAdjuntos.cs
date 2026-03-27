using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity
{
    public class EstructuraCapituloAdjuntos: AuditoriaNew
    {
        public int IdEstructuraCapituloAdjuntos { get; set; }
        public int CodMaeCapitulo { get; set; }
        public int IdEstructuraCapituloAdjuntosPadre { get; set; }
        public int Orden { get; set; }
        public string PropiedadJson { get; set; }
        public int EsAdjunto { get; set; }
    }
}

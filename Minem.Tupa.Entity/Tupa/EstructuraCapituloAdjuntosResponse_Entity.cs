using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Tupa
{
    public class EstructuraCapituloAdjuntosResponse_Entity : EstructuraCapituloAdjuntos
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool EsUltHijo { get; set; }
        public int Nivel { get; set; }
        public int IdNewEstructuraCapituloAdjuntosPadre { get; set; }
        public bool MostrarEnEstructura { get; set; }

        public int? Tupa { get; set; }
    }
}

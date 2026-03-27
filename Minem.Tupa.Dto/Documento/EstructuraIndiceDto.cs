using Minem.Tupa.Dto.Formulario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Documento
{
    public class EstructuraIndiceDto
    {
        public int IdEstructuraCapituloAdjuntos { get; set; }
        public int IdNewEstructuraCapituloAdjuntos { get; set; }
        public int IdEstructuraCapituloAdjuntosPadre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public List<Adjunto> Adjuntos { get; set; }
        public bool EsUltHijo { get; set; }
        public bool TieneAdjunto { get; set; }

        public int? Tupa { get; set; }
    }
}

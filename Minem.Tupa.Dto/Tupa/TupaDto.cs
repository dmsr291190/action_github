using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tupa
{
    public class TupaDto
    {
        public long? IdTupa { get; set; }
        public string Codigo { get; set; }
        public string? Denominacion { get; set; }
        public string? Origen { get; set; }
        public string? Dependencia { get; set; }
        public string? UrlDocumento { get; set; }
        public string? TipoPersona { get; set; }
        public string? Coordinador { get; set; }
        public string? Director { get; set; }
        public string? Plazo { get; set; }
        public long? IdSector { get; set; }
        public int CodMaeUniOrganica { get; set; }
        public string? CodMaeTupa { get; set; }
        
    }
}

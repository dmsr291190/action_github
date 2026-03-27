using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tupa
{
    public class PersonaDto
    {
        public long CodMovPersona { get; set; }
        public string? Nombre { get; set; }
        public string? ApePaterno { get; set; }
        public string? ApeMaterno { get; set; }
        public string? NumDocumento { get; set; }
        public string? CodTabUbigeo { get; set; }
    }
}

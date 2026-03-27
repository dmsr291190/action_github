using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity
{
    public class GEMovPersona
    {
        public long CodMovPersona { get; set; }
        public string CodMaeDocumento { get; set; }
        public string CodTabTipoPersona { get; set; }
        public string CodTabSexo { get; set; }
        public string CodTabUbigeo { get; set; }
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string NomCompleto { get; set; }
        public string CorrElectronico { get; set; }
    }
}

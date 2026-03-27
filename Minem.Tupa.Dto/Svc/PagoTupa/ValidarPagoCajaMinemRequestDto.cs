using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Svc.PagoTupa
{
    public class ValidarPagoCajaMinemRequestDto
    {
        public string CodigoCaja { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Importe { get; set; }
        public int Expediente { get; set; }
    }
}

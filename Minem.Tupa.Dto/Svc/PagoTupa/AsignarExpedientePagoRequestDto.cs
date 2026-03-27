using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Svc.PagoTupa
{
    public class AsignarExpedientePagoRequestDto
    {
        public int IdPago { get; set; }
        public int IdSistema { get; set; }
        public int Expediente { get; set; }
    }
}

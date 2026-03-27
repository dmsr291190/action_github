using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Svc.PagoTupa
{
    public class ValidarPagoPagaloPeRequestDto
    {
        public string NroSecuencia { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string CodigoOficina { get; set; }
    }
}

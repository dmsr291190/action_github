using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class RegistrarPagoSolicitudRequestDto
    {
        public int IdPagoSolicitud { get; set; }
        public int IdSolicitud { get; set; }
        public int CodDetConCatRequisito { get; set; }
        public string CodigoUniPasarela { get; set; }
        public string CodigoVoucher{ get; set; }
        public int IdTipoPago { get; set; }
        public string CodigoCaja { get; set; }
        public DateTime FechaPago{ get; set; }
        public decimal Importe { get; set; }
        public string NroSecuencia { get; set; }
        public string CodigoOficina { get; set; }
        public int Usuario { get; set; }
    }
}

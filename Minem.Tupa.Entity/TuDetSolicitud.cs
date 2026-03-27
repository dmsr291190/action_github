using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity
{
    public class TuDetSolicitud
    {
        public int CODDETSOLICITUD { get; set; }
        public int CODMAESOLICITUD { get; set; }
        public int CODDETCONCATREQUISITO { get; set; }
        public string CODUNIPASARELA { get; set; }
        public string CODVOUCHER { get; set; }
        public int ID_TIPO_PAGO { get; set; }
        public string CODIGO_CAJA { get; set; }
        public DateTime FECHA_PAGO { get; set; }
        public decimal IMPORTE { get; set; }
        public string NRO_SECUENCIA { get; set; }
        public string CODIGO_OFICINA { get; set; }
        public int REGUSUAREGISTRA { get; set; }
        public DateTime FECHREGISTRA { get; set; }
    }
}

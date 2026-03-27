using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Formulario
{
    public class TransactionListOpinantesResponseDto
    {
        public int IdSolicitudOpinante { get; set; }
        public int IdSolicitud { get; set; }
        public int IdSolicitudExpediente { get; set; }
        public int IdOpinante { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }
}

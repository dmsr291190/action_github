using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Formulario
{
    public class TransactionListOpinantes
    {
        public int Id_solicitud_opinante { get; set; }
        public int Id_solicitud { get; set; }
        public int Id_solicitud_expediente { get; set; }
        public int Id_opinante { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }
}

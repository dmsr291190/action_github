using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity
{
    public class Auditoria
    {
        public int Estado { get; set; }
        public int RegUsuaRegistra { get; set; }
        public DateTime FechRegistra { get; set; }
        public int? RegUsuaModifica { get; set; }
        public DateTime? FechModifica { get; set; }
    }

    public class AuditoriaNew
    {
        public int Estado { get; set; }
        public int UsuarioRegistra { get; set; }
        public DateTime FechaRegistra { get; set; }
        public int UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}

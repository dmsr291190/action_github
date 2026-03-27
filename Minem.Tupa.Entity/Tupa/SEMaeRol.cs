using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Tupa
{
    public class SEMaeRol
    {
        public int CodMaeRol { get; set; }
        public string? Abreviatura { get; set; }
        public string? Denominacion { get; set; }
        public string? Descripcion { get; set; }
        public int? RegUsuaRegistra { get; set; }
        public DateTime? FechRegistra { get; set; }
        public bool? Estado { get; set; }
    }
}

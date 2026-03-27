using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Tupa
{
    public class SEMovUsuario
    {
        public int CodMovUsuario { get; set; }
        public string? CodMovTrabajador { get; set; }
        public string? NombUsuario { get; set; }
        public string? Contrasenia { get; set; }
        public DateTime FechIniVigencia { get; set; }
        public DateTime FechFinVigencia { get; set; }
        public long? CodMovPersona { get; set; }
        public bool? IndTrabajador { get; set; }
        public bool? IndConfirmacion { get; set; }
        public string? Token { get; set; }
        public int RegUsuaRegistra { get; set; }
        public DateTime? FechRegistra { get; set; }
        public bool? Estado { get; set; }

    }
}

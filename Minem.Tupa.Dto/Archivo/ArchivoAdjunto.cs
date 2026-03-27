using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Archivo
{
    public class ArchivoAdjunto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public string DescripcionTipo { get; set; }
    }
}

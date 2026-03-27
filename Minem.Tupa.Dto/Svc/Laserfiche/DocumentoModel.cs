using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Svc.Laserfiche
{
    public class DocumentoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Base64Documento { get; set; }
        public string Extension { get; set; }
        public long Tamanio { get; set; }
    }
}

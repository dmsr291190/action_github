using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Its
{
    public class SP_INSERT_ITS_PROYECTO_Response_Entity
    {
        public long? idProyecto { get; set; }
        public decimal? codMaeSolicitud { get; set; }
        public string nombre { get; set; }
        public decimal? montoEstimado { get; set; }
        public long? idUnidadMinera { get; set; }
        public string unidadMinera { get; set; }
        public int? estado { get; set; }
        public long? usuarioRegistra { get; set; }
        public string? fechaRegistra { get; set; }
        public long? usuarioModifica { get; set; }
        public string? fechaModifica { get; set; }
    }
}

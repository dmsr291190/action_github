using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Its
{
    public class SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity
    {
        public long? idProfesional { get; set; }
        public long? idProyecto { get; set; }
        public string? nombresApellidos { get; set; }
        public string? profesion { get; set; }
        public string? colegiatura { get; set; }
        public int? estado { get; set; }
        public long? usuarioRegistra { get; set; }
        public string? fechaRegistra { get; set; }
        public long? usuarioModifica { get; set; }
        public string? fechaModifica { get; set; }
        public string? idsEliminar { get; set; }
    }
}

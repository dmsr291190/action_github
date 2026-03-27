using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_INSERTAR_MOTIVOS_INFORME_Request_Entity
    {
        public long infoMotivoId {  get; set; }
        public long motivoId { get; set; }
        public long informeId { get; set; }
        public long? motivoPadreId { get; set; }
        public string descripcion {  get; set; }
        public string? informacionAdicional { get; set; }
        public long usuarioId { get; set; }
    }
}

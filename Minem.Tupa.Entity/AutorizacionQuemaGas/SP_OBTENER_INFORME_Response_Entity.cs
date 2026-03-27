using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_OBTENER_INFORME_Response_Entity
    {
        public long informeId { get; set; }
        public long solicitudId { get; set; }
        public long loteId { get; set; }
        public string? antecedentes { get; set; }
        public string? objetivo { get; set; }
        public string? trabajoRealizar { get; set; }
        public string? facilidadesExistentes { get; set; }
        public string? procedimiento { get; set; }
        public string? porqueNoInyectar { get; set; }
        public string? porqueNoComercializar { get; set; }
        public string? porqueNoUtilizar { get; set; }
        public string? fechaInicioQuema { get; set; }
        public int? quemaLiquido { get; set; }
        public long usuarioId { get; set; }
    }
}

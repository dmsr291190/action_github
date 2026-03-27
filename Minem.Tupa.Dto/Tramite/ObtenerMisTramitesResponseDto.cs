using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class ObtenerMisTramitesResponseDto
    {
        public int IdRow { get; set; }
        public int CodMaeSolicitud { get; set; }
        public int CodMovPersona { get; set; }
        public string CodMaeTupa { get; set; }
        public int CodIdMaeTupa { get; set; }
        public int CodMaeEstado { get; set; }
        public string NumDocumento { get; set; }
        public string NumSTD { get; set; }
        public int IdSTD { get; set; }
        public string Descripcion { get; set; }
        public int RegUsuaRegistra { get; set; }
        public string nvFechRegSolicitud { get; set; }
        public int Estado { get; set; }
        public int CodMaeUniOrganica { get; set; }
        public DateTime FechRegSolicitud { get; set; }
        public string DenominacionEstado { get; set; }
        public int CodMaeCatRequisito { get; set; }
        public string DenominacionTitulo { get; set; }
        public string Denominacion { get; set; }
        public string PlazoResolverTexto { get; set; }
        public int CodDetCatRequisito { get; set; }
        public long Notificado { get; set; }
        public string NombreProyecto { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaSubsanacion { get; set; }
        public DateTime FechaFinAdmisibilidad { get; set; }
        public string VerDetalleAdmisibilidad { get; set; }
    }
}

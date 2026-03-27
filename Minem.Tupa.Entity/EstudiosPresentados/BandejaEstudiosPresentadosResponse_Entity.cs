using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.EstudiosPresentados
{
    public class BandejaEstudiosPresentadosResponse_Entity : Paginacion
    {
        public int CodMaeSolicitud { get; set; }
        public string CodMaeTupa { get; set; }
        public int CodIdMaeTupa { get; set; }
        public int CodMaeEstado { get; set; }
        public string TipoEstudio { get; set; }
        public string Situacion { get; set; }
        public string NombreTitular { get; set; }
        public string NombreProyecto { get; set; }
        public string UnidadMinera { get; set; }
        public string NroExpediente { get; set; }
        public DateTime FechaExpediente { get; set; }
        public string Region { get; set; }
        public int IdSolicitudExpediente { get; set; }
        public string NroExpedienteUlt { get; set; }
    }
}

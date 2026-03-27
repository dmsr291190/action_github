using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Minem.Tupa.Utils.Enumeration;

namespace Minem.Tupa.Entity.Tramite
{
    public class PRC_USP_S_LISTAR_TRAZASIGNACION_Response_Entity
    {
        public int? IDROW { get; set; }
        public string? DENUNIORGANICA { get; set; }
        public string? EMISOR { get; set; }
        public string? RECEPTOR { get; set; }
        public DateTime FECHINIASIGNACION { get; set; }
        public DateTime? FECHFINASIGNACION { get; set; }
        public int? IDESTADOASIGNACION { get; set; }
        public string? DESCESTADOASIGNACION { get; set; }
        public long? CODMOVTUPASIGNACION { get; set; }



       // public int IdRow { get; set; }
        //public string DenUniOrganica { get; set; }
       // public string Emisor { get; set; }
       // public string Receptor { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRows { get; set; }
        public int Expediente { get; set; }
        public int? ExpedientePadre { get; set; }

        //Adicionales
        public int? IdEtapaSituacionCambio { get; set; }
        public string NombreEtapaSituacionCambio { get; set; }
        public int? CodMaeEstadoSituacionCambio { get; set; }
        public string EstadoDenominacionSituacionCambio { get; set; }
        public int? IdSituacionCambio { get; set; }
        public string DescripcionSituacionCambio { get; set; }
        public int? IdResponsableSituacionCambio { get; set; }
        public string NombreResponsableSituacionCambio { get; set; }

        public int? IdTupaAsignacionSituacion { get; set; }



    }

}

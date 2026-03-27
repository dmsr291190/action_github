using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Svc.Externo
{
    public class DatosGeneralesEmpresaResponseDto
    {
        public DatosTitularDto DatosTitular { get; set; }
        public DatosRepresentanteDto DatosRepresentante { get; set; }
    }

    public class DatosTitularDto
    {
        public string NombreTitular { get; set; }
        public string Direccion { get; set; }
        public string Region { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Ruc { get; set; }
        public string Email { get; set; }
        public string PaginaWeb { get; set; }
    }

    public class DatosRepresentanteDto
    {
        public int IdRepresentante { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string Cargo { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string EmailRepresentante { get; set; }
    }
}

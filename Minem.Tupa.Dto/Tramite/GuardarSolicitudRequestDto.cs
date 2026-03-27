using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class GuardarSolicitudRequestDto
    {
        public int? IdProc { get; set; }
        public int? IdTramite { get; set; }
        public int? PlazoAtencion { get; set; }

        // Campos adicionales
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Nombres { get; set; }
        public string Ubigeo { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string DocAdjunto { get; set; }
        public int? Folios { get; set; }
    }
}

using Minem.Tupa.Dto.Archivo;
using Minem.Tupa.Dto.Tramite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.EstudiosPresentados
{
    public class SolicitudAporteRequestDto
    {
        public int IdSolicitud { get; set; }
        public int IdSolicitudExpediente { get; set; }

        public string? CodigoCelular { get; set; }
        public string? CodigoCorreoElectronico { get; set; }

        public string? CorreoElectronico { get; set; }
        public string? NombresApellidos { get; set; }
        public string? NumeroCelular { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Ruc { get; set; }
        public string? TipoDocumento { get; set; }
        public string? TipoPersona { get; set; }
        public int TipoValidacion { get; set; }

        public string Descripcion { get; set; }
        public int IdUser { get; set; }
        public List<ArchivoAdjunto> Documentos { get; set; }
    }
} 

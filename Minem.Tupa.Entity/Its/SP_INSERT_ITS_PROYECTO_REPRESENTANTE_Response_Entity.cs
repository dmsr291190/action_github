using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Its
{
    public class SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity
    {
        public long? idRepresentante { get; set; }              // ID del registro (PK)

        public string? nombreTitular { get; set; }              // Nombre completo del titular minero
        public long? ruc { get; set; }                        // RUC del titular
        public string? emailTitular { get; set; }               // Correo del titular

        public string? nombreRepresentante { get; set; }        // Nombres del representante legal
        public string? apellidoPaterno { get; set; }            // Apellido paterno del representante
        public string? apellidoMaterno { get; set; }            // Apellido materno del representante
        public string? cargo { get; set; }                      // Cargo del representante
        public string? documentoIdentidad { get; set; }         // Documento de identidad
        public string? emailRepresentante { get; set; }         // Correo del representante legal

        public int? estado { get; set; }                        // Estado del registro: 1 = Activo, 0 = Inactivo

        public long? usuarioRegistra { get; set; }              // Usuario que registró
        public string? fechaRegistra { get; set; }            // Fecha de registro

        public long? usuarioModifica { get; set; }              // Usuario que modificó
        public string? fechaModifica { get; set; }            // Fecha de modificación

        public string? nombreConsultora { get; set; }

        public string? objetivo { get; set; }
    }
}

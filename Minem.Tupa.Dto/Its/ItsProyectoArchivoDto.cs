using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Its
{
    public class ItsProyectoArchivoDto
    {
        public long? idArchivo { get; set; }              // ID del archivo (proporcionado por el servicio)
        public long? idProyecto { get; set; }             // ID del proyecto ITS
        public string? nombreArchivo { get; set; }        // Nombre del archivo cargado
        public string? fechaCarga { get; set; }         // Fecha en que se carga el archivo
        public int? seccion { get; set; }                 // Sección a la que pertenece el archivo
        public long? usuarioRegistra { get; set; }        // Usuario que registra
    }
}

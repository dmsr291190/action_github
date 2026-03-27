using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Formulario
{
    public class DocumentoInstitucionAdjuntos_Entity
    {
        public int Id_solicitud_opinante { get; set; }
        public int Id_Solicitud_Opinante_Respuesta_Adj { get; set; }
        public int Id_Solicitud_Opinante_Respuesta { get; set; }
        public string Nombre_Documento { get; set; }
        public int Id_Archivo { get; set; }
        public int Estado { get; set; }
        public decimal Usuario_Registra { get; set; }
        public DateTime Fecha_Registra { get; set; }
        public decimal Usuario_Modifica { get; set; }
        public DateTime Fecha_Modifica { get; set; }
    }
}

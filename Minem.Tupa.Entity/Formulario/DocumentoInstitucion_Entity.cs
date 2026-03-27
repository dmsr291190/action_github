using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Formulario
{
    public class DocumentoInstitucion_Entity
    {
        public int Id_solicitud_opinante_respuesta { get; set; }
        public int Id_solicitud_opinante { get; set; }
        public string Nro_expediente { get; set; }
        public DateTime? Fecha_expediente { get; set; }
        public int Id_asunto_adjunto { get; set; }
        public string Nro_oficio_notificado { get; set; }
        public DateTime? Fecha_notificacion { get; set; }
        public string Nro_cut { get; set; }
        public string Codigo_expediente_principal { get; set; }
        public string Ruc { get; set; }
        public string Usuario_externo { get; set; }
        public int Id_tipo_documento { get; set; }
        public string Descripcion_documento { get; set; }
        public string Asunto { get; set; }
        public string Codigo_expediente_anexado { get; set; }
        public int Codigo_resultado { get; set; }
        public string Envio_correo { get; set; }
        public int Estado { get; set; }
        public decimal Usuario_registra { get; set; }
        public DateTime Fecha_registra { get; set; }
        public decimal Usuario_modifica { get; set; }
        public DateTime? Fecha_modifica { get; set; }
        //***        
        public string Asunto_adjunto { get; set; }

        public int Notificar_impulso { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_INSERTAR_QUEMADOR_Request_Entity
    {
        public long infoQuemadorId { get; set; }
        public long informeId { get; set; }
        public string serie { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string fabricante { get; set; }
        public int estado { get; set; }
        public int? tipo { get; set; }
        public int? condicion { get; set; }
        public int? anioFabricacion { get; set; }
        public float? capNominal { get; set; }
        public float? capOperativa { get; set; }
        public float? altura { get; set; }
        public float? diametro { get; set; }
        public float? distanciaOtra { get; set; }
        public string? nombreInstalacion { get; set; }
        public int? encendidoAuto { get; set; }
        public string? latitud { get; set; }
        public string? longitud { get; set; }
        public long usuarioId { get; set; }
    }
}

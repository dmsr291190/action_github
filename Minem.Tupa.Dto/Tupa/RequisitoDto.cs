using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tupa
{
    public class RequisitoDto
    {
        public long Id { get; set; }
        public string? Requisito { get; set; }
        public bool? TieneCosto { get; set; }
        public decimal? Costo { get; set; }
        public string? TipoSolicitud { get; set; }
        public string? CodigoTributo { get; set; }
        public string? PagoVoucher { get; set; }
        public DateTime? PagoFecha { get; set; }
        public string? PagoOficina { get; set; }
        public string? Observaciones { get; set; }
        public int? Obligatorio { get; set; }
    }
}

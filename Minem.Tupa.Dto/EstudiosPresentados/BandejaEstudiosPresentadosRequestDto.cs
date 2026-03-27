using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.EstudiosPresentados
{
    public class BandejaEstudiosPresentadosRequestDto
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string? Filtro { get; set; }
        public int CodIdMaeTupa { get; set; }
        public int CodMaeEstado { get; set; }
        public string codMaeTupa { get; set; }
        
    }
}

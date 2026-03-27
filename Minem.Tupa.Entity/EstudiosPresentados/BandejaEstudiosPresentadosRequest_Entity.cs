using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.EstudiosPresentados
{
    public class BandejaEstudiosPresentadosRequest_Entity
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string? Filtro { get; set; }
        public int CodIdMaeTupa { get; set; }
        public int CodMaeEstado { get; set; }
        public string CodMaeTupa { get; set; }
        
    }
}

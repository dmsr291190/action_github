using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity
{
    public class Paginacion
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRows { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public int RecordCount { get; set; }
        public int PageCount { get; set; }
        public int First { get; set; }
        public int Last { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto
{
    public class PaginacionResultDto<T>
    {
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Result { get; set; }
        public int First { get; set; }
        public int Last { get; set; }
    }
}

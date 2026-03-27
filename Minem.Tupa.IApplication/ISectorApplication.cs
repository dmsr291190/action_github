using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface ISectorApplication
    {
        Task<StatusResponse<List<SectorDto>>> ObtenerSectores();
    }
}

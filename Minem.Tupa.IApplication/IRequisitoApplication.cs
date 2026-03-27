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
    public interface IRequisitoApplication
    {
        Task<StatusResponse<List<RequisitoDto>>> ObtenerRequisitos(string codigoTupa);
    }
}

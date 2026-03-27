using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Mapa;
using Minem.Tupa.Dto.Observacion;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface IMapaApplication
    {
        Task<StatusResponse<List<TipoAreaResponseDto>>> ObtenerTipoActividad(int tipo);
    }
}

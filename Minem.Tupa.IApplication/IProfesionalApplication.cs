using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.Dto.Profesional;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface IProfesionalApplication
    {
        Task<StatusResponse<List<ObtenerProfesionesResponseDto>>> ObtenerProfesiones();
    }
}

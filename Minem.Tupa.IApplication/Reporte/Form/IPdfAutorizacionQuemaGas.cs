using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication.Reporte.Form
{
    public interface IPdfAutorizacionQuemaGas
    {
        Task<StatusResponse<DescargarPlantillaDiaResponseDto>> Generar(int IdSolicitud);
    }
}

using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface IDocumentoApplication
    {
        Task<StatusResponse<DescargarPlantillaDiaResponseDto>> GenerarDocumentoFormulario(int idSolicitud);
    }
}

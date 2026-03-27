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
    public interface ITupaApplication
    {
        Task<StatusResponse<List<TupaDto>>> ObtenerTupaPorSector(long idSector, string tipoPersona);
        Task<StatusResponse<TupaDto>> ObtenerTupaPorCodigo(string codigoTupa);
        Task<StatusResponse<List<TupaDto>>> ObtenerTupa();
        Task<StatusResponse<List<DocumentoDespachadoDto>>> ObtenerDocumentosDespachados(long codMaeSolicitud);
    }
}

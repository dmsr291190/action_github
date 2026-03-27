using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface IFormularioApplication
    {
        Task<StatusResponse<long>> GuardarFormulario(GuardarFormularioRequestDto request);
        Task<StatusResponse<ObtenerFormularioDiaResponseDto>> ObtenerFormularioDia(long CodMaeSolicitud);
        Task<StatusResponse<DescargarPlantillaDiaResponseDto>> DescargarDocumento(DescargarPlantillaDiaRequestDto request);
        Task<StatusResponse<long>> GuardarResumenEjecutivo(GuardarFormularioRequestDto request);
        Task<StatusResponse<long>> ActualizarFormulario(long p_CodMaeSolicitud, string jsonData);
        Task<StatusResponse<List<TransactionListOpinantesResponseDto>>> GetTransactionListOpinantes(int codMaeSolicitud, int codSolicitudExpediente);
        Task<StatusResponse<List<DocumentoInstitucionResponseDto>>> ListarDocumentosInstitucion(int codMaeSolicitud, int codSolicitudExpediente);
        Task<StatusResponse<List<DocumentoInstitucionAdjuntosResponseDto>>> ListarDocumentosInstitucionAdjuntos(int codMaeSolicitud, int codSolicitudExpediente);
        Task<StatusResponse<TransactionResumeDataResponseDto>> GetTransactionResumeData(int codMaeSolicitud);
    }
}

using Minem.Tupa.Entity;
using Minem.Tupa.Entity.Formulario;

namespace Minem.Tupa.IRepository
{
    public interface IFormularioRepository
    {
        Task<long> GuardarFormulario(long p_CodMaeSolicitud, string p_DataJson);
        Task<USP_S_OBTENER_FORMULARIO_DIA_Response_Entity> ObtenerFormularioDia(long codMaeSolicitud);
        Task<long> GuardarResumenEjecutivo(long p_CodMaeSolicitud, string p_DataJson, string p_User);
        Task<DP_S_OBTENER_RESUMEN_EJECUTIVO> ObtenerResumenEjecutivoPorCodigoSolicitud(long codigoSolicitud);
        Task<long> ActualizarFormulario(long p_CodMaeSolicitud, string jsonData);
        Task<List<TransactionListOpinantes>> GetTransactionListOpinantes(int codMaeSolicitud, int codSolicitudExpediente);
        Task<List<DocumentoInstitucion_Entity>> ListarDocumentosInstitucion(int codMaeSolicitud, int codSolicitudExpediente);
        Task<List<DocumentoInstitucionAdjuntos_Entity>> ListarDocumentosInstitucionAdjuntos(int codMaeSolicitud, int codSolicitudExpediente);
        Task<TransactionResumeData> GetTransactionResumeData(int codMaeSolicitud);
    }
}

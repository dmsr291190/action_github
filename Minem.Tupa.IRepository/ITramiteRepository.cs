using Microsoft.Win32;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Entity;
using Minem.Tupa.Entity.Tramite;
using System;
using System.Data;

namespace Minem.Tupa.IRepository
{
    public interface ITramiteRepository
    {
        Task<long> GenerarTramite(int idtupa, string tipopersona, string tipodoc, string numdoc, string tiposol);
        Task<List<SP_OBTENER_NOTAS_Response_Entity>> ObtenerNotas(SP_OBTENER_NOTAS_Request_Entity request);
        Task<List<SP_OBTENER_TRAMITE_DOCS_Response_Entity>> ObtenerDocumentos(SP_OBTENER_TRAMITE_DOCS_Request_Entity request);
        Task<SP_OBTENER_TRAMITE_Response_Entity> ObtenerTramite(SP_OBTENER_TRAMITE_Request_Entity request);
        Task<List<SP_OBTENER_REQUISITOS_TRAMITE_Response_Entity>> ObtenerTramiteRequisitos(SP_OBTENER_TRAMITE_Request_Entity request);
        Task<long> InsertarSolicitud(int idTupa, int idPersona, int idUsuario, int estado);

        Task<long> ActualizarPadreITS(long codMaeSolicitud, long codMaeSolicitudPadre);
        Task<long> InsertarDetalleSolicitud(int p_CodMaeSolicitud, long? p_CodMaeRequisito, int p_CodMovPersona,
            int p_CodMaeEntFinanciera, string p_NomArchivo, string p_UrlArchivo, int p_TamArchivo, string p_TipArchivo, string p_CodPago,
            string p_Descripcion, int p_RegUsuaRegistra, string p_CodUniPasarela, string p_CodVoucher, int? p_CodMaeConCatRequisito, int p_IndAceCompromiso);
        Task<long> AsignarSolicitudAlFuncionario(int p_CodMaeSolicitud, int p_CodMaeEstado, int p_CodPerEmisor, int p_CodUsuarioRegistra, int? p_NroExpediente);
        Task<SP_OBTENER_DATOS_SOLICITUD_Response_Entity> ObtenerDatosSolicitud(long codMaeSolicitud);
        Task<long> ActualizarSolicitud(int p_CodMaeEstado, int p_CodMaeSolicitud, int? p_NumExpediente, int idCliente);
        Task<List<USP_S_OBTENER_MIS_TRAMITES_Response_Entity>> ObtenerMisTramites(int codMovPersona, int codEstSolicitud);
        Task<List<USP_S_OBTENER_DIA_APROBADO_Response_Entity>> ObtenerDIAAprobado(int codMovPersona, int codEstSolicitud);
        Task<long> ActualizarArchivoAdjunto(int p_CodMaeSolicitud, int p_CodMaeRequisito, int p_NumDocumento, string p_NomArchivo);
        Task<long> GenerarDocumentoAdicional(long p_CodMaeSolicitud, string p_NomArchivo, long p_NumDocRespuesta, long p_TipoTramite, long p_TipoDocumento, string p_NumeroDocumento, string p_Descripcion, long p_Uusuario);
        Task<List<USP_S_OBTENER_DOCUMENTOS_ADCIONALES_Response_Entity>> ObtenerDocumentosAdicionales(long idTramite);
        Task<List<USP_S_OBTENER_DOCUMENTOS_ADCIONALES_DETALLE_Response_Entity>> ObtenerDocumentosAdicionalesDetalle(long idTramiteAdicional);
        Task<long> EliminarDocumentoAdicional(long p_NumDocumento);
        Task<long> InsertarDataFormulario(Int64 id, string p_DataJson, long p_CodMaeSolicitud, string usuario, Int64 p_estado, Int64 p_notificado);
        Task<long> ActualizarIdEstudio(long p_CodMaeSolicitud, long p_IdEstudio);
        Task<long> AnularTramite(long p_CodMaeSolicitud);
        Task<List<USP_S_OBTENER_TIPO_COMUNICACION_Response_Entity>> ObtenerTipoComunicacion(int codMaeFiltro);
        Task<List<USP_S_OBTENER_TIPO_DOCUMENTO_Response_Entity>> ObtenerTipoDocumento(string codMaeTipoDocumento);
        Task<long> RegistrarNombreProyecto(long p_CodMaeSolicitud, string p_NombreProyecto);
        Task<long> ValidarNombreProyecto(long p_CodMaeSolicitud, string p_NombreProyecto);

        Task<long> ActualizarTramiteAdicional(int p_CodMaeEstado, int p_CodMaeSolicitud, int? p_NumExpediente, int p_TipoComunicacion);
        Task<SP_INSERT_REGISTRO_DIA_Response_Entity> RegistrarEstudio(long p_IdCliente);
        Task<SP_OBTENER_DATOS_DETALLADO_SOLICITUD_Response_Entity> ObtenerDatosDetalladoSolicitud(long codMaeSolicitud);
        Task<List<PRC_USP_S_LISTAR_TRAZASIGNACION_Response_Entity>> VerTrazabilidad(long codMaeSolicitud);
        Task<long> ActualizarExpedienteTrazabilidad(long idTumovTupAsignacion, long idSolicitudExpediente);
        Task<SP_OBTENER_TIPO_DOCUMENTO_NOTIFICADO_Response_Entity> GetNombreTipoDocumento(long codMaeSolicitud, long idArchivo);             
        Task<SituacionSolicitudFinal_Entity> ObtenerSituacionSolicitudFinal(int codMaeSolicitud);
        Task<long> ActualizarSituacionAdmisibilidadTumaesolicitud(int codMaeSolicitud, int codSituacion, int codEtapa);
        Task<long> ActualizarSituacionAdmisibilidadFuncionario(RegistrarAdmisibilidadFuncionarioDto request);
        Task<long> InsertarCambioSituacionDeLaAsignacion(long idTumovTupAsignacion);
        Task<long> ActualizarFechaFinAdmisibilidad(int codIdMaeTupa, int codMaeSolicitud);
        Task<long> ActualizarArchivoRequisito2(int codMaeSolicitud, int idArchivo, string nomArchivo, string extArchivo);
        Task<List<ArchivoRequisito2_Entity>> ObtenerArchivoRequisito2(int codMaeSolicitud);
        Task<TuDetSolicitud> ObtenerDatoPagoTramite(int codDetSolicitud);
        Task<long> RegistrarPagoSolicitud(TuDetSolicitud request);
        Task<List<TuDetSolicitud>> ObtenerDatoPagoPorIdSolicitud(int idSolicitud);
        Task<long> GuardarArchivoRequisitoSolicitud(SP_INSERT_ARCHIVO_REQUISITO_SOLICITUD_Request_Entity request);
        Task<long> ActualizarDetalleSolicitud(long solicitudId, long? requisitoId, int codEstado);
        Task<int> ActualizarEstadoRequisitosParaRevision(long solicitudId, int codEstado);
        Task<int> ObtenerCantidadRequisitosPendientes(long solicitudId);
    }
}

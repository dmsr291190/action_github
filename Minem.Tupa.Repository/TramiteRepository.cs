using Microsoft.EntityFrameworkCore;
using Minem.Tupa.Data;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Entity;
using Minem.Tupa.Entity.Tramite;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Minem.Tupa.Repository
{
    public class TramiteRepository(Minem_Db_Context _minemDbContext) : ITramiteRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

        public async Task<long> GenerarTramite(int idtupa, string tipopersona, string tipodoc, string numdoc, string tiposol)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDPROC", OracleDbType.Int32, idtupa, ParameterDirection.Input),
                new OracleParameter("P_TIPO_PERSONA", OracleDbType.Varchar2, tipopersona, ParameterDirection.Input),
                new OracleParameter("P_TIPODOC", OracleDbType.Varchar2, tipodoc, ParameterDirection.Input),
                new OracleParameter("P_NUMERODOC", OracleDbType.Varchar2, numdoc, ParameterDirection.Input),
                new OracleParameter("P_TIPOSOLICITUD", OracleDbType.Varchar2, tiposol, ParameterDirection.Input),
                //new OracleParameter("P_IDTRAMITE", OracleDbType.Int64)

            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_TRAMITE_BANDEJA.SP_GENERAR_NUEVO", param, "P_IDTRAMITE");
        }

        public async Task<List<SP_OBTENER_NOTAS_Response_Entity>> ObtenerNotas(SP_OBTENER_NOTAS_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDPROC", OracleDbType.Int32, request.IdProcedimiento, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_NOTAS_Response_Entity>("PCK_TRAMITE_BANDEJA.SP_OBTENER_NOTAS", param);
        }

        public async Task<List<SP_OBTENER_TRAMITE_DOCS_Response_Entity>> ObtenerDocumentos(SP_OBTENER_TRAMITE_DOCS_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDTRAMITE", OracleDbType.Int32, request.IdTramite, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_TRAMITE_DOCS_Response_Entity>("PCK_TRAMITE_BANDEJA.SP_OBTENER_TRAMITE_DOCS", param);
        }

        public async Task<SP_OBTENER_TRAMITE_Response_Entity> ObtenerTramite(SP_OBTENER_TRAMITE_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDTRAMITE", OracleDbType.Int32, request.IdTramite, ParameterDirection.Input),
                new OracleParameter("P_CODIGOTUPA", OracleDbType.Varchar2, request.CodigoTupa, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToEntity<SP_OBTENER_TRAMITE_Response_Entity>("PCK_ADMINISTRADO.SP_OBTENER_TRAMITE", param);
        }

        public async Task<List<SP_OBTENER_REQUISITOS_TRAMITE_Response_Entity>> ObtenerTramiteRequisitos(SP_OBTENER_TRAMITE_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDTRAMITE", OracleDbType.Int32, request.IdTramite, ParameterDirection.Input),
                new OracleParameter("P_CODIGOTUPA", OracleDbType.Varchar2, request.CodigoTupa, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_REQUISITOS_TRAMITE_Response_Entity>("PCK_ADMINISTRADO.SP_OBTENER_REQUISITOS", param);
        }

        public async Task<long> InsertarSolicitud(int idTupa, int idPersona, int idUsuario, int estado)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMovPersona", OracleDbType.Int32, idPersona, ParameterDirection.Input),
                new OracleParameter("p_CodIdMaeTupa", OracleDbType.Int32, idTupa, ParameterDirection.Input),
                new OracleParameter("p_CodMaeEstado", OracleDbType.Int32, estado, ParameterDirection.Input),
                new OracleParameter("p_CodMaeCatRequisito", OracleDbType.Int32, 0, ParameterDirection.Input),
                new OracleParameter("p_CodDetCatRequisito", OracleDbType.Int32, 0, ParameterDirection.Input),
                new OracleParameter("p_RegUsuaRegistra", OracleDbType.Int32, idUsuario, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_I_INSERTAR_SOLICITUD", param, "o_CodMaeSolicitud");
        }

        public async Task<long> ActualizarPadreITS(long codMaeSolicitud, long codMaeSolicitudPadre)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitudOrigen", OracleDbType.Int64, codMaeSolicitudPadre, ParameterDirection.Input),
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int64, codMaeSolicitud, ParameterDirection.Input),
                //new OracleParameter("p_Resultado", OracleDbType.Int64, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ITS.USP_U_ACTUALIZAR_SOLICITUD_ITS", param, "p_Resultado");
        }

        public async Task<long> InsertarDetalleSolicitud(int p_CodMaeSolicitud, long? p_CodMaeRequisito, int p_CodMovPersona,
            int p_CodMaeEntFinanciera, string p_NomArchivo, string p_UrlArchivo, int p_TamArchivo, string p_TipArchivo, string p_CodPago,
            string p_Descripcion, int p_RegUsuaRegistra, string p_CodUniPasarela, string p_CodVoucher, int? p_CodMaeConCatRequisito, int p_IndAceCompromiso)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int32, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_CodMaeRequisito", OracleDbType.Int32, p_CodMaeRequisito, ParameterDirection.Input),
                new OracleParameter("p_CodMovPersona", OracleDbType.Int32, p_CodMovPersona, ParameterDirection.Input),
                new OracleParameter("p_CodMaeEntFinanciera", OracleDbType.Int32, p_CodMaeEntFinanciera, ParameterDirection.Input),
                new OracleParameter("p_NomArchivo", OracleDbType.Varchar2, p_NomArchivo, ParameterDirection.Input),
                new OracleParameter("p_UrlArchivo", OracleDbType.Varchar2, p_UrlArchivo, ParameterDirection.Input),
                new OracleParameter("p_TamArchivo", OracleDbType.Int32, p_TamArchivo, ParameterDirection.Input),
                new OracleParameter("p_TipArchivo", OracleDbType.Varchar2, p_TipArchivo, ParameterDirection.Input),
                new OracleParameter("p_CodPago", OracleDbType.Varchar2, p_CodPago, ParameterDirection.Input),
                new OracleParameter("p_Descripcion", OracleDbType.Varchar2, p_Descripcion, ParameterDirection.Input),
                new OracleParameter("p_RegUsuaRegistra", OracleDbType.Int32, p_RegUsuaRegistra, ParameterDirection.Input),
                new OracleParameter("p_CodUniPasarela", OracleDbType.Varchar2, p_CodUniPasarela, ParameterDirection.Input),
                new OracleParameter("p_CodVoucher", OracleDbType.Varchar2, p_CodVoucher, ParameterDirection.Input),
                new OracleParameter("p_CodMaeConCatRequisito", OracleDbType.Int32, p_CodMaeConCatRequisito, ParameterDirection.Input),
                new OracleParameter("p_IndAceCompromiso", OracleDbType.Int32, p_IndAceCompromiso, ParameterDirection.Input)
            ];

            return await _db.ExecuteNonQueryProcedure("PCK_ADMINISTRADO.USP_I_DETALLESOLICITUD", param);
        }

        public async Task<long> AsignarSolicitudAlFuncionario(int p_CodMaeSolicitud, int p_CodMaeEstado, int p_CodPerEmisor, int p_CodUsuarioRegistra, int? p_NroExpediente)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int32, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_CodMaeEstado", OracleDbType.Int32, p_CodMaeEstado, ParameterDirection.Input),
                new OracleParameter("p_CodPerEmisor", OracleDbType.Int32, p_CodPerEmisor, ParameterDirection.Input),
                new OracleParameter("p_CodUsuarioRegistra", OracleDbType.Int32, p_CodUsuarioRegistra, ParameterDirection.Input),
                new OracleParameter("p_NumExpediente", OracleDbType.Int32, p_NroExpediente, ParameterDirection.Input),

            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_I_ASIGNAR_SOLICITUD_AL_FUNCIONARIO", param, "p_Resultado");
        }

        public async Task<SP_OBTENER_DATOS_SOLICITUD_Response_Entity> ObtenerDatosSolicitud(long codMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToEntity<SP_OBTENER_DATOS_SOLICITUD_Response_Entity>("PCK_ADMINISTRADO.SP_OBTENER_DATOS_SOLICITUD", param);
        }


        public async Task<long> ActualizarSolicitud(int p_CodMaeEstado, int p_CodMaeSolicitud, int? p_NumExpediente, int p_IdCliente)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeEstado", OracleDbType.Int32, p_CodMaeEstado, ParameterDirection.Input),
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int32, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_NumExpediente", OracleDbType.Int32, p_NumExpediente, ParameterDirection.Input),
                new OracleParameter("p_IdCliente", OracleDbType.Int32, p_IdCliente, ParameterDirection.Input),
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ACTUALIZAR_SOLICITUD", param, "p_Resultado");
        }

        public async Task<List<USP_S_OBTENER_MIS_TRAMITES_Response_Entity>> ObtenerMisTramites(int codMovPersona, int codEstSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMOVPERSONA", OracleDbType.Int32, codMovPersona, ParameterDirection.Input),
                new OracleParameter("P_CODESTSOLICITUD", OracleDbType.Int32, codEstSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<USP_S_OBTENER_MIS_TRAMITES_Response_Entity>("PCK_ADMINISTRADO.USP_S_OBTENER_MIS_TRAMITES", param);
        }

        public async Task<List<USP_S_OBTENER_DIA_APROBADO_Response_Entity>> ObtenerDIAAprobado(int codMovPersona, int codEstSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMOVPERSONA", OracleDbType.Int32, codMovPersona, ParameterDirection.Input),
                new OracleParameter("P_CODESTSOLICITUD", OracleDbType.Int32, codEstSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<USP_S_OBTENER_DIA_APROBADO_Response_Entity>("PCK_ITS.USP_S_OBTENER_DIA_APROBADO", param);
        }

        public async Task<long> ActualizarArchivoAdjunto(int p_CodMaeSolicitud, int p_CodMaeRequisito, int p_NumDocumento, string p_NomArchivo)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_CODMAEREQUISITO", OracleDbType.Int32, p_CodMaeRequisito, ParameterDirection.Input),
                new OracleParameter("P_NUMDOCUMENTO", OracleDbType.Int32, p_NumDocumento, ParameterDirection.Input),
                new OracleParameter("P_NOMARCHIVO", OracleDbType.Varchar2, p_NomArchivo, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ACTUALIZAR_ARCHIVOS", param, "p_Resultado");
        }

        public async Task<long> GenerarDocumentoAdicional(long p_CodMaeSolicitud, string p_NomArchivo, long p_NumDocRespuesta, long p_TipoTramite, long p_TipoDocumento, string p_NumeroDocumento, string p_Descripcion, long p_Usuario)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int64, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_NomArchivo", OracleDbType.Varchar2, p_NomArchivo, ParameterDirection.Input),
                new OracleParameter("p_NumDocRespuesta", OracleDbType.Int64, p_NumDocRespuesta, ParameterDirection.Input),
                new OracleParameter("p_TipoTramite", OracleDbType.Int64, p_TipoTramite, ParameterDirection.Input),
                new OracleParameter("p_TipoDocumento", OracleDbType.Int64, p_TipoDocumento, ParameterDirection.Input),
                new OracleParameter("p_NumeroDocumento", OracleDbType.Varchar2, p_NumeroDocumento, ParameterDirection.Input),
                new OracleParameter("p_Descripcion", OracleDbType.Varchar2, p_Descripcion, ParameterDirection.Input),
                new OracleParameter("p_Usuario", OracleDbType.Varchar2, p_Usuario, ParameterDirection.Input)

            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_I_INSERTAR_DOCUMENTOS_ADICIONALES", param, "p_Resultado");
        }

        public async Task<List<USP_S_OBTENER_DOCUMENTOS_ADCIONALES_Response_Entity>> ObtenerDocumentosAdicionales(long idTramite)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, idTramite, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<USP_S_OBTENER_DOCUMENTOS_ADCIONALES_Response_Entity>("PCK_ADMINISTRADO.USP_S_OBTENER_DOCUMENTOS_ADICIONALES", param);
        }

        public async Task<List<USP_S_OBTENER_DOCUMENTOS_ADCIONALES_DETALLE_Response_Entity>> ObtenerDocumentosAdicionalesDetalle(long idTramiteAdicional)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDTRAMITESADICIONALES", OracleDbType.Int32, idTramiteAdicional, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<USP_S_OBTENER_DOCUMENTOS_ADCIONALES_DETALLE_Response_Entity>("PCK_ADMINISTRADO.USP_S_OBTENER_DOCUMENTOS_ADICIONALES_DETALLE", param);
        }

        public async Task<long> EliminarDocumentoAdicional(long p_NumDocumento)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAERESPUESTA", OracleDbType.Varchar2, p_NumDocumento.ToString(), ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ELIMINAR_DOCUMENTOS_ADICIONALES", param, "p_Resultado");
        }

        public async Task<long> InsertarDataFormulario(Int64 id, string p_DataJson, Int64 p_CodMaeSolicitud, string usuario, Int64 p_estado, Int64 p_notificado)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_ID", OracleDbType.Int64, id, ParameterDirection.Input),
                new OracleParameter("P_DATAJSON", OracleDbType.Clob, p_DataJson, ParameterDirection.Input),
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_CREACION", OracleDbType.Varchar2, usuario, ParameterDirection.Input),
                new OracleParameter("P_ESTADO", OracleDbType.Int64, p_estado, ParameterDirection.Input),
                new OracleParameter("P_NOTIFICADO", OracleDbType.Int64, p_notificado, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_I_INSERTAR_TUMOVMETADAHISTORICO", param, "P_RESULTADO");
        }

        public async Task<long> ActualizarIdEstudio(long p_CodMaeSolicitud, long p_IdEstudio)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int64, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_IdEstudio", OracleDbType.Int64, p_IdEstudio, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ACTUALIZAR_ID_ESTUDIO", param, "p_Resultado");
        }

        public async Task<long> AnularTramite(long p_CodMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int64, p_CodMaeSolicitud, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ANULAR_SOLICITUD", param, "p_Resultado");
        }

        public async Task<List<USP_S_OBTENER_TIPO_COMUNICACION_Response_Entity>> ObtenerTipoComunicacion(int codMaeFiltro)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAEFILTRO", OracleDbType.Int32, codMaeFiltro, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<USP_S_OBTENER_TIPO_COMUNICACION_Response_Entity>("PCK_ADMINISTRADO.SP_OBTENER_DATOS_TIPO_COMUNICACION", param);
        }

        public async Task<List<USP_S_OBTENER_TIPO_DOCUMENTO_Response_Entity>> ObtenerTipoDocumento(string codMaeTipoDocumento)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAETIPODOCUMENTO", OracleDbType.Varchar2, codMaeTipoDocumento, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<USP_S_OBTENER_TIPO_DOCUMENTO_Response_Entity>("PCK_ADMINISTRADO.SP_OBTENER_DATOS_TIPO_DOCUMENTO", param);
        }

        public async Task<long> RegistrarNombreProyecto(long p_CodMaeSolicitud, string p_NombreProyecto)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_NombreProyecto", OracleDbType.Varchar2, p_NombreProyecto, ParameterDirection.Input),
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int64, p_CodMaeSolicitud, ParameterDirection.Input)

            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_I_INSERTAR_NOMBRE_PROYECTO", param, "p_Resultado");
        }

        public async Task<long> ValidarNombreProyecto(long p_CodMaeSolicitud, string p_NombreProyecto)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_NombreProyecto", OracleDbType.Varchar2, p_NombreProyecto, ParameterDirection.Input),
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int64, p_CodMaeSolicitud, ParameterDirection.Input)

            ];

            var resultado = await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_V_VALIDAR_NOMBRE_PROYECTO", param, "p_Resultado");

            return resultado;
        }

        public async Task<long> ActualizarTramiteAdicional(int p_CodMaeEstado, int p_CodMaeSolicitud, int? p_NumExpediente, int p_TipoComunicacion)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeEstado", OracleDbType.Int32, p_CodMaeEstado, ParameterDirection.Input),
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int32, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_NumExpediente", OracleDbType.Int32, p_NumExpediente, ParameterDirection.Input),
                new OracleParameter("p_TipoComunicacion", OracleDbType.Int32, p_TipoComunicacion, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ACTUALIZAR_TRAMITE_ADICIONAL", param, "p_Resultado");
        }

        public async Task<SP_INSERT_REGISTRO_DIA_Response_Entity> RegistrarEstudio(long p_IdCliente)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
                [
                    new OracleParameter("Ln_IdCliente", OracleDbType.Int64, p_IdCliente, ParameterDirection.Input),
                    new OracleParameter("Ln_IdProcedimiento", OracleDbType.Int32, (int)Enumeration.Procedimientos.FormularioDIA, ParameterDirection.Input),
                    new OracleParameter("Ln_IdEstudioMadre", OracleDbType.Int32, null, ParameterDirection.Input),
                    new OracleParameter("Ls_NombreProyecto", OracleDbType.Varchar2, null, ParameterDirection.Input),
                    new OracleParameter("Ln_IdFuente", OracleDbType.Int32, null, ParameterDirection.Input),
                    new OracleParameter("Ls_Usuario", OracleDbType.Varchar2, string.Empty, ParameterDirection.Input),
                    new OracleParameter("Ls_IpIngreso", OracleDbType.Varchar2, string.Empty, ParameterDirection.Input),
                   new OracleParameter("Lr_Recordset", OracleDbType.RefCursor,ParameterDirection.Output)
                ];

            return await _db.ExecuteProcedureToEntity<SP_INSERT_REGISTRO_DIA_Response_Entity>("PCK_GEOMETRY.SP_INSERT_REGISTRO_DIA", param);
        }

        public async Task<SP_OBTENER_DATOS_DETALLADO_SOLICITUD_Response_Entity> ObtenerDatosDetalladoSolicitud(long codMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToEntity<SP_OBTENER_DATOS_DETALLADO_SOLICITUD_Response_Entity>("PCK_ADMINISTRADO.SP_OBTENER_DATOS_DETALLADO_SOLICITUD", param);
        }

        public async Task<List<PRC_USP_S_LISTAR_TRAZASIGNACION_Response_Entity>> VerTrazabilidad(long codMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<PRC_USP_S_LISTAR_TRAZASIGNACION_Response_Entity>("PCK_BANDEJA.PRC_USP_S_LISTAR_TRAZASIGNACION", param);
        }

        public async Task<long> ActualizarExpedienteTrazabilidad(long idTumovTupAsignacion, long idSolicitudExpediente)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_IdTumovTupAsignacion", OracleDbType.Int64, idTumovTupAsignacion, ParameterDirection.Input),
                new OracleParameter("p_IdSolicitudExpediente", OracleDbType.Int64, idSolicitudExpediente, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.Int64, ParameterDirection.Output)
            ];

            // return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ACTUALIZAR_EXPEDIENTE_TRAZABILIDAD", param, "p_Resultado");


            string nombreProcedimiento = string.Format("{0}.{1}", "PCK_ADMINISTRADO", "USP_U_ACTUALIZAR_EXPEDIENTE_TRAZABILIDAD");
            await _db.ExecuteNonQueryProcedure(nombreProcedimiento, param);

            var outputParameter = param.Find(p => p.ParameterName == "p_Resultado");

            return outputParameter?.Value is OracleDecimal oracleDecimalValue ? oracleDecimalValue.ToInt32() : Convert.ToInt32(outputParameter?.Value);
        }

        public async Task<SP_OBTENER_TIPO_DOCUMENTO_NOTIFICADO_Response_Entity> GetNombreTipoDocumento(long codMaeSolicitud, long idArchivo)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_IDARCHIVO", OracleDbType.Int32, idArchivo, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToEntity<SP_OBTENER_TIPO_DOCUMENTO_NOTIFICADO_Response_Entity>("PCK_ADMINISTRADO.SP_OBTENER_TIPO_DOCUMENTO_NOTIFICADO", param);
        }
        public async Task<SituacionSolicitudFinal_Entity> ObtenerSituacionSolicitudFinal(int codMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
            new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToEntity<SituacionSolicitudFinal_Entity>("PCK_ADMINISTRADO.PRC_USP_S_OBTENER_SITUACION_SOLICITUD_FINAL", param);
        }

        public async Task<long> ActualizarSituacionAdmisibilidadTumaesolicitud(int codMaeSolicitud, int codSituacion, int codEtapa)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_IDSITUACION", OracleDbType.Int32, codSituacion, ParameterDirection.Input),
                new OracleParameter("P_IDETAPA", OracleDbType.Int32, codEtapa, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMISIBILIDAD.PRC_USP_U_SITUACION_SOLICITUD_TUMAESOLICITUD", param, "P_RESULT");
        }

        public async Task<long> ActualizarSituacionAdmisibilidadFuncionario(RegistrarAdmisibilidadFuncionarioDto request)
        {
            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> param =
                [
                    new OracleParameter("P_CODMOVTUPASIGNACION", OracleDbType.Int32, request.CodMovtupasignacion, ParameterDirection.Input),
                    new OracleParameter("P_CODMAEESTADO", OracleDbType.Int32, request.CodMaeEstado, ParameterDirection.Input),
                    new OracleParameter("P_IDSITUACION", OracleDbType.Int32, request.CodSituacion, ParameterDirection.Input),
                    new OracleParameter("P_IDETAPA", OracleDbType.Int32, request.CodEtapa, ParameterDirection.Input),
                ];

                return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMISIBILIDAD.PRC_USP_U_SITUACION_SOLICITUD_FUNCIONARIO", param, "P_RESULT");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> InsertarCambioSituacionDeLaAsignacion(long idTumovTupAsignacion)
        {
            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> param =
                [
                    new OracleParameter("p_IdTumovTupAsignacion", OracleDbType.Int64, idTumovTupAsignacion, ParameterDirection.Input),
                    new OracleParameter("p_Resultado", OracleDbType.Int64, ParameterDirection.Output)
                ];

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_ADMISIBILIDAD", "USP_I_CAMBIOS_SITUACION_ASIGNACION");
                await _db.ExecuteNonQueryProcedure(nombreProcedimiento, param);

                var outputParameter = param.Find(p => p.ParameterName == "p_Resultado");

                return outputParameter?.Value is OracleDecimal oracleDecimalValue ? oracleDecimalValue.ToInt32() : Convert.ToInt32(outputParameter?.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> ActualizarFechaFinAdmisibilidad(int codIdMaeTupa, int codMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODIDMAETUPA", OracleDbType.Int32, codIdMaeTupa, ParameterDirection.Input),
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMISIBILIDAD.PRC_USP_U_FECHA_FIN_ADMISIBILIDAD_TUMAESOLICITUD", param, "P_RESULT");
        }

        public async Task<long> ActualizarArchivoRequisito2(int codMaeSolicitud, int idArchivo, string nomArchivo, string extArchivo)
        {
            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> param =
                [
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_IDARCHIVOREQUISITO2", OracleDbType.Int64, idArchivo, ParameterDirection.Input),
                new OracleParameter("P_NOMARCHIVOREQUISITO2", OracleDbType.Varchar2, nomArchivo, ParameterDirection.Input),
                new OracleParameter("P_EXTARCHIVOREQUISITO2", OracleDbType.Varchar2, extArchivo, ParameterDirection.Input)
                ];

                return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ACTUALIZAR_ARCHIVO_REQUISITO_2", param, "P_Resultado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //nuevo
        public async Task<List<ArchivoRequisito2_Entity>> ObtenerArchivoRequisito2(int codMaeSolicitud)
        {
            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> param =
                [
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

                ];

                return await _db.ExecuteProcedureToList<ArchivoRequisito2_Entity>("PCK_ADMINISTRADO.SP_OBTENER_ARCHIVO_REQUISITO_2", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<TuDetSolicitud> ObtenerDatoPagoTramite(int codDetSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODDETSOLICITUD", OracleDbType.Int32, codDetSolicitud, ParameterDirection.Input),
                new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToEntity<TuDetSolicitud>("PCK_ADMINISTRADO.PRC_OBTENER_DATOS_PAGO_EXPEDIENTE", param);
        }

        public async Task<long> RegistrarPagoSolicitud(TuDetSolicitud request)
        {
            using (var cn = new OracleConnection(_connectionString))
            {
                using (var cmd = new OracleCommand())
                {
                    string nombreProcedimiento = "PCK_ADMINISTRADO.PRC_I_INSERTAR_TUDETSOLICITUD";

                    cmd.CommandText = nombreProcedimiento;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cn;


                    List<OracleParameter> oracleParameters = new List<OracleParameter>()
                    {
                        new OracleParameter("PI_CODMAESOLICITUD", OracleDbType.Int64, request.CODMAESOLICITUD, ParameterDirection.Input),
                new OracleParameter("PI_CODDETCONCATREQUISITO", OracleDbType.Int64, request.CODDETCONCATREQUISITO, ParameterDirection.Input),
                new OracleParameter("PI_CODUNIPASARELA", OracleDbType.Varchar2, request.CODUNIPASARELA, ParameterDirection.Input),
                new OracleParameter("PI_CODVOUCHER", OracleDbType.Varchar2, request.CODVOUCHER, ParameterDirection.Input),
                new OracleParameter("PI_REGUSUAREGISTRA", OracleDbType.Int64, request.REGUSUAREGISTRA, ParameterDirection.Input),
                new OracleParameter("PI_IDTIPOPAGO", OracleDbType.Int64, request.ID_TIPO_PAGO, ParameterDirection.Input),
                new OracleParameter("PI_CODIGOCAJA", OracleDbType.Varchar2, request.CODIGO_CAJA, ParameterDirection.Input),
                new OracleParameter("PI_FECHAPAGO", OracleDbType.Date, request.FECHA_PAGO, ParameterDirection.Input),
                new OracleParameter("PI_IMPORTE", OracleDbType.Decimal, request.IMPORTE, ParameterDirection.Input),
                new OracleParameter("PI_NROSECUENCIA", OracleDbType.Varchar2, request.NRO_SECUENCIA, ParameterDirection.Input),
                new OracleParameter("PI_CODIGOOFICINA", OracleDbType.Varchar2, request.CODIGO_OFICINA, ParameterDirection.Input),
                new OracleParameter("PO_CODDETSOLICITUD", OracleDbType.Int64, ParameterDirection.Output)
                    };

                    cmd.Parameters.AddRange(oracleParameters.ToArray());
                    cn.Open();
                    try
                    {
                        await cmd.ExecuteScalarAsync();
                        int codRegistro = 0;
                        int.TryParse(cmd.Parameters["PO_CODDETSOLICITUD"].Value.ToString(), out codRegistro);
                        return codRegistro;
                    }
                    finally
                    {
                        cmd.Connection.Close();
                        cmd.Dispose();
                    }
                }
            }

        }


        public async Task<List<TuDetSolicitud>> ObtenerDatoPagoPorIdSolicitud(int idSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, idSolicitud, ParameterDirection.Input),
                new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<TuDetSolicitud>("PCK_ADMINISTRADO.PRC_OBTENER_DATOS_PAGO_POR_CODMAESOLCITUD", param);
        }

        public async Task<long> GuardarArchivoRequisitoSolicitud(SP_INSERT_ARCHIVO_REQUISITO_SOLICITUD_Request_Entity request)
        {
            using (var cn = new OracleConnection(_connectionString))
            {
                using (var cmd = new OracleCommand())
                {
                    string nombreProcedimiento = "PCK_ADMINISTRADO.PRC_GUARDAR_ARCHIVO_REQUISITO_SOLICITUD";

                    cmd.CommandText = nombreProcedimiento;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cn;


                    List<OracleParameter> oracleParameters = new List<OracleParameter>()
                    {
                        new OracleParameter("P_ARCHIVO_ID", OracleDbType.Int64, request.archivoId, ParameterDirection.Input),
                        new OracleParameter("P_SOLICITUD_ID", OracleDbType.Int64, request.solicitudId, ParameterDirection.Input),
                        new OracleParameter("P_REQUISITO_ID", OracleDbType.Varchar2, request.requisitoId, ParameterDirection.Input),
                        new OracleParameter("P_NOMBRE_ARCHIVO", OracleDbType.Varchar2, request.nombreArchivo, ParameterDirection.Input),
                        new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input),
                        new OracleParameter("P_RESULTADO", OracleDbType.Int64, ParameterDirection.Output)
                    };

                    cmd.Parameters.AddRange(oracleParameters.ToArray());
                    cn.Open();
                    try
                    {
                        await cmd.ExecuteScalarAsync();
                        int codRegistro = 0;
                        int.TryParse(cmd.Parameters["P_RESULTADO"].Value.ToString(), out codRegistro);
                        return codRegistro;
                    }
                    finally
                    {
                        cmd.Connection.Close();
                        cmd.Dispose();
                    }
                }
            }
        }

        public async Task<long> ActualizarDetalleSolicitud(long solicitudId, long? requisitoId, int codEstado)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_SOLICITUD_ID", OracleDbType.Int32, solicitudId, ParameterDirection.Input),
                new OracleParameter("P_REQUISITO_ID", OracleDbType.Int32, requisitoId, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_ESTADO", OracleDbType.Int32, codEstado, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.Int64, ParameterDirection.Output)
            ];

            return await _db.ExecuteNonQueryProcedure("PCK_ADMINISTRADO.PRC_ACTUALIZAR_DETALLE_SOLICITUD", param);
        }

        public async Task<int> ActualizarEstadoRequisitosParaRevision(long solicitudId, int codEstado)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_SOLICITUD_ID", OracleDbType.Int32, solicitudId, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_ESTADO", OracleDbType.Int32, codEstado, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.Int64, ParameterDirection.Output)
            ];

            return await _db.ExecuteNonQueryProcedure("PCK_ADMINISTRADO.PRC_ACTUALIZAR_ESTADO_REQUISITOS_PARA_REVISION", param);
        }

        public async Task<int> ObtenerCantidadRequisitosPendientes(long solicitudId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_SOLICITUD_ID", OracleDbType.Int32, solicitudId, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.Int64, ParameterDirection.Output)
            ];

            return await _db.ExecuteNonQueryProcedure("PCK_TUPA.PRC_OBTENER_CANTIDAD_REQUISITOS_PENDIENTES", param);
        }
    }
}

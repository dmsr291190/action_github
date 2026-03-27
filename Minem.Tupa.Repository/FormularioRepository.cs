using Minem.Tupa.Data;
using Minem.Tupa.IRepository;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using Minem.Tupa.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Minem.Tupa.Entity.Formulario;
using System.Runtime.Serialization.Json;
using Minem.Tupa.Utils;
using Minem.Tupa.Entity.Tramite;

namespace Minem.Tupa.Repository
{
    public class FormularioRepository(Minem_Db_Context Minem_Db_Context) : IFormularioRepository
    {
        private readonly Minem_Db_Context _minemDbContext = Minem_Db_Context;
        private readonly string _connection = Minem_Db_Context.Database.GetConnectionString() ?? string.Empty;


        #region Metodos Dia        

        public async Task<long> GuardarFormulario(long p_CodMaeSolicitud, string p_DataJson)
        {
            var _db = new GenericRepository(_connection);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int64, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_DataJson", OracleDbType.Clob, p_DataJson, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_I_INSERTAR_FORMULARIO_DIA", param, "p_Resultado");
        }

        public async Task<USP_S_OBTENER_FORMULARIO_DIA_Response_Entity> ObtenerFormularioDia(long codMaeSolicitud)
        {
            var _db = new GenericRepository(_connection);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int64, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToEntity<USP_S_OBTENER_FORMULARIO_DIA_Response_Entity>("PCK_ADMINISTRADO.USP_S_OBTENER_FORMULARIO_DIA", param);
        }

        #endregion


        #region Métodos con Dapper
        public async Task<long> GuardarResumenEjecutivo(long p_CodMaeSolicitud, string p_DataJson, string p_User)
        {
            var _db = new GenericRepository(_connection);
            List<OracleParameter> param =
            [
                new OracleParameter("ResumenEjecutivo", OracleDbType.Clob, p_DataJson, ParameterDirection.Input),
                new OracleParameter("CodigoSolicitud", OracleDbType.Int64, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("UsuarioCreacion", OracleDbType.Varchar2, p_User, ParameterDirection.Input),
                new OracleParameter("FechaCreacion", OracleDbType.Date, DateTime.Now, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            string sql = @"
                            INSERT INTO T_TDM_DIA_RESUMEN_EJECUTIVO 
                                (ID, RESUMEN_EJECUTIVO, CODIGO_SOLICITUD, USUARIO_CREACION, FECHA_CREACION)
                            VALUES 
                                (SEQ_T_TDM_DIA_RESUMEN_EJECUTIVO.NEXTVAL, :ResumenEjecutivo, :CodigoSolicitud, :UsuarioCreacion, :FechaCreacion)
                            RETURNING ID INTO :p_Id";

            return await _db.ExecuteSqlReturningLong(sql, param, "p_Id");
        }

        public async Task<DP_S_OBTENER_RESUMEN_EJECUTIVO> ObtenerResumenEjecutivoPorCodigoSolicitud(long codigoSolicitud)
        {
            var _db = new GenericRepository(_connection);
            List<OracleParameter> param =
            [
                new OracleParameter("CodigoSolicitud", OracleDbType.Int64, codigoSolicitud, ParameterDirection.Input),
                //new OracleParameter("p_Resultado", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            string sql = @" SELECT 
                                ID, 
                                RESUMEN_EJECUTIVO 
                            FROM T_TDM_DIA_RESUMEN_EJECUTIVO 
                            WHERE CODIGO_SOLICITUD = :CodigoSolicitud";

            return await _db.ExecuteQueryToEntity<DP_S_OBTENER_RESUMEN_EJECUTIVO>(sql, param);
        }

        public async Task<bool> ActualizarFormulario(int id, string jsonData)
        {
            var _db = new GenericRepository(_connection);

            List<OracleParameter> param =
            [
                new OracleParameter("jsonData", OracleDbType.Clob, jsonData, ParameterDirection.Input),
                new OracleParameter("id", OracleDbType.Int64, id, ParameterDirection.Input)
            ];

            string sql = "UPDATE FORMULARIOS SET DATAJSON = :jsonData WHERE ID = :id";
         
            return await _db.ExecuteUpdateQuery(sql, param);
        }


        public async Task<long> ActualizarFormulario(long p_CodMaeSolicitud, string jsonData)
        {
            var _db = new GenericRepository(_connection);
            List<OracleParameter> param =
            [
                new OracleParameter("p_CodMaeSolicitud", OracleDbType.Int32, p_CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("jsonData", OracleDbType.Clob, jsonData, ParameterDirection.Input),
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ADMINISTRADO.USP_U_ACTUALIZAR_METADATA", param, "p_Resultado");
        }
        #endregion


        #region Observacion de Opinantes

        public async Task<List<TransactionListOpinantes>> GetTransactionListOpinantes(int codMaeSolicitud, int codSolicitudExpediente)
        {
            try
            {
                var _db = new GenericRepository(_connection);
                List<OracleParameter> param =
                [
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_CODSOLICITUDEXPEDIENTE", OracleDbType.Int32, codSolicitudExpediente, ParameterDirection.Input),
                new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                ];

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.OPINANTE, "PRC_USP_S_LISTA_OPINANTES_ADMINISTRADO");                
                return await _db.ExecuteProcedureToList<TransactionListOpinantes>(nombreProcedimiento, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }
        }

        public async Task<List<DocumentoInstitucion_Entity>> ListarDocumentosInstitucion(int codMaeSolicitud, int codSolicitudExpediente)
        {
            try
            {
                var _db = new GenericRepository(_connection);
                List<OracleParameter> param =
                [
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                    new OracleParameter("P_CODSOLICITUDEXPEDIENTE", OracleDbType.Int32, codSolicitudExpediente, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                ];

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.OPINANTE, "PRC_S_LISTAR_DOCUMENTO_INSTITUCION_ADMINISTRADO");                
                return await _db.ExecuteProcedureToList<DocumentoInstitucion_Entity>(nombreProcedimiento, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }            
        }

        public async Task<List<DocumentoInstitucionAdjuntos_Entity>> ListarDocumentosInstitucionAdjuntos(int codMaeSolicitud, int codSolicitudExpediente)
        {
            try
            {
                var _db = new GenericRepository(_connection);
                List<OracleParameter> param =
                [
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                    new OracleParameter("P_CODSOLICITUDEXPEDIENTE", OracleDbType.Int32, codSolicitudExpediente, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                ];

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.OPINANTE, "PRC_S_LISTAR_DOCUMENTO_INSTITUCION_ADJUNTOS_ADMINISTRADO");                
                return await _db.ExecuteProcedureToList<DocumentoInstitucionAdjuntos_Entity>(nombreProcedimiento, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }
        }

        public async Task<TransactionResumeData> GetTransactionResumeData(int codMaeSolicitud)
        {
            try
            {
                var _db = new GenericRepository(_connection);
                List<OracleParameter> param =
                [
                    new OracleParameter("PCODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                ];


                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.TUPA, "PRC_USP_S_DATATUPA_BYTRANSACTIONID_AMPPLAZO_ADMINISTRADO".ToUpper());                
                return await _db.ExecuteProcedureToEntity<TransactionResumeData>(nombreProcedimiento, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }            
        }

        #endregion

    }
}

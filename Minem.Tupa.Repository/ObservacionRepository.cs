using Microsoft.EntityFrameworkCore;
using Minem.Tupa.Data;
using Minem.Tupa.Entity.Observacion;
using Minem.Tupa.IRepository;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace Minem.Tupa.Repository
{
    public class ObservacionRepository(Minem_Db_Context _minemDbContext) : IObservacionRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;
        
        public async Task<List<PRC_S_ULTIMOTUMOVMETADAHISTORICO_Response_Entity>> ConsultaTumovmetadahistorico(int codmaesolicitud)
        {

            var listDto = new List<PRC_S_ULTIMOTUMOVMETADAHISTORICO_Response_Entity>();

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codmaesolicitud, ParameterDirection.Input),
                    new OracleParameter("P_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_BANDEJA", "PRC_S_ULTIMOTUMOVMETADAHISTORICO");
                listDto = await _db.ExecuteProcedureToList<PRC_S_ULTIMOTUMOVMETADAHISTORICO_Response_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listDto;
        }
        public async Task<int> InsertObshistjson(PRC_I_OBSHISTJSON_Request_Entity request)
        {
            int resultado;

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, request.codmaesolicitud, ParameterDirection.Input),
                    new OracleParameter("P_IDTUMOVMETADAHISTORICO", OracleDbType.Int32, request.idtumovmetadahistorico, ParameterDirection.Input),
                    new OracleParameter("P_CAPITULO", OracleDbType.Varchar2, request.capitulo, ParameterDirection.Input),
                    new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                    new OracleParameter("P_USUARIO_CREACION", OracleDbType.Varchar2, request.usuarioCreacion, ParameterDirection.Input),
                    new OracleParameter("P_RESULTADO", OracleDbType.Int32,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_BANDEJA", "PRC_I_OBSHISTJSON");
                await _db.ExecuteNonQueryProcedure(nombreProcedimiento, oracleParameters);
                // Obtener el valor del parámetro de salida y convertirlo a int
                var outputParameter = oracleParameters.Find(p => p.ParameterName == "P_RESULTADO");

                // Verificar si el valor es OracleDecimal y convertirlo
                resultado = outputParameter?.Value is OracleDecimal oracleDecimalValue ? oracleDecimalValue.ToInt32() : Convert.ToInt32(outputParameter?.Value);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }
            return resultado;
        }
        public async Task<int> InsertDetobshistjson(PRC_I_DETOBSHISTJSON_Request_Entity request)
        {
            int resultado;

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_IDOBSHISTJSON", OracleDbType.Int32, request.idobshistjson, ParameterDirection.Input),
                    new OracleParameter("P_CODMOVPERSONA", OracleDbType.Int32, request.codmovpersona, ParameterDirection.Input),
                    new OracleParameter("P_OBSERVACION", OracleDbType.Varchar2, request.observacion, ParameterDirection.Input),
                    new OracleParameter("P_IDDETOBSHISTJSONPADRE", OracleDbType.Int32, request.iddetobshistjsonpadre, ParameterDirection.Input),
                    new OracleParameter("P_ORDEN", OracleDbType.Int32, request.orden, ParameterDirection.Input),
                    new OracleParameter("P_ESTADOOBSERVACION", OracleDbType.Int32, request.estadoObservacion, ParameterDirection.Input),
                    new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                    new OracleParameter("P_USUARIO_CREACION", OracleDbType.Varchar2, request.usuarioCreacion, ParameterDirection.Input),
                    new OracleParameter("P_RESULTADO", OracleDbType.Int32,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_BANDEJA", "PRC_I_DETOBSHISTJSON");
                await _db.ExecuteProcedureToEntity<PRC_I_DETOBSHISTJSON_Request_Entity>(nombreProcedimiento, oracleParameters);

                // Obtener el valor del parámetro de salida y convertirlo a int
                var outputParameter = oracleParameters.Find(p => p.ParameterName == "P_RESULTADO");

                // Verificar si el valor es OracleDecimal y convertirlo
                resultado = outputParameter?.Value is OracleDecimal oracleDecimalValue ? oracleDecimalValue.ToInt32() : Convert.ToInt32(outputParameter?.Value);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return resultado;
        }
        public async Task<List<PRC_S_OBSHISTJSON_Response_Entity>> ConsultaObshistjson(int codmaesolicitud, string capitulo, int iddetobshistjsonpadre)
        {
            var listDto = new List<PRC_S_OBSHISTJSON_Response_Entity>();

            try
            { 
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codmaesolicitud, ParameterDirection.Input),         
                    new OracleParameter("P_CAPITULO", OracleDbType.Varchar2, capitulo ?? (object)DBNull.Value, ParameterDirection.Input),
                    new OracleParameter("P_IDDETOBSHISTJSONPADRE", OracleDbType.Int32, iddetobshistjsonpadre, ParameterDirection.Input),
                    new OracleParameter("P_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_BANDEJA", "PRC_S_OBSHISTJSON");
                listDto = await _db.ExecuteProcedureToList<PRC_S_OBSHISTJSON_Response_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listDto;
        }
        public async Task<int> ConsultaExisteSolicitudCapitulo(int codmaesolicitud, string capitulo)
        {
            int resultado;

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codmaesolicitud, ParameterDirection.Input),
                    new OracleParameter("P_CAPITULO", OracleDbType.Varchar2, capitulo, ParameterDirection.Input),
                    new OracleParameter("P_RESULTADO", OracleDbType.Int32,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_BANDEJA", "PRC_S_EXISTESOLICITUDCAPITULO");
                await _db.ExecuteNonQueryProcedure(nombreProcedimiento, oracleParameters);
                // Obtener el valor del parámetro de salida y convertirlo a int
                var outputParameter = oracleParameters.Find(p => p.ParameterName == "P_RESULTADO");

                // Verificar si el valor es OracleDecimal y convertirlo
                resultado = outputParameter?.Value is OracleDecimal oracleDecimalValue ? oracleDecimalValue.ToInt32() : Convert.ToInt32(outputParameter?.Value);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }
            return resultado;
        }
        public async Task<int> UpdateDetobshistjson(PRC_I_DETOBSHISTJSON_Request_Entity request)
        {
            int resultado;

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_IDDETOBSHISTJSON", OracleDbType.Int32, request.iddetobshistjson, ParameterDirection.Input),
                    new OracleParameter("P_IDOBSHISTJSON", OracleDbType.Int32, request.idobshistjson, ParameterDirection.Input),
                    new OracleParameter("P_CODMOVPERSONA", OracleDbType.Int32, request.codmovpersona, ParameterDirection.Input),
                    new OracleParameter("P_OBSERVACION", OracleDbType.Varchar2, request.observacion, ParameterDirection.Input),
                    new OracleParameter("P_IDDETOBSHISTJSONPADRE", OracleDbType.Int32, request.iddetobshistjsonpadre, ParameterDirection.Input),
                    new OracleParameter("P_ORDEN", OracleDbType.Int32, request.orden, ParameterDirection.Input),
                    new OracleParameter("P_ESTADOOBSERVACION", OracleDbType.Int32, request.estadoObservacion, ParameterDirection.Input),
                    new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                    new OracleParameter("P_USUARIO_ULTIMA_MOD", OracleDbType.Varchar2, request.usuarioCreacion, ParameterDirection.Input),
                    new OracleParameter("P_RESULTADO", OracleDbType.Int32,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_BANDEJA", "PRC_U_DETOBSHISTJSON");
                await _db.ExecuteProcedureToEntity<PRC_I_DETOBSHISTJSON_Request_Entity>(nombreProcedimiento, oracleParameters);

                // Obtener el valor del parámetro de salida y convertirlo a int
                var outputParameter = oracleParameters.Find(p => p.ParameterName == "P_RESULTADO");

                // Verificar si el valor es OracleDecimal y convertirlo
                resultado = outputParameter?.Value is OracleDecimal oracleDecimalValue ? oracleDecimalValue.ToInt32() : Convert.ToInt32(outputParameter?.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return resultado;

        }

        public async Task<int> DeleteDetobshistjson(int iddetobshistjson)
        {
            int resultado;

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_IDDETOBSHISTJSON", OracleDbType.Int32, iddetobshistjson, ParameterDirection.Input),
                    new OracleParameter("P_RESULTADO", OracleDbType.Int32,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_BANDEJA", "PRC_D_DETOBSHISTJSON");
                await _db.ExecuteProcedureToEntity<PRC_I_DETOBSHISTJSON_Request_Entity>(nombreProcedimiento, oracleParameters);

                // Obtener el valor del parámetro de salida y convertirlo a int
                var outputParameter = oracleParameters.Find(p => p.ParameterName == "P_RESULTADO");

                // Verificar si el valor es OracleDecimal y convertirlo
                resultado = outputParameter?.Value is OracleDecimal oracleDecimalValue ? oracleDecimalValue.ToInt32() : Convert.ToInt32(outputParameter?.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return resultado;

        }

        public async Task<List<PRC_S_OBSHISTJSON_Response_Entity>> ObservacionesSolicitud(int codmaesolicitud)
        {
            var listDto = new List<PRC_S_OBSHISTJSON_Response_Entity>();

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codmaesolicitud, ParameterDirection.Input),
                    new OracleParameter("P_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_BANDEJA", "PRC_USP_S_LISTAR_OBSERVACION_SOLICITUD");
                listDto = await _db.ExecuteProcedureToList<PRC_S_OBSHISTJSON_Response_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listDto;
        }

    }
}

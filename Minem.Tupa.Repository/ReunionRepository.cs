using Minem.Tupa.Data;
using Minem.Tupa.IRepository;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Minem.Tupa.Entity.Reunion;
using Minem.Tupa.Utils;
using Minem.Tupa.Dto.Tramite;
using Oracle.ManagedDataAccess.Types;
using System.Diagnostics;
using Minem.Tupa.Dto.Reunion;
using Minem.Tupa.Entity.Its;


namespace Minem.Tupa.Repository
{
    public class ReunionRepository(Minem_Db_Context _minemDbContext) : IReunionRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;
        /*
        public async Task<long> InsertarReunionSolicitud(SP_INSERT_REUNION_SOLICITUD_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            using OracleCommand cmd = new OracleCommand("PCK_ITS_REUNION.USP_I_INSERTAR_REUNION_SOLICITUD", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("P_CODMAESOLICITUD", OracleDbType.Int64).Value = request.codmaesolicitud;
            cmd.Parameters.Add("P_PROPUESTA", OracleDbType.Varchar2).Value = request.propuesta;
            cmd.Parameters.Add("P_CONSULTORA", OracleDbType.Varchar2).Value = request.consultora;
            cmd.Parameters.Add("P_TIPO_REUNION", OracleDbType.Varchar2).Value = request.tipoReunion;
            cmd.Parameters.Add("P_PROPUESTA_FECHA_1", OracleDbType.Date).Value = request.propuestafecha1;
            cmd.Parameters.Add("P_PROPUESTA_FECHA_2", OracleDbType.Date).Value = request.propuestafecha2;
            cmd.Parameters.Add("P_ARCHIVO_PRESENTACION", OracleDbType.Blob).Value = request.archivopresentacion;
            cmd.Parameters.Add("P_NOMBRE_ARCHIVO", OracleDbType.Varchar2).Value = request.nombredelarchivo;
            cmd.Parameters.Add("P_USUARIO_REGISTRA", OracleDbType.Int64).Value = request.usuarioregistra;

            OracleParameter output = new OracleParameter("P_RESULTADO", OracleDbType.Int64)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(output);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return Convert.ToInt64(output.Value.ToString());
        }
        */
        public async Task<long> InsertarSolicitudReunion(SP_INSERT_REUNION_SOLICITUD_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            

            List<OracleParameter> parametros = new List<OracleParameter>
            {

                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, request.codmaesolicitud, ParameterDirection.Input),
                new OracleParameter("P_PROPUESTA", OracleDbType.Varchar2, request.propuesta, ParameterDirection.Input),
                new OracleParameter("P_CONSULTORA", OracleDbType.Varchar2, request.consultora, ParameterDirection.Input),
                new OracleParameter("P_TIPO_REUNION", OracleDbType.Varchar2, request.tipoReunion, ParameterDirection.Input),
                new OracleParameter("P_PROPUESTA_FECHA_1", OracleDbType.Varchar2, request.propuestafecha1, ParameterDirection.Input),
                new OracleParameter("P_PROPUESTA_FECHA_2", OracleDbType.Varchar2, request.propuestafecha2, ParameterDirection.Input),
                new OracleParameter("P_ID_ARCHIVO", OracleDbType.Int64, request.idArchivo, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_ARCHIVO", OracleDbType.Varchar2, request.nombredelarchivo, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioregistra, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ITS_REUNION.USP_I_INSERTAR_REUNION_SOLICITUD", parametros, "P_RESULTADO");
        }

        public async Task<long> InsertarReunionParticipante(long idReunion, string tipoParticipante, long idPersona,  SP_INSERT_REUNION_PARTICIPANTE_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);


            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_REUNION_SOLICITUD", OracleDbType.Int64, idReunion, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_APELLIDOS", OracleDbType.Varchar2, request.nombre, ParameterDirection.Input),
                new OracleParameter("P_DOCUMENTO_IDENTIDAD", OracleDbType.Varchar2, request.documento, ParameterDirection.Input),
                new OracleParameter("P_CARGO", OracleDbType.Varchar2, request.cargo, ParameterDirection.Input),
                new OracleParameter("P_TIPO_PARTICIPANTE", OracleDbType.Varchar2, tipoParticipante, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, idPersona, ParameterDirection.Input),
                
            };

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ITS_REUNION.USP_I_INSERTAR_REUNION_PARTICIPANTE", parametros, "P_RESULTADO");
        }

        public async Task<long> InsertarReunionCorreo(long idReunion,  long idPersona, string correo)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_REUNION_SOLICITUD", OracleDbType.Int64, idReunion, ParameterDirection.Input),
                new OracleParameter("P_EMAIL", OracleDbType.Varchar2, correo, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64,idPersona, ParameterDirection.Input),
            };

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ITS_REUNION.USP_I_INSERTAR_REUNION_CORREO", parametros, "P_RESULTADO");
        }

        public async Task<long> InsertarReunionObjetico(long idReunion, long idPersona, SP_INSERT_REUNION_OBJETIVO_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_REUNION_SOLICITUD", OracleDbType.Int64, idReunion, ParameterDirection.Input),
                new OracleParameter("P_MODIFICACION_PROPUESTA", OracleDbType.Varchar2, request.modificacion, ParameterDirection.Input),
                new OracleParameter("P_SUPUESTO_NORMATIVO", OracleDbType.Varchar2, request.supuesto, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64,idPersona, ParameterDirection.Input),
            };

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ITS_REUNION.USP_I_INSERTAR_REUNION_OBJETIVO", parametros, "P_RESULTADO");
        }

        public async Task<List<SP_OBTENER_REUNION_Request_Entity>> ObtenerReunion(SP_OBTENER_REUNION_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, request.CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_REUNION_Request_Entity>("PCK_ITS_REUNION.USP_S_OBTENER_REUNION_SOLICITUD_CODMAESOLICITUD", param);
        }

        public async Task<List<SP_OBTENER_REUNION_HISTORIAL_Request_Entity>> GetHistorialReuniones(long CodMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, CodMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_REUNION_HISTORIAL_Request_Entity>("PCK_ITS_REUNION.USP_S_OBTENER_HISTORIAL_REUNIONES_ADMINISTRADO", param);
        }
    }
}

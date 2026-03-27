using Minem.Tupa.Data;
using Minem.Tupa.IRepository;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Minem.Tupa.Entity.Tupa;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.Utils;
using Minem.Tupa.Entity.Solicitud;

namespace Minem.Tupa.Repository
{
    public class TupaRepository(Minem_Db_Context _minemDbContext) : ITupaRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

        public async Task<List<TupaEntity>> ObtenerTupaPorSector(long idSector, string tipoPersona)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDSECTOR", OracleDbType.Int64, idSector, ParameterDirection.Input),
                new OracleParameter("P_TIPOPERSONA", OracleDbType.Varchar2, tipoPersona, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<TupaEntity>("PCK_ADMINISTRADO.USP_S_OBTENER_TUPA_POR_SECTOR", param);
        }

        public async Task<TupaEntity> ObtenerTupaPorCodigo(string codigoTupa)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODIGOTUPA", OracleDbType.Varchar2, codigoTupa, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToEntity<TupaEntity>("PCK_ADMINISTRADO.USP_S_OBTENER_TUPA_POR_CODIGO", param);
        }

        public async Task<List<EstructuraCapituloAdjuntosResponse_Entity>> ListarEstructuraCapituloAdjuntos()
        {
            var listEntity = new List<EstructuraCapituloAdjuntosResponse_Entity>();

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.TUPA, "PRC_USP_S_ESTRUCTURA_CAPITULO_ADJUNTOS");
                listEntity = await _db.ExecuteProcedureToList<EstructuraCapituloAdjuntosResponse_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listEntity;
        }

        public async Task<List<TupaEntity>> ObtenerTupa()
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_Resultado", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<TupaEntity>("PCK_ADMINISTRADO.USP_S_OBTENER_TUPA", param);
        }

        public async Task<SolicitudResponse_Entity> ObtenerSolicitudPorCodigo(int codMaeSolicitud)
        {
            SolicitudResponse_Entity result = new SolicitudResponse_Entity();
            try
            {
                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.TUPA, "TUPA_USP_S_SOLICITUD_POR_CODIGO");
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
                };

                result = await _db.ExecuteProcedureToEntity<SolicitudResponse_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return result;
        }

        public async Task<List<DocumentoDespachadoResponse_Entity>> ObtenerDocumentosDespachados(long codMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_SOLICITUD_ID", OracleDbType.Int32, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<DocumentoDespachadoResponse_Entity>("PCK_TUPA.PRC_OBTENER_DOCUMENTOS_DESPACHADOS", param);

        }
    }    
}

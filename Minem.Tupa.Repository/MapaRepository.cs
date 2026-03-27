using Microsoft.EntityFrameworkCore;
using Minem.Tupa.Data;
using Minem.Tupa.Entity.Mapa;
using Minem.Tupa.Entity.Tramite;
using Minem.Tupa.IRepository;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Minem.Tupa.Repository
{
    public class MapaRepository(Minem_Db_Context _minemDbContext) : IMapaRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

        public async Task<List<SP_SELECT_TIPO_AREA_Response_Entity>> ObtenerTipoActividad(int tipo)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("Ls_IdTipoArea", OracleDbType.Int32, tipo, ParameterDirection.Input),
                new OracleParameter("Lr_Recordset", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<SP_SELECT_TIPO_AREA_Response_Entity>("PCK_GEOMETRY.SP_SELECT_TIPO_AREA", param);
        }
    }
}

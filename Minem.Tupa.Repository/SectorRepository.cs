using Minem.Tupa.Data;
using Minem.Tupa.IRepository;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Minem.Tupa.Entity.Tupa;

namespace Minem.Tupa.Repository
{
    public class SectorRepository(Minem_Db_Context _minemDbContext) : ISectorRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

        public async Task<List<SectorEntity>> ObtenerSectores()
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_Resultado", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<SectorEntity>("PCK_ADMINISTRADO.USP_S_OBTENER_SECTORES", param);
        }
    }    
}

using Minem.Tupa.Data;
using Minem.Tupa.IRepository;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Minem.Tupa.Entity.Tupa;

namespace Minem.Tupa.Repository
{
    public class RequisitoRepository(Minem_Db_Context _minemDbContext) : IRequisitoRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

        public async Task<List<RequisitoEntity>> ObtenerRequisitos(string codigoTupa)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODIGOTUPA", OracleDbType.Varchar2, codigoTupa, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor,ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<RequisitoEntity>("PCK_ADMINISTRADO.USP_S_OBTENER_REQUISITOS_POR_TUPA", param);
        }
    }    
}

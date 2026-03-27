using Minem.Tupa.Data;
using Minem.Tupa.IRepository;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Minem.Tupa.Entity;
using Minem.Tupa.Entity.Tramite;
using Minem.Tupa.Entity.Profesional;

namespace Minem.Tupa.Repository
{
    public class ProfesionalRepository(Minem_Db_Context _minemDbContext) : IProfesionalRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

       
        public async Task<List<SP_OBTENER_PROFESIONES_Response_Entity>> ObtenerProfesiones()
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_PROFESIONES_Response_Entity>("PCK_TRAMITE_BANDEJA.SP_OBTENER_PROFESIONES", param);
        }
    }    
}

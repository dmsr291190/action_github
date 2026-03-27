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

namespace Minem.Tupa.Repository
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly Minem_Db_Context _minemDbContext;
        private readonly string _connection;
        public ArchivoRepository(Minem_Db_Context Minem_Db_Context)
        {
            _minemDbContext = Minem_Db_Context;
            _connection = Minem_Db_Context.Database.GetConnectionString();
        }
        
    }

    
}

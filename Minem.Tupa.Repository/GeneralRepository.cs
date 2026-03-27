using Microsoft.EntityFrameworkCore;
using Minem.Tupa.Data;
using Minem.Tupa.Entity;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Repository
{
    public class GeneralRepository(Minem_Db_Context _minemDbContext) : IGeneralRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

        public async Task<GEMovPersona> GetPersonaPorCodMovUsuario(int codMovUsuario)
        {
            var entity = new GEMovPersona();

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_CODMOVUSUARIO", OracleDbType.Int32, codMovUsuario, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.GENERAL, "PRC_USP_S_PERSONA_POR_CODMOVUSUARIO");
                entity = await _db.ExecuteProcedureToEntity<GEMovPersona>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return entity;
        }

        public async Task<List<GEMovPersona>> GetPersonaOrganicaTupa(int CodIdMaeTupa)
        {
            var listDto = new List<GEMovPersona>();

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_ID_TUPA", OracleDbType.Int32, CodIdMaeTupa, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.GENERAL, "PRC_USP_S_PERSONA_ORGANICA_TUPA");
                listDto = await _db.ExecuteProcedureToList<GEMovPersona>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listDto;
        }

        public async Task<List<GEMovPersona>> GetPersonaOrganicaRol(int CodIdMaeTupa, int CodMaeUniOrganica)
        {
            var listDto = new List<GEMovPersona>();

            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_ID_TUPA", OracleDbType.Int32, CodIdMaeTupa, ParameterDirection.Input),
                    new OracleParameter("P_CODMAEUNIORGANICA", OracleDbType.Int32, CodMaeUniOrganica, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.GENERAL, "PRC_USP_S_PERSONA_ORGANICA_ROL");
                listDto = await _db.ExecuteProcedureToList<GEMovPersona>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listDto;
        }
    }
}

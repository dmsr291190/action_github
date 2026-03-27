using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Minem.Tupa.Data;
using Minem.Tupa.Entity.EstudiosPresentados;
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
    public class EstudiosPresentadosRepository(Minem_Db_Context minem_Db_Context) : IEstudiosPresentadosRepository
    {
        private readonly string CONNECTION_STRING_MINEM = minem_Db_Context.Database.GetConnectionString();


        public async Task<List<BandejaEstudiosPresentadosResponse_Entity>> GetBandeja(BandejaEstudiosPresentadosRequest_Entity request)
        {
            var listEntity = new List<BandejaEstudiosPresentadosResponse_Entity>();

            try
            {
                var oracleGenericRepository = new GenericRepository(CONNECTION_STRING_MINEM);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_PAGESIZE", OracleDbType.Int32, request.PageSize, ParameterDirection.Input),
                    new OracleParameter("P_CURRENTPAGE", OracleDbType.Int32, request.CurrentPage, ParameterDirection.Input),
                    new OracleParameter("P_FILTRO", OracleDbType.Varchar2, request.Filtro, ParameterDirection.Input),
                    new OracleParameter("P_CODIDMAETUPA", OracleDbType.Int32, request.CodIdMaeTupa, ParameterDirection.Input),
                    new OracleParameter("P_CODMAEESTADO", OracleDbType.Int32, request.CodMaeEstado, ParameterDirection.Input),
                    new OracleParameter("P_CODMAETUPA", OracleDbType.Varchar2, request.CodMaeTupa, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.ESTUDIOS_PRESENTADOS, "PRC_S_LISTAR");
                listEntity = await oracleGenericRepository.ExecuteProcedureToList<BandejaEstudiosPresentadosResponse_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listEntity;
        }

        public async Task<List<TipoEstudioResponse_Entity>> GetTipoEstudio()
        {
            var listEntity = new List<TipoEstudioResponse_Entity>();

            try
            {
                var oracleGenericRepository = new GenericRepository(CONNECTION_STRING_MINEM);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.ESTUDIOS_PRESENTADOS, "PRC_S_TIPO_ESTUDIO");
                listEntity = await oracleGenericRepository.ExecuteProcedureToList<TipoEstudioResponse_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listEntity;
        }

        public async Task<List<TipoEstudiosResponse_Entity>> GetListarTipoEstudio()
        {
            var listEntity = new List<TipoEstudiosResponse_Entity>();

            try
            {
                var oracleGenericRepository = new GenericRepository(CONNECTION_STRING_MINEM);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {        
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.ESTUDIOS_PRESENTADOS, "PRC_S_LISTAR_TIPO_ESTUDIO");
                listEntity = await oracleGenericRepository.ExecuteProcedureToList<TipoEstudiosResponse_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listEntity;
        }

        public async Task<List<TipoEstudiosTupaResponse_Entity>> GetListarTipoEstudioTupa(string CodMaeTupa)
        {
            var listEntity = new List<TipoEstudiosTupaResponse_Entity>();

            try
            {
                var oracleGenericRepository = new GenericRepository(CONNECTION_STRING_MINEM);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("P_CODMAETUPA", OracleDbType.Varchar2, CodMaeTupa, ParameterDirection.Input),
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.ESTUDIOS_PRESENTADOS, "PRC_S_LISTAR_TIPO_ESTUDIO_TUPA");
                listEntity = await oracleGenericRepository.ExecuteProcedureToList<TipoEstudiosTupaResponse_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listEntity;
        }
        public async Task<List<SituacionResponse_Entity>> GetSituacion()
        {
            var listEntity = new List<SituacionResponse_Entity>();

            try
            {
                var oracleGenericRepository = new GenericRepository(CONNECTION_STRING_MINEM);
                List<OracleParameter> oracleParameters = new List<OracleParameter>()
                {
                    new OracleParameter("PC_RESULTADO", OracleDbType.RefCursor,ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.ESTUDIOS_PRESENTADOS, "PRC_S_SITUACION");
                listEntity = await oracleGenericRepository.ExecuteProcedureToList<SituacionResponse_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }

            return listEntity;
        }

        public async Task<long> GuardarAporte(int idSolicitud, int idSolicitudExpediente, string descripcion, int idUser, string CodigoCelular, string CodigoCorreoElectronico,
            string CorreoElectronico, string NombresApellidos, string NumeroCelular, string NumeroDocumento, string Ruc, string TipoDocumento, string TipoPersona, int TipoValidacion)
        {
            var _db = new GenericRepository(CONNECTION_STRING_MINEM);
            List<OracleParameter> param =
            [
                new OracleParameter("P_ID_SOLICITUD", OracleDbType.Int64, idSolicitud, ParameterDirection.Input),
                new OracleParameter("P_ID_SOLICITUD_EXPEDIENTE", OracleDbType.Int64, idSolicitudExpediente, ParameterDirection.Input),                
                
                new OracleParameter("P_CODIGO_CELULAR", OracleDbType.Varchar2, CodigoCelular, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_CORREO_ELECTRONICO", OracleDbType.Varchar2, CodigoCorreoElectronico, ParameterDirection.Input),
                new OracleParameter("P_CORREO_ELECTRONICO", OracleDbType.Varchar2, CorreoElectronico, ParameterDirection.Input),
                new OracleParameter("P_NOMBRES_APELLIDOS", OracleDbType.Varchar2, NombresApellidos, ParameterDirection.Input),
                new OracleParameter("P_NUMERO_CELULAR", OracleDbType.Varchar2, NumeroCelular, ParameterDirection.Input),
                new OracleParameter("P_NUMERO_DOCUMENTO", OracleDbType.Varchar2, NumeroDocumento, ParameterDirection.Input),
                new OracleParameter("P_RUC", OracleDbType.Varchar2, Ruc, ParameterDirection.Input),
                new OracleParameter("P_TIPO_DOCUMENTO", OracleDbType.Varchar2, TipoDocumento, ParameterDirection.Input),
                new OracleParameter("P_TIPO_PERSONA", OracleDbType.Varchar2, TipoPersona, ParameterDirection.Input),
                new OracleParameter("P_TIPO_VALIDACION", OracleDbType.Int64, TipoValidacion, ParameterDirection.Input),      

                new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, descripcion, ParameterDirection.Input),
                new OracleParameter("P_ID_USER", OracleDbType.Int64, idUser, ParameterDirection.Input) 

            ];
            string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.ESTUDIOS_PRESENTADOS, "PRC_I_SOLICITUD_APORTE");
            return await _db.ExecuteProcedureToLongWithOutput(nombreProcedimiento, param, "P_ID_SOLICITUD_APORTE");
        }

        public async Task<long> GuardarDetalleAporte(long idSolicitudAporte, int idUser, int idArchivo)
        {
            var _db = new GenericRepository(CONNECTION_STRING_MINEM);
            List<OracleParameter> param =
            [
                new OracleParameter("P_ID_SOLICITUD_APORTE", OracleDbType.Int64, idSolicitudAporte, ParameterDirection.Input),
                new OracleParameter("P_ID_USER", OracleDbType.Int64, idUser, ParameterDirection.Input),
                new OracleParameter("P_ID_ARCHIVO", OracleDbType.Int64, idArchivo, ParameterDirection.Input)
            ];

            string nombreProcedimiento = string.Format("{0}.{1}", Constante.Package.ESTUDIOS_PRESENTADOS, "PRC_I_SOLICITUD_APORTE_DETALLE");
            return await _db.ExecuteProcedureToLongWithOutput(nombreProcedimiento, param, "P_ID_SOLICITUD_APORTE_DETALLE");
        }
    }
}

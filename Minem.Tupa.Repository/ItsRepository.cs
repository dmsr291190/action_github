using Minem.Tupa.Data;
using Minem.Tupa.IRepository;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Minem.Tupa.Utils;
using Minem.Tupa.Dto.Tramite;
using Oracle.ManagedDataAccess.Types;
using System.Diagnostics;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Entity.Its;
using Minem.Tupa.Entity.Tramite;
using Azure.Core;

namespace Minem.Tupa.Repository
{
    public class ItsRepository(Minem_Db_Context _minemDbContext) : IItsRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

        public async Task<long> InsertarProyectoITS(SP_INSERT_ITS_PROYECTO_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_PROYECTO", OracleDbType.Decimal, request.idProyecto, ParameterDirection.Input),
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Decimal, request.codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE", OracleDbType.Varchar2, request.nombre, ParameterDirection.Input),
                new OracleParameter("P_MONTO_ESTIMADO", OracleDbType.Decimal, request.montoEstimado, ParameterDirection.Input),
                new OracleParameter("P_ID_UNIDAD_MINERA", OracleDbType.Int64, request.idUnidadMinera, ParameterDirection.Input),
                new OracleParameter("P_UNIDAD_MINERA", OracleDbType.Varchar2, request.unidadMinera, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioRegistra, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_ITS.USP_I_INSERTAR_PROYECTO",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<List<SP_OBTENER_ITS_PROYECTO_Request_Entity>> ObtenerProyecto(SP_OBTENER_ITS_PROYECTO_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, request.codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_ITS_PROYECTO_Request_Entity>("PCK_ITS.USP_S_OBTENER_PROYECTO", param);
        }

        public async Task<long> InsertarProyectoArchivoITS(SP_INSERT_ITS_PROYECTO_ARCHIVO_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_ARCHIVO", OracleDbType.Int64, request.idArchivo, ParameterDirection.Input),
                new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, request.idProyecto, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_ARCHIVO", OracleDbType.Varchar2, request.nombreArchivo, ParameterDirection.Input),
                
                new OracleParameter("P_SECCION", OracleDbType.Int64, request.seccion, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioRegistra, ParameterDirection.Input)
            };
            //new OracleParameter("P_FECHA_CARGA", OracleDbType.TimeStamp, request.fechaCarga, ParameterDirection.Input),
            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_ITS.USP_I_INSERTAR_PROYECTO_ARCHIVO",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<List<SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity>> ObtenerProyectoArchivos(SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, request.idProyecto, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity>("PCK_ITS.USP_S_OBTENER_PROYECTO_ARCHIVO", param);
        }

        public async Task<long> EliminarProyectoArchivos(SP_ELIMINAR_ITS_PROYECTO_ARCHIVO_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDS_ARCHIVO", OracleDbType.Varchar2, request.idsArchivos, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioModifica, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ITS.USP_U_ELIMINAR_PROYECTO_ARCHIVO", param, "p_Resultado");
        }

        public async Task<long> InsertarRepresentanteITS(SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_REPRESENTANTE",     OracleDbType.Int64,       request.idRepresentante,     ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_TITULAR",       OracleDbType.Varchar2,    request.nombreTitular,       ParameterDirection.Input),
                new OracleParameter("P_RUC",                  OracleDbType.Int64,       request.ruc,                 ParameterDirection.Input),
                new OracleParameter("P_EMAIL_TITULAR",        OracleDbType.Varchar2,    request.emailTitular,        ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_REPRESENTANTE", OracleDbType.Varchar2,    request.nombreRepresentante, ParameterDirection.Input),
                new OracleParameter("P_APELLIDO_PATERNO",     OracleDbType.Varchar2,    request.apellidoPaterno,     ParameterDirection.Input),
                new OracleParameter("P_APELLIDO_MATERNO",     OracleDbType.Varchar2,    request.apellidoMaterno,     ParameterDirection.Input),
                new OracleParameter("P_CARGO",                OracleDbType.Varchar2,    request.cargo,               ParameterDirection.Input),
                new OracleParameter("P_DOCUMENTO_IDENTIDAD",  OracleDbType.Varchar2,    request.documentoIdentidad,  ParameterDirection.Input),
                new OracleParameter("P_EMAIL_REPRESENTANTE",  OracleDbType.Varchar2,    request.emailRepresentante,  ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_CONSULTORA",    OracleDbType.Varchar2,    request.nombreConsultora,  ParameterDirection.Input),
                new OracleParameter("P_OBJETIVO",             OracleDbType.Varchar2,    request.objetivo,  ParameterDirection.Input),
                //new OracleParameter("P_ESTADO",               OracleDbType.Int32,       request.estado,              ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA",     OracleDbType.Int64,       request.usuarioRegistra,     ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_ITS.USP_I_INSERTAR_REPRESENTANTE",
                parametros,
                "P_RESULTADO"
            );
        }

        

        public async Task<List<SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity>> ObtenerRepresentante(SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_ID_REPRESENTANTE", OracleDbType.Int64, request.idRepresentante, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity>("PCK_ITS.USP_S_OBTENER_REPRESENTANTE", param);
        }

        public async Task<long> InsertarProfesionalITS(SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                //new OracleParameter("P_ID_PROFESIONAL",     OracleDbType.Int64,    request.idProfesional,     ParameterDirection.Input),
                new OracleParameter("P_ID_PROYECTO",        OracleDbType.Int64,    request.idProyecto,        ParameterDirection.Input),
                new OracleParameter("P_NOMBRES_APELLIDOS",  OracleDbType.Varchar2, request.nombresApellidos,  ParameterDirection.Input),
                new OracleParameter("P_PROFESION",          OracleDbType.Varchar2, request.profesion,         ParameterDirection.Input),
                new OracleParameter("P_COLEGIATURA",        OracleDbType.Varchar2, request.colegiatura,       ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA",   OracleDbType.Int64,    request.usuarioRegistra,   ParameterDirection.Input)

            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_ITS.USP_I_INSERTAR_PROFESIONAL",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<List<SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity>> ObtenerProfesional(SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, request.idProyecto, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity>("PCK_ITS.USP_S_OBTENER_PROFESIONALES", param);
        }

        public async Task<long> EliminarProfesional(SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDS_PROFESIONAL", OracleDbType.Varchar2, request.idsEliminar, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioModifica, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ITS.USP_U_ELIMINAR_PROFESIONAL", param, "p_Resultado");
        }

        public async Task<long> InsertarMapa(SP_INSERT_ITS_MAPA_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, request.IdProyecto, ParameterDirection.Input),
                new OracleParameter("P_MAPA_JSON", OracleDbType.Clob, request.MapaJson, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.UsuarioRegistra, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_ITS_MAPA.USP_I_INSERTAR_MAPA",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<long> ActualizarMapa(SP_UPDATE_ITS_MAPA_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, request.IdProyecto, ParameterDirection.Input),
                new OracleParameter("P_MAPA_JSON", OracleDbType.Clob, request.MapaJson, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_MODIFICA", OracleDbType.Int64, request.UsuarioModifica, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_ITS_MAPA.USP_U_ACTUALIZAR_MAPA",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<SP_OBTENER_ITS_MAPA_Response_Entity> ObtenerMapa(long IdProyecto)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, IdProyecto, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var lista = await _db.ExecuteProcedureToList<SP_OBTENER_ITS_MAPA_Response_Entity>(
                "PCK_ITS_MAPA.USP_S_OBTENER_MAPA", parametros);

            return lista.FirstOrDefault();
        }
        public async Task<SP_OBTENER_ITS_MAPA_Response_Entity> ObtenerMapaConSolicitud(long CodmaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_CODMAESOLICITUD", OracleDbType.Int64, CodmaeSolicitud, ParameterDirection.Input),
                new OracleParameter("p_Resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var lista = await _db.ExecuteProcedureToList<SP_OBTENER_ITS_MAPA_Response_Entity>(
                "PCK_ITS_MAPA.USP_S_OBTENER_MAPA_CON_SOLICITUD", parametros);

            return lista.FirstOrDefault();
        }


        public async Task<long> EliminarMapa(long IdProyecto)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, IdProyecto, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_ITS_MAPA.USP_D_ELIMINAR_MAPA",
                parametros,
                "P_RESULTADO"
            );
        }


        public async Task<List<ObservacionPorProyectoCapituloResponseDto>> ObtenerProyectosObservacionCapitulo(long idProyecto, string capitulo)
        {
            var lista = new List<ObservacionPorProyectoCapituloResponseDto>();
            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, idProyecto, ParameterDirection.Input),
                    new OracleParameter("P_CAPITULO", OracleDbType.Varchar2, capitulo, ParameterDirection.Input),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_ITS_OBSERVACION", "USP_S_OBTENER_PROYECTO_OBSERVACION_CAP");
                lista = await _db.ExecuteProcedureToList<ObservacionPorProyectoCapituloResponseDto>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }
            return lista;
        }

        public async Task<List<ObservacionesProyectoPorCapitulo_Response_Entity>> ObtenerTotalObservacionesPorProyectoYCapitulos(long idProyecto)
        {
            var lista = new List<ObservacionesProyectoPorCapitulo_Response_Entity>();
            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ID_PROYECTO", OracleDbType.Int64, idProyecto, ParameterDirection.Input),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_ITS_OBSERVACION", "USP_S_TOTAL_OBSERVACIONES_PROYECTO_CAP");
                lista = await _db.ExecuteProcedureToList<ObservacionesProyectoPorCapitulo_Response_Entity>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }
            return lista;
        }

        public async Task<ItsReunion_Response_Entity> ObtenerReunionIts(long codMaeSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_SOLICITUD_ID", OracleDbType.Int64, codMaeSolicitud, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var lista = await _db.ExecuteProcedureToList<ItsReunion_Response_Entity>("PCK_ITS_REUNION.USP_OBTENER_REUNION_POR_SOLICITUD", parametros);

            return lista.FirstOrDefault();
        }

        public async Task<List<ObservacionPorProyectoCapituloResponseDto>> ObtenerRespuestasObservacionIts(long codMaeObservacion)
        {
            var lista = new List<ObservacionPorProyectoCapituloResponseDto>();
            try
            {
                var _db = new GenericRepository(_connectionString);
                List<OracleParameter> oracleParameters = new List<OracleParameter>
                {
                    new OracleParameter("P_ID_OBSERVACION", OracleDbType.Int64, codMaeObservacion, ParameterDirection.Input),
                    new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
                };

                string nombreProcedimiento = string.Format("{0}.{1}", "PCK_ITS_OBSERVACION", "USP_S_OBTENER_PROYECTO_RESPUESTAS_OBSERVACION");
                lista = await _db.ExecuteProcedureToList<ObservacionPorProyectoCapituloResponseDto>(nombreProcedimiento, oracleParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
            }
            return lista;
        }

        public async Task<long> InsertarReunionAdjunto(USP_I_INSERTAR_REUNION_ADJUNTO_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ID_REUNION_SOLICITUD", OracleDbType.Int64, request.IdReunionSolicitud, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_ARCHIVO", OracleDbType.Varchar2, request.NombreArchivo, ParameterDirection.Input),

                new OracleParameter("P_ID_ARCHIVO", OracleDbType.Int64, request.IdArchivo, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.Usuario, ParameterDirection.Input)
            };
            //new OracleParameter("P_FECHA_CARGA", OracleDbType.TimeStamp, request.fechaCarga, ParameterDirection.Input),
            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_ITS_REUNION.USP_I_INSERTAR_REUNION_ADJUNTO",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<List<USP_S_OBTENER_REUNION_ADJUNTO_Response_Entity>> ObtenerReunionAdjuntos(int IdReunionSolicitud)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_ID_REUNION_SOLICITUD", OracleDbType.Int64,IdReunionSolicitud, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<USP_S_OBTENER_REUNION_ADJUNTO_Response_Entity>("PCK_ITS_REUNION.USP_S_OBTENER_REUNION_ADJUNTO", param);
        }

        public async Task<long> EliminarReunionAdjuntos(USP_U_ELIMINAR_REUNION_ADJUNTO_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_IDS_ARCHIVO", OracleDbType.Varchar2, request.IdsArchivos, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_MODIFICA", OracleDbType.Int64, request.Usuario, ParameterDirection.Input)
            ];

            return await _db.ExecuteProcedureToLongWithOutput("PCK_ITS_REUNION.USP_U_ELIMINAR_REUNION_ADJUNTO", param, "P_RESULTADO");
        }
    }
}

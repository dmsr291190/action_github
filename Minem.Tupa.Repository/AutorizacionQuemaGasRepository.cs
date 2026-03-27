using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Minem.Tupa.Data;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Entity.AutorizacionQuemaGas;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Minem.Tupa.Repository
{
    public class AutorizacionQuemaGasRepository(Minem_Db_Context _minemDbContext) : IAutorizacionQuemaGasRepository
    {
        private readonly string _connectionString = _minemDbContext.Database.GetConnectionString() ?? string.Empty;

        public async Task<long> GuardarAccion(SP_INSERTAR_ACCION_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_INFO_ACCION_ID", OracleDbType.Int64, request.infoAccionId, ParameterDirection.Input),
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, request.informeId, ParameterDirection.Input),
                new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, request.descripcion, ParameterDirection.Input),
                new OracleParameter("P_ES_OBJECTIVO", OracleDbType.Int32, request.esObjetivo, ParameterDirection.Input),
                new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_ACCION_INFORME",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<long> GuardarAdjunto(SP_INSERTAR_ADJUNTO_INFORME_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_ARCHIVO_ID", OracleDbType.Int64, request.infoArchivoId, ParameterDirection.Input),
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, request.informeId, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_ARCHIVO", OracleDbType.Varchar2, request.nombreArchivo, ParameterDirection.Input),
                new OracleParameter("P_SECCION", OracleDbType.Int32, request.seccion, ParameterDirection.Input),
                new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_ARCHIVO_INFORME",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<long> GuardarBalance(SP_INSERTAR_BALANCE_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_INFO_BALANCE_ID", OracleDbType.Int64, request.infoBalanceId, ParameterDirection.Input),
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, request.informeId, ParameterDirection.Input),
                new OracleParameter("P_PERIODO", OracleDbType.Int32, request.periodo, ParameterDirection.Input),
                new OracleParameter("P_PRODUC_OIL", OracleDbType.Decimal, request.producOil, ParameterDirection.Input),
                new OracleParameter("P_PRODUC_GAS", OracleDbType.Decimal, request.producGas, ParameterDirection.Input),
                new OracleParameter("P_PRODUC_AGUA", OracleDbType.Decimal, request.producAgua, ParameterDirection.Input),
                new OracleParameter("P_CONSUMO_GAS", OracleDbType.Decimal, request.consumoGas, ParameterDirection.Input),
                new OracleParameter("P_INYECCION_GAS", OracleDbType.Decimal, request.inyeccionGas, ParameterDirection.Input),
                new OracleParameter("P_QUEMA_GAS", OracleDbType.Decimal, request.quemaGas, ParameterDirection.Input),
                new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_BALANCE_INFORME",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<long> GuardarCronograma(SP_INSERTAR_CRONOGRAMA_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_INFO_CRONOGRAMA_ID", OracleDbType.Int64, request.infoCronogramaId, ParameterDirection.Input),
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, request.informeId, ParameterDirection.Input),
                new OracleParameter("P_INFO_MOTIVO_FACILIDAD_ID", OracleDbType.Int64, request.infoMotivoFacilidadId, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, request.fechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FIN", OracleDbType.Varchar2, request.fechaFin, ParameterDirection.Input),
                new OracleParameter("P_VOL_GAS_QUEMADO", OracleDbType.Decimal, request.volGasQuemado, ParameterDirection.Input),
                new OracleParameter("P_VOL_LIQUIDO_QUEMADO", OracleDbType.Decimal, request.volLiquidoQuemado, ParameterDirection.Input),
                new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_CRONOGRAMA_INFORME",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<long> GuardarInforme(SP_INSERTAR_INFORME_JUSTIFICACION_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, request.informeId, ParameterDirection.Input),
                new OracleParameter("P_SOLICITUD_ID", OracleDbType.Int64, request.solicitudId, ParameterDirection.Input),
                new OracleParameter("P_LOTE_ID", OracleDbType.Int64, request.loteId, ParameterDirection.Input),
                new OracleParameter("P_ANTECEDENTES", OracleDbType.Clob, request.antecedentes, ParameterDirection.Input),
                new OracleParameter("P_OBJETIVO", OracleDbType.Clob, request.objetivo, ParameterDirection.Input),
                new OracleParameter("P_TRABAJO_REALIZAR", OracleDbType.Clob, request.trabajoRealizar, ParameterDirection.Input),
                new OracleParameter("P_FACILIDADES_EXISTENTES", OracleDbType.Clob, request.facilidadesExistentes, ParameterDirection.Input),
                new OracleParameter("P_PROCEDIMIENTO", OracleDbType.Clob, request.procedimiento, ParameterDirection.Input),
                new OracleParameter("P_PORQUE_NO_REINYECTAR", OracleDbType.Clob, request.porqueNoInyectar, ParameterDirection.Input),
                new OracleParameter("P_PORQUE_NO_COMERCIALIZAR", OracleDbType.Clob, request.porqueNoComercializar, ParameterDirection.Input),
                new OracleParameter("P_PORQUE_NO_UTILIZAR", OracleDbType.Clob, request.porqueNoUtilizar, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO_QUEMA", OracleDbType.Varchar2, request.fechaInicioQuema, ParameterDirection.Input),
                new OracleParameter("P_QUEMA_LIQUIDOS", OracleDbType.Int32, request.quemaLiquido, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_INFORME",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<long> GuardarFacilidad(SP_INSERTAR_FACILIDAD_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, request.informeId, ParameterDirection.Input),
                new OracleParameter("P_FACILIDAD_ID", OracleDbType.Int64, request.infoFacilidadId, ParameterDirection.Input),
                new OracleParameter("P_MOTIVO_ID", OracleDbType.Int64, request.infoMotivoId, ParameterDirection.Input),
                new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, request.descripcion, ParameterDirection.Input),
                new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_GUARDA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input),
                new OracleParameter("P_GOR", OracleDbType.Decimal, request.gor, ParameterDirection.Input),
                new OracleParameter("P_LATITUD", OracleDbType.Varchar2, request.latitud, ParameterDirection.Input),
                new OracleParameter("P_LONGITUD", OracleDbType.Varchar2, request.longitud, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_FACILIDAD_INFORME",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<long> GuardarQuemador(SP_INSERTAR_QUEMADOR_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_INFO_QUEMADOR_ID", OracleDbType.Int64, request.infoQuemadorId, ParameterDirection.Input),
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, request.informeId, ParameterDirection.Input),
                new OracleParameter("P_SERIE", OracleDbType.Varchar2, request.serie, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE", OracleDbType.Varchar2, request.nombre, ParameterDirection.Input),
                new OracleParameter("P_MARCA", OracleDbType.Varchar2, request.marca, ParameterDirection.Input),
                new OracleParameter("P_FABRICANTE", OracleDbType.Varchar2, request.fabricante, ParameterDirection.Input),
                new OracleParameter("P_TIPO", OracleDbType.Int32, request.tipo, ParameterDirection.Input),
                new OracleParameter("P_CONDICION", OracleDbType.Int32, request.condicion, ParameterDirection.Input),
                new OracleParameter("P_ANIO_FABRICACION", OracleDbType.Int32, request.anioFabricacion, ParameterDirection.Input),
                new OracleParameter("P_CAP_NOMINAL", OracleDbType.Int32, request.capNominal, ParameterDirection.Input),
                new OracleParameter("P_CAP_OPERATIVA", OracleDbType.Int32, request.capOperativa, ParameterDirection.Input),
                new OracleParameter("P_ALTURA", OracleDbType.Decimal, request.altura, ParameterDirection.Input),
                new OracleParameter("P_DIAMETRO", OracleDbType.Decimal, request.diametro, ParameterDirection.Input),
                new OracleParameter("P_DISTANCIA_OTRA", OracleDbType.Decimal, request.distanciaOtra, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_INSTALACION", OracleDbType.Varchar2, request.nombreInstalacion, ParameterDirection.Input),
                new OracleParameter("P_ENCENDIDO_AUTO", OracleDbType.Int32, request.encendidoAuto, ParameterDirection.Input),
                new OracleParameter("P_LATITUD", OracleDbType.Varchar2, request.latitud, ParameterDirection.Input),
                new OracleParameter("P_LONGITUD", OracleDbType.Varchar2, request.longitud, ParameterDirection.Input),
                new OracleParameter("P_ESTADO", OracleDbType.Int32, request.estado, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_GUARDA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_QUEMADOR_INFORME",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<long> InsertarMotivoInforme(SP_INSERTAR_MOTIVOS_INFORME_Request_Entity request)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_INFO_MOTIVO_ID", OracleDbType.Int64, request.infoMotivoId, ParameterDirection.Input),
                new OracleParameter("P_MOTIVO_ID", OracleDbType.Int64, request.motivoId, ParameterDirection.Input),
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, request.informeId, ParameterDirection.Input),
                new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, request.descripcion, ParameterDirection.Input),
                new OracleParameter("P_MOTIVO_PADRE_ID", OracleDbType.Int64, request.motivoPadreId, ParameterDirection.Input),
                new OracleParameter("P_INFORMACION_ADICIONAL", OracleDbType.Varchar2, request.informacionAdicional, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_GUARDA", OracleDbType.Int64, request.usuarioId, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_MOTIVO_INFORME",
                parametros,
                "P_RESULTADO"
            );
        }

        public async Task<List<SP_OBTENER_ACCION_Request_Entity>> ObtenerAcciones(long informeId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_C_ACCIONES_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_ACCION_Request_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_ACCIONES_INFORME", param);
        }

        public async Task<List<SP_OBTENER_ADJUNTO_INFORME_Response_Entity>> ObtenerAdjuntos(long informeId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_C_ARCHIVOS_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_ADJUNTO_INFORME_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_ARCHIVOS_INFORME", param);
        }

        public async Task<List<SP_OBTENER_BALANCE_Response_Entity>> ObtenerBalance(long informeId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_C_BALANCE_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_BALANCE_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_BALANCE_INFORME", param);
        }

        public async Task<List<SP_OBTENER_CRONOGRAMA_Response_Entity>> ObtenerCronograma(long informeId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_C_CRONOGRAMA_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_CRONOGRAMA_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_CRONOGRAMA_INFORME", param);
        }

        public async Task<SP_OBTENER_INFORME_Response_Entity> ObtenerInforme(long solicitudId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_SOLICITUD_ID", OracleDbType.Int64, solicitudId, ParameterDirection.Input),
                new OracleParameter("P_C_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            var lista = await _db.ExecuteProcedureToList<SP_OBTENER_INFORME_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_INFORME", param);

            return lista.FirstOrDefault();
        }

        public async Task<List<SP_OBTENER_FACILIDAD_Response_Entity>> ObtenerFacilidades(long informeId, long informeMotivoId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_MOTIVO_ID", OracleDbType.Int64, informeMotivoId, ParameterDirection.Input),
                new OracleParameter("P_C_FACILIDADES_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_FACILIDAD_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_FACILIDADES_INFORME", param);
        }

        public async Task<SP_OBTENER_FACILIDAD_Response_Entity?> ObtenerFacilidadPorNombre(long informeId, string nombre)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE", OracleDbType.Varchar2, nombre, ParameterDirection.Input),
                new OracleParameter("P_C_FACILIDADES_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            var lista = await _db.ExecuteProcedureToList<SP_OBTENER_FACILIDAD_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_FACILIDADES_INFORME_POR_NOMBRE", param);

            return lista?.FirstOrDefault();
        }

        public async Task<List<SP_OBTENER_LOTES_Response_Entity>> ObtenerLotes(long codigoPersona)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_COD_PERSONA", OracleDbType.Int64, codigoPersona, ParameterDirection.Input),
                new OracleParameter("P_C_LOTES_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_LOTES_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_LOTES", param);
        }

        public async Task<List<SP_OBTENER_MOTIVOS_Response_Entity>> ObtenerMotivos()
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_C_MOTIVOS_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_MOTIVOS_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_MOTIVOS_GENERALES", param);
        }

        public async Task<List<SP_INSERTAR_MOTIVOS_INFORME_Request_Entity>> ObtenerMotivosInforme(long informeId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_C_MOTIVOS_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_INSERTAR_MOTIVOS_INFORME_Request_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_MOTIVOS_INFORME", param);
        }

        public async Task<List<SP_OBTENER_QUEMADOR_Response_Entity>> ObtenerQuemadores(long informeId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_C_QUEMADORES_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_QUEMADOR_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_QUEMADORES_INFORME", param);
        }

        public async Task<SP_OBTENER_QUEMADOR_Response_Entity?> ObtenerQuemadorPorSerie(long usuarioId, string serie, string nombre)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_USUARIO_ID", OracleDbType.Int64, usuarioId, ParameterDirection.Input),
                new OracleParameter("P_SERIE", OracleDbType.Varchar2, serie, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE", OracleDbType.Varchar2, nombre, ParameterDirection.Input),
                new OracleParameter("P_C_QUEMADORES_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            var lista = await _db.ExecuteProcedureToList<SP_OBTENER_QUEMADOR_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_QUEMADOR_INFORME_POR_SERIE", param);

            return lista?.FirstOrDefault();
        }

        public async Task<List<SP_OBTENER_OBSERVACION_CAPITULO_Response_Entity>> ObtenerCantidadObservaciones(long informeId)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_C_OBSERVACIONES_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)

            ];

            return await _db.ExecuteProcedureToList<SP_OBTENER_OBSERVACION_CAPITULO_Response_Entity>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_OBSERVACIONES", param);
        }

        public async Task<List<ObservacionPorProyectoCapituloResponseDto>> ObtenerProyectosObservacionCapitulo(long idInforme, string capitulo)
        {
            var _db = new GenericRepository(_connectionString);
            List<OracleParameter> param =
            [
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, idInforme, ParameterDirection.Input),
                new OracleParameter("P_CAPITULO", OracleDbType.Varchar2, capitulo, ParameterDirection.Input),
                new OracleParameter("P_C_OBSERVACIONES_INFORME_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
            ];

            return await _db.ExecuteProcedureToList<ObservacionPorProyectoCapituloResponseDto>("PCK_AUTORIZACION_QUEMA_GAS.PRC_OBTENER_OBSERVACIONES_INFORME_CAP", param);
        }

        public async Task<long> GuardarInformeHistorico(long informeId, long usuarioId)
        {
            var _db = new GenericRepository(_connectionString);

            List<OracleParameter> parametros = new List<OracleParameter>
            {
                new OracleParameter("P_INFORME_ID", OracleDbType.Int64, informeId, ParameterDirection.Input),
                new OracleParameter("P_USUARIO_REGISTRA", OracleDbType.Int64, usuarioId, ParameterDirection.Input)
            };

            return await _db.ExecuteProcedureToLongWithOutput(
                "PCK_AUTORIZACION_QUEMA_GAS.PRC_GUARDAR_INFORME_HISTORICO",
                parametros,
                "P_RESULTADO"
            );
        }
    }
}

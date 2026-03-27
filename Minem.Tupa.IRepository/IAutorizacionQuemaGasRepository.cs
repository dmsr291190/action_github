using Minem.Tupa.Dto.Its;
using Minem.Tupa.Entity.AutorizacionQuemaGas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IRepository
{
    public interface IAutorizacionQuemaGasRepository
    {
        Task<long> GuardarAccion(SP_INSERTAR_ACCION_Request_Entity request);
        Task<long> GuardarAdjunto(SP_INSERTAR_ADJUNTO_INFORME_Request_Entity request);
        Task<long> GuardarBalance(SP_INSERTAR_BALANCE_Request_Entity request);
        Task<long> GuardarCronograma(SP_INSERTAR_CRONOGRAMA_Request_Entity request);
        Task<long> GuardarInforme(SP_INSERTAR_INFORME_JUSTIFICACION_Request_Entity request);
        Task<long> GuardarFacilidad(SP_INSERTAR_FACILIDAD_Request_Entity request);
        Task<long> GuardarQuemador(SP_INSERTAR_QUEMADOR_Request_Entity request);
        Task<long> InsertarMotivoInforme(SP_INSERTAR_MOTIVOS_INFORME_Request_Entity request);
        Task<List<SP_OBTENER_ACCION_Request_Entity>> ObtenerAcciones(long informeId);
        Task<List<SP_OBTENER_ADJUNTO_INFORME_Response_Entity>> ObtenerAdjuntos(long informeId);
        Task<List<SP_OBTENER_BALANCE_Response_Entity>> ObtenerBalance(long informeId);
        Task<List<SP_OBTENER_CRONOGRAMA_Response_Entity>> ObtenerCronograma(long informeId);
        Task<SP_OBTENER_INFORME_Response_Entity> ObtenerInforme(long solicitudId);
        Task<List<SP_OBTENER_FACILIDAD_Response_Entity>> ObtenerFacilidades(long informeId, long informeMotivoId);
        Task<SP_OBTENER_FACILIDAD_Response_Entity?> ObtenerFacilidadPorNombre(long informeId, string nombre);
        Task<List<SP_OBTENER_LOTES_Response_Entity>> ObtenerLotes(long codigoPersona);
        Task<List<SP_OBTENER_MOTIVOS_Response_Entity>> ObtenerMotivos();
        Task<List<SP_INSERTAR_MOTIVOS_INFORME_Request_Entity>> ObtenerMotivosInforme(long informeId);
        Task<List<SP_OBTENER_QUEMADOR_Response_Entity>> ObtenerQuemadores(long informeId);
        Task<SP_OBTENER_QUEMADOR_Response_Entity?> ObtenerQuemadorPorSerie(long usuarioId, string serie, string nombre);
        Task<List<SP_OBTENER_OBSERVACION_CAPITULO_Response_Entity>> ObtenerCantidadObservaciones(long informeId);
        Task<List<ObservacionPorProyectoCapituloResponseDto>> ObtenerProyectosObservacionCapitulo(long idInforme, string capitulo);
        Task<long> GuardarInformeHistorico(long informeId, long usuarioId);
    }
}

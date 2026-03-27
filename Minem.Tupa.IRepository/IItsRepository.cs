using Minem.Tupa.Dto.Its;
using Minem.Tupa.Entity.Its;
using Minem.Tupa.Entity.Tramite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IRepository
{
    public interface IItsRepository
    {
        Task<long> InsertarProyectoITS(SP_INSERT_ITS_PROYECTO_Response_Entity request);

        Task<List<SP_OBTENER_ITS_PROYECTO_Request_Entity>> ObtenerProyecto(SP_OBTENER_ITS_PROYECTO_Request_Entity request);

        Task<long> InsertarProyectoArchivoITS(SP_INSERT_ITS_PROYECTO_ARCHIVO_Response_Entity request);

        Task<List<SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity>> ObtenerProyectoArchivos(SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity request);

        Task<long> EliminarProyectoArchivos(SP_ELIMINAR_ITS_PROYECTO_ARCHIVO_Response_Entity request);

        Task<long> InsertarRepresentanteITS(SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity request);

        Task<List<SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity>> ObtenerRepresentante(SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity request);
        Task<long> InsertarProfesionalITS(SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity request);

        Task<List<SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity>> ObtenerProfesional(SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity request);

        Task<long> EliminarProfesional(SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity request);
        
        Task<long> InsertarMapa(SP_INSERT_ITS_MAPA_Request_Entity request);
        Task<long> ActualizarMapa(SP_UPDATE_ITS_MAPA_Request_Entity request);
        Task<SP_OBTENER_ITS_MAPA_Response_Entity> ObtenerMapa(long idProyecto);
        Task<SP_OBTENER_ITS_MAPA_Response_Entity> ObtenerMapaConSolicitud(long codMaeSolicitud);
        Task<long> EliminarMapa(long idProyecto);
        Task<List<ObservacionPorProyectoCapituloResponseDto>> ObtenerProyectosObservacionCapitulo(long idProyecto, string capitulo);
        Task<List<ObservacionesProyectoPorCapitulo_Response_Entity>> ObtenerTotalObservacionesPorProyectoYCapitulos(long idProyecto);
        Task<ItsReunion_Response_Entity> ObtenerReunionIts(long codMaeSolicitud);
        Task<List<ObservacionPorProyectoCapituloResponseDto>> ObtenerRespuestasObservacionIts(long codMaeObservacion);
        Task<long> InsertarReunionAdjunto(USP_I_INSERTAR_REUNION_ADJUNTO_Request_Entity request);
        Task<List<USP_S_OBTENER_REUNION_ADJUNTO_Response_Entity>> ObtenerReunionAdjuntos(int IdReunionSolicitud);
        Task<long> EliminarReunionAdjuntos(USP_U_ELIMINAR_REUNION_ADJUNTO_Request_Entity request);
    }
}

using System.Threading.Tasks;
using System.Collections.Generic;
using Minem.Tupa.Utils;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Dto.Formulario;

namespace Minem.Tupa.IApplication
{
    public interface IItsApplication
    {
        Task<StatusResponse<long>> InsertarProyectoITS(ItsProyectoDto request);

        Task<StatusResponse<List<ItsProyectoDto>>> ObtenerProyecto(ItsProyectoDto request);

        Task<StatusResponse<long>> InsertarProyectoArchivoITS(ItsProyectoArchivoDto request);

        Task<StatusResponse<List<ItsProyectoArchivoDto>>> ObtenerProyectoArchivos(ItsProyectoArchivoDto request);

        Task<StatusResponse<long>> EliminarProyectoArchivos(ItsProyectoArchivoEliminarDto request);

        Task<StatusResponse<long>> InsertarRepresentanteITS(ItsProyectoRepresentanteDto request);

        Task<StatusResponse<List<ItsProyectoRepresentanteDto>>> ObtenerRepresentante(ItsProyectoRepresentanteDto request);
        Task<StatusResponse<long>> InsertarProfesionalITS(ItsProyectoProfesionalDto request);

        Task<StatusResponse<List<ItsProyectoProfesionalDto>>> ObtenerProfesional(ItsProyectoProfesionalDto request);

        Task<StatusResponse<long>> EliminarProfesional(ItsProyectoProfesionalDto request);

        Task<StatusResponse<ObtenerFormularioDiaResponseDto>> ObtenerPadre(long CodMaeSolicitud);
        
        Task<StatusResponse<long>> InsertarMapa(ItsMapaDto request);
        Task<StatusResponse<long>> ActualizarMapa(ItsMapaDto request);
        Task<StatusResponse<ItsMapaDto>> ObtenerMapa(int IdProyecto);
        Task<StatusResponse<ItsMapaDto>> ObtenerMapaConSolicitud(int codMaeSolicitud);
        Task<StatusResponse<long>> EliminarMapa(int IdProyecto);
        Task<StatusResponse<List<ObservacionPorProyectoCapituloResponseDto>>> ObtenerProyectosObservacionCapitulo(long idProyecto, string capitulo);
        Task<StatusResponse<ObservacionesPorProyectoYCapRenposeDto>> ObtenerProyectoTotalObservacionesPorCapitulo(long idProyecto);
        Task<StatusResponse<ItsReunionDto>> ObtenerReunionIts(long codMaeSolicitud);
        Task<StatusResponse<List<ObservacionPorProyectoCapituloResponseDto>>> ObtenerRespuestasObservacionIts(long codMaeObservacion);
        Task<StatusResponse<long>> InsertarReunionAdjunto(ItsReunionAdjuntoDto request);
        Task<StatusResponse<List<ItsReunionAdjuntoDto>>> ObtenerReunionAdjuntos(int IdReunionSolicitud);
        Task<StatusResponse<long>> EliminarReunionAdjuntos(ItsReunionAdjuntoEliminarDto request);
    }
}

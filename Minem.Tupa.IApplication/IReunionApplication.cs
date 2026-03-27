using System.Threading.Tasks;
using Minem.Tupa.Dto.Reunion;
using System.Collections.Generic;
using Minem.Tupa.Utils;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Dto.Its;

namespace Minem.Tupa.IApplication
{
    public interface IReunionApplication
    {
   
        Task<StatusResponse<long>> InsertarSolicitudReunion(ReunionSolicitudDto request);
        Task<StatusResponse<long>> InsertarReunionParticipante(long idReunion, string tipoParticipante, long idPersona, ReunionParticipanteDto request);

        Task<StatusResponse<long>> InsertarReunionCorreo(long idReunion, long idPersona, string correo);
        Task<StatusResponse<long>> InsertarReunionObjetico(long idReunion, long idPersona, ReunionObjetivoDto request);

        Task<StatusResponse<List<ReunionRequestDto>>> ObtenerReunion(ReunionRequestDto request);

        Task<StatusResponse<List<ReunionHistorialDto>>> GetHistorialReuniones(long CodMaeSolicitud);
        /*
         Task<long> InsertarReunionParticipante(ReunionParticipanteDto request);
         Task<long> InsertarReunionCorreo(ReunionCorreoDto request);
         Task<long> InsertarReunionObjetivo(ReunionObjetivoDto request);

         Task<long> ActualizarReunionSolicitud(ReunionSolicitudDto request);
         Task<long> ActualizarReunionParticipante(ReunionParticipanteDto request);
         Task<long> ActualizarReunionObjetivo(ReunionObjetivoDto request);

         Task<ReunionSolicitudDto> ObtenerReunionSolicitud(long idreunionsolicitud);
         Task<List<ReunionParticipanteDto>> ObtenerParticipantesPorSolicitud(long idreunionsolicitud);
         Task<List<ReunionCorreoDto>> ObtenerCorreosPorSolicitud(long idreunionsolicitud);
         Task<List<ReunionObjetivoDto>> ObtenerObjetivosPorSolicitud(long idreunionsolicitud);
        */
    }
}

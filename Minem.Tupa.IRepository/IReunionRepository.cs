using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minem.Tupa.Dto.Reunion;
using Minem.Tupa.Entity.Its;
using Minem.Tupa.Entity.Reunion;

namespace Minem.Tupa.IRepository
{
    public interface IReunionRepository
    {
        Task<long> InsertarSolicitudReunion(SP_INSERT_REUNION_SOLICITUD_Response_Entity request);
        Task<long> InsertarReunionParticipante(long idReunion, string tipoParticipante, long idPersona, SP_INSERT_REUNION_PARTICIPANTE_Response_Entity request);
        Task<long> InsertarReunionCorreo(long idReunion, long idPersona, string correo);
        Task<long> InsertarReunionObjetico(long idReunion, long idPersona, SP_INSERT_REUNION_OBJETIVO_Response_Entity request);

        Task<List<SP_OBTENER_REUNION_Request_Entity>> ObtenerReunion(SP_OBTENER_REUNION_Request_Entity request);

        Task<List<SP_OBTENER_REUNION_HISTORIAL_Request_Entity>> GetHistorialReuniones(long CodMaeSolicitud);

    }
}

using Minem.Tupa.Dto.Svc.Notificacion;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Proxy.Interface
{
    public interface INotificacionService
    {
        Task RegistrarNotificacionInterna(NotificationInternaRequestDto request);
        Task<StatusResponse<List<DocumentoDespachadoResponseDto>>> GetDocumentosDespachados(int idSolicitud);

    }
}

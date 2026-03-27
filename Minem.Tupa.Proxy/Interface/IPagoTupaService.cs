using Minem.Tupa.Dto.Svc.PagoTupa;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Proxy.Interface
{
    public interface IPagoTupaService
    {
        Task PagoCajaMinemAsignarExpediente(AsignarExpedientePagoRequestDto request);
        Task<StatusResponse<int>> ValidarPagoCajaMinem(ValidarPagoCajaMinemRequestDto request);
        Task<StatusResponse<int>> RegistrarPagoCajaMinem(RegistrarPagoCajaMinemRequestDto request);
        Task PagoPagaloPEAsignarExpediente(AsignarExpedientePagoRequestDto request);
        Task<StatusResponse<int>> ValidarPagoPagaloPE(ValidarPagoPagaloPeRequestDto request);
    }
}

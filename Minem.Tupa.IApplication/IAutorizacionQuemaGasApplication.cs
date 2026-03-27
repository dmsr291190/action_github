using Minem.Tupa.Dto.AutorizacionQuemaGas;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface IAutorizacionQuemaGasApplication
    {
        Task<StatusResponse<long>> GuardarInforme(InformeJustificacionDto request);
        Task<StatusResponse<InformeJustificacionDto>> ObtenerInforme(long solicitudId);
        Task<StatusResponse<List<LoteDto>>> ObtenerLotes();
        Task<StatusResponse<List<MotivoDto>>> ObtenerMotivos();
        Task<StatusResponse<QuemadorDto?>> ObtenerQuemadorPorSerie(long usuarioId, string serie, string nombre);
        Task<StatusResponse<List<ObservacionCapituloDto>>> ObtenerCantidadObservaciones(long informeId);
        Task<StatusResponse<List<ObservacionPorProyectoCapituloResponseDto>>> ObtenerProyectosObservacionCapitulo(long idInforme, string capitulo);
    }
}

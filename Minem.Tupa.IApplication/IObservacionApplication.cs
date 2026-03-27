using Minem.Tupa.Dto.Observacion;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface IObservacionApplication
    {
        Task<StatusResponse<int>> InsertarObservacion(ObservacionRequestDto request);
        Task<StatusResponse<List<ObshistjsonDto>>> ConsultaObshistjson(int codmaesolicitud, string capitulo, int iddetobshistjsonpadre);
        Task<StatusResponse<int>> ActualizarObservacion(ObservacionRequestDto request);
        Task<StatusResponse<int>> EliminarObservacion(int iddetobshistjson);
        Task<StatusResponse<List<ObshistjsonDto>>> ObservacionesSolicitud(int codmaesolicitud);
        
    }
}

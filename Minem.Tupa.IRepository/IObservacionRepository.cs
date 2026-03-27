using Minem.Tupa.Entity.Observacion;

namespace Minem.Tupa.IRepository
{
    public interface IObservacionRepository
    {
        Task<List<PRC_S_ULTIMOTUMOVMETADAHISTORICO_Response_Entity>> ConsultaTumovmetadahistorico(int codmaesolicitud);
        Task<int> InsertObshistjson(PRC_I_OBSHISTJSON_Request_Entity request);
        Task<int> InsertDetobshistjson(PRC_I_DETOBSHISTJSON_Request_Entity request);
        Task<List<PRC_S_OBSHISTJSON_Response_Entity>> ConsultaObshistjson(int codmaesolicitud, string capitulo, int iddetobshistjsonpadre);
        Task<int> ConsultaExisteSolicitudCapitulo(int codmaesolicitud, string capitulo);
        Task<int> UpdateDetobshistjson(PRC_I_DETOBSHISTJSON_Request_Entity request);
        Task<int> DeleteDetobshistjson(int iddetobshistjson);

        Task<List<PRC_S_OBSHISTJSON_Response_Entity>> ObservacionesSolicitud(int codmaesolicitud);
    }
}

using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.Entity.Solicitud;
using Minem.Tupa.Entity.Tupa;
using System.Threading.Tasks;

namespace Minem.Tupa.IRepository
{
    public interface ITupaRepository
    {
        Task<List<TupaEntity>> ObtenerTupaPorSector(long idSector, string tipoPersona);
        Task<TupaEntity> ObtenerTupaPorCodigo(string codigoTupa);
        Task<List<EstructuraCapituloAdjuntosResponse_Entity>> ListarEstructuraCapituloAdjuntos();
        Task<List<TupaEntity>> ObtenerTupa();
        Task<SolicitudResponse_Entity> ObtenerSolicitudPorCodigo(int codMaeSolicitud);
        Task<List<DocumentoDespachadoResponse_Entity>> ObtenerDocumentosDespachados(long codMaeSolicitud);
    }
}

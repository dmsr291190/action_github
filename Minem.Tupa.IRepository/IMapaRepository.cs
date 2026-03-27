
using Minem.Tupa.Entity.Mapa;

namespace Minem.Tupa.IRepository
{
    public interface IMapaRepository
    {
        Task<List<SP_SELECT_TIPO_AREA_Response_Entity>> ObtenerTipoActividad(int tipo);
    }
}

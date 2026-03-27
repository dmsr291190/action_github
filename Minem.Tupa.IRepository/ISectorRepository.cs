using Minem.Tupa.Entity.Tupa;

namespace Minem.Tupa.IRepository
{
    public interface ISectorRepository
    {
        Task<List<SectorEntity>> ObtenerSectores();
    }
}

using Minem.Tupa.Entity.Tupa;

namespace Minem.Tupa.IRepository
{
    public interface IRequisitoRepository
    {
        Task<List<RequisitoEntity>> ObtenerRequisitos(string codigoTupa);
    }
}

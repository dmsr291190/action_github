using Minem.Tupa.Entity;
using Minem.Tupa.Entity.Profesional;
using Minem.Tupa.Entity.Tramite;

namespace Minem.Tupa.IRepository
{
    public interface IProfesionalRepository
    {
        Task<List<SP_OBTENER_PROFESIONES_Response_Entity>> ObtenerProfesiones();
    }
}

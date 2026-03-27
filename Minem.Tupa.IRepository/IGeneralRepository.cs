using Minem.Tupa.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IRepository
{
    public interface IGeneralRepository
    {
        Task<GEMovPersona> GetPersonaPorCodMovUsuario(int codMovUsuario);
        Task<List<GEMovPersona>> GetPersonaOrganicaTupa(int CodIdMaeTupa);
        Task<List<GEMovPersona>> GetPersonaOrganicaRol(int CodIdMaeTupa, int CodMaeUniOrganica);
        

    }
}

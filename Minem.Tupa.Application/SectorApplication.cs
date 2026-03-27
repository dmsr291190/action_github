using AutoMapper;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;

namespace Minem.Tupa.Application
{
    public class SectorApplication : ISectorApplication
    {
        private readonly IMapper _mapper;
        private readonly ISectorRepository _sectorRepository;

        public SectorApplication(ISectorRepository sectorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _sectorRepository = sectorRepository;
        }

        public async Task<StatusResponse<List<SectorDto>>> ObtenerSectores()
        {
            try
            {
                var respuesta = _mapper.Map<List<SectorDto>>(await _sectorRepository.ObtenerSectores());
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<SectorDto>>(ex);
            }

        }
    }
}

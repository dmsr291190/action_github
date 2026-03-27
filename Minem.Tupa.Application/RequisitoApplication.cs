using AutoMapper;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;

namespace Minem.Tupa.Application
{
    public class RequisitoApplication : IRequisitoApplication
    {
        private readonly IMapper _mapper;
        private readonly IRequisitoRepository _requisitoRepository;

        public RequisitoApplication(IRequisitoRepository requisitoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _requisitoRepository = requisitoRepository;
        }

        public async Task<StatusResponse<List<RequisitoDto>>> ObtenerRequisitos(string codigoTupa)
        {
            try
            {
                var respuesta = _mapper.Map<List<RequisitoDto>>(await _requisitoRepository.ObtenerRequisitos(codigoTupa));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<RequisitoDto>>(ex);
            }
        }
    }
}

using AutoMapper;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Mapa;
using Minem.Tupa.Dto.Observacion;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Application
{
    public class MapaApplication(IMapaRepository repository, IMapper mapper) : IMapaApplication
    {
        private readonly IMapaRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<StatusResponse<List<TipoAreaResponseDto>>> ObtenerTipoActividad(int tipo)
        {
            try
            {
                var respuesta = _mapper.Map<List<TipoAreaResponseDto>>(await _repository.ObtenerTipoActividad(tipo));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<TipoAreaResponseDto>>(ex);
            }
        }
    }
}

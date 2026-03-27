using AutoMapper;
using Microsoft.AspNetCore.Http;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Profesional;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.Entity.Tramite;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;
using System.Security.Claims;

namespace Minem.Tupa.Application
{
    public class ProfesionalApplication : IProfesionalApplication
    {
        private readonly IMapper _mapper;
        private readonly IProfesionalRepository _profesionalRepository;

        public ProfesionalApplication(IProfesionalRepository profesionalRepository, IMapper mapper)
        {
            _mapper = mapper;
            _profesionalRepository = profesionalRepository;
        }

        public async Task<StatusResponse<List<ObtenerProfesionesResponseDto>>> ObtenerProfesiones()
        {
            try
            {
                var respuesta = _mapper.Map<List<ObtenerProfesionesResponseDto>>(
                    await _profesionalRepository.ObtenerProfesiones());
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObtenerProfesionesResponseDto>>(ex);
            }
        }
    }
}

using AutoMapper;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;

namespace Minem.Tupa.Application
{
    public class TupaApplication : ITupaApplication
    {
        private readonly IMapper _mapper;
        private readonly ITupaRepository _tupaRepository;

        public TupaApplication(ITupaRepository tupaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tupaRepository = tupaRepository;
        }

        public async Task<StatusResponse<List<TupaDto>>> ObtenerTupaPorSector(long idSector, string tipoPersona)
        {
            try
            {
                var respuesta = _mapper.Map<List<TupaDto>>(await _tupaRepository.ObtenerTupaPorSector(idSector, tipoPersona));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<TupaDto>>(ex);
            }

        }

        public async Task<StatusResponse<TupaDto>> ObtenerTupaPorCodigo(string codigoTupa)
        {
            try
            {
                var respuesta = _mapper.Map<TupaDto>(await _tupaRepository.ObtenerTupaPorCodigo(codigoTupa));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<TupaDto>(ex);
            }

        }

        public async Task<StatusResponse<List<TupaDto>>> ObtenerTupa()
        {
            try
            {
                var respuesta = _mapper.Map< List<TupaDto>>(await _tupaRepository.ObtenerTupa());
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception< List<TupaDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<DocumentoDespachadoDto>>> ObtenerDocumentosDespachados(long codMaeSolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<List<DocumentoDespachadoDto>>(await _tupaRepository.ObtenerDocumentosDespachados(codMaeSolicitud));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<DocumentoDespachadoDto>>(ex);
            }
        }
    }
}

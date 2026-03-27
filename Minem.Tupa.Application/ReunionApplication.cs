using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minem.Tupa.Dto.Reunion;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;
using Minem.Tupa.IRepository;
using Minem.Tupa.Proxy.Interface;
using System.Security.Claims;
using static Minem.Tupa.Utils.Constante;
using SVC_NOTIFICACION = Minem.Tupa.Dto.Svc.Notificacion;
using Minem.Tupa.Entity.Reunion;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Entity.Its;


namespace Minem.Tupa.Application
{
    public class ReunionApplication : IReunionApplication
    {
        private readonly IMapper _mapper;
        private readonly ITramiteRepository _tramiteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IFormularioRepository _formularioRepository;
        private readonly IEmailApplication _emailApplication;
        private readonly IGeneralRepository _generalRepository;
        private readonly IConfiguration _configuration;
        private readonly IReunionRepository _reunionRepository;

        public ReunionApplication(ITramiteRepository tramiteRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IFormularioRepository formularioRepository,
             IEmailApplication emailApplication, IGeneralRepository generalRepository, IConfiguration configuration, IReunionRepository reunionRepository)
        {
            _mapper = mapper;
            _tramiteRepository = tramiteRepository;
            _contextAccessor = contextAccessor;
            _formularioRepository = formularioRepository;
            _formularioRepository = formularioRepository;
            _emailApplication = emailApplication;
            _generalRepository = generalRepository;
            _configuration = configuration;
            _reunionRepository = reunionRepository;
        }

        public async Task<StatusResponse<long>> InsertarSolicitudReunion(ReunionSolicitudDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _reunionRepository.InsertarSolicitudReunion(_mapper.Map<SP_INSERT_REUNION_SOLICITUD_Response_Entity>(request)); 

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> InsertarReunionParticipante(long idReunion, string tipoParticipante, long idPersona, ReunionParticipanteDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _reunionRepository.InsertarReunionParticipante( idReunion, tipoParticipante ,idPersona , _mapper.Map<SP_INSERT_REUNION_PARTICIPANTE_Response_Entity>(request));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> InsertarReunionCorreo(long idReunion, long idPersona, string correo)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _reunionRepository.InsertarReunionCorreo(idReunion,  idPersona, correo);

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> InsertarReunionObjetico(long idReunion, long idPersona, ReunionObjetivoDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _reunionRepository.InsertarReunionObjetico(idReunion, idPersona, _mapper.Map<SP_INSERT_REUNION_OBJETIVO_Response_Entity>(request));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<List<ReunionRequestDto>>> ObtenerReunion(ReunionRequestDto request)
        {
            try
            {
                var respuesta = _mapper.Map<List<ReunionRequestDto>>(
                    await _reunionRepository.ObtenerReunion(_mapper.Map<SP_OBTENER_REUNION_Request_Entity>(request)));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ReunionRequestDto>>(ex);
            }
        }


        public async Task<StatusResponse<List<ReunionHistorialDto>>> GetHistorialReuniones(long CodMaeSolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<List<ReunionHistorialDto>>(
                   await _reunionRepository.GetHistorialReuniones(CodMaeSolicitud));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ReunionHistorialDto>>(ex);
            }

        }

    }
}

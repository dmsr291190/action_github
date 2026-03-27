using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Entity.Its;
using Minem.Tupa.Entity.Tramite;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;

namespace Minem.Tupa.Application
{
    public class ItsApplication : IItsApplication
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IItsRepository _itsRepository;
        private readonly ITramiteRepository _tramiteRepository;
        private readonly IFormularioRepository _formularioRepository;
        public ItsApplication(IMapper mapper, IHttpContextAccessor contextAccessor, IItsRepository itsRepository, ITramiteRepository tramiteRepository, IFormularioRepository formularioRepository)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _itsRepository = itsRepository;
            _tramiteRepository = tramiteRepository;
            _formularioRepository = formularioRepository;
        }

        public async Task<StatusResponse<long>> InsertarProyectoITS(ItsProyectoDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _itsRepository.InsertarProyectoITS(_mapper.Map<SP_INSERT_ITS_PROYECTO_Response_Entity>(request));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<List<ItsProyectoDto>>> ObtenerProyecto(ItsProyectoDto request)
        {
            try
            {
                var respuesta = _mapper.Map<List<ItsProyectoDto>>(
                    await _itsRepository.ObtenerProyecto(_mapper.Map<SP_OBTENER_ITS_PROYECTO_Request_Entity>(request)));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ItsProyectoDto>>(ex);
            }
        }


        public async Task<StatusResponse<long>> InsertarProyectoArchivoITS(ItsProyectoArchivoDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _itsRepository.InsertarProyectoArchivoITS(_mapper.Map<SP_INSERT_ITS_PROYECTO_ARCHIVO_Response_Entity>(request));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }


        public async Task<StatusResponse<List<ItsProyectoArchivoDto>>> ObtenerProyectoArchivos(ItsProyectoArchivoDto request)
        {
            try
            {
                var respuesta = _mapper.Map<List<ItsProyectoArchivoDto>>(
                    await _itsRepository.ObtenerProyectoArchivos(_mapper.Map<SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity>(request)));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ItsProyectoArchivoDto>>(ex);
            }
        }

        public async Task<StatusResponse<long>> EliminarProyectoArchivos(ItsProyectoArchivoEliminarDto request)
        {
            try
            {
                var solicitud = await _itsRepository.EliminarProyectoArchivos(_mapper.Map<SP_ELIMINAR_ITS_PROYECTO_ARCHIVO_Response_Entity>(request));
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> InsertarRepresentanteITS(ItsProyectoDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _itsRepository.InsertarProyectoITS(_mapper.Map<SP_INSERT_ITS_PROYECTO_Response_Entity>(request));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> InsertarRepresentanteITS(ItsProyectoRepresentanteDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _itsRepository.InsertarRepresentanteITS(_mapper.Map<SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity>(request));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<List<ItsProyectoRepresentanteDto>>> ObtenerRepresentante(ItsProyectoRepresentanteDto request)
        {
            try
            {
                var respuesta = _mapper.Map<List<ItsProyectoRepresentanteDto>>(
                    await _itsRepository.ObtenerRepresentante(_mapper.Map<SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity>(request)));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ItsProyectoRepresentanteDto>>(ex);
            }
        }

        public async Task<StatusResponse<long>> InsertarProfesionalITS(ItsProyectoProfesionalDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _itsRepository.InsertarProfesionalITS(_mapper.Map<SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity>(request));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<List<ItsProyectoProfesionalDto>>> ObtenerProfesional(ItsProyectoProfesionalDto request)
        {
            try
            {
                var respuesta = _mapper.Map<List<ItsProyectoProfesionalDto>>(
                    await _itsRepository.ObtenerProfesional(_mapper.Map<SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity>(request)));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ItsProyectoProfesionalDto>>(ex);
            }
        }

        public async Task<StatusResponse<long>> EliminarProfesional(ItsProyectoProfesionalDto request)
        {
            try
            {
                var solicitud = await _itsRepository.EliminarProfesional(_mapper.Map<SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity>(request));
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<ObtenerFormularioDiaResponseDto>> ObtenerPadre(long CodMaeSolicitud)
        {
            try
            {
                var solicitud = await _tramiteRepository.ObtenerDatosSolicitud(CodMaeSolicitud);
                long idPadre = (long)solicitud.CodMaeSolicitudOrigen;
                var respuesta = _mapper.Map<ObtenerFormularioDiaResponseDto>(
                    await _formularioRepository.ObtenerFormularioDia(idPadre));

                if (string.IsNullOrEmpty(respuesta.DataJson))
                {
                    respuesta.DataJson = Constante.JsonData;
                }

                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<ObtenerFormularioDiaResponseDto>(ex);
            }
        }

        public async Task<StatusResponse<long>> InsertarMapa(ItsMapaDto request)
        {
            try
            {
                var solicitud = await _itsRepository.InsertarMapa(_mapper.Map<SP_INSERT_ITS_MAPA_Request_Entity>(request));
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> ActualizarMapa(ItsMapaDto request)
        {
            try
            {
                var solicitud = await _itsRepository.ActualizarMapa(_mapper.Map<SP_UPDATE_ITS_MAPA_Request_Entity>(request));
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<ItsMapaDto>> ObtenerMapa(int IdProyecto)
        {
            try
            {
                var entidad = await _itsRepository.ObtenerMapa(IdProyecto);
                var respuesta = _mapper.Map<ItsMapaDto>(entidad);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<ItsMapaDto>(ex);
            }
        }
        public async Task<StatusResponse<ItsMapaDto>> ObtenerMapaConSolicitud(int codMaeSolicitud)
        {
            try
            {
                var entidad = await _itsRepository.ObtenerMapaConSolicitud(codMaeSolicitud);
                var respuesta = _mapper.Map<ItsMapaDto>(entidad);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<ItsMapaDto>(ex);
            }
        }

        public async Task<StatusResponse<long>> EliminarMapa(int IdProyecto)
        {
            try
            {
                var solicitud = await _itsRepository.EliminarMapa(IdProyecto);
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<List<ObservacionPorProyectoCapituloResponseDto>>> ObtenerProyectosObservacionCapitulo(long idProyecto, string capitulo)
        {
            try
            {
                var resultado = await _itsRepository.ObtenerProyectosObservacionCapitulo(idProyecto, capitulo);
                return Message.Successful(resultado);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObservacionPorProyectoCapituloResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<ObservacionesPorProyectoYCapRenposeDto>> ObtenerProyectoTotalObservacionesPorCapitulo(long idProyecto)
        {
            try
            {
                var responseDto = new ObservacionesPorProyectoYCapRenposeDto();
                var resultado = await _itsRepository.ObtenerTotalObservacionesPorProyectoYCapitulos(idProyecto);

                responseDto.EstaObservado = resultado[0].EstaObservado > 0;
                responseDto.TotalObservados = resultado[0].TotalObservados;

                responseDto.CapitulosObservados = new List<ObservacionesPorCapitulo>();
                if (responseDto.EstaObservado)
                {
                    responseDto.CapitulosObservados = resultado.Select(s => new ObservacionesPorCapitulo
                    {
                        Capitulo = s.Capitulo,
                        TotalObservado = s.TotalObservadoCapitulo
                    }).ToList();
                }

                return Message.Successful(responseDto);
            }
            catch (Exception ex)
            {
                return Message.Exception<ObservacionesPorProyectoYCapRenposeDto>(ex);
            }
        }

        public async Task<StatusResponse<ItsReunionDto>> ObtenerReunionIts(long codMaeSolicitud)
        {
            try
            {
                var entidad = await _itsRepository.ObtenerReunionIts(codMaeSolicitud);
                var respuesta = _mapper.Map<ItsReunionDto>(entidad);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<ItsReunionDto>(ex);
            }
        }

        public async Task<StatusResponse<List<ObservacionPorProyectoCapituloResponseDto>>> ObtenerRespuestasObservacionIts(long codMaeObservacion)
        {
            try
            {
                var respuesta = await _itsRepository.ObtenerRespuestasObservacionIts(codMaeObservacion);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObservacionPorProyectoCapituloResponseDto>> (ex);
            }
        }

        public async Task<StatusResponse<long>> InsertarReunionAdjunto(ItsReunionAdjuntoDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                var solicitud = await _itsRepository.InsertarReunionAdjunto(_mapper.Map<USP_I_INSERTAR_REUNION_ADJUNTO_Request_Entity>(request));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }


        public async Task<StatusResponse<List<ItsReunionAdjuntoDto>>> ObtenerReunionAdjuntos(int IdReunionSolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<List<ItsReunionAdjuntoDto>>(
                    await _itsRepository.ObtenerReunionAdjuntos(IdReunionSolicitud));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ItsReunionAdjuntoDto>>(ex);
            }
        }

        public async Task<StatusResponse<long>> EliminarReunionAdjuntos(ItsReunionAdjuntoEliminarDto request)
        {
            try
            {
                var solicitud = await _itsRepository.EliminarReunionAdjuntos(_mapper.Map<USP_U_ELIMINAR_REUNION_ADJUNTO_Request_Entity>(request));
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }
    }
}

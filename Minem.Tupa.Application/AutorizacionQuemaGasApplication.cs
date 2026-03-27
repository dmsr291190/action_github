using AutoMapper;
using Microsoft.AspNetCore.Http;
using Minem.Tupa.Dto.AutorizacionQuemaGas;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Entity.AutorizacionQuemaGas;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Application
{
    public class AutorizacionQuemaGasApplication : IAutorizacionQuemaGasApplication
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAutorizacionQuemaGasRepository _autorizacionQuemaGasRepository;

        public AutorizacionQuemaGasApplication(IMapper mapper, IHttpContextAccessor contextAccessor, IAutorizacionQuemaGasRepository autorizacionQuemaGasRepository)
        {
            this._mapper = mapper;
            this._contextAccessor = contextAccessor;
            this._autorizacionQuemaGasRepository = autorizacionQuemaGasRepository;
        }

        public async Task<StatusResponse<long>> GuardarInforme(InformeJustificacionDto request)
        {
            try
            {
                var response = await _autorizacionQuemaGasRepository.GuardarInforme(_mapper.Map<SP_INSERTAR_INFORME_JUSTIFICACION_Request_Entity>(request));
                long prevMotivoId = -1;

                foreach (List<MotivoInformeDto> grupo in request.motivos)
                {
                    prevMotivoId = -1;

                    foreach (MotivoInformeDto motivo in grupo)
                    {
                        if (prevMotivoId != -1) motivo.motivoPadreId = prevMotivoId;

                        if (motivo.informeId == 0) motivo.informeId = response;

                        prevMotivoId = await _autorizacionQuemaGasRepository.InsertarMotivoInforme(_mapper.Map<SP_INSERTAR_MOTIVOS_INFORME_Request_Entity>(motivo));
                    }
                }

                if (request.facilidades != null)
                {
                    foreach (KeyValuePair<long, List<FacilidadDto>> pair in request.facilidades)
                    {
                        foreach (FacilidadDto facilidad in pair.Value)
                        {
                            if (facilidad.informeId == 0) facilidad.informeId = response;

                            if (facilidad.usuarioId == 0) facilidad.usuarioId = request.usuarioId;

                            if (facilidad.infoFacilidadId == 0)
                            {
                                var facilidadGuardada = await _autorizacionQuemaGasRepository.ObtenerFacilidadPorNombre(response, facilidad.descripcion);

                                if (facilidadGuardada != null) facilidad.infoFacilidadId = facilidadGuardada.infoFacilidadId;
                            }

                            facilidad.infoMotivoFacilidadId = await _autorizacionQuemaGasRepository.GuardarFacilidad(_mapper.Map<SP_INSERTAR_FACILIDAD_Request_Entity>(facilidad));
                        }
                    }
                }

                if (request.quemadores != null)
                {
                    foreach (QuemadorDto quemador in request.quemadores)
                    {
                        if (quemador.informeId == 0) quemador.informeId = response;
                        
                        if (quemador.usuarioId == 0) quemador.usuarioId = request.usuarioId;

                        await _autorizacionQuemaGasRepository.GuardarQuemador(_mapper.Map<SP_INSERTAR_QUEMADOR_Request_Entity>(quemador));
                    }
                }

                if (request.balance != null)
                {
                    foreach (BalanceDto balance in request.balance)
                    {
                        if (balance.informeId == 0) balance.informeId = response;

                        if (balance.usuarioId == 0) balance.usuarioId = request.usuarioId;

                        await _autorizacionQuemaGasRepository.GuardarBalance(_mapper.Map<SP_INSERTAR_BALANCE_Request_Entity>(balance));
                    }
                }

                if (request.acciones != null)
                {
                    foreach (AccionDto accion in request.acciones)
                    {
                        if (accion.informeId == 0) accion.informeId = response;

                        if (accion.usuarioId == 0) accion.usuarioId = request.usuarioId;

                        await _autorizacionQuemaGasRepository.GuardarAccion(_mapper.Map<SP_INSERTAR_ACCION_Request_Entity>(accion));
                    }
                }

                if (request.objetivos != null)
                {
                    foreach (AccionDto accion in request.objetivos)
                    {
                        if (accion.informeId == 0) accion.informeId = response;

                        if (accion.usuarioId == 0) accion.usuarioId = request.usuarioId;

                        await _autorizacionQuemaGasRepository.GuardarAccion(_mapper.Map<SP_INSERTAR_ACCION_Request_Entity>(accion));
                    }
                }

                if (request.cronograma != null)
                {
                    foreach (KeyValuePair<string, List<CronogramaDto>> pair in request.cronograma)
                    {
                        var infoMotivoFacilidadIdStr = pair.Key;
                        long infoMotivoFacilidadId = 0;

                        if (infoMotivoFacilidadIdStr.Contains('-'))
                        {
                            var p = infoMotivoFacilidadIdStr.Split('-');
                            var facilidad = request.facilidades[long.Parse(p[0])][int.Parse(p[1])];
                            infoMotivoFacilidadId = facilidad.infoMotivoFacilidadId;
                        }
                        else infoMotivoFacilidadId = long.Parse(infoMotivoFacilidadIdStr);

                        foreach (CronogramaDto cronograma in pair.Value)
                        {
                            if (cronograma.informeId == 0) cronograma.informeId = response;

                            if (cronograma.usuarioId == 0) cronograma.usuarioId = request.usuarioId;

                            cronograma.infoMotivoFacilidadId = infoMotivoFacilidadId;
                            await _autorizacionQuemaGasRepository.GuardarCronograma(_mapper.Map<SP_INSERTAR_CRONOGRAMA_Request_Entity>(cronograma));
                        }
                    }
                }

                if (request.adjuntos != null)
                {
                    var adjuntos = request.adjuntos.Values.SelectMany(x => x).ToList();

                    foreach (var adjunto in adjuntos)
                    {
                        if (adjunto.informeId == 0) adjunto.informeId = response;

                        if (adjunto.usuarioId == 0) adjunto.usuarioId = request.usuarioId;

                        await _autorizacionQuemaGasRepository.GuardarAdjunto(_mapper.Map<SP_INSERTAR_ADJUNTO_INFORME_Request_Entity>(adjunto));
                    }
                }

                return Message.Successful(response);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<InformeJustificacionDto>> ObtenerInforme(long solicitudId)
        {
            try
            {
                var informe = _mapper.Map<InformeJustificacionDto>(await _autorizacionQuemaGasRepository.ObtenerInforme(solicitudId));
                
                var motivos = _mapper.Map<List<MotivoInformeDto>>(await _autorizacionQuemaGasRepository.ObtenerMotivosInforme(informe.informeId));
                var motivosPadres = motivos.Where(motivo => motivo.motivoPadreId == null).ToList();

                var adjuntos = _mapper.Map<List<AdjuntoInformeDto>>(await _autorizacionQuemaGasRepository.ObtenerAdjuntos(informe.informeId));
                var cronograma = _mapper.Map<List<CronogramaDto>>(await _autorizacionQuemaGasRepository.ObtenerCronograma(informe.informeId));
                var acciones = _mapper.Map<List<AccionDto>>(await _autorizacionQuemaGasRepository.ObtenerAcciones(informe.informeId));

                if (motivos.Count > 0)
                {
                    informe.motivos = new List<List<MotivoInformeDto>>();
                    informe.facilidades = new Dictionary<long, List<FacilidadDto>>();
                    informe.cronograma = new Dictionary<string, List<CronogramaDto>>();
                }

                if (adjuntos.Count > 0) informe.adjuntos = adjuntos.GroupBy(x => x.seccion).ToDictionary(g => g.Key, g => g.ToList());

                long prevMotivoId = -1;

                foreach (var padre in motivosPadres)
                {
                    var grupo = new List<MotivoInformeDto>();
                    grupo.Add(padre);

                    prevMotivoId = padre.infoMotivoId;

                    while (prevMotivoId != -1)
                    {
                        var nextMotivo = motivos.Find(motivo => motivo.motivoPadreId == prevMotivoId);

                        if (nextMotivo != null)
                        {
                            grupo.Add(nextMotivo);
                            prevMotivoId = nextMotivo.infoMotivoId;
                        } else
                        {
                            prevMotivoId = -1;
                        }
                    }

                    informe.motivos.Add(grupo);

                    var motivo = grupo.LastOrDefault();

                    if (motivo != null)
                    {
                        var facilidades = _mapper.Map<List<FacilidadDto>>(await _autorizacionQuemaGasRepository.ObtenerFacilidades(informe.informeId, motivo.infoMotivoId));
                        informe.facilidades?.Add(motivo.infoMotivoId, facilidades);

                        foreach (FacilidadDto facilidad in facilidades)
                        {
                            var key = facilidad.infoMotivoFacilidadId.ToString();
                            var list = cronograma.Where(c => c.infoMotivoFacilidadId == facilidad.infoMotivoFacilidadId).ToList();

                            if (informe.cronograma.ContainsKey(key)) informe.cronograma[key].AddRange(list);
                            else informe.cronograma[key] = list;
                        }
                    }
                }

                informe.quemadores = _mapper.Map<List<QuemadorDto>>(await _autorizacionQuemaGasRepository.ObtenerQuemadores(informe.informeId));
                informe.balance = _mapper.Map<List<BalanceDto>>(await _autorizacionQuemaGasRepository.ObtenerBalance(informe.informeId));
                informe.acciones = acciones.Where(accion => accion.esObjetivo == 0).ToList();
                informe.objetivos = acciones.Where(accion => accion.esObjetivo == 1).ToList();

                return Message.Successful(informe);
            }
            catch (Exception ex)
            {
                return Message.Exception<InformeJustificacionDto>(ex);
            }
        }

        public async Task<StatusResponse<List<LoteDto>>> ObtenerLotes()
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                string personaId = principal.Claims.FirstOrDefault(c => c.Type == "personaId").Value;

                var response = await _autorizacionQuemaGasRepository.ObtenerLotes(long.Parse(personaId));
                return Message.Successful(_mapper.Map<List<LoteDto>>(response));
            }
            catch (Exception ex)
            {
                return Message.Exception<List<LoteDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<MotivoDto>>> ObtenerMotivos()
        {
            try
            {
                var response = await _autorizacionQuemaGasRepository.ObtenerMotivos();
                return Message.Successful(_mapper.Map<List<MotivoDto>>(response));
            }
            catch (Exception ex)
            {
                return Message.Exception<List<MotivoDto>>(ex);
            }
        }

        public async Task<StatusResponse<QuemadorDto?>> ObtenerQuemadorPorSerie(long usuarioId, string serie, string nombre)
        {
            try
            {
                var response = await _autorizacionQuemaGasRepository.ObtenerQuemadorPorSerie(usuarioId, serie, nombre);
                var quemador = response == null ? null : _mapper.Map<QuemadorDto>(response);
                return Message.Successful(quemador);
            }
            catch (Exception ex)
            {
                return Message.Exception<QuemadorDto?> (ex);
            }
        }

        public async Task<StatusResponse<List<ObservacionCapituloDto>>> ObtenerCantidadObservaciones(long informeId)
        {
            try
            {
                var response = await _autorizacionQuemaGasRepository.ObtenerCantidadObservaciones(informeId);
                return Message.Successful(_mapper.Map<List<ObservacionCapituloDto>>(response));
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObservacionCapituloDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<ObservacionPorProyectoCapituloResponseDto>>> ObtenerProyectosObservacionCapitulo(long idInforme, string capitulo)
        {
            try
            {
                var resultado = await _autorizacionQuemaGasRepository.ObtenerProyectosObservacionCapitulo(idInforme, capitulo);
                return Message.Successful(resultado);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObservacionPorProyectoCapituloResponseDto>>(ex);
            }
        }
    }
}

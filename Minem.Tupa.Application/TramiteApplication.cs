using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.AutorizacionQuemaGas;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Svc.PagoTupa;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.Entity;
using Minem.Tupa.Entity.Tramite;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Proxy.Implementation;
using Minem.Tupa.Proxy.Interface;
using Minem.Tupa.Utils;
using System.Linq.Expressions;
using System.Security.Claims;
using static Minem.Tupa.Utils.Constante;
using static Minem.Tupa.Utils.Enumeration;
using SVC_NOTIFICACION = Minem.Tupa.Dto.Svc.Notificacion;

namespace Minem.Tupa.Application
{
    public class TramiteApplication : ITramiteApplication
    {
        private readonly IMapper _mapper;
        private readonly ITramiteRepository _tramiteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IFormularioRepository _formularioRepository;
        private readonly IEmailApplication _emailApplication;
        private readonly IGeneralRepository _generalRepository;
        private readonly IConfiguration _configuration;
        private readonly ITupaRepository _tupaRepository;
        private readonly INotificacionService _notificacionService;
        private readonly IPagoTupaService _pagoTupaService;
        private readonly IAutorizacionQuemaGasRepository _autorizacionQuemaGasRepository;

        public TramiteApplication(ITramiteRepository tramiteRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IFormularioRepository formularioRepository,
             IEmailApplication emailApplication, IGeneralRepository generalRepository, IConfiguration configuration, ITupaRepository tupaRepository, INotificacionService notificacionService,
             IPagoTupaService pagoTupaService, IAutorizacionQuemaGasRepository autorizacionQuemaGasRepository)
        {
            _mapper = mapper;
            _tramiteRepository = tramiteRepository;
            _contextAccessor = contextAccessor;
            _formularioRepository = formularioRepository;
            _formularioRepository = formularioRepository;
            _emailApplication = emailApplication;
            _generalRepository = generalRepository;
            _configuration = configuration;
            _tupaRepository = tupaRepository;
            _notificacionService = notificacionService;
            _pagoTupaService = pagoTupaService;
            _autorizacionQuemaGasRepository = autorizacionQuemaGasRepository;
        }



        public async Task<StatusResponse<long>> GenerarTramite(GenerarTramiteRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                string personaId = principal.Claims.FirstOrDefault(c => c.Type == "personaId").Value;
                string userId = principal.Claims.FirstOrDefault(c => c.Type == "userid").Value;

                var solicitudId = await _tramiteRepository.InsertarSolicitud(request.TupaId, Convert.ToInt32(personaId), Convert.ToInt32(userId), 2);
                var solicitud = await _tupaRepository.ObtenerSolicitudPorCodigo((int)solicitudId);

                var requisitos = await _tramiteRepository.ObtenerTramiteRequisitos(new SP_OBTENER_TRAMITE_Request_Entity { IdTramite = (int)solicitudId, CodigoTupa = solicitud.CodMaeTupa.ToString() });

                foreach (var item in requisitos)
                {
                    await _tramiteRepository.InsertarDetalleSolicitud(
                        (int)solicitudId,
                        item.RequisitoId,
                        int.Parse(personaId),
                        0,
                        string.Empty,
                        string.Empty,
                        0,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        int.Parse(userId),
                        string.Empty,
                        string.Empty,
                        0,
                        0
                    );
                }

                return Message.Successful(solicitudId);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }

        }

        public async Task<StatusResponse<long>> ActualizarPadreITS(long codMaeSolicitud, long codMaeSolicitudPadre)

        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);

                var solicitud = await _tramiteRepository.ActualizarPadreITS(codMaeSolicitud, codMaeSolicitudPadre);

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }

        }

        public async Task<StatusResponse<List<ObtenerNotasResponseDto>>> ObtenerNotas(ObtenerNotasRequestDto request)
        {
            try
            {
                var respuesta = _mapper.Map<List<ObtenerNotasResponseDto>>(
                    await _tramiteRepository.ObtenerNotas(_mapper.Map<SP_OBTENER_NOTAS_Request_Entity>(request)));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObtenerNotasResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<ObtenerDocumentosResponseDto>>> ObtenerDocumentos(ObtenerDocumentosRequestDto request)
        {
            try
            {
                var respuesta = _mapper.Map<List<ObtenerDocumentosResponseDto>>(
                    await _tramiteRepository.ObtenerDocumentos(_mapper.Map<SP_OBTENER_TRAMITE_DOCS_Request_Entity>(request)));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObtenerDocumentosResponseDto>>(ex);
            }

        }

        public async Task<StatusResponse<ObtenerTramiteResponseDto>> ObtenerTramite(ObtenerTramiteRequestDto request)
        {
            try
            {
                var respuesta = _mapper.Map<ObtenerTramiteResponseDto>(
                    await _tramiteRepository.ObtenerTramite(_mapper.Map<SP_OBTENER_TRAMITE_Request_Entity>(request)));

                respuesta.Requisitos = _mapper.Map<List<TramiteRequisito>>(await _tramiteRepository.ObtenerTramiteRequisitos(_mapper.Map<SP_OBTENER_TRAMITE_Request_Entity>(request)));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<ObtenerTramiteResponseDto>(ex);
            }
        }
        public async Task<StatusResponse<ObtenerTramiteResponseDto>> EnviarSolicitud(EnviarSolicitudRequestDto request)
        {
            try
            {
                var result = false;
                string mensajeEmail = "", asuntoEmail = "";
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                var claims = principal.Claims.FirstOrDefault();
                var usuario = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == "userid").Value);
                var nroDoc = principal.Claims.FirstOrDefault(c => c.Type == "nroDoc").Value;
                var tipoPersona = principal.Claims.FirstOrDefault(c => c.Type == "tipoPersona").Value;
                var tipoDoc = principal.Claims.FirstOrDefault(c => c.Type == "tipoDoc").Value;
                var idCliente = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == "idCliente").Value);

                var cntRequisitosPendientes = 0;
                var solicitud = await _tramiteRepository.ObtenerDatosSolicitud(request.IdSolicitud);

                if (solicitud.CodIdMaeTupa == 182)
                {
                    var informe = _mapper.Map<InformeJustificacionDto>(await _autorizacionQuemaGasRepository.ObtenerInforme(request.IdSolicitud));
                    await _autorizacionQuemaGasRepository.GuardarInformeHistorico(informe.informeId, usuario);

                    cntRequisitosPendientes = await _tramiteRepository.ObtenerCantidadRequisitosPendientes(request.IdSolicitud);
                }

                await _tramiteRepository.ActualizarEstadoRequisitosParaRevision(request.IdSolicitud, 9);

                var estadoEnviado = 2;
                switch (solicitud.CodMaeEstado)
                {
                    case (int)TransactionStates.PENDIENTE:
                        //estadoEnviado = 4;
                        estadoEnviado = request.CodMaeEstado;
                        #region Asignar Dias validos para Admisibilidad
                        await _tramiteRepository.ActualizarFechaFinAdmisibilidad(solicitud.CodIdMaeTupa, solicitud.CodMaeSolicitud);
                        #endregion
                        break;
                    case 4:
                        estadoEnviado = 4;
                        break;
                    case 5:
                        estadoEnviado = 4;
                        break;
                    case 48:
                        estadoEnviado = 4;
                        break;
                }

                //Asignamos a Funcionario
                long idTumovTupAsignacion = await _tramiteRepository.AsignarSolicitudAlFuncionario(solicitud.CodMaeSolicitud, estadoEnviado, usuario, usuario, request.NroExpediente);

                #region "Actualizamos en Funcionario: Estado / Situacion / Etapa"                
                RegistrarAdmisibilidadFuncionarioDto oRegistrarAdmisibilidadFuncionarioDto = new RegistrarAdmisibilidadFuncionarioDto();
                oRegistrarAdmisibilidadFuncionarioDto.CodMovtupasignacion = (int)idTumovTupAsignacion;
                oRegistrarAdmisibilidadFuncionarioDto.CodMaeEstado = request.CodMaeEstado;
                oRegistrarAdmisibilidadFuncionarioDto.CodSituacion = request.CodSituacion;
                oRegistrarAdmisibilidadFuncionarioDto.CodEtapa = request.CodEtapa;
                var resultado = await _tramiteRepository.ActualizarSituacionAdmisibilidadFuncionario(oRegistrarAdmisibilidadFuncionarioDto);
                #endregion

                #region "Registramos en la tabla T_TUPADIGITAL_TUMOVASIGNACION_SITUACION: Situacion / Etapa"

                long idTupaAsignacionSituacion = await _tramiteRepository.InsertarCambioSituacionDeLaAsignacion(idTumovTupAsignacion);

                #endregion

                foreach (var item in request.Documentos)
                {
                    await _tramiteRepository.ActualizarArchivoAdjunto(request.IdSolicitud, item.IdRequisito, item.CodigoGenerado, item.NombreDocumento);
                }

                //var numeroSolicitud = (int)new Random().Next(1, 99999999);//Actualizo al tramite el numero de expediente

                var respuesta = _mapper.Map<ObtenerFormularioDiaResponseDto>(
                   await _formularioRepository.ObtenerFormularioDia(solicitud.CodMaeSolicitud));

                if (string.IsNullOrEmpty(respuesta.DataJson))
                {
                    respuesta.DataJson = Constante.JsonData;
                }

                #region "Registrar Pago TUPA"
                var lstPagoSolicitud = await _tramiteRepository.ObtenerDatoPagoPorIdSolicitud(solicitud.CodMaeSolicitud);
                if (lstPagoSolicitud != null && lstPagoSolicitud.Count > 0)
                {
                    var pagoSolicitud = lstPagoSolicitud.First();
                    if (pagoSolicitud.ID_TIPO_PAGO == Constante.TipoPagoTupa.CAJA_MINEM)
                    {
                        ValidarPagoCajaMinemRequestDto objValidarPagoCajaMinemRequest = new ValidarPagoCajaMinemRequestDto();
                        objValidarPagoCajaMinemRequest.CodigoCaja = pagoSolicitud.CODIGO_CAJA;
                        objValidarPagoCajaMinemRequest.FechaPago = pagoSolicitud.FECHA_PAGO;
                        objValidarPagoCajaMinemRequest.Importe = pagoSolicitud.IMPORTE;
                        objValidarPagoCajaMinemRequest.Expediente = request.NroExpediente.HasValue ? request.NroExpediente.Value : 0;

                        var responseValidar = await _pagoTupaService.ValidarPagoCajaMinem(objValidarPagoCajaMinemRequest);
                        if (responseValidar.Success)
                        {
                            RegistrarPagoCajaMinemRequestDto objRegistrarPago = new RegistrarPagoCajaMinemRequestDto();
                            objRegistrarPago.CodigoCaja = pagoSolicitud.CODIGO_CAJA;
                            objRegistrarPago.FechaPago= pagoSolicitud.FECHA_PAGO;
                            objRegistrarPago.Importe = pagoSolicitud.IMPORTE;
                            objRegistrarPago.Expediente = 0;
                            objRegistrarPago.IdSistema = 0;
                            objRegistrarPago.IdArchivoLaserfiche = 0;

                            var responseRegistrar= await _pagoTupaService.RegistrarPagoCajaMinem(objRegistrarPago);

                            if (responseRegistrar.Success)
                            {
                                AsignarExpedientePagoRequestDto objAsignarExpedientePago = new AsignarExpedientePagoRequestDto();
                                objAsignarExpedientePago.IdPago = responseRegistrar.Data;
                                objAsignarExpedientePago.IdSistema = Notificacion.Sistema.TUPA_DIGITAL;
                                objAsignarExpedientePago.Expediente = objValidarPagoCajaMinemRequest.Expediente;
                                await _pagoTupaService.PagoCajaMinemAsignarExpediente(objAsignarExpedientePago);
                            }
                        }
                    }
                    else if (pagoSolicitud.ID_TIPO_PAGO == Constante.TipoPagoTupa.PAGALO_PE)
                    {
                        ValidarPagoPagaloPeRequestDto objValidarPagoPagaloPeRequest = new ValidarPagoPagaloPeRequestDto();
                        objValidarPagoPagaloPeRequest.NroSecuencia = pagoSolicitud.NRO_SECUENCIA;
                        objValidarPagoPagaloPeRequest.FechaMovimiento = pagoSolicitud.FECHA_PAGO;
                        objValidarPagoPagaloPeRequest.CodigoOficina = pagoSolicitud.CODIGO_OFICINA;
                        //objValidarPagoPagaloPeRequest.Expediente = request.NroExpediente.HasValue ? request.NroExpediente.Value : 0;
                        var responseValidar = await _pagoTupaService.ValidarPagoPagaloPE(objValidarPagoPagaloPeRequest);
                        if (responseValidar.Success)
                        {
                            AsignarExpedientePagoRequestDto objAsignarExpedientePago = new AsignarExpedientePagoRequestDto();
                            objAsignarExpedientePago.IdPago = responseValidar.Data;
                            objAsignarExpedientePago.IdSistema = Notificacion.Sistema.TUPA_DIGITAL;
                            objAsignarExpedientePago.Expediente = request.NroExpediente.HasValue ? request.NroExpediente.Value : 0;
                            await _pagoTupaService.PagoPagaloPEAsignarExpediente(objAsignarExpedientePago);
                        }
                    }

                }

                #endregion

                var p_usuario = usuario.ToString();
                var p_estado = 4;
                var p_notificado = 0;
                var dataFormulario = await _tramiteRepository.InsertarDataFormulario(respuesta.Id, respuesta.DataJson, solicitud.CodMaeSolicitud, p_usuario, p_estado, p_notificado);

                long idSolicitudExpediente = await _tramiteRepository.ActualizarSolicitud(estadoEnviado, solicitud.CodMaeSolicitud, request.NroExpediente, idCliente);

                if (idSolicitudExpediente == 0)
                    throw new Exception("La solicitud no fue actualizada. Se generó un error.");

                long idExpedienteTrazabilidad = await _tramiteRepository.ActualizarExpedienteTrazabilidad(idTumovTupAsignacion, idSolicitudExpediente);

                //Indicador: 1:casilla 2:Correo
                //destinoCorreo: 1:casilla 2:Correo
                //tipo: 1: inicio tramite ...

                var datosPersona = await _generalRepository.GetPersonaPorCodMovUsuario(usuario);
                if (!string.IsNullOrWhiteSpace(datosPersona.CorrElectronico))
                {
                    var correoRemitente = _configuration.GetSection("ConfigurationSmtp:SmtpFromAddress").Value.ToString();
                    var correoDestinatario = datosPersona.CorrElectronico;

                    var mensajeAdicional = string.Empty;

                    if (cntRequisitosPendientes > 0)
                    {
                        mensajeAdicional = @"<tr><td><br><strong>En caso no haya presentado información de acuerdo a lo estipulado en los artículos 189, 235, 244, 246 del Decreto "
                                    + "Supremo N° 032-2004-EM, su solicitud podrá ser observada</strong><br></td>"
                                    + "</tr>";
                    }

                    var objMensajeAdministrado = Email.ObtenerMensajeAsunto(2, 2, 1, request.NroExpediente.ToString(), datosPersona.NomCompleto, "", mensajeAdicional);
                    mensajeEmail = objMensajeAdministrado.MensajeCorreo;
                    asuntoEmail = objMensajeAdministrado.asunto;

                    result = await _emailApplication.SendMail(correoRemitente, correoDestinatario, asuntoEmail, mensajeEmail, true, false, null);
                }

                var datosPersonaUnidadOrganica = await _generalRepository.GetPersonaOrganicaTupa(solicitud.CodIdMaeTupa);

                if (datosPersonaUnidadOrganica != null)
                {
                    foreach (var itemPersonaUniOrg in datosPersonaUnidadOrganica)
                    {
                        if (!string.IsNullOrWhiteSpace(itemPersonaUniOrg.CorrElectronico))
                        {
                            var correoRemitente = _configuration.GetSection("ConfigurationSmtp:SmtpFromAddress").Value.ToString();
                            var correoDestinatario = itemPersonaUniOrg.CorrElectronico;

                            var objMensajeAsuntoDirectores = Email.ObtenerMensajeAsunto(1, 0, 1, request.NroExpediente.ToString(), itemPersonaUniOrg.NomCompleto, "");
                            mensajeEmail = objMensajeAsuntoDirectores.MensajeCorreo;
                            asuntoEmail = objMensajeAsuntoDirectores.asunto;

                            result = await _emailApplication.SendMail(correoRemitente, correoDestinatario, asuntoEmail, mensajeEmail, true, false, null);

                        }

                    }
                }

                var respuestatupa = _mapper.Map<TupaDto>(await _tupaRepository.ObtenerTupaPorCodigo(solicitud.CodMaeTupa));
                var datosPersonaUnidadRol = await _generalRepository.GetPersonaOrganicaRol(solicitud.CodIdMaeTupa, respuestatupa.CodMaeUniOrganica);

                if (datosPersonaUnidadRol != null)
                {
                    foreach (var itemPersonaUniRol in datosPersonaUnidadRol)
                    {
                        if (!string.IsNullOrWhiteSpace(itemPersonaUniRol.CorrElectronico))
                        {
                            var correoRemitente = _configuration.GetSection("ConfigurationSmtp:SmtpFromAddress").Value.ToString();
                            var correoDestinatario = itemPersonaUniRol.CorrElectronico;

                            var objMensajeAsuntoAdm = Email.ObtenerMensajeAsunto(1, 0, 1, request.NroExpediente.ToString(), itemPersonaUniRol.NomCompleto, "");
                            mensajeEmail = objMensajeAsuntoAdm.MensajeCorreo;
                            asuntoEmail = objMensajeAsuntoAdm.asunto;

                            result = await _emailApplication.SendMail(correoRemitente, correoDestinatario, asuntoEmail, mensajeEmail, true, false, null);

                        }
                    }
                }

                int categoria = 0;
                var objMensajeAsunto = Email.ObtenerMensajeAsunto(2, 1, 1, request.NroExpediente.ToString(), datosPersona.NomCompleto, null);
                mensajeEmail = objMensajeAsunto.MensajeCorreo;
                asuntoEmail = objMensajeAsunto.asunto;
                categoria = objMensajeAsunto.Categoria;

                List<int> lstNotificacionAttaches = new List<int>(Array.Empty<int>());

                SVC_NOTIFICACION.NotificationInternaRequestDto notificacion = new SVC_NOTIFICACION.NotificationInternaRequestDto();
                notificacion.CodMaeCategoria = categoria;
                notificacion.notificationAttaches = lstNotificacionAttaches;
                notificacion.Asunto = asuntoEmail;
                notificacion.Mensaje = mensajeEmail.Replace("\"", "'");
                notificacion.SelloTiempo = false;
                notificacion.NumDocumento = nroDoc;
                notificacion.CodTipoDocumento = tipoDoc;
                notificacion.CodTipoPersona = tipoPersona; ;
                notificacion.NumDocAdicional = "";// item.NumDocAdicional;              
                notificacion.IdSolicitud = solicitud.CodMaeSolicitud;
                notificacion.Expediente = request.NroExpediente.Value;
                notificacion.IdSistema = Notificacion.Sistema.TUPA_DIGITAL;

                //Funciones.WriteLog("Respuesta Notificacion :::>" + notificacion.Mensaje + '-' + notificacion.NumDocumento);
                await _notificacionService.RegistrarNotificacionInterna(notificacion);

                return Message.Successful(new ObtenerTramiteResponseDto()
                {
                    NumExpediente = request.NroExpediente.ToString(),
                    ClaveAcceso = "XXXX"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public async Task<StatusResponse<List<ObtenerMisTramitesResponseDto>>> ObtenerMisTramites()
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                var usuario = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == "userid").Value);

                var respuesta = _mapper.Map<List<ObtenerMisTramitesResponseDto>>(await _tramiteRepository.ObtenerMisTramites(usuario, -1));
                var respuestaOrdenada = respuesta
                    //.OrderByDescending(x => x.IdSTD)
                    .ToList();
                return Message.Successful(respuestaOrdenada);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObtenerMisTramitesResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<ObtenerDiaAprobadoResponseDto>>> ObtenerDIAAprobado()
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                var usuario = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == "userid").Value);

                var respuesta = _mapper.Map<List<ObtenerDiaAprobadoResponseDto>>(await _tramiteRepository.ObtenerDIAAprobado(usuario, -1));
                var respuestaOrdenada = respuesta
                    //.OrderByDescending(x => x.IdSTD)
                    .ToList();
                return Message.Successful(respuestaOrdenada);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObtenerDiaAprobadoResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<long>> GenerarDocumentoAdicional(GenerarDocumentoAdicionalRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                var claims = principal.Claims.FirstOrDefault();
                var usuario = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == "userid").Value);

                var solicitud = await _tramiteRepository.GenerarDocumentoAdicional(request.IdTramite, request.NombreArchivo, request.NumeroDocumentoRespuesta, request.TipoTramite, request.TipoDocumento, request.NumeroDocumento, request.Descripcion, usuario);

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }

        }

        public async Task<StatusResponse<List<ObtenerDocumentoAdicionalResponseDto>>> ObtenerDocumentosAdicionales(long idTramite)
        {
            try
            {

                var respuesta = _mapper.Map<List<ObtenerDocumentoAdicionalResponseDto>>(
                    await _tramiteRepository.ObtenerDocumentosAdicionales(idTramite));


                foreach (var item in respuesta)
                {

                    var detallesDocumentos = await _tramiteRepository.ObtenerDocumentosAdicionalesDetalle(item.idTramite);


                    var documentos = detallesDocumentos
                     .Select(d => new Documentos
                     {
                         TramiteDocId = (int)d.tramitedocId,
                         TramiteId = (int)d.tramiteId,
                         Descripcion = d.descripcion
                     })
                     .ToList();


                    item.Documentos = documentos;
                }


                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObtenerDocumentoAdicionalResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<long>> EliminarDocumentoAdicional(long request)
        {
            try
            {
                var solicitud = await _tramiteRepository.EliminarDocumentoAdicional(request);

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> ActualizarIdEstudio(ActualizarEstudioRequestDto request)
        {
            try
            {
                var solicitud = await _tramiteRepository.ActualizarIdEstudio(request.CodMaeSolicitud, request.IdEstudio);
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> AnularTramite(AnularTramiteRequestDto request)
        {
            try
            {
                var solicitud = await _tramiteRepository.AnularTramite(request.codMaeSolicitud);
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }
        public async Task<StatusResponse<List<ObtenerTipoComunicacionResponseDto>>> ObtenerTipoComunicacion()
        {
            try
            {
                var respuesta = _mapper.Map<List<ObtenerTipoComunicacionResponseDto>>(await _tramiteRepository.ObtenerTipoComunicacion(1));
                var respuestaOrdenada = respuesta.ToList();

                return Message.Successful(respuestaOrdenada);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObtenerTipoComunicacionResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<ObtenerTipoDocumentoResponseDto>>> ObtenerTipoDocumento()
        {
            try
            {
                var respuesta = _mapper.Map<List<ObtenerTipoDocumentoResponseDto>>(await _tramiteRepository.ObtenerTipoDocumento("00004"));
                var respuestaOrdenada = respuesta.ToList();

                return Message.Successful(respuestaOrdenada);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObtenerTipoDocumentoResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<long>> RegistrarNombreProyecto(RegistrarNombreProyectoRequestDto request)
        {
            try
            {
                var solicitud = await _tramiteRepository.RegistrarNombreProyecto(request.idTramite, request.nombreProyecto);

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }

        }

        public async Task<StatusResponse<long>> ValidarNombreProyecto(long idTramite, string nombreProyecto)
        {
            try
            {
                var solicitud = await _tramiteRepository.ValidarNombreProyecto(idTramite, nombreProyecto);

                return Message.Successful(solicitud);

            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }

        }

        public async Task<StatusResponse<ObtenerTramiteResponseDto>> EnviarTramiteAdicional(EnviarTramiteAdicionalRequestDto request)
        {
            try
            {

                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                var claims = principal.Claims.FirstOrDefault();
                var usuario = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == "userid").Value);

                var solicitud = await _tramiteRepository.ObtenerDatosSolicitud(request.IdSolicitud);

                /*var requisitos = await _tramiteRepository.ObtenerTramiteRequisitos(new SP_OBTENER_TRAMITE_Request_Entity { IdTramite = request.IdSolicitud, CodigoTupa = solicitud.CodMaeTupa.Trim() });

                foreach (var item in requisitos)
                {
                    await _tramiteRepository.InsertarDetalleSolicitud(request.IdSolicitud, item.RequisitoId, solicitud.CodMovPersona, 0, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, usuario, string.Empty, string.Empty, 0, 0);
                }*/

                var estadoEnviado = 0;
                switch (request.TipoComunicacion)
                {
                    case 1:
                        estadoEnviado = 49;
                        break;
                    case 2:
                        estadoEnviado = 4;
                        break;
                    case 3:
                        estadoEnviado = 4;
                        break;
                    case 4:
                        estadoEnviado = 4;
                        break;
                    case 5:
                        estadoEnviado = 4;
                        break;
                }


                //Asignamos a Funcionario
                await _tramiteRepository.AsignarSolicitudAlFuncionario(solicitud.CodMaeSolicitud, estadoEnviado, usuario, usuario, request.NroExpediente);


                foreach (var item in request.Documentos)
                {
                    await _tramiteRepository.ActualizarArchivoAdjunto(request.IdSolicitud, item.IdRequisito, item.CodigoGenerado, item.NombreDocumento);
                }

                //var numeroSolicitud = (int)new Random().Next(1, 99999999);//Actualizo al tramite el numero de expediente

                var respuesta = _mapper.Map<ObtenerFormularioDiaResponseDto>(
                   await _formularioRepository.ObtenerFormularioDia(solicitud.CodMaeSolicitud));

                if (string.IsNullOrEmpty(respuesta.DataJson))
                {
                    respuesta.DataJson = Constante.JsonData;
                }

                var p_usuario = usuario.ToString();
                var p_estado = 4;
                var p_notificado = 0;
                var dataFormulario = await _tramiteRepository.InsertarDataFormulario(respuesta.Id, respuesta.DataJson, solicitud.CodMaeSolicitud, p_usuario, p_estado, p_notificado);

                long actualizacionSolicitud = await _tramiteRepository.ActualizarTramiteAdicional(estadoEnviado, solicitud.CodMaeSolicitud, request.NroExpediente, request.TipoComunicacion);
                if (actualizacionSolicitud == 0)
                    throw new Exception("La solicitud no fue actualizada. Se generó un error.");

                return Message.Successful(new ObtenerTramiteResponseDto()
                {
                    NumExpediente = request.NroExpediente.ToString(),
                    ClaveAcceso = "XXXX"
                });


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public async Task<StatusResponse<RegistrarEstudioResponseDto>> RegistrarEstudio(RegistrarEstudioRequestDto request)
        {
            try
            {
                var solicitud = _mapper.Map<RegistrarEstudioResponseDto>(await _tramiteRepository.RegistrarEstudio(request.IdCliente));

                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<RegistrarEstudioResponseDto>(ex);
            }
        }

        public async Task<StatusResponse<List<TrazabilidadAsignacionReponseDto>>> VerTrazabilidad(long codMaeSolicitud)
        {
            try
            {
                var solicitud = _mapper.Map<List<TrazabilidadAsignacionReponseDto>>(await _tramiteRepository.VerTrazabilidad(codMaeSolicitud));
                return Message.Successful(solicitud);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<TrazabilidadAsignacionReponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<SituacionSolicitudFinalResponseDto>> ObtenerSituacionSolicitudFinal(int codMaeSolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<SituacionSolicitudFinalResponseDto>(await _tramiteRepository.ObtenerSituacionSolicitudFinal(codMaeSolicitud));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<SituacionSolicitudFinalResponseDto>(ex);
            }
        }

        public async Task<StatusResponse<long>> ActualizarSituacionAdmisibilidadTumaesolicitud(int codMaeSolicitud, int codSituacion, int codEtapa)
        {
            try
            {
                var respuesta = await _tramiteRepository.ActualizarSituacionAdmisibilidadTumaesolicitud(codMaeSolicitud, codSituacion, codEtapa);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> ActualizarSituacionAdmisibilidadFuncionario(RegistrarAdmisibilidadFuncionarioDto request)
        {
            try
            {
                var respuesta = await _tramiteRepository.ActualizarSituacionAdmisibilidadFuncionario(request);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> ActualizarArchivoRequisito2(int codMaeSolicitud, int codMaeRequisito, int codMaeEstado, int idArchivo, string nomArchivo, string extArchivo)
        {
            try
            {
                var respuesta = await _tramiteRepository.ActualizarArchivoRequisito2(codMaeSolicitud, idArchivo, nomArchivo, extArchivo);
                await _tramiteRepository.ActualizarDetalleSolicitud(codMaeSolicitud, codMaeRequisito, codMaeEstado);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<List<ArchivoRequisito2ResponseDto>>> ObtenerArchivoRequisito2(int codMaeSolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<List<ArchivoRequisito2ResponseDto>>(await _tramiteRepository.ObtenerArchivoRequisito2(codMaeSolicitud));
                var respuestaOrdenada = respuesta.ToList();

                return Message.Successful(respuestaOrdenada);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<ArchivoRequisito2ResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<ObtenerDatoPagoTramiteResponseDto>> ObtenerDatoPagoTramite(int idDetSolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<ObtenerDatoPagoTramiteResponseDto>(await _tramiteRepository.ObtenerDatoPagoTramite(idDetSolicitud));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<ObtenerDatoPagoTramiteResponseDto>(ex);
            }
        }

        public async Task<StatusResponse<long>> RegistrarPagoSolicitud(RegistrarPagoSolicitudRequestDto request)
        {
            try
            {
                var requestEntity = _mapper.Map<TuDetSolicitud>(request);
                var respuesta = await _tramiteRepository.RegistrarPagoSolicitud(requestEntity);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> GuardarArchivoRequisitoSolicitud(ArchivoRequisitoSolicitudDto request)
        {
            try
            {
                var requestEntity = _mapper.Map<SP_INSERT_ARCHIVO_REQUISITO_SOLICITUD_Request_Entity>(request);
                var respuesta = await _tramiteRepository.GuardarArchivoRequisitoSolicitud(requestEntity);
                await _tramiteRepository.ActualizarDetalleSolicitud(request.solicitudId, request.requisitoId, request.estadoId);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> ActualizarDetalleSolicitud(ActualizarDetalleSolicitudDto request)
        {
            try
            {
                var respuesta = await _tramiteRepository.ActualizarDetalleSolicitud(request.solicitudId, request.requisitoId, request.estadoId);
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }
    }
}

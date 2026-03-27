using AutoMapper;
using Minem.Tupa.Dto.AutorizacionQuemaGas;
using Minem.Tupa.Dto.EstudiosPresentados;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Dto.Mapa;
using Minem.Tupa.Dto.Observacion;
using Minem.Tupa.Dto.Profesional;
using Minem.Tupa.Dto.Reunion;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Dto.Tupa;
using Minem.Tupa.Entity;
using Minem.Tupa.Entity.AutorizacionQuemaGas;
using Minem.Tupa.Entity.EstudiosPresentados;
using Minem.Tupa.Entity.Formulario;
using Minem.Tupa.Entity.Its;
using Minem.Tupa.Entity.Mapa;
using Minem.Tupa.Entity.Observacion;
using Minem.Tupa.Entity.Profesional;
using Minem.Tupa.Entity.Reunion;
using Minem.Tupa.Entity.Solicitud;
using Minem.Tupa.Entity.Tramite;
using Minem.Tupa.Entity.Tupa;
using System;
namespace Minem.Tupa.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Tupa
            CreateMap<PersonaDto, USP_S_Persona_Buscar_DNI_Response_Entity>().ReverseMap();
            CreateMap<RequisitoEntity, RequisitoDto>().ReverseMap();
            CreateMap<SectorEntity, SectorDto>().ReverseMap();
            CreateMap<TupaEntity, TupaDto>().ReverseMap();
            CreateMap<DocumentoDespachadoResponse_Entity, DocumentoDespachadoDto>().ReverseMap();
            CreateMap<DatosFormularioDto, SolicitudResponse_Entity>()
                   .ForMember(dest => dest.AbreviaturaTupa, opt => opt.MapFrom(src => src.TipoIga))
                   .ForMember(dest => dest.NombreTitular, opt => opt.MapFrom(src => src.TitulaMinero))
                   .ForMember(dest => dest.NombreProyecto, opt => opt.MapFrom(src => src.NombreProyecto))
                   .ForMember(dest => dest.NumSTD, opt => opt.MapFrom(src => src.NumeroExpediente))
                   .ReverseMap();

            #endregion Tupa

            #region Its
            CreateMap<ItsProyectoDto, SP_INSERT_ITS_PROYECTO_Response_Entity>().ReverseMap();
            CreateMap<ItsProyectoDto, SP_OBTENER_ITS_PROYECTO_Request_Entity>().ReverseMap();
            CreateMap<ItsProyectoArchivoDto, SP_INSERT_ITS_PROYECTO_ARCHIVO_Response_Entity>().ReverseMap();
            CreateMap<ItsProyectoArchivoDto, SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity>().ReverseMap();
            CreateMap<ItsProyectoArchivoEliminarDto, SP_ELIMINAR_ITS_PROYECTO_ARCHIVO_Response_Entity>().ReverseMap();
            CreateMap<ItsProyectoRepresentanteDto, SP_INSERT_ITS_PROYECTO_REPRESENTANTE_Response_Entity>().ReverseMap();
            CreateMap<ItsProyectoProfesionalDto, SP_INSERT_ITS_PROYECTO_PROFESIONAL_Response_Entity>().ReverseMap();
            CreateMap<ItsMapaDto, SP_INSERT_ITS_MAPA_Request_Entity>().ReverseMap();
            CreateMap<ItsMapaDto, SP_UPDATE_ITS_MAPA_Request_Entity>().ReverseMap();
            CreateMap<ItsMapaDto, SP_OBTENER_ITS_MAPA_Response_Entity>().ReverseMap();
            CreateMap<ItsReunionDto, ItsReunion_Response_Entity>().ReverseMap();

            #endregion Its

            #region Reunion
            CreateMap<ReunionSolicitudDto, SP_INSERT_REUNION_SOLICITUD_Response_Entity>().ReverseMap();
            CreateMap<ReunionParticipanteDto, SP_INSERT_REUNION_PARTICIPANTE_Response_Entity>().ReverseMap();
            CreateMap<ReunionObjetivoDto, SP_INSERT_REUNION_OBJETIVO_Response_Entity>().ReverseMap();
            CreateMap<ReunionRequestDto, SP_OBTENER_REUNION_Request_Entity>().ReverseMap();
            CreateMap<ReunionHistorialDto, SP_OBTENER_REUNION_HISTORIAL_Request_Entity>().ReverseMap();
            #endregion Reunion

            #region Tramite
            CreateMap<ObtenerNotasRequestDto, SP_OBTENER_NOTAS_Request_Entity>().ReverseMap();
            CreateMap<SP_OBTENER_NOTAS_Response_Entity, ObtenerNotasResponseDto>().ReverseMap();

            CreateMap<ObtenerDocumentosRequestDto, SP_OBTENER_TRAMITE_DOCS_Request_Entity>().ReverseMap();
            CreateMap<SP_OBTENER_TRAMITE_DOCS_Response_Entity, ObtenerDocumentosResponseDto>().ReverseMap();

            CreateMap<SP_OBTENER_PROFESIONES_Response_Entity, ObtenerProfesionesResponseDto>().ReverseMap();
            CreateMap<SP_OBTENER_PROFESIONES_Response_Entity, ObtenerProfesionesResponseDto>().ReverseMap();

            CreateMap<ObtenerTramiteRequestDto, SP_OBTENER_TRAMITE_Request_Entity>().ReverseMap();
            CreateMap<SP_OBTENER_TRAMITE_Response_Entity, ObtenerTramiteResponseDto>().ReverseMap();

            CreateMap<ObtenerTramiteRequestDto, SP_OBTENER_TRAMITE_Request_Entity>().ReverseMap();
            CreateMap<SP_OBTENER_REQUISITOS_TRAMITE_Response_Entity, TramiteRequisito>().ReverseMap();

            CreateMap<EnviarSolicitudRequestDto, SP_OBTENER_TRAMITE_Request_Entity>().ReverseMap();
            CreateMap<USP_S_OBTENER_MIS_TRAMITES_Response_Entity, ObtenerMisTramitesResponseDto>().ReverseMap();

            CreateMap<USP_S_OBTENER_DIA_APROBADO_Response_Entity, ObtenerDiaAprobadoResponseDto>().ReverseMap();

            CreateMap<USP_S_OBTENER_DOCUMENTOS_ADCIONALES_Response_Entity, ObtenerDocumentoAdicionalResponseDto>().ReverseMap();
            CreateMap<USP_S_OBTENER_FORMULARIO_DIA_Response_Entity, ObtenerFormularioDiaResponseDto>().ReverseMap();
            CreateMap<USP_S_OBTENER_FORMULARIO_DIA_Response_Entity, ObshistjsonDto>().ReverseMap();
            CreateMap<USP_S_OBTENER_TIPO_COMUNICACION_Response_Entity, ObtenerTipoComunicacionResponseDto>().ReverseMap();
            CreateMap<USP_S_OBTENER_TIPO_DOCUMENTO_Response_Entity, ObtenerTipoDocumentoResponseDto>().ReverseMap();

            CreateMap<ArchivoRequisito2_Entity, ArchivoRequisito2ResponseDto>().ReverseMap();

            CreateMap<ObtenerDocumentoAdicionalResponseDto, USP_S_OBTENER_DOCUMENTOS_ADCIONALES_DETALLE_Response_Entity>().ReverseMap();

            CreateMap<SP_INSERT_REGISTRO_DIA_Response_Entity, RegistrarEstudioResponseDto>().ReverseMap();

            CreateMap<PRC_USP_S_LISTAR_TRAZASIGNACION_Response_Entity, TrazabilidadAsignacionReponseDto>()
                .ForMember(dest => dest.IdRow, opt => opt.MapFrom(src => src.IDROW))
                .ForMember(dest => dest.DenUniOrganica, opt => opt.MapFrom(src => src.DENUNIORGANICA))
                .ForMember(dest => dest.Emisor, opt => opt.MapFrom(src => src.EMISOR))
                .ForMember(dest => dest.Receptor, opt => opt.MapFrom(src => src.RECEPTOR))
                .ForMember(dest => dest.FechIniAsignacion, opt => opt.MapFrom(src => src.FECHINIASIGNACION))
                .ForMember(dest => dest.FechFinAsignacion, opt => opt.MapFrom(src => src.FECHFINASIGNACION))
                .ForMember(dest => dest.IdEstadoAsignacion, opt => opt.MapFrom(src => src.IDESTADOASIGNACION))
                .ForMember(dest => dest.DescripcionEstadoAsignacion, opt => opt.MapFrom(src => src.DESCESTADOASIGNACION))
                .ReverseMap();

            CreateMap<SP_OBTENER_DATOS_DETALLADO_SOLICITUD_Response_Entity, ObtenerSolicitudResponseDto>()
                .ForMember(dest => dest.NombreProcedimiento, opt => opt.MapFrom(src => src.NombreProcedimiento))
                .ForMember(dest => dest.CodigoProcedimiento, opt => opt.MapFrom(src => src.CodMaeTupa))
                .ForMember(dest => dest.NombreUnidadOrganica, opt => opt.MapFrom(src => src.DenominacionDependencia))
                .ForMember(dest => dest.NroComprobante, opt => opt.MapFrom(src => src.NroComprobante))
                .ForMember(dest => dest.FechaPago, opt => opt.MapFrom(src => src.FechaPago))
                .ForMember(dest => dest.NombreProyecto, opt => opt.MapFrom(src => src.NombreProyecto))
                .ForMember(dest => dest.RazonSocialSolicitante, opt => opt.MapFrom(src => src.NombreTitular))
                .ForMember(dest => dest.NroDocumentoSolicitante, opt => opt.MapFrom(src => src.NroDocumentoSolicitante))
                .ForMember(dest => dest.NroRucSolicitante, opt => opt.MapFrom(src => src.Ruc))
                .ForMember(dest => dest.TelefonoSolicitante, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.CorreoSolicitante, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DomicilioLegalSolicitante, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))

                .ForMember(dest => dest.RepresentanteLegal, opt => opt.MapFrom(src => src.RepresentanteLegal))
                .ForMember(dest => dest.DomicilioRepresentante, opt => opt.MapFrom(src => src.DomicilioRepresentante))
                .ForMember(dest => dest.DescSolicitadoLinea01, opt => opt.MapFrom(src => src.DescSolicitadoLinea01))
                .ReverseMap();
            CreateMap<SituacionSolicitudFinal_Entity, SituacionSolicitudFinalResponseDto>().ReverseMap();

            CreateMap<ObtenerDatoPagoTramiteResponseDto, TuDetSolicitud>()
                .ForMember(dest => dest.CODDETSOLICITUD, opt => opt.MapFrom(src => src.IdPagoSolicitud))
                .ForMember(dest => dest.CODMAESOLICITUD, opt => opt.MapFrom(src => src.IdSolicitud))
                .ForMember(dest => dest.CODDETCONCATREQUISITO, opt => opt.MapFrom(src => src.CodDetConCatRequisito))
                .ForMember(dest => dest.CODUNIPASARELA, opt => opt.MapFrom(src => src.CodigoUniPasarela))
                .ForMember(dest => dest.CODVOUCHER, opt => opt.MapFrom(src => src.CodigoVoucher))
                .ForMember(dest => dest.ID_TIPO_PAGO, opt => opt.MapFrom(src => src.IdTipoPago))
                .ForMember(dest => dest.CODIGO_CAJA, opt => opt.MapFrom(src => src.CodigoCaja))
                .ForMember(dest => dest.FECHA_PAGO, opt => opt.MapFrom(src => src.FechaPago))
                .ForMember(dest => dest.IMPORTE, opt => opt.MapFrom(src => src.Importe))
                .ForMember(dest => dest.NRO_SECUENCIA, opt => opt.MapFrom(src => src.NroSecuencia))
                .ForMember(dest => dest.CODIGO_OFICINA, opt => opt.MapFrom(src => src.CodigoOficina))
                .ReverseMap();

            CreateMap<RegistrarPagoSolicitudRequestDto, TuDetSolicitud>()
                .ForMember(dest => dest.CODDETSOLICITUD, opt => opt.MapFrom(src => src.IdPagoSolicitud))
                .ForMember(dest => dest.CODMAESOLICITUD, opt => opt.MapFrom(src => src.IdSolicitud))
                .ForMember(dest => dest.CODDETCONCATREQUISITO, opt => opt.MapFrom(src => src.CodDetConCatRequisito))
                .ForMember(dest => dest.CODUNIPASARELA, opt => opt.MapFrom(src => src.CodigoUniPasarela))
                .ForMember(dest => dest.CODVOUCHER, opt => opt.MapFrom(src => src.CodigoVoucher))
                .ForMember(dest => dest.ID_TIPO_PAGO, opt => opt.MapFrom(src => src.IdTipoPago))
                .ForMember(dest => dest.CODIGO_CAJA, opt => opt.MapFrom(src => src.CodigoCaja))
                .ForMember(dest => dest.FECHA_PAGO, opt => opt.MapFrom(src => src.FechaPago))
                .ForMember(dest => dest.IMPORTE, opt => opt.MapFrom(src => src.Importe))
                .ForMember(dest => dest.NRO_SECUENCIA, opt => opt.MapFrom(src => src.NroSecuencia))
                .ForMember(dest => dest.CODIGO_OFICINA, opt => opt.MapFrom(src => src.CodigoOficina))
                .ForMember(dest => dest.REGUSUAREGISTRA, opt => opt.MapFrom(src => src.Usuario))

                .ReverseMap();

            CreateMap<SP_INSERT_ARCHIVO_REQUISITO_SOLICITUD_Request_Entity, ArchivoRequisitoSolicitudDto>().ReverseMap();
            #endregion Tramite

            #region Observacion         
            CreateMap<PRC_S_ULTIMOTUMOVMETADAHISTORICO_Response_Entity, TumovmetadahistoricoDto>().ReverseMap();
            CreateMap<PRC_S_OBSHISTJSON_Response_Entity, ObshistjsonDto>().ReverseMap();

            CreateMap<ObshistjsonDto, PRC_I_OBSHISTJSON_Request_Entity>().ReverseMap();
            CreateMap<DetobshistjsonDto, PRC_I_DETOBSHISTJSON_Request_Entity>().ReverseMap();
            #endregion Observacion

            #region Mapa
            CreateMap<SP_SELECT_TIPO_AREA_Response_Entity, TipoAreaResponseDto>()
                .ForMember(dest => dest.IdTipoArea, opt => opt.MapFrom(src => src.ID_TIPO_AREA))
                .ForMember(dest => dest.NombreActividad, opt => opt.MapFrom(src => src.NOMBRE_ACTIVIDAD))
                .ReverseMap();
            #endregion Mapa

            #region BandejaEstudiosPresentados
            CreateMap<BandejaEstudiosPresentadosRequest_Entity, BandejaEstudiosPresentadosRequestDto>()
                .ReverseMap();
            CreateMap<BandejaEstudiosPresentadosResponse_Entity, BandejaEstudiosPresentadosResponseDto>()
                .ReverseMap();

            CreateMap<TipoEstudioResponse_Entity, TipoEstudioResponseDto>()
               .ReverseMap();

            CreateMap<TipoEstudiosResponseDto, TipoEstudiosResponse_Entity>()
                .ForMember(dest => dest.Id_tipo_estudio, opt => opt.MapFrom(src => src.IdTipoEstudio))
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.Sigla))
                .ReverseMap();

            CreateMap<TipoEstudiosTupaResponseDto, TipoEstudiosTupaResponse_Entity>()
                .ForMember(dest => dest.Id_tipo_estudio, opt => opt.MapFrom(src => src.IdTipoEstudio))
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.Sigla))
                .ReverseMap();

            CreateMap<SituacionResponse_Entity, SituacionResponseDto>()
                .ForMember(dest => dest.CodMaeEstado, opt => opt.MapFrom(src => src.CODMAEESTADO))
                .ForMember(dest => dest.Denominacion, opt => opt.MapFrom(src => src.DENOMINACION))
                .ReverseMap();

            #endregion BandejaEstudiosPresentados

            #region Observacion de Opinantes

            CreateMap<TransactionListOpinantesResponseDto, TransactionListOpinantes>()
                .ForMember(dest => dest.Id_solicitud_opinante, opt => opt.MapFrom(src => src.IdSolicitudOpinante))
                .ForMember(dest => dest.Id_solicitud, opt => opt.MapFrom(src => src.IdSolicitud))
                .ForMember(dest => dest.Id_solicitud_expediente, opt => opt.MapFrom(src => src.IdSolicitudExpediente))
                .ForMember(dest => dest.Id_opinante, opt => opt.MapFrom(src => src.IdOpinante))
                .ReverseMap();

            CreateMap<DocumentoInstitucionResponseDto, DocumentoInstitucion_Entity>()
                .ForMember(dest => dest.Id_solicitud_opinante_respuesta, opt => opt.MapFrom(src => src.IdSolicitudOpinanteRespuesta))
                .ForMember(dest => dest.Id_solicitud_opinante, opt => opt.MapFrom(src => src.IdSolicitudOpinante))
                .ForMember(dest => dest.Nro_expediente, opt => opt.MapFrom(src => src.NroExpediente))
                .ForMember(dest => dest.Fecha_expediente, opt => opt.MapFrom(src => src.FechaExpediente))
                .ForMember(dest => dest.Id_asunto_adjunto, opt => opt.MapFrom(src => src.IdAsuntoAdjunto))
                .ForMember(dest => dest.Nro_oficio_notificado, opt => opt.MapFrom(src => src.NroOficioNotificado))
                .ForMember(dest => dest.Fecha_notificacion, opt => opt.MapFrom(src => src.FechaNotificacion))
                .ForMember(dest => dest.Nro_cut, opt => opt.MapFrom(src => src.NroCut))
                .ForMember(dest => dest.Codigo_expediente_principal, opt => opt.MapFrom(src => src.CodigoExpedientePrincipal))
                .ForMember(dest => dest.Ruc, opt => opt.MapFrom(src => src.Ruc))
                .ForMember(dest => dest.Usuario_externo, opt => opt.MapFrom(src => src.UsuarioExterno))
                .ForMember(dest => dest.Id_tipo_documento, opt => opt.MapFrom(src => src.IdTipoDocumento))
                .ForMember(dest => dest.Descripcion_documento, opt => opt.MapFrom(src => src.DescripcionDocumento))
                .ForMember(dest => dest.Asunto, opt => opt.MapFrom(src => src.Asunto))
                .ForMember(dest => dest.Codigo_expediente_anexado, opt => opt.MapFrom(src => src.CodigoExpedienteAnexado))
                .ForMember(dest => dest.Codigo_resultado, opt => opt.MapFrom(src => src.CodigoResultado))
                .ForMember(dest => dest.Envio_correo, opt => opt.MapFrom(src => src.EnvioCorreo))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Usuario_registra, opt => opt.MapFrom(src => src.UsuarioRegistra))
                .ForMember(dest => dest.Fecha_registra, opt => opt.MapFrom(src => src.FechaRegistra))
                .ForMember(dest => dest.Usuario_modifica, opt => opt.MapFrom(src => src.UsuarioModifica))
                .ForMember(dest => dest.Fecha_modifica, opt => opt.MapFrom(src => src.FechaModifica))
                .ForMember(dest => dest.Asunto_adjunto, opt => opt.MapFrom(src => src.AsuntoAdjunto))
                .ForMember(dest => dest.Notificar_impulso, opt => opt.MapFrom(src => src.NotificarImpulso))
                .ReverseMap();

            CreateMap<DocumentoInstitucionAdjuntosResponseDto, DocumentoInstitucionAdjuntos_Entity>()
                .ForMember(dest => dest.Id_solicitud_opinante, opt => opt.MapFrom(src => src.IdSolicitudOpinante))
                .ForMember(dest => dest.Id_Solicitud_Opinante_Respuesta_Adj, opt => opt.MapFrom(src => src.IdSolicitudOpinanteRespuestaAdj))
                .ForMember(dest => dest.Id_Solicitud_Opinante_Respuesta, opt => opt.MapFrom(src => src.IdSolicitudOpinanteRespuesta))
                .ForMember(dest => dest.Nombre_Documento, opt => opt.MapFrom(src => src.NombreDocumento))
                .ForMember(dest => dest.Id_Archivo, opt => opt.MapFrom(src => src.IdArchivo))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Usuario_Registra, opt => opt.MapFrom(src => src.UsuarioRegistra))
                .ForMember(dest => dest.Fecha_Registra, opt => opt.MapFrom(src => src.FechaRegistra))
                .ForMember(dest => dest.Usuario_Modifica, opt => opt.MapFrom(src => src.UsuarioModifica))
                .ForMember(dest => dest.Fecha_Modifica, opt => opt.MapFrom(src => src.FechaModifica))
                .ReverseMap();

            CreateMap<TransactionResumeDataResponseDto, TransactionResumeData>().ReverseMap();

            #endregion

            #region aqg
            CreateMap<LoteDto, SP_OBTENER_LOTES_Response_Entity>().ReverseMap();
            CreateMap<MotivoDto, SP_OBTENER_MOTIVOS_Response_Entity>().ReverseMap();
            CreateMap<SP_OBTENER_MOTIVOS_Response_Entity, MotivoDto>().ReverseMap();
            CreateMap<InformeJustificacionDto, SP_INSERTAR_INFORME_JUSTIFICACION_Request_Entity>().ReverseMap();
            CreateMap<SP_OBTENER_INFORME_Response_Entity, InformeJustificacionDto>().ReverseMap();
            CreateMap<MotivoInformeDto, SP_INSERTAR_MOTIVOS_INFORME_Request_Entity>().ReverseMap();
            CreateMap<SP_INSERTAR_MOTIVOS_INFORME_Request_Entity, MotivoInformeDto>().ReverseMap();
            CreateMap<FacilidadDto, SP_OBTENER_FACILIDAD_Response_Entity>().ReverseMap();
            CreateMap<SP_INSERTAR_FACILIDAD_Request_Entity, FacilidadDto>().ReverseMap();
            CreateMap<QuemadorDto, SP_OBTENER_QUEMADOR_Response_Entity>().ReverseMap();
            CreateMap<SP_INSERTAR_QUEMADOR_Request_Entity, QuemadorDto>().ReverseMap();
            CreateMap<BalanceDto, SP_OBTENER_BALANCE_Response_Entity>().ReverseMap();
            CreateMap<SP_INSERTAR_BALANCE_Request_Entity, BalanceDto>().ReverseMap();
            CreateMap<AccionDto, SP_OBTENER_ACCION_Request_Entity>().ReverseMap();
            CreateMap<SP_INSERTAR_ACCION_Request_Entity, AccionDto>().ReverseMap();
            CreateMap<SP_INSERTAR_CRONOGRAMA_Request_Entity, CronogramaDto>().ReverseMap();
            CreateMap<CronogramaDto, SP_OBTENER_CRONOGRAMA_Response_Entity>().ReverseMap();
            CreateMap<SP_INSERTAR_ADJUNTO_INFORME_Request_Entity, AdjuntoInformeDto>().ReverseMap();
            CreateMap<AdjuntoInformeDto, SP_OBTENER_ADJUNTO_INFORME_Response_Entity>().ReverseMap();
            CreateMap<ObservacionCapituloDto, SP_OBTENER_OBSERVACION_CAPITULO_Response_Entity>().ReverseMap();
            #endregion aqg

            CreateMap<ItsReunionAdjuntoDto, USP_I_INSERTAR_REUNION_ADJUNTO_Request_Entity>()
                .ReverseMap();

            CreateMap<ItsReunionAdjuntoDto, USP_S_OBTENER_REUNION_ADJUNTO_Response_Entity>()
                .ForMember(dest => dest.ID_REUNION_SOLICITUD, opt => opt.MapFrom(src => src.IdReunionSolicitud))
                .ForMember(dest => dest.NOMBRE_ARCHIVO, opt => opt.MapFrom(src => src.NombreArchivo))
                .ForMember(dest => dest.ID_ARCHIVO, opt => opt.MapFrom(src => src.IdArchivo))
                .ForMember(dest => dest.USUARIO_REGISTRA, opt => opt.MapFrom(src => src.Usuario))
                .ReverseMap();

            CreateMap<ItsReunionAdjuntoEliminarDto, USP_U_ELIMINAR_REUNION_ADJUNTO_Request_Entity>()
                .ReverseMap();

        }
    }
}

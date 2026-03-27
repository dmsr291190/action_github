using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface ITramiteApplication
    {
        Task<StatusResponse<long>> GenerarTramite(GenerarTramiteRequestDto request);

        Task<StatusResponse<long>> ActualizarPadreITS(long codMaeSolicitud, long codMaeSolicitudPadre);

        Task<StatusResponse<List<ObtenerNotasResponseDto>>> ObtenerNotas(ObtenerNotasRequestDto request);
        Task<StatusResponse<List<ObtenerDocumentosResponseDto>>> ObtenerDocumentos(ObtenerDocumentosRequestDto request);
        Task<StatusResponse<ObtenerTramiteResponseDto>> ObtenerTramite(ObtenerTramiteRequestDto request);
        Task<StatusResponse<ObtenerTramiteResponseDto>> EnviarSolicitud(EnviarSolicitudRequestDto request);
        Task<StatusResponse<List<ObtenerMisTramitesResponseDto>>> ObtenerMisTramites();
        Task<StatusResponse<List<ObtenerDiaAprobadoResponseDto>>> ObtenerDIAAprobado();
        Task<StatusResponse<long>> GenerarDocumentoAdicional(GenerarDocumentoAdicionalRequestDto request);
        Task<StatusResponse<List<ObtenerDocumentoAdicionalResponseDto>>> ObtenerDocumentosAdicionales(long idTramite);
        Task<StatusResponse<long>> EliminarDocumentoAdicional(long request);
        Task<StatusResponse<long>> ActualizarIdEstudio(ActualizarEstudioRequestDto request);
        Task<StatusResponse<long>> AnularTramite(AnularTramiteRequestDto request);
        Task<StatusResponse<List<ObtenerTipoComunicacionResponseDto>>> ObtenerTipoComunicacion();
        Task<StatusResponse<List<ObtenerTipoDocumentoResponseDto>>> ObtenerTipoDocumento();
        Task<StatusResponse<long>> RegistrarNombreProyecto(RegistrarNombreProyectoRequestDto request);
        Task<StatusResponse<long>> ValidarNombreProyecto(long idTramite, string nombreProyecto);
        Task<StatusResponse<ObtenerTramiteResponseDto>> EnviarTramiteAdicional(EnviarTramiteAdicionalRequestDto request);
        Task<StatusResponse<RegistrarEstudioResponseDto>> RegistrarEstudio(RegistrarEstudioRequestDto request);
        Task<StatusResponse<List<TrazabilidadAsignacionReponseDto>>> VerTrazabilidad(long codMaeSolicitud);        
        Task<StatusResponse<SituacionSolicitudFinalResponseDto>> ObtenerSituacionSolicitudFinal(int codMaeSolicitud);
        Task<StatusResponse<long>> ActualizarSituacionAdmisibilidadTumaesolicitud(int codMaeSolicitud, int codSituacion, int codEtapa);
        Task<StatusResponse<long>> ActualizarSituacionAdmisibilidadFuncionario(RegistrarAdmisibilidadFuncionarioDto request);
        Task<StatusResponse<long>> ActualizarArchivoRequisito2(int codMaeSolicitud, int codMaeRequisito, int codMaeEstado, int idArchivo, string nomArchivo, string extArchivo);
        Task<StatusResponse<List<ArchivoRequisito2ResponseDto>>> ObtenerArchivoRequisito2(int codMaeSolicitud);
        Task<StatusResponse<ObtenerDatoPagoTramiteResponseDto>> ObtenerDatoPagoTramite(int idDetSolicitud);
        Task<StatusResponse<long>> RegistrarPagoSolicitud(RegistrarPagoSolicitudRequestDto request);
        Task<StatusResponse<long>> GuardarArchivoRequisitoSolicitud(ArchivoRequisitoSolicitudDto request);
        Task<StatusResponse<long>> ActualizarDetalleSolicitud(ActualizarDetalleSolicitudDto request);
    }
}

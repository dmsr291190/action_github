using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Documento;
using Minem.Tupa.Dto.EstudiosPresentados;
using Minem.Tupa.Dto.Svc.Notificacion;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface IEstudiosPresentadosApplication
    {
        Task<StatusResponse<PaginacionResultDto<BandejaEstudiosPresentadosResponseDto>>> GetBandeja(BandejaEstudiosPresentadosRequestDto request);
        Task<StatusResponse<List<TipoEstudioResponseDto>>> GetTipoEstudio();
        Task<StatusResponse<List<SituacionResponseDto>>> GetSituacion();
        Task<StatusResponse<long>> GuardarAporte(SolicitudAporteRequestDto request);
        Task<StatusResponse<List<TipoEstudiosResponseDto>>> GetListarTipoEstudio();
        Task<StatusResponse<List<TipoEstudiosTupaResponseDto>>> GetListarTipoEstudioTupa(string CodMaeTupa);

        Task<StatusResponse<List<DocumentoDespachadoResponseDto>>> GetDocumentosDespachados(int idSolicitud);
        Task<StatusResponse<byte[]>> DescargaEnBloqueZip(DescargaBloqueRequestDto request);
        Task<StatusResponse<byte[]>> DescargaDocumentoZip(DescargaBloqueRequestDto request);

        Task<StatusResponse<DescargaBloqueResponseDto>> DescargaEnBloque(int idSolicitud);

        Task<StatusResponse<byte[]>> DescargaEnBloqueIndiceExcel(DescargaBloqueRequestDto request);
    }
}

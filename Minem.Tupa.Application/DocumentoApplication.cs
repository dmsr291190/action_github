using AutoMapper;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Configuration;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.IApplication;
using Minem.Tupa.IApplication.Reporte.Form;
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
    public class DocumentoApplication(
        IMapper mapper,
        IConfiguration configuration,
        ITramiteRepository tramiteRepository,
        IPdfAutorizacionQuemaGas pdfAutorizacionQuemaGas
        ) : IDocumentoApplication
    {
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;
        private readonly ITramiteRepository _tramiteRepository = tramiteRepository;
        private readonly IPdfAutorizacionQuemaGas _pdfAutorizacionQuemaGas = pdfAutorizacionQuemaGas;

        public async Task<StatusResponse<DescargarPlantillaDiaResponseDto>> GenerarDocumentoFormulario(int idSolicitud)
        {
            var response = new StatusResponse<DescargarPlantillaDiaResponseDto>();
            try
            {
                var solicitudEntity = await _tramiteRepository.ObtenerDatosDetalladoSolicitud(idSolicitud);
                //var solicitudResponseDto = _mapper.Map<ObtenerSolicitudResponseDto>(solicitudEntity);
                //if (solicitudResponseDto == null)
                //{
                //    solicitudResponseDto = solicitudResponseDto ?? new ObtenerSolicitudResponseDto();

                //    solicitudResponseDto.NombreProyecto = " "; //"NEO PROJECT";
                //    solicitudResponseDto.NombreProcedimiento = " "; //"DIA";
                //    solicitudResponseDto.CodigoProcedimiento = " "; //"BG203";
                //    solicitudResponseDto.NombreUnidadOrganica = " "; //"DGAAM";
                //    solicitudResponseDto.NroComprobante = " "; //"001-2025-002";
                //    solicitudResponseDto.FechaPago = " "; //string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                //    solicitudResponseDto.RazonSocialSolicitante = " "; //"JUAN PEREZ MARTINEZ";
                //    solicitudResponseDto.NroDocumentoSolicitante = " "; //"99999999";
                //    solicitudResponseDto.NroRucSolicitante = " "; //"10999999991";
                //    solicitudResponseDto.Asiento = " "; //"12";
                //    solicitudResponseDto.PartidaRegistral = " "; //"23";
                //    solicitudResponseDto.TelefonoSolicitante = " "; //"18737887";
                //    solicitudResponseDto.CelularSolicitante = " "; //"99999999";
                //    solicitudResponseDto.CorreoSolicitante = " "; //"jperez@minem.gob.pe";
                //    solicitudResponseDto.DomicilioLegalSolicitante = " "; //"av. peru 12345";
                //    solicitudResponseDto.Departamento = " "; //"LIMA";
                //    solicitudResponseDto.Provincia = " "; //"LIMA";
                //    solicitudResponseDto.Distrito = " "; //"SAN BORJA";
                //    solicitudResponseDto.RepresentanteLegal = " "; //"JUAN PEREZ MARTINEZ rep";
                //    solicitudResponseDto.DomicilioRepresentante = " "; //"AV. BRASIL #123";
                //}

                if (solicitudEntity.CodMaeTupa == Constante.Tupa.AUTORIZACION_QUEMA_GAS)
                {
                    response = await _pdfAutorizacionQuemaGas.Generar(idSolicitud);
                }

            }
            catch (Exception ex)
            {
                response = Message.Exception<DescargarPlantillaDiaResponseDto>(ex);
            }

            return response;
        }


    }
}

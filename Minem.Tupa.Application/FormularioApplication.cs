using AutoMapper;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Minem.Tupa.Application.PDF;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.Entity.Tramite;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Proxy.Interface;
using Minem.Tupa.Utils;
using OfficeOpenXml;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Document = iText.Layout.Document;
using Table = iText.Layout.Element.Table;

namespace Minem.Tupa.Application
{
    public class FormularioApplication : IFormularioApplication
    {
        private readonly IMapper _mapper;
        private readonly IFormularioRepository _formularioRepository;
        private readonly IConfiguration _configuration;
        private readonly ITramiteRepository _tramiteRepository;
        private readonly IExternoService _externoService;
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly IOptions<AppSettings> _appSettings = appSettings;

        public FormularioApplication(IFormularioRepository formularioRepository, IMapper mapper, IConfiguration configuration, ITramiteRepository tramiteRepository, IExternoService externoService, IHttpContextAccessor httpContextAccessor)
        //, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _configuration = configuration;
            _formularioRepository = formularioRepository;
            _tramiteRepository = tramiteRepository;
            _externoService = externoService;
            _contextAccessor = httpContextAccessor;
            //_appSettings = appSettings;
        }

        #region #region Metodos Dia          

        public async Task<StatusResponse<long>> GuardarFormulario(GuardarFormularioRequestDto request)
        {
            try
            {
                Console.WriteLine("guardar formulario: " + request);
                var response = await _formularioRepository.GuardarFormulario(request.codMaeSolicitud, request.dataJson);
                return Message.Successful(response);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<ObtenerFormularioDiaResponseDto>> ObtenerFormularioDia(long CodMaeSolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<ObtenerFormularioDiaResponseDto>(
                    await _formularioRepository.ObtenerFormularioDia(CodMaeSolicitud));

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

        public async Task<StatusResponse<DescargarPlantillaDiaResponseDto>> DescargarDocumento(DescargarPlantillaDiaRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                var idCliente = Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == "idCliente").Value);

                var solicitudEntity = await _tramiteRepository.ObtenerDatosDetalladoSolicitud(request.CodMaeSolicitud);
                var solicitudResponseDto = _mapper.Map<ObtenerSolicitudResponseDto>(solicitudEntity);
                if (solicitudResponseDto == null)
                {
                    solicitudResponseDto = solicitudResponseDto ?? new ObtenerSolicitudResponseDto();

                    solicitudResponseDto.NombreProyecto = " "; //"NEO PROJECT";
                    solicitudResponseDto.NombreProcedimiento = " "; //"DIA";
                    solicitudResponseDto.CodigoProcedimiento = " "; //"BG203";
                    solicitudResponseDto.NombreUnidadOrganica = " "; //"DGAAM";
                    solicitudResponseDto.NroComprobante = " "; //"001-2025-002";
                    solicitudResponseDto.FechaPago = " "; //string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    solicitudResponseDto.RazonSocialSolicitante = " "; //"JUAN PEREZ MARTINEZ";
                    solicitudResponseDto.NroDocumentoSolicitante = " "; //"99999999";
                    solicitudResponseDto.NroRucSolicitante = " "; //"10999999991";
                    solicitudResponseDto.Asiento = " "; //"12";
                    solicitudResponseDto.PartidaRegistral = " "; //"23";
                    solicitudResponseDto.TelefonoSolicitante = " "; //"18737887";
                    solicitudResponseDto.CelularSolicitante = " "; //"99999999";
                    solicitudResponseDto.CorreoSolicitante = " "; //"jperez@minem.gob.pe";
                    solicitudResponseDto.DomicilioLegalSolicitante = " "; //"av. peru 12345";
                    solicitudResponseDto.Departamento = " "; //"LIMA";
                    solicitudResponseDto.Provincia = " "; //"LIMA";
                    solicitudResponseDto.Distrito = " "; //"SAN BORJA";
                    solicitudResponseDto.RepresentanteLegal = " "; //"JUAN PEREZ MARTINEZ rep";
                    solicitudResponseDto.DomicilioRepresentante = " "; //"AV. BRASIL #123";
                }

                var datosEmpresa = await _externoService.ObtenerDatoTitularEmpresa(idCliente);

                if (datosEmpresa != null)
                {
                    solicitudResponseDto.RazonSocialSolicitante = datosEmpresa.DatosTitular.NombreTitular;
                    solicitudResponseDto.DomicilioLegalSolicitante = datosEmpresa.DatosTitular.Direccion;
                    solicitudResponseDto.TelefonoSolicitante = datosEmpresa.DatosTitular.Telefono;
                    solicitudResponseDto.NroRucSolicitante = datosEmpresa.DatosTitular.Ruc;
                    solicitudResponseDto.CorreoSolicitante = datosEmpresa.DatosTitular.Email;
                    solicitudResponseDto.RepresentanteLegal = $"{datosEmpresa.DatosRepresentante.ApellidoPaterno} {datosEmpresa.DatosRepresentante.ApellidoMaterno} {datosEmpresa.DatosRepresentante.Nombres}";
                    solicitudResponseDto.NroDocumentoSolicitante = Regex.Replace(datosEmpresa.DatosRepresentante.DocumentoIdentidad, @"\D", "");

                    if (datosEmpresa.DatosTitular.Region.Length > 0)
                    {
                        var region = datosEmpresa.DatosTitular.Region.Split("-");

                        solicitudResponseDto.Departamento = region[0];
                        solicitudResponseDto.Provincia = region[1];
                        solicitudResponseDto.Distrito = region[2];
                    }
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (PdfWriter writer = new(memoryStream))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            Document documento = new Document(pdf);
                            documento.SetMargins(20, 50, 20, 50);

                            PdfFont fuenteTitulo = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                            PdfFont fuenteTexto = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                            DeviceRgb colorSeccion = new DeviceRgb(211, 211, 211);

                            AddTituloAnexo(documento, fuenteTitulo);
                            AddLogo(documento);
                            AddTituloFormato(documento, fuenteTitulo, fuenteTitulo, colorSeccion);
                            AddDatosProcedimiento(documento, fuenteTitulo, fuenteTexto, solicitudResponseDto);
                            AddDatosDependencia(documento, fuenteTitulo, fuenteTexto, solicitudResponseDto);

                            AddDatosSolicitante(documento, fuenteTitulo, fuenteTexto, colorSeccion, solicitudResponseDto);
                            AddDatosDescripcionSolicitado(documento, fuenteTitulo, fuenteTexto, colorSeccion, solicitudResponseDto);
                            AddDatosDocumentoAdjunto(documento, fuenteTitulo, fuenteTexto, colorSeccion);
                            AddDatosDeclaracionJurada(documento, fuenteTitulo, fuenteTexto, colorSeccion, solicitudResponseDto);
                            AddDatosAclaracion(documento, fuenteTitulo, fuenteTexto, colorSeccion);
                            AddTablaSeccionFinal(documento, fuenteTitulo, colorSeccion);
                            documento.Close();
                        }
                    }

                    byte[] pdfBytes = memoryStream.ToArray();
                    return new StatusResponse<DescargarPlantillaDiaResponseDto>
                    {
                        Success = true,
                        Data = new DescargarPlantillaDiaResponseDto
                        {
                            base64Documento = Convert.ToBase64String(pdfBytes)
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return Message.Exception<DescargarPlantillaDiaResponseDto>(ex);
            }
        }
        private void AddParrafoEnBlanco(Document documento, PdfFont fuenteTitulo, float Height = 2f)
        {
            documento.Add(new Paragraph("ASDFG")
                .SetHeight(Height)
                .SetFont(fuenteTitulo)
                .SetFontSize(1)
                .SetTextAlignment(TextAlignment.CENTER));
        }
        private void AddTituloAnexo(Document documento, PdfFont fuenteTitulo)
        {
            documento.Add(new Paragraph("ANEXO: FORMULARIOS Y ANEXOS - APROBADO POR R.M. N°178-2020-MEM/DM")
                .SetFont(fuenteTitulo)
                .SetFontSize(12.5f)
                .SetTextAlignment(TextAlignment.CENTER));
        }
        private void AddLogo(Document documento)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "imagen", "logo-minem.png");

            // Cargar imagen
            ImageData imageData = ImageDataFactory.Create(imagePath);
            Image image = new Image(imageData)
                .ScaleToFit(150, 150)
                .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);

            documento.Add(image);
        }

        private void AddEspacio(Document documento, PdfFont fuenteTitulo, float Height = 0.2f)
        {
            Table espacio = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph(""))
                .SetHeight(Height)
                .SetFont(fuenteTitulo)
                .SetFontSize(0.2f)
                .SetBorder(Border.NO_BORDER);
            espacio.SetBorder(Border.NO_BORDER);
            espacio.AddCell(cell);
            documento.Add(espacio);
        }

        private void AddTituloFormato(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, DeviceRgb color)
        {
            float borderRadius = 2f;
            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 20, 80 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("Formulario 001");
            Cell cell = new Cell()
                .Add(paragraph)
                .SetFont(fuenteTexto)
                .SetFontSize(7)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.SetNextRenderer(new RoundedCellRenderer(cell, borderRadius, color));
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("FORMATO DE SOLICITUD"))
                .SetFont(fuenteTitulo)
                .SetFontSize(12)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.SetNextRenderer(new RoundedCellRenderer(cell, borderRadius, color));
            tablaDatos.AddCell(cell);

            //tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, 2f, new DeviceRgb(230, 240, 250)));
            //tablaDatos.SetMarginTop(10);
            documento.Add(tablaDatos);
        }

        private void AddDatosProcedimiento(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, ObtenerSolicitudResponseDto datos)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            this.AddEspacio(documento, fuenteTitulo);
            //this.AddParrafoEnBlanco(documento, fuenteTexto);
            Table tablaDatos = new Table(2).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("ASUNTO SOLICITADO / NOMBRE DEL PROCEDIMIENTO");
            Cell cell = new Cell()
                .Add(paragraph)
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            //cell.SetNextRenderer(new RoundedCellRenderer(cell, borderRadius, new DeviceRgb(220, 240, 255)));
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("CÓDIGO"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            //cell.SetNextRenderer(new RoundedCellRenderer(cell, borderRadius, new DeviceRgb(220, 240, 255)));
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(datos.NombreProcedimiento) ? string.Empty : datos.NombreProcedimiento))
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            //cell.SetNextRenderer(new RoundedCellRenderer(cell, borderRadius, new DeviceRgb(220, 240, 255)));
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(datos.CodigoProcedimiento) ? string.Empty : datos.CodigoProcedimiento))
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            //.SetFontColor(ColorConstants.WHITE));
            //cell.SetNextRenderer(new RoundedCellRenderer(cell, borderRadius, new DeviceRgb(220, 240, 255)));
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddDatosDependencia(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, ObtenerSolicitudResponseDto dato)
        {
            this.AddEspacio(documento, fuenteTitulo);
            //this.AddParrafoEnBlanco(documento, fuenteTexto);
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 60, 20, 20 })).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell()
                .Add(new Paragraph("DEPENDENCIA A LA CUAL SE DIRIGE LA SOLICITUD"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("N° Comprobante"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("Fecha de pago"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(dato.NombreUnidadOrganica) ? string.Empty : dato.NombreUnidadOrganica))
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(dato.NroComprobante) ? string.Empty : dato.NroComprobante))
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(dato.FechaPago) ? string.Empty : dato.FechaPago))
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddDatosSolicitante(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, DeviceRgb color, ObtenerSolicitudResponseDto dato)
        {
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTituloSeccionDatosSolicitante(documento, fuenteTitulo, color);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaTipoPersonaSolicitante(documento, fuenteTitulo);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaNombreCompletoSolicitante(documento, fuenteTexto, dato);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaNroDocumentoSolicitante(documento, fuenteTexto, dato);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaContactoSolicitante(documento, fuenteTexto, dato);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaDomicilioSolicitante(documento, fuenteTexto, dato);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaUbigeoSolicitante(documento, fuenteTexto, dato);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaRepresentanteLegalSolicitante(documento, fuenteTexto, dato);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaDomicilioRepLegalSolicitante(documento, fuenteTexto, dato);
        }
        private void AddTituloSeccion(Document documento, PdfFont fuenteTitulo, DeviceRgb color, string numero, string titulo)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 97 })).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell()
                .Add(new Paragraph(numero))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(titulo))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, color));

            documento.Add(tablaDatos);
        }

        private void AddTituloSeccionDatosSolicitante(Document documento, PdfFont fuenteTitulo, DeviceRgb color)
        {
            this.AddTituloSeccion(documento, fuenteTitulo, color, "I.", "DATOS DEL SOLICITANTE");
        }

        private void AddTablaTipoPersonaSolicitante(Document documento, PdfFont fuenteTitulo)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 })).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph("PERSONA NATURAL"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("PERSONA JURIDICA"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddTablaNombreCompletoSolicitante(Document documento, PdfFont fuenteTitulo, ObtenerSolicitudResponseDto dato)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph("APELLIDOS Y NOMBRES O RAZÓN SOCIAL"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.RazonSocialSolicitante) ? string.Empty : dato.RazonSocialSolicitante))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);
            tablaDatos.SetPaddingTop(10);
            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddTablaNroDocumentoSolicitante(Document documento, PdfFont fuenteTitulo, ObtenerSolicitudResponseDto dato)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 20, 20, 60 })).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph("N° de DNI / CE /PASAPORTE"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("N° de RUC"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("Inscripción en SUNARP: Asiento y Partida Registral en donde consta inscrito dicho poder"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.NroDocumentoSolicitante) ? string.Empty : dato.NroDocumentoSolicitante))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.NroRucSolicitante) ? string.Empty : dato.NroRucSolicitante))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);
            string asientoYPartida = !string.IsNullOrEmpty(dato.Asiento) && !string.IsNullOrEmpty(dato.PartidaRegistral) ? string.Format("{0} {1}", dato.Asiento, dato.PartidaRegistral) : string.Empty;
            cell = new Cell().Add(new Paragraph(asientoYPartida))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddTablaContactoSolicitante(Document documento, PdfFont fuenteTitulo, ObtenerSolicitudResponseDto dato)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 20, 20, 60 })).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph("TELÉFONO / FAX"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("CELULAR"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("CORREO ELECTRÓNICO"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.TelefonoSolicitante) ? string.Empty : dato.TelefonoSolicitante))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.CelularSolicitante) ? string.Empty : dato.CelularSolicitante))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.CorreoSolicitante) ? string.Empty : dato.CorreoSolicitante))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddTablaDomicilioSolicitante(Document documento, PdfFont fuenteTitulo, ObtenerSolicitudResponseDto dato)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell()
                .Add(new Paragraph("DOMICILIO LEGAL (AV / CALLE / JIRÓN / PSJE / N° / DPTO / MZ / LOTE / URB )"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.DomicilioLegalSolicitante) ? string.Empty : dato.DomicilioLegalSolicitante))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddTablaUbigeoSolicitante(Document documento, PdfFont fuenteTitulo, ObtenerSolicitudResponseDto dato)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 60, 20, 20 })).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph("DISTRITO"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("PROVINCIA"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("DEPARTAMENTO"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.Distrito) ? string.Empty : dato.Distrito))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.Provincia) ? string.Empty : dato.Provincia))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.Departamento) ? string.Empty : dato.Departamento))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddTablaRepresentanteLegalSolicitante(Document documento, PdfFont fuenteTitulo, ObtenerSolicitudResponseDto dato)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell()
                .Add(new Paragraph("REPRESENTANTE LEGAL (APELLIDOS Y NOMBRE)"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(dato.RepresentanteLegal) ? string.Empty : dato.RepresentanteLegal))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));

            documento.Add(tablaDatos);
        }

        private void AddTablaDomicilioRepLegalSolicitante(Document documento, PdfFont fuenteTitulo, ObtenerSolicitudResponseDto dato)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell()
                .Add(new Paragraph("DOMICILIO REPRESENTANTE LEGAL (AV / CALLE / JIRÓN / PSJE / N° / DPTO / MZ / LOTE / URB )"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(dato.DomicilioRepresentante) ? string.Empty : dato.DomicilioRepresentante))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, null));
            documento.Add(tablaDatos);
        }

        private void AddDatosDescripcionSolicitado(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, DeviceRgb color, ObtenerSolicitudResponseDto dato)
        {
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTituloSeccion(documento, fuenteTitulo, color, "II.", "DESCRIPCIÓN DE LO SOLICITADO");
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaDescripcionSolicitado(documento, fuenteTexto, null, dato);
        }

        private void AddTablaDescripcionSolicitado(Document documento, PdfFont fuenteTitulo, DeviceRgb color, ObtenerSolicitudResponseDto dato)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            string texto1 = string.Format("- {0}" , dato.DescSolicitadoLinea01);
            string texto2 = "- Autorizo que todo acto administrativo del presente procedimiento, se notifique por la casilla electrónica del MINEM.";
            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph(texto1))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(texto2))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, color));

            documento.Add(tablaDatos);
        }

        private void AddDatosDocumentoAdjunto(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, DeviceRgb color)
        {
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTituloSeccion(documento, fuenteTitulo, color, "III.", "DOCUMENTOS QUE SE ADJUNTAN");
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaDatosDocumentoAdjunto(documento, fuenteTexto, null);
        }

        private void AddTablaDatosDocumentoAdjunto(Document documento, PdfFont fuenteTitulo, DeviceRgb color)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            string texto1 = "1. Expediente Virtual";
            string texto2 = "2. Pago TUPA";
            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph(texto1))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(texto2))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, color));

            documento.Add(tablaDatos);
        }

        private void AddDatosDeclaracionJurada(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, DeviceRgb color, ObtenerSolicitudResponseDto dato)
        {
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTituloSeccion(documento, fuenteTitulo, color, "IV.", "DECLARACION JURADA");
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaDeclaracionJuradaFirma(documento, fuenteTitulo, fuenteTexto, null, dato.RepresentanteLegal);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaDeclaracionJuradaAutoriza(documento, fuenteTitulo, null);
        }

        private void AddTablaDeclaracionJuradaFirma(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, DeviceRgb color, string nombreRepresentante)
        {
            float borderRadius = 2f;
            float fontSize = 10f;
            string texto1 = "DECLARO BAJO JURAMENTO QUE LOS DATOS SEÑALADOS EXPRESAN LA VERDAD";
            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 })).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell(1, 2).Add(new Paragraph(texto1))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);

            tablaDatos.AddCell(cell);
            fontSize = 7f;
            cell = new Cell().Add(new Paragraph(string.IsNullOrEmpty(nombreRepresentante) ? string.Empty : nombreRepresentante))
                .SetHeight(40f)
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.BOTTOM)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph())
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("APELLIDOS Y NOMBRES"))
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("FIRMA DEL SOLICITANTE / REPRESENTANTE LEGAL"))
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, color));

            documento.Add(tablaDatos);
        }

        private void AddTablaDeclaracionJuradaAutoriza(Document documento, PdfFont fuenteTitulo, DeviceRgb color)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            string texto1 = "Asimismo, autorizo que todo acto administrativo derivado del presente procedimiento, " +
                "se me notifique en el correo electrónico (E-mail) consignado en el presente formulario. (TUO de la Ley N° 27444, numeral 20.4 del artículo 20°)";

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 70, 5, 5, 5, 5, 5, 5 })).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph(texto1))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(""))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("SI"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("X"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph("NO"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(""))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(""))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, color));

            documento.Add(tablaDatos);
        }

        private void AddDatosAclaracion(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, DeviceRgb color)
        {
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaSeccionAclaracion(documento, fuenteTitulo, color);
            this.AddEspacio(documento, fuenteTitulo);
            this.AddTablaDatosAclaracion(documento, fuenteTitulo, fuenteTexto, null);
        }

        private void AddTablaSeccionAclaracion(Document documento, PdfFont fuenteTitulo, DeviceRgb color)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph("ACLARACIÓN SOBRE FALSEDAD DE LA INFORMACIÓN DECLARADA"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, color));

            documento.Add(tablaDatos);
        }

        private void AddTablaDatosAclaracion(Document documento, PdfFont fuenteTitulo, PdfFont fuenteTexto, DeviceRgb color)
        {
            float borderRadius = 2f;
            float fontSize = 7f;
            string texto1 = "TUO de la Ley N° 27444 (numeral 33.3 del artículo 33°)";
            string texto2 = @"“En caso de comprobar fraude o falsedad en la declaración, información o en la documentación presentada por el administrado, la entidad considerará no satisfecha la exigencia respectiva ";
            texto2 += "para todos sus efectos, procediendo a declarar la nulidad del acto administrativo sustentado en dicha declaración, información o documento; e imponer a quien haya empleado esa ";
            texto2 += "declaración, información o documento una multa en favor de la entidad entre cinco y diez Unidades Impositivas Tributarias vigentes a la fecha de pago; y además, si la conducta se adecúa ";
            texto2 += "a los supuestos previstos en el Título XIX Delitos Contra la Fe Pública del Código Penal, ésta deberá ser comunicada al Ministerio Público para que interponga la acción penal correspondiente.”";

            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph(texto1))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell().Add(new Paragraph(texto2))
                .SetFont(fuenteTexto)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, color));

            documento.Add(tablaDatos);
        }

        private void AddTablaSeccionFinal(Document documento, PdfFont fuenteTitulo, DeviceRgb color)
        {
            this.AddEspacio(documento, fuenteTitulo);
            float borderRadius = 2f;
            float fontSize = 9f;
            Table tablaDatos = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph("SÍRVASE COMPLETAR CON LETRA LEGIBLE"))
                .SetFont(fuenteTitulo)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            tablaDatos.SetNextRenderer(new RoundedTableRenderer(tablaDatos, borderRadius, color));

            documento.Add(tablaDatos);
        }

        #endregion

        #region Nueva modalidad de guardado
        public async Task<StatusResponse<long>> GuardarResumenEjecutivo(GuardarFormularioRequestDto request)
        {
            try
            {
                var response = await _formularioRepository.GuardarResumenEjecutivo(request.codMaeSolicitud, request.dataJson, request.usuario);
                return Message.Successful(response);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        public async Task<StatusResponse<long>> ActualizarFormulario(long p_CodMaeSolicitud, string jsonData)
        {
            try
            {
                var response = await _formularioRepository.ActualizarFormulario(p_CodMaeSolicitud, jsonData);
                return Message.Successful(response);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }
        }

        #endregion

        #region Observacion de Opinantes

        public async Task<StatusResponse<List<TransactionListOpinantesResponseDto>>> GetTransactionListOpinantes(int codMaeSolicitud, int codSolicitudExpediente)
        {
            try
            {
                PaginacionResultDto<TransactionListOpinantesResponseDto> paginacion =
                new PaginacionResultDto<TransactionListOpinantesResponseDto>();

                var resultado = _mapper.Map<List<TransactionListOpinantesResponseDto>>(
                await _formularioRepository.GetTransactionListOpinantes(codMaeSolicitud, codSolicitudExpediente));

                return Message.Successful(resultado);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<TransactionListOpinantesResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<DocumentoInstitucionResponseDto>>> ListarDocumentosInstitucion(int codMaeSolicitud, int codSolicitudExpediente)
        {
            try
            {
                PaginacionResultDto<DocumentoInstitucionResponseDto> paginacion = 
                    new PaginacionResultDto<DocumentoInstitucionResponseDto>();

                var resultado = _mapper.Map<List<DocumentoInstitucionResponseDto>>(
                    await _formularioRepository.ListarDocumentosInstitucion(codMaeSolicitud, codSolicitudExpediente));

                return Message.Successful(resultado);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<DocumentoInstitucionResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<DocumentoInstitucionAdjuntosResponseDto>>> ListarDocumentosInstitucionAdjuntos(int codMaeSolicitud, int codSolicitudExpediente)
        {
            try
            {
                PaginacionResultDto<DocumentoInstitucionAdjuntosResponseDto> paginacion = new PaginacionResultDto<DocumentoInstitucionAdjuntosResponseDto>();

                var resultado = _mapper.Map<List<DocumentoInstitucionAdjuntosResponseDto>>(
                    await _formularioRepository.ListarDocumentosInstitucionAdjuntos(codMaeSolicitud, codSolicitudExpediente));

                return Message.Successful(resultado);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<DocumentoInstitucionAdjuntosResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<TransactionResumeDataResponseDto>> GetTransactionResumeData(int codMaeSolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<TransactionResumeDataResponseDto>(
                    await _formularioRepository.GetTransactionResumeData(codMaeSolicitud));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<TransactionResumeDataResponseDto>(ex);
            }
        }

        #endregion


    }
}

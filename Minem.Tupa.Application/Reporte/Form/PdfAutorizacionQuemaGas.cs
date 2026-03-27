using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Minem.Tupa.Application.PDF;
using Minem.Tupa.Dto.AutorizacionQuemaGas;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Tramite;
using Minem.Tupa.IApplication;
using Minem.Tupa.IApplication.Reporte.Form;
using Minem.Tupa.Utils;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Application.Reporte.Form
{
    public class PdfAutorizacionQuemaGas(IAutorizacionQuemaGasApplication autorizacionQuemaGasApplication) : IPdfAutorizacionQuemaGas
    {
        private readonly IAutorizacionQuemaGasApplication _autorizacionQuemaGasApplication = autorizacionQuemaGasApplication;

        private readonly PdfFont FUENTE_TITULO = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
        private readonly PdfFont FUENTE_TEXTO = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        private readonly float FONT_SIZE_7 = 7f;
        private readonly float FONT_SIZE_12_5 = 12.5f;
        private readonly float FONT_SIZE_10 = 10f;

        public async Task<StatusResponse<DescargarPlantillaDiaResponseDto>> Generar(int IdSolicitud)
        {
            var response = new StatusResponse<DescargarPlantillaDiaResponseDto>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (PdfWriter writer = new(memoryStream))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document documento = new Document(pdf);
                        documento.SetMargins(20, 50, 20, 50);

                        //PdfFont fuenteTitulo = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                        //PdfFont fuenteTexto = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                        DeviceRgb colorSeccion = new DeviceRgb(211, 211, 211);

                        var responseInforme = await _autorizacionQuemaGasApplication.ObtenerInforme(IdSolicitud);
                        var datosInforme = responseInforme.Data;

                        AddTituloAnexo(documento, IdSolicitud);
                        AddSeccionIntroduccion(documento, datosInforme);
                        AddSeccionAnalisis(documento, datosInforme);
                        AddSeccionJustificacion(documento, datosInforme);
                        AddSeccionProgramaQuemado(documento, datosInforme);
                        AddSeccionAcciones(documento, datosInforme);
                        AddSeccionConclusiones(documento, datosInforme);
                        AddSeccionAnexos(documento, datosInforme);
                        AddSeccionFirmas(documento);
                        
                        documento.Close();
                    }
                }

                byte[] pdfBytes = memoryStream.ToArray();
                response = new StatusResponse<DescargarPlantillaDiaResponseDto>
                {
                    Success = true,
                    Data = new DescargarPlantillaDiaResponseDto
                    {
                        base64Documento = Convert.ToBase64String(pdfBytes)
                    }
                };
            }
            return response;
        }
        private void AddTituloAnexo(Document documento, int IdSolicitud)
        {
            documento.Add(new Paragraph(string.Format( "INFORME TECNICO N° {0}", IdSolicitud))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(FONT_SIZE_12_5)
                .SetTextAlignment(TextAlignment.CENTER));
        }

        private void AddSeccionIntroduccion(Document documento, InformeJustificacionDto datos)
        {
            AddEspacio(documento);
            documento.Add(new Paragraph("1. INTRODUCCIÓN")
                .SetFont(FUENTE_TITULO)
                .SetFontSize(FONT_SIZE_10)
                );


            AddSeccion1_1(documento, datos);
            AddSeccion1_2(documento, datos);
            AddSeccion1_3(documento, datos);
        }

        private void AddSeccion1_1(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("1.1.");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Antecedentes");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(datos.antecedentes) ? string.Empty : datos.antecedentes))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            documento.Add(tablaDatos);
        }

        private void AddSeccion1_2(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("1.2.");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Motivo de la solicitud de Quema de Gas");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            foreach (var motivos in datos.motivos)
            {
                string strMotivo = string.Join(" / ", motivos.Select(s => s.descripcion).ToList());

                string strInfoAdicional = motivos.LastOrDefault().informacionAdicional;

                long infoMotivoId = motivos.DefaultIfEmpty(new MotivoInformeDto()).LastOrDefault().infoMotivoId;

                cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);

                cell = new Cell()
                    .Add(new Paragraph(""))
                    .SetFont(FUENTE_TITULO)
                    .SetFontSize(fontSize)
                    .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);



                cell = new Cell()
                    .Add(new Paragraph(strMotivo))
                    .SetFont(FUENTE_TITULO)
                    .SetFontSize(fontSize)
                    .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);



                cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);

                cell = new Cell()
                    .Add(new Paragraph(""))
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);



                cell = new Cell()
                    .Add(new Paragraph(strInfoAdicional))
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);

                var facilidades = datos.facilidades[infoMotivoId];

                int nroFila = 1;
                foreach (var facilidad in facilidades)
                {
                    cell = new Cell()
                        .Add(new Paragraph(""))
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetBorder(Border.NO_BORDER);
                    tablaDatos.AddCell(cell);

                    cell = new Cell()
                        .Add(new Paragraph(""))
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetBorder(Border.NO_BORDER);
                    tablaDatos.AddCell(cell);



                    cell = new Cell()
                        .Add(new Paragraph(string.Format("{0}. {1}", nroFila, facilidad.descripcion)))
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetBorder(Border.NO_BORDER);
                    tablaDatos.AddCell(cell);
                    nroFila++;
                }

            }


            documento.Add(tablaDatos);
        }

        private void AddSeccion1_3(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("1.3.");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Objetivos");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(datos.objetivo) ? string.Empty : datos.objetivo))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            documento.Add(tablaDatos);
        }

        private void AddSeccionAnalisis(Document documento, InformeJustificacionDto datos)
        {
            AddEspacio(documento);
            documento.Add(new Paragraph("2. ANALISIS")
                .SetFont(FUENTE_TITULO)
                .SetFontSize(FONT_SIZE_10)
                );


            AddSeccion2_1(documento, datos);
            AddSeccion2_2(documento, datos);
            AddSeccion2_3(documento, datos);
            AddSeccion2_4(documento, datos);
            AddSeccion2_5(documento, datos);
        }

        private void AddSeccion2_1(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("2.1.");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Trabajos a Realizar");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(datos.trabajoRealizar) ? string.Empty : datos.trabajoRealizar))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            documento.Add(tablaDatos);
        }

        private void AddSeccion2_2(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("2.2.");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Facilidades Existentes");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(datos.facilidadesExistentes) ? string.Empty : datos.facilidadesExistentes))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            documento.Add(tablaDatos);
        }

        private void AddSeccion2_3(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("2.3.");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Procedimiento de Quema de Gas");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(string.IsNullOrEmpty(datos.procedimiento) ? string.Empty : datos.procedimiento))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            documento.Add(tablaDatos);
        }

        private void AddSeccion2_4(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("2.4.");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Quemador");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);


            documento.Add(tablaDatos);

            tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 5, 23, 24, 24, 24 })).SetWidth(UnitValue.CreatePercentValue(100));

            paragraph = new Paragraph("N°");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Nombre");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Marca");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Serie");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Fabricante");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);
            int nro = 1;
            foreach (var quemador in datos.quemadores)
            {
                paragraph = new Paragraph(nro.ToString());
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(quemador.nombre);
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(quemador.marca);
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(quemador.serie);
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(quemador.fabricante);
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                nro++;
            }

            documento.Add(tablaDatos);
        }
        public class AqgSelectorItem
        {
            public int Id { get; set; }
            public string Label { get; set; }
        }
        private void AddSeccion2_5(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);


            var opcionesBalance = new List<AqgSelectorItem>
            {
                new AqgSelectorItem { Id = 1, Label = "Último mes" },
                new AqgSelectorItem { Id = 2, Label = "Últimos 6 meses" },
                new AqgSelectorItem { Id = 3, Label = "Último año" }
            };

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("2.5.");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Balance de Gas");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);


            documento.Add(tablaDatos);

            tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 10, 15, 15, 15, 15, 15, 15 })).SetWidth(UnitValue.CreatePercentValue(100));

            paragraph = new Paragraph("Periodo");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Producción oil (BPD)");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Producción gas (MPCD)");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Producción agua (BPD)");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Consumo gas (MPCD)");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Inyección gas (MPCD)");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("Quema gas (MPCD)");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            int nro = 1;
            string periodo = string.Empty;
            foreach (var balance in datos.balance)
            {
                periodo = opcionesBalance.Where(w => w.Id == (int)balance.periodo).First().Label;

                paragraph = new Paragraph(periodo);
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(balance.producOil.ToString("N2"));
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(balance.producGas.ToString("N2"));
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(balance.producAgua.ToString("N2"));
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(balance.consumoGas.ToString("N2"));
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(balance.inyeccionGas.ToString("N2"));
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(balance.quemaGas.ToString("N2"));
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);
                nro++;
            }

            documento.Add(tablaDatos);
        }


        private void AddSeccionJustificacion(Document documento, InformeJustificacionDto datos)
        {
            AddEspacio(documento);
            AddEspacio(documento);
            documento.Add(new Paragraph("3. JUSTIFICACIÓN")
                .SetFont(FUENTE_TITULO)
                .SetFontSize(FONT_SIZE_10)
                );


            AddSeccion3_1(documento, datos);
        }

        private void AddSeccion3_1(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            this.AddEspacio(documento);

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("La presente solicitud de autorización de quema de gas natural se encuentra justificada debido a la imposibilidad de no poder realizar las siguientes actividades:");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            #region "Reinyeccion"
            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("- Reinyección");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph(datos.porqueNoInyectar);
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);
            #endregion

            #region "Comercialización"
            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("- Comercialización");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph(datos.porqueNoComercializar);
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);
            #endregion

            #region "Uso en operaciones (consumo propio)"
            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("- Uso en operaciones (consumo propio)");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph(datos.porqueNoUtilizar);
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);
            #endregion

            documento.Add(tablaDatos);
        }

        private void AddSeccionProgramaQuemado(Document documento, InformeJustificacionDto datos)
        {
            AddEspacio(documento);
            documento.Add(new Paragraph("4. PROGRAMA DE QUEMADO")
                .SetFont(FUENTE_TITULO)
                .SetFontSize(FONT_SIZE_10)
                );

            float fontSize = FONT_SIZE_10;
            Table tablaDatos; //= new Table(UnitValue.CreatePercentArray(new float[] { 10, 15, 15, 15, 15, 15 })).SetWidth(UnitValue.CreatePercentValue(100));
            Paragraph paragraph = new Paragraph("");

            int nroFila = 1, nroCronograma = 1, nroFilaCronograma = 1;
            string actividad = string.Empty;
            string keyCronograma = string.Empty;
            bool salirBucle = false;
            long infoMotivoId = 0;
            string descripcionFacilidad = string.Empty;
            string descripcionCronograma = string.Empty;

            float totalVolGasQuemado = 0;
            float totalVolLiquidoQuemado = 0;

            float totalCronoVolGasQuemado = 0;
            float totalCronoVolLiquidoQuemado = 0;

            Cell cell;

            foreach (var cronograma in datos.cronograma)
            {
                AddEspacio(documento);
                if (datos.quemaLiquido == 1)
                {
                    tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 2, 24, 18, 18, 19, 19 })).SetWidth(UnitValue.CreatePercentValue(100));
                }
                else
                {
                    tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 2, 22, 19, 19, 19 })).SetWidth(UnitValue.CreatePercentValue(100));
                }
                salirBucle = false;
                keyCronograma = cronograma.Key;
                foreach (var facilidades in datos.facilidades)
                {
                    foreach (var facilidad in facilidades.Value)
                    {
                        if (facilidad.infoMotivoFacilidadId.ToString() == keyCronograma)
                        {
                            descripcionFacilidad = facilidad.descripcion;
                            salirBucle = true;
                            infoMotivoId = facilidad.infoMotivoId;
                            break;
                        }
                    }

                    if (salirBucle)
                    {
                        break;
                    }

                }

                foreach (var motivos in datos.motivos)
                {
                    var existeMotivo = motivos.Exists(w => w.infoMotivoId == infoMotivoId);
                    if (existeMotivo)
                    {
                        var listaMotivos = motivos.Select(s => s.descripcion).ToList();
                        descripcionFacilidad += string.Format(" - ({0})", string.Join(" / ", listaMotivos));
                        break;
                    }
                }

                descripcionCronograma = string.Format("Cronograma N° {0} - {1}", nroCronograma, descripcionFacilidad);

                paragraph = new Paragraph(descripcionCronograma);
                int totalColumnasCronograma = datos.quemaLiquido == 1 ? 6 : 5;
                cell = new Cell(1, totalColumnasCronograma)
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph("N°");
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TITULO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);


                paragraph = new Paragraph("Actividad/Pozo/Activo");
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TITULO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph("Fecha Inicio");
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TITULO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph("Fecha Fin");
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TITULO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph("Vol. a quemar de gas (MPCD)");
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TITULO)
                    .SetFontSize(fontSize)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                tablaDatos.AddCell(cell);

                if (datos.quemaLiquido == 1)
                {
                    paragraph = new Paragraph("Vol. a quemar de líquido (BPD)");
                    cell = new Cell()
                        .Add(paragraph)
                        .SetFont(FUENTE_TITULO)
                        .SetFontSize(fontSize)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                    tablaDatos.AddCell(cell);
                }

                nroFilaCronograma = 1;
                totalCronoVolGasQuemado = 0;
                totalCronoVolLiquidoQuemado = 0;
                int totalFilasCronograma = cronograma.Value.Count;

                if (totalFilasCronograma == 0)
                {
                    paragraph = new Paragraph("No se agregó cronograma");
                    cell = new Cell(1, totalColumnasCronograma)
                        .Add(paragraph)
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(9f)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                    tablaDatos.AddCell(cell);
                }

                foreach (var item in cronograma.Value)
                {
                    paragraph = new Paragraph(nroFilaCronograma.ToString());
                    cell = new Cell()
                        .Add(paragraph)
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                    tablaDatos.AddCell(cell);

                    paragraph = new Paragraph(descripcionFacilidad);
                    cell = new Cell()
                        .Add(paragraph)
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                    tablaDatos.AddCell(cell);

                    paragraph = new Paragraph(item.fechaInicio);
                    cell = new Cell()
                        .Add(paragraph)
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                    tablaDatos.AddCell(cell);

                    paragraph = new Paragraph(item.fechaFin);
                    cell = new Cell()
                        .Add(paragraph)
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                    tablaDatos.AddCell(cell);

                    paragraph = new Paragraph(item.volGasQuemado.ToString("N2"));
                    cell = new Cell()
                        .Add(paragraph)
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                    tablaDatos.AddCell(cell);

                    if (datos.quemaLiquido == 1)
                    {
                        paragraph = new Paragraph(item.volLiquidoQuemado.HasValue ? item.volLiquidoQuemado.Value.ToString("N2") : string.Empty);
                        cell = new Cell()
                            .Add(paragraph)
                            .SetFont(FUENTE_TEXTO)
                            .SetFontSize(fontSize)
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                        tablaDatos.AddCell(cell);
                    }

                    totalCronoVolGasQuemado += item.volGasQuemado;
                    totalCronoVolLiquidoQuemado += item.volLiquidoQuemado.HasValue ? item.volLiquidoQuemado.Value : 0;

                    if (nroFilaCronograma == totalFilasCronograma)
                    {
                        paragraph = new Paragraph("Total");
                        cell = new Cell(1, 4)
                        .Add(paragraph)
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                        tablaDatos.AddCell(cell);

                        paragraph = new Paragraph(totalCronoVolGasQuemado.ToString("N2"));
                        cell = new Cell()
                        .Add(paragraph)
                        .SetFont(FUENTE_TEXTO)
                        .SetFontSize(fontSize)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                        tablaDatos.AddCell(cell);

                        if (datos.quemaLiquido == 1)
                        {
                            paragraph = new Paragraph(totalCronoVolLiquidoQuemado.ToString("N2"));
                            cell = new Cell()
                            .Add(paragraph)
                            .SetFont(FUENTE_TEXTO)
                            .SetFontSize(fontSize)
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                            tablaDatos.AddCell(cell);
                        }
                    }

                    nroFilaCronograma++;
                    nroFila++;
                }
                totalVolGasQuemado += totalCronoVolGasQuemado;
                totalVolLiquidoQuemado += totalCronoVolLiquidoQuemado;
                nroCronograma++;
                documento.Add(tablaDatos);
            }

            tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 60, 35, 5 })).SetWidth(UnitValue.CreatePercentValue(100));
            paragraph = new Paragraph("Volumen total de gas autorizado para quema:");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph(totalVolGasQuemado.ToString("N2"));
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            paragraph = new Paragraph("");
            cell = new Cell()
                .Add(paragraph)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            if (datos.quemaLiquido == 1)
            {
                paragraph = new Paragraph("Volumen total de líquido autorizado para quema:");
                cell = new Cell()
                   .Add(paragraph)
                   .SetFont(FUENTE_TITULO)
                   .SetFontSize(fontSize)
                   .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph(totalVolLiquidoQuemado.ToString("N2"));
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);

                paragraph = new Paragraph("");
                cell = new Cell()
                    .Add(paragraph)
                    .SetFont(FUENTE_TEXTO)
                    .SetFontSize(fontSize)
                    .SetBorder(Border.NO_BORDER);
                tablaDatos.AddCell(cell);
            }

            documento.Add(tablaDatos);

            //AddSeccion3_1(documento, datos);
        }

        private void AddSeccionAcciones(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            AddEspacio(documento);
            documento.Add(new Paragraph("5. ACCIONES PARA EVITAR QUEMAR NUEVAMENTE GAS NATURAL")
                .SetFont(FUENTE_TITULO)
                .SetFontSize(FONT_SIZE_10)
                );

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));

            Table tablaDatosAcciones = new Table(UnitValue.CreatePercentArray(new float[] { 2, 98 })).SetWidth(UnitValue.CreatePercentValue(100));
            //tablaDatosAcciones.AddCell(new Cell().Add(new Paragraph("N°")).SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));
            //tablaDatosAcciones.AddCell(new Cell().Add(new Paragraph("Acciones a realizar")).SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

            int nroFila = 1;
            foreach (var accion in datos.acciones)
            {
                tablaDatosAcciones.AddCell(new Cell().Add(new Paragraph(string.Format("{0}.", nroFila.ToString()))).SetBorder(Border.NO_BORDER));
                tablaDatosAcciones.AddCell(new Cell().Add(new Paragraph(accion.descripcion)).SetBorder(Border.NO_BORDER));
                nroFila++;
            }

            Paragraph paragraph = new Paragraph("");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(tablaDatosAcciones)
                //.Add(new Paragraph("wsdee"))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            documento.Add(tablaDatos);

        }

        private void AddSeccionConclusiones(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            AddEspacio(documento);
            documento.Add(new Paragraph("6. CONCLUSIONES")
                .SetFont(FUENTE_TITULO)
                .SetFontSize(FONT_SIZE_10)
                );

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));

            Table tablaDatosConclusiones = new Table(UnitValue.CreatePercentArray(new float[] { 2, 98 })).SetWidth(UnitValue.CreatePercentValue(100));
            //tablaDatosConclusiones.AddCell(new Cell().Add(new Paragraph("N°")).SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));
            //tablaDatosConclusiones.AddCell(new Cell().Add(new Paragraph("Conclusiones")).SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

            int nroFila = 1;
            foreach (var objetivo in datos.objetivos)
            {
                tablaDatosConclusiones.AddCell(new Cell().Add(new Paragraph(string.Format("{0}.", nroFila.ToString()))).SetBorder(Border.NO_BORDER));
                tablaDatosConclusiones.AddCell(new Cell().Add(new Paragraph(objetivo.descripcion)).SetBorder(Border.NO_BORDER));
                nroFila++;
            }

            Paragraph paragraph = new Paragraph("");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(tablaDatosConclusiones)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            documento.Add(tablaDatos);

        }

        private void AddSeccionAnexos(Document documento, InformeJustificacionDto datos)
        {
            float fontSize = FONT_SIZE_10;
            AddEspacio(documento);
            documento.Add(new Paragraph("7. ANEXOS")
                .SetFont(FUENTE_TITULO)
                .SetFontSize(FONT_SIZE_10)
                );

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 3, 94 })).SetWidth(UnitValue.CreatePercentValue(100));

            Table tablaDatosAdjuntos = new Table(UnitValue.CreatePercentArray(new float[] { 3, 97 })).SetWidth(UnitValue.CreatePercentValue(100));
            //tablaDatosConclusiones.AddCell(new Cell().Add(new Paragraph("N°")).SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));
            //tablaDatosConclusiones.AddCell(new Cell().Add(new Paragraph("Conclusiones")).SetBorder(new SolidBorder(ColorConstants.BLACK, 1)));

            int nroFila = 1;
            foreach (var adjuntos in datos.adjuntos)
            {
                foreach (var adjunto in adjuntos.Value)
                {
                    tablaDatosAdjuntos.AddCell(new Cell().Add(new Paragraph(string.Format("{0}. ", nroFila.ToString()))).SetBorder(Border.NO_BORDER));
                    tablaDatosAdjuntos.AddCell(new Cell().Add(new Paragraph(adjunto.nombreArchivo)).SetBorder(Border.NO_BORDER));
                    nroFila++;
                }
            }

            Paragraph paragraph = new Paragraph("");
            Cell cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TITULO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(tablaDatosAdjuntos)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            documento.Add(tablaDatos);

        }

        private void AddSeccionFirmas(Document documento)
        {
            float fontSize = FONT_SIZE_10;

            Table tablaDatos = new Table(UnitValue.CreatePercentArray(new float[] { 5f, 40, 10, 5, 40 })).SetWidth(UnitValue.CreatePercentValue(100));

            Paragraph paragraph = new Paragraph("");
            Cell cell = new Cell(1, 5)
                .Add(new Paragraph(""))
                .SetHeight(100f)
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("Firma"))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorderBottom(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));

            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("Firma"))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorderBottom(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            //FILA 3
            cell = new Cell()
                .Add(new Paragraph("Ing."))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorderTop(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("Ing."))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorderTop(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            //FILA 4
            cell = new Cell()
                .Add(new Paragraph("CIP:"))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorderTop(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("CIP:"))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorderTop(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
            tablaDatos.AddCell(cell);

            //FILA 5

            cell = new Cell(1, 5)
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetHeight(5f)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);


            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("Ingresar nombre, apellidos y CIP del ingeniero colegiado y habilitado"))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph(""))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);

            cell = new Cell()
                .Add(new Paragraph("Ingresar nombre, apellidos y CIP del ingeniero colegiado y habilitado (Opcional)"))
                .SetFont(FUENTE_TEXTO)
                .SetFontSize(fontSize)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tablaDatos.AddCell(cell);


            documento.Add(tablaDatos);
        }

        private void AddEspacio(Document documento, float Height = 0.2f)
        {
            Table espacio = new Table(1).SetWidth(UnitValue.CreatePercentValue(100));
            Cell cell = new Cell().Add(new Paragraph(""))
                .SetHeight(Height)
                .SetFont(FUENTE_TITULO)
                .SetFontSize(0.2f)
                .SetBorder(Border.NO_BORDER);
            espacio.SetBorder(Border.NO_BORDER);
            espacio.AddCell(cell);
            documento.Add(espacio);
        }
    }
}

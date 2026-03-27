using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minem.Tupa.Dto;
using Minem.Tupa.Dto.Documento;
using Minem.Tupa.Dto.EstudiosPresentados;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.Dto.Its;
using Minem.Tupa.Dto.Svc.Notificacion;
using Minem.Tupa.Entity.EstudiosPresentados;
using Minem.Tupa.Entity.Its;
using Minem.Tupa.Entity.Tupa;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Proxy.Interface;
using Minem.Tupa.Utils;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO.Compression;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Transactions;
using static System.Collections.Specialized.BitVector32;


namespace Minem.Tupa.Application
{
    public class EstudiosPresentadosApplication(IMapper mapper, IConfiguration configuration, IEstudiosPresentadosRepository estudiosPresentadosRepository,
        INotificacionService notificacionService, IFormularioRepository formularioRepository, 
        ILaserficheService laserficheService,
        ITupaRepository tupaRepository,
        ITramiteRepository tramiteRepository,
        IHttpContextAccessor contextAccessor, IItsRepository itsRepository) : IEstudiosPresentadosApplication
    {
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;
        private readonly IEstudiosPresentadosRepository _estudiosPresentadosRepository = estudiosPresentadosRepository;
        private readonly INotificacionService _notificacionService = notificacionService;
        private readonly IFormularioRepository _formularioRepository = formularioRepository;
        private readonly ILaserficheService _laserficheService = laserficheService;
        private readonly ITupaRepository _tupaRepository = tupaRepository;
        private readonly ITramiteRepository _tramiteRepository = tramiteRepository;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly IItsRepository _itsRepository = itsRepository;
    
        public async Task<StatusResponse<PaginacionResultDto<BandejaEstudiosPresentadosResponseDto>>> GetBandeja(BandejaEstudiosPresentadosRequestDto request)
        {
            try
            {
                PaginacionResultDto<BandejaEstudiosPresentadosResponseDto> paginacion = new PaginacionResultDto<BandejaEstudiosPresentadosResponseDto>();
                var requestEntity = _mapper.Map<BandejaEstudiosPresentadosRequest_Entity>(request);
                var responseEntity = await _estudiosPresentadosRepository.GetBandeja(requestEntity);
                responseEntity = responseEntity ?? new List<BandejaEstudiosPresentadosResponse_Entity>();
                paginacion.Result = _mapper.Map<List<BandejaEstudiosPresentadosResponseDto>>(responseEntity);
                paginacion.TotalRows = !responseEntity.Any() ? 0 : responseEntity.First().TotalRows;
                paginacion.TotalPage = !responseEntity.Any() ? 0 : responseEntity.First().PageCount;
                paginacion.CurrentPage = request.CurrentPage;
                paginacion.PageSize = request.PageSize;
                paginacion.First = !responseEntity.Any() ? 0 : responseEntity.First().First;
                paginacion.Last = !responseEntity.Any() ? 0 : responseEntity.First().Last;
                return Message.Successful(paginacion);
            }
            catch (Exception ex)
            {
                return Message.Exception<PaginacionResultDto<BandejaEstudiosPresentadosResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<SituacionResponseDto>>> GetSituacion()
        {
            try
            {
                var respuesta = _mapper.Map<List<SituacionResponseDto>>(await _estudiosPresentadosRepository.GetSituacion());
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<SituacionResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<TipoEstudioResponseDto>>> GetTipoEstudio()
        {
            try
            {
                var respuesta = _mapper.Map<List<TipoEstudioResponseDto>>(await _estudiosPresentadosRepository.GetTipoEstudio());
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<TipoEstudioResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<TipoEstudiosResponseDto>>> GetListarTipoEstudio()
        {
            try
            {
                var respuesta = _mapper.Map<List<TipoEstudiosResponseDto>>(await _estudiosPresentadosRepository.GetListarTipoEstudio());
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<TipoEstudiosResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<List<TipoEstudiosTupaResponseDto>>> GetListarTipoEstudioTupa(string CodMaeTupa)
        {
            try
            {
                var respuesta = _mapper.Map<List<TipoEstudiosTupaResponseDto>>(await _estudiosPresentadosRepository.GetListarTipoEstudioTupa(CodMaeTupa));
                return Message.Successful(respuesta);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<TipoEstudiosTupaResponseDto>>(ex);
            }
        }
        public async Task<StatusResponse<long>> GuardarAporte(SolicitudAporteRequestDto request)
        {
            long idSolicitudAporte = 0;

            if (request.NombresApellidos == "" || request.NombresApellidos == null)
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                var claims = principal.Claims.FirstOrDefault();
                var nombre = principal.Claims.FirstOrDefault(c => c.Type == "nombre").Value;
                var nroDoc = principal.Claims.FirstOrDefault(c => c.Type == "nroDoc").Value;
                var ruc = principal.Claims.FirstOrDefault(c => c.Type == "ruc").Value;
                var tipoPersona = principal.Claims.FirstOrDefault(c => c.Type == "tipoPersona").Value;
                var tipoDoc = principal.Claims.FirstOrDefault(c => c.Type == "tipoDoc").Value;
                var razonSocial = principal.Claims.FirstOrDefault(c => c.Type == "razonSocial").Value;

                if (razonSocial == "")
                {
                    request.NombresApellidos = nombre;
                }
                else {
                    request.NombresApellidos = razonSocial;
                }               

                request.NumeroDocumento = nroDoc;
                request.Ruc = ruc;
                request.TipoPersona = tipoPersona;
                request.TipoDocumento = tipoDoc;
            }
                
            try
            {
              
                    


                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    idSolicitudAporte = await _estudiosPresentadosRepository.GuardarAporte(request.IdSolicitud, request.IdSolicitudExpediente, request.Descripcion, request.IdUser, request.CodigoCelular, request.CodigoCorreoElectronico,
            request.CorreoElectronico, request.NombresApellidos, request.NumeroCelular, request.NumeroDocumento, request.Ruc, request.TipoDocumento, request.TipoPersona, request.TipoValidacion);

                    foreach (var item in request.Documentos)
                    {
                        await _estudiosPresentadosRepository.GuardarDetalleAporte(idSolicitudAporte, request.IdUser, item.Id);
                    }
                    ts.Complete();
                }
                return Message.Successful(idSolicitudAporte);
            }
            catch (Exception ex)
            {
                return Message.Exception<long>(ex);
            }

        }

        public async Task<StatusResponse<List<DocumentoDespachadoResponseDto>>> GetDocumentosDespachados(int idSolicitud)
        {
            try
            {
                var respuesta = await _notificacionService.GetDocumentosDespachados(idSolicitud);


               // var denominacion = await _tramiteRepository.GetNombreTipoDocumento(idSolicitud, Id);


                var documentos = respuesta.Data;
         
                foreach (var doc in documentos)
                {
                    if (doc.Id.HasValue)
                    {                       
                        var denominacion = await _tramiteRepository.GetNombreTipoDocumento(idSolicitud, doc.Id.Value);
                        doc.Denominacion = denominacion.Denominacion ?? string.Empty;
                    }
                }


                return Message.Successful(documentos);
            }
            catch (Exception ex)
            {
                return Message.Exception<List<DocumentoDespachadoResponseDto>>(ex);
            }
        }

        public async Task<StatusResponse<byte[]>> DescargaEnBloqueZip(DescargaBloqueRequestDto request)
        {
            try
            {
                byte[] zipBytes = null;

                var responseFormularioDia = await _formularioRepository.ObtenerFormularioDia(request.IdSolicitud);
                responseFormularioDia = responseFormularioDia ?? new Entity.Formulario.USP_S_OBTENER_FORMULARIO_DIA_Response_Entity();
                //_logService.WriteLog("ObtenerFormularioDia:  ");
                FormularioDiaResponseDto formularioDia;
                try
                {
                    formularioDia = JsonSerializer.Deserialize<FormularioDiaResponseDto>(responseFormularioDia.dataJson);
                }
                catch (Exception)
                {
                    formularioDia = new FormularioDiaResponseDto();
                }

                var listaEstructura = await this.ObtenerData(request.IdSolicitud);
                string nombreProp = "DescripcionProyecto.DescripcionEtapas.TopSoil";
                //object valor = ReflectionHelper.GetNestedPropertyValueFast(formularioDia, nombreProp);

                //listaNivelEnEstructura.ForEach(f => f.Descripcion = Regex.Replace(f.Descripcion, @"[^a-zA-Z0-9\s]", " "));
                listaEstructura.ForEach(f => f.Descripcion = Regex.Replace(f.Descripcion, @"[\/:*?""<>|]", " "));

                List<DirectoriosComprimidosDto> directorios = CrearEstructuraJerarquicaCarpetas(listaEstructura, 0, "", formularioDia, request.ListaIdDocumentos);

                zipBytes = await ComprimirArchivosYCarpetaEnMemoria(directorios);
                var response = Message.Successful(zipBytes);
                response.Message = string.Format("DIA_{0}.zip", DateTime.Now.ToString("ddMMyyyyHH24mmss"));
                return response;
            }
            catch (Exception ex)
            {
                //_logService.WriteLog("DescargaEnBloque:  " + ex.Message);
                return Message.Exception<byte[]>(ex);
            }

        }

        private async Task<List<EstructuraIndiceDto>> ObtenerData(int idSolicitud)
        {
            //aqui nos dice q tipo de tupa es
            DescargaBloqueResponseDto oDescargaBloqueResponseDto = new DescargaBloqueResponseDto();
            oDescargaBloqueResponseDto.DatosFormulario = _mapper.Map<DatosFormularioDto>(await _tupaRepository.ObtenerSolicitudPorCodigo(idSolicitud));

            List<EstructuraIndiceDto> listaEstructuraIndice = new List<EstructuraIndiceDto>();

            var responseFormularioDia = await _formularioRepository.ObtenerFormularioDia(idSolicitud);
            responseFormularioDia = responseFormularioDia ?? new Entity.Formulario.USP_S_OBTENER_FORMULARIO_DIA_Response_Entity();
            //_logService.WriteLog("ObtenerFormularioDia:  ");
            FormularioDiaResponseDto formularioDia;
            try
            {
                formularioDia = JsonSerializer.Deserialize<FormularioDiaResponseDto>(responseFormularioDia.dataJson);
            }
            catch (Exception)
            {
                formularioDia = null;
            }
            List<SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity> archivosITS = null;
            var listaEstructura = await _tupaRepository.ListarEstructuraCapituloAdjuntos();
            if (oDescargaBloqueResponseDto.DatosFormulario.TipoIga == "ITS")
            {
                // Filtrar usando Array.FindAll
                listaEstructura = listaEstructura
                                    .Where(x => x.Tupa == 181)
                                    .ToList();

                //obtenemos proyecto 
                SP_OBTENER_ITS_PROYECTO_Request_Entity proyectoEntity = new SP_OBTENER_ITS_PROYECTO_Request_Entity();
                proyectoEntity.codMaeSolicitud = idSolicitud;
                var proyecto = await _itsRepository.ObtenerProyecto(proyectoEntity);
                //obtenemos archivos
                SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity archivosEntity = new SP_OBTENER_ITS_PROYECTO_ARCHIVO_Request_Entity();
                archivosEntity.idProyecto = proyecto.FirstOrDefault().idProyecto;
                archivosITS = await _itsRepository.ObtenerProyectoArchivos(archivosEntity);
            }
            else
            {
                listaEstructura = listaEstructura
                                    .Where(x => x.Tupa != 181)
                                    .ToList();
            }
            List<EstructuraCapituloAdjuntosResponse_Entity> listaNivelEnEstructura = listaEstructura.Where(w => w.MostrarEnEstructura).ToList();
            List<EstructuraCapituloAdjuntosResponse_Entity> listaEstructuraSoloAdjuntos = listaEstructura.Where(w => w.EsAdjunto == 1).ToList();

            int totalFilasEstructura = listaNivelEnEstructura.Count;

            EstructuraIndiceDto estructuraIndiceDto;
            string tipoDato = string.Empty;

            List<EstructuraCapituloAdjuntosResponse_Entity> listaAdjuntosFiltrados = new List<EstructuraCapituloAdjuntosResponse_Entity>();
            int nroCorrelativo = 1;
            foreach (var estructura in listaNivelEnEstructura)
            {
                estructuraIndiceDto = new EstructuraIndiceDto();
                estructuraIndiceDto.Descripcion = string.Format("{0}.-{1}", estructura.Codigo, estructura.Descripcion);
                estructuraIndiceDto.Nivel = estructura.Nivel;
                estructuraIndiceDto.Codigo = estructura.Codigo;
                estructuraIndiceDto.EsUltHijo = estructura.EsUltHijo;
                estructuraIndiceDto.IdEstructuraCapituloAdjuntos = estructura.IdEstructuraCapituloAdjuntos;
                estructuraIndiceDto.IdNewEstructuraCapituloAdjuntos = estructura.IdNewEstructuraCapituloAdjuntosPadre;
                estructuraIndiceDto.IdEstructuraCapituloAdjuntosPadre = estructura.IdEstructuraCapituloAdjuntosPadre;
                estructuraIndiceDto.Tupa = estructura.Tupa;

                listaAdjuntosFiltrados = new List<EstructuraCapituloAdjuntosResponse_Entity>();

                listaAdjuntosFiltrados = listaEstructuraSoloAdjuntos.Where(w => w.IdNewEstructuraCapituloAdjuntosPadre == estructura.IdEstructuraCapituloAdjuntos).ToList();
                estructuraIndiceDto.TieneAdjunto = !string.IsNullOrEmpty(estructura.PropiedadJson) || (listaAdjuntosFiltrados != null && listaAdjuntosFiltrados.Any());

                estructuraIndiceDto.Adjuntos = new List<Adjunto>();
                foreach (var adjuntoFiltrado in listaAdjuntosFiltrados)
                {
                    List<string> listaPropiedadJson = new List<string>();
                    listaPropiedadJson = adjuntoFiltrado.PropiedadJson.Split(",").Select(s => s.Trim()).ToList();

                    foreach (var propiedadJson in listaPropiedadJson)
                    {
                        if (oDescargaBloqueResponseDto.DatosFormulario.TipoIga == "ITS")
                        {
                            if (!string.IsNullOrEmpty(propiedadJson))
                            {
                                var archivosFiltrados = string.IsNullOrEmpty(propiedadJson)
                                                ? archivosITS  // Si propiedadJson está vacío, tomar todos los archivos
                                                : archivosITS.Where(archivo =>
                                                      archivo.seccion.HasValue &&
                                                      archivo.seccion.Value.ToString() == propiedadJson);

                                int nroCorrelativo123 = 1;
                                
                                foreach (var archivo in archivosFiltrados)
                                {
                                    estructuraIndiceDto.Adjuntos.Add(new Adjunto
                                    {
                                        Id =  (int)archivo.idArchivo.Value,
                                        Nombre = string.Format("{0}.pdf", archivo.idArchivo ?? 0),
                                        Nro = nroCorrelativo123++
                                    });
                                }

                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(propiedadJson))
                            {
                                object objValor = ReflectionHelper.GetNestedPropertyValueFast(formularioDia, propiedadJson);
                                tipoDato = ReflectionHelper.GetValueType(objValor);
                                if (tipoDato.Equals(Constante.TipoDatoReflection.LISTA))
                                {
                                    var listaAdjuntos = (List<Adjunto>)objValor;
                                    listaAdjuntos.ForEach(f => f.Nro = nroCorrelativo++);

                                    estructuraIndiceDto.Adjuntos.AddRange(listaAdjuntos);
                                }
                                else if (tipoDato.Equals(Constante.TipoDatoReflection.ENTERO))
                                {
                                    var entero = (int)objValor;

                                }
                                else if (tipoDato.Equals(Constante.TipoDatoReflection.CADENA))
                                {
                                    int idAdjunto;
                                    bool esEntero = int.TryParse(objValor.ToString(), out idAdjunto);
                                    if (esEntero)
                                    {
                                        estructuraIndiceDto.Adjuntos.Add(new Adjunto
                                        {
                                            Id = idAdjunto,
                                            Nombre = string.Format("{0}.pdf", idAdjunto),
                                            Nro = nroCorrelativo++

                                        });
                                    }
                                }
                            }
                        }
                    }

                }
                listaEstructuraIndice.Add(estructuraIndiceDto);

            }

            return listaEstructuraIndice;
        }
         

        private List<DirectoriosComprimidosDto> CrearEstructuraJerarquicaCarpetas(List<EstructuraIndiceDto> lista, int idPadre, string prefijo, FormularioDiaResponseDto formularioDia, List<int> listaDocumentosSeleccionados)
        {
            string tipoDato = string.Empty;
            DirectoriosComprimidosDto directoriosComprimidosDto = new DirectoriosComprimidosDto();
            // Filtramos los capítulos que corresponden al padre dado
            var capitulos = lista.Where(x => x.IdEstructuraCapituloAdjuntosPadre == idPadre)
                                 //.OrderBy(x => x.Orden) // Si hay un orden, lo usamos
                                 .ToList();

            var resultado = new List<DirectoriosComprimidosDto>();

            foreach (var capitulo in capitulos)
            {
                //if (capitulo.Codigo == "8"){}
                // Verificar si el capítulo tiene hijos. Si tiene, no lo agregamos como item directamente
                var tieneHijos = lista.Any(x => x.IdEstructuraCapituloAdjuntosPadre == capitulo.IdEstructuraCapituloAdjuntos);

                // Si el capítulo tiene hijos, no lo agregamos directamente como item
                if (tieneHijos)
                {
                    // Solo agregamos sus subcapítulos, no el capítulo padre en la lista
                    var subcapitulos = CrearEstructuraJerarquicaCarpetas(lista, capitulo.IdEstructuraCapituloAdjuntos, $"{prefijo}{capitulo.Descripcion}\\", formularioDia, listaDocumentosSeleccionados);

                    resultado.AddRange(subcapitulos);
                }
                else
                {
                    // Si el capítulo no tiene hijos, agregamos el capítulo con su prefijo
                    string capituloDescripcion = $"{prefijo}{capitulo.Descripcion}";
                    directoriosComprimidosDto = new DirectoriosComprimidosDto();
                    directoriosComprimidosDto.directorio = capituloDescripcion;
                    directoriosComprimidosDto.IdEstructuraCapituloAdjuntos = capitulo.IdEstructuraCapituloAdjuntos;

                    #region "Agregar Adjuntos en capitulos"

                    directoriosComprimidosDto.Adjuntos = new List<Adjunto>();
                    var listaAdjuntosFiltrada = capitulo.Adjuntos.Where(w => listaDocumentosSeleccionados.Exists(e => e == w.Id)).ToList();
                    directoriosComprimidosDto.Adjuntos.AddRange(listaAdjuntosFiltrada);

                    resultado.Add(directoriosComprimidosDto);
                    #endregion "Agregar Adjuntos en capitulos"
                }
            }

            return resultado;
        }

        private async Task<byte[]> ComprimirArchivosYCarpetaEnMemoria(List<DirectoriosComprimidosDto> directoriosComprimidos)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Crear el archivo ZIP en memoria
                using (ZipArchive zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    // Comprimir toda la carpeta
                    foreach (var item in directoriosComprimidos)
                    {
                        //if (item.directorio.StartsWith("8. SOLICITUD TITULO HABITANTE"))
                        //{ }

                        ZipArchiveEntry entrada;// = zip.CreateEntry(item.directorio + @"\");

                        if (item.Adjuntos.Count == 0)
                        {
                            entrada = zip.CreateEntry(item.directorio + @"\");
                            continue;
                        }

                        foreach (var adjunto in item.Adjuntos)
                        {
                            var respDownloadDocument = await _laserficheService.DownloadDocument(adjunto.Id);
                            if (respDownloadDocument != null)
                            {
                                try
                                {
                                    byte[] fileBytes = Convert.FromBase64String(respDownloadDocument.Base64Documento);
                                    string rutaNombreArchivo = string.Format(@"{0}\{1}.- {2}", item.directorio, adjunto.Nro, respDownloadDocument.Nombre);
                                    entrada = zip.CreateEntry(rutaNombreArchivo);
                                    using (Stream entryStream = entrada.Open())
                                    {
                                        entryStream.Write(fileBytes, 0, fileBytes.Length);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //_logService.WriteLog("Laserfiche:  documento no encontrado IdArchivo " + adjunto.Id);
                                }

                            }

                        }
                    }
                }

                // Obtener el archivo ZIP en memoria como un array de bytes
                return memoryStream.ToArray();
            }
        }

        public async Task<StatusResponse<byte[]>> DescargaDocumentoZip(DescargaBloqueRequestDto request)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var zip = new System.IO.Compression.ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
                    {
                        foreach (var id in request.ListaIdDocumentos)
                        {
                            // Obtener el archivo desde Laserfiche
                            var respDownloadDocument = await _laserficheService.DownloadDocument(id);
                            if (respDownloadDocument != null)
                            {
                                try
                                {
                                    byte[] fileBytes = Convert.FromBase64String(respDownloadDocument.Base64Documento);
                                    string nombreArchivo = string.IsNullOrEmpty(respDownloadDocument.Nombre) ? $"{id}.pdf" : respDownloadDocument.Nombre;
                                    var entry = zip.CreateEntry(nombreArchivo, System.IO.Compression.CompressionLevel.Fastest);
                                    using (var entryStream = entry.Open())
                                    {
                                        await entryStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                                    }
                                }
                                catch (Exception)
                                {
                                    // Manejo de error si el documento no se puede agregar
                                }
                            }
                        }
                    }
                    memoryStream.Position = 0;
                    var response = Message.Successful(memoryStream.ToArray());
                    response.Message = "documentos.zip";
                    return response;
                }
            }
            catch (Exception ex)
            {
                return Message.Exception<byte[]>(ex);
            }
        }

        public async Task<StatusResponse<DescargaBloqueResponseDto>> DescargaEnBloque(int idSolicitud)
        {
            try
            {
                DescargaBloqueResponseDto oDescargaBloqueResponseDto = new DescargaBloqueResponseDto();
                oDescargaBloqueResponseDto.DatosFormulario = _mapper.Map<DatosFormularioDto>(await _tupaRepository.ObtenerSolicitudPorCodigo(idSolicitud));
                int totalAdjuntos = 0;
                oDescargaBloqueResponseDto.ListaEstructuraIndice = await this.ObtenerData(idSolicitud);
                totalAdjuntos = oDescargaBloqueResponseDto.ListaEstructuraIndice.Sum(item => item.Adjuntos.Count);
                if (oDescargaBloqueResponseDto.DatosFormulario.TipoIga== "ITS")
                {
                    
                }
                else
                {
                    
                }
                    
                
                oDescargaBloqueResponseDto.TotalAdjuntos = totalAdjuntos;
                var response = Message.Successful(oDescargaBloqueResponseDto);
                return response;
            }
            catch (Exception ex)
            {
                //_logService.WriteLog("DescargaEnBloque:  " + ex.Message);
                return Message.Exception<DescargaBloqueResponseDto>(ex);
            }
        }

        public async Task<StatusResponse<byte[]>> DescargaEnBloqueIndiceExcel(DescargaBloqueRequestDto request)
        {
            try
            {
                byte[] excelBytes = null;
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                int nroFila = 1, nroColumna = 0, nuevoNroColumna = 0, nuevoNroColumnaTmp = 0, nroColumnaDocumentos = 4, nroColumnaMergeCapitulo = 0, nroColumnaMerge = 0, nroFilaMergeOrigen = 0, nroFilaMergeDestino = 0;
                bool documentoSeleccionado = false;
                List<int> listaPosicionesDocumento = new List<int>();
                List<EstructuraIndiceDto> listaEstructuraIndice = await this.ObtenerData(request.IdSolicitud);
                int nivelMax = listaEstructuraIndice.Max(x => x.Nivel);
                ExcelRange range;
                using (var package = new ExcelPackage())
                {
                    // Crear una hoja de cálculo
                    var worksheet = package.Workbook.Worksheets.Add("Hoja1");

                    // Escribir datos en la hoja
                    foreach (var estructuraIndice in listaEstructuraIndice)
                    {
                        nuevoNroColumna = nroColumna + estructuraIndice.Nivel;
                        nuevoNroColumnaTmp = nuevoNroColumna;
                        range = worksheet.Cells[nroFila, nuevoNroColumnaTmp];
                        range.Value = estructuraIndice.Descripcion;
                        range.Style.Font.Bold = true;
                        range.Style.Font.UnderLine = true;

                        nroColumnaMergeCapitulo = nuevoNroColumnaTmp + nivelMax + nroColumnaDocumentos - estructuraIndice.Nivel;
                        //nroColumnaMerge = nuevoNroColumnaTmp + nivelMax + nroColumnaDocumentos - estructuraIndice.Nivel;
                        range = worksheet.Cells[nroFila, nuevoNroColumnaTmp, nroFila, nroColumnaMergeCapitulo];
                        range.Merge = true;
                        AsignarBordes(range, Constante.Excel.Border.All);

                        for (var nuevoNroColumnaMerge = nuevoNroColumnaTmp - 1; nuevoNroColumnaMerge > 0; nuevoNroColumnaMerge--)
                        {
                            range = worksheet.Cells[nroFila, nuevoNroColumnaMerge];
                            AsignarBordes(range, Constante.Excel.Border.All);
                        }

                        nroFila++;
                        if (estructuraIndice.TieneAdjunto)
                        {
                            nroFilaMergeOrigen = nroFila;
                            nuevoNroColumnaTmp++;
                            worksheet.Cells[nroFila, nuevoNroColumnaTmp].Value = "NOMBRE DOCUMENTO";
                            worksheet.Cells[nroFila, nuevoNroColumnaTmp].Style.Font.Bold = true;
                            listaPosicionesDocumento.Add(nuevoNroColumnaTmp);
                            nuevoNroColumnaTmp++;
                            worksheet.Cells[nroFila, nuevoNroColumnaTmp].Value = "FECHA DOCUMENTO";
                            worksheet.Cells[nroFila, nuevoNroColumnaTmp].Style.Font.Bold = true;

                            range = worksheet.Cells[nroFila, nroColumnaMergeCapitulo];
                            AsignarBordes(range, Constante.Excel.Border.Right);

                            nroFila++;
                        }

                        if (estructuraIndice.Adjuntos != null && estructuraIndice.Adjuntos.Any())
                        {

                            foreach (var indiceAdjunto in estructuraIndice.Adjuntos)
                            {
                                documentoSeleccionado = request.ListaIdDocumentos.Exists(e => e == indiceAdjunto.Id);
                                if (!documentoSeleccionado) { continue; }
                                nuevoNroColumnaTmp = nuevoNroColumna + 1;
                                worksheet.Cells[nroFila, nuevoNroColumnaTmp].Value = string.Format("{0}.- {1}", indiceAdjunto.Nro, indiceAdjunto.Nombre);
                                nuevoNroColumnaTmp++;
                                worksheet.Cells[nroFila, nuevoNroColumnaTmp].Value = DateTime.Now.ToString("dd/MM/yyyy");

                                range = worksheet.Cells[nroFila, nroColumnaMergeCapitulo];
                                AsignarBordes(range, Constante.Excel.Border.Right);
                                nroFila++;
                            }

                            for (var nuevoNroColumnaMerge = nuevoNroColumna - 1; nuevoNroColumnaMerge > 0; nuevoNroColumnaMerge--)
                            {
                                nroFilaMergeDestino = estructuraIndice.Adjuntos.Count() > 0 ? nroFila - 1 : nroFila;
                                range = worksheet.Cells[nroFilaMergeOrigen, nuevoNroColumnaMerge, nroFilaMergeDestino, nuevoNroColumnaMerge];
                                range.Merge = true;
                                AsignarBordes(range, Constante.Excel.Border.All);
                            }

                        }
                    }

                    var listaAgrupadaPosicionesDocumento = listaPosicionesDocumento.GroupBy(x => x).Select(s => s.Key).ToList().OrderBy(x => x).ToList();
                    if (listaAgrupadaPosicionesDocumento.Count > 0)
                    {
                        foreach (var posicionDocumento in listaAgrupadaPosicionesDocumento)
                        {
                            worksheet.Column(posicionDocumento).Width = 60;
                        }
                    }
                    range = worksheet.Cells[nroFila - 1, 1, nroFila - 1, nroColumnaMergeCapitulo];
                    AsignarBordes(range, Constante.Excel.Border.Bottom);

                    // Guardar en memoria
                    using (var memoryStream = new MemoryStream())
                    {
                        package.SaveAs(memoryStream);

                        // Obtener el archivo en formato byte[]
                        excelBytes = memoryStream.ToArray();

                        // Guardar el archivo en disco (opcional)
                        //File.WriteAllBytes("archivo.xlsx", excelBytes);

                        Console.WriteLine("Archivo Excel creado en memoria y guardado en disco.");
                    }
                }


                var response = Message.Successful(excelBytes);
                response.Message = string.Format("Indice_DIA_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHH24mmss"));
                return response;
            }
            catch (Exception ex)
            {
                //_logService.WriteLog("DescargaEnBloque:  " + ex.Message);
                return Message.Exception<byte[]>(ex);
            }

        }

        private void AsignarBordes(ExcelRange range, int border)
        {
            if (border == Constante.Excel.Border.Top || border == Constante.Excel.Border.All)
            {
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
            }
            if (border == Constante.Excel.Border.Left || border == Constante.Excel.Border.All)
            {
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
            }

            if (border == Constante.Excel.Border.Bottom || border == Constante.Excel.Border.All)
            {
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
            }

            if (border == Constante.Excel.Border.Right || border == Constante.Excel.Border.All)
            {
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
            }
        }

    }
}

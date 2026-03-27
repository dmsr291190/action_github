using Microsoft.AspNetCore.Http;
using Minem.Tupa.Proxy.Interface;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Minem.Tupa.Dto.Svc.Laserfiche;

namespace Minem.Tupa.Proxy.Implementation
{
    public class LaserficheService(HttpClient httpClient, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
             IOptions<AppSettings> appSettings, IOptions<LaserficheSvcSettings> laserficheSvcSettings ) : ILaserficheService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IOptions<AppSettings> _appSettings = appSettings;
        private readonly IOptions<LaserficheSvcSettings> _laserficheSvcSettings = laserficheSvcSettings;
        private string BASE_URL = appSettings.Value.MsSettings.LaserficheSvcSettings.BaseUrl;
         
        public async Task<DocumentoModel> DownloadDocument(int idDocumento)
        {
            DocumentoModel response;
            try
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                string strDownloadDocument = _appSettings.Value.MsSettings.LaserficheSvcSettings.DownloadDocument; 
                string urlEndPoint = $"{strDownloadDocument}?idDocument={idDocumento}";


                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);
                
                var httpResponseMessage = await _httpClient.GetAsync(urlEndPoint);
                   var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                 var apiResponse = JsonSerializer.Deserialize<DocumentoModel>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                 if (apiResponse is null)
                {
                    return null;
                }
                response = apiResponse;
            }
            catch (Exception ex)
            {
                 
                throw;
            }
            return response;
        }

        public async Task<int> UploadDocument(byte[] file, string documentName)
        {
            try
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                string strDownloadDocument = _appSettings.Value.MsSettings.LaserficheSvcSettings.UploadDocument;
                string urlEndPoint = $"{strDownloadDocument} ";


                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(file);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    content.Add(fileContent, "file", documentName);

                    var httpResponseMessage = await _httpClient.PostAsync(urlEndPoint, content);

                    var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                     
                    if (int.TryParse(responseString, out int documentId))
                    {
                        return documentId;
                    }
                    else
                    {
                        throw new InvalidOperationException("La respuesta de la API no es un número válido.");
                    }
                }




            }
            catch (Exception ex)
            {
               
                throw;
            }

        }
    }
}

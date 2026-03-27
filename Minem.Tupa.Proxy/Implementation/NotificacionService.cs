using Minem.Tupa.Dto.Svc.Notificacion;
using Minem.Tupa.Proxy.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minem.Tupa.Utils;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;
using Minem.Tupa.Infraestructure;
using System.Text.Json;

namespace Minem.Tupa.Proxy.Implementation
{
    public class NotificacionService: INotificacionService
    {
        private readonly HttpClient _httpClient;
        private readonly NotificacionesSvcSettings _settings;       
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOptions<AppSettings> _appSettings;
        //  private readonly ILogService _logService;

        public NotificacionService(
            HttpClient httpClient,
            IOptions<NotificacionesSvcSettings> options,
            IOptions<AppSettings> appSettings,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            //ILogService logService
               IConfiguration configuration
            )
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _appSettings = appSettings;
            _contextAccessor = contextAccessor;
        //    _logService = logService;
        }

        public async Task RegistrarNotificacionInterna(NotificationInternaRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string strUploadDocument = _appSettings.Value.MsSettings.NotificacionesSvcSettings.NotificacioneInterna;
                string urlEndPoint = $"{strUploadDocument}";

                //var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                //ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                //var claims = principal.Claims.FirstOrDefault();

                //   _logService.WriteLog("Token:  " + authorizationHeader);
                //   _logService.WriteLog("Base address NotificacioneInterna: " + _httpClient.BaseAddress);
                //   _logService.WriteLog("Ruta NotificacioneInterna: " + _settings.NotificacioneInterna);

                var objRequest = new
                {
                    numDocumento = request.NumDocumento,
                    numDocAdicional = request.NumDocAdicional,
                    asunto = request.Asunto,
                    mensaje = request.Mensaje,
                    codMaeCategoria = request.CodMaeCategoria,                  
                    request.notificationAttaches,
                    selloTiempo = request.SelloTiempo,
                    idSolicitud = request.IdSolicitud,
                    expediente = request.Expediente,
                    idSistema = request.IdSistema,
                    codTipoDocumento = request.CodTipoDocumento,
                    codTipoPersona = request.CodTipoPersona
                };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);
                
                var httpNotificacionInternaResponseMessage = await _httpClient.PostAsJsonAsync(urlEndPoint, objRequest);
                //   _logService.WriteLog("Respuesta de Notificacion httpNotificacionInternaResponseMessage :::>" + httpNotificacionInternaResponseMessage);
                var respNotificacionInternaString = await httpNotificacionInternaResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
               // _logService.WriteLog("Respuesta del servicio Notificacion :::>" + ex.Message);
                throw new Exception("Error interno del API Notificacion.", ex);
            }
        }

        public async Task<StatusResponse<List<DocumentoDespachadoResponseDto>>> GetDocumentosDespachados(int idSolicitud)
        {
            try
            { 
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string strUploadDocument = _appSettings.Value.MsSettings.NotificacionesSvcSettings.ObtenerDocumentosDespachados;
                string urlEndPoint = $"{strUploadDocument}/{idSolicitud}";

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                var httpResponseMessage = await _httpClient.GetAsync(urlEndPoint);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                //_logService.WriteLog("responseString:  " + responseString);
                var apiResponse = JsonSerializer.Deserialize<StatusResponse<List<DocumentoDespachadoResponseDto>>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse != null)
                    return Message.Successful(apiResponse.Data);
                else
                    return Message.Successful(new List<DocumentoDespachadoResponseDto>());
            }
            catch (Exception ex)
            {
                //_logService.WriteLog("Respuesta del servicio Notificacion :::>" + ex.Message);
                return Message.Exception<List<DocumentoDespachadoResponseDto>>(ex);
            }
        }

    }
}

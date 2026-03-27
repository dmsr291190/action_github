using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Minem.Tupa.Dto.Svc.Notificacion;
using Minem.Tupa.Dto.Svc.PagoTupa;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.Proxy.Interface;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Minem.Tupa.Proxy.Implementation
{
    public class PagoTupaService : IPagoTupaService
    {
        private readonly HttpClient _httpClient;
        private readonly PagoTupaSvcSettings _settings;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOptions<AppSettings> _appSettings;
        public PagoTupaService(
              HttpClient httpClient,
            IOptions<PagoTupaSvcSettings> options,
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
        }

        public async Task PagoCajaMinemAsignarExpediente(AsignarExpedientePagoRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string strAsignarExpediente = _appSettings.Value.MsSettings.PagoTupaSvcSettings.PagoCajaMinemAsignarExpediente;
                string urlEndPoint = $"{strAsignarExpediente}";

                //var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                //ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                //var claims = principal.Claims.FirstOrDefault();

                //   _logService.WriteLog("Token:  " + authorizationHeader);
                //   _logService.WriteLog("Base address NotificacioneInterna: " + _httpClient.BaseAddress);
                //   _logService.WriteLog("Ruta NotificacioneInterna: " + _settings.NotificacioneInterna);

                var objRequest = new
                {
                    idPago = request.IdPago,
                    idSistema = request.IdSistema,
                    expediente = request.Expediente
                };

                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                var httpNotificacionInternaResponseMessage = await _httpClient.PutAsJsonAsync(urlEndPoint, objRequest);
                //   _logService.WriteLog("Respuesta de Notificacion httpNotificacionInternaResponseMessage :::>" + httpNotificacionInternaResponseMessage);
                var respNotificacionInternaString = await httpNotificacionInternaResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // _logService.WriteLog("Respuesta del servicio Notificacion :::>" + ex.Message);
                throw new Exception("Error interno del API PAGO TUPA.", ex);
            }
        }

        public async Task<StatusResponse<int>> ValidarPagoCajaMinem(ValidarPagoCajaMinemRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string strValidarPagoCajaMinem = _appSettings.Value.MsSettings.PagoTupaSvcSettings.ValidarPagoCajaMinem;
                string urlEndPoint = $"{strValidarPagoCajaMinem}?CodigoCaja={request.CodigoCaja}&FechaPago={request.FechaPago.ToString("yyyy-MM-dd")}&Importe={request.Importe}&Expediente={request.Expediente}";

                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                var httpResponseMessage = await _httpClient.GetAsync(urlEndPoint);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                //_logService.WriteLog("responseString:  " + responseString);
                var apiResponse = JsonSerializer.Deserialize<StatusResponse<int>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse != null)
                    return Message.Successful(apiResponse.Data);
                else
                    return Message.Successful(0);
            }
            catch (Exception ex)
            {
                //_logService.WriteLog("Respuesta del servicio Notificacion :::>" + ex.Message);
                return Message.Exception<int>(ex);
            }
        }

        public async Task<StatusResponse<int>> RegistrarPagoCajaMinem(RegistrarPagoCajaMinemRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string strAsignarExpediente = _appSettings.Value.MsSettings.PagoTupaSvcSettings.RegistrarPagoCajaMinem;
                string urlEndPoint = $"{strAsignarExpediente}";

                //var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                //ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                //var claims = principal.Claims.FirstOrDefault();

                //   _logService.WriteLog("Token:  " + authorizationHeader);
                //   _logService.WriteLog("Base address NotificacioneInterna: " + _httpClient.BaseAddress);
                //   _logService.WriteLog("Ruta NotificacioneInterna: " + _settings.NotificacioneInterna);

                var objRequest = new
                {
                    codigoCaja = request.CodigoCaja,
                    fechaPago = request.FechaPago,
                    importe = request.Importe,
                    idSistema = request.IdSistema,
                    expediente = request.Expediente,
                    idArchivoLaserfiche = request.IdArchivoLaserfiche
                };

                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                var httpResponseMessage = await _httpClient.PostAsJsonAsync(urlEndPoint, objRequest);
                //   _logService.WriteLog("Respuesta de Notificacion httpNotificacionInternaResponseMessage :::>" + httpNotificacionInternaResponseMessage);

                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                //_logService.WriteLog("responseString:  " + responseString);
                var apiResponse = JsonSerializer.Deserialize<StatusResponse<int>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse != null)
                    return Message.Successful(apiResponse.Data);
                else
                    return Message.Successful(0);
            }
            catch (Exception ex)
            {
                // _logService.WriteLog("Respuesta del servicio Notificacion :::>" + ex.Message);
                throw new Exception("Error interno del API PAGO TUPA.", ex);
            }
        }

        public async Task PagoPagaloPEAsignarExpediente(AsignarExpedientePagoRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string strAsignarExpediente = _appSettings.Value.MsSettings.PagoTupaSvcSettings.PagoPagaloPeAsignarExpediente;
                string urlEndPoint = $"{strAsignarExpediente}";

                //var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                //ClaimsPrincipal principal = CustomFunctions.GetClaimsPrincipalFromToken(token);
                //var claims = principal.Claims.FirstOrDefault();

                //   _logService.WriteLog("Token:  " + authorizationHeader);
                //   _logService.WriteLog("Base address NotificacioneInterna: " + _httpClient.BaseAddress);
                //   _logService.WriteLog("Ruta NotificacioneInterna: " + _settings.NotificacioneInterna);

                var objRequest = new
                {
                    idPago = request.IdPago,
                    idSistema = request.IdSistema,
                    expediente = request.Expediente
                };

                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                var httpNotificacionInternaResponseMessage = await _httpClient.PutAsJsonAsync(urlEndPoint, objRequest);
                //   _logService.WriteLog("Respuesta de Notificacion httpNotificacionInternaResponseMessage :::>" + httpNotificacionInternaResponseMessage);
                var respNotificacionInternaString = await httpNotificacionInternaResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // _logService.WriteLog("Respuesta del servicio Notificacion :::>" + ex.Message);
                throw new Exception("Error interno del API PAGO TUPA.", ex);
            }
        }

        public async Task<StatusResponse<int>> ValidarPagoPagaloPE(ValidarPagoPagaloPeRequestDto request)
        {
            try
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string strValidarPagoPagaloPe = _appSettings.Value.MsSettings.PagoTupaSvcSettings.ValidarPagoPagaloPE;
                //?NroSecuencia=00525&FechaMovimiento=2025-08-29&CodigoOficina=1256
                string urlEndPoint = $"{strValidarPagoPagaloPe}?NroSecuencia={request.NroSecuencia}&FechaMovimiento={request.FechaMovimiento.ToString("yyyy-MM-dd")}&CodigoOficina={request.CodigoOficina}";

                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                var httpResponseMessage = await _httpClient.GetAsync(urlEndPoint);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                //_logService.WriteLog("responseString:  " + responseString);
                var apiResponse = JsonSerializer.Deserialize<StatusResponse<int>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse != null)
                    return Message.Successful(apiResponse.Data);
                else
                    return Message.Successful(0);
            }
            catch (Exception ex)
            {
                //_logService.WriteLog("Respuesta del servicio Notificacion :::>" + ex.Message);
                return Message.Exception<int>(ex);
            }
        }
    }
}

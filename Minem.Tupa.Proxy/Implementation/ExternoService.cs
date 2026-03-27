using AutoMapper;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minem.Tupa.Dto.Proxy.Externo;
using Minem.Tupa.Dto.Svc.Externo;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.Proxy.Interface;
using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Minem.Tupa.Proxy.Implementation
{
    public class ExternoService(HttpClient httpClient, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> appSettings) : IExternoService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IOptions<AppSettings> _appSettings = appSettings;


        public async Task<string> ObtenerRepresentantePorCliente(RepresentanteRequestDto request)
        {
            string response;
            try
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string obtenerRepresentantePorCliente = "";//_appSettings.Value.MsSettings.ExternoSvcSettings.EndPoint.GetRepresentantePorCliente;
                string urlEndPoint = $"{obtenerRepresentantePorCliente}?IdOficina={request.IdCliente}"; //+
                    //$"&IdTipoDocumento={request.IdTipoDocumento}" +
                    //$"&FlgActualiza={request.FlgActualiza}&IdUsuario={request.IdUsuario}&IpPc={request.IpPc}";


                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);
                var httpResponseMessage = await _httpClient.GetAsync(urlEndPoint);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<StatusResponse<string>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse is null)
                {
                    return null;
                }
                response = apiResponse.Data;
            }
            catch (Exception ex)
            {
                //Funciones.WriteLog("Respuesta del servicio Notificacion :::>" + ex.Message);
                throw;
            }
            return response;
        }

        public async Task<DatosGeneralesEmpresaResponseDto?> ObtenerDatoTitularEmpresa(int idCliente)
        {
            try
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                string strUploadDocument = _appSettings.Value.MsSettings.ExternoSvcSettings.DatosTitularEmpresa;
                string urlEndPoint = $"{strUploadDocument}/{idCliente}";

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader);

                var httpResponseMessage = await _httpClient.GetAsync(urlEndPoint);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<StatusResponse<DatosGeneralesEmpresaResponseDto>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResponse is null)
                {
                    return null;
                }
                
                return apiResponse.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

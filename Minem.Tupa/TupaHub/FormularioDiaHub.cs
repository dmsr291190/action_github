using Microsoft.AspNetCore.SignalR;

namespace Minem.Tupa.Api.TupaHub
{
    public class FormularioDiaHub : Hub
    {
        public static string GlobalFormularioData = string.Empty;

        public async Task UpdateResumenEjecutivo(string data) => await Clients.All.SendAsync("actualizarResumenEjecutivo", data);
        
    }
}

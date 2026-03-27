using Microsoft.AspNetCore.SignalR;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.IApplication;
using System.Collections.Concurrent;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Minem.Tupa.Api.TupaHub
{
    public class UserEditingHub(IFormularioApplication service) : Hub
    {
        private static ConcurrentDictionary<string, string> UsersEditing = new();
        private readonly IFormularioApplication _service = service;

        public async Task StartEditing(string section, long codMaeSolicitud, string username, string idSession)
        {
            try
            {
                //string groupName = codMaeSolicitud.ToString()+'-'+section;
                string groupName = $"{codMaeSolicitud}";
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                //UsersEditing[Context.ConnectionId] = $"{username} en Sección {section}";
                UsersEditing.AddOrUpdate(Context.ConnectionId,
                    $"{username} en Sección {section}", (key, oldValue) => $"{username} en Sección {section}");

                await Clients.Group(groupName).SendAsync("UpdateEditingUsers", new HubResponse
                {
                    json = "",
                    usuarios = UsersEditing.Values,
                    action = false,
                    usuarioResponsable = idSession
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error en StartEditing: {ex.Message}");
            }
        }

        public async Task StopEditing(string section, long codMaeSolicitud, string username, string data, bool action, string idSession)
        {
            try
            {
                string groupName = $"{codMaeSolicitud}";
                // Remover el usuario del diccionario si existe
                UsersEditing.TryRemove(Context.ConnectionId, out _);

                if (action)
                {
                    var request = new GuardarFormularioRequestDto
                    {
                        codMaeSolicitud = codMaeSolicitud,
                        dataJson = data,
                        usuario = username
                    };

                    await _service.GuardarFormulario(request);
                }

                await Clients.Group(groupName).SendAsync("UpdateEditingUsers", new HubResponse
                {
                    json = data,
                    usuarios = UsersEditing.Values,
                    action = action,
                    usuarioResponsable = idSession
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en StopEditing: {ex.Message}");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                // Obtener la conexión antes de eliminarla
                if (UsersEditing.TryRemove(Context.ConnectionId, out _))
                {
                    await Clients.All.SendAsync("UpdateEditingUsers", new HubResponse
                    {
                        json = "",
                        usuarios = UsersEditing.Values,
                        action = false,
                        usuarioResponsable = ""
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en OnDisconnectedAsync: {ex.Message}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}

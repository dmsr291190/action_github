namespace Minem.Tupa.Entity.Its;

public class SP_UPDATE_ITS_MAPA_Request_Entity
{
    public long IdProyecto { get; set; }
    public string MapaJson { get; set; } = string.Empty;
    public long UsuarioModifica { get; set; }
}
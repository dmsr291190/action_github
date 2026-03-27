namespace Minem.Tupa.Entity.Its;

public class SP_INSERT_ITS_MAPA_Request_Entity
{
    public long IdProyecto { get; set; }
    public string MapaJson { get; set; } = string.Empty;
    public long UsuarioRegistra { get; set; }
}
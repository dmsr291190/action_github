namespace Minem.Tupa.Entity.Its;

public class SP_OBTENER_ITS_MAPA_Response_Entity
{
    public long IdProyecto { get; set; }
    public string MapaJson { get; set; } = string.Empty;
    public long? UsuarioRegistra { get; set; }
    public DateTime? FechaRegistra { get; set; }
    public long? UsuarioModifica { get; set; }
    public DateTime? FechaModifica { get; set; }
}
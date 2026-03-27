public class ReunionCorreoDto
{
    public long? IdReunionCorreo { get; set; } // Puede ser null para inserciones
    public long IdReunionSolicitud { get; set; } // Requerido
    public string Correo { get; set; } = string.Empty; // Requerido, VARCHAR2(200)

    // Estado lógico: 1 = activo, 0 = inactivo (útil para eliminación lógica)
    public int? Estado { get; set; }

    // Auditoría
    public long? UsuarioRegistra { get; set; }
    public DateTime? FechaRegistra { get; set; }
    public long? UsuarioModifica { get; set; }
    public DateTime? FechaModifica { get; set; }
}

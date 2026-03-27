namespace Minem.Tupa.Api.TupaHub
{
    public class HubResponse
    {
        public IEnumerable<string> usuarios { get; set; }
        public string json { get; set; }
        public bool action { get; set; }
        public string usuarioResponsable { get; set; }
    }
}
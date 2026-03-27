using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Utils
{
    public class AppSettings
    {
        public MsSettings MsSettings { get; set; }
    }

    public class MsSettings
    {
        public NotificacionesSvcSettings NotificacionesSvcSettings { get; set; }
        public TransversalSvcSettings TransversalSvcSettings { get; set; }
        public LaserficheSvcSettings LaserficheSvcSettings { get; set; }
        public PagoTupaSvcSettings PagoTupaSvcSettings { get; set; }

        public ExternoSvcSettings ExternoSvcSettings { get; set; }
    }

    public class NotificacionesSvcSettings
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public double TimeoutSeconds { get; set; } = 300.0;
        public string NotificacioneInterna { get; set; } = string.Empty;
        public string EnviarCorreo { get; set; } = string.Empty;
        public string Sms { get; set; } = string.Empty;
        public string ObtenerDocumentosDespachados { get; set; } = string.Empty;

    }

    public class TransversalSvcSettings
    {
        public string BaseUrl { get; set; }
        public double TimeoutSeconds { get; set; } = 300.0;
        public EndPointUrl EndPoint { get; set; }

        public class EndPointUrl
        {
            public string GenerarCodigoDocumentoInterno { get; set; }
        }

    }
    public class LaserficheSvcSettings
    {

        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public double TimeoutSeconds { get; set; } = 300.0;
        
        public string DownloadDocument { get; set; } = string.Empty;
        public string UploadDocument { get; set; } = string.Empty;        
    }

    public class PagoTupaSvcSettings
    {
        public string BaseUrl { get; set; }
        public double TimeoutSeconds { get; set; } = 300.0;

        public string PagoCajaMinemAsignarExpediente { get; set; } = string.Empty;
        public string PagoPagaloPeAsignarExpediente { get; set; } = string.Empty;
        public string ValidarPagoCajaMinem { get; set; } = string.Empty;
        public string ValidarPagoPagaloPE { get; set; } = string.Empty;
        public string RegistrarPagoCajaMinem { get; set; }= string.Empty;
    }

    public class ExternoSvcSettings
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public double TimeoutSeconds { get; set; } = 300.0;
        public string DatosTitularEmpresa { get; set; } = string.Empty;
    }
}

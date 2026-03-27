using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Utils
{
    public static class Constante
    {
        public const string EX_GENERICA = "\"INTERNAL SERVER ERROR - SE PRESENTO UNA CONDICION INESPERADA QUE IMPIDIO COMPLETAR EL REQUEST.\"";
        public const string RESPUESTA_EXITOSA = "RESPUESTA EXITOSA";
        public const string EX_MENSAJE_NO_AUTORIZADO = "Usted no tiene acceso autorizado a ésta opción";
        public const string ORIGIN_WEB = "http://localhost:4200";
        public const string KEY_ECRYPTATION = "clavedeencriptaciondetupadigital";
        public static string CodigoFuncionario = "00000";
        public static string Dominio = "MTC";
        public static string JsonData = "{\"IdSolicitud\":0,\"ResumenEjecutivo\":{\"Resumen\":\"\",\"Documentos\":[]},\"DescripcionProyecto\":{\"Antecedentes\":{\"DatosGenerales\":{\"NombreProyecto\":\"\",\"InversionEstimada\":\"\",\"UnidadMinera\":0,\"Tipo\":\"0\"},\"DatosTitular\":{\"NombreTitular\":\"\",\"DireccionTitular\":\"\",\"Region\":\"\",\"Telefono\":\"\",\"Fax\":\"\",\"Ruc\":\"\",\"Email\":\"\",\"PaginaWeb\":\"\"},\"DatosRepresentante\":{\"ApellidoPaterno\":\"\",\"ApellidoMaterno\":\"\",\"Nombres\":\"\",\"Cargo\":\"\",\"DocumentoIdentidad\":\"\",\"EmailRepresentante\":\"\"},\"CorreoNotificacion\":{\"EmailNotificacion1\":\"\",\"EmailNotificacion2\":\"\",\"Telefono1\":\"\",\"Telefono2\":\"\"},\"PasivosAmbientales\":[],\"ComponentesNoCerrados\":[],\"MapaComponentes\":[],\"Estudios\":[],\"Permisos\":[],\"PropiedadSuperficial\":[],\"AreasProtegidas\":[],\"Distanciamiento\":[],\"MapaAreasNaturales\":[]},\"Objetivo\":\"\",\"LocalizacionGeografica\":{\"Localizacion\":\"\",\"UbicacionGeografica\":{\"Region\":\"\",\"Este\":\"\",\"Norte\":\"\"},\"LocalizacionSuperpuesta\":[],\"DistanciaPobladoscercanos\":[]},\"Delimitacion\":{\"DelimitacionPerimetral\":\"\",\"AreasActividadMinera\":[],\"AreasUsoMinero\":[],\"CoordenadaPuntoEste\":\"\",\"CoordenadaPuntoNorte\":\"\",\"Zona\":\"\",\"Datum\":\"\",\"MapaArea\":\"\"},\"AreasInfluencia\":{\"AreaDirectaAmbiental\":[],\"AreaIndirectaAmbiental\":[],\"AreaDirectaSocial\":[],\"AreaIndirectaSocial\":[],\"ArchivoEscaneado\":\"\"},\"CronogramaInversion\":{\"Construccion\":{\"FechaInicio\":\"\",\"FechaFin\":\"\",\"TotalMeses\":\"\",\"Inversion\":\"\"},\"Exploracion\":{\"FechaInicio\":\"\",\"FechaFin\":\"\",\"TotalMeses\":\"\",\"Inversion\":\"\"},\"Cierre\":{\"FechaInicio\":\"\",\"FechaFin\":\"\",\"TotalMeses\":\"\",\"Inversion\":\"\"},\"PostCierre\":{\"FechaInicio\":\"\",\"FechaFin\":\"\",\"TotalMeses\":\"\",\"Inversion\":\"\"}},\"DescripcionEtapas\":{\"Documento\":[],\"MineralAExplotar\":[],\"ComponentesPrincipales\":{\"NroPerforaciones\":\"\",\"NroPlataformas\":\"\",\"Zona\":\"\",\"Datum\":\"\",\"PlataformasPerforaciones\":[]},\"Residuos\":[],\"RequerimientoAgua\":[],\"ComponentesProyecto\":[],\"BalanceAgua\":[],\"Archivos\":[],\"Instalaciones\":[],\"ArchivosMDS\":[],\"MapaComponentes\":[],\"Insumos\":[],\"Maquinarias\":[],\"Equipos\":[],\"ViasAccesoExistente\":[],\"ViasAccesoNueva\":[],\"ManoObra\":{\"Construccion\":\"\",\"PorcentajeConstruccion\":\"\",\"Exploracion\":\"\",\"PorcentajeExploracion\":\"\",\"Cierre\":\"\",\"PorcentajeCierre\":\"\"},\"TipoManoObra\":[],\"FuenteAbastecimientoEnergia\":[],\"CierrePostCierre\":\"\"}},\"Objetivo\":\"\",\"LocalizacionGeografica\":{\"Localizacion\":\"\",\"UbicacionGeografica\":{\"Region\":\"\",\"Este\":\"\",\"Norte\":\"\"},\"LocalizacionSuperpuesta\":[],\"DistanciaPobladoscercanos\":[]},\"Delimitacion\":{\"DelimitacionPerimetral\":\"\",\"AreasActividadMinera\":[],\"AreasUsoMinero\":[],\"CoordenadaPuntoEste\":\"\",\"CoordenadaPuntoNorte\":\"\",\"MapaArea\":\"\"},\"AreasInfluencia\":{\"AreaDirectaAmbiental\":[],\"AreaIndirectaAmbiental\":[],\"AreaDirectaSocial\":[],\"AreaIndirectaDirectaSocial\":[],\"ArchivoEscaneado\":\"\"},\"CronogramaInversion\":{\"Construccion\":{\"FechaInicio\":\"\",\"FechaFin\":\"\",\"TotalMeses\":\"\",\"Inversion\":\"\"},\"Exploracion\":{\"FechaInicio\":\"\",\"FechaFin\":\"\",\"TotalMeses\":\"\",\"Inversion\":\"\"},\"Cierre\":{\"FechaInicio\":\"\",\"FechaFin\":\"\",\"TotalMeses\":\"\",\"Inversion\":\"\"},\"PostCierre\":{\"FechaInicio\":\"\",\"FechaFin\":\"\",\"TotalMeses\":\"\",\"Inversion\":\"\"}},\"DescripcionEtapas\":{\"Documento\":[],\"MineralAExplotar\":[],\"ComponentesPrincipales\":{\"NroPerforaciones\":\"\",\"NroPlataformas\":\"\",\"Zona\":\"\",\"Datum\":\"\",\"PlataformasPerforaciones\":[]},\"Residuos\":[],\"RequerimientoAgua\":[],\"ComponentesProyecto\":[],\"BalanceAgua\":[],\"Archivos\":[],\"Instalaciones\":[],\"ArchivosMDS\":[],\"MapaComponentes\":[],\"Insumos\":[],\"Maquinarias\":[],\"Equipos\":[],\"ViasAccesoExistente\":[],\"ViasAccesoNueva\":[],\"ManoObra\":{\"Construccion\":\"\",\"PorcentajeConstruccion\":\"\",\"Exploracion\":\"\",\"PorcentajeExploracion\":\"\",\"Cierre\":\"\",\"PorcentajeCierre\":\"\"},\"TipoManoObra\":[],\"FuenteAbastecimientoEnergia\":[],\"CierrePostCierre\":\"\"},\"DescripcionMedioFisico\":{\"Metereologia\":\"\",\"CalidadAire\":\"\",\"CalidadRuidoAmbiental\":\"\",\"Topografia\":\"\",\"Geologia\":\"\",\"Geomorfologia\":\"\",\"Hidrologia\":\"\",\"Hidrogeologia\":\"\",\"CalidadAgua\":\"\",\"EstudioSuelo\":\"\",\"ClasificacionTierras\":\"\",\"UsoActualTierra\":\"\",\"CalidadSuelos\":\"\"},\"DescripcionMedioBiologico\":{\"CriteriosEvaluacion\":\"\",\"DescripcionEcosistemas\":\"\",\"Ecosistemas\":\"\",\"FloraTerrestre\":\"\",\"FaunaTerrestre\":\"\",\"Hidrobiologia\":\"\",\"EcosistemasFragiles\":\"\",\"AreasNaturales\":\"\",\"Documentos\":[]},\"PuntoMuestreo\":[],\"Cartografia\":[],\"DescripcionAspectoSocial\":{\"Indices\":\"\",\"Descripcion\":\"\",\"OtrosAspectos\":\"\"},\"Arqueologia\":{\"Descripcion\":\"\",\"Documentos\":[]},\"PlanMinimizacion\":{\"Descripcion\":\"\"},\"PlanContingencia\":{\"Descripcion\":\"\"},\"ProtocoloRelacionamiento\":{\"Descripcion\":\"\"},\"PlanCierre\":{\"DescripcionCierre\":\"\",\"DescripcionPostCierre\":\"\"},\"ParticipacionCiudadana\":{\"Mecanismos\":[],\"Documentos\":[]},\"ImpactosAmbientales\":[],\"PlanManejoAmbiental\":{\"PlanManejo\":[],\"Resumen\":{\"Compromisos\":[],\"Documentos\":[],\"MontoInversion\":\"\",\"UnidadMonetaria\":\"\"},\"PlanVigilanciaAmbiental\":{\"PuntosMonitoreo\":[],\"Documentos\":[]}},\"Consultora\":{\"EmpresaConsultora\":{\"Ruc\":\"\",\"Nombre\":\"\",\"IdCliente\":0},\"ProfesionalConsultora\":[],\"OtroProfesionalConsultora\":[],\"Documentos\":[]},\"SolicitudTitulo\":{\"SolicitaTitulo\":\"\",\"RequisitoA\":{\"Requisito\":false,\"Formato\":\"\",\"Pago\":\"\"},\"RequisitoB\":{\"Requisito\":false,\"Formato\":\"\",\"Pago\":\"\"},\"RequisitoC\":{\"Requisito\":false,\"Formato\":\"\",\"Pago\":\"\"},\"RequisitoD\":{\"Requisito\":false,\"Formato\":\"\",\"Pago\":\"\"},\"RequisitoE\":{\"Requisito\":false,\"Formato\":\"\",\"Pago\":\"\"},\"RequisitoF\":{\"Requisito\":false,\"Formato\":\"\",\"Pago\":\"\"}}}";

        public static class KeyAppConfig
        {
            public const string MsSettings = "MsSettings";
            public const string NotificacionesSvcSettings = "NotificacionesSvcSettings";
            public const string ExternoSvcSettings = "ExternoSvcSettings";
            public const string LaserficheSvcSettings = "LaserficheSvcSettings";
            public const string PagoTupaSvcSettings = "PagoTupaSvcSettings";
        }
        public static class Package
        {
            public static string BANDEJA = "PCK_BANDEJA";
            public static string CONFIGURACION = "PCK_CONFIGURACION";
            public static string CASILLA = "PCK_CASIEL";
            public static string SEGURIDAD = "PCK_SEGURIDAD";
            public static string TUPA = "PCK_TUPA";
            public static string GENERAL = "PCK_GENERAL";
            public static string ADMINISTRADO = "PCK_ADMINISTRADO";
            public static string ESTUDIOS_PRESENTADOS = "PCK_ESTUDIOS_PRESENTADOS";
            public static string ITS = "PCK_ITS";
            public static string OPINANTE = "PCK_OPINANTE";
        }
        public static class Notificacion
        {
            public static class Sistema
            {
                public static int TUPA_DIGITAL = 1;
            }
        }
        public static class TipoDatoReflection
        {
            public static string NULO = "NULO";
            public static string LISTA = "LISTA";
            public static string ENTERO = "ENTERO";
            public static string CADENA = "CADENA";
            public static string ENTIDAD = "ENTIDAD";
        }
        public static class Excel
        {
            public static class Border
            {
                public static int Top = 1;
                public static int Bottom = 2;
                public static int Left = 3;
                public static int Right = 4;
                public static int All = 0;
            }
        }

        public static class TipoPagoTupa
        {
            public static int CAJA_MINEM = 1;
            public static int PAGALO_PE = 2;
            public static int PAGO_EN_LINEA = 3;
        }

        public static class Tupa
        {
            public static string AUTORIZACION_QUEMA_GAS = "AH03";
        }
    }
}

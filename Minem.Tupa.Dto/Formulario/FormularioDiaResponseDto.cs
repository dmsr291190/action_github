using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Formulario
{
    public class FormularioDiaResponseDto
    {
        public ResumenEjecutivo ResumenEjecutivo { get; set; }
        public DescripcionProyecto DescripcionProyecto { get; set; }
        public DescripcionMedioBiologico DescripcionMedioBiologico { get; set; }
        public List<Adjunto> PuntoMuestreo { get; set; }
        public List<Adjunto> Cartografia { get; set; }
        public Arqueologia Arqueologia { get; set; }
        public ParticipacionCiudadana ParticipacionCiudadana { get; set; }
        public List<Adjunto> ImpactosAmbientales { get; set; }
        public PlanManejoAmbiental PlanManejoAmbiental { get; set; }
        public Consultora Consultora { get; set; }
        public SolicitudTitulo SolicitudTitulo { get; set; }
    }

    public class ResumenEjecutivo
    {
        public string Resumen { get; set; }
        public List<Adjunto> Documentos { get; set; }
    }

    public class DescripcionProyecto
    {
        public Antecedentes Antecedentes { get; set; }
        public LocalizacionGeografica LocalizacionGeografica { get; set; }
        public Delimitacion Delimitacion { get; set; }
        public AreasInfluencia AreasInfluencia { get; set; }
        public DescripcionEtapas DescripcionEtapas { get; set; }

    }

    public class DescripcionMedioBiologico
    {
        public string CriteriosEvaluacion { get; set; }
        public string DescripcionEcosistemas { get; set; }
        public List<Adjunto> Documentos { get; set; }
    }

    public class Arqueologia
    {
        public string Descripcion { get; set; }
        public List<Adjunto> Documentos { get; set; }
    }

    public class ParticipacionCiudadana
    {
        public List<Adjunto> Documentos { get; set; }
    }

    public class PlanManejoAmbiental
    {

        public ResumenPlanManejo Resumen { get; set; }
        public PlanVigilanciaAmbiental PlanVigilanciaAmbiental { get; set; }
        public class ResumenPlanManejo
        {
            public List<Adjunto> Documentos { get; set; }
        }
    }

    public class Consultora
    {
        public List<Adjunto> Documentos { get; set; }
    }

    public class PlanVigilanciaAmbiental
    {
        public List<Adjunto> Documentos { get; set; }
    }

    #region "Descripcion Proyectos"
    public class Antecedentes
    {
        public DatosGenerales DatosGenerales { get; set; }
        public DatosTitular DatosTitular { get; set; }
        public List<Adjunto> MapaComponentes { get; set; }
        public List<Adjunto> PropiedadSuperficial { get; set; }
        public List<Adjunto> MapaAreasNaturales { get; set; }

    }

    public class LocalizacionGeografica
    {
        public string Localizacion { get; set; }
        public List<Adjunto> LocalizacionSuperpuesta { get; set; }
    }

    public class Delimitacion
    {
        public List<Adjunto> Documentos { get; set; }
    }

    public class AreasInfluencia
    {
        public List<Adjunto> Documentos { get; set; }
        public List<Adjunto> ArchivoEscaneado { get; set; }
    }

    public class DescripcionEtapas
    {
        public List<Adjunto> Documento { get; set; }
        public List<Adjunto> BalanceAgua { get; set; }
        public List<Adjunto> Archivos { get; set; }
        public List<Adjunto> Instalaciones { get; set; }
        public List<Adjunto> ArchivosMDS { get; set; }
        public List<Adjunto> MapaComponentes { get; set; }
        public List<Adjunto> TopSoil { get; set; }
    }
    #endregion "Descripcion Proyectos"
    public class DatosGenerales
    {
        public string NombreProyecto { get; set; }
        public int UnidadMinera { get; set; }
        public string NombreUnidadMinera { get; set; }
        public string Tipo { get; set; }
        public string NuevaUnidadMinera { get; set; }
    }

    public class DatosTitular
    {
        public string NombreTitular { get; set; }
        public string DireccionTitular { get; set; }
    }

    public class Adjunto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public string DescripcionTipo { get; set; }
        public string FechaRegistroDocumento { get; set; }
        public int Nro { get; set; }
    }

    public class SolicitudTitulo
    {
        public RequisitoAdjunto RequisitoA { get; set; }
        public RequisitoAdjunto RequisitoB { get; set; }
        public RequisitoAdjunto RequisitoC { get; set; }
        public RequisitoAdjunto RequisitoD { get; set; }
        public RequisitoAdjunto RequisitoF { get; set; }
        public RequisitoAdjunto RequisitoE { get; set; }

    }

    public class RequisitoAdjunto
    {
        public string Formato { get; set; }
        public string Pago { get; set; }
        public bool Requisito { get; set; }
    }
}

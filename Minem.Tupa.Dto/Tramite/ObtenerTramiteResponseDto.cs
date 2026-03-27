using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class ObtenerTramiteResponseDto
    {
        public long Id { get; set; }
        public int TupaId { get; set; }
        public string NumExpediente { get; set; }
        public string NumSolicitud { get; set; }
        public string Observaciones { get; set; }
        public string TupaCodigo { get; set; }
        public string TupaNombre { get; set; }
        public string TupaOrganizacion { get; set; }
        public int TupaUnidadId { get; set; }
        public string TupaAcronimo { get; set; }
        public string TupaTipoEvaluacion { get; set; }
        public int TupaPlazoDias { get; set; }
        public string TipoPersona { get; set; }
        /// <value>00001: Persona natural | 00002: Persona juridica | 00004: Persona extranjera | 00005: Persona natural con RUC</value> 
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Estado { get; set; }
        public string FechaCreacion { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string FechaEnvioSub { get; set; }
        public string ClaveAcceso { get; set; }
        public bool Completo { get; set; }
        public bool EsGratuito { get; set; }
        public string CodigoTributo { get; set; }
        public string DocExpediente { get; set; }
        public string DocExpedienteSub { get; set; }
        public int RowId { get; set; }
        public string TipoSubsanacion { get; set; }
        public string FechaAcuseObs { get; set; }
        public string FechaAcuseAte { get; set; }
        public string FechaRecibido { get; set; }
        public string DocumentoAtendido { get; set; }
        public List<TramiteRequisito> Requisitos { get; set; }
        public string Clave { get; set; }
        public long? IdEstudio { get; set; }
        public string NombreProyecto { get; set; }
        public int RespuestaMsg { get; set; }
        public string Ubicacion { get; set; }
        public string NombreEstadoRequisito2 { get; set; }
        public DateTime? FechaInicioVigencia { get; set; }
        public DateTime? FechaFinVigencia { get; set; }
    }

    public class TramiteRequisito
    {
        public long TramiteId { get; set; }
        public long TramiteReqId { get; set; }
        public long TupaId { get; set; }
        public long TupaReqId { get; set; }
        public string Descripcion { get; set; }
        public bool TieneAdjunto { get; set; }
        public bool TieneFormAnexo { get; set; }
        public bool TieneCosto { get; set; }
        public bool Completo { get; set; }
        public string CodigoFormAnexo { get; set; }
        public string RutaDocumento { get; set; }
        public string PlantillaAdjunto { get; set; }
        public long TramiteReqRefId { get; set; }
        public long MovId { get; set; }
        public decimal Costo { get; set; }
        public bool Obligatorio { get; set; }
        public string CodigoTributo { get; set; }
        public string PagoVoucher { get; set; }
        public string PagoFecha { get; set; }
        public string PagoOficina { get; set; }
        public bool Conforme { get; set; }
        public string Observaciones { get; set; }
        public string Clasificador { get; set; }
        public int Orden { get; set; }
        public long ArchivoId { get; set; }
        public int PagosAdicionales { get; set; }
        public decimal MontoAdicional { get; set; }
        public int CodigoObligatorio { get; set; }
        //public TipoSolicitud TipoSolicitud { get; set; }
        public bool RequiereFirma { get; set; }
        public bool Firmado { get; set; }
        public long? ProfAsigId { get; set; }
        //public Profesional ProfAsignado { get; set; }
        public long? RequisitoId { get; set; }
        public string NombreEstado { get; set; }
        #region Campos adicionales
        public string CostoFormatted
        {
            get
            {
                if (TieneCosto)
                {
                    return string.Concat("S/ ", Costo.ToString("#,#.00"));
                }
                return "";
            }
        }
        #endregion

        //Determina la configuracion del Pago
        public int CodDetConCatRequisito { get; set; }
        //Determina el pago realizado
        public int CodDetSolicitud { get; set; }
    }
}

using Minem.Tupa.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Infraestructure
{

    public class Email
    {
        public class MensajeNotificacion
        {
            public string asunto { get; set; }
            public string MensajeCorreo { get; set; }
            public int Categoria { get; set; }
        }
                                                                      
        public static MensajeNotificacion ObtenerMensajeAsunto(int Indicador, int destinoCorreo, int tipo, string nroExpediente, string datosDestinatario, string link, string mensajeAdicional = @"")
        {
            string cuerpo = "";
            var obMensaje = new MensajeNotificacion();

            string Cabecera = @"<tr><th COLSPAN=2><img src=https://www.minem.gob.pe/resources/images/logo_main.jpg alt=Logo Ministerio de Energia y Minas width=402 height=80><br><br>"
                                    + "</th>"
                                    + "</tr>"
                                    + "<tr align = left>"
                                    + "<th COLSPAN=2><strong> Estimado(a) Señor(a): </strong> : " + datosDestinatario + "<br></th>"
                                    + "<br><br></tr>";
            string piePagina = @"<tr><th><strong>Responsable de Entregar la Información de Acceso Público</strong></th></tr>"
                                   + "<tr align = left>"
                                    + "<th ><strong> BG03-2 : Declaración de Impacto Ambiental (DIA) para proyectos de exploración minera (gran y mediana minería) </strong><br></th>"
                                    + "</tr>"
                                    + "<tr align = left>"
                                    + "<th ><strong> Ministerio de Energía y Minas </strong><br></th>"
                                    + "</tr>"
                                    + "<tr align = left>"
                                    + "<th ><strong> Teléfono: (+511) 4111100, 5100300 </strong><br></th>"
                                    + "</tr>";
            if (Indicador == 1)//funcionario
            {
                if (destinoCorreo == 1)//1 casilla
                { 

                }
                else {
                    switch (tipo)
                    {
                        case 1://solicitud para evaluación
                            cuerpo = @"<tr><td> Usted tiene una solicitud pendiente de revisión al procedimiento: BG03-2 : Declaración de Impacto Ambiental (DIA) para proyectos de exploración minera (gran y mediana minería).<br>"
                                    + "Se ha asignado el Número de Expediente: " + nroExpediente + "<br></td>"
                                    + "</tr>";
                            obMensaje.MensajeCorreo = string.Format(@"<html><body><table width=""100%"" style=""font-style:arial;"">{0}{1}</table> </body> </html>", Cabecera, cuerpo);
                            obMensaje.asunto = "SOLICITUD PARA EVALUACION";
                            break;

                        default:
                            break;
                    }
                }
                
              

            }
            else//administrado
            {
                if (destinoCorreo == 1)//1 casilla
                {
                    switch (tipo)
                    {
                        case 1://se ha enviado a casilla su tramite y esta en evaluacion su inicio de tramite.
                            cuerpo = @"<tr><td> Tengo el agrado de informar que se ha enviado su trámite y esta en evaluación al procedimiento: BG03-2 : Declaración de Impacto Ambiental (DIA) para proyectos de exploración minera (gran y mediana minería).<br>"
                                    + "Se ha asignado el Número de Expediente: " + nroExpediente + "<br></td>"
                                    + "</tr>";
                            obMensaje.MensajeCorreo = string.Format(@"<html><body><table width=""100%"" style=""font-style:arial;"">{0}{1}{2}</table> </body> </html>", Cabecera, cuerpo, piePagina);
                            obMensaje.asunto = "INICIO DE TRAMITE";
                            obMensaje.Categoria = 1;
                            break;

                        default:
                            break;
                    }
                }
                else
                { //2 correo
                    switch (tipo)
                    {
                        case 1://se ha enviado su tramite y esta en evaluacion su inicio de tramite.
                            cuerpo = @"<tr><td> Tengo el agrado de informar que se ha enviado su trámite y esta en evaluación al procedimiento: BG03-2 : Declaración de Impacto Ambiental (DIA) para proyectos de exploración minera (gran y mediana minería).<br>"
                                    + "Se ha asignado el Número de Expediente: " + nroExpediente + "<br></td>"
                                    + "</tr>"
                                    + mensajeAdicional;
                            obMensaje.MensajeCorreo = string.Format(@"<html><body><table width=""100%"" style=""font-style:arial;"">{0}{1}{2}</table> </body> </html>", Cabecera, cuerpo, piePagina);
                            obMensaje.asunto = "INICIO DE TRAMITE";
                            obMensaje.Categoria = 1;
                            break;

                        default:
                            break;
                    }
                }
                   
            }
            return obMensaje;
        }
         
    }
}

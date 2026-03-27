using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Utils
{
    public class Enumeration
    {       

        public enum Procedimientos
        {
            FormularioDIA = 25
        }
        public enum StateAssignment
        {
            No_Definido = 0,
            Pendiente = 1,
            En_Proceso = 2,
            Terminado = 3
        }

        public enum ResultAssignment
        {
            Other = -1,
            Activity = 0,
            Not_Activity = -3

        }

        public enum TransactionStates
        {
            PENDIENTE = 2,
            DOCUMENTACION_COMPLETA = 3,
            EN_PROCESO = 4,
            OBSERVADO = 5,
            APROBADO = 6,
            NO_PRESENTADO = 7,
            PENDIENTE_DE_REVISION = 9,
            EN_COLA = 15,
            DESAPROBADO = 16,
            PENDIENTE_DE_COMPLETAR_REQUISITO = 17,
            PENDIENTE_PRESENTACION_FISICA = 27,
            ADJUNTAR_PUBLICACION = 29,
            APROBADO_PENDIENTE_PAGO_DE_TASA = 30,
            FINALIZADO = 33,
            SE_REQUIERE_MAS_DETALLE = 35,
            TICKET_DERIVADO = 37,
            SUSPENDIDO = 47,
            EN_EVALUACION = 58,
        }
    }
}

using Minem.Tupa.Dto.EstudiosPresentados;
using Minem.Tupa.Entity.EstudiosPresentados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IRepository
{
    public interface IEstudiosPresentadosRepository
    {
        Task<List<BandejaEstudiosPresentadosResponse_Entity>> GetBandeja(BandejaEstudiosPresentadosRequest_Entity request);
        Task<List<TipoEstudioResponse_Entity>> GetTipoEstudio();
        Task<List<SituacionResponse_Entity>> GetSituacion();
        Task<long> GuardarAporte(int idSolicitud, int idSolicitudExpediente, string descripcion, int idUser, string CodigoCelular, string CodigoCorreoElectronico,
            string CorreoElectronico, string NombresApellidos, string NumeroCelular, string NumeroDocumento, string Ruc, string TipoDocumento, string TipoPersona, int TipoValidacion);
        Task<long> GuardarDetalleAporte(long idSolicitudAporte, int idUser, int idArchivo);
        Task<List<TipoEstudiosResponse_Entity>> GetListarTipoEstudio();
        Task<List<TipoEstudiosTupaResponse_Entity>> GetListarTipoEstudioTupa(string CodMaeTupa);


    }
}

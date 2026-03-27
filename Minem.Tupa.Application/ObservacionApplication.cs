using AutoMapper;
using Minem.Tupa.Dto.Observacion;
using Minem.Tupa.Entity.Observacion;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;

namespace Minem.Tupa.Application
{
    public class ObservacionApplication(IObservacionRepository observacionRepository, IMapper mapper) : IObservacionApplication
    {
        private readonly IObservacionRepository _observacionRepository = observacionRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<StatusResponse<int>> InsertarObservacion(ObservacionRequestDto request)
        {
            try
            {
                int idtumovmetadahistorico = 0;
                int respuestaCabecera;
                // consulta TUMOVMETADAHISTORICO 
                var respuestaHistorico = _mapper.Map<List<TumovmetadahistoricoDto>>(await _observacionRepository.ConsultaTumovmetadahistorico(request.codmaesolicitud));
                // Verifica si la lista tiene elementos y accede al primer valor de idtumovmetadahistorico
                if (respuestaHistorico != null && respuestaHistorico.Any())
                {
                    // Obtén el primer elemento de la lista
                    idtumovmetadahistorico = respuestaHistorico[0].idtumovmetadahistorico;

                }
                // consulta cabecera comentario 
                var respuestaExisteSolicitudCapitulo = await _observacionRepository.ConsultaExisteSolicitudCapitulo(request.codmaesolicitud, request.capitulo);
                // Verifica si la lista tiene elementos y accede al primer valor de idtumovmetadahistorico
                if (respuestaExisteSolicitudCapitulo == 0)
                {
                    // cabecera 
                    var obshistjsonDto = new ObshistjsonDto();
                    obshistjsonDto.idtumovmetadahistorico = idtumovmetadahistorico;
                    obshistjsonDto.idobshistjson = request.iddetobshistjson;
                    obshistjsonDto.capitulo = request.capitulo;
                    obshistjsonDto.estado = request.estado;
                    obshistjsonDto.usuarioCreacion = request.usuarioCreacion;
                    obshistjsonDto.fechaCreacion = request.fechaCreacion;
                    obshistjsonDto.usuarioUltimaMod = request.usuarioUltimaMod;
                    obshistjsonDto.fechaUltimaMod = request.fechaUltimaMod;
                    obshistjsonDto.codmaesolicitud = request.codmaesolicitud;
                    respuestaCabecera = await _observacionRepository.InsertObshistjson(_mapper.Map<PRC_I_OBSHISTJSON_Request_Entity>(obshistjsonDto));
                    // Aquí puedes asignar el valor o realizar alguna otra operación
                }
                else
                {
                    respuestaCabecera = respuestaExisteSolicitudCapitulo;
                }

                // detalle
                var detobshistjsonDto = new DetobshistjsonDto();
                detobshistjsonDto.iddetobshistjson = request.iddetobshistjson;
                detobshistjsonDto.idobshistjson = respuestaCabecera;
                detobshistjsonDto.codmovpersona = request.codmovpersona;
                detobshistjsonDto.observacion = request.observacion;
                detobshistjsonDto.iddetobshistjsonpadre = request.iddetobshistjsonpadre;
                detobshistjsonDto.orden = request.orden;
                detobshistjsonDto.estado = request.estado;
                detobshistjsonDto.usuarioCreacion = request.usuarioCreacion;
                detobshistjsonDto.estadoObservacion = request.estadoObservacion;
                var respuestaDetalle = await _observacionRepository.InsertDetobshistjson(_mapper.Map<PRC_I_DETOBSHISTJSON_Request_Entity>(detobshistjsonDto));

                if (respuestaCabecera == 1 && respuestaDetalle == 1)
                {
                    return Message.Successful(1);
                }
                else
                {
                    return Message.Successful(0);
                }
            }
            catch (Exception ex)
            {
                return Message.Exception<int>(ex);
            }
        }
        public async Task<StatusResponse<List<ObshistjsonDto>>> ConsultaObshistjson(int codmaesolicitud, string capitulo, int iddetobshistjsonpadre)
        {
            try
            {
                var respuesta = _mapper.Map<List<ObshistjsonDto>>(await _observacionRepository.ConsultaObshistjson(codmaesolicitud, capitulo, iddetobshistjsonpadre));
                return Message.Successful(respuesta);

            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObshistjsonDto>>(ex);
            }
        }
        public async Task<StatusResponse<int>> ActualizarObservacion(ObservacionRequestDto request)
        {
            try
            {

                // detalle
                var detobshistjsonDto = new DetobshistjsonDto();
                detobshistjsonDto.iddetobshistjson = request.iddetobshistjson;
                detobshistjsonDto.idobshistjson = request.idobshistjson;
                detobshistjsonDto.codmovpersona = request.codmovpersona;
                detobshistjsonDto.observacion = request.observacion;
                detobshistjsonDto.iddetobshistjsonpadre = request.iddetobshistjsonpadre;
                detobshistjsonDto.orden = request.orden;
                detobshistjsonDto.estado = request.estado;
                detobshistjsonDto.usuarioCreacion = request.usuarioCreacion;
                detobshistjsonDto.estadoObservacion = request.estadoObservacion;
                

                var respuestaDetalle = await _observacionRepository.UpdateDetobshistjson(_mapper.Map<PRC_I_DETOBSHISTJSON_Request_Entity>(detobshistjsonDto));

                if (respuestaDetalle == 1)
                {
                    return Message.Successful(1);
                }
                else
                {
                    return Message.Successful(0);
                }

            }
            catch (Exception ex)
            {
                return Message.Exception<int>(ex);
            }
        }
        public async Task<StatusResponse<int>> EliminarObservacion(int iddetobshistjson)
        {
            try
            {
                var respuestaDetalle = await _observacionRepository.DeleteDetobshistjson(iddetobshistjson);

                if (respuestaDetalle == 1)
                {
                    return Message.Successful(1);
                }
                else
                {
                    return Message.Successful(0);
                }

            }
            catch (Exception ex)
            {
                return Message.Exception<int>(ex);
            }
        }

        public async Task<StatusResponse<List<ObshistjsonDto>>> ObservacionesSolicitud(int codmaesolicitud)
        {
            try
            {
                var respuesta = _mapper.Map<List<ObshistjsonDto>>(await _observacionRepository.ObservacionesSolicitud(codmaesolicitud));
                return Message.Successful(respuesta);

            }
            catch (Exception ex)
            {
                return Message.Exception<List<ObshistjsonDto>>(ex);
            }
        }
        

    }
}

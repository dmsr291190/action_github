using GraphQL;
using GraphQL.Types;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.IApplication;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Minem.Tupa.Api.TupaGraphQL
{
    public class FormularioQuery : ObjectGraphType
    {
        private readonly IFormularioApplication _service;

        public FormularioQuery(IFormularioApplication service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));


            Field<FormularioType>("formulario")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }))
                .ResolveAsync(async context =>
                {
                    long id = context.GetArgument<long>("id");
                    return await ObtenerFormularioPorId(id);
                });
        }

        private async Task<Formulario> ObtenerFormularioPorId(long id)
        {
            var respuesta = await _service.ObtenerFormularioDia(id);
            return new Formulario { 
                Id = respuesta.Data.Id,
                DataJson = respuesta.Data.DataJson
            };
        }
    }
}

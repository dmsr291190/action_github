using GraphQL;
using GraphQL.Types;
using Minem.Tupa.Dto.Formulario;
using Minem.Tupa.IApplication;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;

namespace Minem.Tupa.Api.TupaGraphQL
{
    public class FormularioMutation : ObjectGraphType
    {
        private readonly IFormularioApplication _service;

        [Obsolete]
        public FormularioMutation(IFormularioApplication service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));

            Field<BooleanGraphType>("actualizarFormulario")
                .Arguments(new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    //new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "jsonData" }
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "campo" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "valor" }
                ))
                .ResolveAsync(async context =>
                {
                    //int id = context.GetArgument<int>("id");
                    //string jsonData = context.GetArgument<string>("jsonData");
                    var id = context.GetArgument<string>("id");
                    var campo = context.GetArgument<string>("campo");
                    var valor = context.GetArgument<string>("valor");

                    var formulario = await _service.ObtenerFormularioDia(long.Parse(id));
                    if (formulario.Success)
                    {
                        context.Errors.Add(new ExecutionError("Formulario no encontrado"));
                        return null;
                    }

                    // Convertir JSON string a objeto
                    var jsonObj = JObject.Parse(formulario.Data.DataJson ?? "{}");

                    // Modificar solo el campo específico
                    jsonObj[campo] = valor;

                    // Convertir de vuelta a string y guardar
                    formulario.Data.DataJson = jsonObj.ToString();

                    return await ActualizarFormulario(long.Parse(id), formulario.Data.DataJson);
                });
        }

        private async Task<long> ActualizarFormulario(long p_CodMaeSolicitud, string jsonData)
        {
           var respuesta = await _service.ActualizarFormulario(p_CodMaeSolicitud, jsonData);
            return respuesta.Data;
        }
    }
}

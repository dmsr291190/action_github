using GraphQL.Types;
using Minem.Tupa.Dto.Formulario;
using Newtonsoft.Json.Linq;
using System;

namespace Minem.Tupa.Api.TupaGraphQL
{
    public class FormularioType : ObjectGraphType<Formulario>
    {
        public FormularioType()
        {
            Name = "Formulario";
            Field(x => x.Id).Description("ID del formulario");
           // Field(x => x.Nombre).Description("Nombre del formulario");
            Field(x => x.DataJson, nullable: true).Description("Datos en formato JSON del formulario");
        }
    }
}

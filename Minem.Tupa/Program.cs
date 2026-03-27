using GraphQL;
using GraphQL.Types;
using GraphQL.Server;
using Microsoft.EntityFrameworkCore;
using Minem.Tupa.Api.TupaGraphQL;
using Minem.Tupa.Api.TupaHub;
using Minem.Tupa.Automapper;
using Minem.Tupa.Data;
using Minem.Tupa.Register.IoC;
using Minem.Tupa.Utils;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<AppSettings>(builder.Configuration);
//CustomExtensions.AddPersistenceServices(builder.Services, builder.Configuration.GetValue<string>("ConnectionStrings:ContextMinem"));
IoCRegisterExtensions.AddCustomIntegration(builder.Services);

builder.Services.AddScoped<FormularioType>();
builder.Services.AddScoped<FormularioQuery>();
builder.Services.AddScoped<FormularioMutation>();
builder.Services.AddScoped<ISchema, Schema>(provider =>
            new Schema
            {
                Query = provider.GetRequiredService<FormularioQuery>(),
                Mutation = provider.GetRequiredService<FormularioMutation>()
            });

builder.Services.AddGraphQL(b => b.AddSystemTextJson());

var configuration = builder.Configuration;
builder.Services.AddHttpClientConfig(configuration); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Minem_Db_Context>(options => options.UseOracle(builder.Configuration.GetConnectionString("ContextMinem")));
builder.Services.AddScoped<DbContext, Minem_Db_Context>();

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        //.WithOrigins(Constants.GetWithOriginsProgram.GetOrigins0, Constants.GetWithOriginsProgram.GetOrigins1, Constants.GetWithOriginsProgram.GetOrigins2)
        .WithOrigins(Constante.ORIGIN_WEB)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddSignalR();
 


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseRouting();

app.MapHub<UserEditingHub>("/hub/userEditingHub");
app.UseGraphQL<ISchema>();
app.UseGraphQLPlayground();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

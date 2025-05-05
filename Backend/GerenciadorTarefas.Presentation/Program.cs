

using GerenciadorTarefas.Application.Services;
using GerenciadorTarefas.Application.Services.Interfaces;
using GerenciadorTarefas.Infrastructure.Persistence.Context;
using GerenciadorTarefas.Infrastructure.Repositories;
using GerenciadorTarefas.Infrastructure.Repositories.Interfaces;
using GerenciadorTarefas.Presentation.Middlewares;
using Microsoft.AspNetCore.Builder;
using Serilog;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddSerilog();

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IncludeFields = true;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<GerenciadorTarefasContext>(connectionString : builder.Configuration.GetConnectionString("GerenciadorTarefas"));

builder.Services.AddScoped<ITarefaCRUDService, TarefaCRUDService>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(cors =>
{
    cors.AllowAnyOrigin();
    cors.AllowAnyHeader();
    cors.AllowAnyMethod();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }
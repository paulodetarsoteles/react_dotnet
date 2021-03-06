using System.Text.Json.Serialization;
using back.src.ProAtividade.Data.Context;
using Microsoft.EntityFrameworkCore;
using ProAtividade.Data.Repositories;
using ProAtividade.Domain.Interfaces.Repositories;
using ProAtividade.Domain.Interfaces.Services;
using ProAtividade.Domain.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration; 

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(); 
builder.Services.AddScoped<IAtividadeRepo, AtividadeRepo>(); 
builder.Services.AddScoped<IGeralRepo, GeralRepo>(); 
builder.Services.AddScoped<IAtividadeService, AtividadeService>(); 
builder.Services.AddControllers()
                .AddJsonOptions
                (
                    options => {options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());}
                );
builder.Services.AddDbContext<DataContext>
(
    options => options.UseSqlite(Configuration.GetConnectionString("Default"))
); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); 

app.Run();
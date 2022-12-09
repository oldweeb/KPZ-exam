using AutoMapper;
using back_end.DB;
using back_end.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<VetClinicDbContext>(c => c.UseSqlServer(builder.Configuration["ConnectionString"]));
services.AddScoped<IAnimalService, AnimalService>();
services.AddScoped<IVisitService, VisitService>();
services.AddSingleton<IMapper>(Mapping.Create());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

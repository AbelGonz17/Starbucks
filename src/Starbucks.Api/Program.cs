using Core.Mappy.Extensions;
using Core.Mappy.Interfaces;
using Starbucks.Api.Extensions;
using Starbucks.Application;
using Starbucks.Application.Categories.DTOs;
using Starbucks.Persistence;

var builder = WebApplication.CreateBuilder(args);
var enviroment = builder.Environment;

builder.Services.AddControllers();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddAplication();

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.RegisterMappings(typeof(CategoryMappingProfile).Assembly);    

await app.ApplyMigration(enviroment);

app.MapControllers();

app.Run();

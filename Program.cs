using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrganizadorContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StandardConnection")));
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment()){
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
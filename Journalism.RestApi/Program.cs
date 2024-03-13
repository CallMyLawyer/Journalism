using System.Data.Common;
using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Persistence.EF.Tags;
using Journalism.Services.Categories;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Tags;
using Journalism.Services.Tags.Contracts;
using Journalism.TaavContracts.Interfaces;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EFDataContext>(
    options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<EFDataContext>();
builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<AuthorCategoryService, AuthorCategoryAppService>();
builder.Services.AddScoped<AuthorCategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<AuthorTagService, AuthorTagAppService>();
builder.Services.AddScoped<AuthorTagRepository, EFTagRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
using System.ComponentModel;
using System.Data.Common;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Persistence.EF.Managment;
using Journalism.Persistence.EF.News;
using Journalism.Persistence.EF.NewsPapers;
using Journalism.Persistence.EF.PublishedNewsPapers;
using Journalism.Persistence.EF.Tags;
using Journalism.Persistence.EF.Users;
using Journalism.RestApi.Services;
using Journalism.Services.Categories;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.Managment;
using Journalism.Services.Managment.Contracts;
using Journalism.Services.News;
using Journalism.Services.News.Contracts;
using Journalism.Services.NewsPapers;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.PublishedNewsPapers;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.Services.Tags;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Users;
using Journalism.Services.Users.Contracts;
using Journalism.TaavContracts.Interfaces;
using Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SQLitePCL;

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
builder.Host.AddAutofac();
builder.Services.AddDbContext<EFDataContext>();

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


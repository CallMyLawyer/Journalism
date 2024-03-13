using System.Data.Common;
using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Persistence.EF.News;
using Journalism.Persistence.EF.NewsPapers;
using Journalism.Persistence.EF.Tags;
using Journalism.Persistence.EF.Users;
using Journalism.Services.Categories;
using Journalism.Services.Categories.Contracts;
using Journalism.Services.News;
using Journalism.Services.News.Contracts;
using Journalism.Services.NewsPapers;
using Journalism.Services.NewsPapers.Contracts;
using Journalism.Services.Tags;
using Journalism.Services.Tags.Contracts;
using Journalism.Services.Users;
using Journalism.Services.Users.Contracts;
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
builder.Services.AddScoped<AuthorNewsPapersRepository, EFNewsPaperRepository>();
builder.Services.AddScoped<AuthorNewsPapersService, AuthorNewsPapersAppService>();
builder.Services.AddScoped<AuthorNewsService, AuthorNewsAppService>();
builder.Services.AddScoped<AuthorNewsRepository, EFNewsRepository>();
builder.Services.AddScoped<UserRepository, EFUserRepository>();
builder.Services.AddScoped<UserService, UserAppService>();

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
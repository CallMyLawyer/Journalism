using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Migrations.Migrations;

const string connectionString = "Server=.;Database=Journalism;Trusted_Connection=True;" +
                                "TrustServerCertificate=True;Integrated Security=true";

var serviceCollection = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb.AddSqlServer()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(_202413032342_AddPublishedNewsPaperTable).Assembly).For.Migrations())
    .BuildServiceProvider();
serviceCollection.GetRequiredService<IMigrationRunner>().MigrateUp(202413032342);
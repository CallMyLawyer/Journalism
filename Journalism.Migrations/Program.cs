using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Migrations.Migrations;

const string connectionString = "Server=.;Database=JournalismTests;Trusted_Connection=True;" +
                                "TrustServerCertificate=True;Integrated Security=true";

var serviceCollection = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb.AddSqlServer()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(_202412031747_AddTagTable).Assembly).For.Migrations())
    .BuildServiceProvider();
serviceCollection.GetRequiredService<IMigrationRunner>().MigrateUp(202412031747);
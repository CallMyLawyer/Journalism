using Journalism.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;

public class BusinessIntegrationTest : EFDataContextDatabaseFixture
{
    public EFDataContext DbContext { get; init; }
    public EFDataContext SetupContext { get; init; }
    public EFDataContext ReadContext { get; init; }


    public BusinessIntegrationTest()
    {
        SetupContext = CreateDataContext();
        DbContext = CreateDataContext();
        ReadContext = CreateDataContext();
    }

    protected void Save<T>(params T[] entities)
        where T : class
    {
        foreach (var entity in entities)
        {
            DbContext.Save(entity);
        }
    }
}
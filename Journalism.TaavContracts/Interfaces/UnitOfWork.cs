namespace Journalism.TaavContracts.Interfaces;

public interface UnitOfWork
{
    Task Begin();
    Task Complete();
    Task Commit();
    Task Rollback();
}
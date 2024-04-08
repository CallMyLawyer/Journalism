using Autofac;
using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Services.Categories;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.RestApi.Services;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(AuthorCategoryAppService).Assembly)
            .AssignableTo<Service>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(EFCategoryRepository).Assembly)
            .AssignableTo<Repository>()
            .AsImplementedInterfaces().AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.RegisterType<EFUnitOfWork>().As<UnitOfWork>();
        base.Load(builder);
    }
}
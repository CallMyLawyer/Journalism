using Autofac;
using Autofac.Extensions.DependencyInjection;
using Journalism.Persistence.EF;
using Journalism.Persistence.EF.Categories;
using Journalism.Services.Categories;
using Journalism.Services.PublishedNewsPapers.Contracts;
using Journalism.TaavContracts.Interfaces;

namespace Journalism.RestApi.Services;

public static class AddFacConfiguration
{
    public static ConfigureHostBuilder AddAutofac(
        this ConfigureHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(_ =>
        {
            _.RegisterModule(new AutofacBusinessModule());
        });
        return builder;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartERP.Development.Application.Avalonia.Services;
using SmartERP.Development.Database;
using SmartERP.Development.Infrastructure.Repositories;

namespace SmartERP.Development.Application.Avalonia
{
    public static class DependencyInjection
    {
        public static IServiceProvider BuildServiceProvider(string connectionString = @"Data Source=GIGABYTE\SQLEXPRESS;Initial Catalog=SmartERP;Integrated Security=True")
        {
            var optionsBuilder = new DbContextOptionsBuilder<DevelopmentContext>();
            optionsBuilder.UseSqlServer(connectionString);
            DevelopmentContext context = new DevelopmentContext(optionsBuilder.Options);
            DevelopmentRepository repository = new DevelopmentRepository(context);
            var services = new ServiceCollection()
                .AddScoped(x => new DevelopmentService(repository))
                .BuildServiceProvider();

            return services;
        }
    }
}

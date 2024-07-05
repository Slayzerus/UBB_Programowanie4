using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartERP.Development.Application.Avalonia.Services;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Database;
using SmartERP.Development.Infrastructure.Repositories;

namespace SmartERP.Development.Application.Avalonia
{
    public static class DependencyInjection
    {
        public static IServiceProvider BuildServiceProvider(string connectionString = @"Data Source=AERO16; Database=SmartERP; Integrated Security=true;TrustServerCertificate=True")
        {
            var optionsBuilder = new DbContextOptionsBuilder<DevelopmentContext>();
            optionsBuilder.UseSqlServer(connectionString);
            DevelopmentContext context = new DevelopmentContext(optionsBuilder.Options);
            DevelopmentRepository repository = new DevelopmentRepository(context);
            var services = new ServiceCollection()
                .AddScoped<IDevelopmentService>(x => new DevelopmentService(repository))
                .BuildServiceProvider();

            return services;
        }
    }
}

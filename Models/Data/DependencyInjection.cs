using LAHJAAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionDataContext
{

    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblyName = typeof(DataContext).Assembly.GetName().Name;
        services.AddDbContext<DataContext>(option =>
        {
            option.EnableSensitiveDataLogging();
            option.EnableDetailedErrors();
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
           x =>
           {
               x.MigrationsAssembly(assemblyName);
               x.EnableRetryOnFailure();
           }
           );
        });
        //.ValidateDataAnnotations()
        //.ValidateOnStart();


        return services;
    }

    //// تحميل إعدادات الاتصال من ملف appsettings.json
    //var configuration = new ConfigurationBuilder()
    //    .SetBasePath(Directory.GetCurrentDirectory())
    //    .AddJsonFile("appsettings.json")
    //    .Build();

    //var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    //var connectionString = configuration.GetConnectionString("DefaultConnection");
}

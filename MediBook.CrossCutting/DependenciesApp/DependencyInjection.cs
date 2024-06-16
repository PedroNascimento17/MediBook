using MediBook.Domain.Abstractions;
using MediBook.Infrastructure.Context;
using MediBook.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediBook.CrossCutting.DependenciesApp;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("MediBook.Infrastructure")));
        services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("MediBook.Infrastructure")));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddScoped<IPatientRepository, PatientRepository>();
        return services;
    }
}

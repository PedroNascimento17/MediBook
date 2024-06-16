using MediBook.Domain.Abstractions;
using MediBook.Domain.Entities;
using MediBook.Infrastructure.Context;
using MediBook.Infrastructure.Repositories;
using MediBook.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediBook.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("MediBook.Infrastructure")));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IMedicalDiaryRepository, MedicalDiaryRepository>();
        services.AddScoped<IMedicalSpecialtyRepository, MedicalSpecialtyRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IZipService, ZipService>();
        return services;
    }
}

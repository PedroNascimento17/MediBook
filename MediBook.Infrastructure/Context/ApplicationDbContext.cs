using MediBook.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace MediBook.Infrastructure.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Patient>? Patient { get; set; }
    public DbSet<Doctor>? Doctor { get; set; }
    public DbSet<MedicalSpecialty>? MedicalSpecialty { get; set; }
    public DbSet<DoctorMedicalSpecialty>? DoctorMedicalSpecialty { get; set; }
    public DbSet<MedicalDiary>? MedicalDiary { get; set; }
    public DbSet<Appointment>? Appointment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
              .HasOne(e => e.MedicalDiary)
              .WithOne(e => e.Appointment)
              .HasForeignKey<Appointment>(a => a.MedicalDiaryId);
        base.OnModelCreating(modelBuilder);
    }
}

using MediBook.Domain.Abstractions;
using MediBook.Domain.Entities;
using MediBook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MediBook.Infrastructure.Repositories;

public class DoctorRepository(ApplicationDbContext context) : IDoctorRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<Doctor> Add(Doctor entity)
    {
        if (context is not null && entity is not null && context.Doctor is not null)
        {
            context.Doctor.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        throw new InvalidOperationException("Dados inválidos");
    }

    public async Task<bool> Delete(Guid id)
    {
        var doctor = await Get(id);

        if (context is not null && doctor is not null && context.Doctor is not null)
        {
            doctor.Active = false;
            return await Update(doctor);
        }

        return false;
    }

    public async Task<Doctor?> Get(Guid id)
    {
        if (context is not null && context.Doctor is not null)
        {
            return await context.Doctor.Where(d => d.Active)
                                       .Include(d => d.ApplicationUser)
                                       .Include(d => d.DoctorMedicalSpecialty)
                                            .ThenInclude(d => d.MedicalSpecialty)
                                       .FirstOrDefaultAsync(p => p.DoctorId == id);
        }

        return null;
    }

    public async Task<IEnumerable<Doctor>> GetAll()
    {
        if (context is not null && context.Doctor is not null)
        {
            return await context.Doctor.Where(d => d.Active)
                                       .Include(d => d.ApplicationUser)
                                       .Include(d => d.DoctorMedicalSpecialty)
                                            .ThenInclude(d => d.MedicalSpecialty)
                                       .ToListAsync();
        }

        return [];
    }

    public async Task<bool> Update(Doctor entity)
    {
        if (context is not null && entity is not null && context.Doctor is not null)
        {
            context.Entry(entity).State = EntityState.Modified;
            var updates = await context.SaveChangesAsync();
            return updates > 0;
        }

        return false;
    }

    public async Task<Doctor?> GetByUser(string id)
    {
        if (context is not null && context.Doctor is not null)
        {
            return await context.Doctor.Where(p => p.Active)
                                       .Include(d => d.ApplicationUser)
                                       .Include(d => d.DoctorMedicalSpecialty)
                                            .ThenInclude(d => d.MedicalSpecialty)
                                        .Include(p => p.ApplicationUser)
                                        .FirstOrDefaultAsync(p => p.ApplicationUser.Id == id);
        }

        return null;
    }
}

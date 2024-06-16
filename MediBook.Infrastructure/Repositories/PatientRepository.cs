using MediBook.Domain.Abstractions;
using MediBook.Domain.Entities;
using MediBook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MediBook.Infrastructure.Repositories;

public class PatientRepository(ApplicationDbContext context) : IPatientRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<Patient> Add(Patient entity)
    {
        if (context is not null && entity is not null && context.Patient is not null)
        {
            context.Patient.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        throw new InvalidOperationException("Dados inválidos");
    }

    public async Task<bool> Delete(Guid id)
    {
        var patient = await Get(id);

        if (context is not null && patient is not null && context.Patient is not null)
        {
            patient.Active = false;
            return await Update(patient);
        }

        return false;
    }

    public async Task<Patient?> Get(Guid id)
    {
        if (context is not null && context.Patient is not null)
        {
            return await context.Patient.Where(p => p.Active)
                                        .Include(p => p.ApplicationUser)
                                        .FirstOrDefaultAsync(p => p.PatientID == id);
        }

        return null;
    }

    public async Task<Patient?> GetByUser(string id)
    {
        if (context is not null && context.Patient is not null)
        {
            return await context.Patient.Where(p => p.Active)
                                        .Include(p => p.ApplicationUser)
                                        .FirstOrDefaultAsync(p => p.ApplicationUser.Id == id);
        }

        return null;
    }

    public async Task<IEnumerable<Patient>> GetAll()
    {
        if (context is not null && context.Patient is not null)
        {
            return await context.Patient.Where(p => p.Active)
                                        .Include(p => p.ApplicationUser)
                                        .ToListAsync();
        }

        return [];
    }

    public async Task<bool> Update(Patient entity)
    {
        if (context is not null && entity is not null && context.Patient is not null)
        {
            context.Entry(entity).State = EntityState.Modified;
            var updates = await context.SaveChangesAsync();
            return updates > 0;
        }

        return false;
    }
}

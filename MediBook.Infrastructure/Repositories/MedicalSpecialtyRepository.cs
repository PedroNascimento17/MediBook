using MediBook.Domain.Abstractions;
using MediBook.Domain.Entities;
using MediBook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MediBook.Infrastructure.Repositories;

public class MedicalSpecialtyRepository(ApplicationDbContext context) : IMedicalSpecialtyRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<MedicalSpecialty> Add(MedicalSpecialty entity)
    {
        if (context is not null && entity is not null && context.MedicalSpecialty is not null)
        {
            context.MedicalSpecialty.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        throw new InvalidOperationException("Dados inválidos");
    }

    public async Task<bool> Delete(Guid id)
    {
        var medicalSpecialty = await Get(id);

        if (context is not null && medicalSpecialty is not null && context.MedicalSpecialty is not null)
        {
            medicalSpecialty.Active = false;
            return await Update(medicalSpecialty);
        }

        return false;
    }

    public async Task<MedicalSpecialty?> Get(Guid id)
    {
        if (context is not null && context.MedicalSpecialty is not null)
        {
            return await context.MedicalSpecialty.FirstOrDefaultAsync(p => p.Active && p.MedicalSpecialtyId == id);
        }

        return null;
    }

    public async Task<IEnumerable<MedicalSpecialty>> GetAll()
    {
        if (context is not null && context.MedicalSpecialty is not null)
        {
            return await context.MedicalSpecialty.Where(m => m.Active).ToListAsync();
        }

        return [];
    }

    public async Task<bool> Update(MedicalSpecialty entity)
    {
        if (context is not null && entity is not null && context.MedicalSpecialty is not null)
        {
            context.Entry(entity).State = EntityState.Modified;
            var updates = await context.SaveChangesAsync();
            return updates > 0;
        }

        return false;
    }

    public async Task<bool> BindWithDoctor(Guid id, Guid doctorId)
    {
        var medicalSpecialty = await Get(id);
        var doctor = await GetDoctor(doctorId);

        if (context is not null && medicalSpecialty is not null && doctor is not null && context.DoctorMedicalSpecialty is not null)
        {
            var doctorMedicalSpecialty = new DoctorMedicalSpecialty(doctorId, id, true);
            context.DoctorMedicalSpecialty.Add(doctorMedicalSpecialty);
            var rows = await context.SaveChangesAsync();
            return rows > 0;
        }

        throw new InvalidOperationException("Especialidade ou médico não encontrados");
    }

    public async Task<bool> UnbindWithDoctor(Guid id, Guid doctorId)
    {
        var doctorMedicalSpecialty = context?.DoctorMedicalSpecialty?.FirstOrDefault(d => d.MedicalSpecialtyId == id &&
                                                                                        d.DoctorId == doctorId);
        if (context is not null && context.DoctorMedicalSpecialty is not null && doctorMedicalSpecialty is not null)
        {
            doctorMedicalSpecialty.Active = false;
            context.Entry(doctorMedicalSpecialty).State = EntityState.Modified;
            var updates = await context.SaveChangesAsync();
            return updates > 0;
        }

        return false;
    }

    private async Task<Doctor?> GetDoctor(Guid id)
    {
        if (context is not null && context.Doctor is not null)
        {
            return await context.Doctor.FirstOrDefaultAsync(p => p.Active && p.DoctorId == id);
        }

        return null;
    }
}

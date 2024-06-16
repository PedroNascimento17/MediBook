using MediBook.Domain.Abstractions;
using MediBook.Domain.Entities;
using MediBook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MediBook.Infrastructure.Repositories;

public class MedicalDiaryRepository(ApplicationDbContext context) : IMedicalDiaryRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<MedicalDiary> Add(MedicalDiary entity)
    {
        if (context is not null && entity is not null && context.MedicalDiary is not null)
        {
            context.MedicalDiary.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        throw new InvalidOperationException("Dados inválidos");
    }

    public async Task<bool> Delete(Guid id)
    {
        var medicalDiary = await Get(id);

        if (context is not null && medicalDiary is not null && context.MedicalDiary is not null)
        {
            medicalDiary.Active = false;
            return await Update(medicalDiary);
        }

        return false;
    }

    public async Task<MedicalDiary?> Get(Guid id)
    {
        if (context is not null && context.MedicalDiary is not null)
        {
            return await context.MedicalDiary.Where(m => m.Active)
                                             .Include(m => m.Doctor)
                                             .Include(m => m.MedicalSpecialty)
                                             .FirstOrDefaultAsync(p => p.MedicalDiaryId == id);
        }

        return null;
    }

    public async Task<IEnumerable<MedicalDiary>> GetAll()
    {
        if (context is not null && context.MedicalDiary is not null)
        {
            return await context.MedicalDiary.Where(m => m.Active)
                                             .Include(m => m.Doctor)
                                             .Include(m => m.MedicalSpecialty)
                                             .ToListAsync();
        }

        return [];
    }

    public async Task<IEnumerable<MedicalDiary>> GetAllByDoctor(Guid doctorId)
    {
        if (context is not null && context.MedicalDiary is not null)
        {
            return await context.MedicalDiary.Where(m => m.Active && m.DoctorId == doctorId)
                                             .Include(m => m.Doctor)
                                             .Include(m => m.MedicalSpecialty)
                                             .ToListAsync();
        }

        return [];
    }

    public async Task<bool> Update(MedicalDiary entity)
    {
        if (context is not null && entity is not null && context.MedicalDiary is not null)
        {
            context.Entry(entity).State = EntityState.Modified;
            var updates = await context.SaveChangesAsync();
            return updates > 0;
        }

        return false;
    }

    public async Task<IEnumerable<MedicalDiary>> GetByDoctorMedicalSpecialtyAndDate(Guid doctorId, Guid medicalSpecialtyId, DateTime dateTime)
    {
        if (context is not null && context.MedicalDiary is not null)
        {
            return await context.MedicalDiary.Where(p => p.Active &&
                                                         p.DoctorId == doctorId && 
                                                         p.MedicalSpecialtyId == medicalSpecialtyId &&
                                                         p.DateAndTime.Date == dateTime.Date)
                                             .ToListAsync();
        }

        return [];
    }

    public async Task<IEnumerable<MedicalDiary>> GetAvailableByDoctorMedicalSpecialtyAndDate(Guid doctorId, Guid medicalSpecialtyId, DateTime dateTime)
    {
        if (context is not null && context.MedicalDiary is not null)
        {
            return await context.MedicalDiary.Include(m => m.Appointment)
                                             .Where(p => p.Active &&
                                                         p.DoctorId == doctorId &&
                                                         p.MedicalSpecialtyId == medicalSpecialtyId &&
                                                         p.DateAndTime.Date == dateTime.Date &&
                                                         (p.Appointment == null || p.Appointment.AppointmentStatus != Domain.Enums.AppointmentStatus.Scheduled))
                                             .ToListAsync();
        }

        return [];
    }

    public async Task<IEnumerable<MedicalDiary>> GetByDoctorAndMedicalSpecialty(Guid doctorId, Guid medicalSpecialtyId)
    {
        if (context is not null && context.MedicalDiary is not null)
        {
            return await context.MedicalDiary.Where(p => p.Active &&
                                                         p.DoctorId == doctorId &&
                                                         p.MedicalSpecialtyId == medicalSpecialtyId)
                                             .ToListAsync();
        }

        return [];
    }
}

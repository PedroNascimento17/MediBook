using MediBook.Domain.Abstractions;
using MediBook.Domain.Entities;
using MediBook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MediBook.Infrastructure.Repositories;

public class AppointmentRepository(ApplicationDbContext context) : IAppointmentRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<Appointment> Add(Appointment entity)
    {
        if (context is not null && entity is not null && context.Appointment is not null)
        {
            context.Appointment.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        throw new InvalidOperationException("Dados inválidos");
    }

    public async Task<bool> Delete(Guid id)
    {
        var appointment = await Get(id);   

        if (context is not null && appointment is not null && context.Appointment is not null)
        {
            appointment.AppointmentStatus = Domain.Enums.AppointmentStatus.Canceled;
            return await Update(appointment);
        }

        return false;
    }

    public async Task<Appointment?> Get(Guid id)
    {
        if (context is not null && context.Appointment is not null)
        {
            return await context.Appointment.Include(a => a.MedicalDiary)
                                            .ThenInclude(a => a.Doctor)
                                            .Include(a => a.MedicalDiary)
                                            .ThenInclude(a => a.MedicalSpecialty)
                                            .Include(a => a.Patient)
                                            .FirstOrDefaultAsync(p => p.Active && p.AppointmentId == id);
        }

        return null;
    }

    public async Task<IEnumerable<Appointment>> GetAll()
    {
        if (context is not null && context.Appointment is not null)
        {
            return await context.Appointment.Include(a => a.MedicalDiary)
                                            .ThenInclude(a => a.Doctor)
                                            .Include(a => a.MedicalDiary)
                                            .ThenInclude(a => a.MedicalSpecialty)
                                            .Include(a => a.Patient)
                                            .Where(a => a.Active)
                                            .OrderBy(a => a.MedicalDiary.DateAndTime)
                                            .ToListAsync();
        }

        return [];
    }

    public async Task<IEnumerable<Appointment>> GetByIdAll(Guid patientId)
    {
        if (context is not null && context.Appointment is not null)
        {
            return await context.Appointment.Include(a => a.MedicalDiary)
                                            .ThenInclude(a => a.Doctor)
                                            .Include(a => a.MedicalDiary)
                                            .ThenInclude(a => a.MedicalSpecialty)
                                            .Include(a => a.Patient)
                                            .Where(a => a.Active && a.PatientID == patientId && a.AppointmentStatus == Domain.Enums.AppointmentStatus.Scheduled)
                                            .OrderBy(a => a.MedicalDiary.DateAndTime)
                                            .ToListAsync();
        }

        return [];
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAll(Guid doctorId)
    {
        if (context is not null && context.Appointment is not null)
        {
            return await context.Appointment.Include(a => a.MedicalDiary)
                                            .ThenInclude(a => a.Doctor)
                                            .Include(a => a.MedicalDiary)
                                            .ThenInclude(a => a.MedicalSpecialty)
                                            .Include(a => a.Patient)
                                            .Where(a => a.Active && a.MedicalDiary.DoctorId == doctorId && a.AppointmentStatus == Domain.Enums.AppointmentStatus.Scheduled)
                                            .OrderBy(a => a.MedicalDiary.DateAndTime)
                                            .ToListAsync();
        }

        return [];
    }

    public async Task<bool> Update(Appointment entity)
    {
        if (context is not null && entity is not null && context.Appointment is not null)
        {
            context.Entry(entity).State = EntityState.Modified;
            var updates = await context.SaveChangesAsync();
            return updates > 0;
        }

        return false;
    }
}

using MediBook.Domain.Entities;

namespace MediBook.Domain.Abstractions;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetByIdAll(Guid patientId);
    Task<IEnumerable<Appointment>> GetByDoctorIdAll(Guid doctorId);
}

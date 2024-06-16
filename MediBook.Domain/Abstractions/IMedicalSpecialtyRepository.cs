using MediBook.Domain.Entities;

namespace MediBook.Domain.Abstractions;

public interface IMedicalSpecialtyRepository : IRepository<MedicalSpecialty>
{
    Task<bool> BindWithDoctor(Guid id, Guid doctorId);
    Task<bool> UnbindWithDoctor(Guid id, Guid doctorId);
}

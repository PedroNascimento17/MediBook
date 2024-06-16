using MediBook.Domain.Entities;

namespace MediBook.Domain.Abstractions;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetByUser(string id);
}

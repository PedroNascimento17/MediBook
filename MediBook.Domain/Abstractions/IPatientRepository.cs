using MediBook.Domain.Entities;

namespace MediBook.Domain.Abstractions;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByUser(string id);
}

using MediBook.Domain.Entities;

namespace MediBook.Domain.Abstractions;

public interface IMedicalDiaryRepository : IRepository<MedicalDiary>
{
    Task<IEnumerable<MedicalDiary>> GetByDoctorMedicalSpecialtyAndDate(Guid doctorId, Guid medicalSpecialtyId, DateTime dateTime);
    Task<IEnumerable<MedicalDiary>> GetByDoctorAndMedicalSpecialty(Guid doctorId, Guid medicalSpecialtyId);
    Task<IEnumerable<MedicalDiary>> GetAvailableByDoctorMedicalSpecialtyAndDate(Guid doctorId, Guid medicalSpecialtyId, DateTime dateTime);
    Task<IEnumerable<MedicalDiary>> GetAllByDoctor(Guid doctorId);
}

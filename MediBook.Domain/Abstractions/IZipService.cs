using MediBook.Domain.Models;

namespace MediBook.Domain.Abstractions;

public interface IZipService
{
    Task<ZipData> GetZip(string zipCode);
}

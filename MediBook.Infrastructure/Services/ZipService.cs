using Flurl.Http;
using MediBook.Domain.Abstractions;
using MediBook.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace MediBook.Infrastructure.Services;

public class ZipService(IConfiguration configuration) : IZipService
{
    public async Task<ZipData> GetZip(string zipCode)
    {
        ZipData zipData;
        try
        {
            var url = configuration.GetValue<string>("ZipServiceUrl") ?? "";
            url = url.Replace("{zip}", zipCode);

            zipData = await url.GetJsonAsync<ZipData>();

            if (string.IsNullOrWhiteSpace(zipData?.Localidade))
            {
                zipData = new ZipData()
                {
                    Logradouro = "Endereço não encontrado"
                };
            }
        }
        catch 
        {
            zipData = new ZipData()
            {
                Logradouro = "Endereço não encontrado"
            };
        }

        return zipData;
    }
}

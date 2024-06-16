using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MediBook.Infrastructure.Context;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private readonly string _connectionString =
        @"Server=PEDRO-PC\\MSSQLSERVER01;Database=MediBookDb;User Id=sa;Password=123456;TrustServerCertificate=True";

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(_connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

//migration olusmadı cozum:
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using petsas2.Data;

public class ApplicationDbContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
               optionsBuilder.UseSqlServer(
            "Server=SELINPIR\\MSSQLSERVER01;Database=PetsasDbTest;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

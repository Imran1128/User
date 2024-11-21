using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using UserRegistration.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=mehedi1128_;User ID=mehedi1128_;Password=Imr123;Trusted_Connection=False;Encrypt=False;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

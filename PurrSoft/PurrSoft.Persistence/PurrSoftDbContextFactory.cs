using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PurrSoft.Persistence;

public class PurrSoftDbContextFactory : IDesignTimeDbContextFactory<PurrSoftDbContext>
{
    public PurrSoftDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PurrSoftDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PurrSoft;Username=robert12;Password=Asmodeus011235");
        return new PurrSoftDbContext(optionsBuilder.Options);
    }
}

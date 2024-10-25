using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PurrSoft.Persistence;

public class PurrSoftDbContextFactory : IDesignTimeDbContextFactory<PurrSoftDbContext>
{
    public PurrSoftDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PurrSoftDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=PurrSoft;Username=postgres;Password=piki");
        return new PurrSoftDbContext(optionsBuilder.Options);
    }
}

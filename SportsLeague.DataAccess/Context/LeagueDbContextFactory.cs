using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SportsLeague.DataAccess.Context;

public class LeagueDbContextFactory : IDesignTimeDbContextFactory<LeagueDbContext>
{
    public LeagueDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LeagueDbContext>();
        // Use environment variable if present, otherwise fallback to LocalDB default used in development
        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection")
            ?? "Server=(localdb)\\mssqllocaldb;Database=SportsLeagueDb;Trusted_Connection=True;";

        optionsBuilder.UseSqlServer(connectionString);
        return new LeagueDbContext(optionsBuilder.Options);
    }
}

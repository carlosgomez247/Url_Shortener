using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Contexts
{
    public class UrlShortenerContextSqlServer : DbContext
    {
        public DbSet<Entities.UrlShortener> urlShorteners { get; set; }

        public UrlShortenerContextSqlServer(DbContextOptions<UrlShortenerContextSqlServer> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}

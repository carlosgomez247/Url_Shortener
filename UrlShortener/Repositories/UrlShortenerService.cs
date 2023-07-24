using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Contexts;
using UrlShortener.Entities;

namespace UrlShortener.Repositories
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly UrlShortenerContextSqlServer _urlShortenerContext;
        private readonly IConfiguration _configuration;      

        public UrlShortenerService(UrlShortenerContextSqlServer urlShortenerContext, IConfiguration configuration)
        {
            _urlShortenerContext = urlShortenerContext ?? throw new ArgumentNullException(nameof(urlShortenerContext));
            _configuration = configuration;
        }
        public async Task<string> GetUrlShortenerAsync(string url)
        {
            var existingUrl = await GetUrlShortenerByUrlAsync(url);
            if (existingUrl != null)            {
                
                return $"{_configuration["Url:Dev"]!}/url/{existingUrl.shortGuid}";
            }

            string shortGuid = GetGuid();
            var urlShortener = new Entities.UrlShortener { url = url, shortGuid = shortGuid };
            _urlShortenerContext.urlShorteners.Add(urlShortener);
            await _urlShortenerContext.SaveChangesAsync();

            return $"{_configuration["Url:Dev"]!}/url/{shortGuid}";
        }

        public async Task<Entities.UrlShortener?> GetUrlShortenerByShortGuidAsync(string shortGuid)
        {
            return await _urlShortenerContext.urlShorteners
                 .Where(x => x.shortGuid == shortGuid)
                 .FirstOrDefaultAsync();
        }

        public string GetGuid()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        public async Task<Entities.UrlShortener?> GetUrlShortenerByUrlAsync(string url)
        {
            return await _urlShortenerContext.urlShorteners
                 .Where(x => x.url == url)
                 .FirstOrDefaultAsync();
        }
    }
}

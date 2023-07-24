namespace UrlShortener.Repositories
{
    public interface IUrlShortenerService
    {
        public Task<string> GetUrlShortenerAsync(string url);

        public Task<Entities.UrlShortener?> GetUrlShortenerByShortGuidAsync(string shortGuid);

        public Task<Entities.UrlShortener?> GetUrlShortenerByUrlAsync(string url);
    }
}

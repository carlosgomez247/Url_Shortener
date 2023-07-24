using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using UrlShortener.Repositories;

namespace UrlShortener.Controllers
{
    [Route("url")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlShortenerService _urlShortenerService;

        public UrlShortenerController(IUrlShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService ?? throw new ArgumentNullException(nameof(urlShortenerService));
        }
        [HttpPost()]
        public async Task<ActionResult<string>> UrlShortener([FromBody] string url)
        {
            if (string.IsNullOrEmpty(url) || url.Length > 250)
            {
                return BadRequest("La URL debe tener una longitud entre 1 y 250 caracteres.");
            }
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return BadRequest("Url invalida");
            }

            string shortUrl = await _urlShortenerService.GetUrlShortenerAsync(url);

            return Ok(shortUrl);
        }

        [HttpGet("{shortGuid}")]
        public async Task<ActionResult> UrlRedirect(string shortGuid)
        {
            var url = await _urlShortenerService.GetUrlShortenerByShortGuidAsync(shortGuid);
            if (url == null)
            {
                return NotFound();
            }
            return Redirect(url.url);
        }
    }
}

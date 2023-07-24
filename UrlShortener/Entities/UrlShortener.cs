using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Entities
{
    public class UrlShortener
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string url { get; set; } = null!;

        [Required]
        public string shortGuid { get; set; } = null!;
    }
}

using Microsoft.JSInterop.Infrastructure;

namespace Screenly_api.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}

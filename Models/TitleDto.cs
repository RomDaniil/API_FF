namespace Screenly_api.Models
{
    public class TitleDto
    {
        public int TitleId { get; set; }
        public string Name { get; set; } = null!;
        public short? ReleaseYear { get; set; }
        public decimal? Rating { get; set; }
        public string? PosterUrl { get; set; }
    }
}

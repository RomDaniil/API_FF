namespace Screenly_api.Models
{
    public class Title
    {
        public int TitleId { get; set; }
        public byte TitleType { get; set; }
        public string Name { get; set; } = null!;
        public string? OriginalName { get; set; }
        public string? Description { get; set; }
        public short? ReleaseYear { get; set; }
        public short? EndYear { get; set; }
        public short? DurationMinutes { get; set; }
        public string? AgeRating { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public int? CountryId { get; set; }
        public int? OriginalLanguageId { get; set; }
        public decimal? Rating { get; set; }
        public int RatingCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

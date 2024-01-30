namespace API_ArtworkSharingPlatform.ViewModels.PostViewModels
{
    public class CreateArtworkDTO
    {
        public int ArtworkPId { get; set; }
        public string? ContentArtwork { get; set; }
        public double? PriceArtwork { get; set; }
        public DateTime? DatePost { get; set; }
        public string? PictureArtwork { get; set; }
        public int? GenreId { get; set; }
        public int? UserId { get; set; }
        public int? Quanity { get; set; }
        public bool? Status { get; set; }
    }
}

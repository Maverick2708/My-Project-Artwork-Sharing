namespace API_ArtworkSharingPlatform.ViewModels.RateStarViewModels
{
    public class CreateRateStarDTO
    {
        public int RateId { get; set; }
        public int? UserId { get; set; }
        public int? ArtworkPId { get; set; }
        public int? Rate { get; set; }
    }
}

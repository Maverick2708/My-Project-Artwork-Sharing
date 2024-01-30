namespace API_ArtworkSharingPlatform.ViewModels.FollowViewModels
{
    public class CreateFollowDTO
    {
        public int FollowId { get; set; }
        public int? UserId { get; set; }
        public int? UserIdFollow { get; set; }
        public DateTime? DateFollow { get; set; }
        public bool? Status { get; set; }
    }
}

namespace API_ArtworkSharingPlatform.ViewModels.RequestViewModels
{
    public class CreateRequestDTO
    {
        public int RequestId { get; set; }
        public int? UserId { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}

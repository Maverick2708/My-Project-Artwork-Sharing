namespace API_ArtworkSharingPlatform.ViewModels.NotificationViewModels
{
    public class CreateNotificationDTO
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public string? ContentNoti { get; set; }
        public DateTime? DateNoti { get; set; }
        public bool? Status { get; set; }
    }
}

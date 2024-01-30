namespace API_ArtworkSharingPlatform.ViewModels.ReportViewModels
{
    public class CreateReportDTO
    {
        public int ReportId { get; set; }
        public int? ArtworkPId { get; set; }
        public int? UserId { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}

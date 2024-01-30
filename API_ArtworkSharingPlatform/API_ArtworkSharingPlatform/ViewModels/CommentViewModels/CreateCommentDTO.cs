namespace API_ArtworkSharingPlatform.ViewModels.CommentViewModels
{
    public class CreateCommentDTO
    {
        public int CommentId { get; set; }
        public string? ContentComment { get; set; }
        public DateTime? DateSub { get; set; }
        public int? UserId { get; set; }
        public int? ArtworkPId { get; set; }
        public bool? IsLikePost { get; set; }
        public bool? Status { get; set; }
    }
}

namespace API_ArtworkSharingPlatform.ViewModels.PersonViewModels
{
    public class CreatePersonDTO
    {

        public int UserId { get; set; }
        public string? FullName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateUserRe { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }
        public string Avatar { get; set; } = null!;
        public string? Email { get; set; }

    }
}

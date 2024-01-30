using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Artworks = new HashSet<Artwork>();
        }

        public int GenreId { get; set; }
        public string? GenreArtwork { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Artwork> Artworks { get; set; }
    }
}

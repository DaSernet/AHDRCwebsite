namespace AHDRCwebsite.Models
{
    public class ArtworkImage
    {
        public int ArtworkImageId { get; set; }
        public string ImageURL { get; set; }
        public string ImageSize { get; set; }
        public Artwork Artwork { get; set; }
    }
}
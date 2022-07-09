namespace AHDRCwebsite.Models
{
    public class ArtworkImage
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public Artwork Artwork { get; set; }
    }
}

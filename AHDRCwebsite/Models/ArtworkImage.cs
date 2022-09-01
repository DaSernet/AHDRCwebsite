namespace AHDRCwebsite.Models
{
    public class ArtworkImage
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }

        public string Courtesy { get; set; }
        public string Photographer { get; set; }

        public string Copyright { get; set; }

        public string ImageSize { get; set; }
        public Artwork Artwork { get; set; }
    }
}
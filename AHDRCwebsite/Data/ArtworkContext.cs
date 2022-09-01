using AHDRCwebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace AHDRCwebsite.Data
{
    public class ArtworkContext : DbContext
    {
        public ArtworkContext(DbContextOptions<ArtworkContext> options) : base(options)
        {
        }

        public DbSet<Artwork> Artworks { get; set; }

        public DbSet<ArtworkImage> ArtworkImages { get; set; }
    }
}
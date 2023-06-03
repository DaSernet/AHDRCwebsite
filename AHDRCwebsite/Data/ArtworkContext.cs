using AHDRCwebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace AHDRCwebsite.Data
{
    public class ArtworkContext : DbContext
    {
        public ArtworkContext(DbContextOptions<ArtworkContext> options) : base(options)
        {
        }
        public DbSet<ViewingHistory> ViewingHistories { get; set; }
        public DbSet<Artwork> Artworks { get; set; }

        public DbSet<ArtworkImage> ArtworkImages { get; set; }
    }
}
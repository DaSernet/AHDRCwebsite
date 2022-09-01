using AHDRCwebsite.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AHDRCwebsite.ViewModel
{
    public class ArtImages
    {
        public List<IFormFile> Images { get; set; }
        public Artwork Artwork { get; set; }
    }
}
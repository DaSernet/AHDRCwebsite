using System;

namespace AHDRCwebsite.Models
{
    public class ViewingHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Artwork Artwork { get; set; }
        public DateTime ViewedDateTime { get; set; }
    }

}

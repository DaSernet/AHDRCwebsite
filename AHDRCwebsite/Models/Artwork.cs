using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AHDRCwebsite.Models
{
    public class Artwork
    {
        public int Id { get; set; }

        [Display(Name = "Acquired from")]
        public string Acquiredfrom { get; set; }

        [Display(Name = "Acquisition date")]
        public string Acquisitiondate { get; set; }

        [Display(Name = "Additional features")]
        public string Additionalfeatures { get; set; }

        [Display(Name = "Artist")]
        public string Artist { get; set; }

        [Display(Name = "Artist Gender")]
        public string Artistgender { get; set; }

        [Display(Name = "Artist's social group")]
        public string Artistsg { get; set; }

        [Display(Name = "Objects sharing associate features")]
        public string Associatefeatures { get; set; }

        [Display(Name = "Auctions")]
        public string Auctions { get; set; }

        [Display(Name = "Calabash")]
        public string Calabashinfo { get; set; }

        [Display(Name = "Certificates")]
        public string Certificate { get; set; }

        [Display(Name = "Chefferie")]
        public string Chefferie { get; set; }

        [Display(Name = "Clan")]
        public string Clan { get; set; }

        [Display(Name = "Field collected by")]
        public string Collectedby { get; set; }

        [Display(Name = "Collected date")]
        public string Collectedwhen { get; set; }

        [Display(Name = "collection")]
        public string Collection { get; set; }

        [Display(Name = "commanditaire")]
        public string Commanditaire { get; set; }

        [Display(Name = "Authoritative comments")]
        public string Comments { get; set; }

        [Display(Name = "Common gender")]
        public string Commgender { get; set; }

        [Display(Name = "Objects sharing common features")]
        public string Commonfeatures { get; set; }

        [Display(Name = "Commissioner's social group")]
        public string Commsg { get; set; }

        [Display(Name = "Condition")]
        public string Condition { get; set; }

        [Display(Name = "Confidential")]
        public string Confidential { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Creation date (est.)")]
        public string Createdate { get; set; }

        [Display(Name = "Created before")]
        public string Createdatemax { get; set; }

        [Display(Name = "Created after")]
        public string Createdatemin { get; set; }

        [Display(Name = "Credit Line")]
        public string Creditline { get; set; }

        [Display(Name = "Depth")]
        public string Depth { get; set; }

        [Display(Name = "Diameter")]
        public string Diameter { get; set; }

        [Display(Name = "Donation from")]
        public string Donationfrom { get; set; }

        [Display(Name = "Tribe")]
        public string Ethnicgroup { get; set; }

        [Display(Name = "Exhibitions")]
        public string Exhibition { get; set; }

        [Display(Name = "Objects sharing features")]
        public string Features { get; set; }

        [Display(Name = "Groups")]
        public string Groups { get; set; }

        [Display(Name = "Hair")]
        public string Hairinfo { get; set; }

        [Display(Name = "Height")]
        public string Height { get; set; }

        [Display(Name = "Identifier")]
        public string Identifier { get; set; }

        public string IdentifierNoCategory
        {
            get { return Identifier != null ? Identifier.Substring(Identifier.LastIndexOf('-') + 1) : ""; }
        }

        [Display(Name = "Inventory nr")]
        public string Inventory { get; set; }

        [Display(Name = "Cultural Complex")]
        public string Kingdom { get; set; }

        [Display(Name = "Language group")]
        public string Langgroup { get; set; }

        [Display(Name = "Length")]
        public string Length { get; set; }

        [Display(Name = "Beads")]
        public string Medbeinfo { get; set; }

        [Display(Name = "Bark")]
        public string Medbkinfo { get; set; }

        [Display(Name = "Bone")]
        public string Medboinfo { get; set; }

        [Display(Name = "T.Cotta")]
        public string Medceinfo { get; set; }

        [Display(Name = "Cloth")]
        public string Medclinfo { get; set; }

        [Display(Name = "Feather")]
        public string Medfeinfo { get; set; }

        [Display(Name = "Fiber")]
        public string Medfiinfo { get; set; }

        [Display(Name = "Glass")]
        public string Medglinfo { get; set; }

        [Display(Name = "Horn")]
        public string Medhoinfo { get; set; }

        [Display(Name = "Metal")]
        public string Medirinfo { get; set; }

        [Display(Name = "Medium")]
        public string Medium { get; set; }

        [Display(Name = "Ivory")]
        public string Medivinfo { get; set; }

        [Display(Name = "Magical subst.")]
        public string Medmainfo { get; set; }

        [Display(Name = "Other")]
        public string Medotinfo { get; set; }

        [Display(Name = "Raffia")]
        public string Medrainfo { get; set; }

        [Display(Name = "Resin")]
        public string Medreinfo { get; set; }

        [Display(Name = "Seed")]
        public string Medseedpodsinfo { get; set; }

        [Display(Name = "Shell")]
        public string Medshinfo { get; set; }

        [Display(Name = "Skin/hide/leather")]
        public string Medskinfo { get; set; }

        [Display(Name = "Stone")]
        public string Medstinfo { get; set; }

        [Display(Name = "Wood")]
        public string Medwoinfo { get; set; }

        [Display(Name = "Need better?")]
        public string Needbetter { get; set; }

        [Display(Name = "Object gender")]
        public string Objectgender { get; set; }

        [Display(Name = "Acephalous")]
        public string Objectjanus { get; set; }

        [Display(Name = "Name")]
        public string Objectname { get; set; }

        [Display(Name = "Excl. name")]
        public string Objectnameex { get; set; }

        [Display(Name = "Ethn. name")]
        public string Objectnamegn { get; set; }

        [Display(Name = "Object posture")]
        public string Objectposture { get; set; }

        [Display(Name = "copyright photo")]
        public string Photocopy { get; set; }

        [Display(Name = "photographer")]
        public string Photographer { get; set; }

        [Display(Name = "Photo inventory nr")]
        public string Photoinvnr { get; set; }

        [Display(Name = "courtesy of")]
        public string Photoprov { get; set; }

        [Display(Name = "Pigment")]
        public string Pigmentinfo { get; set; }

        [Display(Name = "Provenance")]
        public string Provenance { get; set; }

        [Display(Name = "Public")]
        public string Ispublic { get; set; }

        [Display(Name = "Publications")]
        public string Publication { get; set; }

        [Display(Name = "RAAI ID")]
        public string Raaiid { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Restoration(s)")]
        public string Restoration { get; set; }

        [Display(Name = "Ritual context")]
        public string Ritualassoc { get; set; }

        [Display(Name = "Site (archeo.)")]
        public string Sitearcheo { get; set; }

        [Display(Name = "Structural features")]
        public string Structuralfeatures { get; set; }

        [Display(Name = "TMS, alternative inv.")]
        public string Tms { get; set; }

        [Display(Name = "Usage notes")]
        public string Usage { get; set; }

        [Display(Name = "Village")]
        public string Village { get; set; }

        [Display(Name = "Web")]
        public string Web { get; set; }

        [Display(Name = "Weight")]
        public string Weight { get; set; }

        [Display(Name = "Width")]
        public string Width { get; set; }

        [Display(Name = "Workshop")]
        public string Workshop { get; set; }

        [Display(Name = "Objects in the same workshop")]
        public string Workshoplist { get; set; }

        public string Yaleid { get; set; }
        public string Unit { get; set; }
        public string Associatfeatures { get; set; }
        public string Multiline { get; set; }
        public string Langsubgroup { get; set; }
        public string Aquisitiondate { get; set; }
        public string Medwoodinfo { get; set; }
        public string Reacttmp { get; set; }

        [Required]
        public string Category { get; set; }

        public ICollection<ArtworkImage> ArtworkImage { get; set; }
    }
}
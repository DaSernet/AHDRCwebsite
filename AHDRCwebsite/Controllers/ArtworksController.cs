using AHDRCwebsite.Data;
using AHDRCwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AHDRCwebsite.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly ArtworkContext _context;
        private readonly HttpClient _httpClient;

        public ArtworksController(ArtworkContext context, HttpClient httpClient)
        {
            _context = context;

            _httpClient = httpClient;
        }

        //Get: Artworks new attempt
        public async Task<IActionResult> Index(string currentCategory, string currentFilter, string searchString, int? pageNumber, string[] selectedCategory, string sortOrder, string artworkQueryString)
        {
            // Initialize queryable variable
            IQueryable<Artwork> artworksQuery = _context.Artworks;


            //If query shorter then 2 then wwe should return nothing
            if (currentFilter != null || searchString != null && searchString.Length > 2)
            {
                artworksQuery = (from s in _context.Artworks
                            select s).AsNoTracking();
            }
            else if (currentFilter == null && searchString == null)
            {
                artworksQuery = (from s in _context.Artworks
                            select s).Take(0);
            }
            else if (currentFilter == null && searchString.Length <= 2)
            {
                artworksQuery = (from s in _context.Artworks
                            select s).Take(0);
            }


            //confidentials v2
            if (!User.IsInRole("Administrator"))
            {
                artworksQuery = artworksQuery.Except(artworksQuery.Where(b => b.AvailableForPublic == true));
            }

            //not logged in v2
            if (!User.Identity.IsAuthenticated)
            {
                artworksQuery = artworksQuery.Except(artworksQuery.Where(i => i.Category == "ph"));
                artworksQuery = artworksQuery.Except(artworksQuery.Where(i => i.Category == "co"));
                artworksQuery = artworksQuery.Except(artworksQuery.Where(i => i.Category == "ao"));
            }

            if (!User.IsInRole("Administrator") && !User.IsInRole("Subscriber"))
            {
                var list = new List<string>(selectedCategory);
                list.Remove("ph");
                list.Remove("co");
                list.Remove("au");
                selectedCategory = list.ToArray();
            }

            if (searchString != null)
            {
                pageNumber = 1;
                if (!User.Identity.IsAuthenticated)
                {
                    //non authenticated users can't search?
                    //artworks = artworks.Where(s => s.Category == null);
                }
            }
            else
            {
                searchString = currentFilter;
                if (currentCategory != null)
                {
                    selectedCategory = currentCategory.Split(',');
                }
            }

            ViewData["IdentifierSortParm"] = String.IsNullOrEmpty(sortOrder) ? "identifier_desc" : "";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSelectedCategory"] = String.Join(",", selectedCategory);
            ViewData["pageNumber"] = pageNumber;

            // Applying existing filters and categories
            if (!String.IsNullOrEmpty(searchString))
            {
                // Split the search string into individual terms
                string[] searchTerms = searchString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Filter artworks based on multiple search terms
                foreach (var term in searchTerms)
                {
                    artworksQuery = artworksQuery.Where(a =>
                        EF.Functions.Like(a.Artist, $"%{term}%") ||
                        EF.Functions.Like(a.Objectname, $"%{term}%") ||
                        EF.Functions.Like(a.Medium, $"%{term}%") ||
                        EF.Functions.Like(a.Acquiredfrom, $"%{term}%") ||
                        EF.Functions.Like(a.Acquisitiondate, $"%{term}%") ||
                        EF.Functions.Like(a.Additionalfeatures, $"%{term}%") ||
                        EF.Functions.Like(a.Artistgender, $"%{term}%") ||
                        EF.Functions.Like(a.Artistsg, $"%{term}%") ||
                        EF.Functions.Like(a.Associatefeatures, $"%{term}%") ||
                        EF.Functions.Like(a.Auctions, $"%{term}%") ||
                        EF.Functions.Like(a.Calabashinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Certificate, $"%{term}%") ||
                        EF.Functions.Like(a.Chefferie, $"%{term}%") ||
                        EF.Functions.Like(a.Clan, $"%{term}%") ||
                        EF.Functions.Like(a.Collectedby, $"%{term}%") ||
                        EF.Functions.Like(a.Collectedwhen, $"%{term}%") ||
                        EF.Functions.Like(a.Collection, $"%{term}%") ||
                        EF.Functions.Like(a.Commanditaire, $"%{term}%") ||
                        EF.Functions.Like(a.Comments, $"%{term}%") ||
                        EF.Functions.Like(a.Commgender, $"%{term}%") ||
                        EF.Functions.Like(a.Commonfeatures, $"%{term}%") ||
                        EF.Functions.Like(a.Commsg, $"%{term}%") ||
                        EF.Functions.Like(a.Condition, $"%{term}%") ||
                        EF.Functions.Like(a.Confidential, $"%{term}%") ||
                        EF.Functions.Like(a.Country, $"%{term}%") ||
                        EF.Functions.Like(a.Createdate, $"%{term}%") ||
                        EF.Functions.Like(a.Createdatemax, $"%{term}%") ||
                        EF.Functions.Like(a.Createdatemin, $"%{term}%") ||
                        EF.Functions.Like(a.Creditline, $"%{term}%") ||
                        EF.Functions.Like(a.Depth, $"%{term}%") ||
                        EF.Functions.Like(a.Diameter, $"%{term}%") ||
                        EF.Functions.Like(a.Donationfrom, $"%{term}%") ||
                        EF.Functions.Like(a.Ethnicgroup, $"%{term}%") ||
                        EF.Functions.Like(a.Exhibition, $"%{term}%") ||
                        EF.Functions.Like(a.Features, $"%{term}%") ||
                        EF.Functions.Like(a.Groups, $"%{term}%") ||
                        EF.Functions.Like(a.Hairinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Height, $"%{term}%") ||
                        EF.Functions.Like(a.Identifier, $"%{term}%") ||
                        EF.Functions.Like(a.Inventory, $"%{term}%") ||
                        EF.Functions.Like(a.Kingdom, $"%{term}%") ||
                        EF.Functions.Like(a.Langgroup, $"%{term}%") ||
                        EF.Functions.Like(a.Length, $"%{term}%") ||
                        EF.Functions.Like(a.Medbeinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medbkinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medboinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medceinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medclinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medfeinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medfiinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medglinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medhoinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medirinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medium, $"%{term}%") ||
                        EF.Functions.Like(a.Medivinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medmainfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medotinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medrainfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medreinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medseedpodsinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medshinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medskinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medstinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Medwoinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Needbetter, $"%{term}%") ||
                        EF.Functions.Like(a.Objectgender, $"%{term}%") ||
                        EF.Functions.Like(a.Objectjanus, $"%{term}%") ||
                        EF.Functions.Like(a.Objectname, $"%{term}%") ||
                        EF.Functions.Like(a.Objectnameex, $"%{term}%") ||
                        EF.Functions.Like(a.Objectnamegn, $"%{term}%") ||
                        EF.Functions.Like(a.Objectposture, $"%{term}%") ||
                        EF.Functions.Like(a.Photocopy, $"%{term}%") ||
                        EF.Functions.Like(a.Photographer, $"%{term}%") ||
                        EF.Functions.Like(a.Photoinvnr, $"%{term}%") ||
                        EF.Functions.Like(a.Photoprov, $"%{term}%") ||
                        EF.Functions.Like(a.Pigmentinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Provenance, $"%{term}%") ||
                        EF.Functions.Like(a.Ispublic, $"%{term}%") ||
                        EF.Functions.Like(a.Publication, $"%{term}%") ||
                        EF.Functions.Like(a.Raaiid, $"%{term}%") ||
                        EF.Functions.Like(a.Region, $"%{term}%") ||
                        EF.Functions.Like(a.Restoration, $"%{term}%") ||
                        EF.Functions.Like(a.Ritualassoc, $"%{term}%") ||
                        EF.Functions.Like(a.Sitearcheo, $"%{term}%") ||
                        EF.Functions.Like(a.Structuralfeatures, $"%{term}%") ||
                        EF.Functions.Like(a.Tms, $"%{term}%") ||
                        EF.Functions.Like(a.Usage, $"%{term}%") ||
                        EF.Functions.Like(a.Village, $"%{term}%") ||
                        EF.Functions.Like(a.Web, $"%{term}%") ||
                        EF.Functions.Like(a.Weight, $"%{term}%") ||
                        EF.Functions.Like(a.Width, $"%{term}%") ||
                        EF.Functions.Like(a.Workshop, $"%{term}%") ||
                        EF.Functions.Like(a.Workshoplist, $"%{term}%") ||
                        EF.Functions.Like(a.Yaleid, $"%{term}%") ||
                        EF.Functions.Like(a.Unit, $"%{term}%") ||
                        EF.Functions.Like(a.Associatfeatures, $"%{term}%") ||
                        EF.Functions.Like(a.Multiline, $"%{term}%") ||
                        EF.Functions.Like(a.Langsubgroup, $"%{term}%") ||
                        EF.Functions.Like(a.Aquisitiondate, $"%{term}%") ||
                        EF.Functions.Like(a.Medwoodinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Reacttmp, $"%{term}%") ||
                        EF.Functions.Like(a.Category, $"%{term}%") ||
                        EF.Functions.Like(a.Author, $"%{term}%") ||
                        EF.Functions.Like(a.Collections, $"%{term}%") ||
                        EF.Functions.Like(a.Date, $"%{term}%") ||
                        EF.Functions.Like(a.ISBN, $"%{term}%") ||
                        EF.Functions.Like(a.Publisher, $"%{term}%") ||
                        EF.Functions.Like(a.Title, $"%{term}%") ||
                        EF.Functions.Like(a.Year, $"%{term}%") ||
                        EF.Functions.Like(a.Reprints, $"%{term}%") ||
                        EF.Functions.Like(a.Datemax, $"%{term}%") ||
                        EF.Functions.Like(a.Datemin, $"%{term}%") ||
                        EF.Functions.Like(a.medriinfo, $"%{term}%") ||
                        EF.Functions.Like(a.Bio2, $"%{term}%") ||
                        EF.Functions.Like(a.Biography, $"%{term}%") ||
                        EF.Functions.Like(a.Book_about, $"%{term}%") ||
                        EF.Functions.Like(a.Born, $"%{term}%") ||
                        EF.Functions.Like(a.Dead, $"%{term}%") ||
                        EF.Functions.Like(a.First_Name, $"%{term}%") ||
                        EF.Functions.Like(a.Full_name, $"%{term}%") ||
                        EF.Functions.Like(a.Last_name, $"%{term}%") ||
                        EF.Functions.Like(a.Catalogue, $"%{term}%") ||
                        EF.Functions.Like(a.City, $"%{term}%") ||
                        EF.Functions.Like(a.Commercial, $"%{term}%") ||
                        EF.Functions.Like(a.Curator, $"%{term}%") ||
                        EF.Functions.Like(a.Location, $"%{term}%") ||
                        EF.Functions.Like(a.Categoryofobject, $"%{term}%"));
                    // Add more properties as needed for search
                }

            }

            // Apply any additional filters, such as categories
            if (selectedCategory != null && selectedCategory.Length > 0)
            {
                artworksQuery = artworksQuery.Where(a => selectedCategory.Contains(a.Category));
            }

            // Execute the query and get the total count for pagination


            // Apply sorting based on sortOrder
            switch (sortOrder)
            {
                // Add cases for different sorting options
                // For example:
                case "artist_desc":
                    artworksQuery = artworksQuery.OrderByDescending(a => a.Artist);
                    break;
                case "name_asc":
                    artworksQuery = artworksQuery.OrderBy(a => a.Objectname);
                    break;
                // Add more cases for other sorting options
                default:
                    artworksQuery = artworksQuery.OrderBy(a => a.ArtworkId);
                    break;
            }

            // Paginate the results
            int pageSize = 100; // Change the page size as needed


            if (!User.IsInRole("Administrator"))
            {
                artworksQuery = artworksQuery.Take(1000);
            }

            var totalArtworks = await artworksQuery.CountAsync();
            artworkQueryString = String.Join(",", artworksQuery.OrderBy(s => s.ArtworkId).Select(a => a.ArtworkId).AsEnumerable());
            ViewBag.artworkQueryString = artworkQueryString;


            artworksQuery = artworksQuery.Include(i => i.ArtworkImage);
            var paginatedArtworks = await PaginatedList<Artwork>.CreateAsync(artworksQuery, pageNumber ?? 1, pageSize, totalArtworks);

            return View(paginatedArtworks);
        }




        //GET: Artworks
                public async Task<IActionResult> Index2(string currentCategory, string currentFilter, string searchString, int? pageNumber, string[] selectedCategory, string sortOrder, string artworkQueryString)
        {
            var artworks = (from s in _context.Artworks
                            select s);


            //If query shorter then 2 then wwe should return nothing
            if (currentFilter != null || searchString != null && searchString.Length > 2)
            {
                artworks = (from s in _context.Artworks
                            select s).AsNoTracking();
            }
            else if (currentFilter == null && searchString == null)
            {
                artworks = (from s in _context.Artworks
                            select s).Take(0);
            }
            else if (currentFilter == null && searchString.Length <= 2)
            {
                artworks = (from s in _context.Artworks
                            select s).Take(0);
            }

            var publicArtworkList = from y in _context.Artworks
                                .Select(i => new { i.ArtworkId, i.AvailableForPublic, i.Category })
                                    select y;

            //working BUT ONLY RETURNS CATEGORY & NOTHING ELSE
            /*var FilteredArtworkList = from s in publicArtworkList
                                      join x in _context.Artworks on s.Id equals x.Id
                                      select x.Category;*/

            //confidentials v2
            if (!User.IsInRole("Administrator"))
            {
                publicArtworkList = publicArtworkList.Except(publicArtworkList.Where(b => b.AvailableForPublic == false));
            }

            //not logged in v2
            if (!User.Identity.IsAuthenticated)
            {
                publicArtworkList = publicArtworkList.Except(publicArtworkList.Where(i => i.Category == "ph"));
                publicArtworkList = publicArtworkList.Except(publicArtworkList.Where(i => i.Category == "co"));
                publicArtworkList = publicArtworkList.Except(publicArtworkList.Where(i => i.Category == "ao"));
            }

            if (!User.IsInRole("Administrator") && !User.IsInRole("Subscriber"))
            {
                var list = new List<string>(selectedCategory);
                list.Remove("ph");
                list.Remove("co");
                list.Remove("au");
                selectedCategory = list.ToArray();
            }

            if (searchString != null)
            {
                pageNumber = 1;
                if (!User.Identity.IsAuthenticated)
                {
                    //non authenticated users can't search?
                    //artworks = artworks.Where(s => s.Category == null);
                }
            }
            else
            {
                searchString = currentFilter;
                if (currentCategory != null)
                {
                    selectedCategory = currentCategory.Split(',');
                }
            }

            ViewData["IdentifierSortParm"] = String.IsNullOrEmpty(sortOrder) ? "identifier_desc" : "";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSelectedCategory"] = String.Join(",", selectedCategory);
            ViewData["pageNumber"] = pageNumber;

            /*if (selectedCategory.Length >= 1)
            {
                artworks = artworks.Where(s => selectedCategory.Contains(s.Category));
            }*/

            publicArtworkList = publicArtworkList.Where(s => selectedCategory.Contains(s.Category));

            artworks = artworks.Where(a => publicArtworkList.Any(a2 => a2.ArtworkId == a.ArtworkId));

            IQueryable<Artwork> filteredArtworks = artworks.Take(0);
            if (searchString != null)
            {
                var searchTerms = SplitSearchString(searchString);

                for (int x = 0; x <= searchTerms.Count - 1; x++)
                {
                    string test = searchTerms[x];
                    var test2 = artworks.Where(s => s.Acquiredfrom.Contains(test) ||
                s.Acquisitiondate.Contains(test) ||
                s.Additionalfeatures.Contains(test) ||
                s.Artist.Contains(test) ||
                s.Artistgender.Contains(test) ||
s.Artistsg.Contains(test) ||
s.Associatefeatures.Contains(test) ||
s.Auctions.Contains(test) ||
s.Calabashinfo.Contains(test) ||
s.Certificate.Contains(test) ||
s.Chefferie.Contains(test) ||
s.Clan.Contains(test) ||
s.Collectedby.Contains(test) ||
s.Collectedwhen.Contains(test) ||
s.Collection.Contains(test) ||
s.Commanditaire.Contains(test) ||
s.Comments.Contains(test) ||
s.Commgender.Contains(test) ||
s.Commonfeatures.Contains(test) ||
s.Commsg.Contains(test) ||
s.Condition.Contains(test) ||
s.Country.Contains(test) ||
s.Createdate.Contains(test) ||
s.Createdatemax.Contains(test) ||
s.Createdatemin.Contains(test) ||
s.Creditline.Contains(test) ||
s.Depth.Contains(test) ||
s.Diameter.Contains(test) ||
s.Donationfrom.Contains(test) ||
s.Ethnicgroup.Contains(test) ||
s.Exhibition.Contains(test) ||
s.Features.Contains(test) ||
s.Groups.Contains(test) ||
s.Hairinfo.Contains(test) ||
s.Height.Contains(test) ||
s.Identifier.Contains(test) ||
s.Inventory.Contains(test) ||
s.Kingdom.Contains(test) ||
s.Langgroup.Contains(test) ||
s.Length.Contains(test) ||
s.Medbeinfo.Contains(test) ||
s.Medbkinfo.Contains(test) ||
s.Medboinfo.Contains(test) ||
s.Medceinfo.Contains(test) ||
s.Medclinfo.Contains(test) ||
s.Medfeinfo.Contains(test) ||
s.Medfiinfo.Contains(test) ||
s.Medglinfo.Contains(test) ||
s.Medhoinfo.Contains(test) ||
s.Medirinfo.Contains(test) ||
s.Medium.Contains(test) ||
s.Medivinfo.Contains(test) ||
s.Medmainfo.Contains(test) ||
s.Medotinfo.Contains(test) ||
s.Medrainfo.Contains(test) ||
s.Medreinfo.Contains(test) ||
s.Medseedpodsinfo.Contains(test) ||
s.Medshinfo.Contains(test) ||
s.Medskinfo.Contains(test) ||
s.Medstinfo.Contains(test) ||
s.Medwoinfo.Contains(test) ||
s.Needbetter.Contains(test) ||
s.Objectgender.Contains(test) ||
s.Objectjanus.Contains(test) ||
s.Objectname.Contains(test) ||
s.Objectnameex.Contains(test) ||
s.Objectnamegn.Contains(test) ||
s.Objectposture.Contains(test) ||
s.Photocopy.Contains(test) ||
s.Photographer.Contains(test) ||
s.Photoinvnr.Contains(test) ||
s.Photoprov.Contains(test) ||
s.Pigmentinfo.Contains(test) ||
s.Provenance.Contains(test) ||
s.Publication.Contains(test) ||
s.Region.Contains(test) ||
s.Restoration.Contains(test) ||
s.Ritualassoc.Contains(test) ||
s.Sitearcheo.Contains(test) ||
s.Structuralfeatures.Contains(test) ||
s.Usage.Contains(test) ||
s.Village.Contains(test) ||
s.Weight.Contains(test) ||
s.Width.Contains(test) ||
s.Workshop.Contains(test) ||
s.Workshoplist.Contains(test) ||
s.Associatfeatures.Contains(test) ||
s.Langsubgroup.Contains(test) ||
s.Aquisitiondate.Contains(test) ||
s.Medwoodinfo.Contains(test));


                    //add artworks to filtered artworks
                    filteredArtworks = filteredArtworks.Union(test2);
                }
            }

            if (!User.IsInRole("Administrator"))
            {
                filteredArtworks = filteredArtworks.Take(1000);
            }

            if (filteredArtworks != null && 1 == 2)
            {
                switch (sortOrder)
                {
                    case "identifier_desc":
                        filteredArtworks = filteredArtworks.OrderByDescending(s => s.ArtworkId);
                        break;

                    case "Size":
                        filteredArtworks = filteredArtworks.OrderBy(s => s.Height);
                        break;

                    case "size_desc":
                        filteredArtworks = filteredArtworks.OrderByDescending(s => s.Height);
                        break;

                    default:
                        filteredArtworks = filteredArtworks.OrderBy(s => s.ArtworkId);
                        break;
                }
            }
            artworkQueryString = String.Join(",", filteredArtworks.OrderBy(s => s.ArtworkId).Select(a => a.ArtworkId).AsEnumerable());
            ViewBag.artworkQueryString = artworkQueryString;

            filteredArtworks = filteredArtworks.Include(i => i.ArtworkImage);
            int pageSize = 100;
            int totalArtworks = filteredArtworks.Count();
            return View(await PaginatedList<Artwork>.CreateAsync(filteredArtworks.AsNoTracking(), pageNumber ?? 1, pageSize, totalArtworks));
        }

        // GET: Artworks/Details/5
        public async Task<IActionResult> Details(int artworkId, string currentCategory, string currentFilter, int? pageNumber)
        {
            if (artworkId == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks.Include(i => i.ArtworkImage)
                .FirstOrDefaultAsync(m => m.ArtworkId == artworkId);
            if (artwork == null)
            {
                return NotFound();
            }

            ViewData["CurrentFilter"] = currentFilter;
            ViewData["CurrentSelectedCategory"] = String.Join(",", currentCategory);
            ViewData["pageNumber"] = pageNumber;

            // Retrieve the properties of the artwork object
            var properties = artwork.GetType().GetProperties();

            // Iterate through each property
            foreach (var p in properties)
            {
                // Check if the property type is a string
                if (p.PropertyType.Name.Equals("String"))
                {
                    // Exclude the "IdentifierNoCategory" property
                    if (p.Name != "IdentifierNoCategory")
                    {
                        // Check if the property value is not null
                        if (p.GetValue(artwork, null) != null)
                        {
                            // Retrieve the property value as a string
                            var value2 = p.GetValue(artwork, null).ToString();

                            // Replace special characters in the string value
                            var value = value2.Replace("&#39;", "'").Replace(@"\n", "<br/>");

                            // Set the updated value back to the property
                            p.SetValue(artwork, value, null);
                        }
                    }
                }
            }


            //Viewing history
            var userId = User.Identity.Name;
            var viewingHistory = new ViewingHistory
            {
                UserId = userId,
                Artwork = artwork,
                ViewedDateTime = DateTime.Now
            };

            //var artwork2 = _context.Artworks.Find(artwork.ArtworkId);
            //viewingHistory.Artwork = artwork2;
            _context.ViewingHistories.Add(viewingHistory);
            await _context.SaveChangesAsync();

            return View(artwork);
        }

        // GET: Artworks/Create
        public async Task<IActionResult> CreateAsync(int? artworkid)
        {
            if (artworkid == null || _context.Artworks == null)
            {
                return View();
            }
            //var artwork = await _context.Artworks.FindAsync(id);
            var artwork = await _context.Artworks.FirstOrDefaultAsync(m => m.ArtworkId == artworkid);
            if (artwork == null)
            {
                return NotFound();
            }

            var newArtwork = new Artwork
            {

            };

            switch (artwork.Category)
            {
                //au: auction sectie
                //bk: publicatie sectie
                //ph: field photo(terrein foto's) sectie
                //xp: exhibition sectie
                //co: collection sectie
                //pc: postcard sectie

                case "xp":

                    break;
                case "pc":

                    break;
                case "au":
                    newArtwork.Title = artwork.Title;
                    newArtwork.Date = artwork.Date;
                    newArtwork.Country = artwork.Country;
                    newArtwork.City = artwork.City;
                    newArtwork.Location = artwork.Location;
                    newArtwork.Catalogue = artwork.Catalogue;
                    newArtwork.Commercial = artwork.Commercial;
                    break;
                case "bk":
                    newArtwork.Title = artwork.Title;
                    newArtwork.Author = artwork.Author;
                    newArtwork.Publisher = artwork.Publisher;
                    newArtwork.Year = artwork.Year;
                    newArtwork.Country = artwork.Country;
                    newArtwork.Categoryofobject = artwork.Categoryofobject;
                    break;
                case "ph":
                    newArtwork.Clan = artwork.Clan;
                    newArtwork.Objectname = artwork.Objectname;
                    newArtwork.Country = artwork.Country;
                    newArtwork.Collection = artwork.Collection;
                    newArtwork.Publication = artwork.Publication;
                    newArtwork.Photographer = artwork.Photographer;
                    newArtwork.Photoprov = artwork.Photoprov;
                    newArtwork.Photocopy = artwork.Photocopy;
                    newArtwork.Photoinvnr = artwork.Photoinvnr;
                    break;
                case "co":
                    newArtwork.Clan = artwork.Clan;
                    newArtwork.Objectname = artwork.Objectname;
                    newArtwork.Country = artwork.Country;
                    newArtwork.Collection = artwork.Collection;
                    newArtwork.Publication = artwork.Publication;
                    newArtwork.Photographer = artwork.Photographer;
                    newArtwork.Photoprov = artwork.Photoprov;
                    newArtwork.Photocopy = artwork.Photocopy;
                    newArtwork.Photoinvnr = artwork.Photoinvnr;
                    break;

                case "ao":
                    newArtwork.Clan = artwork.Clan;
                    newArtwork.Country = artwork.Country;
                    newArtwork.Objectname = artwork.Objectname;
                    newArtwork.Medium = artwork.Medium;
                    newArtwork.Collection = artwork.Collection;
                    newArtwork.Photographer = artwork.Photographer;
                    newArtwork.Photoprov = artwork.Photoprov;
                    newArtwork.Photocopy = artwork.Photocopy;
                    newArtwork.Photoinvnr = artwork.Photoinvnr;
                    newArtwork.Provenance = artwork.Provenance;
                    newArtwork.Publication = artwork.Publication;
                    newArtwork.Exhibition = artwork.Exhibition;
                    newArtwork.Auctions = artwork.Auctions;
                    break;
                case "wh":
                    newArtwork.First_Name = artwork.First_Name;
                    newArtwork.Last_name = artwork.Last_name;
                    newArtwork.Born = artwork.Born;
                    newArtwork.Dead = artwork.Dead;
                    newArtwork.Categoryofobject = artwork.Categoryofobject;
                    newArtwork.Biography = artwork.Biography;
                    break;

                default:

                    break;
            }
            newArtwork.Category = artwork.Category;

            //Filter
            return View(newArtwork);
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("ArtworkId,Acquiredfrom,Category,Acquisitiondate,Additionalfeatures,Artist,Artistgender,Artistsg,Associatefeatures,Auctions,Calabashinfo,Certificate,Chefferie,Clan,Collectedby,Collectedwhen,Collection,Commanditaire,Comments,Commgender,Commonfeatures,Commsg,Condition,Confidential,Country,Createdate,Createdatemax,Createdatemin,Creditline,Depth,Diameter,Donationfrom,Ethnicgroup,Exhibition,Features,Groups,Hairinfo,Height,Inventory,Kingdom,Langgroup,Length,Medbeinfo,Medbkinfo,Medboinfo,Medceinfo,Medclinfo,Medfeinfo,Medfiinfo,Medglinfo,Medhoinfo,Medirinfo,Medium,Medivinfo,Medmainfo,Medotinfo,Medrainfo,Medreinfo,Medseedpodsinfo,Medshinfo,Medskinfo,Medstinfo,Medwoinfo,Needbetter,Objectgender,Objectjanus,Objectname,Objectnameex,Objectnamegn,Objectposture,Photocopy,Photographer,Photoinvnr,Photoprov,Pigmentinfo,Provenance,Ispublic,Publication,Raaiid,Region,Restoration,Ritualassoc,Sitearcheo,Structuralfeatures,Tms,Usage,Village,Web,Weight,Width,Workshop,Workshoplist,Yaleid,Unit,Associatfeatures,Multiline,Langsubgroup,Aquisitiondate,Medwoodinfo,Reacttmp")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                string category = artwork.Category;
                var artworks = from s in _context.Artworks
                               select s;

                artworks = artworks.Where(s => s.Category.Contains(category));
                string identifier = "";
                identifier = artworks.OrderBy(s => s.ArtworkId).LastOrDefault().Identifier;
                var identifierNumber = "";

                // get everything after -
                if (identifier != null)
                {
                    identifierNumber = identifier.Substring(identifier.IndexOf('-') + 1);
                }

                var newIdentifierNumber = int.Parse(identifierNumber) + 1;
                var newIdentifier = category + "-" + newIdentifierNumber.ToString("D7");

                artwork.Identifier = newIdentifier;

                _context.Add(artwork);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { artworkid = artwork.ArtworkId });
            }
            return View(artwork);
        }

        // GET: Artworks/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? artworkid)
        {
            if (artworkid == null || _context.Artworks == null)
            {
                return NotFound();
            }

            //var artwork = await _context.Artworks.FindAsync(id);
            var artwork = await _context.Artworks.Include(i => i.ArtworkImage).FirstOrDefaultAsync(m => m.ArtworkId == artworkid);
            if (artwork == null)
            {
                return NotFound();
            }
            return View(artwork);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int artworkid, [Bind("ArtworkImage,AvailableForPublic,ArtworkId,Category,Identifier,Acquiredfrom,Acquisitiondate,Additionalfeatures,Artist,Artistgender,Artistsg,Associatefeatures,Auctions,Calabashinfo,Certificate,Chefferie,Clan,Collectedby,Collectedwhen,Collection,Commanditaire,Comments,Commgender,Commonfeatures,Commsg,Condition,Confidential,Country,Createdate,Createdatemax,Createdatemin,Creditline,Depth,Diameter,Donationfrom,Ethnicgroup,Exhibition,Features,Groups,Hairinfo,Height,Inventory,Kingdom,Langgroup,Length,Medbeinfo,Medbkinfo,Medboinfo,Medceinfo,Medclinfo,Medfeinfo,Medfiinfo,Medglinfo,Medhoinfo,Medirinfo,Medium,Medivinfo,Medmainfo,Medotinfo,Medrainfo,Medreinfo,Medseedpodsinfo,Medshinfo,Medskinfo,Medstinfo,Medwoinfo,Needbetter,Objectgender,Objectjanus,Objectname,Objectnameex,Objectnamegn,Objectposture,Photocopy,Photographer,Photoinvnr,Photoprov,Pigmentinfo,Provenance,Ispublic,Publication,Raaiid,Region,Restoration,Ritualassoc,Sitearcheo,Structuralfeatures,Tms,Usage,Village,Web,Weight,Width,Workshop,Workshoplist,Yaleid,Unit,Associatfeatures,Multiline,Langsubgroup,Aquisitiondate,Medwoodinfo,Reacttmp,Author,Collections,Date,ISBN,Publisher,Title,Year,Reprints,Datemax,Datemin,medriinfo,Bio2,Biography,Book_about,Born,Dead,First_Name,Full_name,Last_name,Catalogue,City,Commercial,Curator,Location,Categoryofobject")] Artwork artwork)
        {
            if (artworkid != artwork.ArtworkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artwork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtworkExists(artwork.ArtworkId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { artworkid = artworkid });
            }
            return View(artwork);
        }

        // GET: Artworks/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? ArtworkId)
        {
            if (ArtworkId == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks
                .FirstOrDefaultAsync(m => m.ArtworkId == ArtworkId);
            if (artwork == null)
            {
                return NotFound();
            }

            return View(artwork);
        }

        // POST: Artworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int artworkid)
        {
            if (_context.Artworks == null)
            {
                return Problem("Entity set 'ArtworkContext.Artworks'  is null.");
            }
            var artwork = await _context.Artworks.FindAsync(artworkid);
            if (artwork != null)
            {
                _context.Artworks.Remove(artwork);
                //_context.ArtworkImages.Remove((ArtworkImage)artwork.ArtworkImage.Where(m => m.Artwork == artwork));
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworkExists(int artworkid)
        {
            return _context.Artworks.Any(e => e.ArtworkId == artworkid);
        }

        static List<string> SplitSearchString(string searchString)
        {
            // Split the searchString by spaces, hyphens, and double quotes
            string[] splitBySpace = Regex.Split(searchString, @"[\s-]+");

            // Remove empty entries and double quotes
            var searchTerms = splitBySpace.Where(term => !string.IsNullOrWhiteSpace(term) && !term.Contains("\""));

            // Remove double quotes from terms enclosed in double quotes
            var quotedTerms = Regex.Matches(searchString, "\"([^\"]*)\"");
            foreach (var quotedTerm in quotedTerms)
            {
                string term = quotedTerm.ToString().Trim('"');
                searchTerms = searchTerms.Append(term);
            }

            return searchTerms.ToList();
        }
    }
}
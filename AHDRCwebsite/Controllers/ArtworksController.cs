using AHDRCwebsite.Data;
using AHDRCwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHDRCwebsite.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly ArtworkContext _context;

        public ArtworksController(ArtworkContext context)
        {
            _context = context;
        }

        // GET: Artworks
        public async Task<IActionResult> Index(string currentCategory, string currentFilter, string searchString, int? pageNumber, string[] selectedCategory, string sortOrder, string artworkQueryString)
        {


            var artworks = (from s in _context.Artworks
                            select s);

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
                                .Select(i => new { i.ArtworkId, i.Ispublic, i.Category })
                                    select y;

            //working BUT ONLY RETURNS CATEGORY & NOTHING ELSE
            /*var FilteredArtworkList = from s in publicArtworkList
                                      join x in _context.Artworks on s.Id equals x.Id
                                      select x.Category;*/

            //confidentials v2
            if (!User.IsInRole("Administrator"))
            {
                publicArtworkList = publicArtworkList.Except(publicArtworkList.Where(b => b.Ispublic == "false"));
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

            if (!String.IsNullOrEmpty(searchString))
            {
                artworks = artworks.Where(s => s.Acquiredfrom.Contains(searchString) ||
                s.Acquisitiondate.Contains(searchString) ||
                s.Additionalfeatures.Contains(searchString) ||
                s.Artist.Contains(searchString) ||
                s.Artistgender.Contains(searchString) ||
s.Artistsg.Contains(searchString) ||
s.Associatefeatures.Contains(searchString) ||
s.Auctions.Contains(searchString) ||
s.Calabashinfo.Contains(searchString) ||
s.Certificate.Contains(searchString) ||
s.Chefferie.Contains(searchString) ||
s.Clan.Contains(searchString) ||
s.Collectedby.Contains(searchString) ||
s.Collectedwhen.Contains(searchString) ||
s.Collection.Contains(searchString) ||
s.Commanditaire.Contains(searchString) ||
s.Comments.Contains(searchString) ||
s.Commgender.Contains(searchString) ||
s.Commonfeatures.Contains(searchString) ||
s.Commsg.Contains(searchString) ||
s.Condition.Contains(searchString) ||
s.Country.Contains(searchString) ||
s.Createdate.Contains(searchString) ||
s.Createdatemax.Contains(searchString) ||
s.Createdatemin.Contains(searchString) ||
s.Creditline.Contains(searchString) ||
s.Depth.Contains(searchString) ||
s.Diameter.Contains(searchString) ||
s.Donationfrom.Contains(searchString) ||
s.Ethnicgroup.Contains(searchString) ||
s.Exhibition.Contains(searchString) ||
s.Features.Contains(searchString) ||
s.Groups.Contains(searchString) ||
s.Hairinfo.Contains(searchString) ||
s.Height.Contains(searchString) ||
s.Identifier.Contains(searchString) ||
s.Inventory.Contains(searchString) ||
s.Kingdom.Contains(searchString) ||
s.Langgroup.Contains(searchString) ||
s.Length.Contains(searchString) ||
s.Medbeinfo.Contains(searchString) ||
s.Medbkinfo.Contains(searchString) ||
s.Medboinfo.Contains(searchString) ||
s.Medceinfo.Contains(searchString) ||
s.Medclinfo.Contains(searchString) ||
s.Medfeinfo.Contains(searchString) ||
s.Medfiinfo.Contains(searchString) ||
s.Medglinfo.Contains(searchString) ||
s.Medhoinfo.Contains(searchString) ||
s.Medirinfo.Contains(searchString) ||
s.Medium.Contains(searchString) ||
s.Medivinfo.Contains(searchString) ||
s.Medmainfo.Contains(searchString) ||
s.Medotinfo.Contains(searchString) ||
s.Medrainfo.Contains(searchString) ||
s.Medreinfo.Contains(searchString) ||
s.Medseedpodsinfo.Contains(searchString) ||
s.Medshinfo.Contains(searchString) ||
s.Medskinfo.Contains(searchString) ||
s.Medstinfo.Contains(searchString) ||
s.Medwoinfo.Contains(searchString) ||
s.Needbetter.Contains(searchString) ||
s.Objectgender.Contains(searchString) ||
s.Objectjanus.Contains(searchString) ||
s.Objectname.Contains(searchString) ||
s.Objectnameex.Contains(searchString) ||
s.Objectnamegn.Contains(searchString) ||
s.Objectposture.Contains(searchString) ||
s.Photocopy.Contains(searchString) ||
s.Photographer.Contains(searchString) ||
s.Photoinvnr.Contains(searchString) ||
s.Photoprov.Contains(searchString) ||
s.Pigmentinfo.Contains(searchString) ||
s.Provenance.Contains(searchString) ||
s.Publication.Contains(searchString) ||
s.Region.Contains(searchString) ||
s.Restoration.Contains(searchString) ||
s.Ritualassoc.Contains(searchString) ||
s.Sitearcheo.Contains(searchString) ||
s.Structuralfeatures.Contains(searchString) ||
s.Usage.Contains(searchString) ||
s.Village.Contains(searchString) ||
s.Weight.Contains(searchString) ||
s.Width.Contains(searchString) ||
s.Workshop.Contains(searchString) ||
s.Workshoplist.Contains(searchString) ||
s.Associatfeatures.Contains(searchString) ||
s.Langsubgroup.Contains(searchString) ||
s.Aquisitiondate.Contains(searchString) ||
s.Medwoodinfo.Contains(searchString));
            }

            artworks = artworks.Take(500);

            if (artworks != null)
            {
                switch (sortOrder)
                {
                    case "identifier_desc":
                        artworks = artworks.OrderByDescending(s => s.ArtworkId);
                        break;

                    case "Size":
                        artworks = artworks.OrderBy(s => s.Height);
                        break;

                    case "size_desc":
                        artworks = artworks.OrderByDescending(s => s.Height);
                        break;

                    default:
                        artworks = artworks.OrderBy(s => s.ArtworkId);
                        break;
                }
            }
            artworkQueryString = String.Join(",", artworks.OrderBy(s => s.ArtworkId).Select(a => a.ArtworkId).AsEnumerable());
            ViewData["artworkQueryString"] = artworkQueryString;

            artworks = artworks.Include(i => i.ArtworkImage);
            int pageSize = 50;
            int totalArtworks = artworks.Count();
            return View(await PaginatedList<Artwork>.CreateAsync(artworks.AsNoTracking(), pageNumber ?? 1, pageSize, totalArtworks));
        }

        // GET: Artworks/Details/5
        public async Task<IActionResult> Details(int? ArtworkId, string currentCategory, string currentFilter, int? pageNumber, string artworkQueryString)
        {
            if (ArtworkId == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks.Include(i => i.ArtworkImage)
                .FirstOrDefaultAsync(m => m.ArtworkId == ArtworkId);
            if (artwork == null)
            {
                return NotFound();
            }

            ViewData["CurrentFilter"] = currentFilter;
            ViewData["CurrentSelectedCategory"] = String.Join(",", currentCategory);
            ViewData["pageNumber"] = pageNumber;
            ViewData["artworkQueryString"] = artworkQueryString;
            if (artworkQueryString != null)
            {
                string[] artworkQueryArray = artworkQueryString.Split(',');

                var index = Array.FindIndex(artworkQueryArray, row => row == ArtworkId.ToString());

                if (index < artworkQueryArray.Length - 1)
                {
                    ViewData["artworkQueryStringNext"] = artworkQueryArray[index + 1];
                }
                else
                {
                    ViewData["artworkQueryStringNext"] = null;
                }

                if (index > 0)
                {
                    ViewData["artworkQueryStringPrev"] = artworkQueryArray[index - 1];
                }
                else
                {
                    ViewData["artworkQueryStringPrev"] = null;
                }
            }

            return View(artwork);
        }

        // GET: Artworks/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
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
                string identifier = "0";
                identifier = artworks.OrderBy(s => s.ArtworkId).LastOrDefault().Identifier;
                var identifierNumber = "0";

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
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int artworkid, [Bind("ArtworkImage,ArtworkId,Category,Identifier,Acquiredfrom,Acquisitiondate,Additionalfeatures,Artist,Artistgender,Artistsg,Associatefeatures,Auctions,Calabashinfo,Certificate,Chefferie,Clan,Collectedby,Collectedwhen,Collection,Commanditaire,Comments,Commgender,Commonfeatures,Commsg,Condition,Confidential,Country,Createdate,Createdatemax,Createdatemin,Creditline,Depth,Diameter,Donationfrom,Ethnicgroup,Exhibition,Features,Groups,Hairinfo,Height,Inventory,Kingdom,Langgroup,Length,Medbeinfo,Medbkinfo,Medboinfo,Medceinfo,Medclinfo,Medfeinfo,Medfiinfo,Medglinfo,Medhoinfo,Medirinfo,Medium,Medivinfo,Medmainfo,Medotinfo,Medrainfo,Medreinfo,Medseedpodsinfo,Medshinfo,Medskinfo,Medstinfo,Medwoinfo,Needbetter,Objectgender,Objectjanus,Objectname,Objectnameex,Objectnamegn,Objectposture,Photocopy,Photographer,Photoinvnr,Photoprov,Pigmentinfo,Provenance,Ispublic,Publication,Raaiid,Region,Restoration,Ritualassoc,Sitearcheo,Structuralfeatures,Tms,Usage,Village,Web,Weight,Width,Workshop,Workshoplist,Yaleid,Unit,Associatfeatures,Multiline,Langsubgroup,Aquisitiondate,Medwoodinfo,Reacttmp")] Artwork artwork)
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
                return RedirectToAction(nameof(Index));
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
    }
}
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
        public async Task<IActionResult> Index(string currentSelectedCategory, string currentFilter, string searchString, int? pageNumber, string[] selectedCategory, string sortOrder)
        {
            //var artworks = from s in _context.Artworks
            //select s;

            var artworks = (from s in _context.Artworks.Include(i => i.ArtworkImage)
                            select s).Take(100);
            if (currentFilter != null || searchString != null)
            {
                artworks = from s in _context.Artworks.Include(i => i.ArtworkImage)
                           select s;
            }

            //working
            /*var categoryArtworkList = from s in publicArtworkList
                                      join x in _context.Artworks on s.Id equals x.Id
                                      select x.Category;*/

            //working
            /*var publicArtworkList = from y in _context.Artworks
                                    .Select (i => new { i.Id, i.Ispublic })
                                    select y;*/

            //confidentials
            if (!User.IsInRole("Administrator"))
            {
                artworks = artworks.Except(artworks.Where(i => i.Ispublic == "false"));
            }

            //not logged in
            if (!User.Identity.IsAuthenticated)
            {
                artworks = artworks.Except(artworks.Where(i => i.Category == "ph"));
                artworks = artworks.Except(artworks.Where(i => i.Category == "co"));
                artworks = artworks.Except(artworks.Where(i => i.Category == "ao"));
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
                if (currentSelectedCategory != null)
                {
                    selectedCategory = currentSelectedCategory.Split(',');
                }
            }

            ViewData["IdentifierSortParm"] = String.IsNullOrEmpty(sortOrder) ? "identifier_desc" : "";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSelectedCategory"] = String.Join(",", selectedCategory);

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

                if (!User.IsInRole("Administrator") && !User.IsInRole("Subscriber"))
                {
                    var list = new List<string>(selectedCategory);
                    list.Remove("ph");
                    list.Remove("co");
                    list.Remove("au");
                    selectedCategory = list.ToArray();
                }
            }

            if (selectedCategory.Length >= 1)
            {
                artworks = artworks.Where(s => selectedCategory.Contains(s.Category));
            }

            if (artworks != null)
            {
                switch (sortOrder)
                {
                    case "identifier_desc":
                        artworks = artworks.OrderByDescending(s => s.Identifier);
                        break;

                    case "Size":
                        artworks = artworks.OrderBy(s => s.Height);
                        break;

                    case "size_desc":
                        artworks = artworks.OrderByDescending(s => s.Height);
                        break;

                    default:
                        artworks = artworks.OrderBy(s => s.Identifier);
                        break;
                }
            }

            int pageSize = 50;
            return View(await PaginatedList<Artwork>.CreateAsync(artworks.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Artworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks.Include(i => i.ArtworkImage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artwork == null)
            {
                return NotFound();
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
        public async Task<IActionResult> Create([Bind("Id,Acquiredfrom,Category,Acquisitiondate,Additionalfeatures,Artist,Artistgender,Artistsg,Associatefeatures,Auctions,Calabashinfo,Certificate,Chefferie,Clan,Collectedby,Collectedwhen,Collection,Commanditaire,Comments,Commgender,Commonfeatures,Commsg,Condition,Confidential,Country,Createdate,Createdatemax,Createdatemin,Creditline,Depth,Diameter,Donationfrom,Ethnicgroup,Exhibition,Features,Groups,Hairinfo,Height,Inventory,Kingdom,Langgroup,Length,Medbeinfo,Medbkinfo,Medboinfo,Medceinfo,Medclinfo,Medfeinfo,Medfiinfo,Medglinfo,Medhoinfo,Medirinfo,Medium,Medivinfo,Medmainfo,Medotinfo,Medrainfo,Medreinfo,Medseedpodsinfo,Medshinfo,Medskinfo,Medstinfo,Medwoinfo,Needbetter,Objectgender,Objectjanus,Objectname,Objectnameex,Objectnamegn,Objectposture,Photocopy,Photographer,Photoinvnr,Photoprov,Pigmentinfo,Provenance,Ispublic,Publication,Raaiid,Region,Restoration,Ritualassoc,Sitearcheo,Structuralfeatures,Tms,Usage,Village,Web,Weight,Width,Workshop,Workshoplist,Yaleid,Unit,Associatfeatures,Multiline,Langsubgroup,Aquisitiondate,Medwoodinfo,Reacttmp")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                string category = artwork.Category;
                var artworks = from s in _context.Artworks
                               select s;

                artworks = artworks.Where(s => s.Category.Contains(category));
                string identifier = "0";
                identifier = artworks.OrderBy(s => s.Id).LastOrDefault().Identifier;
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Artworks == null)
            {
                return NotFound();
            }

            //var artwork = await _context.Artworks.FindAsync(id);
            var artwork = await _context.Artworks.Include(i => i.ArtworkImage).FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Identifier,Acquiredfrom,Acquisitiondate,Additionalfeatures,Artist,Artistgender,Artistsg,Associatefeatures,Auctions,Calabashinfo,Certificate,Chefferie,Clan,Collectedby,Collectedwhen,Collection,Commanditaire,Comments,Commgender,Commonfeatures,Commsg,Condition,Confidential,Country,Createdate,Createdatemax,Createdatemin,Creditline,Depth,Diameter,Donationfrom,Ethnicgroup,Exhibition,Features,Groups,Hairinfo,Height,Inventory,Kingdom,Langgroup,Length,Medbeinfo,Medbkinfo,Medboinfo,Medceinfo,Medclinfo,Medfeinfo,Medfiinfo,Medglinfo,Medhoinfo,Medirinfo,Medium,Medivinfo,Medmainfo,Medotinfo,Medrainfo,Medreinfo,Medseedpodsinfo,Medshinfo,Medskinfo,Medstinfo,Medwoinfo,Needbetter,Objectgender,Objectjanus,Objectname,Objectnameex,Objectnamegn,Objectposture,Photocopy,Photographer,Photoinvnr,Photoprov,Pigmentinfo,Provenance,Ispublic,Publication,Raaiid,Region,Restoration,Ritualassoc,Sitearcheo,Structuralfeatures,Tms,Usage,Village,Web,Weight,Width,Workshop,Workshoplist,Yaleid,Unit,Associatfeatures,Multiline,Langsubgroup,Aquisitiondate,Medwoodinfo,Reacttmp")] Artwork artwork)
        {
            if (id != artwork.Id)
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
                    if (!ArtworkExists(artwork.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Artworks == null)
            {
                return Problem("Entity set 'ArtworkContext.Artworks'  is null.");
            }
            var artwork = await _context.Artworks.FindAsync(id);
            if (artwork != null)
            {
                _context.Artworks.Remove(artwork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworkExists(int id)
        {
            return _context.Artworks.Any(e => e.Id == id);
        }
    }
}
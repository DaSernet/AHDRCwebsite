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
            var artworks = from s in _context.Artworks.Include(i => i.ArtworkImage)
                           select s;

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
                    artworks = artworks.Where(s => s.Category == null);
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
                artworks = artworks.Where(s => s.Identifier.Contains(searchString)
                                       || s.Country.Contains(searchString));

                if (!User.IsInRole("Administrator") || !User.IsInRole("Subscriber"))
                {
                    var list = new List<string>(selectedCategory);
                    list.Remove("bk");
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
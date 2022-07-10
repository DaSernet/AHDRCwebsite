using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AHDRCwebsite.Data;
using AHDRCwebsite.Models;

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
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var artworks = from s in _context.Artworks
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                artworks = artworks.Where(s => s.Identifier.Contains(searchString)
                                       || s.Country.Contains(searchString));
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

            var artwork = await _context.Artworks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artwork == null)
            {
                return NotFound();
            }

            return View(artwork);
        }

        // GET: Artworks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Acquiredfrom,Acquisitiondate,Additionalfeatures,Artist,Artistgender,Artistsg,Associatefeatures,Auctions,Calabashinfo,Certificate,Chefferie,Clan,Collectedby,Collectedwhen,Collection,Commanditaire,Comments,Commgender,Commonfeatures,Commsg,Condition,Confidential,Country,Createdate,Createdatemax,Createdatemin,Creditline,Depth,Diameter,Donationfrom,Ethnicgroup,Exhibition,Features,Groups,Hairinfo,Height,Identifier,Inventory,Kingdom,Langgroup,Length,Medbeinfo,Medbkinfo,Medboinfo,Medceinfo,Medclinfo,Medfeinfo,Medfiinfo,Medglinfo,Medhoinfo,Medirinfo,Medium,Medivinfo,Medmainfo,Medotinfo,Medrainfo,Medreinfo,Medseedpodsinfo,Medshinfo,Medskinfo,Medstinfo,Medwoinfo,Needbetter,Objectgender,Objectjanus,Objectname,Objectnameex,Objectnamegn,Objectposture,Photocopy,Photographer,Photoinvnr,Photoprov,Pigmentinfo,Provenance,Ispublic,Publication,Raaiid,Region,Restoration,Ritualassoc,Sitearcheo,Structuralfeatures,Tms,Usage,Village,Web,Weight,Width,Workshop,Workshoplist,Yaleid,Unit,Associatfeatures,Multiline,Langsubgroup,Aquisitiondate,Medwoodinfo,Reacttmp")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artwork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artwork);
        }

        // GET: Artworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Acquiredfrom,Acquisitiondate,Additionalfeatures,Artist,Artistgender,Artistsg,Associatefeatures,Auctions,Calabashinfo,Certificate,Chefferie,Clan,Collectedby,Collectedwhen,Collection,Commanditaire,Comments,Commgender,Commonfeatures,Commsg,Condition,Confidential,Country,Createdate,Createdatemax,Createdatemin,Creditline,Depth,Diameter,Donationfrom,Ethnicgroup,Exhibition,Features,Groups,Hairinfo,Height,Identifier,Inventory,Kingdom,Langgroup,Length,Medbeinfo,Medbkinfo,Medboinfo,Medceinfo,Medclinfo,Medfeinfo,Medfiinfo,Medglinfo,Medhoinfo,Medirinfo,Medium,Medivinfo,Medmainfo,Medotinfo,Medrainfo,Medreinfo,Medseedpodsinfo,Medshinfo,Medskinfo,Medstinfo,Medwoinfo,Needbetter,Objectgender,Objectjanus,Objectname,Objectnameex,Objectnamegn,Objectposture,Photocopy,Photographer,Photoinvnr,Photoprov,Pigmentinfo,Provenance,Ispublic,Publication,Raaiid,Region,Restoration,Ritualassoc,Sitearcheo,Structuralfeatures,Tms,Usage,Village,Web,Weight,Width,Workshop,Workshoplist,Yaleid,Unit,Associatfeatures,Multiline,Langsubgroup,Aquisitiondate,Medwoodinfo,Reacttmp")] Artwork artwork)
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

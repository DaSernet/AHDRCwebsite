using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AHDRCwebsite.Data;
using AHDRCwebsite.Models;
using Microsoft.AspNetCore.Hosting;
using AHDRCwebsite.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace AHDRCwebsite.Controllers
{
    public class ArtworkImagesController : Controller
    {
        private readonly ArtworkContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArtworkImagesController(ArtworkContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: ArtworkImages
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArtworkImages.ToListAsync());
        }

        // GET: ArtworkImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ArtworkImages == null)
            {
                return NotFound();
            }

            var artworkImage = await _context.ArtworkImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artworkImage == null)
            {
                return NotFound();
            }

            return View(artworkImage);
        }

        // GET: ArtworkImages/Create
        public IActionResult Create()
        {
            ArtImages vm = new ArtImages();
            ViewBag.images = new SelectList(_context.Artworks.ToList(), "Id", "Identifier");
            return View(vm);
        }

        // POST: ArtworkImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtImages vm)
        {
            foreach (var item in vm.Images)
            {
                string stringFileName = UploadFile(item);
                var artworkImage = new ArtworkImage
                {
                    ImageURL = stringFileName,
                    Artwork = vm.Artwork
                };
                _context.ArtworkImages.Add(artworkImage);

                
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private string UploadFile(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        // GET: ArtworkImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ArtworkImages == null)
            {
                return NotFound();
            }

            var artworkImage = await _context.ArtworkImages.FindAsync(id);
            if (artworkImage == null)
            {
                return NotFound();
            }
            return View(artworkImage);
        }

        // POST: ArtworkImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageURL")] ArtworkImage artworkImage)
        {
            if (id != artworkImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artworkImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtworkImageExists(artworkImage.Id))
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
            return View(artworkImage);
        }

        // GET: ArtworkImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ArtworkImages == null)
            {
                return NotFound();
            }

            var artworkImage = await _context.ArtworkImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artworkImage == null)
            {
                return NotFound();
            }

            return View(artworkImage);
        }

        // POST: ArtworkImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ArtworkImages == null)
            {
                return Problem("Entity set 'ArtworkContext.ArtworkImages'  is null.");
            }
            var artworkImage = await _context.ArtworkImages.FindAsync(id);
            if (artworkImage != null)
            {
                _context.ArtworkImages.Remove(artworkImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworkImageExists(int id)
        {
          return _context.ArtworkImages.Any(e => e.Id == id);
        }
    }
}

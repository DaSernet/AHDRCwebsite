using AHDRCwebsite.Data;
using AHDRCwebsite.Models;
using AHDRCwebsite.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArtworkImages.ToListAsync());
        }

        // GET: ArtworkImages/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? ArtworkImageId)
        {
            if (ArtworkImageId == null || _context.ArtworkImages == null)
            {
                return NotFound();
            }

            var artworkImage = await _context.ArtworkImages
                .FirstOrDefaultAsync(m => m.ArtworkImageId == ArtworkImageId);
            if (artworkImage == null)
            {
                return NotFound();
            }

            return View(artworkImage);
        }

        // GET: ArtworkImages/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(int ArtworkId)
        {
            ArtImages vm = new ArtImages();
            ViewBag.images = new SelectList(_context.Artworks.Where(m => m.ArtworkId == ArtworkId).ToList(), "ArtworkId", "Identifier");
            return View(vm);
        }

        // POST: ArtworkImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(ArtImages vm)
        {
            if (vm.Images != null && vm.Artwork != null)
            {
                foreach (var item in vm.Images)
                {
                    string stringFileName = UploadFile(item);
                    var artworkImage = new ArtworkImage
                    {
                        ImageURL = stringFileName,
                        Artwork = vm.Artwork
                    };

                    var artwork = _context.Artworks.Find(vm.Artwork.ArtworkId);
                    artworkImage.Artwork = artwork;
                    _context.ArtworkImages.Add(artworkImage);
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Artworks", new { artworkid = vm.Artwork.ArtworkId });
        }

        private string UploadFile(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);

                // Replace reserved characters with underscores
                char[] invalidChars = Path.GetInvalidFileNameChars();
                originalFileName = originalFileName.Replace(" ", "_");
                string sanitizedFileName = string.Concat(originalFileName.Select(c => invalidChars.Contains(c) ? '_' : c));

                fileName = $"{Guid.NewGuid()}-{sanitizedFileName}{extension}";
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return fileName;
        }


        // GET: ArtworkImages/Edit/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int ArtworkImageId, [Bind("ArtworkImageId,ImageURL")] ArtworkImage artworkImage)
        {
            if (ArtworkImageId != artworkImage.ArtworkImageId)
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
                    if (!ArtworkImageExists(artworkImage.ArtworkImageId))
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? ArtworkImageId)
        {
            if (ArtworkImageId == null || _context.ArtworkImages == null)
            {
                return NotFound();
            }

            var artworkImage = await _context.ArtworkImages
                .FirstOrDefaultAsync(m => m.ArtworkImageId == ArtworkImageId);
            if (artworkImage == null)
            {
                return NotFound();
            }

            return View(artworkImage);
        }

        // POST: ArtworkImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int ArtworkImageId)
        {
            string fileName = null;
            if (_context.ArtworkImages == null)
            {
                return Problem("Entity set 'ArtworkContext.ArtworkImages'  is null.");
            }
            var artworkImage = await _context.ArtworkImages.Include(x => x.Artwork)
                .FirstOrDefaultAsync(m => m.ArtworkImageId == ArtworkImageId);

            var artworkId = artworkImage.Artwork.ArtworkId;
            if (artworkImage != null)
            {
                _context.ArtworkImages.Remove(artworkImage);
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                fileName = artworkImage.ImageURL;
                string filePath = Path.Combine(uploadDir, fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Artworks", new { artworkid = artworkId });
        }

        private bool ArtworkImageExists(int ArtworkImageId)
        {
            return _context.ArtworkImages.Any(e => e.ArtworkImageId == ArtworkImageId);
        }
    }
}
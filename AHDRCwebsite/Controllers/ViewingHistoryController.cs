using AHDRCwebsite.Data;
using AHDRCwebsite.Models;
using AHDRCwebsite.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AHDRCwebsite.Controllers
{
    public class ViewingHistoryController : Controller
    {
        private readonly ArtworkContext _context;

        public ViewingHistoryController(ArtworkContext context)
            {
                _context = context;
            }

            // Action method to add a viewing history record
            [HttpPost]
            public IActionResult AddViewingHistory(string userId, Artwork artwork)
            {
                var viewingHistory = new ViewingHistory
                {
                    UserId = userId,
                    Artwork = artwork,
                    ViewedDateTime = DateTime.Now
                };

                _context.ViewingHistories.Add(viewingHistory);
                _context.SaveChanges();

                return Ok(); // Return an HTTP 200 OK status
            }

            // GET
            [Authorize(Roles = "Administrator")]
            public async Task<IActionResult> Everyone()
            {
                var viewingHistories = (from s in _context.ViewingHistories
                                        select s);
                viewingHistories = viewingHistories.Include(i => i.Artwork);
                viewingHistories = viewingHistories.Include(x => x.Artwork.ArtworkImage);
                viewingHistories = viewingHistories.OrderByDescending(s => s.ViewedDateTime);
                viewingHistories = viewingHistories.Take(50);
            return View(viewingHistories);
            }

            // GET
            public async Task<IActionResult> Single()
            {
                if(User.Identity.IsAuthenticated)
                {
                    var viewingHistories = (from s in _context.ViewingHistories
                                            where s.UserId == User.Identity.Name
                                            select s);
                    viewingHistories = viewingHistories.Include(i => i.Artwork);
                    viewingHistories = viewingHistories.Include(x => x.Artwork.ArtworkImage);
                    viewingHistories = viewingHistories.OrderByDescending(s => s.ViewedDateTime);
                    viewingHistories = viewingHistories.Take(50);
                    return View(viewingHistories);
                }
                else
                {
                    return RedirectToAction("Index", "Artworks");
                }
            }

        }
    }

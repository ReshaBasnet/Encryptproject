using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EncryptCLoud.Models;
using EncryptProject.Data;

namespace EncryptCLoud.Controllers
{
    public class SharedfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SharedfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sharedfiles
        public async Task<IActionResult> Index()
        {
            var encryptappContext = _context.Sharedfile.Include(s => s.Friend).Include(s => s.Image);
            return View(await encryptappContext.ToListAsync());
        }

        // GET: Sharedfiles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedfile = await _context.Sharedfile
                .Include(s => s.Friend)
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharedfile == null)
            {
                return NotFound();
            }

            return View(sharedfile);
        }

        // GET: Sharedfiles/Create
        public IActionResult Create()
        {
            ViewData["FriendId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ImageId"] = new SelectList(_context.File, "Id", "Path");
            return View();
        }

        // POST: Sharedfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FriendId,ImageId")] Sharedfile sharedfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sharedfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FriendId"] = new SelectList(_context.Users, "Id", "UserName", sharedfile.FriendId);
            ViewData["ImageId"] = new SelectList(_context.File, "Id", "Path", sharedfile.ImageId);
            return View(sharedfile);
        }

        // GET: Sharedfiles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedfile = await _context.Sharedfile.FindAsync(id);
            if (sharedfile == null)
            {
                return NotFound();
            }
            ViewData["FriendId"] = new SelectList(_context.Users, "Id", "UserName", sharedfile.FriendId);
            ViewData["ImageId"] = new SelectList(_context.File, "Id", "Path", sharedfile.ImageId);
            return View(sharedfile);
        }

        // POST: Sharedfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FriendId,ImageId")] Sharedfile sharedfile)
        {
            if (id != sharedfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharedfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharedfileExists(sharedfile.Id))
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
            ViewData["FriendId"] = new SelectList(_context.Users, "Id", "UserName", sharedfile.FriendId);
            ViewData["ImageId"] = new SelectList(_context.File, "Id", "Path", sharedfile.ImageId);
            return View(sharedfile);
        }

        // GET: Sharedfiles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedfile = await _context.Sharedfile
                .Include(s => s.Friend)
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharedfile == null)
            {
                return NotFound();
            }

            return View(sharedfile);
        }

        // POST: Sharedfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sharedfile = await _context.Sharedfile.FindAsync(id);
            _context.Sharedfile.Remove(sharedfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SharedfileExists(long id)
        {
            return _context.Sharedfile.Any(e => e.Id == id);
        }
    }
}

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
    public class UserfriendsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserfriendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Userfriends
        public async Task<IActionResult> Index()
        {
            var encryptappContext = _context.Userfriend.Include(u => u.Friend).Include(u => u.User);
            return View(await encryptappContext.ToListAsync());
        }

        // GET: Userfriends/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userfriend = await _context.Userfriend
                .Include(u => u.Friend)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userfriend == null)
            {
                return NotFound();
            }

            return View(userfriend);
        }

        // GET: Userfriends/Create
        public IActionResult Create()
        {
            ViewData["FriendId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Userfriends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FriendId")] Userfriend userfriend)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userfriend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FriendId"] = new SelectList(_context.Users, "Id", "UserName", userfriend.FriendId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userfriend.UserId);
            return View(userfriend);
        }

        // GET: Userfriends/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userfriend = await _context.Userfriend.FindAsync(id);
            if (userfriend == null)
            {
                return NotFound();
            }
            ViewData["FriendId"] = new SelectList(_context.Users, "Id", "UserName", userfriend.FriendId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userfriend.UserId);
            return View(userfriend);
        }

        // POST: Userfriends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,UserId,FriendId")] Userfriend userfriend)
        {
            if (id != userfriend.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userfriend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserfriendExists(userfriend.Id))
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
            ViewData["FriendId"] = new SelectList(_context.Users, "Id", "UserName", userfriend.FriendId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", userfriend.UserId);
            return View(userfriend);
        }

        // GET: Userfriends/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userfriend = await _context.Userfriend
                .Include(u => u.Friend)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userfriend == null)
            {
                return NotFound();
            }

            return View(userfriend);
        }

        // POST: Userfriends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userfriend = await _context.Userfriend.FindAsync(id);
            _context.Userfriend.Remove(userfriend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserfriendExists(long id)
        {
            return _context.Userfriend.Any(e => e.Id == id);
        }
    }
}

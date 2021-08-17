using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EncryptCLoud.Models;
using EncryptProject.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using File = EncryptCLoud.Models.File;
using System.Net.Mime;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using static EncryptProject.Models.encrypt;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.AspNetCore.Authorization;

namespace EncryptCLoud.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        //private readonly encryptappContext _context;
        private readonly ApplicationDbContext _context;

        public FilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var encryptappContext = _context.File.Include(f => f.User);
            return View(await encryptappContext.ToListAsync());
        }

        // GET: Files/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(File file)
        {
            if (ModelState.IsValid)
            {

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.uploads.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                string encPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/encrypted/", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.uploads.CopyTo(fileStream);
                }
                file.Path = uniqueFileName;

                AesOperation aes = new AesOperation();
                aes.EncryptFile(filePath,encPath);
                
                System.IO.File.Delete(filePath);

                _context.Add(file);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", file.UserId);
            return View(file);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", file.UserId);
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, File file)
        {
            if (id != file.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.uploads.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    string encPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/encrypted/", uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.uploads.CopyTo(fileStream);
                    }
                    file.Path = uniqueFileName;

                    AesOperation aes = new AesOperation();
                    aes.EncryptFile(filePath, encPath);

                    System.IO.File.Delete(filePath);

                    _context.Update(file);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", file.UserId);
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var file = await _context.File.FindAsync(id);
            _context.File.Remove(file);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(long id)
        {
            return _context.File.Any(e => e.Id == id);
        }

        [HttpGet]
        public FileResult DownloadFile(string Path)
        {
            string uploadsFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/");
            //Build the File Path.
            string path = System.IO.Path.Combine(uploadsFolder, Path);
            string encPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/encrypted/", Path);

            AesOperation aes = new AesOperation();
            aes.DecryptFile(encPath,path);

            //Read the File data into Byte Array.    
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            var a = File(bytes, "application/octet-stream", Path);

            System.IO.File.Delete(path);

            return a;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;
using DoAnCuoiKi.Areas.Admin.Models;
using Microsoft.IdentityModel.Abstractions;

namespace DoAnCuoiKi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly MyDbContext _context;

        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            var data = await _context.categories
              .Where(item => item.isDelete == false)
              .ToListAsync();
            return View(data);
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            var category = await _context.categories
                .FirstOrDefaultAsync(m => m.categoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryId,name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            var category = await _context.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name)
        {
            var category = await _context.categories.FirstOrDefaultAsync(item => item.categoryId == id);
            if (category == null)
            {
                return View();
            }

            category.name = name;
            _context.categories.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            var category = await _context.categories
                .FirstOrDefaultAsync(m => m.categoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5

        /*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.categories == null)
            {
                return Problem("Entity set 'MyDbContext.categories'  is null.");
            }
            var category = await _context.categories.FindAsync(id);
            if (category != null)
            {
                _context.categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        [HttpPost]
        public async Task<Boolean> HandleDeleteCategoryAdmin(int id)
        {

            var category = await _context.categories.FirstOrDefaultAsync(item => item.categoryId == id);

            if (category == null)
            {
                return false;
            }

            category.isDelete = true;
            _context.categories.Update(category);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CategoryExists(int id)
        {
          return _context.categories.Any(e => e.categoryId == id);
        }

        public async Task<IActionResult> Restore()
        {
            var categories = await _context.categories.Where(item => item.isDelete == true).ToListAsync();

            return View(categories);
        }
        [HttpPost]
        public async Task<Boolean> HandleRestore(int id)
        {
            if (id == null)
            {
                return false;
            }

            var category = await _context.categories.FirstOrDefaultAsync(item => item.categoryId == id);

            if (category == null) { return false; }

            category.isDelete = false;

            _context.categories.Update(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

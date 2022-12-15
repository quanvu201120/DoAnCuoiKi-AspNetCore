using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Authorization;

namespace DoAnCuoiKi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
              return View(await _context.users.Where(item => item.isDelete == false).ToListAsync());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var user = await _context.users
                .FirstOrDefaultAsync(m => m.userId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,phone,email,gender,password,address,role")] User user)
        {
            if (ModelState.IsValid)
            {

                var check = await _context.users.FirstOrDefaultAsync(item => item.email == user.email);

                if (check != null) { return View(user); }

                user.isDelete = false;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("userId,name,phone,email,gender,password,address,role")] User user)
        {
            if (id != user.userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var check = await _context.users.FirstOrDefaultAsync(item => item.email == user.email && item.userId != user.userId);

                    if (check != null) {
                        ViewBag.check = "Email đã tồn tại!";
                        return View(user);
                    }


                    user.isDelete = false;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.userId))
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
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var user = await _context.users
                .FirstOrDefaultAsync(m => m.userId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.users == null)
            {
                return Problem("Entity set 'MyDbContext.users'  is null.");
            }
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<Boolean> HandleDeleteAccountAdmin(int id)
        {

            var account = await _context.users.FirstOrDefaultAsync(item => item.userId == id);

            if (account == null)
            {
                return false;
            }

            account.isDelete = true;
            _context.users.Update(account);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool UserExists(int? id)
        {
          return _context.users.Any(e => e.userId == id);
        }


        public async Task<IActionResult> Restore()
        {
            var users = await _context.users.Where(item => item.isDelete == true).ToListAsync();

            return View(users);
        }
        [HttpPost]
        public async Task<Boolean> HandleRestore(int id)
        {
            if (id == null)
            {
                return false;
            }

            var user = await _context.users.FirstOrDefaultAsync(item => item.userId == id);

            if (user == null) { return false; }

            user.isDelete = false;
            _context.users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }



    }
}

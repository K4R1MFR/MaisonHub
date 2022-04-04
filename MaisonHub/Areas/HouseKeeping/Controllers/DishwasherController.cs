#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaisonHub.Areas.HouseKeeping.Data;
using MaisonHub.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MaisonHub.Areas.HouseKeeping.Controllers
{
    [Area("HouseKeeping"), Authorize(Roles = "Member, SuperUser")]
    public class DishwasherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DishwasherController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HouseKeeping/Dishwasher
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dishwashers.Include(d => d.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HouseKeeping/Dishwasher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishwasher = await _context.Dishwashers
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishwasher == null)
            {
                return NotFound();
            }

            return View(dishwasher);
        }

        // GET: HouseKeeping/Dishwasher/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user is null)
            {
                return NotFound();
            }

            var m = new Models.Dishwasher.Create
            {
                UserId = user.Id,
                Date = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm"))

            };

            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View(m);
        }

        // POST: HouseKeeping/Dishwasher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Date,HasEmptied,HasCleanedFilter")] Models.Dishwasher.Create m)
        {
            if (ModelState.IsValid)
            {
                var dishwasher = new Dishwasher
                {
                    UserId = m.UserId,
                    Date = m.Date,
                    HasEmptied = m.HasEmptied,
                    HasCleanedFilter = m.HasCleanedFilter
                };

                _context.Add(dishwasher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", dishwasher.UserId);
            return View(m);
        }

        // GET: HouseKeeping/Dishwasher/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishwasher = await _context.Dishwashers.FindAsync(id);
            if (dishwasher == null)
            {
                return NotFound();
            }

            var m = new Models.Dishwasher.Edit
            {
                Id = dishwasher.Id,
                UserId = dishwasher.UserId,
                Date = dishwasher.Date,
                HasEmptied = dishwasher.HasEmptied,
                HasCleanedFilter = dishwasher.HasCleanedFilter
            };

            return View(m);
        }

        // POST: HouseKeeping/Dishwasher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Date,HasEmptied,HasCleanedFilter")] Models.Dishwasher.Edit m)
        {
            if (id != m.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dishwasher = new Dishwasher
                    {
                        Id = m.Id,
                        UserId = m.UserId,
                        Date = m.Date,
                        HasEmptied = m.HasEmptied,
                        HasCleanedFilter = m.HasCleanedFilter
                    };

                    _context.Update(dishwasher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishwasherExists(m.Id))
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

            return View(m);
        }

        // GET: HouseKeeping/Dishwasher/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishwasher = await _context.Dishwashers
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishwasher == null)
            {
                return NotFound();
            }

            return View(dishwasher);
        }

        // POST: HouseKeeping/Dishwasher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishwasher = await _context.Dishwashers.FindAsync(id);
            _context.Dishwashers.Remove(dishwasher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishwasherExists(int id)
        {
            return _context.Dishwashers.Any(e => e.Id == id);
        }
    }
}

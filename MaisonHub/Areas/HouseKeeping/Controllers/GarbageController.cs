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

namespace MaisonHub.Areas.HouseKeeping.Controllers
{
    [Area("HouseKeeping")]
    public class GarbageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GarbageController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HouseKeeping/Garbage
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Garbage.Include(g => g.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HouseKeeping/Garbage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garbage = await _context.Garbage
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garbage == null)
            {
                return NotFound();
            }

            return View(garbage);
        }

        // GET: HouseKeeping/Garbage/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var m = new Models.Garbage.Create
            {
                UserId = user.Id,
                Date = DateTime.Parse(DateTime.Now.ToString("f")),
            };

            return View(m);
        }

        // POST: HouseKeeping/Garbage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Date,HasPutNewBag,HasTakenDownBin,HasTakenDownRecycling")] Models.Garbage.Create m)
        {
            if (ModelState.IsValid)
            {
                var garbage = new Garbage
                {
                    UserId = m.UserId,
                    Date = m.Date,
                    HasPutNewBag = m.HasPutNewBag,
                    HasTakenDownBin = m.HasTakenDownBin,
                    HasTakenDownRecycling = m.HasTakenDownRecycling
                };

                _context.Add(garbage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(m);
        }

        // GET: HouseKeeping/Garbage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garbage = await _context.Garbage.FindAsync(id);
            if (garbage == null)
            {
                return NotFound();
            }

            var m = new Models.Garbage.Edit
            {
                Id = garbage.Id,
                UserId = garbage.UserId,
                Date = garbage.Date,
                HasPutNewBag = garbage.HasPutNewBag,
                HasTakenDownBin= garbage.HasTakenDownBin,
                HasTakenDownRecycling= garbage.HasTakenDownRecycling
            };

            return View(m);
        }

        // POST: HouseKeeping/Garbage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Date,HasPutNewBag,HasTakenDownBin,HasTakenDownRecycling")] Models.Garbage.Edit m)
        {
            if (id != m.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var garbage = new Garbage
                    {
                        Id = m.Id,
                        UserId = m.UserId,
                        Date = m.Date,
                        HasPutNewBag = m.HasPutNewBag,
                        HasTakenDownBin = m.HasTakenDownBin,
                        HasTakenDownRecycling = m.HasTakenDownRecycling
                    };
                    _context.Update(garbage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarbageExists(m.Id))
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

        // GET: HouseKeeping/Garbage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garbage = await _context.Garbage
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garbage == null)
            {
                return NotFound();
            }

            return View(garbage);
        }

        // POST: HouseKeeping/Garbage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var garbage = await _context.Garbage.FindAsync(id);
            _context.Garbage.Remove(garbage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarbageExists(int id)
        {
            return _context.Garbage.Any(e => e.Id == id);
        }
    }
}

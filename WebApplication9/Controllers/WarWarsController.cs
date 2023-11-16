using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication9.Data;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class WarWarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarWarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WarWars
        public async Task<IActionResult> Index()
        {
            return View(await _context.WarWar.ToListAsync());
        }

        // GET: WarWars/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View( );
        }

        // GET: WarWars/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPharse)
        {
            return View("Index",await _context.WarWar.Where(j => j.Question.Contains(SearchPharse)).ToListAsync());
        }

        // GET: WarWars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warWar = await _context.WarWar
                .FirstOrDefaultAsync(m => m.id == id);
            if (warWar == null)
            {
                return NotFound();
            }

            return View(warWar);
        }

        // GET: WarWars/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: WarWars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Question,Answer")] WarWar warWar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warWar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warWar);
        }

        // GET: WarWars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warWar = await _context.WarWar.FindAsync(id);
            if (warWar == null)
            {
                return NotFound();
            }
            return View(warWar);
        }

        // POST: WarWars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Question,Answer")] WarWar warWar)
        {
            if (id != warWar.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warWar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarWarExists(warWar.id))
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
            return View(warWar);
        }

        // GET: WarWars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warWar = await _context.WarWar
                .FirstOrDefaultAsync(m => m.id == id);
            if (warWar == null)
            {
                return NotFound();
            }

            return View(warWar);
        }

        // POST: WarWars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warWar = await _context.WarWar.FindAsync(id);
            _context.WarWar.Remove(warWar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarWarExists(int id)
        {
            return _context.WarWar.Any(e => e.id == id);
        }
    }
}

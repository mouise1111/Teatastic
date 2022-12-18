using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Teatastic.Data;
using Teatastic.Models;

namespace Teatastic.Controllers
{
    public class TeasController : Controller
    {
        private readonly TeatasticContext _context;

        public TeasController(TeatasticContext context)
        {
            _context = context;
        }

        // GET: Teas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teas.Include(t => t.Teas_Functions)
                .ToListAsync());

            //return View(await _context.Teas.ToListAsync());

        }

        // GET: Teas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teas == null)
            {
                return NotFound();
            }

            var tea = await _context.Teas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tea == null)
            {
                return NotFound();
            }

            return View(tea);
        }

        // GET: Teas/Create
        public IActionResult Create()
        {
            /*ViewData["FunctionIds"] = new MultiSelectList(_context.Category.OrderBy(c => c.Name), "Id", "Name");
            Media media = new Media();
            return View(media);*/
            return View();
        }
        /*public async Task<IActionResult> Create()
        {
            Functions = await _context.Functions.ToListAsync();
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }*/

        // POST: Teas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Tea tea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tea);
        }

        // GET: Teas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teas == null)
            {
                return NotFound();
            }

            var tea = await _context.Teas.FindAsync(id);
            if (tea == null)
            {
                return NotFound();
            }
            return View(tea);
        }

        // POST: Teas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Tea tea)
        {
            if (id != tea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeaExists(tea.Id))
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
            return View(tea);
        }

        // GET: Teas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teas == null)
            {
                return NotFound();
            }

            var tea = await _context.Teas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tea == null)
            {
                return NotFound();
            }

            return View(tea);
        }

        // POST: Teas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teas == null)
            {
                return Problem("Entity set 'TeatasticContext.Teas'  is null.");
            }
            var tea = await _context.Teas.FindAsync(id);
            if (tea != null)
            {
                _context.Teas.Remove(tea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeaExists(int id)
        {
            return _context.Teas.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(await _context.Tea.Include(t => t.Functions).Include(t => t.Brand).ToListAsync());
        }

        // GET: Teas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tea == null)
            {
                return NotFound();
            }

            var tea = await _context.Tea
                .Include(t => t.Functions)
                .FirstOrDefaultAsync(m => m.Id == id);
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (tea == null)
            {
                return NotFound();
            }

            return View(tea);
        }

        // GET: Teas/Create
        public IActionResult Create()
        {
            ViewData["FunctionIds"] = new MultiSelectList(_context.Function.OrderBy(c => c.Name), "Id", "Name");
            ViewData["BrandId"] = new MultiSelectList(_context.Brands.OrderBy(c => c.Name), "Id", "Name");
            Tea tea = new Tea();
            return View(tea);
        }

        // POST: Teas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,FunctionIds,BrandId")] Tea tea)
        {

            if (ModelState.IsValid)
            {
                if (tea.Functions == null)
                {
                    tea.Functions = new List<Function>();
                }
                foreach (int FunctionId in tea.FunctionIds)
                {
                    tea.Functions.Add(_context.Function.FirstOrDefault(f => f.Id == FunctionId));
                }

                // Add brand to tea
                tea.Brand = _context.Brands.FirstOrDefault(b => b.Id == tea.BrandId);

                _context.Add(tea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tea);
        }

        // GET: Teas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Tea == null)
            {
                return NotFound();
            }

            var tea = await _context.Tea.Include(t => t.Functions).FirstOrDefaultAsync(t => t.Id == id);
            tea.FunctionIds = new List<int>();
            foreach (Function func in tea.Functions)
            {
                tea.FunctionIds.Add(func.Id);
            }
            ViewData["FunctionIds"] = new MultiSelectList(_context.Function.OrderBy(c => c.Name), "Id", "Name", tea.FunctionIds);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,FunctionIds")] Tea tea)
        {
            if (id != tea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Tea existingTea = _context.Tea.Include(t => t.Functions).First(t => t.Id == tea.Id);
                    existingTea.Name = tea.Name;
                    existingTea.Price = tea.Price;
                    existingTea.Functions.Clear();

                    foreach (int funcId in tea.FunctionIds)
                    {
                        existingTea.Functions.Add(_context.Function.FirstOrDefault(c => c.Id == funcId));
                    }
                    _context.Update(existingTea);
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
            if (id == null || _context.Tea == null)
            {
                return NotFound();
            }

            var tea = await _context.Tea
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
            if (_context.Tea == null)
            {
                return Problem("Entity set 'TeatasticContext.Tea'  is null.");
            }
            var tea = await _context.Tea.FindAsync(id);
            if (tea != null)
            {
                _context.Tea.Remove(tea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeaExists(int id)
        {
            return _context.Tea.Any(e => e.Id == id);
        }
    }
}

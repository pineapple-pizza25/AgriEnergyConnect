using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agri_Energy_Connect.Models;

namespace Agri_Energy_Connect.Controllers
{
    public class FarmsController : Controller
    {
        private readonly AgriEnergyConnectContext _context;

        public FarmsController(AgriEnergyConnectContext context)
        {
            _context = context;
        }

        // GET: Farms
        public async Task<IActionResult> Index()
        {
            var agriEnergyConnectContext = _context.Farms.Include(f => f.Farmer);
            return View(await agriEnergyConnectContext.ToListAsync());
        }

        // GET: Farms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farm = await _context.Farms
                .Include(f => f.Farmer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // GET: Farms/Create
        public IActionResult Create()
        {
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "Id", "Id");
            return View();
        }

        // POST: Farms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FarmerId,FarmName,FarmDescription,FarmAddress")] Farm farm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "Id", "Id", farm.FarmerId);
            return View(farm);
        }

        // GET: Farms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farm = await _context.Farms.FindAsync(id);
            if (farm == null)
            {
                return NotFound();
            }
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "Id", "Id", farm.FarmerId);
            return View(farm);
        }

        // POST: Farms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FarmerId,FarmName,FarmDescription,FarmAddress")] Farm farm)
        {
            if (id != farm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmExists(farm.Id))
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
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "Id", "Id", farm.FarmerId);
            return View(farm);
        }

        // GET: Farms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farm = await _context.Farms
                .Include(f => f.Farmer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // POST: Farms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farm = await _context.Farms.FindAsync(id);
            if (farm != null)
            {
                _context.Farms.Remove(farm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmExists(int id)
        {
            return _context.Farms.Any(e => e.Id == id);
        }
    }
}

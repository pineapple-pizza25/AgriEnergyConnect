using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agri_Energy_Connect.Models;
using Agri_Energy_Connect.Utils;

namespace Agri_Energy_Connect.Controllers
{
    public class FarmerCategoriesController : Controller
    {
        private readonly AgriEnergyConnectContext _context;
        readonly UserChecker _userChecker;

        public FarmerCategoriesController(AgriEnergyConnectContext context)
        {
            _context = context;
            _userChecker = new UserChecker();
        }

        // GET: FarmerCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.FarmerCategories.ToListAsync());
        }

        // GET: FarmerCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerCategory = await _context.FarmerCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmerCategory == null)
            {
                return NotFound();
            }

            return View(farmerCategory);
        }

        // GET: FarmerCategories/Create
        public IActionResult Create()
        {
            if (!_userChecker.IsEmployee(HttpContext.Session.GetString("currentUser"), HttpContext.Session.GetString("userRole")))
            { return RedirectToAction("Index", "Home"); }

            return View();
        }

        // POST: FarmerCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Details")] FarmerCategory farmerCategory)
        {
            if (!_userChecker.IsEmployee(HttpContext.Session.GetString("currentUser"), HttpContext.Session.GetString("userRole")))
            { return RedirectToAction("Index", "Home"); }

            if (ModelState.IsValid)
            {
                _context.Add(farmerCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmerCategory);
        }

        // GET: FarmerCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerCategory = await _context.FarmerCategories.FindAsync(id);
            if (farmerCategory == null)
            {
                return NotFound();
            }
            return View(farmerCategory);
        }

        // POST: FarmerCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,Details")] FarmerCategory farmerCategory)
        {
            if (id != farmerCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmerCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerCategoryExists(farmerCategory.Id))
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
            return View(farmerCategory);
        }

        // GET: FarmerCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerCategory = await _context.FarmerCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmerCategory == null)
            {
                return NotFound();
            }

            return View(farmerCategory);
        }

        // POST: FarmerCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmerCategory = await _context.FarmerCategories.FindAsync(id);
            if (farmerCategory != null)
            {
                _context.FarmerCategories.Remove(farmerCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmerCategoryExists(int id)
        {
            return _context.FarmerCategories.Any(e => e.Id == id);
        }
    }
}

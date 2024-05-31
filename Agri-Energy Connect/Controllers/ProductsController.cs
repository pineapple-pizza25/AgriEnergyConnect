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
    public class ProductsController : Controller
    {
        private readonly AgriEnergyConnectContext _context;
        UserChecker userChecker;

        public ProductsController(AgriEnergyConnectContext context)
        {
            _context = context;
            userChecker = new UserChecker();
        }

        public IList<ProductCategory> GetProductCategories()
        {
            return _context.ProductCategories.ToList();
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchBy, string searchValue)
        {
            var agriEnergyContext = _context.Products.Include(b => b.ProductCategory);
            if (string.IsNullOrEmpty(searchValue))
            {
                TempData["InfoMessage"] = "Please provide a search value";
            }
            else
            {
                if (searchBy == "ProductName")
                {
                    var search = agriEnergyContext.Where(p => p.ProductName.ToLower().Contains(searchValue.ToLower()));
                    return View(search);
                }
                else if (searchBy == "ProductCategory")
                {
                    var search = agriEnergyContext.Where(p => p.ProductCategory.CategoryName.Contains(searchValue.ToLower()));
                    return View(search);
                }
                
            }

            return View(await agriEnergyContext.ToListAsync());
        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (userChecker.IsFarmer(HttpContext.Session.GetString("currentUser"), HttpContext.Session.GetString("userRole")) == false)
            { return RedirectToAction("Index", "Home"); ; }

            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id");
            ViewBag.DropdownOptions = GetProductCategories();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Details,Price,ProductionDate,Quantity,Unit,ExpirationDate,ProductCategoryId")] Product product, 
            int categoryId)
        {
            if (userChecker.IsFarmer(HttpContext.Session.GetString("currentUser"), HttpContext.Session.GetString("userRole")) == false) 
            { return RedirectToAction("Index", "Home"); ; }

            product.FarmerId = HttpContext.Session.GetString("currentUser");
            product.ProductCategoryId = categoryId;

            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.ProductCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Details,Price,ProductionDate,Quantity,Unit,ExpirationDate,ProductCategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MyProducts()
        {
            var agriEnergyContext = _context.Products.Include(b => b.Farmer);
            string userId = HttpContext.Session.GetString("currentUser");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["InfoMessage"] = "Please Log In to see your products";
            }
            else
            {
                
                    var myProducts = agriEnergyContext.Where(p => p.Farmer.Id == userId);
                    return View(myProducts);
               
            }

            return View(await agriEnergyContext.ToListAsync());
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

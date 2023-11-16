using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Foodweb.Models;
using Foodweb.Helpper;
using X.PagedList;
using Foodweb.Models.ViewModel;

namespace Foodweb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly FoodContext _context;
        public int PageSize = 9;
       

        public ProductsController(FoodContext context)
        {
            _context = context;
        }

        // GET: Products
        public IActionResult Index(int page=1)
        {
           
            return View(
                new ProductListViewModel
                {
                    Products = _context.Products
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemPerPage = PageSize,
                        CurrentPage = page,
                        TotalItems = _context.Products.Count()
                    }
                }
                );

        }
        [HttpPost]
        public IActionResult Search(string keywords, int page = 1)
        {
            int totalItems = _context.Products
                                 .Where(p => p.ProName.Contains(keywords))
                                 .Count();

            return View("Index",
                new ProductListViewModel
                {
                    Products = _context.Products
                                    .Where(p => p.ProName.Contains(keywords))
                                    .Skip((page - 1) * PageSize)
                                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemPerPage = PageSize,
                        CurrentPage = page,
                        TotalItems = totalItems
                    }
                }
            );
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Cate)
                .FirstOrDefaultAsync(m => m.ProId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CateId"] = new SelectList(_context.Categories, "CateId", "CateId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProId,ProName,ShortDesc,Price,Discout,CateId,CreateDate,ModifiedDate,MetaKey,MetaDesc,BestSeller,Thumb,Active,Title")] Product product, IFormFile thumbFile, IFormFileCollection thumbListFiles)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if(ModelState.IsValid)
            {
                product.ProName = Utilities.ToTitleCase(product.ProName);
                

                if (thumbFile != null)
                {
                    string extension = Path.GetExtension(thumbFile.FileName);
                    string image = Utilities.SEOUrl(product.ProName) + extension;
                    product.Thumb = await Utilities.UploadFile(thumbFile, @"products", image.ToLower());

                }

                if (string.IsNullOrEmpty(product.Thumb))
                {
                    product.Thumb = "default.jpg";
                }

                
                product.Title = Utilities.SEOUrl(product.ProName);
                product.CreateDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
           
            ViewData["CateId"] = new SelectList(_context.Categories, "CateId", "CateId", product.CateId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CateId"] = new SelectList(_context.Categories, "CateId", "CateId", product.CateId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProId,ProName,ShortDesc,Price,Discout,CateId,CreateDate,ModifiedDate,MetaKey,MetaDesc,BestSeller,Thumb,Active,Title")] Product product)
        {
            if (id != product.ProId)
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
                    if (!ProductExists(product.ProId))
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
            ViewData["CateId"] = new SelectList(_context.Categories, "CateId", "CateId", product.CateId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Cate)
                .FirstOrDefaultAsync(m => m.ProId == id);
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
            if (_context.Products == null)
            {
                return Problem("Entity set 'FoodContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProId == id)).GetValueOrDefault();
        }
    }
}

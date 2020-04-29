using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;
using static BookShop.Extensions.StringExtensions;
using System;

namespace BookShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BookShopDbContext _context;

        public CategoriesController(BookShopDbContext context)
        {
            _context = context;
        }

     
        public async Task<IActionResult> CreateOrUpdate(int? Id)
        {
            var model = new Category();

            if (Id.HasValue)
            {
                model = await _context.Categories
               .FirstOrDefaultAsync(m => m.Id == Id);           
            }
            return PartialView("_CreateOrUpdateModalPartial", model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(Category model,int Id)
        {
            if (ModelState.IsValid)
            {
                if (Id == 0)
                {
                    model.Alias = model.Name.ToFriendlyUrl();
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

               
            }

            return PartialView("_CreateOrUpdateModalPartial", model);
        }
        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

       

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Description,CreateDate,UpdateDate")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                category.UpdateDate = DateTime.Now;
                category.Alias= category.Name.ToFriendlyUrl();
                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == Id);
            if (category == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteCategory", category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var category = await _context.Categories.FindAsync(Id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;

namespace BookShop.Areas.Admin.Controllers
{
	using static BookShop.Extensions.StringExtensions;
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		private readonly BookShopDbContext _context;

		public CategoriesController(BookShopDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _context.Categories.OrderByDescending(n => n.CreateDate).ToListAsync();

			return View(model);
		}

		public async Task<IActionResult> CreateOrUpdate(int? id)
		{
			var model = new Category();

			if (id != null)
			{
				model = await _context.Categories.FindAsync(id);

				if (model == null)
				{
					return NotFound();
				}
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Alias,Description,CreateDate,UpdateDate")] Category category)
		{
			if (ModelState.IsValid)
			{
				category.Alias = category.Name.ToFriendlyUrl();
				_context.Add(category);
				TempData["Message"] = "Your caterory was successfully added!";
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

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
				category.Alias = category.Name.ToFriendlyUrl();
				category.UpdateDate = DateTime.Now;
				_context.Update(category);
				TempData["Message"] = "Your caterory was successfully updated!";
				await _context.SaveChangesAsync();


				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var category = await _context.Categories
				.FirstOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			TempData["Message"] = "Your caterory was successfully deleted!";
			return RedirectToAction(nameof(Index));
		}

		
	}
}

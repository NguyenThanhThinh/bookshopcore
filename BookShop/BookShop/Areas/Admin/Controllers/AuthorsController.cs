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
	public class AuthorsController : Controller
	{
		private readonly BookShopDbContext _context;

		public AuthorsController(BookShopDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _context.Authors.OrderByDescending(n => n.CreateDate).ToListAsync();

			return View(model);
		}

		public async Task<IActionResult> CreateOrUpdate(int? id)
		{
			var model = new Author();

			if (id != null)
			{
				model = await _context.Authors.FindAsync(id);

				if (model == null)
				{
					return NotFound();
				}
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Alias,Description,CreateDate,UpdateDate")] Author Author)
		{
			if (ModelState.IsValid)
			{
				Author.Alias = Author.Name.ToFriendlyUrl();
				_context.Add(Author);
				TempData["Message"] = "Your caterory was successfully added!";
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View("~/Areas/Admin/Views/Authors/CreateOrUpdate.cshtml",Author);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Description,CreateDate,UpdateDate")] Author Author)
		{
			if (id != Author.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				Author.Alias = Author.Name.ToFriendlyUrl();
				Author.UpdateDate = DateTime.Now;
				_context.Update(Author);
				TempData["Message"] = "Your caterory was successfully updated!";
				await _context.SaveChangesAsync();


				return RedirectToAction(nameof(Index));
			}
			return View("~/Areas/Admin/Views/Authors/CreateOrUpdate.cshtml", Author);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var Author = await _context.Authors
				.FirstOrDefaultAsync(m => m.Id == id);
			if (Author == null)
			{
				return NotFound();
			}

			return View(Author);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var Author = await _context.Authors.FindAsync(id);
			_context.Authors.Remove(Author);
			await _context.SaveChangesAsync();
			TempData["Message"] = "Your caterory was successfully deleted!";
			return RedirectToAction(nameof(Index));
		}


	}
}

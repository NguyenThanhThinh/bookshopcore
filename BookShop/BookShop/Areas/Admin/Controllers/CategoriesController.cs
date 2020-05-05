using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;
using AutoMapper;
using BookShop.ViewModels.Categories;
using System.Collections.Generic;

namespace BookShop.Areas.Admin.Controllers
{
	using static BookShop.Extensions.StringExtensions;
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		private readonly BookShopDbContext _context;

		private readonly IMapper _mapper;
		public CategoriesController(BookShopDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _context.Categories.OrderByDescending(n => n.CreateDate).ToListAsync();

			var viewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(model);

			return View(viewModel);
		}

		public async Task<IActionResult> CreateOrUpdate(int? id)
		{
			var model = new CreateOrUpdateViewModel();

			if (id != null)
			{
				var data = await _context.Categories.FindAsync(id);

				model = _mapper.Map<CreateOrUpdateViewModel>(data);

				if (model == null)
				{
					return NotFound();
				}
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Alias,Description,CreateDate,UpdateDate")] CreateOrUpdateViewModel category)
		{
			if (ModelState.IsValid)
			{
				category.Alias = category.Name.ToFriendlyUrl();

				category.UpdateDate = DateTime.Now;

				var viewModel = _mapper.Map<Category>(category);

				_context.Add(viewModel);

				TempData["Message"] = "Your caterory was successfully added!";

				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			return View("~/Areas/Admin/Views/Categories/CreateOrUpdate.cshtml", category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Description,CreateDate,UpdateDate")] CreateOrUpdateViewModel category)
		{
			if (id != category.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				
				category.Alias = category.Name.ToFriendlyUrl();

				category.UpdateDate = DateTime.Now;

				var viewModel = _mapper.Map<Category>(category);

				_context.Update(viewModel);

				TempData["Message"] = "Your caterory was successfully updated!";

				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			return View("~/Areas/Admin/Views/Categories/CreateOrUpdate.cshtml", category);
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

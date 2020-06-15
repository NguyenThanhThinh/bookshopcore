using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;
using AutoMapper;
using System.Collections.Generic;
using BookShop.ViewModels.Authors;

namespace BookShop.Areas.Admin.Controllers
{
	using static BookShop.Extensions.StringExtensions;
	[Area("Admin")]
	public class AuthorsController : BaseController
	{
		private readonly BookShopDbContext _context;

		private readonly IMapper _mapper;
		public AuthorsController(BookShopDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _context.Authors.OrderByDescending(n => n.CreateDate).ToListAsync();

			var viewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(model);

			return View(viewModel);
		}

		public async Task<IActionResult> CreateOrUpdate(int? id)
		{
			var model = new CreateOrUpdateAuthorViewModel();

			if (id != null)
			{
				var data = await _context.Authors.FindAsync(id);

				model = _mapper.Map<CreateOrUpdateAuthorViewModel>(data);

				if (model == null)
				{
					return NotFound();
				}
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateOrUpdateAuthorViewModel Author)
		{
			if (ModelState.IsValid)
			{
				Author.Alias = Author.Name.ToFriendlyUrl();

				Author.UpdateDate = DateTime.Now;

				var viewModel = _mapper.Map<Author>(Author);

				_context.Add(viewModel);

				Alert("Lưu danh mục thành công!");
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(nameof(CreateOrUpdate), Author);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, CreateOrUpdateAuthorViewModel Author)
		{
			if (id != Author.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				Author.Alias = Author.Name.ToFriendlyUrl();

				var viewModel = _mapper.Map<Author>(Author);

				_context.Update(viewModel);

				Alert("Lưu danh mục thành công!");

				await _context.SaveChangesAsync();


				return RedirectToAction(nameof(Index));
			}
			return View(nameof(CreateOrUpdate), Author);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			bool sucess = false;

			if (id == 0) return new OkObjectResult(new { id, Success = sucess });

			var author = await _context.Authors.FindAsync(id);

			if (author != null)
			{
				_context.Authors.Remove(author);

				await _context.SaveChangesAsync();

				sucess = true;
			}

			return new OkObjectResult(new { id, Success = sucess });
		}


	}
}

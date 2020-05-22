using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Http;
using BookShop.Extensions;
using AutoMapper;
using BookShop.ViewModels.Books;

namespace BookShop.Areas.Admin.Controllers
{

	public class BooksController : BaseController
	{
		private readonly BookShopDbContext _context;

		private readonly IMapper _mapper;
		public BooksController(BookShopDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		// GET: Admin/Books
		public async Task<IActionResult> Index()
		{
			var books = await _context.Books.Include(b => b.Author).Include(b => b.Category).ToListAsync();

			var viewModel = _mapper.Map<IEnumerable<BookIndexViewModel>>(books);

			return View(viewModel);
		}

		// GET: Admin/Books/Create
		public async Task<IActionResult> CreateOrUpdate(int? id)
		{
			var model = new CreateOrUpdateBookViewModel();

			if (id != null)
			{
				var book = await _context.Books.FindAsync(id);

				model = _mapper.Map<CreateOrUpdateBookViewModel>(book);
				DropdownForm(model);
			}
			else
			{
				DropdownForm(null);
			}

			return View(model);
		}

		private void DropdownForm(CreateOrUpdateBookViewModel book)
		{
			var authors = _context.Authors.Select(n => new
			{
				n.Id,
				n.Name

			}).ToList();

			var categories = _context.Categories.Select(n => new
			{
				n.Id,
				n.Name

			}).ToList();

			ViewData["AuthorId"] = new SelectList(authors, "Id", "Name", book != null ? book.AuthorId : 0);

			ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", book != null ? book.AuthorId : 0);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateOrUpdateBookViewModel book, IFormFile fImage)
		{
			if (ModelState.IsValid)
			{
				if (fImage == null || fImage.Length == 0)
				{
					ModelState.AddModelError("", "Uploaded file is empty or null.");

					DropdownForm(null);

					return View(nameof(CreateOrUpdate), book);
				}
				string image = UploadFileExtensions.UploadFile(fImage, "books");

				if (!string.IsNullOrEmpty(image)) book.Image = image;

				book.Alias = book.Title.ToFriendlyUrl();

				var viewModel = _mapper.Map<Book>(book);

				_context.Add(viewModel);

				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			DropdownForm(book);

			return View(nameof(CreateOrUpdate), book);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, CreateOrUpdateBookViewModel book, IFormFile fImage)
		{
			if (id != book.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				if (string.IsNullOrEmpty(book.Image) && fImage.Length == 0)
				{
					ModelState.AddModelError("", "Uploaded file is empty or null.");

					DropdownForm(null);

					return View(nameof(CreateOrUpdate), book);
				}

				string image = UploadFileExtensions.UploadFile(fImage, "books");

				if (!string.IsNullOrEmpty(image)) book.Image = image;

				book.Alias = book.Title.ToFriendlyUrl();

				var viewModel = _mapper.Map<Book>(book);

				_context.Update(viewModel);

				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			DropdownForm(book);

			return View(nameof(CreateOrUpdate), book);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			bool sucess = false;

			if (id == 0) return new OkObjectResult(new { id, Success = sucess });

			var book = await _context.Books.FindAsync(id);

			if (book != null)
			{
				_context.Books.Remove(book);

				await _context.SaveChangesAsync();

				sucess = true;
			}

			return new OkObjectResult(new { id, Success = sucess });
		}
	}
}

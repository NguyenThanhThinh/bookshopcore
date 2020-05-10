using System;
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
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	
	public class BooksController : Controller
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

		// GET: Admin/Books/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _context.Books
				.Include(b => b.Author)
				.Include(b => b.Category)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// GET: Admin/Books/Create
		public async Task<IActionResult> CreateOrUpdate(int? id)
		{
			var model = new CreateOrUpdateBookViewModel();

			if (id != null)
			{
				var book = await _context.Books.FindAsync(id);

				model = _mapper.Map<CreateOrUpdateBookViewModel>(book);

				ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
				ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
			}
			else
			{
				ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
				ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
			}

			return View(model);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Description,PriceFormatted,Alias,AuthorId,Image,CategoryId,CreateDate,UpdateDate")] CreateOrUpdateBookViewModel book, IFormFile fImage)
		{
			if (ModelState.IsValid)
			{
				if (fImage == null || fImage.Length == 0)
				{
					ModelState.AddModelError("", "Uploaded file is empty or null.");
					return View("~/Areas/Admin/Views/Books/CreateOrUpdate.cshtml", book);
				}
				string image = UploadFileExtensions.UploadFile(fImage, "books");

				if (!string.IsNullOrEmpty(image)) book.Image = image;

				book.Alias = book.Title.ToFriendlyUrl();

				var viewModel = _mapper.Map<Book>(book);
				_context.Add(viewModel);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
			return View("~/Areas/Admin/Views/Books/CreateOrUpdate.cshtml", book);
		}

		// GET: Admin/Books/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _context.Books.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}
			ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
			return View(book);
		}

		// POST: Admin/Books/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PriceFormatted,Alias,AuthorId,Image,CategoryId,CreateDate,UpdateDate")] CreateOrUpdateBookViewModel book, IFormFile fImage)
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
					return View("~/Areas/Admin/Views/Books/CreateOrUpdate.cshtml", book);
				}
				string image = UploadFileExtensions.UploadFile(fImage, "books");

				if (!string.IsNullOrEmpty(image)) book.Image = image;
				book.Alias = book.Title.ToFriendlyUrl();

				var viewModel = _mapper.Map<Book>(book);

				_context.Update(viewModel);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
			return View("~/Areas/Admin/Views/Books/CreateOrUpdate.cshtml", book);
		}

		// GET: Admin/Books/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _context.Books
				.Include(b => b.Author)
				.Include(b => b.Category)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// POST: Admin/Books/Delete/5
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var book = await _context.Books.FindAsync(id);
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();

			return new OkObjectResult(new { id, Success = true });
		}

		private bool BookExists(int id)
		{
			return _context.Books.Any(e => e.Id == id);
		}
	}
}

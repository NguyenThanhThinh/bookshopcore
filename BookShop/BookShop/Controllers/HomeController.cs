using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookShop.Models;
using AutoMapper;
using BookShop.Data;
using Microsoft.EntityFrameworkCore;
using BookShop.ViewModels.Books;

namespace BookShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly BookShopDbContext _context;

		private readonly IMapper _mapper;

		private readonly ILogger<HomeController> _logger;

		public HomeController(BookShopDbContext context, IMapper mapper,

			ILogger<HomeController> logger)
		{
			_logger = logger;
			_context = context;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var books = await _context.Books.
				Include(b => b.Author).
				Include(b => b.Category).ToListAsync();

			var viewModel = _mapper.Map<IEnumerable<BookIndexViewModel>>(books);

			return View(viewModel);
		}
		[Route("book/{id}/{url}")]
		public IActionResult Details(int id)
		{
			if (id == 0) return NotFound();

			var book = _context.Books.Include(b => b.Author).Include(b => b.Category).SingleOrDefault(n => n.Id == id);

			if (book != null)
			{
				var viewModel = _mapper.Map<DetailBookViewModel>(book);

				return View(viewModel);
			}
			return View();
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

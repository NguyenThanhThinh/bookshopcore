using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Areas.Admin.Controllers
{

	public class AccountController : BaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private RoleManager<AppRole> _roleManager;
		private readonly ILogger _logger;
		private readonly IMapper _mapper;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
			, ILogger<AccountController> logger, IMapper mapper, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_mapper = mapper;
			_roleManager = roleManager;
		}
		[HttpGet]
		public IActionResult Login(string ReturnUrl = null)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(AccountLoginViewModel login, string ReturnUrl = null)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(login.UserName);

				if (user == null) return null;

				var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

				if (!result.Succeeded)
				{
					Alert("Đặng nhập không đúng", true);
				}
				else
				{
					var roles = await _userManager.GetRolesAsync(user);

					var claims = new List<Claim> {
						new Claim(ClaimTypes.Name,user.UserName),
						new Claim(ClaimTypes.Role, string.Join(";",roles)),
					};
					// create identity
					ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");

					ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

					await HttpContext.SignInAsync(principal);

					if (Url.IsLocalUrl(ReturnUrl))
					{
						return Redirect(ReturnUrl);
					}

					return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
				}
			}
			return View();
		}

		[HttpPost, AllowAnonymous]
		public async Task<IActionResult> LogOff()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction("Login", "Account", new { Area = "Admin" });
		}
	
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(AccountCreateUpdateViewModel userModel)
		{
			if (!ModelState.IsValid)
			{
				return View(userModel);
			}
			var user = _mapper.Map<AppUser>(userModel);

			var result = await _userManager.CreateAsync(user, userModel.Password);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}

				return View(userModel);
			}

			return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
		}
	}
}
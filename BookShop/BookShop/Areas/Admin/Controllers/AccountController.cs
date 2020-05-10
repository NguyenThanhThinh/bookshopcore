using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.ViewModels.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
		private readonly ILogger _logger;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
			, ILogger<AccountController> logger)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
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

				return RedirectToAction("Index", "Categories",new { Area = "Admin" });
			}
			return View();
		}

		[HttpPost, AllowAnonymous]
		public async Task<IActionResult> LogOff()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Authors", new { Area = "Admin" });
		}
		public async Task<IActionResult> GetUsers()
		{
			var users = await _userManager.Users.ToListAsync();

			return View();
		}
	}
}
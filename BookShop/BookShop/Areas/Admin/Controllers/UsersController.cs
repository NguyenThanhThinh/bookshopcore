﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Accounts;
using BookShop.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Areas.Admin.Controllers
{
	public class UsersController : BaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private RoleManager<AppRole> _roleManager;
		private readonly ILogger _logger;
		private readonly IMapper _mapper;

		public UsersController(UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			RoleManager<AppRole> roleManager,
			ILogger<UsersController> logger, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var users = await _userManager.Users.ToListAsync();

			var viewModel = _mapper.Map<IEnumerable<UserIndexViewModel>>(users);

			return View(viewModel);
		}

		public async Task<IActionResult> CreateOrUpdate(string id)
		{
			var model = new AccountCreateUpdateViewModel();

			var listRoles = await _roleManager.Roles.ToListAsync();

			if (id != null)
			{
				var user = await _userManager.FindByIdAsync(id);

				var userRoles = await _userManager.GetRolesAsync(user);

				model = _mapper.Map<AccountCreateUpdateViewModel>(user);

				model.RolesList = listRoles.Select(x => new SelectListItem()
				{
					Selected = userRoles.Contains(x.Name),
					Text = x.Name,
					Value = x.Name
				});

				if (model == null)
				{
					return NotFound();
				}
			}
			else
			{
				model.RolesList = listRoles.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Name
				});
			}
			return View(model);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AccountCreateUpdateViewModel userModel, params string[] selectedRoles)
		{

			if (!ModelState.IsValid)
			{
				return View(userModel);
			}
			var user = _mapper.Map<AppUser>(userModel);

			var listRoles = await _roleManager.Roles.ToListAsync();

			var createResult = await _userManager.CreateAsync(user, userModel.Password);

			if (createResult.Succeeded)
			{
				if (selectedRoles != null)
				{
					var result = await _userManager.AddToRolesAsync(user, selectedRoles);

					if (!result.Succeeded)
					{

						userModel.RolesList = listRoles.Select(x => new SelectListItem()
						{
							Text = x.Name,
							Value = x.Name
						});
						return View("~/Areas/Admin/Views/Users/CreateOrUpdate.cshtml", userModel);
					}
				}
			}
			else
			{

				userModel.RolesList = listRoles.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Name
				});
				return View("~/Areas/Admin/Views/Users/CreateOrUpdate.cshtml", userModel);

			}
			return View(nameof(Index));
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(AccountCreateUpdateViewModel userModel, params string[] selectedRoles)
		{
			var listRoles = await _roleManager.Roles.ToListAsync();
			if (!ModelState.IsValid)
			{

				return View(nameof(CreateOrUpdate), userModel);
			}
			var user = await _userManager.FindByIdAsync(userModel.Id.ToString());

			if (user == null)
			{
				return NotFound();
			}

			var userRoles = await _userManager.GetRolesAsync(user);

			selectedRoles = selectedRoles ?? new string[] { };

			var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				userModel.RolesList = listRoles.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Name
				});
				return View("~/Areas/Admin/Views/Users/CreateOrUpdate.cshtml", userModel);
			}
			result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				userModel.RolesList = listRoles.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Name
				});
				return View("~/Areas/Admin/Views/Users/CreateOrUpdate.cshtml", userModel);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
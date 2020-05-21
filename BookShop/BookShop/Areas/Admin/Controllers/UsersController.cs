using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Accounts;
using BookShop.ViewModels.Roles;
using BookShop.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

		public async Task<AccountCreateUpdateViewModel> GetRolesAndUserModel(Guid? id)
		{

			var model = new AccountCreateUpdateViewModel();

			var listRoles = await _roleManager.Roles.ToListAsync();

			List<CheckboxRoleViewModel> list = new List<CheckboxRoleViewModel>();

			if (id == null || id == Guid.Empty)
			{
				list = listRoles.Select(x => new CheckboxRoleViewModel()
				{
					Selected = false,
					Text = x.Name,
					Value = x.Name
				}).ToList();
			}
			else
			{

				var user = await _userManager.FindByIdAsync(id.ToString());

				var userRoles = await _userManager.GetRolesAsync(user);

				model = _mapper.Map<AccountCreateUpdateViewModel>(user);

				list = listRoles.Select(x => new CheckboxRoleViewModel()
				{
					Selected = userRoles.Contains(x.Name),
					Text = x.Name,
					Value = x.Name
				}).ToList();

			}

			model.RolesList = list;

			return model;
		}
		public async Task<IActionResult> CreateOrUpdate(Guid? id)
		{
			var model = new AccountCreateUpdateViewModel();

			var data = await GetRolesAndUserModel(id);

			model = data;


			return View(model);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AccountCreateUpdateViewModel userModel, params string[] selectedRoles)
		{

			var data = await GetRolesAndUserModel(userModel.Id);

			userModel.RolesList = data.RolesList;

			if (!ModelState.IsValid)
			{
				return View(nameof(CreateOrUpdate), userModel);
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
						return View(nameof(CreateOrUpdate), userModel);
					}
				}
			}
			else
			{
				return View(nameof(CreateOrUpdate), userModel);

			}
			return View(nameof(Index));
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(AccountCreateUpdateViewModel userModel, params string[] selectedRoles)
		{
			var listRoles = await _roleManager.Roles.ToListAsync();

			var data = await GetRolesAndUserModel(userModel.Id);

			userModel.RolesList = data.RolesList;

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
				return View(nameof(CreateOrUpdate), userModel);
			}
			result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				return View(nameof(CreateOrUpdate), userModel);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
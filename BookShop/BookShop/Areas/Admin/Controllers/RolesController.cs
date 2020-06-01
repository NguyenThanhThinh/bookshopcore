using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookShop.Models;
using BookShop.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
	public class RolesController : BaseController
	{
		private RoleManager<AppRole> _roleManager;
		private readonly IMapper _mapper;
		public RolesController(RoleManager<AppRole> roleManager, IMapper mapper)
		{
			_roleManager = roleManager;
			_mapper = mapper;
		}
		public IActionResult Index()
		{
			var roles = _roleManager.Roles;

			var viewModel = _mapper.Map<IEnumerable<RoleIndexViewModel>>(roles);

			return View(viewModel);
		}

		public async Task<IActionResult> CreateOrUpdate(string id)
		{
			var model = new CreateOrUpdateRoleViewModel();

			if (id != null)
			{
				var data = await _roleManager.FindByIdAsync(id);

				model = _mapper.Map<CreateOrUpdateRoleViewModel>(data);

				if (model == null)
				{
					return NotFound();
				}
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("Id,Name,Description")] CreateOrUpdateRoleViewModel role)
		{
			if (ModelState.IsValid)
			{
				var viewModel = _mapper.Map<AppRole>(role);

				await _roleManager.CreateAsync(viewModel);

				Alert("Lưu vai trò thành công!");


				return RedirectToAction(nameof(Index));
			}
			return View(nameof(CreateOrUpdate), role);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description")] CreateOrUpdateRoleViewModel role)
		{
			if (id != role.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var viewModel = _mapper.Map<AppRole>(role);

				await _roleManager.UpdateAsync(viewModel);

				Alert("Lưu danh mục thành công!");

				return RedirectToAction(nameof(Index));
			}
			return View(nameof(CreateOrUpdate), role);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);

			bool sucess = false;

			if (role != null)
			{
				var result = await _roleManager.DeleteAsync(role);

				if (result.Succeeded)
					sucess = true;
				
			}

			return new OkObjectResult(new { id, Success = sucess });
		}
	}
}
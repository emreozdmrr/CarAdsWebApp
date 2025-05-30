using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Business.Services;
using CarAdsWebApp.Common;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarAdsWebApp.UI.Controllers
{
	[Authorize(Roles = "Admin")]
	public class GearBoxController : Controller
	{

		private readonly IGearBoxService _gearBoxService;
		private readonly IMapper _mapper;

		public GearBoxController(IGearBoxService gearBoxService, IMapper mapper)
		{
			_gearBoxService = gearBoxService;
			_mapper = mapper;
		}


		public async Task<IActionResult> List(int page = 1)
		{
			int pageSize = 5;
			if (TempData["Error"] != null)
				ViewBag.Error = TempData["Error"];
			var response = await _gearBoxService.GearBoxList(page, pageSize);
			if (response.ResponseType == ResponseType.Success)
			{
				return this.ResponseView(response);
			}
			ModelState.AddModelError("", response.Message);
			return View();
		}

		public IActionResult Create()
		{
			return View(new GearBoxCreateDto());
		}

		[HttpPost]
		public async Task<IActionResult> Create(GearBoxCreateDto dto)
		{
			var response = await _gearBoxService.CreateAsync(dto);
			if (response.ResponseType == ResponseType.Success)
			{
				return RedirectToAction("List");
			}
			foreach (var error in response.ValidationErrors)
			{
				ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
			return View(dto);
		}

		public async Task<IActionResult> Update(int id)
		{
			var response = await _gearBoxService.GetByIdAsync<GearBoxUpdateDto>(id);
			if (response.ResponseType == ResponseType.Success)
			{
				return View(response.Data);
			}
			return RedirectToAction("List");
		}


		[HttpPost]
		public async Task<IActionResult> Update(GearBoxUpdateDto dto)
		{
			var response = await _gearBoxService.UpdateAsync(dto);
			if (response.ResponseType == ResponseType.Success)
			{
				ViewBag.Result = "Success";
				return this.ResponseView(response);
			}
			foreach (var error in response.ValidationErrors)
			{
				ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
			return View(dto);
		}


		public async Task<IActionResult> Delete(int id)
		{
			var checkMakeResponse = await _gearBoxService.CheckGearBoxById(id);
			if (checkMakeResponse.ResponseType == ResponseType.Success)
			{
				var deleteResponse = await _gearBoxService.RemoveAsync(id);
				if (deleteResponse.ResponseType == ResponseType.Success)
					return RedirectToAction("List");
				TempData["Error"] = deleteResponse.Message;
				return RedirectToAction("List");
			}
			TempData["Error"] = checkMakeResponse.Message;
			return RedirectToAction("List");
		}
	}
}

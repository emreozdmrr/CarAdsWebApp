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
	public class BodyTypeController : Controller
	{
		private readonly IBodyTypeService _bodyTypeService;
		private readonly IMapper _mapper;

		public BodyTypeController(IBodyTypeService bodyTypeService, IMapper mapper)
		{
			_bodyTypeService = bodyTypeService;
			_mapper = mapper;
		}

		public async Task<IActionResult> List(int page = 1)
		{
			int pageSize = 5;
			if (TempData["Error"] != null)
				ViewBag.Error = TempData["Error"];
			var response = await _bodyTypeService.BodyTypeList(page, pageSize);
			if (response.ResponseType == ResponseType.Success)
			{
				return this.ResponseView(response);
			}
			ModelState.AddModelError("", response.Message);
			return View();
		}

		public IActionResult Create()
		{
			return View(new BodyTypeCreateDto());
		}

		[HttpPost]
		public async Task<IActionResult> Create(BodyTypeCreateDto dto)
		{
			var response = await _bodyTypeService.CreateAsync(dto);
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
			var response = await _bodyTypeService.GetByIdAsync<BodyTypeUpdateDto>(id);
			if (response.ResponseType == ResponseType.Success)
			{
				return View(response.Data);
			}
			return RedirectToAction("List");
		}


		[HttpPost]
		public async Task<IActionResult> Update(BodyTypeUpdateDto dto)
		{
			var response = await _bodyTypeService.UpdateAsync(dto);
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
			var checkMakeResponse = await _bodyTypeService.CheckBodyTypeById(id);
			if (checkMakeResponse.ResponseType == ResponseType.Success)
			{
				var deleteResponse = await _bodyTypeService.RemoveAsync(id);
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

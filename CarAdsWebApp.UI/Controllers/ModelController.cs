using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Business.Services;
using CarAdsWebApp.Common;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks;

namespace CarAdsWebApp.UI.Controllers
{
	[Authorize(Roles = "Admin")]

	public class ModelController : Controller
	{
		private readonly IModelService _modelService;
		private readonly IMapper _mapper;
		private readonly IMakeService _makeService;

		public ModelController(IModelService modelService, IMapper mapper, IMakeService makeService)
		{
			_modelService = modelService;
			_mapper = mapper;
			_makeService = makeService;
		}


		public async Task<IActionResult> List(int page=1)
		{
			int pageSize = 5;
			if (TempData["Error"] != null)
				ViewBag.Error = TempData["Error"];
			var response = await _modelService.ModelList(page, pageSize);
			if (response.ResponseType == ResponseType.Success)
			{
				return this.ResponseView(response);
			}
			ModelState.AddModelError("", response.Message);
			return View();
		}

		public async Task<IActionResult> Create()
		{
            var makeListResponse = await _makeService.MakeListASC();
            ViewBag.Makes = new SelectList(makeListResponse.Data, "Id", "Definition");
			return View(new ModelCreateDto());
		}

		[HttpPost]
		public async Task<IActionResult> Create(ModelCreateDto dto)
		{
			var response = await _modelService.CreateAsync(dto);
			if (response.ResponseType == ResponseType.Success)
			{
				return RedirectToAction("List");
			}
			var makeListResponse = await _makeService.MakeListASC();
			ViewBag.Makes = new SelectList(makeListResponse.Data, "Id", "Definition");
			foreach (var error in response.ValidationErrors)
			{
				ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
			return View(dto);
		}

		public async Task<IActionResult> Update(int id)
		{
			var response = await _modelService.GetByIdAsync<ModelUpdateDto>(id);
			if (response.ResponseType == ResponseType.Success)
			{
				var makeListResponse = await _makeService.MakeListASC();
				ViewBag.Makes = new SelectList(makeListResponse.Data, "Id", "Definition");
				return View(response.Data);
			}
			return RedirectToAction("List");
		}


		[HttpPost]
		public async Task<IActionResult> Update(ModelUpdateDto dto)
		{
			var response = await _modelService.UpdateAsync(dto);
			var makeListResponse = await _makeService.GetAllAsync();
			ViewBag.Makes = new SelectList(makeListResponse.Data, "Id", "Definition");
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
			var checkModelResponse = await _modelService.CheckModelById(id);
			if (checkModelResponse.ResponseType == ResponseType.Success)
			{
				var deleteResponse = await _modelService.RemoveAsync(id);
				if (deleteResponse.ResponseType == ResponseType.Success)
					return RedirectToAction("List");
				TempData["Error"] = deleteResponse.Message;
				return RedirectToAction("List");
			}
			TempData["Error"] = checkModelResponse.Message;
			return RedirectToAction("List");
		}
	}
}

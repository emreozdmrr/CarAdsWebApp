using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Dtos.Interfaces;
using CarAdsWebApp.UI.Extensions;
using CarAdsWebApp.UI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarAdsWebApp.UI.Controllers
{
	public class AdvertisementController : Controller
	{
		private readonly IAdvertisementService _advertisementService;
		private readonly IMakeService _makeService;
		private readonly IModelService _modelService;
		private readonly ICityService _cityService;
		private readonly IBodyTypeService _bodyTypeService;
		private readonly IGearBoxService _gearBoxService;
		private readonly IValidator<AdvertisementCreateModel> _advertisementCreateModelValidator;
		private readonly IValidator<AdvertisementUpdateModel> _advertisementUpdateModelValidator;
		private readonly IMapper _mapper;
		public AdvertisementController(IAdvertisementService advertisementService, IMakeService makeService, IModelService modelService, ICityService cityService, IBodyTypeService bodyTypeService, IGearBoxService gearBoxService, IValidator<AdvertisementCreateModel> advertisementCreateModelValidator, IValidator<AdvertisementUpdateModel> advertisementUpdateModelValidator, IMapper mapper)
		{
			_advertisementService = advertisementService;
			_makeService = makeService;
			_modelService = modelService;
			_cityService = cityService;
			_bodyTypeService = bodyTypeService;
			_gearBoxService = gearBoxService;
			_advertisementCreateModelValidator = advertisementCreateModelValidator;
			_advertisementUpdateModelValidator = advertisementUpdateModelValidator;
			_mapper = mapper;
		}

		[Authorize(Roles = "User")]
		public async Task<IActionResult> ListByUserId()
		{
			var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
			var response = await _advertisementService.ListByAppUserIdAsync(userId);
			if (response.ResponseType == ResponseType.Success)
			{
				return this.ResponseView(response);
			}
			else
			{
				ModelState.AddModelError("", response.Message);
				return View();
			}
		}

		public async void SelectLists()
		{
			var makeListResponse = await _makeService.GetAllAsync();
			ViewBag.Makes = new SelectList(makeListResponse.Data, "Id", "Definition");
			var cityListResponse = await _cityService.GetAllAsync();
			ViewBag.Cities = new SelectList(cityListResponse.Data, "Id", "Definition");
			var bodyTypeListResponse = await _bodyTypeService.GetAllAsync();
			ViewBag.BodyTypes = new SelectList(bodyTypeListResponse.Data, "Id", "Definition");
			var gearBoxListResponse = await _gearBoxService.GetAllAsync();
			ViewBag.GearBoxes = new SelectList(gearBoxListResponse.Data, "Id", "Definition");
		}

		[Authorize(Roles = "User")]
		public IActionResult CreateAdvertisement()
		{
			SelectLists();
			var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
			return View(new AdvertisementCreateModel
			{
				AppUserId = userId
			});
		}

		[Authorize(Roles = "User")]
		[HttpPost]
		public async Task<IActionResult> CreateAdvertisement(AdvertisementCreateModel model)
		{
			var validationResult = _advertisementCreateModelValidator.Validate(model);
			if (validationResult.IsValid)
			{
				var dto = _mapper.Map<AdvertisementCreateDto>(model);

				if (model.ImageUrl != null)
				{
					var fileName = Guid.NewGuid().ToString();
					var extName = Path.GetExtension(model.ImageUrl.FileName);
					string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imageFiles", fileName + extName);
					var stream = new FileStream(path, FileMode.Create);
					await model.ImageUrl.CopyToAsync(stream);
					dto.ImageUrl = "/imageFiles/" + fileName + extName;
				}
				var response = await _advertisementService.CreateAsync(dto);
				if (response.ResponseType == ResponseType.Success)
				{
					return RedirectToAction("Index", "Home");
				}
				foreach (var error in response.ValidationErrors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(model);
			}
			else
			{
				SelectLists();
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(model);
			}
		}

		[Authorize(Roles = "User")]
		public async Task<IActionResult> GetModelsByMakeId(int makeId)
		{
			var response = await _modelService.GetModelsByMakeIdAsync(makeId);
			return Json(response.Data);
		}

		[Authorize(Roles = "User")]
		public async Task<IActionResult> SetStatus(int advertisementId)
		{
			await _advertisementService.SetAdvertisementStatusAsync(advertisementId);
			return Json(new { success = true, redirectUrl = "/Advertisement/ListByUserId" });
		}

		[Authorize(Roles = "User")]
		public async Task<IActionResult> UpdateAdvertisement(int advertisementId)
		{
			var appUserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
			var response = await _advertisementService.CheckAdvertisementById(advertisementId, appUserId);
			if (response.ResponseType == ResponseType.Success)
			{
				SelectLists();
				var model = _mapper.Map<AdvertisementUpdateModel>(response.Data);
				model.currentImageUrl = response.Data.ImageUrl;
				return View(model);
			}
			else
			{
				return RedirectToAction("ListByUserId", "Advertisement");
			}
		}

		[Authorize(Roles = ("User"))]
		[HttpPost]
		public async Task<IActionResult> UpdateAdvertisement(AdvertisementUpdateModel model)
		{
			var validationResult = _advertisementUpdateModelValidator.Validate(model);
			if (validationResult.IsValid)
			{
				var dto = _mapper.Map<AdvertisementUpdateDto>(model);
				dto.ImageUrl = model.currentImageUrl;
				if (model.ImageUrl != null)
				{
					var fileName = Guid.NewGuid().ToString();
					var extName = Path.GetExtension(model.ImageUrl.FileName);
					string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imageFiles", fileName + extName);
					var stream = new FileStream(path, FileMode.Create);
					await model.ImageUrl.CopyToAsync(stream);
					dto.ImageUrl = "/imageFiles/" + fileName + extName;
				}
				var updateResponse = await _advertisementService.UpdateAsync(dto);
				if (updateResponse.ResponseType == ResponseType.Success)
					return this.ResponseRedirectAction(updateResponse, "ListByUserId");
				else
				{
					foreach (var error in updateResponse.ValidationErrors)
					{
						ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
					}
					SelectLists();
					return View(model);
				}
			}
			else
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				SelectLists();
				return View(model);
			}
		}
	}
}

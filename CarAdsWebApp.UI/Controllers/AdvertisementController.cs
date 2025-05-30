using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Dtos.Interfaces;
using CarAdsWebApp.Entities;
using CarAdsWebApp.UI.Extensions;
using CarAdsWebApp.UI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

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
		private readonly IUow _uow;
		public AdvertisementController(IAdvertisementService advertisementService, IMakeService makeService, IModelService modelService, ICityService cityService, IBodyTypeService bodyTypeService, IGearBoxService gearBoxService, IValidator<AdvertisementCreateModel> advertisementCreateModelValidator, IValidator<AdvertisementUpdateModel> advertisementUpdateModelValidator, IMapper mapper, IUow uow)
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
			_uow = uow;
		}

		[Authorize(Roles = "User")]
		public async Task<IActionResult> ListByUserId(int page = 1)
		{
			int pageSize = 5;
			var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
			var response = await _advertisementService.ListByAppUserIdAsync(userId, page, pageSize);
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

		public async Task SelectLists()
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
		public async Task<IActionResult> CreateAdvertisement()
		{
			await SelectLists();
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
					return RedirectToAction("ListByUserId", "Advertisement");
				}
				foreach (var error in response.ValidationErrors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(model);
			}
			else
			{
				await SelectLists();
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
				await SelectLists();
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
					await SelectLists();
					return View(model);
				}
			}
			else
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				await SelectLists();
				return View(model);
			}
		}

		public async Task<IActionResult> ListAdvertisement(int page = 1, string viewType = "Grid", string sortType = "dateDESC", string makeId = "", string modelId = "", AdvertisementSearchModel searchModel = null)
		{
			int pageSize = 8;
			ViewBag.ViewType = viewType;
			ViewBag.SortType = sortType;
			await SelectLists();
			if (!string.IsNullOrEmpty(makeId))
			{
				ViewBag.MakeId = Convert.ToInt32(makeId);
				var modelListResponse = await _modelService.GetModelsWithCountAsync(Convert.ToInt32(makeId));
				if (modelListResponse.ResponseType == ResponseType.Success)
					ViewBag.modelList = modelListResponse.Data;

			}
			if (!string.IsNullOrEmpty(modelId))
				ViewBag.ModelId = Convert.ToInt32(modelId);
			var makeListResponse = await _makeService.MakeListWithCount();
			ViewBag.makeList = makeListResponse.Data;
			var searchDto = _mapper.Map<AdvertisementSearchDto>(searchModel);
			var response = await _advertisementService.ListAdvertisementAsync(sortType, makeId == "" ? 0 : Convert.ToInt32(makeId), modelId == "" ? 0 : Convert.ToInt32(modelId), searchDto, page, pageSize);
			if (response.ResponseType == ResponseType.Success)
			{
				var viewModel = _mapper.Map<IPagedList<AdvertisementListViewModel>>(response.Data);
				if (viewModel.Count > 0)
					viewModel[0].AdvertisementSearch = searchModel;
				return View(viewModel);
			}
			else
			{
				ModelState.AddModelError("", response.Message);
				var model = new List<AdvertisementListViewModel>
				{
					new AdvertisementListViewModel
					{
						AdvertisementSearch = searchModel,
					}
				};
				return View(model.ToPagedList(page, pageSize));
			}
		}


		public async Task<IActionResult> Detail(int id)
		{
			if (User.Identity.IsAuthenticated)
			{
				var appUserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
				ViewBag.AppUserId = appUserId;
			}
			var response = await _advertisementService.DetailByIdAsync(id);
			if (response.ResponseType == ResponseType.Success)
			{
				return View(response.Data);
			}
			ModelState.AddModelError("", response.Message);
			return View();
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ChartByDate()
		{
			var response = await _advertisementService.ChartByDate();
			ViewBag.Labels = response.Data.Labels;
			ViewBag.Data = response.Data.Data;
			return View();
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ChartByMake()
		{
			var response = await _advertisementService.ChartByMake();
			ViewBag.Labels = response.Data.Labels;
			ViewBag.Data = response.Data.Data;
			return View();
		}
	}
}

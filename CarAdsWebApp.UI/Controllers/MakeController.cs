using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Extensions;
using CarAdsWebApp.UI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarAdsWebApp.UI.Controllers
{
	[Authorize(Roles = "Admin")]
	public class MakeController : Controller
	{
		private readonly IMakeService _makeService;
		private readonly IMapper _mapper;

		public MakeController(IMakeService makeService, IMapper mapper)
		{
			_makeService = makeService;
			_mapper = mapper;
		}

		public async Task<IActionResult> List(int page = 1)
		{
			int pageSize = 5;
			if (TempData["Error"] != null)
				ViewBag.Error = TempData["Error"];
			var response = await _makeService.MakeList(page, pageSize);
			if (response.ResponseType == ResponseType.Success)
			{
				return this.ResponseView(response);
			}
			ModelState.AddModelError("", response.Message);
			return View();
		}

		public IActionResult Create()
		{
			return View(new MakeCreateDto());
		}

		[HttpPost]
		public async Task<IActionResult> Create(MakeCreateDto dto)
		{
			var response = await _makeService.CreateAsync(dto);
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
			var response = await _makeService.GetByIdAsync<MakeUpdateDto>(id);
			if(response.ResponseType == ResponseType.Success)
			{
				return View(response.Data);
			}
			return RedirectToAction("List");
		}


		[HttpPost]
		public async Task<IActionResult> Update(MakeUpdateDto dto)
		{
			var response = await _makeService.UpdateAsync(dto);
			if(response.ResponseType == ResponseType.Success)
			{
				ViewBag.Result = "Success";
				return this.ResponseView(response);
			}
			foreach(var error in response.ValidationErrors)
			{
				ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
			return View(dto);
		}


		public async Task<IActionResult> Delete(int id)
		{
			var checkMakeResponse = await _makeService.CheckMakeById(id);
			if(checkMakeResponse.ResponseType == ResponseType.Success)
			{
				var deleteResponse = await _makeService.RemoveAsync(id);
				if (deleteResponse.ResponseType == ResponseType.Success)
					return RedirectToAction("List");
				TempData["Error"] = deleteResponse.Message;
				return RedirectToAction("List");
			}
			TempData["Error"]= checkMakeResponse.Message;
			return RedirectToAction("List");
		}
	}
}

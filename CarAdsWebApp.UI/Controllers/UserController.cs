using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.Common.Enums;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Extensions;
using CarAdsWebApp.UI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarAdsWebApp.UI.Controllers
{
	public class UserController : Controller
	{
		private readonly IValidator<UserCreateModel> _userCreateModelValidator;
		private readonly IAppUserService _appUserService;
		private readonly IMapper _mapper;
		private readonly IValidator<UserUpdateModel> _userUpdateModelValidator;

		public UserController(IValidator<UserCreateModel> userCreateModelValidator, IAppUserService appUserService, IMapper mapper, IValidator<UserUpdateModel> userUpdateModelValidator)
		{
			_userCreateModelValidator = userCreateModelValidator;
			_appUserService = appUserService;
			_mapper = mapper;
			_userUpdateModelValidator = userUpdateModelValidator;
		}

		public IActionResult SignUp()
		{
			return View(new UserCreateModel());
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(UserCreateModel model)
		{
			var result = _userCreateModelValidator.Validate(model);
			if (result.IsValid)
			{
				var dto = _mapper.Map<AppUserCreateDto>(model);
				var createResponse = await _appUserService.CreateUserWithRoleAsync(dto, (int)RoleType.User);
				if (createResponse.ResponseType == ResponseType.Success)
					return this.ResponseRedirectAction(createResponse, "SignIn");
				foreach (var error in createResponse.ValidationErrors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(model);
			}
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
			return View(model);
		}


		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(AppUserLoginDto dto, string returnUrl)
		{
			var result = await _appUserService.CheckUserAsync(dto);
			if (result.ResponseType == ResponseType.Success)
			{
				var roleResult = await _appUserService.GetRolesByUserIdAsync(result.Data.Id);
				var claims = new List<Claim>();
				if (roleResult.ResponseType == ResponseType.Success)
				{
					foreach (var role in roleResult.Data)
					{
						claims.Add(new Claim(ClaimTypes.Role, role.Definition));
					}
				}

				claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));

				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties
				{
					IsPersistent = dto.RememberMe,
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);
				if (!string.IsNullOrEmpty(returnUrl))
					return Redirect(returnUrl);
				return RedirectToAction("ListAdvertisement", "Advertisement");
			}
			ModelState.AddModelError("Kullanıcı Adı veya Şifre hatalı!", result.Message);
			return View(dto);
		}

		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(
				CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("ListAdvertisement", "Advertisement");
		}

		[Authorize(Roles = "User")]
		public async Task<IActionResult> Update()
		{
			var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
			var response = await _appUserService.GetUserByIdAsync(userId);
			if (response.ResponseType == ResponseType.Success)
			{
				var userUpdateModel = _mapper.Map<UserUpdateModel>(response.Data);
				return View(userUpdateModel);
			}
			ModelState.AddModelError("", response.Message);
			return View();
		}

		[Authorize(Roles = "User")]
		[HttpPost]
		public async Task<IActionResult> Update(UserUpdateModel userUpdateModel)
		{
			var validationResult = _userUpdateModelValidator.Validate(userUpdateModel);
			if (validationResult.IsValid)
			{
				var dto = _mapper.Map<AppUserUpdateDto>(userUpdateModel);
				var response = await _appUserService.UpdateAsync(dto);
				if (response.ResponseType == ResponseType.Success)
				{
					ViewBag.Result = "Bilgileriniz başarıyla güncellendi.";
					return View(userUpdateModel);
				}
				ModelState.AddModelError("",response.Message);
				return View(userUpdateModel);
			}
			foreach (var error in validationResult.Errors)
			{
				ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
			}
			return View(userUpdateModel);
		}

		public IActionResult AccessDenied(string returnUrl)
		{
			return View();
		}
	}
}

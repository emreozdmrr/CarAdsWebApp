using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using CarAdsWebApp.UI.Extensions;
using CarAdsWebApp.UI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarAdsWebApp.UI.Controllers
{
	public class MessageController : Controller
	{
		private readonly IMessageService _messageService;
		private readonly IAdvertisementService _advertisementService;
		private readonly IValidator<MessageCreateModel> _createModelValidator;
		private readonly IMapper _mapper;

		public MessageController(IMessageService messageService, IValidator<MessageCreateModel> createModelValidator, IMapper mapper, IAdvertisementService advertisementService)
		{
			_messageService = messageService;
			_createModelValidator = createModelValidator;
			_mapper = mapper;
			_advertisementService = advertisementService;
		}

		[Authorize(Roles = "User")]
		public IActionResult ReceivedMessages(int page = 1)
		{
			int pageSize = 5;
			var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
			var response = _messageService.GetReceivedMessagesByUserId(userId, page, pageSize);
			if (response.ResponseType == ResponseType.Success)
			{
				return this.ResponseView(response);
			}
			ModelState.AddModelError("", response.Message);
			return View();
		}


		[Authorize(Roles = "User")]
		public IActionResult SentMessages(int page = 1)
		{
			int pageSize = 5;
			var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
			var response = _messageService.GetSentMessagesByUserId(userId, page, pageSize);
			if (response.ResponseType == ResponseType.Success)
			{
				return this.ResponseView(response);
			}
			ModelState.AddModelError("", response.Message);
			return View();
		}


		[Authorize(Roles = "User")]
		public async Task<IActionResult> MessageDetails(int advertisementId, int toUserId)
		{
			var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
			var advertisementAppUserId = toUserId;
			var advertisementResponse = await _advertisementService.CheckAdvertisementByAppUserId(advertisementId, advertisementAppUserId);
			if (advertisementResponse.ResponseType == ResponseType.NotFound)
			{
				advertisementResponse = await _advertisementService.CheckAdvertisementByAppUserId(advertisementId, userId);
			}
			if (advertisementResponse.ResponseType == ResponseType.Success)
			{
				var response = _messageService.MessageDetails(userId, advertisementId, toUserId);
				if (response.ResponseType == ResponseType.Success)
				{
					if (response.Data[0].Advertisement.AppUserId == userId)
						ViewBag.User = "Receiver";
					else
						ViewBag.User = "Sender";
					return this.ResponseView(response);
				}
				else
				{
					if (advertisementResponse.Data.AppUser.Id != userId)
					{
						ViewBag.User = "Sender";
						return View(new List<MessageListDto>()
						{
							new MessageListDto
							{
								Advertisement = new AdvertisementListDto
								{
									Id = advertisementId,
								},
								Receiver = new AppUserListDto
								{
									Id = toUserId,
									Firstname = advertisementResponse.Data.AppUser.Firstname,
									Surname = advertisementResponse.Data.AppUser.Surname
								},
								SenderId = userId,
								ReceiverId = toUserId,
							}
						});
					}
					else
					{
						return RedirectToAction("ListAdvertisement", "Advertisement");
					}

				}
			}
			return RedirectToAction("ListAdvertisement", "Advertisement");
		}

		[Authorize(Roles = "User")]
		[HttpPost]
		public async Task<IActionResult> MessageDetails(MessageCreateModel messageCreateModel)
		{
			var userId = messageCreateModel.SenderId;
			var messageDetailsResponse = _messageService.MessageDetails(userId, messageCreateModel.AdvertisementId, messageCreateModel.ReceiverId);
			if (messageDetailsResponse.ResponseType == ResponseType.Success)
			{
				if (messageDetailsResponse.Data[0].Advertisement.AppUserId == userId)
					ViewBag.User = "Receiver";
				else
					ViewBag.User = "Sender";
			}
			else
				ViewBag.User = "Sender";
			var validationResult = _createModelValidator.Validate(messageCreateModel);
			if (validationResult.IsValid)
			{
				var dto = _mapper.Map<MessageCreateDto>(messageCreateModel);
				var response = await _messageService.CreateAsync(dto);
				if (response.ResponseType == ResponseType.Success)
				{
					userId = messageCreateModel.SenderId;
					messageDetailsResponse = _messageService.MessageDetails(userId, messageCreateModel.AdvertisementId, messageCreateModel.ReceiverId);
					return View(messageDetailsResponse.Data);
				}
				foreach (var error in response.ValidationErrors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return View(messageDetailsResponse.Data);
			}
			foreach (var error in validationResult.Errors)
			{
				ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
			if (messageDetailsResponse.Data != null)
				return View(messageDetailsResponse.Data);
			var advertisementResponse = await _advertisementService.CheckAdvertisementByAppUserId(messageCreateModel.AdvertisementId, messageCreateModel.ReceiverId);
			return View(new List<MessageListDto>()
			{
				new MessageListDto
				{
					Advertisement = new AdvertisementListDto
					{
						Id = messageCreateModel.AdvertisementId
					},
					Receiver = new AppUserListDto
					{
						Id = messageCreateModel.ReceiverId,
						Firstname = advertisementResponse.Data.AppUser.Firstname,
						Surname = advertisementResponse.Data.AppUser.Surname
					},
					SenderId = messageCreateModel.SenderId,
					ReceiverId = messageCreateModel.ReceiverId
				}
			});
		}
	}
}

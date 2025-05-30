using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CarAdsWebApp.Business.Services
{
	public class MessageService : Service<MessageCreateDto, MessageUpdateDto, MessageListDto, Message>, IMessageService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		public MessageService(IMapper mapper, IValidator<MessageCreateDto> createDtoValidator, IValidator<MessageUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
		{
			_uow = uow;
			_mapper = mapper;
		}

		public IResponse<IPagedList<MessageListDto>> GetReceivedMessagesByUserId(int userId, int page = 1, int pageSize = 5)
		{
			var query = _uow.GetRepository<Message>().GetQuery();
			var list = query.Include(x => x.Sender).Include(x => x.Receiver).Include(x => x.Advertisement)
				.Where(x=>x.Advertisement.AppUserId == userId && (x.ReceiverId == userId)).ToList()
				.OrderByDescending(x=>x.MessageDate)
				.GroupBy(x => new { x.AdvertisementId, x.SenderId }).Select(x => x.FirstOrDefault()).OrderByDescending(x=>x.MessageDate).ToList();
			if (list.Count > 0)
			{
				var dto = _mapper.Map<List<MessageListDto>>(list);
				return new Response<IPagedList<MessageListDto>>(ResponseType.Success, dto.ToPagedList(page, pageSize));
			}
			return new Response<IPagedList<MessageListDto>>(ResponseType.NotFound, "Mesaj bulunamadı.");
		}

		public IResponse<IPagedList<MessageListDto>> GetSentMessagesByUserId(int userId, int page = 1, int pageSize = 5)
		{
			var query = _uow.GetRepository<Message>().GetQuery();
			var list = query.Include(x => x.Sender).Include(x => x.Receiver).Include(x => x.Advertisement)
				.Where(x=>x.Advertisement.AppUserId != userId && (x.SenderId == userId)).ToList()
				.GroupBy(x => new { x.AdvertisementId, x.SenderId }).Select(x => x.FirstOrDefault()).ToList();
			if (list.Count > 0)
			{
				var dto = _mapper.Map<List<MessageListDto>>(list);
				return new Response<IPagedList<MessageListDto>>(ResponseType.Success, dto.ToPagedList(page, pageSize));
			}
			return new Response<IPagedList<MessageListDto>>(ResponseType.NotFound, "Mesaj bulunamadı.");
		}

		public IResponse<List<MessageListDto>> MessageDetails(int userId, int advertisementId, int senderId)
		{
			var query = _uow.GetRepository<Message>().GetQuery();
			var list = query.Include(x=>x.Sender).Include(x=>x.Receiver).Include(x=>x.Advertisement).ThenInclude(x=>x.Make).Include(x=>x.Advertisement.Model)
				.Where(x=>x.AdvertisementId ==  advertisementId && ((x.SenderId == userId && x.ReceiverId == senderId) || (x.SenderId == senderId && x.ReceiverId == userId)))
				.OrderBy(x=>x.MessageDate).ToList();
            if (list.Count > 0)
            {
				var dto = _mapper.Map<List<MessageListDto>>(list);
				return new Response<List<MessageListDto>>(ResponseType.Success, dto);
            }
			return new Response<List<MessageListDto>>(ResponseType.NotFound, "Mesaj Detayı bulunamadı.");
        }
	}
}

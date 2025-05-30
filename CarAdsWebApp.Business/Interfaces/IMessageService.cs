using CarAdsWebApp.Common;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CarAdsWebApp.Business.Interfaces
{
	public interface IMessageService : IService<MessageCreateDto, MessageUpdateDto, MessageListDto, Message>
	{
		IResponse<IPagedList<MessageListDto>> GetReceivedMessagesByUserId(int userId ,int page, int pageSize);
		IResponse<IPagedList<MessageListDto>> GetSentMessagesByUserId(int userId, int page, int pageSize);
		IResponse<List<MessageListDto>> MessageDetails(int userId, int advertisementId, int senderId);

	}
}

using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Interfaces
{
    public interface IMessageService:IService<MessageCreateDto, MessageUpdateDto,MessageListDto,Message>
    {
    }
}

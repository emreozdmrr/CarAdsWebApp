using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Services
{
    public class MessageService:Service<MessageCreateDto,MessageUpdateDto,MessageListDto,Message>,IMessageService
    {
        public MessageService(IMapper mapper, IValidator<MessageCreateDto> createDtoValidator, IValidator<MessageUpdateDto> updateDtoValidator, IUow uow):base(mapper,createDtoValidator,updateDtoValidator,uow)
        {
            
        }
    }
}

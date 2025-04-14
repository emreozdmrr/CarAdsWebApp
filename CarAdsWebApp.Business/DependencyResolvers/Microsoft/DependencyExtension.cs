using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Business.Mappings.AutoMapper;
using CarAdsWebApp.Business.Services;
using CarAdsWebApp.Business.ValidationRules;
using CarAdsWebApp.DataAccess.Contexts;
using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Local"));
            });

          

            services.AddScoped<IUow, Uow>();

            services.AddTransient<IValidator<AppUserCreateDto>, AppUserCreateDtoValidator>();
            services.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateDtoValidator>();
            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();
            services.AddTransient<IValidator<BodyTypeUpdateDto>, BodyTypeUpdateDtoValidator>();
            services.AddTransient<IValidator<BodyTypeCreateDto>, BodyTypeCreateDtoValidator>();
            services.AddTransient<IValidator<GearBoxUpdateDto>, GearBoxUpdateDtoValidator>();
            services.AddTransient<IValidator<GearBoxCreateDto>, GearBoxCreateDtoValidator>();
            services.AddTransient<IValidator<MakeUpdateDto>, MakeUpdateDtoValidator>();
            services.AddTransient<IValidator<MakeCreateDto>, MakeCreateDtoValidator>();
            services.AddTransient<IValidator<MessageUpdateDto>, MessageUpdateDtoValidator>();
            services.AddTransient<IValidator<MessageCreateDto>, MessageCreateDtoValidator>();
            services.AddTransient<IValidator<ModelUpdateDto>, ModelUpdateDtoValidator>();
            services.AddTransient<IValidator<ModelCreateDto>, ModelCreateDtoValidator>();
            services.AddTransient<IValidator<CityUpdateDto>, CityUpdateDtoValidator>();
            services.AddTransient<IValidator<CityCreateDto>, CityCreateDtoValidator>();
            services.AddTransient<IValidator<AdvertisementCreateDto>, AdvertisementCreateDtoValidator>();
            services.AddTransient<IValidator<AdvertisementUpdateDto>, AdvertisementUpdateDtoValidator>();

            services.AddScoped<IAppUserService, AppUserService>(); 
            services.AddScoped<IBodyTypeService, BodyTypeService>();
            services.AddScoped<IGearBoxService, GearBoxService>();
            services.AddScoped<IMakeService, MakeService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IAdvertisementService,AdvertisementService>();
        }
    }
}

using AutoMapper;
using EWallet.Data.Entities;
using EWallet.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Service.AutoMappers
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<VerifyAccount, VerifyAccountViewModel>();
            //Add more mapper in here
        }
    }
}

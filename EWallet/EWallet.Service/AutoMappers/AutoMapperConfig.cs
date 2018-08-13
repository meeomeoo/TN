﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Service.AutoMappers
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToModelMappingProfile());
            });
        }
    }
}

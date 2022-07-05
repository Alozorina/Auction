﻿using AutoMapper;
using BLL.Models;
using DAL.Entities;
using System;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserRegistrationModel, User>();
            CreateMap<UserPersonalInfoModel, User>();
            CreateMap<User, UserPersonalInfoModel>()
                .ForMember(um => um.BirthDate, opt => opt
                    .MapFrom(user => user.BirthDate != null ? ((DateTime)user.BirthDate).ToString("yyyy-MM-dd") : null));
            CreateMap<UserCreds, User>().ReverseMap();
            CreateMap<User, UserPulicInfo>();
        }
    }
}

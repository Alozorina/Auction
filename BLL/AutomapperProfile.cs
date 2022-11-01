using AutoMapper;
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
            CreateMap<User, UserPulicInfo>()
                .ForMember(upi => upi.Role, opt => opt
                    .MapFrom(u => u.Role.Name));
            CreateMap<UserPassword, User>()
                .ForMember(u => u.Password, opt => opt
                    .MapFrom(up => up.NewPassword));

            CreateMap<Item, ItemPublicInfo>()
                .AfterMap((s, d) =>
                {
                    d.Status.Items = null;
                    foreach (var ic in d.ItemCategories)
                    {
                        ic.Item = null;
                        ic.Category.ItemCategories = null;
                    }
                });

            CreateMap<ItemPublicInfo, Item>()
                .ForMember(i => i.StatusId, opt => opt
                    .MapFrom(ipi => ipi.Status.Id));

            CreateMap<ItemCreateNewEntity, Item>();

            CreateMap<Item, ItemUpdateBid>();
        }
    }
}

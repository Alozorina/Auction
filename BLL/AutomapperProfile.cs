using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserRegistrationModel, User>();
            CreateMap<UserPersonalInfoModel, User>().ReverseMap();
            CreateMap<UserCreds, User>();
        }
    }
}

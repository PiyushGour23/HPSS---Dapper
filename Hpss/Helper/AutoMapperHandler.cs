using AutoMapper;
using Laptop.Models;

namespace Hpss.Helper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler() 
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + "" + src.LastName))
                .ForMember(dest => dest.DateofBirth, opt => opt.MapFrom(src => src.DOB))
                .ReverseMap();
                
        }
    }
}

using AutoMapper;
using PersonelTakipMVC.Models.DTOs;
using PersonelTakipMVC.Models.Entities;

namespace PersonelTakipMVC.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Personel, PersonelDto>().ReverseMap();
            CreateMap<Departman, DepartmanDto>().ReverseMap();

            CreateMap<Personel, PersonelDto>()
                .ForMember(dest => dest.DepartmanAdi, opt => opt.MapFrom(src => src.Departman.DepartmanAdi));
        }
    }
}

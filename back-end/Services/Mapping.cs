using AutoMapper;
using back_end.Models;
using back_end.Models.Dtos;

namespace back_end.Services
{
    public static class Mapping
    {
        public static IMapper Create()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnimalDto, Animal>();
                cfg.CreateMap<VisitDto, Visit>();
            });

            return config.CreateMapper();
        }
    }
}

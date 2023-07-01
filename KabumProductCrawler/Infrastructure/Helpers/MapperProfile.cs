using AutoMapper;
using Domain.Model;
using Entity.Entity;

namespace Infrastructure.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Product, ProductModel>().ReverseMap();
    }
}
using AutoMapper;
using PutProduct.Data;
using PutProduct.Model;

namespace PutProduct.Infrastructure
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(x=>x.UserName,
                    o=>o
                        .MapFrom(x=>x.User.UserName)).
                ReverseMap();
        }
    }
}

using AutoMapper;
using PutProduct.Data;
using PutProduct.Model;
using Profile = AutoMapper.Profile;

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

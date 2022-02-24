using AutoMapper;
using PutProduct.Data;
using PutProduct.Model;

namespace PutProduct.Infrastructure
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<ProductModel, Product>().ReverseMap();
        }
    }
}

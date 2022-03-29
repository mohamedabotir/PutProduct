using AutoMapper;
using PutProduct.Cores.Repositories;
using PutProduct.Data;
using PutProduct.Model;

namespace PutProduct.Units.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        

        public ProductRepository(ApplicationDbContext context,IMapper mapper)
        {
                _context=context;
                _mapper = mapper;
        }
        public int CreateProduct(ProductModel product,string userId)
        {
            product.UserId = userId;
            var prod = _mapper.Map<ProductModel, Product>(product);
            _context.Products?.Add(prod);
              _context.SaveChanges();
            return prod.Id;
        }

        public void UpdateProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductModel> RetriveAllProduct()
        {
            throw new NotImplementedException();
        }

        public ProductModel RetriveProduct(string productId)
        {
            throw new NotImplementedException();
        }
    }
}

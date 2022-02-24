using AutoMapper;
using PutProduct.abstracts.Repository;
using PutProduct.Data;
using PutProduct.Model;

namespace PutProduct.Cores.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> CreateProduct(ProductModel product, string userId)
        {
            var prod = _mapper.Map<ProductModel, Product>(product);

            _context.Products?.Add(prod);
            await _context.SaveChangesAsync();
            return prod.Id;
        }
    }
}

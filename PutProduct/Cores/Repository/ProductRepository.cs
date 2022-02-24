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

        public async Task<int> DeleteProduct(string userId, int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ModifyProduct(ProductModel product, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> RetrieveProduct(ProductModel product, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>?> RetrieveMyProducts(string userId)
        {
            var result = _context.Products?.Where(x => x.UserId == userId);
            return _mapper.Map<IEnumerable<Product>,IEnumerable <ProductModel >>(result);
        }

        public async Task<IEnumerable<Product>> RetrieveAllProducts(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}

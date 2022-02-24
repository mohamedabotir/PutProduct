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
        public async Task<int> CreateProduct(ProductModel product, string? userId)
        {
            var prod = _mapper.Map<ProductModel, Product>(product);

            _context.Products?.Add(prod);
            await _context.SaveChangesAsync();
            return prod.Id;
        }

        public async Task<int> DeleteProduct(string? userId, int productId)
        {
            var product = _context.Products?.
                Where(x => x.UserId == userId && x.Id == productId).SingleOrDefault();
            if (product == null)
                return 0;
            _context.Products?.Remove(product);
           await _context.SaveChangesAsync();
           return product.Id;
        }

        public async Task<int> ModifyProduct(ProductModel product, string? userId)
        {
            var model = _mapper.Map<ProductModel,Product>(product);
            model.UserId = userId;
          var modify = _context.Products?.Update(model);
          await _context.SaveChangesAsync();
          return modify.Entity.Id;
        }

        public async Task<Product> RetrieveProduct(ProductModel product, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>?> RetrieveMyProducts(string? userId)
        {
            var result = _context.Products?.Where(x => x.UserId == userId);
            return _mapper.Map<IEnumerable<Product>,IEnumerable <ProductModel >>(result);
        }

        public async Task<IEnumerable<ProductModel>?> RetrieveAllProducts()
        {

            var result = _context.Products.AsEnumerable();
            return _mapper.Map<IEnumerable<Product>,IEnumerable<ProductModel>>(result);
        }
    }
}

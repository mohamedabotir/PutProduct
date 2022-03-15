using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var models = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (models.UserId != userId)
            {
                return 0;
            }

            models.CategoryId = product.CategoryId;
            models.Description = product.Description;
            models.ImageUrl = product.ImageUrl;
            models.Name = product.Name;
            models.Price = product.Price;
            models.Quantity= product.Quantity;
            var model = _mapper.Map<ProductModel,Product>(product);
             
            var modify = _context.Products?.Update(models);
              _context.SaveChanges();
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

            var result = _context.Products.Include(x=>x.User).Where(e => e.DeletedBy == null).AsEnumerable();
            return _mapper.Map<IEnumerable<Product>,IEnumerable<ProductModel>>(result);
        }

        public async Task<ProductModel>? RetrieveSpecificProduct(int id)
        {
            var product = _context.Products.Include(x=>x.User).SingleOrDefault(x=>x.Id==id);
            var result = _mapper.Map<Product, ProductModel>(product);
            result.UserName = product.User.UserName;
            return result;
        }
    }
}

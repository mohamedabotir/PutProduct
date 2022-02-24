using PutProduct.Data;
using PutProduct.Model;

namespace PutProduct.abstracts.Repository
{
    public interface IProductRepository
    {
        Task<int> CreateProduct(ProductModel product,string? userId);
        Task<int> DeleteProduct(string? userId,int productId);
        Task<int> ModifyProduct(ProductModel product, string? userId);
        Task<Product> RetrieveProduct(ProductModel product, string userId);
        Task<IEnumerable<ProductModel>?> RetrieveMyProducts(string? userId);
        Task<IEnumerable<ProductModel>?> RetrieveAllProducts();
    }
}

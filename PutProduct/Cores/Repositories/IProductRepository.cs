using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PutProduct.Model;

namespace PutProduct.Cores.Repositories
{
    public interface IProductRepository
    {
        int CreateProduct(ProductModel product,string userId);
        void UpdateProduct(ProductModel product);
        void DeleteProduct(ProductModel product);
        IEnumerable<ProductModel> RetriveAllProduct();
        ProductModel RetriveProduct(string productId);
    }
}

using PutProduct.Model;

namespace PutProduct.abstracts.Repository
{
    public interface IProductRepository
    {
        Task<int> CreateProduct(ProductModel product,string userId);
    }
}

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
        Task<ProductModel>? RetrieveSpecificProduct(int id);
        Task<bool> AddOrder(OrderModel id);
        IEnumerable<Order> GetAllOrders();

        Task<CommentModel> Comment(CommentModel comment);

        Task<IEnumerable<CommentModel>> GetProductComment(int ProductId);
        Task<CommentModel> GetComment(int commentId);

        Task<bool> UpdateComment(UpdateCommentModel comment);
        Task<bool> DeleteComment(int id);
    }
}

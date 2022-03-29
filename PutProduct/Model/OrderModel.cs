using PutProduct.Data;

namespace PutProduct.Model
{
    public class OrderModel
    {
        public ICollection<ProductModel> products { set; get; }
        public decimal totalPrice { set; get; }
        public string? discountCode { set; get; }

       
    }
}

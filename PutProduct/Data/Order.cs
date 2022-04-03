using PutProduct.Data.Migrations;
using PutProduct.Model;

namespace PutProduct.Data
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<OrderProducts> OrderProducts { set; get; }
        public decimal totalPrice { set; get; }
        public int? DiscountId { set; get; }
        public Discount Discount { set; get; }
        public DateTime OrderTime { get; set; }


        public string UserId { get; set; }

        public User User { get; set; }


    }
}

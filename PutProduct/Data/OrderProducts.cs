using System.Security.Principal;

namespace PutProduct.Data
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { set; get; }

        public int OrderId { get; set; }
        public int qty { get; set; }


    }
}

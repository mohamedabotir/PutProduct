namespace PutProduct.Data.Migrations
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DiscountValue { set; get; }

        public DateTime ExpireTime { set; get; }
    }
}

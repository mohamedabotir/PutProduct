namespace PutProduct.Model
{
    public class PromoCodeModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double DiscountValue { set; get; }

        public DateTime ExpireTime { set; get; }
        
        public string? CreatedBy { get; set; }
    }
}

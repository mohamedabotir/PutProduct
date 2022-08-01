using PutProduct.abstracts.Models;

namespace PutProduct.Data
{
    public class Discount : IDeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DiscountValue { set; get; }

        public DateTime ExpireTime { set; get; }
        public string? DeletedBy { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }
        public string CreatedBy { get ; set ; }
        public DateTime CreatedOn { get ; set ; }
        public string? ModifiedBy { get ; set ; }
        public DateTime? ModifiedOn { get ; set ; }
    }
}

namespace PutProduct.abstracts.Models
{
    public interface IEntity
    {
        string CreatedBy { set; get; }
        DateTime CreatedOn { set; get; }
        string? ModifiedBy { set; get; }
        DateTime? ModifiedOn { set; get; }
    }
}

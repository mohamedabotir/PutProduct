namespace PutProduct.abstracts.Models
{
    public interface IDeleteEntity :IEntity
    {
        string? DeletedBy { set; get; }
        DateTime? DeletedOn { set; get; }
    }
}

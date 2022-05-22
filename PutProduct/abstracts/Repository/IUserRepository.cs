namespace PutProduct.abstracts.Repository
{
    public interface IUserRepository
    {
        Task<string?> checkUsername(string name);
        Task<string?> checkEmailAddress(string emailAddress);
    }
}

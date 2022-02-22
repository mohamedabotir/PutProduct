namespace PutProduct.Services.jwt
{
    public interface IJwtService
    {
        string JwtGenerate(string userId,string userName,string email);
    }
}

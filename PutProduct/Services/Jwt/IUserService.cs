using PutProduct.Data;
using PutProduct.Model;

namespace PutProduct.Services.Jwt
{
    public interface IUserService
    {
        ResponseUser Authenticate(RequestUser model);
        IEnumerable<User> GetAll();
         Task<User> GetById(string id);
       
    }
}

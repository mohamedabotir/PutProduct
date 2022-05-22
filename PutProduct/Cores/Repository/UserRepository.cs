using Microsoft.EntityFrameworkCore;
using PutProduct.abstracts.Repository;
using PutProduct.Data;

namespace PutProduct.Cores.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string?> checkUsername(string name)
        {
            var userCheck = await _context.User?.
                FirstOrDefaultAsync(e => e.UserName == name)!;
            if (userCheck==null)
            {
                return null;
            }
            return userCheck.UserName;

        }

        public async Task<string?> checkEmailAddress(string emailAddress)
        {
            var checkEmail =await _context.User?.FirstOrDefaultAsync(e => e.Email == emailAddress)!;
            if (checkEmail == null)
                return null;
            return checkEmail.Email;
        }
    }
}

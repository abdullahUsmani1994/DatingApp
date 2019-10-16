using System;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<UserModel> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;
             // var varifiedPassword = VarifyPasswordHash(password,user.PasswordHash, user.PasswordSalt);
            if (!VarifyPasswordHash(password,user.PasswordHash, user.PasswordSalt))
            return null;

            return user;
        }

        private bool VarifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
         using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))// anything inside using is going to be disposed of as soon as we finished with it.
            {
                var Computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
                for (int i = 0; i< Computedhash.Length; i++ )
                {
                    if(Computedhash[i] != passwordHash[i])
                    return false;
                }
            }
            return true;
        }

        public async Task<UserModel> Register(UserModel user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt); // we cannot pass values as a parameter we only pass as a reference so we use out keyword so when these reference are updated in the function they are also upated outside the function.
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())// anything inside using is going to be disposed off as soon as we are finished with it.
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x =>x.Username == username))
            return true;

            return false;
        }
    }
}
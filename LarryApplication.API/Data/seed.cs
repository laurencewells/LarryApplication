using System.Collections.Generic;
using LarryApplication.API.Models;
using Newtonsoft.Json;

namespace LarryApplication.API.Data
{
    public class seed
    {
        private readonly DataContext _context;
        public seed(DataContext context)
        {
            _context = context;

        }

        public void seedusers(){
            var userData = System.IO.File.ReadAllText("Data/UserSeed.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                byte[] passwordHash, paswordSalt;
                CreatePasswordHash("password", out passwordHash, out paswordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = paswordSalt;
                user.Username = user.Username.ToLower();

                _context.Users.Add(user);
            }

            _context.SaveChanges();
        }

                private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
    }
}
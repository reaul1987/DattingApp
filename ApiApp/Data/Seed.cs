using ApiApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ApiApp.Data
{
    public class Seed
    {

        public static async Task SendUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            users.ForEach(async user => {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Abcd1234"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            });

            await context.SaveChangesAsync();
        }
    }
}

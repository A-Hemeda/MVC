using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Identity
{
    public class StoreIdentityDBContextSeed
    {


        public static async Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "heshamfathikamal@gmail.com",
                    DisplayName = "Hesham Fathy",
                    UserName = "Ichatosha",
                    PhoneNumber = "01060349907",
                    Address = new Address()
                    {
                        Firstname = "Hesham",
                        Lastname = "Fathy",
                        City = "Mansoura",
                        Country = "Egypt",
                        Street = "Hay Al Gamaa"
                    }
                };

                await _userManager.CreateAsync(user, "hesham123");
            }
        }
    }
}

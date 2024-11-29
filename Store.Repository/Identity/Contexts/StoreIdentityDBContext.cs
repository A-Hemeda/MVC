using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Identity.Contexts
{
    public class StoreIdentityDBContext : IdentityDbContext<AppUser>
    {

        public StoreIdentityDBContext(DbContextOptions<StoreIdentityDBContext> options) : base(options)
        {
            
        }
    }
}

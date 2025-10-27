
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistance.Identity
{
    public class IdentityAppDbContext : IdentityDbContext
    {
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options)
        {

        }
        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);    //config7 dbcontext 
            builder.Entity<Address>().ToTable("Addresses");


        }
    }
}

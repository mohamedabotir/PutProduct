using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PutProduct.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> User { get; set; }
        public ApplicationDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasOne(x => x.user).
                WithMany(x => x.products).
                HasForeignKey(x => x.userId).OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);
        }
    }
}
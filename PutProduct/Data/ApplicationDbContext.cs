using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PutProduct.abstracts.Models;
using PutProduct.abstracts.Services;
using PutProduct.Data.Migrations;

namespace PutProduct.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly IIdentityService _user;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IIdentityService user)
            : base(options)
        {
            _user = user;
        }
        public DbSet<Product>? Products { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<Discount>? Discounts { get; set; }
        public DbSet<Order>? Orders { get; set; }

        public DbSet<OrderProducts> ? OrderProducts { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }



        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            Invoke();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

       

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Invoke();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasOne(x => x.User).
                WithMany(x => x.Products).
                HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>().OwnsOne(e => e.profile);
            builder.Entity<Order>().
                HasMany(e => e.OrderProducts).WithOne().HasForeignKey(e=>e.OrderId);
           
            builder.Entity<Comment>().Property(e => e.Message).HasMaxLength(250);

            builder.Entity<Product>().HasMany(e => e.Comments).WithOne(e => e.Product);

            builder.Entity<Notification>().HasOne(e => e.Sender).WithMany(e => e.Notifications)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(builder);
        }

        public void Invoke()
        {
           this.ChangeTracker.Entries()
               .ToList().ForEach(e =>
               {
                   var username = _user.GetUserName();
                   if (e.Entity is IDeleteEntity deleteEntity)
                   {
                       if (e.State == EntityState.Deleted)
                       {
                           deleteEntity.DeletedOn = DateTime.UtcNow;
                           deleteEntity.DeletedBy = username;
                           e.State = EntityState.Modified;
                           return;
                       }

                       if (e.State == EntityState.Added)
                       {
                           deleteEntity.CreatedBy = username;
                           deleteEntity.CreatedOn = DateTime.UtcNow;
                           if (e.Entity is Notification notification)
                           {
                               deleteEntity.CreatedBy = username;
                               deleteEntity.CreatedOn = DateTime.UtcNow;
                               notification.SenderId = _user?.GetUserId()!;
                           }
                           if (e.Entity is Comment comment)
                           {
                               comment.UserId = _user?.GetUserId()!;
                           }
                           if (e.Entity is Product prod)
                           {
                               prod.UserId = _user?.GetUserId();
                           }
                       }
                       else if (e.State == EntityState.Modified)
                       {
                           deleteEntity.CreatedBy = username;
                           
                           deleteEntity.ModifiedBy = username;
                           deleteEntity.ModifiedOn = DateTime.UtcNow;
                           if (e.Entity is Notification notification)
                           {
                               notification.isRead = true;
                               
                           }
                       }
                   }
                    
               });
        }
    }
}
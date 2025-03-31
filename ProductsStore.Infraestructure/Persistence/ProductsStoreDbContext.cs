using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProductsStore.Domain.Entities;
using ProductsStore.Infraestructure.Persistence.Seeds;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Infraestructure.Persistence
{
    public class ProductsStoreDbContext : IdentityDbContext<User>
    {
        public ProductsStoreDbContext(DbContextOptions<ProductsStoreDbContext> dbContextOptions)
            :base(dbContextOptions)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.AddRoles();
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }


}

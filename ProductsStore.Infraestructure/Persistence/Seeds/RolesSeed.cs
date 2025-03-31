using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Infraestructure.Persistence.Seeds
{
    public static  class RolesSeed
    {
        public static ModelBuilder AddRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new List<IdentityRole>()
                    {
                        new IdentityRole
                        {
                            Id = "902198e2-c4bc-41e6-9e55-423133261673",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        }
                    }
                
                );

            return modelBuilder;
        }
    }
}

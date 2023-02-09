
using Company.Models;
using Company.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data
{
   public class CompanyDbContext : IdentityDbContext<ApplicationUser>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options ) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwUsers");
            });

        }
        
        public DbSet<VwUser> VwUsers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<About> About { get; set; }
    }
}

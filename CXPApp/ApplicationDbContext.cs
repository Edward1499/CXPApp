using CXPApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CXPApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<PaymentConcept> PaymentConcepts { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<AccountingSeat> AccountingSeat { get; set;}
        public DbSet<DocumentEntry> DocumentEntry { get; set; }
    }
}

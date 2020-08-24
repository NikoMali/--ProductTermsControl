using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductTermsControl.Domain.Entities;

namespace ProductTermsControl.Insfrastructure.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
        //Main Migration and Invoke PM Code: Add-Migration <Name> -Context DataContext -Project ProductTermsControl.Insfrastructure
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<MagazineBranch> MagazineBranches { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductToBranch> ProductToBranches { get; set; }
        public DbSet<ResponsiblePersonsForProduct> ResponsiblePersonsForProducts { get; set; }
        public DbSet<ResponsiblePersonsGroup> ResponsiblePersonsGroups { get; set; }

    }
}